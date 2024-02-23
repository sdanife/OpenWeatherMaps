using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMaps.Pages
{
    public class WeatherMapDashPage : BasePage
    {

        private const string base_URL = "https://api.openweathermap.org";
        private const string endpoint = "/data/2.5/weather?";

        private By weatherSearchInput = By.XPath("(//input[@name='q'])[1]");
        private By apiKeyMenuLink = By.XPath("//*[@id='myTab']/li[3]/a");
        private By apiKey = By.XPath("//table[@class='material_table api-keys']//pre");
        private By weatherTemp = By.XPath("//span[@class='badge badge-info']");

        private By invalidSearchText = By.XPath("//div[text()='Not found']");




        public WeatherMapDashPage(IWebDriver driver) : base(driver)
        {
        }


        public string getAPIKey()
        {
            FindElement(apiKeyMenuLink).Click();
            var apiKeyNo = FindElement(apiKey).Text;
            Console.WriteLine("The API Key No is : " + apiKeyNo);

            return apiKeyNo;
        }

        public void searchForWeather(string searchInput)
        {
            FindElement(weatherSearchInput).SendKeys(searchInput);
            FindElement(weatherSearchInput).SendKeys(Keys.Enter);
            Thread.Sleep(10000);

            if (FindElement(invalidSearchText).Displayed)
            {
                Console.WriteLine("You have inputted invalid values, please search a valid ountry/city");

            }
            else
            {
                Console.WriteLine("You have inputted valid value!");
            }

        }

        public string getCountryWeatherTemp()
        {
            Thread.Sleep(10000);
            var weatherTempVal = FindElement(weatherTemp).Text;
            var weatherTempStr = weatherTempVal.ToString();
            Console.WriteLine("Weather Temperature is : " + weatherTempVal);

            return weatherTempStr;
        }



        public string APITest_GetWeatherData()
        {
            string id = this.getAPIKey();
            Console.WriteLine(id);

            var country = "Philippines";

            // arrange
            // Create CLient connection
            //Create request to gete data from server 
            RestClient client = new RestClient(base_URL);
            RestRequest request =
                new RestRequest($"{endpoint}q={country}&appId={id}", Method.Get);

            // act
            //Executing request to server and checking server response to it
            RestResponse response = client.Execute(request);

            // assert
            //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Not Matching");

            // Extracting output data from received response 
            string jresponse = response.Content;

            //var jsonResponse = JsonConvert.DeserializeObject<WeatherMapperModel>(jresponse);
            var apiTempVal = JObject.Parse(jresponse).SelectToken("main.temp");
            //double temp = jsonResponse.main.temp;
            Console.WriteLine(apiTempVal);
            string convert = apiTempVal.ToString();

            string result = convert.Remove(convert.Length - 4);
            Console.WriteLine(result);

            return result;
        }
    }


}
