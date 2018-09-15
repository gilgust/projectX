using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;
using projectX.ViewModel.proectVM;
using projectX.Views.windows;

namespace projectX.ViewModel
{
    class AppViewModel
    {
        #region command

        private RelayCommand _showCasesCommand;

        public RelayCommand ShowCasesCommand
        {
            get
            {
                return _showCasesCommand ??
                       (_showCasesCommand = new RelayCommand(obj =>
                           {
                               CasesWindow cw = new CasesWindow();
                               cw.Show();
                               cw.DataContext = new CasesViewModel();
                           })
                       );
            }
        }

        private RelayCommand _showProectsCommand;

        public RelayCommand ShowProectsCommand
        {
            get
            {
                return _showProectsCommand ??
                       (_showProectsCommand = new RelayCommand(obj =>
                           {
                               ProectsWindow cw = new ProectsWindow() { DataContext = new ProectsViewModel() };
                               cw.Show();
                           })
                       );
            }
        }

        #endregion
    }
}
