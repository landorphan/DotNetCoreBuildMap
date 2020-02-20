#!/usr/bin/env bash

function writeUsage() {
    echo "Usage:"
    echo "    find-project.sh -h                   Display this help message"
    echo "    find-project.sh -?                   Display this help message"
    echo "    find-project.sh -n {project-name}    Finds a project by project name"
    echo "    find-project.sh -s {solution}        Finds projects by Solution name"
    exit 0;
}

PRJ_FILE=""
SLN_FILE=""

while getopts ":hn:s:p:" opt; do
    case ${opt} in
        h )
            writeUsage
            ;;
        \? )
            writeUsage
            ;;
        n )
            PRJ_NAME=$OPTARG
            ;;
        s )
            SLN_FILE=$OPTARG
            ;;
        p )
            PRJ_PATH=$OPTARG
            ;;
    esac
done

if [ "$PRJ_NAME" == "" ]  && [ "$SLN_FILE" == "" ] && [ "$PRJ_PATH" == "" ]; then
    writeUsage
else
    INPT=$(cat /dev/stdin)

    #First find the map id for project and solution files.
    PRJ_NAME_ID=$(echo "$INPT" | grep ':.*>Name<' | sed -E -n 's/:(.*)>.*/\1/p')
    SLN_NAME_ID=$(echo "$INPT" | grep ':.*>Solution<' | sed -E -n 's/:(.*)>.*/\1/p')
    PRJ_PATH_ID=$(echo "$INPT" | grep ':.*>FilePath<' | sed -E -n 's/:(.*)>.*/\1/p')
    PRJ_NAME_FILES=""
    PRJ_PATH_FILES=""
    SLN_NAME_FILES=""

    BASE_MAP=$(echo "$INPT" | grep "|0>")
    HEADER=$(echo "$INPT" | grep "^:")

    if [ "$PRJ_NAME" == '' ]; then
        PRJ_NAME_FILES=$(echo "$BASE_MAP")
    else
        PRJ_NAME_FILES=$(echo "$BASE_MAP" | grep "|$PRJ_NAME_ID>$PRJ_NAME<$PRJ_NAME_ID|")
    fi
    if [ "$PRJ_PATH" == "" ]; then
        PRJ_PATH_FILES=$(echo "$PRJ_NAME_FILES")
    else
        PRJ_PATH_FILES=$(echo "$PRJ_NAME_FILES" | grep "|$PRJ_PATH_ID>.*$PRJ_PATH<$PRJ_PATH_ID|")
    fi
    if [ "$SLN_FILE" == "" ]; then
        SLN_NAME_FILES=$(echo "$PRJ_PATH_FILES")
    else
        SLN_NAME_FILES=$(echo "$PRJ_PATH_FILES" | grep "|$SLN_NAME_ID>$SLN_FILE<$SLN_NAME_ID|")
    fi
    echo "$HEADER"
    echo "$SLN_NAME_FILES"
fi 
