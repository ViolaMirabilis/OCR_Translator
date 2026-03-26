using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Security.Policy;

namespace OCR_Translator
{
    public partial class MainWindow : Window
    {
        #region Native Windows Hooks
        // responsible for registering KEY DOWN event
        int WM_KEYDOWN = 0x0100;
        // defined key responsible for opening up the overlay (pg up in this case)
        int VK_Prior = 0x21;
        // WH_KEYBOARD_LL needed for LowLeveleyboardProc and SetWindowsHookEx
        private const int WH_KEYBOARD_LL = 13;

        IntPtr windowHandle;
        static IntPtr hHook = 0;
        // the key down basically
        HookProc KeyboardHookProcedure;

        // needed for SetWindowsHookEx function
        // wParam = nonzero if some message is sent by the current process, 0 if null
        // lParam = details about the message that was sent (keycode[?])
        // this is the LowLevelKeyboardProc
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // attachess a hook to a given process and listens for event on the thread it's ran on
        // utilises the WH_KEYBOARD_LL hook
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        // unhooks once the program is shut down, so the general system settings remain unchanged
        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // no input blocking behaviour
        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // needed for the lParam in the CallNextHookEx, to determine that the pressed key is right (i.e. "user pressed page up? do this")
        private struct KBDLLHOOKSTRUCT(uint vkCode, uint scanCode, uint flags, uint time, ulong dwExtraInfo);

        #endregion
        // overlay visibility variable
        private bool _isVisible = true;
        
        public MainWindow()
        {
            InitializeComponent();
            // @see: https://stackoverflow.com/questions/1556182/finding-the-handle-to-a-wpf-window
            // gets the current window handle (the main, transparent window)
            windowHandle = new WindowInteropHelper(this).Handle;
            // hooking into the current window
            InitialiseWindowHook(windowHandle);
        }

        // @see: https://www.betaarchive.com/wiki/index.php/Microsoft_KB_Archive/318804
        private IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0)
            {
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }
            else
            {
                // do some code
                if (wParam == WM_KEYDOWN)
                {
                    // holds the VK value of the currently pressed key.
                    // this helped a lot: https://www.reddit.com/r/csharp/comments/876cq6/requesting_help_trying_to_understand_something/
                    int pressedKeyVK = Marshal.ReadInt32(lParam);

                    // if currently pressed key is equal to the key defined by us (page up in this case)
                    if (pressedKeyVK == VK_Prior)
                    {
                        //MessageBox.Show("THE BUTTON WORKS!");
                        ToggleOverlayVisibility(_isVisible);
                    }
                }
                return CallNextHookEx(hHook, nCode, wParam, lParam);
            }
        }
        private void InitialiseWindowHook(IntPtr windowHandle)
        {
            if (hHook == 0)
            {
                // passing in the delegate
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                // passing in the "current thread" as 0, so it listens for input globally
                hHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, 0, 0);
                // the value of hHook should be changed now
                if (hHook == 0)
                {
                    MessageBox.Show("SetWindowsHookEx failed");
                    return;
                }
            }
        }
        private void ToggleOverlayVisibility(bool isVisible)
        {
            _isVisible = !_isVisible;
            if (_isVisible)
                Overlay.Show();
            else
                Overlay.Hide();
        }

        

    }
}