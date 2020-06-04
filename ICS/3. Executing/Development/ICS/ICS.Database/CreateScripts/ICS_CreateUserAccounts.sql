
DROP USER ICS CASCADE ;
DROP USER ICS_PROCS CASCADE;

set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('-------------------');
    DBMS_OUTPUT.put_line('Createing User ICS!');
    DBMS_OUTPUT.put_line('-------------------');
end;
/
-- USER SQL that owns the schema
CREATE USER ICS IDENTIFIED BY coopertire
DEFAULT TABLESPACE DATA_TS
TEMPORARY TABLESPACE TEMP_TS;
set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('-------------------------');
    DBMS_OUTPUT.put_line('Granting User ICS Rights.');
    DBMS_OUTPUT.put_line('-------------------------');
end;
/

-- ROLES
GRANT "CONNECT" TO ICS ;
GRANT "RESOURCE" TO ICS ;

ALTER USER "ICS" DEFAULT ROLE "CONNECT","RESOURCE";
GRANT CREATE VIEW TO ICS;
GRANT create public synonym TO ICS;
--GRANT debug any procedure, debug connect session TO ICS; 
GRANT debug connect session TO ICS;
/
set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('-------------------------------------------');
    DBMS_OUTPUT.put_line('Creanting User ICS_PROCS. And Granting rights.');
    DBMS_OUTPUT.put_line('-------------------------------------------');
end;
/

-- USER that will be used on dev
CREATE USER ICS_PROCS IDENTIFIED BY "1234"
DEFAULT TABLESPACE DATA_TS
TEMPORARY TABLESPACE TEMP_TS;
GRANT "CONNECT" TO ICS_PROCS ;
GRANT "RESOURCE" TO ICS_PROCS;
Grant CREATE PROCEDURE TO ICS_PROCS;
GRANT debug connect session TO ICS_Procs;
/
