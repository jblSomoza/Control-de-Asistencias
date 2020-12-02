using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using SistemaAsistencias.Logica;
using System.Windows.Forms;

namespace SistemaAsistencias.Datos
{
    class PermisosController
    {
        public bool insertarPermisos(PermisosModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Permisos", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPermiso", "0");
                cmd.Parameters.AddWithValue("@idModulo", parametros.idModulo);
                cmd.Parameters.AddWithValue("@idUsuario", parametros.idUsuario);
                cmd.Parameters.AddWithValue("@permisoTip", "N");
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public bool eliminarPermisos(PermisosModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Permisos", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPermiso", parametros.idPermiso);
                cmd.Parameters.AddWithValue("@idModulo", parametros.idModulo);
                cmd.Parameters.AddWithValue("@idUsuario", parametros.idUsuario);
                cmd.Parameters.AddWithValue("@permisoTip", parametros.estado);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }finally { ConexionMaestraController.cerrar(); }
        }

        public void mostrarPermisos(ref DataTable dt, PermisosModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("proc_MostrarPermisos", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@idUsuario", parametros.idUsuario);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }
    }
}
