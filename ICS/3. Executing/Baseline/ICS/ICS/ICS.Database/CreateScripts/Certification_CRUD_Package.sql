CREATE OR REPLACE package certification_crud as 

  Type retCursor is ref cursor; 
  --Creates the Log Entry on the Audit Log table
  PROCEDURE AUDITLOG_INSERT(
                            pd_ChangeDateTime in date , 
                            ps_ChangedBy      in VARCHAR2, 
                            ps_Area           in VARCHAR2 , 
                            ps_ChangedFiled_Element in VARCHAR2 , 
                            ps_OLDValue      in VARCHAR2, 
                            ps_NewValue      in VARCHAR2);
  Procedure  AUDITLOG_UpdateApprovalStatus(pi_ChangeLogId in number,pd_ChangeDateTime in date , ps_Status in varchar2,ps_Approver in varchar2);                           
                            
  --Get the information from the   Audit Log table depending on the search criteria                    
  PROCEDURE GET_AUDITLOG(pc_retCursor out retCursor);
  
  PROCEDURE GET_AuditLogForDate(pc_retCursor out retCursor,pd_ChangeDateTime in date);
  
  PROCEDURE GET_AuditLogAfterDate(pc_retCursor out retCursor,pd_ChangeDateTime in date);
  
  PROCEDURE GET_AuditLogByUser(pc_retCursor out retCursor,ps_ChangedBy in varchar2);
  
  PROCEDURE GET_AuditLogByFieldChanged(pc_retCursor out retCursor,ps_ChangedFiled_Element in varchar2);
  
  PROCEDURE GET_AuditLogByArea(pc_retCursor out retCursor,ps_Area in varchar2);
    
  PROCEDURE GET_CERTIFICATIONBYBRANDCODE(pc_retCursor out retCursor,ps_Brandcode in  varchar2);  
  
  PROCEDURE GET_CERTIFICATIONSEARCHRESULT(pc_retCursor out retCursor,
                                          ps_SearchCriteria in varchar2,
                                          ps_SearchType in varchar2);  
  
  PROCEDURE GetCertificatesInfo(pc_retCursor out retCursor,
                                pi_certificationTypeID in number,
                                pi_SKUId in number,
                                ps_TRExists out varchar2);
  
  Procedure GetSimilarCertificateInfo(pc_retCursor out retCursor,ps_SKU in varchar2,pi_CertificationTypeID in Number);
  
    PROCEDURE Certificate_Save( pi_RetId out number,
                                pi_CertificateID in number,
                                pi_SKUId in number,
                                ps_SKU in VARCHAR2,
                                ps_CertificationTypeName in  varchar2,
                                ps_CERTIFICATENUMBER in  varchar2,
                                pd_DATESUBMITED in Date,
                                pc_ACTIVESTATUS in varchar2,
                                pd_DATEASSIGNED_EGI in Date,
                                pd_DateApproved_CEGI in Date,
                                pc_RENEWALREQUIRED_CGIN  in varchar2,
                                pc_SUPPLEMENTALREQUIRED_EI  in varchar2,
                                ps_SUPPLEMENTALNUMBER_EI in  varchar2,
                                ps_JOBREPORTNUMBER_CEN in  varchar2,
                                ps_EXTENSION_EN in  varchar2,
                                ps_SUPPLEMENTALMOLDSTAMPING_E in  varchar2,
                                ps_EMARKREFERENCE_I in  varchar2,
                                pd_EXPIRYDATE_I in Date,
                                ps_FAMILY_I in  varchar2,
                                ps_PRODUCTLOCATION_C in  varchar2,
                                ps_COUNTRYOFMANUFACTURE_N in  varchar2,
                                ps_CUSTOMER_N in  varchar2,
                                ps_CUSTOMERSPECIFIC_N in  varchar2,
                                ps_IMPORTER_N in  varchar2,
                                ps_IMPORTERADDRESS_N in  varchar2,
                                ps_IMPORTERREPRESENTATIVE_N in  varchar2,
                                ps_COUNTRYLOCATION_N in  varchar2,
                                ps_BATCHNUMBER_G  in  varchar2,
                                pd_SUPPLEMENTALDATEASSIGNED_E in Date,
                                pd_SUPPLEMENTALDATESUBMITTED_E in Date,
                                pd_SUPPLEMENTALDATEAPPROVED_E  in Date,
                                ps_UserName in varchar2);
                                /*,
                                pi_CertificateID in number,
                                pi_RetId out number) ;*/

PROCEDURE Certificate_Save_Imark( pi_RetId out number,
                                  pi_CertificateID in number,
                                  pi_SKUId in number,
                                  ps_SKU in VARCHAR2,
                                  ps_CertificationTypeName in  varchar2,
                                  ps_CERTIFICATENUMBER in  varchar2,
                                  pd_DATESUBMITED in Date,
                                  pc_ACTIVESTATUS in varchar2,
                                  pd_DATEASSIGNED_EGI in Date,
                                  pd_DateApproved_CEGI in Date,
                                  pc_RENEWALREQUIRED_CGIN  in varchar2,
                                  pc_SUPPLEMENTALREQUIRED_EI  in varchar2,
                                  ps_SUPPLEMENTALNUMBER_EI in  varchar2,
                                  ps_JOBREPORTNUMBER_CEN in  varchar2,
                                  ps_EXTENSION_EN in  varchar2,
                                  ps_SUPPLEMENTALMOLDSTAMPING_E in  varchar2,
                                  ps_EMARKREFERENCE_I in  varchar2,
                                  pd_EXPIRYDATE_I in Date,
                                  ps_FAMILY_I in  varchar2,
                                  ps_PRODUCTLOCATION_C in  varchar2,
                                  ps_COUNTRYOFMANUFACTURE_N in  varchar2,
                                  ps_CUSTOMER_N in  varchar2,
                                  ps_CUSTOMERSPECIFIC_N in  varchar2,
                                  ps_IMPORTER_N in  varchar2,
                                  ps_IMPORTERADDRESS_N in  varchar2,
                                  ps_IMPORTERREPRESENTATIVE_N in  varchar2,
                                  ps_COUNTRYLOCATION_N in  varchar2,
                                  ps_BATCHNUMBER_G  in  varchar2,
                                  pd_SUPPLEMENTALDATEASSIGNED_E in Date,
                                  pd_SUPPLEMENTALDATESUBMITTED_E in Date,
                                  pd_SUPPLEMENTALDATEAPPROVED_E  in Date,
                                  ps_UserName in varchar2) ;
                                
   Procedure CertificateBasicInfo_Save(ps_SKU                in VARCHAR2,
                                      pi_CertificationTypeId in  number,
                                      ps_CertificateNumber   in  varchar2,
                                      ps_OperatorName        in  varchar2 );
                                
 Procedure GetDefaultValues(pc_retCursor out retCursor,
                            ps_Number out CERTIFICATE.CERTIFICATENUMBER%TYPE,
                            ps_TypeName in CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%Type,
                            pi_NumberID in CERTIFICATE.CERTIFICATEID%TYPE);

Procedure CertificateDefaultvalue_Save(pi_FieldvalueId in number,pi_CertificationTypeID in Number,ps_FieldValue in varchar2,ps_CertificateNumber in varchar2);

Procedure CertificTypeDefaultValue_Save(pi_FieldvalueId in number,pi_CertificationTypeID in Number,ps_FieldValue in varchar2); 

Function CheckIfCertificateDFExist(pi_FieldID in number,pi_certificationTypeID in Number,pi_CertificateID in Number) return varchar2;

Procedure RenewImarkCertificate(ps_OldCertificateNumber in varchar2,ps_NewCertificateNumber in varchar2, ps_UserID in varchar2);

Procedure IMARKCERTIFICATE_RENEW(pi_NewID out number, ps_CertOld in varchar2,ps_CertNew in varchar2 , ps_OperatorName in varchar2);

Procedure AddNewSkusToImarkCertificate(pi_Skuid in number,pi_countryID in number);

Procedure GetLatestImarkCertifNumber(ps_certificateNumber out varchar2);

procedure AddCustomer(pi_skuid in number,
                       ps_CUSTOMER in varchar2,
                       ps_IMPORTER in varchar2,
                       ps_IMPORTERREPRESENTATIVE in varchar2,
                       ps_IMPORTERADDRESS in varchar2,
                       ps_COUNTRYLOCATION in varchar2);
                       
  procedure GetCustomerInfo( pc_RetCustomer out retcursor,
                             pi_skuid in number,
                             ps_CUSTOMER in varchar2);   
                             
  procedure GetCustomerInfoBySKU( pc_RetCustomer out retcursor,pi_skuid in number);   
                             
end certification_crud;
/


CREATE OR REPLACE package body certification_crud as

  PROCEDURE AUDITLOG_INSERT(pd_ChangeDateTime in date , 
                            ps_ChangedBy      in VARCHAR2, 
                            ps_Area           in VARCHAR2 , 
                            ps_ChangedFiled_Element in VARCHAR2 , 
                            ps_OLDValue      in VARCHAR2, 
                            ps_NewValue      in VARCHAR2) as
    
    le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);    
    
  begin
  
        if  ps_changedby is not null then        
            ls_OPERATORID:=ps_ChangedBy;
        end if;
  
  
        INSERT INTO  CERTIFICATION_AUDIT_LOG
          (
          CHANGELOGID,
          CHANGEDATETIME,
          CHANGEDBY,
          AREA,
          CHANGEDFILED_ELEMENT,
          OLDVALUE,
          NEWVALUE          
          )
        VALUES
          (
          CHANGELOGID_SEQ.NextVal,
          pd_ChangeDateTime,
          ps_ChangedBy,
          ps_Area,
          ps_ChangedFiled_Element,
          ps_OLDValue,
          ps_NewValue
          );
      
    Exception
            when le_ParametersNull then    
                   ls_ErrorMsg:=  sqlerrm || '- AUDITLOG_INSERT.  There are null parameters.';
                  APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => ls_MACHINEID,
                  AD_OPERATORID    => ls_OPERATORID,
                  AD_DATERECORDED  => sysdate,
                  AS_PROCESSNAME   => ' CERTIFICATION_CRUD.AUDITLOG_INSERT',
                  AX_RECORDDATA    => 'pi_certificationId is Invalid.',
                  AS_MESSAGECODE   => to_char(sqlcode),
                  AS_MESSAGE      => ls_ErrorMsg);	
                  raise_application_error (-20005,ls_ErrorMsg);
	      
           when others then
            ls_ErrorMsg:=  sqlerrm || '- AUDITLOG_INSERT.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => ls_MACHINEID,
                  AD_OPERATORID    => ls_OPERATORID,
                  AD_DATERECORDED  => sysdate,
                  AS_PROCESSNAME   =>' CERTIFICATION_CRUD.AUDITLOG_INSERT',
                  AX_RECORDDATA    => 'An error have ocurred.(when others)',
                  AS_MESSAGECODE   => to_char(sqlcode),
                  AS_MESSAGE       => ls_ErrorMsg);			 
                 raise_application_error (-20007,ls_ErrorMsg); 
			 
  end AUDITLOG_INSERT;
  
  Procedure  AUDITLOG_UpdateApprovalStatus(pi_ChangeLogId in number,pd_ChangeDateTime in date , ps_Status in varchar2,ps_Approver in varchar2) as
  
    le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000); 
  begin
  
        if ps_Status is null or pi_ChangeLogId is null then
          raise le_ParametersNull;
        end if;
  
        UPDATE  CERTIFICATION_AUDIT_LOG SET 
                      APPROVALSTATUS = ps_Status ,
                      ChangeDateTime= pd_ChangeDateTime,                      
                      Approver       = ps_Approver           
        WHERE CHANGELOGID = pi_ChangeLogId;
  
         Exception
            when le_ParametersNull then    
                   ls_ErrorMsg:=  sqlerrm || ' - AUDITLOG_UpdateApprovalStatus. There are null parameters.';
                  APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => ls_MACHINEID,
                  AD_OPERATORID    => ls_OPERATORID,
                  AD_DATERECORDED  => sysdate,
                  AS_PROCESSNAME   => ' CERTIFICATION_CRUD.AUDITLOG_UpdateApprovalStatus',
                  AX_RECORDDATA    => 'pi_certificationId is Invalid.',
                  AS_MESSAGECODE   => to_char(sqlcode),
                  AS_MESSAGE      => ls_ErrorMsg);
                  raise_application_error (-20005,ls_ErrorMsg);
	      
           when others then
            ls_ErrorMsg:=  sqlerrm || ' - AUDITLOG_UpdateApprovalStatus. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => ls_MACHINEID,
                  AD_OPERATORID    => ls_OPERATORID,
                  AD_DATERECORDED  => sysdate,
                  AS_PROCESSNAME   =>' CERTIFICATION_CRUD.AUDITLOG_UpdateApprovalStatus',
                  AX_RECORDDATA    => 'An error have ocurred.(when others)',
                  AS_MESSAGECODE   => to_char(sqlcode),
                  AS_MESSAGE       => ls_ErrorMsg);	
                  raise_application_error (-20007,ls_ErrorMsg);
                  
  end AUDITLOG_UpdateApprovalStatus;

  PROCEDURE GET_AUDITLOG(pc_retCursor out retCursor) as
  
   le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  
  begin
      Open pc_retCursor FOR
      SELECT CHANGELOGID,
            CHANGEDATETIME,
            CHANGEDBY,
            AREA,
            CHANGEDFILED_ELEMENT,
            OLDVALUE,
            NEWVALUE,
            APPROVALSTATUS,
            Approver
      FROM  CERTIFICATION_AUDIT_LOG 
      order by CHANGEDATETIME desc;

  
  
     EXCEPTION
      when others then
           ls_ErrorMsg:=  sqlerrm || ' - AUDITLOG_UpdateApprovalStatus. An error have ocurred.(when others)';           
          APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
              AS_MACHINEID => ls_MACHINEID,
              AD_OPERATORID => ls_OPERATORID,
              AD_DATERECORDED => sysdate,
              AS_PROCESSNAME =>'  CERTIFICATION_CRUD..GET_AUDITLOG',
              AX_RECORDDATA    => 'An error have ocurred.(when others)',
              AS_MESSAGECODE => to_char(sqlcode),
              AS_MESSAGE       =>sqlerrm 
            );	
            raise_application_error (-20007,ls_ErrorMsg);
            
       
  end GET_AUDITLOG;

  PROCEDURE GET_AuditLogForDate(pc_retCursor out retCursor,pd_ChangeDateTime in date) as
    le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);  
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  begin
        if pd_ChangeDateTime is null then
          raise le_ParametersNull;
        end if;
      
  
    Open pc_retCursor FOR
      SELECT CHANGELOGID,
            CHANGEDATETIME,
            CHANGEDBY,
            AREA,
            CHANGEDFILED_ELEMENT,
            OLDVALUE,
            NEWVALUE,
            APPROVALSTATUS,
            Approver
      FROM  CERTIFICATION_AUDIT_LOG 
      WHERE CHANGEDATETIME=pd_ChangeDateTime
      order by CHANGEDATETIME desc;
      
       EXCEPTION
            when le_ParametersNull then
                    ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogForDate. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogForDate',
                        AX_RECORDDATA    => 'pd_ChangeDateTime is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );			
                 raise_application_error (-20005,ls_ErrorMsg);
                  
            when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogForDate. An error have ocurred.(when others)';   
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogForDate',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm                     
                  );			
                  raise_application_error (-20007,ls_ErrorMsg);
              
  end GET_AuditLogForDate;


