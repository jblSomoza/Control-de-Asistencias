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
using System.Data.SqlClient;
using System.Xml;

namespace SistemaAsistencias.Logica.AsistenteInstalacion
{
    public partial class ConexionRemota : Form
    {
        public ConexionRemota()
        {
            InitializeComponent();
        }

        string cadenaConexion;
        string indicadorConexion;
        int idUsuario;
        private AES aes = new AES();


        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIp.Text))
            {
                conectarManualmente();
            }
            else 
            {
                MessageBox.Show("Ingrese la ip", "Conexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void conectarManualmente() 
        {
            string ip = txtIp.Text;
            cadenaConexion = "Data Source =" + ip + ";Initial Catalog=SistemaAsistencias;Integrated Security=False;User Id=strixowl;Password=strixOwl";
            comprobarConexion();
            if (indicadorConexion == "Hay Conexion")
            {
                SavetoXML(aes.Encrypt(cadenaConexion, Desencryptacion.appPwdUnique, int.Parse("256")));
                MessageBox.Show("Conexion Correcta. Vuelve a abrir el sistema", "Conexion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dispose();
            }
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

        private void comprobarConexion() 
        {
            try
            {
                SqlConnection connection = new SqlConnection(cadenaConexion);
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios", connection);
                idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                indicadorConexion = "Hay Conexion";
            }
            catch (Exception)
            {
                indicadorConexion = "No Hay Conexion";
            }
        }
    }
}
