CREATE OR REPLACE package ics_crud as 
  Type retCursor is ref cursor;
  procedure GetRegions( pc_retCursor out retCursor);
  procedure GetRegionsAndCountries( pc_retCursor out retCursor); 
  procedure GetCountriesByRegionID( pc_Countries out retCursor,pi_RegionId in integer);
  procedure GetCountriesByRegionName( pc_Countries out retCursor,ps_RegionName in varchar);
  procedure GetSearchTypes( pc_retCursor out retCursor); 
  procedure GetCertifications( pc_retCursor out retCursor);
  procedure GetSearchTypesBycertification( pc_retCursor out retCursor,pi_certificationId in integer);
  procedure SearchBrand(pc_BrandProduct out retCursor, pc_RegionsCertified out retCursor,pc_RegionNotCertified out retCursor, ps_Brandcode in  varchar2);
  procedure CreateOrDeleteProductCountry (ps_DeleteMe in char,ps_SKU in Varchar2,pi_skuId in integer,pi_Countryid in integer);
   procedure ProductCountry_Save (ps_DeleteMe in char,ps_sku in varchar2,pi_CertificationId in integer,pi_SKUId in integer);
 Function  GetCertificateState(ps_SKU in Varchar2,pi_CertifId in integer,pi_SKUID in Number) return varchar2 ; 
end ics_crud;
/


