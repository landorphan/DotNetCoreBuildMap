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
| `a`b`c   | Windows | {G} 0   | 0      | insert before | 0``a`b`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 0      | append after  | `0`a`b`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 3      | insert before | `a`b`0`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 3      | append after  | `a`b`c`0     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 9      | insert before | `a`b`c`0     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {G} 0   | 9      | append after  | `a`b`c`0     | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 0      | insert before | 0```a`b`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 0      | append after  | ``a`0`b`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 2      | insert before | ``a`b`0`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 2      | append after  | ``a`b`c`0    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 9      | insert before | ``a`b`c`0    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {G} 0   | 9      | append after  | ``a`b`c`0    | Leading '{U} a'                                                                             |
| (empty)  | Windows | {G} 0   | 0      | insert before | 0`.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {G} 0   | 0      | append after  | .`0          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {G} 0   | 9      | insert before | .`0          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {G} 0   | 9      | append after  | .`0          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {G} 0   | 0      | insert before | 0`.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {G} 0   | 0      | append after  | .`0          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {G} 0   | 9      | insert before | .`0          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {G} 0   | 9      | append after  | .`0          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Windows | {G} 0   | 0      | insert before | 0`.          | Leading '.'                                                                                 |
| .        | Windows | {G} 0   | 0      | append after  | .`0          | Leading '.'                                                                                 |
| .        | Windows | {G} 0   | 9      | insert before | .`0          | Leading '.'                                                                                 |
| .        | Windows | {G} 0   | 9      | append after  | .`0          | Leading '.'                                                                                 |
| ..       | Windows | {G} 0   | 0      | insert before | 0`..         | Leading '..'                                                                                |
| ..       | Windows | {G} 0   | 0      | append after  | ..`0         | Leading '..'                                                                                |
| ..       | Windows | {G} 0   | 9      | insert before | ..`0         | Leading '..'                                                                                |
| ..       | Windows | {G} 0   | 9      | append after  | ..`0         | Leading '..'                                                                                |
| a`b`c    | Windows | {.} .   | 0      | insert before | .`a`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {.} .   | 0      | append after  | a`.`b`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {.} .   | 2      | insert before | a`b`.`c      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {.} .   | 2      | append after  | a`b`c`.      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {.} .   | 9      | insert before | a`b`c`.      | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {.} .   | 9      | append after  | a`b`c`.      | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {.} .   | 0      | insert before | .`C:`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {.} .   | 0      | append after  | C:`.`a`b`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {.} .   | 3      | insert before | C:`a`b`.`c   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {.} .   | 3      | append after  | C:`a`b`c`.   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {.} .   | 9      | insert before | C:`a`b`c`.   | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {.} .   | 9      | append after  | C:`a`b`c`.   | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {.} .   | 0      | insert before | .`C:a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {.} .   | 0      | append after  | C:.`a`b`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {.} .   | 3      | insert before | C:a`b`.`c    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {.} .   | 3      | append after  | C:a`b`c`.    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {.} .   | 9      | insert before | C:a`b`c`.    | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {.} .   | 9      | append after  | C:a`b`c`.    | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {.} .   | 0      | insert before | .``a`b`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {.} .   | 0      | append after  | `.`a`b`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {.} .   | 3      | insert before | `a`b`.`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {.} .   | 3      | append after  | `a`b`c`.     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {.} .   | 9      | insert before | `a`b`c`.     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {.} .   | 9      | append after  | `a`b`c`.     | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {.} .   | 0      | insert before | .```a`b`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {.} .   | 0      | append after  | ``a`.`b`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {.} .   | 2      | insert before | ``a`b`.`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {.} .   | 2      | append after  | ``a`b`c`.    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {.} .   | 9      | insert before | ``a`b`c`.    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {.} .   | 9      | append after  | ``a`b`c`.    | Leading '{U} a'                                                                             |
| (empty)  | Windows | {.} .   | 0      | insert before | .`.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {.} .   | 0      | append after  | .`.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {.} .   | 9      | insert before | .`.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {.} .   | 9      | append after  | .`.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {.} .   | 0      | insert before | .`.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {.} .   | 0      | append after  | .`.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {.} .   | 9      | insert before | .`.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {.} .   | 9      | append after  | .`.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Windows | {.} .   | 0      | insert before | .`.          | Leading '.'                                                                                 |
| .        | Windows | {.} .   | 0      | append after  | .`.          | Leading '.'                                                                                 |
| .        | Windows | {.} .   | 9      | insert before | .`.          | Leading '.'                                                                                 |
| .        | Windows | {.} .   | 9      | append after  | .`.          | Leading '.'                                                                                 |
| ..       | Windows | {.} .   | 0      | insert before | .`..         | Leading '..'                                                                                |
| ..       | Windows | {.} .   | 0      | append after  | ..`.         | Leading '..'                                                                                |
| ..       | Windows | {.} .   | 9      | insert before | ..`.         | Leading '..'                                                                                |
| ..       | Windows | {.} .   | 9      | append after  | ..`.         | Leading '..'                                                                                |
| a`b`c    | Windows | {..} .. | 0      | insert before | ..`a`b`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {..} .. | 0      | append after  | a`..`b`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {..} .. | 2      | insert before | a`b`..`c     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {..} .. | 2      | append after  | a`b`c`..     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {..} .. | 9      | insert before | a`b`c`..     | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {..} .. | 9      | append after  | a`b`c`..     | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {..} .. | 0      | insert before | ..`C:`a`b`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {..} .. | 0      | append after  | C:`..`a`b`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {..} .. | 3      | insert before | C:`a`b`..`c  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {..} .. | 3      | append after  | C:`a`b`c`..  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {..} .. | 9      | insert before | C:`a`b`c`..  | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {..} .. | 9      | append after  | C:`a`b`c`..  | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {..} .. | 0      | insert before | ..`C:a`b`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {..} .. | 0      | append after  | C:..`a`b`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {..} .. | 3      | insert before | C:a`b`..`c   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {..} .. | 3      | append after  | C:a`b`c`..   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {..} .. | 9      | insert before | C:a`b`c`..   | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {..} .. | 9      | append after  | C:a`b`c`..   | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {..} .. | 0      | insert before | ..``a`b`c    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {..} .. | 0      | append after  | `..`a`b`c    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {..} .. | 3      | insert before | `a`b`..`c    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {..} .. | 3      | append after  | `a`b`c`..    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {..} .. | 9      | insert before | `a`b`c`..    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {..} .. | 9      | append after  | `a`b`c`..    | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {..} .. | 0      | insert before | ..```a`b`c   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {..} .. | 0      | append after  | ``a`..`b`c   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {..} .. | 2      | insert before | ``a`b`..`c   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {..} .. | 2      | append after  | ``a`b`c`..   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {..} .. | 9      | insert before | ``a`b`c`..   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {..} .. | 9      | append after  | ``a`b`c`..   | Leading '{U} a'                                                                             |
| (empty)  | Windows | {..} .. | 0      | insert before | ..`.         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {..} .. | 0      | append after  | .`..         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {..} .. | 9      | insert before | .`..         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {..} .. | 9      | append after  | .`..         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {..} .. | 0      | insert before | ..`.         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {..} .. | 0      | append after  | .`..         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {..} .. | 9      | insert before | .`..         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {..} .. | 9      | append after  | .`..         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Windows | {..} .. | 0      | insert before | ..`.         | Leading '.'                                                                                 |
| .        | Windows | {..} .. | 0      | append after  | .`..         | Leading '.'                                                                                 |
| .        | Windows | {..} .. | 9      | insert before | .`..         | Leading '.'                                                                                 |
| .        | Windows | {..} .. | 9      | append after  | .`..         | Leading '.'                                                                                 |
| ..       | Windows | {..} .. | 0      | insert before | ..`..        | Leading '..'                                                                                |
| ..       | Windows | {..} .. | 0      | append after  | ..`..        | Leading '..'                                                                                |
| ..       | Windows | {..} .. | 9      | insert before | ..`..        | Leading '..'                                                                                |
| ..       | Windows | {..} .. | 9      | append after  | ..`..        | Leading '..'                                                                                |
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
| `a`b`c   | Windows | {E}     | 0      | insert before | ``a`b`c      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 0      | append after  | ``a`b`c      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 3      | insert before | `a`b``c      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 3      | append after  | `a`b`c`      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 9      | insert before | `a`b`c`      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {E}     | 9      | append after  | `a`b`c`      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 0      | insert before | ```a`b`c     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 0      | append after  | ``a``b`c     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 2      | insert before | ``a`b``c     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 2      | append after  | ``a`b`c`     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 9      | insert before | ``a`b`c`     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {E}     | 9      | append after  | ``a`b`c`     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
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
| a`b`c    | Windows | {N}     | 0      | insert before | `a`b`c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {N}     | 0      | append after  | a``b`c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {N}     | 2      | insert before | a`b``c       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {N}     | 2      | append after  | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {N}     | 9      | insert before | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| a`b`c    | Windows | {N}     | 9      | append after  | a`b`c`       | Leading '{G} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {N}     | 0      | insert before | `C:`a`b`c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {N}     | 0      | append after  | C:``a`b`c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {N}     | 3      | insert before | C:`a`b``c    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {N}     | 3      | append after  | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {N}     | 9      | insert before | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:`a`b`c | Windows | {N}     | 9      | append after  | C:`a`b`c`    | Leading '{R} C:`' NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {N}     | 0      | insert before | `C:a`b`c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {N}     | 0      | append after  | C:`a`b`c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {N}     | 3      | insert before | C:a`b``c     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {N}     | 3      | append after  | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {N}     | 9      | insert before | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| C:a`b`c  | Windows | {N}     | 9      | append after  | C:a`b`c`     | Leading '{V} C:'  NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {N}     | 0      | insert before | ``a`b`c      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {N}     | 0      | append after  | ``a`b`c      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {N}     | 3      | insert before | `a`b``c      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {N}     | 3      | append after  | `a`b`c`      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {N}     | 9      | insert before | `a`b`c`      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| `a`b`c   | Windows | {N}     | 9      | append after  | `a`b`c`      | Leading '{/} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {N}     | 0      | insert before | ```a`b`c     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {N}     | 0      | append after  | ``a``b`c     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {N}     | 2      | insert before | ``a`b``c     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {N}     | 2      | append after  | ``a`b`c`     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {N}     | 9      | insert before | ``a`b`c`     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| ``a`b`c  | Windows | {N}     | 9      | append after  | ``a`b`c`     | Leading '{U} a'   NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {N}     | 0      | insert before | `.           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {N}     | 0      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {N}     | 9      | insert before | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (empty)  | Windows | {N}     | 9      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {N}     | 0      | insert before | `.           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {N}     | 0      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {N}     | 9      | insert before | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| (null)   | Windows | {N}     | 9      | append after  | .`           | Leading '{E}'     NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {N}     | 0      | insert before | `.           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {N}     | 0      | append after  | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {N}     | 9      | insert before | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| .        | Windows | {N}     | 9      | append after  | .`           | Leading '.'       NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {N}     | 0      | insert before | `..          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {N}     | 0      | append after  | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {N}     | 9      | insert before | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
| ..       | Windows | {N}     | 9      | append after  | ..`          | Leading '..'      NOTE: Because the segment is created by caller, it is not turned into '.' |
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
| `a`b`c   | Windows | {V} C   | 0      | insert before | C:`a`b`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 0      | append after  | `C:a`b`c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 3      | insert before | `a`b`C:c     | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 3      | append after  | `a`b`c`C:    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 9      | insert before | `a`b`c`C:    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {V} C   | 9      | append after  | `a`b`c`C:    | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 0      | insert before | C:``a`b`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 0      | append after  | ``a`C:b`c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 2      | insert before | ``a`b`C:c    | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 2      | append after  | ``a`b`c`C:   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 9      | insert before | ``a`b`c`C:   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {V} C   | 9      | append after  | ``a`b`c`C:   | Leading '{U} a'                                                                             |
| (empty)  | Windows | {V} C   | 0      | insert before | C:.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {V} C   | 0      | append after  | .`C:         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {V} C   | 9      | insert before | .`C:         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {V} C   | 9      | append after  | .`C:         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {V} C   | 0      | insert before | C:.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {V} C   | 0      | append after  | .`C:         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {V} C   | 9      | insert before | .`C:         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {V} C   | 9      | append after  | .`C:         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
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
| `a`b`c   | Windows | {R} C   | 0      | insert before | C:``a`b`c    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 0      | append after  | `C:`a`b`c    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 3      | insert before | `a`b`C:`c    | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 3      | append after  | `a`b`c`C:`   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 9      | insert before | `a`b`c`C:`   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {R} C   | 9      | append after  | `a`b`c`C:`   | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 0      | insert before | C:```a`b`c   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 0      | append after  | ``a`C:`b`c   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 2      | insert before | ``a`b`C:`c   | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 2      | append after  | ``a`b`c`C:`  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 9      | insert before | ``a`b`c`C:`  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {R} C   | 9      | append after  | ``a`b`c`C:`  | Leading '{U} a'                                                                             |
| (empty)  | Windows | {R} C   | 0      | insert before | C:`.         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {R} C   | 0      | append after  | .`C:`        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {R} C   | 9      | insert before | .`C:`        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {R} C   | 9      | append after  | .`C:`        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {R} C   | 0      | insert before | C:`.         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {R} C   | 0      | append after  | .`C:`        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {R} C   | 9      | insert before | .`C:`        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {R} C   | 9      | append after  | .`C:`        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Windows | {R} C   | 0      | insert before | C:`.         | Leading '.'                                                                                 |
| .        | Windows | {R} C   | 0      | append after  | .`C:`        | Leading '.'                                                                                 |
| .        | Windows | {R} C   | 9      | insert before | .`C:`        | Leading '.'                                                                                 |
| .        | Windows | {R} C   | 9      | append after  | .`C:`        | Leading '.'                                                                                 |
| ..       | Windows | {R} C   | 0      | insert before | C:`..        | Leading '..'                                                                                |
| ..       | Windows | {R} C   | 0      | append after  | ..`C:`       | Leading '..'                                                                                |
| ..       | Windows | {R} C   | 9      | insert before | ..`C:`       | Leading '..'                                                                                |
| ..       | Windows | {R} C   | 9      | append after  | ..`C:`       | Leading '..'                                                                                |
| a`b`c    | Windows | {/}     | 0      | insert before | `a`b`c       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {/}     | 0      | append after  | a``b`c       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {/}     | 2      | insert before | a`b``c       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {/}     | 2      | append after  | a`b`c`       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {/}     | 9      | insert before | a`b`c`       | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {/}     | 9      | append after  | a`b`c`       | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {/}     | 0      | insert before | `C:`a`b`c    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {/}     | 0      | append after  | C:``a`b`c    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {/}     | 3      | insert before | C:`a`b``c    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {/}     | 3      | append after  | C:`a`b`c`    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {/}     | 9      | insert before | C:`a`b`c`    | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {/}     | 9      | append after  | C:`a`b`c`    | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {/}     | 0      | insert before | `C:a`b`c     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {/}     | 0      | append after  | C:`a`b`c     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {/}     | 3      | insert before | C:a`b``c     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {/}     | 3      | append after  | C:a`b`c`     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {/}     | 9      | insert before | C:a`b`c`     | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {/}     | 9      | append after  | C:a`b`c`     | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {/}     | 0      | insert before | ``a`b`c      | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {/}     | 0      | append after  | ``a`b`c      | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {/}     | 3      | insert before | `a`b``c      | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {/}     | 3      | append after  | `a`b`c`      | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {/}     | 9      | insert before | `a`b`c`      | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {/}     | 9      | append after  | `a`b`c`      | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {/}     | 0      | insert before | ```a`b`c     | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {/}     | 0      | append after  | ``a``b`c     | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {/}     | 2      | insert before | ``a`b``c     | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {/}     | 2      | append after  | ``a`b`c`     | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {/}     | 9      | insert before | ``a`b`c`     | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {/}     | 9      | append after  | ``a`b`c`     | Leading '{U} a'                                                                             |
| (empty)  | Windows | {/}     | 0      | insert before | `.           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {/}     | 0      | append after  | .`           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {/}     | 9      | insert before | .`           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {/}     | 9      | append after  | .`           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {/}     | 0      | insert before | `.           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {/}     | 0      | append after  | .`           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {/}     | 9      | insert before | .`           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {/}     | 9      | append after  | .`           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Windows | {/}     | 0      | insert before | `.           | Leading '.'                                                                                 |
| .        | Windows | {/}     | 0      | append after  | .`           | Leading '.'                                                                                 |
| .        | Windows | {/}     | 9      | insert before | .`           | Leading '.'                                                                                 |
| .        | Windows | {/}     | 9      | append after  | .`           | Leading '.'                                                                                 |
| ..       | Windows | {/}     | 0      | insert before | `..          | Leading '..'                                                                                |
| ..       | Windows | {/}     | 0      | append after  | ..`          | Leading '..'                                                                                |
| ..       | Windows | {/}     | 9      | insert before | ..`          | Leading '..'                                                                                |
| ..       | Windows | {/}     | 9      | append after  | ..`          | Leading '..'                                                                                |
| a`b`c    | Windows | {U} 0   | 0      | insert before | ``0`a`b`c    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {U} 0   | 0      | append after  | a```0`b`c    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {U} 0   | 2      | insert before | a`b```0`c    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {U} 0   | 2      | append after  | a`b`c```0    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {U} 0   | 9      | insert before | a`b`c```0    | Leading '{G} a'                                                                             |
| a`b`c    | Windows | {U} 0   | 9      | append after  | a`b`c```0    | Leading '{G} a'                                                                             |
| C:`a`b`c | Windows | {U} 0   | 0      | insert before | ``0`C:`a`b`c | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {U} 0   | 0      | append after  | C:```0`a`b`c | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {U} 0   | 3      | insert before | C:`a`b```0`c | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {U} 0   | 3      | append after  | C:`a`b`c```0 | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {U} 0   | 9      | insert before | C:`a`b`c```0 | Leading '{R} C:`'                                                                           |
| C:`a`b`c | Windows | {U} 0   | 9      | append after  | C:`a`b`c```0 | Leading '{R} C:`'                                                                           |
| C:a`b`c  | Windows | {U} 0   | 0      | insert before | ``0`C:a`b`c  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {U} 0   | 0      | append after  | C:``0`a`b`c  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {U} 0   | 3      | insert before | C:a`b```0`c  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {U} 0   | 3      | append after  | C:a`b`c```0  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {U} 0   | 9      | insert before | C:a`b`c```0  | Leading '{V} C:'                                                                            |
| C:a`b`c  | Windows | {U} 0   | 9      | append after  | C:a`b`c```0  | Leading '{V} C:'                                                                            |
| `a`b`c   | Windows | {U} 0   | 0      | insert before | ``0``a`b`c   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {U} 0   | 0      | append after  | ```0`a`b`c   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {U} 0   | 3      | insert before | `a`b```0`c   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {U} 0   | 3      | append after  | `a`b`c```0   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {U} 0   | 9      | insert before | `a`b`c```0   | Leading '{/} a'                                                                             |
| `a`b`c   | Windows | {U} 0   | 9      | append after  | `a`b`c```0   | Leading '{/} a'                                                                             |
| ``a`b`c  | Windows | {U} 0   | 0      | insert before | ``0```a`b`c  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {U} 0   | 0      | append after  | ``a```0`b`c  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {U} 0   | 2      | insert before | ``a`b```0`c  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {U} 0   | 2      | append after  | ``a`b`c```0  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {U} 0   | 9      | insert before | ``a`b`c```0  | Leading '{U} a'                                                                             |
| ``a`b`c  | Windows | {U} 0   | 9      | append after  | ``a`b`c```0  | Leading '{U} a'                                                                             |
| (empty)  | Windows | {U} 0   | 0      | insert before | ``0`.        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {U} 0   | 0      | append after  | .```0        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {U} 0   | 9      | insert before | .```0        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Windows | {U} 0   | 9      | append after  | .```0        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Windows | {U} 0   | 0      | insert before | ``0`.        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {U} 0   | 0      | append after  | .```0        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {U} 0   | 9      | insert before | .```0        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Windows | {U} 0   | 9      | append after  | .```0        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Windows | {U} 0   | 0      | insert before | ``0`.        | Leading '.'                                                                                 |
| .        | Windows | {U} 0   | 0      | append after  | .```0        | Leading '.'                                                                                 |
| .        | Windows | {U} 0   | 9      | insert before | .```0        | Leading '.'                                                                                 |
| .        | Windows | {U} 0   | 9      | append after  | .```0        | Leading '.'                                                                                 |
| ..       | Windows | {U} 0   | 0      | insert before | ``0`..       | Leading '..'                                                                                |
| ..       | Windows | {U} 0   | 0      | append after  | ..```0       | Leading '..'                                                                                |
| ..       | Windows | {U} 0   | 9      | insert before | ..```0       | Leading '..'                                                                                |
| ..       | Windows | {U} 0   | 9      | append after  | ..```0       | Leading '..'                                                                                |
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
| /a/b/c   | Linux   | {G} 0   | 0      | insert before | 0//a/b/c     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 0      | append after  | /0/a/b/c     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 3      | insert before | /a/b/0/c     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 3      | append after  | /a/b/c/0     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 9      | insert before | /a/b/c/0     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {G} 0   | 9      | append after  | /a/b/c/0     | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 0      | insert before | 0///a/b/c    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 0      | append after  | //a/0/b/c    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 2      | insert before | //a/b/0/c    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 2      | append after  | //a/b/c/0    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 9      | insert before | //a/b/c/0    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {G} 0   | 9      | append after  | //a/b/c/0    | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {G} 0   | 0      | insert before | 0/.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {G} 0   | 0      | append after  | ./0          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {G} 0   | 9      | insert before | ./0          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {G} 0   | 9      | append after  | ./0          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {G} 0   | 0      | insert before | 0/.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {G} 0   | 0      | append after  | ./0          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {G} 0   | 9      | insert before | ./0          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {G} 0   | 9      | append after  | ./0          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
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
| /a/b/c   | Linux   | {R}     | 0      | insert before | //a/b/c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 0      | append after  | //a/b/c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 3      | insert before | /a/b//c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 3      | append after  | /a/b/c/      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 9      | insert before | /a/b/c/      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {R}     | 9      | append after  | /a/b/c/      | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 0      | insert before | ///a/b/c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 0      | append after  | //a//b/c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 2      | insert before | //a/b//c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 2      | append after  | //a/b/c/     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 9      | insert before | //a/b/c/     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {R}     | 9      | append after  | //a/b/c/     | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {R}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {R}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {R}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {R}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {R}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {R}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {R}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {R}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Linux   | {R}     | 0      | insert before | /.           | Leading '.'                                                                                 |
| .        | Linux   | {R}     | 0      | append after  | ./           | Leading '.'                                                                                 |
| .        | Linux   | {R}     | 9      | insert before | ./           | Leading '.'                                                                                 |
| .        | Linux   | {R}     | 9      | append after  | ./           | Leading '.'                                                                                 |
| ..       | Linux   | {R}     | 0      | insert before | /..          | Leading '..'                                                                                |
| ..       | Linux   | {R}     | 0      | append after  | ../          | Leading '..'                                                                                |
| ..       | Linux   | {R}     | 9      | insert before | ../          | Leading '..'                                                                                |
| ..       | Linux   | {R}     | 9      | append after  | ../          | Leading '..'                                                                                |
| a/b/c    | Linux   | {U} 0   | 0      | insert before | //0/a/b/c    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {U} 0   | 0      | append after  | a///0/b/c    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {U} 0   | 2      | insert before | a/b///0/c    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {U} 0   | 2      | append after  | a/b/c///0    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {U} 0   | 9      | insert before | a/b/c///0    | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {U} 0   | 9      | append after  | a/b/c///0    | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {U} 0   | 0      | insert before | //0/C:/a/b/c | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {U} 0   | 0      | append after  | C:///0/a/b/c | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {U} 0   | 3      | insert before | C:/a/b///0/c | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {U} 0   | 3      | append after  | C:/a/b/c///0 | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {U} 0   | 9      | insert before | C:/a/b/c///0 | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {U} 0   | 9      | append after  | C:/a/b/c///0 | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {U} 0   | 0      | insert before | //0/C:a/b/c  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {U} 0   | 0      | append after  | C:a///0/b/c  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {U} 0   | 2      | insert before | C:a/b///0/c  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {U} 0   | 2      | append after  | C:a/b/c///0  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {U} 0   | 9      | insert before | C:a/b/c///0  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {U} 0   | 9      | append after  | C:a/b/c///0  | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {U} 0   | 0      | insert before | //0//a/b/c   | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {U} 0   | 0      | append after  | ///0/a/b/c   | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {U} 0   | 3      | insert before | /a/b///0/c   | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {U} 0   | 3      | append after  | /a/b/c///0   | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {U} 0   | 9      | insert before | /a/b/c///0   | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {U} 0   | 9      | append after  | /a/b/c///0   | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {U} 0   | 0      | insert before | //0///a/b/c  | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {U} 0   | 0      | append after  | //a///0/b/c  | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {U} 0   | 2      | insert before | //a/b///0/c  | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {U} 0   | 2      | append after  | //a/b/c///0  | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {U} 0   | 9      | insert before | //a/b/c///0  | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {U} 0   | 9      | append after  | //a/b/c///0  | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {U} 0   | 0      | insert before | //0/.        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {U} 0   | 0      | append after  | .///0        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {U} 0   | 9      | insert before | .///0        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {U} 0   | 9      | append after  | .///0        | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {U} 0   | 0      | insert before | //0/.        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {U} 0   | 0      | append after  | .///0        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {U} 0   | 9      | insert before | .///0        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {U} 0   | 9      | append after  | .///0        | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Linux   | {U} 0   | 0      | insert before | //0/.        | Leading '.'                                                                                 |
| .        | Linux   | {U} 0   | 0      | append after  | .///0        | Leading '.'                                                                                 |
| .        | Linux   | {U} 0   | 9      | insert before | .///0        | Leading '.'                                                                                 |
| .        | Linux   | {U} 0   | 9      | append after  | .///0        | Leading '.'                                                                                 |
| ..       | Linux   | {U} 0   | 0      | insert before | //0/..       | Leading '..'                                                                                |
| ..       | Linux   | {U} 0   | 0      | append after  | ..///0       | Leading '..'                                                                                |
| ..       | Linux   | {U} 0   | 9      | insert before | ..///0       | Leading '..'                                                                                |
| ..       | Linux   | {U} 0   | 9      | append after  | ..///0       | Leading '..'                                                                                |
| a/b/c    | Linux   | {.} .   | 0      | insert before | ./a/b/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {.} .   | 0      | append after  | a/./b/c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {.} .   | 2      | insert before | a/b/./c      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {.} .   | 2      | append after  | a/b/c/.      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {.} .   | 9      | insert before | a/b/c/.      | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {.} .   | 9      | append after  | a/b/c/.      | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {.} .   | 0      | insert before | ./C:/a/b/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {.} .   | 0      | append after  | C:/./a/b/c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {.} .   | 3      | insert before | C:/a/b/./c   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {.} .   | 3      | append after  | C:/a/b/c/.   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {.} .   | 9      | insert before | C:/a/b/c/.   | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {.} .   | 9      | append after  | C:/a/b/c/.   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {.} .   | 0      | insert before | ./C:a/b/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {.} .   | 0      | append after  | C:a/./b/c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {.} .   | 2      | insert before | C:a/b/./c    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {.} .   | 2      | append after  | C:a/b/c/.    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {.} .   | 9      | insert before | C:a/b/c/.    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {.} .   | 9      | append after  | C:a/b/c/.    | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {.} .   | 0      | insert before | .//a/b/c     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {.} .   | 0      | append after  | /./a/b/c     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {.} .   | 3      | insert before | /a/b/./c     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {.} .   | 3      | append after  | /a/b/c/.     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {.} .   | 9      | insert before | /a/b/c/.     | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {.} .   | 9      | append after  | /a/b/c/.     | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {.} .   | 0      | insert before | .///a/b/c    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {.} .   | 0      | append after  | //a/./b/c    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {.} .   | 2      | insert before | //a/b/./c    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {.} .   | 2      | append after  | //a/b/c/.    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {.} .   | 9      | insert before | //a/b/c/.    | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {.} .   | 9      | append after  | //a/b/c/.    | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {.} .   | 0      | insert before | ./.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {.} .   | 0      | append after  | ./.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {.} .   | 9      | insert before | ./.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {.} .   | 9      | append after  | ./.          | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {.} .   | 0      | insert before | ./.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {.} .   | 0      | append after  | ./.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {.} .   | 9      | insert before | ./.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {.} .   | 9      | append after  | ./.          | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Linux   | {.} .   | 0      | insert before | ./.          | Leading '.'                                                                                 |
| .        | Linux   | {.} .   | 0      | append after  | ./.          | Leading '.'                                                                                 |
| .        | Linux   | {.} .   | 9      | insert before | ./.          | Leading '.'                                                                                 |
| .        | Linux   | {.} .   | 9      | append after  | ./.          | Leading '.'                                                                                 |
| ..       | Linux   | {.} .   | 0      | insert before | ./..         | Leading '..'                                                                                |
| ..       | Linux   | {.} .   | 0      | append after  | ../.         | Leading '..'                                                                                |
| ..       | Linux   | {.} .   | 9      | insert before | ../.         | Leading '..'                                                                                |
| ..       | Linux   | {.} .   | 9      | append after  | ../.         | Leading '..'                                                                                |
| a/b/c    | Linux   | {..} .. | 0      | insert before | ../a/b/c     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {..} .. | 0      | append after  | a/../b/c     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {..} .. | 2      | insert before | a/b/../c     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {..} .. | 2      | append after  | a/b/c/..     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {..} .. | 9      | insert before | a/b/c/..     | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {..} .. | 9      | append after  | a/b/c/..     | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {..} .. | 0      | insert before | ../C:/a/b/c  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {..} .. | 0      | append after  | C:/../a/b/c  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {..} .. | 3      | insert before | C:/a/b/../c  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {..} .. | 3      | append after  | C:/a/b/c/..  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {..} .. | 9      | insert before | C:/a/b/c/..  | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {..} .. | 9      | append after  | C:/a/b/c/..  | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {..} .. | 0      | insert before | ../C:a/b/c   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {..} .. | 0      | append after  | C:a/../b/c   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {..} .. | 2      | insert before | C:a/b/../c   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {..} .. | 2      | append after  | C:a/b/c/..   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {..} .. | 9      | insert before | C:a/b/c/..   | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {..} .. | 9      | append after  | C:a/b/c/..   | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {..} .. | 0      | insert before | ..//a/b/c    | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {..} .. | 0      | append after  | /../a/b/c    | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {..} .. | 3      | insert before | /a/b/../c    | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {..} .. | 3      | append after  | /a/b/c/..    | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {..} .. | 9      | insert before | /a/b/c/..    | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {..} .. | 9      | append after  | /a/b/c/..    | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {..} .. | 0      | insert before | ..///a/b/c   | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {..} .. | 0      | append after  | //a/../b/c   | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {..} .. | 2      | insert before | //a/b/../c   | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {..} .. | 2      | append after  | //a/b/c/..   | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {..} .. | 9      | insert before | //a/b/c/..   | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {..} .. | 9      | append after  | //a/b/c/..   | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {..} .. | 0      | insert before | ../.         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {..} .. | 0      | append after  | ./..         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {..} .. | 9      | insert before | ./..         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {..} .. | 9      | append after  | ./..         | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {..} .. | 0      | insert before | ../.         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {..} .. | 0      | append after  | ./..         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {..} .. | 9      | insert before | ./..         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {..} .. | 9      | append after  | ./..         | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Linux   | {..} .. | 0      | insert before | ../.         | Leading '.'                                                                                 |
| .        | Linux   | {..} .. | 0      | append after  | ./..         | Leading '.'                                                                                 |
| .        | Linux   | {..} .. | 9      | insert before | ./..         | Leading '.'                                                                                 |
| .        | Linux   | {..} .. | 9      | append after  | ./..         | Leading '.'                                                                                 |
| ..       | Linux   | {..} .. | 0      | insert before | ../..        | Leading '..'                                                                                |
| ..       | Linux   | {..} .. | 0      | append after  | ../..        | Leading '..'                                                                                |
| ..       | Linux   | {..} .. | 9      | insert before | ../..        | Leading '..'                                                                                |
| ..       | Linux   | {..} .. | 9      | append after  | ../..        | Leading '..'                                                                                |
| a/b/c    | Linux   | {N}     | 0      | insert before | /a/b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {N}     | 0      | append after  | a//b/c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {N}     | 2      | insert before | a/b//c       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {N}     | 2      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {N}     | 9      | insert before | a/b/c/       | Leading '{G} a'                                                                             |
| a/b/c    | Linux   | {N}     | 9      | append after  | a/b/c/       | Leading '{G} a'                                                                             |
| C:/a/b/c | Linux   | {N}     | 0      | insert before | /C:/a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {N}     | 0      | append after  | C://a/b/c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {N}     | 3      | insert before | C:/a/b//c    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {N}     | 3      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {N}     | 9      | insert before | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:/a/b/c | Linux   | {N}     | 9      | append after  | C:/a/b/c/    | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {N}     | 0      | insert before | /C:a/b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {N}     | 0      | append after  | C:a//b/c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {N}     | 2      | insert before | C:a/b//c     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {N}     | 2      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {N}     | 9      | insert before | C:a/b/c/     | Leading '{G} C:'                                                                            |
| C:a/b/c  | Linux   | {N}     | 9      | append after  | C:a/b/c/     | Leading '{G} C:'                                                                            |
| /a/b/c   | Linux   | {N}     | 0      | insert before | //a/b/c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {N}     | 0      | append after  | //a/b/c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {N}     | 3      | insert before | /a/b//c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {N}     | 3      | append after  | /a/b/c/      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {N}     | 9      | insert before | /a/b/c/      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {N}     | 9      | append after  | /a/b/c/      | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {N}     | 0      | insert before | ///a/b/c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {N}     | 0      | append after  | //a//b/c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {N}     | 2      | insert before | //a/b//c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {N}     | 2      | append after  | //a/b/c/     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {N}     | 9      | insert before | //a/b/c/     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {N}     | 9      | append after  | //a/b/c/     | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {N}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {N}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {N}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {N}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {N}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {N}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {N}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {N}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Linux   | {N}     | 0      | insert before | /.           | Leading '.'                                                                                 |
| .        | Linux   | {N}     | 0      | append after  | ./           | Leading '.'                                                                                 |
| .        | Linux   | {N}     | 9      | insert before | ./           | Leading '.'                                                                                 |
| .        | Linux   | {N}     | 9      | append after  | ./           | Leading '.'                                                                                 |
| ..       | Linux   | {N}     | 0      | insert before | /..          | Leading '..'                                                                                |
| ..       | Linux   | {N}     | 0      | append after  | ../          | Leading '..'                                                                                |
| ..       | Linux   | {N}     | 9      | insert before | ../          | Leading '..'                                                                                |
| ..       | Linux   | {N}     | 9      | append after  | ../          | Leading '..'                                                                                |
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
| /a/b/c   | Linux   | {E}     | 0      | insert before | //a/b/c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 0      | append after  | //a/b/c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 3      | insert before | /a/b//c      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 3      | append after  | /a/b/c/      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 9      | insert before | /a/b/c/      | Leading '{/} a'                                                                             |
| /a/b/c   | Linux   | {E}     | 9      | append after  | /a/b/c/      | Leading '{/} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 0      | insert before | ///a/b/c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 0      | append after  | //a//b/c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 2      | insert before | //a/b//c     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 2      | append after  | //a/b/c/     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 9      | insert before | //a/b/c/     | Leading '{U} a'                                                                             |
| //a/b/c  | Linux   | {E}     | 9      | append after  | //a/b/c/     | Leading '{U} a'                                                                             |
| (empty)  | Linux   | {E}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {E}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {E}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (empty)  | Linux   | {E}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{E}' turns into '{.}'                                           |
| (null)   | Linux   | {E}     | 0      | insert before | /.           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {E}     | 0      | append after  | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {E}     | 9      | insert before | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| (null)   | Linux   | {E}     | 9      | append after  | ./           | Leading '{E}' - Notice how '{N}' turns into '{.}'                                           |
| .        | Linux   | {E}     | 0      | insert before | /.           | Leading '.'                                                                                 |
| .        | Linux   | {E}     | 0      | append after  | ./           | Leading '.'                                                                                 |
| .        | Linux   | {E}     | 9      | insert before | ./           | Leading '.'                                                                                 |
| .        | Linux   | {E}     | 9      | append after  | ./           | Leading '.'                                                                                 |
| ..       | Linux   | {E}     | 0      | insert before | /..          | Leading '..'                                                                                |
| ..       | Linux   | {E}     | 0      | append after  | ../          | Leading '..'                                                                                |
| ..       | Linux   | {E}     | 9      | insert before | ../          | Leading '..'                                                                                |
| ..       | Linux   | {E}     | 9      | append after  | ../          | Leading '..'                                                                                |
