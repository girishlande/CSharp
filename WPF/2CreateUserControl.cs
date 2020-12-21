 
 To make the code manageable we can split it in multiple different pages. 
 One way is to create User Controls. Right click on the project, click add 	and then select User control (WPF)
 Now we can embed this set of controls in another control as follows 
 
 <TabItem Header="UserControl">
        <local:UserControl1></local:UserControl1>
 </TabItem>