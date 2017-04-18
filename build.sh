function get_help() 
{
    echo "Usage: build -n -d"
    echo "-n : skip initialization <optional>"
    echo "-d : skip database configuration <optional>"
}

function exists() 
{
    command -v "$1" >/dev/null 2>&1
}

function sub_init() 
{
    if exists cp
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
        apt-get install jq
    fi

    if exists node 
    then
        echo "Node is installed"
    else
        echo "Node is not installed. Installing NodeJs..."
        apt-get install node
    fi
}

function sub_db_config() 
{
    conn_str=$2; 
    pushd $1;
        cat appsettings.json | jq 'ConnectionStrings.Default=$2' appsettings.json
    popd;
}

function main() 
{
    echo "Main"
}

init=true;
db_config=true;

while getopts "nd" OPTION
do
case $OPTION in
    n)
        init=false;            
        ;;
    d)
        echo "D passed"
        db_config=false;
        ;;
    ?)
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
    echo "Please set a connection string to an empty or existing database"
    read connection_string
    sub_db_config "src/RestServices/" $connection_string
    sub_db_config "src/UI/" $connection_string
fi

main