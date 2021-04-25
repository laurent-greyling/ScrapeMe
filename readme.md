# Scraper

This project is intended to retrieve information from a 3rd party APi, and save the data into SQL for the use of private API calls

## Setting Up

### Scraper.Db project

To get the database setup and initialize the table structure we need, you will have to publish/deploy the `dacpac` or `sql server project` into your own sql server instance and database. You can deploy the `dacpac` into any sql server and database.

When you publish the `dacpac` you will have the following tables in your database

- Shows => hold show information
- Cast => hold cast information with foreign key relation to shows

### Create-ServiceBusResource

This powershell module is used to create a service bus resource and queue attached to this resource. The service bus queue will have messages with the show id and the endpoint of cast for a specified show id. We will use the queue system as a sort of aditional rate limeter by setting the `"maxConcurrentCalls": 1`. This will set the amout of concurrent calls our function can make to the service bus queue.

- [Install Azure cli](https://docs.microsoft.com/nl-nl/cli/azure/install-azure-cli-windows?tabs=azure-cli)
- open powershell
- `Import-Module .\Create-ServiceBusResource.psm1 -force`
- run `Set-ServiceBusResource -ResourceGroupName lscraperrg -ServiceBusName lscrapersb -QueueName mainscraperqueue`
- You will be asked to login to azure. After you have logged in the script will create a resourcegroup with a servicebus resource in west europe and a queue for messages

### Scraper Project

The scraper project consist our of 2 `Azure Functions`. The project is intended to fetch tv shows and cast member details from `http://api.tvmaze.com/`. 

To run these function calls locally, create a `local.settings.json` file (this file is never checked in). Inside this file add the following settings:

```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "ApiUri": "http://api.tvmaze.com/",
    "DbConnectionString": "Data base connection string",
    "SbConnectionString": "Service bus connection string",
    "QueueName": "Queue name"
  }
}
```

- `RetrieveShows` function:
    - This is a trigger function that will be triggered every 3 hours. For more or less time, set own cron expression.
    - Will call the endpoint to retrieve the tv shows
    - Will check if Shows are in database and only add shows not in DB yet
    - Will add a message to the queue per show id `shows/{show.Id}/cast` with show id in the user properties
- `RetrieveCast` function:
    - This is a service bus queue function which is triggered by messages on the queue
    - This function is limited by how manany concurrent messages it can pick up by `"maxConcurrentCalls": 1` in the `host.json`
    - Will retrieve the message with endpoint details and retrieve the cast details with this endpoint
    - Will add details to the database
    - Functions have an automatic retry of [5 times if failed](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-error-pages?tabs=csharp#triggers-with-additional-resiliency-or-retries).

### MyShow.Api porject

This api is intended to fetch the scraper saved data from the selected sql database.

To run this api you need to first add your db connection string to the `appsettings.json`

For more information, you can view the `swagger` documentation when api is started up.

### ScraperTest project (Incomplete)

This project holds the unit tests for the scraper functions project. The tests are done in `XUnit` with `Moq`.

All code with `[ExcludeFromCodeCoverage]` are code with reference to 3rd party code, such as `httpclient` extracted into own class to contain code we do not test as we believe the creators tested the behind code.

## Final Note

This project can still be improved a lot. There are a few shortcuts taken to get the code in good working order which can be improved apon.

There are also a few things missing from the project that still can/should be added, such as increasing code coverage and testing.