﻿Feature: Change Extension feature
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want to change the extension on a ptah.

Scenario Outline: Change Extension Safe Execute (no Exceptions)
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Windows
	 When I parse the path 
	  And I change the path's extension to: <New Extension>
     Then the resulting status should be <New Path Status>
	  And the resulting path's IsDiscouraged property should be <Is Discouraged>
	  And the result should be: <Result>
  	  And the resulting path's Extension should be: <Resulting Extension>
Examples:
| Path            | New Extension | Result                | New Path Status | Is Discouraged | Resulting Extension | Notes                                                   |
| /foo.txt        | .json         | `foo.json             | Legal           | false          | .json               | The leading period in the extension is ignored          |
| /foo.txt        | json          | `foo.json             | Legal           | false          | .json               | simple change                                           |
| /foo.txt        | (null)        | `foo                  | Legal           | false          | (empty)             | Null becomes empty string                               |
| /foo.txt        | (empty)       | `foo                  | Legal           | false          | (empty)             | Empty string means no extension                         |
| /foo.txt        | .             | `foo                  | Legal           | false          | (empty)             | The leading period in the extension is ignored          |
| /foo.txt        | json.txt      | `foo.json.txt         | Legal           | false          | .txt                | only the last part of the new extension is an extension |
| /foo.txt        | .json.txt.    | `foo.json.txt.        | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.txt        | ..            | `foo..                | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.txt        | .json.        | `foo.json.            | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.txt        | json.         | `foo.json.            | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.txt        | json%20       | `foo.json%20          | Illegal         | false          | .json%20            | Trailing ' ' are illegal on Windows                     |
| /foo.bar.txt    | .json         | `foo.bar.json         | Legal           | false          | .json               | The leading period in the extension is ignored          |
| /foo.bar.txt    | json          | `foo.bar.json         | Legal           | false          | .json               | simple change                                           |
| /foo.bar.txt    | (null)        | `foo.bar              | Legal           | false          | .bar                | Null becomes empty string                               |
| /foo.bar.txt    | (empty)       | `foo.bar              | Legal           | false          | .bar                | Empty string means no extension                         |
| /foo.bar.txt    | .             | `foo.bar              | Legal           | false          | .bar                | The leading period in the extension is ignored          |
| /foo.bar.txt    | json.txt      | `foo.bar.json.txt     | Legal           | false          | .txt                | only the last part of the new extension is an extension |
| /foo.bar.txt    | .json.txt.    | `foo.bar.json.txt.    | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.bar.txt    | ..            | `foo.bar..            | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.bar.txt    | .json.        | `foo.bar.json.        | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.bar.txt    | json.         | `foo.bar.json.        | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /foo.bar.txt    | json%20       | `foo.bar.json%20      | Illegal         | false          | .json%20            | Trailing ' ' are illegal on Windows                     |
| /.gitignore     | json          | `.gitignore.json      | Legal           | false          | .json               | The leading period in the extension is ignored          |
| /.gitignore     | .json         | `.gitignore.json      | Legal           | false          | .json               | simple change                                           |
| /.gitignore     | (null)        | `.gitignore           | Legal           | false          | (empty)             | Null becomes empty string                               |
| /.gitignore     | (empty)       | `.gitignore           | Legal           | false          | (empty)             | Empty string means no extension                         |
| /.gitignore     | .             | `.gitignore           | Legal           | false          | (empty)             | The leading period in the extension is ignored          |
| /.gitignore     | json.txt      | `.gitignore.json.txt  | Legal           | false          | .txt                | only the last part of the new extension is an extension |
| /.gitignore     | .json.txt.    | `.gitignore.json.txt. | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore     | ..            | `.gitignore..         | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore     | .json.        | `.gitignore.json.     | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore     | json.         | `.gitignore.json.     | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore     | json%20       | `.gitignore.json%20   | Illegal         | false          | .json%20            | Trailing ' ' are illegal on Windows                     |
| /.gitignore.txt | json          | `.gitignore.json      | Legal           | false          | .json               | The leading period in the extension is ignored          |
| /.gitignore.txt | .json         | `.gitignore.json      | Legal           | false          | .json               | simple change                                           |
| /.gitignore.txt | (null)        | `.gitignore           | Legal           | false          | (empty)             | Null becomes empty string                               |
| /.gitignore.txt | (empty)       | `.gitignore           | Legal           | false          | (empty)             | Empty string means no extension                         |
| /.gitignore.txt | .             | `.gitignore           | Legal           | false          | (empty)             | The leading period in the extension is ignored          |
| /.gitignore.txt | json.txt      | `.gitignore.json.txt  | Legal           | false          | .txt                | only the last part of the new extension is an extension |
| /.gitignore.txt | .json.txt.    | `.gitignore.json.txt. | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore.txt | ..            | `.gitignore..         | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore.txt | .json.        | `.gitignore.json.     | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore.txt | json.         | `.gitignore.json.     | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /.gitignore.txt | json%20       | `.gitignore.json%20   | Illegal         | false          | .json%20            | Trailing ' ' are illegal on Windows                     |
| /file           | json          | `file.json            | Legal           | false          | .json               | The leading period in the extension is ignored          |
| /file           | .json         | `file.json            | Legal           | false          | .json               | simple change                                           |
| /file           | (null)        | `file                 | Legal           | false          | (empty)             | Null becomes empty string                               |
| /file           | (empty)       | `file                 | Legal           | false          | (empty)             | Empty string means no extension                         |
| /file           | .             | `file                 | Legal           | false          | (empty)             | The leading period in the extension is ignored          |
| /file           | json.txt      | `file.json.txt        | Legal           | false          | .txt                | only the last part of the new extension is an extension |
| /file           | .json.txt.    | `file.json.txt.       | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /file           | ..            | `file..               | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /file           | .json.        | `file.json.           | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /file           | json.         | `file.json.           | Illegal         | false          | (empty)             | Trailing '.' are illegal on Windows                     |
| /file           | json%20       | `file.json%20         | Illegal         | false          | .json%20            | Trailing ' ' are illegal on Windows                     |