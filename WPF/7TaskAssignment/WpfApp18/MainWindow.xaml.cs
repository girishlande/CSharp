using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Windows.Threading;

namespace WpfApp18
{
    public class RelayCommand : ICommand
    {
        private Action<Object> ExecuteAction;
        private Predicate<object> ExecutePredicate;
        private event EventHandler CanExecuteChangedInternal;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public RelayCommand(Action<Object> executeAction, Predicate<object> executePredicate)
        {
            ExecuteAction = executeAction;
            ExecutePredicate = executePredicate;
        }

        public bool CanExecute(object parameter)
        {
            return this.ExecutePredicate != null && this.ExecutePredicate(parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public class TaskItem : INotifyPropertyChanged {
        private string taskname;
        public string TaskName
        {
            get { return taskname; }
            set { taskname = value; OnPropertyChanged("TaskName"); }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        private string taskstarttime;

        public string TaskStartTime
        {
            get { return taskstarttime; }
            set { taskstarttime = value; OnPropertyChanged("TaskStartTime"); }
        }

        private string _taskendtime;
        public string TaskEndTime
        {
            get { return _taskendtime; }
            set { _taskendtime = value; OnPropertyChanged("TaskEndTime"); }
        }

        private string _taskduration;
        public string TaskDuration
        {
            get { return _taskduration; }
            set { _taskduration = value; OnPropertyChanged("TaskDuration"); }
        }

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand DiscardCommand { get; set; }

        public event EventHandler DestroyMe;
        public event PropertyChangedEventHandler PropertyChanged;

        System.Timers.Timer TaskTimer;
        DateTime StartTimeCache;

        public bool Running { get; set; }

        public TaskItem()
        {
            StartCommand = new RelayCommand(StartCommandAction, CanStartCommand);
            StopCommand = new RelayCommand(StopCommandAction, CanStopCommand);
            DiscardCommand = new RelayCommand(DiscardCommandAction, CanDiscardCommand);
            Running = false;
            TaskStartTime = "NA";
            TaskEndTime = "NA";
            TaskDuration = "NA";
            TaskTimer = new System.Timers.Timer
            {
                Interval = 100
            };
            TaskTimer.Elapsed += OnTimerEvent;
        }

        private void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                TimeSpan delta = DateTime.Now - StartTimeCache;
                TaskDuration = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", delta.Hours, delta.Minutes, delta.Seconds,delta.Milliseconds);
            });
        }

        private bool CanDiscardCommand(object obj)
        {
            return true;
        }

        private void DiscardCommandAction(object obj)
        {
            DestroyMe?.Invoke(this, new EventArgs());
        }

        private bool CanStopCommand(object obj)
        {
            return Running;
        }

        private void StopCommandAction(object obj)
        {
            Running = false;
            TaskEndTime = DateTime.Now.ToString("HH:mm:ss:fff");
            TaskTimer.Enabled = false;
        }

        private bool CanStartCommand(object obj)
        {
            return !Running;
        }

        private void StartCommandAction(object obj)
        {
            Running = true;
            StartTimeCache = DateTime.Now;
            TaskStartTime = DateTime.Now.ToString("HH:mm:ss:fff");
            TaskEndTime = "NA";
            TaskTimer.Enabled = true;
            Console.WriteLine("Start task :" + TaskName);
        }
    }

    public class ViewModel : INotifyPropertyChanged {

        System.Timers.Timer SystemTimer;
        DateTime StartTimeCache;
        private string _systemUpTime;
        public string SystemUpTime
        {
            get { return _systemUpTime; }
            set { _systemUpTime = value; OnPropertyChanged("SystemUpTime"); }
        }


        public string TaskCounter
        {
            get { return TaskList.Count.ToString(); }
        }

        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TaskItem> TaskList { get; set; }
        public string TaskName { get; set; }

        public ICommand SaveCommand { get; set; }

        public ViewModel()
        {
            SaveCommand = new RelayCommand(SaveTaskAction, SaveTaskPredicate);
            TaskList = new ObservableCollection<TaskItem>();
            StartTimeCache = DateTime.Now;
            SystemTimer = new Timer();
            SystemTimer.Interval = 1000;
            SystemTimer.Elapsed += OnTimerEvent;
            SystemTimer.Start();
            TaskList.CollectionChanged += UpdateCounter;
        }

        private void UpdateCounter(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("TaskCounter");
        }

        private void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            Task.Run(() =>
            {
                TimeSpan delta = DateTime.Now - StartTimeCache;
                SystemUpTime = string.Format("{0:00}:{1:00}:{2:00}", delta.Hours, delta.Minutes, delta.Seconds);
            });
        }

        private bool SaveTaskPredicate(object obj)
        {
            return true;
        }

        private void SaveTaskAction(object obj)
        {
            var task = new TaskItem() { TaskName = this.TaskName };
            task.DestroyMe += DestroyTask;
            TaskList.Add(task);
        }

        private void DestroyTask(object sender, EventArgs e)
        {
            var task = sender as TaskItem;
            if (TaskList.Contains(task))
            {
                TaskList.Remove(task);
            }
        }
    }

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }
    }
}
