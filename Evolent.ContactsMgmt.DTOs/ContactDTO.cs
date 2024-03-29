﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evolent.ContactsMgmt.DTOs
{
    public class ContactDTO
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}