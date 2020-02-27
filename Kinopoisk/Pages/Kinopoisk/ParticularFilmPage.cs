using System;
using System.Threading;
using OpenQA.Selenium;

namespace Kinopoisk.Pages.Kinopoisk
{
    public class ParticularFilmPage : BasePage
    {
        public enum PageElements
        {
            AddToFavouritesButton,
            AddToWatchedButton
        }

        private readonly By _addToFavouritesButton = By.Id("btn_fav_film");
        private readonly By _addToWatchedButton = By.Id("btn_null_vote");
        private readonly By _addNoticeButton = By.Id("btn_film_notice");
        private readonly By _noticeTextArea = By.XPath("//div[@id='ta_film_notice']//form//textarea");
        private readonly By _saveNoticeButton = By.XPath("//div[@id='ta_film_notice']//input[@class='save']");
        private readonly By _savedNotice = By.XPath("//div[@id='txt_film_notice']/div[1]");
        private readonly By _foldersSpanButton = By.XPath("//span[contains(@title,'Мои фильмы')]/following-sibling::span");
        private readonly By _willWatchFolderButton = By.XPath("//div[contains(@class,'movie-info__sidebar')]//dd[1]");
        private readonly By _notificationAddedMessage = By.XPath("//div[@id='ui_notice_container']//*[@class='tdtext']");
        private readonly By _boldFolder = By.XPath("//div[@class='list_div']/dl[@class='list']/dd[1]");
        private readonly By _trailerButton = By.XPath("//button[@id='movie-trailer-button']");
        private readonly By _closeTrailerButton = By.XPath("//button[@class='discovery-trailers-closer']");

        public ParticularFilmPage(ConciseApi conciseApi) : base(conciseApi)
        {
        }

        public override void OpenPage()
        {
            throw new NotImplementedException();
        }

        public void AddToFavourites()
        {
            const string title = "Добавить в любимые фильмы";
            bool isAdded = AddTo(_addToFavouritesButton, title);

            if (isAdded)
            {
                return;
            }

            Console.WriteLine("The film is already added to favourites");
        }

        public void AddToWatched()
        {
            const string title = "Пометить фильм как просмотренный";
            bool isAdded = AddTo(_addToWatchedButton, title);

            if (isAdded)
            {
                return;
            }

            Console.WriteLine("The film is already added to watched");
        }

        public ParticularFilmPage AddNotice(string text)
        {
            ConciseApi.ClickOnElement(_addNoticeButton);
            ConciseApi.ClearField(_noticeTextArea);
            ConciseApi.InputText(text, _noticeTextArea);
            ConciseApi.ClickOnElement(_saveNoticeButton);

            return this;
        }

        public void AddToWillWatchFolder()
        {
            ConciseApi.ClickOnElement(_foldersSpanButton);
            ConciseApi.ClickOnElement(_willWatchFolderButton);
        }

        public void TearDownWillWatchFolder()
        {
            ConciseApi.ClickOnElementJs(_boldFolder);
        }

        public void OpenTrailerAndWatchFor(int seconds)
        {
            ConciseApi.ClickOnElement(_trailerButton);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            ConciseApi.ClickOnElement(_closeTrailerButton);
        }

        public string GetTitleOfButton(PageElements button)
        {
            switch (button)
            {
                case PageElements.AddToFavouritesButton:
                    return ConciseApi.GetTextOfAttributeOfElement(_addToFavouritesButton, "title");
                case PageElements.AddToWatchedButton:
                    return ConciseApi.GetTextOfAttributeOfElement(_addToWatchedButton, "title");                 
            }

            return "Element is not found";
        }

        public string GetNoticeText() => ConciseApi.GetTextOfElement(_savedNotice);

        public string GetNotificationMessageText() => ConciseApi.GetTextOfElement(_notificationAddedMessage);

        public bool IsNotificationMessageDisplayed() => ConciseApi.GetElement(_notificationAddedMessage).Displayed;

        private bool AddTo(By element, string text)
        {
            var button = ConciseApi.GetElement(element);
            if (button.GetAttribute("title") == text)
            {
                button.Click();

                return true;
            }

            return false;
        }
    }
}
