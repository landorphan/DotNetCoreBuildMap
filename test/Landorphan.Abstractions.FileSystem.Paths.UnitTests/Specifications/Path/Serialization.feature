@Check-In
Feature: Serialization
	In order to persist and recover paths
	As a developer
	I want to to be able to serialize/deserialize paths to Json.

Scenario Outline: I can serialize to and from various forms
	Given I have the following path: <Path>
	  And I'm running on the following Operating System: <OS>
	  And I parse the path 
	 When I serialize the path to <Serializer> as <Serialized Form>
	 Then the following should be the serialized form: <Serialized Value>
Examples:
| Path            | OS      | Serializer | Serialized Form     | Serialized Value                    |
| C:/dir/file.txt | Windows | Json       | Simple              | "C:`dir`file.txt"                   |
| C:/dir/file.txt | Windows | Json       | PathSegmentNotation | "[PSN:WIN]/{R}C/{G}dir/{G}file.txt" |
#| C:/dir/file.txt | Windows | Yaml       | Simple              | "C:`dir`file.txt"                   |
#| C:/dir/file.txt | Windows | Yaml       | PathSegmentNotation | "[PSN:WIN]/{R}C/{G}dir/{G}file.txt" |