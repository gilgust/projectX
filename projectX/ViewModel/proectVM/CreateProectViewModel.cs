using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using projectX.Annotations;
using projectX.domain;
using projectX.Data;
using projectX.Data.interfaces;
using projectX.services;

namespace projectX.ViewModel.proectVM
{
    public class CreateProectViewModel : INotifyPropertyChanged
    {
        private readonly IProectCrud _proectsProvider;
        private readonly IMarkCrud _markProvider;

        public delegate void AddedItemHandler(int Id);
        public event AddedItemHandler AddedItem;

        public CreateProectViewModel()
        { 
            _proectsProvider = new ProectProvider();
            _markProvider = new MarkProvider();

            _proect = new Proect { Name = "", Description = "" };
            Marks = new ObservableCollection<Mark>(_proect.Marks);
            NewMark = "";
        }

        #region prop

        private Proect _proect;
        public Proect NewProect
        {
            get => _proect;
            set
            {
                if (_proect == value) return;
                _proect = value;
                OnPropertyChanged(nameof(NewProect));
            }
        }

        private ObservableCollection<Mark> _marks; 
        public ObservableCollection<Mark> Marks
        {
            get => _marks;
            set
            {
                if(ReferenceEquals(_marks, value)) return;
                _marks = value;
                OnPropertyChanged(nameof(Marks));
            }
        }

        private string _newMark;
        public string NewMark
        {
            get => _newMark;
            set
            {
                if (_newMark == value) return;
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
                               NewProect.Marks.Add(mark);
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
                               NewProect.Marks.Remove((Mark) obj);
                               Marks.Remove((Mark) obj);
                           })
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
                               var p = _proectsProvider.AddProect(_proect);
                               AddedItem?.Invoke(p.Id);
                               NewProect = new Proect { Name = "", Description = "" };
                               Marks = new ObservableCollection<Mark>(NewProect.Marks);
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
