CREATE OR REPLACE package reports_package as 
  Type retCursor is ref cursor; 
  procedure GetEmarkReportPassengerInfo(pc_Certificate out retCursor,
                                   pc_Brand out retCursor,
                                   pc_Product out retCursor,
                                   pc_CertificateDfValue out retcursor,
                                   ps_certificateNumber in varchar2,
                                   ps_extension in varchar2,
                                   pi_certificationTypeID in number,
                                   ps_Operatorid in varchar2,
                                   pi_TireTypeID in number
                                   );
     procedure GetImarkReportPassengerInfo(pc_Certificate out retCursor,
                                           pc_Brand out retCursor,
                                           pc_Product out retCursor,
                                           pc_CertificateDfValue out retcursor,
                                           pc_ProdBrandList out retcursor);
                       
    procedure GetImarkSamplingAndTestsInfo(pc_Certificate out retCursor,
                                           pc_Product out retCursor,
                                           pc_MeasureHdr out retCursor,
                                           pc_MeasureDtl out retCursor,
                                           pc_TreadWearHdr out retCursor,
                                           pc_TreadWearDtl out retCursor,
                                           pc_HighSpeedHdr out retCursor,
                                           pc_HighSpeedDtl out retCursor,
                                           pc_CertificateDfValue out retcursor
                                          );                   
                       
    procedure GetCCCSequentialReportInfo(pc_Certificate out retCursor,
                                         pc_Brand out retCursor,
                                         pc_Product out retCursor,
                                         pc_CertificateDfValue out retcursor,
                                         ps_certificateNumber in varchar2,
                                         ps_extension in varchar2,
                                         pi_certificationTypeID in number,
                                         ps_Operatorid in varchar2
                                        );                   
                                       
                                 
    procedure GetGSOPassengerReport(   pc_Certificate out retCursor,
                                       pc_Brand out retCursor,
                                       pc_SkuList out retCursor,
                                       pc_Product out retCursor,
                                       pc_CertificateDfValue out retcursor,
                                       pc_MeasureHDR out retcursor,
                                       pc_PlungerHDR out retcursor,
                                       pc_beadunseathdr out retcursor,
                                       pc_treadwearhdr out retcursor,
                                       pc_endurance out retcursor,
                                       pc_highspeedhdr out retcursor,
                                       ps_certificateNumber in varchar2,
                                       ps_extension in varchar2,
                                       pi_certificationTypeID in number,
                                       ps_Operatorid in varchar2,
                                       pi_TireTypeId in number
                                       );                                   
                                       
  PROCEDURE GetCertificateReportInfoBySKU(  pc_Product       out retCursor,
                                             pc_Certificate   out retCursor,
                                             PC_TESTREFERENCE out retCursor,
                                             ps_sku           in varchar2,
                                             ps_Operatorid    in varchar2)  ;  
                                     
 procedure GetImarkCertificationInfo(pc_ImarkCertification out retcursor,pd_DateSearchCriteria in Timestamp) ;
  
 procedure GetEmarkCertificationInfo(pc_EmarkCertification out retcursor,
                                      pc_Product out retcursor); 

 Procedure GetEmarkPassengerWithTR( ps_CertificateNumber in varchar2,
                                     pi_tiretypeid        in number,
                                     pc_CertificateDfValue  out retCursor,
                                     pc_CertificateInfo   out retCursor,
                                     pc_Product           out retcursor, 
                                     pc_MeasureHDR        out retCursor,
                                     pc_measureDtl        out retCursor,
                                     pc_BEADUNSEATHDR     out retCursor,
                                     pc_BEADUNSEATDTL     out retCursor,
                                     pc_PLUNGERHDR        out retCursor,
                                     pc_PLUNGERDTL        out retCursor,
                                     pc_TREADWEARHDR      out retCursor,
                                     pc_TREADWEARDTL      out retCursor,
                                     pc_ENDURANCEHDR      out retCursor,
                                     pc_ENDURANCEDTL      out retCursor,
                                     pc_HIGHSPEEDHDR      out retCursor,
                                     pc_HIGHSPEEDDTL      out retCursor,
                                     pc_SPEEDTESTDETAIL   out retCursor,
                                     pc_Brand             out retCursor) ;

 procedure GetTraceabilityReportInfo(pc_Traceability out retcursor,
                                      ps_CertificateNumber in varchar2,
                                      pi_certificationTypeID in number)   ; 
                                      
 procedure GetExceptionReportInfo(pc_Exception out retcursor); 
 procedure CompareSKUMainProductColumns ;
 
 procedure GetEmark117Info(pc_Certificate out retCursor,
                            pc_Brand out retCursor,
                            pc_Product out retCursor,
                            pc_CertificateDfValue out retcursor,
                            ps_certificateNumber in varchar2,
                            ps_extension in varchar2,
                            pi_certificationTypeID in number,
                            ps_Operatorid in varchar2,
                            pi_TireTypeID in number);

end reports_package;
/


