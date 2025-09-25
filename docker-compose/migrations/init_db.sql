-- ============================================
-- Idempotent DB + Users setup
-- ============================================


DECLARE @DBName NVARCHAR(128) = '$(DB_NAME)';

-- Create database if it does not exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = @DBName)
BEGIN
    EXEC('CREATE DATABASE [' + @DBName + ']');
END
GO

USE [$(DB_NAME)];
GO

-- ===== Migrator User =====
IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name = '$(MIGRATOR_USER)')
BEGIN
    EXEC('CREATE LOGIN [' + '$(MIGRATOR_USER)' + '] WITH PASSWORD = ''' + '$(MIGRATOR_PASSWORD)' + '''');
END
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = '$(MIGRATOR_USER)')
BEGIN
    EXEC('CREATE USER [' + '$(MIGRATOR_USER)' + '] FOR LOGIN [' + '$(MIGRATOR_USER)' + ']');
END
GO

ALTER ROLE db_ddladmin ADD MEMBER [$(MIGRATOR_USER)];
ALTER ROLE db_datareader ADD MEMBER [$(MIGRATOR_USER)];
ALTER ROLE db_datawriter ADD MEMBER [$(MIGRATOR_USER)];
GO

-- Ensure database-level rights for migrations
GRANT CONTROL ON DATABASE::[$(DB_NAME)] TO [$(MIGRATOR_USER)];
GO

-- ===== Application User =====
IF NOT EXISTS (SELECT name FROM sys.sql_logins WHERE name = '$(APPLICATION_USER)')
BEGIN
    EXEC('CREATE LOGIN [' + '$(APPLICATION_USER)' + '] WITH PASSWORD = ''' + '$(APPLICATION_PASSWORD)' + '''');
END
GO

IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = '$(APPLICATION_USER)')
BEGIN
    EXEC('CREATE USER [' + '$(APPLICATION_USER)' + '] FOR LOGIN [' + '$(APPLICATION_USER)' + ']');
END
GO

ALTER ROLE db_datareader ADD MEMBER [$(APPLICATION_USER)];
ALTER ROLE db_datawriter ADD MEMBER [$(APPLICATION_USER)];
IF EXISTS (SELECT 1 FROM sys.database_role_members drm
           JOIN sys.database_principals r ON drm.role_principal_id = r.principal_id
           JOIN sys.database_principals u ON drm.member_principal_id = u.principal_id
           WHERE r.name = 'db_ddladmin' AND u.name = '$(APPLICATION_USER)')
BEGIN
    EXEC sp_droprolemember 'db_ddladmin', '$(APPLICATION_USER)';
END
GO