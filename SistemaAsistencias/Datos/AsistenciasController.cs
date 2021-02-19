using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Datos
{
    class AsistenciasController
    {

        public bool insertarAsistencias(AsistenciasModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("insertarAsistencias", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersonal", parametros.idPersonal);
                cmd.Parameters.AddWithValue("@fechaEntrada", parametros.fechaEntrada);
                cmd.Parameters.AddWithValue("@fechaSalida", parametros.fechaSalida);
                cmd.Parameters.AddWithValue("@estado", parametros.estado);
                cmd.Parameters.AddWithValue("@horas", parametros.horas);
                cmd.Parameters.AddWithValue("@observacion", parametros.observacion);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally 
            {
                ConexionMaestraController.cerrar();
            }
        }

        public bool modificarAsistencia(AsistenciasModel parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("confirmarSalida", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersonal", parametros.idPersonal);
                cmd.Parameters.AddWithValue("@fechaSalida", parametros.fechaSalida);
                cmd.Parameters.AddWithValue("@horas", parametros.horas);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally 
            {
                ConexionMaestraController.cerrar();
            }
        }

        public void buscarAsistenciaId(ref DataTable dtPersonal, int idPersonal)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("buscarAsistenciaId", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@idPersonal", idPersonal);
                adapter.Fill(dtPersonal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void mostrarAsistenciasDiarias(ref DataTable dt, DateTime desde, DateTime hasta, int semana) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("proc_mostrarAsistenciasDiarias", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@desde", desde);
                adapter.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                adapter.SelectCommand.Parameters.AddWithValue("@semana", semana);
                adapter.Fill(dt);
            }
            catch (Exception ex) { MessageBox.Show(ex.StackTrace); }
            finally { ConexionMaestraController.cerrar(); }
        }
    }
}
