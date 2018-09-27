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
        private readonly Proect _originalProect;
        private readonly Proect _cloneProect;
        private bool _wasChange;

        private readonly IProectCrud _proectProvider;
        private readonly IMarkCrud _markProvider;

        public delegate void EditProectHandler(int id);
        public event EditProectHandler EditItem;

        private string _newMark; 

        public EditProectViewModel(Proect orProect)
        {
            _proectProvider = new ProectProvider();
            _markProvider = new MarkProvider();
            _wasChange = false;
            
            _originalProect = orProect;
            _cloneProect = (Proect)orProect.Clone();
            Marks = new ObservableCollection<Mark>(_cloneProect.Marks);

            _cloneProect.PropertyChanged += Proect_PropertyChanged;

            _newMark = "";
        }

        public void Dispose() => _cloneProect.PropertyChanged -= Proect_PropertyChanged;
        private void Proect_PropertyChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged(e.PropertyName);

        #region prop

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

        public ObservableCollection<Mark> Marks { get; set; } 

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
                           _cloneProect.Marks.Add(mark);

                           NewMark = "";
                           _wasChange = true;
                       }, obj => !string.IsNullOrWhiteSpace(NewMark)));
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
                           Marks.Remove((Mark) obj);
                           _cloneProect.Marks.Remove((Mark)obj);
                           _wasChange = true;
                       }));
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
                           _proectProvider.EditProect(_cloneProect);
                           EditItem?.Invoke(_cloneProect.Id);

                           _wasChange = false;
                       }, x => ReadeToSave()));
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
