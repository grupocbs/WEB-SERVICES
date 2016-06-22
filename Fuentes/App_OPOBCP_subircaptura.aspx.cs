using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class App_OPOBCP_subircaptura : System.Web.UI.Page
    {

    protected void Page_Init(object sender, EventArgs e)
        {
             try
             {
                Response.ContentEncoding = System.Text.Encoding.UTF8;
               
                    string carpeta = Request.Form["description"].ToString();
                    if (!Directory.Exists(Server.MapPath("Archivos\\Capturas\\" + carpeta)))
                    {
                        Directory.CreateDirectory(Server.MapPath("Archivos\\Capturas\\" + carpeta));
                    }


                    string FilePath = Server.MapPath("Archivos/Capturas/" + carpeta.Replace("\\","/") + "/" + Request.Form["title"].ToString());

                

                    HttpFileCollection MyFileCollection = Request.Files;
                    if (MyFileCollection.Count > 0)
                    {
                        MyFileCollection[0].SaveAs(FilePath);
                    }

               }
             
        catch (Exception ex)
        {

            Response.Write(JsonHelper.JsonSerializer(ex.Message));
        }
     }

        
 }
