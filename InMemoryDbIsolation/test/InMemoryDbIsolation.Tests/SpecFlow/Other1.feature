Feature: Other 1

Scenario: 1
	Given the other movies:
		| Title     |
		| Desperado |
	When this other movie is added
		| Title     |
		| Kill Bill |
	Then other movies contains
		| Title     |
		| Desperado |
		| Kill Bill |