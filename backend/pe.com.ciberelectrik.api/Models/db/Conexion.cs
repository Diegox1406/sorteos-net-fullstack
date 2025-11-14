using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace pe.com.ciberelectrik.api.Models.db
{
    public class Conexion
    {
        //cadena de conexion
        //con autenticacion en windows con SQL Server Enterprise
        //private string cadena = "Data Source=.; Initial Catalog=bdciberelectrik2025; Integrated Security=True";
        //con autenticacion en windows con SQL Server Express
        private string cadena = "Data Source=NameMotorDb; Initial Catalog=sorteo; Integrated Security=True; TrustServerCertificate=true;";
        //con autenticacion SQL Server
        //private string cadena = "Data Source=DESKTOP-VGLO15C; Initial Catalog=bdciberelectrik2025; User ID=sa;Password=sql;";

        private SqlConnection xcon;

        public SqlConnection Conectar()
        {
            xcon = new SqlConnection(cadena);
            xcon.Open();
            return xcon;
        }

        public void CerrarConexion()
        {
            xcon.Close();
            xcon.Dispose();
        }

    }
}