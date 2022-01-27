using MailKit.Net.Smtp;
using MimeKit;
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
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext context;

        public AccountRepository(MyContext Mycontext) : base(Mycontext)
        {
            this.context = Mycontext;
        }

        public int Login(RegisterVM registervm)
        {
            var phone = (from e in context.Employees where e.Phone == registervm.PhoneNumber select e).FirstOrDefault<Employee>();
            var email = (from e in context.Employees where e.Email == registervm.email select e).FirstOrDefault<Employee>();

            if(phone != null || email !=null){
                var result = (
                    from emp in context.Employees
                    join acc in context.Accounts on emp.NIK equals acc.NIK
                    where emp.Phone == registervm.PhoneNumber || emp.Email == registervm.email
                    select acc).FirstOrDefault();

                if (BCrypt.Net.BCrypt.Verify(registervm.password, result.Password)) 
                    {
                         return 1;//login
                }
                else
                {
                    return 3;//pass tdk ada
                }

            }
            else
            {
                return 2;//akun tidak ada
            }     

        }


        public void ForgotPassword(RegisterVM registervm)
        {
            var isEmail = context.Employees.Where(s => s.Email == registervm.email).FirstOrDefault<Employee>();

            if(isEmail != null)
            {
                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Admin",
                "emailmii112@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User",registervm.email);
                message.To.Add(to);
                message.Subject = "Kode OTP";
                Random generator = new Random();
                int otpCode = generator.Next(0, 1000000);
                
                //email body
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Hello W</h1>";
                bodyBuilder.TextBody = "Kode otp" + otpCode;

                //Smtp Config
                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate("emailmii112@gmail.com", "pengamanmii11");

                //message.Body();
            }
            
        }
       
        public void sedEmail()
        {

        }

        public IEnumerable GetProfile(RegisterVM registervm)
        {
            var result = (from emp in context.Employees
                          join acc in context.Accounts on emp.NIK equals acc.NIK
                          join prof in context.Profillings on acc.NIK equals prof.NIK
                          join edu in context.Educations on prof.Education_id equals edu.Id
                          join univ in context.Universities on edu.University_id equals univ.Id
                          where emp.Email == registervm.email || emp.Phone == registervm.PhoneNumber 
                          select new
                          {
                              FullName = emp.FirstNama + " " + emp.LastName,
                              Phone = emp.Phone,
                              Birthdate = emp.BirthDate,
                              Salary = emp.Salary,
                              email = emp.Email,
                              degree = edu.Degree,
                              universitas = univ.Name,

                          });

            return result;
        }
    }
}

//select new
//{
//    phohe = emp.Phone,
//    email = emp.Email,
//    password = acc.Password
//}).ToList().Where(e => (e.phohe == registervm.PhoneNumber || e.email == registervm.email) && BCrypt.Net.BCrypt.Verify(registervm.password, e.password));
