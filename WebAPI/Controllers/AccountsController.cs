using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Model;
using WebAPI.Repository.Data;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        public AccountRepository accountrepo;
        public AccountsController(AccountRepository accountrepo) : base(accountrepo)
        {
            this.accountrepo = accountrepo;
        }
        [HttpGet("Login")]
        public ActionResult Login(RegisterVM registervm)
        {
            var result = accountrepo.Login(registervm);
            if (result == 1)
            {
                var a = accountrepo.GetProfile(registervm);
                // //RedirectToAction("accountrepo.GetProfile", new { registervm = registervm });
                return StatusCode(200, new { status = HttpStatusCode.OK, a, message = "account ditemukan" });
                    //Ok(accountrepo.GetProfile(registervm).ToString());
            }
            else if (result == 2)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "account tidak ditemukan" });
            }
            else if (result == 3)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "password tidak ada" });
            }

            return StatusCode(400, new { status = HttpStatusCode.BadRequest, result, message = "Login gagal" });


        }

        [HttpPost("{ForgotPassword}")]
        public ActionResult ForgotPassword(RegisterVM registervm)
        {
            accountrepo.ForgotPassword(registervm);
           return  Ok(new { status = HttpStatusCode.OK, message = "Cek email" });
        }

        //public string GetProfile(RegisterVM registervm)
        //{
        //   private readonly MyContext context;

        //var result = (from emp in context.Employees
        //                  join acc in context.Accounts on emp.NIK equals acc.NIK
        //                  join prof in context.Profillings on acc.NIK equals prof.NIK
        //                  join edu in context.Educations on prof.Education_id equals edu.Id
        //                  join univ in context.Universities on edu.University_id equals univ.Id
        //                  where emp.Email == registervm.email
        //                  select new
        //                  {
        //                      FullName = emp.FirstNama + " " + emp.LastName,
        //                      Phone = emp.Phone,
        //                      Birthdate = emp.BirthDate,
        //                      Salary = emp.Salary,
        //                      email = emp.Email,
        //                      degree = edu.Degree,
        //                      universitas = univ.Name,

        //                  });

        //    return result.ToString();
        //}


    }
}
