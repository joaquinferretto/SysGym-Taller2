using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using Thread = System.Threading.Thread;

/* Abre los diseñadores en una instancia aislada de Visual Studio y captura su ventana. */
internal static class DesignerCheck
{
    [DllImport("user32.dll")] private static extern bool SetForegroundWindow(IntPtr hwnd);
    [DllImport("user32.dll")] private static extern bool GetWindowRect(IntPtr hwnd, out Rect rect);
    [DllImport("user32.dll")] private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdc, uint flags);
    private struct Rect { public int Left, Top, Right, Bottom; }

    /* Reintenta llamadas COM cuando Visual Studio está ocupado cargando el proyecto. */
    private static T Retry<T>(Func<T> action)
    {
        for (int i = 0; ; i++)
        {
            try { return action(); }
            catch (COMException) { if (i >= 60) throw; Thread.Sleep(500); }
        }
    }

    /* Ejecuta una acción COM sin resultado con la misma política de reintentos. */
    private static void Do(Action action) { Retry(() => { action(); return true; }); }

    /* Captura la ventana de la instancia usada para verificar el diseñador. */
    private static void Capture(DTE dte, string path)
    {
        var hwnd = Retry(() => dte.MainWindow.HWnd);
        SetForegroundWindow(hwnd); Thread.Sleep(500);
        Rect rect; GetWindowRect(hwnd, out rect);
        using (var bitmap = new Bitmap(rect.Right - rect.Left, rect.Bottom - rect.Top))
        using (var graphics = Graphics.FromImage(bitmap))
        {
            var dc = graphics.GetHdc();
            try { PrintWindow(hwnd, dc, 2); } finally { graphics.ReleaseHdc(dc); }
            bitmap.Save(path, ImageFormat.Png);
        }
    }

    /* Carga cada archivo visual y registra su ventana real de diseño. */
    [STAThread]
    private static int Main(string[] args)
    {
        DTE dte = null;
        try
        {
            var solution = Path.GetFullPath(args[0]);
            var shots = Path.GetFullPath(args[1]); Directory.CreateDirectory(shots);
            dte = (DTE)Activator.CreateInstance(Type.GetTypeFromProgID("VisualStudio.DTE.18.0"));
            Thread.Sleep(10000);
            Do(() => dte.SuppressUI = true); Do(() => dte.UserControl = false);
            Do(() => dte.MainWindow.Visible = true); Do(() => dte.MainWindow.WindowState = vsWindowState.vsWindowStateMaximize);
            Do(() => dte.Solution.Open(solution)); Thread.Sleep(10000);
            Console.WriteLine("SOLUTION=" + Retry(() => dte.Solution.FullName));
            int count = 0;
            foreach (var file in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(solution), "capaVisual"), "*.Designer.cs", SearchOption.AllDirectories).OrderBy(f => f))
            {
                var source = file.Replace(".Designer.cs", ".cs");
                var item = Retry(() => dte.Solution.FindProjectItem(source));
                var window = Retry(() => item.Open(Constants.vsViewKindDesigner));
                Do(() => window.Activate()); Thread.Sleep(2500);
                var caption = Retry(() => window.Caption);
                Console.WriteLine("DESIGNER=" + Path.GetFileName(source) + "|" + caption + "|" + Retry(() => window.Kind));
                Capture(dte, Path.Combine(shots, Path.GetFileNameWithoutExtension(source) + ".png"));
                if (!caption.Contains("Dise") && !caption.Contains("Design")) throw new Exception("No es ventana de diseño: " + caption);
                Do(() => window.Close(vsSaveChanges.vsSaveChangesNo)); count++;
            }
            Console.WriteLine("TOTAL_DESIGNERS=" + count); return count == 17 ? 0 : 1;
        }
        catch(Exception ex) { Console.WriteLine(ex); return 1; }
        finally { if (dte != null) { try { Do(() => dte.Solution.Close(false)); Do(() => dte.Quit()); } catch(Exception ex) { Console.WriteLine(ex.Message); } } }
    }
}
