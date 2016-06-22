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

public partial class App_OPOBOJ_MailConf : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Get("us") != null)
            {

                DataTable dt = new DataTable();
                string sql = "SELECT USR_OPOBCF_CODCNF as codigo";
                sql += " , USR_OPOBCF_MLSMTP AS direccion_smtp";
                sql += " , USR_OPOBCF_SMTPPT AS puerto_smtp";
                //sql += " , USR_OPOBCF_MLFROM AS direccion_envia";
                sql += " , USR_OPOBCF_MLDEST AS direccion_recibe1";
                sql += " , USR_OPOBCF_MDEST2 AS direccion_recibe2";
                sql += " , USR_OPOBCF_MLTITU AS titulo";
                sql += " , USR_OPOBCF_MLDESC AS cuerpo";
                sql += " , USR_OPOBCF_USASSL AS ssl";
                //sql += " , USR_OPOBCF_USSMTP AS usuario_smtp";
                //sql += " , USR_OPOBCF_PASMTP AS pass_smtp";

                sql += " FROM USR_OPOBCF with(nolock)  ";
                sql += " WHERE USR_OPOBCF_CODCNF='APPOBS'";

                dt = Interfaz.EjecutarConsultaBD("CBS",sql);

                if (dt.Rows.Count > 0)
                {
                    List<Registros> l = new List<Registros>();
                    for (int t = 0; t < dt.Rows.Count; t++)
                    {
                        Registros p = new Registros();
                        p.codigo = dt.Rows[t]["codigo"].ToString();
                        p.direccion_smtp = dt.Rows[t]["direccion_smtp"].ToString();
                        p.puerto_smtp = dt.Rows[t]["puerto_smtp"].ToString();
                       // p.direccion_envia = dt.Rows[t]["direccion_envia"].ToString();
                        p.direccion_recibe1 = dt.Rows[t]["direccion_recibe1"].ToString();
                        p.direccion_recibe2 = dt.Rows[t]["direccion_recibe2"].ToString();
                        p.titulo = dt.Rows[t]["titulo"].ToString();
                        p.cuerpo = dt.Rows[t]["cuerpo"].ToString();
                        p.ssl = dt.Rows[t]["ssl"].ToString();
                       // p.usuario_smtp = dt.Rows[t]["usuario_smtp"].ToString();
                       // p.pass_smtp = dt.Rows[t]["pass_smtp"].ToString();
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
        public string codigo { get; set; }
        public string direccion_smtp { get; set; }
             public string puerto_smtp { get; set; }
          public string direccion_envia { get; set; }
          public string direccion_recibe1 { get; set; }
          public string direccion_recibe2 { get; set; }
           public string titulo { get; set; }
          public string cuerpo { get; set; }
           public string ssl { get; set; }
           public string usuario_smtp { get; set; }
             public string pass_smtp { get; set; }
    }
    

   

}
