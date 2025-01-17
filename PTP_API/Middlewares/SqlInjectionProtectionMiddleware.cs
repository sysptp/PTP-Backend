using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.RegularExpressions;

public class SqlInjectionProtectionMiddleware
{
    private readonly RequestDelegate _next;

    public SqlInjectionProtectionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Excluir validaciones si el contenido es multipart/form-data
        if (context.Request.ContentType?.Contains("multipart/form-data") == true)
        {
            await _next(context); // Ignorar validaciones en este caso
            return;
        }

        context.Request.EnableBuffering();

        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            var model = JsonSerializer.Deserialize<Dictionary<string, object>>(requestBody);
            if (model != null)
            {
                try
                {
                    ValidateModelProperties(model);
                }
                catch (ValidationException ex)
                {
                    await HandleSqlInjectionResponseAsync(context, ex.Message);
                    return;
                }
            }
        }

        await _next(context);
    }

    private void ValidateModelProperties(Dictionary<string, object> model)
    {
        foreach (var property in model)
        {
            if (property.Value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.String)
            {
                var value = jsonElement.GetString();
                if (!string.IsNullOrEmpty(value) && ContainsSqlInjection(value))
                {
                    throw new ValidationException($"La propiedad '{property.Key}' contiene caracteres potencialmente peligrosos.");
                }
            }
        }
    }

    private bool ContainsSqlInjection(string input)
    {
        var sqlInjectionPattern = @"[';]|--";
        return Regex.IsMatch(input, sqlInjectionPattern, RegexOptions.IgnoreCase);
    }

    private static async Task HandleSqlInjectionResponseAsync(HttpContext context, string message)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";

        var response = new
        {
            succeeded = false,
            statusCode = 400,
            message = message,
            errors = new[] { message },
            data = (object)null
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
