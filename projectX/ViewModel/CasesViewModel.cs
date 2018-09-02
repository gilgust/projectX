using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using projectX.Annotations;
using projectX.domain;
using projectX.Views;

namespace projectX.ViewModel
{
    class CasesViewModel : INotifyPropertyChanged
    {
        private Case _selectedCase;
        private object _currentView;
        private readonly object _caseInfoView;
        private readonly object _editCaseView;
        private readonly object _createCaseView;

        //ctor
        public CasesViewModel(ObservableCollection<Case> cases)
        {
            Cases = cases; 
            _caseInfoView = new CaseInfoView();
            _editCaseView = new EditCaseView(cases);
            _createCaseView = new CreateCaseView(cases);
            
            _currentView = null;
        }

        #region property
        public ObservableCollection<Case> Cases { get; set; }

        public Case SelectedCase
        {
            get => _selectedCase;
            set
            {
                if (_selectedCase == value) return;
                _selectedCase = value;
                OnPropertyChanged(nameof(SelectedCase));
                ShowCaseInfoCommand.Execute(null);
            }
        }

        public object CurrentView {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        } 

        #endregion

        #region commands
        //show CaseInfo
        private RelayCommand _showCaseInfoCommand;
        public RelayCommand ShowCaseInfoCommand
        {
            get
            {
                return _showCaseInfoCommand ??
                       (_showCaseInfoCommand = new RelayCommand(obj => { CurrentView = _caseInfoView; }));
            }
        }

        //show CaseInfo
        private RelayCommand _editCaseCommand;
        public RelayCommand EditCaseCommand
        {
            get
            {
                return _editCaseCommand ??
                       (_editCaseCommand = new RelayCommand(obj =>
                           {
                               CurrentView = _editCaseView;
                               ((EditCaseView) _editCaseView).TargetCase = SelectedCase;
                           },
                           obj => SelectedCase != null));
            }
        }

        private RelayCommand _createCaseCommand; 
        public RelayCommand CreateCaseCommand
        {
            get
            {
                return _createCaseCommand ??
                       (_createCaseCommand = new RelayCommand(obj =>
                           {
                               SelectedCase = null;
                               CurrentView = _createCaseView;
                           }));
            }
        }

        private RelayCommand _deleteCaseCommand;
        public RelayCommand DeleteCaseCommand
        {
            get
            {
                return _deleteCaseCommand ??
                       (_deleteCaseCommand = new RelayCommand(obj =>
                           {
                               Cases.Remove(SelectedCase);
                               CurrentView = null;
                           },
                           obj => SelectedCase != null));
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
