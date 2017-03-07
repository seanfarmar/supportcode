
/* TableNameVariable */

set @tableName = concat(@tablePrefix, 'SagaTwo');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableName);
prepare script from @dropTable;
execute script;
deallocate prepare script;
