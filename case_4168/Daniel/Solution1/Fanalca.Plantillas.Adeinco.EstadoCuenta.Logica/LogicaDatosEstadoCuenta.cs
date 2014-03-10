using System;
using Spring.Context;
using Spring.Context.Support;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Interfaces;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Excepcion;
using System.Data;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica
{
    /// <summary>
    /// Clase encargada de llamar a las clases de repositorio o de datos para obtener la informacion de estado de cuenta
    /// a generar.
    /// </summary>
    public class LogicaDatosEstadoCuenta : IConsultasPlantillaDB, IDisposable
    {

        #region Propiedades

        /// <summary>
        ///  Atributo que contiene la instancia de la clase que sirve de acceso a  datos.
        /// </summary>
        IConsultasPlantillaDB ConsultaDatos { get; set; }
        #endregion

        #region Constructor

        public LogicaDatosEstadoCuenta()
        {
           
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Metodo que retorna la informacion de los insumos para generar las plantillas de estado de cuenta.
        /// </summary>
        /// <param name="IdConcesionario"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public System.Data.DataTable ObtenerInsumosPlantilla(decimal IdConcesionario, DateTime fecha)
        {
            DataTable insumo;
            try
            {
                IApplicationContext contexto = new XmlApplicationContext("DataConfiguration.xml");
                this.ConsultaDatos = (IConsultasPlantillaDB)contexto.GetObject("DaoConsulta");

                insumo = this.ConsultaDatos.ObtenerInsumosPlantilla(IdConcesionario, fecha);

                
            }
            catch (ExcepcionDatosEstadoCuenta excepcion)
            {
                throw excepcion;
            }
            catch (Exception excepcion)
            {
                string mensajeError = "Error presentado en la clase LogicaDatosEstadoCuenta en el método ObtenerInsumosPlantilla" +
                                      ". Detalle del error: " + excepcion.Message;

                throw new ExcepcionLogicaEstadoCuenta(mensajeError, excepcion);
            }

            return insumo;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
