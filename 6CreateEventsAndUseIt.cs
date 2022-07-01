// How custom events can be written in C# and how to use it. 

using System;
using System.Threading;

namespace ConsoleApp1
{
    class Video {
        public String Title { get; set; }
    }
    class VideoArgs : EventArgs {
        public Video myvideo { get; set; }
    }


    class VideoEncoder {
        public event EventHandler<VideoArgs> VideoEncoded;
        public void EncodeVideo(Video video)
        {
            Console.WriteLine("Encoding video:"+video.Title);
            Thread.Sleep(2000);
            VideoEncoded(this, new VideoArgs { myvideo = video });
        }
    }

    class MailService { 
        public void OnVideoEncoded(object source, VideoArgs args)
        {
            Console.WriteLine("Sending mail...."+args.myvideo.Title);
        }
    }

    class MessageService {
        public void SendMesssage(object source, VideoArgs args)
        {
            Console.WriteLine("Sending SMS...." + args.myvideo.Title);
        }
    }


    class Program
    {
        static void Main()
        {
            var video = new Video { Title = "GirishVideo" };
            var videoencoder = new VideoEncoder();

            var mailservice = new MailService();
            videoencoder.VideoEncoded += mailservice.OnVideoEncoded;

            var msgservice = new MessageService();
            videoencoder.VideoEncoded += msgservice.SendMesssage;

            videoencoder.EncodeVideo(video);
        }
    }
}
