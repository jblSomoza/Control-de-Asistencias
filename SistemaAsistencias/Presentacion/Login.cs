using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using SistemaAsistencias.Logica.AsistenteInstalacion;
using SistemaAsistencias.Presentacion.AsistenteInstalacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAsistencias.Presentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        string usuario;
        int idUsuario;
        int contador;
        string indicador;

        private void Login_Load(object sender, EventArgs e)
        {
            validarConexion();
        }

        private void validarConexion() 
        {
            verificarConexion();
            if (indicador == "Correcto")
            {
                mostrarUsuarios();
                if (contador == 0)
                {
                    Dispose();
                    UsuarioPrincipal frm = new UsuarioPrincipal();
                    frm.ShowDialog();
                }
                else
                {
                    dibujarUsuarios();
                }
            }
            else
            {
                Dispose();
                EleccionServidor frm = new EleccionServidor();
                frm.ShowDialog();
            }
        }

        private void mostrarUsuarios() 
        {
            DataTable dt = new DataTable();
            UsuarioController funcion = new UsuarioController();
            funcion.mostrarUsuarios(ref dt);
            contador = dt.Rows.Count;
        }

        private void verificarConexion() 
        {
            UsuarioController funcion = new UsuarioController();
            funcion.verificarUsuarios(ref indicador);
        }

        private void dibujarUsuarios() 
        {
            try
            {
                panelUsuario.Visible = true;
                panelUsuario.Dock = DockStyle.Fill;
                panelUsuario.BringToFront();
                DataTable dt = new DataTable();
                UsuarioController funcion = new UsuarioController();
                funcion.mostrarUsuarios(ref dt);
                foreach (DataRow row in dt.Rows)
                {
                    Label b = new Label();
                    Panel p1 = new Panel();
                    PictureBox i1 = new PictureBox();
                    b.Text = row["usuario"].ToString();
                    b.Name = row["idUsuario"].ToString();
                    b.Size = new Size(175, 25);
                    b.Font = new Font("Microsoft Sans Serif", 10);
                    b.BackColor = Color.Transparent;
                    b.ForeColor = Color.White;
                    b.Dock = DockStyle.Bottom;
                    b.TextAlign = ContentAlignment.MiddleCenter;
                    b.Cursor = Cursors.Hand;

                    p1.Size = new Size(155, 167);
                    p1.BorderStyle = BorderStyle.None;
                    p1.BackColor = Color.FromArgb(20, 20, 20);

                    i1.Size = new Size(175, 132);
                    i1.Dock = DockStyle.Top;
                    i1.BackgroundImage = null;
                    byte[] bi = (byte[])row["icono"];
                    MemoryStream ms = new MemoryStream(bi);
                    i1.Image = Image.FromStream(ms);
                    i1.SizeMode = PictureBoxSizeMode.Zoom;
                    i1.Tag = row["usuario"].ToString();
                    i1.Cursor = Cursors.Hand;

                    p1.Controls.Add(b);
                    p1.Controls.Add(i1);
                    b.BringToFront();

                    flowLayoutPanel1.Controls.Add(p1);

                    b.Click += new EventHandler(miEventoLabel);
                    i1.Click += new EventHandler(miEventoImagen);
                }
            }
            catch (Exception) { }
        }

        private void miEventoImagen(object sender, EventArgs e)
        {
            usuario = Convert.ToString(((PictureBox)sender).Tag);
            Icono.Image = ((PictureBox)sender).Image;
            mostrarPanelSesion();
        }

        private void miEventoLabel(object sender, EventArgs e)
        {
            usuario = ((Label)sender).Text;
            Icono.Image = ((PictureBox)sender).Image;
            mostrarPanelSesion();
        }

        private void mostrarPanelSesion() 
        {
            panelSesion.Visible = true;
            panelSesion.Location = new Point((Width - panelSesion.Width) / 2, (Height - panelSesion.Height) / 2);
            panelUsuario.Visible = false;
            txtUsuario.Text = usuario;
            txtUsuario.ForeColor = Color.White;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            validarUsuarios();
        }

        private void validarUsuarios() 
        {
            UsuarioModel parametros = new UsuarioModel();
            UsuarioController funcion = new UsuarioController();
            parametros.clave = txtPassword.Text;
            parametros.usuario = usuario;
            funcion.validarUsuario(parametros, ref idUsuario);

            if(idUsuario > 0) 
            {
                Dispose();
                MenuPrincipal frm = new MenuPrincipal();
                frm.idUsuario = idUsuario;
                frm.loginV = usuario;
                frm.Icono.Image = Icono.Image;
                frm.ShowDialog();
            }
        }

        private void btnInicioSesion_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
