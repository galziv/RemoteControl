using RemoteControl.Actions;

namespace RemoteControl.Server.Helpers
{
    public class SysHelper
    {
        private static Keyboard keyboard;

        public static void Handle(string key)
        {
            switch (key)
            {
                case "change-language":
                    keyboard.ChangeLanguage();
                    break;
            }
        }
    }
}
