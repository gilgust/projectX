﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectX.domain;

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
                               CasesWindow cw = new CasesWindow(){DataContext = new CasesViewModel()};
                               cw.Show(); 
                           })
                       );
            }
        }

        #endregion
    }
}
