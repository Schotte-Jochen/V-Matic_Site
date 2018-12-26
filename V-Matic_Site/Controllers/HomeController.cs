using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
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
            throw new NotImplementedException();
            //TO-DO: Mail-service in werking krijgen
            if (ModelState.IsValid)
            {
                /*try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(content.Sender);
                    mail.To.Add("beheerder@vmatic.be");
                    mail.Subject = content.Subject;
                    mail.Body = content.Message;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "send.one.com";
                    smtp.Port = 465;
                    smtp.Credentials = new System.Net.NetworkCredential("dummy@vmatic.be", "dummy1");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    ModelState.Clear();

                    return View("Thanks");
                }
                catch (Exception e)
                {
                    ModelState.Clear();
                    return View("Error");
                }*/
                //prepare email
                var toAddress = "beheerder@vmatic.be";
                var fromAddress = content.Sender;
                var subject = content.Subject;
                var message = content.Message;

                //start email Thread
                var tEmail = new Thread(() =>
               SendEmail(toAddress, fromAddress, subject, message));
                tEmail.Start();
                return View("Thanks");
            }
            else
            {
                return View();
            }
        }

        private void SendEmail(string toAddress, string fromAddress,
                      string subject, string message)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    const string email = "dummy@telenet.be";
                    const string password = "dummy1";

                    var loginInfo = new NetworkCredential(email, password);


                    mail.From = new MailAddress(fromAddress);
                    mail.To.Add(new MailAddress(toAddress));
                    mail.Subject = subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;

                    try
                    {
                        using (var smtpClient = new SmtpClient(
                                                         "send.one.com", 465))
                        {
                            smtpClient.EnableSsl = true;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = loginInfo;
                            smtpClient.Send(mail);
                        }

                    }

                    finally
                    {
                        //dispose the client
                        mail.Dispose();
                    }

                }
            }
            catch (SmtpFailedRecipientsException ex)
            {
                /*foreach (SmtpFailedRecipientException t in ex.InnerExceptions)
                {
                    var status = t.StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        Response.Write("Delivery failed - retrying in 5 seconds.");
                        System.Threading.Thread.Sleep(5000);
                        //resend
                        //smtpClient.Send(message);
                    }
                    else
                    {
                        Response.Write("Failed to deliver message to {0}",
                                          t.FailedRecipient);
                    }
                }*/
            }
            catch (SmtpException Se)
            {
                // handle exception here
                //Response.Write(Se.ToString());
            }

            catch (Exception ex)
            {
                //Response.Write(ex.ToString());
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
