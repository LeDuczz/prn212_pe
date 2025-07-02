using ResearchProjectManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchProjectManagement.DAL.Repositories
{
    public class UserAccountRepository
    {
        private readonly Su25researchDbContext _db;

        public UserAccountRepository()
        {
            _db = new Su25researchDbContext();
        }
        public UserAccount? GetUserAccount(string email, string password)
        {
            return _db.UserAccounts.FirstOrDefault(a => a.Email == email && a.Password == password);
        }
    }
}
