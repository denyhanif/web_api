using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModel
{
    public class ForgotPasswordVM
    {
        public string Email { set; get; }
        public int Otp { set; get; }
        public string NewPassword { set; get; }
        public string ConforimPassword { set; get; }
    }
}
