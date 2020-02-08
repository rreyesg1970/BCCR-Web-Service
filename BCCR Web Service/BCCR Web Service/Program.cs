/* Programa hecho según video de Youtube de Elias Salazar del año 2014.
* Modificado por mí, Roberto Reyes García el 28/01/2020
* con Visual Studio 19, .Net Framework 4.7.2
* Permite consumir el webservice de indicadores económicos de Banco Central de Costa Rica, 
* utilizando tecnología .NET
* Se añade variable para obtener la fecha actual y así el tipo de cambio del día y se guarda 
* el resultado del precio de venta y de compra cada uno en su respectivo archivo de texto en 
* el directorio actual donde se ejecuta la aplicación.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace BCCR_Web_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            string ruta = Directory.GetCurrentDirectory();
            string fechaConsultar = DateTime.Now.ToShortDateString();
            string usuario = "su usuario";
            string correo = "su correo";
            string token = "su token";

            //  (https://gee.bccr.fi.cr/Indicadores/Suscripciones/WS/wsindicadoreseconomicos.asmx);

            cr.fi.bccr.gee.wsindicadoreseconomicos cliente = new cr.fi.bccr.gee.wsindicadoreseconomicos();
            DataSet dsVenta = cliente.ObtenerIndicadoresEconomicos("318", fechaConsultar, fechaConsultar, usuario, "N", correo, token);
            DataSet dsCompra = cliente.ObtenerIndicadoresEconomicos("317", fechaConsultar, fechaConsultar, usuario, "N", correo, token);

            Console.WriteLine("Fecha Consulta: " + dsVenta.Tables[0].Rows[0].ItemArray[1].ToString());
            Console.WriteLine();
            Console.WriteLine("Codigo Indicador: " + dsVenta.Tables[0].Rows[0].ItemArray[0].ToString());
            Console.WriteLine("Tipo Cambio Venta: " + dsVenta.Tables[0].Rows[0].ItemArray[2].ToString());
            Console.WriteLine();
            Console.WriteLine("Codigo Indicador: " + dsCompra.Tables[0].Rows[0].ItemArray[0].ToString());
            Console.WriteLine("Tipo Cambio Compra: " + dsCompra.Tables[0].Rows[0].ItemArray[2].ToString());
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Presione cualquier tecla para finalizar");
            Console.ReadLine();

            #region EscrituraArchivos
            // Precio Venta
            string precioVenta = dsVenta.Tables[0].Rows[0].ItemArray[2].ToString();
            string archivoFinalVenta = ruta + "\\" + "tipoCambioVenta.txt";

            StreamWriter flujoSalidaVenta = File.CreateText(archivoFinalVenta);
            flujoSalidaVenta.Write(precioVenta);

            flujoSalidaVenta.Close();

            // Precio Compra
            string precioCompra = dsCompra.Tables[0].Rows[0].ItemArray[2].ToString();
            string archivoFinalCompra = ruta + "\\" + "tipoCambioCompra.txt";
            StreamWriter flujoSalidaCompra = File.CreateText(archivoFinalCompra);
            flujoSalidaCompra.Write(precioCompra);

            flujoSalidaCompra.Close();
            #endregion EscrituraArchivos
        }
    }

}
