# Cross-Platform Expense Tracker 

A multi-platform expense management application built using **.NET MAUI** (Multi-platform App UI). This application allows users to log, categorize, and track their daily expenses from a single C# codebase that compiles natively to Android, iOS, macOS, and Windows.

## Key Features

* **Cross-Platform Deployment:** Write once, run anywhere. The application provides a native user experience across mobile (iOS/Android) and desktop (Windows/MacCatalyst) environments.
* **MVVM Architecture:** Clean separation of concerns utilizing the Model-View-ViewModel design pattern, ensuring highly maintainable and testable code.
* **Reactive UI:** User interfaces designed with XAML, featuring responsive data binding that updates the UI in real-time as the underlying view models change.
* **Local Data Persistence:** Utilizes local storage mechanisms (`ExpenseStorage.cs`) to securely save user financial data on the device without requiring an active internet connection.

## Tech Stack

* **Language:** C#
* **Framework:** .NET 8.0 MAUI
* **Architecture:** MVVM (Model-View-ViewModel)
* **UI Markup:** XAML

## How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/expense-tracker-maui.git
   ```
2. Open the Project2MIP.sln solution file in Visual Studio (ensure the .NET MAUI workload is installed).
3. Build and run the application.

## What I Learned
This project introduced me to cross-platform development using .NET MAUI. I learned how to structure an application using the MVVM design pattern, work in XAML, and handle platform-specific configurations while sharing over 90% of the business logic across multiple operating systems.
