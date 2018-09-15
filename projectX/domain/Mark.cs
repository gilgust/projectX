using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectX.domain
{
    public class Mark
    {

        public int Id { get; set; }
        public string Text { get; set; }

        public List<Case> Cases { get; set; }
        public List<Proect> Proects { get; set; }
    }
}
