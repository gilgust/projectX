using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using projectX.Annotations;

namespace projectX.domain
{
    public class Proect : INotifyPropertyChanged, ICloneable
    {
        private string _name;
        private string _description;

        public Proect()
        {
            //Id = Guid.NewGuid().GetHashCode();
            //Cases = new ObservableCollection<Case>();
            Marks = new ObservableCollection<string>();
        }

        #region prop
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if(_description == value) return;
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        //public ObservableCollection<Case> Cases { get; set; }
        public ObservableCollection<string> Marks { get; set; }


        #endregion

        #region notify     
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
