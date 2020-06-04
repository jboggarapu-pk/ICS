set serveroutput ON
DECLARE
	  PC_RETCURSOR ICS.CERTIFICATION_CRUD.retCursor;
	  PS_SKU VARCHAR2(200);
	  PS_CERTIFICATIONNAME VARCHAR2(200);
	  PI_SKUID Number;
  
	li_CERTIFICATIONTYPEID      ICS.certificate.certificationtypeid%type;
	ls_CERTIFICATENUMBER        ICS.certificate.certificationtypeid%type;
	ld_DATESUBMITED             ICS.certificate.DATESUBMITED%type;
	ls_ACTIVESTATUS             ICS.certificate.ACTIVESTATUS%type;
	ld_DATEASSIGNED_EGI         ICS.certificate.DATEASSIGNED_EGI%type;
	ld_DateApproved_CEGI        ICS.certificate.DateApproved_CEGI%type;
	ls_RENEWALREQUIRED_CGIN     ICS.certificate.RENEWALREQUIRED_CGIN%type;
	ls_SUPPLEMENTALREQUIRED_EI  ICS.certificate.SUPPLEMENTALREQUIRED_EI%type;
	ls_SUPPLEMENTALNUMBER_EI    ICS.certificate.SUPPLEMENTALNUMBER_EI%type;
	ls_JOBREPORTNUMBER_CEN      ICS.certificate.JOBREPORTNUMBER_CEN%type;
	ls_EXTENSION_EN             ICS.certificate.EXTENSION_EN%type;
	ls_SUPPLEMENTALMOLDSTAMPING_E ICS.certificate.SUPPLEMENTALMOLDSTAMPING_E%type;
	ls_EMARKREFERENCE_I         ICS.certificate.EMARKREFERENCE_I%type;
	ld_EXPIRYDATE_I             ICS.certificate.EXPIRYDATE_I%type;
	ls_FAMILY_I                 ICS.certificate.FAMILY_I%type;
	ls_PRODUCTLOCATION_C        ICS.certificate.PRODUCTLOCATION_C%type;
	ls_COUNTRYOFMANUFACTURE_N   ICS.certificate.COUNTRYOFMANUFACTURE_N%type;
	ls_CUSTOMER_N               ICS.certificate.CUSTOMER_N%type;
	ls_CUSTOMERSPECIFIC_N       ICS.certificate.CUSTOMERSPECIFIC_N%type;
	ls_IMPORTER_N               ICS.certificate.IMPORTER_N%type;
	ls_IMPORTERADDRESS_N        ICS.certificate.IMPORTERADDRESS_N%type;
	ls_IMPORTERREPRESENTATIVE_N ICS.certificate.IMPORTERREPRESENTATIVE_N%type;
	ls_COUNTRYLOCATION_N        ICS.certificate.COUNTRYLOCATION_N%type;
	ls_BATCHNUMBER_G            ICS.certificate.BATCHNUMBER_G%type;
	li_SKUID                    ICS.certificate.SKUID%type;
BEGIN
  PS_SKU := 'x';
  PS_CERTIFICATIONNAME := 'Imark';
  PI_SKUID:=1;
  ICS.CERTIFICATION_CRUD.GETCERTIFICATESINFO(
    PC_RETCURSOR => PC_RETCURSOR,
    PS_SKU => PS_SKU,
    PS_CERTIFICATIONNAME => PS_CERTIFICATIONNAME,
    PI_SKUID =>PI_SKUID
  );
   if  PC_RETCURSOR is null then
           DBMS_OUTPUT.PUT_LINE('PC_RETCURSOR returned null' ); 
      else      
          LOOP   
                FETCH PC_RETCURSOR INTO 
                    li_CERTIFICATIONTYPEID      ,
										ls_CERTIFICATENUMBER       ,
										ld_DATESUBMITED            ,
										ls_ACTIVESTATUS            ,
										ld_DATEASSIGNED_EGI        ,
										ld_DateApproved_CEGI       ,
										ls_RENEWALREQUIRED_CGIN    ,
										ls_SUPPLEMENTALREQUIRED_EI  ,
										ls_SUPPLEMENTALNUMBER_EI    ,
										ls_JOBREPORTNUMBER_CEN      ,
										ls_EXTENSION_EN             ,
										ls_SUPPLEMENTALMOLDSTAMPING_E ,
										ls_EMARKREFERENCE_I         ,
										ld_EXPIRYDATE_I             ,
										ls_FAMILY_I                 ,
										ls_PRODUCTLOCATION_C        ,
										ls_COUNTRYOFMANUFACTURE_N   ,
										ls_CUSTOMER_N               ,
										ls_CUSTOMERSPECIFIC_N       ,
										ls_IMPORTER_N               ,
										ls_IMPORTERADDRESS_N        ,
										ls_IMPORTERREPRESENTATIVE_N ,
										ls_COUNTRYLOCATION_N        ,
										ls_BATCHNUMBER_G            ,
										li_SKUID                    ;
                EXIT WHEN PC_RETCURSOR%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(li_CERTIFICATIONTYPEID   || '||' || 
                                        ls_CERTIFICATENUMBER  || ' | ' || 
                                        ld_DATESUBMITED       || ' | ' || 
                                        ls_ACTIVESTATUS       || ' | ' || 
                                        ld_DATEASSIGNED_EGI   || ' | ' ||  
                                        ld_DateApproved_CEGI  || ' | ' || 
                                        ls_RENEWALREQUIRED_CGIN     || ' | ' || 
                                        ls_SUPPLEMENTALREQUIRED_EI  || ' | ' || 
                                        ls_SUPPLEMENTALNUMBER_EI    || ' | ' || 
                                        ls_JOBREPORTNUMBER_CEN      || ' | ' || 
                                        ls_EXTENSION_EN             || ' | ' ||  
                                        ls_SUPPLEMENTALMOLDSTAMPING_E  || ' | ' ||  
                                        ls_EMARKREFERENCE_I            || ' | ' ||  
                                        ld_EXPIRYDATE_I                || ' | ' ||  
                                        ls_FAMILY_I                    || ' | ' ||  
                                        ls_PRODUCTLOCATION_C           || ' | ' ||  
                                        ls_COUNTRYOFMANUFACTURE_N      || ' | ' ||  
                                        ls_CUSTOMER_N            || ' | ' ||  
                                        ls_CUSTOMERSPECIFIC_N    || ' | ' ||  
                                        ls_IMPORTER_N            || ' | ' ||  
                                        ls_IMPORTERADDRESS_N     || ' | ' ||  
                                        ls_IMPORTERREPRESENTATIVE_N  || ' | ' ||  
                                        ls_COUNTRYLOCATION_N     || ' | ' ||  
                                        ls_BATCHNUMBER_G  || ' | ' || 
                                        li_skuid   ); 
            END LOOP; 
            CLOSE PC_RETCURSOR;
        
       end if;
END;
