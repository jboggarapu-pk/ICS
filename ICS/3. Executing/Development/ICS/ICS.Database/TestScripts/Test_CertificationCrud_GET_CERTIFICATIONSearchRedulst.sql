set serveroutput ON
DECLARE
  PC_RETCURSOR ICS.CERTIFICATION_CRUD.retCursor;
  PS_SEARCHCRITERIA VARCHAR2(200);
  PS_SEARCHTYPE VARCHAR2(200); 
  
  li_BrandCode ICS.BRAND.BRANDCODE%type; 
  
  li_SKUID       ICS.PRODUCT.SKUID%type; 
  ls_SKU         ICS.PRODUCT.SKU%type; 
  ls_SIZESTAMP   ICS.PRODUCT.SIZESTAMP%type;
  li_CERTIFICATIONTYPEID   ICS.CERTIFICATIONTYPE.CERTIFICATIONTYPEID%type;
  li_CERTIFICATIONTYPENAME ICS.CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%type; 
  ls_state varchar2(30);
BEGIN
  PS_SEARCHCRITERIA := 'tse' ;
  PS_SEARCHTYPE :='Brand Code';
    /*
 PS_SEARCHCRITERIA := 'W11-3456-32453';
 PS_SEARCHTYPE := 'SKU No.';
 PS_SEARCHCRITERIA := '1' ;
  PS_SEARCHTYPE :='NPR ID No.';

   */
  ICS.CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT(
    PC_RETCURSOR => PC_RETCURSOR,
    PS_SEARCHCRITERIA => PS_SEARCHCRITERIA,
    PS_SEARCHTYPE => PS_SEARCHTYPE
  );
  if  PC_RETCURSOR is null then
           DBMS_OUTPUT.PUT_LINE('PC_RETCURSOR returned null' ); 
      else      
          LOOP   
                FETCH PC_RETCURSOR INTO li_BrandCode,
                                        ls_SKU,
                                        ls_SIZESTAMP,
                                        li_CERTIFICATIONTYPEID,
                                        li_CERTIFICATIONTYPENAME,
                                        li_SKUID,
                                        ls_state ;
                EXIT WHEN PC_RETCURSOR%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(li_BrandCode || ' | ' || 
                                     ls_SKU  || ' | ' || 
                                     ls_SIZESTAMP || ' | ' || 
                                     li_CERTIFICATIONTYPEID || ' | ' || 
                                     li_CERTIFICATIONTYPENAME || ' | ' || 
                                     ls_state || ' | ' || 
                                     li_SKUID);
               
            END LOOP; 
            CLOSE PC_RETCURSOR;
        
        end if;
END;

/
/*
select * from app_message;
delete from app_message;
*/
