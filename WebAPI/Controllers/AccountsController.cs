
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
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
        private readonly MyContext context;
        public AccountRepository accountrepo;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountrepo,IConfiguration configuration, MyContext Mycontext) : base(accountrepo)
        {
            this.accountrepo = accountrepo;
            this._configuration = configuration;
            this.context = Mycontext;
        }
        [HttpGet("Login")]
        public ActionResult Login(RegisterVM registervm)
        {
            var result = accountrepo.Login(registervm);
            if (result == 1)
            {
                var a = accountrepo.GetProfile(registervm);

                var getUserData = context.Employees.Where(e => e.Email == registervm.email
               || e.Phone == registervm.PhoneNumber).FirstOrDefault(); 
                var getRole = context.Roles.Where(a => a.AccountRole.Any(ar => ar.Account.NIK == getUserData.NIK)).ToList();

                var claims = new List<Claim> {
                        new Claim("Email", getUserData.Email ),
                        //new Claim("Role", roles.Role.Name ),
                    };

                foreach (var item in getRole)//multiple role
                {
                    claims.Add(new Claim("roles", item.Name));
                }
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires :DateTime.UtcNow.AddHours(12),
                            signingCredentials: signIn
                    );
                var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idToken.ToString()));

                return StatusCode(200, new { status = HttpStatusCode.OK, idToken, message = "account ditemukan" });
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
            var result = accountrepo.ForgotPassword(registervm);
            if(result != 0)
            {
   
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Email telah di kirim" });

            }
            return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email tidak ditemukan" });
        }


        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ForgotPasswordVM forgotvm)
        {
            var result = accountrepo.ChangePassword(forgotvm);
            if (result != 0)
            {
                if (result == 1)
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Password Berhasil diubah!" });
                }
                else if (result == 2)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "New Password dan Confirm Password tidak sama" });
                }
                else if (result == 3)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP sudah digunakan" });
                }
                else if (result == 4)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP Expired" });
                }
                else
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Kode OTP salah!" });
                }
            }
            else
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "EMAIL TIDAK DITEMUKAN!" });
            }
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Tes JWT BERHASIL");
        }



    }
}

/**var userData = (from e in context.Employees where e.Email == registervm.email select e).FirstOrDefault<Employee>();
var roles = (from e in context.AccountRoles where e.Account_id == userData.Account.NIK select e).FirstOrDefault<AccountRole>().;
var roles = context.Roles.Where(a => AccountRole.Any(ar => ar.Account.NIK == getUserData.NIK)).ToList();
*/