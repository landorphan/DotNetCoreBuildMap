@Check-In
Feature: Posix Illegal Paths
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want illegal paths to be identified.

Scenario Outline: I can identify illegal paths
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Linux
	 When I parse the path 
	 Then the parse status should be <Path Status>
Examples:
| Path                           | Path Status | Notes                                            |
| [PSN:POS]/{R}                  | Legal       | Legal                                            |
| [PSN:POS]/{G} dir              | Legal       | Legal                                            |
| [PSN:POS]/{R}/{E}/{G} dir      | Legal       | Legal                                            |
| [PSN:POS]/{R}/{0}/{G} dir      | Legal       | Legal                                            |
| [PSN:POS]/{R}/{S} ./{G} dir    | Legal       | Legal                                            |
| [PSN:POS]/{R}/{P} ../{G} dir   | Legal       | Legal                                            |
| [PSN:POS]/{X} server/{G} share | Legal       | Legal                                            |
| [PSN:POS]/{V} C                | Illegal     | Vol Rel on Posix are Illegal                     |
| [PSN:POS]/{$}/{G} dir          | Illegal     | Vol Abs on Posix are Illegal                     |
| [PSN:POS]/{D} CON              | Illegal     | Device on Posix are Illegal                      |
| [PSN:POS]/{S}/{G} dir          | Legal       | === START NOTE FOR SECTION ===                   |
| [PSN:POS]/{S} ./{G} dir        | Legal       | Path status is always compared against the       |
| [PSN:POS]/{S} ..               | Legal       | simplpified form.  When a path is simplified     |
| [PSN:POS]/{S} dir              | Legal       | Only the '.' and '..' that can not be simplified |
| [PSN:POS]/{P}/{G} dir          | Legal       | are kept.  They are further replaced with the    |
| [PSN:POS]/{P} ../{G} dir       | Legal       | singleton instance of of that segment ...        |
| [PSN:POS]/{P} ./{G} dir        | Legal       | Therefore they are always legal when evaluted    |
| [PSN:POS]/{P} dir/{G} dir      | Legal       | === END   NOTE FOR SECTION ===                   |