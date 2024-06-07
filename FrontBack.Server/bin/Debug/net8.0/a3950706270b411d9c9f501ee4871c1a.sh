function list_child_processes () {
    local ppid=$1;
    local current_children=$(pgrep -P $ppid);
    local local_child;
    if [ $? -eq 0 ];
    then
        for current_child in $current_children
        do
          local_child=$current_child;
          list_child_processes $local_child;
          echo $local_child;
        done;
    else
      return 0;
    fi;
}

ps 29550;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 29550 > /dev/null;
done;

for child in $(list_child_processes 29558);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/fivvif/Downloads/oopProject/FrontBack.Server/bin/Debug/net8.0/a3950706270b411d9c9f501ee4871c1a.sh;
