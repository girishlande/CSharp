When you want to implement view model you should derive it from INotifyPropertyChanged.

Then you should create property for each member that you want to expose to View. 
When view manipulates this member then automatically your property values is updated. 
And then you can notify world that property has changed


public class DCViewModel : INotifyPropertyChanged {
	
	 public event PropertyChangedEventHandler PropertyChanged;
	 
	    private bool _autoStartChanged;

        public bool AutoStartSequence
        {
            get { return _autoStartChanged; }
            set { _autoStartChanged = value; OnPropertyChanged("AutoStartSequence"); }
        }
		
		private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(v));
        }
		
		
}