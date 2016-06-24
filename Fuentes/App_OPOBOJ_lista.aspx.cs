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

public partial class App_OPOBOJ_lista: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("ultimoid") != null && Request.QueryString.Get("us") != null)
            {
                DataTable dt = new DataTable();

                string sql = " select right(('000000'+ convert(varchar,ISNULL(max(USR_OPOBOJ_CODOBS),0)+1)),6)  as ultimo  from [USR_OPOBOJ] with(nolock)";
                sql += " WHERE USR_OPOBOJ_USERNM='" + Request.QueryString.Get("us").ToString() + "'";

                dt = Interfaz.EjecutarConsultaBD("CBS", sql);

                List<UltimoID> l1 = new List<UltimoID>();
                if (dt.Rows.Count > 0)
                {

                    UltimoID p1 = new UltimoID();
                    p1.Ultimo = dt.Rows[0]["ultimo"].ToString();
                    l1.Add(p1);

                }
                string jsonString = "{'registros':";
                jsonString += JsonHelper.JsonSerializer<List<UltimoID>>(l1);
                jsonString += "}";
                Response.Write(jsonString);
            }
            else
            {
                if (Request.QueryString.Get("us") != null)
                {
                    DataTable dt = new DataTable();

                    string sql = "select USR_OPOBOJ_CODOBS as ID";
                    sql += "    ,t.USR_OPOBTP_DESCRP as tipo ";
                    sql += "	,USR_CLIOBJ_OBJDSC as cliente";
                    sql += "	,USR_OPOBOJ_OBSERV as obs";
                    sql += "	, replace(isnull(USR_OPOBOJ_FIRMA1,''),'192.168.1.141','127.0.0.1') as firma1";
                    sql += "	,USR_OPOBOJ_CORREC as correccion";
                    //sql += "	,convert(varchar,USR_OPOBOJ_FCHOBS,105) as fecha";
                    sql += "	,CONVERT(varchar, USR_OPOBOJ_FCHOBS ,105)+ ' ' + convert(varchar,USR_OPOBOJ_FCHOBS,108) as fecha";
                    sql += "	,e.USR_OPOBES_DESCRP as estado";
                    sql += " from [USR_OPOBOJ] o with(nolock)";
                    sql += " left join USR_OPOBES e WITH(NOLOCK) on o.USR_OPOBOJ_ESTADO=e.USR_OPOBES_CODIGO";
                    sql += " left join USR_OPOBTP t WITH(NOLOCK) on o.USR_OPOBOJ_TIPOOB=t.USR_OPOBTP_CODIGO";
                    sql += " left join USR_CLIOBJ c WITH(NOLOCK) on o.USR_OPOBOJ_CODCLI=c.USR_CLIOBJ_CODOBJ";
                    sql += " WHERE USR_OPOBOJ_USERNM='" + Request.QueryString.Get("us").ToString() + "'";

                    if (Request.QueryString.Get("estado") != null)
                    {
                        sql += " and USR_OPOBOJ_ESTADO='" + Request.QueryString.Get("estado").ToString() + "'";
                    }
                    if (Request.QueryString.Get("id") != null)
                    {
                        sql += " and USR_OPOBOJ_CODOBS='" + Request.QueryString.Get("id").ToString() + "'";
                    }
                    sql += " ORDER BY USR_OPOBOJ_CODOBS desc";


                    dt = Interfaz.EjecutarConsultaBD("CBS", sql);



                    if (dt.Rows.Count > 0)
                    {
                        List<Registros> l = new List<Registros>();
                        for (int t = 0; t < dt.Rows.Count; t++)
                        {
                            Registros p = new Registros();
                            p.ID = dt.Rows[t]["ID"].ToString();
                            p.tipo = dt.Rows[t]["tipo"].ToString();
                            p.cliente = dt.Rows[t]["cliente"].ToString();
                            p.obs = dt.Rows[t]["obs"].ToString();
                            p.firma1 = dt.Rows[t]["firma1"].ToString();
                            p.correccion = dt.Rows[t]["correccion"].ToString();
                            p.fecha = dt.Rows[t]["fecha"].ToString();
                            p.estado = dt.Rows[t]["estado"].ToString();
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
        }
        catch (Exception ex)
        {

            Response.Write(JsonHelper.JsonSerializer(ex.Message));
        }
    }

    public class Registros
    {
        public string ID { get; set; }
        public string tipo { get; set; }
        public string cliente { get; set; }
        public string obs { get; set; }
        public string firma1 { get; set; }
        public string correccion { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
    }


    public class UltimoID
    {
        public string Ultimo { get; set; }
    
    }
   
}
