 set serveroutput ON
DECLARE
  PC_COUNTRIES ICS.ICS_CRUD.retCursor;
  PS_REGIONNAME VARCHAR2(200);
  li_RegId integer
  ;ls_RegName VARCHAR2(200);
  li_Id integer;
  ls_Name VARCHAR2(200);
  li_CertId integer;
  ls_CertName VARCHAR2(400);
BEGIN
  PS_REGIONNAME :='OCEANIA';

  ICS_CRUD.GETCOUNTRIESBYREGIONNAME(
    PC_COUNTRIES => PC_COUNTRIES,
    PS_REGIONNAME => PS_REGIONNAME
  );
      if  PC_COUNTRIES is null then
           DBMS_OUTPUT.PUT_LINE('PC_COUNTRIES returned null' ); 
      else      
          LOOP   
                FETCH PC_COUNTRIES INTO li_Id,ls_name, li_RegId,ls_RegName,li_CertId,ls_CertName ;
                EXIT WHEN PC_COUNTRIES%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(li_Id || ' | ' || ls_name || ' | ' || li_RegId || ' | ' || ls_RegName || ' | ' || li_CertId || ' | ' || ls_CertName); 
            END LOOP; 
            CLOSE PC_COUNTRIES;
        
       end if;
END;
/
select * from app_message;
--delete from app_message;