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
    /// Логика взаимодействия для EditStaffWindow.xaml
    /// </summary>
    public partial class EditStaffWindow : Window
    {
        public EditStaffWindow(Staff staffToEdit)
        {
            InitializeComponent();
            DataContext = new DataManageViewModel();
            DataManageViewModel.SelectedStaff = staffToEdit;
            string name;
            if (staffToEdit.Surename != null && staffToEdit.Middlename != null)
                name = staffToEdit.Name +" "+ staffToEdit.Surename +" "+ staffToEdit.Middlename;
            if (staffToEdit.Surename != null && staffToEdit.Middlename == null)
                name = staffToEdit.Name + " " + staffToEdit.Surename;
            else
                name = staffToEdit.Name;
            DataManageViewModel.StaffName = name;
            string[] date = Convert.ToString(staffToEdit.Date).Split();
            DataManageViewModel.Date = date[0];
            DataManageViewModel.Sex = (int)staffToEdit.Sex;
            DataManageViewModel.Department = staffToEdit.Department;
        }
    }
}
