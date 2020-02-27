using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public class ProfilePage : BasePage
    {
        private By ChangeDataButton { get; } = By.XPath("//a[contains(text(),'Изменить данные')]");
        public ProfilePage(ConciseApi conciseApi) : base(conciseApi)
        {
        }

        public override void OpenPage()
        {
            throw new System.NotImplementedException();
        }

        public ProfileSettingsPage GoToProfileSettingsPage()
        {
            ConciseApi.ClickOnElement(ChangeDataButton);

            return new ProfileSettingsPage(ConciseApi);
        }
    }
}
