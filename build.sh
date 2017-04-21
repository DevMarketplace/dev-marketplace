#!/bin/sh

get_help()
{
    echo "Usage: build -n -d"
    echo "-n : skip initialization <optional>"
    echo "-d : skip database configuration <optional>"
}

exists()
{
    command -v "$1" >/dev/null 2>&1
}

sub_init()
{
    if exists dotnet
    then
        echo "dotnet core is installed on your machine"
    else
        echo "The .Net Core SDK 1.0.3 is not installed on your machine."
        echo "Please get it from here: https://go.microsoft.com/fwlink/?linkid=843448 "

        exit
    fi

    if exists jq 
    then
        echo "jq is installed"
    else
        echo "The jq JSON parser is not installed. Installing jq..."
        sudo apt-get install jq
    fi

    if exists nodejs 
    then
        echo "NodeJS is installed"
    else
        echo "NodeJS is not installed. Installing NodeJs..."
        sudo apt-get install nodejs

        current_location=`pwd`
        cd /usr/bin
        sudo ln -s nodejs node
        cd $current_location
    fi

    if exists npm
    then
        echo "npm is installed"
    else 
        echo "npm is not installed. Installing npm..."
        sudo apt-get install npm
    fi

    if exists gulp
    then
        echo "gulp is installed"
    else
        echo "gulp is not installed. Installing gulp..."
        sudo npm install --global gulp
    fi
}

sub_db_config()
{
    conn_str=$2; 
    echo "switching dir to $1"
    old_folder=`pwd`
    cd $1

    encoding=`file --mime-encoding -b appsettings.json`
    case $encoding in
        "utf-16le")
            encoding="utf-16"
            ;;
        "utf-16be")
            encoding="utf-16"
            ;;
    esac

    contents=`cat appsettings.json | iconv -f $encoding -t ascii`

    echo $contents | jq ".ConnectionStrings.Default=\"$2\"" | iconv -f ascii -t $encoding > appsettings.json
        
    cd $old_folder
    echo "switching back to parent dir"
}

build_project()
{
    folder=$1
    current_location=`pwd`
    cd $folder
        echo "Restoring nuget packages"
        dotnet restore

        echo "Building project $2"
        dotnet build

        xterm -e dotnet run &
    cd $current_location    
}

init=true;
db_config=true;

while getopts "ndh" OPTION
do
case $OPTION in
    n)
        init=false;            
        ;;
    d)
        db_config=false;
        ;;
    h)
        get_help
        exit
        ;;
    esac
done

if $init
then
    sub_init
fi

if $db_config
then
    IFS= read -r -p "Please set a connection string to an empty or existing database: " connection_string
    echo "The connection string is: $connection_string "
    sub_db_config "src/RestServices/" "$connection_string"
    sub_db_config "src/UI/" "$connection_string"
fi

build_project "src/RestServices" "RestServices"

current=`pwd`

cd "src/UI"
    npm install
    gulp -b "`pwd`" --color --gulpfile gulpfile.js copy-all
    gulp -b "`pwd`" --color --gulpfile gulpfile.js sass
cd $current

build_project "src/UI" "Developer Marketplace"

echo "All Done!"

cat << "EOF"
 __________________________________________________
|          _____________________________           |
| [1] [2]  _____________________________ [_][_][_] |
| [3] [4]  [_][_][_] [_][_][_][_] [_][_] [_][_][_] |
| [5] [6]  [][][][][][][][][][][][][][_] [1][2][3] |
| [7] [8]  [_][][][][][][][][][][][][][] [4][5][6] |
| [9][10]  [__][][][][][][][][][][][][_] [7][8][9] |
| [11][12] [___][][][][][][][][][][][__] [__][0][] |
|          [_][______________][_]                  |
|__________________________________________________|

EOF