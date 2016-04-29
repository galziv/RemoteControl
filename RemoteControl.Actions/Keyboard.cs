using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace RemoteControl.Actions
{
    public class Keyboard
    {
        private InputSimulator simulator;

        public Keyboard()
        {
            this.simulator = new InputSimulator();
        }

        [Flags]
        public enum KeyboardEventFlags
        {
            A = 0x00000030,
            Mute = 0x000000AD,
        }

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public void PressMute()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
        }

        public void PressVolumeUp()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
        }

        public void PressVolumeDown()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
        }

        public void PressUp()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.UP);
        }

        public void PressDown()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.DOWN);
        }

        public void PressLeft()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.LEFT);
        }

        public void PressRight()
        {
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
        }

        public void PressKey(string key)
        {
            var names = Enum.GetNames(typeof(VirtualKeyCode));
            key = key.ToUpper();

            var toPress = names.FirstOrDefault(x =>
            {
                var name = x.ToString();
                var splitted = name.Split('_');

                return splitted.Length == 1 ? splitted[0].Equals(key) : splitted[1].Equals(key);
            });

            if (toPress != null)
            {
                VirtualKeyCode value = (VirtualKeyCode)Enum.Parse(typeof(VirtualKeyCode), toPress);
                this.simulator.Keyboard.KeyPress(value);
            }
        }
    }
}