CREATE OR REPLACE package body ics_crud as

	procedure GetRegions( pc_retCursor out retCursor) as
		ls_MachineId VARCHAR2(50):=null;
		ls_OperatorId VARCHAR2(50):='ICSDEV';     
    ls_ErrorMsg VARCHAR2(4000);    
	begin
		Open pc_retCursor for
		Select REGIONID,RegionName from  Region;

		EXCEPTION
		when others then
            ls_ErrorMsg:=  sqlerrm || ' - GetRegions. An error have ocurred.(when others)'; 
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT
              (
                as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED => sysdate,
                AS_PROCESSNAME =>' ICS_CRUD.GetRegions',
                AX_RECORDDATA    => 'An error have ocurred.(when others)',
                AS_MESSAGECODE => to_char(sqlcode),
                AS_MESSAGE       =>sqlerrm 
              );		  
            raise_application_error (-20007,ls_ErrorMsg);
    
	end GetRegions;

  procedure GetRegionsAndCountries( pc_retCursor out retCursor) as
	  ls_MachineId VARCHAR2(50):=null;
    ls_OperatorId VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  begin
    Open pc_retCursor for
	SELECT 
			CountryId,
			COUNTRYNAME ,
			CE.CERTIFICATIONTYPEID,
			CE.CERTIFICATIONTYPENAME,
			R.REGIONID,
			R.REGIONNAME
	FROM 
		 Country Co,
		 CertificationType ce,
		 Region R
	WHERE 
		R.RegionId = Co.REGIONID And
		Co.CertificationTypeID = ce.CertificationTypeID 		
	ORDER BY R.REGIONNAME,Co.COUNTRYNAME ASC;
    
    EXCEPTION
      when others then
          ls_ErrorMsg:=  sqlerrm || ' - GetRegionsAndCountries. An error have ocurred.(when others)';  
          APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
              as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
              AD_DATERECORDED => sysdate,
              AS_PROCESSNAME =>' ICS_CRUD.GetRegionsAndCountries',
              AX_RECORDDATA    => 'An error have ocurred.(when others)',
              AS_MESSAGECODE => to_char(sqlcode),
              AS_MESSAGE       =>sqlerrm 
            );			
            raise_application_error (-20007,ls_ErrorMsg);
        
  end GetRegionsAndCountries;

 procedure GetCountriesByRegionID( pc_Countries out retCursor,pi_RegionId in integer) as
    --Exception variables
      li_IdNull exception;
      li_IdInvalid exception;
      -- link the exception to the error number
      pragma exception_init( li_IdNull,-20005);
      pragma exception_init( li_IdInvalid,-20006);
      
    ls_MachineId VARCHAR2(50):=null;
    ls_OperatorId VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg varchar2(4000);
 begin
        if  pi_RegionId is null then        
            raise li_IdNull;
        end if;
        if  pi_RegionId <=0 then
            raise li_IdInvalid;
        end if;        
        
        Open pc_Countries for
        SELECT CO.COUNTRYID,
               CO.COUNTRYNAME,              
               CO.REGIONID,
               CO.CertificationTypeId as CertificationID,
               CE.CertificationTypeId as CertificationID,
               CE.CERTIFICATIONTYPENAME as CertificationName
        FROM  COUNTRY CO INNER JOIN  CERTIFICATIONTYPE CE ON 
                    CO.CertificationTypeId = ce.CertificationTypeId
        WHERE co.regionid = pi_RegionId;

    
    EXCEPTION
		  when li_IdNull then
		      ls_ErrorMsg:=  sqlerrm || ' - GetCountriesByRegion. pi_RegionId is null.';
			   APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegion',
				  AX_RECORDDATA    => 'pi_RegionId is null.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       => ls_ErrorMsg);	      
				  raise_application_error (-20005,ls_ErrorMsg);
	           
		  when li_IdInvalid then    
		       ls_ErrorMsg:=  sqlerrm || ' - GetCountriesByRegion. pi_RegionId is Invalid.';
			    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegion',
				  AX_RECORDDATA    => 'pi_RegionId is Invalid.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE      => ls_ErrorMsg);	 
					          
			   raise_application_error (-20006,ls_ErrorMsg); 
	      
		   when others then
		    ls_ErrorMsg:=  sqlerrm || ' - GetCountriesByRegion. An error have ocurred.(when others)';
			 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
          ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   =>' ICS_CRUD.GetCountriesByRegion',
				  AX_RECORDDATA    => 'An error have ocurred.(when others)',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       =>ls_ErrorMsg);
				  			
			 raise_application_error (-20007,ls_ErrorMsg);  
 
 
 end GetCountriesByRegionID;
 
  procedure GetCountriesByRegionName( pc_Countries out retCursor,ps_RegionName in varchar) as
    --Exception variables
      li_IdNull exception;
      li_IdInvalid exception;
      -- link the exception to the error number
      pragma exception_init( li_IdNull,-20005);
      pragma exception_init( li_IdInvalid,-20006);
      
    ls_MachineId VARCHAR2(50):=null;
    ls_OperatorId VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg varchar2(4000);
 begin
 
         if  ps_RegionName is null then        
            raise li_IdNull;
        end if;
          
        
        Open pc_Countries for
           SELECT CO.COUNTRYID,
                 CO.COUNTRYNAME,              
                  CO.REGIONID,
                  r.regionname,
                  ce.CertificationTypeId as CertificationID,
                  Ce.CERTIFICATIONTypeNAME as CERTIFICATIONNAME
           FROM  COUNTRY CO inner join  Region r on 
                                          co.regionid = r.regionid  
                              LEFT JOIN  CERTIFICATIONTYPE CE on 
                                         co.CertificationTypeId = ce.CertificationTypeId      
        WHERE lower(r.regionname) like  '%' || lower(ps_RegionName) || '%' ;


    
    EXCEPTION
		  when li_IdNull then
		      ls_ErrorMsg:=  sqlerrm || ' ps_RegionName is null.';
			   APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegionName',
				  AX_RECORDDATA    => 'ps_RegionName is null.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       => ls_ErrorMsg);	      
				raise_application_error (-20005,ls_ErrorMsg);
	           
		  when li_IdInvalid then    
		       ls_ErrorMsg:=  sqlerrm || ' ps_RegionName is Invalid.';
			    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegionName',
				  AX_RECORDDATA    => ' ps_RegionName is Invalid.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE      => ls_ErrorMsg);	 
					          
			   raise_application_error (-20006,ls_ErrorMsg); 
	      
		   when others then
		    ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
			 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
          ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   =>' ICS_CRUD.GetCountriesByRegionName',
				  AX_RECORDDATA    => 'An error have ocurred.(when others)',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       =>ls_ErrorMsg);
				  			
			 raise_application_error (-20007,ls_ErrorMsg);  
 
 
 end GetCountriesByRegionName;


  procedure GetSearchTypes( pc_retCursor out retCursor) as
	   ls_MachineId VARCHAR2(50):=null;
     ls_OperatorId VARCHAR2(50):='ICSDEV';
     ls_ErrorMsg varchar2(4000);
  begin
    Open pc_retCursor for
    Select TYPEID,TYPENAME from  SEARCHTYPE;
    
    EXCEPTION
      when others then
      ls_ErrorMsg:=  sqlerrm || ' - GetSearchTypes. An error have ocurred.(when others)';  
         APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT
          (
              as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
              AD_DATERECORDED => sysdate,
              AS_PROCESSNAME =>' ICS_CRUD.GetSearchTypes',
               AX_RECORDDATA    => 'An error have ocurred.(when others)',
              AS_MESSAGECODE => to_char(sqlcode),
              AS_MESSAGE       =>sqlerrm 
            );			
         raise_application_error (-20007,ls_ErrorMsg);
        
  end GetSearchTypes;

  procedure GetCertifications( pc_retCursor out retCursor) as
  
	  ls_MachineId VARCHAR2(50):=null;
    ls_OperatorId VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg varchar2(4000);
    
  begin
    Open pc_retCursor for
    Select CertificationTypeId as CertificationId,CertificationTypeName as CERTIFICATIONNAME from  CERTIFICATIONTYPE;
    
    EXCEPTION
       when others then
         ls_ErrorMsg:=  sqlerrm || ' - GetCertifications. An error have ocurred.(when others)';  
         APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT
          (
             as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
              AD_DATERECORDED => sysdate,
              AS_PROCESSNAME =>' ICS_CRUD.GetCertifications',
               AX_RECORDDATA    => 'An error have ocurred.(when others)',
              AS_MESSAGECODE => to_char(sqlcode),
              AS_MESSAGE       =>sqlerrm 
            );			
         raise_application_error (-20007,ls_ErrorMsg);
         
  end GetCertifications;
  
  procedure GetSearchTypesBycertification( pc_retCursor out retCursor,pi_certificationId in integer) as  
      --Exception variables
      li_IdNull exception;
      li_IdInvalid exception;
      pragma exception_init( li_IdNull,-20005);
      pragma exception_init( li_IdInvalid,-20006);
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
	begin
        
        if  pi_certificationId is null then        
            raise li_IdNull;
        end if;
        if  pi_certificationId <=0 then
            raise li_IdInvalid;
        end if;        
        
        Open pc_retCursor for
         SELECT  ce.certificationtypeid as CertificationID,
                Ce.CERTIFICATIONTypeNAME as CERTIFICATIONNAME,
                st.typeid,
                st.typename
        FROM CERTIFICATIONTYPE Ce INNER JOIN CERTIFICATIONSEARCHTYPE  cst on
                   ce.CertificationTypeId=CST.CertificationTypeId
              INNER JOIN SEARCHTYPE ST on 
                  cst.typeid = st.TYPEID
       WHERE CE.CERTIFICATIONTYPEID = pi_certificationId;
    
    EXCEPTION
		  when li_IdNull then
		      ls_ErrorMsg:=  sqlerrm || ' pi_certificationId is null.';
			   APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.GetSearchTypesBycertification',
				  AX_RECORDDATA    => 'pi_certificationId is null.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       => ls_ErrorMsg);	      
				raise_application_error (-20005,ls_ErrorMsg);
	           
		  when li_IdInvalid then    
		       ls_ErrorMsg:=  sqlerrm || ' pi_certificationId is Invalid.';
			    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.GetSearchTypesBycertification',
				  AX_RECORDDATA    => 'pi_certificationId is Invalid.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE      => ls_ErrorMsg);	 
					          
			   raise_application_error (-20006,ls_ErrorMsg);
	      
		   when others then
		    ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
			 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
          ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   =>' ICS_CRUD.GetSearchTypesBycertification',
				  AX_RECORDDATA    => 'An error have ocurred.(when others)',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       =>ls_ErrorMsg);
				  			
			 raise_application_error (-20007,ls_ErrorMsg);  
     
       
  end GetSearchTypesBycertification;
  procedure SearchBrand(pc_BrandProduct out retCursor, pc_RegionsCertified out retCursor,pc_RegionNotCertified out retCursor, ps_Brandcode in  varchar2) as  
      --Exception variables
      li_IdNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_IdNull,-20005);    
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
	begin
        
        if  ps_BrandCode is null then        
            raise li_IdNull;
        end if;
             
        
        --Search the brand and the product based on the brand name
        Open pc_BrandProduct for
        Select spalv.brandcode,
               b.brandname,
               spalv.skuid , 
               spalv.sku,
               spalv.brandcode,
               spalv.sizestamp,
               ce.certificationtypeid as certificationid,
               ce.certificationtypename as certificationname ,
               c.countryid,
               c.countryname,               
                 ( Case
                       WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                       WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                       WHEN pc.RequestStatus  is null                                 THEN 'Requested'     
                   END ) AS State  
          from Brand b inner join SKUMAINPRODUCT_LATEST_VIEW spalv on b.brandcode = spalv.brandcode    
                       left join productcountry pc on
                                          pc.skuid = spalv.skuid
                       Left join country c on 
                                          pc.countryid = c.countryid
                       Left join CertificationType ce on 
                                          c.certificationtypeid = ce.certificationtypeid
          WHERE lower(B.brandcode)= lower(ps_BrandCode)  
          order by   spalv.sku;      
         
         
          --Regions that contains countries that use any certification     
          Open pc_RegionsCertified for
          Select r.regionid,
                 r.regionname,
                 co.countryid,
                 co.countryname
          FROM  Region r inner join  Country co on r.regionid = co.regionid
                        inner join  PRODUCTCOUNTRY pc on
                                   co.countryid = pc.countryid 
                        inner join product p on 
                               pc.skuid=p.skuid and 
                               lower(p.brandcode)= lower(ps_BrandCode)           
          Order by r.regionname,CO.COUNTRYNAME;   
        --Regions that contains countries that don't use certifications
        Open pc_RegionNotCertified for
       Select r.regionid,r.regionname,co.countryid,co.countryname
       FROM  REGION r,
             COUNTRY co
       where r.regionid = co.regionid and
             r.regionid not in (Select distinct(r.regionid)
                                FROM Region r 
                                        inner join  Country co on
                                                 r.regionid = co.regionid
                                        inner join  PRODUCTCOUNTRY pc on 
                                                 co.countryid = pc.countryid
                                        inner join  product p on 
                                                 pc.skuid=p.skuid and 
                                                 lower(p.brandcode)= lower(ps_BrandCode) 
                                 )
        order by r.regionid;

          
                    
        
    EXCEPTION
		  when li_IdNull then
		      ls_ErrorMsg:=  sqlerrm || ' pi_BrandCode is null.';
			   APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
				  AD_DATERECORDED  => sysdate,
				  AS_PROCESSNAME   => ' ICS_CRUD.SearchBrand',
				  AX_RECORDDATA    => 'ps_BrandCode is null.',
				  AS_MESSAGECODE   => to_char(sqlcode),
				  AS_MESSAGE       => ls_ErrorMsg);	      
				raise_application_error (-20005,ls_ErrorMsg);        
		 
	       when others then
					ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
					 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
				  ad_OPERATORID => ls_OperatorId,
						  AD_DATERECORDED  => sysdate,
						  AS_PROCESSNAME   =>' ICS_CRUD.SearchBrand',
						  AX_RECORDDATA    => 'An error have ocurred.(when others)',
						  AS_MESSAGECODE   => to_char(sqlcode),
						  AS_MESSAGE       =>ls_ErrorMsg);				  			
					raise_application_error (-20007,ls_ErrorMsg);	   
  
  end SearchBrand;
  
    procedure CreateOrDeleteProductCountry (ps_DeleteMe in char,ps_SKU in Varchar2,pi_skuId in integer,pi_Countryid in integer) as
   --Exception variables
      li_ParametersAreNull exception;
      li_ParametersArInvalid exception;
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);
      pragma exception_init( li_ParametersArInvalid,-20006);
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      li_TotalSkuWithCountryIdFound integer;
      li_TotalSKUID number;
      LI_SKUID  product.skuid%type;
      li_CertificationTypeId number;
      ls_CertificateNumber VARCHAR2(200); 
      
  begin
        if (ps_DeleteMe is null or pi_Countryid is null or pi_skuId is null )then
           raise li_ParametersAreNull;        
        end if;
        
         if (ps_DeleteMe = '' or pi_Countryid <=0  or pi_skuId <=0 )then
           raise li_ParametersArInvalid;        
        end if;
        
        --Checks if the sku and countryid already exists on the ProductCountry table
        SELECT count(1) as total into li_TotalSkuWithCountryIdFound
        FROM  PRODUCTCOUNTRY pc
        WHERE pc.skuid=pi_skuId  and 
              pc.countryid = pi_Countryid;
        
        if	ps_DeleteMe= 'y' or ps_DeleteMe= 'Y' then 
			      DELETE FROM  PRODUCTCOUNTRY pc WHERE PC.SKUID =pi_skuId  AND   COUNTRYID = pi_Countryid;
        else 
               if li_TotalSkuWithCountryIdFound = 0 then
                    SELECT Count(SKUID) into li_TotalSKUID FROM PRODUCT WHERE SKUID = pi_skuId ; 
                    if li_TotalSKUID > 0 then
                      --Skuid exist in Product
                      SELECT SKUID INTO LI_SKUID FROM PRODUCT WHERE SKUID = pi_skuId AND ROWNUM < 2 ORDER BY SKU;
                      INSERT INTO  PRODUCTCOUNTRY (SKUID, COUNTRYID) VALUES (LI_SKUID,pi_Countryid);
              
                    else
                     --Skuid Does not exist in Product,So we insert into product first and then on product contry
                      insert into product (skuid,BRANDCODE,SKU,SIZESTAMP)
                         Select SKUID_Seq.NextVal,BrandCode,SKU,SizeStamp
                         from SKUMain
                         WHERE SKU = PS_SKU;
                         
                        INSERT INTO  PRODUCTCOUNTRY (SKUID, COUNTRYID) values(SKUID_Seq.CurrVal,pi_Countryid);
                        
                        
                       
                    end if; 
               --INSERT INTO  PRODUCTCOUNTRY (SKU, COUNTRYID) VALUES (ps_sku,pi_Countryid);               
               end if;
                li_CertificationTypeId:= ICS_COMMON_FUNCTIONS.GETCERTIFTYPEIDBYCOUNTRYID(
                                                PI_COUNTRYID => PI_COUNTRYID);
               ls_CertificateNumber:=ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();                              
               if li_CertificationTypeId = 4 And
                  ls_CertificateNumber <> 'NotFound' then
                    CERTIFICATION_CRUD.ADDNEWSKUSTOIMARKCERTIFICATE(
                                              PI_SKUID => pi_skuId,
                                              PI_COUNTRYID => pi_Countryid
                                            );
               end if;
        
        end if;
	       
      
               
      
   
   EXCEPTION
		  when li_ParametersAreNull then
		      ls_ErrorMsg:=  sqlerrm || ' At least one of the parameters is.';
			     APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
              AD_DATERECORDED  => sysdate,
              AS_PROCESSNAME   => ' ICS_CRUD.CreateOrDeleteProductCountry',
              AX_RECORDDATA    => 'ps_DeleteMe  is null or pi_Countryid is null or ps_sku  is null.',
              AS_MESSAGECODE   => to_char(sqlcode),
              AS_MESSAGE       => ls_ErrorMsg);
           raise_application_error (-20005,ls_ErrorMsg);
          
      when li_ParametersArInvalid then
		      ls_ErrorMsg:=  sqlerrm || ' At least one of the parameters isinvalid .';
			     APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                  ad_OPERATORID => ls_OperatorId,
                  AD_DATERECORDED  => sysdate,
                  AS_PROCESSNAME   => ' ICS_CRUD.CreateOrDeleteProductCountry',
                  AX_RECORDDATA    => 'ps_DeleteMe  is null or pi_Countryid is invalid or ps_sku  is invalid.',
                  AS_MESSAGECODE   => to_char(sqlcode),
                  AS_MESSAGE       => ls_ErrorMsg);	
           raise_application_error (-20006,ls_ErrorMsg); 
          
		   when others then   
            ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                    ad_OPERATORID => ls_OperatorId,
                    AD_DATERECORDED  => sysdate,
                    AS_PROCESSNAME   =>' ICS_CRUD.CreateOrDeleteProductCountry',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE   => to_char(sqlcode),
                    AS_MESSAGE       =>ls_ErrorMsg);
              raise_application_error (-20007,ls_ErrorMsg);
              
  end CreateOrDeleteProductCountry; 
  
  
  procedure ProductCountry_Save (ps_DeleteMe in char,ps_sku in varchar2,pi_CertificationId in integer,pi_SKUId in integer) as
   --Exception variables
      li_ParametersAreNull exception;
      li_ParametersArInvalid exception;
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);
      pragma exception_init( li_ParametersArInvalid,-20006);
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      li_SKUID  Product.SKUID%Type;
      
      li_TotalSKUID number;
      li_CountryID number;
      
      Cursor Cursor_CoutryIDs is
      Select co.CountryId
      FROM Country co
      WHERE co.certificationtypeid =  pi_CertificationId;
      
  begin
        if (ps_DeleteMe is null or pi_CertificationId is null or ps_sku is null )then
           raise li_ParametersAreNull;        
        end if;
        
         if (ps_DeleteMe = '' or pi_CertificationId =''  or ps_sku ='' )then
           raise li_ParametersArInvalid;        
        end if; 
        
        if	ps_DeleteMe= 'y' or ps_DeleteMe= 'Y' then 
			       DELETE FROM  PRODUCTCOUNTRY 
             WHERE SKUID =pi_skuid  AND   
                   COUNTRYID in (SELECT co.countryid
                                 FROM country co inner join CertificationType ce on 
                                                     co.CertificationTypeId = ce.CertificationTypeId
                                 where ce.CertificationTypeId=pi_CertificationId);
             
        else         
              --Deletes the SKUID records from Product Country
              DELETE FROM  PRODUCTCOUNTRY 
                     WHERE SKUID =pi_SKUId  AND   
                           COUNTRYID in (SELECT co.countryid
                                         FROM country co inner join CertificationType ce on 
                                                             co.CertificationTypeId = ce.CertificationTypeId
                                         where ce.CertificationTypeId=pi_CertificationId); 
              --Checks to see if that skuid exists on product                                 
              SELECT Count(SKUID) into li_TotalSKUID FROM PRODUCT WHERE SKUID = pi_SKUId ; 
              
               if li_TotalSKUID > 0 then
                  --Skuid exist in Product
                  SELECT SKUID INTO LI_SKUID FROM PRODUCT WHERE SKUID = pi_SKUId AND ROWNUM < 2 ORDER BY SKU;
                  INSERT INTO  PRODUCTCOUNTRY (SKUID, COUNTRYID)
                    ( Select li_SKUID,co.countryid
                     FROM country co inner join CertificationType ce on 
                         co.CertificationTypeId = ce.CertificationTypeId
                     where ce.CertificationTypeId=pi_CertificationId);
               else
                 --Skuid Does not exist in Product,So we insert into product first and then on product contry
                    insert into product (skuid,BRANDCODE,SKU,SIZESTAMP)
                       Select SKUID_Seq.NextVal,BrandCode,SKU,SizeStamp
                       from SKUMain
                       WHERE SKU = PS_SKU;
                    INSERT INTO  PRODUCTCOUNTRY (SKUID, COUNTRYID)
                    ( Select SKUID_Seq.CurrVal,co.countryid
                     FROM country co inner join CertificationType ce on 
                         co.CertificationTypeId = ce.CertificationTypeId
                     where ce.CertificationTypeId=pi_CertificationId);
               end if;            		
                
           --Updates  the status of the sku/country on the product country table (collor related)   
           --I ->In Progress, A->Approved, R->Requested (all the countries that doesn't have certification stay with this one.               
                UPDATE  PRODUCTCOUNTRY SET
                     ( PRODUCTCOUNTRY.CERTIFICATIONTYPEID, PRODUCTCOUNTRY.REQUESTSTATUS)=
                      (
                       Select pi_CertificationId,
                         ( Case
                              WHEN pce.SKUID  is not null  AND (cer.dateapproved_cegi is null or cer.datesubmited is null)   THEN 'I'
                              WHEN pce.SKUID  is not null  AND  (cer.dateapproved_cegi is not null or cer.datesubmited is not null) THEN 'A'
                          else 'R'     
                          END ) as state
                       FROM  Certificate cer 
                                 inner join ProductCertificate pce on
                                       cer.certificateid   = pce.certificateid and
                                       cer.certificationtypeid = pce.certificationtypeid                              
                       WHERE pce.skuid=pi_skuid and 
                             pce.certificationtypeid=pi_CertificationId And 
                             rownum < 2
                      )
                where COUNTRYID in (SELECT co.countryid
                                   FROM country co inner join CertificationType ce on 
                                                       co.CertificationTypeId = ce.CertificationTypeId
                                   where ce.CertificationTypeId=pi_CertificationId);
               --This adds the skus to the last Imark Certificate 
               if pi_CertificationId = 4 then 
                    Open Cursor_CoutryIDs;
                    loop
                        Fetch Cursor_CoutryIDs INTO li_CountryID;
                        EXIT  When Cursor_CoutryIDs%notfound; 
                        CERTIFICATION_CRUD.ADDNEWSKUSTOIMARKCERTIFICATE(
                                              PI_SKUID => pi_skuId,
                                              PI_COUNTRYID => li_CountryID
                                            );
                    end loop;
                    Close Cursor_CoutryIDs;
               end if;
        end if;        
       
   
   EXCEPTION
		  when li_ParametersAreNull then
         
		      ls_ErrorMsg:=  sqlerrm || '-ProductCountry_Save.  At least one of the parameters is.';
			     APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
              AD_DATERECORDED  => sysdate,
              AS_PROCESSNAME   => ' ICS_CRUD.CreateProductCountry',
              AX_RECORDDATA    => 'ps_DeleteMe  is null or pi_Countryid is null or ps_sku  is null.',
              AS_MESSAGECODE   => to_char(sqlcode),
              AS_MESSAGE       => ls_ErrorMsg);
              raise_application_error (-20005,ls_ErrorMsg);
          
      when li_ParametersArInvalid then
        
		      ls_ErrorMsg:=  sqlerrm || '-ProductCountry_Save.  At least one of the parameters isinvalid .';
			     APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                  ad_OPERATORID => ls_OperatorId,
                  AD_DATERECORDED  => sysdate,
                  AS_PROCESSNAME   => ' ICS_CRUD.CreateProductCountry',
                  AX_RECORDDATA    => 'ps_DeleteMe  is null or pi_Countryid is invalid or ps_sku  is invalid.',
                  AS_MESSAGECODE   => to_char(sqlcode),
                  AS_MESSAGE       => ls_ErrorMsg);	
            raise_application_error (-20006,ls_ErrorMsg);
		   when others then
           
            ls_ErrorMsg:=  sqlerrm || '-ProductCountry_Save. An error have ocurred.(when others)';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                    ad_OPERATORID => ls_OperatorId,
                    AD_DATERECORDED  => sysdate,
                    AS_PROCESSNAME   =>' ICS_CRUD.ProductCountry_Save',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE   => to_char(sqlcode),
                    AS_MESSAGE       =>ls_ErrorMsg);
             raise_application_error (-20007,ls_ErrorMsg);       
                    
  end ProductCountry_Save; 
  
  Function  GetCertificateState(ps_SKU in Varchar2,pi_CertifId in integer,pi_SKUID in Number) return varchar2 is
  --Exception variables
      li_ParametersAreNull exception;     
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);    
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
  
      ls_result varchar2(30);
  
  begin
         if ps_SKU is null or pi_CertifId is null then 
            raise li_ParametersAreNull;
         end if;
           SELECT 
                CASE
                    WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                    WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                    WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                END as CertificateState into ls_result
          FROM  ProductCountry pc
          WHERE pc.SKUID =  pi_SKUID and 
                pc.certificationTypeId = pi_CertifId And
                pc.skuid=pi_skuid;
          
          if ls_result is null or nvl(ls_result,'')='' then
             ls_result:=null;
          end if;
          
          
          return ls_result;
     EXCEPTION
		  when li_ParametersAreNull then          
		     
          ls_ErrorMsg:=  sqlerrm || ' At least one of the parameters is.';
			     APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
				      ad_OPERATORID => ls_OperatorId,
              AD_DATERECORDED  => sysdate,
              AS_PROCESSNAME   => ' ICS_CRUD.CreateProductCountry',
              AX_RECORDDATA    => 'ps_DeleteMe  is null or pi_Countryid is null or ps_sku  is null.',
              AS_MESSAGECODE   => to_char(sqlcode),
              AS_MESSAGE       => ls_ErrorMsg);
          raise_application_error (-20005,ls_ErrorMsg);
          
		   when others then
          
            ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                    ad_OPERATORID => ls_OperatorId,
                    AD_DATERECORDED  => sysdate,
                    AS_PROCESSNAME   =>' ICS_CRUD.SearchBrand',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE   => to_char(sqlcode),
                    AS_MESSAGE       =>ls_ErrorMsg);
             raise_application_error (-20007,ls_ErrorMsg);
             
  end GetCertificateState;
  
end ics_crud;
/
