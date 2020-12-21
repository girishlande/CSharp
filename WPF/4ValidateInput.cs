 
 // IN XAML file 
 <TextBox x:Name="text1" HorizontalAlignment="Left" Height="23" Margin="111,250,0,0" TextWrapping="Wrap" Text="" VerticalAlignment="Top" Width="120" TextChanged="text1_TextChanged" PreviewTextInput="NumberValidationTextBox" ToolTip="Enter value between 40 and 80"/>
 
 // In SOURCE file.
  private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

       private void text1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t1 = sender as TextBox;
            string numberStr = t1.Text.ToString();
            int number;
            bool isParsable = Int32.TryParse(numberStr, out number);
            if (isParsable)
            {
                Console.WriteLine("Text changed:" + t1.Text);
                if (number >= 40 && number <= 80 )
                    t1.Background = new SolidColorBrush(Colors.Green);
                else
                    t1.Background = new SolidColorBrush(Colors.Red);
            }

        }