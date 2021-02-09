using System;
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

namespace WpfApp13
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

    public class Student {
        public int Roll { get; set; }
        public string Name { get; set; }
    }

    public class StudentViewModel {

        public int Roll { get; set; }
        public string Name { get; set; }

        public ICommand AddCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        public ObservableCollection<Student> Students { get; set; }
        
        public StudentViewModel()
        {
            Roll = 11;
            Name = "Girish";

            Students = new ObservableCollection<Student>();
            AddCommand = new RelayCommand(AddStudentAction, AddStudentPredicate);
            ClearCommand = new RelayCommand(ClearStudentAction, ClearStudentPredicate);
        }

        private bool ClearStudentPredicate(object obj)
        {
            return Students.Count > 0;
        }

        private void ClearStudentAction(object obj)
        {
            Students.Clear();
        }

        private bool AddStudentPredicate(object obj)
        {
            return true;
        }

        private void AddStudentAction(object obj)
        {
            Students.Add(new Student() { Roll = this.Roll, Name = this.Name });
        }
    }


    public partial class MainWindow : Window
    {
        StudentViewModel viewmodel;
        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new StudentViewModel();
            this.DataContext = viewmodel;
        }
    }
}
