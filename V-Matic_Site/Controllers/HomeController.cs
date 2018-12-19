using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vmatic_Site.Models;
using Vmatic_Site.ViewModels;

namespace V_Matic_Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Contact(Mail content)
        {
            //TO-DO: Mail-service in werking krijgen
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(content.Sender);
                    mail.To.Add("beheerder@vmatic.be");
                    mail.Subject = content.Subject;
                    mail.Body = content.Message;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.one.com";
                    smtp.Port = 465;
                    smtp.Credentials = new System.Net.NetworkCredential("beheerder@vmatic.be", "vmatic47");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    ModelState.Clear();

                    return View("Thanks");
                }
                catch (Exception e)
                {
                    ModelState.Clear();
                    return View("Error");
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult Thanks()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
