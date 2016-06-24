using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;


/// <summary>
/// Descripción breve de Class1
/// </summary>
public class Interfaz
{

    public static DataTable EjecutarConsultaBD(string NombreBase, string consulta)
    {
        try
        {


            DataTable dt = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings[NombreBase].ConnectionString;
            SqlDataAdapter daMenu = new SqlDataAdapter(consulta, strConnString);
            daMenu.SelectCommand.CommandTimeout = 0;
            daMenu.Fill(dt);
            daMenu.SelectCommand.Connection.Close();
            daMenu.Dispose();

            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EjecutarConsultaBD(string NombreBase, string consulta)");
        }
    }
    public static DataTable EjecutarSP(string NombreBase, string nombresp, SortedList<string, string> parametros)
    {
        try
        {

            string strConnString = ConfigurationManager.ConnectionStrings[NombreBase].ConnectionString;

            SqlDataSource SqlDataSource1 = new SqlDataSource(strConnString, nombresp);
            SqlDataSource1.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
            SqlDataSource1.Selecting += new SqlDataSourceSelectingEventHandler(SqlDataSource1_Selecting);

            for (int i = 0; i < parametros.Count; i++)
            {
                SqlDataSource1.SelectParameters.Add(parametros.Keys[i], parametros.Values[i]);
            }


            DataView dt = new DataView();
            dt = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);

            SqlDataSource1.Dispose();
            return dt.ToTable();


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EjecutarSP(string NombreBase, string nombresp)");
        }
    }
    static void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        e.Command.CommandTimeout = 0;
    }

  

    #region CBS_Observaciones
    public static void AltaObservacion(string codobs,DateTime fecha, string tipoob, string codcli, string observ, string coordn, string usernm)
    {
        try
        {


            string sql = "INSERT INTO [USR_OPOBOJ] ([USR_OPOBOJ_CODOBS],[USR_OPOBOJ_FCHOBS],[USR_OPOBOJ_TIPOOB],[USR_OPOBOJ_CODCLI],[USR_OPOBOJ_ESTADO],[USR_OPOBOJ_OBSERV],[USR_OPOBOJ_COORDN],[USR_OP_FECALT],[USR_OP_FECMOD],[USR_OP_ULTOPR],[USR_OP_DEBAJA],[USR_OP_OALIAS],[USR_OP_USERID],[USR_OPOBOJ_USERNM])";
            sql += " VALUES ('" + codobs + "','" + fecha.ToString("yyyyMMdd HH:mm:ss.FFF") + "','" + tipoob + "','" + codcli + "','P','" + observ + "','" + coordn + "',GETDATE(),GETDATE(),'A','N','USR_OPOBOJ','ADMIN','" + usernm + "')";


            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AltaObservacion(string codobs,string fecha, string nroleg, string codemp, string tipoob, string codcli, string observ, string coordn, string usernm))");
        }

    }
    public static void EditarObservacion(string codobs,string tipoob, string codcli, string observ, string usernm, string Correcciones)
    {
        try
        {


            string sql = "UPDATE [USR_OPOBOJ] ";
            sql += " SET USR_OPOBOJ_TIPOOB='" + tipoob + "'";
            sql += " ,USR_OPOBOJ_CODCLI='" + codcli + "'";
            sql += " ,USR_OPOBOJ_OBSERV='" + observ + "'";
            sql += " ,USR_OPOBOJ_CORREC='" + Correcciones + "'";
            sql += " WHERE USR_OPOBOJ_CODOBS='" + codobs + "'";
	        sql += " and USR_OPOBOJ_USERNM='"+usernm+"'";

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarObservacion(string codobs,string tipoob, string codcli, string observ, string coordn)");
        }

    }
    public static void EditarObservacion_firma(string codobs, string usernm, string nombre_imagen_firma)
    {
        try
        {
            string url = @"\\192.168.1.141\web_services\Archivos\Capturas\" + usernm + @"\" + codobs + @"\" + nombre_imagen_firma;

            string sql = "UPDATE [USR_OPOBOJ] ";
            sql += " SET USR_OPOBOJ_FIRMA1='" + url + "'";
            sql += " WHERE USR_OPOBOJ_CODOBS='" + codobs + "'";
            sql += " and USR_OPOBOJ_USERNM='" + usernm + "'";

            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - EditarObservacion(string codobs,string tipoob, string codcli, string observ, string coordn)");
        }

    }
    public static void AltaCaptura_Observacion(string codobs, string nombre_imagen, string usuario)
    {
        try
        {
           //string url = "http://192.168.1.141:8080/Archivos/Capturas/" + codobs + "/" + nombre_imagen;
            string url = @"\\192.168.1.141\web_services\Archivos\Capturas\" + usuario + @"\" + codobs + @"\" + nombre_imagen;
		DateTime Fecha=DateTime.Now;

        string sql = "INSERT INTO [USR_OPOBCA] ([USR_OPOBCA_CODOBS],[USR_OPOBCA_URLOBS],[USR_OPOBCA_CPTITL],[USR_OPOBCA_FCHALT],[USR_OP_FECALT],[USR_OP_FECMOD],[USR_OP_ULTOPR],[USR_OP_DEBAJA],[USR_OP_OALIAS],[USR_OP_USERID],[USR_OPOBCA_USERNM])";
            sql += " VALUES ('" + codobs + "','" + url + "','" + nombre_imagen + "','" + Fecha.ToString("yyyyMMdd 00:00:00.000") + "',GETDATE(),GETDATE(),'A','N','USR_OPOBCA','ADMIN','" + usuario + "')";




            DataTable dtMenuItems = new DataTable();
            string strConnString = ConfigurationManager.ConnectionStrings["CBS"].ConnectionString;
            SqlCommand daMenu = new SqlCommand(sql);
            daMenu.Connection = new SqlConnection(strConnString);
            daMenu.Connection.Open();
            daMenu.ExecuteNonQuery();
            daMenu.Connection.Close();
            daMenu.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + " - AltaCaptura_Observacion(string codobs, string url)");
        }

    }
   
    #endregion
}