using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using projectX.Annotations;
using projectX.domain;

namespace projectX.ViewModel
{
    public class CaseViewModel : INotifyPropertyChanged
    {
        private Case _case; 

        #region properties
        public Case Case
        {
            get => _case;
            set
            { 
                if (_case == value) return;

                _case = value; 
                OnPropertyChanged(nameof(Case));
            }
        }



        private string _selectedMark; 
        public string SelectedMark
        {
            get => _selectedMark ;
            set
            {
                if (_selectedMark == value) return;
                _selectedMark = value;
                OnPropertyChanged(nameof(SelectedMark));
            }
        }

        private string _selectedImg; 
        public string SelectedImg
        {
            get => _selectedImg ;
            set
            {
                if (_selectedImg == value) return;
                _selectedImg = value;
                OnPropertyChanged(nameof(SelectedImg));
            }
        }
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
