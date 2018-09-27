using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using projectX.Annotations;
using projectX.domain;
using projectX.Data;
using projectX.Data.interfaces;
using projectX.Views.proect;

namespace projectX.ViewModel.proectVM
{
    public class ProectsViewModel: INotifyPropertyChanged
    {
        private readonly IProectCrud _proectProvider;

        private ObservableCollection<Proect> _proects;
        private Proect _selectedProect;

        private UserControl _currentView;
        private readonly UserControl _proectView;
        private readonly UserControl _editProectView;
        private readonly UserControl _createProectView;
        

        public ProectsViewModel()
        {
            _proectProvider = new ProectProvider();

            _proectView = new ProectView();
            _editProectView = new EditProectView();
            _createProectView = new CreateProectView{DataContext = new CreateProectViewModel()};
            ((CreateProectViewModel)_createProectView.DataContext).AddedItem += ChangeTargetAfterAddedProect;

            _currentView = null;
        }

        #region prop  

        public ObservableCollection<Proect> Proects
        {
            get
            {
                if (_proects == null)
                {
                    Task.Run(() => { Proects = _proectProvider.Proects; }); 
                }

                return _proects;
            }
            private set
            {
                _proects = value;
                OnPropertyChanged(nameof(Proects));
            }
        }

        public Proect SelectedProect
        {
            get => _selectedProect;
            set
            {
                if (_selectedProect == value) return;
                _selectedProect = value;
                
                _proectView.DataContext = new ProectViewModel(value);
                
                OnPropertyChanged(nameof(SelectedProect));
                CurrentView = _proectView;
            }
        }

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                if (ReferenceEquals(_currentView, value)) return;
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        #endregion

        #region commands


        //show EditProect
        private RelayCommand _editProectCommand;
        public RelayCommand EditProectCommand
        {
            get
            {
                return _editProectCommand ??
                       (_editProectCommand = new RelayCommand(obj =>
                           {
                               if (_editProectView.DataContext != null)
                                   ((EditProectViewModel) _editProectView.DataContext).EditItem -= GetChangeProect;

                               _editProectView.DataContext = new EditProectViewModel(SelectedProect);
                               ((EditProectViewModel)_editProectView.DataContext).EditItem += GetChangeProect;

                               CurrentView = _editProectView;
                       },
                           obj => SelectedProect != null));
            }
        }

        private RelayCommand _createProectCommand;
        public RelayCommand CreateProectCommand
        {
            get
            {
                return _createProectCommand ??
                       (_createProectCommand = new RelayCommand(obj =>
                       { 
                           CurrentView = _createProectView;
                       }));
            }
        }

        private RelayCommand _deleteProectCommand;
        public RelayCommand DeleteProecCommand
        {
            get
            {
                return _deleteProectCommand ??
                       (_deleteProectCommand = new RelayCommand(obj =>
                       {
                           _proectProvider.RemoveProect(SelectedProect);
                           Proects.Remove(SelectedProect);
                           CurrentView = null;
                       },
                           obj => SelectedProect != null));
            }
        }
        #endregion

        private void GetChangeProect(int id)
        {
            var target = Proects.First(c => c.Id == id);
            target = _proectProvider.GetProectById(id);

            SelectedProect = target;
        }

        private void ChangeTargetAfterAddedProect(int id)
        {
            SelectedProect = Proects.First(p => p.Id == id);
        }

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
