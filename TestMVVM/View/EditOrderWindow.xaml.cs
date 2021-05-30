using System;
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
using TestMVVM.Model;
using TestMVVM.ViewModel;

namespace TestMVVM.View
{
    /// <summary>
    /// Логика взаимодействия для EditOrderWindow.xaml
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        public EditOrderWindow(Order orderToEdit)
        {
            InitializeComponent();
            DataManageViewModel.SelectedOrder = orderToEdit;
            DataManageViewModel.ContrAgent = orderToEdit.ContrAgent;
            string[] date = Convert.ToString(orderToEdit.Date).Split();
            DataManageViewModel.OrderDate = date[0];
            DataManageViewModel.OrderStaff = orderToEdit.Staff;

        }
    }
}
