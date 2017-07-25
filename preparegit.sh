git status | sed /"\/bin\/"/d | sed /"\/obj\/"/d | sed /"\.vs"/d | sed /"TestResults"/d | sed s/"modified:"/"git add "/g  | sed s/"deleted:"/"git rm -rf "/g > ~/run.sh

vi ~/run.sh
