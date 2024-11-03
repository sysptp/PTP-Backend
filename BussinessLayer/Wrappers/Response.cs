namespace BussinessLayer.Wrappers
{
    public class Response<T>
    {
        public Response() { }

        public Response(T data, string? message = null, int statusCode = 200)
        {
            Succeeded = true;
            Message = message ?? "Operation completed successfully.";
            Data = data;
            StatusCode = statusCode;
        }

        public Response(string message, int statusCode = 400)
        {
            Succeeded = false;
            Message = null;
            StatusCode = statusCode;
            Errors = new List<string> { message };
        }

        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }

        public static Response<T> Success(T data, string? message = "Operación exitosa.", int statusCode = 200)
        {
            return new Response<T>(data, message, statusCode);
        }

        public static Response<T> Created(T data, string? message = "Recurso creado exitosamente.")
        {
            return new Response<T>(data, message, 201);
        }

        public static Response<T> NoContent(string message = "No hay contenido disponible.")
        {
            return new Response<T>
            {
                Succeeded = true,
                StatusCode = 204,
                Message = message
            };
        }

        public static Response<T> BadRequest(List<string> errors, int statusCode = 400)
        {
            return new Response<T>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Errors = errors,
                Message = null
            };
        }

        public static Response<T> NotFound(string message = "Recurso no encontrado.")
        {
            return new Response<T>
            {
                Succeeded = false,
                StatusCode = 404,
                Message = null,
                Errors = new List<string> { message }
            };
        }

        public static Response<T> Unauthorized(string message = "No está autorizado.")
        {
            return new Response<T>
            {
                Succeeded = false,
                StatusCode = 401,
                Message = message,
                Errors = new List<string> { message }
            };
        }

        public static Response<T> Forbidden(string message = "No posee el permiso para acceder.")
        {
            return new Response<T>
            {
                Succeeded = false,
                StatusCode = 403,
                Message = message,
                Errors = new List<string> { message }
            };
        }

        public static Response<T> ServerError(string message = "Sucedió un error interno.")
        {
            return new Response<T>
            {
                Succeeded = false,
                StatusCode = 500,
                Message = message,
                Errors = new List<string> { message }
            };
        }
    }
}
