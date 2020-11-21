using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencias.Logica
{
    public class Personal
    {	
		public int idPersonal { get; set; }

		public string nombres { get; set; }

		public string apellidos { get; set; }

		public string identificacion { get; set; }

		public string pais { get; set; }

		public int idCargo { get; set; }

		public double sueldoPorHora { get; set; }

		public string estado { get; set; }

		public string codigo { get; set; }
	}
}
