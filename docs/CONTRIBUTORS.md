# How to become a contributor

The Developer Marketplace seeks contributors. If you'd like to learn .NET Core and MVC 6 this will be the perfect opportunity for you to work on a [SOLID](https://en.wikipedia.org/wiki/SOLID_(object-oriented_design)) project that strives to follow the .NET Core guitelines and abides by all open source practices. 
If you want to become a contributor, you have to follow this workflow:
- Please read the contributors code of conduct file [here]()
- Accept the Contributors License Agreement (CLA). You can find more information about the CLA below.
- Go to `Git Issues` or [Waffle.IO](https://waffle.io) and select a story that is tagged with the `help wanted` (most of them are).
- If you're using Waffle.IO please move the story to the `In Progress` column and create a new branch from `master` using the following name convention `US##_FriendlyName` where `##` is a placeholder for the git issue ID (e.g. US35_ClaimsAuthentication).
- You can use either Visual Studio 2015, VSCode or any IDE in combination with .netcore. Just exclude from the .gitignore any potential temporary and configuration files that are specific to the IDE. 
- The Developer Marketplace project abides by [TDD](https://en.wikipedia.org/wiki/Test-driven_development) practices. For every piece of production code there should be a unit test that failed and the production code fixes it and makes it pass. All code is subject to testing except the generated code, business objects, models and POCOs. An AppVeyor CI build is in place to assure that all unit tests pass and the project code base is intact.
- Follow the coding conventions described in the [CONVENTIONS.md](https://github.com/cracker4o/dev-marketplace/blob/master/docs/CODING_CONVENTIONS.md) file.
- When you are finished implementing the code for the story, please commit and push your changes and make a pull request.
- Once the pull request is accepted, the comment message on that request should be in the following format: `Closes #xxx - Freeform message` where the #xxx is the number of the GitHub issue. 

## CLA guidelines
Some companies require their software engineers to sign a special agreement which states that their code is company property even though they are doing it in their spare time. The Contributor License Agreement protects their code from legal actions. Please download the CLA pdf file and use the signature field to sign it. When the document is signed, please send it to [cracker4o@gmail.com](mailto:cracker4o@gmail.com)

## Kanban story management
The Kanban board is managed on Waffle.IO . It is an UI over GitHub issues and it gives kanban states to all GitHub issues. It is preferable for every new issue/story to be created in the Waffle.IO board. It has to be described thoroughly and estimated with story points from 1 to 8 (if a story is greater than 8, then it needs to be split into multiple smaller stories). Every story is tagged with the `enhancement` tag and every defect is tagged as a `bug`. If you need help from some of the other contributors, please tag the defect/story with the `help wanted` tag.

## Development guidelines
To start contributing to the Developer Marketplace project, you can clone the repository and build the project using the `build.ps1` on Windows and the `build.sh` script on Linux. The project requires the dotnet core 1.1 runtime and the 1.0.3 SDK versions which you can download [for your OS](https://www.microsoft.com/net/download/core). It is recommended to add the self signed certificate to the trusted certificates so your development environment can better match the real environment.
* Running the build.sh script (Ubuntu)
    
    Before runnung the sh script, please make sure that you have the following prerequisites:
    
    - You have installed the latest version of the DotNetCore SDK for Ubuntu. You can find instructions here - [https://www.microsoft.com/net/core#linuxubuntu](https://www.microsoft.com/net/core#linuxubuntu)
    - You have installed the MSSQL server for linux following the instructions here - 
    [https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-setup-ubuntu](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-setup-ubuntu)

    The script will automatically install nodejs, npm and gulp in order to perform all necessary build/run operations.
    
    `sh build.sh -h` displays help information about the build script
    
    `sh build.sh` builds and runs the DevMarketplace project. It checks all necessery prerequisites and it also configures the connection string to the MSSQL instance
    
    `sh build.sh -n -d` the parameter -d skips the database connection string input and the -n parameter disables the prerequisites check

* Running the build.ps1 script (Windows)

    The powershell script needs the following prerequisites:

    - The latest version of the DotNetCore SDK which can be found here: [https://www.microsoft.com/net/download/core](https://www.microsoft.com/net/download/core)
    - An instance of MSSQL server or MSSQL server EXPRESS
    - NodeJS and NPM

    The powershell script will automatically install gulp.

    `./build.ps1` builds and runs the DevMarketplace project
    `./build.ps1 -noinit -noDbConfig` the parameter -noinit skips the prerequisites check and teh -noDbConfig doesn't prompt for a connection string 

## Git guidelines

