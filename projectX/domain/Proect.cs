using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectX.domain
{
    public class Proect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Case> Cases { get; set; }

        public Proect()
        {
            Cases = new List<Case>();
        }
    }
}
