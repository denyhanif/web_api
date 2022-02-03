using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class AccountRoleController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        private readonly MyContext context;
        public readonly AccountRoleRepository accrolerepo;
        public AccountRoleController(AccountRoleRepository accountrolerepository,MyContext context) : base(accountrolerepository)
        {
            this.context = context;
            this.accrolerepo = accountrolerepository;
        }


        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult SignManager(AccountRoleVM accountrolevm)
        {
            var getEmployee =accrolerepo.SignInManager(accountrolevm);
            if (getEmployee !=0)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Role Berhasil diubah!" });
            }
            return StatusCode(404, new { status = HttpStatusCode.NotFound, message = "Nik Tidak DiTemukan" });
        }

    }
}
