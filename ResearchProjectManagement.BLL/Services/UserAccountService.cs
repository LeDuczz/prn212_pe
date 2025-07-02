using ResearchProjectManagement.DAL.Entities;
using ResearchProjectManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchProjectManagement.BLL.Services
{
    public class UserAccountService
    {
        private readonly UserAccountRepository _userAccountRepository;

        public UserAccountService()
        {
            _userAccountRepository = new UserAccountRepository();
        }
        public UserAccount? GetUserAccount(string email, string password)
        {
            return _userAccountRepository.GetUserAccount(email, password);
        }
    }
}
