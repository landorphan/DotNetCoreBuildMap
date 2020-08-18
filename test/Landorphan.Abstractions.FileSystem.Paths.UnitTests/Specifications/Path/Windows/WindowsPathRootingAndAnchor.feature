Feature: Windows Rooting and Anchor Normalization
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to determin the rooting and anchor of a path

Scenario Outline: Get Path Root, Ancor and Relative Paths
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Windows
	 When I parse the path 
     Then the parse status should be <Path Status>
	  And the parse path should be anchored to <Anchor>
	  And get relative path should return: <Relative Path>
	  And the resulting path should be anchored to Relative
	  And the resulting status should be <Resulting Status>
	  And the parse path's root segment should return: <Root Segment>
Examples:
| Path                    | Path Status | Anchor   | Root Segment | Relative Path       | Resulting Status |
| /                       | Legal       | Absolute | {/}          | .                   | Legal            |
| /dir                    | Legal       | Absolute | {/}          | dir                 | Legal            |
| /dir/dir/file           | Legal       | Absolute | {/}          | dir`dir`file        | Legal            |
| dir/dir/file            | Legal       | Relative | {E} (empty)  | dir`dir`file        | Legal            |
| //server/share/dir/file | Legal       | Absolute | {U} server   | share`dir`file      | Legal            |
| C:`                     | Legal       | Absolute | {R} C        | .                   | Legal            |
| C:`dir`file             | Legal       | Absolute | {R} C        | dir`file            | Legal            |
| C:`C:`dir`file          | Illegal     | Absolute | {R} C        | dir`file            | Legal            |
| C:`dir`C:`dir`dir`file  | Illegal     | Absolute | {R} C        | dir`C:`dir`dir`file | Illegal          |