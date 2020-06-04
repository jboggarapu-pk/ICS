 set serveroutput ON
DECLARE
  PC_RETCURSOR ICS.ICS_CRUD.retCursor;
  li_RegId     ICS.Region.REGIONID%Type;
  ls_RegName   ICS.Region.REGIONNAme%Type;
BEGIN

  ICS.ICS_CRUD.GETALLREGIONS(
    PC_RETCURSOR => PC_RETCURSOR
  );
  LOOP 
        FETCH PC_RETCURSOR INTO li_RegId,ls_RegName ;
        EXIT WHEN PC_RETCURSOR%NOTFOUND; 
        DBMS_OUTPUT.PUT_LINE(li_RegId || ' | ' || ls_RegName ); 
    END LOOP; 
    CLOSE PC_RETCURSOR;
  
END;

set serveroutput ON
DECLARE
  PC_RETCURSOR ICS.ICS_CRUD.retCursor;
  PI_CERTIFICATIONID NUMBER;
  
  li_CertId     integer;
  ls_CertName   varchar2(200);
  li_TYPEID     integer;
  ls_TYPEName   varchar2(200);
BEGIN
  PI_CERTIFICATIONID := NULL;

  ICS_CRUD.GETSEARCHTYPESBYCERTIFICATION(
    PC_RETCURSOR => PC_RETCURSOR,
    PI_CERTIFICATIONID => PI_CERTIFICATIONID
  );
    FETCH PC_RETCURSOR INTO li_CertId,ls_CertName,li_TYPEID,LS_TYPENAME ;
   LOOP        
        EXIT WHEN PC_RETCURSOR%NOTFOUND; 
        DBMS_OUTPUT.PUT_LINE(li_CertId || ' | ' || ls_CertName || ' | ' || li_TYPEID || ' | ' || LS_TYPENAME); 
         FETCH PC_RETCURSOR INTO li_CertId,ls_CertName,li_TYPEID,LS_TYPENAME ;
    END LOOP; 
    CLOSE PC_RETCURSOR;
  -- Modify the code to output the variable
  -- DBMS_OUTPUT.PUT_LINE('PC_RETCURSOR = ' || PC_RETCURSOR);
END;