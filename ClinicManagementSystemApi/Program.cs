using ClinicManagementSystemApi.CustomeMiddlewares.GlobalExceptions;
using ClinicManagementSystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using static ClinicManagementSystem.Core.AppContants;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddAppServices();
builder.AddInfrastructureRegistration();
builder.AddCustomDbContext();

//change the clientId and tenantId in the apppsettings.json and expose an api
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(configuration: builder.Configuration, Config.Azure);
//builder.Services.AddAuthorization();


var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddlerware>();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
