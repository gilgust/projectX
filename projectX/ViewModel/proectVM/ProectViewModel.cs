using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using projectX.Annotations;
using projectX.domain;
using projectX.Views.windows;

namespace projectX.ViewModel.proectVM
{
    class ProectViewModel : INotifyPropertyChanged, IDisposable
    {
        private Proect _proect;
        public ProectViewModel() { }
        public ProectViewModel(Proect p)
        {
            if (p != null)
            {
                _proect = p;
                _proect.PropertyChanged += Proect_PropertyChanged;
                CaseResults = new ObservableCollection<CaseResult>(_proect.CaseResults);
            }
        }

        public void Dispose()
        {
            if (_proect != null)
                _proect.PropertyChanged -= Proect_PropertyChanged;
        }

        private void Proect_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged(e.PropertyName);

        #region properties
 
        public int Id => _proect.Id;
        public string Name => _proect.Name;
        public string Description => _proect.Description;
        public List<Mark> Marks => _proect.Marks;

        private string _selectedMark;
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

        public ObservableCollection<CaseResult> CaseResults { get;}

        #endregion

        #region commands

        private RelayCommand _addCaseResultCommand;
        public RelayCommand AddCaseResultCommand
        {
            get
            {
                return _addCaseResultCommand ??
                       (_addCaseResultCommand = new RelayCommand(obj =>
                           {
                               Pick_a_Case pickACase = new Pick_a_Case(); 

                               if (pickACase.ShowDialog() == true)
                               {
                                   _proect.CaseResults.Add(new CaseResult{Case = pickACase.SelectedCase});
                                   CaseResults.Add(new CaseResult { Case = pickACase.SelectedCase });
                               }
                               else
                               {
                                   MessageBox.Show("Case not picked");
                               }
                           })
                       );
            }
        }

        #endregion


        #region prop
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
