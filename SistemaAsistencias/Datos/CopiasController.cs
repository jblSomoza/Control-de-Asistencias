using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAsistencias.Logica;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    class CopiasController
    {

        public bool insertarCopiasDB()
        {
            //En ese caso no colocamos parametros ya que en el procedimiento no se declararon 
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_InsertarCopiasDB", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }


        }

        public void mostrarRuta(ref string ruta)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("SELECT ruta FROM CopiasDB", ConexionMaestraController.conectar);
                ruta = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public bool editarCopiaDB(CopiasModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("editarCopiasDB", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ruta", parametros.ruta);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { ConexionMaestraController.cerrar(); }

            return true;
        }
    }
}
