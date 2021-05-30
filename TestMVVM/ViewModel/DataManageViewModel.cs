using TestMVVM.View;
using TestMVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMVVM.ViewModel
{
    public class DataManageViewModel : INotifyPropertyChanged
    {
        //все отделы
        private List<Department> allDepartments = DataWorker.GetAllDepartments();
        public List<Department> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }

        private List<Staff> allStaff = DataWorker.GetAllStaff();
        public List<Staff> AllStaff
        {
            get { return allStaff; }
            set
            {
                allStaff = value;
                NotifyPropertyChanged("AllStaff");
            }
        }

        private List<Order> allOrders = DataWorker.GetAllOrders();
        public List<Order> AllOrders
        {
            get { return allOrders; }
            set
            {
                allOrders = value;
                NotifyPropertyChanged("AllOrders");
            }
        }

        //свойства для отдела
        public static string DepartmentName { get; set; }
        public static Staff Boss { get; set; }

        //свойства для сотрудников
        public static string StaffName { get; set; }
        [Column(TypeName = "date")]
        public static string Date { get; set; }
        public static int Sex { get; set; }
        public static Department Department { get; set; }

        //свойства для заказов
        public static string ContrAgent { get; set; }
        [Column(TypeName = "date")]
        public static string OrderDate { get; set; }
        public static Staff OrderStaff { get; set; }

        //свойства для выделения
        public TabItem SelectedTabItem { get; set; }
        public static Staff SelectedStaff { get; set; }
        public static Department SelectedDepartment { get; set; }
        public static Order SelectedOrder { get; set;  }

        

        #region edit
        private RelayCommand editStaff;
        public RelayCommand EditStaff
        {
            get
            {
                return editStaff ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedStaff != null) 
                    {
                        DataWorker.EditStaff(SelectedStaff, StaffName, Date, Sex, Department);
                        UpdateAllViews();
                        SetNullValues();
                        window.Close();
                    }
                });
            }
        }
        private RelayCommand editOrder;
        public RelayCommand EditOrder
        {
            get
            {
                return editOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedOrder != null)
                    {
                        DataWorker.EditOrder(SelectedOrder, ContrAgent, OrderDate, OrderStaff);
                        UpdateAllViews();
                        SetNullValues();
                        window.Close();
                    }
                });
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    if (SelectedDepartment != null)
                    {
                        DataWorker.EditDepartment(SelectedDepartment, DepartmentName, Boss);
                        UpdateAllViews();
                        SetNullValues();
                        window.Close();
                    }
                });
            }
        }
        #endregion

        #region delete
        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    //если сотрудник
                    if (SelectedTabItem.Name == "StaffTab" && SelectedStaff != null)
                    {
                        DataWorker.DeleteStaff(SelectedStaff);
                        UpdateAllViews();
                    }
                    //если ордер
                    if (SelectedTabItem.Name == "OrderTab" && SelectedOrder != null)
                    {
                        DataWorker.DeleteOrder(SelectedOrder);
                        UpdateAllViews();
                    }
                    //если отдел
                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllViews();
                    }
                    //обновление
                    SetNullValues();
                }
                    );
            }
        }

        #endregion

        #region add

        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    DataWorker.CreateOrder(ContrAgent, OrderDate, OrderStaff);
                    UpdateAllViews();
                    SetNullValues();
                    wnd.Close();
                });
            }
        }

        private RelayCommand addNewStaff;
        public RelayCommand AddNewStaff
        {
            get
            {
                return addNewStaff ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    DataWorker.CreateStaff(StaffName, Date, Sex, Department);
                    UpdateAllViews();
                    SetNullValues();
                    wnd.Close();
                });
            }
        }

        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    DataWorker.CreateDepartment(DepartmentName);
                    UpdateAllViews();
                    SetNullValues();
                    wnd.Close();
                });
            }
        }
        #endregion

        #region updateview

        private void SetNullValues()
        {
            //для сотрудника
            StaffName = null;
            Date = null;
            Sex = 0;
            Department = null;
            //для Заказа
            ContrAgent = null;
            OrderDate = null;
            OrderStaff = null;
            //для отдела
            DepartmentName = null;
            Boss = null;
        }

        private void UpdateAllViews()
        {
            UpdateAllDepartmentsView();
            UpdateAllEmployeesView();
            UpdateAllOrdersView();
        }

        private void UpdateAllDepartmentsView()
        {
            AllDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }
        private void UpdateAllEmployeesView()
        {
            AllStaff = DataWorker.GetAllStaff();
            MainWindow.AllEmployeesView.ItemsSource = null;
            MainWindow.AllEmployeesView.Items.Clear();
            MainWindow.AllEmployeesView.ItemsSource = AllStaff;
            MainWindow.AllEmployeesView.Items.Refresh();
        }
        private void UpdateAllOrdersView()
        {
            AllOrders = DataWorker.GetAllOrders();
            MainWindow.AllOrdersView.ItemsSource = null;
            MainWindow.AllOrdersView.Items.Clear();
            MainWindow.AllOrdersView.ItemsSource = AllOrders;
            MainWindow.AllOrdersView.Items.Refresh();
        }

        #endregion

        #region opencomands
        private RelayCommand openAddNewDepartmentWnd;

        public RelayCommand OpenAddNewDepartmentWnd
        {
            get
            {
                return openAddNewDepartmentWnd ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewStaffWnd;

        public RelayCommand OpenAddNewStaffWnd
        {
            get
            {
                return openAddNewStaffWnd ?? new RelayCommand(obj =>
                {
                    OpenAddStaffWindowMethod();
                }
                );
            }
        }
        private RelayCommand openAddNewOrdertWnd;

        public RelayCommand OpenAddNewOrdertWnd
        {
            get
            {
                return openAddNewOrdertWnd ?? new RelayCommand(obj =>
                {
                    OpenAddOrderWindowMethod();
                }
                );
            }
        }

        private RelayCommand openeditItemWnd;

        public RelayCommand OpenEditItemWnd
        {
            get
            {
                return openeditItemWnd ?? new RelayCommand(obj =>
                {
                    //если сотрудник
                    if (SelectedTabItem.Name == "StaffTab" && SelectedStaff != null)
                    {
                        OpenEditStaffWindowMethod(SelectedStaff);
                    }
                    //если ордер
                    if (SelectedTabItem.Name == "OrderTab" && SelectedOrder != null)
                    {
                        OpenEditOrderWindowMethod(SelectedOrder);
                    }
                    //если отдел
                    if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentWindowMethod(SelectedDepartment);
                    }
                });
            }
        }

        #endregion

        #region openmethods
        //Открытие окон
        private void OpenAddDepartmentWindowMethod()
        {
            AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
            SetCenterPositionAndOpen(newDepartmentWindow);
        }
        private void OpenAddStaffWindowMethod()
        {
            AddNewStaffWindow newStaffWindow = new AddNewStaffWindow();
            SetCenterPositionAndOpen(newStaffWindow);
        }
        private void OpenAddOrderWindowMethod()
        {
            AddNewOrderWindow newOrderWindow = new AddNewOrderWindow();
            SetCenterPositionAndOpen(newOrderWindow);
        }

        //Окна редактирования
        private void OpenEditDepartmentWindowMethod(Department department)
        {
            EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow(department);
            SetCenterPositionAndOpen(editDepartmentWindow);
        }
        private void OpenEditStaffWindowMethod(Staff staff)
        {
            EditStaffWindow editStaffWindow = new EditStaffWindow(staff);
            SetCenterPositionAndOpen(editStaffWindow);
        }
        private void OpenEditOrderWindowMethod(Order order)
        {
            EditOrderWindow editOrderWindow = new EditOrderWindow(order);
            SetCenterPositionAndOpen(editOrderWindow);
        }
        #endregion



        private void SetRedBlockControll(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
