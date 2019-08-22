using Evolent.ContactsMgmt.Common.Contracts;
using Evolent.ContactsMgmt.Common.Helpers;
using Evolent.ContactsMgmt.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Evolent.ContactsMgmt.WebApi.Controllers
{
    [RoutePrefix("api/contact")]
    public class ContactsController : ApiController
    {
        private IRepository<ContactDTO> _contactRepository;
        public ContactsController()
        {
            _contactRepository = ServiceLocator.Resolve<IRepository<ContactDTO>>();
        }
        [Route("getAllContacts")]
        [HttpGet]
        public IHttpActionResult GetAllContacts()
        {
            var contacts = _contactRepository.GetAll();
            if (contacts.Count() == 0)
            {
                return NotFound();
            }
            return Ok(contacts);
        }

        [Route("getContact/{contactID}")]
        [HttpGet]
        public IHttpActionResult GetContact(int contactID)
        {
            var contact = _contactRepository.Get(contactID);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [Route("addContact")]
        [HttpPost]
        public IHttpActionResult AddContact(ContactDTO contactDTO)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            if (!_contactRepository.IsDataAlreadyExist(contactDTO))
            {
                _contactRepository.Add(contactDTO);
                int result = _contactRepository.Save();
                if (result == 1)
                {
                    var message = "Contact added successfully!!!";
                    response.Add("Message", message);
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Contact not added!!!");
                }
            }
            else
            {
                return BadRequest("Contact with matching Email or Phone already exist!!!");
            }
        }

        [Route("updateContact")]
        [HttpPut]
        public IHttpActionResult UpdateContact(ContactDTO contactDTO)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();

            _contactRepository.Update(contactDTO);
            int result = _contactRepository.Save();
            if (result == 1)
            {
                var message = "Contact updated successfully!!!";
                response.Add("Message", message);
                return Ok(response);
            }
            else
            {
                return BadRequest("Contact not updated!!! Please try again.");
            }

        }

        [Route("deleteContact/{contactID}")]
        [HttpDelete]
        public IHttpActionResult DeleteContact(int contactID)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            _contactRepository.Delete(contactID);
            if (_contactRepository.Save() == 1)
            {
                var message = "Contact deleted successfully!!!";
                response.Add("Message", message);
                return Ok(response);
            }
            else
            {
                return BadRequest("Contact not deleted!!! Please try again.");
            }
        }

        [Route("deleteContact/{email}")]
        [HttpDelete]
        public IHttpActionResult DeleteContact(string email)
        {
            Dictionary<string, string> response = new Dictionary<string, string>();
            _contactRepository.Delete(email);
            if (_contactRepository.Save() == 1)
            {
                var message = "Contact deleted successfully!!!";
                response.Add("Message", message);
                return Ok(response);
            }
            else
            {
                return BadRequest("Contact not deleted!!! Please try again.");
            }
        }
    }
}
