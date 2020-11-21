using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SistemaAsistencias.Logica;
using SistemaAsistencias.Datos;

namespace SistemaAsistencias.Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }
        int idCargo = 0;
        int desde = 1;
        int hasta = 10;
        int contador;
        int idPersonal;
        string identificacion;
        private int itemsPagina = 10;
        string estado;
        int totalPaginas;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            localizarDgCargos();
            PanelCargos.Visible = false;
            PanelPaginado.Visible = false;

            PanelRegistro.Visible = true;
            PanelRegistro.Dock = DockStyle.Fill;

            btnGuardarPersonal.Visible = true;
            btnGuardarCambiosPersonal.Visible = false;
            limpiar();
        }

        private void localizarDgCargos() 
        {
            dgListadoCargos.Location = new Point(txtSueldo.Location.X, txtSueldo.Location.Y);
            dgListadoCargos.Size = new Size(469, 141);
            dgListadoCargos.Visible = true;
            lblSueldo.Visible = false;
            panelBtnAgregarPersonal.Visible = false;
        }

        private void limpiar() 
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldo.Clear();
            buscarCargo();
        }

        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text) || !string.IsNullOrEmpty(txtApellidos.Text)) 
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text)) 
                {
                    if (!string.IsNullOrEmpty(cboPais.Text)) 
                    {
                        if (idCargo > 0) 
                        {
                            if (!string.IsNullOrEmpty(txtSueldo.Text)) 
                            {
                                insertarPersonal();
                            }
                        }
                    }
                }
            }
        }

        private void mostrarPersonal() 
        {
            DataTable dtPersonal = new DataTable();
            PersonalController funcion = new PersonalController();

            funcion.mostrarPersonal(ref dtPersonal, desde, hasta);
            dgPersonal.DataSource = dtPersonal;
            diseñarDgPersonal();            
        }

        private void diseñarDgPersonal() 
        {
            Bases.DiseñoDG(ref dgPersonal);
            Bases.DiseñoDGEliminar(ref dgPersonal);
            PanelPaginado.Visible = true;
            dgPersonal.Columns["idPersonal"].Visible = false;
            dgPersonal.Columns["idCargo"].Visible = false;
        }

        private void insertarPersonal() 
        {
            Logica.Personal parametros = new Logica.Personal();
            PersonalController funcion = new PersonalController();

            parametros.nombres = txtNombres.Text;
            parametros.apellidos = txtApellidos.Text;
            parametros.identificacion = txtIdentificacion.Text;
            parametros.pais = cboPais.Text;
            parametros.idCargo = idCargo;
            parametros.sueldoPorHora = Convert.ToDouble(txtSueldo.Text);

            if (funcion.insertarPersonal(parametros) == true) 
            {
                reiniciarPaginado();
                mostrarPersonal();
                PanelRegistro.Visible = false;
            }
        }

        private void insertarCargo() 
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    CargoModel parametros = new CargoModel();
                    CargoController funcion = new CargoController();

                    parametros.cargo = txtCargoG.Text;
                    parametros.sueldoPorHora = Convert.ToDouble(txtSueldoG.Text);

                    if (funcion.insertarCargo(parametros) == true)
                    {
                        txtCargoG.Clear();
                        buscarCargo();
                        PanelCargos.Visible = false;
                    }
                }
                else { MessageBox.Show("Agregue el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Agregue el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void buscarCargo() 
        {
            CargoController funcion = new CargoController();
            DataTable dt = new DataTable();

            funcion.buscarCargo(txtCargo.Text, ref dt);
            dgListadoCargos.DataSource = dt;
            Bases.DiseñoDG(ref dgListadoCargos);
            dgListadoCargos.Columns[1].Visible = false;
            dgListadoCargos.Columns[3].Visible = false;
            dgListadoCargos.Visible = true;
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            buscarCargo();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
            btnGuardarCargo.Visible = true;
            btnGuardarCargoCam.Visible = false;
            txtCargoG.Clear();
            txtSueldoG.Clear();
        }

        private void btnGuardarCargo_Click(object sender, EventArgs e)
        {
            insertarCargo();
        }

        private void txtSueldoG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoG, e);
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldo, e);
        }

        private void dgListadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgListadoCargos.Columns["EditarC"].Index) 
            {
                ObtenerCargosEditar();
            }

            if (e.ColumnIndex == dgListadoCargos.Columns["Cargo"].Index) 
            {
                obtenerDatosCargo();   
            }
        }

        private void obtenerDatosCargo() 
        {
            idCargo = Convert.ToInt32(dgListadoCargos.SelectedCells[1].Value);
            txtCargo.Text = dgListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldo.Text = dgListadoCargos.SelectedCells[3].Value.ToString();
            dgListadoCargos.Visible = false;
            panelBtnAgregarPersonal.Visible = true;
            lblSueldo.Visible = false;
        }

        private void ObtenerCargosEditar() 
        {
            idCargo = Convert.ToInt32(dgListadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = dgListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoG.Text = dgListadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarCargo.Visible = false;
            btnGuardarCargoCam.Visible = true;
            txtCargoG.Focus();
            txtCargoG.SelectAll();
            PanelCargos.Visible = true;
            PanelCargos.Dock = DockStyle.Fill;
            PanelCargos.BringToFront();
        }

        private void btnVolverCargos_Click(object sender, EventArgs e)
        {
            PanelCargos.Visible = false;
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            PanelRegistro.Visible = false;
            PanelPaginado.Visible = true;
        }

        private void btnGuardarCargoCam_Click(object sender, EventArgs e)
        {
            editarCargos();
        }

        private void editarCargos() 
        {
            CargoModel parametros = new CargoModel();
            CargoController funcion = new CargoController();

            parametros.idCargo = idCargo;
            parametros.cargo = txtCargoG.Text;
            parametros.sueldoPorHora = Convert.ToDouble(txtSueldoG.Text);

            if (funcion.actualizarCargo(parametros) == true)
            {
                txtCargo.Clear();
                buscarCargo();
                PanelCargos.Visible = false;
            }
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            reiniciarPaginado();
            mostrarPersonal();            
        }

        private void reiniciarPaginado() 
        {
            desde = 1;
            hasta = 10;
            contar();
            if (contador > hasta) 
            {
                btnPagSiguiente.Visible = true;
                btnPagAnterior.Visible = false;
                btnPagUltima.Visible = true;
                btnPagPrimera.Visible = true;
            }
            else 
            {
                btnPagSiguiente.Visible = false;
                btnPagAnterior.Visible = false;
                btnPagUltima.Visible = false;
                btnPagPrimera.Visible = false;
            }
            paginar();
        }

        private void dgPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgPersonal.Columns["Eliminar"].Index) 
            {
                DialogResult result = MessageBox.Show("Solo se cambiara el estado para que no pueda acceder, Desea continuar?",
                    "Eliminado registros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK) 
                {
                    eliminarPersonal();
                }                
            }

            if (e.ColumnIndex == dgPersonal.Columns["Editar"].Index) 
            {
                obtenerDatos();
            }
        }

        private void obtenerDatos() 
        {
            idPersonal = Convert.ToInt32(dgPersonal.SelectedCells[2].Value);
            estado = dgPersonal.SelectedCells[9].Value.ToString();
            if (estado == "ELIMINADO") 
            {
                restaurarPersonal();
            }
            else 
            {
                localizarDgCargos();
                txtNombres.Text = dgPersonal.SelectedCells[3].Value.ToString();
                txtApellidos.Text = dgPersonal.SelectedCells[4].Value.ToString();
                txtIdentificacion.Text = dgPersonal.SelectedCells[5].Value.ToString();
                cboPais.Text = dgPersonal.SelectedCells[11].Value.ToString();
                txtCargo.Text = dgPersonal.SelectedCells[7].Value.ToString();
                idCargo = Convert.ToInt32(dgPersonal.SelectedCells[8].Value);
                txtSueldo.Text = dgPersonal.SelectedCells[6].Value.ToString();

                PanelPaginado.Visible = false;
                PanelRegistro.Visible = true;
                PanelRegistro.Dock = DockStyle.Fill;
                dgListadoCargos.Visible = false;
                lblSueldo.Visible = true;
                panelBtnAgregarPersonal.Visible = true;
                btnGuardarPersonal.Visible = false;
                btnGuardarCambiosPersonal.Visible = true;
                PanelCargos.Visible = false;
            }
        }

        private void restaurarPersonal() 
        {
            DialogResult result = MessageBox.Show("Este usuario se elimino, desea volver a habilitarlo", "Restauracon de registros",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) 
            {
                habilitarPersonal();
            }
        }

        private void habilitarPersonal() 
        {
            Logica.Personal parametros = new Logica.Personal();
            PersonalController funcion = new PersonalController();

            parametros.idPersonal = idPersonal;
            if (funcion.restaurarPersonal(parametros) == true) 
            {
                mostrarPersonal();
            }
        }

        private void eliminarPersonal() 
        {
            idPersonal = Convert.ToInt32(dgPersonal.SelectedCells[2].Value);
            identificacion = dgPersonal.SelectedCells[5].Value.ToString();

            Logica.Personal parametros = new Logica.Personal();
            PersonalController funcion = new PersonalController();
            parametros.idPersonal = idPersonal;
            parametros.nombres = "";
            parametros.apellidos = "";
            parametros.identificacion = identificacion;
            parametros.pais = "";
            parametros.idCargo = 0;
            parametros.sueldoPorHora = 0;
            parametros.estado = "E";

            if (funcion.eliminarPersonal(parametros) == true) 
            {
                mostrarPersonal();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            diseñarDgPersonal();
            timer1.Stop();
        }

        private void btnGuardarCambiosPersonal_Click(object sender, EventArgs e)
        {
            editarPersonal();
        }

        private void editarPersonal() 
        {
            Logica.Personal parametros = new Logica.Personal();
            PersonalController funcion = new PersonalController();

            parametros.idPersonal = idPersonal;
            parametros.nombres = txtNombres.Text;
            parametros.apellidos = txtApellidos.Text;
            parametros.identificacion = txtIdentificacion.Text;
            parametros.pais = cboPais.Text;
            parametros.idCargo = idCargo;
            parametros.sueldoPorHora = Convert.ToDouble(txtSueldo.Text);
            parametros.estado = "M";

            if (funcion.actualizarPersonal(parametros) == true) 
            {
                mostrarPersonal();
                PanelRegistro.Visible = false;
            }
        }

        private void btnPagSiguiente_Click(object sender, EventArgs e)
        {
            desde += 10;
            hasta += 10;
            mostrarPersonal();
            contar();
            if (contador > hasta) 
            {
                btnPagSiguiente.Visible = true;
                btnPagAnterior.Visible = true;
            }
            else 
            {
                btnPagSiguiente.Visible = false;
                btnPagAnterior.Visible = true;
            }
            paginar();
        }

        private void paginar() 
        {
            try
            {
                //Pagina actual
                lblPaginaActual.Text = (hasta / itemsPagina).ToString();
                //Total de paginas
                lblPaginasTotal.Text = Math.Ceiling(Convert.ToSingle(contador) / itemsPagina).ToString();
                totalPaginas = Convert.ToInt32(lblPaginasTotal.Text);
            }
            catch (Exception) {  }
        }

        private void contar() 
        {
            PersonalController funcion = new PersonalController();
            funcion.contarPersonal(ref contador);
        }

        private void btnPagAnterior_Click(object sender, EventArgs e)
        {
            desde -= 10;
            hasta -= 10;
            mostrarPersonal();
            contar();
            if (contador > hasta) 
            {
                btnPagSiguiente.Visible = true;
                btnPagAnterior.Visible = true;
            }
            else 
            {
                btnPagSiguiente.Visible = false;
                btnPagAnterior.Visible = true;
            }

            if(desde == 1) 
            {
                reiniciarPaginado();
            }
            paginar();
        }

        private void btnPagUltima_Click(object sender, EventArgs e)
        {
            hasta = totalPaginas * itemsPagina;
            desde = hasta - 9;
            mostrarPersonal();
            contar();

            if (contador > hasta)
            {
                btnPagSiguiente.Visible = true;
                btnPagAnterior.Visible = true;
            }
            else
            {
                btnPagSiguiente.Visible = false;
                btnPagAnterior.Visible = true;
            }
            paginar();
        }

        private void btnPagPrimera_Click(object sender, EventArgs e)
        {
            reiniciarPaginado();
            mostrarPersonal();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            buscarPersonal();
        }

        private void buscarPersonal() 
        {
            PersonalController funcion = new PersonalController();
            DataTable dtPersonal = new DataTable();

            funcion.buscarPersonal(ref dtPersonal, desde, hasta, txtBuscador.Text);
            dgPersonal.DataSource = dtPersonal;
            diseñarDgPersonal();
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            reiniciarPaginado();
            mostrarPersonal();
        }
    }
}
