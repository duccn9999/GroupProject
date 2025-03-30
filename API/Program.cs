using System.Text;
using BusinessLogics.Repositories;
using BusinessLogics.RepositoryImpl;
using BusinessLogics.Service;
using DataAccess.DTOs.Books;
using DataAccess.DTOs.Categories;
using DataAccess.Models;
using DataAccess.Profiles;
using DataAccess.Seed;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddDbContext<GroupProjectContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
IEdmModel GetEdmModel()
{
    var modelBuilder = new ODataConventionModelBuilder();
    modelBuilder.EntitySet<Category>("Categories");
    modelBuilder.EntitySet<Book>("Books");
    modelBuilder.EntitySet<User>("Users");
    return modelBuilder.GetEdmModel();
}
builder.Services.AddControllers()
    .AddOData(opt =>
        opt.AddRouteComponents("odata", GetEdmModel()) // Use "odata" as the route prefix
           .Select()
           .Filter()
           .OrderBy()
           .Expand()
           .Count()
           .SetMaxTop(100));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });
        options.CustomSchemaIds(type => type.FullName); // Use full name for schema IDs to avoid conflicts
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });
        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            { new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { } }
        });
    }
);
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IBookRepository, BookRepositoryImpl>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(BookProfile));
builder.Services.AddScoped<AuthService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("https://localhost:7116") // Replace with your frontend URL
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
        };
    });


var app = builder.Build();
// add seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GroupProjectContext>();
    var seeder = new DataSeeder(context);
    seeder.SeedRoles();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");


app.UseAuthorization();

app.MapControllers();

app.Run();
