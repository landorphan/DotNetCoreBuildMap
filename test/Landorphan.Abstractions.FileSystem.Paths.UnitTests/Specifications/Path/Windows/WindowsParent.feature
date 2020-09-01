Feature: Windows Parent
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want to retreive the parent of a given path

Scenario Outline: I can get the parent of the current path
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Windows
	 When I parse the path 
	  And I ask for the parent path
	 Then the resulting path should read: <Parent>
Examples:
| Path             | Parent  |
| /                | `       |
| C:/              | C:`     |
| /dir/dir         | `dir    |
| dir/dir          | dir     |
| dir/dir/file.txt | dir`dir |
| ./././.          | ..      |
| ..               | ..`..   |
| .                | ..      |
| a/b/c/../        | a       |
| a/b/c/../..      | .       |
| a/b/c/../d/..    | a       |
| (null)           | ..      |
| (empty)          | ..      |