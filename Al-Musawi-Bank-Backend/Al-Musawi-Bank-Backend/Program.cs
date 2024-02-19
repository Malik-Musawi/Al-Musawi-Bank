using Microsoft.EntityFrameworkCore;
using Al_Musawi_Bank_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext< BankDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankConnectionString")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddSingleton<TokenService>(new TokenService("jIkIg+dvyCeuJ0SJWWMnwCsHa7EmKElDS7+GJoJPfmdFfhF9LH5W3m2K7+0KbIdCUg4NM3dsEZ09YcFAJehIgg==\n"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jIkIg+dvyCeuJ0SJWWMnwCsHa7EmKElDS7+GJoJPfmdFfhF9LH5W3m2K7+0KbIdCUg4NM3dsEZ09YcFAJehIgg==\n")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4200") // Angular app's URL
        .AllowAnyMethod()
        .AllowAnyHeader());


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();