using System.Runtime.InteropServices;
using Microsoft.Win32;

[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

const int SPI_SETDESKWALLPAPER = 0x0014;
const int SPIF_UPDATEINIFILE = 0x01;
const int SPIF_SENDCHANGE = 0x02;

Console.Title = "DayWallPaperChanger";
Console.WriteLine("This is DayWallPaperChanger so don't be afraid of the popup!");

var Dir = AppDomain.CurrentDomain.BaseDirectory;
var DaysDir = Path.Combine(Dir, "Days");
var theday = DateTime.Today.DayOfWeek.ToString();

if (!Directory.Exists(DaysDir))
{
    Console.WriteLine("Directory doesn't exist");
}

else
{
    ChangeWallpaper();
}

void ChangeWallpaper()
{
    using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", writable: true)!)
    {
        key.SetValue("WallpaperStyle", "10");
        key.SetValue("TileWallpaper", "0");

        // changing the wallpaper part is ai :sob: (i do not know shit about user32.dll)
    }
    if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, $@"{DaysDir}\{theday}.png", SPIF_UPDATEINIFILE | SPIF_SENDCHANGE))
    {
        throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Failed to change wallpaper");
    }
}

