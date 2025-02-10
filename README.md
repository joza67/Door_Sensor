Overview
mrzim-te-working-app is an application designed for real-time monitoring of door sensors, developed using ASP.NET Core and JavaScript. The application allows users to track door status, battery voltage, and event history through a web interface.

Project Structure
bash
Copy
Edit
/mrzim-te-working-app
│-- /Controllers       # API controllers (DoorSensorController)
│-- /Hubs              # SignalR hubs (NotificationHub)
│-- /Models            # Entities (DoorSensorData, ApplicationDbContext)
│-- /wwwroot           # Static files (CSS, JavaScript, images)
│-- /Pages             # Razor pages for the web interface
│-- appsettings.json   # Configuration file (database, API keys)
│-- Program.cs         # Main entry point of the application
Features
Real-time Door Status Monitoring – Displays the state as "Open" (green) or "Closed" (red).
Automatic Dashboard Refresh every 5 seconds.
REST API Integration for fetching sensor data.
SignalR Support for real-time notifications.
Battery Voltage Tracking and event history logging.
Installation and Setup
Prerequisites:

.NET 6+ SDK
SQL Server
Visual Studio 2022
Download the Project:

Download the compressed file mrzim-te-working-app-master.7z and extract it to your desired directory.
Database Configuration:

In the appsettings.json file, update the SQL Server connection string:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YourDatabaseName;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Run the database migrations:

bash
Copy
Edit
dotnet ef database update
Running the Application:

bash
Copy
Edit
dotnet run
The application will be accessible at: http://localhost:5000

API Endpoints
GET /api/DoorSensor/doorstatus: Returns the current door status (open/closed).

json
Copy
Edit
{
  "DoorOpenStatus": 1
}
GET /api/DoorSensor/doorsensordata: Retrieves all sensor records.

json
Copy
Edit
[
  {
    "DeviceName": "Front Door Sensor",
    "DevEUI": "123456789ABCDEF",
    "BatteryVoltage": 3.8,
    "DoorOpenTimes": 5,
    "DoorOpenStatus": 1,
    "LastDoorOpenDuration": 10,
    "Alarm": 0,
    "Timestamp": "2024-02-10T12:45:00Z"
  }
]
SignalR Integration
For real-time notifications using SignalR:

javascript
Copy
Edit
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", function (message) {
    console.log("New notification:", message);
});

connection.start().catch(err => console.error(err.toString()));

To access the door status page in your application, navigate to http://localhost:5000/DoorStatus. In Razor Pages, the URL structure is typically based on the file path within the Pages directory. For example, a page located at Pages/DoorStatus.cshtml would be accessible via /DoorStatus in the URL. If your DoorStatus page is within a subfolder, such as Pages/Status/DoorStatus.cshtml, the URL would be /Status/DoorStatus. Ensure that the DoorStatus page is located appropriately within your project's Pages directory to match the desired URL. 
LEARNRAZORPAGES.COM



Note
This project is currently under development. If you have suggestions or wish to contribute, feel free to open an issue or submit a pull request.

License
This project is licensed under the MIT License. See the LICENSE file for details.

For additional assistance or questions, please open an issue or contact the repository owner.



