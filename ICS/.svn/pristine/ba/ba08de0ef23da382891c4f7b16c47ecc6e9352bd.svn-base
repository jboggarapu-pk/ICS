set serveroutput ON
----------------------------------------------------------------------------
-- This script will create a test enviromemnt with all objects and packages
-- All you have to do is run this script. It will take care of everything
---------------------------------------------------------------------------
--This should run first with a user that has rights to create users
---------------------------------------------------------------------------
 connect system/password;
START "C:\TekProjects\CooperTire\ICS\ICS.Database\TestEnvironmentScripts\DropTestUsers.sql"
--START "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\TestEnvironmentScripts\DropTestUsers.sql"
set serveroutput on format wraped;
begin
    DBMS_OUTPUT.put_line('-----------------------------');
    DBMS_OUTPUT.put_line('Createing User ICS Test user!');
    DBMS_OUTPUT.put_line('-----------------------------');
end;
/
-- USER SQL that owns the schema
CREATE USER "ICS" IDENTIFIED BY "password"
DEFAULT TABLESPACE DATA_TS
TEMPORARY TABLESPACE TEMP;
set serveroutput on format wraped;
begin
    DBMS_OUTPUT.put_line('------------------------------');
    DBMS_OUTPUT.put_line('Granting User ICS Test Rights.');
    DBMS_OUTPUT.put_line('------------------------------');
end;
/

-- ROLES
GRANT "DBA" TO ICS WITH ADMIN OPTION;
GRANT "CONNECT" TO ICS WITH ADMIN OPTION;
GRANT "RESOURCE" TO ICS WITH ADMIN OPTION;
ALTER USER "ICS" DEFAULT ROLE "DBA","CONNECT","RESOURCE";
GRANT SELECT ANY SEQUENCE TO ICS ;

set serveroutput on format wraped;
begin
    DBMS_OUTPUT.put_line('-----------------------------------------------');
    DBMS_OUTPUT.put_line('Creanting User ICSTESTDEV. And Granting rights.');
    DBMS_OUTPUT.put_line('-----------------------------------------------');
end;
/

-- USER that will be used on dev
CREATE USER "ICSTestDEV" IDENTIFIED BY "password"
DEFAULT TABLESPACE DATA_TS
TEMPORARY TABLESPACE TEMP;
GRANT "CONNECT" TO "ICSTestDEV" ;
GRANT SELECT  ANY TABLE     To "ICSTestDEV";
GRANT EXECUTE ANY PROCEDURE TO "ICSTestDEV" ;

set serveroutput on format wraped;
begin
    DBMS_OUTPUT.put_line('----------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICSTEST. And Creating objetcs and Creates Packages.');
    DBMS_OUTPUT.put_line('----------------------------------------------------------------------');
end;
/
--Connect ics/test;
--START "C:\TekProjects\CooperTire\ICS\ICS.Database\Create Scripts\ICS_CreateTables.sql"
--START "C:\TekProjects\CooperTire\ICS\ICS.Database\Create Scripts\App_Message_Package.sql"
--START "C:\TekProjects\CooperTire\ICS\ICS.Database\Create Scripts\ICS_PACKAGE_CRUD.sql"
--START "C:\TekProjects\CooperTire\ICS\ICS.Database\Create Scripts\Certification_CRUD_Package.sql"
--START "C:\TekProjects\CooperTire\ICS\ICS.Database\Create Scripts\TestResults_CRUD_Package.sql"
--START "C:\TekProjects\CooperTire\ICS\ICS.Database\Create Scripts\ReferenceData.sql"




--START "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\Create Scripts\ICS_PACKAGE_CRUD.sql"
--START "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\Create Scripts\App_Message_Package.sql"
--START "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\Create Scripts\Certification_CRUD_Package.sql"
--START "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\Create Scripts\TestResults_CRUD_Package.sql"
--START "C:\Workspaces\Cooper_Tire\ICS\ICS.Database\Create Scripts\ReferenceData.sql"
set serveroutput on format wraped;
begin
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Connecting as User ICSTESTDEV, and Testing its rights on ICS Created objetcs.');
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
end;
/
COMMIT;
--connect as ICSTestDEV to test its access
Connect ICSTestDEV/test;

select count(1) as TotalCountries from ICS.Country; 

select count(1) as TotalProducts from ICS.Product; 