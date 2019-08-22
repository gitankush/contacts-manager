using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evolent.ContactsMgmt.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolent.ContactsMgmt.DTOs;
using System.Web.Http;
using Evolent.ContactsMgmt.Common.Helpers;
using Evolent.ContactsMgmt.Common.Contracts;
using Evolent.ContactsMgmt.BL.Repository;
using System.Web.Http.Results;
using System.Threading;
using System.Net;

namespace Evolent.ContactsMgmt.WebApi.Controllers.Tests
{
    [TestClass()]
    public class ContactsControllerTests
    {
        private ContactsController _contactsController;
        public ContactsControllerTests()
        {
            ServiceLocator.Register<IRepository<ContactDTO>>(new ContactRepository());
            _contactsController = new ContactsController();
            _contactsController.Configuration = new System.Web.Http.HttpConfiguration();
            _contactsController.Request = new System.Net.Http.HttpRequestMessage();
        }
        [TestMethod()]
        public void AddContactTest()
        {
            //Arrange
            ContactDTO contactDTO = new ContactDTO()
            {
                Email = "john.craig@gmail.com",
                FirstName = "John",
                LastName = "Craig",
                PhoneNumber = "9999999999",
                Status = true
            };
            _contactsController.DeleteContact(contactDTO.Email);

            //Act
            var addContactResult = _contactsController.AddContact(contactDTO);
            var addContactResponse = addContactResult.ExecuteAsync(CancellationToken.None).Result;
            //Assert
            Assert.AreEqual(HttpStatusCode.OK,addContactResponse.StatusCode);

        }
    }
}