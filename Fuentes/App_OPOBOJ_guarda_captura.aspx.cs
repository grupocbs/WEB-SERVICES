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

public partial class App_OPOBOJ_guarda_captura: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            if (Request.QueryString.Get("us") != null && Request.QueryString.Get("codobs") != null && Request.QueryString.Get("nombre_imagen") != null)
            {


                Interfaz.AltaCaptura_Observacion(Request.QueryString.Get("codobs").ToString(), Request.QueryString.Get("nombre_imagen").ToString(), Request.QueryString.Get("us").ToString());



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
