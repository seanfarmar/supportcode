using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Logic.NoSql.Interface
{
    /// <summary>
    /// Interfaz que debe cumplir cada clase que almacene una entidad.
    /// </summary>
    public interface IAlmacenaPlantillaNoSql
    {
        #region Metodos

        /// <summary>
        /// Metodo que almacena una plantilla.
        /// </summary>
        /// <param name="plantilla">Clase con la informacion de la plantilla.</param>
        void AlmacenaPlantilla(PlantillaEstadoCuenta plantilla);

        #endregion
    }
}
