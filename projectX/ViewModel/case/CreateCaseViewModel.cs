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
    public class CreateCaseViewModel : INotifyPropertyChanged
    {
        private Case _case;
        private  readonly ICaseCrud _cases;
        private readonly IDialogService _dialogService;
        private string _selectedMark;
        private string _selectedImg;

        public CreateCaseViewModel()
        {
            _cases = DataFromCollections.Instance;

            _selectedMark = null;
            _selectedImg = null;

            _dialogService = new DefaultDialogService();
            
            _case = new Case {Name = "", Description = ""};
        }

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
                if(_selectedImg == value) return;
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
                               NewCase.Marks.Add(SelectedMark);
                               SelectedMark = null;
                           }, obj => !string.IsNullOrEmpty(SelectedMark) && SelectedMark != " " && !_case.Marks.Contains(SelectedMark))
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
                           }, obj => !string.IsNullOrEmpty(SelectedMark) && _case.Marks.Contains(SelectedMark))
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
                           NewCase.ImgSrc.Add(_dialogService.FilePath);
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
                       }, obj => NewCase.ImgSrc.Count >0 && SelectedImg != null));
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
                               _cases.AddCase(_case);
                               NewCase = new Case {Name = "", Description = ""}; 
                           })
                       );
            }
        }

        #endregion

        #region propertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion 
    }
}
