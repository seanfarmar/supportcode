using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Aplicacion
{
    /// <summary>
    /// Evento que almacena la información de la plantilla publicada
    /// </summary>
    /// <typeparam name="T">Plantilla públicada</typeparam>
    public class EventoGeneraPlantilla<T>:EventArgs
    {
        private T _plantilla = default(T);

        public T Plantilla
        {
            get { return _plantilla; }
            set { _plantilla = value; }
        }
        
        /// <summary>
        /// Contructor del evento
        /// </summary>
        /// <param name="plantilla"></param>
        public EventoGeneraPlantilla(T plantilla)
        {
            this.Plantilla = plantilla;
        }

      
    }
}
