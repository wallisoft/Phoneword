using System.Text;
using System;
using Xamarin.Essentials;
using Android.OS;

namespace Core
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return "";
            else
                raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                if (c.Equals(" "))
                    newNumber.Append(c);
                else
                {
                    var result = GetMorseForChar(c);
                    if (result != null)
                        newNumber.Append(result);
                        newNumber.Append(".");
                }
                // otherwise we've skipped a non-numeric char
            }
            return newNumber.ToString();
        }
        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }
        static int? TranslateToNumber(char c)
        {
            if ("ABC".Contains(c))
                return 2;
            else if ("DEF".Contains(c))
                return 3;
            else if ("GHI".Contains(c))
                return 4;
            else if ("JKL".Contains(c))
                return 5;
            else if ("MNO".Contains(c))
                return 6;
            else if ("PQRS".Contains(c))
                return 7;
            else if ("TUV".Contains(c))
                return 8;
            else if ("WXYZ".Contains(c))
                return 9;
            return null;
        }
 

        internal static async void SendMorseAsync(string translatedNumber)
        {
            int sigSpeed = 4;
            int sigTime = 0;
            int pauseTime = 0;
            int count = 0;
            char msgChar;
            char msgPause;

            translatedNumber = translatedNumber.Replace("\n", " ").Replace(" .", " ").Replace(". ", " ");
            //translatedNumber = translatedNumber.Remove(translatedNumber.Length - 1, 1);

            for (count = 0; count < translatedNumber.Length -1; count++)
            {
                msgChar = translatedNumber[count];
                msgPause = translatedNumber[count+1];

                switch (msgChar)
                {
                    case '0':
                        sigTime = 1;
                        break;
                    case '1':
                        sigTime = 3;
                        break;
                }

                switch (msgPause)
                {
                    case '.':
                        count++;
                        pauseTime = 3;
                        break;
                    case ' ':
                        count++;
                        pauseTime = 7;
                        break;
                    default:
                        pauseTime = 1;
                        break;
                }

                await Flashlight.TurnOnAsync();
                SystemClock.Sleep(sigTime * 30 * sigSpeed);

                await Flashlight.TurnOffAsync();
                SystemClock.Sleep(pauseTime * 30 * sigSpeed);                    
            }
        }

        public static string GetMorseForChar(char character)
        {
            string dot = "0";
            string dash = "1";

            switch (character)
            {
                case 'A': return dot + dash;

                case 'B': return dash + dot + dot + dot;

                case 'C': return dash + dot + dash + dot;

                case 'D': return dash + dot + dot;

                case 'E': return dot;

                case 'F': return dot + dot + dash + dot;

                case 'G': return dash + dash + dot;

                case 'H': return dot + dot + dot + dot;

                case 'I': return dot + dot;

                case 'J': return dot + dash + dash + dash;

                case 'K': return dash + dot + dash;

                case 'L': return dot + dash + dot + dot;

                case 'M': return dash + dash;

                case 'N': return dash + dot;

                case 'O': return dash + dash + dash;

                case 'P': return dot + dash + dash + dot;

                case 'Q': return dash + dash + dot + dash;

                case 'R': return dot + dash + dot;

                case 'S': return dot + dot + dot;

                case 'T': return dash;

                case 'U': return dot + dot + dash;

                case 'V': return dot + dot + dot + dash;

                case 'W': return dot + dash + dash;

                case 'X': return dash + dot + dot + dash;

                case 'Y': return dash + dot + dash + dash;

                case 'Z': return dash + dash + dot + dot;

                case ' ': return "\n";

                case '_': goto case ' ';

                case '0': return dash + dash + dash + dash + dash;

                case '1': return dot + dash + dash + dash + dash;

                case '2': return dot + dot + dash + dash + dash;

                case '3': return dot + dot + dot + dash + dash;

                case '4': return dot + dot + dot + dot + dash;

                case '5': return dot + dot + dot + dot + dot;

                case '6': return dash + dot + dot + dot + dot;

                case '7': return dash + dash + dot + dot + dot;

                case '8': return dash + dash + dash + dot + dot;

                case '9': return dash + dash + dash + dash + dot;

                default: return "";
            }
        }
    }
}
