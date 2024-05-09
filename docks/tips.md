# Tips

## Create migration

```
dotnet ef migrations add InitialCreate --project Scheduler.Infrastructure --startup-project Scheduler.Api --context SchedulerDbContext
dotnet ef database update --project Scheduler.Infrastructure --startup-project Scheduler.Api --context SchedulerDbContext
```

## Add user secrets

```
dotnet user-secrets init --project .\src\Scheduler.Api\
dotnet user-secrets set "JwtSettings:Secret" "user_secret_user_secret123123123123" --project .\src\Scheduler.Api\
```