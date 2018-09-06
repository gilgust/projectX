using System;
using System.Collections.Generic;
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

namespace projectX.ViewModel.proectVM
{
    public class CreateProectViewModel : INotifyPropertyChanged
    {
        private Proect _proect;
        private readonly IProectCrud _proects; 
        private string _selectedMark;

        //public CreateProectViewModel()
        //{

        //}

        public CreateProectViewModel()
        {
            _proects = DataFromCollections.Instance;

            _selectedMark = null;

            _proect = new Proect { Name = "", Description = "" };
        }

        #region prop

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
                           NewProect.Marks.Add(SelectedMark);
                           SelectedMark = null;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && SelectedMark != " " && !_proect.Marks.Contains(SelectedMark))
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
                           if (!NewProect.Marks.Contains(SelectedMark)) return;
                           NewProect.Marks.Remove(SelectedMark);
                           SelectedMark = null;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && _proect.Marks.Contains(SelectedMark))
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
                           _proects.AddProect(_proect);
                           NewProect = new Proect { Name = "", Description = "" };
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
