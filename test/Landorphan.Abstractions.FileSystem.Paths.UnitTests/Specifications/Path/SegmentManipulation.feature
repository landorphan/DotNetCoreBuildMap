@Check-In
Feature: Segment Manipulation
	In order to develop applications that access the file system.
	As a Landorphan OSS consumer
	I want to to be able to manipulate the segments in a path 

Scenario: I can insert and append segments to any point in a path
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: <OS>
	  And I parse the path 
	 When I manipulate the path by adding the segment (<Segment>) <Action> offset <Offset>
	  And I ask for the path to be represented as a string
	 Then I should receive the following string: <Result>
Examples:
| Path     | OS      | Segment | Offset | Action        | Result       | Notes                                                                                       |
| a`b`c    | Windows | {G} 0   | 0      | insert before | 0`a`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {G} 0   | 0      | append after  | a`0`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {G} 0   | 2      | insert before | a`b`0`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {G} 0   | 2      | append after  | a`b`c`0      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {G} 0   | 9      | insert before | a`b`c`0      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {G} 0   | 9      | append after  | a`b`c`0      | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {G} 0   | 0      | insert before | 0`C:`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {G} 0   | 0      | append after  | C:`0`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {G} 0   | 3      | insert before | C:`a`b`0`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {G} 0   | 3      | append after  | C:`a`b`c`0   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {G} 0   | 9      | insert before | C:`a`b`c`0   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {G} 0   | 9      | append after  | C:`a`b`c`0   | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {G} 0   | 0      | insert before | 0`C:a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {G} 0   | 0      | append after  | C:0`a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {G} 0   | 3      | insert before | C:a`b`0`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {G} 0   | 3      | append after  | C:a`b`c`0    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {G} 0   | 9      | insert before | C:a`b`c`0    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {G} 0   | 9      | append after  | C:a`b`c`0    | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {G} 0   | 0      | insert before | 0``a`b`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 0      | append after  | `0`a`b`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 3      | insert before | `a`b`0`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 3      | append after  | `a`b`c`0     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 9      | insert before | `a`b`c`0     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 9      | append after  | `a`b`c`0     | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 0      | insert before | 0```a`b`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 0      | append after  | ``a`0`b`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 2      | insert before | ``a`b`0`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 2      | append after  | ``a`b`c`0    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 9      | insert before | ``a`b`c`0    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 9      | append after  | ``a`b`c`0    | Leading '{X} a'                                                                             |
| (empty)  | Windows | {G} 0   | 0      | insert before | 0`.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {G} 0   | 0      | append after  | .`0          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {G} 0   | 9      | insert before | .`0          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {G} 0   | 9      | append after  | .`0          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {G} 0   | 0      | insert before | 0`.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {G} 0   | 0      | append after  | .`0          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {G} 0   | 9      | insert before | .`0          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {G} 0   | 9      | append after  | .`0          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {G} 0   | 0      | insert before | 0`.          | Leading '.'                                                                                 |
| .        | Windows | {G} 0   | 0      | append after  | .`0          | Leading '.'                                                                                 |
| .        | Windows | {G} 0   | 9      | insert before | .`0          | Leading '.'                                                                                 |
| .        | Windows | {G} 0   | 9      | append after  | .`0          | Leading '.'                                                                                 |
| ..       | Windows | {G} 0   | 0      | insert before | 0`..         | Leading '..'                                                                                |
| ..       | Windows | {G} 0   | 0      | append after  | ..`0         | Leading '..'                                                                                |
| ..       | Windows | {G} 0   | 9      | insert before | ..`0         | Leading '..'                                                                                |
| ..       | Windows | {G} 0   | 9      | append after  | ..`0         | Leading '..'                                                                                |
| a`b`c    | Windows | {S} .   | 0      | insert before | .`a`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {S} .   | 0      | append after  | a`.`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {S} .   | 2      | insert before | a`b`.`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {S} .   | 2      | append after  | a`b`c`.      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {S} .   | 9      | insert before | a`b`c`.      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {S} .   | 9      | append after  | a`b`c`.      | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {S} .   | 0      | insert before | .`C:`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {S} .   | 0      | append after  | C:`.`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {S} .   | 3      | insert before | C:`a`b`.`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {S} .   | 3      | append after  | C:`a`b`c`.   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {S} .   | 9      | insert before | C:`a`b`c`.   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {S} .   | 9      | append after  | C:`a`b`c`.   | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {S} .   | 0      | insert before | .`C:a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {S} .   | 0      | append after  | C:.`a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {S} .   | 3      | insert before | C:a`b`.`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {S} .   | 3      | append after  | C:a`b`c`.    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {S} .   | 9      | insert before | C:a`b`c`.    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {S} .   | 9      | append after  | C:a`b`c`.    | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {S} .   | 0      | insert before | .``a`b`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {S} .   | 0      | append after  | `.`a`b`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {S} .   | 3      | insert before | `a`b`.`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {S} .   | 3      | append after  | `a`b`c`.     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {S} .   | 9      | insert before | `a`b`c`.     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {S} .   | 9      | append after  | `a`b`c`.     | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {S} .   | 0      | insert before | .```a`b`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {S} .   | 0      | append after  | ``a`.`b`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {S} .   | 2      | insert before | ``a`b`.`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {S} .   | 2      | append after  | ``a`b`c`.    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {S} .   | 9      | insert before | ``a`b`c`.    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {S} .   | 9      | append after  | ``a`b`c`.    | Leading '{X} a'                                                                             |
| (empty)  | Windows | {S} .   | 0      | insert before | .`.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {S} .   | 0      | append after  | .`.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {S} .   | 9      | insert before | .`.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {S} .   | 9      | append after  | .`.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {S} .   | 0      | insert before | .`.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {S} .   | 0      | append after  | .`.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {S} .   | 9      | insert before | .`.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {S} .   | 9      | append after  | .`.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {S} .   | 0      | insert before | .`.          | Leading '.'                                                                                 |
| .        | Windows | {S} .   | 0      | append after  | .`.          | Leading '.'                                                                                 |
| .        | Windows | {S} .   | 9      | insert before | .`.          | Leading '.'                                                                                 |
| .        | Windows | {S} .   | 9      | append after  | .`.          | Leading '.'                                                                                 |
| ..       | Windows | {S} .   | 0      | insert before | .`..         | Leading '..'                                                                                |
| ..       | Windows | {S} .   | 0      | append after  | ..`.         | Leading '..'                                                                                |
| ..       | Windows | {S} .   | 9      | insert before | ..`.         | Leading '..'                                                                                |
| ..       | Windows | {S} .   | 9      | append after  | ..`.         | Leading '..'                                                                                |
| a`b`c    | Windows | {P} ..  | 0      | insert before | ..`a`b`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {P} ..  | 0      | append after  | a`..`b`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {P} ..  | 2      | insert before | a`b`..`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {P} ..  | 2      | append after  | a`b`c`..     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {P} ..  | 9      | insert before | a`b`c`..     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {P} ..  | 9      | append after  | a`b`c`..     | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {P} ..  | 0      | insert before | ..`C:`a`b`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {P} ..  | 0      | append after  | C:`..`a`b`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {P} ..  | 3      | insert before | C:`a`b`..`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {P} ..  | 3      | append after  | C:`a`b`c`..  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {P} ..  | 9      | insert before | C:`a`b`c`..  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {P} ..  | 9      | append after  | C:`a`b`c`..  | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {P} ..  | 0      | insert before | ..`C:a`b`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {P} ..  | 0      | append after  | C:..`a`b`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {P} ..  | 3      | insert before | C:a`b`..`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {P} ..  | 3      | append after  | C:a`b`c`..   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {P} ..  | 9      | insert before | C:a`b`c`..   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {P} ..  | 9      | append after  | C:a`b`c`..   | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {P} ..  | 0      | insert before | ..``a`b`c    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {P} ..  | 0      | append after  | `..`a`b`c    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {P} ..  | 3      | insert before | `a`b`..`c    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {P} ..  | 3      | append after  | `a`b`c`..    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {P} ..  | 9      | insert before | `a`b`c`..    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {P} ..  | 9      | append after  | `a`b`c`..    | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {P} ..  | 0      | insert before | ..```a`b`c   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {P} ..  | 0      | append after  | ``a`..`b`c   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {P} ..  | 2      | insert before | ``a`b`..`c   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {P} ..  | 2      | append after  | ``a`b`c`..   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {P} ..  | 9      | insert before | ``a`b`c`..   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {P} ..  | 9      | append after  | ``a`b`c`..   | Leading '{X} a'                                                                             |
| (empty)  | Windows | {P} ..  | 0      | insert before | ..`.         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {P} ..  | 0      | append after  | .`..         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {P} ..  | 9      | insert before | .`..         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {P} ..  | 9      | append after  | .`..         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {P} ..  | 0      | insert before | ..`.         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {P} ..  | 0      | append after  | .`..         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {P} ..  | 9      | insert before | .`..         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {P} ..  | 9      | append after  | .`..         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {P} ..  | 0      | insert before | ..`.         | Leading '.'                                                                                 |
| .        | Windows | {P} ..  | 0      | append after  | .`..         | Leading '.'                                                                                 |
| .        | Windows | {P} ..  | 9      | insert before | .`..         | Leading '.'                                                                                 |
| .        | Windows | {P} ..  | 9      | append after  | .`..         | Leading '.'                                                                                 |
| ..       | Windows | {P} ..  | 0      | insert before | ..`..        | Leading '..'                                                                                |
| ..       | Windows | {P} ..  | 0      | append after  | ..`..        | Leading '..'                                                                                |
| ..       | Windows | {P} ..  | 9      | insert before | ..`..        | Leading '..'                                                                                |
| ..       | Windows | {P} ..  | 9      | append after  | ..`..        | Leading '..'                                                                                |
| a`b`c    | Windows | {E}     | 0      | insert before | `a`b`c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {E}     | 0      | append after  | a``b`c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {E}     | 2      | insert before | a`b``c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {E}     | 2      | append after  | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {E}     | 9      | insert before | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {E}     | 9      | append after  | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {E}     | 0      | insert before | `C:`a`b`c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {E}     | 0      | append after  | C:``a`b`c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {E}     | 3      | insert before | C:`a`b``c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {E}     | 3      | append after  | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {E}     | 9      | insert before | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {E}     | 9      | append after  | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {E}     | 0      | insert before | `C:a`b`c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {E}     | 0      | append after  | C:`a`b`c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {E}     | 3      | insert before | C:a`b``c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {E}     | 3      | append after  | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {E}     | 9      | insert before | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {E}     | 9      | append after  | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 0      | insert before | ``a`b`c      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 0      | append after  | ``a`b`c      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 3      | insert before | `a`b``c      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 3      | append after  | `a`b`c`      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 9      | insert before | `a`b`c`      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 9      | append after  | `a`b`c`      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 0      | insert before | ```a`b`c     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 0      | append after  | ``a``b`c     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 2      | insert before | ``a`b``c     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 2      | append after  | ``a`b`c`     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 9      | insert before | ``a`b`c`     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 9      | append after  | ``a`b`c`     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {E}     | 0      | insert before | `.           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {E}     | 0      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {E}     | 9      | insert before | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {E}     | 9      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {E}     | 0      | insert before | `.           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {E}     | 0      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {E}     | 9      | insert before | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {E}     | 9      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {E}     | 0      | insert before | `.           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {E}     | 0      | append after  | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {E}     | 9      | insert before | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {E}     | 9      | append after  | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {E}     | 0      | insert before | `..          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {E}     | 0      | append after  | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {E}     | 9      | insert before | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {E}     | 9      | append after  | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {0}     | 0      | insert before | `a`b`c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {0}     | 0      | append after  | a``b`c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {0}     | 2      | insert before | a`b``c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {0}     | 2      | append after  | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {0}     | 9      | insert before | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {0}     | 9      | append after  | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {0}     | 0      | insert before | `C:`a`b`c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {0}     | 0      | append after  | C:``a`b`c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {0}     | 3      | insert before | C:`a`b``c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {0}     | 3      | append after  | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {0}     | 9      | insert before | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {0}     | 9      | append after  | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {0}     | 0      | insert before | `C:a`b`c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {0}     | 0      | append after  | C:`a`b`c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {0}     | 3      | insert before | C:a`b``c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {0}     | 3      | append after  | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {0}     | 9      | insert before | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {0}     | 9      | append after  | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {0}     | 0      | insert before | ``a`b`c      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {0}     | 0      | append after  | ``a`b`c      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {0}     | 3      | insert before | `a`b``c      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {0}     | 3      | append after  | `a`b`c`      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {0}     | 9      | insert before | `a`b`c`      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {0}     | 9      | append after  | `a`b`c`      | Leading '{$} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {0}     | 0      | insert before | ```a`b`c     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {0}     | 0      | append after  | ``a``b`c     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {0}     | 2      | insert before | ``a`b``c     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {0}     | 2      | append after  | ``a`b`c`     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {0}     | 9      | insert before | ``a`b`c`     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {0}     | 9      | append after  | ``a`b`c`     | Leading '{X} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {0}     | 0      | insert before | `.           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {0}     | 0      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {0}     | 9      | insert before | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {0}     | 9      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {0}     | 0      | insert before | `.           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {0}     | 0      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {0}     | 9      | insert before | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {0}     | 9      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {0}     | 0      | insert before | `.           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {0}     | 0      | append after  | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {0}     | 9      | insert before | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {0}     | 9      | append after  | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {0}     | 0      | insert before | `..          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {0}     | 0      | append after  | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {0}     | 9      | insert before | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {0}     | 9      | append after  | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {V} C   | 0      | insert before | C:a`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {V} C   | 0      | append after  | a`C:b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {V} C   | 2      | insert before | a`b`C:c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {V} C   | 2      | append after  | a`b`c`C:     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {V} C   | 9      | insert before | a`b`c`C:     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {V} C   | 9      | append after  | a`b`c`C:     | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {V} C   | 0      | insert before | C:C:`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {V} C   | 0      | append after  | C:`C:a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {V} C   | 3      | insert before | C:`a`b`C:c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {V} C   | 3      | append after  | C:`a`b`c`C:  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {V} C   | 9      | insert before | C:`a`b`c`C:  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {V} C   | 9      | append after  | C:`a`b`c`C:  | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {V} C   | 0      | insert before | C:C:a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {V} C   | 0      | append after  | C:C:a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {V} C   | 3      | insert before | C:a`b`C:c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {V} C   | 3      | append after  | C:a`b`c`C:   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {V} C   | 9      | insert before | C:a`b`c`C:   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {V} C   | 9      | append after  | C:a`b`c`C:   | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {V} C   | 0      | insert before | C:`a`b`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 0      | append after  | `C:a`b`c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 3      | insert before | `a`b`C:c     | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 3      | append after  | `a`b`c`C:    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 9      | insert before | `a`b`c`C:    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 9      | append after  | `a`b`c`C:    | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 0      | insert before | C:``a`b`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 0      | append after  | ``a`C:b`c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 2      | insert before | ``a`b`C:c    | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 2      | append after  | ``a`b`c`C:   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 9      | insert before | ``a`b`c`C:   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 9      | append after  | ``a`b`c`C:   | Leading '{X} a'                                                                             |
| (empty)  | Windows | {V} C   | 0      | insert before | C:.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {V} C   | 0      | append after  | .`C:         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {V} C   | 9      | insert before | .`C:         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {V} C   | 9      | append after  | .`C:         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {V} C   | 0      | insert before | C:.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {V} C   | 0      | append after  | .`C:         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {V} C   | 9      | insert before | .`C:         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {V} C   | 9      | append after  | .`C:         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {V} C   | 0      | insert before | C:.          | Leading '.'                                                                                 |
| .        | Windows | {V} C   | 0      | append after  | .`C:         | Leading '.'                                                                                 |
| .        | Windows | {V} C   | 9      | insert before | .`C:         | Leading '.'                                                                                 |
| .        | Windows | {V} C   | 9      | append after  | .`C:         | Leading '.'                                                                                 |
| ..       | Windows | {V} C   | 0      | insert before | C:..         | Leading '..'                                                                                |
| ..       | Windows | {V} C   | 0      | append after  | ..`C:        | Leading '..'                                                                                |
| ..       | Windows | {V} C   | 9      | insert before | ..`C:        | Leading '..'                                                                                |
| ..       | Windows | {V} C   | 9      | append after  | ..`C:        | Leading '..'                                                                                |
| a`b`c    | Windows | {R} C   | 0      | insert before | C:`a`b`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {R} C   | 0      | append after  | a`C:`b`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {R} C   | 2      | insert before | a`b`C:`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {R} C   | 2      | append after  | a`b`c`C:`    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {R} C   | 9      | insert before | a`b`c`C:`    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {R} C   | 9      | append after  | a`b`c`C:`    | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {R} C   | 0      | insert before | C:`C:`a`b`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {R} C   | 0      | append after  | C:`C:`a`b`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {R} C   | 3      | insert before | C:`a`b`C:`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {R} C   | 3      | append after  | C:`a`b`c`C:` | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {R} C   | 9      | insert before | C:`a`b`c`C:` | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {R} C   | 9      | append after  | C:`a`b`c`C:` | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {R} C   | 0      | insert before | C:`C:a`b`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {R} C   | 0      | append after  | C:C:`a`b`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {R} C   | 3      | insert before | C:a`b`C:`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {R} C   | 3      | append after  | C:a`b`c`C:`  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {R} C   | 9      | insert before | C:a`b`c`C:`  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {R} C   | 9      | append after  | C:a`b`c`C:`  | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {R} C   | 0      | insert before | C:``a`b`c    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 0      | append after  | `C:`a`b`c    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 3      | insert before | `a`b`C:`c    | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 3      | append after  | `a`b`c`C:`   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 9      | insert before | `a`b`c`C:`   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 9      | append after  | `a`b`c`C:`   | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 0      | insert before | C:```a`b`c   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 0      | append after  | ``a`C:`b`c   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 2      | insert before | ``a`b`C:`c   | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 2      | append after  | ``a`b`c`C:`  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 9      | insert before | ``a`b`c`C:`  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 9      | append after  | ``a`b`c`C:`  | Leading '{X} a'                                                                             |
| (empty)  | Windows | {R} C   | 0      | insert before | C:`.         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {R} C   | 0      | append after  | .`C:`        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {R} C   | 9      | insert before | .`C:`        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {R} C   | 9      | append after  | .`C:`        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {R} C   | 0      | insert before | C:`.         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {R} C   | 0      | append after  | .`C:`        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {R} C   | 9      | insert before | .`C:`        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {R} C   | 9      | append after  | .`C:`        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {R} C   | 0      | insert before | C:`.         | Leading '.'                                                                                 |
| .        | Windows | {R} C   | 0      | append after  | .`C:`        | Leading '.'                                                                                 |
| .        | Windows | {R} C   | 9      | insert before | .`C:`        | Leading '.'                                                                                 |
| .        | Windows | {R} C   | 9      | append after  | .`C:`        | Leading '.'                                                                                 |
| ..       | Windows | {R} C   | 0      | insert before | C:`..        | Leading '..'                                                                                |
| ..       | Windows | {R} C   | 0      | append after  | ..`C:`       | Leading '..'                                                                                |
| ..       | Windows | {R} C   | 9      | insert before | ..`C:`       | Leading '..'                                                                                |
| ..       | Windows | {R} C   | 9      | append after  | ..`C:`       | Leading '..'                                                                                |
| a`b`c    | Windows | {$}     | 0      | insert before | `a`b`c       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {$}     | 0      | append after  | a``b`c       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {$}     | 2      | insert before | a`b``c       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {$}     | 2      | append after  | a`b`c`       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {$}     | 9      | insert before | a`b`c`       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {$}     | 9      | append after  | a`b`c`       | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {$}     | 0      | insert before | `C:`a`b`c    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {$}     | 0      | append after  | C:``a`b`c    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {$}     | 3      | insert before | C:`a`b``c    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {$}     | 3      | append after  | C:`a`b`c`    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {$}     | 9      | insert before | C:`a`b`c`    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {$}     | 9      | append after  | C:`a`b`c`    | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {$}     | 0      | insert before | `C:a`b`c     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {$}     | 0      | append after  | C:`a`b`c     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {$}     | 3      | insert before | C:a`b``c     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {$}     | 3      | append after  | C:a`b`c`     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {$}     | 9      | insert before | C:a`b`c`     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {$}     | 9      | append after  | C:a`b`c`     | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {$}     | 0      | insert before | ``a`b`c      | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {$}     | 0      | append after  | ``a`b`c      | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {$}     | 3      | insert before | `a`b``c      | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {$}     | 3      | append after  | `a`b`c`      | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {$}     | 9      | insert before | `a`b`c`      | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {$}     | 9      | append after  | `a`b`c`      | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {$}     | 0      | insert before | ```a`b`c     | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {$}     | 0      | append after  | ``a``b`c     | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {$}     | 2      | insert before | ``a`b``c     | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {$}     | 2      | append after  | ``a`b`c`     | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {$}     | 9      | insert before | ``a`b`c`     | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {$}     | 9      | append after  | ``a`b`c`     | Leading '{X} a'                                                                             |
| (empty)  | Windows | {$}     | 0      | insert before | `.           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {$}     | 0      | append after  | .`           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {$}     | 9      | insert before | .`           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {$}     | 9      | append after  | .`           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {$}     | 0      | insert before | `.           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {$}     | 0      | append after  | .`           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {$}     | 9      | insert before | .`           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {$}     | 9      | append after  | .`           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {$}     | 0      | insert before | `.           | Leading '.'                                                                                 |
| .        | Windows | {$}     | 0      | append after  | .`           | Leading '.'                                                                                 |
| .        | Windows | {$}     | 9      | insert before | .`           | Leading '.'                                                                                 |
| .        | Windows | {$}     | 9      | append after  | .`           | Leading '.'                                                                                 |
| ..       | Windows | {$}     | 0      | insert before | `..          | Leading '..'                                                                                |
| ..       | Windows | {$}     | 0      | append after  | ..`          | Leading '..'                                                                                |
| ..       | Windows | {$}     | 9      | insert before | ..`          | Leading '..'                                                                                |
| ..       | Windows | {$}     | 9      | append after  | ..`          | Leading '..'                                                                                |
| a`b`c    | Windows | {X} 0   | 0      | insert before | ``0`a`b`c    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {X} 0   | 0      | append after  | a```0`b`c    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {X} 0   | 2      | insert before | a`b```0`c    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {X} 0   | 2      | append after  | a`b`c```0    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {X} 0   | 9      | insert before | a`b`c```0    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {X} 0   | 9      | append after  | a`b`c```0    | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {X} 0   | 0      | insert before | ``0`C:`a`b`c | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {X} 0   | 0      | append after  | C:```0`a`b`c | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {X} 0   | 3      | insert before | C:`a`b```0`c | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {X} 0   | 3      | append after  | C:`a`b`c```0 | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {X} 0   | 9      | insert before | C:`a`b`c```0 | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {X} 0   | 9      | append after  | C:`a`b`c```0 | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {X} 0   | 0      | insert before | ``0`C:a`b`c  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {X} 0   | 0      | append after  | C:``0`a`b`c  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {X} 0   | 3      | insert before | C:a`b```0`c  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {X} 0   | 3      | append after  | C:a`b`c```0  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {X} 0   | 9      | insert before | C:a`b`c```0  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {X} 0   | 9      | append after  | C:a`b`c```0  | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {X} 0   | 0      | insert before | ``0``a`b`c   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {X} 0   | 0      | append after  | ```0`a`b`c   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {X} 0   | 3      | insert before | `a`b```0`c   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {X} 0   | 3      | append after  | `a`b`c```0   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {X} 0   | 9      | insert before | `a`b`c```0   | Leading '{$} a'                                                                             |
| `a`b`c   | Windows | {X} 0   | 9      | append after  | `a`b`c```0   | Leading '{$} a'                                                                             |
| ``a`b`c  | Windows | {X} 0   | 0      | insert before | ``0```a`b`c  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {X} 0   | 0      | append after  | ``a```0`b`c  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {X} 0   | 2      | insert before | ``a`b```0`c  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {X} 0   | 2      | append after  | ``a`b`c```0  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {X} 0   | 9      | insert before | ``a`b`c```0  | Leading '{X} a'                                                                             |
| ``a`b`c  | Windows | {X} 0   | 9      | append after  | ``a`b`c```0  | Leading '{X} a'                                                                             |
| (empty)  | Windows | {X} 0   | 0      | insert before | ``0`.        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {X} 0   | 0      | append after  | .```0        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {X} 0   | 9      | insert before | .```0        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Windows | {X} 0   | 9      | append after  | .```0        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Windows | {X} 0   | 0      | insert before | ``0`.        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {X} 0   | 0      | append after  | .```0        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {X} 0   | 9      | insert before | .```0        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Windows | {X} 0   | 9      | append after  | .```0        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Windows | {X} 0   | 0      | insert before | ``0`.        | Leading '.'                                                                                 |
| .        | Windows | {X} 0   | 0      | append after  | .```0        | Leading '.'                                                                                 |
| .        | Windows | {X} 0   | 9      | insert before | .```0        | Leading '.'                                                                                 |
| .        | Windows | {X} 0   | 9      | append after  | .```0        | Leading '.'                                                                                 |
| ..       | Windows | {X} 0   | 0      | insert before | ``0`..       | Leading '..'                                                                                |
| ..       | Windows | {X} 0   | 0      | append after  | ..```0       | Leading '..'                                                                                |
| ..       | Windows | {X} 0   | 9      | insert before | ..```0       | Leading '..'                                                                                |
| ..       | Windows | {X} 0   | 9      | append after  | ..```0       | Leading '..'                                                                                |
| a/b/c    | Linux   | {G} 0   | 0      | insert before | 0/a/b/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {G} 0   | 0      | append after  | a/0/b/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {G} 0   | 2      | insert before | a/b/0/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {G} 0   | 2      | append after  | a/b/c/0      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {G} 0   | 9      | insert before | a/b/c/0      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {G} 0   | 9      | append after  | a/b/c/0      | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {G} 0   | 0      | insert before | 0/C:/a/b/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {G} 0   | 0      | append after  | C:/0/a/b/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {G} 0   | 3      | insert before | C:/a/b/0/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {G} 0   | 3      | append after  | C:/a/b/c/0   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {G} 0   | 9      | insert before | C:/a/b/c/0   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {G} 0   | 9      | append after  | C:/a/b/c/0   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {G} 0   | 0      | insert before | 0/C:a/b/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {G} 0   | 0      | append after  | C:a/0/b/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {G} 0   | 2      | insert before | C:a/b/0/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {G} 0   | 2      | append after  | C:a/b/c/0    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {G} 0   | 9      | insert before | C:a/b/c/0    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {G} 0   | 9      | append after  | C:a/b/c/0    | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {G} 0   | 0      | insert before | 0//a/b/c     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 0      | append after  | /0/a/b/c     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 3      | insert before | /a/b/0/c     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 3      | append after  | /a/b/c/0     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 9      | insert before | /a/b/c/0     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 9      | append after  | /a/b/c/0     | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 0      | insert before | 0///a/b/c    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 0      | append after  | //a/0/b/c    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 2      | insert before | //a/b/0/c    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 2      | append after  | //a/b/c/0    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 9      | insert before | //a/b/c/0    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 9      | append after  | //a/b/c/0    | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {G} 0   | 0      | insert before | 0/.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {G} 0   | 0      | append after  | ./0          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {G} 0   | 9      | insert before | ./0          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {G} 0   | 9      | append after  | ./0          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {G} 0   | 0      | insert before | 0/.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {G} 0   | 0      | append after  | ./0          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {G} 0   | 9      | insert before | ./0          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {G} 0   | 9      | append after  | ./0          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {G} 0   | 0      | insert before | 0/.          | Leading '.'                                                                                 |
| .        | Linux   | {G} 0   | 0      | append after  | ./0          | Leading '.'                                                                                 |
| .        | Linux   | {G} 0   | 9      | insert before | ./0          | Leading '.'                                                                                 |
| .        | Linux   | {G} 0   | 9      | append after  | ./0          | Leading '.'                                                                                 |
| ..       | Linux   | {G} 0   | 0      | insert before | 0/..         | Leading '..'                                                                                |
| ..       | Linux   | {G} 0   | 0      | append after  | ../0         | Leading '..'                                                                                |
| ..       | Linux   | {G} 0   | 9      | insert before | ../0         | Leading '..'                                                                                |
| ..       | Linux   | {G} 0   | 9      | append after  | ../0         | Leading '..'                                                                                |
| a/b/c    | Linux   | {R}     | 0      | insert before | /a/b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {R}     | 0      | append after  | a//b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {R}     | 2      | insert before | a/b//c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {R}     | 2      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {R}     | 9      | insert before | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {R}     | 9      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {R}     | 0      | insert before | /C:/a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {R}     | 0      | append after  | C://a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {R}     | 3      | insert before | C:/a/b//c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {R}     | 3      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {R}     | 9      | insert before | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {R}     | 9      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {R}     | 0      | insert before | /C:a/b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {R}     | 0      | append after  | C:a//b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {R}     | 2      | insert before | C:a/b//c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {R}     | 2      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {R}     | 9      | insert before | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {R}     | 9      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {R}     | 0      | insert before | //a/b/c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 0      | append after  | //a/b/c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 3      | insert before | /a/b//c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 3      | append after  | /a/b/c/      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 9      | insert before | /a/b/c/      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 9      | append after  | /a/b/c/      | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 0      | insert before | ///a/b/c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 0      | append after  | //a//b/c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 2      | insert before | //a/b//c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 2      | append after  | //a/b/c/     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 9      | insert before | //a/b/c/     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 9      | append after  | //a/b/c/     | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {R}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {R}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {R}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {R}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {R}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {R}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {R}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {R}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {R}     | 0      | insert before | /.           | Leading '.'                                                                                 |
| .        | Linux   | {R}     | 0      | append after  | ./           | Leading '.'                                                                                 |
| .        | Linux   | {R}     | 9      | insert before | ./           | Leading '.'                                                                                 |
| .        | Linux   | {R}     | 9      | append after  | ./           | Leading '.'                                                                                 |
| ..       | Linux   | {R}     | 0      | insert before | /..          | Leading '..'                                                                                |
| ..       | Linux   | {R}     | 0      | append after  | ../          | Leading '..'                                                                                |
| ..       | Linux   | {R}     | 9      | insert before | ../          | Leading '..'                                                                                |
| ..       | Linux   | {R}     | 9      | append after  | ../          | Leading '..'                                                                                |
| a/b/c    | Linux   | {X} 0   | 0      | insert before | //0/a/b/c    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {X} 0   | 0      | append after  | a///0/b/c    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {X} 0   | 2      | insert before | a/b///0/c    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {X} 0   | 2      | append after  | a/b/c///0    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {X} 0   | 9      | insert before | a/b/c///0    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {X} 0   | 9      | append after  | a/b/c///0    | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {X} 0   | 0      | insert before | //0/C:/a/b/c | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {X} 0   | 0      | append after  | C:///0/a/b/c | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {X} 0   | 3      | insert before | C:/a/b///0/c | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {X} 0   | 3      | append after  | C:/a/b/c///0 | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {X} 0   | 9      | insert before | C:/a/b/c///0 | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {X} 0   | 9      | append after  | C:/a/b/c///0 | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {X} 0   | 0      | insert before | //0/C:a/b/c  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {X} 0   | 0      | append after  | C:a///0/b/c  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {X} 0   | 2      | insert before | C:a/b///0/c  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {X} 0   | 2      | append after  | C:a/b/c///0  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {X} 0   | 9      | insert before | C:a/b/c///0  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {X} 0   | 9      | append after  | C:a/b/c///0  | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {X} 0   | 0      | insert before | //0//a/b/c   | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {X} 0   | 0      | append after  | ///0/a/b/c   | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {X} 0   | 3      | insert before | /a/b///0/c   | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {X} 0   | 3      | append after  | /a/b/c///0   | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {X} 0   | 9      | insert before | /a/b/c///0   | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {X} 0   | 9      | append after  | /a/b/c///0   | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {X} 0   | 0      | insert before | //0///a/b/c  | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {X} 0   | 0      | append after  | //a///0/b/c  | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {X} 0   | 2      | insert before | //a/b///0/c  | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {X} 0   | 2      | append after  | //a/b/c///0  | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {X} 0   | 9      | insert before | //a/b/c///0  | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {X} 0   | 9      | append after  | //a/b/c///0  | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {X} 0   | 0      | insert before | //0/.        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {X} 0   | 0      | append after  | .///0        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {X} 0   | 9      | insert before | .///0        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {X} 0   | 9      | append after  | .///0        | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {X} 0   | 0      | insert before | //0/.        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {X} 0   | 0      | append after  | .///0        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {X} 0   | 9      | insert before | .///0        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {X} 0   | 9      | append after  | .///0        | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {X} 0   | 0      | insert before | //0/.        | Leading '.'                                                                                 |
| .        | Linux   | {X} 0   | 0      | append after  | .///0        | Leading '.'                                                                                 |
| .        | Linux   | {X} 0   | 9      | insert before | .///0        | Leading '.'                                                                                 |
| .        | Linux   | {X} 0   | 9      | append after  | .///0        | Leading '.'                                                                                 |
| ..       | Linux   | {X} 0   | 0      | insert before | //0/..       | Leading '..'                                                                                |
| ..       | Linux   | {X} 0   | 0      | append after  | ..///0       | Leading '..'                                                                                |
| ..       | Linux   | {X} 0   | 9      | insert before | ..///0       | Leading '..'                                                                                |
| ..       | Linux   | {X} 0   | 9      | append after  | ..///0       | Leading '..'                                                                                |
| a/b/c    | Linux   | {S} .   | 0      | insert before | ./a/b/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {S} .   | 0      | append after  | a/./b/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {S} .   | 2      | insert before | a/b/./c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {S} .   | 2      | append after  | a/b/c/.      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {S} .   | 9      | insert before | a/b/c/.      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {S} .   | 9      | append after  | a/b/c/.      | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {S} .   | 0      | insert before | ./C:/a/b/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {S} .   | 0      | append after  | C:/./a/b/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {S} .   | 3      | insert before | C:/a/b/./c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {S} .   | 3      | append after  | C:/a/b/c/.   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {S} .   | 9      | insert before | C:/a/b/c/.   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {S} .   | 9      | append after  | C:/a/b/c/.   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {S} .   | 0      | insert before | ./C:a/b/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {S} .   | 0      | append after  | C:a/./b/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {S} .   | 2      | insert before | C:a/b/./c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {S} .   | 2      | append after  | C:a/b/c/.    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {S} .   | 9      | insert before | C:a/b/c/.    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {S} .   | 9      | append after  | C:a/b/c/.    | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {S} .   | 0      | insert before | .//a/b/c     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {S} .   | 0      | append after  | /./a/b/c     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {S} .   | 3      | insert before | /a/b/./c     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {S} .   | 3      | append after  | /a/b/c/.     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {S} .   | 9      | insert before | /a/b/c/.     | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {S} .   | 9      | append after  | /a/b/c/.     | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {S} .   | 0      | insert before | .///a/b/c    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {S} .   | 0      | append after  | //a/./b/c    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {S} .   | 2      | insert before | //a/b/./c    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {S} .   | 2      | append after  | //a/b/c/.    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {S} .   | 9      | insert before | //a/b/c/.    | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {S} .   | 9      | append after  | //a/b/c/.    | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {S} .   | 0      | insert before | ./.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {S} .   | 0      | append after  | ./.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {S} .   | 9      | insert before | ./.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {S} .   | 9      | append after  | ./.          | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {S} .   | 0      | insert before | ./.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {S} .   | 0      | append after  | ./.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {S} .   | 9      | insert before | ./.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {S} .   | 9      | append after  | ./.          | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {S} .   | 0      | insert before | ./.          | Leading '.'                                                                                 |
| .        | Linux   | {S} .   | 0      | append after  | ./.          | Leading '.'                                                                                 |
| .        | Linux   | {S} .   | 9      | insert before | ./.          | Leading '.'                                                                                 |
| .        | Linux   | {S} .   | 9      | append after  | ./.          | Leading '.'                                                                                 |
| ..       | Linux   | {S} .   | 0      | insert before | ./..         | Leading '..'                                                                                |
| ..       | Linux   | {S} .   | 0      | append after  | ../.         | Leading '..'                                                                                |
| ..       | Linux   | {S} .   | 9      | insert before | ../.         | Leading '..'                                                                                |
| ..       | Linux   | {S} .   | 9      | append after  | ../.         | Leading '..'                                                                                |
| a/b/c    | Linux   | {P} ..  | 0      | insert before | ../a/b/c     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {P} ..  | 0      | append after  | a/../b/c     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {P} ..  | 2      | insert before | a/b/../c     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {P} ..  | 2      | append after  | a/b/c/..     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {P} ..  | 9      | insert before | a/b/c/..     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {P} ..  | 9      | append after  | a/b/c/..     | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {P} ..  | 0      | insert before | ../C:/a/b/c  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {P} ..  | 0      | append after  | C:/../a/b/c  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {P} ..  | 3      | insert before | C:/a/b/../c  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {P} ..  | 3      | append after  | C:/a/b/c/..  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {P} ..  | 9      | insert before | C:/a/b/c/..  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {P} ..  | 9      | append after  | C:/a/b/c/..  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {P} ..  | 0      | insert before | ../C:a/b/c   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {P} ..  | 0      | append after  | C:a/../b/c   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {P} ..  | 2      | insert before | C:a/b/../c   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {P} ..  | 2      | append after  | C:a/b/c/..   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {P} ..  | 9      | insert before | C:a/b/c/..   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {P} ..  | 9      | append after  | C:a/b/c/..   | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {P} ..  | 0      | insert before | ..//a/b/c    | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {P} ..  | 0      | append after  | /../a/b/c    | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {P} ..  | 3      | insert before | /a/b/../c    | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {P} ..  | 3      | append after  | /a/b/c/..    | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {P} ..  | 9      | insert before | /a/b/c/..    | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {P} ..  | 9      | append after  | /a/b/c/..    | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {P} ..  | 0      | insert before | ..///a/b/c   | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {P} ..  | 0      | append after  | //a/../b/c   | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {P} ..  | 2      | insert before | //a/b/../c   | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {P} ..  | 2      | append after  | //a/b/c/..   | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {P} ..  | 9      | insert before | //a/b/c/..   | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {P} ..  | 9      | append after  | //a/b/c/..   | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {P} ..  | 0      | insert before | ../.         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {P} ..  | 0      | append after  | ./..         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {P} ..  | 9      | insert before | ./..         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {P} ..  | 9      | append after  | ./..         | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {P} ..  | 0      | insert before | ../.         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {P} ..  | 0      | append after  | ./..         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {P} ..  | 9      | insert before | ./..         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {P} ..  | 9      | append after  | ./..         | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {P} ..  | 0      | insert before | ../.         | Leading '.'                                                                                 |
| .        | Linux   | {P} ..  | 0      | append after  | ./..         | Leading '.'                                                                                 |
| .        | Linux   | {P} ..  | 9      | insert before | ./..         | Leading '.'                                                                                 |
| .        | Linux   | {P} ..  | 9      | append after  | ./..         | Leading '.'                                                                                 |
| ..       | Linux   | {P} ..  | 0      | insert before | ../..        | Leading '..'                                                                                |
| ..       | Linux   | {P} ..  | 0      | append after  | ../..        | Leading '..'                                                                                |
| ..       | Linux   | {P} ..  | 9      | insert before | ../..        | Leading '..'                                                                                |
| ..       | Linux   | {P} ..  | 9      | append after  | ../..        | Leading '..'                                                                                |
| a/b/c    | Linux   | {0}     | 0      | insert before | /a/b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {0}     | 0      | append after  | a//b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {0}     | 2      | insert before | a/b//c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {0}     | 2      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {0}     | 9      | insert before | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {0}     | 9      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {0}     | 0      | insert before | /C:/a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {0}     | 0      | append after  | C://a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {0}     | 3      | insert before | C:/a/b//c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {0}     | 3      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {0}     | 9      | insert before | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {0}     | 9      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {0}     | 0      | insert before | /C:a/b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {0}     | 0      | append after  | C:a//b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {0}     | 2      | insert before | C:a/b//c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {0}     | 2      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {0}     | 9      | insert before | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {0}     | 9      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {0}     | 0      | insert before | //a/b/c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {0}     | 0      | append after  | //a/b/c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {0}     | 3      | insert before | /a/b//c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {0}     | 3      | append after  | /a/b/c/      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {0}     | 9      | insert before | /a/b/c/      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {0}     | 9      | append after  | /a/b/c/      | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {0}     | 0      | insert before | ///a/b/c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {0}     | 0      | append after  | //a//b/c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {0}     | 2      | insert before | //a/b//c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {0}     | 2      | append after  | //a/b/c/     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {0}     | 9      | insert before | //a/b/c/     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {0}     | 9      | append after  | //a/b/c/     | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {0}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {0}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {0}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {0}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {0}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {0}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {0}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {0}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {0}     | 0      | insert before | /.           | Leading '.'                                                                                 |
| .        | Linux   | {0}     | 0      | append after  | ./           | Leading '.'                                                                                 |
| .        | Linux   | {0}     | 9      | insert before | ./           | Leading '.'                                                                                 |
| .        | Linux   | {0}     | 9      | append after  | ./           | Leading '.'                                                                                 |
| ..       | Linux   | {0}     | 0      | insert before | /..          | Leading '..'                                                                                |
| ..       | Linux   | {0}     | 0      | append after  | ../          | Leading '..'                                                                                |
| ..       | Linux   | {0}     | 9      | insert before | ../          | Leading '..'                                                                                |
| ..       | Linux   | {0}     | 9      | append after  | ../          | Leading '..'                                                                                |
| a/b/c    | Linux   | {E}     | 0      | insert before | /a/b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {E}     | 0      | append after  | a//b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {E}     | 2      | insert before | a/b//c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {E}     | 2      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {E}     | 9      | insert before | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {E}     | 9      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {E}     | 0      | insert before | /C:/a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {E}     | 0      | append after  | C://a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {E}     | 3      | insert before | C:/a/b//c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {E}     | 3      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {E}     | 9      | insert before | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {E}     | 9      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {E}     | 0      | insert before | /C:a/b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {E}     | 0      | append after  | C:a//b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {E}     | 2      | insert before | C:a/b//c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {E}     | 2      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {E}     | 9      | insert before | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {E}     | 9      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {E}     | 0      | insert before | //a/b/c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 0      | append after  | //a/b/c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 3      | insert before | /a/b//c      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 3      | append after  | /a/b/c/      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 9      | insert before | /a/b/c/      | Leading '{$} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 9      | append after  | /a/b/c/      | Leading '{$} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 0      | insert before | ///a/b/c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 0      | append after  | //a//b/c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 2      | insert before | //a/b//c     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 2      | append after  | //a/b/c/     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 9      | insert before | //a/b/c/     | Leading '{X} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 9      | append after  | //a/b/c/     | Leading '{X} a'                                                                             |
| (empty)  | Linux   | {E}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {E}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {E}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (empty)  | Linux   | {E}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{S}'                                           |
| (null)   | Linux   | {E}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {E}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {E}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| (null)   | Linux   | {E}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{0}' turns into '{S}'                                           |
| .        | Linux   | {E}     | 0      | insert before | /.           | Leading '.'                                                                                 |
| .        | Linux   | {E}     | 0      | append after  | ./           | Leading '.'                                                                                 |
| .        | Linux   | {E}     | 9      | insert before | ./           | Leading '.'                                                                                 |
| .        | Linux   | {E}     | 9      | append after  | ./           | Leading '.'                                                                                 |
| ..       | Linux   | {E}     | 0      | insert before | /..          | Leading '..'                                                                                |
| ..       | Linux   | {E}     | 0      | append after  | ../          | Leading '..'                                                                                |
| ..       | Linux   | {E}     | 9      | insert before | ../          | Leading '..'                                                                                |
| ..       | Linux   | {E}     | 9      | append after  | ../          | Leading '..'                                                                                |
