using Ada.Kanban.Api.Filters;
using Ada.Kanban.Api.ServiceExtensions;
using Ada.Kanban.Db.DbContexts;
using Ada.Kanban.Db.Repositories;
using Ada.Kanban.Service.Options;
using Ada.Kanban.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ErrorHandlingFilter>();
    opt.Filters.Add<LoggingFilter>();
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddDbContext<AdaKanbanDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("AdaKanbanDatabase")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<LoginCredentialOptionsSetup>();

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

var app = builder.Build();

//Needed for reading request body on filters.
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next.Invoke();
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(op =>
{
    op.AllowAnyOrigin();
    op.AllowAnyHeader();
    op.AllowAnyMethod();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers()
    .RequireAuthorization();

var dbContext = app.Services.GetRequiredService<AdaKanbanDbContext>();
await dbContext.Database.MigrateAsync();

app.Run();
