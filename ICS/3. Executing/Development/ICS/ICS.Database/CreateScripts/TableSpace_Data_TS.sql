create tablespace DATA_TS
  logging
  datafile 'C:\ORACLEXE\ORADATA\XE\Data_TS.dbf' 
  size 10m 
  autoextend on 
  next 10m maxsize 100m
  extent management local;
  
  CREATE TEMPORARY TABLESPACE TEMP_TS
  TEMPFILE 'C:\ORACLEXE\ORADATA\XE\TEMP_TS.dbf'
  SIZE 5M 
  AUTOEXTEND ON;