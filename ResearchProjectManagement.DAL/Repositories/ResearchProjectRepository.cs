using Microsoft.EntityFrameworkCore;
using ResearchProjectManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchProjectManagement.DAL.Repositories
{
    public class ResearchProjectRepository
    {
        private readonly Su25researchDbContext _db;
        public ResearchProjectRepository()
        {
            _db = new Su25researchDbContext();
        }

        public void CreateResearchProject(ResearchProject researchProject)
        {
            researchProject.ProjectId = _db.ResearchProjects.Max(x => x.ProjectId) + 1;
            _db.ResearchProjects.Add(researchProject);
            _db.SaveChanges();
        }

        public void DeleteResearchProject(ResearchProject researchProject)
        {
            _db.ResearchProjects.Remove(researchProject);
            _db.SaveChanges();
        }

        public List<ResearchProject> GetResearchProjects()
        {
            return _db.ResearchProjects.Include(e => e.LeadResearcher).ToList();
        }

        public List<ResearchProject> GetResearchProjects(string searchText)
        {
            return _db.ResearchProjects.Where(e => e.ProjectTitle.ToLower().Contains(searchText) || e.ResearchField.ToLower().Contains(searchText)).ToList();
        }

        public void UpdateResearchProject(ResearchProject researchProject)
        {
            var researchProjectFromDB = _db.ResearchProjects.Find(researchProject.ProjectId);
            if(researchProjectFromDB != null)
            {
                researchProjectFromDB.ProjectTitle = researchProject.ProjectTitle;
                researchProjectFromDB.ResearchField = researchProject.ResearchField;
                researchProjectFromDB.StartDate = researchProject.StartDate;
                researchProjectFromDB.EndDate = researchProject.EndDate;
                researchProjectFromDB.LeadResearcherId = researchProject.LeadResearcherId;
                researchProjectFromDB.Budget = researchProject.Budget;
                _db.ResearchProjects.Update(researchProjectFromDB);
                _db.SaveChanges();
            }
        }
    }
}
