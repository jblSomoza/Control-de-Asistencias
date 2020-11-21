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
    class CargoController
    {

        public bool insertarCargo(CargoModel parametros)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Cargo", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCargo", "");
                cmd.Parameters.AddWithValue("@cargo", parametros.cargo);
                cmd.Parameters.AddWithValue("@sueldoPorHora", parametros.sueldoPorHora);
                cmd.Parameters.AddWithValue("@cargoTipo", "N");
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

        public bool actualizarCargo(CargoModel parametros)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("proc_Cargo", ConexionMaestraController.conectar);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCargo", parametros.idCargo);
                cmd.Parameters.AddWithValue("@cargo", parametros.cargo);
                cmd.Parameters.AddWithValue("@sueldoPorHora", parametros.sueldoPorHora);
                cmd.Parameters.AddWithValue("@cargoTipo", "M");
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
        
        public void buscarCargo(string buscador, ref DataTable dtCargo)
        {
            try
            {
                ConexionMaestraController.abrir();
                SqlDataAdapter adapter = new SqlDataAdapter("proc_buscarCargos", ConexionMaestraController.conectar);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
                adapter.Fill(dtCargo);
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
