using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SistemaAsistencias.Datos;
using SistemaAsistencias.Logica;
using System.IO;

namespace SistemaAsistencias.Presentacion
{
    public partial class ControlUsuario : UserControl
    {
        public ControlUsuario()
        {
            InitializeComponent();
        }

        int idUsuario;

        private void ControlUsuario_Load(object sender, EventArgs e)
        {
            mostrarUsuarios();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            limpiar();
            habilitarPaneles();
            mostrarModulos();
        }

        private void limpiar() 
        {
            txtNombre.Clear();
            txtPassword.Clear();
            txtUsuario.Clear();
            txtApellidos.Clear();
        }

        private void habilitarPaneles() 
        {
            panelRegistro.Visible = true;
            lblEscogerIcono.Visible = true;
            panelIcono.Visible = false;
            panelRegistro.Dock = DockStyle.Fill;
            panelRegistro.BringToFront();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
            btnVolver.Visible = true;
        }

        private void mostrarModulos() 
        {
            ModulosController funcion = new ModulosController();
            DataTable dtModulos = new DataTable();
            funcion.mostrarModulos(ref dtModulos);
            dgModulos.DataSource = dtModulos;
            dgModulos.Columns["idModulo"].Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text) || !string.IsNullOrEmpty(txtApellidos.Text))
            {
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtPassword.Text))
                    {
                        if (lblEscogerIcono.Visible == false)
                        {
                            insertarUsuarios();
                        }
                        else { MessageBox.Show("Ingrese un icono"); }
                    }
                    else { MessageBox.Show("El campo de contraseña debe de estar lleno", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else { MessageBox.Show("El campo de usuario debe de estar lleno", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Los campos de nombre y apellidos deben de estar llenos", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information); }            
        }

        private void insertarUsuarios() 
        {
            UsuarioModel parametros = new UsuarioModel();
            UsuarioController funcion = new UsuarioController();

            parametros.nombres = txtNombre.Text;
            parametros.apellidos = txtApellidos.Text;
            parametros.usuario = txtUsuario.Text;
            parametros.clave = txtPassword.Text;

            MemoryStream ms = new MemoryStream();
            pcIcono.Image.Save(ms, pcIcono.Image.RawFormat);
            parametros.icono = ms.GetBuffer();

            if (funcion.insertarUsuario(parametros) == true) 
            {
                obtenerIdUsuario();
                insertarPermisos();
            }
        }

        private void insertarPermisos() 
        {
            foreach (DataGridViewRow row in dgModulos.Rows)
            {
                int idModulo = Convert.ToInt32(row.Cells["idModulo"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);

                if(marcado == true) 
                {
                    PermisosModel parametros = new PermisosModel();
                    PermisosController funcion = new PermisosController();

                    parametros.idModulo = idModulo;
                    parametros.idUsuario = idUsuario;
                    parametros.estado = "N";
                    funcion.insertarPermisos(parametros);
                }
            }
            mostrarUsuarios();
            panelRegistro.Visible = false;
        }

        private void obtenerIdUsuario() 
        {
            UsuarioController funcion = new UsuarioController();
            funcion.obtenerIdUsuario(ref idUsuario, txtUsuario.Text);
        }

        private void mostrarUsuarios() 
        {
            DataTable dtUsuarios = new DataTable();
            UsuarioController funcion = new UsuarioController();
            funcion.mostrarUsuarios(ref dtUsuarios);
            dgUsuario.DataSource = dtUsuarios;
            diseñarDgvUsuarios();
        }

        private void diseñarDgvUsuarios() 
        {
            Bases.DiseñoDG(ref dgUsuario);
            Bases.DiseñoDGEliminar(ref dgUsuario);
            dgUsuario.Columns["idUsuario"].Visible = false;
            dgUsuario.Columns["clave"].Visible = false;
            dgUsuario.Columns["icono"].Visible = false;
        }

        private void lblEscogerIcono_Click(object sender, EventArgs e)
        {
            mostrarPanelIcono();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pcIcono.Image = pictureBox3.Image;
            ocultarPanelIconos();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pcIcono.Image = pictureBox4.Image;
            ocultarPanelIconos();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pcIcono.Image = pictureBox5.Image;
            ocultarPanelIconos();
        }

        private void ocultarPanelIconos() 
        {
            panelIcono.Visible = false;
            lblEscogerIcono.Visible = false;
            pcIcono.Visible = true;
        }

        private void pcAgregarIconoPC_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes|*.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargardor de imagenes";

            if (dlg.ShowDialog() == DialogResult.OK) 
            {
                pcIcono.BackgroundImage = null;
                pcIcono.Image = new Bitmap(dlg.FileName);
                ocultarPanelIconos();
            }
        }

        private void pcIcono_Click(object sender, EventArgs e)
        {
            mostrarPanelIcono();
        }

        private void mostrarPanelIcono() 
        {
            panelIcono.Visible = true;
            panelIcono.Dock = DockStyle.Fill;
            panelIcono.BringToFront();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelRegistro.Visible = false;
        }

        private void btnVolverIcono_Click(object sender, EventArgs e)
        {
            ocultarPanelIconos();
        }
    }
}
