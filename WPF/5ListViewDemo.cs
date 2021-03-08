// In this demo we create simple listview and add strings to it on button click. 
// Basically listview has item source set to observable collection and we add the strings in that observable collection 


// -----------------
// WPF
// -----------------

<Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="100"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <Button Content="Click me" Click="Button_Click"/>
        <ListView Grid.Row="1" ItemsSource="{Binding Logs}" ScrollViewer.VerticalScrollBarVisibility="Visible" Background="Black" Foreground="LimeGreen">
            
        </ListView>

    </Grid>

// -----------------
// Code Behind 
// -----------------

public class ViewModel {
        public ObservableCollection<string> Logs { get; set; }
        public ViewModel()
        {
            Logs = new ObservableCollection<string>();
        }

        public void AddLogEntry(string log)
        {
            Logs.Add(log);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewmodel;
        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new ViewModel();
            this.DataContext = viewmodel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewmodel.AddLogEntry("Hello Girish");
        }
    }