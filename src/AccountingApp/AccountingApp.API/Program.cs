using AccountingApp.Core.Interfaces;
using AccountingApp.Data;
using AccountingApp.Data.Context;
using AccountingApp.Service.Mappings;
using AccountingApp.Services.Interfaces;
using AccountingApp.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AccountingAppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(AccountingApp.Data.Repositories.Repository<>));
builder.Services.AddScoped<IAccountRepository, AccountingApp.Data.Repositories.AccountRepository>();
builder.Services.AddScoped<ICustomerRepository, AccountingApp.Data.Repositories.CustomerRepository>();
builder.Services.AddScoped<IInvoiceRepository, AccountingApp.Data.Repositories.InvoiceRepository>();
builder.Services.AddScoped<ITransactionRepository, AccountingApp.Data.Repositories.TransactionRepository>();

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
