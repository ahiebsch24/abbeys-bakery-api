using Microsoft.AspNetCore.Hosting;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure Key Vault
var keyVaultName = $"abbeysbakerykv";
string keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";
DefaultAzureCredential defaultAzureCredential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
{
    // These settings are used to speed up the loading of KeyVault. This way it can ignore unused credential sources.
    ExcludeAzureCliCredential = true,
    ExcludeAzurePowerShellCredential = true,
    ExcludeEnvironmentCredential = true,
    ExcludeInteractiveBrowserCredential = true,
    ExcludeManagedIdentityCredential = !builder.Environment.IsProduction(),
    ExcludeSharedTokenCacheCredential = true,
    ExcludeVisualStudioCodeCredential = !builder.Environment.IsDevelopment(),
    ExcludeVisualStudioCredential = !builder.Environment.IsDevelopment(),
});
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), defaultAzureCredential);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
var app = builder.Build();

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
