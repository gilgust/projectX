using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectX.domain
{
    public class CaseResult
    {
        public int Id { get; set; }
        public Case Case { get; set; }
        public Proect Proect { get; set; }
        public string Coment { get; set; }
        public string Condition { get; set; }
    }
}
