using AssignmentWPF.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;

namespace AssignmentWPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private members
        private DispatcherTimer timer;
        private Stopwatch stopwatch; 
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        } 
        #endregion

        #region Properties
        private string taskName;
        public string TaskName
        {
            get
            {
                return taskName;
            }
            set
            {
                taskName = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan runningTime;
        public TimeSpan RunningTime
        {
            get
            {
                return runningTime;
            }
            set
            {
                runningTime = value;
                OnPropertyChanged();
            }
        }
        public int RunningTasksCount { get; set; }
        public ObservableCollection<TaskModel> Tasks { get; set; }
        public TaskModel SelectedTaskModel { get; set; } 
        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            Tasks = new ObservableCollection<TaskModel>();
            SaveCommand = new RelayCommand(new Action<object>(Save), canSave);
            StartCommand = new RelayCommand(new Action<object>(Start), canStart);
            StopCommand = new RelayCommand(new Action<object>(Stop), canStop);
            DiscardCommand = new RelayCommand(new Action<object>(Discard), canDiscard);
            ExitCommand = new RelayCommand(new Action<object>(Exit));
            StartTimer();
        }
        #endregion

        #region Private methods
        private bool canDiscard(object arg)
        {
            return SelectedTaskModel != null;
        }

        private bool canStop(object arg)
        {
            return SelectedTaskModel != null && SelectedTaskModel.IsRunning;
        }

        private bool canStart(object arg)
        {
            return SelectedTaskModel != null && !SelectedTaskModel.IsRunning;
        }

        private bool canSave(object arg)
        {
            return !string.IsNullOrEmpty(TaskName);
        }

        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RunningTime = stopwatch.Elapsed;
        }

        private void Exit(object obj)
        {
            foreach (var task in Tasks)
            {
                task.Stop();
            }
            Tasks.Clear();
            Tasks = null;
            App.Current.Shutdown();
        }

        private void Discard(object obj)
        {
            TaskModel taskModel = SelectedTaskModel;
            taskModel.Stop();
            Tasks.Remove(taskModel);
        }

        private void Stop(object obj)
        {
            SelectedTaskModel.Stop();
            RunningTasksCount = Tasks.Where(i => i.IsRunning).Count();
            OnPropertyChanged("RunningTasksCount");
        }

        private void Start(object obj)
        {
            SelectedTaskModel.Start();
            RunningTasksCount = Tasks.Where(i => i.IsRunning).Count();
            OnPropertyChanged("RunningTasksCount");
        }

        private void Save(object obj)
        {
            TaskModel task = new TaskModel() { Name = TaskName };
            Tasks.Add(task);
            TaskName = string.Empty;
            SelectedTaskModel = task;
        }
        #endregion
     
        #region Commands
        public ICommand SaveCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public ICommand DiscardCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion
    }
}
