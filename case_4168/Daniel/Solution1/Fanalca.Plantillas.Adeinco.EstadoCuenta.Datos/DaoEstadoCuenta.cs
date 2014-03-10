using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos.Excepcion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.InterfazDatos;


namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos
{

    /// <summary>
    /// Clase que contiene los metodos de acceso  a datos para guardar los datos de públicación de una plantilla.
    /// </summary>
    class DaoEstadoCuenta : IDaoInsertar, IDisposable
    {
        #region Propiedades

        /// <summary>
        /// Nombre de la base de datos.
        /// </summary>
        public String CadenaConexion { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public DaoEstadoCuenta()
        {
            try
            {
                this.CadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["BaseDatosOracle"].ToString();

            }
            catch (Exception excepcion)
            {

            }
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene un listado de plantillas
        /// </summary>
        /// <param name="numeroIdentificacion">número de identificación del cliente</param>
        /// <returns>Listado de estado de cuentas.</returns>
        List<PlantillaEstadoCuenta> Consulta(decimal numeroIdentificacion)
        {
            List<PlantillaEstadoCuenta> ListaEstadoCuenta = new List<PlantillaEstadoCuenta>();

            try
            {
                StringBuilder consulta = new StringBuilder();
                consulta.Append(" SELECT EC.RUTA_PLANTILLA, EC.FECHA_REGISTRO, EC.NUMERO_IDENTIFICACION,EC.NUMERO_CREDITO, EC.MENSAJE  ");
                consulta.Append("  FROM ESTADOCUENTA  ");
                consulta.Append("  WHERE EC.NUMERO_IDENTIFICACION = :NumeroIdentificacion ");
                consulta.Append("  ORDER BY  EC.FECHA_REGISTRO DESC ");

                using (SqlConnection conexion = new SqlConnection(this.CadenaConexion))
                {
                    using (SqlCommand comando = new SqlCommand(consulta.ToString(), conexion))
                    {
                        SqlParameter parametro = new SqlParameter();
                        parametro.ParameterName = ":NumeroIdentificacion";
                        parametro.SqlDbType = System.Data.SqlDbType.Decimal;
                        parametro.Value = numeroIdentificacion;

                        comando.Parameters.Add(parametro);

                        conexion.Open();

                        SqlDataReader resultadoConsulta = comando.ExecuteReader();

                        ListaEstadoCuenta = this.MapeoDataReaderPlantillaEstadoCuenta(resultadoConsulta);

                        conexion.Close();
                    }
                }
            }
            catch (SqlException excepcion)
            { 
                string mensajeError = "Error presentado en el motor de base de datos al momento de consultar los registros "
                                       + " para el cliente identificado con número de identificación " + numeroIdentificacion +
                                       ". Detalle del error: " + excepcion.Message;

                throw new ExcepcionDatosEstadoCuenta(mensajeError, excepcion);
            }
            catch (Exception excepcion)
            {
                string mensajeError = "Error presentado  al momento de consultar los registros "
                                       + " para el cliente identificado con número de identificación " + numeroIdentificacion +
                                       ". Detalle del error: " + excepcion.Message;

                throw new ExcepcionDatosEstadoCuenta(mensajeError, excepcion);
            }

            return ListaEstadoCuenta;
        }


        /// <summary>
        /// Método que convierte el objeto OracleDataReader en una lista de Objetos
        /// </summary>
        /// <param name="resultado"></param>
        /// <returns></returns>
        private List<PlantillaEstadoCuenta> MapeoDataReaderPlantillaEstadoCuenta(SqlDataReader resultado)
        {
            int numeroFila = 1;
            List<PlantillaEstadoCuenta> listaEstadoCuenta = new List<PlantillaEstadoCuenta>();
            
            try
            {
                while (resultado.Read())
                {
                    PlantillaEstadoCuenta plantilla = new PlantillaEstadoCuenta();

                    plantilla.RutaPlantilla = resultado.GetValue(0).ToString();
                    plantilla.FechaRegistro = DateTime.Parse(resultado.GetValue(1).ToString());
                    plantilla.NumeroIdentificacion = decimal.Parse(resultado.GetValue(2).ToString());
                    plantilla.NumeroCredito = decimal.Parse(resultado.GetValue(3).ToString());
                    plantilla.Mesaje = resultado.GetValue(4).ToString();

                    listaEstadoCuenta.Add(plantilla);

                    numeroFila = numeroFila + 1;
 
                }
            }
            catch (Exception excepcion)
            { 
                string mensajeError = "Error al momento de convertir registro de la consulta a objeto plantilla. en el registro " + numeroFila
                                      + "Descripción del error: " + excepcion.Message;

                throw new Exception(mensajeError, excepcion);
            }

            return listaEstadoCuenta;

        }

        /// <summary>
        /// Metodo encargado de insertar la información de una plantilla de estado cuenta
        /// </summary>
        /// <param name="estadoCuenta"></param>
        public void Insertar(PlantillaEstadoCuenta estadoCuenta)
        {
            try
            {
                StringBuilder comandoInsert = new StringBuilder();

                comandoInsert.Append(" INSERT INTO ESTADOCUENTA  ");
                comandoInsert.Append("(RUTA_PLANTILLA, FECHA_REGISTRO,NUMERO_IDENTIFICACION, NUMERO_CREDITO, MENSAJE, RUTACORREO, FORMATO) ");
                comandoInsert.Append(" VALUES  ");
                comandoInsert.Append(" (:RUTA_PLANTILLA, trunc(:FECHA_REGISTRO), :NUMERO_IDENTIFICACION, :NUMERO_CREDITO, :MENSAJE,:RUTACORREO, :FORMATO) ");

                using (SqlConnection conexion = new SqlConnection(this.CadenaConexion))
                {
                    using (SqlCommand comando = new SqlCommand(comandoInsert.ToString(), conexion))
                    {
                        SqlParameter parametro = new SqlParameter();
                        parametro.SqlDbType =  System.Data.SqlDbType.NVarChar;
                        parametro.ParameterName = ":RUTA_PLANTILLA";
                        parametro.Value = estadoCuenta.RutaPlantilla;
                        comando.Parameters.Add(parametro);

                        parametro = new SqlParameter();
                        parametro.SqlDbType = System.Data.SqlDbType.Date;
                        parametro.ParameterName = ":FECHA_REGISTRO";
                        parametro.Value = estadoCuenta.FechaRegistro;
                        comando.Parameters.Add(parametro);

                        parametro = new SqlParameter();
                        parametro.SqlDbType = System.Data.SqlDbType.Decimal;
                        parametro.ParameterName = ":NUMERO_IDENTIFICACION";
                        parametro.Value = estadoCuenta.NumeroIdentificacion;
                        comando.Parameters.Add(parametro);

                        parametro = new SqlParameter();
                        parametro.SqlDbType = System.Data.SqlDbType.Decimal;
                        parametro.ParameterName = ":NUMERO_CREDITO";
                        parametro.Value = estadoCuenta.NumeroCredito;
                        comando.Parameters.Add(parametro);

                        parametro = new SqlParameter();
                        parametro.SqlDbType = System.Data.SqlDbType.NVarChar;
                        parametro.ParameterName = ":MENSAJE";
                        parametro.Value = estadoCuenta.Mesaje;
                        comando.Parameters.Add(parametro);

                        parametro = new SqlParameter();
                        parametro.SqlDbType = System.Data.SqlDbType.NVarChar;
                        parametro.ParameterName = ":RUTACORREO";
                        parametro.Value = estadoCuenta.Mesaje;
                        comando.Parameters.Add(parametro);

                        parametro = new SqlParameter();
                        parametro.SqlDbType = System.Data.SqlDbType.NVarChar;
                        parametro.ParameterName = ":FORMATO";
                        parametro.Value = estadoCuenta.Mesaje;
                        comando.Parameters.Add(parametro);

                        conexion.Open();

                        comando.ExecuteNonQuery();

                        conexion.Close();
                    }
                }
            }
            catch (SqlException excepcion)
            {
                StringBuilder mensajeError = this.ConstruyeMensajeError(estadoCuenta);

                mensajeError.Append(excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);
                
            }
            catch (Exception excepcion)
            {
                StringBuilder mensajeError = this.ConstruyeMensajeError(estadoCuenta);

                mensajeError.Append(excepcion.Message);

                throw new ExcepcionDatosEstadoCuenta(mensajeError.ToString(), excepcion);

            }

        }


        /// <summary>
        /// Construye un mensaje de error para detallar apropiadamente el mensaje de error
        /// </summary>
        /// <param name="plantillaEstadoCuenta"></param>
        /// <returns></returns>
        private StringBuilder ConstruyeMensajeError(PlantillaEstadoCuenta plantillaEstadoCuenta)
        { 
            StringBuilder mensajeError = new StringBuilder();
            mensajeError.Append("Error presentado en la clase DaoEstadoCuenta al momento de insertar un registro en base de datos. ");
            mensajeError.Append("el registro Posee los siguientes Datos. Ruta: " + plantillaEstadoCuenta.RutaPlantilla  + ", ");
            mensajeError.Append("número de identificación " + plantillaEstadoCuenta.NumeroIdentificacion + ", número credito" );
            mensajeError.Append(plantillaEstadoCuenta.NumeroCredito +  ", fecha  " + plantillaEstadoCuenta.FechaRegistro.ToString("YYYY/MM/DD"));
            mensajeError.Append(". Detalle del error: " );

            return mensajeError;
        }
        #endregion

        /// <summary>
        /// Realiza el dispose del objeto.
        /// </summary>
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
