using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraCloneBackend.Models
{
    public class Issue
    {
        public int IssueId { get; set; } 
       
        public string Summary { get; set; }
        public string Description { get; set; }
        
        
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int ReporterId { get; set; }
        public User Reporter { get; set; }
        
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Due { get; set; }

    }
}
