\# HospitalApp



Hospital appointment scheduling system — my project for the

ASP.NET Core Fundamentals course at SoftUni.



\## Description

A web application for a hospital that manages:

\- \*\*Departments\*\* — the hospital units (Cardiology, Neurology, etc.)

\- \*\*Doctors\*\* — each doctor belongs to a department

\- \*\*Appointments\*\* — patients book a visit with a doctor at a future date and time



\## Features

\- Full CRUD (Create, Read, Update, Delete) for Departments, Doctors and Appointments

\- Dropdown selection of the department for each doctor

\- Dropdown selection of the doctor for each appointment

\- Server-side and client-side validation (required fields, email, phone, date must be in the future)

\- Navigation bar with links to all pages

\- Responsive design with Bootstrap

\- On first run the database is created, migrated and seeded automatically — no manual commands needed



\## Technologies

\- ASP.NET Core MVC (.NET 10)

\- Entity Framework Core + SQL Server (LocalDB)

\- Razor views (layout, partial views, sections)

\- Bootstrap 5



\## How to run

1\. Install Visual Studio 2022/2026 (Community edition is fine) with the \*\*ASP.NET and web development\*\* workload (includes .NET SDK and SQL Server LocalDB)

2\. Clone the repository:

&#x20;  git clone https://github.com/IvanaGosheva5/HospitalApp.git

3\. Open \*\*HospitalApp.sln\*\*

4\. Press \*\*Ctrl + F5\*\* — the database is created and filled with sample data automatically

5\. Use the navigation menu: Отделения (Departments) / Лекари (Doctors) / Записвания (Appointments)



\## Configuration

\- The connection string is in `appsettings.json` and uses Windows Authentication (Trusted\_Connection) — \*\*no usernames, passwords or environment variables are needed\*\*

\- On first start the app runs the EF Core migrations and seeds sample departments and doctors



\## Author

Ivana Gosheva

