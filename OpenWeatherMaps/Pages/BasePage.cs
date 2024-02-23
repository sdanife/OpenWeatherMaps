using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMaps.Pages
{
    public class BasePage
    {
        protected static IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }


        public void GoToUrl(string url)
        {
            Driver.Url = url;
        }

        public IWebElement FindElement(By by)
        {
            return Driver.FindElement(by);
        }

        public class RandomGenerator
        {
            // Instantiate random number generator.
            // It is better to keep a single Random instance
            // and keep using Next on the same instance.
            private readonly Random _random = new Random();

            // Generates a random number within a range.
            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }

            // Generates a random string with a given size.
            public string RandomString(int size, bool lowerCase = false)
            {
                var builder = new StringBuilder(size);

                // Unicode/ASCII Letters are divided into two blocks
                // (Letters 65–90 / 97–122):
                // The first group containing the uppercase letters and
                // the second group containing the lowercase.

                // char is a single Unicode character
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; // A...Z or a..z: length = 26

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }

            // Generates a random password.
            // 4-LowerCase + 4-Digits + 2-UpperCase
            public string RandomPassword()
            {
                var passwordBuilder = new StringBuilder();

                // 4-Letters lower case
                passwordBuilder.Append(RandomString(4, true));

                // 4-Digits between 1000 and 9999
                passwordBuilder.Append(RandomNumber(1000, 9999));

                // 2-Letters upper case
                passwordBuilder.Append(RandomString(2));
                return passwordBuilder.ToString();
            }

            /// <summary>
            /// Gets a string from the English alphabet at random
            /// </summary>
            public static string GenerateRandomAlphabetString(int length)
            {
                string allowedChars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var rnd = SeedRandom();

                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                {
                    chars[i] = allowedChars[rnd.Next(allowedChars.Length)];
                }

                return new string(chars);
            }

            private static Random SeedRandom()
            {
                return new Random(Guid.NewGuid().GetHashCode());
            }

            public string GenerateRandomEmail()
            {
                return string.Format("{0}@{1}.com", GenerateRandomAlphabetString(10), GenerateRandomAlphabetString(10));
            }
        }



    }


}
