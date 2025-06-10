DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.Students')
AND col_name(parent_object_id, parent_column_id) = 'LastName';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[Students] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[Students] ALTER COLUMN [LastName] [nvarchar](100) NOT NULL
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202506070756082_SetLastNameMaxLengthAndRequiredInStudentsTable', N'StudentDiary.ApplicationDbContext')