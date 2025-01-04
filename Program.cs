using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoenixAPI.Controllers;
using PhoenixAPI.Identity;
using PhoenixAPI.Services;
using PhoenixAPI.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

string CorsPolicy = builder.Configuration.GetSection("Cors:Policy").Get<string>();
string[] allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
string key = builder.Configuration.GetSection("JwtConfig:Secret").Get<string>();
string validIssuer = builder.Configuration.GetSection("JwtConfig:ValidIssuer").Get<string>();
string validAudience = builder.Configuration.GetSection("JwtConfig:ValidAudience").Get<string>();

// Add CORS Policy
builder.Services.AddCors(options => {
                options.AddPolicy(CorsPolicy,
                 builder => builder.WithOrigins(allowedOrigins)
                 //builder => builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                );
            });

// Add JWT Authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuer = true,
        ValidateAudience  = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = validIssuer,
        ValidAudience = validAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddAuthorization();

/* builder.Services.AddHttpClient("MyHttpClient", client => {
    client.BaseAddress = new Uri("https://api.github.com");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}); */

/* builder.Services.AddAuthorization(options => {
    options.AddPolicy(IdentityData.AdminUserPolicyName, p => 
    p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
}); */

//builder.Services.AddTransient<AuthController, AuthController>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//builder.Services.AddHttpClient<GitHubService>();
builder.Services.AddHttpClient<IGitHubService, GitHubService>();
    //.SetHandlerLifetime(TimeSpan.FromMinutes(5));
/* 
builder.Services.AddHttpClient<ICatalogService, CatalogService>()
    .SetHandlerLifetime(TimeSpan.FromMinutes(5));
 */
//builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(CorsPolicy);
// Use Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
