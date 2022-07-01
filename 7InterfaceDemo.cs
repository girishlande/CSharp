// What is use of interfaces 
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;

namespace ConsoleApp2
{
    public class Video {
        public String Title { get; set; }
    }

    class VideoEncoder
    {
        IList<INotificationChannel> _Channels = new List<INotificationChannel>();

        public void EncodeVideo(Video video)
        {
            Console.WriteLine("Encoding video.."  +video.Title);
            Thread.Sleep(2000);
            NotifyChannels(video);
        }

        public void RegisterChannel(INotificationChannel channel)
        {
            _Channels.Add(channel);
        }

        void NotifyChannels(Video v)
        {
            foreach(var c in _Channels)
            {
                c.VideoEncoded(v);
            }
        }
    }

    public interface INotificationChannel {
        public void VideoEncoded(Video video);
    }

    class MailService : INotificationChannel{
        public void VideoEncoded(Video video)
        {
            Console.WriteLine("Sending email ..." + video.Title);
            
        }
    }

    class SMSService : INotificationChannel
    {
        public void VideoEncoded(Video video)
        {
            Console.WriteLine("Sending SMS ..." + video.Title);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var v = new Video();
            var e = new VideoEncoder();
            e.RegisterChannel(new MailService());
            e.RegisterChannel(new SMSService());
            e.EncodeVideo(v);
            
        }
    }
}
