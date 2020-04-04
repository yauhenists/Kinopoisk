Feature: LoginAndLogout
	Check login and logout functionality

Scenario: Login with invalid credentials
	Given I have opened home kinopoisk page
	And I click login button to go to registration page
	When I login with credentials
	| Login             | Password    |
	| test.selenium2002 | selenium124 |
	Then I should see invalid password message

Scenario: Login with valid credentials
	Given I have opened home kinopoisk page
	And I click login button to go to registration page
	When I login with credentials
	| Login             | Password    |
	| test.selenium2002 | selenium123 |
	Then I should see avatar button on reloaded home page

Scenario: Login then logout
	Given I have opened home kinopoisk page
	And I click login button to go to registration page
	When I login with credentials
	| Login             | Password    |
	| test.selenium2002 | selenium123 |
	And I click logout
	Then I should see login button
