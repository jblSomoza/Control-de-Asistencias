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
    public partial class TomarAsistencias : Form
    {
        public TomarAsistencias()
        {
            InitializeComponent();
        }
        string identificacion;
        int idPersonal;
        int contador;
        DateTime fechaRegistro;

        private void TomarAsistencias_Load(object sender, EventArgs e)
        {
            
        }

        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblHora2.Text = DateTime.Now.ToString("hh:mm:ss");
            lblFecha2.Text = DateTime.Now.ToShortDateString();
        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            buscarPersonalIdent();
            if (identificacion == txtIdentificacion.Text) 
            {
                buscarAsistenciaId();
                if (contador == 0) 
                {
                    //No hay entradas registradas
                    DialogResult result = MessageBox.Show("Agregar una observacion", "Observacion", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes) 
                    {
                        panelObservacion.Visible = true;
                        panelObservacion.Location = new Point(panel5.Location.X, panel5.Location.Y);
                        panelObservacion.Size = new Size(panel5.Width, panel5.Height);
                        panelObservacion.BringToFront();
                        rtxObservacion.Clear();
                        rtxObservacion.Focus();                        
                    }
                    else 
                    {
                        insertarAsistencias();
                    }
                }
                else 
                {
                    //Registra una salida
                    confimarSalida();
                }
            }
        }

        private void confimarSalida() 
        {
            AsistenciasModel parametros = new AsistenciasModel();
            AsistenciasController funcion = new AsistenciasController();
            parametros.idPersonal = idPersonal;
            parametros.fechaSalida = DateTime.Now;
            parametros.horas = Bases.DateDiff(Bases.DateInterval.Hour, fechaRegistro, DateTime.Now);
            if (funcion.modificarAsistencia(parametros) == true) 
            {
                txtAviso.Text = "SALIDA REGISTRADA";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }

        private void insertarAsistencias() 
        {
            AsistenciasModel parametros = new AsistenciasModel();
            AsistenciasController funcion = new AsistenciasController();

            if (string.IsNullOrEmpty(rtxObservacion.Text)) 
            {
                rtxObservacion.Text = "-";
            }

            parametros.idPersonal = idPersonal;
            parametros.fechaEntrada = DateTime.Now;
            parametros.fechaSalida = DateTime.Now;
            parametros.estado = "ENTRADA";
            parametros.horas = 0;
            parametros.observacion = rtxObservacion.Text;
            
            if (funcion.insertarAsistencias(parametros) == true) 
            {
                txtAviso.Text = "ENTRADA REGISTRADA";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
                panelObservacion.Visible = false;
            }
        }

        private void buscarAsistenciaId() 
        {
            DataTable dt = new DataTable();
            AsistenciasController funcion = new AsistenciasController();
            funcion.buscarAsistenciaId(ref dt, idPersonal);
            contador = dt.Rows.Count;

            if (contador > 0) 
            {
                fechaRegistro = Convert.ToDateTime(dt.Rows[0]["fechaEntrada"]);
            }
        }

        private void buscarPersonalIdent() 
        {
            DataTable dtPersonal = new DataTable();
            PersonalController funcion = new PersonalController();

            funcion.buscarPersonalIdentificacion(ref dtPersonal, txtIdentificacion.Text);
            if (dtPersonal.Rows.Count > 0) 
            {
                identificacion = dtPersonal.Rows[0]["identificacion"].ToString();
                idPersonal = Convert.ToInt32(dtPersonal.Rows[0]["idPersonal"]);
                txtNombre.Text = dtPersonal.Rows[0]["nombres"].ToString() + " " + dtPersonal.Rows[0]["apellidos"].ToString();
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            insertarAsistencias();
        }
    }
}
