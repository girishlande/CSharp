
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


// ----------------------------
// To get size of the class
// ----------------------------
[StructLayout(LayoutKind.Sequential)]
class A {}

 A a1 = new A();
 Console.WriteLine("size of a:" + Marshal.SizeOf(a1));
    