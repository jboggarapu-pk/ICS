set serveroutput ON
-------------------------------------------------------------------
-------------------Using  system ----------------------------------
--This should run first with a user that has rights to create users
-------------------------------------------------------------------
-- This is for TekSystems internal use
/*
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
*/
set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICS. And Creating objetcs and Creates Packages.');
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
end;
/

Connect ICS/coopertire;
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\Drop_ICS_Objects.sql"
Commit;
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ICS_CreateTables.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ICS_Views.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ICS_Synonym_Grants.sql"

@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ReferenceData.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ReferenceData_DefaultValues_CertificationType.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\NewReferenceData(Product_Brand_NPRMaster).sql"

Commit;

set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICS_PROCS And Creating Packages.');
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
end;
/

Connect ICS_PROCS/1234;
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\App_Message_Package.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ICS_COMMON_FUNCTIONS.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\Certification_CRUD_Package.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\ICS_PACKAGE_CRUD.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\TestResults_CRUD_Package.sql"
@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\Reports_Package.sql"
COMMIT;

/*
set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICS. And Creating objetcs and Creates Packages.');
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
end;
/

--Connect ICS/coopertire;
--@ "C:\TekProjects\CooperTire\ICS\ICS.Database\CreateScripts\Drop_ICS_Objects.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ICS_CreateTables.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ICS_Views.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ICS_Synonym_Grants.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ReferenceData.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ReferenceData_DefaultValues_CertificationType.sql"
--@  "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\NewReferenceData(Product_Brand_NPRMaster).sql"

set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICS_PROCS And Creating Packages.');
    DBMS_OUTPUT.put_line('------------------------------------------------------------------');
end;
/
--Connect ICS_PROCS/1234;
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\App_Message_Package.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ICS_COMMON_FUNCTIONS.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\Certification_CRUD_Package.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\ICS_PACKAGE_CRUD.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\TestResults_CRUD_Package.sql"
--@ "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\CreateScripts\Reports_Package.sql"
*/
set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User System, and giving rights to ICS on procs that belong to ics_procs.');
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
end;
/

Grant Execute on APP_MESSAGE_OPERATIONS To ICS;
Grant Execute on CERTIFICATION_CRUD To ICS;
Grant Execute on ICS_COMMON_FUNCTIONS To ICS;
Grant Execute on ICS_CRUD To ICS;
Grant Execute on REPORTS_PACKAGE To ICS;
Grant Execute on TESTRESULTS_CRUD To ICS;

GRANT debug on APP_MESSAGE_OPERATIONS To ICS;
Grant debug on CERTIFICATION_CRUD     To ICS;
Grant debug on ICS_COMMON_FUNCTIONS   To ICS;
Grant debug on ICS_CRUD To ICS;
Grant debug on REPORTS_PACKAGE  To ICS;
Grant debug on TESTRESULTS_CRUD To ICS;



set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICS_PROCS, and Testing its rights on ICS Created objetcs.');
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
end;
/
COMMIT;
--connect as ICSDEV to test its access
Connect ICS_PROCS/1234;

select count(1) as TotalCountries from Country;
