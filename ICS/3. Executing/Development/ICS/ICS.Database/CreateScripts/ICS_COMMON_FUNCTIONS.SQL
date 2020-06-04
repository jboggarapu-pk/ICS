CREATE OR REPLACE package ics_common_functions as 

   Function GetCertificationID(ps_CertificationTypeName in varchar2) return integer;
   function CheckIfCertificateExists(ps_CertificateNumber in varchar2,pi_CertificationTypeID in integer) return varchar2;
   function getLatestSkuIdBySKU(ps_SKU in Varchar2) return number;
   
   function GetReportNumber(ps_CErtificateNumber in varchar2,pi_CertificationTypeID in Number) return varchar2;
   
   procedure CheckIfSKUExists(ps_Sku in varchar2,ps_skuExist out varchar2);
   procedure CheckIfCertificateNumberExists(ps_CertificateNumber in varchar2,pi_certificateTypeId in number,ps_CertificateNumberExists out varchar2 );
   
   function GetLatestImarkCertificateNum return varchar2;
   
   function GetCertifiIDByCountry(pi_CountryID in Number) return number;
   Function CheckIfProductCountryExists(pi_SkuID in number) return varchar2 ;
   Function CheckIfSkuBelongsToCertificate(pi_SkuID in number,pi_CertificationTypeID in number) return varchar2 ;
   Function GetCertifTypeIDByCountryID(pi_CountryID in number) return Number ;
   
   Function GetCertificateID(ps_CertificateNumber in varchar2,pi_CertificationTypeID in integer) return certificate.certificateid%type ;
   Function GetCertificateNumber(pi_CertificateID in number,pi_CertificationTypeID in number) return certificate.certificatenumber%type ;
   Function GetRequestStatus(ps_CertificateNumber in varchar) return varchar2 ;
   Procedure GetCertificateIDByNumber(ps_CertificateNumber in varchar2,
                                       pi_CertificationTypeID in integer,
                                       pi_CertificateID out number
                                       );
      function CheckIfCertificateContainsSKU(pi_SkuID in number,
                                          pi_CertificationTypeID in number,
                                          pi_CertificateID in number) return varchar2 ;                                       
end ics_common_functions;
/


