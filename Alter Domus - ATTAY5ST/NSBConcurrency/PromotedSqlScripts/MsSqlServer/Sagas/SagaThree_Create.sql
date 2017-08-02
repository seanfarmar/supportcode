
/* TableNameVariable */

declare @tableName nvarchar(max) = @tablePrefix + N'SagaThree';

/* Initialize */

/* CreateTable */

if not exists
(
    select *
    from sys.objects
    where
        object_id = object_id(@tableName) and
        type in ('U')
)
begin
declare @createTable nvarchar(max);
set @createTable = '
    create table ' + @tableName + '(
        Id uniqueidentifier not null primary key,
        Metadata nvarchar(max) not null,
        Data nvarchar(max) not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null
    )
';
exec(@createTable);
end

/* AddProperty IdThree */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_IdThree' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_IdThree nvarchar(max);
  set @createColumn_IdThree = '
  alter table ' + @tableName + N'
    add Correlation_IdThree uniqueidentifier;';
  exec(@createColumn_IdThree);
end

/* VerifyColumnType Guid */

declare @dataType_IdThree nvarchar(max);
set @dataType_IdThree = (
  select data_type
  from information_schema.columns
  where
    table_name = ' + @tableName + N' and
    column_name = 'Correlation_IdThree'
);
if (@dataType_IdThree <> 'uniqueidentifier')
  begin
    declare @error_IdThree nvarchar(max) = N'Incorrect data type for Correlation_IdThree. Expected uniqueidentifier got ' + @dataType_IdThree + '.';
    throw 50000, @error_IdThree, 0
  end

/* WriteCreateIndex IdThree */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_IdThree' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_IdThree nvarchar(max);
  set @createIndex_IdThree = N'
  create unique index Index_Correlation_IdThree
  on ' + @tableName + N'(Correlation_IdThree)
  where Correlation_IdThree is not null;';
  exec(@createIndex_IdThree);
end

/* PurgeObsoleteIndex */

declare @dropIndexQuery nvarchar(max);
select @dropIndexQuery =
(
    select 'drop index ' + name + ' on ' + @tableName + ';'
    from sysindexes
    where
        Id = (select object_id from sys.objects where name = @tableName) and
        Name is not null and
        Name like 'Index_Correlation_%' and
        Name <> N'Index_Correlation_IdThree'
);
exec sp_executesql @dropIndexQuery

/* PurgeObsoleteProperties */

declare @dropPropertiesQuery nvarchar(max);
select @dropPropertiesQuery =
(
    select 'alter table ' + @tableName + ' drop column ' + column_name + ';'
    from information_schema.columns
    where
        table_name = @tableName and
        column_name like 'Correlation_%' and
        column_name <> N'Correlation_IdThree'
);
exec sp_executesql @dropPropertiesQuery
