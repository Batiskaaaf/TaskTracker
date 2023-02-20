using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Model.View;

namespace TaskTracker.Data.Repository.IRepository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpViewModel signUpViewModel);

        Task<string> LoginAsync(SignInViewModel signInViewModel);
    }
}
