# Oven

Generates C# MVC Backend, React (React-Admin) Frontend

Oven.Console uses hard coded configuration to generate a configuration UI that can then be used to call the Oven.Api Project to Generate your own application

Configuration is best done via the API sending in a JSON payload of your configuration.

The JSON configuration is then stored in git and the generated solution is also stored in a seperate git repo.

Currently the project has been configured to use Azure Dev Ops to create repos so that will need updating to GitHub 

## Commit History
The project has been rewritten many times going through the commit history it generates in different ways.

The current commit generates a Mongo DB Data Access Layer, [React-Admin](https://github.com/marmelab/react-admin) UI

Previously it generated a EF Core SQL Server Data Access Layer. This was the most stable version.

Way back I was generating an angular 2+ UI
