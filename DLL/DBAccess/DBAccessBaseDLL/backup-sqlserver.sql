/*****************



Note 删除表与存储过程并重新创建..
Note 

************/


IF EXISTS(SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[TableIDCounter]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	DROP TABLE [dbo].[TableIDCounter]
END
GO

/*** Table **/
CREATE TABLE [dbo].[TableIDCounter](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Tag] [nchar](64) NOT NULL,
 CONSTRAINT [PK_TableIDCounter] PRIMARY KEY CLUSTERED 
(
	[Id] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Procedure ******/
IF EXISTS(SELECT * FROM [dbo].[sysobjects] WHERE id = object_id(N'[dbo].[PD_GenerateID]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE [dbo].[PD_GenerateID]
END
GO

CREATE PROCEDURE [dbo].[PD_GenerateID] 
	@TableTag nchar(64)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [TableIDCounter]
	(Tag) Values(@TableTag);

	SELECT SCOPE_IDENTITY() as bigint;
END
GO