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

public partial class App_Login : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null && Request.QueryString.Get("pass") != null && Request.QueryString.Get("ip") != null && Request.QueryString.Get("puerto") != null && Request.QueryString.Get("imei") != null)
            {


                string sql = " SELECT * FROM usuarios WHERE supervisor <> ' ' and usuario='" + Request.QueryString.Get("us").ToString() + "' and contraseña='" + Request.QueryString.Get("pass").ToString() + "'";
		sql += " and imei='"+ Request.QueryString.Get("imei").ToString() +"'";


                DataTable dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

              
                string jsonString = "";

                Logueo p = new Logueo();
                if (dt.Rows.Count > 0)
                {
                   

                    p.us = Request.QueryString.Get("us").ToString();
                    p.pass = Request.QueryString.Get("pass").ToString();
                    p.ip = Request.QueryString.Get("ip").ToString();
                    p.puerto = Request.QueryString.Get("puerto").ToString();
		    p.mail = dt.Rows[0]["mail"].ToString();
		 p.supervisor  = dt.Rows[0]["supervisor"].ToString();


                    jsonString = JsonHelper.JsonSerializer<Logueo>(p);
                    Response.Write(jsonString);
                }
		else
 		{
			 Response.Write("{'error':'1'}");
		}
              
            }
        }
        catch (Exception ex)
        {
            
            Response.Write(JsonHelper.JsonSerializer(ex.Message));
        }
    }

    public class Logueo
    {
        public string us { get; set; }
        public string pass { get; set; }
        public string ip { get; set; }
        public string puerto { get; set; }
	public string mail { get; set; }
	public string supervisor { get; set; }
    }

   
}
