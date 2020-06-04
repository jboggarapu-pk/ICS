set serveroutput ON
DECLARE
  PS_CERTIFICATIONNAME VARCHAR2(200);
  v_Return NUMBER;
BEGIN
  PS_CERTIFICATIONNAME := 'Emark';

  v_Return := CERTIFICATION_CRUD.GETCERTIFICATIONID(
    PS_CERTIFICATIONNAME => PS_CERTIFICATIONNAME
  );
  DBMS_OUTPUT.PUT_LINE('v_Return = ' || v_Return);
END;
/*
select ics.CERTIFICATION_CRUD.GetCertificationID('Emark') from dual;
select ics.CERTIFICATION_CRUD.CheckIfCertificateExists('W11-3456-32458',1) from dual;

SELECT *
FROM CERTIFICATES where sku='W11-3456-32458';
select * from app_message;
select * from certification;

--delete from app_message;
*/