using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion
{
    /// <summary>
    /// Excepcion que almancenara informacion de error en las clases de acceso a adatos.
    /// </summary>
    public class ExcepcionDatosEstadoCuenta: Exception
    {
        public ExcepcionDatosEstadoCuenta()
      {
      }
   public ExcepcionDatosEstadoCuenta(string message)
      : base(message)
      {
      }
   public ExcepcionDatosEstadoCuenta(string message, Exception inner)
      : base(message, inner)
      {
      }

    }
}
