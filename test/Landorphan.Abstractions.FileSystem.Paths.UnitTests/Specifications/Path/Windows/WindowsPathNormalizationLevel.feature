@Check-In
Feature: Windows Path Normalization Level
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to convert incoming paths into a more managable form

Scenario Outline: Windows Paths can be normalized to best available form.
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Windows
     When I parse the path 
	 Then the resulting path should have the following Normalization Level: <Normalization Level>
Examples:
# NOTE: We are using the "Alt" Path seperator here ... to keep consistantcy with the Posix paths.
# The path parser accepts both back and forward slash carachters as path seperators for Windows Paths 
# BUT NOTE: It only accepts foward slash characters for Posix paths.
| Path                         | Normalization Level | Notes                                                                                              |
| (null)                       | NotNormalized       | Imposible for Null path to normalize                                                               |
| (empty)                      | NotNormalized       | Imposible for Empty path to normalize                                                              |
| C:/                          | NotNormalized       | The trailing slash adds an empty segment thus it's not normalized                                  |
| /                            | Fully               | A root only segment should be created and this path is normalized                                  |
| /foo/bar                     | Fully               | This path only contains root and generic segments                                                  |
| foo/bar                      | Fully               | This path is relative but fully normalized                                                         |
| foo/../bar                   | NotNormalized       | This path is not normal because of the parent segment but can be fully normalized                  |
| ../foo/bar                   | LeadingParentsOnly  | This path is as normalized as it can be ... leading parent segements can't be removed              |
| C:/dir/file.txt              | Fully               | This path is fully normalized                                                                      |
| C:/dir/file.txt/             | NotNormalized       | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:/dir                       | Fully               | This path is fully normalized                                                                      |
| C:/dir/                      | NotNormalized       | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:./file.txt                 | NotNormalized       | This is a Windows path so the '.' is a self reference ... thus resulting in NotNormalized          |
| C:./file.txt/                | NotNormalized       | The self reference and the trailing slash prevent this from being normalized                       |
| C:file.txt                   | Fully               | While not relative this path is still normalized                                                   |
| C:file.txt/                  | NotNormalized       | Trailing Slash prevents normalization                                                              |
| C:dir                        | Fully               | Fully normalized                                                                                   |
| C:dir/                       | NotNormalized       | Trailing slash prevents normalziaiton                                                              |
| C:dir/file.txt               | Fully               | Fully normalized                                                                                   |
| C:dir/file.txt/              | NotNormalized       | Trailing Slash                                                                                     |
| //server/share               | Fully               | Fully normalized remote path                                                                       |
| //server/share/              | NotNormalized       | Trailing Slash                                                                                     |
| //server/file.txt            | Fully               | Fully normalized remote path                                                                       |
| //server/file.txt/           | NotNormalized       | Trailing Slash                                                                                     |
| //server/share/dir/file.txt  | Fully               | Fully normalized remote path                                                                       |
| //server/share/dir/file.txt/ | NotNormalized       | Trailing Slash                                                                                     |
| .                            | SelfReferenceOnly   | This is normalized as best as posilbe, its a special case where the path only has a self reference |
| ./                           | NotNormalized       | Self Reference ant Trailing Slash                                                                  |
| ./file.txt                   | NotNormalized       | The self reference could be removed in this case                                                   |
| ./file.txt/                  | NotNormalized       | The Self reference and the trailing path                                                           |
| ./dir                        | NotNormalized       | The self reference                                                                                 |
| ./dir/                       | NotNormalized       | The self reference and the trailing slash                                                          |
| ./dir/file.txt               | NotNormalized       | The self reference                                                                                 |
| ./dir/file.txt/              | NotNormalized       | The self refrence and the trialing slash                                                           |
| ..                           | LeadingParentsOnly  | Only has a leading parent ... best posible normalization                                           |
| ../                          | NotNormalized       | Trailing slash                                                                                     |
| ../dir/file.txt              | LeadingParentsOnly  | Only has leading parents ... best posible normalization                                            |
| ../dir/file.txt/             | NotNormalized       | Trailing slash                                                                                     |
| a/b/./c/../../d/../../../e   | NotNormalized       | To many resons to mention                                                                          |
| a/b/./c/../d/../../e         | NotNormalized	     | To many resons to mention                                                                          |
