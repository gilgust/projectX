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
        private readonly ICaseCrud _caseProvider;
        private readonly IMarkCrud _markProvider;

        private readonly IDialogService _dialogService; 

        public delegate void AddedItemHandler(int Id); 
        public event AddedItemHandler AddedItem;

        //ctor
        public CreateCaseViewModel()
        {
            _caseProvider = new CasesProvider(); 
            _markProvider = new MarkProvider();

            _dialogService = new DefaultDialogService();

            NewCase = new Case {Name = "", Description = ""};
            Marks = new ObservableCollection<Mark>(_case.Marks);
            Imgs = new ObservableCollection<projectX.domain.Img>(_case.ImgSrc);
            NewMark = "";
        }

        #region prop

        private Case _case;
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

        private ObservableCollection<Mark> _marks;
        public ObservableCollection<Mark> Marks
        {
            get => _marks;
            set
            {
                if(ReferenceEquals(_marks, value))return;
                _marks = value;
                OnPropertyChanged(nameof(Marks));
            }
        }

        private ObservableCollection<projectX.domain.Img> _img;
        public ObservableCollection<projectX.domain.Img> Imgs
        {
            get => _img;
            set
            {
                _img = value;
                OnPropertyChanged(nameof(Imgs));
            }
        }

        private string _newMark; 
        public string NewMark
        {
            get => _newMark;
            set
            {
                if(_newMark == value) return;
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
                               var mark = _markProvider.AddMark(NewMark);
                               Marks.Add(mark);
                               NewCase.Marks.Add(mark);
                               NewMark = "";
                           }, obj => !string.IsNullOrWhiteSpace(NewMark))
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
                               NewCase.Marks.Remove((Mark) obj);
                               Marks.Remove((Mark) obj);
                           })
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
                           NewCase.ImgSrc.Add(new Img { src =_dialogService.FilePath });
                           Imgs.Add(new projectX.domain.Img{src = _dialogService.FilePath });
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
                           NewCase.ImgSrc.Remove((projectX.domain.Img) obj);
                           Imgs.Add((projectX.domain.Img)obj);
                       }));
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
                               var c = _caseProvider.AddCase(_case);
                               AddedItem?.Invoke(c.Id);
                               NewCase = new Case {Name = "", Description = ""}; 
                               Marks = new ObservableCollection<Mark>(NewCase.Marks);
                               Imgs = new ObservableCollection<Img>(NewCase.ImgSrc);
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
