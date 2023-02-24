using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace AutoMation
{
    public class UnitTest
    {
        private  IWebDriver driver;
        private  Screenshot screenshot;
        readonly string url = "https://ui.stage.exostechnology.com/auction/";
        readonly string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        [OneTimeSetUp]
        public void Setup()
        {
            #region using with driver exe file path
            //driver = new ChromeDriver(path + @"\drivers\chromedriver.exe");
            //driver.Navigate().GoToUrl("https://ui.stage.exostechnology.com/auction/");
            #endregion

            // setup browser call without linking driver exe file with the application we have to update chrome driver from nueget only. without DriverManager Process.
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser); 
            // DriverManger update autometically chrome as per the browser version. Now we don't need to update  chrome driver manully.
            driver = DriverFactory.SetDriver("Chrome"); 
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch
            {
                CaptureScreenShot();
                throw;
            }
        }


        [Test, Order(1)]
        public void TestLogin()
        {

            driver.FindElement(By.Id("accountSignInlink4.5")).Click();

            driver.FindElement(By.Id("email-6")).SendKeys("gagandeep.walia+36@svclnk.com");
            driver.FindElement(By.Id("password-1")).SendKeys("Tester@123");
            Assert.IsTrue(driver.FindElement(By.CssSelector("button[class='full-button']")).Displayed);

            driver.FindElement(By.CssSelector("button[class='full-button']")).Click();

            Assert.IsTrue(driver.FindElement(By.ClassName("homepage-component")).Displayed);
        }

        [Test, Order(2)]
        public void TestVerifyAccountBYSMS()
        {
            Assert.IsTrue(driver.FindElement(By.CssSelector("button[class='button btn mr-1']")).Displayed);

            driver.FindElement(By.CssSelector("button[class='button btn mr-1']")).Click();
            var otp = ""; // enter letest otp you got.
            driver.FindElement(By.Name("mfaCode")).SendKeys(otp);
            var element = driver.FindElement(By.Id("btnFrmSubmit"));
            if (element.Enabled)
            {
                driver.FindElement(By.Id("btnFrmSubmit")).Click();
            }

        }


        private void CaptureScreenShot()
        {            
            screenshot.SaveAsFile(path + @$"\AuctionSPA_Error_img\{Guid.NewGuid()}.png", ScreenshotImageFormat.Png);
        }

        //[TearDown]
        //public void TearDown()
        //{
        //    driver.Quit();
        //}
    }
}