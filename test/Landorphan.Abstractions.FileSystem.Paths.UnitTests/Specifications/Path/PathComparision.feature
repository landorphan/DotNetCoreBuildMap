@Check-In
Feature: Path Comparison
	In order to work with paths
	As a developer
	I need to be able to compare two paths

# TODO: mocking mechansim for PathUtilities.DefaultComparison (i.e. on Windows run a Linux test).

Scenario Outline: Comare two paths
	Given I'm running on the following Operating System: <OS>
	  And I parse the following as path 1: <Path 1>
	  And I parse the following as path 2: <Path 2>
	 When I compare the paths using the <Comparer> comparer
	 Then path 1 should be <Compare Type> path 2
Examples:
| OS      | Path 1 | Path 2 | Compare Type | Comparer    |
# | Linux   | /      | /      | equal to     | Default     |
# | Linux   | /a     | /b     | less than    | Default     |
# | Linux   | /b     | /a     | greater than | Default     |
# | Linux   | /a     | /a     | equal to     | Default     |
# | Linux   | /A     | /a     | less than    | Default     |
# | Linux   | /a     | /A     | greater than | Default     |
| Linux   | (null) | /A     | less than    | Sensitive   |
| Linux   | /a     | (null) | greater than | Sensitive   |
| Linux   | (null) | (null) | equal to     | Sensitive   |
| Linux   | /      | /      | equal to     | Sensitive   |
| Linux   | /a     | /b     | less than    | Sensitive   |
| Linux   | /b     | /a     | greater than | Sensitive   |
| Linux   | /a     | /a     | equal to     | Sensitive   |
| Linux   | /A     | /a     | less than    | Sensitive   |
| Linux   | /a     | /A     | greater than | Sensitive   |
| Linux   | /a     | /a     | equal to     | Insensitive |
| Linux   | /A     | /a     | equal to     | Insensitive |
| Linux   | /a     | /A     | equal to     | Insensitive |
# | Windows | /      | /      | equal to     | Default     |
# | Windows | /a     | /b     | less than    | Default     |
# | Windows | /b     | /a     | greater than | Default     |
# | Windows | /a     | /a     | equal to     | Default     |
# | Windows | /A     | /a     | equal to     | Default     |
# | Windows | /a     | /A     | equal to     | Default     |
| Windows | (null) | /A     | less than    | Insensitive |
| Windows | /a     | (null) | greater than | Insensitive |
| Windows | (null) | (null) | equal to     | Insensitive |
| Windows | /      | /      | equal to     | Sensitive   |
| Windows | /a     | /b     | less than    | Sensitive   |
| Windows | /b     | /a     | greater than | Sensitive   |
| Windows | /a     | /a     | equal to     | Sensitive   |
| Windows | /A     | /a     | less than    | Sensitive   |
| Windows | /a     | /A     | greater than | Sensitive   |
| Windows | /a     | /a     | equal to     | Insensitive |
| Windows | /A     | /a     | equal to     | Insensitive |
| Windows | /a     | /A     | equal to     | Insensitive |
