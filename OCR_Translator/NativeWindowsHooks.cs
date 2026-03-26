using System.Runtime.InteropServices;

namespace OCR_Translator;

/// <summary>
/// Contains native windows hooks form the winusr.h API
/// </summary>
public static class NativeWindowsHooks
{
    // WH_KEYBOARD_LL needed for LowLeveleyboardProc and SetWindowsHookEx
    private const int WH_KEYBOARD_LL = 13;

    // needed for SetWindowsHookEx function
    // wParam = nonzero if some message is sent by the current process, 0 if null
    // lParam = details about the message that was sent (keycode[?])
    // this is the LowLevelKeyboardProc
    public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);
    
    // attachess a hook to a given process and listens for event on the thread it's ran on
    // utilises the WH_KEYBOARD_LL hook
    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

    // unhooks once the program is shut down, so the general system settings remain unchanged
    [DllImport("user32.dll")]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    // no input blocking behaviour
    [DllImport("user32.dll")]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

}
