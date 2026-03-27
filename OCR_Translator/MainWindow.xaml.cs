using OCR_Translator.Model;
using OCR_Translator.Services;
using OCR_Translator.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using static OCR_Translator.NativeWindowsHooks;
using OCR_Translator.Interfaces;
using OCR_Translator.View;
using System.Configuration;
using OCR_Translator.Config;
using OCR_Translator.ViewModel;
using System.Security.Policy;


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

        public MainWindow()
        {
            InitializeComponent();
            // @see: https://stackoverflow.com/questions/1556182/finding-the-handle-to-a-wpf-window
            // gets the current window handle (the main, transparent window)
            windowHandle = new WindowInteropHelper(this).Handle;
            // hooking into the current window
            InitialiseWindowHook(windowHandle);

            var vm = (MainWindowViewModel)this.DataContext;

            // hiding the window when the event is invoked
            vm.OnSubmitClicked += () => HideWindow();
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
                        var vm = (MainWindowViewModel)this.DataContext;
                        // calling the method directly from the view model
                        // change this to an event?
                        vm.ToggleOverlayVisibility();
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

        private void HideWindow()
        {
            StartupWindow.Hide();
        }
    }
}