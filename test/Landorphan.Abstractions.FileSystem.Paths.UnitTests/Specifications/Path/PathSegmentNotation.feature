@Check-In
Feature: Path Segment Notation
	In order to round trip path's to strings
	As a developer
	I want to to be able to convert strings to path and back without loosing fidelity

Scenario Outline: I can convert paths to and from Path Segment Notation
# Test 1 Convert the path to Path Segment Notation
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: <OS>
	  And I parse the path
	 When I convert the path to path segment notation
	 Then The following PSN string should be produced: <PSN Form>

# Test 2 Convert back to a path from PSN form
	Given I have the following path: <PSN Form>
	 When I re-parse the path
	 Then the two paths should be the same
Examples:
| Path                                        | OS      | PSN Form                                    | Notes                                                                                       |
| (empty)                                     | Windows | [PSN:WIN]/{S}.                              | Empty segemnts are converted to self segments                                               |
| (empty)                                     | Linux   | [PSN:POS]/{S}.                              | Empty segemnts are converted to self segments                                               |
| (null)                                      | Windows | [PSN:WIN]/{S}.                              | Null segemnts are converted to self segments                                                |
| (null)                                      | Linux   | [PSN:POS]/{S}.                              | Null segemnts are converted to self segments                                                |
| a`b`c                                       | Windows | [PSN:WIN]/{G}a/{G}b/{G}c                    | Basic Generic Path                                                                          |
| a/b/c                                       | Linux   | [PSN:POS]/{G}a/{G}b/{G}c                    | Basic Generic Path                                                                          |
| `                                           | Windows | [PSN:WIN]/{$}                               | Volumeless Root Segment                                                                     |
| /                                           | Linux   | [PSN:POS]/{R}                               | Root Segment                                                                                |
| `a                                          | Windows | [PSN:WIN]/{$}/{G}a                          | VRS + G                                                                                     |
| /a                                          | Linux   | [PSN:POS]/{R}/{G}a                          | Root + G                                                                                    |
| C:`a                                        | Windows | [PSN:WIN]/{R}C/{G}a                         | Root + G                                                                                    |
| C:/a                                        | Linux   | [PSN:POS]/{G}C:/{G}a                        | G + G                                                                                       |
| C:a                                         | Windows | [PSN:WIN]/{V}C/{G}a                         | V + G                                                                                       |
| C:a                                         | Linux   | [PSN:POS]/{G}C:a                            | G                                                                                           |
| ``server`dir                                | Windows | [PSN:WIN]/{X}server/{G}dir                  | Remote + Generic                                                                            |
| //server/dir                                | Linux   | [PSN:POS]/{X}server/{G}dir                  | Remote + Generic                                                                            |
| .`..`dir                                    | Windows | [PSN:WIN]/{S}./{P}../{G}dir                 | Self + Parent + Generic                                                                     |
| ./../dir                                    | Linux   | [PSN:POS]/{S}./{P}../{G}dir                 | Self + Parent + Generic                                                                     |
| C:`dir``dir                                 | Windows | [PSN:WIN]/{R}C/{G}dir/{E}/{G}dir            | Root + Generic + Empty + Generic                                                            |
| /dir//dir                                   | Linux   | [PSN:POS]/{R}/{G}dir/{E}/{G}dir             | Root + Generic + Empty + Generic                                                            |
| C:`dir`CON                                  | Windows | [PSN:WIN]/{R}C/{G}dir/{D}CON                | Device (Note the path does not auto simplify                                                |
| C:/dir/CON                                  | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}CON               | Generic + Generic + Generic                                                                 |
| C:`dir`file%00name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%00name.txt    | Encoded Characters                                                                          |
| C:/dir/file%00name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%00name.txt   | Encoded Characters                                                                          |
| C:`dir`file%01name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%01name.txt    | Encoded Characters                                                                          |
| C:/dir/file%01name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%01name.txt   | Encoded Characters                                                                          |
| C:`dir`file%02name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%02name.txt    | Encoded Characters                                                                          |
| C:/dir/file%02name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%02name.txt   | Encoded Characters                                                                          |
| C:`dir`file%03name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%03name.txt    | Encoded Characters                                                                          |
| C:/dir/file%03name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%03name.txt   | Encoded Characters                                                                          |
| C:`dir`file%04name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%04name.txt    | Encoded Characters                                                                          |
| C:/dir/file%04name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%04name.txt   | Encoded Characters                                                                          |
| C:`dir`file%05name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%05name.txt    | Encoded Characters                                                                          |
| C:/dir/file%05name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%05name.txt   | Encoded Characters                                                                          |
| C:`dir`file%06name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%06name.txt    | Encoded Characters                                                                          |
| C:/dir/file%06name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%06name.txt   | Encoded Characters                                                                          |
| C:`dir`file%07name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%07name.txt    | Encoded Characters                                                                          |
| C:/dir/file%07name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%07name.txt   | Encoded Characters                                                                          |
| C:`dir`file%08name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%08name.txt    | Encoded Characters                                                                          |
| C:/dir/file%08name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%08name.txt   | Encoded Characters                                                                          |
| C:`dir`file%09name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%09name.txt    | Encoded Characters                                                                          |
| C:/dir/file%09name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%09name.txt   | Encoded Characters                                                                          |
| C:`dir`file%0Aname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%0Aname.txt    | Encoded Characters                                                                          |
| C:/dir/file%0Aname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%0Aname.txt   | Encoded Characters                                                                          |
| C:`dir`file%0Bname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%0Bname.txt    | Encoded Characters                                                                          |
| C:/dir/file%0Bname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%0Bname.txt   | Encoded Characters                                                                          |
| C:`dir`file%0Cname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%0Cname.txt    | Encoded Characters                                                                          |
| C:/dir/file%0Cname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%0Cname.txt   | Encoded Characters                                                                          |
| C:`dir`file%0Dname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%0Dname.txt    | Encoded Characters                                                                          |
| C:/dir/file%0Dname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%0Dname.txt   | Encoded Characters                                                                          |
| C:`dir`file%0Ename.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%0Ename.txt    | Encoded Characters                                                                          |
| C:/dir/file%0Ename.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%0Ename.txt   | Encoded Characters                                                                          |
| C:`dir`file%0Fname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%0Fname.txt    | Encoded Characters                                                                          |
| C:/dir/file%0Fname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%0Fname.txt   | Encoded Characters                                                                          |
| C:`dir`file%10name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%10name.txt    | Encoded Characters                                                                          |
| C:/dir/file%10name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%10name.txt   | Encoded Characters                                                                          |
| C:`dir`file%11name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%11name.txt    | Encoded Characters                                                                          |
| C:/dir/file%11name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%11name.txt   | Encoded Characters                                                                          |
| C:`dir`file%12name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%12name.txt    | Encoded Characters                                                                          |
| C:/dir/file%12name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%12name.txt   | Encoded Characters                                                                          |
| C:`dir`file%13name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%13name.txt    | Encoded Characters                                                                          |
| C:/dir/file%13name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%13name.txt   | Encoded Characters                                                                          |
| C:`dir`file%14name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%14name.txt    | Encoded Characters                                                                          |
| C:/dir/file%14name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%14name.txt   | Encoded Characters                                                                          |
| C:`dir`file%15name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%15name.txt    | Encoded Characters                                                                          |
| C:/dir/file%15name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%15name.txt   | Encoded Characters                                                                          |
| C:`dir`file%16name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%16name.txt    | Encoded Characters                                                                          |
| C:/dir/file%16name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%16name.txt   | Encoded Characters                                                                          |
| C:`dir`file%17name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%17name.txt    | Encoded Characters                                                                          |
| C:/dir/file%17name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%17name.txt   | Encoded Characters                                                                          |
| C:`dir`file%18name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%18name.txt    | Encoded Characters                                                                          |
| C:/dir/file%18name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%18name.txt   | Encoded Characters                                                                          |
| C:`dir`file%19name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%19name.txt    | Encoded Characters                                                                          |
| C:/dir/file%19name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%19name.txt   | Encoded Characters                                                                          |
| C:`dir`file%1Aname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%1Aname.txt    | Encoded Characters                                                                          |
| C:/dir/file%1Aname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%1Aname.txt   | Encoded Characters                                                                          |
| C:`dir`file%1Bname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%1Bname.txt    | Encoded Characters                                                                          |
| C:/dir/file%1Bname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%1Bname.txt   | Encoded Characters                                                                          |
| C:`dir`file%1Cname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%1Cname.txt    | Encoded Characters                                                                          |
| C:/dir/file%1Cname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%1Cname.txt   | Encoded Characters                                                                          |
| C:`dir`file%1Dname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%1Dname.txt    | Encoded Characters                                                                          |
| C:/dir/file%1Dname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%1Dname.txt   | Encoded Characters                                                                          |
| C:`dir`file%1Ename.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%1Ename.txt    | Encoded Characters                                                                          |
| C:/dir/file%1Ename.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%1Ename.txt   | Encoded Characters                                                                          |
| C:`dir`file%1Fname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%1Fname.txt    | Encoded Characters                                                                          |
| C:/dir/file%1Fname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%1Fname.txt   | Encoded Characters                                                                          |
| C:`dir`file%20name.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%20name.txt    | Encoded Characters                                                                          |
| C:/dir/file%20name.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%20name.txt   | Encoded Characters                                                                          |
| C:`dir`file%name.txt                        | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%25name.txt    | Encoded Characters                                                                          |
| C:/dir/file%name.txt                        | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%25name.txt   | Encoded Characters                                                                          |
| C:`dir`file%2Fname.txt                      | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%252Fname.txt  | NOTE: It's imposible to embed a '/' using normal path syntax so this is the expected result |
| C:/dir/file%2Fname.txt                      | Linux   | [PSN:POS]/{G}C:/{G}dir/{G}file%252Fname.txt | NOTE: It's imposible to embed a '/' using normal path syntax so this is the expected result |
| [PSN:WIN]/{R} C/{G} dir/{G} file%2Fname.txt | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file%2Fname.txt    | The only way to embed a '/' in a path would be through PSN (the result is an illegal path)  |
| [PSN:POS]/{R} C/{G} dir/{G} file%2Fname.txt | Linux   | [PSN:POS]/{R}C/{G}dir/{G}file%2Fname.txt    | The only way to embed a '/' in a path would be through PSN (the result is an illegal path)  |
| [PSN:WIN]/{R} C/{G} dir/{G} file`name.txt   | Windows | [PSN:WIN]/{R}C/{G}dir/{G}file`name.txt      | The '\' character is acceptable in PSN but will generate an illegal path                    |
| [PSN:POS]/{R} C/{G} dir/{G} file`name.txt   | Linux   | [PSN:POS]/{R}C/{G}dir/{G}file`name.txt      | The '\' character is acceptable in PSN but will generate an illegal path                    |