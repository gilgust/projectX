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
        private ObservableCollection<Mark> _marks;
        private ObservableCollection<projectX.domain.Img> _imgSrc;

        private bool _wasChange;

        public delegate void EditCaseHandler(Case editCase);
        public event EditCaseHandler EditItem;

        private readonly ICaseCrud _data;
        private readonly IDialogService _dialogService; 

        private string _newMark;

//ctor
        public EditCaseViewModel(Case orCase) 
        {
            _dialogService = new DefaultDialogService();
            _data = new CasesProvider();  

            _wasChange = false;

            _originalCase = orCase;
            _cloneCase = (Case)orCase.Clone();
            _marks = new ObservableCollection<Mark>(_cloneCase.Marks);
            _imgSrc = new ObservableCollection<projectX.domain.Img>(_cloneCase.ImgSrc);


            _cloneCase.PropertyChanged += Case_PropertyChanged;

            NewMark = "";
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

        public ObservableCollection<Mark> Marks
        {
            get => _marks;
            set
            {
                _marks = value;
                _wasChange = true;
            }
        }

        public ObservableCollection<projectX.domain.Img> ImgSrc
        {
            get => _imgSrc;
            set
            {
                _imgSrc = value;
                _wasChange = true;
            }
        }
        #endregion 

        public string NewMark
        {
            get => _newMark;
            set
            {
                _newMark = value;
                OnPropertyChanged(nameof(NewMark));
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
                           Marks.Add(new Mark { Text = NewMark });
                           _cloneCase.Marks.Add(new Mark { Text = NewMark });
                           NewMark = "";
                           _wasChange = true;
                       }, obj => !String.IsNullOrWhiteSpace(NewMark)));
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
                           Marks.Remove((Mark) obj);
                           _cloneCase.Marks.Remove((Mark)obj);

                           _wasChange = true;
                       }));
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
                           if (string.IsNullOrWhiteSpace(_dialogService.FilePath)) return;

                           ImgSrc.Add(new Img{ src = _dialogService.FilePath });
                           _cloneCase.ImgSrc.Add(new Img{src = _dialogService.FilePath });
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
                           ImgSrc.Remove((projectX.domain.Img) obj);
                           _cloneCase.ImgSrc.Remove((projectX.domain.Img)obj);
                           _wasChange = true;
                       })); 
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
                           
                               EditItem?.Invoke(_cloneCase);

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