
# Book Review


## Migrations
```
dotnet ef migrations add InitialMigration --startup-project BookReview.WebApi

dotnet ef database update --startup-project BookReview.WebApi
```

## Run Application

```
dotnet run  --project BookReview.WebApi  --launch-profile https
```