CREATE OR REPLACE package body ics_common_functions as

  Function GetCertificationID(ps_CertificationTypeName in varchar2) return integer as
    
    li_CertificationId number;    
    begin
          SELECT ce.CertificationTypeId into li_certificationId
          FROM  CertificationType ce
          WHERE lower(ce.CertificationTypeName)=lower(ps_CertificationTypeName);
          
          return li_CertificationId;
          
    end GetCertificationID;
   
   function CheckIfCertificateExists(ps_CertificateNumber in varchar2,pi_CertificationTypeID in integer) return varchar2 as
   lc_Exist char:= 'n';
   li_totalCertificates integer;   
   begin
        SELECT COUNT(1) INTO li_totalCertificates
        FROM  CERTIFICATE ce
        WHERE CE.CERTIFICATIONTYPEID = pi_CertificationTypeID And
              lower(ce.certificatenumber)   = lower(ps_CertificateNumber);
        
        if li_totalCertificates > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;
   
   end;  
   
   function CheckIfSkuBelongsToCertificate(pi_SkuID in number,pi_CertificationTypeID in number) return varchar2 as
   lc_Exist char:= 'n';
   li_totalCertificates integer;   
   begin
        SELECT COUNT(1) INTO li_totalCertificates
        FROM  productcertificate pce
        WHERE pce.CERTIFICATIONTYPEID = pi_CertificationTypeID And
              pce.skuid=pi_SkuID;
        
        if li_totalCertificates > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;
   
   end; 
   
   function CheckIfCertificateContainsSKU(pi_SkuID in number,
                                          pi_CertificationTypeID in number,
                                          pi_CertificateID in number) return varchar2 as
   lc_Exist char:= 'n';
   li_totalCertificates integer;   
   begin
        SELECT COUNT(1) INTO li_totalCertificates
        FROM  productcertificate pce
        WHERE pce.CERTIFICATIONTYPEID = pi_CertificationTypeID And
              pce.skuid=pi_SkuID and
              pce.certificateid = pi_certificateid;
        
        if li_totalCertificates > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;
   
   end CheckIfCertificateContainsSKU;   
   function getLatestSkuIdBySKU(ps_SKU in Varchar2) return number as
   li_SKUID number;
   
   begin
       Select nvl(Max(SKUID),0) into li_SKUID
       from Product
       where sku = ps_SKU ; 
       
        return li_SKUID;
   
   end;  
   
   function GetReportNumber(ps_CErtificateNumber in varchar2,pi_CertificationTypeID in Number) return varchar2 as   
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      ls_ReportNumber varchar2(50);
    
   begin
      
       SELECT SUBSTR(sku,9,4) || '-' || 
               p.singloadindex || 'H' || '-' ||    
                (case
                    when p.mudsnowyn ='y' then 'Y'
                    when p.mudsnowyn ='n' then 'N'
                 end 
                )  as ReportNumber into ls_reportnumber
      FROM Certificate ce 
            inner join ProductCertificate pce on
                  ce.certificateid   = pce.certificateid and
                  ce.certificationtypeid = pce.certificationtypeid
            inner join product p on
                  pce.skuid = p.skuid
      WHERE lower(ce.certificatenumber) = lower(ps_CErtificateNumber) And            
            ce.certificationtypeid = pi_CertificationTypeID;
       
       return ls_reportnumber;
   
   end GetReportNumber;
   
   procedure CheckIfSKUExists(ps_Sku in varchar2,ps_skuExist out varchar2) as
   --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      li_Skuexist integer;
   begin   
        if ps_sku is null then
            raise li_ParametersAreNull;        
        end if ;
        
        SELECT Count(SKU) into li_Skuexist
        FROM  SKUMAIN_PRODUCT_VIEW 
        WHERE sku = ps_sku;
        
        if li_Skuexist > 0 then
            ps_skuExist:='y';
        else
            ps_skuExist:='n';
        end if;
     EXCEPTION
        when li_ParametersAreNull then
           ls_ErrorMsg:=  sqlerrm || ' - CheckIfSKUExists. There are null parameters.';           
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  ics_common_functions.CheckIfSKUExists',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
               raise_application_error (-20005,ls_ErrorMsg);
                
         when others then
            
              ls_ErrorMsg:=  sqlerrm || ' - CheckIfSKUExists.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  ics_common_functions.CheckIfSKUExists',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);       

   end CheckIfSKUExists;
   
   
   procedure CheckIfCertificateNumberExists(ps_CertificateNumber in varchar2,pi_certificateTypeId in number,ps_CertificateNumberExists out varchar2 ) as 
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      li_CertificateNumberExists integer;
      li_SKUID  PRODUCT.SKUID%type;
   begin   
        if ps_CertificateNumber is null or pi_certificateTypeId is null  then
            raise li_ParametersAreNull;        
        end if ;  
        
		SELECT Count(1) into li_CertificateNumberExists
		FROM  Certificate ce
		WHERE Lower(ce.certificatenumber) = lower(ps_CertificateNumber) and 
			ce.certificationtypeid = pi_certificateTypeId ;
		    
		if li_CertificateNumberExists > 0 then
		  ps_CertificateNumberExists:='y';
		else
		  ps_CertificateNumberExists:='n';
		end if;              
       
        
     EXCEPTION
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- CheckIfSKUExists. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  ics_common_functions.CheckIfSKUExists',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
                raise_application_error (-20005,ls_ErrorMsg);
                
         when others then
            
              ls_ErrorMsg:=  sqlerrm || '- CheckIfSKUExists. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  ics_common_functions.CheckIfSKUExists',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg); 
                      raise_application_error (-20007,ls_ErrorMsg);
                      
   end CheckIfCertificateNumberExists;
   
   function GetLatestImarkCertificateNum return varchar2 as
   
   ls_CertificateNumber varchar2(100);
   li_Total number;
   begin
        Select count(certificatenumber) into li_total
        from Certificate
        where certificationtypeid = 4 and
              certificatenumber like '%I033%';
        if li_total > 0 then
            select ce.certificatenumber into LS_CERTIFICATENUMBER
            from  certificate ce
            where certificationtypeid = 4 and
                  certificatenumber like '%I033%' And
                  ce.modifiedon = (Select Max(modifiedon) from certificate where certificationtypeid = 4 ) ;      
       
           return  LS_CERTIFICATENUMBER;
        
        else 
             return 'NotFound';
        end if;
   
        
   end GetLatestImarkCertificateNum;
   
   FUNCTION GetCertifiIDByCountry(pi_CountryID in Number) return number as
   li_CertificationTypeID  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE;
      
   Begin
         SELECT nvl(c.certificationtypeid,'0') into li_certificationtypeid
         FROM COUNTRY c
         WHERE c.countryid = pi_countryid; 
         
         return LI_CERTIFICATIONTYPEID;
   End GetCertifiIDByCountry;
   
   Function CheckIfProductCountryExists(pi_SkuID in number) return varchar2 as
   
   ls_TotalSkuids number;
   begin
        Select Count(SKUID) into ls_TotalSkuids
        From  ProductCountry
        Where skuid = pi_SkuID;
        
        if ls_TotalSkuids > 0 then 
            return 'y';
        else
            return 'n';        
        end if;
        
   end CheckIfProductCountryExists;
  
   Function GetCertifTypeIDByCountryID(pi_CountryID in number) return Number as
   
   li_CertificationTypeID number;
   begin
        Select co.certificationtypeid into li_CertificationTypeID
        From  Country co 
        Where co.countryid = pi_CountryID;
        
        return li_CertificationTypeID;
        
   end GetCertifTypeIDByCountryID;
   
    Procedure GetCertificateIDByNumber(ps_CertificateNumber in varchar2,
                                       pi_CertificationTypeID in integer,
                                       pi_CertificateID out number
                                       )  as
  
   li_CertificateID certificate.certificateid%type; 
   li_Total number;
   begin
       
           Select Count(1) into li_Total
           FROM  CERTIFICATE ce
           WHERE CE.CERTIFICATIONTYPEID = pi_CertificationTypeID And
                 lower(ce.certificatenumber)   = lower(ps_CertificateNumber);
           
           if li_Total = 0 then
               li_CertificateID:=0;
           else
                 SELECT CertificateID INTO li_CertificateID
                  FROM  CERTIFICATE ce
                  WHERE CE.CERTIFICATIONTYPEID = pi_CertificationTypeID And
                        lower(ce.certificatenumber)   = lower(ps_CertificateNumber);                        
           end if;
           
        pi_CertificateID:= li_CertificateID;
   
   end GetCertificateIDByNumber; 
   
   Function GetCertificateID(ps_CertificateNumber in varchar2,pi_CertificationTypeID in integer) return certificate.certificateid%type as
  
   li_CertificateID certificate.certificateid%type;   
   begin
       if ps_CertificateNumber <> '' or ps_CertificateNumber is not null then 
       
           SELECT CertificateID INTO li_CertificateID
            FROM  CERTIFICATE ce
            WHERE CE.CERTIFICATIONTYPEID = pi_CertificationTypeID And
                  lower(ce.certificatenumber)   = lower(ps_CertificateNumber);
       else 
            li_CertificateID:=-1;
       end if;
        
        return li_CertificateID;
   
   end GetCertificateID;  
  
  Function GetCertificateNumber(pi_CertificateID in number,pi_CertificationTypeID in number) return certificate.certificatenumber%type as
   
   ls_CertificateNumber certificate.certificatenumber%type; 
   li_TotalID number;
  begin
        Select count(ce.CertificateID) into li_TotalID
        From Certificate ce
        where ce.certificateid=pi_CertificateID and
              ce.certificationtypeid=pi_CertificationTypeID;
              
        if li_TotalID  < 0 then
        
            Select ce.CertificateNumber into ls_CertificateNumber
            from certificate ce
            where ce.certificateid=pi_CertificateID and
                  ce.certificationtypeid=pi_CertificationTypeID;
            
            return ls_CertificateNumber;
        else
            return 'NotFound';
        end if;
        
  end GetCertificateNumber;
  
  Function GetRequestStatus(ps_CertificateNumber in varchar) return varchar2 as
   
   ls_RequestStatus varchar2(50); 
  
  begin
       SELECT 
                 distinct(
          CASE
          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
          WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
          END 
         ) AS State into ls_RequestStatus
        FROM Certificate ce
                inner join CertificationType ct on
                     ce.certificationtypeid = ct.certificationtypeid
                inner join Country c on 
                     c.certificationtypeid = ct.certificationtypeid
                inner join productCountry pc on 
                    c.countryid = pc.countryid
         where lower(ce.certificatenumber) =lower(ps_CertificateNumber)  And Rownum < 2; 
              
       return ls_RequestStatus;
        
  end GetRequestStatus;  
   
end ics_common_functions;
/