CREATE OR REPLACE package body reports_package as

  procedure GetEmarkReportPassengerInfo(pc_Certificate out retCursor,
                                         pc_Brand out retCursor,
                                         pc_Product out retCursor,
                                         pc_CertificateDfValue out retcursor,
                                         ps_certificateNumber in varchar2,
                                         ps_extension in varchar2,
                                         pi_certificationTypeID in number,
                                         ps_Operatorid in varchar2,
                                         pi_TireTypeID in number
                                         ) as

      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
  begin
      if ps_certificateNumber is null or 
          pi_certificationTypeID is null then 
          raise li_ParametersAreNull ;
      end if;
      
       if ps_certificateNumber ='' or 
          pi_certificationTypeID  <= 0 then 
          raise li_ParametersAreNull ;
      end if;
      
      if ps_Operatorid is not null or 
         ps_Operatorid <> '' then
          ls_OperatorId:=ps_Operatorid;
      end if;
      
          if ps_extension is null or ps_extension = '' then
                    
              Open pc_Certificate for
              select distinct(CE.certificatenumber),ce.extension_en
              from  certificate ce 
                     inner join productcertificate pce on
                           ce.certificateid       = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                     inner join product p on 
                               pce.skuid = p.skuid
              where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                    ce.certificationtypeid = pi_certificationTypeID And
                    p.tiretypeid = pi_TireTypeID;
          else
                Open pc_Certificate for
                select distinct(CE.certificatenumber),ce.extension_en
                from  certificate ce 
                     inner join productcertificate pce on
                           ce.certificateid       = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                     inner join product p on 
                               pce.skuid = p.skuid
                where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                      ce.certificationtypeid = pi_certificationTypeID  And
                      LOWER(CE.EXTENSION_EN) = LOWER(ps_extension) and
                      p.tiretypeid = pi_TireTypeID;
          end if ;
    
          -- Gets the brand information
          Open pc_brand for
          --SELECT BrandCode,BrandName,Null as CertificateNumber, null as Extension_En
          SELECT *
          FROM  brand_view
          where lower(certificatenumber) = lower(ps_certificateNumber);   
          
          -- Gets the pc_Product information
           --TODO: ERASE ROWNUM < 2
          Open pc_Product for
          SELECT  *
          FROM  productdata_report_view
          where lower(certificatenumber) = lower(ps_certificateNumber)  and
                TireTypeID = pi_tiretypeid and rownum < 2;   
          
          --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                    MAX(DECODE(fieldid,1,fieldvalue,NULL)) MANUFACTURERNAME_E,
                    MAX(DECODE(fieldid,2,fieldvalue,NULL)) MANUFACTURERNAMEADDRESS_E,
                    MAX(DECODE(fieldid,3,fieldvalue,NULL)) TECHNICALSERVICE_E,
                    MAX(DECODE(fieldid,4,fieldvalue,NULL)) PLACE_E,
                    MAX(DECODE(fieldid,5,fieldvalue,NULL)) MEASURERIM_E,
                    MAX(DECODE(fieldid,6,fieldvalue,NULL)) INFLATIONPRESSURE_E,
                    MAX(DECODE(fieldid,7,fieldvalue,NULL)) TESTLABORATORY_E,
                    MAX(DECODE(fieldid,8,fieldvalue,NULL)) REPRESENTATIVENAME_E,
                    MAX(DECODE(fieldid,9,fieldvalue,NULL)) REPRESENTATIVEADDRESS_E,
                    MAX(DECODE(fieldid,10,fieldvalue,NULL)) REASONOFEXTENSION_E,
                    MAX(DECODE(fieldid,11,fieldvalue,NULL)) REMARKS_E
            FROM (
                SELECT FIELDID,
                  cdv.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  FIELDVALUE
                FROM CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                       cdv.CERTIFICATEID = ce.CERTIFICATEID 
                WHERE lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                      cdv.CERTIFICATIONTYPEID = pi_certificationTypeID 
              )
            group by CERTIFICATENUMBER ;
                
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  ' There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || ' There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);            
            
         when others then            
              ls_ErrorMsg:= sqlerrm ||  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetInfoReportPassenger',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
    
  end GetEmarkReportPassengerInfo;
  
  
  procedure GetEmark117Info(pc_Certificate out retCursor,
                            pc_Brand out retCursor,
                            pc_Product out retCursor,
                            pc_CertificateDfValue out retcursor,
                            ps_certificateNumber in varchar2,
                            ps_extension in varchar2,
                            pi_certificationTypeID in number,
                            ps_Operatorid in varchar2,
                            pi_TireTypeID in number) as 

      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                            
  
  begin
       if ps_certificateNumber is null or 
          pi_certificationTypeID is null then 
          raise li_ParametersAreNull ;
       end if;
      
       if ps_certificateNumber ='' or 
          pi_certificationTypeID  <= 0 then 
          raise li_ParametersAreNull ;
      end if;
      
      if ps_Operatorid is not null or 
         ps_Operatorid <> '' then
          ls_OperatorId:=ps_Operatorid;
      end if;
      
          if ps_extension is null or ps_extension = '' then
                    
              Open pc_Certificate for
              select distinct(CE.certificatenumber),ce.extension_en
              from  certificate ce 
                     inner join productcertificate pce on
                           ce.certificateid       = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                     inner join product p on 
                               pce.skuid = p.skuid
              where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                    ce.certificationtypeid = pi_certificationTypeID And
                    p.tiretypeid = pi_TireTypeID;
          else
                Open pc_Certificate for
                select distinct(CE.certificatenumber),ce.extension_en
                from  certificate ce 
                     inner join productcertificate pce on
                           ce.certificateid       = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                     inner join product p on 
                               pce.skuid = p.skuid
                where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                      ce.certificationtypeid = pi_certificationTypeID  And
                      LOWER(CE.EXTENSION_EN) = LOWER(ps_extension) and
                      p.tiretypeid = pi_TireTypeID;
          end if ;
          
          -- Gets the brand information
          Open pc_brand for
          SELECT *
          FROM  brand_view
          where lower(certificatenumber) = lower(ps_certificateNumber)  ;  
          
          -- Gets the pc_Product information
           --TODO: ERASE ROWNUM < 2
          Open pc_Product for
          SELECT  *
          FROM  productdata_report_view
          where lower(certificatenumber) = lower(ps_certificateNumber)  and
                TireTypeID = pi_tiretypeid and rownum < 2;   
  
  
         --Gets the default values information.         
           Open pc_CertificateDfValue for
           SELECT CERTIFICATENUMBER,
                  MANUFACTURERNAME_E,
                  MANUFACTURERNAMEADDRESS_E,
                  TECHNICALSERVICE_E,
                  PLACE_E,
                  MEASURERIM_E,
                  INFLATIONPRESSURE_E,
                  TESTLABORATORY_E,
                  REPRESENTATIVENAME_E,
                  REPRESENTATIVEADDRESS_E,
                  REASONOFEXTENSION_E,
                  REMARKS_E,
                  REPRTNUMBERISSUEDBYSERVICE_E,
                  SOUNDLEVEL_E,
                  REFERENCESPEED_E,
                  APPLICANTNAMEADDRESS_E,
                  PERFORMANCECHARACTERISTICS_E,
                  PLANTSADDRESSES_E,
                  TIRESIZEDESIGNATIONS_E,
                  ZONEA_E,
                  ZONEB_E,
                  ZONEC_E,
                  PPNPROFILEFAMILY_E
            FROM EMARKECE117_VIEW 
            WHERE lower(CERTIFICATENUMBER) = lower(ps_certificateNumber);
            
            
   EXCEPTION
         when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm ||  ' - GetEmark117Info. There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetEmark117Info',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm ||  ' - GetEmark117Info. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetEmark117Info',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);           
            
        when others then            
                    ls_ErrorMsg:= sqlerrm ||  ' - GetEmark117Info. An error have ocurred.(when others)' || sqlerrm;
                     APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                            ad_OPERATORID => ls_OperatorId,
                            AD_DATERECORDED  => sysdate,
                            AS_PROCESSNAME   =>' Reports_Package.GetEmark117Info',
                            AX_RECORDDATA    => 'An error have ocurred.(when others)',
                            AS_MESSAGECODE   => to_char(sqlcode),
                            AS_MESSAGE       =>ls_ErrorMsg);
                     raise_application_error (-20007,ls_ErrorMsg);
  end GetEmark117Info;
  
  
   procedure GetImarkReportPassengerInfo(pc_Certificate out retCursor,
                                       pc_Brand out retCursor,
                                       pc_Product out retCursor,
                                       pc_CertificateDfValue out retcursor,
                                       pc_ProdBrandList out retcursor            
                                       ) as

      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      ls_LatestImarkCertificateNum varchar2(20);
      
  begin  
       ls_LatestImarkCertificateNum := ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
      
      if   ls_LatestImarkCertificateNum is null or ls_LatestImarkCertificateNum = '' then
          ls_LatestImarkCertificateNum:='NotFound';
      end if;    
      
        
                    
              Open pc_Certificate for
              select distinct(CE.certificatenumber),ce.extension_en,ce.family_i
              from  certificate ce 
              where lower(certificatenumber) = lower(ls_LatestImarkCertificateNum) And                    
                    ce.certificationtypeid = 4 ;              
               -- Gets the brand information
              Open pc_brand for
              SELECT *
              FROM  brand_view
              where lower(certificatenumber) = lower(ls_LatestImarkCertificateNum) ;
              
              --TODO: ERASE ROWNUM < 2     
               Open pc_Product for
                SELECT SKUID,
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
                      INFORMENUMBER,
                      FECHADATE,
                      TREADPATTERN,
                      SPECIALPROTECTIVEBAND,
                      NOMINALTIREWIDTH,
                      ASPECTRADIO,
                      TREADWEARINDICATORS,
                      NAMEOFMANUFACTURER,
                      FAMILY,
                      DOTSERIALNUMBER,
                      CERTIFICATENUMBER,
                      EXTENSION_EN
                FROM PRODUCTDATA_REPORT_VIEW 
                where lower(certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                     rownum < 2  ;         
              
         
          
          --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                    MAX(DECODE(fieldid,91,fieldvalue,NULL)) SUPPLIERNAME_I,
                    MAX(DECODE(fieldid,92,fieldvalue,NULL)) COMPLETEADDRESS_I,
                    MAX(DECODE(fieldid,93,fieldvalue,NULL)) COUNTRYOFORIGIN_I,
                    MAX(DECODE(fieldid,94,fieldvalue,NULL)) TELEPHONE_I,
                    MAX(DECODE(fieldid,95,fieldvalue,NULL)) FAX_I,
                    MAX(DECODE(fieldid,96,fieldvalue,NULL)) MANUFACTURERNAME_I,
                    MAX(DECODE(fieldid,97,fieldvalue,NULL)) TECHNICALDEVELOPMENTCENTER_I,
                    MAX(DECODE(fieldid,98,fieldvalue,NULL)) APPLICANTNAME_I,
                    MAX(DECODE(fieldid,99,fieldvalue,NULL)) APPLICANTTITLE_I,
                    MAX(DECODE(fieldid,100,fieldvalue,NULL)) ASSOCIATEDPLANT_I,
                    MAX(DECODE(fieldid,101,fieldvalue,NULL)) OTHERASPECTS_I
            FROM (
                    SELECT FIELDID,
                          cdv.CERTIFICATIONTYPEID,
                          CERTIFICATENUMBER,
                          FIELDVALUE
                    FROM  CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                       cdv.CERTIFICATEID = ce.CERTIFICATEID 
                    WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                          cdv.CERTIFICATIONTYPEID = 4 
              )
            group by CERTIFICATENUMBER ;
            
            
            Open pc_ProdBrandList for
            Select * 
            From PRODBRANDLIST_VIEW v
            where lower(v.certificateNUmber)= lower(ls_LatestImarkCertificateNum)  And                    
                  v.certificationtypeid = 4 ;
                        
                
                
  EXCEPTION 
         when others then            
              ls_ErrorMsg:=  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetIMarkReportPassengerInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
    
  end GetImarkReportPassengerInfo;
  
   procedure GetImarkSamplingAndTestsInfo(pc_Certificate out retCursor,
                                           pc_Product out retCursor,
                                           pc_MeasureHdr out retCursor,
                                           pc_MeasureDtl out retCursor,
                                           pc_TreadWearHdr out retCursor,
                                           pc_TreadWearDtl out retCursor,
                                           pc_HighSpeedHdr out retCursor,
                                           pc_HighSpeedDtl out retCursor,
                                           pc_CertificateDfValue out retcursor
                                          ) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      li_MeasureId  MeasureHdr.measureid%type;
      li_TreadWearId TREADWEARHDR.TREADWEARID%type;
      li_HighSpeedId HIGHSPEEDHDR.HIGHSPEEDID%type;
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      ls_LatestImarkCertificateNum varchar2(20);
      
    begin  
         ls_LatestImarkCertificateNum := ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
          
          if   ls_LatestImarkCertificateNum is null or ls_LatestImarkCertificateNum = '' then
              ls_LatestImarkCertificateNum:='NotFound';
          end if; 
      
          -- Gets Certificate information 
              Open pc_Certificate for
              select distinct(CE.certificatenumber),ce.extension_en
              from  certificate ce 
              where lower(certificatenumber) = lower(ls_LatestImarkCertificateNum) And                    
                    ce.certificationtypeid = 4 ;
                    
               -- Gets the Product information
               
              --TODO: ERASE ROWNUM < 2     
               Open pc_Product for
                SELECT SKUID,
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
                      INFORMENUMBER,
                      FECHADATE,
                      TREADPATTERN,
                      SPECIALPROTECTIVEBAND,
                      NOMINALTIREWIDTH,
                      ASPECTRADIO,
                      TREADWEARINDICATORS,
                      NAMEOFMANUFACTURER,
                      FAMILY,
                      DOTSERIALNUMBER,
                      CERTIFICATENUMBER,
                      EXTENSION_EN
                FROM PRODUCTDATA_REPORT_VIEW 
                where lower(certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                      rownum < 2  ;  
                      
           --Gets MeasureHdr information.
                  Open pc_MeasureHdr FOR 
                  SELECT MEASUREID as Mea_ID,
                        PROJECTNUMBER as ProjectNum,
                        TIRENUMBER as TireNum,
                        TESTSPEC as TestSpec,
                        COMPLETIONDATE,
                        INFLATIONPRESSURE,
                        MOLDDESIGN,
                        RIMWIDTH,
                        DOTSERIALNUMBER,
                        DIAMETER,
                        AVGSECTIONWIDTH,
                        AVGOVERALLWIDTH,
                        MAXOVERALLWIDTH,
                        SIZEFACTOR,
                        MOUNTTIME,
                        MOUNTTEMP,
                        SERIALDATE,
                        ENDTIME,
                        ACTSIZEFACTOR,
                        m.CERTIFICATIONTYPEID,
                        CERTIFICATENUMBER,                        
                        STARTINFLATIONPRESSURE,
                        ENDINFLATIONPRESSURE,
                        ADJUSTMENT,
                        CIRCUNFERENCE,
                        NOMINALDIAMETER,
                        NOMINALWIDTH,
                        NOMINALWIDTHPASSFAIL,
                        NOMINALWIDTHDIFERENCE,
                        NOMINALWIDTHTOLERANCE,
                        MAXOVERALLDIAMETER,
                        MINOVERALLDIAMETER,
                        OVERALLWIDTHPASSFAIL,
                        OVERALLDIAMETERPASSFAIL,
                        DIAMETERDIFERENCE,
                        DIAMETERTOLERANCE,
                        TEMPRESISTANCEGRADING,
                        TENSILESTRENGHT1,
                        TENSILESTRENGHT2,
                        ELONGATION1,
                        ELONGATION2,
                        TENSILESTRENGHTAFTERAGE1,
                        TENSILESTRENGHTAFTERAGE2
                  FROM  Certificate ce inner join MeasureHdr m on 
                             ce.certificateid = m.certificateid and
                             ce.certificationtypeid = m.certificationtypeid                             
                  WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                        ce.CERTIFICATIONTYPEID = 4;
                  
                  if ls_LatestImarkCertificateNum='NotFound' then 
                      li_MeasureId:=0;
                  else
                      
                      -- Get MeasureId
                      SELECT MeasureID INTO li_MeasureId
                      FROM  Certificate ce inner join MeasureHdr m on 
                             ce.certificateid = m.certificateid and
                             ce.certificationtypeid = m.certificationtypeid
                      WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum)  and
                            ce.CERTIFICATIONTYPEID = 4;
                 end if;
                                
                  --Gets MeasureDtl information.
                  Open pc_MeasureDtl FOR
                  SELECT SECTIONWIDTH, OVERALLWIDTH, MEASUREID as Mea_ID,Iteration
                  FROM  MeasureDtl MD
                  WHERE MD.MEASUREID = li_MeasureId;

           --Gets TreadWearHdr information.
                  Open pc_TreadWearHdr FOR 
                  SELECT  TREADWEARID as TW_ID,
                          PROJECTNUMBER as ProjectNum,
                          TIRENUMBER as TireNum,
                          TESTSPEC as TestSpec,
                          COMPLETIONDATE,
                          DOTSERIALNUMBER,
                          LOWESTWEARBAR,
                          PASSYN,                          
                          ce.CERTIFICATIONTYPEID,
                          ce.CERTIFICATENUMBER,                         
                          SERIALDATE,
                          INDICATORSREQUIREMENT
                  FROM  Certificate ce inner join  TREADWEARHDR t on 
                                  ce.certificateid = t.certificateid and
                                  ce.certificationtypeid = t.certificationtypeid
                  WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                        ce.CERTIFICATIONTYPEID = 4;
                  
                    if ls_LatestImarkCertificateNum='NotFound' then 
                      li_TreadWearId:=0;
                  else      
                       --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                      SELECT TreadWearID INTO li_TreadWearId
                      FROM  Certificate ce inner join  TREADWEARHDR t on 
                                  ce.certificateid = t.certificateid and
                                  ce.certificationtypeid = t.certificationtypeid
                      WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                            ce.CERTIFICATIONTYPEID = 4;
                  end if;                
                                                        
                  --Gets TreadWearDtl information.
                  Open pc_TreadWearDtl FOR
                  SELECT  TREADWEARID as TW_ID, WEARBARHEIGHT ,ITERATION
                  FROM  TREADWEARDTL td
                  WHERE td.treadwearid = li_TreadWearId;
           
           --Gets HighSpeedHdr information.
                  Open pc_HighSpeedHdr FOR 
                  SELECT HIGHSPEEDID as HS_ID,
                        PROJECTNUMBER as ProjectNum,
                        TIRENUM as TireNum,
                        TESTSPEC as TestSpec,
                        COMPETIONDATE,
                        DOTSERIALNUMBER,
                        MFGWWYY,
                        PRECONDSTARTDATE,
                        PRECONDSARTTEMP,
                        RIMDIAMETER,
                        RIMWIDTH,
                        PRECONDENDDATE,
                        PRECONDENDTEMP,
                        INFLATIONPRESSURE,
                        BEFOREDIAMETER,
                        AFTERDIAMETER,
                        BEFOREINFLATION,
                        AFTERINFLATION,
                        WHEELPOSITION,
                        WHEELNUMBER,
                        FINALTEMP,
                        FINALDISTANCE,
                        FINALINFLATION,
                        POSTCONDSTARTDATE,
                        POSTCONDENDDATE,
                        POSTCONDENDTEMP,
                        PASSYN,
                        SERIALDATE,
                        POSTCONDTIME,
                        ce.CERTIFICATIONTYPEID,
                        CERTIFICATENUMBER,                        
                        DIAMETERTESTDRUM,
                        PRECONDTIME,
                        PRECONDTEMP,
                        INFLATIONPRESSUREREADJUSTED,
                        CIRCUNFERENCEBEFORETEST,
                        WHEELSPEEDRPM,
                        WHEELSPEEDKMH,
                        CIRCUNFERENCEAFTERTEST,
                        ODDIFERENCE,
                        ODDIFERENCETOLERANCE,
                        SERIENOM,
                        FINALJUDGEMENT,
                        APPROVER,
                        PASSATKMH,
                        SPEEDTTESTPASSFAIL,
                        SPEEDTOTALTIME,
                        MAXSPEED,
                        MAXLOAD
                  FROM  Certificate ce inner join  HIGHSPEEDHDR h on 
                                  ce.certificateid = h.certificateid and
                                  ce.certificationtypeid = h.certificationtypeid  
                  WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                        ce.CERTIFICATIONTYPEID = 4;
                  
                    if ls_LatestImarkCertificateNum='NotFound' then 
                      li_HighSpeedId:=0;
                  else  
                       --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                      SELECT HighSpeedID INTO li_HighSpeedId
                       FROM  Certificate ce inner join  HIGHSPEEDHDR h on 
                                  ce.certificateid = h.certificateid and
                                  ce.certificationtypeid = h.certificationtypeid  
                  WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                        ce.CERTIFICATIONTYPEID = 4;
                   end if;               
                                
                  --Gets HighSpeedDtl information.       
                  Open pc_HighSpeedDtl FOR
                  SELECT TESTSTEP,
                          TIMEINMIN,
                          SPEED,
                          TOTMILES,
                          LOAD,
                          LOADPERCENT,
                          SETINFLATION,
                          AMBTEMP,
                          INFPRESSURE,
                          STEPCOMPLETIONDATE,
                          HIGHSPEEDID  as HS_ID
                  FROM  HIGHSPEEDDTL h
                  WHERE h.HIGHSPEEDID = li_HighSpeedId;
                      
          --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                    MAX(DECODE(fieldid,102,fieldvalue,NULL)) SAMPLINGDATE_I,
                    MAX(DECODE(fieldid,103,fieldvalue,NULL)) CUSTOMERDATA_I,
                    MAX(DECODE(fieldid,104,fieldvalue,NULL)) SAMPLINGOBJECTIVE_I,
                    MAX(DECODE(fieldid,105,fieldvalue,NULL)) SAMPLINGLOCATION_I,
                    MAX(DECODE(fieldid,106,fieldvalue,NULL)) SAMPLINGLOCATIONCONDITION_I,
                    MAX(DECODE(fieldid,107,fieldvalue,NULL)) SAMPLINGLOCATIONCOMMENTS_I,
                    MAX(DECODE(fieldid,108,fieldvalue,NULL)) STORAGELOCATIONCONDITION_I,
                    MAX(DECODE(fieldid,109,fieldvalue,NULL)) STORAGELOCATIONCOMMENTS_I,
                    MAX(DECODE(fieldid,110,fieldvalue,NULL)) REFERENCESTANDARD_I,
                    MAX(DECODE(fieldid,111,fieldvalue,NULL)) SAMPLINGOBSERVATION_I,
                    MAX(DECODE(fieldid,112,fieldvalue,NULL)) TESTRESULTSOBSERVATION_I
            FROM (
                    SELECT FIELDID,
                          cdv.CERTIFICATIONTYPEID,
                          ce.CERTIFICATENUMBER,
                          FIELDVALUE
                     FROM CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                       cdv.CERTIFICATEID = ce.CERTIFICATEID  
                    WHERE lower(ce.certificatenumber) = lower(ls_LatestImarkCertificateNum) and
                          cdv.CERTIFICATIONTYPEID = 4 
              )
            group by CERTIFICATENUMBER ; 
            
    
                
  EXCEPTION 
         when others then            
              ls_ErrorMsg:=  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetIMarkReportPassengerInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
      
  end GetImarkSamplingAndTestsInfo;
   
  
procedure GetCCCSequentialReportInfo(pc_Certificate out retCursor,
									 pc_Brand out retCursor,
									 pc_Product out retCursor,
									 pc_CertificateDfValue out retcursor,
									 ps_certificateNumber in varchar2,
									 ps_extension in varchar2,
									 pi_certificationTypeID in number,
									 ps_Operatorid in varchar2
									) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
  begin
      if ps_certificateNumber is null or 
          pi_certificationTypeID is null then 
          raise li_ParametersAreNull ;
      end if;
      
       if ps_certificateNumber ='' or 
          pi_certificationTypeID  <= 0 then 
          raise li_ParametersAreNull ;
      end if;
      
      if ps_Operatorid is not null or 
         ps_Operatorid <> '' then
          ls_OperatorId:=ps_Operatorid;
      end if;
      
          if ps_extension is null or ps_extension = '' then
                    
              Open pc_Certificate for
              select distinct(CE.certificatenumber),ce.extension_en
              from  certificate ce 
                     inner join productcertificate pce on
                           ce.certificateid   = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                     inner join product p on 
                               pce.skuid = p.skuid
              where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                    ce.certificationtypeid = pi_certificationTypeID;
          else
                Open pc_Certificate for
                select distinct(CE.certificatenumber),ce.extension_en
                from  certificate ce 
                      inner join productcertificate pce on
                           ce.certificateid   = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                     inner join product p on 
                               pce.skuid = p.skuid
                where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                      ce.certificationtypeid = pi_certificationTypeID  And
                      LOWER(CE.EXTENSION_EN) = LOWER(ps_extension);
          end if ;
    
          -- Gets the brand information
          Open pc_brand for
          SELECT *
          FROM  brand_view
          where lower(certificatenumber) = lower(ps_certificateNumber)  ;  
          
          -- Gets the pc_Product information
           --TODO: ERASE ROWNUM < 2
          Open pc_Product for
          SELECT  *
          FROM  productdata_report_view
          where lower(certificatenumber) = lower(ps_certificateNumber) and rownum < 2;   
          
          --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                    MAX(DECODE(fieldid,12,fieldvalue,NULL)) PRODUCTCATEGORY_C,
                    MAX(DECODE(fieldid,13,fieldvalue,NULL)) APPLICATION_C,
                    MAX(DECODE(fieldid,14,fieldvalue,NULL)) TOPIC_C,
                    
                    MAX(DECODE(fieldid,15,fieldvalue,NULL)) APPLICANTNATIONALITY_C,
                    MAX(DECODE(fieldid,16,fieldvalue,NULL)) APPLICANTCOMPANYNAMECHINESE_C,
                    MAX(DECODE(fieldid,17,fieldvalue,NULL)) APPLICANTCOMPANYNAMEENGLISH_C,
                    MAX(DECODE(fieldid,18,fieldvalue,NULL)) APPLICANTPAYERNAME_C,
                    MAX(DECODE(fieldid,19,fieldvalue,NULL)) APPLICANTPAYERADDRESS_C,
                    MAX(DECODE(fieldid,20,fieldvalue,NULL)) APPLICANTADDRESSCHINESE_C,
                    MAX(DECODE(fieldid,21,fieldvalue,NULL)) APPLICANTADDRESSENGLISH_C,
                    MAX(DECODE(fieldid,22,fieldvalue,NULL)) APPLICANTORGNIZATIONCODE_C,
                    MAX(DECODE(fieldid,23,fieldvalue,NULL)) APPLICANTPOSTCODE_C,
                    MAX(DECODE(fieldid,24,fieldvalue,NULL)) APPLICANTPERSONTOBECONTACT_C,
                    MAX(DECODE(fieldid,25,fieldvalue,NULL)) APPLICANTCONTACTPERSON_C,
                    MAX(DECODE(fieldid,26,fieldvalue,NULL)) APPLICANTTELEPHONE_C,
                    MAX(DECODE(fieldid,27,fieldvalue,NULL)) APPLICANTFAX_C,
                    MAX(DECODE(fieldid,28,fieldvalue,NULL)) APPLICANTEMAIL_C,
                    MAX(DECODE(fieldid,29,fieldvalue,NULL)) APPLICANTMOBILEPHONE_C,
                    
                    MAX(DECODE(fieldid,30,fieldvalue,NULL)) AGENCYNATIONALITY_C,
                    MAX(DECODE(fieldid,31,fieldvalue,NULL)) AGENCYPROVINCE_C,
                    MAX(DECODE(fieldid,32,fieldvalue,NULL)) AGENCYCITY_C,
                    MAX(DECODE(fieldid,33,fieldvalue,NULL)) AGENCYCOUNTY_C,
                    MAX(DECODE(fieldid,34,fieldvalue,NULL)) AGENCYRCOMPANYNAME_C,
                    MAX(DECODE(fieldid,35,fieldvalue,NULL)) AGENCYRCOMPANYADDRESS_C,
                    MAX(DECODE(fieldid,36,fieldvalue,NULL)) AGENCYORGNIZATIONCODE_C,
                    MAX(DECODE(fieldid,37,fieldvalue,NULL)) AGENCYAPPROVALNUMBER_C,
                    MAX(DECODE(fieldid,38,fieldvalue,NULL)) AGENCYPOSTCODE_C,
                    MAX(DECODE(fieldid,39,fieldvalue,NULL)) AGENCYCONTACTPERSON_C,
                    MAX(DECODE(fieldid,40,fieldvalue,NULL)) AGENCYEMAIL_C,
                    MAX(DECODE(fieldid,41,fieldvalue,NULL)) AGENCYTELEPHONE_C,
                    MAX(DECODE(fieldid,42,fieldvalue,NULL)) AGENCYFAX_C,
                    MAX(DECODE(fieldid,43,fieldvalue,NULL)) AGENCYMOBILE_C,
                    
                    MAX(DECODE(fieldid,44,fieldvalue,NULL)) MANUFACTURESAMEASAPPLICANT_C,
                    MAX(DECODE(fieldid,45,fieldvalue,NULL)) MANUFACTURENATIONALITY_C,
                    MAX(DECODE(fieldid,46,fieldvalue,NULL)) MANUFACTURECOMPANYNAMECH_C,
                    MAX(DECODE(fieldid,47,fieldvalue,NULL)) MANUFACTURECOMPANYNAMEEN_C,
                    MAX(DECODE(fieldid,48,fieldvalue,NULL)) MANUFACTUREADDRESSCHINESE_C,
                    MAX(DECODE(fieldid,49,fieldvalue,NULL)) MANUFACTUREADDRESSENGLISH_C,
                    MAX(DECODE(fieldid,50,fieldvalue,NULL)) MANUFACTUREORGNIZATIONCODE_C,
                    MAX(DECODE(fieldid,51,fieldvalue,NULL)) MANUFACTUREPOSTCODE_C,
                    MAX(DECODE(fieldid,52,fieldvalue,NULL)) MANUFACTUREPERSONTOBECONTACT_C,
                    MAX(DECODE(fieldid,53,fieldvalue,NULL)) MANUFACTURECONTACTPERSON_C,
                    MAX(DECODE(fieldid,54,fieldvalue,NULL)) MANUFACTURETELEPHONE_C,
                    MAX(DECODE(fieldid,55,fieldvalue,NULL)) MANUFACTUREFAX_C,
                    MAX(DECODE(fieldid,56,fieldvalue,NULL)) MANUFACTUREEMAIL_C,
                    MAX(DECODE(fieldid,57,fieldvalue,NULL)) MANUFACTUREMOBILEPHONE_C,
                    
                    MAX(DECODE(fieldid,58,fieldvalue,NULL)) FACTORYSAMEASAPPLICANT_C,
                    MAX(DECODE(fieldid,59,fieldvalue,NULL)) FACTORYSAMEASMANUFACTURER_C,
                    MAX(DECODE(fieldid,60,fieldvalue,NULL)) FACTORYNATIONALITY_C,
                    MAX(DECODE(fieldid,61,fieldvalue,NULL)) FACTORYNAMECHINESE_C,
                    MAX(DECODE(fieldid,62,fieldvalue,NULL)) FACTORYNAMEENGLISH_C,
                    MAX(DECODE(fieldid,63,fieldvalue,NULL)) FACTORYADDRESSCHINESE_C,
                    MAX(DECODE(fieldid,64,fieldvalue,NULL)) FACTORYADDRESSENGLISH_C,
                    MAX(DECODE(fieldid,65,fieldvalue,NULL)) FACTORYORGNIZATIONCODE_C,
                    MAX(DECODE(fieldid,66,fieldvalue,NULL)) FACTORYNUMBER_C,
                    MAX(DECODE(fieldid,67,fieldvalue,NULL)) FACTORYPOSTCODE_C,
                    MAX(DECODE(fieldid,68,fieldvalue,NULL)) FACTORYCONTACTPERSONCHINESE_C,
                    MAX(DECODE(fieldid,69,fieldvalue,NULL)) FACTORYCONTACTPERSONENGLISH_C,
                    MAX(DECODE(fieldid,70,fieldvalue,NULL)) FACTORYEMAIL_C,
                    MAX(DECODE(fieldid,71,fieldvalue,NULL)) FACTORYTELEPHONE_C,
                    MAX(DECODE(fieldid,72,fieldvalue,NULL)) FACTORYFAX_C,
                    MAX(DECODE(fieldid,73,fieldvalue,NULL)) FACTORYMOBILE_C,
                    
                    MAX(DECODE(fieldid,74,fieldvalue,NULL)) REMARK_C,
                    MAX(DECODE(fieldid,75,fieldvalue,NULL)) GBSAFETYSTANDARDNUMBER_C,
                    MAX(DECODE(fieldid,76,fieldvalue,NULL)) GBEMCSTANDARDNUMBER_C,
                    MAX(DECODE(fieldid,77,fieldvalue,NULL)) CBTESTCERTIFICATEYN_C,
                    MAX(DECODE(fieldid,78,fieldvalue,NULL)) CBCERTIFICATENUMBER_C,
                    MAX(DECODE(fieldid,79,fieldvalue,NULL)) CBCERTIFICATEISSUEDDATE_C,
                    MAX(DECODE(fieldid,80,fieldvalue,NULL)) CBCERTIFICATENBCNAME_C,
                    MAX(DECODE(fieldid,81,fieldvalue,NULL)) CCCCERTIFIACTENUMBER_C,
                    MAX(DECODE(fieldid,82,fieldvalue,NULL)) CERTIFICATEMODEL_C
            FROM (
                SELECT FIELDID,
                  cdv.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  FIELDVALUE
                FROM CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                       cdv.CERTIFICATEID = ce.CERTIFICATEID                     
                WHERE lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                      cdv.CERTIFICATIONTYPEID = pi_certificationTypeID 
              )
            group by CERTIFICATENUMBER ;
                
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  ' There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || ' There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);         
            
         when others then            
              ls_ErrorMsg:=  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetInfoReportPassenger',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
    
  end GetCCCSequentialReportInfo;

    procedure GetGSOPassengerReport(   pc_Certificate out retCursor,
                                       pc_Brand out retCursor,
                                       pc_SkuList out retCursor,
                                       pc_Product out retCursor,
                                       pc_CertificateDfValue out retcursor,
                                       pc_MeasureHDR out retcursor,
                                       pc_PlungerHDR out retcursor,
                                       pc_beadunseathdr out retcursor,
                                       pc_treadwearhdr out retcursor,
                                       pc_endurance out retcursor,
                                       pc_highspeedhdr out retcursor,
                                       ps_certificateNumber in varchar2,
                                       ps_extension in varchar2,
                                       pi_certificationTypeID in number,
                                       ps_Operatorid in varchar2,
                                       pi_TireTypeId in number
                                       ) as
