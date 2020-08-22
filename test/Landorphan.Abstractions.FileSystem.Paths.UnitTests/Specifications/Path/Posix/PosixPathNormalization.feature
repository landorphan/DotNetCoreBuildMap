@Check-In
Feature: Posix Path Simplification
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to convert incoming paths into a more managable form

Scenario Outline: Posix Paths can be simplified to best available form.
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Linux
	  And I parse the path 
	  And the parse status should be <Path Status>
	  And the parse path should be anchored to <Anchor>
	  And the parse path's root segment should return: <Root Segment>
	  And the parse path's FullyQualified property should be: <Fully Qualified>
     When I normalize the path
	 Then the resulting path should read: <Normalized Path>
	  And the resulting status should be <Path Status>
	  And the resulting path should be anchored to <Anchor>
	  And the resulting path's FullyQualified property should be: <Fully Qualified>
	  And the resulting path's root segment should return: <Root Segment>
	  And the resulting path should have the following Simplification Level: <Simplification Level>
Examples:
# NOTE: Due to Gherkin parsing rules, \ needs to be escaped.  In order to avoid that necissity and
# make the following examples easier to read (/) will be used in place of the (\) character
| Path                         | Path Status | Anchor   | Fully Qualified | Root Segment | Normalized Path             | Simplification Level | Notes                                                                                              |
| (null)                       | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | Imposible for Null path to normalize                                                               |
| (empty)                      | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | Imposible for Empty path to normalize                                                              |
| C:/                          | Legal       | Relative | false           | {E} (empty)  | C:                          | Fully                | The trailing slash adds an empty segment thus it's not normalized                                  |
| /                            | Legal       | Absolute | true            | {R}          | /                           | Fully                | A root only segment should be created and this path is normalized                                  |
| /foo/bar                     | Legal       | Absolute | true            | {R}          | /foo/bar                    | Fully                | This path only contains root and generic segments                                                  |
| foo/bar                      | Legal       | Relative | false           | {E} (empty)  | foo/bar                     | Fully                | This path is relative but fully normalized                                                         |
| foo/../bar                   | Legal       | Relative | false           | {E} (empty)  | bar                         | Fully                | This path is not normal because of the parent segment but can be fully normalized                  |
| ../foo/bar                   | Legal       | Relative | false           | {E} (empty)  | ../foo/bar                  | LeadingParentsOnly   | This path is as normalized as it can be ... leading parent segements can't be removed              |
| C:/dir/file.txt              | Legal       | Relative | false           | {E} (empty)  | C:/dir/file.txt             | Fully                | This path is fully normalized                                                                      |
| C:/dir/file.txt/             | Legal       | Relative | false           | {E} (empty)  | C:/dir/file.txt             | Fully                | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:/dir                       | Legal       | Relative | false           | {E} (empty)  | C:/dir                      | Fully                | This path is fully normalized                                                                      |
| C:/dir/                      | Legal       | Relative | false           | {E} (empty)  | C:/dir                      | Fully                | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:./file.txt/                | Legal       | Relative | false           | {E} (empty)  | C:./file.txt                | Fully                | The self reference and the trailing slash prevent this from being normalized                       |
| C:file.txt                   | Legal       | Relative | false           | {E} (empty)  | C:file.txt                  | Fully                | While not relative this path is still normalized                                                   |
| C:file.txt/                  | Legal       | Relative | false           | {E} (empty)  | C:file.txt                  | Fully                | Trailing Slash prevents normalization                                                              |
| C:dir                        | Legal       | Relative | false           | {E} (empty)  | C:dir                       | Fully                | Fully normalized                                                                                   |
| C:dir/                       | Legal       | Relative | false           | {E} (empty)  | C:dir                       | Fully                | Trailing slash prevents normalziaiton                                                              |
| C:dir/file.txt               | Legal       | Relative | false           | {E} (empty)  | C:dir/file.txt              | Fully                | Fully normalized                                                                                   |
| C:dir/file.txt/              | Legal       | Relative | false           | {E} (empty)  | C:dir/file.txt              | Fully                | Trailing Slash                                                                                     |
| //server/share               | Legal       | Absolute | true            | {X} server   | //server/share              | Fully                | Fully normalized remote path                                                                       |
| //server/share/              | Legal       | Absolute | true            | {X} server   | //server/share              | Fully                | Trailing Slash                                                                                     |
| //server/file.txt            | Legal       | Absolute | true            | {X} server   | //server/file.txt           | Fully                | Fully normalized remote path                                                                       |
| //server/file.txt/           | Legal       | Absolute | true            | {X} server   | //server/file.txt           | Fully                | Trailing Slash                                                                                     |
| //server/share/dir/file.txt  | Legal       | Absolute | true            | {X} server   | //server/share/dir/file.txt | Fully                | Fully normalized remote path                                                                       |
| //server/share/dir/file.txt/ | Legal       | Absolute | true            | {X} server   | //server/share/dir/file.txt | Fully                | Trailing Slash                                                                                     |
| .                            | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | This is normalized as best as posilbe, its a special case where the path only has a self reference |
| ./                           | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | Self Reference ant Trailing Slash                                                                  |
| ./file.txt                   | Legal       | Relative | false           | {E} (empty)  | file.txt                    | Fully                | The self reference could be removed in this case                                                   |
| ./file.txt/                  | Legal       | Relative | false           | {E} (empty)  | file.txt                    | Fully                | The Self reference and the trailing path                                                           |
| ./dir                        | Legal       | Relative | false           | {E} (empty)  | dir                         | Fully                | The self reference                                                                                 |
| ./dir/                       | Legal       | Relative | false           | {E} (empty)  | dir                         | Fully                | The self reference and the trailing slash                                                          |
| ./dir/file.txt               | Legal       | Relative | false           | {E} (empty)  | dir/file.txt                | Fully                | The self reference                                                                                 |
| ./dir/file.txt/              | Legal       | Relative | false           | {E} (empty)  | dir/file.txt                | Fully                | The self refrence and the trialing slash                                                           |
| ..                           | Legal       | Relative | false           | {E} (empty)  | ..                          | LeadingParentsOnly   | Only has a leading parent ... best posible normalization                                           |
| ../                          | Legal       | Relative | false           | {E} (empty)  | ..                          | LeadingParentsOnly   | Trailing slash                                                                                     |
| ../dir/file.txt              | Legal       | Relative | false           | {E} (empty)  | ../dir/file.txt             | LeadingParentsOnly   | Only has leading parents ... best posible normalization                                            |
| ../dir/file.txt/             | Legal       | Relative | false           | {E} (empty)  | ../dir/file.txt             | LeadingParentsOnly   | Trailing slash                                                                                     |
| a/b/./c/../../d/../../../e   | Legal       | Relative | false           | {E} (empty)  | ../e                        | LeadingParentsOnly   | To many resons to mention                                                                          |
| a/b/./c/../d/../../e         | Legal       | Relative | false           | {E} (empty)  | a/e                         | Fully                | To many resons to mention                                                                          |
| ./././././                   | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | Nothing but self references = one self reference                                                   |
| .//.//.//.                   | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | Same as above as empty segments count as self references for normalization purposes                |
| C:/..                        | Legal       | Relative | false           | {E} (empty)  | .                           | SelfReferenceOnly    | Parrent trversal stops at the first "rooted" segment                                               |
| C:/../..                     | Legal       | Relative | false           | {E} (empty)  | ..                          | LeadingParentsOnly   | Parrent trversal stops at the first "rooted" segment                                               |
| /..                          | Legal       | Absolute | true            | {R}          | /                           | Fully                | Parrent trversal stops at the first "rooted" segment                                               |
| /../..                       | Legal       | Absolute | true            | {R}          | /                           | Fully                | Parrent trversal stops at the first "rooted" segment                                               |
| //server/..                  | Legal       | Absolute | true            | {X} server   | //server                    | Fully                | Parrent trversal stops at the first "rooted" segment                                               |
| //server/../..               | Legal       | Absolute | true            | {X} server   | //server                    | Fully                | Parrent trversal stops at the first "rooted" segment                                               |
