# 📊 ETL File Watcher & Report Generator

A desktop application built with **C# (Windows Forms)** for automated directory monitoring, data extraction from heterogeneous sources (`CSV` and `XML`), real-time data matching, and generating secure reports in **JSON** and **Excel (.xlsx)** formats.

![Demo](docs/demo.gif) *(Replace with a screenshot or GIF of the application)*

---

## 🚀 Key Features

* **Real-Time Monitoring:** Watches for new files in the selected directory using `FileSystemWatcher`, featuring a built-in `Retry Policy` to handle files temporarily locked by other processes.
* **Smart Data Parsing:**
  * Reads customer data from `CSV` (auto-detects `,` and `;` delimiters, case-insensitive headers).
  * Extracts bank card details from `XML` with fault-tolerant node validation.
* **ETL Join & Deduplication:** Automatically joins records by `UserId` and prevents duplicate entries.
* **Data Security (PCI DSS):** Automatically masks bank card numbers during UI display and file export (`411122******1111`).
* **Flexible Export:** Exports the final matched report to **JSON** or a formatted **Excel (.xlsx)** spreadsheet.

---

## 🏗 Project Architecture

The solution follows a layered architecture to ensure testability and decouple business logic from the UI:

* **`JsonGeneratorApp.Core`** — Class library containing core business logic:
  * `Models` — Data models and PAN masking logic.
  * `Parsers` — CSV (`CsvHelper`) and XML (`LINQ to XML`) processors.
  * `Exporters` — Implementation of the *Strategy* pattern (`IReportExporter` for JSON and Excel via `ClosedXML`).
  * `Services` — Thread-safe `FileWatcher` with asynchronous file queue processing.
* **`JsonGeneratorApp.UI`** — Windows Forms client application built using the **MVP (Passive View)** pattern (`MainForm`, `IMainView`, `MainPresenter`).
* **`JsonGeneratorApp.Tests`** — Unit tests (xUnit) covering parsers and data-matching logic.

---

## 📂 Input & Output Examples

### Input `users.csv`
```csv
UserId,Name,SecondName,Number
101,John,Doe,+15551234567
102,Jane,Smith,+15557654321
```

### Input `cards.xml`
```xml
<?xml version="1.0" encoding="utf-8"?>
<Cards>
  <Card UserId="101">
    <Pan>4111222233334444</Pan>
    <ExpDate>2028-12-31</ExpDate>
  </Card>
</Cards>
```

### Output `report.json`
```json
{
  "Records": [
    {
      "UserId": 101,
      "FirstName": "John",
      "LastName": "Doe",
      "Phone": "+15551234567",
      "MaskedPan": "411122******4444",
      "ExpDate": "2028-12-31T00:00:00"
    }
  ]
}
```

---

## 🛠 Tech Stack

* **Language & Framework:** C# / .NET Framework 4.8 (or .NET 8)
* **UI:** Windows Forms (MVP pattern)
* **Libraries:**
  * `CsvHelper` — CSV file parsing
  * `Newtonsoft.Json` — JSON serialization
  * `ClosedXML` — Excel report generation

---

## 💻 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com
   ```
2. Open the solution file `JsonGeneratorApp.sln` in **Visual Studio**.
3. Restore NuGet packages and run the `JsonGeneratorApp.UI` project.
4. Click **"Select Folder..."** and drop your test `.csv` and `.xml` files into the chosen directory.