# Environmental Complaint Management System

Desktop-based Database Application using ADO.NET and SQL Server.

---

## Project Overview

The Environmental Complaint Management System is a desktop-based application developed to digitalize environmental and public service complaint management processes. The system allows citizens to submit complaints digitally while enabling administrators to verify, monitor, update, and manage complaint records efficiently.

The application was developed using:

- C#
- Windows Forms (.NET Framework)
- ADO.NET
- SQL Server

---

# Objectives

The system was designed with the following objectives:

- Allow citizens to submit complaints easily
- Help administrators manage complaint data efficiently
- Provide complaint status tracking
- Store complaint information using a structured relational database
- Demonstrate implementation of database concepts using ADO.NET

---

# User Roles

## 1. Citizen (User)

Users can:

- Register account
- Login
- Submit complaints
- View submitted complaints
- Search submitted complaints
- Track complaint status

---

## 2. Admin (Officer)

Admins can:

- Login
- View all complaints
- Insert complaint records
- Update complaint status
- Delete complaints
- Search complaints
- Verify complaints

---

# Core Features

## Complaint Submission

Citizens can submit complaints with:
- title
- description
- category
- report date

---

## Complaint Status Tracking

Complaint statuses include:

- Submitted
- Verified
- In Process
- Resolved
- Rejected

---

## Complaint Management

Admins can:
- insert complaints
- update complaints
- delete complaints
- search complaints
- verify complaints

---

## Complaint Search System

The system contains:
- safe search implementation
- unsafe search implementation

for educational SQL Injection comparison.

---

## Data Navigation

The system uses:
- BindingSource
- BindingNavigator

for record navigation inside DataGridView.

Implemented in:
- Admin Panel
- User Panel

---

# Database Design

The database contains the following main tables:

| Table | Purpose |
|------|------|
| users | Store user/admin accounts |
| complaints | Store complaint records |
| categories | Store complaint categories |
| status | Store complaint status values |

---

# Relationships

- One user can submit multiple complaints
- Each complaint belongs to one category
- Each complaint has one status

---

# Technologies Used

| Technology | Purpose |
|------|------|
| C# | Application development |
| Windows Forms | User Interface |
| ADO.NET | Database connectivity |
| SQL Server | Database management |
| SSMS | Database administration |

---

# UCP2 Improvements

The system was upgraded in UCP2 with advanced database concepts.

---

# 1. Stored Procedures

The following Stored Procedures were implemented:

| Procedure | Purpose |
|------|------|
| sp_InsertComplaint | Insert complaint |
| sp_UpdateComplaint | Update complaint |
| sp_DeleteComplaint | Delete complaint |
| sp_SearchComplaint | Search complaints |
| sp_ViewUserComplaints | View user complaints |
| sp_SearchUserComplaints | Safe user complaint search |

---

# 2. SQL View

A SQL View was created:

```sql
vw_Complaints
```

Purpose:
- simplify complex JOIN queries
- improve query readability
- centralize complaint display logic

The VIEW includes:
- complaint information
- user information
- category information
- complaint status information

---

# 3. BindingSource and BindingNavigator

Implemented in:
- Admin Panel
- User Panel

Purpose:
- easier navigation
- better WinForms data binding architecture

---

# 4. SQL Injection Demonstration

The system contains both:
- secure search implementation
- intentionally vulnerable search implementation

for educational comparison purposes.

---

# SQL Injection Scenario

## Safe Search

The secure implementation uses:

- Stored Procedures
- Parameterized Queries

Example:

```csharp
cmd.Parameters.AddWithValue("@search",
    txtSearch.Text.Trim());
```

Purpose:
- prevent malicious SQL execution
- protect database records
- maintain user data isolation

---

## Unsafe Search

The vulnerable implementation directly concatenates user input into SQL queries.

### Vulnerable Code

```csharp
string query =
    "SELECT * FROM vw_Complaints " +
    "WHERE user_id = " +
    Session.UserID +
    " AND title LIKE '%" +
    txtSearch.Text.Trim() +
    "%'";
```

---

## SQL Injection Input

```sql
%' OR 1=1 --
```

---

## Result

The malicious input modifies the SQL query and bypasses filtering conditions.

As a result:
- all complaint records become visible
- user isolation is broken
- other users' complaint data can be accessed

---

## Why It Happens

The query directly concatenates user input into SQL syntax without parameterization.

---

## Prevention

SQL Injection is prevented using:
- parameterized queries
- stored procedures

---

# Validation Features

The system contains multiple validation mechanisms.

---

## Registration Validation

- Empty field validation
- Email format validation
- Name validation
- Password minimum length validation
- Confirm password validation
- Duplicate email validation

---

## Complaint Validation

- Empty field validation
- ComboBox selection validation
- Complaint title minimum length validation
- Complaint description minimum length validation
- Complaint title symbol restriction validation
- Grid selection validation before update/delete
- Search input validation

---

## Rejection Validation

If complaint status is set to:

```text
Rejected
```

then:
- rejection reason becomes required
- rejection reason must contain at least 5 characters

---

## Date Validation

Admin complaint dates are restricted to:
- current date
- previous 100 years only