/*
pi_TireTypeId is ignored for now. 
kept in place for future use.
*/
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
  begin
      if ps_certificateNumber is null or 
          pi_certificationTypeID is null then 
          raise li_ParametersAreNull ;
      end if;
      
       if ps_certificateNumber ='' or 
          pi_certificationTypeID  <= 0 then 
          raise li_ParametersAreNull ;
      end if;
      
      if ps_Operatorid is not null or 
         ps_Operatorid <> '' then
          ls_OperatorId:=ps_Operatorid;
      end if;
      
          if ps_extension is null or ps_extension = '' then
                    
              Open pc_Certificate for
              select distinct(CE.certificatenumber),ce.extension_en
              from  certificate ce
                   inner join productcertificate pce on
                           ce.certificateid   = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                   inner join product p on
                           pce.skuid = p.skuid
              where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                    ce.certificationtypeid = pi_certificationTypeID; 
               -- Gets the brand information
              Open pc_brand for
              SELECT *
              FROM  brand_view
              where lower(certificatenumber) = lower(ps_certificateNumber) and  rownum < 2 ;
              
              Open pc_SkuList for
              SELECT BRANDCODE,
                      BRANDNAME,
                      CERTIFICATENUMBER,
                      EXTENSION_EN,
                      SKUID,
                      SKU,
                      SIZESTAMP
              FROM SKULIST_VIEW 
               where lower(certificatenumber) = lower(ps_certificateNumber); 
                      
            
               Open pc_Product for
                SELECT SKUID,
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
                      CERTIFICATENUMBER,
                      EXTENSION_EN,
                      SERIALDATE,
                      BRANDDESC,
                      LOADRANGE,
                      MEARIMWIDTH,
                      REGROOVABLEIND,
                      PLANTPRODUCED,
                      MOSTRECENTTESTDATE,
                      IMARK,
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
                FROM PRODUCTDATA_REPORT_VIEW 
                where lower(certificatenumber) = lower(ps_certificateNumber) and rownum < 2;
                            
              
          else
                Open pc_Certificate for
                select distinct(CE.certificatenumber),ce.extension_en
                FROM  CERTIFICATE CE 
                       inner join productcertificate pce on
                           ce.certificateid   = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid
                       inner join product p on
                                 pce.skuid = p.skuid
              where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                    ce.certificationtypeid = pi_certificationTypeID And                     
                    LOWER(CE.EXTENSION_EN) = LOWER(ps_extension);
                -- Gets the brand information       
               Open pc_brand for
                SELECT *
                FROM  brand_view
                where lower(certificatenumber) = lower(ps_certificateNumber) AND 
                    LOWER(EXTENSION_EN) = LOWER(ps_extension);
                
                Open pc_SkuList for
              SELECT BRANDCODE,
                      BRANDNAME,
                      CERTIFICATENUMBER,
                      EXTENSION_EN,
                      SKUID,
                      SKU,
                      SIZESTAMP
              FROM SKULIST_VIEW 
               where lower(certificatenumber) = lower(ps_certificateNumber)  AND 
                    LOWER(EXTENSION_EN) = LOWER(ps_extension);
               
                 -- Gets the pc_Product information               
                 Open pc_Product for
                SELECT SKUID,
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
                      CERTIFICATENUMBER,
                      EXTENSION_EN,
                      SERIALDATE,
                      BRANDDESC,
                      LOADRANGE,
                      MEARIMWIDTH,
                      REGROOVABLEIND,
                      PLANTPRODUCED,
                      MOSTRECENTTESTDATE,
                      IMARK,
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
                FROM PRODUCTDATA_REPORT_VIEW 
                where lower(certificatenumber) = lower(ps_certificateNumber) and rownum < 2;
                         
          end if ;  
          
          --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                    MAX(DECODE(fieldid,83,fieldvalue,NULL)) COUNTRYOFPRODUCTION_G,
                    MAX(DECODE(fieldid,84,fieldvalue,NULL)) MANUFACTURER_G,
                    MAX(DECODE(fieldid,85,fieldvalue,NULL)) NAMEOFMANUFACTURER_G,
                    MAX(DECODE(fieldid,86,fieldvalue,NULL)) COUNTRYOFPRODUCTION_G,
                    MAX(DECODE(fieldid,87,fieldvalue,NULL)) TO_G,
                    MAX(DECODE(fieldid,88,fieldvalue,NULL)) MANUFACTUREDBY_G,                   
                    MAX(DECODE(fieldid,89,fieldvalue,NULL)) REGULATIONNOPASSENGER_G,                   
                    MAX(DECODE(fieldid,90,fieldvalue,NULL)) REGULATIONNOLIGHTTRUCK_G                     
            FROM (
                    SELECT FIELDID,
                          cdv.CERTIFICATIONTYPEID,
                          ce.CERTIFICATENUMBER,
                          FIELDVALUE
                    FROM CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                             cdv.certificateid = ce.certificateid
                    WHERE lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                          cdv.CERTIFICATIONTYPEID = pi_certificationTypeID 
              )
            group by CERTIFICATENUMBER ;
            
            Open pc_MeasureHDR for
            SELECT MEASUREID,
                  NVL(PROJECTNUMBER,'') as PROJECTNUMBER,
                   NVL(TIRENUMBER,0) as TIRENUMBER,
                   NVL(TESTSPEC,'') as TESTSPEC,
                  nvl(COMPLETIONDATE , to_date('2000/01/01','yyyy/mm/dd')) as COMPLETIONDATE,
                  nvl(INFLATIONPRESSURE,0)  as INFLATIONPRESSURE,
                  NVL( MOLDDESIGN,'.') as MOLDDESIGN,
                  nvl(RIMWIDTH,'0') as RIMWIDTH,
                  NVL( DOTSERIALNUMBER,'.') as DOTSERIALNUMBER,
                  nvl(DIAMETER,0) as DIAMETER,
                  nvl(AVGSECTIONWIDTH,0) as AVGSECTIONWIDTH,
                  nvl(AVGOVERALLWIDTH,0) as AVGOVERALLWIDTH,
                  nvl(MAXOVERALLWIDTH,0) as MAXOVERALLWIDTH,
                  nvl(SIZEFACTOR,0) as SIZEFACTOR,
                  nvl(MOUNTTIME, to_date('2000/01/01','yyyy/mm/dd')) as MOUNTTIME,
                  nvl(MOUNTTEMP, 0) as MOUNTTEMP,
                  nvl(SERIALDATE,to_date('2000/01/01','yyyy/mm/dd')) as SERIALDATE,
                  nvl(ENDTIME,to_date('2000/01/01','yyyy/mm/dd')) as ENDTIME,
                  nvl(ACTSIZEFACTOR,0) as ACTSIZEFACTOR,
                  nvl(STARTINFLATIONPRESSURE,0) as STARTINFLATIONPRESSURE,
                  nvl(ENDINFLATIONPRESSURE,0) as ENDINFLATIONPRESSURE,
                  nvl(ADJUSTMENT,'') as ADJUSTMENT,
                  nvl(CIRCUNFERENCE, 0) as CIRCUNFERENCE,
                  nvl(NOMINALDIAMETER, 0) as NOMINALDIAMETER,
                  nvl(NOMINALWIDTH, 0) as NOMINALWIDTH,
                  nvl(NOMINALWIDTHPASSFAIL, 'y') as NOMINALWIDTHPASSFAIL,
                  nvl(NOMINALWIDTHDIFERENCE, 0) as NOMINALWIDTHDIFERENCE,
                  nvl(NOMINALWIDTHTOLERANCE, 0) as NOMINALWIDTHTOLERANCE,
                  nvl(MAXOVERALLDIAMETER, 0) as MAXOVERALLDIAMETER,
                  nvl(MINOVERALLDIAMETER, 0) as MINOVERALLDIAMETER,
                  nvl(OVERALLWIDTHPASSFAIL, 'y') as OVERALLWIDTHPASSFAIL,
                  nvl(OVERALLDIAMETERPASSFAIL, 'y') as OVERALLDIAMETERPASSFAIL,
                  nvl(DIAMETERDIFERENCE, 0) as DIAMETERDIFERENCE,
                  nvl(DIAMETERTOLERANCE, 0) as DIAMETERTOLERANCE,
                  nvl(TEMPRESISTANCEGRADING, 0) as TEMPRESISTANCEGRADING,
                  nvl(TENSILESTRENGHT1, 0) as TENSILESTRENGHT1,
                  nvl(TENSILESTRENGHT2, 0) as TENSILESTRENGHT2,
                  nvl(ELONGATION1, 0) as ELONGATION1,
                  nvl(ELONGATION2, 0) as ELONGATION2,
                  nvl(TENSILESTRENGHTAFTERAGE1, 0) as TENSILESTRENGHTAFTERAGE1,
                  nvl(TENSILESTRENGHTAFTERAGE2, 0) as TENSILESTRENGHTAFTERAGE2,
                  ce.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  pce.SKUID
            FROM CERTIFICATE CE 
                   inner join productcertificate pce on
                           ce.certificateid   = pce.certificateid and
                           ce.certificationtypeid = pce.certificationtypeid                         
                   inner join Certificationtype ct on 
                        ce.CERTIFICATIONTYPEID = ct.CERTIFICATIONTYPEID
                   inner join MEASUREHDR m on
                      ce.certificateid = m.certificateid and
                      ce.certificationtypeid = m.certificationtypeid
           where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                 ce.CERTIFICATIONTYPEID = pi_certificationTypeID and rownum < 2 ;                     
            
            Open pc_PlungerHDR For
            SELECT  PLUNGERID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    DOTSERIALNUMBER,
                    AVGBREAKINGENERGY,
                    PASSYN,                   
                    SERIALDATE,
                    MINPLUNGER,                  
                    ce.CERTIFICATIONTYPEID,
                    ce.CERTIFICATENUMBER,
                    pce.SKUID                
            FROM  CERTIFICATE CE 
                   inner join productcertificate pce on
                           ce.certificateid     = pce.certificateid and
                         ce.certificationtypeid = pce.certificationtypeid
                   inner join  Certificationtype ct on 
                        ce.CERTIFICATIONTYPEID = ct.CERTIFICATIONTYPEID
                   inner join  PLUNGERHDR  p on
                      ce.certificateid       = p.certificateid and
                      ce.certificationtypeid = p.certificationtypeid 
             where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                 ce.CERTIFICATIONTYPEID = pi_certificationTypeID And  rownum < 2  ;                            
            
            Open pc_beadunseathdr for
            SELECT  BEADUNSEATID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    DOTSERIALNUMBER,
                    LOWESTUNSEATVALUE,
                    PASSYN,                   
                    SERIALDATE,
                    MINBEADUNSEAT,                    
                    PASSYN,
                    ce.CERTIFICATIONTYPEID,
                    ce.CERTIFICATENUMBER,
                    pce.SKUID           
             FROM  CERTIFICATE CE 
                    inner join productcertificate pce on
                           ce.certificateid     = pce.certificateid and
                         ce.certificationtypeid = pce.certificationtypeid
                   inner join  Certificationtype ct on 
                        ce.CERTIFICATIONTYPEID = ct.CERTIFICATIONTYPEID
                   inner join  BEADUNSEATHDR B on
                       ce.certificateid      = b.certificateid and
                      ce.certificationtypeid = b.certificationtypeid 
             where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                 ce.CERTIFICATIONTYPEID = pi_certificationTypeID And  rownum < 2  ;   

            Open pc_treadwearhdr FOR
            SELECT  TREADWEARID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    DOTSERIALNUMBER,
                    LOWESTWEARBAR,
                    PASSYN,
                    SERIALDATE,  
                    INDICATORSREQUIREMENT,                
                    ce.CERTIFICATIONTYPEID,
                    ce.CERTIFICATENUMBER,
                    pce.SKUID
            FROM  CERTIFICATE CE 
                   inner join productCertificate pce on
                         ce.certificateid   = pce.certificateid and
                         ce.certificationtypeid = pce.certificationtypeid
                   inner join  Certificationtype ct on 
                        ce.CERTIFICATIONTYPEID = ct.CERTIFICATIONTYPEID
                   inner join  TREADWEARHDR T on
                       ce.certificateid      = t.certificateid and
                      ce.certificationtypeid = t.certificationtypeid
            where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                 ce.CERTIFICATIONTYPEID = pi_certificationTypeID And  rownum < 2  ;   
                 
            Open  pc_endurance FOR
            SELECT 
                  ENDURANCEID,
                  PROJECTNUMBER,
                  TIRENUMBER,
                  TESTSPEC,
                  COMPLETIONDATE,
                  DOTSERIALNUMBER,
                  MFGWWYY,
                  PRECONDSTARTDATE,
                  PRECONDSTARTTEMP,
                  RIMDIAMETER,
                  RIMWIDTH,
                  PRECONDENDDATE,
                  PRECONDENDTEMP,
                  INFLATIONPRESSURE,
                  BEFOREDIAMETER,
                  AFTERDIAMETER,
                  BEFOREINFLATION,
                  AFTERINFLATION,
                  WHEELPOSITION,
                  WHEELNUMBER,
                  FINALTEMP,
                  FINALDISTANCE,
                  FINALINFLATION,
                  POSTCONDSTARTDATE,
                  POSTCONDENDDATE,
                  POSTCONDENDTEMP,
                  PASSYN, 
                  SERIALDATE,
                  PRECONDTIME,
                  POSTCONDTIME, 
                  DIAMETERTESTDRUM,
                  PRECONDTEMP,
                  INFLATIONPRESSUREREADJUSTED,
                  CIRCUNFERENCEBEFORETEST,
                  RESULTPASSFAIL,
                  ENDURANCEHOURS,
                  POSSIBLEFAILURESFOUND,
                  CIRCUNFERENCEAFTERTEST,
                  OUTERDIAMETERDIFERENCE,
                  ODDIFERENCETOLERANCE,
                  SERIENOM,
                  FINALJUDGEMENT,
                  APPROVER,
                  ce.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  pce.SKUID
            FROM  CERTIFICATE CE 
                   inner join productCertificate pce on
                         ce.certificateid       = pce.certificateid and
                         ce.certificationtypeid = pce.certificationtypeid
                       inner join  Certificationtype ct on 
                            ce.CERTIFICATIONTYPEID = ct.CERTIFICATIONTYPEID
                       inner join  ENDURANCEHDR E on
                           ce.certificateid      = e.certificateid and
                          ce.certificationtypeid = e.certificationtypeid 
            where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                 ce.CERTIFICATIONTYPEID = pi_certificationTypeID And  rownum < 2  ;                             
            
            Open pc_highspeedhdr FOR      
            SELECT HIGHSPEEDID,
                  PROJECTNUMBER,
                  TIRENUM,
                  TESTSPEC,
                  COMPETIONDATE,
                  DOTSERIALNUMBER,
                  MFGWWYY,
                  PRECONDSTARTDATE,
                  PRECONDSARTTEMP,
                  RIMDIAMETER,
                  RIMWIDTH,
                  PRECONDENDDATE,
                  PRECONDENDTEMP,
                  INFLATIONPRESSURE,
                  BEFOREDIAMETER,
                  AFTERDIAMETER,
                  BEFOREINFLATION,
                  AFTERINFLATION,
                  WHEELPOSITION,
                  WHEELNUMBER,
                  FINALTEMP,
                  FINALDISTANCE,
                  FINALINFLATION,
                  POSTCONDSTARTDATE,
                  POSTCONDENDDATE,
                  POSTCONDENDTEMP,
                  PASSYN,
                  SERIALDATE,
                  POSTCONDTIME,  
                  DIAMETERTESTDRUM,
                  PRECONDTIME,
                  PRECONDTEMP,
                  INFLATIONPRESSUREREADJUSTED,
                  CIRCUNFERENCEBEFORETEST,
                  WHEELSPEEDRPM,
                  WHEELSPEEDKMH,
                  CIRCUNFERENCEAFTERTEST,
                  ODDIFERENCE,
                  ODDIFERENCETOLERANCE,
                  SERIENOM,
                  FINALJUDGEMENT,
                  APPROVER,
                  PASSATKMH,
                  SPEEDTTESTPASSFAIL,
                  SPEEDTOTALTIME,
                  MAXSPEED,
                  MAXLOAD,
                  ce.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  pce.SKUID
            FROM  CERTIFICATE CE 
                   inner join productCertificate pce on
                          ce.certificateid       = pce.certificateid and
                         ce.certificationtypeid = pce.certificationtypeid
                       inner join  Certificationtype ct on 
                            ce.CERTIFICATIONTYPEID = ct.CERTIFICATIONTYPEID
                       inner join  HIGHSPEEDHDR H on
                          ce.certificateid       = h.certificateid and
                          ce.certificationtypeid = h.certificationtypeid 
            where lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                 ce.CERTIFICATIONTYPEID = pi_certificationTypeID And  rownum < 2  ;     
                 
            
                
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  ' There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetGSOPassengerReport',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || ' There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetGSOPassengerReport',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);         
            
         when others then            
              ls_ErrorMsg:=  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetGSOPassengerReport',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
    
  end GetGSOPassengerReport;
  
   PROCEDURE GetCertificateReportInfoBySKU(  pc_Product       out retCursor,
                                             pc_Certificate   out retCursor,
                                             PC_TESTREFERENCE out retCursor,
                                             ps_sku           in varchar2,
                                             ps_Operatorid    in varchar2) as 
   --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_LatestSkuId  product.skuid%TYPE;
  begin
  
      if ps_sku is null then 
          raise li_ParametersAreNull ;
      end if;
      
       if ps_sku =''  then 
          raise li_ParametersAreNull ;
      end if;
      
      if ps_Operatorid is not null or ps_Operatorid <> '' then
          ls_OperatorId:=ps_Operatorid;
      end if;
      
      li_LatestSkuId := ICS_COMMON_FUNCTIONS.GETLATESTSKUIDBYSKU(PS_SKU => ps_sku);    
      
      
       Open pc_Product for
        SELECT SKUID,
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
              MEARIMWIDTH,
              UTQGTREADWEAR,
              UTQGTRACTION,
              UTQGTEMP,
              MUDSNOWYN,
              RIMDIAMETER,
              CERTIFICATENUMBER,
              EXTENSION_EN
        FROM  PRODUCTDATA_REPORT_VIEW  
        where SKUID = li_LatestSkuId;
      
    
          -- Gets the Certificate information
       Open pc_Certificate for
       SELECT pce.SKUID,
              ce.CERTIFICATIONTYPEID,
              ct.certificationtypename,
              ce.CERTIFICATENUMBER,
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
              BATCHNUMBER_G       
            FROM  CERTIFICATE CE 
                   inner join  productCertificate pce on
                         ce.certificateid       = pce.certificateid and
                         ce.certificationtypeid = pce.certificationtypeid
                   INNER JOIN  CERTIFICATIONTYPE CT ON
                         CE.CERTIFICATIONTYPEID = ct.certificationtypeid
       WHERE pce.skuid = li_LatestSkuId ;
       
        OPEN PC_TESTREFERENCE FOR
        SELECT CERTIFICATENUMBER,
              SKUID,
              MEASUREMENT_TESTREFERENCE,
              PLUNGER_TESTREFERENCE,
              BEADUNSEAT_TESTREFERENCE,
              ENDURANCE_TESTREFERENCE,
              HIGHSPEED_TESTREFERENCE,
              LAB_TESTREFERENCE,
              WHEEL_TESTREFERENCE,
              NOISE_TESTREFERENCE,
              WG_TESTREFERENCE,
              RR_TESTREFERENCE
        FROM  TESTREFERENCE_VIEW T
        WHERE T.SKUID = li_LatestSkuId ;
  
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  ' There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetCertificateReportInfo',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || ' There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetCertificateReportInfo',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);         
            
         when others then            
              ls_ErrorMsg:=  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetCertificateReportInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
  end GetCertificateReportInfoBySKU;
  
   procedure GetImarkCertificationInfo(pc_ImarkCertification out retcursor,pd_DateSearchCriteria in Timestamp) as     
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
     
   begin
        if pd_DateSearchCriteria is null then 
                open pc_ImarkCertification for
                SELECT FAMILY,
                        SKU,
                        EMARKREFERENCE,
                        SIZESTAMP,
                        BRANDNAME,
                        SINGLOADINDEX,
                        DUALLOADINDEX,
                        SPEEDRATING,
                        DATESUBMITED,
                        DATEAPPROVED,
                        DISCONTINUEDDATE
                 FROM  IMARKCERTIFICATE_VIEW ;
                        
         else
              open pc_ImarkCertification for
              SELECT FAMILY,
                        SKU,
                        EMARKREFERENCE,
                        SIZESTAMP,
                        BRANDNAME,
                        SINGLOADINDEX,
                        DUALLOADINDEX,
                        SPEEDRATING,
                        DATESUBMITED,
                        DATEAPPROVED,
                        DISCONTINUEDDATE
                 FROM  IMARKCERTIFICATE_VIEW
                 WHERE datesubmited >= pd_DateSearchCriteria ;         
         end if ;
         
  
  EXCEPTION
        
         when others then            
              ls_ErrorMsg:=  'An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetImarkCertificationInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
   end GetImarkCertificationInfo;

  procedure GetEmarkCertificationInfo(pc_EmarkCertification out retcursor,
                                      pc_Product out retcursor) as
  
  li_LatestSkuId  product.skuid%type;
  
  begin
      
        Open pc_Product for
             SELECT p.SKUID,
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
                    MEARIMWIDTH,
                    UTQGTREADWEAR,
                    UTQGTRACTION,
                    UTQGTEMP,
                    MUDSNOWYN,
                    RIMDIAMETER
             from  product p 
                  inner join productCertificate pce on 
                          p.skuid = pce.skuid
                  inner join certificate ce on 
                           ce.certificateid      = pce.certificateid and
                          ce.certificationtypeid = pce.certificationtypeid
             where ce.certificationtypeid = 1;
             
          OPEN pc_EmarkCertification for         
          SELECT SKUID,
            CERTIFICATENUMBER,            
            CERTIFICATIONTYPEID,            
            DATESUBMITED,
            ACTIVESTATUS,
            DATEASSIGNED_EGI,
            DATEAPPROVED_CEGI,
            SUPPLEMENTALREQUIRED_EI,
            SUPPLEMENTALNUMBER_EI,
            JOBREPORTNUMBER_CEN,
            EXTENSION_EN,
            SUPPLEMENTALMOLDSTAMPING_E,
            SUPPLEMENTALDATEASSIGNED_E,
            SUPPLEMENTALDATESUBMITTED_E,
            SUPPLEMENTALDATEAPPROVED_E,
            WHEELTESTREFERENCE,
            NOISETESTREFERENCE,
            WGTESTREFERENCE,
            RRTESTREFERENCE
          FROM EMARKCERTIFICATIONREPORT_VIEW
          where certificationtypeid = 1;
         
             
             
  end GetEmarkCertificationInfo;
 

 Procedure GetEmarkPassengerWithTR( ps_CertificateNumber in varchar2,
                                     pi_tiretypeid        in number,
                                     pc_CertificateDfValue  out retCursor,
                                     pc_CertificateInfo   out retCursor,
                                     pc_Product           out retcursor, 
                                     pc_MeasureHDR        out retCursor,
                                     pc_measureDtl        out retCursor,
                                     pc_BEADUNSEATHDR     out retCursor,
                                     pc_BEADUNSEATDTL     out retCursor,
                                     pc_PLUNGERHDR        out retCursor,
                                     pc_PLUNGERDTL        out retCursor,
                                     pc_TREADWEARHDR      out retCursor,
                                     pc_TREADWEARDTL      out retCursor,
                                     pc_ENDURANCEHDR      out retCursor,
                                     pc_ENDURANCEDTL      out retCursor,
                                     pc_HIGHSPEEDHDR      out retCursor,
                                     pc_HIGHSPEEDDTL      out retCursor,
                                     pc_SPEEDTESTDETAIL   out retCursor,
                                     pc_Brand             out retCursor) as 

      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);   
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                                   
  begin
      
      if ps_CertificateNumber is null then
          raise li_ParametersAreNull;
      end if ;
      if ps_CertificateNumber = '' then
          raise li_ParametersAreInvalid;
      end if ;
            
      
      -- Gets the brand information
          Open pc_brand for
          SELECT *
          FROM  brand_view
          where lower(certificatenumber) = lower(ps_certificateNumber)  ; 
          
      --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                    MAX(DECODE(fieldid,1,fieldvalue,NULL)) MANUFACTURERNAME_E,
                    MAX(DECODE(fieldid,2,fieldvalue,NULL)) MANUFACTURERNAMEADDRESS_E,
                    MAX(DECODE(fieldid,3,fieldvalue,NULL)) TECHNICALSERVICE_E,
                    MAX(DECODE(fieldid,4,fieldvalue,NULL)) PLACE_E,
                    MAX(DECODE(fieldid,5,fieldvalue,NULL)) MEASURERIM_E,
                    MAX(DECODE(fieldid,6,fieldvalue,NULL)) INFLATIONPRESSURE_E,
                    MAX(DECODE(fieldid,7,fieldvalue,NULL)) TESTLABORATORY_E,
                    MAX(DECODE(fieldid,8,fieldvalue,NULL)) REPRESENTATIVENAME_E,
                    MAX(DECODE(fieldid,9,fieldvalue,NULL)) REPRESENTATIVEADDRESS_E,
                    MAX(DECODE(fieldid,10,fieldvalue,NULL)) REASONOFEXTENSION_E,
                    MAX(DECODE(fieldid,11,fieldvalue,NULL)) REMARKS_E
            FROM (
                SELECT FIELDID,
                  cdv.CERTIFICATIONTYPEID,
                  ce.CERTIFICATENUMBER,
                  FIELDVALUE
                FROM CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                             cdv.certificateid = ce.certificateid
                WHERE lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                      cdv.CERTIFICATIONTYPEID = 1 
              )
            group by CERTIFICATENUMBER ;
            
             Open pc_CertificateInfo for
             Select *
             From Certificate_View cv 
                      inner join productcertificate pce on
                            cv.certificationtypeid = pce.certificationtypeid and
                            cv.certificateid   = pce.certificateid
                      inner join product p on 
                            pce.skuid = p.skuid and
                            p.tiretypeid = pi_tiretypeid
             Where cv.CERTIFICATIONTYPEID = 1 And
                   lower(cv.certificatenumber) = lower(ps_certificateNumber);
            
             Open pc_Product for
              SELECT  *
              FROM  productdata_report_view
              where lower(certificatenumber) = lower(ps_certificateNumber)  and
                    TireTypeID = pi_tiretypeid and rownum < 2;  
            
            Open pc_MeasureHDR for
            SELECT  MEASUREID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    INFLATIONPRESSURE,
                    MOLDDESIGN,
                    RIMWIDTH,
                    DOTSERIALNUMBER,
                    DIAMETER,
                    AVGSECTIONWIDTH,
                    AVGOVERALLWIDTH,
                    MAXOVERALLWIDTH,
                    SIZEFACTOR,
                    MOUNTTIME,
                    MOUNTTEMP,
                    SERIALDATE,
                    ENDTIME,
                    ACTSIZEFACTOR,
                    ce.CERTIFICATIONTYPEID,
                    CERTIFICATENUMBER,                   
                    STARTINFLATIONPRESSURE,
                    ENDINFLATIONPRESSURE,
                    ADJUSTMENT,
                    CIRCUNFERENCE,
                    NOMINALDIAMETER,
                    NOMINALWIDTH,
                    NOMINALWIDTHPASSFAIL,
                    NOMINALWIDTHDIFERENCE,
                    NOMINALWIDTHTOLERANCE,
                    MAXOVERALLDIAMETER,
                    MINOVERALLDIAMETER,
                    OVERALLWIDTHPASSFAIL,
                    OVERALLDIAMETERPASSFAIL,
                    DIAMETERDIFERENCE,
                    DIAMETERTOLERANCE,
                    TEMPRESISTANCEGRADING,
                    TENSILESTRENGHT1,
                    TENSILESTRENGHT2,
                    ELONGATION1,
                    ELONGATION2,
                    TENSILESTRENGHTAFTERAGE1,
                    TENSILESTRENGHTAFTERAGE2
              FROM Certificate ce inner join MEASUREHDR m on 
                       ce.certificateid = m.certificateid and
                       ce.certificationtypeid = m.certificationtypeid
              where ce.certificationtypeid = 1 and
                    lower(ce.certificatenumber) = lower(ps_CertificateNumber);
              
              open pc_measureDtl for
              SELECT md.MEASUREID,
                    SECTIONWIDTH,
                    OVERALLWIDTH,
                    ITERATION
              FROM Certificate ce inner join MEASUREHDR m on
                       ce.certificateid = m.certificateid and
                       ce.certificationtypeid = m.certificationtypeid
                    inner join MEASUREDTL md on 
                           m.measureid = md.measureid
              where m.certificationtypeid = 1 and
                    lower(ce.certificatenumber) = lower(ps_CertificateNumber);
              
              open pc_BEADUNSEATHDR for
              SELECT BEADUNSEATID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    DOTSERIALNUMBER,
                    LOWESTUNSEATVALUE,
                    PASSYN,
                    b.CERTIFICATIONTYPEID,
                    CERTIFICATENUMBER,
                    SERIALDATE,
                    MINBEADUNSEAT,                    
                    TESTPASSFAIL
             FROM Certificate ce inner join BEADUNSEATHDR b on
                       ce.certificateid = b.certificateid and
                       ce.certificationtypeid = b.certificationtypeid
              where b.certificationtypeid = 1 and
                    lower(ce.certificatenumber) = lower(ps_CertificateNumber);
              
               open pc_BEADUNSEATDTL for   
               SELECT bd.BEADUNSEATID,
                      UNSEATFORCE,
                      ITERATION
               FROM Certificate ce 
                             inner join BEADUNSEATHDR b on
                                    ce.certificateid = b.certificateid and
                                    ce.certificationtypeid = b.certificationtypeid
                             inner join BEADUNSEATDTL bd on
                                   b.beadunseatid = bd.beadunseatid                                   
               where b.certificationtypeid = 1 and
                     lower(ce.certificatenumber) = lower(ps_CertificateNumber); 
                          
               open pc_PLUNGERHDR for 
               SELECT PLUNGERID,
                      PROJECTNUMBER,
                      TIRENUMBER,
                      TESTSPEC,
                      COMPLETIONDATE,
                      DOTSERIALNUMBER,
                      AVGBREAKINGENERGY,
                      PASSYN,
                      p.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER,
                      SERIALDATE,
                      MINPLUNGER 
                 FROM Certificate ce 
                             inner join  PLUNGERHDR p on
                                    ce.certificateid = p.certificateid and
                                    ce.certificationtypeid = p.certificationtypeid
                where p.certificationtypeid = 1 and
                       lower(ce.certificatenumber) = lower(ps_CertificateNumber);
                       
                open pc_PLUNGERDTL for
                SELECT pd.PLUNGERID,
                        BREAKINGENERGY,
                        ITERATION 
                 FROM Certificate ce 
                             inner join PLUNGERHDR ph on
                                   ce.certificateid       = ph.certificateid and
                                   ce.certificationtypeid = ph.certificationtypeid
                             inner join PLUNGERDTL pd on
                                  ph.plungerid = pd.plungerid
                where ph.certificationtypeid = 1 and
                      lower(ce.certificatenumber) = lower(ps_CertificateNumber);           
                                   
                
                open pc_TREADWEARHDR for
                SELECT TREADWEARID,
                      PROJECTNUMBER,
                      TIRENUMBER,
                      TESTSPEC,
                      COMPLETIONDATE,
                      DOTSERIALNUMBER,
                      LOWESTWEARBAR,
                      PASSYN,
                      SERIALDATE,
                      t.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER,
                      INDICATORSREQUIREMENT
                FROM Certificate ce 
                             inner join TREADWEARHDR t on
                                   ce.certificateid       = t.certificateid and
                                   ce.certificationtypeid = t.certificationtypeid
                where t.certificationtypeid = 1 and
                       lower(ce.certificatenumber) = lower(ps_CertificateNumber);
                
                 open pc_TREADWEARDTL for
                  SELECT  td.TREADWEARID,
                          WEARBARHEIGHT,
                          ITERATION
                 FROM Certificate ce 
                             inner join TREADWEARHDR t on
                                   ce.certificateid       = t.certificateid and
                                   ce.certificationtypeid = t.certificationtypeid
                            inner join TREADWEARDTL td on
                                   t.treadwearid = td.treadwearid
                where t.certificationtypeid = 1 and
                      lower(ce.certificatenumber) = lower(ps_CertificateNumber); 
              
                open pc_ENDURANCEHDR for
                SELECT ENDURANCEID,
                      PROJECTNUMBER,
                      TIRENUMBER,
                      TESTSPEC,
                      COMPLETIONDATE,
                      DOTSERIALNUMBER,
                      MFGWWYY,
                      PRECONDSTARTDATE,
                      PRECONDSTARTTEMP,
                      RIMDIAMETER,
                      RIMWIDTH,
                      PRECONDENDDATE,
                      PRECONDENDTEMP,
                      INFLATIONPRESSURE,
                      BEFOREDIAMETER,
                      AFTERDIAMETER,
                      BEFOREINFLATION,
                      AFTERINFLATION,
                      WHEELPOSITION,
                      WHEELNUMBER,
                      FINALTEMP,
                      FINALDISTANCE,
                      FINALINFLATION,
                      POSTCONDSTARTDATE,
                      POSTCONDENDDATE,
                      POSTCONDENDTEMP,
                      PASSYN,
                      e.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER,
                      SERIALDATE,
                      PRECONDTIME,
                      POSTCONDTIME,                     
                      DIAMETERTESTDRUM,
                      PRECONDTEMP,
                      INFLATIONPRESSUREREADJUSTED,
                      CIRCUNFERENCEBEFORETEST,
                      RESULTPASSFAIL,
                      ENDURANCEHOURS,
                      POSSIBLEFAILURESFOUND,
                      CIRCUNFERENCEAFTERTEST,
                      OUTERDIAMETERDIFERENCE,
                      ODDIFERENCETOLERANCE,
                      SERIENOM,
                      FINALJUDGEMENT,
                      APPROVER
                    FROM Certificate ce inner join ENDURANCEHDR e on
                               ce.certificateid = e.certificateid and
                               ce.certificationtypeid = e.certificationtypeid 
                    where e.certificationtypeid = 1 and
                       lower(ce.certificatenumber) = lower(ps_CertificateNumber);
                       
                    open pc_ENDURANCEDTL for
                    SELECT TESTSTEP,
                      TIMEINMIN,
                      SPEED,
                      TOTMILES,
                      LOAD,
                      LOADPERCENT,
                      SETINFLATION,
                      AMBTEMP,
                      INFPRESSURE,
                      STEPCOMPLETIONDATE,
                      ed.ENDURANCEID
                    FROM Certificate ce 
                              inner join ENDURANCEHDR e on
                                     ce.certificateid = e.certificateid and
                                     ce.certificationtypeid = e.certificationtypeid 
                              inner join ENDURANCEDTL ed on 
                                     e.enduranceid = ed.enduranceid
                    where e.certificationtypeid = 1 and
                          lower(ce.certificatenumber) = lower(ps_CertificateNumber);              

                    open pc_HIGHSPEEDHDR for
                    SELECT HIGHSPEEDID,
                      PROJECTNUMBER,
                      TIRENUM,
                      TESTSPEC,
                      COMPETIONDATE,
                      DOTSERIALNUMBER,
                      MFGWWYY,
                      PRECONDSTARTDATE,
                      PRECONDSARTTEMP,
                      RIMDIAMETER,
                      RIMWIDTH,
                      PRECONDENDDATE,
                      PRECONDENDTEMP,
                      INFLATIONPRESSURE,
                      BEFOREDIAMETER,
                      AFTERDIAMETER,
                      BEFOREINFLATION,
                      AFTERINFLATION,
                      WHEELPOSITION,
                      WHEELNUMBER,
                      FINALTEMP,
                      FINALDISTANCE,
                      FINALINFLATION,
                      POSTCONDSTARTDATE,
                      POSTCONDENDDATE,
                      POSTCONDENDTEMP,
                      PASSYN,
                      SERIALDATE,
                      POSTCONDTIME,
                      h.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER, 
                      DIAMETERTESTDRUM,
                      PRECONDTIME,
                      PRECONDTEMP,
                      INFLATIONPRESSUREREADJUSTED,
                      CIRCUNFERENCEBEFORETEST,
                      WHEELSPEEDRPM,
                      WHEELSPEEDKMH,
                      CIRCUNFERENCEAFTERTEST,
                      ODDIFERENCE,
                      ODDIFERENCETOLERANCE,
                      SERIENOM,
                      FINALJUDGEMENT,
                      APPROVER,
                      PASSATKMH,
                      SPEEDTTESTPASSFAIL,
                      SPEEDTOTALTIME,
                      MAXSPEED,
                      MAXLOAD
                    FROM Certificate ce inner join HIGHSPEEDHDR h on
                               ce.certificateid = h.certificateid and
                               ce.certificationtypeid = h.certificationtypeid 
                    where h.certificationtypeid = 1 and
                          lower(ce.certificatenumber) = lower(ps_CertificateNumber);
                            


                  open pc_HIGHSPEEDDTL for
                  SELECT TESTSTEP,
                    TIMEINMIN,
                    SPEED,
                    TOTMILES,
                    LOAD,
                    LOADPERCENT,
                    SETINFLATION,
                    AMBTEMP,
                    INFPRESSURE,
                    STEPCOMPLETIONDATE,
                    hd.HIGHSPEEDID
                  FROM Certificate ce 
                              inner join HIGHSPEEDHDR h on
                                     ce.certificateid = h.certificateid and
                                     ce.certificationtypeid = h.certificationtypeid 
                              inner join HIGHSPEEDDTL hd on 
                                     h.highspeedid = hd.highspeedid
                    where h.certificationtypeid = 1 and
                          lower(ce.certificatenumber) = lower(ps_CertificateNumber);
               
               
                  
                  open pc_SPEEDTESTDETAIL for
                  SELECT ITERATION,
                        TIME,
                        SPEED,
                        s.HIGHSPEEDID
                  FROM Certificate ce 
                              inner join HIGHSPEEDHDR h  on
                                     ce.certificateid = h.certificateid and
                                     ce.certificationtypeid = h.certificationtypeid 
                              inner join SPEEDTESTDETAIL s on
                                     h.highspeedid = s.highspeedid
                  WHERE h.certificationtypeid = 1 and
                        lower(ce.certificatenumber) = lower(ps_CertificateNumber);
                        
                        
  
           
            
  EXCEPTION     
          when li_ParametersAreNull then           
            ls_ErrorMsg:=  sqlerrm ||  '-GetEmarkPassengerWithTR.There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '-GetEmarkPassengerWithTR. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-GetEmarkPassengerWithTR. An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetEmarkPassengerWithTR',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
  end GetEmarkPassengerWithTR;
  
  procedure GetTraceabilityReportInfo(pc_Traceability out retcursor,
                                      ps_CertificateNumber in varchar2,
                                      pi_certificationTypeID in number) as 

      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);      
      
  begin
      
      if (ps_CertificateNumber is not null  or ps_CertificateNumber <> '' ) and pi_certificationTypeID > 0 then 
          Open pc_Traceability for        
          SELECT CERTIFICATENUMBER,
                CERTIFICATIONTYPEID,
                CERTIFICATIONTYPENAME,
                SKU,
                DATESUBMITED,
                DATEASSIGNED_EGI,
                DATEAPPROVED_CEGI,
                SUPPLEMENTALDATEASSIGNED_E,
                SUPPLEMENTALDATESUBMITTED_E,
                SUPPLEMENTALDATEAPPROVED_E,
                CERTIFICATEREQUESTED
            FROM ICS.TRACEABILITY_VIEW 
            WHERE lower(CERTIFICATENUMBER) = lower(ps_CertificateNumber) and
                  CERTIFICATIONTYPEID = pi_certificationTypeID;
                  
      elsif (ps_CertificateNumber is null  or ps_CertificateNumber = '' ) and pi_certificationTypeID  > 0 then
          Open pc_Traceability for        
          SELECT CERTIFICATENUMBER,
                CERTIFICATIONTYPEID,
                CERTIFICATIONTYPENAME,
                SKU,
                DATESUBMITED,
                DATEASSIGNED_EGI,
                DATEAPPROVED_CEGI,
                SUPPLEMENTALDATEASSIGNED_E,
                SUPPLEMENTALDATESUBMITTED_E,
                SUPPLEMENTALDATEAPPROVED_E,
                CERTIFICATEREQUESTED
            FROM ICS.TRACEABILITY_VIEW 
            WHERE CERTIFICATIONTYPEID = pi_certificationTypeID;
            
      elsif  (ps_CertificateNumber is not null  or ps_CertificateNumber <> '' ) and pi_certificationTypeID  = 0 then
            Open pc_Traceability for        
              SELECT CERTIFICATENUMBER,
                    CERTIFICATIONTYPEID,
                    CERTIFICATIONTYPENAME,
                    SKU,
                    DATESUBMITED,
                    DATEASSIGNED_EGI,
                    DATEAPPROVED_CEGI,
                    SUPPLEMENTALDATEASSIGNED_E,
                    SUPPLEMENTALDATESUBMITTED_E,
                    SUPPLEMENTALDATEAPPROVED_E,
                    CERTIFICATEREQUESTED
                FROM ICS.TRACEABILITY_VIEW 
                WHERE lower(CERTIFICATENUMBER) = lower(ps_CertificateNumber);
      end if;
    
   EXCEPTION  
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-GetTraceabilityReportInfo. An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetTraceabilityReportInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
  
  end GetTraceabilityReportInfo; 
  
  procedure GetExceptionReportInfo(pc_Exception out retcursor) as
  ls_MachineId VARCHAR2(50):=null;
  ls_OperatorId VARCHAR2(50):='ICSDEV';
  ls_ErrorMsg varchar2(4000);
  begin 
  
       Open pc_Exception For
         SELECT SKU,
                PRODUCTDATAFIELDNAME,
                LASTMODIFIED,
                ICSVALUE,
                SKUMASTERVALUE
         FROM ICS.EXCEPTIONREPORT ;
     
   EXCEPTION  
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-GetExceptionReportInfo. An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetExceptionReportInfo',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);  
  
  end  GetExceptionReportInfo;  
  
  procedure CompareSKUMainProductColumns as
  
  ls_MachineId VARCHAR2(50):=null;
  ls_OperatorId VARCHAR2(50):='ICSDEV';
  ls_ErrorMsg varchar2(4000);
  
  cursor SkuCol is
  select distinct(COLUMN_NAME)
  from ALL_TAB_COLUMNS
  where lower(table_name) = lower('SKUMAIN') and lower(owner) = lower('ics');
  
  cursor ProdCol is
  select distinct(COLUMN_NAME)
  from ALL_TAB_COLUMNS
  where lower(table_name) = lower('product') and lower(owner) = lower('ics');
  
  cursor CSKUMain is
  select distinct(sm.sku),sm.brandcode
  from SKUMAIN sm
  order by sm.brandcode;
  
  ls_Sku varchar(30);
  ls_BrandCode varchar2(10);
  
  ls_SMColNames varchar2(200);
  ls_ProdColNames varchar2(200);
  ls_Sql varchar2(5000);
  
  ls_SkuMainValue varchar2(200);
  ls_ProductValue varchar2(200);
  ld_ModifiedOn timestamp:= systimestamp;
  begin
      
      delete from EXCEPTIONREPORT;
      Commit;
      
      Open SkuCol; 
      Open CSKUMain;
      LOOP 
          FETCH SkuCol INTO ls_SMColNames ;
          FETCH CSKUMain INTO ls_Sku ,ls_BrandCode;
          EXIT WHEN SkuCol%NOTFOUND;
           Open ProdCol;
           Loop
              FETCH ProdCol INTO ls_ProdColNames ;
              EXIT WHEN ProdCol%NOTFOUND; 
              if (ls_SMColNames = ls_prodcolnames) AND 
                ( (LOWER(ls_prodcolnames) <> 'sku'  and LOWER(ls_SMColNames) <> 'sku') and
                  (lower(ls_prodcolnames) <> 'brandcode'And lower(ls_SMColNames) <> 'brandcode')
                )then  
                
                  ls_Sql :='Select ' || ls_SMColNames || ' FROM SKUMAIN WHERE SKU =:Sku and brandcode =:b and rownum < 2';                           
                  EXECUTE IMMEDIATE ls_Sql  into ls_SkuMainValue using ls_Sku,ls_BrandCode; 
                           
                  ls_Sql:='Select  ' || ls_SMColNames || ' FROM Product  WHERE SKU =:Sku and brandcode =:b and rownum < 2';
                   EXECUTE IMMEDIATE ls_Sql  into ls_ProductValue using ls_Sku,ls_BrandCode;         
                   
                 if nvl(ls_SkuMainValue,'IsNull') <> nvl(ls_ProductValue,'IsNull')  then
                    Insert INTO EXCEPTIONREPORT ( SKU,PRODUCTDATAFIELDNAME,LASTMODIFIED,ICSVALUE,SKUMASTERVALUE) 
                    VALUES (ls_Sku,ls_SMColNames,ld_ModifiedOn, nvl(ls_ProductValue,null), nvl(ls_SkuMainValue,null) );
                    Commit;
                 end if;
              end if;
           end Loop; 
           close ProdCol;
     END LOOP; 
     CLOSE SkuCol;
     close CSKUMain;
       
   EXCEPTION  
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-CompareSKUMainProductColumns. An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.CompareSKUMainProductColumns',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
  end CompareSKUMainProductColumns;
  
end reports_package;
/
