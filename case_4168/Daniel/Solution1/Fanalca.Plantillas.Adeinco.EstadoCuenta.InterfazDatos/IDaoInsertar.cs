using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;


namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.InterfazDatos
{
    /// <summary>
    /// Interfaz con el contrado que debe cumplir toda clase que implemente la interfaz
    /// </summary>
    public interface IDaoInsertar
    {
        /// <summary>
        /// Definición del metodo insertar que debe cumplir toda clase que implemente la interfaz
        /// </summary>
        /// <param name="plantilla">Objeto plantilla</param>
        void Insertar(PlantillaEstadoCuenta plantilla);
    }
}
