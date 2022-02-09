using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Model;
using WebAPI.ViewModel;

namespace WebAPI.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;

        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.context = myContext;

        }

        public int Register(RegisterVM registervm)
       {
            
            var idbaru = "";
            var year = DateTime.Now.ToString("yyyy");
            var i = context.Employees.ToList().Count();
            if (i != 0)
            {
                foreach (Employee e in Get())
                {
                    idbaru = e.NIK;
                }
                idbaru = Convert.ToString(int.Parse(idbaru) + 1);
            }
            else
            {
                idbaru = year + "001";
            }
            var EmailExist = IsEmailExist(registervm);
            var PhoneExist = IsPhoneExist(registervm);

            if (EmailExist == false)
            {
                if (PhoneExist == false)
                {
                    Employee emp = new Employee()
                    {
                        FirstNama = registervm.FirstName,
                        LastName = registervm.LastName,
                        Phone = registervm.PhoneNumber,
                        BirthDate = registervm.bitrhDate,
                        Salary = registervm.salary,
                        Email = registervm.email,
                        NIK = registervm.nik,
                    };
                    emp.NIK = idbaru;
                    context.Employees.Add(emp);
                    context.SaveChanges();

                    Account acc = new Account()
                    {
                        NIK = emp.NIK,
                     
                        Password = registervm.password,
                    };
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(acc.Password);
                   

                    context.Accounts.Add(acc);
                    context.SaveChanges();

                    var accRole = new AccountRole()
                    {
                        Account_id = emp.NIK,
                        Role_id = 1
                    };
                    context.AccountRoles.Add(accRole);

                    Education edu = new Education()
                    {
                        Degree = registervm.degree,
                        GPA = registervm.GPA,
                        University_id = registervm.universityId,
                    };
                    context.Educations.Add(edu);
                    context.SaveChanges();

                    Profilling prof = new Profilling()
                    {
                        NIK = acc.NIK,
                        Education_id = edu.Id
                    };
                    context.Profillings.Add(prof);
                    context.SaveChanges();
                    return 1;//

                    

                }
                else
                {
                    return 2;//phone exist
                }
            }else if (EmailExist == true && PhoneExist == true)
            {
                return 3;//phone dan emal exist
            }

            else
            {
                return 4;//email 
            }

        }

        public IEnumerable GetRegister()
        {

            //var emp = context.Employees.Include(e =>e.Account).ThenInclude(a=> a.Profilling).ThenInclude(ed=>ed.Education).ToList();

            var result = (from emp in context.Employees
                          join acc in context.Accounts on emp.NIK equals acc.NIK
                          join accrole in context.AccountRoles on acc.NIK equals accrole.Account_id
                          join role in context.Roles on accrole.Role_id equals role.Id
                          join prof in context.Profillings on acc.NIK equals prof.NIK
                          join edu in context.Educations on prof.Education_id equals edu.Id
                          join univ in context.Universities on edu.University_id equals univ.Id
                          select new { 
                              FullName = emp.FirstNama+" " +emp.LastName,
                              Phone = emp.Phone,
                              Birthdate= emp.BirthDate,
                              Salary = emp.Salary,
                              email= emp.Email,
                              degree = edu.Degree,
                              universitas = univ.Name,
                              role = role.Name,
                            
                          });
                          
            return result;

        }

        public bool IsEmailExist(RegisterVM registervm)
        {
            var cek = context.Employees.Where(s => s.Email == registervm.email).FirstOrDefault<Employee>();
            if (cek != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsPhoneExist(RegisterVM registervm)
        {
            var cek2 = context.Employees.Where(s => s.Phone == registervm.PhoneNumber).FirstOrDefault<Employee>();
            if (cek2 != null)
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

