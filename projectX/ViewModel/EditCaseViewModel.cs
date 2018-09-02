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
using projectX.services;
namespace projectX.ViewModel
{
    public class EditCaseViewModel : INotifyPropertyChanged
    {
        private Case _originalCase;
        private Case _cloneCase;
        private bool _wasChange;

        private readonly ObservableCollection<Case> _cases;
        private readonly IDialogService _dialogService;

        private string _selectedMark;
        private string _selectedImg;
         
        public EditCaseViewModel(ObservableCollection<Case> cases, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _cases = cases;

            _selectedMark = null;
            _selectedImg = null;

            _wasChange = false;
            _originalCase = new Case();
            _cloneCase = (Case)_originalCase.Clone(); 
        }

        #region prop
        
        public ObservableCollection<string> ImgSrc
        {
            get => CloneCase.ImgSrc;
            set
            {
                CloneCase.ImgSrc = value;
                _wasChange = true;
            } 
        }

        public Case TargetCase
        {
            get => _originalCase;
            set
            {
                if (_originalCase == value || value == null) return;
                _originalCase = value;
                CloneCase = (Case)value.Clone();
                OnPropertyChanged(nameof(TargetCase));
            }
        }

        public Case CloneCase
        {
            get => _cloneCase;
            set
            {
                if(_cloneCase == value) return;
                _cloneCase = value;
                OnPropertyChanged(nameof(CloneCase));
            }
        }

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
                           CloneCase.Marks.Add(SelectedMark);
                           SelectedMark = null;
                           _wasChange = true;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && SelectedMark != " " && !CloneCase.Marks.Contains(SelectedMark))
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
                           if (!CloneCase.Marks.Contains(SelectedMark)) return;
                           CloneCase.Marks.Remove(SelectedMark);
                           SelectedMark = null;
                           _wasChange = true;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && CloneCase.Marks.Contains(SelectedMark))
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
                           CloneCase.ImgSrc.Add(_dialogService.FilePath);
                           _wasChange = true;
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
                           CloneCase.ImgSrc.Remove(SelectedImg);
                           SelectedImg = null;
                           _wasChange = true;
                       }, obj => CloneCase.ImgSrc.Count > 0 && SelectedImg != null));
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
                               var idOriginalCase = _cases.IndexOf(_originalCase);
                               _cases[idOriginalCase] = CloneCase;
                               _wasChange = false;
                           }, x=> _wasChange)
                       );
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
