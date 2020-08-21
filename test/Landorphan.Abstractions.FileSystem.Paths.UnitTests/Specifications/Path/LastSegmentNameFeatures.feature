Feature: Path last segment name features
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want path segment names to be handled corectly

Scenario Outline: Get Segment Name Information (Last Segment)
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Linux
	 When I parse the path 
	 Then the parsed path's <Retreival> should be: <Value>
	  And the path's has extension property is: <Has Extension>
Examples:
| Path           | Retreival            | Value      | Has Extension |
| foo.txt        | Name                 | foo.txt    | true          |
| foo.txt        | NameWithoutExtension | foo        | true          |
| foo.txt        | Extension            | .txt       | true          |
| .gitignore     | NameWithoutExtension | .gitignore | false         |
| .gitignore     | Extension            | (empty)    | false         |
| .gitignore.txt | NameWithoutExtension | .gitignore | true          |
| .gitignore.txt | Extension            | .txt       | true          |
| file           | Name                 | file       | false         |
| file           | NameWithoutExtension | file       | false         |
| file           | Extension            | (empty)    | false         |
