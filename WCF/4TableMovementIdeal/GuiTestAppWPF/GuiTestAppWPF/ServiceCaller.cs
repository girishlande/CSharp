using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

using GuiTestAppWPF.ServiceReference1;

namespace GuiTestAppWPF
{
    public class DataArgs : EventArgs
    {
        public int TablePosition { get; set; }
    }

    public class OnlineStatusArgs : EventArgs
    {
        public bool Onlineflag { get; set; }
    }

    public class MessageArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public class ServiceCaller : ITableMoverCallback
    {
        TableMoverClient client = null;

        System.Timers.Timer KeepaliveTimer = null;
        public event EventHandler<DataArgs> RecievedTablePosition;
        public event EventHandler<MessageArgs> ErrorOccured;
        public event EventHandler<OnlineStatusArgs> ConnectionStatusChanged;

        public bool ValidClientState { get; set; }

        public ServiceCaller()
        {
            InitialiseClient();
            InitialiseTimer();
        }

        public void InitialiseTimer()
        {
            if (KeepaliveTimer == null)
            {
                KeepaliveTimer = new System.Timers.Timer();
                KeepaliveTimer.Interval = 1000;
                KeepaliveTimer.Elapsed += KeepAlive;
                KeepaliveTimer.Start();
            }
        }

        void InitialiseClient()
        {
            ValidClientState = true;
            try
            {
                if (client == null)
                {
                    var context = new InstanceContext(this);
                    client = new TableMoverClient(context);
                }
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new MessageArgs() { Message = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        void DisposeClient()
        {
            ValidClientState = false;
            client.Abort();
            client = null;
        }

        public void MoveTable(int position)
        {
            InitialiseClient();
            try
            {
                client.MoveTable(position);
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new MessageArgs() { Message = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        public void GetPosition()
        {
            InitialiseClient();
            try
            {
                client.GetPosition();
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new MessageArgs() { Message = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        private void KeepAlive(Object source, EventArgs args)
        {
            InitialiseClient();
            try
            {
                if (client != null)
                    client.IsOnline();
            }
            catch (Exception ex)
            {
                DisposeClient();
                ConnectionStatusChanged?.Invoke(this, new OnlineStatusArgs() { Onlineflag = ValidClientState });
            }
        }


        public void SendTablePosition(int position)
        {
            DataArgs dataargs = new DataArgs() { TablePosition = position };
            RecievedTablePosition(this, dataargs);
        }

        public void SendOnlineStatus(bool flag)
        {
            ConnectionStatusChanged?.Invoke(this, new OnlineStatusArgs() { Onlineflag = flag });
        }
    }
}
