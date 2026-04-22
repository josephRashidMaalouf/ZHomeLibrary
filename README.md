# Z Home Library

Z Home Library is a cross-platform app for managing your personal at-home library. It helps you organize books, track borrowers, and keep up with lent books in a simple and practical interface.

## Download

You can download Z Home Library from the Microsoft Store here:

https://apps.microsoft.com/detail/9p2hblr8dshm?hl=en-US&gl=US

## Features

- Manage your personal home book collection
- Add and organize books
- Register and manage borrowers
- Lend out books and keep track of who has them
- Get reminders for due returns
- Available in Swedish and English

## Built With

Z Home Library is built with:

- **.NET MAUI**
- **C#**
- **MVVM architecture**
- **SQLite** for local data storage

## Platform Support

This project targets:

- Android
- iOS
- Mac Catalyst
- Windows

> Note: the notification feature is not supported on Windows.

## Project Structure

- `ZHomeLibrary.sln` — solution file
- `ZHomeLibraryShellApp/` — main .NET MAUI application
  - `Pages/` — application pages for books, borrowers, and lending
  - `Models/` — app data models
  - `Managers/` — app management logic
  - `DataAccess/` — database and persistence logic
  - `Resources/` — images, fonts, icons, and splash assets
  - `Platforms/` — platform-specific code

## Main Screens

The app includes pages for:

- Book shelf management
- Book details
- Borrower management
- Borrower details
- Lending out books

## Development

To build and run this project locally, you will need:

- .NET 8 SDK
- .NET MAUI workload installed
- Visual Studio 2022 or later with MAUI support

### Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/josephRashidMaalouf/ZHomeLibrary.git
   ```

2. Open the solution:
   ```bash
   ZHomeLibrary.sln
   ```

3. Build and run the app using your preferred target platform.

## About

Z Home Library was created as a straightforward tool for people who want an easy way to manage their books at home without relying on complex library systems.

## License

No license has been specified for this repository.
