using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using projectX.Annotations;
using projectX.domain;

namespace projectX.ViewModel.proectVM
{
    class ProectViewModel : INotifyPropertyChanged
    {
        private Proect _proect;

        #region properties
        public Proect Proect
        {
            get => _proect;
            set
            {
                if (_proect == value) return;

                _proect = value;
                OnPropertyChanged(nameof(Proect));
            }
        }
        
        private string _selectedMark;
        public string SelectedMark
        {
            get => _selectedMark;
            set
            {
                if (_selectedMark == value) return;
                _selectedMark = value;
                OnPropertyChanged(nameof(SelectedMark));
            }
        }

        #endregion


        #region prop
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
