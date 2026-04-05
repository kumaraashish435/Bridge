import sys
import os
import pdfplumber
import json

# ✅ Get file path from backend
if len(sys.argv) < 2:
    print(json.dumps({"error": "No file path provided"}))
    sys.exit(1)

pdf_path = sys.argv[1]

# ✅ Check file exists
if not os.path.exists(pdf_path):
    print(json.dumps({"error": f"File not found: {pdf_path}"}))
    sys.exit(1)

# ✅ Extract data
data = {
    "pages": [],
    "full_text": ""
}

with pdfplumber.open(pdf_path) as pdf:
    for i, page in enumerate(pdf.pages):
        text = page.extract_text() or ""
        clean_text = text.strip()

        data["pages"].append({
            "page": i + 1,
            "text": clean_text
        })

        data["full_text"] += clean_text + "\n"

# ✅ IMPORTANT: Only JSON output
print(json.dumps(data))