PROCEDURE GET_AuditLogAfterDate(pc_retCursor out retCursor,pd_ChangeDateTime in date) as
    le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);  
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  begin
        if pd_ChangeDateTime is null then
          raise le_ParametersNull;
        end if;
      
  
    Open pc_retCursor FOR
      SELECT CHANGELOGID,
            CHANGEDATETIME,
            CHANGEDBY,
            AREA,
            CHANGEDFILED_ELEMENT,
            OLDVALUE,
            NEWVALUE,
            APPROVALSTATUS,
            Approver
      FROM  CERTIFICATION_AUDIT_LOG 
      WHERE CHANGEDATETIME >= pd_ChangeDateTime
      order by CHANGEDATETIME desc;
      
       EXCEPTION
            when le_ParametersNull then
                     ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogAfterDate. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogAfterDate',
                        AX_RECORDDATA    => 'pd_ChangeDateTime is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );			
                     raise_application_error (-20005,ls_ErrorMsg);
                  
            when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogAfterDate. An error have ocurred.(when others)'; 
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogAfterDate',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm 
                  );			
                 raise_application_error (-20007,ls_ErrorMsg);
                 
  end GET_AuditLogAfterDate;



  PROCEDURE GET_AuditLogByUser(pc_retCursor out retCursor,ps_ChangedBy in varchar2) as
   le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);  
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  begin
        if ps_ChangedBy is null then
          raise le_ParametersNull;
        end if;
      
  
    Open pc_retCursor FOR
      SELECT CHANGELOGID,
            CHANGEDATETIME,
            CHANGEDBY,
            AREA,
            CHANGEDFILED_ELEMENT,
            OLDVALUE,
            NEWVALUE,
            APPROVALSTATUS,
            Approver
      FROM  CERTIFICATION_AUDIT_LOG 
      WHERE CHANGEDBY=ps_ChangedBy
      order by CHANGEDATETIME desc;
      
       EXCEPTION
            when le_ParametersNull then
                   ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogByUser. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogByUser',
                        AX_RECORDDATA    => 'ps_ChangedBy is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );			
                  raise_application_error (-20005,ls_ErrorMsg);
                  
            when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogByUser. An error have ocurred.(when others)'; 
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogByUser',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm 
                  );	
                  raise_application_error (-20007,ls_ErrorMsg);
             
    
  end GET_AuditLogByUser;

  PROCEDURE GET_AuditLogByFieldChanged(pc_retCursor out retCursor,ps_ChangedFiled_Element in varchar2) as
   le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);  
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  begin
        if ps_ChangedFiled_Element is null then
          raise le_ParametersNull;
        end if;
      
  
    Open pc_retCursor FOR
      SELECT CHANGELOGID,
            CHANGEDATETIME,
            CHANGEDBY,
            AREA,
            CHANGEDFILED_ELEMENT,
            OLDVALUE,
            NEWVALUE,
            APPROVALSTATUS,
            Approver
      FROM  CERTIFICATION_AUDIT_LOG 
      WHERE CHANGEDFILED_ELEMENT = ps_ChangedFiled_Element
      order by CHANGEDATETIME desc;
      
       EXCEPTION
           when le_ParametersNull then
                   ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogByFieldChanged. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogByFieldChanged',
                        AX_RECORDDATA    => 'ps_ChangedBy is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );			
                  raise_application_error (-20005,ls_ErrorMsg);
 
            when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogByFieldChanged. An error have ocurred.(when others)'; 
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogByFieldChanged',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm 
                  );	
                  raise_application_error (-20007,ls_ErrorMsg);
             
  end GET_AuditLogByFieldChanged;

  PROCEDURE GET_AuditLogByArea(pc_retCursor out retCursor,ps_Area in varchar2) as
    le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);  
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
  begin
        if ps_Area is null then
          raise le_ParametersNull;
        end if;
      
  
    Open pc_retCursor FOR
      SELECT CHANGELOGID,
            CHANGEDATETIME,
            CHANGEDBY,
            AREA,
            CHANGEDFILED_ELEMENT,
            OLDVALUE,
            NEWVALUE,
            APPROVALSTATUS,
            Approver
      FROM  CERTIFICATION_AUDIT_LOG 
      WHERE AREA = ps_Area
      order by CHANGEDATETIME desc;
      
       EXCEPTION
     when le_ParametersNull then
                   ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogByArea. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogByArea',
                        AX_RECORDDATA    => 'ps_ChangedBy is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );			
                  raise_application_error (-20005,ls_ErrorMsg);
 
    when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_AuditLogByArea. An error have ocurred.(when others)'; 
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_AuditLogByArea',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm 
                  );	
                  raise_application_error (-20007,ls_ErrorMsg);
             
  end GET_AuditLogByArea;

   
   
   PROCEDURE GET_CERTIFICATIONBYBRANDCODE(pc_retCursor out retCursor,ps_Brandcode in  varchar2)as
    le_ParametersNull exception;
    pragma exception_init( le_ParametersNull,-20005);  
    
    ls_MACHINEID VARCHAR2(50):=null;
    ls_OPERATORID VARCHAR2(50):='ICSDEV';
    ls_ErrorMsg VARCHAR2(4000);  
   begin
        if ps_Brandcode is null then
          raise le_ParametersNull;
        end if;
        open pc_retCursor for 
        select 
              b.brandcode,
              p.sku,
              p.SIZESTAMP,
              ce.CertificationTypeId     as CertificationId,
              ce.CertificationTypeName   as CertificationName     
        from 
              BRAND b inner join  Product p on
                          b.brandcode = p.brandcode
                     inner join  ProductCountry pc on 
                          p.skuid = pc.skuid
                     inner join  Country co on
                          pc.countryid = co.countryid
                     inner join  CertificationType ce on
                          co.CertificationTypeId = ce.CertificationTypeId   
        where b.brandcode =  ps_Brandcode
        order by p.sku,ce.CertificationTypeName;
   
        EXCEPTION
     when le_ParametersNull then
                   ls_ErrorMsg:=  sqlerrm || ' - GET_CERTIFICATIONBYBRANDCODE. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_CERTIFICATIONBYBRANDCODE',
                        AX_RECORDDATA    => 'ps_ChangedBy is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );			
                  raise_application_error (-20005,ls_ErrorMsg);
 
    when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_CERTIFICATIONBYBRANDCODE. An error have ocurred.(when others)'; 
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_CERTIFICATIONBYBRANDCODE',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm 
                  );	
                  raise_application_error (-20007,ls_ErrorMsg);
             
   END GET_CERTIFICATIONBYBRANDCODE;
   
      PROCEDURE GET_CERTIFICATIONSEARCHRESULT(pc_retCursor out retCursor,ps_SearchCriteria in varchar2,ps_SearchType in varchar2) as
    --Exception variables
      le_ParametersNull exception;
      li_ParametersInvalid exception;
      -- link the exception to the error number
      pragma exception_init( le_ParametersNull,-20005);
      pragma exception_init( li_ParametersInvalid,-20006);
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      li_IsSearchCriteriaNumber number;
      ls_CertificateNumber certificate.certificatenumber%type;
      
   begin
        if ps_SearchCriteria is null or ps_SearchType is null then
          raise le_ParametersNull;
        end if;
        if ps_SearchCriteria = ''  or ps_SearchType = '' then
          raise li_ParametersInvalid;
        end if;
        
 if ps_SearchType = 'Brand Code' then
          begin
            Open pc_retCursor for
             select 
                    b.brandcode,
                    p.sku,
                    p.SIZESTAMP,
                    ce.CertificationTypeId     as CertificationId,
                    ce.CertificationTypeName   as CertificationName,   
                    p.skuid,
                    (
                      CASE
                        WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                        WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                        WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                      END 
                    ) AS State ,
                    NVL(cu.Customer,'N/A') as CUSTOMER
              from 
                    Brand b inner join  Product p on
                                b.brandcode = p.brandcode
                           inner join  ProductCountry pc on 
                                p.skuid = pc.skuid
                           inner join  Country co on
                                pc.countryid = co.countryid
                           inner join  CertificationType ce on
                                co.CertificationTypeId = ce.CertificationTypeId
                           LEFT JOIN  PRODUCTCERTIFICATE PCE ON 
                                 P.SKUID = PCE.SKUID 
                           LEFT JOIN  CERTIFICATE cer on 
                                 pce.certificateid   = cer.certificateid and 
                                 pce.CertificationTypeId = cer.CertificationTypeId 
                           LEFT JOIN Customer cu on
                                 cu.skuid = p.skuid      
              where lower(b.brandcode) =  lower(ps_SearchCriteria)
              order by p.sku,ce.CertificationTypeName;
           end; 
       -- End if ;
      elsif ps_SearchType = 'SKU No.' then
              begin
                    Open pc_retCursor for
                    select 
                          b.brandcode,
                          p.sku,
                          p.SIZESTAMP,
                          ce.CertificationTypeId     as CertificationId,
                          ce.CertificationTypeName   as CertificationName,   
                                      p.skuid,
                          (
                            CASE
                              WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                              WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                              WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                            END 
                          ) AS State,
                          cu.Customer
                      FROM 
                           Brand b inner join  Product p on
                                              b.brandcode = p.brandcode
                                  inner join  ProductCountry pc on 
                                              p.skuid = pc.skuid
                                  inner join  Country co on
                                              pc.countryid = co.countryid
                                  LEFT join  CertificationType ce on
                                              co.CertificationTypeId = ce.CertificationTypeId 
                                  LEFT JOIN  PRODUCTCERTIFICATE PCE ON 
                                              P.SKUID = PCE.SKUID 
                                  LEFT JOIN  CERTIFICATE cer on 
                                             pce.certificateid       = cer.certificateid and 
                                             pce.CertificationTypeId = cer.CertificationTypeId 
                                  LEFT JOIN Customer cu on
                                             cu.skuid = p.skuid
                      WHERE lower(p.sku) = lower(ps_SearchCriteria)
                      ORDER BY p.sku,ce.CertificationTypeName;
               end;
        elsif ps_SearchType = 'NPR ID No.' then 
              Begin   
                    li_IsSearchCriteriaNumber:=LENGTH(TRIM(TRANSLATE(ps_SearchCriteria, ' +-.0123456789',' ')));
              
                   if li_IsSearchCriteriaNumber is null then 
                         Open pc_retCursor for
                         SELECT 
                              b.brandcode,
                              p.sku,
                              p.SIZESTAMP,
                              ct.CertificationTypeId     as CertificationId,
                              ct.CertificationTypeName   as CertificationName,   
                                          p.skuid,
                              (
                                CASE
                                WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                                WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                                WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                                END 
                               ) AS State,
                               cu.Customer                   
                         FROM  CERTIFICATE ce 
                                         inner join  productCertificate pce on
                                                  pce.certificateid       = ce.certificateid and 
                                                  pce.certificationtypeid = ce.certificationtypeid  
                                         inner join  product p  on 
                                                  pce.skuid = p.skuid
                                         inner join  certificationtype ct on 
                                                  ce.certificationtypeid = ct.certificationtypeid
                                         INNER JOIN  BRAND B ON
                                                  P.BRANDCODE = B.BRANDCODE
                                         INNER JOIN  PRODUCTCOUNTRY PC ON
                                                  p.skuid = PC.SKUID
                                          LEFT JOIN Customer cu on
                                                  cu.skuid = p.skuid         
                         WHERE p.NPRID = To_Number(ps_SearchCriteria)
                         ORDER BY p.sku,ct.CertificationTypeName;
                  else
                      --If it ins not numeric return an empty cursor
                       Open pc_retCursor for
                         SELECT 
                              b.brandcode,
                              p.sku,
                              p.SIZESTAMP,
                              ct.CertificationTypeId     as CertificationId,
                              ct.CertificationTypeName   as CertificationName,   
                                          p.skuid,
                              (
                                CASE
                                WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                                WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                                WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                                END 
                               ) AS State,
                               cu.Customer                   
                         FROM  CERTIFICATE ce 
                                         inner join  productCertificate pce on
                                                  ce.certificateid       = pce.certificateid and 
                                                  ce.certificationtypeid = pce.certificationtypeid
                                         inner join  product p  on 
                                                  pce.skuid = p.skuid
                                         inner join  certificationtype ct on 
                                                  ce.certificationtypeid = ct.certificationtypeid
                                         INNER JOIN  BRAND B ON
                                                  P.BRANDCODE = B.BRANDCODE
                                         INNER JOIN  PRODUCTCOUNTRY PC ON
                                                  p.skuid = PC.SKUID 
                                         LEFT JOIN Customer cu on
                                                  cu.skuid = p.skuid         
                         WHERE 1 = 2
                         ORDER BY p.sku,ct.CertificationTypeName;
                  end if;
              End;
        elsif ps_searchtype = 'Certification No.' then
            begin
                if SUBSTR(lower(ps_SearchCriteria),1,4)='i033' then
                    if LENGTH(ps_SearchCriteria) > 4 then
                        ls_CertificateNumber:='';
                    else
                        ls_CertificateNumber:=ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
                    end if;
                else
                    ls_CertificateNumber:=ps_SearchCriteria;
                end if;
            
                Open pc_retCursor for
                select 
                       b.brandcode,
                        p.sku,
                        p.SIZESTAMP,
                        ct.CertificationTypeId     as CertificationId,
                        ct.CertificationTypeName   as CertificationName,   
                        p.skuid,
                        ICS_Common_Functions.GetRequestStatus(ls_CertificateNumber) as State,
                        cu.Customer
                 FROM  CERTIFICATE ce 
                          inner join  productCertificate pce on
                                 ce.certificateid       = pce.certificateid and
                                 ce.certificationtypeid = pce.certificationtypeid
                           inner join  product p  on 
                                 pce.skuid = p.skuid
                           inner join  certificationtype ct on 
                                 ce.certificationtypeid = ct.certificationtypeid
                           INNER JOIN  BRAND B ON
                                 P.BRANDCODE = B.BRANDCODE     
                           LEFT JOIN Customer cu on
                                cu.skuid = p.skuid     
                 where lower(ce.certificatenumber) =lower(ls_CertificateNumber);
             end;
        elsif ps_searchtype = 'Batch No.' then
             begin
             Open pc_retCursor for
                 select 
                        b.brandcode,
                        p.sku,
                        p.SIZESTAMP,
                        ct.CertificationTypeId     as CertificationId,
                        ct.CertificationTypeName   as CertificationName,   
                                    p.skuid,
                        (  CASE
                          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                          WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                          END 
                         ) AS State ,
                          cu.Customer                  
                 FROM  CERTIFICATE ce 
                          inner join  productCertificate pce on
                                            ce.certificateid       = pce.certificateid and
                                            ce.certificationtypeid = pce.certificationtypeid
                           inner join  product p  on 
                                            pce.skuid = p.skuid
                                   inner join  certificationtype ct on 
                                            ce.certificationtypeid = ct.certificationtypeid
                                   INNER JOIN  BRAND B ON
                                            P.BRANDCODE = B.BRANDCODE
                                   INNER JOIN  PRODUCTCOUNTRY PC ON
                                            p.skuid = PC.SKUID
                                   LEFT JOIN Customer cu on
                                             cu.skuid = p.skuid         
                 where lower(ce.batchnumber_G) = lower(ps_SearchCriteria)
                 order by p.sku,ct.CertificationTypeName;
              end;
        elsif ps_searchtype = 'Spec No.' then
              begin
                 Open pc_retCursor for
                 select 
                        b.brandcode,
                        p.sku,
                        p.SIZESTAMP,
                        ct.CertificationTypeId     as CertificationId,
                        ct.CertificationTypeName   as CertificationName,   
                                    p.skuid,
                        (
                          CASE
                          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                          WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                          END 
                         ) AS State,
                          cu.Customer               
                    FROM  CERTIFICATE ce 
                          inner join  productCertificate pce on
                                            ce.certificateid       = pce.certificateid and
                                            ce.certificationtypeid = pce.certificationtypeid
                           inner join  product p  on 
                                            pce.skuid = p.skuid
                                   inner join  certificationtype ct on 
                                            ce.certificationtypeid = ct.certificationtypeid
                                   INNER JOIN  BRAND B ON
                                            P.BRANDCODE = B.BRANDCODE
                                   INNER JOIN  PRODUCTCOUNTRY PC ON
                                            p.skuid = PC.SKUID
                                    LEFT JOIN Customer cu on
                                            cu.skuid = p.skuid         
                              where lower(p.SpecNumber) = lower(ps_SearchCriteria)
                              order by p.sku,ct.CertificationTypeName;
              end;
        elsif ps_searchtype = 'Importer' then
             begin
                Open pc_retCursor for
                 select 
                        b.brandcode,
                        p.sku,
                        p.SIZESTAMP,
                        ct.CertificationTypeId     as CertificationId,
                        ct.CertificationTypeName   as CertificationName,   
                                    p.skuid,
                        (
                          CASE
                          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='I'  THEN 'InProgress'
                          WHEN pc.RequestStatus  is not null  AND  pc.RequestStatus='A'  THEN 'Approved'
                          WHEN pc.RequestStatus  is null                                 THEN 'Requested'  
                          END 
                         ) AS State,
                          cu.Customer                          
                FROM  CERTIFICATE ce 
                          inner join  productCertificate pce on
                                            ce.certificateid       = pce.certificateid and
                                            ce.certificationtypeid = pce.certificationtypeid
                           inner join  product p  on 
                                            pce.skuid = p.skuid
                       inner join  certificationtype ct on 
                                ce.certificationtypeid = ct.certificationtypeid
                       INNER JOIN  BRAND B ON
                                P.BRANDCODE = B.BRANDCODE
                       INNER JOIN  PRODUCTCOUNTRY PC ON
                                p.skuid = PC.SKUID
                       LEFT JOIN Customer cu on
                                cu.skuid = p.skuid
                    where lower(ce.importer_n) = lower(ps_SearchCriteria)
                    order by p.sku,ct.CertificationTypeName;
             end;
        else
            pc_retCursor:=null;
        End if ;
        
        EXCEPTION
            when le_ParametersNull then
                  ls_ErrorMsg:=  sqlerrm || ' - GET_CERTIFICATIONSEARCHRESULT. There are null parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT',
                        AX_RECORDDATA    => 'ps_SearchCriteria is null or ps_SearchType is null.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );
                      
                      raise_application_error (-20005,ls_ErrorMsg);
                      
             when li_ParametersInvalid then
                    ls_ErrorMsg:=  sqlerrm || ' - GET_CERTIFICATIONSEARCHRESULT. There are invalid parameters.';
                    APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                        AS_MACHINEID =>  ls_MACHINEID,
                        AD_OPERATORID => ls_OPERATORID,
                        AD_DATERECORDED => sysdate,
                        AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT',
                        AX_RECORDDATA    => 'ps_SearchCriteria is invalid or ps_SearchType is invalid.',
                        AS_MESSAGECODE => to_char(sqlcode),
                        AS_MESSAGE       =>sqlerrm 
                      );
                      raise_application_error (-20006,ls_ErrorMsg); 
            when others then
                ls_ErrorMsg:=  sqlerrm || ' - GET_CERTIFICATIONSEARCHRESULT. An error have ocurred.(when others)'; 
                APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                    AS_MACHINEID =>  ls_MACHINEID,
                    AD_OPERATORID => ls_OPERATORID,
                    AD_DATERECORDED => sysdate,
                    AS_PROCESSNAME =>'  CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT',
                    AX_RECORDDATA    => 'An error have ocurred.(when others)',
                    AS_MESSAGECODE => to_char(sqlcode),
                    AS_MESSAGE       =>sqlerrm 
                  );
                  raise_application_error (-20007,ls_ErrorMsg);
   
   end GET_CERTIFICATIONSEARCHRESULT; 
   
    PROCEDURE Certificate_Save( pi_RetId out number,
                                pi_CertificateID in number,
                                pi_SKUId in number,
                                ps_SKU in VARCHAR2,
                                ps_CertificationTypeName in  varchar2,
                                ps_CERTIFICATENUMBER in  varchar2,
                                pd_DATESUBMITED in Date,
                                pc_ACTIVESTATUS in varchar2,
                                pd_DATEASSIGNED_EGI in Date,
                                pd_DateApproved_CEGI in Date,
                                pc_RENEWALREQUIRED_CGIN  in varchar2,
                                pc_SUPPLEMENTALREQUIRED_EI  in varchar2,
                                ps_SUPPLEMENTALNUMBER_EI in  varchar2,
                                ps_JOBREPORTNUMBER_CEN in  varchar2,
                                ps_EXTENSION_EN in  varchar2,
                                ps_SUPPLEMENTALMOLDSTAMPING_E in  varchar2,
                                ps_EMARKREFERENCE_I in  varchar2,
                                pd_EXPIRYDATE_I in Date,
                                ps_FAMILY_I in  varchar2,
                                ps_PRODUCTLOCATION_C in  varchar2,
                                ps_COUNTRYOFMANUFACTURE_N in  varchar2,
                                ps_CUSTOMER_N in  varchar2,
                                ps_CUSTOMERSPECIFIC_N in  varchar2,
                                ps_IMPORTER_N in  varchar2,
                                ps_IMPORTERADDRESS_N in  varchar2,
                                ps_IMPORTERREPRESENTATIVE_N in  varchar2,
                                ps_COUNTRYLOCATION_N in  varchar2,
                                ps_BATCHNUMBER_G  in  varchar2,
                                pd_SUPPLEMENTALDATEASSIGNED_E in Date,
                                pd_SUPPLEMENTALDATESUBMITTED_E in Date,
                                pd_SUPPLEMENTALDATEAPPROVED_E  in Date,
                                ps_UserName in varchar2) as 
                              
    --Exception variables
      le_ParametersNull exception;
      li_ParametersInvalid exception;
      -- link the exception to the error number
      pragma exception_init( le_ParametersNull,-20005);
      pragma exception_init( li_ParametersInvalid,-20006);
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      li_certificationTypeId integer;
      li_TotalSkuFound integer;  
      lc_Requeststatus char(1);
      li_CertificateID number;
      ls_CertificateNumberExists varchar2(1);
    Begin
    
        if ps_SKU is null or ps_CertificationTypeName is null then
          raise le_ParametersNull;
        end if;
         if ps_SKU = '' or ps_CertificationTypeName =  '' then
          raise li_ParametersInvalid;
        end if;
        
         if ps_UserName is null or ps_username = '' then
          ls_OperatorId:= ps_UserName;
        end if;
        
        --Gets the certification id based on the certification name
        SELECT CertificationTypeId into li_certificationTypeId
        FROM  CERTIFICATIONTYPE ce
        WHERE lower(ce.CERTIFICATIONTYPEName)=lower(ps_CertificationTypeName); 
        --Checks if the sku already exists on the table
        SELECT count(pce.skuid) as total into li_TotalSkuFound
        FROM  ProductCertificate pce 
                  inner join  CERTIFICATE ce On
                      pce.certificateid   = ce.certificateid And
                      pce.certificationtypeid = ce.certificationtypeid
        WHERE pce.skuid = pi_skuid and 
              ce.CertificationTypeId=li_certificationTypeId;
              
       if li_TotalSkuFound > 0 then     
                  UPDATE CERTIFICATE SET  
                        DATESUBMITED             = pd_DATESUBMITED,
                        ACTIVESTATUS             = lower(pc_ACTIVESTATUS),
                        DATEASSIGNED_EGI         = pd_DATEASSIGNED_EGI,
                        DateApproved_CEGI         = pd_DateApproved_CEGI,
                        RENEWALREQUIRED_CGIN     = lower(pc_RENEWALREQUIRED_CGIN) ,
                        SUPPLEMENTALREQUIRED_EI  = lower(pc_SUPPLEMENTALREQUIRED_EI) ,
                        SUPPLEMENTALNUMBER_EI    = ps_SUPPLEMENTALNUMBER_EI,
                        JOBREPORTNUMBER_CEN      = ps_JOBREPORTNUMBER_CEN,
                        EXTENSION_EN             = ps_EXTENSION_EN,
                        SUPPLEMENTALMOLDSTAMPING_E = ps_SUPPLEMENTALMOLDSTAMPING_E,
                        EMARKREFERENCE_I         = ps_EMARKREFERENCE_I,
                        EXPIRYDATE_I             = pd_EXPIRYDATE_I,
                        FAMILY_I                 = ps_FAMILY_I,
                        PRODUCTLOCATION_C        = ps_PRODUCTLOCATION_C,
                        COUNTRYOFMANUFACTURE_N   = ps_COUNTRYOFMANUFACTURE_N,
                        CUSTOMER_N               = ps_CUSTOMER_N  ,
                        CUSTOMERSPECIFIC_N       = lower(ps_CUSTOMERSPECIFIC_N),
                        IMPORTER_N               = ps_IMPORTER_N,
                        IMPORTERADDRESS_N        = ps_IMPORTERADDRESS_N,
                        IMPORTERREPRESENTATIVE_N = ps_IMPORTERREPRESENTATIVE_N,
                        COUNTRYLOCATION_N        = ps_COUNTRYLOCATION_N,
                        BATCHNUMBER_G            = ps_BATCHNUMBER_G,
                        SUPPLEMENTALDATEASSIGNED_E = pd_SUPPLEMENTALDATEASSIGNED_E ,
                        SUPPLEMENTALDATESUBMITTED_E = pd_SUPPLEMENTALDATESUBMITTED_E ,
                        SUPPLEMENTALDATEAPPROVED_E = pd_SUPPLEMENTALDATEAPPROVED_E,
                        MODIFIEDON               = SYSTIMESTAMP,
                        MODIFIEDBY               = ls_OperatorId,
                        CERTIFICATENUMBER        = upper(ps_CERTIFICATENUMBER)
                  WHERE 
                        CertificationTypeId = li_certificationTypeId And
                        CERTIFICATEID       = pi_CertificateID ;
               
               --Nom uses date submited the other use Date approved.
                if  li_certificationTypeId = 3 and pd_DATESUBMITED is not null then
                      lc_Requeststatus:='A';
                elsif li_certificationTypeId <> 3 and pd_DateApproved_CEGI is not null then
                      lc_Requeststatus:='A';
                else
                       lc_Requeststatus:='I';
                end if;                        
               
                UPDATE PRODUCTCOUNTRY SET 
                         CERTIFICATIONTYPEID = li_certificationTypeId,
                         REQUESTSTATUS = lc_Requeststatus
                WHERE SKUID = pi_skuid and
                      countryid in ( select co.countryid
                                     from CertificationType ct inner join Country co  on
                                                    ct.certificationTypeId = co.certificationTypeId
                                               inner join productcountry pc on 
                                                    co.countryid = pc.countryid
                                      where  ct.certificationTypeId = li_certificationTypeId );  
                                                       
                pi_RetId:= pi_CertificateID;        
                        
       else
                 
            INSERT INTO CERTIFICATE (                  
                  CertificationTypeID,
                  CERTIFICATENUMBER,
                  DATESUBMITED,
                  ACTIVESTATUS,
                  DATEASSIGNED_EGI ,
                  DateApproved_CEGI,
                  RENEWALREQUIRED_CGIN ,
                  SUPPLEMENTALREQUIRED_EI ,
                  SUPPLEMENTALNUMBER_EI,
                  JOBREPORTNUMBER_CEN ,
                  EXTENSION_EN ,
                  SUPPLEMENTALMOLDSTAMPING_E ,
                  EMARKREFERENCE_I,
                  EXPIRYDATE_I,
                  FAMILY_I,
                  PRODUCTLOCATION_C,
                  COUNTRYOFMANUFACTURE_N ,
                  CUSTOMER_N ,
                  CUSTOMERSPECIFIC_N,
                  IMPORTER_N,
                  IMPORTERADDRESS_N ,
                  IMPORTERREPRESENTATIVE_N,
                  COUNTRYLOCATION_N,
                  BATCHNUMBER_G,
                  SUPPLEMENTALDATEASSIGNED_E,
                  SUPPLEMENTALDATESUBMITTED_E,
                  SUPPLEMENTALDATEAPPROVED_E,
                  CREATEDBY,
                  createdon,
                  CertificateID
            )
            VALUES 
            (                           
                li_certificationTypeId,
                upper(ps_CERTIFICATENUMBER),
                pd_DATESUBMITED,
                lower(pc_ACTIVESTATUS),
                pd_DATEASSIGNED_EGI,
                pd_DateApproved_CEGI,
                lower(pc_RENEWALREQUIRED_CGIN) ,
                lower(pc_SUPPLEMENTALREQUIRED_EI) ,
                ps_SUPPLEMENTALNUMBER_EI,
                ps_JOBREPORTNUMBER_CEN,
                ps_EXTENSION_EN,
                ps_SUPPLEMENTALMOLDSTAMPING_E,
                ps_EMARKREFERENCE_I,
                pd_EXPIRYDATE_I,
                ps_FAMILY_I,
                ps_PRODUCTLOCATION_C,
                ps_COUNTRYOFMANUFACTURE_N,
                ps_CUSTOMER_N  ,
                lower(ps_CUSTOMERSPECIFIC_N),
                ps_IMPORTER_N,
                ps_IMPORTERADDRESS_N,
                ps_IMPORTERREPRESENTATIVE_N,
                ps_COUNTRYLOCATION_N,
                ps_BATCHNUMBER_G,
                pd_SUPPLEMENTALDATEASSIGNED_E ,
                pd_SUPPLEMENTALDATESUBMITTED_E ,
                pd_SUPPLEMENTALDATEAPPROVED_E,
                ls_operatorid,
                systimestamp,
                CERTIFICATEID_SEQ.nextVal
                );
                
                Select CERTIFICATEID_SEQ.CurrVal into li_CertificateID
                from Dual;
                
                 INSERT INTO  PRODUCTCERTIFICATE
                        (
                          SKUID,
                          CERTIFICATIONTYPEID,
                          CERTIFICATEID
                        )
                VALUES
                     (
                       pi_SKUId,
                       li_certificationTypeId,
                       li_CertificateID
                     );
                
                
                if  li_certificationTypeId = 3 and pd_DATESUBMITED is not null then
                      lc_Requeststatus:='A';
                elsif li_certificationTypeId <> 3 and pd_DateApproved_CEGI is not null then
                      lc_Requeststatus:='A';
                else
                       lc_Requeststatus:='I';
                end if;    
                                
                UPDATE PRODUCTCOUNTRY SET 
                         CERTIFICATIONTYPEID = li_certificationTypeId,
                         REQUESTSTATUS = lc_Requeststatus
                WHERE SKUID = pi_skuid and
                      countryid in ( select co.countryid
                                     from CertificationType ct inner join Country co  on
                                                    ct.certificationTypeId = co.certificationTypeId
                                               inner join productcountry pc on 
                                                    co.countryid = pc.countryid
                                      where  ct.certificationTypeId = li_certificationTypeId );
                pi_RetId:=   li_CertificateID;                                   
       end if ;
    
    
        EXCEPTION
                  when le_ParametersNull then
                     
                      ls_ErrorMsg:=  sqlerrm || '- Certificate_Save. There is at least one parameters null.';
                       APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                          ad_OPERATORID => ls_OperatorId,
                          AD_DATERECORDED  => sysdate,
                          AS_PROCESSNAME   => '  CERTIFICATION_CRUD.Certificate_Save',
                          AX_RECORDDATA    => 'There is at least one parameters null..',
                          AS_MESSAGECODE   => to_char(sqlcode),
                          AS_MESSAGE       => ls_ErrorMsg);
                          
                           raise_application_error (-20005,ls_ErrorMsg);
                      
                    when li_ParametersInvalid then
                         ls_ErrorMsg:=  sqlerrm || '- Certificate_Save. There is at least one parameters is invalid.';
                              APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                                  AS_MACHINEID =>  ls_MACHINEID,
                                  AD_OPERATORID => ls_OPERATORID,
                                  AD_DATERECORDED => sysdate,
                                  AS_PROCESSNAME =>'  CERTIFICATION_CRUD.Certificate_Save',
                                  AX_RECORDDATA    => 'ps_SearchCriteria is invalid or ps_SearchType is invalid.',
                                  AS_MESSAGECODE => to_char(sqlcode),
                                  AS_MESSAGE       =>sqlerrm 
                                );	      
                    raise_application_error (-20006,ls_ErrorMsg);
            
                   when others then
                      
                        ls_ErrorMsg:=  sqlerrm || '- Certificate_Save. An error have ocurred.(when others)';
                         APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                                ad_OPERATORID => ls_OperatorId,
                                AD_DATERECORDED  => sysdate,
                                AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.Certificate_Save',
                                AX_RECORDDATA    => 'An error have ocurred.(when others)',
                                AS_MESSAGECODE   => to_char(sqlcode),
                                AS_MESSAGE       =>ls_ErrorMsg);   
                     
                     raise_application_error (-20007,ls_ErrorMsg);
                      
    End Certificate_Save;
    
 PROCEDURE Certificate_Save_Imark(pi_RetId out number,
                                  pi_CertificateID in number,
                                  pi_SKUId in number,
                                  ps_SKU in VARCHAR2,
                                  ps_CertificationTypeName in  varchar2,
                                  ps_CERTIFICATENUMBER in  varchar2,
                                  pd_DATESUBMITED in Date,
                                  pc_ACTIVESTATUS in varchar2,
                                  pd_DATEASSIGNED_EGI in Date,
                                  pd_DateApproved_CEGI in Date,
                                  pc_RENEWALREQUIRED_CGIN  in varchar2,
                                  pc_SUPPLEMENTALREQUIRED_EI  in varchar2,
                                  ps_SUPPLEMENTALNUMBER_EI in  varchar2,
                                  ps_JOBREPORTNUMBER_CEN in  varchar2,
                                  ps_EXTENSION_EN in  varchar2,
                                  ps_SUPPLEMENTALMOLDSTAMPING_E in  varchar2,
                                  ps_EMARKREFERENCE_I in  varchar2,
                                  pd_EXPIRYDATE_I in Date,
                                  ps_FAMILY_I in  varchar2,
                                  ps_PRODUCTLOCATION_C in  varchar2,
                                  ps_COUNTRYOFMANUFACTURE_N in  varchar2,
                                  ps_CUSTOMER_N in  varchar2,
                                  ps_CUSTOMERSPECIFIC_N in  varchar2,
                                  ps_IMPORTER_N in  varchar2,
                                  ps_IMPORTERADDRESS_N in  varchar2,
                                  ps_IMPORTERREPRESENTATIVE_N in  varchar2,
                                  ps_COUNTRYLOCATION_N in  varchar2,
                                  ps_BATCHNUMBER_G  in  varchar2,
                                  pd_SUPPLEMENTALDATEASSIGNED_E in Date,
                                  pd_SUPPLEMENTALDATESUBMITTED_E in Date,
                                  pd_SUPPLEMENTALDATEAPPROVED_E  in Date,
                                  ps_UserName in varchar2) as
    --Exception variables
      le_ParametersNull exception;
      li_ParametersInvalid exception;
      -- link the exception to the error number
      pragma exception_init( le_ParametersNull,-20005);
      pragma exception_init( li_ParametersInvalid,-20006);
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      li_certificationTypeId integer;
      li_TotalSkuFound integer;  
      lc_Requeststatus char(1);
      
      Cursor SkuIdCursor (li_CertificationTypeID in number) is     
      SELECT pc.skuid
      FROM productcountry pc inner join country c on 
                       pc.countryid = c.countryid
      WHERE c.certificationtypeid = li_CertificationTypeID;  
    
      ls_CertificateExists varchar2(1);
      li_PCSkuID  PRODUCT.SKUID%TYPE;
      li_CertificateID number;
    Begin
    
        if ps_SKU is null or ps_CertificationTypeName is null then
          raise le_ParametersNull;
        end if;
         if ps_SKU = '' or ps_CertificationTypeName =  '' then
          raise li_ParametersInvalid;
        end if;
        
        if ps_UserName is not null or ps_username <> '' then
          ls_OperatorId:= ps_UserName;
        end if;
        
        
        --Gets the certification id based on the certification name
        SELECT CertificationTypeId into li_certificationTypeId
        FROM  CERTIFICATIONTYPE ce
        WHERE lower(ce.CERTIFICATIONTYPEName)=lower(ps_CertificationTypeName); 
       
        --Checks if the sku already exists on the table
        SELECT count(pce.skuid) as total into li_TotalSkuFound
        FROM  ProductCertificate pce 
                  inner join  CERTIFICATE ce On
                      pce.certificateid   = ce.certificateid And
                      pce.certificationtypeid = ce.certificationtypeid
        WHERE pce.skuid = pi_skuid and 
              ce.CertificationTypeId=li_certificationTypeId;      
              
              
       if li_TotalSkuFound > 0 then     
                  UPDATE CERTIFICATE SET  
                        DATESUBMITED             = pd_DATESUBMITED,
                        ACTIVESTATUS             = lower(pc_ACTIVESTATUS),
                        DATEASSIGNED_EGI         = pd_DATEASSIGNED_EGI,
                        DateApproved_CEGI         = pd_DateApproved_CEGI,
                        RENEWALREQUIRED_CGIN     = lower(pc_RENEWALREQUIRED_CGIN) ,
                        SUPPLEMENTALREQUIRED_EI  = lower(pc_SUPPLEMENTALREQUIRED_EI) ,
                        SUPPLEMENTALNUMBER_EI    = ps_SUPPLEMENTALNUMBER_EI,
                        JOBREPORTNUMBER_CEN      = ps_JOBREPORTNUMBER_CEN,
                        EXTENSION_EN             = ps_EXTENSION_EN,
                        SUPPLEMENTALMOLDSTAMPING_E = ps_SUPPLEMENTALMOLDSTAMPING_E,
                        EMARKREFERENCE_I         = ps_EMARKREFERENCE_I,
                        EXPIRYDATE_I             = pd_EXPIRYDATE_I,
                        FAMILY_I                 = ps_FAMILY_I,
                        PRODUCTLOCATION_C        = ps_PRODUCTLOCATION_C,
                        COUNTRYOFMANUFACTURE_N   = ps_COUNTRYOFMANUFACTURE_N,
                        CUSTOMER_N               = ps_CUSTOMER_N  ,
                        CUSTOMERSPECIFIC_N       = lower(ps_CUSTOMERSPECIFIC_N),
                        IMPORTER_N               = ps_IMPORTER_N,
                        IMPORTERADDRESS_N        = ps_IMPORTERADDRESS_N,
                        IMPORTERREPRESENTATIVE_N = ps_IMPORTERREPRESENTATIVE_N,
                        COUNTRYLOCATION_N        = ps_COUNTRYLOCATION_N,
                        BATCHNUMBER_G            = ps_BATCHNUMBER_G,
                        SUPPLEMENTALDATEASSIGNED_E = pd_SUPPLEMENTALDATEASSIGNED_E ,
                        SUPPLEMENTALDATESUBMITTED_E = pd_SUPPLEMENTALDATESUBMITTED_E ,
                        SUPPLEMENTALDATEAPPROVED_E = pd_SUPPLEMENTALDATEAPPROVED_E,
                        MODIFIEDON               = SYSTIMESTAMP,
                        MODIFIEDBY               = ls_OperatorId,
                        CERTIFICATENUMBER        = ps_CERTIFICATENUMBER
                  WHERE CertificationTypeId = li_certificationTypeId And
                        CertificateID       = pi_certificateid;
                        
                if pd_DateApproved_CEGI is not null  then
                   lc_Requeststatus:='A';
                else
                   lc_Requeststatus:='I';
                end if ;  
                UPDATE PRODUCTCOUNTRY SET 
                         CERTIFICATIONTYPEID = li_certificationTypeId,
                         REQUESTSTATUS = lc_Requeststatus
                WHERE SKUID in ( Select SKUID 
                                 FROM Certificate ce 
                                 Where lower(ce.CERTIFICATENUMBER) = lower(ps_CERTIFICATENUMBER) and
                                      ce.CertificationTypeId =li_certificationTypeId)  and 
                      countryid in ( select co.countryid
                                     from CertificationType ct inner join Country co  on
                                                    ct.certificationTypeId = co.certificationTypeId
                                               inner join productcountry pc on 
                                                    co.countryid = pc.countryid
                                      where  ct.certificationTypeId = li_certificationTypeId );  
                pi_RetId:=pi_certificateid;
                        
                        
       else
            Open SkuIdCursor(li_certificationTypeId);
            Loop
                fetch SkuIdCursor into li_PCSkuID;
                exit when SkuIdCursor%notfound;
                ls_CertificateExists := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATEEXISTS(
                                      PS_CERTIFICATENUMBER => ps_CERTIFICATENUMBER,
                                      PI_CERTIFICATIONTYPEID => li_certificationTypeId
                                    );
                                    
                if ls_CertificateExists = 'n' then            
                    INSERT INTO CERTIFICATE (                                                  
                                    CertificationTypeID,
                                    CERTIFICATENUMBER,
                                    DATESUBMITED,
                                    ACTIVESTATUS,
                                    DATEASSIGNED_EGI ,
                                    DateApproved_CEGI,
                                    RENEWALREQUIRED_CGIN ,
                                    SUPPLEMENTALREQUIRED_EI ,
                                    SUPPLEMENTALNUMBER_EI,
                                    JOBREPORTNUMBER_CEN ,
                                    EXTENSION_EN ,
                                    SUPPLEMENTALMOLDSTAMPING_E ,
                                    EMARKREFERENCE_I,
                                    EXPIRYDATE_I,
                                    FAMILY_I,
                                    PRODUCTLOCATION_C,
                                    COUNTRYOFMANUFACTURE_N ,
                                    CUSTOMER_N ,
                                    CUSTOMERSPECIFIC_N,
                                    IMPORTER_N,
                                    IMPORTERADDRESS_N ,
                                    IMPORTERREPRESENTATIVE_N,
                                    COUNTRYLOCATION_N,
                                    BATCHNUMBER_G,
                                    SUPPLEMENTALDATEASSIGNED_E,
                                    SUPPLEMENTALDATESUBMITTED_E,
                                    SUPPLEMENTALDATEAPPROVED_E,
                                    CREATEDBY,
                                    createdon,
                                    CertificateID
                          )
                          VALUES 
                          (                                        
                              li_certificationTypeId,
                              ps_CERTIFICATENUMBER,
                              pd_DATESUBMITED,
                              lower(pc_ACTIVESTATUS),
                              pd_DATEASSIGNED_EGI,
                              pd_DateApproved_CEGI,
                              lower(pc_RENEWALREQUIRED_CGIN) ,
                              lower(pc_SUPPLEMENTALREQUIRED_EI) ,
                              ps_SUPPLEMENTALNUMBER_EI,
                              ps_JOBREPORTNUMBER_CEN,
                              ps_EXTENSION_EN,
                              ps_SUPPLEMENTALMOLDSTAMPING_E,
                              ps_EMARKREFERENCE_I,
                              pd_EXPIRYDATE_I,
                              ps_FAMILY_I,
                              ps_PRODUCTLOCATION_C,
                              ps_COUNTRYOFMANUFACTURE_N,
                              ps_CUSTOMER_N  ,
                              lower(ps_CUSTOMERSPECIFIC_N),
                              ps_IMPORTER_N,
                              ps_IMPORTERADDRESS_N,
                              ps_IMPORTERREPRESENTATIVE_N,
                              ps_COUNTRYLOCATION_N,
                              ps_BATCHNUMBER_G,
                              pd_SUPPLEMENTALDATEASSIGNED_E ,
                              pd_SUPPLEMENTALDATESUBMITTED_E ,
                              pd_SUPPLEMENTALDATEAPPROVED_E,
                              ls_OperatorId,
                              systimestamp,
                              CERTIFICATEID_SEQ.nextVal
                              );
                end if;                              
                          
                Select CERTIFICATEID_SEQ.CurrVal into li_CertificateID
                from Dual;
                          
                INSERT INTO  PRODUCTCERTIFICATE
                        (
                          SKUID,
                          CERTIFICATIONTYPEID,
                          CertificateID
                        )
                VALUES
                     (
                       li_PCSkuID,
                       li_certificationTypeId,
                       li_CertificateID
                     );
           end loop;
          
				if  li_certificationTypeId = 3 and pd_DATESUBMITED is not null then
                      lc_Requeststatus:='A';
                elsif li_certificationTypeId <> 3 and pd_DateApproved_CEGI is not null then
                      lc_Requeststatus:='A';
                else
                       lc_Requeststatus:='I';
                end if;      
                                
                UPDATE PRODUCTCOUNTRY SET 
                         CERTIFICATIONTYPEID = li_certificationTypeId,
                         REQUESTSTATUS = lc_Requeststatus
                WHERE SKUID in ( Select SKUID 
                                 FROM Certificate ce 
                                 Where lower(ce.CERTIFICATENUMBER) = lower(ps_CERTIFICATENUMBER) and
                                      ce.CertificationTypeId =li_certificationTypeId)  and 
                      countryid in ( select co.countryid
                                     from CertificationType ct inner join Country co  on
                                                    ct.certificationTypeId = co.certificationTypeId
                                               inner join productcountry pc on 
                                                    co.countryid = pc.countryid
                                      where  ct.certificationTypeId = li_certificationTypeId ); 
                                                   
           pi_RetId:=li_CertificateID;     
       end if ;
    
    
        EXCEPTION
                  when le_ParametersNull then
                     
                      ls_ErrorMsg:=  sqlerrm || '- Certificate_Save_Imark. There is at least one parameters null.';
                       APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                          ad_OPERATORID => ls_OperatorId,
                          AD_DATERECORDED  => sysdate,
                          AS_PROCESSNAME   => '  CERTIFICATION_CRUD.Certificate_Save_Imark',
                          AX_RECORDDATA    => 'There is at least one parameters null..',
                          AS_MESSAGECODE   => to_char(sqlcode),
                          AS_MESSAGE       => ls_ErrorMsg);
                          raise_application_error (-20005,ls_ErrorMsg);
                      
                    when li_ParametersInvalid then
                               ls_ErrorMsg:=  sqlerrm || '- Certificate_Save_Imark. There is at least one parameters is invalid.';
                              APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(
                                  AS_MACHINEID =>  ls_MACHINEID,
                                  AD_OPERATORID => ls_OPERATORID,
                                  AD_DATERECORDED => systimestamp,
                                  AS_PROCESSNAME =>'  CERTIFICATION_CRUD.Certificate_Save_Imark',
                                  AX_RECORDDATA    => 'ps_SearchCriteria is invalid or ps_SearchType is invalid.',
                                  AS_MESSAGECODE => to_char(sqlcode),
                                  AS_MESSAGE       =>sqlerrm 
                                );	   
                  raise_application_error (-20006,ls_ErrorMsg);
            
                   when others then
                      
                        ls_ErrorMsg:=  sqlerrm || 'Certificate_Save_Imark. An error have ocurred.(when others)';
                         APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                                ad_OPERATORID => ls_OperatorId,
                                AD_DATERECORDED  => systimestamp,
                                AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.Certificate_Save_Imark',
                                AX_RECORDDATA    => 'An error have ocurred.(when others)',
                                AS_MESSAGECODE   => to_char(sqlcode),
                                AS_MESSAGE       =>ls_ErrorMsg); 
                        raise_application_error (sqlcode,ls_ErrorMsg);
                      
    End Certificate_Save_Imark;

   PROCEDURE GetCertificatesInfo(pc_retCursor out retCursor,
                                 pi_certificationTypeID in number,
                                 pi_SKUId in number,
                                 ps_TRExists out varchar2) as
     --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);   
      
      ls_CertificateNumber  CERTIFICATE.CERTIFICATENUMBER%type;
      ls_TRExists varchar2(1);
      li_TotalTR number;
   begin
        if  pi_certificationTypeID is null or pi_SKUId is null then
          raise li_ParametersAreNull;
        end if;
   
        if pi_certificationTypeID = 4 then 
        --Get the latest certificate number
          ls_CertificateNumber:= ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
        --Get certficate info based on last step
           Open pc_retCursor FOR
           SELECT 
                  ce.certificateid,
                  ce.CERTIFICATIONTYPEID,
                  PCE.SKUID ,                  
                  CE.CERTIFICATENUMBER,
                  DATESUBMITED,
                  ACTIVESTATUS,
                  DATEASSIGNED_EGI,
                  DATEAPPROVED_CEGI,
                  RENEWALREQUIRED_CGIN,
                  SUPPLEMENTALREQUIRED_EI,
                  SUPPLEMENTALNUMBER_EI,
                  JOBREPORTNUMBER_CEN,
                  EXTENSION_EN,
                  SUPPLEMENTALMOLDSTAMPING_E,
                  SUPPLEMENTALDATEASSIGNED_E,
                  SUPPLEMENTALDATESUBMITTED_E,
                  SUPPLEMENTALDATEAPPROVED_E,
                  EMARKREFERENCE_I,
                  EXPIRYDATE_I,
                  FAMILY_I,
                  PRODUCTLOCATION_C,
                  COUNTRYOFMANUFACTURE_N,
                  CUSTOMER_N,
                  CUSTOMERSPECIFIC_N,
                  IMPORTER_N,
                  IMPORTERADDRESS_N,
                  IMPORTERREPRESENTATIVE_N,
                  COUNTRYLOCATION_N,
                  BATCHNUMBER_G,                 
                  cer.certificationtypename     
            FROM  PRODUCTcERTIFICATE PCE 
                      INNER JOIN  CERTIFICATE ce ON
                          PCE.CERTIFICATEID       = CE.CERTIFICATEID AND
                          PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
                      INNER JOIN  CERTIFICATIONTYPE cer on
                                 ce.CertificationTypeId = cer.CertificationTypeId
            WHERE ce.CertificationTypeId = pi_certificationTypeID ANd
                  lower(ce.CERTIFICATENUMBER) = lower(ls_CertificateNumber);
                  
        else
        
            Open pc_retCursor FOR
            Select P.Skuid,
                   CE.*,
                   cer.certificationtypename
            FROM  Product p 
                     inner join  productcertificate pc on
                            p.skuid = pc.skuid
                     inner join  certificate ce on
                            PC.CERTIFICATEID       = CE.CERTIFICATEID AND
                            PC.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
                     inner join  CERTIFICATIONTYPE cer on
                          ce.CertificationTypeId = cer.CertificationTypeId
            WHERE p.skuid = pi_SKUid and 
                  pc.CertificationTypeId = pi_certificationTypeID ;
                   
        end if;   
        
        Select Count(m.measureid) into li_TotalTR
        from  PRODUCTCERTIFICATE pce 
                    inner join  Certificate ce on
                        pce.CERTIFICATEID       = ce.CERTIFICATEID And
                        pce.certificationtypeid = ce.certificationtypeid
                    inner join  measurehdr m on 
                        ce.CERTIFICATEID = m.CERTIFICATEID and
                        ce.certificationtypeid = m.certificationtypeid
         where pce.skuid = pi_SKUid and
               pce.certificationtypeid = pi_certificationtypeid;
               
         if li_TotalTR > 0 then
            ps_trexists:='y' ;
         else 
            ps_trexists:='n' ;
         end if;
            
                                  
       
    EXCEPTION
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || 'GetCertificatesInfo. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.GetCertificatesInfo',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            
            raise_application_error (-20005,ls_ErrorMsg);
            
         when others then
            
              ls_ErrorMsg:=  sqlerrm || '- GetCertificatesInfo. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.GetCertificatesInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);  
                      
               raise_application_error (-20005,ls_ErrorMsg);
               
   end GetCertificatesInfo;
   
   Procedure GetSimilarCertificateInfo(pc_retCursor out retCursor,ps_SKU in varchar2,pi_CertificationTypeID in Number) as
   --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);    
      
      li_SKUID  PRODUCT.SKUID%TYPE;
   
   begin
        if ps_SKU is null or pi_CertificationTypeID is null then
          raise li_ParametersAreNull;
        end if;
       
       
       li_SKUID:= ICS_COMMON_FUNCTIONS.GETLATESTSKUIDBYSKU(PS_SKU => ps_SKU );
       
        Open pc_retCursor FOR
           SELECT 
                  pce.CertificateId,
                  ce.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  DATESUBMITED,
                  ACTIVESTATUS,
                  DATEASSIGNED_EGI,
                  DateApproved_CEGI,
                  RENEWALREQUIRED_CGIN,
                  SUPPLEMENTALREQUIRED_EI,
                  SUPPLEMENTALNUMBER_EI,
                  JOBREPORTNUMBER_CEN,
                  EXTENSION_EN,
                  SUPPLEMENTALMOLDSTAMPING_E,
                  EMARKREFERENCE_I,
                  EXPIRYDATE_I,
                  FAMILY_I,
                  PRODUCTLOCATION_C,
                  COUNTRYOFMANUFACTURE_N,
                  CUSTOMER_N,
                  CUSTOMERSPECIFIC_N,
                  IMPORTER_N,
                  IMPORTERADDRESS_N,
                  IMPORTERREPRESENTATIVE_N,
                  COUNTRYLOCATION_N,
                  BATCHNUMBER_G ,
                  pCE.SKUID ,
                  cer.certificationtypename ,
                  SUPPLEMENTALDATEASSIGNED_E,
                  SUPPLEMENTALDATESUBMITTED_E,
                  SUPPLEMENTALDATEAPPROVED_E                  
            FROM  PRODUCTcERTIFICATE PCE 
                      INNER JOIN  CERTIFICATE ce ON
                          PCE.CERTIFICATEID       = CE.CERTIFICATEID AND
                          PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
                      inner join  CERTIFICATIONTYPE cer on
                          ce.CertificationTypeId = cer.CertificationTypeId
            WHERE pce.skuid = li_SKUID and                 
                  ce.CertificationTypeId = pi_certificationTypeID;
   
       EXCEPTION
            when li_ParametersAreNull then
                ls_ErrorMsg:=  sqlerrm || ' There is at least one parameters null.';
                 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                    ad_OPERATORID => ls_OperatorId,
                    AD_DATERECORDED  => sysdate,
                    AS_PROCESSNAME   => '  CERTIFICATION_CRUD.GetCertificatesInfo',
                    AX_RECORDDATA    => 'There is at least one parameters null..',
                    AS_MESSAGECODE   => to_char(sqlcode),
                    AS_MESSAGE       => ls_ErrorMsg);
                    raise_application_error (-20005,ls_ErrorMsg);
                
             when others then
                  ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
                   APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                          ad_OPERATORID => ls_OperatorId,
                          AD_DATERECORDED  => sysdate,
                          AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.GetCertificatesInfo',
                          AX_RECORDDATA    => 'An error have ocurred.(when others)',
                          AS_MESSAGECODE   => to_char(sqlcode),
                          AS_MESSAGE       =>ls_ErrorMsg);
                   raise_application_error (-20005,ls_ErrorMsg);       
                      
                          
   end GetSimilarCertificateInfo;
   
   Procedure GetDefaultValues(pc_retCursor out retCursor,
                              ps_Number out CERTIFICATE.CERTIFICATENUMBER%TYPE,
                              ps_TypeName in CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%Type,
                              pi_NumberID in CERTIFICATE.CERTIFICATEID%TYPE) as
   --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      li_CertificationTypeId  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE;
      li_CertificateId        CERTIFICATEDEFAULTVALUE.CERTIFICATEID%TYPE;
      
      ls_CertificateNumber    CERTIFICATE.CERTIFICATENUMBER%TYPE;
      
   begin 
         if  ps_TypeName is null then
          raise li_ParametersAreNull;
        end if;  
        
        li_CertificationTypeId:=ICS_COMMON_FUNCTIONS.GETCERTIFICATIONID(PS_CERTIFICATIONTYPENAME => ps_TypeName); 
   
        if  pi_NumberID > 0  then 
        
             Open pc_retCursor for
              SELECT 
                     df.fieldid,
                     df.FIELDNAME,
                     df.fieldtext,
                     df.CERTIFICATIONTYPEID,                     
                     cdv.FIELDVALUE,
                     cdv.certificateid
              FROM  DEFAULTFIELD df  inner join  CERTIFICATEDEFAULTVALUE cdv on 
                         df.fieldid = cdv.FIELDID and
                         df.certificationtypeid = cdv.CERTIFICATIONTYPEID                       
              where cdv.certificateid = pi_NumberID and 
                    cdv.CERTIFICATIONTYPEID = li_CertificationTypeId
                    Order by df.fieldid;
              
              Select ce.certificatenumber into ls_CertificateNumber
              from Certificate ce
              where ce.certificateid = pi_NumberID;
              
              ps_Number:=ls_CertificateNumber;
                    
        else
             Open pc_retCursor for
              SELECT 
                     df.fieldid,
                     DF.FIELDNAME,
                     df.fieldtext,
                     df.certificationtypeid,                       
                     CTDV.FIELDVALUE,
                     null as certificateid
              FROM  DEFAULTFIELD df 
                   inner join  CERTIFICATETYPEDEFAULTVALUE ctdv on 
                              df.certificationtypeid = ctdv.certificationtypeid and
                              df.fieldid = CTDV.FIELDID   
              where df.certificationtypeid = li_CertificationTypeId
              Order by DF.FIELDID;
             ps_Number:='';
        end if ;
        
   EXCEPTION
        when li_ParametersAreNull then
            ls_ErrorMsg:=  sqlerrm || '- GetDefaultValues. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.GetDefaultValues',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
                raise_application_error (-20005,ls_ErrorMsg);    
                
         when others then
            
              ls_ErrorMsg:=  sqlerrm || '- GetDefaultValues.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.GetDefaultValues',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                raise_application_error (-20007,ls_ErrorMsg);           
                      
   end GetDefaultValues;
   Procedure CertificateBasicInfo_Save(ps_SKU                in VARCHAR2,
                                      pi_CertificationTypeId in  number,
                                      ps_CertificateNumber   in  varchar2,
                                      ps_OperatorName        in  varchar2 ) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      li_SKUId  PRODUCT.SKUID%TYPE;
      ls_CertificateExists varchar2(1);
      ls_ProductExixts varchar2(1);
      li_CertificateID number;
      ls_CertificateContainsSku varchar2(1);
   begin
         if ps_sku is null or Pi_CERTIFICATIONTYPEID is null or ps_CertificateNumber is null then
            raise li_ParametersAreNull;
         end if;
         
         if ps_OperatorName is not null or ps_OperatorName <> '' then
            ls_OperatorId:=ps_OperatorName;
         end if;
         
         
         li_SKUId:= ICS_COMMON_FUNCTIONS.getLatestSkuIdBySKU(PS_SKU => PS_SKU);
         
          ls_CertificateExists := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATEEXISTS(
                                      PS_CERTIFICATENUMBER => ps_CertificateNumber,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeId
                                    );
                                    
             if ls_CertificateExists = 'n' then  
                 INSERT INTO  CERTIFICATE
                  (            
                    CERTIFICATIONTYPEID,
                    CERTIFICATENUMBER,
                    CREATEDBY ,
                    CERTIFICATEID
                  )
                  VALUES
                  (            
                    Pi_CERTIFICATIONTYPEID,
                    upper(ps_CERTIFICATENUMBER),
                    ls_OperatorId,
                    CERTIFICATEID_SEQ.NextVal
              );
              
               Select CERTIFICATEID_SEQ.CurrVal into li_CertificateID
              from Dual;
            else
                 li_CertificateID := ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(
                                            PS_CERTIFICATENUMBER => ps_CertificateNumber,
                                            PI_CERTIFICATIONTYPEID => pi_CertificationTypeId
                                          );
            end if;  
            
           ls_CertificateContainsSku:=ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATECONTAINSSKU(
                                            PI_SKUID => li_SKUId,
                                            PI_CERTIFICATIONTYPEID => Pi_CERTIFICATIONTYPEID,
                                            PI_CERTIFICATEID => li_CertificateID
                                          );
                                          
           if ls_CertificateContainsSku = 'n' then                                      
               INSERT INTO  PRODUCTCERTIFICATE
                      (                   
                        SKUID,
                        CERTIFICATIONTYPEID,
                        CERTIFICATEID
                      )
                      VALUES
                      (
                       li_SKUId,
                        Pi_CERTIFICATIONTYPEID,
                        li_CertificateID
                      );
          end if;
                  
          ls_ProductExixts := ICS_COMMON_FUNCTIONS.CHECKIFPRODUCTCOUNTRYEXISTS( PI_SKUID => li_SKUId );  
          
          if ls_ProductExixts='n' then            
              --Creates entries on product country in order to be able to search by this new certificate number that was created
              INSERT INTO PRODUCTCOUNTRY ( COUNTRYID, CERTIFICATIONTYPEID, REQUESTSTATUS, SKUID )
              Select co.countryid,Pi_CERTIFICATIONTYPEID,null,li_SKUId
              from certificationtype cet inner join country co on
                           cet.CertificationTypeId = co.CertificationTypeId
              where cet.CertificationTypeId =Pi_CERTIFICATIONTYPEID;
          end if;

    EXCEPTION
        when li_ParametersAreNull then
            ls_ErrorMsg:=  sqlerrm || ' - CertificateBasicInfo_Save. There are null parameters.';            
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.CertificateBasicInfo_Save',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),                
                AS_MESSAGE       => ls_ErrorMsg);
                raise_application_error (-20005,ls_ErrorMsg);
                
         when others then
            
              ls_ErrorMsg:=  sqlerrm || ' - CertificateBasicInfo_Save. An error have ocurred.(when others)';   
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.CertificateBasicInfo_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                     raise_application_error (-20007,ls_ErrorMsg); 

   end CertificateBasicInfo_Save;
   
  Procedure CertificateDefaultvalue_Save(pi_FieldvalueId in number,pi_CertificationTypeID in Number,ps_FieldValue in varchar2,ps_CertificateNumber in varchar2) as 
     --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      ls_DefaultValueExist varchar2(1);
      li_CertificateID number;
      ls_CertificateNumber CERTIFICATE.CERTIFICATENUMBER%type;
  begin
        if pi_FieldvalueId is null or pi_CertificationTypeID is null  or ps_CertificateNumber is null then
          raise li_ParametersAreNull;
        end if;
        
        if pi_CertificationTypeID = 4 then
            ls_CertificateNumber:=ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
            li_CertificateID:=ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(
                                      PS_CERTIFICATENUMBER => ls_CertificateNumber,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                    );
        else
            li_CertificateID:=ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(
                                      PS_CERTIFICATENUMBER => ps_CertificateNumber,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                    );
        end if;
        
        ls_DefaultValueExist := CERTIFICATION_CRUD.CHECKIFCERTIFICATEDFEXIST(
                                              PI_FIELDID => pi_FieldvalueId,
                                              PI_CERTIFICATIONTYPEID => pi_CertificationTypeID,
                                              pi_CertificateID => li_CertificateID
                                            );
        if ls_DefaultValueExist = 'y' then
        
               UPDATE  CERTIFICATEDEFAULTVALUE SET 
                         FIELDVALUE = ps_FieldValue,
                         MODIFIEDON = SYSDATE
               WHERE FIELDID             = pi_FieldvalueId And
                     CERTIFICATIONTYPEID = PI_CERTIFICATIONTYPEID and
                     CERTIFICATEID   = li_CertificateID;     
        else
              INSERT INTO   CERTIFICATEDEFAULTVALUE (FIELDID,CERTIFICATIONTYPEID,CERTIFICATEID,FIELDVALUE)
              VALUES(pi_FieldvalueId,pi_CertificationTypeID,li_CertificateID,ps_FieldValue);    
        end if;
  
  
   EXCEPTION
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- CertificateDefaultvalue_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.CertificateDefaultvalue_Save',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
                raise_application_error (-20005,ls_ErrorMsg);
                
         when others then
            
              ls_ErrorMsg:=  sqlerrm || '- CertificateDefaultvalue_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.CertificateDefaultvalue_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);     
  
  end CertificateDefaultvalue_Save;
  

 Procedure CertificTypeDefaultValue_Save(pi_FieldvalueId in number,pi_CertificationTypeID in Number,ps_FieldValue in varchar2) as 
      --This procedure should Update the Field value on the CERTIFICATETYPEDEFAULTVALUE table.
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
  begin
         if pi_FieldvalueId is null or pi_CertificationTypeID is null Then
          raise li_ParametersAreNull;
        end if;
        
        UPDATE  CERTIFICATETYPEDEFAULTVALUE SET
        FIELDVALUE =  ps_FieldValue,
        MODIFIEDON =  SYSDATE
        WHERE  FIELDID = pi_FieldvalueId And
               CERTIFICATIONTYPEID = pi_CertificationTypeID;
               
 
   EXCEPTION
        when li_ParametersAreNull then
            ls_ErrorMsg:=  sqlerrm || '- CertificTypeDefaultValue_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.CertificTypeDefaultValue_Save',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
              raise_application_error (-20005,ls_ErrorMsg);  
                
         when others then
              ls_ErrorMsg:=  sqlerrm ||  '- CertificTypeDefaultValue_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.CertificTypeDefaultValue_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                      raise_application_error (-20007,ls_ErrorMsg);
                      
  end CertificTypeDefaultValue_Save;
  
  Function CheckIfCertificateDFExist(pi_FieldID in number,pi_certificationTypeID in Number,pi_CertificateID in Number) return varchar2 as
  --This function should check if the default value for the certificate exists...
    li_Total integer;
    ls_DefaultValueExists varchar2(1) := 'n';
  begin
        Select Count(1) into li_Total
        from   CERTIFICATEDEFAULTVALUE 
        WHERE CERTIFICATIONTYPEID=pi_certificationTypeID aND
              fieldid = pi_FieldID AND
              certificateID = pi_CertificateID;
              
        if li_Total > 0 then
            ls_DefaultValueExists:='y';            
        end if;
        
        return ls_DefaultValueExists;
        
  end CheckIfCertificateDFExist;
   
