using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraCloneBackend.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        
        public List<Issue> Issues { get; set; }

        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
