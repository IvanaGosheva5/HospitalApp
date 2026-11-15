# HospitalApp

Hospital appointment scheduling system – my project for the ASP.NET Core Fundamentals course at SoftUni.

## Description
A web application for a hospital that manages:
- **Departments** – the hospital units (Cardiology, Neurology, etc.)
- **Doctors** – each doctor belongs to a department
- **Appointments** – patients book a visit with a doctor at a future date and time

## Features
- Full CRUD (Create, Read, Update, Delete) for Departments, Doctors and Appointments
- Dropdown selection of the department for each doctor and of the doctor for each appointment
- Server-side and client-side validation (required fields, length, email, phone, date must be in the future, unique department name, unique doctor email)
- Protection against deleting a department that has doctors or a doctor that has appointments
- Navigation bar with links to all pages
- Responsive design with Bootstrap 5
- On first run the database is created, migrated and seeded automatically

## Technologies
- ASP.NET Core MVC (.NET 10)
- Entity Framework Core 10 + SQL Server (LocalDB)
- Razor views (layout, partial views, sections)
- Bootstrap 5, jQuery Validation Unobtrusive

## Project structure

HospitalApp
├── Controllers   – Departments, Doctors, Appointments, Home
├── Data          – ApplicationDbContext, DbInitializer (seed data)
├── Models        – Department, Doctor, Appointment
├── Migrations    – EF Core migrations
└── Views         – Razor views, layout and partial views


## How to run
**Requirements:** .NET 10 SDK and SQL Server LocalDB (installed with Visual Studio and the *ASP.NET and web development* workload).

1. Clone the repository:
   
   git clone https://github.com/IvanaGosheva5/HospitalApp.git
   cd HospitalApp
   
2. **Option A – Visual Studio:** open `HospitalApp.slnx` and press **Ctrl + F5**.

   **Option B – command line:**
   
   dotnet restore
   dotnet run --project HospitalApp
   
   Then open the address shown on the `Now listening on:` line.
3. Optionally the database can be created manually before the first start:
   
   dotnet tool install --global dotnet-ef
   dotnet ef database update --project HospitalApp
   

## Configuration
- The connection string is in `HospitalApp/appsettings.json`:
  
  Server=(localdb)\MSSQLLocalDB;Database=HospitalAppDb;Trusted_Connection=True;TrustServerCertificate=True
  
- It uses Windows Authentication – **no usernames, passwords or environment variables are needed**.
- On first start the app applies the migrations and seeds 5 departments, 3 doctors and 3 appointments.

## Author
Ivana Gosheva