Procedure IMARKCERTIFICATE_RENEW(pi_NewID out number, ps_CertOld in varchar2,ps_CertNew in varchar2 , ps_OperatorName in varchar2) as 
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);   
      
      li_NewCertificateExists exception;
       pragma exception_init( li_NewCertificateExists,-20010); 
     
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
     
      --cURSOR THAT GETS THE SKUID`S ATTACHED TO THAT SPECIFIC CERTIFICATE NUMBER
     Cursor Cursor_ProdIDs(ls_CertificateNumber varchar2) is     
      Select pce.skuid, p.sku
      FROM  Certificate ce 
              inner join  ProductCertificate pce on
                    ce.CERTIFICATEID       = pce.CERTIFICATEID and
                    ce.certificationtypeid = pce.certificationtypeid 
              inner join  Product  p on 
                    pce.skuid = p.skuid
      WHERE ce.certificatenumber   = ls_CertificateNumber And
            ce.certificationtypeid = 4  ;  --Imark Cert.TypeID   
      
      
      Cursor Cursor_ProdCountry is
      Select pc.countryid,PC.REQUESTSTATUS
      from productcountry pc
      where pc.certificationtypeid = 4;
      
      
      li_OldSkuID  PRODUCT.SKUID%TYPE;
      li_CurrSkuID  PRODUCT.SKUID%TYPE;      
      
      ls_Sku  PRODUCT.SKU%TYPE;
      
      li_CertificationTypeID  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE;
      
      ls_CertificateExists varchar2(1);
      
      li_REQUESTSTATUS  PRODUCTCOUNTRY.REQUESTSTATUS%TYPE;
      li_countryid  PRODUCTCOUNTRY.COUNTRYID%TYPE;
      li_CertificateID number;
  
   begin
         if ps_CertOld is null or ps_CertNew is null then
          raise li_ParametersAreNull;
        end if ;
          
        if ps_OperatorName is not null or ps_OperatorName <> '' then 
            ls_OperatorId:=ps_OperatorName;
        end if;
        
        ls_CertificateExists := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATEEXISTS(
                                      PS_CERTIFICATENUMBER => ps_CertNew,
                                      PI_CERTIFICATIONTYPEID => 4
                                    );
                                    
         if ls_CertificateExists = 'n' then  
            INSERT INTO  CERTIFICATE
            (            
               CERTIFICATIONTYPEID,
               CERTIFICATENUMBER,
               CREATEDBY,
               CERTIFICATEID
            )
            VALUES
            (           
              4,
              ps_CertNew,
              ls_OperatorId,
              CERTIFICATEID_SEQ.NextVal
            );
                            
             Select CERTIFICATEID_SEQ.CurrVal into li_CertificateID
             from Dual;
             
             pi_NewID:=li_CertificateID;
             
         else
              raise li_NewCertificateExists;
         end if;  
        
        
        open Cursor_ProdIDs(ps_CertOld);
        open Cursor_ProdCountry; 
        
        LOOP
              FETCH Cursor_ProdIDs INTO li_OldSkuID,ls_Sku;
              EXIT  When Cursor_ProdIDs%notfound;  
                  --Create a new product
              INSERT INTO PRODUCT
                          (
                            SKUID,
                            SKU,
                            BRANDCODE,
                            TIRETYPEID,
                            NPRID,
                            SIZESTAMP,
                            DISCONTINUEDDATE,
                            SPECNUMBER,
                            SPEEDRATING,
                            SINGLOADINDEX,
                            DUALLOADINDEX,
                            BELTEDRADIALYN,
                            TUBELESSYN,
                            REINFORCEDYN,
                            EXTRALOADYN,
                            UTQGTREADWEAR,
                            UTQGTRACTION,
                            UTQGTEMP,
                            MUDSNOWYN,
                            RIMDIAMETER,
                            SERIALDATE,
                            BRANDDESC,
                            LOADRANGE,
                            MEARIMWIDTH,
                            REGROOVABLEIND,
                            PLANTPRODUCED,
                            MOSTRECENTTESTDATE,
                            IMARK,
                            CREATEDBY,                           
                            MODIFIEDBY,                          
                            INFORMENUMBER,
                            FECHADATE,
                            TREADPATTERN,
                            SPECIALPROTECTIVEBAND,
                            NOMINALTIREWIDTH,
                            ASPECTRADIO,
                            TREADWEARINDICATORS,
                            NAMEOFMANUFACTURER,
                            FAMILY,
                            DOTSERIALNUMBER
                          )
                SELECT SKUID_SEQ.nextval,
                      SKU,
                      BRANDCODE,
                      TIRETYPEID,
                      NPRID,
                      SIZESTAMP,
                      DISCONTINUEDDATE,
                      SPECNUMBER,
                      SPEEDRATING,
                      SINGLOADINDEX,
                      DUALLOADINDEX,
                      BELTEDRADIALYN,
                      TUBELESSYN,
                      REINFORCEDYN,
                      EXTRALOADYN,
                      UTQGTREADWEAR,
                      UTQGTRACTION,
                      UTQGTEMP,
                      MUDSNOWYN,
                      RIMDIAMETER,
                      SERIALDATE,
                      BRANDDESC,
                      LOADRANGE,
                      MEARIMWIDTH,
                      REGROOVABLEIND,
                      PLANTPRODUCED,
                      MOSTRECENTTESTDATE,
                      IMARK,
                      ls_OperatorId,                  
                      ls_OperatorId,
                      INFORMENUMBER,
                      FECHADATE,
                      TREADPATTERN,
                      SPECIALPROTECTIVEBAND,
                      NOMINALTIREWIDTH,
                      ASPECTRADIO,
                      TREADWEARINDICATORS,
                      NAMEOFMANUFACTURER,
                      FAMILY,
                      DOTSERIALNUMBER
                FROM PRODUCT 
                WHERE skuid = li_OldSkuID;
                
                Select Max(Skuid) into li_CurrSkuID
                From Product
                Where SKU=(Select SKU from product where skuid=li_OldSkuID);               
                
                 --Creates a new record on Product country to complete the refresh
                 FETCH Cursor_ProdCountry INTO li_countryid,li_REQUESTSTATUS;
                 
                   --Delete from product country
                 delete from productcountry 
                 where skuid = li_OldSkuID And
                       CountryID =li_countryid ;
                 
                 
                 INSERT INTO PRODUCTCOUNTRY (
                                              COUNTRYID,
                                              CERTIFICATIONTYPEID,
                                              REQUESTSTATUS,
                                              SKUID,
                                              CREATEDBY,
                                              MODIFIEDBY                                              
                                            )
                                            VALUES
                                            (
                                              li_countryid,
                                              4,
                                              li_REQUESTSTATUS,
                                              li_CurrSkuID,
                                              ps_OperatorName,                                             
                                              ps_OperatorName                                             
                                            );
         
                 INSERT INTO  PRODUCTCERTIFICATE
                        (
                          SKUID,
                          CERTIFICATIONTYPEID,
                          CERTIFICATEID
                        )
                        VALUES
                        (
                          li_CurrSkuID,
                          4,
                          li_CertificateID
                  );
                
        end loop;       
        
        
        close Cursor_ProdIDs;
        close Cursor_ProdCountry;
      
        
     EXCEPTION
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || 'Renew_Imark_Certificate. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.RenewImarkCertificate',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
             raise_application_error (-20005,ls_ErrorMsg);
        
         when li_NewCertificateExists then           
            ls_ErrorMsg:=  sqlerrm || 'Renew_Imark_Certificate. New Certificate number Exists...';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.RenewImarkCertificate',
                AX_RECORDDATA    => 'New Certificate number Exists...',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
             raise_application_error (-20010,ls_ErrorMsg);
            
         when others then
            
              ls_ErrorMsg:=  sqlerrm || 'Renew_Imark_Certificate.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.RenewImarkCertificate',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20005,ls_ErrorMsg);
   
   end ImarkCertificate_Renew;
   
   
   Procedure RenewImarkCertificate(ps_OldCertificateNumber in varchar2,ps_NewCertificateNumber in varchar2, ps_UserID in varchar2) as
    ls_OperatorId VARCHAR2(50):='ICSDEV';
    /*
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
     
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
     
      --cURSOR THAT GETS THE SKUID`S ATTACHED TO THAT SPECIFIC CERTIFICATE NUMBER
      --Cursor Cursor_ProdIDs(ls_CertificateNumber in  CERTIFICATE.CERTIFICATENUMBER%TYPE) is
     -- Cursor Cursor_ProdIDs(ls_CertificateNumber varchar2) is
      Cursor Cursor_ProdIDs is
      Select pce.skuid, p.sku
      FROM  Certificate ce 
              inner join  ProductCertificate pce on
                    ce.certificatenumber   = pce.certificatenumber and
                    ce.certificationtypeid = pce.certificationtypeid 
              inner join  Product  p on 
                    pce.skuid = p.skuid;
    --  WHERE ce.certificatenumber = ls_CertificateNumber;     
      
      
      Cursor Cursor_ProdCountry is
      Select pc.countryid,PC.REQUESTSTATUS
      from productcountry pc
      where pc.certificationtypeid = 4;
      
      
      li_OldSkuID  PRODUCT.SKUID%TYPE;
      li_CurrSkuID  PRODUCT.SKUID%TYPE;      
      
      ls_Sku  PRODUCT.SKU%TYPE;
      li_CertificationTypeID  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE;
      
      ls_CertificateExists varchar2(1);
      
      li_REQUESTSTATUS  PRODUCTCOUNTRY.REQUESTSTATUS%TYPE;
      li_countryid  PRODUCTCOUNTRY.COUNTRYID%TYPE;
      
     */
      
   begin
   ls_OperatorId:='Andre';
       
