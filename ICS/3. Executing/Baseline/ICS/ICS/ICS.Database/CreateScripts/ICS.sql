--Create the DATA_TS TableSpace
CREATE TABLESPACE DATA_TS
  logging
  datafile 'C:\ORACLEXE\ORADATA\XE\Data_TS.dbf' 
  size 10m 
  autoextend on 
  next 10m maxsize 50m
  extent management local;

-- USER SQL
CREATE USER ICS IDENTIFIED BY coopertire
DEFAULT TABLESPACE DATA_TS
TEMPORARY TABLESPACE TEMP

-- ROLES
GRANT "DBA" TO ICS WITH ADMIN OPTION;

GRANT "CONNECT" TO ICS WITH ADMIN OPTION;

GRANT "RESOURCE" TO ICS WITH ADMIN OPTION;

ALTER USER ICS DEFAULT ROLE "DBA","CONNECT","RESOURCE";
--- Create dev user
CREATE USER ICSDEV IDENTIFIED BY password
DEFAULT TABLESPACE DATA_TS
TEMPORARY TABLESPACE TEMP
-- Grant Connect rights to this user
GRANT "RESOURCE" TO ICS;
GRANT "CONNECT" TO ICS;