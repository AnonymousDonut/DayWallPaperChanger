using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DaysWallpaperChanger;

public class Methods
{
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
    
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    
    public readonly static IntPtr handle = GetConsoleWindow();
    public const int SW_HIDE = 0;
    public const int SW_SHOW = 5;
    const int SPI_SETDESKWALLPAPER = 0x0014;
    const int SPIF_UPDATEINIFILE = 0x01;
    const int SPIF_SENDCHANGE = 0x02;
    public static void ChangeWallpaper(string Path, string Day)
    {
        using (var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", writable: true)!)
        {
            key.SetValue("WallpaperStyle", "10");
            key.SetValue("TileWallpaper", "0");
        }
        if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, $@"{Path}\{Day}.png", SPIF_UPDATEINIFILE | SPIF_SENDCHANGE))
        {
            throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), "Failed to change wallpaper");
        }
    }

    public static void HideWindow()
    {
        ShowWindow(handle, SW_HIDE);
    }

    public static void ShowWindow()
    {
        ShowWindow(handle, SW_SHOW);
    }
}
