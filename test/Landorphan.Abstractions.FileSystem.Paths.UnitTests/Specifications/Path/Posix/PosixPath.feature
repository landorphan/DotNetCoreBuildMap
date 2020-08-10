@Check-In
Feature: PosixPaths
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want to be able to parse paths on multiple platforms correctly

Scenario Outline: Drive Rooted Paths
	Given I have the following path: <Path>
	 When I parse the path as a Posix Path
     And I evaluate the original form
	 Then I should receive a path object
	  And segment '0' should be: <Root>
	  And segment '1' should be: <Segment 1>
	  And segment '2' should be: <Segment 2>
	  And segment '3' should be: <Segment 3>
	  And the path should be anchored to <Anchor>
	  And the parse status should be <Status>
	  And the segment length should be <Length>
	  And the PathType should be Posix
# NOTE: Due to Gherkin parsing rules, \ needs to be escaped.  In order to avoid that necissity and
# make the following examples easier to read (`) will be used in place of the (\) character
#
# NOTES:
# Per the spec, the following characters are illegal (anywere in the path)
# ILLEGAL CHARACTERS: 0x00
# 
# Per the spec, the following characters always represent a Path Separator regarless of location and can not be part of the path
# Foward Slash (/)
#
# Per the spec, the following characters are reserved and have special meaning: No such characters for Posix paths  
#
# Path Segment Type Shorthand:
# {N} = NullSegment, {E} = EmptySegment, {R} = RootSegment, {D} = DeviceSegment, {/} = VolumelessRootSegment
# {V} = VolumeRelativeSegment, {U} = UncSegment, {G} = Segment, {.} = SelfSegmentk, {..} = ParentSegment
Examples: 
| Name               | Path                              | Length | Root        | Segment 1             | Segment 2    | Segment 3    | Anchor   | Status      |
# a null string can be parsed but will produce a null path (which is an illegal path)
| Null               | (null)                            | 1      | {N} (null)  | {N} (null)            | {N} (null)   | {N} (null)   | Absolute | Illegal     |
# an empty string can be parsed but will produce an empty path (which is an illegal path)
| Empty              | (empty)                           | 1      | {E} (empty) | {N} (null)            | {N} (null)   | {N} (null)   | Absolute | Illegal     |
# Volume Absolute and Volume Relative have no meaning in Posix
| Volume Absolute    | c:                                | 1      | {G} c:      | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| Volume Relative    | C:/./file.txt                     | 3      | {G} C:      | {.} .                 | {G} file.txt | {N} (null)   | Relative | Legal       |
| UNC                | //server/share/dir/file.txt       | 4      | {U} server  | {G} share             | {G} dir      | {G} file.txt | Absolute | Legal       |
| Long Volume Abs    | //?/C:/dir/file.txt               | 4      | {U} ?       | {G} C:                | {G} dir      | {G} file.txt | Absolute | Legal       |
| Self Relative      | ./dir/file.txt                    | 3      | {.} .       | {G} dir               | {G} file.txt | {N} (null)   | Relative | Legal       |
| Parent Relative    | ../dir/file.txt                   | 3      | {..} ..     | {G} dir               | {G} file.txt | {N} (null)   | Relative | Legal       |
# Empty segments will show up in the non-normalized for but will not be present in the normalized form
| Empty Abs Segment  | C:/dir//file.txt                  | 4      | {G} C:      | {G} dir               | {E} (empty)  | {G} file.txt | Relative | Legal       |
| Empty Rel Segment  | ./dir//file.txt                   | 4      | {.} .       | {G} dir               | {E} (empty)  | {G} file.txt | Relative | Legal       |
| Relative           | dir/file.txt                      | 2      | {G} dir     | {G} file.txt          | {N} (null)   | {N} (null)   | Relative | Legal       |

