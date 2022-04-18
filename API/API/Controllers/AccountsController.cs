using API.Base;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this.configuration = configuration;
        }
        
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (accountRepository.GetPhone(registerVM.PhoneNumber) != null)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "DATA TIDAK BERHASIL DITAMBAHKAN, PHONE SUDAH TERPAKAI" });
            }
            
            if(accountRepository.GetEmail(registerVM.Email) != null)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "DATA TIDAK BERHASIL DITAMBAHKAN, EMAIL SUDAH TERPAKAI" });
            }
            
            var result = accountRepository.Register(registerVM);
            return Ok(new { status = HttpStatusCode.OK, Result = result, Message = "DATA BERHASIL DITAMBAHKAN" });

        }


        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var checkLogin = accountRepository.Login(loginVM);
            if(checkLogin == 1)
            {
                var cekRole = accountRepository.checkRole(loginVM);
                var claims = new List<Claim>();
                claims.Add(new Claim("Email", loginVM.Email));
                claims.Add(new Claim("roles", cekRole));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow,
                    signingCredentials: signIn
                    );
                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                return Ok(new { status = HttpStatusCode.OK, idtoken, message = "Berhasil Login" });
            }

            return BadRequest(new { status = HttpStatusCode.BadRequest, message = "Gagal Login" });

            //var result = accountRepository.Login(loginVM);
            //if (result == 1)
            //{
            //    return Ok(new { status = HttpStatusCode.OK,Message = "BERHASIL LOGIN" });
            //}
            //else if(result == 2)
            //{
            //    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "GAGAL LOGIN, PASSWORD SALAH" });
            //}
            //else if (result == 3)
            //{
            //    return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "GAGAL LOGIN, EMAIL TIDAK DITEMUKAN" });
            //}
            //else
            //{
            //    return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "ADA KESALAHAN" });
            //}
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            try
            {
                var result = accountRepository.ForgotPassword(forgotPasswordVM);
                return Ok(new { status = HttpStatusCode.OK, Message = "EMAIl SEND" });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            var result = accountRepository.ChangePassword(changePasswordVM);
            if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, Message = "BERHASIL UBAH PASSWORD" });
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "GAGAL UBAH PASSWORD, NEWPASSWORD HARUS SAMA DENGAN CONFIRMPASSWORD" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, Message = "GAGAL UBAH PASSWORD, EMAIL TIDAK DITEMUKAN" });
            }
            else
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "ADA KESALAHAN" });
            }
        }
    }
}
