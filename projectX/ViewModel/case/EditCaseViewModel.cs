using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using projectX.Annotations;
using projectX.domain;
using projectX.Data;
using projectX.Data.interfaces;
using projectX.services;
namespace projectX.ViewModel
{
    public class EditCaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private Case _originalCase;
        private readonly Case _cloneCase;
        private bool _wasChange;

        private readonly ICaseCrud _data;
        private readonly IDialogService _dialogService;

        private string _selectedMark;
        private string _selectedImg;
         
        public EditCaseViewModel(Case orCase) 
        {
            _dialogService = new DefaultDialogService();
            _data = DataFromCollections.Instance;

            _selectedMark = null;
            _selectedImg = null;

            _wasChange = false;

            _originalCase = orCase;
            _cloneCase = (Case)orCase.Clone();

            _cloneCase.PropertyChanged += Case_PropertyChanged;
        }

        public void Dispose() => _cloneCase.PropertyChanged -= Case_PropertyChanged;
        private void Case_PropertyChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

#region prop  
        #region ClonProperty 
        public string Name
        {
            get => _cloneCase.Name;
            set => _cloneCase.Name = value;
        }
        public string Description
        {
            get => _cloneCase.Description;
            set => _cloneCase.Description = value;
        }

        public ObservableCollection<string> Marks
        {
            get => _cloneCase.Marks;
            set
            {
                _cloneCase.Marks = value;
                _wasChange = true;
            }
        }

        public ObservableCollection<string> ImgSrc
        {
            get => _cloneCase.ImgSrc;
            set
            {
                _cloneCase.ImgSrc = value;
                _wasChange = true;
            }
        }
        #endregion


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

        

        public string SelectedImg
        {
            get => _selectedImg;
            set
            {
                if (_selectedImg == value) return;
                _selectedImg = value;
                OnPropertyChanged(nameof(SelectedImg));
            }
        }

#endregion

#region command
//manipulation with marks
        private RelayCommand _addMarkCommnad;
        public RelayCommand AddMarkCommnad
        {
            get
            {
                return _addMarkCommnad ??
                       (_addMarkCommnad = new RelayCommand(obj =>
                       {
                           _cloneCase.Marks.Add(SelectedMark);
                           SelectedMark = null;
                           _wasChange = true;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && SelectedMark != " " && !_cloneCase.Marks.Contains(SelectedMark))
                       );
            }
        }

        private RelayCommand _deleteMarkCommnad;
        public RelayCommand DeleteMarkCommnad
        {
            get
            {
                return _deleteMarkCommnad ??
                       (_deleteMarkCommnad = new RelayCommand(obj =>
                       {
                           if (!_cloneCase.Marks.Contains(SelectedMark)) return;
                           _cloneCase.Marks.Remove(SelectedMark);
                           SelectedMark = null;
                           _wasChange = true;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && _cloneCase.Marks.Contains(SelectedMark))
                       );
            }
        }


//manipulation with img
        private RelayCommand _addImgCommand;
        public RelayCommand AddImgCommand
        {
            get
            {
                return _addImgCommand ??
                       (_addImgCommand = new RelayCommand(obj =>
                       {
                           _dialogService.OpenFileDialog();
                           if (_dialogService.FilePath != null && _dialogService.FilePath != " ")
                           {
                               _cloneCase.ImgSrc.Add(_dialogService.FilePath);
                               _wasChange = true;
                           }
                       }));
            }
        }

        private RelayCommand _deleteImgCommnad;
        public RelayCommand DeleteImgCommnad
        {
            get
            {
                return _deleteImgCommnad ??
                       (_deleteImgCommnad = new RelayCommand(obj =>
                       {
                           _cloneCase.ImgSrc.Remove(SelectedImg);
                           SelectedImg = null;
                           _wasChange = true;
                       }, obj =>  SelectedImg != null)); //CloneCase.ImgSrc.Count > 0 &&
            }
        }

//save
        private RelayCommand _saveCaseCommand;
        public RelayCommand SaveCaseCommnad
        {
            get
            {
                return _saveCaseCommand ??
                       (_saveCaseCommand = new RelayCommand(obj =>
                           {
                               _data.EditCase(_cloneCase);
                               _originalCase = (Case) _cloneCase.Clone();
                               _wasChange = false;
                           }, x=> ReadeToSave())
                       );
            }
        }

#endregion

        private bool ReadeToSave()
        {
            return _wasChange||( _originalCase.Name != _cloneCase.Name || _originalCase.Description != _cloneCase.Description);
        }

#region notifyprop
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
#endregion
    }
}
