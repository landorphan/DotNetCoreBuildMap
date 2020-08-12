@Check-In
Feature: WindowsPaths
	In order to reliably interact with the file systems of multiple platforms
	As a developer
	I want to be able to parse paths on multiple platforms correctly

Scenario Outline: Drive Rooted Paths
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: Windows
	 When I parse the path 
     And I evaluate the original form
	 Then I should receive a path object
	  And segment '0' should be: <Root>
	  And segment '1' should be: <Segment 1>
	  And segment '2' should be: <Segment 2>
	  And segment '3' should be: <Segment 3>
	  And the path should be anchored to <Anchor>
	  And the parse status should be <Status>
	  And the segment length should be <Length>
	  And the PathType should be Windows
	  And the psth's IsNoramlized property should be <Is Normalized> 

# NOTE: Due to Gherkin parsing rules, \ needs to be escaped.  In order to avoid that necissity and
# make the following examples easier to read (`) will be used in place of the (\) character
#
# NOTES:
# Per the spec, the following characters are illegal (anywere in the path)
# ILLEGAL CHARACTERS: Less Than (<), Greater Than (>), Double Quote ("), Pipe (|), Asterisk (*)
# 
# Per the spec, the following characters always represent a Path Separator regarless of location and can not be part of the path
# Foward Slash (/), Back Slash (\)
#
# Per the spec, the following characters are reserved and have special meaning.  They are only legal in or before the first segmant
# Colon (:) - Legal in first segment, Question Mark (?) - Legal only before the first segment (when using "long" sentax)
#
# Path Segment Type Shorthand:
# {N} = NullSegment, {E} = EmptySegment, {R} = RootSegment, {D} = DeviceSegment, {/} = VolumelessRootSegment
# {V} = VolumeRelativeSegment, {U} = UncSegment, {G} = Segment, {.} = SelfSegmentk, {..} = ParentSegment
Examples: 
| Name               | Path                              | Length | Root        | Segment 1       | Segment 2    | Segment 3    | Segment 4  | Segment 5  | Segment 6  | Anchor   | Status      | Norm Level | Is Normalized |
# a null string can be parsed but will produce a null path (which is an illegal path)																										
| Null               | (null)                            | 1      | {N} (null)  | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Illegal     | 1          | false         |
# an empty string can be parsed but will produce an empty path (which is an illegal path)										  															
| Empty              | (empty)                           | 1      | {E} (empty) | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Illegal     | 1          | false         |
| Volume Absolute    | C:`                               | 2      | {R} C:      | {E} (empty)     | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | false         |
| Volume Relative    | C:.`file.txt                      | 3      | {V} C:      | {.} .           | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 1          | false         |
| UNC                | ``server`share`dir`file.txt       | 4      | {U} server  | {G} share       | {G} dir      | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 3          | true          |
| Long Volume Abs    | ``?`C:`dir`file.txt               | 3      | {R} C:      | {G} dir         | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 2          | true          |
| Long UNC           | ``?`UNC`server`share`dir`file.txt | 4      | {U} server  | {G} share       | {G} dir      | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 3          | true          |
| Self Relative      | .`dir`file.txt                    | 3      | {.} .       | {G} dir         | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | false         |
| Parent Relative    | ..`dir`file.txt                   | 3      | {..} ..     | {G} dir         | {G} file.txt | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | -1         | false         |
# Empty segments will show up in the non-normalized for but will not be present in the normalized form							  															
| Empty Abs Segment  | C:`dir``file.txt                  | 4      | {R} C:      | {G} dir         | {E} (empty)  | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 3          | false         |
| Empty Rel Segment  | .`dir``file.txt                   | 4      | {.} .       | {G} dir         | {E} (empty)  | {G} file.txt | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | false         |
| Relative           | dir`file.txt                      | 2      | {G} dir     | {G} file.txt    | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Legal       | 2          | true          |
# Normalizaiton level does not increase once it is below zero																		 
| Neg Normal 2       | ../../-1/0/1						 | 5      | {..} ..     | {..} ..         | {G} -1       | {G} 0        | {G} 1      | {N} (null) | {N} (null) | Relative | Legal       | -2         | false         |																					  
| Neg Normal 1       | ./../-1/0/1						 | 5      | {.} .       | {..} ..         | {G} -1       | {G} 0        | {G} 1      | {N} (null) | {N} (null) | Relative | Legal       | -1         | false         |																						  

