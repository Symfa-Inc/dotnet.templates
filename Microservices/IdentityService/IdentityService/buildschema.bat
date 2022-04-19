dotnet ef migrations add PersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/PersistedGrantDb
dotnet ef migrations add ConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/ConfigurationDb
dotnet ef migrations add UserDbMigration -c UserDbContext -o Data/Migrations/UserDb

dotnet ef database update --context PersistedGrantDbContext
dotnet ef database update --context ConfigurationDbContext
dotnet ef database update --context UserDbContext