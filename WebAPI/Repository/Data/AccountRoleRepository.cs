using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Model;
using WebAPI.ViewModel;

namespace WebAPI.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext, AccountRole, int>
    {
        private readonly MyContext context;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;
        }

        public int SignInManager(AccountRoleVM accouuntrolevm)
        {
            var getEmployee = (from e in context.Employees where e.NIK == accouuntrolevm.NIK select e).SingleOrDefault();

            if(getEmployee != null)
            {

                var accountRole = new AccountRole();
                accountRole.Account_id = accouuntrolevm.NIK;
                accountRole.Role_id = 3;

                context.Add(accountRole);
                var result = context.SaveChanges();
                return result;
            }
            else
            {
                return 0;
            }


        }
    }
}
