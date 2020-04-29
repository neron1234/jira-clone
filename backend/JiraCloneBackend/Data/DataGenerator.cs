using JiraCloneBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraCloneBackend.Data
{
    public class DataGenerator
    {
        private readonly JiraContext _context;

        

        public DataGenerator(JiraContext context)
        {
            _context = context;
        }

        public void seedData()
        {

        }
    }
}
