@Check-In
Feature: Path Join
	In order to work with paths
	As a developer
	I need to be able to join paths


Scenario Outline: Join null a path
	Given I have the following path: <Target>
	  And I have the following other path: <Other>
	 When I parse the path
	  And I parse the other path
	  And I call path join other
	 Then The join result should be a new instance
	 #And the join result should have the value:
| Target | Other  |
| c:\\   | (null) |