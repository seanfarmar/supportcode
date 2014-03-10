using System;
using System.Text;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.InterfazDatos;
using Spring.Context;
using Spring.Context.Support;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica.Excepcion;


namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Logica
{
    /// <summary>
    /// Clase que se encarga de hacer las validaciones de datos
    /// </summary>
    public class LogicaEstadoCuenta : IDisposable
    {
        #region Propiedades

        /// <summary>
        /// Información de la plantilla
        /// </summary>
        public PlantillaEstadoCuenta PlantillaEstadoCuenta { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="plantilla"></param>
        public LogicaEstadoCuenta(PlantillaEstadoCuenta plantilla)
        {
            this.PlantillaEstadoCuenta = plantilla;
        }

        #endregion

        #region Metodos


        /// <summary>
        ///  metodo que valida la informacion de la plantilla antes de enviarlo a la clase Dao
        /// </summary>
        public void Insertar(PlantillaEstadoCuenta plantilla)
        {

            if (this.PlantillaEstadoCuenta.FechaRegistro == DateTime.MinValue || this.PlantillaEstadoCuenta.FechaRegistro == DateTime.MaxValue
               || this.PlantillaEstadoCuenta.Estado == false || this.PlantillaEstadoCuenta.NumeroCredito == 0 || this.PlantillaEstadoCuenta == null
               || this.PlantillaEstadoCuenta.NumeroIdentificacion == 0 || this.PlantillaEstadoCuenta.NumeroIdentificacion == null
               || this.PlantillaEstadoCuenta.RutaPlantilla == string.Empty || this.PlantillaEstadoCuenta.RutaPlantilla == null
               || this.PlantillaEstadoCuenta.Mesaje == string.Empty || this.PlantillaEstadoCuenta.Mesaje == null)
            {
                throw new Exception("Todos los valores de la la plantilla estado de cuenta deben tener un valor valido.");
            }
            else
            {
                try
                {
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    // aca se realiza el proceso de insercion de los datos

                    IDaoInsertar DaoBaseDatos = ctx.GetObject("MotorDB") as IDaoInsertar;

                    DaoBaseDatos.Insertar(plantilla);
                }
                catch (ExcepcionDatosEstadoCuenta excepcion)
                {
                    throw excepcion;
                }
                catch (Exception exepcion)
                {
                    StringBuilder mensajeError = new StringBuilder();
                    mensajeError.Append("Se presentó un error no controlado en la clase LogicaEstadoCuenta, en el método Insertar().");
                    mensajeError.Append(" Detalle del error " + exepcion.Message);

                    throw new ExcepcionLogicaEstadoCuenta(mensajeError.ToString(), exepcion);
                }

            }
        }

        /// <summary>
        /// Metodo que ejecuta el dispose de la clase.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
