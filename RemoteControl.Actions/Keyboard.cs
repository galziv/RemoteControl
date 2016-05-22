using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace RemoteControl.Actions
{
    public class Keyboard
    {
        private InputSimulator simulator;
        private Dictionary<string, VirtualKeyCode> mediaKeyMap;
        private KeyboardHelper keyboardHelper;
        private readonly string HebrewLanguageCode = "he";

        public Keyboard()
        {
            this.simulator = new InputSimulator();
            this.keyboardHelper = new KeyboardHelper();

            mediaKeyMap = new Dictionary<string, VirtualKeyCode>();
            mediaKeyMap.Add("play-pause", VirtualKeyCode.MEDIA_PLAY_PAUSE);
            mediaKeyMap.Add("stop", VirtualKeyCode.MEDIA_STOP);
            mediaKeyMap.Add("prev", VirtualKeyCode.MEDIA_PREV_TRACK);
            mediaKeyMap.Add("next", VirtualKeyCode.MEDIA_NEXT_TRACK);
        }

        [Flags]
        public enum KeyboardEventFlags
        {
            A = 0x00000030,
            Mute = 0x000000AD,
        }

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        public void ChangeLanguage()
        {
            this.simulator.Keyboard.KeyDown(VirtualKeyCode.LMENU);
            this.simulator.Keyboard.KeyPress(VirtualKeyCode.LSHIFT);
            this.simulator.Keyboard.KeyUp(VirtualKeyCode.LMENU);
        }

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

        public void PressMediaKey(string key)
        {
            if (mediaKeyMap.ContainsKey(key))
            {
                var mediaKey = mediaKeyMap[key];
                this.simulator.Keyboard.KeyPress(mediaKey);
            }
        }

        public void PressKey(string key)
        {
            if (keyboardHelper.IsHebrew(key[0]))
            {
                CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

                if (!currentCulture.TwoLetterISOLanguageName.ToLower().Equals(HebrewLanguageCode))
                {
                    this.ChangeLanguage();
                }
            }
            else {
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
}
