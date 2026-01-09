using HDI.Application;
using HDI.Infrastructure;
using HDI.Persistence;
using HDI.Persistence.Contexts;
using HDI.WebAPI.Helpers;
using HDI.WebAPI.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDI API V1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>(); 
app.UseMiddleware<TenantMiddleware>();

app.UseAuthorization();
app.MapControllers(); 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı migrate edilirken bir hata oluştu.");
    }
}

app.Run();