using BusinessLogics.Repositories;
using BusinessLogics.RepositoryImpl;
using DataAccess.DTOs.Books;
using DataAccess.DTOs.Categories;
using DataAccess.Models;
using DataAccess.Profiles;
using DataAccess.Seed;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IBookRepository, BookRepositoryImpl>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<IRoleRepository, RoleRepositoryImpl>();
builder.Services.AddAutoMapper(typeof(CategoryProfile), typeof(BookProfile));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
