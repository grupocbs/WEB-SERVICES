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

public partial class App_OPOBOJ_clientes : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null)
            {

                DataTable dt1 = new DataTable();
                string sql1 = "SELECT VTMCLH_NROCTA as id, VTMCLH_NOMBRE as cliente FROM VTMCLH with(nolock)  ";
               
                if (Request.QueryString.Get("id") != null)
                {
                    sql1 += " WHERE  VTMCLH_NROCTA='" + Request.QueryString.Get("id").ToString() + "'";
                }
                else
                {
                    DataTable dt = new DataTable();

                    string sql = "SELECT cliente FROM USUARIO_CLIENTES with(nolock) WHERE  USUARIO ='" + Request.QueryString.Get("us").ToString() + "'";
                    dt = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql);

                    string clientes = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        clientes += "'" + dt.Rows[i]["cliente"].ToString() + "',";
                    }

                    clientes = clientes.Substring(0, clientes.Length - 1);

                    
                    sql1 += " WHERE VTMCLH_NROCTA in (" + clientes + ")";
                }

                sql1 += " ORDER BY VTMCLH_NOMBRE";

                dt1 = Interfaz.EjecutarConsultaBD("CBS",sql1);

                if (dt1.Rows.Count > 0)
                {
                    List<Registros> l = new List<Registros>();
                    for (int t = 0; t < dt1.Rows.Count; t++)
                    {
                        Registros p = new Registros();
                        p.id = dt1.Rows[t]["id"].ToString();
                        p.cliente = dt1.Rows[t]["cliente"].ToString();
                      
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
        public string cliente { get; set; }
        
    }

   
}
