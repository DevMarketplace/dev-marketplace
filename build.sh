function get_help {
    echo "Usage: build -n -d"
    echo "-n : skip initialization <optional>"
    echo "-d : skip database configuration <optional>"
}

function main {
    no_init=false;
    no_db_config = false;

    while getopts "nd" OPTION
    do
    case $OPTION in
        n)
            no_init=true;            
            ;;
        d)
            no_db_config=true;
            ;;
        ?)
            get_help
            exit
            ;;
        esac
    done

    if()
    fi
}