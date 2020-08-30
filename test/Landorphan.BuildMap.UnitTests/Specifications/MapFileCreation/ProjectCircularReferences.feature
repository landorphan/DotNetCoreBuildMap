@Check-In
Feature: Project Circular References
	In order to build complicated project structures
	As a developer
	I want to to be able to create a mapping of all projects, their dependencies and their build order.

Scenario: Direct Circular References
	Given I locate the following project files:
		| Name     | Language |
		| Project1 | CSharp   |
		| Project2 | CSharp   |
      And I locate the following solution files: 
		| Name      |
		| Solution1 |
	  And the following solutions contain the following located projects:
	    | Solution  | Project  |
	    | Solution1 | Project1 |
	    | Solution1 | Project2 |
	  And the following projects contain the following references:
	    | Project  | Reference |
	    | Project2 | Project1  |
	    | Project1 | Project2  |
      And the projects and solutions are saved on disk
	 When I create the map file with the following search patterns: **/*.sln;**/*.csproj
	 Then the map file should contain the following projects:
		| Group | Item | Types   | Language | Name     | Status | Solutions | Id | Relative Path            | Dependent On |
		| 0     | 0    | Library | CSharp   | Project1 | Valid  | Solution1 | 1  | Project1`Project1.csproj | 2            |
		| 0     | 0    | Library | CSharp   | Project2 | Valid  | Solution1 | 2  | Project2`Project2.csproj | 1            |

