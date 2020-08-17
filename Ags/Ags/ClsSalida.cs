using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Ags
{
    class ClsSalida
    {
        FormProgreso form;
        
        public ClsSalida(FormProgreso frm)
        {
            form = frm;
        }
        public void Actualizar(int NumGeneracion, int MaxGeneracion, double Fitness, string Salida)
        {
            form.Actualizar(NumGeneracion, MaxGeneracion, Fitness, Salida);
        }
    }
}
