🧭 Registration Wizard (Angular + .NET)

A multi-step registration wizard built with Angular 20, Angular Material, and ASP.NET Core Web API.
This project demonstrates best practices for onboarding: company → industry → user setup, with validation and secure account creation.

✨ Features

🔹 Step-by-step registration flow with Angular Material Stepper.

🔹 Live username availability check.

🔹 Secure password handling with ASP.NET Identity.

🔹 Data validation on both front-end (Angular Reactive Forms) and back-end (Data Annotations).

🔹 API tested with Swagger UI.

🔹 SQL Server database with Entity Framework Core migrations.

🛠️ Tech Stack
Frontend

Angular 20

Angular Material

TypeScript

Reactive Forms

Backend

ASP.NET Core 8 Web API

Entity Framework Core

ASP.NET Identity

SQL Server

📂 Project Structure
/ (repo root)
│
├── backend/ (RegistWizard.Api)
│   ├── Controllers/       # API endpoints
│   ├── Models/            # Domain models
│   ├── Dtos/              # Data transfer objects
│   ├── AppDbContext.cs    # EF Core context
│   ├── Program.cs         # Startup configuration
│   └── ...
│
└── frontend/ (regist-wizard Angular app)
    ├── src/app/           # Components & services
    ├── styles.scss        # Global styles
    ├── src/main.ts          # Angular entry point (bootstraps AppComponent)
    └── ...

🚀 Getting Started
1. Clone the repo
git clone https://github.com/GeorgeCsd/registration-wizard.git
cd registration-wizard

2. Backend setup
cd backend
dotnet ef database update   # apply migrations
dotnet run


Backend runs on 👉 http://localhost:5009

3. Frontend setup
cd frontend
npm install
ng serve


Frontend runs on 👉 http://localhost:4200

🔗 API Endpoints

GET /api/industry → List industries

GET /api/user/check-username?username=... → Check availability

POST /api/registration → Submit registration

Swagger UI available at 👉 http://localhost:5009/swagger

📸 UI Preview

Step 1: Company name & industry dropdown

Step 2: User details, password & validation

Step 3: Summary & accept Terms/Privacy before submitting



🧑‍💻 Development Notes

Frontend & backend run with separate commands (dotnet run, ng serve).

Make sure CORS allows http://localhost:4200 in backend.

You can use npm run dev with concurrently
for one-command dev experience.