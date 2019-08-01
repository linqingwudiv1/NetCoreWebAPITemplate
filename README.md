# NetCoreWebAPITemplate

## support vs2019 version last

	NetCore2.2.5 

	新瓶装旧酒.一个极简的WebAPI/MVC开发环境,封装一些常用的类包.

## Description

	F.基本的DLL划分/单元测试(Unit)
	
	New.Some Common Helper 封装一些常用帮助类：Redis/Email/Singleton/Logger/加密等
	
	New.Basic Auth and Role System.基本的身份认证(Session) 示例.

	New.ID Generator Case 分布式ID生成示例. (wait)
	
	New.EF/Dapper 多数据库支持(Multiple Database Support)/EF Core数据库迁移 示例 Case.(Sql Server/Oracle/MySql/PostgreSQL/Sqlite )(条件预编译控制)
	
	New.Swagger Case集成示例.
	
	New.Autofac Case集成示例.
	
	New.Log4Net Case集成示例.

	New.Cors等一些常用配置示例 .

	New.Bogus Faker Data case模拟数据示例.()

	New.Script System Case脚本系统 示例.(wait)
	
	New.SingleR Case示例. (wait)
		
	New.ELK Case示例. Case (wait)

	New.quartz.net Case 示例 (wait)
	
	New.Other Same WebAPI Use Guide. e.g: excel and image handle Case.其他一些WebAPI的常用接口示例，如Excel操作和图片资源上传等

##工程说明目录说明:

	NetCoreWebAPITemplate--:

	-DLL--: 目录

	  BaseDLL: 业务和功能无关的
	  
	  BusinessDLL: 业务Logic
	  
	  DBAccessDLL: 业务逻辑无关的数据库访问层
	  
	  DTOModelDLL:DTO Model. 传输对象Model
	  
	  NetApplictionServiceDLL: Web基础层.Basic Logic Layer about Web
	  
	  ScriptDLL: Web运维脚本(C#/JavaScript)

	  SearchEngineDLL: 全文搜索引擎层(全文搜索和图片搜索)

	  LogDLL: ELK日志收集 Log Collection System

	--WebAPI--:

	  .Cache: Image/Excel File Cache Directory 图片/Excel等文件缓存存储位置
	  
	  .Config: Config File Directory.配置文件目录
	  
	  .LocalDB: 嵌入式DB目录(Sqlite/Sql Or Server Embedded)

##一些文档 Doc Reference：

	EF迁移文档 EF Migration Doc：https://docs.microsoft.com/zh-cn/ef/core/managing-schemas/migrations/
	
	数据迁移(EF Data Migration) 命令参考 Command reference : https://docs.microsoft.com/zh-cn/ef/core/miscellaneous/cli/powershell
	
	Note 慎用Update-Migration功能，尽量Script-Migration转SQL后手动更新/或手动修改数据库