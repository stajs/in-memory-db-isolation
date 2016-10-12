Feature: Other 3

Scenario: 1
	Given the other movies:
		| Title        |
		| Pulp Fiction |
	When this other movie is added
		| Title        |
		| Jackie Brown |
	Then other movies contains
		| Title        |
		| Pulp Fiction |
		| Jackie Brown |