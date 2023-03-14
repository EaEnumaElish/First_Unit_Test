using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.Threading;


namespace Laba6
{
    [TestClass]
    public class UnitTest1
    {
        //��������� ��������� EdgeDriverService, �������������� �������� ���� �� ������������ ����� EdgeDriver - msedgedriver.exe
        EdgeDriverService service = EdgeDriverService.CreateDefaultService(@"D:\Code_projects\csharp\Laba6\Laba6\bin");
        //��������� ��������� EdgeOptions, �� ��������� ����� ������� ����������� �������   
        EdgeOptions options = new EdgeOptions();

        [TestMethod]
        public void Log_to_site()
        {
            //������ ����� ��� ������� �������� � ����������� 1280�720 �� ��� ��������� ������ ������
            options.AddArgument("--window-size=1280,720");

            //��������� ������ �������� ��� ��������� ����� ����������� (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            if (!Directory.Exists(ProfileDirect))
            {




                Directory.CreateDirectory(ProfileDirect);
            }
            //������ ����� ��� ����� �� ����������� ���� ������ ����� �����������
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //�������� ����� ��������� EdgeDriver �� ����� ��������� ���� ������ �� �����
            var driver = new EdgeDriver(service, options);

            //��������� �� ������� "��������� ������" �������� ��������
            driver.Url = "https://memory.net.ua/customer/account/";

            if (driver.Title != "Memory.Net.Ua ̳� �������")
            {
                //�� ����������� ������� �������� ������� (�� ��������� XPath), �� ������� �� ��� ������
                IWebElement email = driver.FindElement(By.XPath("//*[@id=\"email\"]"));
                //������� � ��������� ������� ���������� ����� (�� ��������� ������� �������� �����)
                email.SendKeys("mkqodopsefyzsbvpoz@tcwlx.com");
                //�������� �������, �� ������� �� ��� ������
                IWebElement password = driver.FindElement(By.XPath("//*[@id=\"pass\"]"));
                //������� ������
                password.SendKeys("mkqodopsefyzsbvpoz");

                //����-����� ����������� � �������� ��� (����-�-����), �������� �� ����
                var capcha = driver.FindElement(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]"));
                //ϳ���������, �� �� �� ����� ����������� �������
                capcha.Click();
                //������ ��������� 2 �������, ��� �������� �� ������ ������� �����������
                Thread.Sleep(2000);

                //��� ������ �������� �� �� ������� ������������ �� ��������� Frame
                driver.SwitchTo().Frame(capcha);
                //�������� � ����� ������� �� ������� �� "�����������" �������
                var capchaCheckBox = driver.FindElement(By.Id("recaptcha-anchor"));
                //������������ �� �������� �� ������������� (� ������ ����-����� �� ���� html �������)
                driver.SwitchTo().DefaultContent();

                Thread.Sleep(8000);

                //�������� ���������� - ������ "����"
                IWebElement enter = driver.FindElement(By.XPath("//*[@id=\"send2\"]"));
                //��������� ������ "����"
                enter.Click();
            }
            driver.Quit();
        }

        [TestMethod]
        public void Search_item()
        {
            //������ ����� ��� ������� �������� � ����������� 1280�720 �� ��� ��������� ������ ������
            options.AddArgument("--window-size=1280,720");
            //��������� � ������� ������ �������� ��� ��������� ����� ����������� (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //�������� ����� ��������� EdgeDriver
            var driver = new EdgeDriver(service, options);
            //��������� �� ������� ���������� �������
            driver.Url = "https://memory.net.ua/customer/account/";

            //�������� ���������� - ������ "search"
            IWebElement search = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //������� search
            search.SendKeys("Fury 16");

            //�������� ���������� - ������ "search_press"
            IWebElement search_press = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/header/div/div[5]/form/div[1]/button"));
            search_press.Click();
            driver.Quit();
        }

        [TestMethod]
        public void add_to_wishlist()
        {
            //������ ����� ��� ������� �������� � ����������� 1280�720 �� ��� ��������� ������ ������
            options.AddArgument("--window-size=1280,720");
            //��������� � ������� ������ �������� ��� ��������� ����� ����������� (cookies)
            string ProfileDirect = Directory.GetCurrentDirectory() + "\\MyProfile";
            options.AddArgument(@"user-data-dir=" + ProfileDirect);

            //�������� ����� ��������� EdgeDriver
            var driver = new EdgeDriver(service, options);
            //��������� �� ������� ���������� �������
            driver.Url = "https://memory.net.ua/customer/account/";

            //�������� ���������� - ������ "search"
            IWebElement search = driver.FindElement(By.XPath("//*[@id=\"search\"]"));
            //������� search
            search.SendKeys("Fury 16");

            //�������� ���������� - ������ "search_press"
            IWebElement search_press = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/header/div/div[5]/form/div[1]/button"));
            search_press.Click();

            //�������� ���������� - ������ "add_to_wishlist"
            IWebElement add_to_wishlist = driver.FindElement(By.XPath("//*[@id=\"top\"]/body/div/div/div[2]/div/div[2]/div[2]/div[4]/div/ul/li[1]/div/div[2]/ul/li[1]/a"));
            add_to_wishlist.Click();
            Thread.Sleep(15000);
            driver.Quit();
        }
    }
}