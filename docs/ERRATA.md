# ERRATA

## Entity Framework Core
- The current version of EF Core doesn't support the data access layer to be in a separate assembly. As a workaround a factory class was created that initializes the DbContext and also the DataAccess assembly was turned into a console application with an empty Program.cs main method. This should be removed when they provide support for having the Data Access layer in a separate assembly.

## Build.sh
- the sh build script for Ubuntu fails when setting the new value of the connection string if there is an escaped backslash `\\` in the old connection string setting in appsettings.json
