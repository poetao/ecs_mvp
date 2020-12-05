# git简略教程

## 与svn的差别

> * 可以随时提交，本地的提交不会影响到他人
> * 可以随意建立分支，本地的分支不会影响到他人
> * 可以随意改写历史，能够无所顾忌地提交
> * 可以在提交前先将修改放入暂存区，隔离其它修改
> * 可以让团队成员协同工作

git的诸多特性，使它成为编辑器的延伸，成为开发工作流非常重要的一部分，它不再是
单纯的版本控制系统，还是不可或缺的开发工具。

## 学习流程

遗忘svn的所有概念，然后了解暂存区、rebase、远程仓库等概念，在大致了解git的命令
后建立本地的版本库动手实践。命令数量较多，不需要全都学会，在能走通基本流程后每
隔一段时间学一点。本文仅对常用的命令进行简略的说明，如果需要更详细的说明或参数
，请使用git help查看git的文档，例如git help cherry-pick。

依开发流程，可分为几个学习阶段：
> * 查看修改，撤销修改，提交版本，查看版本
> * 分支合并，解决冲突
> * 远程仓库
> * 改写历史

git自学难度相当高，如果不想耗费大量的时间，在遇上不懂的概念时就不耻下问吧。

git是开发工作流的一部分，实际使用中会高频操作git的命令，图形界面的操作效率太低
，不可能跟得上编码与调试所需的操作频率。事实上，寻找图形界面工具的开发者最后都
会回归到命令行的版本。

## 概念

### 暂存区

在提交之前，需要将修改移入暂存区，然后才能将暂区存的修改提交到版本库。
本地的修改以红字显示，暂存区的修改以绿字显示。
本文的cache即指暂存区。

### HEAD

HEAD表示当前的版本库，一般情况下它会和某个分支一致。
在执行一些特殊操作的情况下，HEAD会与任何分支失去关联，称为detached HEAD。如果
在这种情况下提交修改，然后切换到别的分支，那么这些提交将会丢失，处于detached
HEAD时请特别注意！

### ~

git用~符号表示往前1个版本，用~3则表示往前数3个版本，因此，HEAD~5表示当前版本往
前数5个版本，8a96~2表示8a96这个版本往前数2个版本。

### 远程仓库

git是分布式的版本管理系统，它有远程仓库的概念。clone一个远程仓库后，就会在本地
出现一个远程仓库的镜像，远程仓库的分支称为远程分支。

提交版本后，这些版本将记录在本地分支，本地分支的修改要push到远程分支，这样其他
人才能看到本地分支的这些修改。

要同步他人的修改，需要先fetch远程仓库的数据，然后再从远程分支同步到本地分支。

## 常用命令

git clone git://xxx
克隆地址为git://xxx的版本库

git init
创建本地版本库

git st 
查看当前状态

### 将修改移入cache

git add .gitignore
将特定文件加入cache

git add -- *.json
将特定文件加入cache

git add -A
将所有修改（包括新增的文件和删除的文件）加入cache

git add -u
将版本库相关的修改（包括删除的文件，不包括新增的文件）加入cache

git add -i
使用交互模式将修改加入cache

git add -p
使用交互模式以补丁的形式将修改加入cache

### 将修改移出cache

git reset
取消cache的修改

git reset *.DS_*
取消cache的特定修改

### 提交

git ci
将cache的修改提交到版本库。
自动打开文本编辑器等待输入版本说明，写完版本说明关闭编辑器即可完成提交。
编辑器会同时列出详细的修改记录。

git ci -m "x"
以x为版本说明将cache的修改提交到版本库

git ci -am "x"
以x为版本说明将cache和工作目录的修改提交到版本库

git ci --amend
以cache的修改修正最后一个版本

git ci -a --amend
以本地修改和cache的修改修正最后一个版本

### 版本记录

git show 
以补丁的形式查看最近一个版本

git show --stat
以文件列表的形式查看最近一个版本

git show 8c141b --stat
以文件列表的形式查看特定版本

git log
列出所有版本

git log -3
列出最近3个版本

git log -- **/Module/Map*
列出涉及特定文件的版本

git log --oneline
以简略的格式列出所有版本

git log -p
以补丁的形式列出所有版本

git log master 
列出master分支的所有版本

### 查看差异

git di
以补丁的形式查看本地修改

git di -- *.cs
以补丁的形式查看特定文件的本地修改

git di --cached
以补丁的形式查看cache的修改

git di 64bc21
以补丁的形式比较特定的版本

git di 64bc21 --stat
以文件列表的形式比较特定的版本

git di HEAD~2
以补丁的形式比较前两个版本

git di dev
以补丁的形式比较dev分支

git di dev --stat
以文件列表的形式比较dev分支

git dt
使用比较工具查看本地修改

git dt --cached
使用比较工具查看cached的修改

git dt HEAD~
使用比较工具比较前一个版本

