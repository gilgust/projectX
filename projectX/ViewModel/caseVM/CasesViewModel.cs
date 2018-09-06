using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using projectX.Views;

namespace projectX.ViewModel
{
    public class CasesViewModel : INotifyPropertyChanged
    {
        private Case _selectedCase;
        private UserControl _currentView;
        private  UserControl _caseView;
        private  UserControl _editCaseView;
        private readonly UserControl _createCaseView;

        //ctor

        public CasesViewModel()
        {
            Cases = DataFromCollections.Instance;
            _caseView = null;
            _editCaseView = new EditCaseView();
            _createCaseView = new CreateCaseView() { DataContext = new CreateCaseViewModel() };

            _currentView = null;
        } 

        #region property 

        public ICaseCrud Cases { get; }

        public Case SelectedCase
        {
            get => _selectedCase;
            set
            {
                if (_selectedCase == value) return;
                _selectedCase = value;
                
                if(_caseView == null)
                    _caseView = new CaseView() { DataContext = new CaseViewModel() }; 
                ((CaseViewModel)_caseView.DataContext).Case = value;


                OnPropertyChanged(nameof(SelectedCase));
                ShowCaseInfoCommand.Execute(null);
            }
        }

        public UserControl CurrentView {
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
        //show CaseInfo
        private RelayCommand _showCaseInfoCommand;
        public RelayCommand ShowCaseInfoCommand
        {
            get
            {
                return _showCaseInfoCommand ??
                       (_showCaseInfoCommand = new RelayCommand(obj =>
                       {
                           if(SelectedCase!= null)
                               CurrentView = _caseView;
                       }));
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

                               _editCaseView.DataContext = new EditCaseViewModel(SelectedCase);

                               CurrentView = _editCaseView;
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
                               Cases.RemoveCace(SelectedCase);
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
