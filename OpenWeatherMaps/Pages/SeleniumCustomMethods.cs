using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMaps.Pages
{
    //Extension Method Rules
    //1. Class needs to be static access specifier
    //2. Methods should be also static 

    public static class SeleniumCustomMethods
    {
        //1. Method should get the locator
        //2. Start getting the type of identifier
        //3. Perform operation on the locator 

        public static void Click(IWebElement element)
        {
            element.Click();
        }

        public static void EnterText(IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }

        public static void selectDropdownByText(IWebElement element, string text)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByText(text);
        }

        public static void selectDrodpownByValue(IWebElement element, string value)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByValue(value);
        }

        public static void MultiSelectElement(IWebElement element, string[] values)
        {
            var multiSelect = new SelectElement(element);

            foreach (var value in values)
            {
                multiSelect.SelectByValue(value);
            }
        }

        public static List<string> GetAllSelectedLists(IWebElement element)
        {
            var options = new List<string>();
            var multiSelect = new SelectElement(element);

            var selectedOption = multiSelect.AllSelectedOptions;

            foreach (IWebElement option in selectedOption)
            {
                options.Add(option.Text);
            }
            return options;

        }


    }

}
