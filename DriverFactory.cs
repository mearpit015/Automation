namespace AutoMation
{
    public class DriverFactory
    {
        public static IWebDriver SetDriver(string name)
        {
            return new ChromeDriver();
        }
    }
}