The system blocks:
- future dates
- unrealistic historical dates

---

## Character Restriction Validation

Complaint titles allow only:
- letters
- digits
- spaces

Special symbols are blocked such as:

```text
@
#
$
%
```

---

## SQL Injection Prevention

Most database operations use:
- parameterized queries
- stored procedures

to prevent malicious SQL execution.


---

## Dashboard Analytics

The application includes an administrative dashboard that provides a visual overview of complaint statistics.

### Dashboard Features

* Total Complaints
* Verified Complaints
* Resolved Complaints
* Rejected Complaints
* Complaint Distribution by Category

### Purpose

* Improve data visibility
* Provide quick statistical summaries
* Support administrative decision-making

---

## Report Generation

The system supports professional report generation using Crystal Reports.

### Report Features

* Category-based Complaint Reports
* Complete Complaint Listings
* Dynamic Report Filtering
* Printable Report Format
* Summary Totals

### Technologies Used

* Crystal Reports
* ADO.NET DataTable Integration
* SQL Server LocalDB

### Purpose

* Generate professional reports
* Simplify complaint analysis
* Support documentation and auditing activities

---

## Excel Data Import

The application supports importing complaint data from Microsoft Excel files.

### Import Features

* Import from `.xlsx` files
* Bulk Data Insertion
* Automatic Data Validation
* Error Handling During Import

### Benefits

* Reduce manual data entry
* Improve efficiency for large datasets
* Support external data integration

---

## Database Deployment Automation

To simplify deployment on different computers, the system includes automated database setup features.

### Deployment Features

* SQL Server LocalDB Integration
* Automatic LocalDB Instance Creation
* Automatic Database Restoration
* One-Click Installation Process

### Benefits

* Simplified deployment
* Reduced manual configuration
* Improved portability across systems

---

## Crystal Reports Integration

Crystal Reports was integrated to provide enterprise-style reporting capabilities.

### Implemented Features

* Dynamic Report Generation
* Category Filtering
* Summary Footer Calculations
* Print-Ready Layout Design

### Benefits

* Better data presentation
* Improved reporting workflow
* Enhanced administrative usability


---
# Security Features

- Parameterized queries
- Stored Procedures
- SQL Injection prevention
- Confirmation dialogs before update/delete
- Input validation
- Controlled date selection
- User complaint isolation

---

# System Workflow

```text
Citizen Login/Register
        ↓
Submit Complaint
        ↓
Complaint Stored in Database
        ↓
Admin Reviews Complaint
        ↓
Status Updated
        ↓
Citizen Tracks Complaint Progress
```

---

# Screenshots

Add screenshots here:

- Connection Form
- <img width="745" height="488" alt="image" src="https://github.com/user-attachments/assets/95eaf899-1e47-4dfa-8de4-a02da5b6d74c" />

- Login Form
- <img width="746" height="487" alt="image" src="https://github.com/user-attachments/assets/7433ff23-a827-456b-9f58-454d92415bb1" />

- Registration Form
- <img width="747" height="490" alt="image" src="https://github.com/user-attachments/assets/2ad2db21-1c04-486d-ad8a-23bfafec92d1" />

- User Panel
- <img width="987" height="695" alt="image" src="https://github.com/user-attachments/assets/f84e62b8-6c0a-43e6-840f-ba4026940f35" />

- Admin Panel
- <img width="1068" height="697" alt="image" src="https://github.com/user-attachments/assets/722a9d7c-06cd-44d1-9cf3-40bf5bea1a2a" />

- Search
- <img width="1068" height="697" alt="image" src="https://github.com/user-attachments/assets/7d42cdd3-f1a6-44d1-845f-0c6a7a6c1e43" />

- SQL Injection Demonstration
- <img width="1076" height="696" alt="image" src="https://github.com/user-attachments/assets/7ce0ee06-8f05-4803-91b4-5031972fcdc0" />
- 
- Stored Procedures
- <img width="342" height="399" alt="image" src="https://github.com/user-attachments/assets/876838ed-c137-4834-bbac-dec71bd245bf" />



---

# Project Structure

```text
F14_ComplaintRegisterSystem
│
├── Forms
│   ├── FrmLogin
│   ├── FrmRegister
│   ├── FrmUserPanel
│   ├── FrmAdminLogin
│   └── FrmComplaint
│
├── Database
│   ├── Stored Procedures
│   ├── VIEW
│   └── Tables
│
└── Utilities
    ├── DBHelper
    └── Session
```

---

# Authors

| Name | Student ID |
|------|------|
| Md Sazzad Hossain Sohag | 20240140253 |
| Muhammad Ilyas | 20240140147 |
| Nimra Tariq | 20240140146 |

---

# Lecturer

Apriliya Kurnianti S.T., M.Eng

---

# Conclusion

The Environmental Complaint Management System successfully demonstrates the implementation of relational database concepts using ADO.NET and SQL Server in a desktop application environment. The project integrates CRUD operations, Stored Procedures, SQL Views, Data Binding, Validation, secure search systems, and SQL Injection demonstration to provide a structured and efficient complaint management workflow.
