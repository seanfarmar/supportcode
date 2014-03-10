using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Datos;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Aplicacion;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Interfaces;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.NoSQL.Raven;
using Fanalca.Plantillas.Adeinco.EstadoCuenta.Entidades;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            DaoEstadoCuenta daoEstadoCuenta = new DaoEstadoCuenta();

            daoEstadoCuenta.Servidor = "http://Localhost:9090";
            daoEstadoCuenta.BaseDatos = "Prueba";



            PlantillaEstadoCuenta plantilla = new PlantillaEstadoCuenta();

            plantilla.Estado = false;
            plantilla.FechaRegistro = System.DateTime.Now;
            plantilla.Mesaje = "Plantilla Demo 3333";
            plantilla.NumeroCredito = 11112;
            plantilla.NumeroIdentificacion = 11111112;
            plantilla.RutaCorreoPlantilla ="w:\\";
            plantilla.RutaPlantilla = "d:\\";


            
            string mensaje;

            daoEstadoCuenta.InsertarEstadoCuenta(plantilla,out mensaje);

            List<PlantillaEstadoCuenta> lista = daoEstadoCuenta.Consulta(11111112, 11112);


           // bool resultado = daoEstadoCuenta.EliminarEstadoCuenta(plantilla,out mensaje );
            PlantillaEstadoCuenta plbus;

             bool encontrado =  daoEstadoCuenta.BusquedaEstadoDeCuenta(11111111, 11111, System.DateTime.Now,out  plbus);

            EstadoCuentaDatos datos = new EstadoCuentaDatos();
            DateTime fecha = new DateTime(2014,2,5);
            datos.ObtenerInsumosPlantilla(33, fecha);

            GenerarPlantillas generarPlantilla = new GenerarPlantillas(fecha,33);

            generarPlantilla.PublicarEstadoCuenta += delegate
            {
                Console.WriteLine("PlantillaGenerada..............");
            };

            generarPlantilla.ObtenerInsumos();

            Console.ReadLine();

        }

      
    }
}
