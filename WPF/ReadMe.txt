// ----------------------------------------------
// To get console output for WPF application 
// ----------------------------------------------

Right click on the project, "Properties", "Application" tab, 
change "Output Type" to "Console Application", and then it will also have a console.


// -------------------------------------------
// WPF application creating element anywhere 
// -------------------------------------------
Create button or any controls anywhere using canvas

// ---------------------------------------
// Use styles for all controls in WPF
// -----------------------------------------
Add following styles before grid element
<Window.Resources>
        <Style TargetType="TextBlock">
            <Setter Property="Foreground" Value="Gray" />
            <Setter Property="FontSize" Value="24" />
        </Style>
    </Window.Resources>

>>For application wide styles
------------------------------------
App.xaml

<Application.Resources>
        <Style TargetType="TextBlock">
            <Setter Property="Foreground" Value="Gray" />
            <Setter Property="FontSize" Value="24" />
        </Style>
    </Application.Resources>
	
>>Explicit styles
--------------------------
<Window.Resources>
        <Style x:Key="HeaderStyle" TargetType="TextBlock">
            <Setter Property="Foreground" Value="Gray" />
            <Setter Property="FontSize" Value="24" />
        </Style>
    </Window.Resources>
    <StackPanel Margin="10">
        <TextBlock>Header 1</TextBlock>
        <TextBlock Style="{StaticResource HeaderStyle}">Header 2</TextBlock>
        <TextBlock>Header 3</TextBlock>
    </StackPanel>

	
>> Apply style to all buttons
-----------------------------------
<Window.Resources>
        <Style TargetType="Button">
            <Style.Triggers>
                <Trigger Property="IsPressed" Value="True">
                    <Setter Property="Effect">
                        <Setter.Value>
                            <DropShadowEffect BlurRadius="10" ShadowDepth="5"/>
                        </Setter.Value>
                    </Setter>
                </Trigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>
	
// --------------------------------------------------------------------------------------	
// Changed style of listview TO make sure that there is no highlighting of the elements
// --------------------------------------------------------------------------------------	
<Setter Property="Template">
                            <Setter.Value>
                                <ControlTemplate TargetType="{x:Type ListViewItem}">
                                    <Border
                         BorderBrush="Transparent"
                         BorderThickness="0"
                         Background="{TemplateBinding Background}">
                                        <GridViewRowPresenter HorizontalAlignment="Stretch" VerticalAlignment="{TemplateBinding VerticalContentAlignment}" Width="Auto" Margin="0" Content="{TemplateBinding Content}"/>
                                    </Border>
                                </ControlTemplate>
                            </Setter.Value>
                        </Setter>
						