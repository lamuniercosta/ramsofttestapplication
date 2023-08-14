namespace RamSoftTaskApplication.API;

public class Program
{
    public static void Main(string[] args)
    {
        var app = Startup.InitiateApp(args);

        app.Run();
    }
}