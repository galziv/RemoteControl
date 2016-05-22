using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace RemoteControl.Actions
{
    public class KeyboardHelper
    {
        public Dictionary<string, Dictionary<string, VirtualKeyCode>> LanguageKeyMapDictionary { get; set; }

        public Dictionary<string,VirtualKeyCode> HebrewMap { get; set; }

        public KeyboardHelper()
        {
            this.LanguageKeyMapDictionary = new Dictionary<string, Dictionary<string, VirtualKeyCode>>(2);

            this.LanguageKeyMapDictionary.Add("he", new Dictionary<string, VirtualKeyCode>(24));

            this.HebrewMap = this.LanguageKeyMapDictionary["he"];

            this.HebrewMap.Add("ש", VirtualKeyCode.VK_A);
            this.HebrewMap.Add("נ", VirtualKeyCode.VK_B);
            this.HebrewMap.Add("ב", VirtualKeyCode.VK_C);
            this.HebrewMap.Add("ג", VirtualKeyCode.VK_D);
            this.HebrewMap.Add("ק", VirtualKeyCode.VK_E);
            this.HebrewMap.Add("כ", VirtualKeyCode.VK_F);
            this.HebrewMap.Add("ע", VirtualKeyCode.VK_G);
            this.HebrewMap.Add("י", VirtualKeyCode.VK_H);
            this.HebrewMap.Add("ח", VirtualKeyCode.VK_J);
            this.HebrewMap.Add("ל", VirtualKeyCode.VK_K);
            this.HebrewMap.Add("ך", VirtualKeyCode.VK_L);
            this.HebrewMap.Add("צ", VirtualKeyCode.VK_M);
            this.HebrewMap.Add("נ", VirtualKeyCode.VK_N);
            this.HebrewMap.Add("ם", VirtualKeyCode.VK_O);
            this.HebrewMap.Add("פ", VirtualKeyCode.VK_P);
            this.HebrewMap.Add("/", VirtualKeyCode.VK_Q);
            this.HebrewMap.Add("ר", VirtualKeyCode.VK_R);
            this.HebrewMap.Add("ד", VirtualKeyCode.VK_S);
            this.HebrewMap.Add("א", VirtualKeyCode.VK_T);
            this.HebrewMap.Add("ו", VirtualKeyCode.VK_U);
            this.HebrewMap.Add("ה", VirtualKeyCode.VK_V);
            this.HebrewMap.Add("'", VirtualKeyCode.VK_W);
            this.HebrewMap.Add("ס", VirtualKeyCode.VK_X);
            this.HebrewMap.Add("ט", VirtualKeyCode.VK_Y);
            this.HebrewMap.Add("ז", VirtualKeyCode.VK_Z);
        }

        public bool IsEnglish(char character)
        {
            return "abcdefghijklmnopqrstuvwxyz".Contains(character.ToString().ToLower());
        }

        public bool IsHebrew(char character)
        {
            return "אבגדהוזחטיכלמנסעפצקרשת".Contains(character);
        }
    }
}
