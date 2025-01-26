using BankAccountTransactions.Application.Service;
using BankAccountTransactions.Application.UseCase;
using BankAccountTransactions.Data.Context;
using BankAccountTransactions.Data.Repository;
using BankAccountTransactions.Domain.Repository;
using BankAccountTransactions.Domain.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankAccountTransactionsContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgresConnection")
    ).UseEnumCheckConstraints()
);

//DI
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<CreateAccountUseCase>();
builder.Services.AddScoped<GetUserByDocumentUseCase>();
builder.Services.AddScoped<GetUserAccountUseCase>();
builder.Services.AddScoped<GetUserTransactionsByDateUseCase>();
builder.Services.AddScoped<GetUserBalanceUseCase>();
builder.Services.AddScoped<GetAllUserTransactionsUseCase>();
builder.Services.AddScoped<AddUserBalanceUseCase>();
builder.Services.AddScoped<UpdateUserAccountBalanceUseCase>();
builder.Services.AddScoped<CreateTransactionUseCase>();
builder.Services.AddScoped<ValidateUserCredentialsUseCase>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
var app = builder.Build();

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
