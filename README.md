Al-Musawi Bank Web Application - User Guide 📖
Welcome to the Al-Musawi Bank Web Application! This guide will walk you through the steps to get your banking application up and running. Follow these instructions, and you'll be managing your finances with ease in no time!

Prerequisites ✅
Before you start, ensure you have the following installed:

Node.js (which comes with npm)
.NET Core SDK
Angular CLI
SQL Server (if running the database locally)
An IDE or code editor of your choice (like Visual Studio, VSCode, etc.)
Backend Setup 🖥️
Database Setup:

Navigate to your SQL Server Management Studio.
Open the provided SQL script SQLQuery_1.sql.
Execute the script to create your database and tables.
API Configuration:

Open the appsettings.json file.
Update the BankConnectionString with your database connection string.
Run the API:

Open a terminal in the API project directory.
Run dotnet restore to restore any missing dependencies.
Run dotnet run to start the API.
Make sure the API is running on http://localhost:5240.
Frontend Setup 🌐
Install Dependencies:

Navigate to the Angular project directory.
Run npm install to install the required npm packages.
Environment Configuration:

Open the environment.ts file.
Ensure the API URL matches your backend setup (by default, it should be http://localhost:5240/api).
Start the Angular Application:

Run ng serve to start the Angular application.
Open your browser and navigate to http://localhost:4200.
Usage 🚀
Registration & Login:

Use the Sign Up page to create a new user account.
After registration, use the Sign In page to access your account.
Upon successful login, your session will be stored in session storage.
Dashboard:

Once logged in, you can view your dashboard by navigating to /dashboard.
Your account and transaction options are available from the dashboard.
Session Persistence:

The application uses session storage to maintain your session state.
If you refresh the page, you will not be logged out.
Security:

The application uses JWT tokens for secure authentication.
Sensitive data such as passwords are hashed for security.
Troubleshooting 🛠️
If you face any issues:

Check the console for any errors.
Ensure the API and Angular app are running on the correct ports.
Make sure you have executed the SQL script to set up your database.
