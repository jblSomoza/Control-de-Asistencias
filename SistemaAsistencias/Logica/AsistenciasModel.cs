using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencias.Logica
{
    class AsistenciasModel
    {

        public int idAsistencia { get; set; }

        public int idPersonal { get; set; }

        public DateTime fechaEntrada { get; set; }

        public DateTime fechaSalida { get; set; }

        public string estado { get; set; }

        public double horas { get; set; }

        public string observacion { get; set; }


    }
}
