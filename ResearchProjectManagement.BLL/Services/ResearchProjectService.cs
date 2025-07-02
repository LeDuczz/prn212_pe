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
    public class ResearchProjectService
    {
        private readonly ResearchProjectRepository _researchProjectRepository;

        public ResearchProjectService()
        {
            _researchProjectRepository = new ResearchProjectRepository();
        }

        public void CreateResearchProject(ResearchProject researchProject)
        {
            if (researchProject.StartDate >= researchProject.EndDate)
            {
                throw new ArgumentException("StartDate must be earlier than EndDate.");
            }

            if (researchProject.ProjectTitle.Length < 5 || researchProject.ProjectTitle.Length > 100)
            {
                throw new ArgumentException("ProjectTitle must be between 5 and 100 characters.");
            }

            _researchProjectRepository.CreateResearchProject(researchProject);
        }

        public void DeleteResearchProject(ResearchProject researchProject)
        {
            _researchProjectRepository.DeleteResearchProject(researchProject);
        }

        public List<ResearchProject> GetResearchProjects()
        {
            return _researchProjectRepository.GetResearchProjects();
        }

        public List<ResearchProject> GetResearchProjects(string searchText)
        {
            return _researchProjectRepository.GetResearchProjects(searchText);
        }

        public void UpdateResearchService(ResearchProject researchProject)
        {
            if (researchProject.StartDate >= researchProject.EndDate)
            {
                throw new ArgumentException("StartDate must be earlier than EndDate.");
            }

            if (researchProject.ProjectTitle.Length < 5 || researchProject.ProjectTitle.Length > 100)
            {
                throw new ArgumentException("ProjectTitle must be between 5 and 100 characters.");
            }

            _researchProjectRepository.UpdateResearchProject(researchProject);
        }
    }
}
