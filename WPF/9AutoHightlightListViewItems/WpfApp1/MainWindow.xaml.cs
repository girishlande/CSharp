using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    public class TaskItem {
        public string TaskName { get; set; }
        public string TaskStartTime { get; set; }
        public string TaskEndTime { get; set; }
        public string TaskDuration { get; set; }

        public TaskItem()
        {
            TaskName = "Girish";
            TaskStartTime = "12:00:01";
            TaskEndTime = "2:32:16";
            TaskDuration = "4:00:00";
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> TaskList { get; set; }

        private int _selectedIndex;

        public int SelectedItemIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; OnPropertyChanged("SelectedItemIndex"); }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public ViewModel()
        {
            TaskList = new ObservableCollection<TaskItem>();
            TaskList.Add(new TaskItem());
            TaskList.Add(new TaskItem());
            TaskList.Add(new TaskItem());
            TaskList.Add(new TaskItem());
            TaskList.Add(new TaskItem());
            TaskList.Add(new TaskItem());
            SelectedItemIndex = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel m_viemodel;
        System.Timers.Timer timer1;

        public MainWindow()
        {
            InitializeComponent();

            m_viemodel = new ViewModel();
            this.DataContext = m_viemodel;

            timer1 = new System.Timers.Timer();
            timer1.Interval = 1000;
            timer1.Elapsed += OnTimeOut;
            timer1.Start();
        }

        private void OnTimeOut(object sender, ElapsedEventArgs e)
        {
            m_viemodel.SelectedItemIndex = (m_viemodel.SelectedItemIndex + 1) % 6;
        }
    }
}
