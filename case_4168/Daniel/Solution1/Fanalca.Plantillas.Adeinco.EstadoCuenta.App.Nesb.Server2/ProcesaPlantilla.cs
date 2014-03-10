using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Raven;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Interfaces;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Logica.NoSql;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logic.NoSql.Interface;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Excepcion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.App.Nesb.Server
{
    /// <summary>
    /// Clase que se encarga de controlar la llegada de un mensaje plantilla estado de cuenta
    /// y almacenarlo en base de datos no sql.
    /// </summary>
    public class ProcesaPlantilla : IHandleMessages<PlantillaEstadoCuenta>
    {

        public IBus Bus { get; set; }

        /// <summary>
        /// Método que se encarga de insertar la información de la plantilla en el repositorio
        /// donde será consultada
        /// </summary>
        /// <param name="message"></param>
        void IHandleMessages<PlantillaEstadoCuenta>.Handle(PlantillaEstadoCuenta message)
        {
            try
            {


                using (AlmacenamientoPlantilla almacenaPlantilla = new AlmacenamientoPlantilla())
                {
                    almacenaPlantilla.AlmacenaPlantilla(message);
                }

                Console.WriteLine("mensaje procesado");
            }
            catch (ExcepcionDatosEstadoCuenta excepcion)
            {
            }
            catch (ExcepcionLogicaEstadoCuenta excepcion)
            {
            }
            catch (Exception excepcion)
            {
            }

        }
    }
}
