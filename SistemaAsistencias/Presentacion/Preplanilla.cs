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
using System.Globalization;

namespace SistemaAsistencias.Presentacion
{
    public partial class Preplanilla : UserControl
    {
        public Preplanilla()
        {
            InitializeComponent();
        }

        private void Preplanilla_Load(object sender, EventArgs e)
        {
            txtdesde.Text = DateTime.Now.ToString();
            txthasta.Text = DateTime.Now.ToString();
            calcularNoSemana();            
        }

        private void ReporteAsistencias() 
        {
            Reportes.ReportAsistencias rpt = new Reportes.ReportAsistencias();
            DataTable dt = new DataTable();
            AsistenciasController funcion = new AsistenciasController();
            funcion.mostrarAsistenciasDiarias(ref dt, txtdesde.Value, txthasta.Value, Convert.ToInt32(lblnumerosemana.Text));
            rpt.DataSource = dt;
            rpt.table1.DataSource = dt;
            reportViewer1.ReportSource = rpt;
            reportViewer1.RefreshReport();
        }

        private void calcularNoSemana() 
        {
            DateTime v2 = txthasta.Value;
            //Obtenemos el numero de semana del año actual
            lblnumerosemana.Text = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(v2, CalendarWeekRule.FirstDay, v2.DayOfWeek).ToString();
        }

        private void txtdesde_ValueChanged(object sender, EventArgs e)
        {
            calcularNoSemana();
            //ReporteAsistencias();
        }

        private void txthasta_ValueChanged(object sender, EventArgs e)
        {
            calcularNoSemana();
            //ReporteAsistencias();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            ReporteAsistencias();
        }
    }
}
