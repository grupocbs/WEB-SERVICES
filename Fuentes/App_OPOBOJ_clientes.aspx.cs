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
                string sql1 = "select USR_CLIOBJ_CODOBJ as id, o.USR_CLIOBJ_OBJDSC as cliente";
                sql1 += " from CBS.dbo.USR_CLIOBJ o with(nolock)";
                
                


                if (Request.QueryString.Get("id") != null)
                {
                    sql1 += " where USR_CLIOBJ_CODOBJ='" + Request.QueryString.Get("id").ToString() + "'";
                }
                else
                {
                    
                    sql1 += " INNER join USUARIO_OBJETIVO uc with(nolock)";
                    sql1 += " on cast(uc.OBJETIVO as varchar(6)) COLLATE DATABASE_DEFAULT=cast(o.USR_CLIOBJ_CODOBJ  as varchar(6))";
                    sql1 += " where USUARIO='" + Request.QueryString.Get("us").ToString() + "'";
                    sql1 += " order by o.USR_CLIOBJ_OBJDSC";
                }
                dt1 = Interfaz.EjecutarConsultaBD("LocalSqlServer", sql1);

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
