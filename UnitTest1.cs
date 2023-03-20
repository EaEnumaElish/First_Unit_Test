using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;


namespace Laba6
{
    [TestClass]
    public class UnitTest1
    {
        //Ñòâîðþºìî åêçåìïëÿð EdgeDriverService, âèêîðèñòîâóþ÷è âêàçàíèé øëÿõ äî âèêîíóâàíîãî ôàéëó EdgeDriver - msedgedriver.exe
        EdgeDriverService service = EdgeDriverService.CreateDefaultService(@"D:\");
        //Ñòâîðþºìî åêçåìïëÿð EdgeOptions, çà äîïîìîãîþ ÿêîãî ìîæëèâî íàëàøòóâàòè áðàóçåð   
        EdgeOptions options = new EdgeOptions();

        [TestMethod]
        public void Log_to_site()
        {
            //äîäàìî îïö³þ äëÿ çàïóñêó áðàóçåðà ç ðîçøèðåííÿì 1280õ720 ï³ä ÷àñ âèêîíàííÿ äàíîãî ìåòîäó
            options.AddArgument("--window-size=1280,720");

            //Çãåíåðóºìî àäðåñó äèðåêòîð³¿ äëÿ çáåð³ãàííÿ äàíèõ êîðèñòóâà÷à (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            if (!Directory.Exists(ProfileDirect))
            {




                Directory.CreateDirectory(ProfileDirect);
            }
            //äîäàìî îïö³þ ÿêà âêàçóº íà çãåíåðîâàíó âèùå àäðåñó äàíèõ êîðèñòóâà÷à
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //Ñòâîðèìî íîâèé åêçåìïëÿð EdgeDriver íà îñíîâ³ ñòâîðåíèõ âèùå ñåðâ³ñó òà îïö³é
            var driver = new EdgeDriver(service, options);

            //Ïåðåéäåìî íà ñòîð³íêó "îñîáèñòèé êàá³íåò" ³íòåðíåò ìàãàçèíó
            driver.Url = "https://memory.net.ua/customer/account/";

            if (driver.Title != "Memory.Net.Ua Ì³é àêêàóíò")
            {
                //Íà çàâàíòàæåí³é ñòîð³íö³ çíàéäåìî åëåìåíò (çà äîïîìîãîþ XPath), ùî â³äïîâ³äàº çà ââ³ä åìåéëà
                IWebElement email = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
                //Ââåäåìî â çíàéäåíèé åëåìåíò åëåêòðîííó ïîøòó (çà äîïîìîãîþ ôóíêö³¿ â³äïðàâêè êëàâ³ø)
                email.SendKeys("mkqodopsefyzsbvpoz@tcwlx.com");
                //Çíàéäåìî åëåìåíò, ùî â³äïîâ³äàº çà ââ³ä ïàðîëÿ
                IWebElement password = driver.FindElement(By.XPath("//*[@id=\"pass\"]"));
                //Ââåäåìî ïàðîëü
                password.SendKeys("mkqodopsefyzsbvpoz");

                //Ãóãë-êàï÷à çíàõîäèòüñÿ â îêðåìîìó â³êí³ (ñàéò-â-ñàéò³), çíàéäåìî öå â³êíî
                var capcha = driver.FindElement(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]"));
                //Ï³äòâåðäèìî, ùî ìè íå ðîáîò ïðîñòàâèâøè ãàëî÷êó
                capcha.Click();
                //×åêàºìî íàïðèêëàä 2 ñåêóíäè, ùîá ïåðåâ³ðêà íà ðîáîò³â âñòèãëà çàâåðøèòèñü
                Thread.Sleep(2000);

                //Äëÿ ïîøóêó åëåìåíò³â íà í³é ïîòð³áíî ïåðåìêíåìîñü íà â³äïîâ³äíèé Frame
                driver.SwitchTo().Frame(capcha);
                //Çíàéäåìî ó ôðåéì³ åëåìåíò ùî â³äïîâ³äàº çà "â³äîáðàæåííÿ" ãàëî÷êè
                var capchaCheckBox = driver.FindElement(By.Id("recaptcha-anchor"));
                //Ïåðåìêíåìîñü äî êîíòåíòó çà çàìîâ÷óâàííÿì (ç ôðåéìó ãóãë-êàï÷³ äî íàøî¿ html ñòîð³íêè)
                driver.SwitchTo().DefaultContent();

                Thread.Sleep(8000);

                //Çíàéäåìî âåáåëåìåíò - êíîïêó "Âõ³ä"
                IWebElement enter = driver.FindElement(By.XPath("//*[@id=\"send2\"]"));
                //Íàòèñíåìî êíîïêó "Âõ³ä"
                enter.Click();
            }
            driver.Quit();
        }

        [TestMethod]
        public void Search_item()
        {
            //äîäàìî îïö³þ äëÿ çàïóñêó áðàóçåðà ç ðîçøèðåííÿì 1280õ720 ï³ä ÷àñ âèêîíàííÿ äàíîãî ìåòîäó
            options.AddArgument("--window-size=1280,720");
            //Ñòâîðþºìî ³ âêàçóºìî àäðåñó äèðåêòîð³¿ äëÿ çáåð³ãàííÿ äàíèõ êîðèñòóâà÷à (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //Ñòâîðèìî íîâèé åêçåìïëÿð EdgeDriver
            var driver = new EdgeDriver(service, options);
            //Ïåðåéäåìî íà ñòîð³íêó îñîáèñòîãî êàá³íåòó
            driver.Url = "https://memory.net.ua/customer/account/";

            //Çíàéäåìî âåáåëåìåíò - êíîïêó "search"
            IWebElement search = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //Ââåäåìî search
            search.SendKeys("Fury 16");

            //Çíàéäåìî âåáåëåìåíò - êíîïêó "search_press"
            IWebElement search_press = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/header/div/div[5]/form/div[1]/button"));
            search_press.Click();
            driver.Quit();
        }

        [TestMethod]
        public void add_to_wishlist()
        {
            //äîäàìî îïö³þ äëÿ çàïóñêó áðàóçåðà ç ðîçøèðåííÿì 1280õ720 ï³ä ÷àñ âèêîíàííÿ äàíîãî ìåòîäó
            options.AddArgument("--window-size=1280,720");
            //Ñòâîðþºìî ³ âêàçóºìî àäðåñó äèðåêòîð³¿ äëÿ çáåð³ãàííÿ äàíèõ êîðèñòóâà÷à (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //Ñòâîðèìî íîâèé åêçåìïëÿð EdgeDriver
            var driver = new EdgeDriver(service, options);
            //Ïåðåéäåìî íà ñòîð³íêó îñîáèñòîãî êàá³íåòó
            driver.Url = "https://memory.net.ua/customer/account/";

            //Çíàéäåìî âåáåëåìåíò - êíîïêó "search"
            IWebElement search = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //Ââåäåìî search
            search.SendKeys("Fury 16");

            //Çíàéäåìî âåáåëåìåíò - êíîïêó "search_press"
            IWebElement search_press = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/header/div/div[5]/form/div[1]/button"));
            search_press.Click();

            //Çíàéäåìî âåáåëåìåíò - êíîïêó "add_to_wishlist"
            IWebElement add_to_wishlist = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/div[2]/div/div[2]/div[2]/div[4]/div/ul/li[1]/div/div[2]/ul/li[1]/a"));
            add_to_wishlist.Click();
            Thread.Sleep(15000);
            driver.Quit();
        }
    }
}
