namespace Fanalca.Plantillas.Adeinco.EstadoCuenta.Aplicacion
{
    using System;
    using System.Text;
    using System.Data;
    using CrystalDecisions.Shared;
    using Amib.Threading;
    using System.IO;
    using Entidades;
    using Logica;

    public delegate void PublicacionEstadoCuenta(object sender, EventoGeneraPlantilla<PlantillaEstadoCuenta> e);

    public class GenerarPlantillas
    {

        #region Propiedades

        /// <summary>
        /// Fecha en que se generan las plantillas.
        /// </summary>
        public DateTime FechaCorte { get; set; }

        /// <summary>
        /// Id del concesionario al cual se generaran las plantillas.
        /// </summary>
        public int IdConcesionario { get; set; }

        /// <summary>
        /// Ruta donde se generan las plantillas
        /// </summary>
        public String RutaGeneracion { get; set; }

        /// <summary>
        /// Contenido de la plantilla Mail
        /// </summary>
        private StringBuilder ContenidoMail { get; set; }

        /// <summary>
        /// Trhead Pool para usar hilos en la generación de las plantillas.
        /// </summary>
        private SmartThreadPool PoolGeneraPlantilla { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="fechaCorte"></param>
        /// <param name="idConcesionario"></param>
        public GenerarPlantillas(DateTime fechaCorte, int idConcesionario)
        {
            FechaCorte = fechaCorte;
            IdConcesionario = idConcesionario;


            const int cantidadHilos = 100; //int.Parse(System.Configuration.ConfigurationSettings.AppSettings["NumeroPlantillas"]);

            PoolGeneraPlantilla = new SmartThreadPool(
                10 * 1000,    // Idle timeout in milliseconds
                cantidadHilos,         // Threads upper limit
                1);          // Threads lower limit);}

            using (var plantillaHtml = new StreamReader("Plantilla.mht"))
            {
                ContenidoMail = new StringBuilder(plantillaHtml.ReadToEnd());
            }
        }


        #endregion

        #region Metodos

        private void AddQueue(object state)
        {
            IWorkItemResult wir = this.PoolGeneraPlantilla.QueueWorkItem(new WorkItemCallback(this.GenerarPlantilla), state);
            object result = wir.Result;
        }

        public void ObtenerInsumos()
        {
            DataTable insumos;

            using (var logica = new LogicaDatosEstadoCuenta())
            {
                insumos = logica.ObtenerInsumosPlantilla(this.IdConcesionario, this.FechaCorte);
            }

            if (insumos != null && insumos.Rows.Count > 0)
            {

                foreach (DataRow filaPlantilla in insumos.Rows)
                {

                    this.AddQueue(filaPlantilla);
                } 
            }
        }

        private object GenerarPlantilla(object state)
        {

            // declaracion de variables para el proceso
            bool objetoValido = false;
            PlantillaEstadoCuenta datosPlantilla = new PlantillaEstadoCuenta();

            try
            {
                DataRow filaPlantilla = (DataRow)state;
                objetoValido = true;

                if (objetoValido)
                {
                    string nombreArchivo = string.Empty;

                    PlantillaEstadoCuenta informacionPlantilla = new PlantillaEstadoCuenta();

                    informacionPlantilla.FechaRegistro = System.DateTime.Now;
                    informacionPlantilla.NumeroCredito = Decimal.Parse(filaPlantilla["CRED_CONSECUTIVO"].ToString());
                    informacionPlantilla.NumeroIdentificacion = Decimal.Parse(filaPlantilla["CEDULA"].ToString());

                    nombreArchivo = this.RutaGeneracion + informacionPlantilla.NumeroIdentificacion + "_"
                                    + informacionPlantilla.NumeroCredito + "_" + this.FechaCorte.Year.ToString()
                                    + this.FechaCorte.Month.ToString() + informacionPlantilla.NumeroCredito
                                    + this.FechaCorte.Day.ToString();

                    informacionPlantilla.RutaPlantilla = nombreArchivo + ".pdf";


                    DataTable tablaPlantilla = filaPlantilla.Table.Clone();

                    tablaPlantilla.ImportRow(filaPlantilla);

                    Fanalca.Mail.Plantillas.Pdf.Adeinco.EstadoCuentaColor plantillaEstadoCuenta = new Fanalca.Mail.Plantillas.Pdf.Adeinco.EstadoCuentaColor();
                    plantillaEstadoCuenta.SetDataSource(tablaPlantilla);

                    plantillaEstadoCuenta.ExportToDisk(ExportFormatType.PortableDocFormat, informacionPlantilla.RutaPlantilla);

                    // creo el contenido del mensaje mail


                    StringBuilder cadena = new StringBuilder(this.ContenidoMail.ToString());

                    cadena.Replace("@@@usuario", filaPlantilla["PERS_NOMBRE"].ToString());
                    cadena.Replace("@@@numero", filaPlantilla["CRED_CONSECUTIVO"].ToString());
                    cadena.Replace("@@@Fecha", filaPlantilla["FECHAVENCIMIENTO"].ToString());

                    using (StreamWriter archivoDisco = new StreamWriter(informacionPlantilla.RutaPlantilla = nombreArchivo + ".mht"))
                    {
                        archivoDisco.Write(cadena.ToString());
                    }


                    informacionPlantilla.Estado = true;
                    informacionPlantilla.Mesaje = "Plantilla generada de manera correcta.";

                    if (PublicarEstadoCuenta != null)
                    {
                        PublicarEstadoCuenta(this,new EventoGeneraPlantilla<PlantillaEstadoCuenta>(informacionPlantilla));
                    }

                    //Bus.Publish<PlantillaEstadoCuenta>(informacionPlantilla);
                }
            }
            catch (Exception ex)
            {
                string mensajeError = "Se presentó un error al momento de hacer la conversión del objeto a un objeto de tipo Auditoria. Detalle del error : " + ex.Message;
            }

            return 0;
        }
        
        #endregion

        public event PublicacionEstadoCuenta PublicarEstadoCuenta;
    }
}
