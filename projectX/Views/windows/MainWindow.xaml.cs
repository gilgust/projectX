using projectX.services;
using System.Windows;

namespace projectX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();
            InitFolders IF = new InitFolders(); 
        }
    }
}
