using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SQLitePCL;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _Context;

        public AccountController(DataContext dataContext)
        {
            this._Context = dataContext;
        }
        [HttpPost("register")] // Api/Account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {

            using var hmac = new HMACSHA512();

            if (await IsUserExists(registerDto.UserName))
                return BadRequest("User Exists");
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key
            };
            _Context.Users.Add(user);
            await _Context.SaveChangesAsync();
            return user;
        }
        private async Task<bool> IsUserExists(string username)
        {
            return await _Context.Users.AnyAsync(x => x.UserName == username.ToLower());

        }
        [HttpPost("login")]
        public async Task<ActionResult<AppUser>> Login(LoginDto loginDto)
        {
            var user = await _Context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.username);
            if (user == null) return Unauthorized("User not exist.");
            using var hmac=new HMACSHA512(user.PasswordSalt);
            var computeHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
            for (int i = 0; i < computeHash.Length; i++)
            {
                if(computeHash[i]!=user.PasswordHash[i]) return Unauthorized("Incorrect password.");
            }
            return user;
        }
    }
}