using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Logic.Interfaz
{
    public interface ILogicEstadoCuentaService
    {
        /// <summary>
        /// Metodo que inserta un registro de estado de cuenta.
        /// </summary>
        /// <param name="plantilla">Plantilla.</param>
        void InsertaEstadoCuenta(PlantillaEstadoCuenta plantilla);
    }
}