## The following have absolutly no specail meaning in Posix paths
| CON                | CON                               | 1      | {G} CON     | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| PRN                | PRN                               | 1      | {G} PRN     | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| AUX                | AUX                               | 1      | {G} AUX     | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| NUL                | NUL                               | 1      | {G} NUL     | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM1               | COM1                              | 1      | {G} COM1    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM2               | COM2                              | 1      | {G} COM2    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM3               | COM3                              | 1      | {G} COM3    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM4               | COM4                              | 1      | {G} COM4    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM5               | COM5                              | 1      | {G} COM5    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM6               | COM6                              | 1      | {G} COM6    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM7               | COM7                              | 1      | {G} COM7    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM8               | COM8                              | 1      | {G} COM8    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| COM9               | COM9                              | 1      | {G} COM9    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT1               | LPT1                              | 1      | {G} LPT1    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT2               | LPT2                              | 1      | {G} LPT2    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT3               | LPT3                              | 1      | {G} LPT3    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT4               | LPT4                              | 1      | {G} LPT4    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT5               | LPT5                              | 1      | {G} LPT5    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT6               | LPT6                              | 1      | {G} LPT6    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT7               | LPT7                              | 1      | {G} LPT7    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT8               | LPT8                              | 1      | {G} LPT8    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
| LPT9               | LPT9                              | 1      | {G} LPT9    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
## Long variants of the device paths are also have no special meaning
| Long CON           | //?/CON                           | 2      | {U} ?       | {G} CON               | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long PRN           | //?/PRN                           | 2      | {U} ?       | {G} PRN               | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long AUX           | //?/AUX                           | 2      | {U} ?       | {G} AUX               | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long NUL           | //?/NUL                           | 2      | {U} ?       | {G} NUL               | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM1          | //?/COM1                          | 2      | {U} ?       | {G} COM1              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM2          | //?/COM2                          | 2      | {U} ?       | {G} COM2              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM3          | //?/COM3                          | 2      | {U} ?       | {G} COM3              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM4          | //?/COM4                          | 2      | {U} ?       | {G} COM4              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM5          | //?/COM5                          | 2      | {U} ?       | {G} COM5              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM6          | //?/COM6                          | 2      | {U} ?       | {G} COM6              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM7          | //?/COM7                          | 2      | {U} ?       | {G} COM7              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM8          | //?/COM8                          | 2      | {U} ?       | {G} COM8              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long COM9          | //?/COM9                          | 2      | {U} ?       | {G} COM9              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT1          | //?/LPT1                          | 2      | {U} ?       | {G} LPT1              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT2          | //?/LPT2                          | 2      | {U} ?       | {G} LPT2              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT3          | //?/LPT3                          | 2      | {U} ?       | {G} LPT3              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT4          | //?/LPT4                          | 2      | {U} ?       | {G} LPT4              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT5          | //?/LPT5                          | 2      | {U} ?       | {G} LPT5              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT6          | //?/LPT6                          | 2      | {U} ?       | {G} LPT6              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT7          | //?/LPT7                          | 2      | {U} ?       | {G} LPT7              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT8          | //?/LPT8                          | 2      | {U} ?       | {G} LPT8              | {N} (null)   | {N} (null)   | Absolute | Legal       |
| Long LPT9          | //?/LPT9                          | 2      | {U} ?       | {G} LPT9              | {N} (null)   | {N} (null)   | Absolute | Legal       |

# Per previous tests, device path's have no special meaning in Posix
| Rel CON            | .././CON                          | 3      | {..} ..     | {.} .                 | {G} CON      | {N} (null)   | Relative | Legal       |
# Per previous tests, device path's have no special meaning in Posix
| Abs Con            | C:/CON                            | 2      | {G} C:      | {G} CON               | {N} (null)   | {N} (null)   | Relative | Legal       |
# Per previous tests, device path's have no special meaning in Posix
| Volume CON         | CON:                              | 1      | {G} CON:    | {N} (null)            | {N} (null)   | {N} (null)   | Relative | Legal       |
# Per previous tests, device path's have no special meaning in Posix (NOTE: This is NOT Discouraged on Posix) 
| Discuraged Rel NUL | ./NUL.txt                         | 2      | {.} .       | {G} NUL.txt           | {N} (null)   | {N} (null)   | Relative | Legal       |
# Nonprintable charachters are legal but Discuraged 
| Illegal Rel Astr   | ./start-%03-end.txt               | 2      | {.} .       | {G} start-%03-end.txt | {N} (null)   | {N} (null)   | Relative | Discouraged |
# Using an illegal character in a path is illegal
| Illegal Rel Astr   | ./start-%00-end.txt               | 2      | {.} .       | {G} start-%00-end.txt | {N} (null)   | {N} (null)   | Relative | Illegal     |
# Question mark is legal any place on Posix
| Illegal Rel Ques   | ./foo?bar.txt                     | 2      | {.} .       | {G} foo?bar.txt       | {N} (null)   | {N} (null)   | Relative | Legal       |
# Colon is legaln any place on Posix
| Illegal Rel Colon  | ./foo:bar.txt                     | 2      | {.} .       | {G} foo:bar.txt       | {N} (null)   | {N} (null)   | Relative | Legal       |
## NOTE: There are no pipe test cases here (because Gherkin uses that for the table, we will have to test those in scenarios and not ountlines)


# TODO: Add this back latter it's too long for current test system
#| Long UNC           | //?/UNC/server/share/dir/file.txt | 4      | {U} server  | {G} share             | {G} dir      | {G} file.txt | Absolute | Legal       |