/*
        if PS_OLDCERTIFICATENUMBER is null or ps_newcertificatenumber is null then
          raise li_ParametersAreNull;
        end if ;
           /*
        if PS_USERID is not null or PS_USERID <> '' then 
            ls_OperatorId:=PS_USERID;
        end if;
        
      
         li_CertificationTypeID := ICS_COMMON_FUNCTIONS.GETCERTIFICATIONID(
                                    PS_CERTIFICATIONTYPENAME => 'Imark' );
        
         
        --1)Find skus for imark cert to be renewed
        --2)copy skus
        --3)Create New Certificate with the new skus
        --4)Replace product country info
          OPEN Cursor_ProdIDs(ps_OldCertificateNumber);
          Open Cursor_ProdCountry;
          Close Cursor_ProdIDs;
          Close Cursor_ProdCountry;
                      */   /* 
          LOOP
              FETCH Cursor_ProdIDs INTO li_OldSkuID,ls_Sku;
              EXIT  When Cursor_ProdIDs%notfound;              
              --Create a new product
              INSERT INTO PRODUCT
                          (
                            SKUID,
                            SKU,
                            BRANDCODE,
                            TIRETYPEID,
                            NPRID,
                            SIZESTAMP,
                            DISCONTINUEDDATE,
                            SPECNUMBER,
                            SPEEDRATING,
                            SINGLOADINDEX,
                            DUALLOADINDEX,
                            BELTEDRADIALYN,
                            TUBELESSYN,
                            REINFORCEDYN,
                            EXTRALOADYN,
                            MEASURINGRIM,
                            UTQGTREADWEAR,
                            UTQGTRACTION,
                            UTQGTEMP,
                            MUDSNOWYN,
                            RIMDIAMETER,
                            SERIALDATE,
                            BRANDDESC,
                            LOADRANGE,
                            MEARIMWIDTH,
                            REGROOVABLEIND,
                            PLANTPRODUCED,
                            MOSTRECENTTESTDATE,
                            IMARK,
                            CREATEDBY,
                            MODIFIEDBY
                          )
                SELECT SKUID_SEQ.nextval,
                  SKU,
                  BRANDCODE,
                  TIRETYPEID,
                  NPRID,
                  SIZESTAMP,
                  DISCONTINUEDDATE,
                  SPECNUMBER,
                  SPEEDRATING,
                  SINGLOADINDEX,
                  DUALLOADINDEX,
                  BELTEDRADIALYN,
                  TUBELESSYN,
                  REINFORCEDYN,
                  EXTRALOADYN,
                  MEASURINGRIM,
                  UTQGTREADWEAR,
                  UTQGTRACTION,
                  UTQGTEMP,
                  MUDSNOWYN,
                  RIMDIAMETER,
                  SERIALDATE,
                  BRANDDESC,
                  LOADRANGE,
                  MEARIMWIDTH,
                  REGROOVABLEIND,
                  PLANTPRODUCED,
                  MOSTRECENTTESTDATE,
                  IMARK,
                  ls_OperatorId,                  
                  ls_OperatorId                 
                FROM PRODUCT 
                WHERE skuid = li_OldSkuID;
                
                Select Max(Skuid) into li_CurrSkuID
                From Product
                Where SKU=(Select SKU from product where skuid=li_OldSkuID);
                
                             
                  --Delete from product country
                 delete from productcountry where skuid = li_OldSkuID;
                 --Creates a new record on Product country to complete the refresh
                 FETCH Cursor_ProdCountry INTO li_countryid,li_REQUESTSTATUS;
                 INSERT INTO PRODUCTCOUNTRY (
                                              COUNTRYID,
                                              CERTIFICATIONTYPEID,
                                              REQUESTSTATUS,
                                              SKUID,
                                              CREATEDBY,
                                              MODIFIEDBY                                              
                                            )
                                            VALUES
                                            (
                                              li_countryid,
                                              4,
                                              li_REQUESTSTATUS,
                                              li_CurrSkuID,
                                              PS_USERID,                                             
                                              PS_USERID                                             
                                            );
                
                  ls_CertificateExists := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATEEXISTS(
                                      PS_CERTIFICATENUMBER => ps_NewCertificateNumber,
                                      PI_CERTIFICATIONTYPEID => 4
                                    );
                                    
                   if ls_CertificateExists = 'n' then  
                           INSERT INTO  CERTIFICATE
                            (            
                              CERTIFICATIONTYPEID,
                              CERTIFICATENUMBER,
                              CREATEDBY            
                            )
                            VALUES
                            (            
                              4,
                              ps_NewCertificateNumber,
                              ls_OperatorId
                            );
                  end if;  
         
         
                 INSERT INTO  PRODUCTCERTIFICATE
                        (
                          SKUID,
                          CERTIFICATIONTYPEID,
                          CERTIFICATENUMBER
                        )
                        VALUES
                        (
                          li_CurrSkuID,
                          4,
                          ps_NewCertificateNumber
                  );
                 
          end loop; 
          --close Cursor_ProdIDs;
         -- close Cursor_ProdCountry;
        
   EXCEPTION
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || 'Renew_Imark_Certificate. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.RenewImarkCertificate',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
             raise_application_error (-20005,ls_ErrorMsg);
                
         when others then
            
              ls_ErrorMsg:=  sqlerrm || 'Renew_Imark_Certificate.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.RenewImarkCertificate',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20005,ls_ErrorMsg);
     */
   end RenewImarkCertificate;
   
 Procedure AddNewSkusToImarkCertificate(pi_Skuid in number,pi_countryID in number) as
 
 --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
     
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      ls_SkuBelongsTOCertificate varchar(1);
      ls_LastImarkCertificate varchar2(20);
      ld_SubmitedDate Timestamp;
      ls_RequestStatus varchar2(1);
      li_CertificateID number;
      
 begin
      ls_SkuBelongsTOCertificate:=ICS_COMMON_FUNCTIONS.CHECKIFSKUBELONGSTOCERTIFICATE(
                                      PI_SKUID => PI_SKUID,
                                      PI_CERTIFICATIONTYPEID => 4 );
                                      
      ls_LastImarkCertificate:= ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();  
      
      li_CertificateID := ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(
                                PS_CERTIFICATENUMBER => ls_LastImarkCertificate,
                                PI_CERTIFICATIONTYPEID => 4 );     
                 
                                    
      if ls_SkuBelongsTOCertificate = 'n' then
      --Insert on the
      INSERT INTO ICS.PRODUCTCERTIFICATE
            (
              SKUID,
              CERTIFICATIONTYPEID,
              CERTIFICATEID
            )
            VALUES
            (
              PI_SKUID,
              4,
              li_CertificateID
            );
            
            Select ce.dateapproved_cegi into ld_SubmitedDate
            from Certificate ce
            where ce.certificatenumber = ls_LastImarkCertificate And
                  ce.certificationtypeid=4;
            
            if   ld_SubmitedDate is null then
            ls_RequestStatus:='I';
            else
            ls_RequestStatus:='A';
            end if;
            
