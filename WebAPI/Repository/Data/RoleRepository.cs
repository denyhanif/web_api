using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Model;

namespace WebAPI.Repository.Data
{
    public class RoleRepository : GeneralRepository<MyContext, Role, int>
    {
       
        public RoleRepository(MyContext myContext) : base(myContext)
        {
            
        }

    }
}
