using System.Runtime.InteropServices;
using Microsoft.Win32;

[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

const int SPI_SETDESKWALLPAPER = 0x0014;
const int SPIF_UPDATEINIFILE   = 0x01;
const int SPIF_SENDCHANGE      = 0x02;

var Dir = AppDomain.CurrentDomain.BaseDirectory;
var DaysDir = Path.Combine(Dir, "Days");
var theday =  DateTime.Today.DayOfWeek;

if (!Directory.Exists(DaysDir))
{
    Console.WriteLine("Directory doesn't exist");
}

Console.WriteLine(WallpaperForDay());

string WallpaperForDay()
{
    if (theday == DayOfWeek.Sunday)
    {
        return "Sunday";
    }
    if (theday == DayOfWeek.Monday)
    {
        return "Monday";
    }
    if (theday == DayOfWeek.Tuesday)
    {
        return "Tuesday";
    }
    if (theday == DayOfWeek.Wednesday)
    {
        return "Wednesday";
    }
    if (theday == DayOfWeek.Thursday)
    {
        return "Thursday";
    }
    if (theday == DayOfWeek.Friday)
    {
        return "Friday";
    }
    if (theday == DayOfWeek.Saturday)
    {
        return "Saturday";
    }

    return "Error";
}

void ChangeWallpaper()
{
    using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", writable: true)!)
    {
        key.SetValue("WallpaperStyle", "10");
        key.SetValue("TileWallpaper", "0");
        
        // changing the wallpaper part is ai :sob: (i do not know shit about user32.dll)
    }
    if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, $@"{DaysDir}\{WallpaperForDay()}.png", SPIF_UPDATEINIFILE | SPIF_SENDCHANGE))
    {
        throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Failed to change wallpaper");
    }
}


ChangeWallpaper();
