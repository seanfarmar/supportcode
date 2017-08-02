
/* TableNameVariable */

declare @tableName nvarchar(max) = @tablePrefix + N'SagaTwo';

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

/* AddProperty IdTwo */

if not exists
(
  select * from sys.columns
  where
    name = N'Correlation_IdTwo' and
    object_id = object_id(@tableName)
)
begin
  declare @createColumn_IdTwo nvarchar(max);
  set @createColumn_IdTwo = '
  alter table ' + @tableName + N'
    add Correlation_IdTwo uniqueidentifier;';
  exec(@createColumn_IdTwo);
end

/* VerifyColumnType Guid */

declare @dataType_IdTwo nvarchar(max);
set @dataType_IdTwo = (
  select data_type
  from information_schema.columns
  where
    table_name = ' + @tableName + N' and
    column_name = 'Correlation_IdTwo'
);
if (@dataType_IdTwo <> 'uniqueidentifier')
  begin
    declare @error_IdTwo nvarchar(max) = N'Incorrect data type for Correlation_IdTwo. Expected uniqueidentifier got ' + @dataType_IdTwo + '.';
    throw 50000, @error_IdTwo, 0
  end

/* WriteCreateIndex IdTwo */

if not exists
(
    select *
    from sys.indexes
    where
        name = N'Index_Correlation_IdTwo' and
        object_id = object_id(@tableName)
)
begin
  declare @createIndex_IdTwo nvarchar(max);
  set @createIndex_IdTwo = N'
  create unique index Index_Correlation_IdTwo
  on ' + @tableName + N'(Correlation_IdTwo)
  where Correlation_IdTwo is not null;';
  exec(@createIndex_IdTwo);
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
        Name <> N'Index_Correlation_IdTwo'
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
        column_name <> N'Correlation_IdTwo'
);
exec sp_executesql @dropPropertiesQuery
