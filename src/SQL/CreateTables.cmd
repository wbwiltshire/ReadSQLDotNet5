SQLCMD -S SCHVW2K12R2-DB\MSSQL2014 -i DropTables.sql
SQLCMD -S SCHVW2K12R2-DB\MSSQL2014 -i CreateState.sql
SQLCMD -S SCHVW2K12R2-DB\MSSQL2014 -i CreateCity.sql
SQLCMD -S SCHVW2K12R2-DB\MSSQL2014 -i CreateContact.sql
Pause