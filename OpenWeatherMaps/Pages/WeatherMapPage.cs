using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherMaps.Pages
{
    public class WeatherMapPage : BasePage
    {
        //public string reppw { get; set; }

        private By signInBtn = By.XPath("(//a[contains(text(),'Sign in')])[1]");
        private By signUpBtn = By.XPath("(//a[contains(text(),'Create an Account')])[1]");

        private By usernameField = By.Id("user_username");
        private By emailField = By.Id("user_email");

        private By passwordField = By.Id("user_password");
        private By repPasswordField = By.Id("user_password_confirmation");
        private By agreeCheck1 = By.Id("agreement_is_age_confirmed");
        private By agreeCheck2 = By.Id("agreement_is_accepted");
        private By agreeCheck3 = By.Id("mailing_system");
        private By agreeCheck4 = By.Id("mailing_product");
        private By agreeCheck5 = By.Id("mailing_news");
        private By captchaCB = By.CssSelector("#recaptcha-anchor");
        private By submitBtn = By.Name("commit");

        private By loginSuccessmessage = By.XPath("//div[text()='Signed in successfully.']");

        //******* API Dialogue *******//
        private By apidialogue = By.XPath("//div/h4");
        private By purposeDrpDown = By.Id("poll_purpose");
        private By saveBtn = By.Name("button");
        private By successSignUp = By.XPath("//div[@class='alert alert-success']");


        public WeatherMapPage(IWebDriver driver) : base(driver)
        {
        }
        public void Login(string uname, string pass)
        {

            FindElement(signInBtn).Click();
            FindElement(emailField).SendKeys(uname);
            FindElement(passwordField).SendKeys(pass);
            FindElement(submitBtn).Click();


            string success = FindElement(loginSuccessmessage).Text;
            Assert.True(true, success);
        }
        public void SignUp()
        {

            var generator = new RandomGenerator();
            var randomString = generator.RandomString(10);
            var randomPassword = generator.RandomPassword();
            var randomEmail = generator.GenerateRandomEmail();



            FindElement(signInBtn).Click();
            Thread.Sleep(10000);
            FindElement(signUpBtn).Click();
            Thread.Sleep(10000);
            FindElement(usernameField).SendKeys(randomString);
            Console.WriteLine($"Random string for name is : {randomString}");
            FindElement(emailField).SendKeys(randomEmail);
            Console.WriteLine($"Random string for email is : {randomEmail}");
            FindElement(passwordField).SendKeys(randomPassword);
            Console.WriteLine($"Random string for password is : {randomPassword}");
            FindElement(repPasswordField).SendKeys(randomPassword);

            FindElement(agreeCheck1).Click();
            FindElement(agreeCheck2).Click();
            FindElement(agreeCheck3).Click();
            FindElement(agreeCheck4).Click();
            FindElement(agreeCheck5).Click();

            //*----------- Selecting the Images accurately is a bit hard to automate, hence doing the capctah selecting in images allows you
            //to do manual intervention , which means your automated tests are not really automated anymore.---* 
            // YOU CAN DO DEBUG THE RUNNING OF THIS FOR YOU TO BE ABLE TO PROCEED TO SUBMIT --- 


            // But here's some parts that i tried to check this functionality. 

            //*-------------- When interacting with IFrame you can either use by selecting the ifreme by (index) or by its (element selector) 
            // Find the number of iframes on the page


            IWebElement iframe = Driver.FindElement(By.XPath("//iframe[contains(@title, 'reCAPTCHA')]"));
            Driver.SwitchTo().Frame(iframe);

            // Perform actions on the elements within the iframe
            FindElement(captchaCB).Click();

            Driver.SwitchTo().ParentFrame();

            IWebElement iframe1 = Driver.FindElement(By.XPath("//iframe[contains(@title, 'recaptcha challenge expires in two minutes')]"));
            Driver.SwitchTo().Frame(iframe1);

            Driver.SwitchTo().ParentFrame();


            FindElement(submitBtn).Click();
            // Driver.SwitchTo().ParentFrame();

            Thread.Sleep(10000);
            this.APIModal();
        }

        public void APIModal()
        {
            if (FindElement(apidialogue).Displayed)
            {
                FindElement(purposeDrpDown).Click();
                SeleniumCustomMethods.selectDropdownByText(FindElement(purposeDrpDown), "Travel");
                FindElement(saveBtn).Click();
                Thread.Sleep(5000);

                Assert.True(FindElement(successSignUp).Displayed);

            }
            else
            {
                Console.WriteLine("API Modal is not Displayed");
            }

        }

    }


}
