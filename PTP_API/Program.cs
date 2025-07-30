using IdentityLayer;
using BussinessLayer.DendeciesInjections;
using PTP_API.Extensions;
using PTP_API.Middlewares;
using BussinessLayer.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDependenciesRegistration(builder.Configuration);
builder.Services.AddServiceRegistration(builder.Configuration);
builder.Services.AddRepositoryInjections();
builder.Services.AddValidationInjections();
builder.Services.AddIdentityLayer(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();


var app = builder.Build();

app.UseCors(policy => policy.AllowAnyHeader()
                             .AllowAnyMethod()
                             .AllowAnyOrigin());

app.UseMiddleware<SqlInjectionProtectionMiddleware>();
app.UseMiddleware<AuditingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None)
     );
}

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None); // Todos los endpoints estarán colapsados
    c.RoutePrefix = string.Empty;
});


//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();