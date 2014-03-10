using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Aplicacion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;


namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.App.Nesb.Public
{
    public class PublicaPlantilla:IWantToRunWhenBusStartsAndStops
    {

        #region Propiedades

        GenerarPlantillas GeneradorPlantilas;
        public IBus Bus { get; set; }
        

        
        #endregion


        public void Start()
        {
            DateTime fecha = new DateTime(2014,2,5);

            this.GeneradorPlantilas = new GenerarPlantillas(fecha,33);

            this.GeneradorPlantilas.PublicarEstadoCuenta += new PublicacionEstadoCuenta(PublicarPlantilla);

            this.GeneradorPlantilas.ObtenerInsumos();
   
        }

        private void PublicarPlantilla(object sender, EventoGeneraPlantilla<PlantillaEstadoCuenta> e)
        {
            Bus.Send("fanalca.plantillas.adeinco.estadocuenta.app.nesb.server2",e.Plantilla);
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
