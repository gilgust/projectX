using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using projectX.domain;
using projectX.services;
using projectX.ViewModel;

namespace projectX.Views
{
    /// <summary>
    /// Interaction logic for EditCaseView.xaml
    /// </summary>
    public partial class EditCaseView : UserControl
    {
        private Case _targetCase;
        private EditCaseViewModel VM;
        public EditCaseView(ObservableCollection<Case> cases)
        {
            InitializeComponent();
            VM = new EditCaseViewModel(cases, new DefaultDialogService());
            DataContext = VM;
        }

        public Case TargetCase
        {
            set
            {
                if(_targetCase != null && _targetCase == value) return;
                _targetCase = value;
                VM.TargetCase = _targetCase;
            }
        }
    }
}
