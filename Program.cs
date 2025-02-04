var videoEncorder = new VideoEncorder();
videoEncorder.VideoEncordedEvent += VideoEncorded;
videoEncorder.Encorde(
    ".mp4", 
    new VideoEncorder.Video{Name= "Video1.pmg4", Format =".pmg4"},
    Console.WriteLine);

void VideoEncorded(VideoEncorder.Video video)
{
    MailSender mailSender = new MailSender();
    mailSender.Send(video.Name);
}

class MailSender
{
    public void Send(string name)
    {
        Console.WriteLine($"Sending email regarding video encorder: {name}");
    }
}

class VideoEncorder
{
    public delegate void VideoEncorded(Video video);
    public delegate void EncordFormat(string name);
    public event VideoEncorded? VideoEncordedEvent;

    public void Encorde(string format, Video video, Action<string> action)
    {
        switch(format)
        {
            case ".mp4":
            {
                EncordeBuilder(Mp4, video, action);
            }
            break;
            case ".vevo":
            {
                EncordeBuilder(Vevo, video, action);
            }
            break;
        }
    }
    private void EncordeBuilder(Func<Video,Action<string>, Video> func, Video video, Action<string> action)
    {
         var encrodedVideo = func(video, action);
         Console.WriteLine($"Video {video.Name} has be encorded to {video.Format} format");
         VideoEncordedEvent?.Invoke(video);
    }

    private static Func<Video, Action<string>, Video> Mp4 = (video, callback) => 
    {
        video.ChangeFormart(".mp4");
        foreach(var count in Enumerable.Range(1,100))
        {
            Task.Delay(1000000);
            callback.Invoke($"{count} %");
        }
        return video;
    };

    private static Func<Video, Action<string>, Video> Vevo = (video, callback) => 
    {
        video.ChangeFormart(".vevo");
        foreach(var count in Enumerable.Range(1,100))
        {
            Task.Delay(1000000);
            callback.Invoke($"{count} %");
        }
        return video;
    };

    public class Video
    {
        public string Name {get;set;}
        public string Format {get;set;}

        public void ChangeFormart(string newFormat)
        {
            Format = newFormat;
        }
    }
}

class TimesTable 
{
   public void Row(Func<int,int> calculate, int left, int right)
   {
       for (var i = 0; i <= left; i++)
       {
          var output = calculate.Invoke(i * right);
          Console.WriteLine(output);
       }
   }
}