--TODO:update product country with same status  as the others.
            update productcountry set 
                     REQUESTSTATUS = ls_RequestStatus,
                     CERTIFICATIONTYPEID =4
            where skuid= PI_SKUID and
                  countryid= pi_countryID;
      end if;
        
 
 end AddNewSkusToImarkCertificate;
 
 Procedure GetLatestImarkCertifNumber(ps_certificateNumber out varchar2) as
 
 ls_CertificateNumber varchar2(20);
    --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
 begin
       ls_CertificateNumber:= ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
       
       if ls_CertificateNumber <> 'NotFound' then 
            ps_certificateNumber:=ls_CertificateNumber;
       else
            ps_certificateNumber:='';
       end if;
 
  EXCEPTION     
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'GetLatestImarkCertifNumber.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.GetLatestImarkCertifNumber',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
 
 end GetLatestImarkCertifNumber;
 
 procedure AddCustomer(pi_skuid in number,
                       ps_CUSTOMER in varchar2,
                       ps_IMPORTER in varchar2,
                       ps_IMPORTERREPRESENTATIVE in varchar2,
                       ps_IMPORTERADDRESS in varchar2,
                       ps_COUNTRYLOCATION in varchar2) as 
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
     
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);    
      
      li_TotalCustomers number;
 begin
        if pi_skuid is null then
          raise li_ParametersAreNull;
        end if;
        
        Select Count(1) into li_TotalCustomers
        FRom customer c
        where c.skuid = pi_skuid And
              c.customer = ps_CUSTOMER;
              
        If   li_TotalCustomers > 0 then 
            UPDATE CUSTOMER C SET 
                  IMPORTER = ps_IMPORTER,
                  IMPORTERREPRESENTATIVE = ps_IMPORTERREPRESENTATIVE,
                  IMPORTERADDRESS = ps_IMPORTERADDRESS,
                  COUNTRYLOCATION = ps_COUNTRYLOCATION
            where c.skuid = pi_skuid And
              c.customer = ps_CUSTOMER;            
        else
            INSERT INTO CUSTOMER
            (
              CUSTOMER,
              IMPORTER,
              IMPORTERREPRESENTATIVE,
              IMPORTERADDRESS,
              COUNTRYLOCATION,
              SKUID
            )
            VALUES
            (
              ps_CUSTOMER,
              ps_IMPORTER,
              ps_IMPORTERREPRESENTATIVE,
              ps_IMPORTERADDRESS,
              ps_COUNTRYLOCATION,
              pi_skuid
            );
        end if;

 
 EXCEPTION     
         when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || 'AddCustomer. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.AddCustomer',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
             raise_application_error (-20005,ls_ErrorMsg);
             
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'AddCustomer.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.AddCustomer',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
 end AddCustomer;
 
  procedure GetCustomerInfo( pc_RetCustomer out retcursor,
                             pi_skuid in number,
                             ps_CUSTOMER in varchar2) as 
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
     
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                      
 begin
        if pi_skuid is null or ps_CUSTOMER is null then
          raise li_ParametersAreNull;
        end if;
        
        
        Open pc_RetCustomer for
        SELECT CUSTOMER,
              IMPORTER,
              IMPORTERREPRESENTATIVE,
              IMPORTERADDRESS,
              COUNTRYLOCATION,
              SKUID
        FROM CUSTOMER 
        Where lower(CUSTOMER) = lower(ps_CUSTOMER) and
              SKUID=pi_skuid;

 
 EXCEPTION     
         when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || 'GetCustomerInfo. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.GetCustomerInfo',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
             raise_application_error (-20005,ls_ErrorMsg);
             
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'GetCustomerInfo.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.GetCustomerInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
 end GetCustomerInfo;
 
  procedure GetCustomerInfoBySKU( pc_RetCustomer out retcursor,pi_skuid in number) as 
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
     
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                      
 begin
        if pi_skuid is null then
          raise li_ParametersAreNull;
        end if;
        
        
        Open pc_RetCustomer for
        SELECT CUSTOMER,
              IMPORTER,
              IMPORTERREPRESENTATIVE,
              IMPORTERADDRESS,
              COUNTRYLOCATION,
              SKUID
        FROM CUSTOMER 
        Where SKUID=pi_skuid;

 
 EXCEPTION     
         when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || 'GetCustomerInfoBySKU. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => '  CERTIFICATION_CRUD.GetCustomerInfoBySKU',
                AX_RECORDDATA    => 'There is at least one parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
             raise_application_error (-20005,ls_ErrorMsg);
             
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'GetCustomerInfoBySKU.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>'  CERTIFICATION_CRUD.GetCustomerInfoBySKU',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
 end GetCustomerInfoBySKU;
   
end certification_crud;
/
