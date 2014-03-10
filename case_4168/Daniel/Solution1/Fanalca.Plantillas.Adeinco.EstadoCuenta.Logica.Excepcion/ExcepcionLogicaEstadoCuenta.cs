using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Excepcion
{
    public class ExcepcionLogicaEstadoCuenta : Exception
    {
        public ExcepcionLogicaEstadoCuenta()
        {
        }
        public ExcepcionLogicaEstadoCuenta(string message)
            : base(message)
        {
        }
        public ExcepcionLogicaEstadoCuenta(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
