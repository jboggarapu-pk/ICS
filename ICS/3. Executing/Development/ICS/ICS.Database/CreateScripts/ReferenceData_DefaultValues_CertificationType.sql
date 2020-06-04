set serveroutput ON
Declare
li_total_Before integer;
li_total_After  integer;
li_CertificationTypeID  ICS.CERTIFICATIONTYPE.CERTIFICATIONTYPEID%type ;
ls_CertificationTypeName ICS.CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%type ;
Cursor CT_Cursor is
Select CertificationTypeID,CertificationTypeName from CERTIFICATIONTYPE;
Begin
      Open CT_Cursor;
      Loop 
          fetch CT_Cursor into li_CertificationTypeID,ls_CertificationTypeName;
          Exit when CT_Cursor%notfound;
          DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
          DBMS_OUTPUT.PUT_LINE('CertificationTypeName: ' || ls_CertificationTypeName);
          DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
          Select Count(1) as total into LI_TOTAL_BEFORE FROM CERTIFICATETYPEDEFAULTVALUE where CERTIFICATIONTYPEID = li_CertificationTypeID;
          DBMS_OUTPUT.PUT_LINE('LI_TOTAL_BEFORE = ' || LI_TOTAL_BEFORE);
          INSERT INTO CERTIFICATETYPEDEFAULTVALUE (FIELDID,CERTIFICATIONTYPEID)
          Select FIELDID, CERTIFICATIONTYPEID          
          From ICS.DEFAULTFIELD           
          Where CERTIFICATIONTYPEID=li_CertificationTypeID;
          
          
           Select Count(1) as total into li_total_After FROM CERTIFICATETYPEDEFAULTVALUE where CERTIFICATIONTYPEID = li_CertificationTypeID;
           DBMS_OUTPUT.PUT_LINE('li_total_After = ' || li_total_After);  
      end Loop;
      close CT_Cursor; 
      Select Count(1) as total into li_total_After FROM CERTIFICATETYPEDEFAULTVALUE ;
      DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
      DBMS_OUTPUT.PUT_LINE('Total of Inserts:' || li_total_After);
      DBMS_OUTPUT.PUT_LINE('--------------------------------------------');
      Commit;
end;
/
