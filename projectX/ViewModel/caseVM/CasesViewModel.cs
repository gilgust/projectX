using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using projectX.Annotations;
using projectX.domain;
using projectX.Data;
using projectX.Data.interfaces;
using projectX.Data.singleton;
using projectX.Views;

namespace projectX.ViewModel
{
    public class CasesViewModel : INotifyPropertyChanged
    {
        private readonly ICaseCrud _caseProvider; 

        private ObservableCollection<Case> _cases;

        private Case _selectedCase;

        private UserControl _currentView;
        private readonly UserControl _caseView;
        private readonly UserControl _editCaseView;
        private readonly UserControl _createCaseView;

        //ctor

        public CasesViewModel()
        {
            _caseProvider = new CasesProvider(); 

            _caseView = new CaseView();
            _editCaseView = new EditCaseView{DataContext = null};

            _createCaseView = new CreateCaseView();
            ((CreateCaseViewModel) _createCaseView.DataContext).AddedItem += ChangeTargetAfterAddedCase;

            _currentView = null;
            Cases = null; 
        } 

        #region property 

        public ObservableCollection<Case> Cases
        {
            get
            {
                if (_cases == null)
                {
                    Task.Run(() => { Cases = _caseProvider.Cases; });
                }

                return _cases;
            }
            private set
            {
                _cases = value;
                OnPropertyChanged(nameof(Cases));
            }
        }

        public Case SelectedCase
        {
            get => _selectedCase;
            set
            {
                if (_selectedCase == value) return;
                _selectedCase = value;
                
                _caseView.DataContext = new CaseViewModel(value); 

                OnPropertyChanged(nameof(SelectedCase));
                CurrentView = _caseView;
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
        private RelayCommand _editCaseCommand;
        public RelayCommand EditCaseCommand
        {
            get
            {
                return _editCaseCommand ??
                       (_editCaseCommand = new RelayCommand(obj =>
                           {
                               if(_editCaseView.DataContext != null)
                                   ((EditCaseViewModel)_editCaseView.DataContext).EditItem -= GetChangedCase;

                               _editCaseView.DataContext = new EditCaseViewModel(SelectedCase);
                               ((EditCaseViewModel)_editCaseView.DataContext).EditItem += GetChangedCase;
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
                               _caseProvider.RemoveCace(SelectedCase);
                               Cases.Remove(SelectedCase); 
                               CurrentView = null;
                           },
                           obj => SelectedCase != null));
            }
        }
        #endregion
         
        private void GetChangedCase(int id)
        {
            var target = Cases.First(c => c.Id == id);
            target = _caseProvider.GetCaseById(id);
            
            SelectedCase = target;
        }

        private void ChangeTargetAfterAddedCase(int id)
        {
            SelectedCase = Cases.First(c => c.Id == id);
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
