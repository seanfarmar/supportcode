using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Interfaces
{

    /// <summary>
    /// Interfaz para las clases de Acceso a Datos Plantillas
    /// </summary>
    public interface IConsultasPlantillaDB
    {
        /// <summary>
        /// Metodo que debe implementar la clase que use la interfaz para obtener los insumos del reporte
        /// </summary>
        /// <param name="IdConcesionario">Id del concesionario</param>
        /// <returns></returns>
        DataTable ObtenerInsumosPlantilla(decimal IdConcesionario, DateTime fecha);
    }
}
