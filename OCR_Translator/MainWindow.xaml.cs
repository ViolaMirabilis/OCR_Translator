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
using static OCR_Translator.NativeWindowsHooks;

namespace OCR_Translator
{
    public partial class MainWindow : Window
    {
        #region Variables for the NativeWindowsHooks
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

        #region Methods related to hooking into the window
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
        #endregion
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