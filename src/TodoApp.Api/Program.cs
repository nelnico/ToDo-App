using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoApp.Api.Common.Extensions;
using TodoApp.Core.Entities;
using TodoApp.Core.Services;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();

services.AddApplicationServices(builder.Configuration);

services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors(builder.Configuration.GetValue<string>("CorsPolicyName") ?? string.Empty);

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToController("Index", "Fallback");
});


using var scope = app.Services.CreateScope();
var scopeServices = scope.ServiceProvider;
try
{
    var context = scopeServices.GetRequiredService<DataContext>();
    var uow = scopeServices.GetRequiredService<IUnitOfWork>();
    var userManager = scopeServices.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scopeServices.GetRequiredService<RoleManager<IdentityRole>>();
    await context.Database.MigrateAsync();
    await SeedData.SeedUsers(uow, userManager, roleManager);
}
catch (Exception ex)
{
    var service = scopeServices.GetRequiredService<ILogger<Program>>();
    service.LogError(ex, "An error occurred during migration");
}

app.Run();