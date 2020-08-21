@Check-In
Feature: Posix Path Simplification Level, Anchor And Qualifed Status
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to convert incoming paths into a more managable form

Scenario Outline: Posix Paths can be normalized to best available form.
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Linux
     When I parse the path 
	 Then the resulting path should have the following Simplification Level: <Simplification Level>
Examples:
# NOTE: Due to Gherkin parsing rules, \ needs to be escaped.  In order to avoid that necissity and
# make the following examples easier to read (/) will be used in place of the (\) character
| Path                         | Anchor   | Fully Qualified | Simplification Level | Notes                                                                                              |
| (null)                       | Relative | false           | SelfReferenceOnly   | Null and Empty paths are equivilent to '.'                                                         |
| (empty)                      | Relative | false           | SelfReferenceOnly   | Null and Empty paths are equivilent to '.'                                                         |
| C:/                          | Relative | false           | NotNormalized       | The trailing slash adds an empty segment thus it's not normalized                                  |
| /                            | Absolute | true            | Fully               | A root only segment should be created and this path is normalized                                  |
| /foo/bar                     | Absolute | true            | Fully               | This path only contains root and generic segments                                                  |
| foo/bar                      | Relative | false           | Fully               | This path is relative but fully normalized                                                         |
| foo/../bar                   | Relative | false           | NotNormalized       | This path is not normal because of the parent segment but can be fully normalized                  |
| ../foo/bar                   | Relative | false           | LeadingParentsOnly  | This path is as normalized as it can be ... leading parent segements can't be removed              |
| C:/dir/file.txt              | Relative | false           | Fully               | This path is fully normalized                                                                      |
| C:/dir/file.txt/             | Relative | false           | NotNormalized       | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:/dir                       | Relative | false           | Fully               | This path is fully normalized                                                                      |
| C:/dir/                      | Relative | false           | NotNormalized       | The trailing slash adds an empty segement thus it's not normalized                                 |
| C:./file.txt                 | Relative | false           | Fully               | This is a Posix path so the '.' is not a self reference but part of the name ... thus Fully        |
| C:./file.txt/                | Relative | false           | NotNormalized       | The self reference and the trailing slash prevent this from being normalized                       |
| C:file.txt                   | Relative | false           | Fully               | While not relative this path is still normalized                                                   |
| C:file.txt/                  | Relative | false           | NotNormalized       | Trailing Slash prevents normalization                                                              |
| C:dir                        | Relative | false           | Fully               | Fully normalized                                                                                   |
| C:dir/                       | Relative | false           | NotNormalized       | Trailing slash prevents normalziaiton                                                              |
| C:dir/file.txt               | Relative | false           | Fully               | Fully normalized                                                                                   |
| C:dir/file.txt/              | Relative | false           | NotNormalized       | Trailing Slash                                                                                     |
| //server/share               | Absolute | true            | Fully               | Fully normalized remote path                                                                       |
| //server/share/              | Absolute | true            | NotNormalized       | Trailing Slash                                                                                     |
| //server/file.txt            | Absolute | true            | Fully               | Fully normalized remote path                                                                       |
| //server/file.txt/           | Absolute | true            | NotNormalized       | Trailing Slash                                                                                     |
| //server/share/dir/file.txt  | Absolute | true            | Fully               | Fully normalized remote path                                                                       |
| //server/share/dir/file.txt/ | Absolute | true            | NotNormalized       | Trailing Slash                                                                                     |
| .                            | Relative | false           | SelfReferenceOnly   | This is normalized as best as posilbe, its a special case where the path only has a self reference |
| ./                           | Relative | false           | NotNormalized       | Self Reference ant Trailing Slash                                                                  |
| ./file.txt                   | Relative | false           | NotNormalized       | The self reference could be removed in this case                                                   |
| ./file.txt/                  | Relative | false           | NotNormalized       | The Self reference and the trailing path                                                           |
| ./dir                        | Relative | false           | NotNormalized       | The self reference                                                                                 |
| ./dir/                       | Relative | false           | NotNormalized       | The self reference and the trailing slash                                                          |
| ./dir/file.txt               | Relative | false           | NotNormalized       | The self reference                                                                                 |
| ./dir/file.txt/              | Relative | false           | NotNormalized       | The self refrence and the trialing slash                                                           |
| ..                           | Relative | false           | LeadingParentsOnly  | Only has a leading parent ... best posible normalization                                           |
| ../                          | Relative | false           | NotNormalized       | Trailing slash                                                                                     |
| ../dir/file.txt              | Relative | false           | LeadingParentsOnly  | Only has leading parents ... best posible normalization                                            |
| ../dir/file.txt/             | Relative | false           | NotNormalized       | Trailing slash                                                                                     |
| a/b/./c/../../d/../../../e   | Relative | false           | NotNormalized       | To many resons to mention                                                                          |
| a/b/./c/../d/../../e         | Relative | false           | NotNormalized       | To many resons to mention                                                                          |