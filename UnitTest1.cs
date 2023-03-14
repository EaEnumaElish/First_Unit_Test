using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;


namespace Laba6
{
    [TestClass]
    public class UnitTest1
    {
        //Створюємо екземпляр EdgeDriverService, використовуючи вказаний шлях до виконуваного файлу EdgeDriver - msedgedriver.exe
        EdgeDriverService service = EdgeDriverService.CreateDefaultService(@"D:\Code_projects\csharp\Laba6\Laba6\bin");
        //Створюємо екземпляр EdgeOptions, за допомогою якого можливо налаштувати браузер   
        EdgeOptions options = new EdgeOptions();

        [TestMethod]
        public void Log_to_site()
        {
            //додамо опцію для запуску браузера з розширенням 1280х720 під час виконання даного методу
            options.AddArgument("--window-size=1280,720");

            //Згенеруємо адресу директорії для зберігання даних користувача (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            if (!Directory.Exists(ProfileDirect))
            {




                Directory.CreateDirectory(ProfileDirect);
            }
            //додамо опцію яка вказує на згенеровану вище адресу даних користувача
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //Створимо новий екземпляр EdgeDriver на основі створених вище сервісу та опцій
            var driver = new EdgeDriver(service, options);

            //Перейдемо на сторінку "особистий кабінет" інтернет магазину
            driver.Url = "https://memory.net.ua/customer/account/";

            if (driver.Title != "Memory.Net.Ua Мій аккаунт")
            {
                //На завантаженій сторінці знайдемо елемент (за допомогою XPath), що відповідає за ввід емейла
                IWebElement email = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
                //Введемо в знайдений елемент електронну пошту (за допомогою функції відправки клавіш)
                email.SendKeys("mkqodopsefyzsbvpoz@tcwlx.com");
                //Знайдемо елемент, що відповідає за ввід пароля
                IWebElement password = driver.FindElement(By.XPath("//*[@id=\"pass\"]"));
                //Введемо пароль
                password.SendKeys("mkqodopsefyzsbvpoz");

                //Гугл-капча знаходиться в окремому вікні (сайт-в-сайті), знайдемо це вікно
                var capcha = driver.FindElement(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]"));
                //Підтвердимо, що ми не робот проставивши галочку
                capcha.Click();
                //Чекаємо наприклад 2 секунди, щоб перевірка на роботів встигла завершитись
                Thread.Sleep(2000);

                //Для пошуку елементів на ній потрібно перемкнемось на відповідний Frame
                driver.SwitchTo().Frame(capcha);
                //Знайдемо у фреймі елемент що відповідає за "відображення" галочки
                var capchaCheckBox = driver.FindElement(By.Id("recaptcha-anchor"));
                //Перемкнемось до контенту за замовчуванням (з фрейму гугл-капчі до нашої html сторінки)
                driver.SwitchTo().DefaultContent();

                Thread.Sleep(8000);

                //Знайдемо вебелемент - кнопку "Вхід"
                IWebElement enter = driver.FindElement(By.XPath("//*[@id=\"send2\"]"));
                //Натиснемо кнопку "Вхід"
                enter.Click();
            }
            driver.Quit();
        }

        [TestMethod]
        public void Search_item()
        {
            //додамо опцію для запуску браузера з розширенням 1280х720 під час виконання даного методу
            options.AddArgument("--window-size=1280,720");
            //Створюємо і вказуємо адресу директорії для зберігання даних користувача (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //Створимо новий екземпляр EdgeDriver
            var driver = new EdgeDriver(service, options);
            //Перейдемо на сторінку особистого кабінету
            driver.Url = "https://memory.net.ua/customer/account/";

            //Знайдемо вебелемент - кнопку "search"
            IWebElement search = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //Введемо search
            search.SendKeys("Fury 16");

            //Знайдемо вебелемент - кнопку "search_press"
            IWebElement search_press = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/header/div/div[5]/form/div[1]/button"));
            search_press.Click();
            driver.Quit();
        }

        [TestMethod]
        public void add_to_wishlist()
        {
            //додамо опцію для запуску браузера з розширенням 1280х720 під час виконання даного методу
            options.AddArgument("--window-size=1280,720");
            //Створюємо і вказуємо адресу директорії для зберігання даних користувача (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //Створимо новий екземпляр EdgeDriver
            var driver = new EdgeDriver(service, options);
            //Перейдемо на сторінку особистого кабінету
            driver.Url = "https://memory.net.ua/customer/account/";

            //Знайдемо вебелемент - кнопку "search"
            IWebElement search = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //Введемо search
            search.SendKeys("Fury 16");

            //Знайдемо вебелемент - кнопку "search_press"
            IWebElement search_press = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/header/div/div[5]/form/div[1]/button"));
            search_press.Click();

            //Знайдемо вебелемент - кнопку "add_to_wishlist"
            IWebElement add_to_wishlist = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/div[2]/div/div[2]/div[2]/div[4]/div/ul/li[1]/div/div[2]/ul/li[1]/a"));
            add_to_wishlist.Click();
            Thread.Sleep(15000);
            driver.Quit();
        }
    }
}