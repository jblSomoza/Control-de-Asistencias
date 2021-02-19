using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;

namespace SistemaAsistencias.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        public int idUsuario;
        public string loginV;
        string nombreDB = "SistemaAsistencias";
        string servidor = @".\SQLEXPRESS";
        string ruta;

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            pnBienvenida.Dock = DockStyle.Fill;
            validarPermisos();
            lblLogin.Text = loginV;
        }

        private void validarPermisos() 
        {
            DataTable dt = new DataTable();
            PermisosController funcion = new PermisosController();
            PermisosModel parametros = new PermisosModel();
            parametros.idUsuario = idUsuario;
            funcion.mostrarPermisos(ref dt, parametros);
            btnConsultas.Enabled = false;
            btnPersonal.Enabled = false;
            btnRegistro.Enabled = false;
            btnUsuarios.Enabled = false;
            btnRestaurar.Enabled = false;
            btnRespaldos.Enabled = false;

            foreach (DataRow rowPermisos in dt.Rows)
            {
                string modulo = Convert.ToString(rowPermisos["modulo"]);
                if (modulo == "PrePlanillas") { btnConsultas.Enabled = true; }
                if (modulo == "Personal") { btnPersonal.Enabled = true; }
                if (modulo == "Usuarios") { btnUsuarios.Enabled = true; btnRegistro.Enabled = true; }
                if (modulo == "Respaldos") { btnRespaldos.Enabled = true; btnRestaurar.Enabled = true; }
            }
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            Personal control = new Personal();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            ControlUsuario control = new ControlUsuario();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            Preplanilla control = new Preplanilla();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            Dispose();
            TomarAsistencias frm = new TomarAsistencias();
            frm.ShowDialog();
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            restaurarDBExpress();
        }

        private void restaurarDBExpress() 
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Backup " + nombreDB + "|*.bak";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de Backup";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ruta = Path.GetFullPath(dlg.FileName);
                DialogResult pregunta = MessageBox.Show("Usted esta a punto de restaurar la base de datos, asegurese que el archivo .bak sea reciente, de lo contrario podria " + 
                    "perder informacion y no podra recuperarla, ¿Desea Continual?", "Restauracion de base de datos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (pregunta == DialogResult.Yes)
                {
                    SqlConnection cnn = new SqlConnection("Server=" + servidor + "; database=master; integrated security=yes");
                    try
                    {
                        string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + nombreDB + "' USE [master] ALTER DATABASE [" + nombreDB +
                        "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + nombreDB + "] RESTORE DATABASE " + nombreDB + " FROM DISK = N'" + ruta +
                        "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";                        
                        cnn.Open();
                        SqlCommand borrarRestaura = new SqlCommand(Proceso, cnn);
                        borrarRestaura.ExecuteNonQuery();
                        MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Aplicativo", "Restauración de base de datos",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Dispose();
                    }
                    catch (Exception)
                    {
                        RestaurarNoExpress();
                    }
                    finally
                    {
                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                        }

                    }

                }
            }
        }

        private void RestaurarNoExpress() 
        {
            servidor = ".";
            SqlConnection cnn = new SqlConnection("Server=" + servidor + "; database=master; integrated security=yes");
            try
            {
                string Proceso = "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'" + nombreDB + "' USE [master] ALTER DATABASE [" + nombreDB +
                "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE DROP DATABASE [" + nombreDB + "] RESTORE DATABASE " + nombreDB + " FROM DISK = N'" + ruta +
                "' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 10";
                cnn.Open();
                SqlCommand borrarRestaura = new SqlCommand(Proceso, cnn);
                borrarRestaura.ExecuteNonQuery();
                MessageBox.Show("La base de datos ha sido restaurada satisfactoriamente! Vuelve a Iniciar El Aplicativo", "Restauración de base de datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
            catch (Exception)
            { }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }

            }
        }

        private void btnRespaldos_Click(object sender, EventArgs e)
        {
            PanelPadre.Controls.Clear();
            CopiasDB control = new CopiasDB();
            control.Dock = DockStyle.Fill;
            PanelPadre.Controls.Add(control);
        }
    }
}
