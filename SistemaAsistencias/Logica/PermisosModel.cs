﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAsistencias.Logica
{
    class PermisosModel
    {
        public int idPermiso { get; set; }
        public int idModulo { get; set; }
        public int idUsuario { get; set; }
        public string estado { get; set; }
    }
}
