using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Interfaces;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;

namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos
{
    /// <summary>
    /// Clase que extrae la información de acceso a Datos para la generación de plantillas de estado de cuenta.
    /// </summary>
    public class EstadoCuentaDatos:IConsultasPlantillaDB, IDisposable
    {
        #region Propiedades

        /// <summary>
        /// Nombre de la base de datos.
        /// </summary>
        public String CadenaConexion { get;private set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public EstadoCuentaDatos()
        {
            try
            {
                this.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["BaseDatosSQL"].ToString();
               
            }
            catch (Exception excepcion)
            {
                string mensajeError = "Error en el constructor de la clase EstadoCuentaDatos. Detalle del error: " + excepcion.Message;

                throw new ExcepcionDatosEstadoCuenta(mensajeError, excepcion);
            }


        }


        #endregion
        
        #region Metodos
    
        /// <summary>
        /// Obtiene los insumos para generar los reportes
        /// </summary>
        /// <param name="IdConcesionario">Identificador del concesionario</param>
        /// <param name="fecha">Fecha</param>
        /// <returns>Insumos para generar la plantilla</returns>
        public DataTable ObtenerInsumosPlantilla(decimal IdConcesionario, DateTime fecha)
        {


            DataTable insumo = new DataTable("Recaudos");

            try
            {
                StringBuilder consulta = new StringBuilder();
                consulta.Append(" select concesionarioc,concesionario,pers_nombre ");
                consulta.Append("   ,cedula,vemo_placas,cred_consecutivo,cuotas ");
                consulta.Append("   ,tasavigente,tasademora,saldocapvencido ");
                consulta.Append("   ,saldointvencido,saldosegurosvencido ");
                consulta.Append("   ,interesmoravencido,gastosprocesales,tipc_codigo, ");
                consulta.Append("   conc_pcntprejuridico,conc_pcntdemanda,conc_pcntjuridico ");
                consulta.Append("   ,saldofechageneracion,fechavencimiento,capitalxvencer ");
                consulta.Append("  ,interesxvencer,cuotasanticipadas,pers_email ");
                consulta.Append("  from estadocuenta.dbo.datos ");
                consulta.Append("   where fechavencimiento = @fecha ");
                consulta.Append("  and concesionarioc = @conce_conce ");
                
                using (SqlConnection conexion = new SqlConnection(this.CadenaConexion))
                {
                    using (SqlDataAdapter comando = new SqlDataAdapter(consulta.ToString(), conexion))
                    {
                        SqlParameter parametro = new SqlParameter();
                        parametro.ParameterName = "@conce_conce";
                        parametro.DbType = DbType.Decimal;
                        parametro.Value = IdConcesionario;
                        comando.SelectCommand.Parameters.Add(parametro);
                        parametro = new SqlParameter();
                        parametro.ParameterName = "@fecha";
                        parametro.DbType = DbType.Date;
                        parametro.Value = fecha;

                        comando.SelectCommand.Parameters.Add(parametro);

                        conexion.Open();

                        comando.Fill(insumo);
                    }
                }

            }
            catch (SqlException excepcion)
            {
                string mensajeError = "Se presentó un error en la clase EstadoCuentaDatos en el método ObtenerInsumosPlantilla " +
                                      "para el concesionario " + IdConcesionario + " en la fecha " + fecha.ToString() + "Detalle del error: " +excepcion.Message;

                throw new ExcepcionDatosEstadoCuenta(mensajeError, excepcion);

            }
            catch (Exception excepcion)
            {
                string mensajeError = "Se presentó un error en la clase EstadoCuentaDatos en el método ObtenerInsumosPlantilla " +
                                      "para el concesionario " + IdConcesionario + " en la fecha " + fecha.ToString() + "Detalle del error: " + excepcion.Message;


                throw new ExcepcionDatosEstadoCuenta(mensajeError, excepcion);

            }

            return insumo;
        }

        /// <summary>
        /// Realiza el dispose del objeto.
        /// </summary>
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
