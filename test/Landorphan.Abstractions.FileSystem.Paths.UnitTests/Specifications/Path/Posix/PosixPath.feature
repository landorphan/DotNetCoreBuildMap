@Check-In
Feature: PosixPaths
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want to be able to parse paths on multiple platforms correctly

Scenario Outline: Posix Paths
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Linux
	 When I parse the path 
      And I evaluate the original form
	 Then I should receive a path object
	  And segment '0' should be: <Segment 0>
	  And segment '1' should be: <Segment 1>
	  And segment '2' should be: <Segment 2>
	  And segment '3' should be: <Segment 3>
	  And segment '4' should be: <Segment 4>
	  And segment '5' should be: <Segment 5>
	  And segment '6' should be: <Segment 6>
	  And the path should be anchored to <Anchor>
	  And the parse status should be <Status>
	  And the segment length should be <Length>
	  And the PathType should be Posix
	  And the normalization depth should be: <Norm Depth>
#	  And the psth's IsNoramlized property should be <Is Normalized> 
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
| Name               | Path                        | Length | Segment 0  | Segment 1             | Segment 2    | Segment 3    | Segment 4  | Segment 5  | Segment 6  | Anchor   | Status      | Norm Depth | Is Normalized |
# a null string can be parsed but will produce a null path (which is an illegal path)												 
| Null               | (null)                      | 1      | {.} .      | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 0          | false         |
# an empty string can be parsed but will produce an empty path (which is an illegal path)											 
| Empty              | (empty)                     | 1      | {.} .      | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 0          | false         |
# Volume Absolute and Volume Relative have no meaning in Posix																		 
| Volume Absolute    | c:                          | 1      | {G} c:     | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| Volume Relative    | C:/./file.txt               | 3      | {G} C:     | {.} .                 | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | false         |
| UNC                | //server/share/dir/file.txt | 4      | {U} server | {G} share             | {G} dir      | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 3          | true          |
| Long Volume Abs    | //?/C:/dir/file.txt         | 4      | {U} ?      | {G} C:                | {G} dir      | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 3          | true          |
| Self Relative      | ./dir/file.txt              | 3      | {.} .      | {G} dir               | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | false         |
| Parent Relative    | ../dir/file.txt             | 3      | {..} ..    | {G} dir               | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | -1         | false         |
# Empty segments will show up in the non-normalized for but will not be present in the normalized form								 
| Empty Abs Segment  | C:/dir//file.txt            | 4      | {G} C:     | {G} dir               | {E} (empty)  | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 3          | false         |
| Empty Rel Segment  | ./dir//file.txt             | 4      | {.} .      | {G} dir               | {E} (empty)  | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | false         |
| Relative           | dir/file.txt                | 2      | {G} dir    | {G} file.txt          | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | true          |
# Normalizaiton level does not increase once it is below zero																		 
| Neg Normal 2       | ../../-1/0/1                | 5      | {..} ..    | {..} ..               | {G} -1       | {G} 0        | {G} 1      | {N} (null) | {N} (null) | Relative | Legal       | -2         | false         |
| Neg Normal 1       | ./../-1/0/1                 | 5      | {.} .      | {..} ..               | {G} -1       | {G} 0        | {G} 1      | {N} (null) | {N} (null) | Relative | Legal       | -1         | false         |
## The following have absolutly no specail meaning in Posix paths																	 
| CON                | CON                         | 1      | {G} CON    | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| PRN                | PRN                         | 1      | {G} PRN    | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| AUX                | AUX                         | 1      | {G} AUX    | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| NUL                | NUL                         | 1      | {G} NUL    | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM1               | COM1                        | 1      | {G} COM1   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM2               | COM2                        | 1      | {G} COM2   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM3               | COM3                        | 1      | {G} COM3   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM4               | COM4                        | 1      | {G} COM4   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM5               | COM5                        | 1      | {G} COM5   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM6               | COM6                        | 1      | {G} COM6   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM7               | COM7                        | 1      | {G} COM7   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM8               | COM8                        | 1      | {G} COM8   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| COM9               | COM9                        | 1      | {G} COM9   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT1               | LPT1                        | 1      | {G} LPT1   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT2               | LPT2                        | 1      | {G} LPT2   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT3               | LPT3                        | 1      | {G} LPT3   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT4               | LPT4                        | 1      | {G} LPT4   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT5               | LPT5                        | 1      | {G} LPT5   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT6               | LPT6                        | 1      | {G} LPT6   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT7               | LPT7                        | 1      | {G} LPT7   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT8               | LPT8                        | 1      | {G} LPT8   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
| LPT9               | LPT9                        | 1      | {G} LPT9   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
## Long variants of the device paths are also have no special meaning																 																				
| Long CON           | //?/CON                     | 2      | {U} ?      | {G} CON               | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long PRN           | //?/PRN                     | 2      | {U} ?      | {G} PRN               | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long AUX           | //?/AUX                     | 2      | {U} ?      | {G} AUX               | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long NUL           | //?/NUL                     | 2      | {U} ?      | {G} NUL               | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM1          | //?/COM1                    | 2      | {U} ?      | {G} COM1              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM2          | //?/COM2                    | 2      | {U} ?      | {G} COM2              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM3          | //?/COM3                    | 2      | {U} ?      | {G} COM3              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM4          | //?/COM4                    | 2      | {U} ?      | {G} COM4              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM5          | //?/COM5                    | 2      | {U} ?      | {G} COM5              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM6          | //?/COM6                    | 2      | {U} ?      | {G} COM6              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM7          | //?/COM7                    | 2      | {U} ?      | {G} COM7              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM8          | //?/COM8                    | 2      | {U} ?      | {G} COM8              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long COM9          | //?/COM9                    | 2      | {U} ?      | {G} COM9              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT1          | //?/LPT1                    | 2      | {U} ?      | {G} LPT1              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT2          | //?/LPT2                    | 2      | {U} ?      | {G} LPT2              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT3          | //?/LPT3                    | 2      | {U} ?      | {G} LPT3              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT4          | //?/LPT4                    | 2      | {U} ?      | {G} LPT4              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT5          | //?/LPT5                    | 2      | {U} ?      | {G} LPT5              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT6          | //?/LPT6                    | 2      | {U} ?      | {G} LPT6              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT7          | //?/LPT7                    | 2      | {U} ?      | {G} LPT7              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT8          | //?/LPT8                    | 2      | {U} ?      | {G} LPT8              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
| Long LPT9          | //?/LPT9                    | 2      | {U} ?      | {G} LPT9              | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
# Per previous tests, device path's have no special meaning in Posix																 
| Rel CON            | .././CON                    | 3      | {..} ..    | {.} .                 | {G} CON      | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | -1         | false         |
# Per previous tests, device path's have no special meaning in Posix																 
| Abs Con            | C:/CON                      | 2      | {G} C:     | {G} CON               | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | true          |
# Per previous tests, device path's have no special meaning in Posix																 
| Volume CON         | CON:                        | 1      | {G} CON:   | {N} (null)            | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | true          |
# Per previous tests, device path's have no special meaning in Posix (NOTE: This is NOT Discouraged on Posix) 						 
| Discuraged Rel NUL | ./NUL.txt                   | 2      | {.} .      | {G} NUL.txt           | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | false         |
# Nonprintable charachters are legal but Discuraged 																				 
| Illegal Rel Astr   | ./start-%03-end.txt         | 2      | {.} .      | {G} start-%03-end.txt | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Discouraged | 1          | false         |
# Using an illegal character in a path is illegal																					 
| Illegal Rel Astr   | ./start-%00-end.txt         | 2      | {.} .      | {G} start-%00-end.txt | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
# Question mark is legal any place on Posix																							 
| Illegal Rel Ques   | ./foo?bar.txt               | 2      | {.} .      | {G} foo?bar.txt       | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | false         |
# Colon is legaln any place on Posix																								 
| Illegal Rel Colon  | ./foo:bar.txt               | 2      | {.} .      | {G} foo:bar.txt       | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | false         |
# Spaces at begining or end of a path is discurraged 																				 
| Space Beginning    | ./%20test.txt               | 2      | {.} .      | {G} %20test.txt       | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Discouraged | 1          | false         |
| Space Ending       | ./test.txt%20               | 2      | {.} .      | {G} test.txt%20       | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Discouraged | 1          | false         |
| Space Both         | ./%20t.txt%20               | 2      | {.} .      | {G} %20t.txt%20       | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Discouraged | 1          | false         |

## NOTE: There are no pipe test cases here (because Gherkin uses that for the table, we will have to test those in scenarios and not ountlines)


# TODO: Add this back latter it's too long for current test system
# TODO: This can be added back now!
#| Long UNC           | //?/UNC/server/share/dir/file.txt | 4      | {U} server  | {G} share             | {G} dir      | {G} file.txt | Absolute | Legal       |
