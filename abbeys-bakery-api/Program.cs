using Microsoft.AspNetCore.Hosting;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.EntityFrameworkCore;
using abbeys_bakery_api.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
var keyVaultName = "abbeysbakeryapikv";
string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";
DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    // These settings are used to speed up the loading of KeyVault. This way it can ignore unused credential sources.
    ExcludeAzureCliCredential = true,
    ExcludeAzurePowerShellCredential = true,
    ExcludeEnvironmentCredential = true,
    ExcludeInteractiveBrowserCredential = true,
    ExcludeManagedIdentityCredential = false,
    ExcludeSharedTokenCacheCredential = true,
    ExcludeVisualStudioCodeCredential = false,
    ExcludeVisualStudioCredential = false,
});
//builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), defaultAzureCredential);
*/
builder.Services.AddControllers();
builder.Configuration.AddUserSecrets<Program>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});

var connectionString = builder.Configuration["AbbeysBakeryContext"];
builder.Services.AddDbContext<AbbeysBakeryContext>(options =>
    options.UseSqlServer(connectionString));

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
