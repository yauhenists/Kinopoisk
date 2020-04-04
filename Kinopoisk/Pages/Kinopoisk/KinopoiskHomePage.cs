using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public sealed class KinopoiskHomePage : BasePage
    {
        private const string Url = "https://www.kinopoisk.ru/";
        private readonly By _loginButton = By.XPath("//button[contains(@class,'login-button')]");
        private readonly By _loginAvatarButton = By.XPath("//div[contains(@class, 'button')]/div[contains(@class, 'avatar')]");
        private readonly By _loginToProfileButton = By.XPath("//li/div/a[contains(@href,'/user/')]");
        private readonly By _logoutButton = By.XPath("//div[contains(text(),'Выйти')]");
        private readonly By _searchField = By.XPath("//input[@placeholder='Фильмы, сериалы, персоны']");
        private readonly By _advancedSearchButton = By.XPath("//a[@aria-label='advanced-search']");
        public string Title { get; } = "КиноПоиск. Все фильмы планеты.";
        public KinopoiskHomePage(ConciseApi conciseApi) : base(conciseApi)
        {
            OpenPage();
        }

        public override void OpenPage()
        {
            ConciseApi.OpenPage(Url);
        }

        public RegistrationPage GoToRegistartionPage()
        {
            ConciseApi.ClickOnElement(_loginButton);

            return new RegistrationPage(ConciseApi);
        }

        public ProfilePage GoToProfilePage()
        {
            ConciseApi.MoveCursorToElement(_loginAvatarButton);
            ConciseApi.ClickOnElementJs(_loginToProfileButton); //only JS works, flexbox

            return new ProfilePage(ConciseApi);
        }

        public KinopoiskHomePage Logout()
        {
            //Thread.Sleep(10000);
            ConciseApi.MoveCursorToElement(_loginAvatarButton);
            if (!ConciseApi.GetElement(_logoutButton).Enabled && !ConciseApi.GetElement(_logoutButton).Displayed)
            {
                ConciseApi.MoveCursorToElement(_loginAvatarButton);
            }
            ConciseApi.ClickOnElementJs(_logoutButton);

            return this;
        }

        public PageWithResults Search(string text)
        {
            ConciseApi.InputTextAndExecute(text, _searchField);

            return new PageWithResults(ConciseApi);
        }

        public AdvancedSearchPage GoToAdvancedSearchPage()
        {
            ConciseApi.ClickOnElement(_advancedSearchButton);

            return new AdvancedSearchPage(ConciseApi);
        }

        public bool IsLoginAvatarButtonDisplayed() => ConciseApi.IsElementDisplayed(_loginAvatarButton);

        public string GetLoginButtonText() => ConciseApi.GetTextOfElement(_loginButton);
    }
}
