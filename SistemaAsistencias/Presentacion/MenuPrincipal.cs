using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            pnBienvenida.Dock = DockStyle.Fill;
            validarPermisos();
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
    }
}
