import contextlib
import sys
import pdfplumber
import json
import requests
import re
import warnings
import os

# -------------------------
# Ignore warnings (pdf + ssl)
# -------------------------

# 🔥 suppress everything
os.environ["PYTHONWARNINGS"] = "ignore"
warnings.filterwarnings("ignore")


# -------------------------
# Get file path
# -------------------------
if len(sys.argv) < 2:
    print(json.dumps({"error": "No file path provided"}))
    sys.exit(1)

pdf_path = sys.argv[1]

# -------------------------
# Extract text from PDF safely
# -------------------------
full_text = ""

try:
    # 🔥 suppress ONLY pdfplumber noise
    with open(os.devnull, 'w') as devnull:
        with contextlib.redirect_stderr(devnull):
            with pdfplumber.open(pdf_path) as pdf:
                for page in pdf.pages:
                    try:
                        full_text += (page.extract_text() or "") + "\n"
                    except:
                        continue

except Exception as e:
    print(json.dumps({"error": f"PDF read error: {str(e)}"}))
    sys.exit(1)



# -------------------------
# Call Ollama AI
# -------------------------
def extract_with_ai(text):
    try:
        response = requests.post(
            "http://localhost:11434/api/generate",
            json={
                "model": "llama3",
                "prompt": f"""
                You are a document AI.

                STRICT RULES:
                - Return ONLY valid JSON
                - DO NOT use ...
                - DO NOT truncate arrays
                - DO NOT skip values
                - NO explanation
                - NO markdown

                If data is long, include only first 10 rows.

                Format:
                {{
                "document_type": "...",
                "title": "...",
                "data": [...]
                }}

                Document:
                {text[:4000]}
                """,
                "stream": False
            },
            timeout=60
        )

        return response.json().get("response", "")

    except Exception as e:
        return json.dumps({"error": f"AI request failed: {str(e)}"})


# -------------------------
# Run AI
# -------------------------
ai_output = extract_with_ai(full_text)

# -------------------------
# Clean and parse JSON
# -------------------------
def clean_json(text):
    # Remove markdown
    text = re.sub(r"```.*?```", "", text, flags=re.DOTALL)

    # Remove ...
    text = text.replace("...", "")

    # Remove trailing commas
    text = re.sub(r",\s*}", "}", text)
    text = re.sub(r",\s*]", "]", text)

    return text.strip()


try:
    json_match = re.search(r"\{.*\}", ai_output, re.DOTALL)

    if json_match:
        cleaned = clean_json(json_match.group())
        parsed = json.loads(cleaned)
    else:
        parsed = {"type": "unknown", "raw": ai_output}

except Exception as e:
    parsed = {
        "type": "unknown",
        "error": str(e),
        "raw": ai_output
    }

# -------------------------
# Return final output
# -------------------------
print(json.dumps(parsed))