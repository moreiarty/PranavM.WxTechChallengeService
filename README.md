# PranavM.WxTechChallengeService
This is my submission for the Recruitment Dev Challenge
## TODOs:
* General cleanup
* Unit testing using Moq & XUnit Framework
* Exporting config (token & baseUrl of downstream services) to Azure App Config dynamically
* Separating Commmand and Query responsibility (modifying & reading data)
* Having Query/Repository layer sitting above the Accessor/client project
* Use Notification pattern for validating trolley request body (Martin Fowler)
* Include error handling of downstream exceptions, establish error code strings for them, and expose them in the Error data model
* Implementing Structured Logging for exporting to Container Insights in Azure
