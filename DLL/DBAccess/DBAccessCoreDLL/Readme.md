# 数据库预编译宏:
# define Q_SqlServerDB
# define Q_OracleDB
# define Q_MySqlDB
# define Q_PostgreSQLDB
# define Q_SqliteDB
# define Q_MemoryDB

修改方式：

DBAccessDLL->右键(right-click)->属性(Property)->生成()->常规()->条件编译和符号->{{写入使用数据库，如-> Q_SqliteDB}}

慎用Update-Migration功能，尽量Script-Migration转SQL后手动更新/或手动修改数据库

数据迁移(Migration) 命令参考:

	

采用其他项目的配置进行初始化

#migrations list

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ migrations list --context CoreContext


#migrations add

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ migrations add Initial --context CoreContext


#migrations update
									 
dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ migrations update --context CoreContext

#database drop

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ database drop --context CoreContext --connection "Username=postgres;Password=1qaz@WSX;Host=192.168.1.172;Port=5432;Database=QCoreDB;"


#database update 

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ database update --context CoreContext --connection "Username=postgres;Password=1qaz@WSX;Host=192.168.1.172;Port=5432;Database=QCoreDB;"

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ database update test --context CoreContext --connection "Username=postgres;Password=1qaz@WSX;Host=192.168.1.172;Port=5432;Database=QCoreDB;"


#migration script


#database script 

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ migrations script test --context CoreContext  -o "D:/js.sql" 
#drop data : 

dotnet ef --startup-project ../../../Console/EFCoreMigrationConsole/ database drop --context CoreContext

#
--connection "Username=postgres;Password=1qaz@WSX;Host=192.168.1.172;Port=5432;Database=QCoreDB;"

# 目录说明:

--Accesser		数据访问器-从一切可能的数据来源(NoSql,DB,Cache)读写数据
--EF			EF Core相关
  --Context		上下文
  --Entity		实体Model