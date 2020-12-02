using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAsistencias.Logica;
using SistemaAsistencias.Datos;
using System.IO;

namespace SistemaAsistencias.Presentacion.AsistenteInstalacion
{
    public partial class UsuarioPrincipal : Form
    {
        public UsuarioPrincipal()
        {
            InitializeComponent();
        }
        int idUsuario;

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtnombre.Text))
            {
                if (!string.IsNullOrEmpty(TXTCONTRASEÑA.Text)) 
                {
                    if (TXTCONTRASEÑA.Text == txtconfirmarcontraseña.Text)
                    {
                        insertarUsuarioDefecto();
                    }
                    else { MessageBox.Show("Las contraseñas no coinsiden", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                else { MessageBox.Show("Falta ingresar la Contraseña", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Falta ingresar el Nombre", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void insertarUsuarioDefecto() 
        {
            UsuarioModel parametros = new UsuarioModel();
            UsuarioController funcion = new UsuarioController();

            parametros.nombres = txtnombre.Text;
            parametros.apellidos = txtApellidos.Text;
            parametros.usuario = TXTUSUARIO.Text;
            parametros.clave = TXTCONTRASEÑA.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.icono = ms.GetBuffer();
            
            if (funcion.insertarUsuario(parametros) == true) 
            {
                insertarModulos();
                obtenerIdUsuario();
                insertarPermisos();
            }
            
        }

        private void obtenerIdUsuario() 
        {
            UsuarioController funcion = new UsuarioController();
            funcion.obtenerIdUsuario(ref idUsuario, TXTUSUARIO.Text);
        }

        private void insertarPermisos() 
        {
            DataTable dt = new DataTable();
            ModulosController funcionModulos = new ModulosController();
            funcionModulos.mostrarModulos(ref dt);
            foreach (DataRow row in dt.Rows)
            {
                int idModulo = Convert.ToInt32(row["idModulo"]);
                PermisosModel parametros = new PermisosModel();
                PermisosController funcion = new PermisosController();
                parametros.idModulo = idModulo;
                parametros.idUsuario = idUsuario;

                if (funcion.insertarPermisos(parametros) == true) 
                {
                    MessageBox.Show("!LISTO! RECUERDA que para Iniciar Sesión tu Usuario es: " + TXTUSUARIO.Text + " y tu Contraseña es: " + TXTCONTRASEÑA.Text, "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dispose();
                    Login frm = new Login();
                    frm.ShowDialog();
                }
            }
        }

        private void insertarModulos() 
        {
            ModulosModel parametros = new ModulosModel();
            ModulosController funcion = new ModulosController();
            var Modulos = new List<string> { "Usuarios", "Respaldos", "Personal", "PrePlanillas" };
            foreach (var Modulo in Modulos)
            {
                parametros.modulo = Modulo;
                funcion.Insertar_Modulos(parametros);
            }
        }
    }
}
