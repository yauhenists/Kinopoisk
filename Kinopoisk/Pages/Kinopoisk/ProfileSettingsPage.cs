using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public class ProfileSettingsPage : BasePage
    {
        public enum PageElements
        {
            AboutTextArea
        }

        private readonly By _aboutTextArea = By.XPath("//textarea[@id='edit[main][about]']");
        private readonly By _saveAll = By.XPath("//input[@value='сохранить все']");
        public ProfileSettingsPage(ConciseApi conciseApi) : base(conciseApi)
        {
        }

        public override void OpenPage()
        {
            throw new System.NotImplementedException();
        }

        public void FillInFieldAndSave(string text, PageElements element)
        {
            By field = null;
            switch (element)
            {
                case PageElements.AboutTextArea:
                    field = _aboutTextArea;
                    break;
            }
            ConciseApi.ClearField(field);
            ConciseApi.InputText(text, field);
            ConciseApi.ClickOnElement(_saveAll);
        }

        public string GetAboutText() => ConciseApi.GetTextOfElement(_aboutTextArea);
    }
}
