using DaysWallpaperChanger;

Methods.HideWindow();

var Dir = AppDomain.CurrentDomain.BaseDirectory;
var DaysDir = Path.Combine(Dir, "Days");
var theday = DateTime.Today.DayOfWeek.ToString();

if (!Directory.Exists(DaysDir))
{
    Methods.ShowWindow();
    Console.WriteLine("Directory doesn't exist");
    Console.ReadLine();
}

else
{
    Methods.ChangeWallpaper(DaysDir, theday);
}

