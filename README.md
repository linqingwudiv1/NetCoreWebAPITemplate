# NetCoreWebAPITemplate

## support vs2019 version last

	NetCore2.2.5 

	新瓶装旧酒.一个极简的WebAPI/MVC开发环境,封装一些常用的类包.

## Description

	F.基本的DLL划分/单元测试
	
	New.封装一些常用帮助类：Redis/Email/Singleton/Logger/加密等
	
	New.基本的身份认证(Session) 示例.
	
	New.EF/Dapper 多数据库支持/EF Core数据库迁移 示例.(Sql Server/Oracle/MySql/PostgreSQL/Sqlite )(条件预编译控制)
	
	New.Swagger集成示例.
	
	New.Autofac集成示例.
	
	New.Log4Net集成示例.

	New.Cors等一些常用配置示例.

	New.Js脚本系统 示例.(wait)
	
	New.SingleR 示例. (wait)
	
	New.分布式ID生成 示例. (wait)

	New.quartz.net 示例 (wait)
	
	New.其他一些WebAPI的常用接口示例，如Excel操作和图片资源上传等

##工程说明目录说明:
	NetCoreWebAPITemplate--:
	--DLL--: 目录

	  BaseDLL:基础层：业务和功能无关的
	  
	  BusinessDLL:业务Logic
	  
	  DBAccessDLL:业务逻辑无关的数据库访问层
	  
	  DTOModelDLL:传输对象Model
	  
	  NetApplictionServiceDLL:Web基础层
	  
	  ScriptDLL: Web运维脚本(JavaScript)

	--WebAPI--:

	  .Cache: 图片/Excel等文件缓存存储位置
	  
	  .Config:
	  
	  .LocalDB: 嵌入式DB目录


##一些文档：

	EF迁移Doc：https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/
	
	数据迁移(Migration) 命令参考: https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/powershell
	
	慎用Update-Migration功能，尽量Script-Migration转SQL后手动更新/或手动修改数据库