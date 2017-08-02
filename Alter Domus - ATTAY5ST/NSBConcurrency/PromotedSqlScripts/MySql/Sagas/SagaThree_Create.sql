
/* TableNameVariable */

set @tableName = concat(@tablePrefix, 'SagaThree');


/* Initialize */

drop procedure if exists sqlpersistence_raiseerror;
create procedure sqlpersistence_raiseerror(message varchar(256))
begin
signal sqlstate
    'ERROR'
set
    message_text = message,
    mysql_errno = '45000';
end;

/* CreateTable */

set @createTable = concat('
    create table if not exists ', @tableName, '(
        Id varchar(38) not null,
        Metadata json not null,
        Data json not null,
        PersistenceVersion varchar(23) not null,
        SagaTypeVersion varchar(23) not null,
        Concurrency int not null,
        primary key (Id)
    ) default charset=ascii;
');
prepare script from @createTable;
execute script;
deallocate prepare script;

/* AddProperty IdThree */

select count(*)
into @exist
from information_schema.columns
where table_schema = database() and
      column_name = 'Correlation_IdThree' and
      table_name = @tableName;

set @query = IF(
    @exist <= 0,
    concat('alter table ', @tableName, ' add column Correlation_IdThree varchar(38) character set ascii'), 'select \'Column Exists\' status');

prepare script from @query;
execute script;
deallocate prepare script;

/* VerifyColumnType Guid */

set @column_type_IdThree = (
  select concat(column_type,' character set ', character_set_name)
  from information_schema.columns
  where
    table_schema = database() and
    table_name = @tableName and
    column_name = 'Correlation_IdThree'
);

set @query = IF(
    @column_type_IdThree <> 'varchar(38) character set ascii',
    'call sqlpersistence_raiseerror(concat(\'Incorrect data type for Correlation_IdThree. Expected varchar(38) character set ascii got \', @column_type_IdThree, \'.\'));',
    'select \'Column Type OK\' status');

prepare script from @query;
execute script;
deallocate prepare script;

/* WriteCreateIndex IdThree */

select count(*)
into @exist
from information_schema.statistics
where
    table_schema = database() and
    index_name = 'Index_Correlation_IdThree' and
    table_name = @tableName;

set @query = IF(
    @exist <= 0,
    concat('create unique index Index_Correlation_IdThree on ', @tableName, '(Correlation_IdThree)'), 'select \'Index Exists\' status');

prepare script from @query;
execute script;
deallocate prepare script;

/* PurgeObsoleteIndex */

select concat('drop index ', index_name, ' on ', @tableName, ';')
from information_schema.statistics
where
    table_schema = database() and
    table_name = @tableName and
    index_name like 'Index_Correlation_%' and
    index_name <> 'Index_Correlation_IdThree' and
    table_schema = database()
into @dropIndexQuery;
select if (
    @dropIndexQuery is not null,
    @dropIndexQuery,
    'select ''no index to delete'';')
    into @dropIndexQuery;

prepare script from @dropIndexQuery;
execute script;
deallocate prepare script;

/* PurgeObsoleteProperties */

select concat('alter table ', @tableName, ' drop column ', column_name, ';')
from information_schema.columns
where
    table_schema = database() and
    table_name = @tableName and
    column_name like 'Correlation_%' and
    column_name <> 'Correlation_IdThree'
into @dropPropertiesQuery;

select if (
    @dropPropertiesQuery is not null,
    @dropPropertiesQuery,
    'select ''no property to delete'';')
    into @dropPropertiesQuery;

prepare script from @dropPropertiesQuery;
execute script;
deallocate prepare script;
