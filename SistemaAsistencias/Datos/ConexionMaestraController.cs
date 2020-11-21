using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAsistencias.Datos
{
    class ConexionMaestraController
    {
        public static string conexion = @"Data source=DESKTOP-6TC7EC2\SQLEXPRESS;Initial Catalog=Asistencia_StrixOwl;User ID=sa;Password=sa123;MultipleActiveResultSets=True";
        public static SqlConnection conectar = new SqlConnection(conexion);

        public static void abrir()
        {
            if (conectar.State == ConnectionState.Closed) { conectar.Open(); }
        }

        public static void cerrar() 
        {
            if (conectar.State == ConnectionState.Open) { conectar.Close(); }        
        }

    }
}
