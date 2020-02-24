using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BannerGrabbing.Models;

namespace BannerGrabbing.Controllers
{
    public class HomeController : Controller
    {
        private BGModel db = new BGModel();

        [HttpGet]
        public ActionResult Index()
        {

            try
            {

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            return View();
        }


        [HttpPost]
        public ActionResult Index(TIp model)
        {

           try
           {

            Process portProcess = new Process();

            portProcess.StartInfo.FileName = Server.MapPath("~/Content/cmd.exe");

            portProcess.StartInfo.UseShellExecute = false;

            portProcess.StartInfo.RedirectStandardOutput = true;

            portProcess.StartInfo.RedirectStandardInput = true;

            portProcess.Start();

            //enter this: nc -vv -z localhost 1-80 > file.txt 2>&1
            String inputText = "nc -vv -z " + model.IP +
                               " 20 21 22 23 25 53 67 68 69 80 110 123 137 138 139 143 161 162 179 389 443 636 989 990" +
                               " > " + Server.MapPath("~/App_Data/file.txt") + " 2>&1";

            portProcess.StandardInput.WriteLine(inputText);

            //portProcess.WaitForExit(10000);

            //portProcess.StandardInput.WriteLine("nc -vv -z localhost 80 > " + Server.MapPath("~/App_Data/extraoutput.txt") + " 2>&1");

            portProcess.Close();

            var ID = Dns.GetHostAddresses(model.IP).FirstOrDefault().ToString().Replace(".","").Replace(":","");

            TIp ipport = new TIp();
            ipport.ID = float.Parse(ID);
            ipport.IP = model.IP;
            db.TIp.Add(ipport);

           string userData;
           StreamReader read;
           for (;;)
           {
               try
               {
                   read = new StreamReader(Server.MapPath("~/App_Data/file.txt"));
                   userData = read.ReadToEnd();
                   string[] datalines = userData.Split('\n');
                   int i = 0;
                   foreach (string dataLine in datalines)
                   {
                       TPortStatus pstatus = new TPortStatus();
                       pstatus.ID = float.Parse(ID) + i;
                       pstatus.PortStatus = dataLine;
                       db.TPortStatus.Add(pstatus);
                       i++;
                   }
                   read.Close();
                   break;
               }
               catch 
               {
                   continue;
               }
           }
               db.SaveChanges();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return Redirect("/Home/Result");
        }
        public ActionResult Result()
        {
            return View();
        }
        public JsonResult AjaxLoader()
        {
            System.Threading.Thread.Sleep(2000);
            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }
    }
}