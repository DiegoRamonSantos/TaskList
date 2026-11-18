# 📋 Tasklist

A web application built with **ASP.NET Core Razor Pages** to manage tasks in a simple, organized, and responsive way. The project was created to help users keep track of daily activities, priorities, deadlines, filters, and progress through a visual dashboard.

## 📌 About the Project

**Tasklist** is a practical solution for anyone who wants to organize commitments, pending tasks, and goals in one place. The application allows users to create, edit, complete, and delete tasks, while providing a quick overview of what still needs to be done and what has already been completed.

The application was built with a focus on usability, simplicity, and a smooth user experience, while keeping the business logic organized and the interface clean.

## ✨ Key Features

- ✅ Full task management (CRUD)
- ✅ Edit and delete tasks
- ✅ Mark tasks as completed or pending
- ✅ Priority levels: Low, Medium, High, and Urgent
- ✅ Set due dates for each task
- ✅ Dashboard with visual summaries of pending, completed, and upcoming tasks
- ✅ Filters by search term, status, priority, and date range
- ✅ Task sorting using multiple criteria
- ✅ Local data persistence with SQLite
- ✅ Responsive interface for different screen sizes

## 🛠️ Technologies Used

### Backend

- **ASP.NET Core 10.0**
- **C#**
- **Razor Pages**
- **Entity Framework Core 10.0**
- **SQLite**

### Frontend

- **Razor Views**
- **HTML5**
- **Custom CSS**
- **JavaScript**

## 🧱 Project Architecture and Structure

The application follows a simple and organized layered structure:

- **Pages**: Razor pages responsible for navigation and UI rendering
- **Models**: Entities and input/output models
- **Services**: Application logic and business rules
- **Data**: Entity Framework DbContext and migrations
- **wwwroot**: Static assets such as CSS and JavaScript files

## ⚙️ System Requirements

Before running the project, make sure you have installed:

- **.NET 10 SDK**
- **Git** (optional, for cloning the repository)

## 🚀 Running Locally

### 1. Clone the Repository

```bash
git clone https://github.com/DiegoRamonSantos/TaskList.git
cd tasklist
```

Alternatively, download the repository as a ZIP file and extract it to your preferred folder.

### 2. Open the Project in Visual Studio

Install **Visual Studio 2026** with the **ASP.NET and Web Development** workload and the **.NET 10 SDK**. Open the `Tasklist.slnx` file in the project folder. Visual Studio will automatically restore the NuGet dependencies when loading or building the solution.

### 3. Run the Application

In Visual Studio, select the `https` launch profile from the toolbar and press **F5**. To run without debugging, press **Ctrl+F5**.

The application will open in your browser. With the `https` profile, the configured URLs are:

- `https://localhost:7226`
- `http://localhost:5142`

### 4. Basic Usage

1. Open the home page and choose the option to create a new task.
2. Fill in the title, description, priority, and due date (if needed).
3. Save the task.
4. Use the available filters to find specific tasks.
5. Mark tasks as completed as your work progresses.

## 🗄️ Database

The project uses **SQLite**, and the database file is automatically generated at:

```text
Data/taskflow.db
```

- Migrations are applied automatically when the application starts.

## 📝 Notes

- The application is designed for local and straightforward use, without requiring external services.
- The project is ideal for learning purposes, portfolio projects, and future expansion into a more complete task management solution.

## 👨‍💻 Author

**Diego Ramon**