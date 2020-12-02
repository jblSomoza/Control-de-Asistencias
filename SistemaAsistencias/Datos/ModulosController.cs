using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Datos
{
    class ModulosController
    {

        public void mostrarModulos(ref DataTable dt) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Modulos", ConexionMaestraController.conectar);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);               
            }
            finally { ConexionMaestraController.cerrar(); }
        }

		public bool Insertar_Modulos(ModulosModel parametros)
		{
			try
			{
				ConexionMaestraController.abrir();
				SqlCommand cmd = new SqlCommand("proc_insertarModulos", ConexionMaestraController.conectar);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@modulo", parametros.modulo);
				cmd.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return true;
			}
			finally
			{
				ConexionMaestraController.cerrar();
			}
		}

	}
}
