This project will demo the use of Non modal dialogs 
This will also demo the use of using event handlers to connect events of mainwindow to the Different Dialog or UI element. 

For e.g in this example, Window has 6 buttons. 
CUT, COPY, PASTE are the user actions. 

when you click on these buttons a non modal dialog is opened which will wait for the user action. 
If user performs this operation then event is generated which will close the waiting dialog. 

THis is done by following the principle of observer pattern. 
Whenever we create the dialog we initialise it with the parent window. 
It starts listening to the event of the parent window. 
When the Dialog object is created we know for which event we are waiting for and hence dialog will only 
register to that particulat event of the parent window. 
Whenever that event occurs child dialog will get notified and it can close itself. 

