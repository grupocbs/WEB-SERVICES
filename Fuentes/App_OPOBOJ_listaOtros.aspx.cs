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

public partial class App_OPOBOJ_listaOtros: System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    sql += "	,CONVERT(varchar, USR_OPOBOJ_FCHOBS ,105)+ ' ' + convert(varchar,USR_OPOBOJ_FCHOBS,108) as fecha";
                    sql += "	,e.USR_OPOBES_DESCRP as estado";
                    sql += "    ,o.USR_OPOBOJ_USERNM as informo";
                    sql += " from [USR_OPOBOJ] o with(nolock)";
                    sql += " left join USR_OPOBES e WITH(NOLOCK) on o.USR_OPOBOJ_ESTADO=e.USR_OPOBES_CODIGO";
                    sql += " left join USR_OPOBTP t WITH(NOLOCK) on o.USR_OPOBOJ_TIPOOB=t.USR_OPOBTP_CODIGO";
                    sql += " left join USR_CLIOBJ c WITH(NOLOCK) on o.USR_OPOBOJ_CODCLI=c.USR_CLIOBJ_CODOBJ";
                    sql += " WHERE USR_OPOBOJ_USERNM<>'" + Request.QueryString.Get("us").ToString() + "'";
                    sql += " AND cast(o.USR_OPOBOJ_CODCLI as varchar(6)) COLLATE DATABASE_DEFAULT IN (SELECT cast(objetivo  as varchar(6))";
                    sql += " 							from INTRANET.dbo.USUARIO_OBJETIVO c with(nolock)";
                    sql += "                            where usuario='" + Request.QueryString.Get("us").ToString() + "')";
                    
                    if (Request.QueryString.Get("id") != null)
                    {
                        sql += " AND USR_OPOBOJ_CODOBS='" + Request.QueryString.Get("id").ToString() + "'";
                    }
                    sql += " ORDER BY o.USR_OP_FECALT desc";


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
                            p.informo = dt.Rows[t]["informo"].ToString();
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
        public string ID { get; set; }
        public string tipo { get; set; }
        public string cliente { get; set; }
        public string obs { get; set; }
        public string firma1 { get; set; }
        public string correccion { get; set; }
        public string fecha { get; set; }
        public string estado { get; set; }
        public string informo { get; set; }
    }


    public class UltimoID
    {
        public string Ultimo { get; set; }
    
    }
   
}
