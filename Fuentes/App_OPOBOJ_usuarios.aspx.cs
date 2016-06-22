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

public partial class App_OPOBOJ_usuarios: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null)
            {
                DataTable dt = new DataTable();

                string sql = "SELECT [IDUSUARIO] as id, lower([USUARIO]) as tipo FROM [USUARIOS] with(nolock) WHERE [SUPERVISOR]<> ' '";
                if (Request.QueryString.Get("id") != null)
                {
                    sql += " and [USUARIO]='" + Request.QueryString.Get("us").ToString() + "'";
                }
		sql += " ORDER BY [USUARIO] ";
                dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                if (dt.Rows.Count > 0)
                {
                    List<Registros> l = new List<Registros>();
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        Registros p = new Registros();
                        p.id = dt.Rows[t]["id"].ToString();
                        p.tipo = dt.Rows[t]["tipo"].ToString();
                      
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
        public string id { get; set; }
        public string tipo { get; set; }
    }

   
}
