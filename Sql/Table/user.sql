If(Object_Id('User') Is Not Null)
Begin
	Drop Table [User]
End
GO

Create  Table DBO.[User]
(
	 Id					Int  Identity(1, 1)	Not Null
	,Username			Nvarchar(100)		
	,PasswordHash		VARBINARY(max)		
	,PasswordSalt		VARBINARY(max)
	,UserRole			Nvarchar(50)
	,CreatedBy			Int
	,CreatedDate		DateTime
	,ModifiedDate		DateTime
	,ModifiedBy			Int
)
GO


Alter Table dbo.[User]
Add Constraint PK_User Primary Key (Id)

Alter Table dbo.[User]
Add Constraint DF_User_CreatedDate
Default GetDate() FOR CreatedDate
Go

Alter Table dbo.[User]
Add Constraint DF_User_ModifiedDate
Default GetDate() FOR ModifiedDate
Go

