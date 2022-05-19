# Student Tracker

## Building the Database

1. Install docker
2. Run the following command in your terminal to spin up a mssql 2019 server instance:
    ```docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuperDuperSecret123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest```
3. Check if dotnet ef tools are installed by running the following in your terminal:
    ```dotnet ef```
   - If installed continue to the next step.
   - If not installed run the following command:
    ```dotnet tool install --global dotnet-ef```
4. Navigate to the following folder in your terminal:
    ```.\student-tracker\StudentTracker\StudentTracker.DAL```
5. Run the following two commands:
    1. ```dotnet ef migrations add --startup-project ../StudentTracker.Web init```
    2. ```dotnet ef database update --startup-project ../StudentTracker.Web```
