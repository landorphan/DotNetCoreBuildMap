@Check-In
Feature: Posix Path Segments
	In order to develop a reliable Windows Path parser 
	As a member of the Landorphan Team
	I want to to be able to convert incoming paths into a more managable form

Scenario Outline: Posix Segmenter generates the following segments
	Given I have the following path: <Path>
         # NOTE: the segmentor does not produce a normalized form.
	 When I segment the Posix path
	 Then segment '0' should be: <Segment 0>
     And segment '1' should be: <Segment 1>
     And segment '2' should be: <Segment 2>
     And segment '3' should be: <Segment 3>
     And segment '4' should be: <Segment 4>
Examples:
# NOTE: Due to Gherkin parsing rules, \ needs to be escaped.  In order to avoid that necissity and
# make the following examples easier to read (/) will be used in place of the (\) character
#
# Path Segment Type Shorthand:
# {N} = NullSegment, {E} = EmptySegment, {R} = RootSegment, {D} = DeviceSegment, {/} = VolumelessRootSegment
# {V} = VolumeRelativeSegment, {U} = RemoteSegment, {G} = Segment, {.} = SelfSegmentk, {..} = ParentSegment
| Path                              | Segment 0      | Segment 1    | Segment 2    | Segment 3    | Segment 4    | Segment 5    |
| (null)                            | {N} (null)     | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| (empty)                           | {E} (empty)    | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:/                               | {G} C:         | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:/dir/file.txt                   | {G} C:         | {G} dir      | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   |
| C:/dir/file.txt/                  | {G} C:         | {G} dir      | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   |
| C:/dir                            | {G} C:         | {G} dir      | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:/dir/                           | {G} C:         | {G} dir      | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| C:/dir//file.txt                  | {G} C:         | {G} dir      | {E} (empty)  | {G} file.txt | {N} (null)   | {N} (null)   |
| C:./file.txt                      | {G} C:.        | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:./file.txt/                     | {G} C:.        | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| C:file.txt                        | {G} C:file.txt | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:file.txt/                       | {G} C:file.txt | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:dir                             | {G} C:dir      | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:dir/                            | {G} C:dir      | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:dir/file.txt                    | {G} C:dir      | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:dir/file.txt/                   | {G} C:dir      | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| //server/share                    | {U} server     | {G} share    | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| //server/share/                   | {U} server     | {G} share    | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| //server/file.txt                 | {U} server     | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| //server/file.txt/                | {U} server     | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| //server/share/dir/file.txt       | {U} server     | {G} share    | {G} dir      | {G} file.txt | {N} (null)   | {N} (null)   |
| //server/share/dir/file.txt/      | {U} server     | {G} share    | {G} dir      | {G} file.txt | {E} (empty)  | {N} (null)   |
| //?/C:/dir/file.txt               | {U} ?          | {G} C:       | {G} dir      | {G} file.txt | {N} (null)   | {N} (null)   |
| //?/C:/dir/file.txt/              | {U} ?          | {G} C:       | {G} dir      | {G} file.txt | {E} (empty)  | {N} (null)   |
| //?/UNC/server/share/dir/file.txt | {U} ?          | {G} UNC      | {G} server   | {G} share    | {G} dir      | {G} file.txt |
| //?/UNC/server//file.txt/         | {U} ?          | {G} UNC      | {G} server   | {E} (empty)  | {G} file.txt | {E} (empty)  |
| /dir/file.txt/                    | {R}            | {G} dir      | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   | 
| .                                 | {.} .          | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ./                                | {.} .          | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ./file.txt                        | {.} .          | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ./file.txt/                       | {.} .          | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| ./dir                             | {.} .          | {G} dir      | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ./dir/                            | {.} .          | {G} dir      | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| ./dir/file.txt                    | {.} .          | {G} dir      | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   |
| ./dir/file.txt/                   | {.} .          | {G} dir      | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   |
| ..                                | {..} ..        | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ../                               | {..} ..        | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ../dir/file.txt                   | {..} ..        | {G} dir      | {G} file.txt | {N} (null)   | {N} (null)   | {N} (null)   |
| ../dir/file.txt/                  | {..} ..        | {G} dir      | {G} file.txt | {E} (empty)  | {N} (null)   | {N} (null)   |
# Device paths should resolve to a device but the unnormalized segments will still be present				   
| CON                               | {G} CON        | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| C:/CON                            | {G} C:         | {G} CON      | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| ../CON                            | {..} ..        | {G} CON      | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| /dir/CON                          | {R}            | {G} dir      | {G} CON      | {N} (null)   | {N} (null)   | {N} (null)   | 
# A Byproduct of the parser means the following will be accepted as a legitimate source                                   
| UNC:server                         | {U} server    | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| UNC:server/                        | {U} server    | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| UNC:server/share                   | {U} server    | {G} share    | {N} (null)   | {N} (null)   | {N} (null)   | {N} (null)   |
| UNC:server/share/                  | {U} server    | {G} share    | {E} (empty)  | {N} (null)   | {N} (null)   | {N} (null)   |
| UNC:server/share/dir               | {U} server    | {G} share    | {G} dir      | {N} (null)   | {N} (null)   | {N} (null)   |
| UNC:server/share/dir/              | {U} server    | {G} share    | {G} dir      | {E} (empty)  | {N} (null)   | {N} (null)   |
																											   