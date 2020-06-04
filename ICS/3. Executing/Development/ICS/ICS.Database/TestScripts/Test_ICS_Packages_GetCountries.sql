 set serveroutput ON
DECLARE
  PC_RETCURSOR ICS.ICS_CRUD.retCursor;
  li_CertId        integer;
  ls_CertName      varchar2(200);
  li_CountryId     integer;
  ls_COUNTRYNAME   varchar2(200);
  li_RegionId     integer;
  ls_RegionNAME   varchar2(200);
BEGIN

  ICS_CRUD.GetRegionsAndCountries(PC_RETCURSOR => PC_RETCURSOR);
      if  PC_RETCURSOR is null then
           DBMS_OUTPUT.PUT_LINE('PC_RETCURSOR returned null' ); 
      else      
          LOOP 
                FETCH PC_RETCURSOR INTO li_CountryId , ls_COUNTRYNAME ,li_CertId,LS_CERTNAME, li_RegionId, LS_REGIONNAME ;
                EXIT WHEN PC_RETCURSOR%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(li_CountryId || ' | ' || ls_COUNTRYNAME || '|' || li_CertId || '|' ||LS_CERTNAME || '|' || li_RegionId || '|' || LS_REGIONNAME); 
            END LOOP; 
            CLOSE PC_RETCURSOR;
        
        end if;
END;
/