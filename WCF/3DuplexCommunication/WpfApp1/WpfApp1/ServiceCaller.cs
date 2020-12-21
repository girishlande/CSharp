using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using WpfApp1.ServiceReference1;
using System.Diagnostics;

namespace WpfApp1
{

    public class DataArgs : EventArgs
    {
        public string Data { get; set; }
    }

    public class OnlineStatusArgs : EventArgs
    {
        public bool Onlineflag { get; set; }
    }

    public class ServiceCaller : IGirishCallback
    {
        GirishClient client = null;
        System.Timers.Timer KeepaliveTimer = null;
        public event EventHandler<DataArgs> ReceivedData;
        public event EventHandler<DataArgs> ErrorOccured;
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
                    client = new GirishClient(context);
                }
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new DataArgs() { Data = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        void DisposeClient()
        {
            ValidClientState = false;
            client.Abort();
            client = null;
        }

        public void GetData(int data)
        {
            InitialiseClient();
            try
            {
                client.GetData(data);
            }
            catch (Exception ex)
            {
                DisposeClient();
                ErrorOccured?.Invoke(this, new DataArgs() { Data = "Girish Exception occured:" + ex.Message });
                Console.WriteLine("Girish Exception:" + ex.Message);
            }
        }

        public void SendDataCallback(string adata)
        {
            DataArgs dataargs = new DataArgs() { Data = adata };
            ReceivedData(this, dataargs);
        }

        private void KeepAlive(Object source, EventArgs args)
        {
            InitialiseClient();
            try
            {
                if (client!=null)
                client.IsOnline();
            }
            catch (Exception ex)
            {
                DisposeClient();
                ConnectionStatusChanged?.Invoke(this, new OnlineStatusArgs() { Onlineflag = ValidClientState });
            }
        }

        public void SendOnlineCallback(bool aflag)
        {
             ConnectionStatusChanged?.Invoke(this, new OnlineStatusArgs() { Onlineflag = aflag });
        }
    }
}
