using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evolent.ContactsMgmt.WebApp.Models
{
    [Table("Contact")]
    public class ContactViewModel
    {
        [Key]
        public int ContactID { get; set; }
        [Required(ErrorMessage = "Required First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Required Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Required Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Required Phone Number")]
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}