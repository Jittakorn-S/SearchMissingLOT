# Search Missing LOT

"Search Missing LOT" is a .NET application designed for efficiently searching directories by name.

## Features

* **Quick Search**: Easily find files and folders by name.
* **Multiple Directory Search**: Configure and search across multiple directories by listing them in the `ListPaths.txt` file.
* **User-Friendly Interface**: A simple and intuitive interface for a seamless user experience.
* **Asynchronous Search**: The application remains responsive while searching, thanks to background worker implementation.
* **Direct Access**: Double-click on a search result to directly open its location in the file explorer.
* **Error Logging**: Any errors during the search process are logged in `LogError.txt` for easy troubleshooting.

## How to Use

1.  **Configuration**:
    * Open the `ListPaths.txt` file and add the directory paths you want to include in the search. Each path should be on a new line.
    * The `App.config` file contains settings for the list paths and log file, which can be customized if needed.

2.  **Launch the application**:
    * Run the `SearchMissingLOT.exe` file.

3.  **Search**:
    * Enter the name of the file or directory you are looking for in the "Input LOT No." field.
    * Click the search icon or press `Enter` to begin the search.

4.  **View Results**:
    * The search results will be displayed in the list box.
    * If a file or directory is not found, a "This LOT was not found...!!" message will be displayed.
    * You can then double-click on any item in the list to open its location.

## Screenshots

*(Placeholder for screenshots of the application)*

## Built With

* [.NET Framework](https://dotnet.microsoft.com/)
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
* [Windows Forms](https://docs.microsoft.com/en-us/dotnet/framework/winforms/)

## Future Improvements

* Implement more advanced search filters (e.g., by date, size, file type).
* Add a feature to preview files directly within the application.
* Enhance the user interface with more visual themes and customization options.

---
