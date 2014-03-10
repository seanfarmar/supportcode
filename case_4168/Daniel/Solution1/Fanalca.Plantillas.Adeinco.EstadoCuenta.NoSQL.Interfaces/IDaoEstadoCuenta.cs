using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;


namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Interfaces
{
    
    /// <summary>
    /// Interfaz que deben implementar cada clase de acceso a repositorio NoSQL
    /// 
    /// </summary>
    public interface IDaoEstadoCuenta
    {

        /// <summary>
        /// Inserta la informacion de una plantilla estado de cuenta
        /// </summary>
        /// <param name="plantilla">Información plantilla estado cuenta.</param>
        bool InsertarEstadoCuenta(PlantillaEstadoCuenta plantilla, out string mensaje);

        /// <summary>
        /// Elimina la información de una plantila
        /// estado de cuenta
        /// </summary>
        /// <param name="plantilla"></param>
        bool EliminarEstadoCuenta(PlantillaEstadoCuenta plantilla, out string mensaje);
        
        /// <summary>
        /// Actualiza  la información de una plantilla estado de cuenta.
        /// </summary>
        /// <param name="plantilla"></param>
        bool ActualizarEstadoCuenta(PlantillaEstadoCuenta plantilla, out string mensaje);
        
        List<PlantillaEstadoCuenta> Consulta(decimal identificacion, decimal credito);


    }
}
