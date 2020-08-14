Feature: Windows Path ToString
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to convert incoming paths objects into a readable string

Scenario Outline: Windows Paths can be converted back to the correct string
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Windows
      And I parse the path 
	 When I ask for the path to be represented as a string
	 Then no exception should be thrown
	  And I should receive the following string: <Result>
Examples: 
| Path                           | Result                         | Notes                                      |
| (null)                         | (empty)                        | Null Paths turn into empty paths           |
| (empty)                        | (empty)                        | Empty                                      |
| .                              | .                              | Self Segment                               |
| ..                             | ..                             | Parent Segment                             |
| C:`                            | C:`                            | Volume Root Segment                        |
| C:`foo                         | C:`foo                         | Volume Root Segment                        |
| ..`                            | ..`                            | Parent + Empty                             |
| .`                             | .`                             | Parent + Self                              |
| .`file.txt                     | .`file.txt                     | Self + File                                |
| C:foo.txt                      | C:foo.txt                      | Volume Relative + File                     |
| c:bar`foo.txt                  | c:bar`foo.txt                  | Volume Relative + Dir + File               |
| ``server`share`file.txt        | ``server`share`file.txt        | UNC Server + Share + File                  |
| `dir``dir``file.txt            | `dir``dir``file.txt            | Empty paths are kept unless normalized     |
| C:`dir`file.txt`               | C:`dir`file.txt`               | Trailing slashs are kept unless normalized |
| `dir`dir`..`dir`..`..`file.txt | `dir`dir`..`dir`..`..`file.txt | Embedded Parent Segemnts                   |
| `dir`dir`.`dir`.`dir`file.txt  | `dir`dir`.`dir`.`dir`file.txt  | Embedded Self Segments                     |
| /                              | `                              | Root Path (note the change of slash types) |
