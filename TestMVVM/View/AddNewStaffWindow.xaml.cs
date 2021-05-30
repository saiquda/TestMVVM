﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestMVVM.ViewModel;

namespace TestMVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewEmployeeWindow.xaml
    /// </summary>
    public partial class AddNewStaffWindow : Window
    {
        public AddNewStaffWindow()
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
        }
    }
}
