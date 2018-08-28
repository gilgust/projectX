﻿using System;
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
    /// Interaction logic for CreateCaseView.xaml
    /// </summary>
    public partial class CreateCaseView : UserControl
    {
        public CreateCaseView(ObservableCollection<Case> cases)
        {
            InitializeComponent();
            this.DataContext = new CreateCaseViewModel(cases, new DefaultDialogService());
        }
    }
}
