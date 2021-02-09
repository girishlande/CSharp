// XAML 


    <Grid Margin="100,10,10,10">
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="300"></ColumnDefinition>
            <ColumnDefinition Width="200"></ColumnDefinition>
        </Grid.ColumnDefinitions>
        <ListView Width="Auto" ItemsSource="{Binding Students}">
            <ListView.View>
                <GridView>
                    <GridViewColumn Header="Roll" Width="100" DisplayMemberBinding="{Binding Roll}"></GridViewColumn>
                    <GridViewColumn Header="Name" Width="200"  DisplayMemberBinding="{Binding Name}"></GridViewColumn>
                </GridView>
            </ListView.View>
        </ListView>

        <StackPanel Grid.Row="0" Grid.Column="1">
            <TextBox x:Name="SRoll"></TextBox>
            <TextBox x:Name="SName"></TextBox>
            <Button x:Name="Submit" Content="Submit" Click="Submit_Click"></Button>
        </StackPanel>
    </Grid>


//  Window backend 

 ClassRoom c;
        public MainWindow()
        {
            InitializeComponent();
            c = new ClassRoom();
            this.DataContext = c;    // this is important 
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (SRoll.Text.ToString().Length>0 && SName.Text.ToString().Length>0)
            {
                int r;
                bool ok = int.TryParse(SRoll.Text.ToString(),out r);
                if (ok)
                {
                    c.Students.Add(new Student() { Name = SName.Text.ToString(), Roll = r });
                }
            }
        }
		
// Classroom 

 class ClassRoom
    {
		private Student _student;

		public Student CStudent
		{
			get { return _student; }
			set { _student = value; }
		}


		private ObservableCollection<Student> _students;

		public ObservableCollection<Student> Students
		{
			get { return _students; }
			set { _students = value; }
		}

		public ClassRoom()
		{
			Students = new ObservableCollection<Student>();
			Students.Add(new Student() { Name = "Girish Lande", Roll = 11 });
			Students.Add(new Student() { Name = "Ajit Lande", Roll = 12 });
			Students.Add(new Student() { Name = "Suhas Walase", Roll = 13 });
		}
	}		