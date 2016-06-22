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

public partial class App_OPOBOJ_tipos: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null)
            {
                DataTable dt = new DataTable();

                string sql = "SELECT USR_OPOBTP_CODIGO as id, USR_OPOBTP_DESCRP as tipo, USR_OPOBTP_TEXTSG as texto FROM USR_OPOBTP with(nolock)  ";
                if (Request.QueryString.Get("id") != null)
                {
                    sql += " WHERE USR_OPOBTP_CODIGO='" + Request.QueryString.Get("id").ToString() + "'";
                }

                dt = Interfaz.EjecutarConsultaBD("CBS",sql);

                if (dt.Rows.Count > 0)
                {
                    List<Registros> l = new List<Registros>();
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        Registros p = new Registros();
                        p.id = dt.Rows[t]["id"].ToString();
                        p.tipo = dt.Rows[t]["tipo"].ToString();
                        p.texto = dt.Rows[t]["texto"].ToString();
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
	public string texto { get; set; }
    }

   
}
