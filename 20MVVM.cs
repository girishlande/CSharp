 // ------------
 // ICOmmand 
 // ------------
 
 public class RelayCommand : ICommand
    {
        private Action<object> execute;

        private Predicate<object> canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

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

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            this.canExecute = _ => false;
            this.execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
	
	// ---------------------
	// Viewmodel 
	// ---------------------
	
	 public class MainWindowViewModel
    {
        private ICommand hiButtonCommand;

        private ICommand toggleExecuteCommand { get; set; }

        private bool canExecute = true;

        public string HiButtonContent
        {
            get
            {
                return "click to hi";
            }
        }

        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }

        public ICommand ToggleExecuteCommand
        {
            get
            {
                return toggleExecuteCommand;
            }
            set
            {
                toggleExecuteCommand = value;
            }
        }

        public ICommand HiButtonCommand
        {
            get
            {
                return hiButtonCommand;
            }
            set
            {
                hiButtonCommand = value;
            }
        }

        public MainWindowViewModel()
        {
            HiButtonCommand = new RelayCommand(ShowMessage, param => this.canExecute);
            toggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }

        public void ShowMessage(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
    }
	
	
	
	// ---------
	// VIEW 
	// ---------
	 <Grid>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition/>
        </Grid.RowDefinitions>

        <Button Grid.Row="0" Command="{Binding HiButtonCommand}" 
        CommandParameter="Hai" Content="{Binding HiButtonContent}"  Width="100" Height="100"  />
        <Button Grid.Row="1" Content="Toggle Can Click" 
        Command="{Binding ToggleExecuteCommand}"  Width="100" Height="100"/>
    </Grid>
	
	// ---------------------------
	// Main Class code behind
	// ---------------------------
	
	 public partial class MainWindow : Window
    {
        MainWindowViewModel viewmodel;

        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new MainWindowViewModel();
            this.DataContext = viewmodel;
        }
    }