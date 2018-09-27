using projectX.domain;
using System.Collections.ObjectModel;

namespace projectX.Data.interfaces
{
    public interface IProectCrud
    {
        ObservableCollection<Proect> Proects{ get; set; }
        Proect AddProect(Proect newProect);
        void RemoveProect(Proect remProect);
        void EditProect(Proect newProect);
        Proect GetProectById(int id);
    }
}
