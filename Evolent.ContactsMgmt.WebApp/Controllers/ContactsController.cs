using Evolent.ContactsMgmt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Evolent.ContactsMgmt.WebApp.Controllers
{
    public class ContactsController : Controller
    {
        private string _contactsManagerApiBaseUrl;
        public ContactsController()
        {
            _contactsManagerApiBaseUrl = ConfigurationManager.AppSettings["ContactsManagerApiBaseUrl"].ToString();
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllContacts()
        {
            IEnumerable<ContactViewModel> contacts = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_contactsManagerApiBaseUrl);

                var responseTask = client.GetAsync("getAllContacts");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<ContactViewModel>>();
                    readTask.Wait();

                    contacts = readTask.Result;
                }
                else
                {
                    contacts = Enumerable.Empty<ContactViewModel>();
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                }

            }
            return Json( contacts , JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteContact(int contactID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_contactsManagerApiBaseUrl);
                var deleteTask = client.DeleteAsync($"deleteContact/{contactID}");
                var result = deleteTask.Result;
                var data = new
                {
                    statusCode = result.StatusCode,
                    message = result.Content.ReadAsStringAsync().Result
                };
                return Json(data, JsonRequestBehavior.AllowGet);

            }
        }

        public ActionResult DeleteContact(string email)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_contactsManagerApiBaseUrl);
                var deleteTask = client.DeleteAsync($"deleteContact/{email}");
                var result = deleteTask.Result;
                var data = new
                {
                    statusCode = result.StatusCode,
                    message = result.Content.ReadAsStringAsync().Result
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddContact(ContactViewModel newContact)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { messaage = "Not a valid model" }, JsonRequestBehavior.AllowGet);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_contactsManagerApiBaseUrl);
                var postTask = client.PostAsJsonAsync<ContactViewModel>("addContact",newContact);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new {success=true,message= result.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = result.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult UpdateContact(ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { messaage = "Not a valid model" }, JsonRequestBehavior.AllowGet);
            }
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_contactsManagerApiBaseUrl);
                var putTask = client.PutAsJsonAsync<ContactViewModel>("updateContact", contact);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Json(new { success = true, message = result.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = result.Content.ReadAsStringAsync().Result }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult GetContact(int contactID)
        {
            ContactViewModel contact = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_contactsManagerApiBaseUrl);

                var responseTask = client.GetAsync($"getContact/{contactID}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                    readTask.Wait();

                    contact = readTask.Result;
                    return Json(contact, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //Error response received   
                    ModelState.AddModelError(string.Empty, result.Content.ReadAsStringAsync().Result);
                    return Json(ModelState, JsonRequestBehavior.AllowGet);
                }

            }
        }
    }
}