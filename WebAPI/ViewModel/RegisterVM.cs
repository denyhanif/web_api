using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModel
{
    public class RegisterVM
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string PhoneNumber { set; get; }
        public DateTime bitrhDate { set; get; }
        public int salary { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public string degree { set; get; }
        public string GPA { set; get; }
        public string nik { set; get; }
        public int universityId { set; get; }
    }
}
