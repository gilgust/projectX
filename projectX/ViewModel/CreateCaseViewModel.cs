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
    public class CreateCaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private Case _case;
        private readonly ObservableCollection<Case> _cases;
        private readonly IDialogService _dialogService;
        private string _selectedMark;
        private string _selectedImg;

        public CreateCaseViewModel(ObservableCollection<Case> cases, IDialogService dialogService)
        { 
            _selectedMark = null;
            _selectedImg = null;

            _dialogService = dialogService;
            _cases = cases;

            _case = new Case {Name = string.Empty, Description = string.Empty};

            _case.PropertyChanged += Case_propertyChanged;
        }

        private void Case_propertyChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);
        public void Dispose() => _case.PropertyChanged -= Case_propertyChanged;

        #region prop

        public Case NewCase
        {
            get => _case;
            set
            {
                if (_case == value) return;
                _case = value;
                OnPropertyChanged(nameof(NewCase));
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
                               NewCase.Marks.Add(SelectedMark);
                               SelectedMark = null;
                           },obj =>  !string.IsNullOrEmpty(SelectedMark) && SelectedMark != " "  && !_case.Marks.Contains(SelectedMark))
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
                               if (!NewCase.Marks.Contains(SelectedMark)) return;
                               NewCase.Marks.Remove(SelectedMark);
                               SelectedMark = null;
                           },obj => !string.IsNullOrEmpty(SelectedMark) && _case.Marks.Contains(SelectedMark))
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
                           NewCase.ImgSrc.Add(@"resources/Chrysanthemum.jpg");
                           NewCase.ImgSrc.Add(@"G:/nuw4Tp80cqs.jpg");
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
                               NewCase.ImgSrc.Remove(SelectedImg);
                               SelectedImg = null;
                           }, obj => !string.IsNullOrEmpty(SelectedImg))
                       );
            }
        }

        private RelayCommand _saveCaseCommand;
        public RelayCommand SaveCaseCommnad
        {
            get
            {
                return _saveCaseCommand ??
                       (_saveCaseCommand = new RelayCommand(obj =>
                           {
                               _cases.Add(_case);
                               NewCase = new Case { Name = string.Empty, Description = string.Empty };
                           },(obj) => NewCase.Name != String.Empty && NewCase.Description != String.Empty)
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
