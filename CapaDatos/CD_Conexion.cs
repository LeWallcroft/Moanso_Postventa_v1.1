using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace CapaDatos
{
    public class CD_Conexion
    {
        private string connectionString;

        public CD_Conexion()
        {
            // Intenta cargar desde archivo local primero
            connectionString = GetConnectionStringFromLocalFile();

            // Si no existe archivo local, usa el de App.config (comportamiento original)
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;
            }
        }

        private string GetConnectionStringFromLocalFile()
        {
            try
            {
                string localConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connections.local.config");

                if (File.Exists(localConfigPath))
                {
                    var doc = new XmlDocument();
                    doc.Load(localConfigPath);

                    var node = doc.SelectSingleNode("//add[@name='MiConexion']");
                    if (node != null && node.Attributes["connectionString"] != null)
                    {
                        return node.Attributes["connectionString"].Value;
                    }
                }
            }
            catch
            {
                // Silenciosamente falla y usa App.config
            }

            return null;
        }

        public SqlConnection AbrirConexion()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            return con;
        }
    }
}