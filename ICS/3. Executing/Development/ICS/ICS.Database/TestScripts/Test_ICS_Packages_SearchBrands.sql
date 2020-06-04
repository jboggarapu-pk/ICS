SET serveroutput ON
DECLARE
  PC_BRANDPRODUCT       ICS.ICS_CRUD.retCursor;
  PC_REGIONSCERTIFIED   ICS.ICS_CRUD.retCursor;
  PC_REGIONNOTCERTIFIED ICS.ICS_CRUD.retCursor;

  Ps_BrandCode         varchar2(200);
  
  li_BRANDCode ICS.Brand.BRANDCODE%Type;
  ls_BRANDNAME ICS.Brand.BRANDNAME%Type;
  ls_SKU ICS.PRODUCT.SKU%Type;
  ls_SizeTire ICS.PRODUCT.SIZESTAMP%Type;  
  
  li_RegionId ICS.REGION.REGIONID%Type;
  ls_RegionName ICS.REGION.REGIONNAME%Type;
  li_CountryId ICS.COUNTRY.COUNTRYID%Type;
  ls_CountryName ICS.COUNTRY.COUNTRYNAME%Type;
   li_CERTIFICATION_FK ICS.CertificationType.CertificationTypeID%Type;
  ls_CertificationName ICS.CertificationType.CertificationTypeNAME%Type;
  
  ls_state varchar2(50);
BEGIN
  Ps_BrandCode :='tse';

  ICS_CRUD.SEARCHBRAND(
    PC_BRANDPRODUCT       => PC_BRANDPRODUCT,
    PC_REGIONSCERTIFIED   => PC_REGIONSCERTIFIED,
    PC_REGIONNOTCERTIFIED => PC_REGIONNOTCERTIFIED,
    Ps_BrandCode          => Ps_BrandCode
  );
  DBMS_OUTPUT.PUT_LINE('---------------------------------------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-----------------pc_BrandProduct-------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('---------------------------------------------------------------------------------');
  IF pc_BrandProduct IS NULL THEN
    DBMS_OUTPUT.PUT_LINE('ret cursor is null');
  ELSE
    DBMS_OUTPUT.PUT_LINE('ID' || ' | ' || 'BRANDNAME' || ' | ' || 'SKU           ' || ' | ' || 'sizetire ' || ' | ' || 'countryid' || ' | ' || 'countryname'  || ' | ' || 'certification' || ' | ' || 'certificationname'  || ' | ' || 'State');
    DBMS_OUTPUT.PUT_LINE('----------------------------------------------------------------------------------------------------------------------');
    LOOP              
      FETCH pc_BrandProduct INTO li_BRANDCode,ls_BRANDNAME,ls_sku,ls_sizetire,li_countryid,ls_countryname,li_certification_fk,ls_certificationname,ls_state ;
      
      DBMS_OUTPUT.PUT_LINE(li_BRANDCode || '  | ' || ls_BRANDNAME || '  | ' || ls_sku || ' | ' || ls_sizetire || ' | ' || li_countryid || '       | ' || ls_countryname  || ' | ' || li_certification_fk || '            | ' || ls_certificationname|| ' | ' || ls_state );
      EXIT WHEN pc_BrandProduct%NOTFOUND;
    END LOOP;
    CLOSE pc_BrandProduct;
  END IF;
  
  DBMS_OUTPUT.PUT_LINE('---------------------------------------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-----------------pc_RegionsCertified---------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('---------------------------------------------------------------------------------');  
  IF pc_RegionsCertified IS NULL THEN
    DBMS_OUTPUT.PUT_LINE('pc_RegionsCertified is null');
  ELSE
     DBMS_OUTPUT.PUT_LINE('ID' || ' | ' || 'Region NAME' || '       | ' || 'id ' || ' | ' || 'Name ' || '      | ' ||  'certif. id' || ' | ' || 'certificationname' );
    DBMS_OUTPUT.PUT_LINE('----------------------------------------------------------------------------------------------------------------------');
    LOOP
      FETCH pc_RegionsCertified INTO li_regionid,ls_regionname,li_countryid,ls_countryname,li_certification_fk,ls_certificationname ;
      DBMS_OUTPUT.PUT_LINE(li_regionid || '  | ' || ls_regionname || '      | ' || li_countryid || ' | ' || ls_countryname || '           |     ' || li_certification_fk || ' | ' || LS_CERTIFICATIONNAME );
      EXIT  WHEN pc_RegionsCertified%NOTFOUND;
    END LOOP;
    CLOSE pc_RegionsCertified;
  END IF;
 
  DBMS_OUTPUT.PUT_LINE('--------------------------------------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-----------------PC_REGIONNOTCERTIFIED------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('--------------------------------------------------------------------------------');
  IF PC_REGIONNOTCERTIFIED IS NULL THEN
    DBMS_OUTPUT.PUT_LINE('PC_REGIONNOTCERTIFIED is null');
  ELSE
    LOOP
      FETCH PC_REGIONNOTCERTIFIED INTO li_regionid,ls_regionname,li_countryid,ls_countryname;--,li_certification_fk,ls_certificationname ;
      DBMS_OUTPUT.PUT_LINE(li_regionid || ' | ' || ls_regionname || ' | ' || li_countryid || ' | ' || ls_countryname ); --|| ' | ' || li_certification_fk || ' | ' || LS_CERTIFICATIONNAME );
      EXIT  WHEN PC_REGIONNOTCERTIFIED%NOTFOUND;
    END LOOP;
    CLOSE PC_REGIONNOTCERTIFIED;
  END IF;
 
END;