# The following represent reserved device paths and are legal (they open a stream to the device in question if it is present)	  															
| CON                | CON                               | 1      | {D} CON     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| PRN                | PRN                               | 1      | {D} PRN     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| AUX                | AUX                               | 1      | {D} AUX     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| NUL                | NUL                               | 1      | {D} NUL     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM1               | COM1                              | 1      | {D} COM1    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM2               | COM2                              | 1      | {D} COM2    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM3               | COM3                              | 1      | {D} COM3    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM4               | COM4                              | 1      | {D} COM4    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM5               | COM5                              | 1      | {D} COM5    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM6               | COM6                              | 1      | {D} COM6    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM7               | COM7                              | 1      | {D} COM7    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM8               | COM8                              | 1      | {D} COM8    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| COM9               | COM9                              | 1      | {D} COM9    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT1               | LPT1                              | 1      | {D} LPT1    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT2               | LPT2                              | 1      | {D} LPT2    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT3               | LPT3                              | 1      | {D} LPT3    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT4               | LPT4                              | 1      | {D} LPT4    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT5               | LPT5                              | 1      | {D} LPT5    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT6               | LPT6                              | 1      | {D} LPT6    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT7               | LPT7                              | 1      | {D} LPT7    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT8               | LPT8                              | 1      | {D} LPT8    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| LPT9               | LPT9                              | 1      | {D} LPT9    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
# Long variants of the device paths are also allowed (with the same behavior as above)											  																			  
| Long CON           | ``?`CON                           | 1      | {D} CON     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long PRN           | ``?`PRN                           | 1      | {D} PRN     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long AUX           | ``?`AUX                           | 1      | {D} AUX     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long NUL           | ``?`NUL                           | 1      | {D} NUL     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM1          | ``?`COM1                          | 1      | {D} COM1    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM2          | ``?`COM2                          | 1      | {D} COM2    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM3          | ``?`COM3                          | 1      | {D} COM3    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM4          | ``?`COM4                          | 1      | {D} COM4    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM5          | ``?`COM5                          | 1      | {D} COM5    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM6          | ``?`COM6                          | 1      | {D} COM6    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM7          | ``?`COM7                          | 1      | {D} COM7    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM8          | ``?`COM8                          | 1      | {D} COM8    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long COM9          | ``?`COM9                          | 1      | {D} COM9    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT1          | ``?`LPT1                          | 1      | {D} LPT1    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT2          | ``?`LPT2                          | 1      | {D} LPT2    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT3          | ``?`LPT3                          | 1      | {D} LPT3    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT4          | ``?`LPT4                          | 1      | {D} LPT4    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT5          | ``?`LPT5                          | 1      | {D} LPT5    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT6          | ``?`LPT6                          | 1      | {D} LPT6    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT7          | ``?`LPT7                          | 1      | {D} LPT7    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT8          | ``?`LPT8                          | 1      | {D} LPT8    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
| Long LPT9          | ``?`LPT9                          | 1      | {D} LPT9    | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
# Using a device path as a relitive path is actually legal (all device paths are absolute)										  															
# NOTE: Unlike all other paths, Device Paths only ever have one segment regardless of what was provided							  															
# Most other paths will keep unecissary components (example {E}) unless they are "explicitly" normalized to have those removed	  															
| Rel CON            | ..`.`CON                          | 3      | {..} ..     | {.} .           | {D} CON      | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | -1         | false         |
# Using a device path as an absolute path is actually legal (all device paths are absolute)										  															
| Abs Con            | C:`CON                            | 2      | {R} C:      | {D} CON         | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 1          | true          |
# Using a device path with a colon is in fact leagal																			  															
| Volume CON         | CON:                              | 1      | {D} CON     | {N} (null)      | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Absolute | Legal       | 0          | true          |
# Using a device path with an extention (as in a file name) is legal but highly discurouged (note this is a relative path because it is not a device path)		  							
| Discuraged Rel NUL | .`NUL.txt                         | 2      | {.} .       | {G} NUL.txt     | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Discouraged | 1          | false         |
# Using an illegal character in a path is illegal																																			
| Illegal Rel Astr   | .`foo*bar.txt                     | 2      | {.} .       | {G} foo*bar.txt | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
# After the long sentax, a question mark is illegal																																			
| Illegal Rel Ques   | .`foo?bar.txt                     | 2      | {.} .       | {G} foo?bar.txt | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
# After the volume sentax, a colon is illegal																																				
| Illegal Rel Colon  | .`foo:bar.txt                     | 2      | {.} .       | {G} foo:bar.txt | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
# Spaces end of a segment is an illegal path.																																				
| Space Ending       | .`test.txt%20                     | 2      | {.} .       | {G} test.txt%20 | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
| Space Both         | .`%20t.txt%20                     | 2      | {.} .       | {G} %20t.txt%20 | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
# period at end of segment is an illegal path.																																				
| Period Ending      | .`test.                           | 2      | {.} .       | {G} test.       | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Illegal     | 1          | false         |
# Space at begining of path is discuraged 																																					
| Space Beginning    | .`%20test.txt                     | 2      | {.} .       | {G} %20test.txt | {N} (null)   | {N} (null)   | {N} (null) | {N} (null) | {N} (null) | Relative | Discouraged | 1          | false         |
																																									  									    
# NOTE: There are no pipe test cases here (because Gherkin uses that for the table, we will have to test those in scenarios and not ountlines)

