using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logic.NoSql.Interface;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Excepcion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Raven;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Interfaces;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Logica.NoSql
{
    public class AlmacenamientoPlantilla:IAlmacenaPlantillaNoSql,IDisposable
    {
        #region Propiedades

        public String BaseDatos { get; set; }
        public String Servidor { get; set; }

        #endregion

        public void AlmacenaPlantilla(Entidades.PlantillaEstadoCuenta plantilla)
        {
            string mensajeError = string.Empty;
            bool resultado = false;

            try
            {
                using (DaoEstadoCuenta daoEstadoCuenta = new DaoEstadoCuenta())
                {
                    daoEstadoCuenta.BaseDatos = "InformacionEstadoCuenta";
                    daoEstadoCuenta.Servidor = "http://Localhost:9090"; 

                    resultado = daoEstadoCuenta.InsertarEstadoCuenta(plantilla, out mensajeError);
                }
            }
            catch (ExcepcionDatosEstadoCuenta excepcion)
            {
                throw excepcion;
            }
            catch (Exception excepcion)
            {
                StringBuilder mensaje = new StringBuilder();
                mensaje.Append("Se presentó un error no controlado en la clase AlmacenaPlantilla ");
                mensaje.Append(" en el mètodo AlmacenaPlantilla(). Detalle del error: ");
                mensaje.Append(excepcion.Message);

                throw new ExcepcionLogicaEstadoCuenta(mensajeError.ToString(), excepcion);
            }
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
