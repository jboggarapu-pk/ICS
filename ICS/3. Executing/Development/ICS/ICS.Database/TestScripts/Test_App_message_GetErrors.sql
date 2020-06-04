 SET serveroutput ON
 DECLARE
      CURSOR cERROR IS
        SELECT         
              TO_CHAR(DATE_RECORDED, 'DD-MON-YYYY HH24:MI:SS') As DATE_RECORDED,
              PROCESS_NAME,
              REDORDED_DATA,
              MESSAGE_CODE,
              MESSAGE
      FROM APP_MESSAGE ;
      
      ls_DateRecorded   Varchar2(30);
      ls_PROCESS_NAME   Varchar2(30);
      ls_REDORDED_DATA  Varchar2(30);
      ls_MESSAGE_CODE   Varchar2(10);
      ls_MESSAGE        Varchar2(4000);   
 begin
       OPEN cERROR;
       DBMS_OUTPUT.PUT_LINE('------------------------------------------------------------------------------------------------------------------------');
       DBMS_OUTPUT.PUT_LINE('DateRecorded ' || '  | ' || 'process_name' || '  | ' || 'Redorded_Data' || ' | ' || 'Message_Code' || ' | ' || 'Message' );
      
       LOOP
            FETCH cERROR INTO ls_DateRecorded,ls_process_name,ls_redorded_data,ls_message_code,ls_message;
            EXIT WHEN cERROR%NOTFOUND;
            DBMS_OUTPUT.PUT_LINE('----------------------------------------------------------------------------------------------------------------------------');
            DBMS_OUTPUT.PUT_LINE(ls_DateRecorded || '  | ' || ls_process_name || '  | ' || ls_redorded_data || ' | ' || ls_message_code || ' | ' || ls_message );
       END LOOP;
       CLOSE cERROR;

 end;
 
