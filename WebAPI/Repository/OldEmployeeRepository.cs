using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Model;
using WebAPI.Repository.Interface;

namespace WebAPI.Repository
{
    public class OldEmployeeRepository : IEmployeeRepository
    {
        public readonly MyContext context;

        public OldEmployeeRepository(MyContext context)
        {
            this.context = context;
        }

        public int Delete(string NIK)
        {
           
            var entity = context.Employees.Where(x => x.NIK == NIK).FirstOrDefault();// context.Employees.Find(NIK);
            context.Remove(entity);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
           
        }

        public int Insert(Employee employe)
        {

            var EmailExist = isEmailExist(employe);

            var PhoneExist = isPhoneExist(employe);

            if(EmailExist == false)
            {
                if(PhoneExist == false)
                {
                    var year = DateTime.Now.ToString("yyyy");
                    var lastId = context.Employees.ToList().Count() + 1;

                    var format = year + "00" + lastId;

                    //while (!format.Equals(Get(format)))
                    //{
                    //    format = year + "00" + (lastId += 1);
                    //    if (Get(format) == null)
                    //    {
                    //        break;
                    //    }
                    //}
                    employe.NIK = format;
                    context.Employees.Add(employe);
                    var result = context.SaveChanges();
                    return result;
                }
                else
                {
                    return 3;//telp sudah ada
                }

            }else if ( EmailExist==true && PhoneExist == true)
            {
                return 4;//email dan phone sudah ada
            }else
            {
                return 2;//email sudah ada
            }






            

            //employe.NIK = nikFormat;
            //var phone = (from e in context.Employees where e.Phone == employe.Phone select e).FirstOrDefault<Employee>();
            //var email = (from e in context.Employees where e.Email == employe.Email select e).FirstOrDefault<Employee>();
            //if (phone == null && email == null ) {
            //    employe.NIK = format;
            //    context.Employees.Add(employe);
            //}
            //var result = context.SaveChanges();
            //return result;
        }

        public int Update(Employee employe)
        {
            var phone = (from s in context.Employees where s.Phone == employe.Phone select s).FirstOrDefault<Employee>();
            var email = (from s in context.Employees where s.Email == employe.Email select s).FirstOrDefault<Employee>();

            if (phone == null && email == null)
            {
                context.Entry(employe).State = EntityState.Modified;
            }
         
            var result = context.SaveChanges();
            return result;
        }

        public bool isEmailExist(Employee employe)
        {
            var CekEmail = context.Employees.Where(emp => emp.Email == employe.Email).FirstOrDefault();
            if (CekEmail !=null) {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isPhoneExist(Employee employe)
        {
            var CekPhone = context.Employees.Where(emp => emp.Phone == employe.Phone).FirstOrDefault();
            if(CekPhone != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}







//context.Employees.Where(x => x.NIK == NIK).FirstOrDefault();
//




/*var phone = (from s in context.Employees where s.Phone == employe.Phone select s).FirstOrDefault<Employee>();
            var email = (from s in context.Employees where s.Email == employe.Email select s).FirstOrDefault<Employee>();
            
            if (phone == null && email == null)
            {
                context.Entry(employe).State = EntityState.Modified;
            }
            else if (phone.Equals(employe.Phone) && phone.Equals(employe.Phone))
            {

                context.Entry(employe).State = EntityState.Modified;
            }
            var result = context.SaveChanges();
            return result;*/
