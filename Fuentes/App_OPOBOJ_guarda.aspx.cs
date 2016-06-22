using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class App_OPOBOJ_guarda: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null)
            {

                if (Request.QueryString.Get("id") != null)
                {
                    if (Request.QueryString.Get("nombre_imagen_firma") != null)
                    {
                        Interfaz.EditarObservacion_firma(Request.QueryString.Get("id").ToString(), Request.QueryString.Get("us").ToString(), Request.QueryString.Get("nombre_imagen_firma").ToString());
                    }
                    else
                    {
                        Interfaz.EditarObservacion(Request.QueryString.Get("codobs").ToString(), Request.QueryString.Get("tipoob").ToString(), Request.QueryString.Get("codcli").ToString(), Request.QueryString.Get("observ").ToString(), Request.QueryString.Get("usuario_carga").ToString(), Request.QueryString.Get("correc").ToString());
                    }

                }
                else
                {
                    Interfaz.AltaObservacion(Request.QueryString.Get("codobs").ToString(), Convert.ToDateTime(Request.QueryString.Get("fecha")), Request.QueryString.Get("tipoob").ToString(), Request.QueryString.Get("codcli").ToString(), Request.QueryString.Get("observ").ToString(), Request.QueryString.Get("coordn").ToString(), Request.QueryString.Get("us").ToString());

                }


                List<Registros> l = new List<Registros>();

                Registros p = new Registros();
                p.success = "1";

                l.Add(p);




                string jsonString = "{'registros':";
                jsonString += JsonHelper.JsonSerializer<List<Registros>>(l);
                jsonString += "}";
                Response.Write(jsonString);
            }
        }
        catch (Exception ex)
        {

            Response.Write(JsonHelper.JsonSerializer(ex.Message));
        }
    }

    public class Registros
    {
        public string success { get; set; }
    }

   
}
