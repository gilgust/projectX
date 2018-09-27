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
        private readonly Case _case; 
        public CaseViewModel(){}

        public CaseViewModel(Case c)
        {
            _case = c;
            if(c != null)
                _case.PropertyChanged += Case_PropertyChanged; 
        }

        public void Dispose()
        {
            if (_case != null)
                _case.PropertyChanged -= Case_PropertyChanged;
        }

        private void Case_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged(e.PropertyName);

        #region properties

        public int Id => _case.Id;
        public string Name => _case.Name;
        public string Description => _case.Description;
        public List<Mark> Marks => _case.Marks;
        public List<projectX.domain.Img> ImgSrc => _case.ImgSrc;


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
