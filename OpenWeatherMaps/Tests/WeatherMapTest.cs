using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenWeatherMaps.Pages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OpenWeatherMaps.Tests
{
    public class WeatherMapTest
    {
        private const string base_URL = "https://api.openweathermap.org";
        private const string endpoint = "/data/2.5/weather?";

        private IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            Driver = new ChromeDriver();
            Driver.Manage().Window.Maximize();

        }

        [Test]
        public void APITest_GetWeatherDataList()
        {

            var country = "Philippines";
            var id = "6575bc41c5afde1787db6dbd82d4a65f";
            var units = "metric";

            string[] countryL = { "Phillipines", "Hongkong", "United States", "Japan" };
            for (int i = 0; i < countryL.Length; i++)
            {
                // arrange
                // Create CLient connection
                //Create request to gete data from server 
                RestClient client = new RestClient(base_URL);
                RestRequest request =
                    new RestRequest($"{endpoint}q={countryL}&appId={id}&units={units}", Method.Get);


                // act
                //Executing request to server and checking server response to it
                RestResponse response = client.Execute(request);

                // assert
                //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Not Matching");

                // Extracting output data from received response 
                string jresponse = response.Content;

                //var jsonResponse = JsonConvert.DeserializeObject<WeatherMapperModel>(jresponse);
                var apiTempVal = JObject.Parse(jresponse).SelectToken("main.temp");
                Console.WriteLine(countryL[i] + " weather data for is" + apiTempVal);
            }
        }

        [Test]
        public void APITest_GetInvalidWeatherDataList()
        {

            //var country = "Philippines";
            var id = "6575bc41c5afde1787db6dbd82d4a65f";
            var units = "metric";

            string[] countryL = { "Test", "Hellow" };
            for (int i = 0; i < countryL.Length; i++)
            {
                // arrange
                // Create CLient connection
                //Create request to gete data from server 
                RestClient client = new RestClient(base_URL);
                RestRequest request =
                    new RestRequest($"{endpoint}q={countryL}&appId={id}&units={units}", Method.Get);


                // act
                //Executing request to server and checking server response to it
                RestResponse response = client.Execute(request);

                // assert
                //Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Not Matching");

                // Extracting output data from received response 
                string jresponse = response.Content;

                //var jsonResponse = JsonConvert.DeserializeObject<WeatherMapperModel>(jresponse);
                var apiTempVal = JObject.Parse(jresponse).SelectToken("main.temp");

                Console.WriteLine(countryL[i] + " weather data for is" + apiTempVal);
                Assert.IsEmpty(apiTempVal);
            }
        }

        [Test]
        public void APITest_GetWeatherData()
        {
            //string id = wmdp.getAPIKey();
            //Console.WriteLine(id);

            var country = "Philippines";
            var id = "6575bc41c5afde1787db6dbd82d4a65f";
            var units = "metric";

            // arrange
            // Create CLient connection
            //Create request to gete data from server 
            RestClient client = new RestClient(base_URL);
            RestRequest request =
                new RestRequest($"{endpoint}q={country}&appId={id}&units={units}", Method.Get);

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


        }

        [Test]
        public void SignUpToWeatherMap()
        {
            WeatherMapPage wmp = new WeatherMapPage(Driver);
            wmp.GoToUrl("https://openweathermap.org/");
            Thread.Sleep(10000);
            wmp.SignUp();

            //******//Get the API Weather Value 

            WeatherMapDashPage wmdp = new WeatherMapDashPage(Driver);
            Thread.Sleep(10000);

            //API
            string apiTempVal = wmdp.APITest_GetWeatherData();
            Console.WriteLine(apiTempVal);

            //UI
            wmdp.searchForWeather("Philippines");
            string tempValUI = wmdp.getCountryWeatherTemp();
            Console.WriteLine(tempValUI);

            Assert.AreEqual(tempValUI, apiTempVal, "Not Matched");

        }


        [Test]
        public void TestInvalidSearch()
        {
            string uname = "LSkBsTDqKS@ytxoOrcNFj.com";
            string pass = "bscd4672PB";
            WeatherMapPage wmp = new WeatherMapPage(Driver);
            wmp.GoToUrl("https://openweathermap.org/");
            Thread.Sleep(10000);
            wmp.Login(uname, pass);

            //******//Search for invalid values

            WeatherMapDashPage wmdp = new WeatherMapDashPage(Driver);
            wmdp.searchForWeather("Test");

        }


        [TearDown]
        public void CleanUpTest()
        {
            Driver.Quit();
        }
    }

}
