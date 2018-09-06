using System;
using System.Collections.Generic;
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
        private Proect _selectedProect;
        private UserControl _currentView;
        private UserControl _proectView;
        private readonly UserControl _editProectView;
        private readonly UserControl _createProectView;

        //public ProectsViewModel()
        //{
            
        //}

        public ProectsViewModel()
        {
            Proects = DataFromCollections.Instance;
            _proectView = null;
            _editProectView = new EditProectView();
            _createProectView = new CreateProectView{DataContext = new CreateProectViewModel()};

            _currentView = null;
        } 

        #region prop 
        public IProectCrud Proects { get; }

        public Proect SelectedProect
        {
            get => _selectedProect;
            set
            {
                if (_selectedProect == value) return;
                _selectedProect = value;

                if (_proectView == null)
                    _proectView = new ProectView{ DataContext = new ProectViewModel()};
                ((ProectViewModel)_proectView.DataContext).Proect = value;
                
                OnPropertyChanged(nameof(SelectedProect));
                ShowProectInfoCommand.Execute(null);
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
        //show ProectInfo
        private RelayCommand _showProectInfoCommand;
        public RelayCommand ShowProectInfoCommand
        {
            get
            {
                return _showProectInfoCommand ??
                       (_showProectInfoCommand = new RelayCommand(obj =>
                       {
                           if (SelectedProect != null)
                               CurrentView = _proectView;
                       }));
            }
        }

        //show CaseInfo
        private RelayCommand _editProectCommand;
        public RelayCommand EditProectCommand
        {
            get
            {
                return _editProectCommand ??
                       (_editProectCommand = new RelayCommand(obj =>
                       {
                           _editProectView.DataContext = new EditProectViewModel(SelectedProect);

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
                           SelectedProect = null;
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
                           Proects.RemoveProect(SelectedProect);
                           CurrentView = null;
                       },
                           obj => SelectedProect != null));
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
