using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectX.domain
{
    public class Img
    {
        public Img()
        { }

        public int Id { get; set; }
        public string src { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }
    }
}
