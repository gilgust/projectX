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
using projectX.Data;

namespace projectX.ViewModel
{
    public class CaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private Case _case;
        private ApplicationContext db;

        public CaseViewModel(){}

        public CaseViewModel(Case c = null)
        {
            if (c != null)
            {
                _case = c;
                _case.PropertyChanged += Case_PropertyChanged; 
            }
        }

        public void Dispose() => _case.PropertyChanged -= Case_PropertyChanged;

        private void Case_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged(e.PropertyName);

        #region properties
        public Case Case
        {
            get => _case;
            set
            {
                if (_case == value) return;
                if (value == null)
                {
                    Dispose(); 
                }
                else
                {
                    _case = value;
                    _case.PropertyChanged += Case_PropertyChanged;
                    Marks = _case.Marks;
                    ImgSrc = _case.ImgSrc;
                    OnPropertyChanged(nameof(Case));
                }
            }
        }


        public int Id => _case.Id;

        public string Name => _case.Name;

        public string Description => _case.Description; 

        public List<Mark> Marks {
            get => _case.Marks;
            set { _case.Marks = value;
                OnPropertyChanged(nameof(Marks)); }
        }

        public List<projectX.domain.Img> ImgSrc
        {
            get => _case.ImgSrc;
            set
            {
                _case.ImgSrc = value;
                OnPropertyChanged(nameof(ImgSrc));
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

        #region notify
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
