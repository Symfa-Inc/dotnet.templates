dotnet ef migrations add PersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/PersistedGrantDb
dotnet ef migrations add ConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/ConfigurationDb

dotnet ef database update --context PersistedGrantDbContext
dotnet ef database update --context ConfigurationDbContext