using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            var emp = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.PhoneNumber,
                BirthDate = registerVM.Birthdate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Gender)registerVM.Gender
            };
            myContext.Employee.Add(emp);

            string hassPassword = HashPassword(registerVM.Password);

            var acc = new Account
            {
                NIK = emp.NIK,
                Password = hassPassword
            };
            myContext.Account.Add(acc);

            var acr = new AccountRole
            {
                NIK = acc.NIK,
                Role_Id = 3
            };
            myContext.AccountRole.Add(acr);

            var edu = new Education
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                University_Id = registerVM.UniversityId
            };
            myContext.Education.Add(edu);
            myContext.SaveChanges();

            var pro = new Profiling
            {
                NIK = acc.NIK,
                Education_Id = edu.Id
            };
            myContext.Profiling.Add(pro);

            return myContext.SaveChanges();
        }

        public int Login(LoginVM loginVM)
        {
            //var checkLogin = myContext.Employee.Include("Account").FirstOrDefault(e => e.Email == loginVM.Email && e.Account.Password == loginVM.Password);
            var checkEmail = myContext.Employee.FirstOrDefault(e => e.Email == loginVM.Email);
            if (checkEmail != null)
            {
                var checkPass = myContext.Account.FirstOrDefault(a => a.NIK == checkEmail.NIK);
                if(checkEmail.Email == loginVM.Email && ValidatePassword(loginVM.Password, checkPass.Password))
                {
                    return 1;
                }
                return 2;
            }
            return 3;
            
        }

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {

            Random random = new Random();
            var RandomNumber = (random.Next(100000, 900000)).ToString("D6"); // membuat angka random dengan 6 digit

            var emp = myContext.Employee.Where(e => e.Email == forgotPasswordVM.Email).SingleOrDefault();
            var acc = myContext.Account.Where(a => a.NIK == emp.NIK).SingleOrDefault();
            acc.OTP = Convert.ToInt32(RandomNumber);
            acc.EkspiredToken = DateTime.Now.AddMinutes(5);
            acc.isUsed = false;
            myContext.Entry(acc).State = EntityState.Modified;
            var result = myContext.SaveChanges();

            // ngirim email
            string to = emp.Email.ToString();
            string from = "jonaabatman@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Kode OTP : {acc.OTP}";
            message.Subject = "OTP Ganti Password";
            message.Body = mailBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential basicCredential = new NetworkCredential("jonaabatman@gmail.com", "snowyking-123");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential;
            try
            {
                client.Send(message);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var checkEmp = myContext.Employee.SingleOrDefault(e => e.Email == changePasswordVM.Email);
            if(checkEmp != null)
            {
                var checkAcc = myContext.Account.SingleOrDefault(a => a.NIK == checkEmp.NIK);
                if (checkAcc.OTP == changePasswordVM.OTP && changePasswordVM.NewPassword == changePasswordVM.ConfirmPassword)
                {
                    checkAcc.isUsed = true;
                    string HasingPassword = HashPassword(changePasswordVM.NewPassword);
                    checkAcc.Password = HasingPassword;
                    myContext.Entry(checkAcc).State = EntityState.Modified;
                    myContext.SaveChanges();
                    return 1;
                }
                return 2;
            }
            return 3;
        }

        public string checkRole(LoginVM loginVM)
        {
            var checkEmp = myContext.Employee.SingleOrDefault(e => e.Email == loginVM.Email);
            if( checkEmp != null)
            {
                var checkAcr = myContext.AccountRole.SingleOrDefault(a => a.NIK == checkEmp.NIK);
                if(checkAcr.Role_Id == 1)
                {
                    string a = "Director";
                    return a;
                }
                else if(checkAcr.Role_Id == 2)
                {
                    string a = "Manager";
                    return a;
                }
                else if( checkAcr.Role_Id == 3)
                {
                    string a = "Employee";
                    return a;
                }
                return null;
            }
            return null;
        } 


        // Mengenerate nilai NIK
        public string GenerateNIK()
        {
            var yearPresent = DateTime.Now.Year.ToString();
            var maxNik = myContext.Employee.ToList().Max(e => e.NIK); //Mengambil NIK paling akhir/tinggi

            if(maxNik == null)
            {
                return yearPresent + "001";
            }
            else
            {
                int incrementNIK = Convert.ToInt32(maxNik) + 1;
                return incrementNIK.ToString();
            }
        }



        // Mengambil nilai email dari database
        public Employee GetEmail(string email)
        {
            return myContext.Employee.FirstOrDefault(e => e.Email == email);
        }
        // Mengambil nilai phone dari database
        public Employee GetPhone(string phone)
        {
            return myContext.Employee.FirstOrDefault(e =>e.Phone == phone);
        }

        private string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
        

    }
}
