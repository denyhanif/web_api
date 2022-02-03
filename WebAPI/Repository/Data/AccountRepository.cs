
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Model;
//using System.Net.Mail.SmtpClient;
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
            //var role = (from e in context.AccountRoles where e.Account_id == registervm.nik select e).FirstOrDefault<AccountRole>();
            //var roleid = (from e in context.AccountRoles where e.Id == role)

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


        public int ForgotPassword(RegisterVM registervm)
        {
            var isEmail = context.Employees.Where(s => s.Email == registervm.email).FirstOrDefault<Employee>();

            if(isEmail != null)
            {
                Random generator = new Random();
                int otpCode = generator.Next(0, 1000000);
                var account = (from g in context.Accounts where g.NIK == isEmail.NIK select g).FirstOrDefault<Account>();
          
                string emailFromAddress = ""; //Sender Email Address  
                string password = ""; //Sender Password  

                account.OTP = Convert.ToInt32(otpCode);
                account.ExpiredToken = DateTime.Now.AddMinutes(5);
                account.isUsed = false; //belum dipake otp-nya
                context.Entry(account).State = EntityState.Modified; //insert data di account
                context.SaveChanges();

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(registervm.email);
                mail.Subject = "OTP " + DateTime.Now;
                mail.Body = "Hello, This is your OTP " + otpCode;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                
                    smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                

                return 1;
                //message.Body();
            }
            else
            {

                return 2;//akun tidak ditemukan
            }

            
        }

        public int ChangePassword(ForgotPasswordVM forgotvm)
        {
            var emailNow = (from g in context.Employees where g.Email == forgotvm.Email select g).FirstOrDefault<Employee>();
            if (emailNow != null)
            {
                var accountNow = (from g in context.Accounts where g.NIK == emailNow.NIK select g).FirstOrDefault<Account>();
                if (accountNow != null)
                {
                    if (accountNow.OTP == forgotvm.Otp)
                    {
                        if (DateTime.Now < accountNow.ExpiredToken)
                        {
                            if (accountNow.isUsed == false)
                            {
                                if (forgotvm.NewPassword == forgotvm.ConforimPassword)
                                {
                                    accountNow.Password = BCrypt.Net.BCrypt.HashPassword(forgotvm.NewPassword);
                                    accountNow.isUsed = true;
                                    context.Entry(accountNow).State = EntityState.Modified;
                                    context.SaveChanges();
                                    return 1; //berhasil
                                }
                                else
                                {
                                    return 2; //Password & ConfirmPass ga sama
                                }
                            }
                            else
                            {
                                return 3; //OTP udah dipakai
                            }
                        }
                        else
                        {
                            return 4; //OTP expired
                        }
                    }
                    else
                    {
                        return 5; //OTP salah
                    }
                }

            }
            return 0;
        }

        public int ChangePassword2(ForgotPasswordVM forgotvm)
        {
            var email = (from e in context.Employees where e.Email == forgotvm.Email select e).FirstOrDefault<Employee>();
            var acc = (from e in context.Accounts where e.NIK == email.NIK select e).FirstOrDefault<Account>();

            if(email !=null)
            {
                if (DateTime.Now < acc.ExpiredToken)
                {
                    if(forgotvm.NewPassword == forgotvm.ConforimPassword)
                    {
                        if (acc.isUsed == false)  
                        {
                            if(acc.OTP == forgotvm.Otp)
                            {
                                acc.Password = forgotvm.ConforimPassword;
                                acc.isUsed = true;
                                context.Entry(acc).State = EntityState.Modified;
                                context.SaveChanges();
                                return 2;//berhasil 
                            }
                            else
                            {
                                return 3;//otp tidak sama
                            }
                        }
                        else
                        {
                            return 4;//otptelah di gunakan 
                        }
                    }
                    else
                    {
                        return 5;//password salah tidak sama
                    }
                }
                else {
                    return 6;//token not valid
                }
            }
            return 1;//emailtidak di temukan
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

