@Check-In
Feature: Posix Path Normalization
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to convert incoming paths into a more managable form

Scenario Outline: Posix Paths can be normalized to best available form.
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Linux
	  And I parse the path 
     When I normalize the path
	 Then the resulting path should read: <Normalized Path>
	  And the resulting path should have the following Normalization Level: <Normalization Level>
Examples:
# NOTE: Due to Gherkin parsing rules, \ needs to be escaped.  In order to avoid that necissity and
# make the following examples easier to read (/) will be used in place of the (\) character
| Path                         | Normalized Path             | Normalization Level | Notes                                                                                              |
| (null)                       | .                           | SelfReferenceOnly   | Imposible for Null path to normalize                                                               |
| (empty)                      | .                           | SelfReferenceOnly   | Imposible for Empty path to normalize                                                              |
| C:/                          | C:                          | Fully               | The trailing slash adds an empty segment thus it's not normalized                                  |
| /                            | /                           | Fully               | A root only segment should be created and this path is normalized                                  |
| /foo/bar                     | /foo/bar                    | Fully               | This path only contains root and generic segments                                                  |
| foo/bar                      | foo/bar                     | Fully               | This path is relative but fully normalized                                                         |
| foo/../bar                   | bar                         | Fully               | This path is not normal because of the parent segment but can be fully normalized                  |
| ../foo/bar                   | ../foo/bar                  | LeadingParentsOnly  | This path is as normalized as it can be ... leading parent segements can't be removed              |
| C:/dir/file.txt              | C:/dir/file.txt             | Fully               | This path is fully normalized                                                                      |
| C:/dir/file.txt/             | C:/dir/file.txt             | Fully               | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:/dir                       | C:/dir                      | Fully               | This path is fully normalized                                                                      |
| C:/dir/                      | C:/dir                      | Fully               | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:./file.txt                 | C:./file.txt                | Fully               | This is a Posix path so the '.' is not a self reference but part of the name ... thus Fully        |
| C:./file.txt/                | C:./file.txt                | Fully               | The self reference and the trailing slash prevent this from being normalized                       |
| C:file.txt                   | C:file.txt                  | Fully               | While not relative this path is still normalized                                                   |
| C:file.txt/                  | C:file.txt                  | Fully               | Trailing Slash prevents normalization                                                              |
| C:dir                        | C:dir                       | Fully               | Fully normalized                                                                                   |
| C:dir/                       | C:dir                       | Fully               | Trailing slash prevents normalziaiton                                                              |
| C:dir/file.txt               | C:dir/file.txt              | Fully               | Fully normalized                                                                                   |
| C:dir/file.txt/              | C:dir/file.txt              | Fully               | Trailing Slash                                                                                     |
| //server/share               | //server/share              | Fully               | Fully normalized remote path                                                                       |
| //server/share/              | //server/share              | Fully               | Trailing Slash                                                                                     |
| //server/file.txt            | //server/file.txt           | Fully               | Fully normalized remote path                                                                       |
| //server/file.txt/           | //server/file.txt           | Fully               | Trailing Slash                                                                                     |
| //server/share/dir/file.txt  | //server/share/dir/file.txt | Fully               | Fully normalized remote path                                                                       |
| //server/share/dir/file.txt/ | //server/share/dir/file.txt | Fully               | Trailing Slash                                                                                     |
| .                            | .                           | SelfReferenceOnly   | This is normalized as best as posilbe, its a special case where the path only has a self reference |
| ./                           | .                           | SelfReferenceOnly   | Self Reference ant Trailing Slash                                                                  |
| ./file.txt                   | file.txt                    | Fully               | The self reference could be removed in this case                                                   |
| ./file.txt/                  | file.txt                    | Fully               | The Self reference and the trailing path                                                           |
| ./dir                        | dir                         | Fully               | The self reference                                                                                 |
| ./dir/                       | dir                         | Fully               | The self reference and the trailing slash                                                          |
| ./dir/file.txt               | dir/file.txt                | Fully               | The self reference                                                                                 |
| ./dir/file.txt/              | dir/file.txt                | Fully               | The self refrence and the trialing slash                                                           |
| ..                           | ..                          | LeadingParentsOnly  | Only has a leading parent ... best posible normalization                                           |
| ../                          | ..                          | LeadingParentsOnly  | Trailing slash                                                                                     |
| ../dir/file.txt              | ../dir/file.txt             | LeadingParentsOnly  | Only has leading parents ... best posible normalization                                            |
| ../dir/file.txt/             | ../dir/file.txt             | LeadingParentsOnly  | Trailing slash                                                                                     |
| a/b/./c/../../d/../../../e   | ../e                        | LeadingParentsOnly  | To many resons to mention                                                                          |
| a/b/./c/../d/../../e         | a/e                         | Fully               | To many resons to mention                                                                          |
| ./././././                   | .                           | SelfReferenceOnly   | Nothing but self references = one self reference                                                   |
| .//.//.//.                   | .                           | SelfReferenceOnly   | Same as above as empty segments count as self references for normalization purposes                |
| /..                          | /                           | Fully               | Parrent trversal stops at the first "rooted" segment                                               |
| /../..                       | /                           | Fully               | Parrent trversal stops at the first "rooted" segment                                               |
| //server/..                  | //server                    | Fully               | Parrent trversal stops at the first "rooted" segment                                               |
| //server/../..               | //server                    | Fully               | Parrent trversal stops at the first "rooted" segment                                               |
