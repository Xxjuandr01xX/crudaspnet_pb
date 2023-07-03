using System.Data.SqlClient; // (1) se hace el llamado al paquete que de instalo previamente por nuGet.

namespace CRUD.Models.Datos
{
    public class Conexion
    {
        private const string V = "appsettings.json";
        private String cadenaSQL = string.Empty;

        public Conexion()
        {
            /**
             * Constructor de la clase
             */
            
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;
        }

        public String getCadenaSQL()
        {
            return this.cadenaSQL;
        }



    }

}
