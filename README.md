"# GitHubWebApi" 

dotnet add package Microsoft.Data.SqlClient --version 5.1.2

dotnet add package Microsoft.EntityFrameworkCore --version 6.0

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.0

dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.0

dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.0.0

dotnet add package Microsoft.Extensions.Logging --version 6.0.0

dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 6.0.0

dotnet add package Microsoft.AspNetCore.Authentication.Negotiate --version 6.0.0




dotnet ef migrations list

dotnet ef database update InitialEmployeeEF

dotnet ef migrations add InitialEmployeeEF

dotnet ef database update -verbose

dotnet ef migrations remove

dotnet build

dotnet watch run -v n

ng new CRUDAngular --no-standalone --routing --ssr=false

npm i @angular/material@17.0.0

dotnet new webapi --name CountryApi



