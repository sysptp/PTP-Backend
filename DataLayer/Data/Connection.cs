using Microsoft.Data.SqlClient;

namespace DataLayer.Data
{
    public class Connection
    {
        public async Task<SqlConnection> Conn()
        {
            try
            {
                string connectionString = "";
                var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al conectarese a la BD: " + ex);
            }
        }
    }
}
