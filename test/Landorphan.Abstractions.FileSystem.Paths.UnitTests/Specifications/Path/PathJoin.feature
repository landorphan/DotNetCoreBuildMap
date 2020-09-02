@Check-In
Feature: Path Join
	In order to work with paths
	As a developer
	I need to be able to join paths

Scenario Outline: Join one path to another
    Given I'm running on the following Operating System: <OS>
	  And I have the following path: <Target>
	  And I have the following other path: <Other>
	 When I parse the path
	  And I parse the other path
	  And I call path join other
	 Then The join result should be a new instance
	  And The join result should have the expected value: <PSN>
	  And The join result should have the expected status: <PathStatus>
Examples:
| OS      | Target | Other  | PSN                         | PathStatus |
| Windows | c:`    | (null) | [PSN:WIN]/{R}c/{E}/{S}.     | Legal      |
| Linux   | c:`    | (null) | [PSN:POS]/{G}c:\\/{S}.      | Legal      |
| Windows | c:`    | c:`    | [PSN:WIN]/{R}c/{E}/{R}c/{E} | Illegal    |
| Linux   | c:`    | c:`    | [PSN:POS]/{G}c:\\/{G}c:\\   | Legal      |  # TODO: Timothy, please look at this case.
