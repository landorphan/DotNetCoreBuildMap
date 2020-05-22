#!/usr/bin/env bash

function writeUsage() {
    echo "Usage:"
    echo "    extract-project-parts.sh -h                   Display this help message"
    echo "    extract-project-parts.sh -?                   Display this help message"
    echo "    extract-project-parts.sh -p {parts}           Comma separated parts list"
    exit 0;
}

while getopts ":hn:s:p:" opt; do
    case ${opt} in
        h )
            writeUsage
            ;;
        \? )
            writeUsage
            ;;
        p )
            X_PARTS=$OPTARG
            ;;
    esac
done

INPT=$(cat /dev/stdin)
BASE_MAP=$(echo "$INPT" | grep "|0>")

function findNumbers() {
    IFS=',' read -ra ADDR <<< "$X_PARTS"
    for i in "${ADDR[@]}"; do
        # echo "$INPT" | grep ":.*>$i<" | sed -E -n 's/:(.*)>.*/\1/p'
        NBR=$(echo "$INPT" | grep ":.*>$i<" | sed -E -n 's/:(.*)>.*/\1/p')
        # \|3>([^<]*)<3
        PTRN="\|$NBR>([^<]*)<$NBR"
        echo "$PTRN"
    done
}

function findBackRefs() {
    IFS=',' read -ra ADDR <<< "$X_PARTS"
    c=0
    for i in "${ADDR[@]}"; do
        let "c++"
        echo "$c"
    done
}

# findNumbers
NBRS=($(findNumbers | sort))
PTRN=$(echo $(IFS=',' ; echo "${NBRS[*]}") | sed -E 's/,/.*/g')
BRFS=($(findBackRefs))
BRPT=$(echo $(IFS="," ; echo "${BRFS[*]}") | sed -E 's/,/\|\\/g')
BRPT="\\$BRPT"
# echo $BRPT
# echo $PTRN
# echo "$BASE_MAP" | sed -n -E \"s/.*$PTRN/$BRPT/p\"

# echo "sed -n -E \"s/.*$PTRN/$BRPT/p\""
echo "$BASE_MAP" | sed -n -E "s/.*$PTRN.*/$BRPT/p"

# for i in "${NBRS[@]}"; do
#     PTRN="$i"
# done

# echo $PTRN
NBR=""
PTRN=""
CTR=0
