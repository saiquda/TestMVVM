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
    /// Логика взаимодействия для EditDepartmentWindow.xaml
    /// </summary>
    public partial class EditDepartmentWindow : Window
    {
        public EditDepartmentWindow(Department departmentToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            DataManageViewModel.SelectedDepartment = departmentToEdit;
            DataManageViewModel.DepartmentName = departmentToEdit.Name;
            DataManageViewModel.Boss = departmentToEdit.DepartmentBoss;

        }
    }
}
