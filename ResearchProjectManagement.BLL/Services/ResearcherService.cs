using ResearchProjectManagement.DAL.Entities;
using ResearchProjectManagement.DAL.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchProjectManagement.BLL.Services
{
    public class ResearcherService
    {
        private readonly ResearcherRepository _researcherRepository;

        public ResearcherService()
        {
            _researcherRepository = new ResearcherRepository();
        }
        public List<Researcher> GetResearchers()
        {
            return _researcherRepository.GetResearchers();
        }
    }
}
