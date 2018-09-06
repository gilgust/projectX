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
using System.Windows.Shapes;
using projectX.domain;
using projectX.ViewModel;

namespace projectX
{
    /// <summary>
    /// Interaction logic for CasesWindow.xaml
    /// </summary>
    public partial class CasesWindow : Window
    {

        public CasesWindow()
        {
            InitializeComponent();
            DataContext = new CasesViewModel();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var a = ((CasesViewModel) DataContext).Cases.ToList();
            if(a.Count>1)
                MessageBox.Show(a[0].Name);
            else
                MessageBox.Show("данных нет");
        }
    }
}
