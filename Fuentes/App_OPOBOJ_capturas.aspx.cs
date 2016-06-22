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

public partial class App_OPOBOJ_capturas: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null)
            {
                DataTable dt = new DataTable();

                string sql = "SELECT replace(USR_OPOBCA_URLOBS,'192.168.1.141','127.0.0.1') as url, USR_OPOBCA_CPTITL as titulo FROM USR_OPOBCA with(nolock)  ";
                if (Request.QueryString.Get("codobs") != null)
                {
                    sql += " WHERE USR_OPOBCA_CODOBS='" + Request.QueryString.Get("codobs").ToString() + "'";
                    sql += " AND USR_OPOBCA_USERNM='" + Request.QueryString.Get("us").ToString() + "'";
                }

                dt = Interfaz.EjecutarConsultaBD("CBS",sql);

                if (dt.Rows.Count > 0)
                {
                    List<Registros> l = new List<Registros>();
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        Registros p = new Registros();
                        p.url = dt.Rows[t]["url"].ToString();
                        p.titulo = dt.Rows[t]["titulo"].ToString();
                      
                        l.Add(p);
                    }




                    string jsonString = "{'registros':";
                    jsonString += JsonHelper.JsonSerializer<List<Registros>>(l);
                    jsonString += "}";
                    Response.Write(jsonString);

                }
		else
			{
				
                                Response.Write("{'registros':[]}");
			}
            }
        }
        catch (Exception ex)
        {

            Response.Write(JsonHelper.JsonSerializer(ex.Message));
        }
    }

    public class Registros
    {
        public string url { get; set; }
        public string titulo { get; set; }
        
    }

   
}
