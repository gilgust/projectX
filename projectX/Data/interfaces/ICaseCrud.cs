using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;

namespace projectX.Data.interfaces
{
    public interface ICaseCrud
    {
        ObservableCollection<Case> Cases { get; set; }
        void AddCase(Case newCase);
        void RemoveCace(Case remCase);
        void EditCase(Case newCase);
    }
}
