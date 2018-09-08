using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
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
        private ApplicationContext db;
        private IEnumerable<Case> _cases;
        private Case _selectedCase;
        private UserControl _currentView;
<<<<<<< HEAD:projectX/ViewModel/caseVM/CasesViewModel.cs
        private UserControl _caseView;
        private UserControl _editCaseView;
=======
        private  UserControl _caseInfoView;
        private  UserControl _editCaseView;
>>>>>>> parent of 9507b51... proect crud done:projectX/ViewModel/case/CasesViewModel.cs
        private readonly UserControl _createCaseView;

        //ctor
        public CasesViewModel()
<<<<<<< HEAD:projectX/ViewModel/caseVM/CasesViewModel.cs
        { 
            db = new ApplicationContext();
            Cases = db.Cases.Local;

            _caseView = null;
=======
        {
            Cases = DataFromCollections.Instance;
            _caseInfoView = null;
>>>>>>> parent of 9507b51... proect crud done:projectX/ViewModel/case/CasesViewModel.cs
            _editCaseView = new EditCaseView();
            _createCaseView = new CreateCaseView() { DataContext = new CreateCaseViewModel() };

            _currentView = null;
        }

        #region property 

        public IEnumerable<Case> Cases
        {
            get => _cases;
            set
            {
<<<<<<< HEAD:projectX/ViewModel/caseVM/CasesViewModel.cs
                _cases = value;
                OnPropertyChanged(nameof(Cases));
=======
                if (_selectedCase == value) return;
                _selectedCase = value;
                
                if(_caseInfoView == null)
                    _caseInfoView = new CaseInfoView() { DataContext = new CaseViewModel() }; 
                ((CaseViewModel)_caseInfoView.DataContext).Case = value;


                OnPropertyChanged(nameof(SelectedCase));
                ShowCaseInfoCommand.Execute(null);
>>>>>>> parent of 9507b51... proect crud done:projectX/ViewModel/case/CasesViewModel.cs
            }
        }

        //public Case SelectedCase
        //{
        //    get => _selectedCase;
        //    set
        //    {
        //        if (_selectedCase == value) return;
        //        _selectedCase = value;
                
        //        if(_caseView == null)
        //            _caseView = new CaseView() { DataContext = new CaseViewModel() }; 
        //        ((CaseViewModel)_caseView.DataContext).Case = value;


        //        OnPropertyChanged(nameof(SelectedCase));
        //        ShowCaseInfoCommand.Execute(null);
        //    }
        //}

        //public UserControl CurrentView {
        //    get => _currentView;
        //    set
        //    {
        //        if (ReferenceEquals(_currentView, value)) return;
        //        _currentView = value;
        //        OnPropertyChanged(nameof(CurrentView));
        //    }
        //} 

        #endregion

<<<<<<< HEAD:projectX/ViewModel/caseVM/CasesViewModel.cs
        //#region commands
        ////show CaseInfo
        //private RelayCommand _showCaseInfoCommand;
        //public RelayCommand ShowCaseInfoCommand
        //{
        //    get
        //    {
        //        return _showCaseInfoCommand ??
        //               (_showCaseInfoCommand = new RelayCommand(obj =>
        //               {
        //                   if(SelectedCase!= null)
        //                       CurrentView = _caseView;
        //               }));
        //    }
        //}

        ////show CaseInfo
        //private RelayCommand _editCaseCommand;
        //public RelayCommand EditCaseCommand
        //{
        //    get
        //    {
        //        return _editCaseCommand ??
        //               (_editCaseCommand = new RelayCommand(obj =>
        //                   {

        //                       _editCaseView.DataContext = new EditCaseViewModel(SelectedCase);

        //                       CurrentView = _editCaseView;
        //                   },
        //                   obj => SelectedCase != null));
        //    }
        //}

        //private RelayCommand _createCaseCommand; 
        //public RelayCommand CreateCaseCommand
        //{
        //    get
        //    {
        //        return _createCaseCommand ??
        //               (_createCaseCommand = new RelayCommand(obj =>
        //                   {
        //                       SelectedCase = null;
        //                       CurrentView = _createCaseView;
        //                   }));
        //    }
        //}

        //private RelayCommand _deleteCaseCommand;
        //public RelayCommand DeleteCaseCommand
        //{
        //    get
        //    {
        //        return _deleteCaseCommand ??
        //               (_deleteCaseCommand = new RelayCommand(obj =>
        //                   {
        //                      // Cases.RemoveCace(SelectedCase);
        //                       CurrentView = null;
        //                   },
        //                   obj => SelectedCase != null));
        //    }
        //}
        //#endregion
=======
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
                               CurrentView = _caseInfoView;
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
>>>>>>> parent of 9507b51... proect crud done:projectX/ViewModel/case/CasesViewModel.cs


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
