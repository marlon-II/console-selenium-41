using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeuProjeto.Tests
{
    [TestClass]
    public class WebFormularioDesafioTest
    {
        private IWebDriver _driver = null!;
        private WebDriverWait _wait = null!;
        private const string Url = "https://www.selenium.dev/selenium/web/web-form.html";

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _driver.Navigate().GoToUrl(Url);
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        private void SubmeterEValidarSucesso()
        {
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            var mensagem = _wait.Until(d => d.FindElement(By.Id("message")));
            Assert.AreEqual("Received!", mensagem.Text);
        }

        [TestMethod]
        public void Exercicio1_CadastroCompleto_PreenchimentoMisto()
        {
            _driver.FindElement(By.Name("my-text")).SendKeys("Marlon Andrade Bartoli");
            _driver.FindElement(By.Name("my-password")).SendKeys("S3nh@Segura#2026");
            _driver.FindElement(By.Name("my-textarea")).SendKeys(
                "Tenho experiência com testes automatizados usando xUnit e Selenium, " +
                "validando regras de negócio e fluxos de interface.");

            var readonlyInput = _driver.FindElement(By.Name("my-readonly"));
            Assert.AreEqual("Readonly input", readonlyInput.GetAttribute("value"));

            SubmeterEValidarSucesso();
        }

        [TestMethod]
        public void Exercicio2_SeletoresEListasInteligentes()
        {
            var select = new SelectElement(_driver.FindElement(By.Name("my-select")));
            select.SelectByText("Two");
            Assert.AreEqual("2", select.SelectedOption.GetAttribute("value"));

            var datalist = _driver.FindElement(By.Name("my-datalist"));
            datalist.SendKeys("Seattle");
            Assert.AreEqual("Seattle", datalist.GetAttribute("value"));

            SubmeterEValidarSucesso();
        }

        [TestMethod]
        public void Exercicio3_EstadosCheckboxesRadiosEColorPicker()
        {
            var check1 = _driver.FindElement(By.Id("my-check-1"));
            Assert.IsTrue(check1.Selected);

            var check2 = _driver.FindElement(By.Id("my-check-2"));
            Assert.IsFalse(check2.Selected);
            check2.Click();
            Assert.IsTrue(check2.Selected);

            var radio2 = _driver.FindElement(By.Id("my-radio-2"));
            radio2.Click();
            Assert.IsTrue(radio2.Selected);

            var colorPicker = _driver.FindElement(By.Name("my-colors"));
            ((IJavaScriptExecutor)_driver).ExecuteScript(
                "arguments[0].value = '#ff0000';" +
                "arguments[0].dispatchEvent(new Event('input', { bubbles: true }));" +
                "arguments[0].dispatchEvent(new Event('change', { bubbles: true }));",
                colorPicker);
            Assert.AreEqual("#ff0000", colorPicker.GetAttribute("value"));

            SubmeterEValidarSucesso();
        }
    }
}