using RemoteControl.Actions;

namespace RemoteControl.Server.Helpers
{
    public class KeyHelper
    {
        private static Keyboard keyboard;

        public static void Handle(string key)
        {
            keyboard.PressMediaKey(key);
        }
    }
}
