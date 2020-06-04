
CREATE SEQUENCE  "APP_MESSAGE_SEQ"  MINVALUE 1 MAXVALUE 9999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE ;
 

CREATE TABLE APP_MESSAGE 
(
  SEQ_NO NUMBER NOT NULL 
, OPERATOR_ID VARCHAR2(20) 
, MACHINE_ID VARCHAR2(20) 
, DATE_RECORDED DATE 
, PROCESS_NAME VARCHAR2(30) 
, REDORDED_DATA VARCHAR2(4000) 
, MESSAGE_CODE VARCHAR2(10) 
, MESSAGE VARCHAR2(4000) 
, CONSTRAINT APP_MESSAGE_PK PRIMARY KEY 
  (
    SEQ_NO 
  )
  ENABLE 
);
