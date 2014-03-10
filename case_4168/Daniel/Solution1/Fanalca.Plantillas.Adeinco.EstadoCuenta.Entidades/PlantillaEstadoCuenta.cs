using System;
using NServiceBus;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades
{
    /// <summary>
    /// Clase que  para almacena la información de la plantilla 
    /// </summary>
    [Serializable]
    public class PlantillaEstadoCuenta:IMessage
    {
        #region Propiedades

        /// <summary>
        /// Clave o identity para cada plantilla
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ruta donde esta la plantilla
        /// </summary>
        public string RutaPlantilla { get; set; }

        /// <summary>
        /// Fecha de generacion
        /// </summary>
        public DateTime FechaRegistro { get; set; }

        /// <summary>
        /// Numero de identificacion
        /// </summary>
        public decimal NumeroIdentificacion { get; set; }

        /// <summary>
        /// Numero del credito
        /// </summary>
        public decimal NumeroCredito { get; set; }

        /// <summary>
        /// Indica si  el estado de la generacion de la plantilla es satisfactorio 
        /// </summary>
        public bool Estado { get; set; }

        /// <summary>
        /// Mensaje del proceso de generacion del estado de cuenta.
        /// </summary>
        public string Mesaje { get; set; }

        /// <summary>
        /// Ruta donde queda almancenada la plantilla mail
        /// </summary>
        public String RutaCorreoPlantilla { get; set; }

        #endregion

        #region Constructor

        public PlantillaEstadoCuenta()
        {
            this.Id = Guid.NewGuid();
        }

        #endregion
    }
}
