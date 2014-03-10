using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Interfaces;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Raven
{

    /// <summary>
    /// Clase encargada de tener conexión a la base de datos documental
    /// RavenDB.
    /// </summary>
    public class DaoEstadoCuenta : IDaoEstadoCuenta, IDisposable
    {

        #region Propiedades

        /// <summary>
        /// Dirección url del servidor
        /// </summary>
        public String Servidor { get; set; }

        /// <summary>
        /// Nombre de la base de datos.
        /// </summary>
        public String BaseDatos { get; set; }

        #endregion

        #region Metodos

        /// <summary>
        /// Metodo que inserta una entidad estado de cuenta en la base de datos no relacional.
        /// </summary>
        /// <param name="plantilla">Información del estado de cuenta.</param>
        /// <param name="mensaje">Mensaje con el detalle del error.</param>
        /// <returns>Verdadero o False según el resultado de la operación.</returns>
        public bool InsertarEstadoCuenta(PlantillaEstadoCuenta plantilla, out string mensaje)
        {
            mensaje = string.Empty;
            bool insercion = false;

            try
            {
                using (DocumentStore almacenamientoNsq = new DocumentStore())
                {
                    almacenamientoNsq.Url = this.Servidor;
                    almacenamientoNsq.DefaultDatabase = this.BaseDatos;
                    almacenamientoNsq.Initialize();

                    PlantillaEstadoCuenta plantillaBusqueda;

                    if (!this.BusquedaEstadoDeCuenta(plantilla.NumeroIdentificacion,
                                                   plantilla.NumeroCredito,
                                                   plantilla.FechaRegistro, out plantillaBusqueda))
                    {
                        using (var session = almacenamientoNsq.OpenSession())
                        {
                            session.Store(plantilla);
                            session.SaveChanges();
                            insercion = true;
                        }
                    }
                    else
                    {
                        StringBuilder mensajeError = new StringBuilder();
                        mensajeError.Append("Ya existe una plantilla registrada para el numero de identificación ");
                        mensajeError.Append(plantilla.NumeroIdentificacion);
                        mensajeError.Append("  el numero de crédito   ");
                        mensajeError.Append(plantilla.NumeroCredito);
                        mensajeError.Append("  y fecha  ");
                        mensajeError.Append(plantilla.FechaRegistro.ToString("yyy/MM/dd"));

                        mensaje = mensajeError.ToString();
                    }
                }
            }
            catch (Exception excepcion)
            {
                StringBuilder mensajeError = new StringBuilder();
                mensajeError.Append("Se presento un error en la clase DaoEstadoCuenta ");
                mensajeError.Append(" para acceso NoSQl en el metodo Insertar ");
                mensajeError.Append(". Detalle del error: " + excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);
            }

            return insercion;
        }

        /// <summary>
        /// Metodo que elimina la información de un estado de cuenta en base de datos.
        /// </summary>
        /// <param name="plantilla">Informacion del estado de cuenta a borrar.</param>
        public bool EliminarEstadoCuenta(PlantillaEstadoCuenta plantilla, out string mensaje)
        {
            bool eliminar = false;
            mensaje = string.Empty;

            try
            {
                PlantillaEstadoCuenta plantillaBusqueda;

                bool resultadoBusqueda = this.BusquedaEstadoDeCuenta(plantilla.NumeroIdentificacion,
                                                                     plantilla.NumeroCredito,
                                                                     plantilla.FechaRegistro,
                                                                     out plantillaBusqueda);
                if (resultadoBusqueda)
                {
                    using (DocumentStore almacenamientoNsq = new DocumentStore())
                    {
                        almacenamientoNsq.Url = this.Servidor;
                        almacenamientoNsq.DefaultDatabase = this.BaseDatos;
                        almacenamientoNsq.Initialize();


                        using (var session = almacenamientoNsq.OpenSession())
                        {
                            session.Advanced.DocumentStore.DatabaseCommands.Delete("PlantillaEstadoCuentas/"
                                                                                    + plantillaBusqueda.Id.ToString(), null);
                            session.SaveChanges();
                            eliminar = true;
                        }
                    }
                }
                else
                {
                    StringBuilder mensajeError = new StringBuilder();
                    mensajeError.Append("No existe una plantilla registrada para el numero de identificación ");
                    mensajeError.Append(plantilla.NumeroIdentificacion);
                    mensajeError.Append("  el numero de crédito   ");
                    mensajeError.Append(plantilla.NumeroCredito);
                    mensajeError.Append("  y fecha  ");
                    mensajeError.Append(plantilla.FechaRegistro.ToString("yyy/MM/dd"));

                    mensaje = mensajeError.ToString();
                }
            }
            catch (Exception excepcion)
            {
                StringBuilder mensajeError = new StringBuilder();
                mensajeError.Append("Se presento un error en la clase DaoEstadoCuenta ");
                mensajeError.Append(" para acceso NoSQl en el metodo EliminarEstadoCuenta ");
                mensajeError.Append(". Detalle del error: " + excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);
            }

            return eliminar;
        }

        /// <summary>
        /// Metodo que actualiza la información de un estado de cuenta.
        /// </summary>
        /// <param name="plantilla"></param>
        public bool ActualizarEstadoCuenta(PlantillaEstadoCuenta plantilla, out string mensaje)
        {
            mensaje = string.Empty;
            bool actualizacion = false;

            try
            {
                PlantillaEstadoCuenta plantillaBusqueda;

                bool resultadoBusqueda = this.BusquedaEstadoDeCuenta(plantilla.NumeroIdentificacion,
                                                                     plantilla.NumeroCredito,
                                                                     plantilla.FechaRegistro,
                                                                     out plantillaBusqueda);

                if (resultadoBusqueda)
                {
                    using (DocumentStore almacenamientoNsq = new DocumentStore())
                    {
                        almacenamientoNsq.Url = this.Servidor;
                        almacenamientoNsq.DefaultDatabase = this.BaseDatos;
                        almacenamientoNsq.Initialize();

                        using (var session = almacenamientoNsq.OpenSession())
                        {
                            PlantillaEstadoCuenta plantillaModificar;

                            plantillaModificar = session.Load<PlantillaEstadoCuenta>("PlantillaEstadoCuentas/"
                                                                                      + plantillaBusqueda.Id.ToString());

                            plantillaModificar.Estado = plantilla.Estado;
                            plantillaModificar.FechaRegistro = plantilla.FechaRegistro;
                            plantillaModificar.Mesaje = plantilla.Mesaje;
                            plantillaModificar.NumeroCredito = plantilla.NumeroCredito;
                            plantillaModificar.NumeroIdentificacion = plantilla.NumeroIdentificacion;
                            plantillaModificar.RutaCorreoPlantilla = plantilla.RutaCorreoPlantilla;
                            plantillaModificar.RutaPlantilla = plantilla.RutaPlantilla;

                            session.SaveChanges();

                            actualizacion = true;
                        }
                    }
                }
                else
                {
                    StringBuilder mensajeError = new StringBuilder();
                    mensajeError.Append("No existe una plantilla registrada para el numero de identificación ");
                    mensajeError.Append(plantilla.NumeroIdentificacion);
                    mensajeError.Append("  el numero de crédito   ");
                    mensajeError.Append(plantilla.NumeroCredito);
                    mensajeError.Append("  y fecha  ");
                    mensajeError.Append(plantilla.FechaRegistro.ToString("yyy/MM/dd"));

                    mensaje = mensajeError.ToString();
                }
            }
            catch (Exception excepcion)
            {
                StringBuilder mensajeError = new StringBuilder();
                mensajeError.Append("Se presento un error en la clase DaoEstadoCuenta ");
                mensajeError.Append(" para acceso NoSQl en el metodo EliminarEstadoCuenta ");
                mensajeError.Append(". Detalle del error: " + excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);
            }

            return actualizacion;
        }

        /// <summary>
        /// Metodo que obtiene un listado de objetos estado de cuenta.
        /// </summary>
        /// <param name="identificacion">Número de identificación</param>
        /// <param name="credito">Número de crédito</param>
        /// <returns>Lista de estados de cuenta</returns>
        public List<PlantillaEstadoCuenta> Consulta(decimal identificacion, decimal credito)
        {
            List<PlantillaEstadoCuenta> listaEstadoCuenta = new List<PlantillaEstadoCuenta>();

            try
            {
                using (DocumentStore almacenamientoNsq = new DocumentStore())
                {
                    almacenamientoNsq.Url = this.Servidor;
                    almacenamientoNsq.DefaultDatabase = this.BaseDatos;
                    almacenamientoNsq.Initialize();

                    using (var session = almacenamientoNsq.OpenSession())
                    {
                        listaEstadoCuenta = (from plantilla in session.Query<PlantillaEstadoCuenta>()
                                             where plantilla.NumeroIdentificacion == identificacion
                                             && plantilla.NumeroCredito == credito
                                             select plantilla).ToList();
                    }
                }
            }
            catch (Exception excepcion)
            {
                StringBuilder mensajeError = new StringBuilder();
                mensajeError.Append("Se presento un error en la clase DaoEstadoCuenta ");
                mensajeError.Append(" para acceso NoSQl en el metodo Consulta ");
                mensajeError.Append(". Detalle del error: " + excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);
            }

            return listaEstadoCuenta;
        }

        /// <summary>
        /// Método que busca un estado de cuenta en todo el repositorio
        /// </summary>
        /// <param name="identificacion">Número de identificación del cliente</param>
        /// <param name="credito">Número de credito del cliente.</param>
        /// <param name="fecha">Fecha de publicación del documento estado de cuenta.</param>
        /// <returns>Lista de documentos estados de cuenta.</returns>
        public bool BusquedaEstadoDeCuenta(decimal identificacion, decimal credito, DateTime fecha, out PlantillaEstadoCuenta resultaPlantilla)
        {
            resultaPlantilla = new PlantillaEstadoCuenta();
            bool resultado = false;

            try
            {
                using (DocumentStore almacenamientoNsq = new DocumentStore())
                {
                    almacenamientoNsq.Url = this.Servidor;
                    almacenamientoNsq.DefaultDatabase = this.BaseDatos;
                    almacenamientoNsq.Initialize();

                    using (var session = almacenamientoNsq.OpenSession())
                    {
                        int nroRegistros = (from plantilla in session.Query<PlantillaEstadoCuenta>()
                                            where plantilla.NumeroIdentificacion == identificacion
                                            && plantilla.NumeroCredito == credito
                                            && fecha.Day == plantilla.FechaRegistro.Day
                                            && fecha.Month == plantilla.FechaRegistro.Month
                                            && fecha.Year == plantilla.FechaRegistro.Year
                                            select plantilla).Count();

                        if (nroRegistros == 1)
                        {
                            resultado = true;

                            resultaPlantilla = (from plantilla in session.Query<PlantillaEstadoCuenta>()
                                                where plantilla.NumeroIdentificacion == identificacion
                                                && plantilla.NumeroCredito == credito
                                                && fecha.Day == plantilla.FechaRegistro.Day
                                                && fecha.Month == plantilla.FechaRegistro.Month
                                                && fecha.Year == plantilla.FechaRegistro.Year
                                                select plantilla).First();
                        }

                        else if (nroRegistros > 1)
                        {
                            StringBuilder mensajeError = new StringBuilder();
                            mensajeError.Append("Existe mas de una plantilla por el criterio de busqueda.");
                            throw new Exception(mensajeError.ToString());

                        }
                    }
                }
            }
            catch (Exception excepcion)
            {
                StringBuilder mensajeError = new StringBuilder();
                mensajeError.Append("Se presento un error en la clase DaoEstadoCuenta ");
                mensajeError.Append(" para acceso NoSQl en el metodo BusquedaEstadoCuenta ");
                mensajeError.Append(". Detalle del error: " + excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);
            }

            return resultado;
        }

        #endregion

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
