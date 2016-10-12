Feature: Feature 1

Scenario: 1
	Given the movies:
		| Guid                                 | Title        |
		| 0c6303e6-c2bf-4358-9c8a-f7a8272f5f71 | Pulp Fiction |
	When this movie is added
		| Guid                                 | Title        |
		| 1167967a-9f1f-4b71-a758-a4b951529c41 | Jackie Brown |
	Then movies contains
		| Guid                                 | Title        |
		| 0c6303e6-c2bf-4358-9c8a-f7a8272f5f71 | Pulp Fiction |
		| 1167967a-9f1f-4b71-a758-a4b951529c41 | Jackie Brown |