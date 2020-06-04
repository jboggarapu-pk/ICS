SET serveroutput ON
DECLARE
  PC_RETCURSOR ICS.CERTIFICATION_CRUD.retCursor;
  PS_BRANDCODE NUMBER;
  
  PI_BrandCode          number;
  
  li_BRANDCode         ICS.Brand.BRANDCODE%Type; 
  ls_SKU               ICS.PRODUCT.SKU%Type;    
  li_CERTIFICATIONID   ICS.CERTIFICATION.CERTIFICATIONID%Type;
  ls_CertificationName ICS.CERTIFICATION.CERTIFICATIONNAME%Type;
  
BEGIN
  PS_BRANDCODE := 3456;

  CERTIFICATION_CRUD.GET_CERTIFICATIONBYBRANDCODE(
    PC_RETCURSOR => PC_RETCURSOR,
    PS_BRANDCODE => PS_BRANDCODE
  );
  DBMS_OUTPUT.PUT_LINE('---------------------------------------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-----------------PC_RETCURSOR-------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('---------------------------------------------------------------------------------');
  IF PC_RETCURSOR IS NULL THEN
    DBMS_OUTPUT.PUT_LINE('ret cursor is null');
  ELSE
    DBMS_OUTPUT.PUT_LINE('li_BRANDCode' || ' | ' || 'ls_SKU' || ' | ' || 'li_CERTIFICATIONID' || ' | ' || 'ls_CertificationName ' );
    DBMS_OUTPUT.PUT_LINE('----------------------------------------------------------------------------------------------------------------------');
    LOOP              
      FETCH PC_RETCURSOR INTO li_BRANDCode,ls_SKU,li_CERTIFICATIONID,ls_CertificationName ;
      
      DBMS_OUTPUT.PUT_LINE(li_BRANDCode || '  | ' || ls_SKU || '  | ' || li_CERTIFICATIONID || ' | ' || ls_CertificationName );
      EXIT WHEN PC_RETCURSOR%NOTFOUND;
    END LOOP;
    CLOSE PC_RETCURSOR;
  END IF;
END;
