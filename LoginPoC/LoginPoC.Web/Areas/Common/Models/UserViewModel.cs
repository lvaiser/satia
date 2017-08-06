using System;
using LoginPoC.Model.User;

namespace LoginPoC.Web.Areas.Common.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Country Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public bool CanRead { get; set; }
        public string CultureName { get; set; }
    }
}