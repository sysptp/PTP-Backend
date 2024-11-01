using Swashbuckle.AspNetCore.SwaggerUI;

namespace PTP_API.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PTP-API");
                options.DefaultModelRendering(ModelRendering.Model);
            });
        }

    }
}
