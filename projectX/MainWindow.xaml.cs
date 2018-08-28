using projectX.models;
using projectX.services;
using System.Windows;

namespace projectX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DomainColections _domainColection;
        public MainWindow()
        {
            InitializeComponent();
            InitFolders IF = new InitFolders();

            _domainColection = new DomainColections();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CasesWindow cw = new CasesWindow(_domainColection.Cases) {Owner = this};  
            cw.Show();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
