using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolent.ContactsMgmt.WebApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolent.ContactsMgmt.WebApp.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Evolent.ContactsMgmt.WebApp.Controllers.Tests
{
    [TestClass()]
    public class ContactsControllerTests
    {
        private ContactsController _contactsController;
        public ContactsControllerTests()
        {
            _contactsController = new ContactsController();
        }
        [TestMethod()]
        public void AddContactTest()
        {
            //Arrange
            bool expectedResult = true;
            ContactViewModel newContact = new ContactViewModel()
            {
                Email = "john.craig@gmail.com",
                FirstName = "John",
                LastName = "Craig",
                PhoneNumber = "9999999999",
                Status = true
            };

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            var allContactsJsonString = jsonSerializer.Serialize(_contactsController.GetAllContacts().Data);
            List<ContactViewModel> contactViewModels = jsonSerializer.Deserialize<List<ContactViewModel>>(allContactsJsonString);
            var testContact=contactViewModels.Where(c => c.Email.Equals(newContact.Email)).FirstOrDefault();
            if (testContact != null)
            {
                _contactsController.DeleteContact(testContact.ContactID);
            }

            //Act
            JsonResult result = _contactsController.AddContact(newContact) as JsonResult;
            var addedContactJsonString= jsonSerializer.Serialize(result.Data);

            //Assert
            Assert.AreEqual(expectedResult, addedContactJsonString.Contains("Contact added successfully"));
        }
    }
}