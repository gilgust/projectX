using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;

namespace projectX.Data.interfaces
{
    interface IMarkCrud
    {
        List<Mark> Marks { get; }
        Mark AddMark(string newMark); 
    }
}
