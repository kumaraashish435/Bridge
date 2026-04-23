# Bridge — PDF Data Extraction SaaS

Vision is a backend-powered SaaS system that allows users to upload PDF files, process them using Python, and extract structured data.

---

##  Tech Stack

* **Backend:** ASP.NET Core (.NET 9)
* **Database:** PostgreSQL
* **ORM:** Entity Framework Core
* **Python:** pdfplumber, pandas
* **Architecture:** Hybrid (.NET + Python processing)

---

##  Project Structure

```
Bridge/
├── Bridge.Backend/        # Backend Api
├── Bridge.Client/         # frontend ( Angular )
├── Bridge.Infra/          # Python extraction logic
│   ├── src/
│   │   └── main.py
│   └── .venv/             # Python virtual environment
```

---

# Local Setup Guide

Follow these steps to run the project locally.

---

## Clone Repository

```bash
git clone <https://github.com/kumaraashish435/Bridge.git>
cd Bridge
# create branch
git checkout -b "branch_name"
```

---

## Setup Backend (.NET)

###  Install Dependencies 
# .net 9 sdk should installed

```bash
cd Bridge.Backend
dotnet restore
```

---

### Configure Database (PostgreSQL)

Create database:

```sql
CREATE DATABASE "Bridge_db";
```

---

### Update `appsettings.json`

```json
"ConnectionStrings": {
  "BridgeConnectionString": "Host=localhost;Port=5432;Database=Bridge_db;Username=postgres;Password=yourpassword"
}
```

---

### Run Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

##  Setup Python Environment

### Navigate to Infra

```bash
cd ../Bridge.Infra
```

---

###  Create Virtual Environment

```bash
python3 -m venv .venv
```

---

###  Activate Virtual Environment

#### macOS / Linux:

```bash
source .venv/bin/activate
```

#### Windows:

```bash
.venv\Scripts\activate
```

---

### Install Python Dependencies

```bash
pip install pdfplumber pandas openpyxl
```

---

##  Verify Python Script

Run manually:

```bash
.venv/bin/python src/main.py ../Bridge.Backend/uploads/sample.pdf
```

---

##  Configure Python Path in Backend

Update `PythonService.cs`:

```csharp
var pythonPath = "/full/path/to/Bridge.Infra/.venv/bin/python";
```

Example:

```csharp
var pythonPath = "/Users/kumar/Desktop/Bridge/Bridge/Bridge.Infra/.venv/bin/python";
```

---

##  Run Backend

```bash
cd ../Bridge.Backend
dotnet run
```

---

##  Test API (Swagger / Curl)

### Upload PDF

```bash
curl -X POST http://localhost:5073/api/File/upload \
  -F "file=@sample.pdf"
```

---

## Expected Response

```json
{
  "fileId": "...",
  "status": "completed",
  "data": "{...json...}"
}
```

---

# Features

* Upload PDF files
* Extract text using Python
* Store structured data in PostgreSQL
* API-based processing
* Scalable SaaS architecture

---

#  Common Issues & Fixes

###  Python not found

-> Ensure correct `.venv` path in `PythonService`

---

###  Database error

-> Run migrations:

```bash
dotnet ef database update
```

---

### PDF parsing error

-> Some PDFs are malformed — handled safely in Python

---

### File path issue

-> Always use absolute paths

---

#  Future Improvements(pending)

* AI-based data extraction
* Invoice parsing (number, total, date)
* Async job queue (Hangfire)
* Frontend dashboard
* Multi-file processing


#  Vision

> Transform unstructured documents into actionable data.
