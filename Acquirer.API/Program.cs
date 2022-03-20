using Acquirer.API.Handlers;
using Acquirer.Client;
using Acquirer.DAL;
using Acquirer.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var defaultSchema = "BasicAuthentication";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContextPool<AcquirerDbContext>(
options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAuthentication(defaultSchema)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                (defaultSchema, null);

builder.Services.AddAuthorization();



//Inject dependencies
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IStripeClient, StripeClient>();

var app = builder.Build();

//Auto db migration
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    using var context = services.GetRequiredService<AcquirerDbContext>();
    context.Database.Migrate();
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }