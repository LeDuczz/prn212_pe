using ResearchProjectManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchProjectManagement.DAL.Repositories
{
    public class ResearcherRepository
    {
        private readonly Su25researchDbContext _db;
        public ResearcherRepository()
        {
            _db = new Su25researchDbContext();
        }
        public List<Researcher> GetResearchers()
        {
            return _db.Researchers.ToList();
        }
    }
}
