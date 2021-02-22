using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using SistemaAsistencias.Logica;


namespace SistemaAsistencias.Logica.AsistenteInstalacion
{
    public partial class InstalacionDB : Form
    {
        public InstalacionDB()
        {
            InitializeComponent();
        }

        private AES aes = new AES();
        string nombreEquipo;
        string ruta;
        public static int milisegundo;
        public static int segundos;
        public static int segundos1;
        public static int minutos1;
        public static int milisegundo1;

        private void InstalacionDB_Load(object sender, EventArgs e)
        {
            centrarPaneles();
            reemplazar();
            comprobarServidores();
            conectar();
        }

        private void conectar() 
        {
            if (btnInstalarServidor.Visible == true)
            {
                comprobarServidorNoSql();
            }
        }

        private void comprobarServidorNoSql() 
        {
            txtservidor.Text = ".";
            ejecutarScryptEliminarBase();
            ejecutarScryptCrearDB();
        }

        private void centrarPaneles()
        {
            Panel2.Location = new Point((Width - Panel2.Width) / 2, (Height - Panel2.Height) / 2);
            nombreEquipo = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Cursor = Cursors.WaitCursor;
            Panel4.Visible = false;
            Panel4.Dock = DockStyle.None;
        }

        private void reemplazar()
        {
            txtCrear_procedimientos.Text = txtCrear_procedimientos.Text.Replace("SistemaAsistencias", TXTbasededatos.Text);

            txtEliminarBase.Text = txtEliminarBase.Text.Replace("SistemaAsistencias", TXTbasededatos.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("StrixOwl", txtusuario.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("SistemaAsistencias", TXTbasededatos.Text);
            txtCrearUsuarioDb.Text = txtCrearUsuarioDb.Text.Replace("softwarereal", lblcontraseña.Text);

            txtCrear_procedimientos.Text = txtCrear_procedimientos.Text + Environment.NewLine + txtCrearUsuarioDb.Text;
        }

        private void comprobarServidores()
        {
            txtservidor.Text = @".\" + lblnombredeservicio.Text;
            ejecutarScryptEliminarBase();
            ejecutarScryptCrearDB();

        }

        private void ejecutarScryptEliminarBase()
        {
            string str;
            SqlConnection conn = new SqlConnection("Data source=" + txtservidor.Text + ";Initial Catalog=master;Integrated Security=True");
            str = txtEliminarBase.Text;
            SqlCommand cmd = new SqlCommand(str, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void ejecutarScryptCrearDB()
        {
            SqlConnection cnn = new SqlConnection("Server=" + txtservidor.Text + "; " + "database=master; integrated security=yes");
            string s = "CREATE DATABASE " + TXTbasededatos.Text;
            SqlCommand cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SavetoXML(aes.Encrypt("Data Source=" + txtservidor.Text +
                    "; Initial Catalog=" + TXTbasededatos.Text + ";Integrated Security=True", Desencryptacion.appPwdUnique, int.Parse("256")));
                ejecutarScript();
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
                label3.Text = @"Instancia Encontrada...
                    No Cierre esta Ventana, se cerrara Automaticamente cuando este todo listo";
                Panel6.Visible = false;
                timer4.Start();
            }
            catch (Exception ex)
            {
                btnInstalarServidor.Visible = true;
                Panel6.Visible = true;
                Panel4.Visible = false;
                Panel4.Dock = DockStyle.None;
                lblbuscador_de_servidores.Text = "De click a instalar Servidor, luego de click a Si cuando se le pida, luego presione ACEPTAR y espere por favor ";
            }
        }

        private void ejecutarScript()
        {
            ruta = Path.Combine(Directory.GetCurrentDirectory(), txtnombre_scrypt.Text + ".txt");
            //Sirve para escribir
            StreamWriter sw;

            try
            {
                if (File.Exists(ruta) == false)
                {
                    sw = File.CreateText(ruta);
                    sw.WriteLine(txtCrear_procedimientos.Text);
                    sw.Flush();
                    sw.Close();
                }
                else if (File.Exists(ruta) == true)
                {
                    File.Delete(ruta);
                    sw = File.CreateText(ruta);
                    sw.WriteLine(txtCrear_procedimientos.Text);
                    sw.Flush();
                    sw.Close();
                }

            }
            catch (Exception)
            { }

            try
            {
                //restaurar el scrypt por consola
                //Abre la consola windows
                Process Pross = new Process();
                Pross.StartInfo.FileName = "sqlcmd";
                Pross.StartInfo.Arguments = " -S " + txtservidor.Text + " -i " + txtnombre_scrypt.Text + ".txt";
                Pross.Start();
            }
            catch (Exception)
            { }
        }

        private void SavetoXML(object dbcnString)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            root.Attributes[0].Value = Convert.ToString(dbcnString);
            XmlTextWriter writer = new XmlTextWriter("ConnectionString.xml", null);
            writer.Formatting = Formatting.Indented;
            doc.Save(writer);
            writer.Close();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            timer3.Stop();
            milisegundo += 1;
            mil3.Text = milisegundo.ToString();
            if (milisegundo == 60)
            {
                segundos += 1;
                seg3.Text = segundos.ToString();
                milisegundo = 0;
            }
            if (segundos == 15)
            {
                timer4.Stop();
                try
                {
                    File.Delete(ruta);
                }
                catch (Exception ex)
                {

                }
                Dispose();
            }
        }

        private void btnInstalarServidor_Click(object sender, EventArgs e)
        {            
            try
            {
                txtArgumentosini.Text = txtArgumentosini.Text.Replace("PRUEBAFINAL22", lblnombredeservicio.Text);
                TimerCRARINI.Start();
                execute();
                timer2.Start();
                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void execute() 
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "SQLEXPR_x64_ENU.exe";
                process.StartInfo.Arguments = "/ConfigurationFile=ConfigurationFile.ini /ACTION=Install /IACCEPTSQLSERVERLICENSETERMS /SECURITYMODE=SQL /SAPWD="
                    + lblcontraseña.Text + " /SQLSYSADMINACCOUNTS=" + nombreEquipo;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
                process.Start();

                Panel4.Visible = true;
                Panel4.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void TimerCRARINI_Tick(object sender, EventArgs e)
        {
            string rutaPreparar;
            StreamWriter sw;
            rutaPreparar = Path.Combine(Directory.GetCurrentDirectory(), "ConfigurationFile.ini");
            rutaPreparar = rutaPreparar.Replace("Configuration.ini", @"SQLEXPR_x64_ENU\ConfigurationFile.ini");
            if (File.Exists(rutaPreparar) == true)
            {
                TimerCRARINI.Stop();
            }

            try
            {
                sw = File.CreateText(rutaPreparar);
                sw.WriteLine(txtArgumentosini.Text);
                sw.Flush();
                sw.Close();
                TimerCRARINI.Stop();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            milisegundo1 += 1;
            milise.Text = Convert.ToString(milisegundo1);
            if (milisegundo1 == 60)
            {
                segundos1 += 1;
                seg.Text = Convert.ToString(segundos1);
                milisegundo1 = 0;
            }

            if (segundos1 == 60)
            {
                minutos1 += 1;
                min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }

            if (minutos1 == 6)
            {
                timer2.Stop();
                ejecutarScryptEliminarDB();
                ejecutarScryptCrearDataB();
                timer3.Start();
            }
        }

        private void ejecutarScryptEliminarDB() 
        {
            string str;
            SqlConnection conn = new SqlConnection("Data source=" + txtservidor.Text + ";Initial Catalog=master;Integrated Security=True");
            str = txtEliminarBase.Text;
            SqlCommand cmd = new SqlCommand(str, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void ejecutarScryptCrearDataB() 
        {
            var cnn = new SqlConnection("Server=" + txtservidor.Text + "; " + "database=master; integrated security=yes");
            string s = "CREATE DATABASE " + TXTbasededatos.Text;
            SqlCommand cmd = new SqlCommand(s, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                SavetoXML(aes.Encrypt("Data Source=" + txtservidor.Text +
                    "; Initial Catalog=" + TXTbasededatos.Text + ";Integrated Security=True", Desencryptacion.appPwdUnique, int.Parse("256")));
                ejecutarScript();
                timer4.Start();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                } 
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            milisegundo1 += 1;
            milise.Text = Convert.ToString(milisegundo1);
            if (milisegundo1 == 60)
            {
                segundos1 += 1;
                seg.Text = Convert.ToString(segundos1);
                milisegundo1 = 0;
            }

            if (segundos1 == 60)
            {
                minutos1 += 1;
                min.Text = Convert.ToString(minutos1);
                segundos1 = 0;
            }

            if (minutos1 == 1)
            {
                timer2.Stop();
                ejecutarScryptEliminarDB();
                ejecutarScryptCrearDataB();
            }
        }
    }
}
