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

ps 30203;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 30203 > /dev/null;
done;

for child in $(list_child_processes 30205);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/fivvif/Downloads/oopProject/FrontBack.Server/bin/Debug/net8.0/8b58da9994684640888c44397f0d0d93.sh;
