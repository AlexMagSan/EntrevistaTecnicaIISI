using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Entrevista_Técnica_IISI
{
    /// <summary>
    /// Summary description for viaje
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class viaje : System.Web.Services.WebService
    {
        //Se crea la instancia del adaptador y la tabla en base de datos.
        DataSet1.estacionDataTable tEstaciones = new DataSet1.estacionDataTable();
        DataSet1TableAdapters.estacionTableAdapter taestaciones = new DataSet1TableAdapters.estacionTableAdapter();

        DataSet1.horarioDataTable tHorario = new DataSet1.horarioDataTable();
        DataSet1TableAdapters.horarioTableAdapter taHorario = new DataSet1TableAdapters.horarioTableAdapter();

        DataSet1TableAdapters.QueriesTableAdapter taQueries = new DataSet1TableAdapters.QueriesTableAdapter();

        DataSet1.ProximosTrenesDataTable tProximosTrenes = new DataSet1.ProximosTrenesDataTable();
        DataSet1TableAdapters.ProximosTrenesTableAdapter taProximosTrenes = new DataSet1TableAdapters.ProximosTrenesTableAdapter();

        DataSet1.usuarioDataTable tUsuario = new DataSet1.usuarioDataTable();
        DataSet1TableAdapters.usuarioTableAdapter taUsuario = new DataSet1TableAdapters.usuarioTableAdapter();

        DataSet1.TarifasViajeDataTable tTarifasViaje = new DataSet1.TarifasViajeDataTable();
        DataSet1TableAdapters.TarifasViajeTableAdapter taTarifasViaje = new DataSet1TableAdapters.TarifasViajeTableAdapter();

        DataSet1.UltimaConfigDataTable tUltimaConfig = new DataSet1.UltimaConfigDataTable();
        DataSet1TableAdapters.UltimaConfigTableAdapter taUltimaConfig = new DataSet1TableAdapters.UltimaConfigTableAdapter();

        [WebMethod]
        public DataTable Estaciones()
        {
            //Para obtener la tabla con la informacion de estaciones para la app
            tEstaciones = taestaciones.GetData();   
            return tEstaciones;
        }

        [WebMethod]
        public DataTable Horarios(int origen, int direccion)
        {
            //Para obtener la tabla con la informacion de horarios para la app
            tHorario = taHorario.GetDataByOrigenDireccion(origen, direccion);            
            return tHorario;
        }

        [WebMethod]
        public string DuracionViaje(int origen, int direccion)
        {
            //Metodo para obtener la duracion del viaje para la app
            string Duracion = taQueries.DuracionViaje(origen, direccion).ToString();  
            return Duracion;
        }

        [WebMethod]
        public DataTable ProximosTrenes(int origen, int direccion)
        {
            //Para obtener la tabla con la informacion de los proximos trenes para la app
            tProximosTrenes = taProximosTrenes.GetData(origen, direccion);            
            return tProximosTrenes;
        }

        [WebMethod]
        public DataTable TipoUsario()
        {
            //Para obtener la tabla con la informacion de los tipos de usuario para la app
            tUsuario = taUsuario.GetData();
            return tUsuario;
        }

        [WebMethod]
        public DataTable TarifasViajes(int origen, int direccion)
        {
            //Para obtener la tabla con la informacion con las tarifas de los viajes para la app
            tTarifasViaje = taTarifasViaje.GetData(origen, direccion);
            return tTarifasViaje;
        }

        [WebMethod]
        public DataTable ConsultarUltimaConfig()
        {
            //Para obtener la tabla con la informacion con la ultima configuracoin hecha desde la app
            tUltimaConfig = taUltimaConfig.GetData();
            return tUltimaConfig;
        }

        [WebMethod]
        public void GuardarUltimaConfig(int origen, int direccion, int tipousuario)
        {
            //Metodo para guardar la ultima configuracion hecha desde la app
            if(taUltimaConfig.GetData().Rows.Count > 0)
            {
                taUltimaConfig.UpdateQuery(origen, direccion, tipousuario);
            }
            else
            {
                taUltimaConfig.Insert(origen, direccion, tipousuario);
            }
            
            tTarifasViaje = taTarifasViaje.GetData(origen, direccion);
        }

    }
}
