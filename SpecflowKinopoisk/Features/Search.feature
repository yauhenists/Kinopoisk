Feature: Search
	To check simple and advanced search

Scenario: Simple Search
	Given I have opened home kinopoisk page
	When I enter "Зеленая миля" and search
	Then I get "Зеленая миля" as the first result on the page with results

Scenario: Advanced Search
	Given I have opened home kinopoisk page
	And I go to advanced search page
	#When I enter "Зеленая миля" and choose country "США" and search
	When I enter "Зеленая миля" and search
	Then I get "Зеленая миля" as the first result on the page with results
