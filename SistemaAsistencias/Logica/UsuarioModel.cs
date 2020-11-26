using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencias.Logica
{
    class UsuarioModel
    {
        public int idUsuario { get; set; }

        public string nombres { get; set; }

        public string apellidos { get; set; }

        public string usuario { get; set; }

        public string clave { get; set; }

        public byte[] icono { get; set; }

        public string estado { get; set; }

    }
}
