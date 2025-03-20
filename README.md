# Silicon Web App
A web application built with ASP.NET Core MVC – my first project using this technology! In this application, users can register, log in, change their profile picture, update their password, and join courses. There is also an administrator account with the ability to create and manage courses.

## Features
- User Management
- Create a new account (registration)
- Log in and log out
- Change profile picture
- Update password
- Course Administration
- Users can join existing courses
- Administrators can create and manage courses

### Tools and Technologies
This project utilizes several modern tools and frameworks, including:
- ASP.NET Core MVC – To build the server-based application using the MVC architecture.
- Entity Framework Core – For database access and ORM.
- Visual Studio – The primary development tool for the project.
- C# – The main programming language for backend development.
- HTML, CSS, and SCSS – For structuring and styling the user interface.
- JavaScript – To handle client-side interactivity.

### Project Structure
The project is divided into several folders to keep the code organized:

### Infrastructure
Contains database models, migrations, and configurations related to Entity Framework Core.

### Web-API-Camilla
Exposes API endpoints used by the web application. (If the API is used to handle asynchronous requests or integrations.)

### Web-app_Camilla
The ASP.NET Core MVC application itself, including controllers, views, and models.

Other Files
Uppgifter_ASPNET.sln – The solution file for Visual Studio.
.gitignore and .gitattributes – For managing version control and code formatting.
README.md – This file.
Installation and Setup
Follow these steps to run the project locally:

### Configure the Database
Ensure that your database connection is correctly set in appsettings.json or Web.config (depending on the project version). If using LocalDB, make sure the connection string points to the correct file path.

### Usage
User Account
Register a new account or log in with an existing one. After logging in, you can update your profile, change your profile picture, or update your password.

### Course Participation
Explore available courses and join the ones you are interested in.

### Administrator
Log in as an administrator to create new courses and manage existing ones.
