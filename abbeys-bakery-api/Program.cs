using Microsoft.AspNetCore.Hosting;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.EntityFrameworkCore;
using abbeys_bakery_api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Configuration.AddUserSecrets<Program>();

// Configure Key Vault
var keyVaultName = "abbeysbakeryapikv";
string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";
<<<<<<< Updated upstream
var Azure_Tenant_Id = builder.Configuration["Azure_Tenant_Id"];
var Azure_Client_Id = builder.Configuration["Azure_Client_Id"];
var Azure_Client_Secret = builder.Configuration["Azure_Client_Secret"];
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new ClientSecretCredential(Azure_Tenant_Id, Azure_Client_Id, Azure_Client_Secret));

// Add CORS policy
builder.Services.AddCors(options =>
=======
/*
DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
>>>>>>> Stashed changes
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
<<<<<<< Updated upstream

=======
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), defaultAzureCredential);
*/
>>>>>>> Stashed changes
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});
var connectionString = builder.Configuration.GetConnectionString("AbbeysBakeryContext"); 
builder.Services.AddDbContext<AbbeysBakeryContext>(options =>
    options.UseSqlServer("Data Source=ADINANDABBEY\\MSSQLSERVER01;Initial Catalog=AbbeysBakery;Integrated Security=True"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseCors("AllowAngularOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
