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
using projectX.Data;

namespace projectX.Views.windows
{
    /// <summary>
    /// Interaction logic for Pick_a_Case.xaml
    /// </summary>
    public partial class Pick_a_Case : Window
    {
        public Pick_a_Case()
        {
            CasesProvider casesProvider = new CasesProvider(); 
            InitializeComponent();
            Task.Run(() =>
            {
                ListBoxCases.ItemsSource = casesProvider.Cases;
            });
        }
        
        public Case SelectedCase => (Case)ListBoxCases.SelectedItem;

        private void Accept_click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
