
using GymInnowise.Authorization.Persistence.Data;
using GymInnowise.Authorization.Persistence.Models;
using GymInnowise.Authorization.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AuthorizationDbContext>();

builder.Services.AddScoped<AccountsRepository>();
builder.Services.AddScoped<RolesRepository>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("accounts", async (AccountsRepository repo) => await repo.GetAllAccountsAsync()
   
);

app.MapPost("new_account", async([FromBody]AccountDto dto,AccountsRepository repo) =>
{
    Account account = new Account
    {
        Email = dto.email,
        PhoneNumber = dto.phonenumber,
        CreatedDate = DateTime.UtcNow,
        ModifiedDate = DateTime.UtcNow,
        PasswordHash = "password_hash",
    };
    await repo.CreateAccountAsync(account);
});

app.MapDelete("rem_account", async ([FromBody] Guid id, AccountsRepository repo) =>
{
    var result = await repo.DeleteAccountAsync(id);
    if (result) return "Success!";
    else return "Failed!";
});

app.UseHttpsRedirection();
app.MapControllers();


app.Run();


public record AccountDto(string email, string phonenumber);