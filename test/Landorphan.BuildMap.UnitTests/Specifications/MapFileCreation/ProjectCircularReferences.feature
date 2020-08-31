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
		| Group | Item | Types   | Language | Name     | Status   | Solutions | Id | Relative Path            | Dependent On |
		| -1    | 0    | Library | CSharp   | Project1 | Circular | Solution1 | 1  | Project1`Project1.csproj | 2            |
		| -1    | 0    | Library | CSharp   | Project2 | Circular | Solution1 | 2  | Project2`Project2.csproj | 1            |

Scenario: Indirect Circular Reference 
	Given I locate the following project files:
		| Name     | Language |
		| Project1 | CSharp   |
		| Project2 | CSharp   |
		| Project3 | CSharp   |
		| Project4 | CSharp   |
      And I locate the following solution files: 
		| Name      |
		| Solution1 |
	  And the following solutions contain the following located projects:
	    | Solution  | Project  |
	    | Solution1 | Project1 |
	    | Solution1 | Project2 |
	    | Solution1 | Project3 |
	    | Solution1 | Project4 |
	  And the following projects contain the following references:
	    | Project  | Reference |
	    | Project3 | Project1  |
	    | Project2 | Project3  |
	    | Project1 | Project2  |
	    | Project4 | Project1  |
      And the projects and solutions are saved on disk
	 When I create the map file with the following search patterns: **/*.sln;**/*.csproj
	 Then the map file should contain the following projects:
		| Group | Item | Types   | Language | Name     | Status   | Solutions | Id | Relative Path            | Dependent On |
		| -1    | 0    | Library | CSharp   | Project1 | Circular | Solution1 | 1  | Project1`Project1.csproj | 2            |
		| -1    | 0    | Library | CSharp   | Project2 | Circular | Solution1 | 2  | Project2`Project2.csproj | 3            |
		| -1    | 0    | Library | CSharp   | Project3 | Circular | Solution1 | 3  | Project3`Project3.csproj | 1            |
		| 0     | 0    | Library | CSharp   | Project4 | Valid    | Solution1 | 4  | Project4`Project4.csproj | 1            |

Scenario: Second Level Indirect Circular Reference 
	Given I locate the following project files:
		| Name     | Language |
		| Project1 | CSharp   |
		| Project2 | CSharp   |
		| Project3 | CSharp   |
		| Project4 | CSharp   |
		| Project5 | CSharp   |
      And I locate the following solution files: 
		| Name      |
		| Solution1 |
	  And the following solutions contain the following located projects:
	    | Solution  | Project  |
	    | Solution1 | Project1 |
	    | Solution1 | Project2 |
	    | Solution1 | Project3 |
	    | Solution1 | Project4 |
	    | Solution1 | Project5 |
	  And the following projects contain the following references:
	    | Project  | Reference |
	    | Project1 | Project4  |
	    | Project2 | Project1  |
	    | Project3 | Project2  |
	    | Project4 | Project3  |
	    | Project5 | Project1  |
      And the projects and solutions are saved on disk
	 When I create the map file with the following search patterns: **/*.sln;**/*.csproj
	 Then the map file should contain the following projects:
		| Group | Item | Types   | Language | Name     | Status   | Solutions | Id | Relative Path            | Dependent On |
		| -1    | 0    | Library | CSharp   | Project1 | Circular | Solution1 | 1  | Project1`Project1.csproj | 4            |
		| -1    | 0    | Library | CSharp   | Project2 | Circular | Solution1 | 2  | Project2`Project2.csproj | 1            |
		| -1    | 0    | Library | CSharp   | Project3 | Circular | Solution1 | 3  | Project3`Project3.csproj | 2            |
		| -1    | 0    | Library | CSharp   | Project4 | Circular | Solution1 | 4  | Project4`Project4.csproj | 3            |
		| 0     | 0    | Library | CSharp   | Project5 | Valid    | Solution1 | 5  | Project5`Project5.csproj | 1            |