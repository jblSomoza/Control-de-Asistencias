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
    public class PersonalController
    {
        public bool insertarPersonal(Personal parametros) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Personal", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersonal", "");
                cmd.Parameters.AddWithValue("@nombres", parametros.nombres);
                cmd.Parameters.AddWithValue("@apellidos", parametros.apellidos);
                cmd.Parameters.AddWithValue("@identificacion", parametros.identificacion);
                cmd.Parameters.AddWithValue("@pais", parametros.pais);
                cmd.Parameters.AddWithValue("@idCargo", parametros.idCargo);
                cmd.Parameters.AddWithValue("@sueldoPorHora", parametros.sueldoPorHora);
                cmd.Parameters.AddWithValue("@estado", "N");
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public bool actualizarPersonal(Personal parametros)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Personal", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersonal", parametros.idPersonal);
                cmd.Parameters.AddWithValue("@nombres", parametros.nombres);
                cmd.Parameters.AddWithValue("@apellidos", parametros.apellidos);
                cmd.Parameters.AddWithValue("@identificacion", parametros.identificacion);
                cmd.Parameters.AddWithValue("@pais", parametros.pais);
                cmd.Parameters.AddWithValue("@idCargo", parametros.idCargo);
                cmd.Parameters.AddWithValue("@sueldoPorHora", parametros.sueldoPorHora);
                cmd.Parameters.AddWithValue("@estado", "M");
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public bool eliminarPersonal(Personal parametros)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Personal", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersonal", parametros.idPersonal);
                cmd.Parameters.AddWithValue("@nombres", parametros.nombres);
                cmd.Parameters.AddWithValue("@apellidos", parametros.apellidos);
                cmd.Parameters.AddWithValue("@identificacion", parametros.identificacion);
                cmd.Parameters.AddWithValue("@pais", parametros.pais);
                cmd.Parameters.AddWithValue("@idCargo", parametros.idCargo);
                cmd.Parameters.AddWithValue("@sueldoPorHora", parametros.sueldoPorHora);
                cmd.Parameters.AddWithValue("@estado", "E");
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public bool restaurarPersonal(Personal parametros)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_restaurarPersonal", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPersonal", parametros.idPersonal);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void mostrarPersonal(ref DataTable dtPersonal, int desde, int hasta)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("proc_mostrarPersonal", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@desde", desde);
                adapter.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                adapter.Fill(dtPersonal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void buscarPersonal(ref DataTable dtPersonal, int desde, int hasta, string buscador)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("proc_buscarPersona", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@desde", desde);
                adapter.SelectCommand.Parameters.AddWithValue("@hasta", hasta);
                adapter.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                adapter.Fill(dtPersonal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally { ConexionMaestraController.cerrar(); }
        }        

        public void contarPersonal(ref int contador) 
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(idPersonal) FROM Personal", ConexionMaestraController.conectar);
                //Execute escalar solo te trae la primera fila y primera columna               
                contador = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception) { contador = 0; }
            finally { ConexionMaestraController.cerrar(); }
        }

        public void buscarPersonalIdentificacion(ref DataTable dtPersonal, string buscador)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("proc_buscarPersonalIdentidad", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                adapter.Fill(dtPersonal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally { ConexionMaestraController.cerrar(); }
        }        
    }
}
