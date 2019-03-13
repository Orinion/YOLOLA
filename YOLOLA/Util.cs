using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace FutureNNAimbot
{
    public static class Util
    {
        public static bool IsKeyPressed(Keys key)
        {
            return User32.GetAsyncKeyState(key) != 0;
        }

        public static bool IsKeyToggled(Keys key)
        {
            return User32.GetAsyncKeyState(key) == -32767;
        }
    }

    public static class VirtualMouse
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }
        public static void MoveTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
        }
     
    }

}