### 放弃本地修改

git co -- src/*.cs
放弃特定文件的本地修改

git co -f
放弃本地修改和cahce的修改，即恢复到和版本库一模一样的状态

git co -p
使用交互模式以补丁的形式放弃本地修改

### 清理

git clean -f
清理没有在版本库里的文件

git clean -f -- *.DS*
清理没有在版本库里的特定文件

git clean -fd
清理没有在版本库里的文件和目录

git clean -fd -- *View*
清理没有在版本库里的特定文件和目录

git clean -fdn -- *View*
列出将会被清理的没有在版本库里的特定文件和目录

git clean -fdx
清理没有在版本库里的文件和目录，包括忽略的文件和目录

### 抹除版本记录

git revert HEAD
撤销最近一个版本

git reset HEAD~
抹除最近1个版本，将版本内容作为本地修改

git reset --soft HEAD~
抹除最近1个版本，将修改内容放到cache

git reset --hard HEAD~4
抹除最近4个版本，并放弃本地和cache的修改

git reset --hard origin/master 
将当前分支的所有版本强制设置为origin/master，并放弃本地和cache的修改

### 分支操作

git br dev
根据当前分支创建名为dev的分支

git br -d xxx
删除名为xxx的分支

git br -m xxx
将当前分支重命名为xxx

git co master 
切换到master分支

git co -b temp
根据当前分支创建名为temp的分支，并切换到该分支
如果已存在名为temp的分支，则操作失败

git co -B temp
根据当前分支创建名为temp的分支，并切换到该分支
如果已存在名为temp的分支，则强制覆盖该分支

git rebase master
合并master分支到当前分支。
将当前分支与master分支的公共版本之后的所有版本应用在master分支的最新版本之后。

git br -vv
列出所有本地分支

git br -a
列出所有分支，包括远程仓库的分支

### 冲突合并

git mergetool
逐个合并所有冲突的文件

git mergetool -- **/Module/World*
合并特定的文件

### 远程仓库

git remote -v
查看所有远程仓库

git remote add xxx git://xxx
添加名为xxx地址为git://xxx的远程仓库

git br -u origin/experiment 
将当前分支关联到远程分支

git fetch
将当前分支关联的远程仓库同步到最新

git fetch -p
将当前分支关联的远程仓库同步到最新，并移除已被删除的远程分支

git fetch env
将当名为env的远程仓库同步到最新

git rebase
将当前分支关联的远程分支同步到当前分支

git push 
推送当前分支到远程仓库

git push -f
慎用！搞不清楚状况千万别用此命令！
强制推送当前分支到远程仓库，覆盖远程仓库的修改

git push origin dev
推送dev分支到远程仓库，如果远程仓库不存在dev分支即创建dev分支

git push -u origin dev
推送dev分支到远程仓库，如果远程仓库不存在dev分支即创建dev分支，并关联当前分支
到这个远程分支

git push origin master:dev
推送master分支到远程仓库的dev分支，如果远程仓库不存在dev分支即创建dev分支

git push origin HEAD:dev
推送当前分支到远程仓库的dev分支，如果远程仓库不存在dev分支即创建dev分支

git push origin :dev
删除名为dev的远程分支

### 其它

git rebase -i HEAD~4
使用交互模式修改最近4个版本

git cherry-pick 8a96
将特定版本应用到当前分支

git cherry-pick 8a96..63d7
将8a96之后到63d7的版本应用到当前分支

## 最佳实践

### 频繁提交

git能够合并和修改版本，因此可以无所顾忌的频繁提交，由此可以带来几个好处：

> * 人类能够关注信息量存在上限，及时将确认的修改存入版本库，避免分散注意力，使
    精力能够集中在当前的任务上
> * 及时将确认的修改存入版本库，能够避免以后被不小心破坏
> * 频繁提交减少了每次操作的文件数量，有效规避命令行接口的局限性

结论就是，步子迈太大，容易扯着蛋。

### 构筑开发环境

使用分支和cherry-pick，可以迅速构建自己的专属开发环境，提高开发和调试效率。例
如，将一些代码或配置的调试设置存放在特定的分支，调试时将这些版本cherry-pick到
当前分支，这样就不需要每次都手工修改，开发完成后再用rebase将这些修改移除。

### 使用ci --amend和cache加速调试

编码完成后，将修改移入cache，然后开始调试，有问题时可以随意加代码打log，确认问
题所在后再迅速撤销调试代码。

如果问题复杂，需要尝试性修正，可以直接将cache的修改提交，接着就可以将尝试性修
正存放在cache了。如果修改失败，则可以直接将环境恢复到最后一次提交。修改完成后
，用ci --amend修正最后一次提交即可。

### 使用add -p和co -p

以补丁的形式存档和撤销修改，能够迅速的review代码，存档正确的代码和撤销不需要的
代码。
