using projectX.Annotations;
using projectX.Data;
using projectX.Data.interfaces;
using projectX.domain;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace projectX.ViewModel.proectVM
{
    public class EditProectViewModel : INotifyPropertyChanged
    {
        private Proect _originalProect;
        private readonly Proect _cloneProect;
        private bool _wasChange;

        private readonly IProectCrud _data;
        private string _selectedMark;

        //public EditProectViewModel()
        //{

        //}

        public EditProectViewModel(Proect orProect)
        {
            _data = DataFromCollections.Instance;

            _selectedMark = null;

            _wasChange = false;

            _originalProect = orProect;
            _cloneProect = (Proect)orProect.Clone();

            _cloneProect.PropertyChanged += Proect_PropertyChanged;
        }

        public void Dispose() => _cloneProect.PropertyChanged -= Proect_PropertyChanged;
        private void Proect_PropertyChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

        #region prop  
        #region ClonProperty 
        public string Name
        {
            get => _cloneProect.Name;
            set => _cloneProect.Name = value;
        }
        public string Description
        {
            get => _cloneProect.Description;
            set => _cloneProect.Description = value;
        }

        public ObservableCollection<string> Marks
        {
            get => _cloneProect.Marks;
            set
            {
                _cloneProect.Marks = value;
                _wasChange = true;
            }
        } 
        #endregion


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
                           _cloneProect.Marks.Add(SelectedMark);
                           SelectedMark = null;
                           _wasChange = true;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && SelectedMark != " " && !_cloneProect.Marks.Contains(SelectedMark))
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
                           if (!_cloneProect.Marks.Contains(SelectedMark)) return;
                           _cloneProect.Marks.Remove(SelectedMark);
                           SelectedMark = null;
                           _wasChange = true;
                       }, obj => !string.IsNullOrEmpty(SelectedMark) && _cloneProect.Marks.Contains(SelectedMark))
                       );
            }
        }

        //save
        private RelayCommand _saveProectCommand;
        public RelayCommand SaveProectCommnad
        {
            get
            {
                return _saveProectCommand ??
                       (_saveProectCommand = new RelayCommand(obj =>
                       {
                           _data.EditProect(_cloneProect);
                           _originalProect = (Proect)_cloneProect.Clone();
                           _wasChange = false;
                       }, x => ReadeToSave())
                       );
            }
        }

        #endregion

        private bool ReadeToSave()
        {
            return _wasChange || (_originalProect.Name != _cloneProect.Name || _originalProect.Description != _cloneProect.Description);
        }

        #region notifyprop
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
