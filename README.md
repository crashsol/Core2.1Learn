# Core2.1Learn
Core2.1新增特性学习

---
## CentOS Systemd添加自定义系统服务设置自定义开机启动
- 服务权限
> systemd有系统和用户区分；系统（/user/lib/systemd/system/）、用户（/etc/lib/systemd/user/）.一般系统管理员手工创建的单元文件建议存放在/etc/systemd/system/目录下面

- 创建服务文件 vi /etc/systemd/system/hellomvc.service,并添加一下内容
```
[Unit]
Description=Example .NET Web API App running on Centos

[Service]
WorkingDirectory=/home/Publish/helloworld
ExecStart=/usr/bin/dotnet /home/Publish/helloworld/helloworld.dll
Restart=always
RestartSec=10  # Restart service after 10 seconds if dotnet service crashes
SyslogIdentifier=dotnet-example
User=root
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```
- 文件内容说明
```
[Unit]
Description : 服务的简单描述
Documentation ： 服务文档
Before、After:定义启动顺序。Before=xxx.service,代表本服务在xxx.service启动之前启动。After=xxx.service,代表本服务在xxx.service之后启动。
Requires：这个单元启动了，它需要的单元也会被启动；它需要的单元被停止了，这个单元也停止了。
Wants：推荐使用。这个单元启动了，它需要的单元也会被启动；它需要的单元被停止了，对本单元没有影响。

[Service]
Type=simple（默认值）：systemd认为该服务将立即启动。服务进程不会fork。如果该服务要启动其他服务，不要使用此类型启动，除非该服务是socket激活型。
Type=forking：systemd认为当该服务进程fork，且父进程退出后服务启动成功。对于常规的守护进程（daemon），除非你确定此启动方式无法满足需求，使用此类型启动即可。使用此启动类型应同时指定 PIDFile=，以便systemd能够跟踪服务的主进程。
Type=oneshot：这一选项适用于只执行一项任务、随后立即退出的服务。可能需要同时设置 RemainAfterExit=yes 使得 systemd 在服务进程退出之后仍然认为服务处于激活状态。
Type=notify：与 Type=simple 相同，但约定服务会在就绪后向 systemd 发送一个信号。这一通知的实现由 libsystemd-daemon.so 提供。
Type=dbus：若以此方式启动，当指定的 BusName 出现在DBus系统总线上时，systemd认为服务就绪。
Type=idle: systemd会等待所有任务(Jobs)处理完成后，才开始执行idle类型的单元。除此之外，其他行为和Type=simple 类似。
PIDFile：pid文件路径
ExecStart：指定启动单元的命令或者脚本，ExecStartPre和ExecStartPost节指定在ExecStart之前或者之后用户自定义执行的脚本。Type=oneshot允许指定多个希望顺序执行的用户自定义命令。
ExecReload：指定单元停止时执行的命令或者脚本。
ExecStop：指定单元停止时执行的命令或者脚本。
PrivateTmp：True表示给服务分配独立的临时空间
Restart：这个选项如果被允许，服务重启的时候进程会退出，会通过systemctl命令执行清除并重启的操作。
RemainAfterExit：如果设置这个选择为真，服务会被认为是在激活状态，即使所以的进程已经退出，默认的值为假，这个选项只有在Type=oneshot时需要被配置。
[Install]
Alias：为单元提供一个空间分离的附加名字。
RequiredBy：单元被允许运行需要的一系列依赖单元，RequiredBy列表从Require获得依赖信息。
WantBy：单元被允许运行需要的弱依赖性单元，Wantby从Want列表获得依赖信息。
Also：指出和单元一起安装或者被协助的单元。
DefaultInstance：实例单元的限制，这个选项指定如果单元被允许运行默认的实例
```
- 重载服务(实现开机自启动)
> 使用下面命令会在/etc/systemd/system/multi-user.target.wants/目录下新建一个/usr/lib/systemd/system/nginx.service 文件的链接。
```
systemctl enable hellomvc.service
```
- 操作服务
```
#启动服务
systemctl start hellomvc.service
#停止服务
systemctl stop hellomvc.service
#重启
systemctl reload hellomvc.service
```
- [Systemd 入门教程：命令篇(阮一峰)](http://www.ruanyifeng.com/blog/2016/03/systemd-tutorial-commands.html)