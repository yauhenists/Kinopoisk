using Kinopoisk.Pages.Kinopoisk;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;

namespace Kinopoisk.Tests
{
    [TestFixture]
    public class KinopoiskTests : BaseTest
    {
        [Test]
        public void LoginWithInvalidCredentials()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            registrationPage.LoginWithCredentials<RegistrationPage>(false);

            Assert.AreEqual(registrationPage.InvalidPasswordMessageText,
                registrationPage.GetInvalidPasswordMessage());
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);

            Assert.IsTrue(ConciseApi.AssertThat(ExpectedConditions.TitleIs(homePage.Title)));
            Assert.IsTrue(homePage.IsLoginAvatarButtonDisplayed());

        }

        [Test]
        public void ChangeProfileData()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);
            ProfilePage profilePage = homePage.GoToProfilePage();
            ProfileSettingsPage profileSettingsPage = profilePage.GoToProfileSettingsPage();
            profileSettingsPage.FillInFieldAndSave("test", ProfileSettingsPage.PageElements.AboutTextArea);

            Assert.AreEqual("test", profileSettingsPage.GetAboutText());

        }

        [Test]
        public void LogoutSuccessfully()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);
            homePage = homePage.Logout();

            Assert.AreEqual("Войти", homePage.GetLoginButtonText());

        }

        [Test]
        public void SearchAndCheckResult()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            PageWithResults pageWithResults = homePage.Search("Зеленая миля");

            Assert.AreEqual("Зеленая миля", pageWithResults.GetFirstResultTitle());
        }

        [Test]
        public void AddToFavouriteFilms()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);
            PageWithResults pageWithResults = homePage.Search("Зеленая миля");
            ParticularFilmPage particularFilmPage = pageWithResults.FollowFirstResult();
            particularFilmPage.AddToFavourites();

            Assert.AreEqual("Удалить из любимых фильмов",
                particularFilmPage.GetTitleOfButton(ParticularFilmPage.PageElements.AddToFavouritesButton));

        }

        [Test]
        public void AddToWatchedFilms()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);
            PageWithResults pageWithResults = homePage.Search("Зеленая миля");
            ParticularFilmPage particularFilmPage = pageWithResults.FollowFirstResult();
            particularFilmPage.AddToWatched();

            Assert.AreEqual("Убрать отметку о просмотре",
                particularFilmPage.GetTitleOfButton(ParticularFilmPage.PageElements.AddToWatchedButton));
        }

        [Test]
        public void AddNoticeToFilm()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);
            PageWithResults pageWithResults = homePage.Search("Зеленая миля");
            ParticularFilmPage particularFilmPage = pageWithResults.FollowFirstResult();
            particularFilmPage = particularFilmPage.AddNotice("test notice");

            Assert.AreEqual("test notice", particularFilmPage.GetNoticeText());
        }

        [Test]
        public void AddFilmToWillWatchFolder()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            RegistrationPage registrationPage = homePage.GoToRegistartionPage();
            homePage = registrationPage.LoginWithCredentials<KinopoiskHomePage>(true);
            PageWithResults pageWithResults = homePage.Search("Зеленая миля");
            ParticularFilmPage particularFilmPage = pageWithResults.FollowFirstResult();
            particularFilmPage.AddToWillWatchFolder();

            Assert.IsTrue(particularFilmPage.IsNotificationMessageDisplayed());
            Assert.AreEqual("Фильм добавлен в папку «Буду смотреть».", particularFilmPage.GetNotificationMessageText());

            particularFilmPage.TearDownWillWatchFolder();
        }

        [Test]
        public void WatchTrailer()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            PageWithResults pageWithResults = homePage.Search("Зеленая миля");
            ParticularFilmPage particularFilmPage = pageWithResults.FollowFirstResult();
            particularFilmPage.OpenTrailerAndWatchFor(10);
        }

        [Test]
        public void AdvancedSearch()
        {
            KinopoiskHomePage homePage = new KinopoiskHomePage(ConciseApi);
            AdvancedSearchPage advancedSearchPage = homePage.GoToAdvancedSearchPage();
            PageWithResults pageWithResults = advancedSearchPage.SearchByNameAndCountry("Зеленая миля", "США");

            Assert.AreEqual("Зеленая миля", pageWithResults.GetFirstResultTitle());
        }
    }
}
