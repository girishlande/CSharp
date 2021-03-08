
Following link has useful data about UI threads and how to update UI from different threads.

http://www.albahari.com/threading/part2.aspx#_Rich_Client_Applications


// ----------------------------
//  Want to print some logs ?
// ----------------------------
Use Debug.WriteLine() for printing log information 

// ----------------------------
// Use of themes in WPF 
// ----------------------------
You can try using NuGet to install theme. 
From VS, go to Tools>NuGet Package Manager>Package Manager Cnsole and write the following command to install theme PM> Install-Package WPF.Themes this will make a directory in your project called Themes and download all the themes. it will also add ResourceDirectory to yourApp.xaml where you can select the desired theme. Now you just need to drag and drop the tools when you run your app you will see the theme.

Install-Package WPF.Themes	


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