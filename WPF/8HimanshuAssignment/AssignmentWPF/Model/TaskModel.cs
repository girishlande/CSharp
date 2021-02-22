using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssignmentWPF.Model
{
    public class TaskModel : INotifyPropertyChanged
    {
        #region Private members
        /// <summary>
        /// Task object to run thread.
        /// </summary>
        private Task task;

        /// <summary>
        /// Cancellation source to generate cancellation token.
        /// </summary>
        private CancellationTokenSource tokenSource;

        /// <summary>
        /// Cancellation token.
        /// </summary>
        private CancellationToken cts; 
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region Properties
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }

        }

        private DateTime? _startTime;
        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged();
            }

        }

        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                OnPropertyChanged();
            }

        }

        private TimeSpan _duration;
        public TimeSpan Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged();
            }

        }
        public bool IsRunning { get; set; } 
        #endregion

        #region Public methods
        /// <summary>
        /// Stop task
        /// </summary>
        public void Stop()
        {
            EndTime = DateTime.Now;
            tokenSource.Cancel();
            Duration = EndTime.Value - StartTime.Value;
            IsRunning = false;
            Console.WriteLine("Task stoped:" + Name);
        }

        /// <summary>
        /// Start task 
        /// </summary>
        public void Start()
        {
            tokenSource = new CancellationTokenSource();
            cts = tokenSource.Token;
            task = new Task(StartMethod, cts);
            Duration = new TimeSpan(0, 0, 0, 0);
            EndTime = null;
            StartTime = DateTime.Now;
            task.Start();
            IsRunning = true;
            Console.WriteLine("Task started:" + Name);
        } 
        #endregion

        #region Private methods

        /// <summary>
        /// Method to be executed in thread.
        /// </summary>
        private void StartMethod()
        {
            if (cts.IsCancellationRequested) return;
            while (true)
            {
                if (!cts.IsCancellationRequested)
                {
                    Duration = DateTime.Now - StartTime.Value;
                }
            }
        } 
        #endregion
    }
}
