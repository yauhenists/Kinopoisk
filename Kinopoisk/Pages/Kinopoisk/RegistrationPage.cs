using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public class RegistrationPage : BasePage
    {
        private readonly By _loginField = By.XPath("//input[@name='login']");
        private readonly By _submitButton = By.XPath("//button[@type='submit']");
        private readonly By _passwordField = By.XPath("//input[@type='password']");
        private readonly string _validLogin = "test.selenium2002";
        private readonly string _validPassword = "selenium123";
        private readonly string _invalidPassword = "selenium124";
        private readonly By _invalidPasswordMessage = By.XPath("//div[@class='passp-form-field__error']");
        public string InvalidPasswordMessageText { get; } = "Неверный пароль";

        public RegistrationPage(ConciseApi conciseApi) : base(conciseApi)
        {
        }

        public override void OpenPage()
        {
            throw new System.NotImplementedException();
        }

        public T LoginWithCredentials<T>(string login, string password) where T : BasePage
        {
            InputLogin(login);
            SubmitLogin();
            InputPassword(password);
            SubmitLogin();

            if (password == _validPassword)
            {
                return new KinopoiskHomePage(ConciseApi) as T;
            }

            return this as T;
        }

        public T LoginWithCredentials<T>(bool isCredentialsValid) where T : BasePage
        {
            return isCredentialsValid
                ? LoginWithCredentials<T>(_validLogin, _validPassword)
                : LoginWithCredentials<T>(_validLogin, _invalidPassword);
        }

        public string GetInvalidPasswordMessage() => ConciseApi.GetTextOfElement(_invalidPasswordMessage);

        private void InputLogin(string login)
        {
            ConciseApi.InputText(login, _loginField);
        }

        private void SubmitLogin()
        {
            ConciseApi.ClickOnElement(_submitButton);
        }

        private void InputPassword(string password)
        {
            ConciseApi.InputText(password, _passwordField);
        }
    }
}
