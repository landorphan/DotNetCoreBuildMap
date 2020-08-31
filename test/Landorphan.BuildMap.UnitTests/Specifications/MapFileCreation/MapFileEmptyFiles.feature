@Check-In
Feature: Map File Emnpty Files
	In order to build complicated project structures
	As a developer
	I want to to be able to create a mapping of all projects, their dependencies and their build order.

Scenario: Single Solution with missing project files
	Given I locate the following project files:
		| Name     | Language | Status  |
		| Project1 | CSharp   | Valid   |
		| Project2 | CSharp   | Empty   |
      And I locate the following solution files: 
		| Name      |
		| Solution1 |
	  And the following solutions contain the following located projects:
	    | Solution  | Project  |
	    | Solution1 | Project1 |
	    | Solution1 | Project2 |
	  And the following projects contain the following references:
 		# NOTE: This is actually not used because Project2 is empty on disk in this test.
	    | Project  | Reference |
	    | Project2 | Project1  |
	  And the following solutions define the following additional dependencies:
	    | Solution  | Base Project | Dependent On |
	    | Solution1 | Project2     | Project1     |
      And the projects and solutions are saved on disk
	 When I create the map file with the following search patterns: **/*.sln
	 Then the map file should contain the following projects:
		| Group | Item | Types   | Language | Name     | Status  | Solutions | Id | Relative Path            | Dependent On |
		| 0     | 0    | Library | CSharp   | Project1 | Valid   | Solution1 | 1  | Project1`Project1.csproj |              |
		| -1    | 0    | Library | CSharp   | Project2 | Empty   | Solution1 | 2  | Project2`Project2.csproj |              |

Scenario: Multiple solution with missing project files.
	Given I locate the following project files:
		| Name     | Language    | Status  |
		| Project1 | CSharp      | Valid   |
		| Project2 | FSharp      | Valid   |
		| Project3 | VisualBasic | Valid   |
		| Project4 | CSharp      | Missing |
      And I locate the following solution files: 
		| Name      |
		| Solution1 |
		| Solution2 |
	  And the following solutions contain the following located projects:
	    | Solution  | Project  |
	    | Solution1 | Project1 |
	    | Solution1 | Project2 |
	    | Solution1 | Project3 |
	    | Solution2 | Project1 |
	    | Solution2 | Project4 |
	  And the following projects contain the following references:
	    | Project  | Reference |
	    | Project2 | Project1  |
	    | Project3 | Project1  |
	  And the following solutions define the following additional dependencies:
	    | Solution  | Base Project | Dependent On |
	    | Solution1 | Project3     | Project2     |
      And the projects and solutions are saved on disk
	 When I create the map file with the following search patterns: **/*.sln;**/*.csproj;**/*.vbproj;**/*.fsproj
	 Then the map file should contain the following projects:
		| Group | Item | Types   | Language    | Name     | Status  | Solutions           | Id | Relative Path            | Dependent On |
		| 0     | 0    | Library | CSharp      | Project1 | Valid   | Solution1,Solution2 | 1  | Project1`Project1.csproj |              |
		| 0     | 0    | Library | FSharp      | Project2 | Valid   | Solution1           | 2  | Project2`Project2.fsproj | 1            |
		| 0     | 0    | Library | VisualBasic | Project3 | Valid   | Solution1           | 3  | Project3`Project3.vbproj | 1            |
		| -1    | 0    | Library | CSharp      | Project4 | Missing | Solution2           | 4  | Project4`Project4.csproj |              |