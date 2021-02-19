using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using System.Threading;
using System.Data.SqlClient;

namespace SistemaAsistencias.Presentacion
{
    public partial class CopiasDB : UserControl
    {
        public CopiasDB()
        {
            InitializeComponent();
        }

        string ruta;
        string software = "StrixOwl";
        string db = "SistemaAsistencias";
        private Thread hilo;
        private bool acaba = false;

        private void CopiasDB_Load(object sender, EventArgs e)
        {
            mostrarRuta();
        }

        private void mostrarRuta() 
        {
            CopiasController funcion = new CopiasController();
            funcion.mostrarRuta(ref ruta);
            txtRuta.Text = ruta;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            generarCopia();
        }

        private void generarCopia()
        {
            if (!string.IsNullOrEmpty(txtRuta.Text))
            {
                hilo = new Thread(new ThreadStart(ejecucion));
                hilo.Start();
                timer1.Start();
            }
            else 
            {
                MessageBox.Show("Selecciona una Ruta donde Guardar las Copias de Seguridad");
                txtRuta.Focus();
            }
        }

        private void ejecucion() 
        {
            string miCarpeta = "Copias_de_seguridad_de_" + software;
            string rutaCompleta = "", subcarpeta = "";
            if (!Directory.Exists(Path.Combine(txtRuta.Text + @"\" + miCarpeta)))
            {                
                Directory.CreateDirectory(txtRuta.Text + @"\" + miCarpeta);
            }

            rutaCompleta = txtRuta.Text + @"\" + miCarpeta;
            subcarpeta = rutaCompleta + @"\Respaldo_al_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;

            try
            {
                Directory.CreateDirectory(Path.Combine(rutaCompleta, subcarpeta));
            }
            catch (Exception)
            {
                
            }

            try
            {
                string vNombreRespaldo = db + ".bak";
                ConexionMaestraController.abrir();
                SqlCommand cmd = new SqlCommand("BACKUP DATABASE " + db + " TO DISK = '" + subcarpeta + @"\" + vNombreRespaldo +"'", ConexionMaestraController.conectar);
                cmd.ExecuteNonQuery();
                acaba = true;

            }
            catch (Exception ex)
            {
                acaba = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void pbBrowser_Click(object sender, EventArgs e)
        {
            obtenerRuta();
        }

        private void obtenerRuta() 
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtRuta.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (acaba == true)
            {
                timer1.Stop();
                editarRespaldos();
            }
        }

        private void editarRespaldos() 
        {
            CopiasModel parametros = new CopiasModel();
            CopiasController funcion = new CopiasController();

            parametros.ruta = txtRuta.Text.Trim();
            if (funcion.editarCopiaDB(parametros) == true)
            {
                MessageBox.Show("Copia de base de datos Generada", "Generacion de Copia de DB", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
