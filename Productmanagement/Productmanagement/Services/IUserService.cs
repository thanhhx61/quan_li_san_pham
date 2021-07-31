using Productmanagement.Entities;
using Productmanagement.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productmanagement.Services
{
    public interface IUserService
    {
        Task<LoginResult> Login(Login LoginUser);

        void Signout();
    }
}
