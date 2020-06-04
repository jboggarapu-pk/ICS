CREATE OR REPLACE package testresults_crud as 
  Type retCursor is ref cursor; 
  
  Procedure GetProductData(pc_retCursor out retCursor,ps_sku in varchar2,pi_SKUId in Number);
  
  procedure Product_Save(   pi_SKUID in   NUMBER,
                          ps_SKU in   VARCHAR2,
                          ps_BRANDCODE in   VARCHAR2,
                          pi_TIRETYPEID in   NUMBER,
                          pi_NPRID in   NUMBER,
                          ps_SIZESTAMP in   VARCHAR2,
                          pd_DISCONTINUEDDATE in   TIMESTAMP,
                          ps_SPECNUMBER in   VARCHAR2,
                          ps_SPEEDRATING in   VARCHAR2,
                          ps_SINGLOADINDEX in   VARCHAR2,
                          ps_DUALLOADINDEX in   VARCHAR2,
                          ps_BELTEDRADIALYN in   VARCHAR2,
                          ps_TUBELESSYN in   VARCHAR2,
                          ps_REINFORCEDYN in   VARCHAR2,
                          ps_EXTRALOADYN in   VARCHAR2,                          
                          ps_UTQGTREADWEAR in   VARCHAR2,
                          ps_UTQGTRACTION in   VARCHAR2,
                          ps_UTQGTEMP in   VARCHAR2,
                          ps_MUDSNOWYN in   VARCHAR2,
                          pi_RIMDIAMETER in   NUMBER,
                          pd_SERIALDATE in   TIMESTAMP,
                          ps_BRANDDESC in   VARCHAR2,
                          ps_LOADRANGE in   VARCHAR2,
                          pi_MEARIMWIDTH in   NUMBER,
                          ps_REGROOVABLEIND in   VARCHAR2,
                          ps_PLANTPRODUCED in   VARCHAR2,
                          pd_MOSTRECENTTESTDATE in   TIMESTAMP,
                          ps_IMARK in   VARCHAR2,                         
                          ps_INFORMENUMBER in   VARCHAR2,
                          pd_FECHADATE in   TIMESTAMP,
                          ps_TREADPATTERN in   VARCHAR2,
                          ps_SPECIALPROTECTIVEBAND in   VARCHAR2,
                          ps_NOMINALTIREWIDTH in   VARCHAR2,
                          ps_ASPECTRADIO in   VARCHAR2,
                          ps_TREADWEARINDICATORS in   VARCHAR2,
                          ps_NAMEOFMANUFACTURER in   VARCHAR2,
                          ps_FAMILY in   VARCHAR2,
                          ps_DOTSERIALNUMBER in   VARCHAR2,
                          ps_OperatorName   in Varchar2) ;
                          
                       
   PROCEDURE GetMeasure(pc_MeasureCursor out retCursor,
                         pc_MeasureDetailCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number)  ;
                         
  Procedure GetPlunger(  pc_PlungerHdrCursor out retCursor,
                         pc_PlungerDtlCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number) ;
                         
 Procedure GetTreadWear(pc_TreadWearHdrCursor out retCursor,
                         pc_TreadWearDtlCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number);  
 
 Procedure GetBeadUnseat(pc_BeadUnseatHdrCursor out retCursor,
                         pc_BeadUnseatDtlCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number);  
                         
  procedure GetENDURANCE(pc_EnduranceHdrCursor out retCursor,
                         pc_EnduranceDtlCursor out retCursor,                        
                         pi_SKUID in Number,
                         ps_CertificateNumber in varchar,
                         pi_CertificationTypeID in Number); 

  PROCEDURE GetHighSpeed(pc_HighSpeedCursor out retCursor,
                         pc_HighSpeedDetailCursor out retCursor, 
                         pc_HSSpeedTestDetail out retCursor,
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number);         

 PROCEDURE GetSound(pc_SoundHDRCursor out retCursor,
                         pc_SoundDetailCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number);

PROCEDURE GetWetGrip(pc_WetGripHDRCursor out retCursor,
                         pc_WetGripDetailCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number);
                         
                           
  procedure GetTestResults(pc_MeasureCursor out retcursor,
                           pc_MeasureDetailCursor out retcursor,                         
                           PC_PLUNGERHDRCURSOR out retcursor,
                           PC_PLUNGERDTLCURSOR out retcursor,
                           PC_TREADWEARHDRCURSOR out retcursor,
                           PC_TREADWEARDTLCURSOR out retcursor,
                           PC_BEADUNSEATHDRCURSOR out retcursor,
                           PC_BEADUNSEATDTLCURSOR out retcursor,
                           PC_ENDURANCEHDRCURSOR out retcursor,
                           PC_ENDURANCEDTLCURSOR out retcursor,
                           PC_HIGHSPEEDCURSOR  out retcursor,
                           PC_HIGHSPEEDDETAILCURSOR  out retcursor,
                           PC_HSSPEEDTESTDETAIL     out retcursor,
                           PC_SOUNDHdrCURSOR  out retcursor,
                           PC_SOUNDDETAILCURSOR  out retcursor,
                           PC_WETGRIPHDRCURSOR  out retcursor,
                           PC_WETGRIPDETAILCURSOR  out retcursor,                           
                           pi_CertificationTypeID in number,                          
                           pi_SKUId in number,
                           ps_CertificateNumber in varchar);
                           
  
   Procedure Measure_Save( pi_MEASUREID out Number,
                          pi_CertificateID  in number,
                          ps_PROJECTNUMBER in Varchar2,
                          pi_TIRENUMBER in Number,
                          ps_TESTSPEC in Varchar2,
                          pd_COMPLETIONDATE in TimeStamp,
                          pi_INFLATIONPRESSURE in Number,
                          ps_MOLDDESIGN in Varchar2,
                          pi_RIMWIDTH in Number,
                          ps_DOTSERIALNUMBER in Varchar2,
                          pi_DIAMETER in Number,
                          pi_AVGSECTIONWIDTH in Number,
                          pi_AVGOVERALLWIDTH in Number,
                          pi_MAXOVERALLWIDTH in Number,
                          pi_SIZEFACTOR in Number,
                          pd_MOUNTTIME in TimeStamp,
                          pi_MOUNTTEMP in Number,
                          pd_SERIALDATE in TimeStamp,
                          pd_ENDTIME in TimeStamp,
                          pi_ACTSIZEFACTOR in Number,
                          pi_CERTIFICATIONTYPEID in Number,                          
                          pi_STARTINFLATIONPRESSURE in Number,
                          pi_ENDINFLATIONPRESSURE in Number,
                          ps_ADJUSTMENT in Varchar2,
                          pi_CIRCUNFERENCE in Number,
                          pi_NOMINALDIAMETER in Number,
                          pi_NOMINALWIDTH in Number,
                          ps_NOMINALWIDTHPASSFAIL in Varchar2,
                          pi_NOMINALWIDTHDIFERENCE in Number,
                          pi_NOMINALWIDTHTOLERANCE in Number,
                          pi_MAXOVERALLDIAMETER in Number,
                          pi_MINOVERALLDIAMETER in Number,
                          ps_OVERALLWIDTHPASSFAIL in Varchar2,
                          ps_OVERALLDIAMETERPASSFAIL in Varchar2,
                          pi_DIAMETERDIFERENCE in Number,
                          pi_DIAMETERTOLERANCE in Number,
                          pi_TEMPRESISTANCEGRADING in Number,
                          pi_TENSILESTRENGHT1 in Number,
                          pi_TENSILESTRENGHT2 in Number,
                          pi_ELONGATION1 in Number,
                          pi_ELONGATION2 in Number,
                          pi_TENSILESTRENGHTAFTERAGE1 in Number,
                          pi_TENSILESTRENGHTAFTERAGE2 in Number,
                          ps_OperatorName   in varchar2);
                          
  Procedure MeasureDetail_Save(pi_SECTIONWIDTH in  MEASUREDTL.SECTIONWIDTH%Type,
                               pi_OVERALLWIDTH in  MEASUREDTL.OVERALLWIDTH%Type,
                               pi_MEASUREID in number,
                               PI_ITERATION IN NUMBER,
                               ps_OperatorName   in varchar2 ); 

  
   procedure Endurance_Save( pi_ENDURANCEID out number,
                            ps_PROJECTNUMBER in varchar2,
                            pi_TIRENUMBER in number,
                            ps_TESTSPEC in varchar2,
                            pd_COMPLETIONDATE in date,
                            ps_DOTSERIALNUMBER in varchar2,
                            ps_MFGWWYY in varchar2,
                            pd_PRECONDSTARTDATE in date,
                            pi_PRECONDSTARTTEMP in number,
                            pi_RIMDIAMETER in number,
                            pi_RIMWIDTH in number,
                            pd_PRECONDENDDATE in date,
                            pi_PRECONDENDTEMP in number,
                            pi_INFLATIONPRESSURE in number,
                            pi_BEFOREDIAMETER in number,
                            pi_AFTERDIAMETER in number,
                            pi_BEFOREINFLATION in number,
                            pi_AFTERINFLATION in number,
                            pi_WHEELPOSITION in number,
                            pi_WHEELNUMBER in number,
                            pi_FINALTEMP in number,
                            pi_FINALDISTANCE in number,
                            pi_FINALINFLATION in number,
                            pd_POSTCONDSTARTDATE in date,
                            pd_POSTCONDENDDATE in date,
                            pi_POSTCONDENDTEMP in number,
                            ps_PASSYN in varchar2,                                                  
                            pi_CertificationTypeID in number,                            
                            pd_SerialDate in date,
                            pi_PreCondTime in number,
                            pi_PostCondTime  in number,
                            pi_DIAMETERTESTDRUM in Number,
                            pi_PRECONDTEMP in Number,
                            pi_INFLATIONPRESSUREREADJUSTED in Number,
                            pi_CIRCUNFERENCEBEFORETEST in Number,
                            ps_RESULTPASSFAIL in Varchar2,
                            pi_ENDURANCEHOURS in Number,
                            ps_POSSIBLEFAILURESFOUND in Varchar2,
                            pi_CIRCUNFERENCEAFTERTEST in Number,
                            pi_OUTERDIAMETERDIFERENCE in Number,
                            pi_ODDIFERENCETOLERANCE in Number,
                            ps_SERIENOM in Varchar2,
                            ps_FINALJUDGEMENT in Varchar2,
                            ps_APPROVER in Varchar2,
                            ps_OperatorName in  Varchar2,
                            pi_certificateid in number)  ;
                            
 PROCEDURE ENDURANCEDETAIL_SAVE( PI_TESTSTEP IN NUMBER,
                                  pi_TIMEINMIN IN NUMBER,
                                  PI_SPEED IN NUMBER,
                                  PI_TOTMILES IN NUMBER,
                                  PI_LOAD IN NUMBER,
                                  PI_LOADPERCENT IN NUMBER,
                                  PI_SETINFLATION IN NUMBER,
                                  PI_AMBTEMP IN NUMBER,
                                  PI_INFPRESSURE IN NUMBER,
                                  PD_STEPCOMPLETIONDATE IN EnduranceDtl.STEPCOMPLETIONDATE%Type,
                                  PI_ENDURANCEID IN NUMBER); 
  
  PROCEDURE TREADWEAR_SAVE(PI_TREADWEARID OUT NUMBER,
                           PS_PROJECTNUMBER  IN VARCHAR2,
                           PI_TIRENUMBER IN NUMBER,
                           PS_TESTSPEC  IN VARCHAR2,
                           PD_COMPLETIONDATE IN DATE,
                           PS_DOTSERIALNUMBER  IN VARCHAR2,
                           PI_LOWESTWEARBAR IN NUMBER,
                           PS_PASSYN  IN VARCHAR2,                          
                           pi_CertificationTypeID in number,
                           PD_SERIALDATE IN DATE,
                           ps_OperatorName in varchar2,
                           pi_INDICATORSREQUIREMENT in number,
                           pi_CertificateID in Number);
  
  PROCEDURE TREADWEARDETAIL_SAVE(PI_TREADWEARID in NUMBER, 
                                 PI_WEARBARHEIGHT IN  treadweardtl.wearbarheight%type,
                                 PI_ITERATION IN  treadweardtl.iteration%TYPE,
                                 ps_OperatorName in varchar2)  ;                                 
                                  
   procedure PLUNGER_Save(  pi_PLUNGERID out Number,
                            ps_PROJECTNUMBER in Varchar2,
                            pi_TIRENUMBER in Number,
                            ps_TESTSPEC in Varchar2,
                            pd_COMPLETIONDATE in TimeStamp,
                            ps_DOTSERIALNUMBER in Varchar2,
                            pi_AVGBREAKINGENERGY in Number,
                            ps_PASSYN in Varchar2,
                            pi_CERTIFICATIONTYPEID in Number,                            
                            pd_SERIALDATE in TimeStamp,
                            pi_MINPLUNGER in Number,
                            ps_OperatorName in varchar2 ,
                           pi_CertificateID in Number);
  
  procedure PLUNGERDETAIL_Save(PI_BREAKINGENERGY  IN NUMBER,
                               PI_PLUNGERID  IN NUMBER,
                               PI_ITERATION IN NUMBER,
                               ps_OperatorName in varchar2) ;
  
  Procedure BeadUnseat_Save(pi_BEADUNSEATID out Number,
                            ps_PROJECTNUMBER in Varchar2,
                            pi_TIRENUMBER in Number,
                            ps_TESTSPEC in Varchar2,
                            pd_COMPLETIONDATE in TimeStamp,
                            ps_DOTSERIALNUMBER in Varchar2,
                            pi_LOWESTUNSEATVALUE in Number,
                            ps_PASSYN in Varchar2,
                            pi_CERTIFICATIONTYPEID in Number,                            
                            pd_SERIALDATE in TimeStamp,
                            pi_MINBEADUNSEAT in Number,                            
                            ps_TESTPASSFAIL in Varchar2,
                            ps_OperatorName   in varchar2,
                            pi_CertificateID in Number);
                            
  Procedure BeadUnseatDetail_Save(pi_BEADUNSEATID in NUMBER, 
                                  pi_UNSEATFORCE in NUMBER,
                                  PI_ITERATION IN NUMBER,
                                  ps_OperatorName   in varchar2);
  
   procedure HighSpeedHdr_Save( pi_HIGHSPEEDID          out number,
                                ps_PROJECTNUMBER        in varchar2,
                                pi_TIRENUM              in number  ,
                                ps_TESTSPEC             in varchar2,
                                pd_COMPETIONDATE        in TIMESTAMP,
                                ps_DOTSERIALNUMBER      in varchar2,
                                ps_MFGWWYY              in varchar2,
                                pd_PRECONDSTARTDATE     in TIMESTAMP,
                                pi_PRECONDSARTTEMP      in number ,
                                pd_precondtime          in  highspeedhdr.precondtime%type,
                                pi_RIMDIAMETER          in  highspeedhdr.rimdiameter%type,
                                pi_RIMWIDTH             in  highspeedhdr.rimwidth%type,
                                pd_PRECONDENDDATE       in TIMESTAMP,
                                pi_PRECONDENDTEMP       in number ,
                                pi_INFLATIONPRESSURE    in number ,
                                pi_BEFOREDIAMETER       in  highspeedhdr.BEFOREDIAMETER%type,
                                pi_AFTERDIAMETER        in  highspeedhdr.AFTERDIAMETER%type,
                                pi_BEFOREINFLATION      in number ,
                                pi_AFTERINFLATION       in number ,
                                pi_WHEELPOSITION        in number ,
                                pi_WHEELNUMBER          in number ,
                                pi_FINALTEMP            in number ,
                                pi_FINALDISTANCE        in  highspeedhdr.FINALDISTANCE%type,
                                pi_FINALINFLATION       in number ,
                                pd_POSTCONDSTARTDATE    in TIMESTAMP,
                                pd_POSTCONDENDDATE      in TIMESTAMP,
                                pi_POSTCONDENDTEMP      in number ,
                                ps_PASSYN               in varchar2,
                                pd_SERIALDATE           in TIMESTAMP,
                                pi_POSTCONDTIME         in  highspeedhdr.POSTCONDTIME%type,
                                pi_CERTIFICATIONTYPEID  in number ,                                
                                pi_DIAMETERTESTDRUM     in Number,                               
                                pi_PRECONDTEMP          in Number,
                                pi_INFLATIONPRESSUREREADJUSTED in Number,
                                pi_CIRCUNFERENCEBEFORETEST     in Number,
                                pi_WHEELSPEEDRPM        in Number,
                                pi_WHEELSPEEDKMH        in Number,
                                pi_CIRCUNFERENCEAFTERTEST in Number,
                                pi_ODDIFERENCE            in Number,
                                pi_ODDIFERENCETOLERANCE   in Number,
                                ps_SERIENOM               in Varchar2,
                                ps_FINALJUDGEMENT         in Varchar2,
                                ps_APPROVER               in Varchar2,
                                pi_PASSATKMH              in Number,
                                ps_SPEEDTTESTPASSFAIL     in Varchar2,
                                pi_SPEEDTOTALTIME         in Number,
                                pi_MAXSPEED     in Number,
                                pi_MAXLOAD      in Number,
                                ps_OperatorName in Varchar2,
                               pi_CertificateID in Number);
  
  procedure HighSpeedDetail_Save ( pi_HIGHSPEEDID in Number,
                                    pi_TESTSTEP in number,
                                    pi_TIMEINMIN in Number,
                                    pi_SPEED in  HighSpeedDtl.SPEED%Type,
                                    pi_TOTMILES in  HighSpeedDtl.TOTMILES%Type,
                                    pi_LOAD in  HighSpeedDtl.LOAD%Type,
                                    pi_LOADPERCENT in Number,
                                    pi_SETINFLATION in Number,
                                    pi_AMBTEMP in Number,
                                    pi_INFPRESSURE in Number,
                                    pd_STEPCOMPLETIONDATE in HighSpeedDtl.STEPCOMPLETIONDATE%Type,                                    
                                    ps_OperatorID in varchar2);
                                    
Procedure HIghSpeed_SpeedTestDetail_Save(pi_ITERATION in Number,
                                            pd_TIME in TimeStamp,
                                            pi_SPEED in Number,
                                            pi_HIGHSPEEDID in Number,
                                            ps_OperatorName in Varchar2);
  
  procedure SoundHDR_Save(   ps_UserID                     in varchar2,
                              pi_SoundID                    out number,
                              ps_PROJECTNUMBER              in varchar2,
                              pi_TIRENUMBER                 in number,
                              ps_TESTSPEC                   in varchar2,
                              ps_TESTREPORTNUMBER           in varchar2,
                              ps_MANUFACTUREANDBRAND        in varchar2,
                              ps_TIRECLASS                  in varchar2,
                              ps_CATEGORYOFUSE              in varchar2,
                              pd_DATEOFTEST                 in TIMESTAMP,
                              ps_TESTVEHICULE               in varchar2,
                              ps_TESTVEHICULEWHEELBASE      in varchar2,
                              ps_LOCATIONOFTESTTRACK        in varchar2,
                              pd_DATETRACKCERTIFTOISO       in TIMESTAMP,
                              ps_TIRESIZEDESIGNATION        in varchar2,
                              ps_TIRESERVICEDESCRIPTION     in varchar2,
                              ps_TESTMASS_FRONTL            in varchar2,
                              ps_TESTMASS_FRONTR            in varchar2,
                              ps_TESTMASS_REARL             in varchar2,
                              ps_TESTMASS_REARR             in varchar2,
                              ps_TIRELOADINDEX_FRONTL       in varchar2,
                              ps_TIRELOADINDEX_FRONTR       in varchar2,
                              ps_TIRELOADINDEX_REARL        in varchar2,
                              ps_TIRELOADINDEX_REARR        in varchar2,
                              ps_INFLATIONPRESSURECO_FRONTL in varchar2,
                              ps_INFLATIONPRESSURECO_FRONTR in varchar2,
                              ps_INFLATIONPRESSURECO_REARL  in varchar2,
                              ps_INFLATIONPRESSURECO_REARR  in varchar2,
                              ps_TESTRIMWIDTHCODE           in varchar2,
                              ps_TEMPMEASURESENSORTYPE      in varchar2,
                              pi_CERTIFICATIONTYPEID        in number,                             
                              pi_SKUID                      in number,
                              ps_ReferenceInflationPressure in varchar2,
                              pi_CertificateID              in Number);

  procedure SoundDetail_Save( ps_UserID in varchar2,
                              pi_ITERATION in number,
                              ps_TESTSPEED  in varchar2,
                              ps_DIRECTIONOFRUN  in varchar2,
                              ps_SOUNDLEVELLEFT  in varchar2,
                              ps_SOUNDLEVELRIGHT  in varchar2,
                              ps_AIRTEMP  in varchar2,
                              ps_TRACKTEMP in varchar2,
                              ps_SOUNDLEVELLEFT_TEMPCOR in varchar2,
                              ps_SOUNDLEVELRIGHT_TEMPCOR  in varchar2,
                              pi_SOUNDID in number);  
                              
   procedure WetGripHDR_Save( ps_UserID in varchar2,
                              pi_WETGRIPID  out Number,
                              ps_PROJECTNUMBER  in Varchar2,
                              pi_TIRENUMBER  in Varchar2,
                              ps_TESTSPEC  in Varchar2,
                              pd_DATEOFTEST  in TIMESTAMP,
                              ps_TESTVEHICLE  in Varchar2,
                              ps_LOCATIONOFTESTTRACK  in Varchar2,
                              ps_TESTTRACKCHARACTERISTICS  in Varchar2,
                              ps_ISSUEBY  in Varchar2,
                              ps_METHODOFCERTIFICATION  in Varchar2,
                              ps_TESTTIREDETAILS  in Varchar2,
                              ps_TIRESIZEANDSERVICEDESC  in Varchar2,
                              ps_TIREBRANDANDTRADEDESC  in Varchar2,
                              ps_REFERENCEINFLATIONPRESSURE  in Varchar2,
                              ps_TESTRIMWITHCODE  in Varchar2,
                              ps_TEMPMEASURESENSORTYPE  in Varchar2,
                              ps_IDENTIFICATIONSRTT  in Varchar2,
                              ps_TESTTIRELOAD_SRTT  in Varchar2,
                              ps_TESTTIRELOAD_CANDIDATE  in Varchar2,
                              ps_TESTTIRELOAD_CONTROL  in Varchar2,
                              ps_WATERDEPTH_SRTT  in Varchar2,
                              ps_WATERDEPTH_CANDIDATE  in Varchar2,
                              ps_WATERDEPTH_CONTROL  in Varchar2,
                              ps_WETTEDTRACKTEMPAVG  in Varchar2,
                              pi_CERTIFICATIONTYPEID  in Number,                              
                              pi_SKUID  in Number,
                               pi_CertificateID in Number);
                            
                             
   procedure WetGripDetail_Save( ps_UserID in varchar2,
                                 pi_ITERATION  in number,
                                  ps_TESTSPEED  in varchar2,
                                  ps_DIRECTIONOFRUN  in varchar2,
                                  ps_SRTT  in varchar2,
                                  ps_CANDIDATETIRE  in varchar2,
                                  ps_PEAKBREAKFORCECOEFICIENT  in varchar2,
                                  ps_MEANFULLYDEVDECELERATION  in varchar2,
                                  ps_WETGRIPINDEX  in varchar2,
                                  ps_COMMENTS  in varchar2,
                                  pi_WETGRIPID  in number);                             
  
  Function GetMeasureID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number) return Number;
  Function GetPlungerID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number;
  Function GetTreadWearID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number;
  Function GetEnduranceID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number;
  Function GetBeadUnseatID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number) return Number;
  Function GetHighSpeedID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number; 
  function GetWetGripHDRID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number,pi_SKUId in number) return Number;
  
  function GetSoundHDRID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number) return Number ;
  
  Function CheckIfProductExists(ps_SKU	in VARCHAR2,pi_SKUId in number) return Varchar2;
  Function CheckIfBeadUnseatExists(PI_CertificateID	in Number,pi_CertificationTypeID in number) return Varchar2;
  Function CheckIfEnduranceExists(PI_CertificateID	in Number,pi_CertificationTypeID in number) return Varchar2;
  Function CheckIfHighSpeedExists(PI_CertificateID	in Number,pi_CertificationTypeID in number) return Varchar2;   
  Function CheckIfTreadWearExists(PI_CertificateID	in Number,pi_CertificationTypeID in number) return Varchar2;  
  Function CheckIfPlungerExists(PI_CertificateID	in Number, pi_CertificationTypeID in number) return Varchar2; 
 Function CheckIfMeasureExists(pi_CertificateID	in number,pi_CertificationTypeID in number) return Varchar2;
  Function CheckIfSoundExixts  (PI_CertificateID	in Number, pi_CertificationTypeID in number)  return Varchar2;
  Function CheckIfWetGripExixts(PI_CertificateID	in Number, pi_CertificationTypeID in number)  return Varchar2;
   
end testresults_crud;
/


CREATE OR REPLACE package body testresults_crud as

  Procedure GetProductData(pc_retCursor out retCursor,ps_sku in varchar2,pi_SKUId in Number) as  
  --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParameterIsInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParameterIsInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
  begin
        if ps_sku is null then
              raise li_ParametersAreNull;
        end if;
        if ps_sku = '' then
              raise li_ParameterIsInvalid;
        end if;
            
        Open pc_retCursor FOR
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
                DOTSERIALNUMBER
        FROM  PRODUCT 
        WHERE SKU = ps_sku And 
              SKUID = pi_SKUId;
                  
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || '- GetProducts.  There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetProducts',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
                
            raise_application_error (-20005,sqlerrm);
            
         when li_ParameterIsInvalid then           
            ls_ErrorMsg:= sqlerrm || '- GetProducts.  There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetProducts',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            
            raise_application_error (-20006,sqlerrm);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- GetProducts. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetProducts',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               
               raise_application_error (-20007,sqlerrm);
        
  end GetProductData;
  
  procedure GetTestResults(pc_MeasureCursor out retcursor,
                           pc_MeasureDetailCursor out retcursor,                         
                           PC_PLUNGERHDRCURSOR out retcursor,
                           PC_PLUNGERDTLCURSOR out retcursor,
                           PC_TREADWEARHDRCURSOR out retcursor,
                           PC_TREADWEARDTLCURSOR out retcursor,
                           PC_BEADUNSEATHDRCURSOR out retcursor,
                           PC_BEADUNSEATDTLCURSOR out retcursor,
                           PC_ENDURANCEHDRCURSOR out retcursor,
                           PC_ENDURANCEDTLCURSOR out retcursor,
                           PC_HIGHSPEEDCURSOR  out retcursor,
                           PC_HIGHSPEEDDETAILCURSOR  out retcursor,
                           PC_HSSPEEDTESTDETAIL     out retcursor,
                           PC_SOUNDHdrCURSOR  out retcursor,
                           PC_SOUNDDETAILCURSOR  out retcursor,
                           PC_WETGRIPHDRCURSOR  out retcursor,
                           PC_WETGRIPDETAILCURSOR  out retcursor,     
                           pi_CertificationTypeID in number,                          
                           pi_SKUId in number,
                           ps_CertificateNumber in varchar) as
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006); 
      
      ls_skuExists varchar2(1);
      ls_MeasureExists varchar2(1);
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      ls_LastImarkCertNum   CERTIFICATE.CERTIFICATENUMBER%type;
      
      li_MeasureId  MeasureHdr.measureid%type;
      li_CertificateID number;
  begin
 
        if pi_skuid is null or pi_CertificationTypeID is null or ps_CertificateNumber is null then
          raise li_ParametersAreNull;
       end if;
      
      if pi_skuid <= 0 or pi_CertificationTypeID <= 0 or ps_CertificateNumber = '' then
              raise li_ParametersAreInvalid;
      end if;
    /*
    Since Imark has one and only one certificate number,
    I am getting the latest one that has the I033 prefix and 
    using it to get the information regarding the tests results.
    */
      if pi_CertificationTypeID = 4 then
           ls_LastImarkCertNum:= ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATENUM();
           li_CertificateID := ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(ls_LastImarkCertNum,pi_CertificationTypeID);
                 --Gets the Measure Table
            TESTRESULTS_CRUD.GETMEASURE(
                                      pc_MeasureCursor => pc_MeasureCursor,
                                      pc_MeasureDetailCursor => pc_MeasureDetailCursor,
                                      PI_SKUID => pi_SKUId,
                                      PS_CERTIFICATENUMBER => ls_LastImarkCertNum,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                    );
                                   
           TESTRESULTS_CRUD.GETPLUNGER(
                                        PC_PLUNGERHDRCURSOR => PC_PLUNGERHDRCURSOR,
                                        PC_PLUNGERDTLCURSOR => PC_PLUNGERDTLCURSOR,
                                        PI_SKUID => pi_SKUId,
                                        PS_CERTIFICATENUMBER => ls_LastImarkCertNum,
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                      );                      
                                    
           TESTRESULTS_CRUD.GETTREADWEAR(
                                      PC_TREADWEARHDRCURSOR => PC_TREADWEARHDRCURSOR,
                                      PC_TREADWEARDTLCURSOR => PC_TREADWEARDTLCURSOR,
                                      PI_SKUID => pi_SKUId,
                                      PS_CERTIFICATENUMBER => ls_LastImarkCertNum,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                    );
                                    
           TESTRESULTS_CRUD.GETBEADUNSEAT(
                                    PC_BEADUNSEATHDRCURSOR => PC_BEADUNSEATHDRCURSOR,
                                    PC_BEADUNSEATDTLCURSOR => PC_BEADUNSEATDTLCURSOR,
                                    PI_SKUID => PI_SKUID,
                                    PS_CERTIFICATENUMBER => ls_LastImarkCertNum,
                                    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                  );
                                  
           TESTRESULTS_CRUD.GETENDURANCE(
                                    PC_ENDURANCEHDRCURSOR => PC_ENDURANCEHDRCURSOR,
                                    PC_ENDURANCEDTLCURSOR => PC_ENDURANCEDTLCURSOR,
                                    PI_SKUID => PI_SKUID,
                                    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                  );
                                  
          TESTRESULTS_CRUD.GETHIGHSPEED(
                            PC_HIGHSPEEDCURSOR => PC_HIGHSPEEDCURSOR,
                            PC_HIGHSPEEDDETAILCURSOR => PC_HIGHSPEEDDETAILCURSOR,
                            PC_HSSPEEDTESTDETAIL => PC_HSSPEEDTESTDETAIL,
                            PI_SKUID => PI_SKUID,
                            PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                            PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID);                                  
                                  
          

           TESTRESULTS_CRUD.GETSOUND(
                                    PC_SOUNDHDRCURSOR => PC_SOUNDHDRCURSOR,
                                    PC_SOUNDDETAILCURSOR => PC_SOUNDDETAILCURSOR,
                                    PI_SKUID => PI_SKUID,
                                    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                  );
                                  
          TESTRESULTS_CRUD.GETWETGRIP(
                                    PC_WETGRIPHDRCURSOR => PC_WETGRIPHDRCURSOR,
                                    PC_WETGRIPDETAILCURSOR => PC_WETGRIPDETAILCURSOR,
                                    PI_SKUID => PI_SKUID,
                                    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                  );
                                  
      else  
            --Gets the Measure Table
            TESTRESULTS_CRUD.GETMEASURE(
                                      pc_MeasureCursor => pc_MeasureCursor,
                                      pc_MeasureDetailCursor => pc_MeasureDetailCursor,
                                      PI_SKUID => pi_SKUId,
                                      PS_CERTIFICATENUMBER => ps_CertificateNumber,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                    );
                                   
           TESTRESULTS_CRUD.GETPLUNGER(
                                        PC_PLUNGERHDRCURSOR => PC_PLUNGERHDRCURSOR,
                                        PC_PLUNGERDTLCURSOR => PC_PLUNGERDTLCURSOR,
                                        PI_SKUID => pi_SKUId,
                                        PS_CERTIFICATENUMBER => ps_CertificateNumber,
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                      );                      
                                    
           TESTRESULTS_CRUD.GETTREADWEAR(
                                      PC_TREADWEARHDRCURSOR => PC_TREADWEARHDRCURSOR,
                                      PC_TREADWEARDTLCURSOR => PC_TREADWEARDTLCURSOR,
                                      PI_SKUID => pi_SKUId,
                                      PS_CERTIFICATENUMBER => ps_CertificateNumber,
                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                    );
                                    
           TESTRESULTS_CRUD.GETBEADUNSEAT(
                                    PC_BEADUNSEATHDRCURSOR => PC_BEADUNSEATHDRCURSOR,
                                    PC_BEADUNSEATDTLCURSOR => PC_BEADUNSEATDTLCURSOR,
                                    PI_SKUID => PI_SKUID,
                                    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                  );
                                  
           TESTRESULTS_CRUD.GETENDURANCE(
                                    PC_ENDURANCEHDRCURSOR => PC_ENDURANCEHDRCURSOR,
                                    PC_ENDURANCEDTLCURSOR => PC_ENDURANCEDTLCURSOR,
                                    PI_SKUID => PI_SKUID,
                                    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                  );
                                  
            TESTRESULTS_CRUD.GETHIGHSPEED(
                            PC_HIGHSPEEDCURSOR => PC_HIGHSPEEDCURSOR,
                            PC_HIGHSPEEDDETAILCURSOR => PC_HIGHSPEEDDETAILCURSOR,
                            PC_HSSPEEDTESTDETAIL => PC_HSSPEEDTESTDETAIL,
                            PI_SKUID => PI_SKUID,
                            PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                            PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID); 
                                  
               TESTRESULTS_CRUD.GETSOUND(
                                          PC_SOUNDHDRCURSOR => PC_SOUNDHDRCURSOR,
                                          PC_SOUNDDETAILCURSOR => PC_SOUNDDETAILCURSOR,
                                          PI_SKUID => PI_SKUID,
                                          PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                          PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                        );
                                        
                TESTRESULTS_CRUD.GETWETGRIP(
                                          PC_WETGRIPHDRCURSOR => PC_WETGRIPHDRCURSOR,
                                          PC_WETGRIPDETAILCURSOR => PC_WETGRIPDETAILCURSOR,
                                          PI_SKUID => PI_SKUID,
                                          PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                          PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                        );                            
      end if;                             
       
     EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  '- GetTestresults. There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetTestresults',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetTestresults. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetTestresults',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);          
            
         when others then            
              ls_ErrorMsg:=  '- GetTestresults. An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetTestresults',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
                 
  end  GetTestResults;
  
  PROCEDURE GetMeasure(pc_MeasureCursor out retCursor,
                         pc_MeasureDetailCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_MeasureId  MeasureHdr.measureid%type;
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
      ls_MeasureExists varchar2(1);
    begin
    
          if pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
            --Return Measure records
                  Open pc_MeasureCursor FOR 
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
                  FROM  Certificate ce 
                              inner join MeasureHdr m on
                                   ce.certificateid = m.certificateid and
                                   ce.certificationtypeid = m.certificationtypeid
                  WHERE m.certificationtypeid = pi_CertificationTypeID AND
                        lower(ce.certificatenumber)   = lower(ps_CertificateNumber);    
                  
                  
                   --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                  li_MeasureId :=  TESTRESULTS_CRUD.GETMEASUREID(
                                        PS_CERTIFICATENUMBER => ps_certificatenumber, 
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID                               
                                      );
                                
                      --Return the Measure Detail records
                      Open pc_measuredetailcursor FOR
                      SELECT SECTIONWIDTH, OVERALLWIDTH, MEASUREID as Mea_ID,Iteration
                      FROM  MeasureDtl MD
                      WHERE MD.MEASUREID = li_MeasureId;

    EXCEPTION   
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- GetMeasure. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetMeasure',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetMeasure.  There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetMeasure',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20006,ls_ErrorMsg);           
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- GetMeasure.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetMeasure',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);
    end GetMeasure;
    
  Procedure GetPlunger(  pc_PlungerHdrCursor out retCursor,
                         pc_PlungerDtlCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_PlungerId  plungerhdr.plungerid%type;
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
      ls_MeasureExists varchar2(1);
    begin
    
          if pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
            --Return Measure records
                  Open pc_PlungerHdrCursor FOR 
                  SELECT PLUNGERID as PLG_ID,
                         PROJECTNUMBER as ProjectNum,
                         TIRENUMBER as TireNum,
                         TESTSPEC as TestSpec,
                         COMPLETIONDATE,
                         DOTSERIALNUMBER,
                         AVGBREAKINGENERGY,
                         PASSYN,                    
                         p.CERTIFICATIONTYPEID,
                         CERTIFICATENUMBER,                         
                         SERIALDATE,
                         MINPLUNGER
                  FROM  Certificate ce 
                              inner join PLUNGERHDR p on
                                   ce.certificateid = p.certificateid and
                                   ce.certificationtypeid = p.certificationtypeid
                  WHERE 
                        p.certificationtypeid         = pi_CertificationTypeID AND
                        lower(ce.certificatenumber)   = lower(ps_certificatenumber);    
                        
                   --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                  li_PlungerId :=  TESTRESULTS_CRUD.GETPlungerID(
                                        PS_CERTIFICATENUMBER => ps_certificatenumber, 
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID                                        
                                      );
                                
                      --Return the Measure Detail records
                      Open pc_PlungerDtlCursor FOR
                      SELECT PLUNGERID as PLG_ID,BREAKINGENERGY ,ITERATION
                      FROM  PLUNGERDTL p 
                      WHERE p.PLUNGERID = li_PlungerId;

    EXCEPTION   
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  sqlerrm || '- GetPlunger. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetPlunger',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetPlunger.  There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetPlunger',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
          raise_application_error (-20006,ls_ErrorMsg);            
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- GetPlunger. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetPlunger',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);
    end GetPlunger;
    
  Procedure GetTreadWear(  pc_TreadWearHdrCursor out retCursor,
                             pc_TreadWearDtlCursor out retCursor,                         
                             pi_SKUId in number,
                             ps_CertificateNumber in varchar2,
                             pi_CertificationTypeID in Number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_PlungerId  plungerhdr.plungerid%type;
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
      ls_MeasureExists varchar2(1);
    begin
    
          if pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
            --Return Measure records
                  Open pc_TreadWearHdrCursor FOR 
                  SELECT  TREADWEARID as TW_ID,
                          PROJECTNUMBER as ProjectNum,
                          TIRENUMBER as TireNum,
                          TESTSPEC as TestSpec,
                          COMPLETIONDATE,
                          DOTSERIALNUMBER,
                          LOWESTWEARBAR,
                          PASSYN,                          
                          t.CERTIFICATIONTYPEID,
                          CERTIFICATENUMBER,                         
                          SERIALDATE,
                          INDICATORSREQUIREMENT
                  FROM  Certificate ce 
                              inner join  TREADWEARHDR t on
                                   ce.certificateid = t.certificateid and
                                   ce.certificationtypeid = t.certificationtypeid
                  WHERE 
                        t.certificationtypeid = pi_CertificationTypeID AND
                        lower(ce.certificatenumber)   = lower(ps_certificatenumber);    
                        
                   --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                  li_PlungerId := TESTRESULTS_CRUD.GetTreadWearID(
                                        PS_CERTIFICATENUMBER => ps_certificatenumber, 
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                      );
                                
                      --Return the Measure Detail records
                      Open pc_TreadWearDtlCursor FOR
                      SELECT  TREADWEARID as TW_ID, WEARBARHEIGHT ,ITERATION
                      FROM  TREADWEARDTL td
                      WHERE td.treadwearid = li_PlungerId;

    EXCEPTION   
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  sqlerrm || '-GetTreadWear. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetTreadWear',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '-GetTreadWear.  There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetTreadWear',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
          raise_application_error (-20006,ls_ErrorMsg);       
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-GetTreadWear.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetTreadWear',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                      
                raise_application_error (-20007,ls_ErrorMsg);
    end GetTreadWear;
  
  
  Procedure GetBeadUnseat(pc_BeadUnseatHdrCursor out retCursor,
                          pc_BeadUnseatDtlCursor out retCursor,                         
                          pi_SKUId in number,
                          ps_CertificateNumber in varchar2,
                          pi_CertificationTypeID in Number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_BeadUnseatId  beadunseathdr.beadunseatid%type;
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
      ls_MeasureExists varchar2(1);
    begin
    
          if pi_SKUId is null or pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_SKUId <=0 or pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
            --Return Measure records
                  Open pc_BeadUnseatHdrCursor FOR 
                  SELECT BEADUNSEATID as BU_ID,
                        PROJECTNUMBER as ProjectNum,
                        TIRENUMBER as TireNum,
                        TESTSPEC as TestSpec,
                        COMPLETIONDATE,
                        DOTSERIALNUMBER,
                        LOWESTUNSEATVALUE,
                        PASSYN,
                        bs.CERTIFICATIONTYPEID,
                        CERTIFICATENUMBER,                       
                        SERIALDATE
                 FROM  Certificate ce 
                              inner join BEADUNSEATHDR bs on 
                                   ce.certificateid = bs.certificateid and
                                   ce.certificationtypeid = bs.certificationtypeid
                  WHERE 
                        bs.certificationtypeid = pi_CertificationTypeID AND
                        lower(ce.certificatenumber)   = lower(ps_certificatenumber);    
                        
                   --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                  li_BeadUnseatId :=  TESTRESULTS_CRUD.GetBeadUnseatID(
                                        PS_CERTIFICATENUMBER => ps_certificatenumber, 
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                      );
                                
                      --Return the Measure Detail records
                      Open pc_BeadUnseatDtlCursor FOR
                      SELECT BEADUNSEATID as BU_ID, UNSEATFORCE,ITERATION 
                      FROM BEADUNSEATDTL bs 
                      WHERE bs.BEADUNSEATID = li_BeadUnseatId;

    EXCEPTION   
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- GetBeadUnseat. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetBeadUnseat',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetBeadUnseat. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetBeadUnseat',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
          raise_application_error (-20006,ls_ErrorMsg);          
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- GetBeadUnseat. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetBeadUnseat',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);
  end GetBeadUnseat ;
  
  
    
  procedure GetENDURANCE(pc_EnduranceHdrCursor out retCursor,
                         pc_EnduranceDtlCursor out retCursor,                        
                         pi_SKUID in Number,
                         ps_CertificateNumber in varchar,
                         pi_CertificationTypeID in Number) as  
  --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParameterIsInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParameterIsInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      li_certificationTypeId  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%type;
      li_EnduranceId  EnduranceHdr.ENDURANCEID%type;
       ls_EnduranceExists varchar2(1);
  begin
        if pi_SKUID is null OR ps_CertificateNumber IS NULL OR  pi_CertificationTypeID IS NULL then
              raise li_ParametersAreNull;
        end if;
        if pi_SKUID <= 0 OR ps_CertificateNumber = '' OR pi_CertificationTypeID <= 0 then
              raise li_ParameterIsInvalid;
        end if;     
     
            Open pc_EnduranceHdrCursor FOR
            SELECT    ENDURANCEID  AS END_ID,
                      PROJECTNUMBER as ProjectNum,
                      TIRENUMBER as TireNum,
                      TESTSPEC as TestSpec,
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
                      WHEELNUMBER as WheelNum,
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
            FROM  Certificate ce 
                              inner join ENDURANCEHdr  e on 
                                ce.certificateid = e.certificateid and
                                ce.certificationtypeid = e.certificationtypeid
            WHERE lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber) And
                  e.certificationtypeid = pi_CertificationTypeID ;
                 
            
            li_EnduranceId:=  testresults_crud.GetEnduranceID(ps_CertificateNumber=>ps_CertificateNumber,
                                                                 pi_CertificationTypeId=>pi_CertificationTypeID);  
     
                  Open pc_EnduranceDtlCursor FOR
                  SELECT
                        ed.TESTSTEP,
                        ed.TIMEINMIN,
                        ed.SPEED,
                        ed.TOTMILES,
                        ed.LOAD,
                        ed.LOADPERCENT,
                        ed.SETINFLATION,
                        ed.AMBTEMP,
                        ed.INFPRESSURE,
                        ed.STEPCOMPLETIONDATE,
                        ed.ENDURANCEID AS END_ID
                  FROM  EnduranceDtl ed
                  WHERE ed.enduranceid = li_EnduranceId;        
        
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || '- GetENDURANCE. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetENDURANCE',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);            
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParameterIsInvalid then           
            ls_ErrorMsg:= sqlerrm || '- GetENDURANCE. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetENDURANCE',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:= '- GetENDURANCE. An error have ocurred.(when others)' || sqlerrm ;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetENDURANCE',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);                      
            raise_application_error (-20007,ls_ErrorMsg);
            
 end GetENDURANCE;
 
 
  PROCEDURE GetHighSpeed(pc_HighSpeedCursor out retCursor,
                         pc_HighSpeedDetailCursor out retCursor, 
                         pc_HSSpeedTestDetail out retCursor,
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_HighSpeedID  highspeedhdr.highspeedid %type;
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
      ls_MeasureExists varchar2(1);
    begin
    
          if pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
            --Return Measure records
                  Open pc_HighSpeedCursor FOR 
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
                        m.CERTIFICATIONTYPEID,
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
                  FROM  Certificate ce 
                              inner join HIGHSPEEDHDR  m on
                                   ce.certificateid = m.certificateid and
                                   ce.certificationtypeid = m.certificationtypeid
                  WHERE 
                        m.certificationtypeid = pi_CertificationTypeID AND
                        lower(ce.certificatenumber)   = lower(ps_certificatenumber);    
                        
                   --Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber                
                  li_HighSpeedID := TESTRESULTS_CRUD.GetHighSpeedId(
                                        PS_CERTIFICATENUMBER => ps_certificatenumber, 
                                        PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                      );
                                
                      --Return the Measure Detail records
                      Open pc_HighSpeedDetailCursor FOR
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
                      WHERE h.HIGHSPEEDID = li_HighSpeedID;
                      
                     Open pc_HSSpeedTestDetail FOR
                     SELECT ITERATION,
                            TIME,
                            SPEED,
                            HIGHSPEEDID as HS_ID
                    FROM SPEEDTESTDETAIL s
                    WHERE s.HIGHSPEEDID = li_HighSpeedID;

    EXCEPTION   
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- GetHighSpeed . There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetHighSpeed',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetHighSpeed . There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetHighSpeed',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
          raise_application_error (-20006,ls_ErrorMsg);           
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetHighSpeed',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
              raise_application_error (-20007,ls_ErrorMsg);
    end GetHighSpeed;
 
  PROCEDURE GetSound(pc_SoundHDRCursor out retCursor,
                         pc_SoundDetailCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number) as
                         
   --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_SoundID  soundhdr.soundid%type;
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
                              
  begin
         if pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
        
          Open pc_SoundHDRCursor for
          SELECT SOUNDID,
                  PROJECTNUMBER,
                  TIRENUMBER,
                  TESTSPEC,
                  TESTREPORTNUMBER,
                  MANUFACTUREANDBRAND,
                  TIRECLASS,
                  CATEGORYOFUSE,
                  DATEOFTEST,
                  TESTVEHICULE,
                  TESTVEHICULEWHEELBASE,
                  LOCATIONOFTESTTRACK,
                  DATETRACKCERTIFTOISO,
                  TIRESIZEDESIGNATION,
                  TIRESERVICEDESCRIPTION,
                  TESTMASS_FRONTL,
                  TESTMASS_FRONTR,
                  TESTMASS_REARL,
                  TESTMASS_REARR,
                  TIRELOADINDEX_FRONTL,
                  TIRELOADINDEX_FRONTR,
                  TIRELOADINDEX_REARL,
                  TIRELOADINDEX_REARR,
                  INFLATIONPRESSURECO_FRONTL,
                  INFLATIONPRESSURECO_FRONTR,
                  INFLATIONPRESSURECO_REARL,
                  INFLATIONPRESSURECO_REARR,
                  TESTRIMWIDTHCODE,
                  TEMPMEASURESENSORTYPE,
                  s.CERTIFICATIONTYPEID,
                  CERTIFICATENUMBER
                 
             FROM  Certificate ce 
                              inner join SOUNDHDR s on
                                   ce.certificateid = s.certificateid and
                                   ce.certificationtypeid = s.certificationtypeid
            WHERE 
                  S.certificationtypeid = pi_CertificationTypeID AND
                  lower(ce.certificatenumber)   = lower(ps_certificatenumber); 
                 
               li_SoundID:=   TESTRESULTS_CRUD.GETSoundHDRID(
                                        PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                        PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                      );

              Open pc_SoundDetailCursor for
              SELECT ITERATION,
                TESTSPEED,
                DIRECTIONOFRUN,
                SOUNDLEVELLEFT,
                SOUNDLEVELRIGHT,
                AIRTEMP,
                TRACKTEMP,
                SOUNDLEVELLEFT_TEMPCORRECTED,
                SOUNDLEVELRIGHT_TEMPCORRECTED,
                SOUNDID
              FROM SOUNDDETAIL 
              WHERE SoundID = li_SoundID;
        
  
        EXCEPTION   
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- GetSound.  There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetSound',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetSound. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetSound',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);           
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- GetSound. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetSound',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);
  end GetSound;
  
   PROCEDURE GetWetGrip(pc_WetGripHDRCursor out retCursor,
                         pc_WetGripDetailCursor out retCursor,                         
                         pi_SKUId in number,
                         ps_CertificateNumber in varchar2,
                         pi_CertificationTypeID in Number) as
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);     
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_WetGripID  WetGripHDR.WetGRIPID%TYPE;
      
      li_CertificationTypeId  certificationtype.certificationtypeid%TYPE;
                              
  begin
         if pi_CertificationTypeID is null or ps_CertificateNumber is null  then
                raise li_ParametersAreNull;
          end if;
          if  pi_CertificationTypeID <=0  or ps_CertificateNumber  ='' then
                raise li_ParametersAreInvalid;
          end if;  
          
          Open PC_WETGRIPHDRCURSOR For
          SELECT WETGRIPID ,
                  PROJECTNUMBER,
                  TIRENUMBER,
                  TESTSPEC,
                  DATEOFTEST,
                  TESTVEHICLE,
                  LOCATIONOFTESTTRACK,
                  TESTTRACKCHARACTERISTICS,
                  ISSUEBY,
                  METHODOFCERTIFICATION,
                  TESTTIREDETAILS,
                  TIRESIZEANDSERVICEDESC,
                  TIREBRANDANDTRADEDESC,
                  REFERENCEINFLATIONPRESSURE,
                  TESTRIMWITHCODE,
                  TEMPMEASURESENSORTYPE,
                  IDENTIFICATIONSRTT,
                  TESTTIRELOAD_SRTT,
                  TESTTIRELOAD_CANDIDATE,
                  TESTTIRELOAD_CONTROL,
                  WATERDEPTH_SRTT,
                  WATERDEPTH_CANDIDATE,
                  WATERDEPTH_CONTROL,
                  WETTEDTRACKTEMPAVG,
                  w.CERTIFICATIONTYPEID,
                  CERTIFICATENUMBER
                 
             FROM  Certificate ce 
                              inner join WETGRIPHDR  w on
                                   ce.certificateid = w.certificateid and
                                   ce.certificationtypeid = w.certificationtypeid
           WHERE 
                 w.certificationtypeid = pi_CertificationTypeID AND
                 lower(ce.certificatenumber)   = lower(ps_certificatenumber); 
                 
               li_WetGripID:=   TESTRESULTS_CRUD.GETWETGRIPHDRID(
                                        PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
                                        PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID,
                                        PI_SKUID => PI_SKUID
                                      );
            
            oPEN pc_WetGripDetailCursor FOR
            SELECT ITERATION,
              TESTSPEED,
              DIRECTIONOFRUN,
              SRTT,
              CANDIDATETIRE,
              PEAKBREAKFORCECOEFICIENT,
              MEANFULLYDEVELOPEDDECELERATION,
              WETGRIPINDEX,
              COMMENTS,
              WETGRIPID
            FROM WETGRIPDETAIL 
            WHERE WETGRIPID = li_WetGripID;
                 
                 
                 
        EXCEPTION   
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '- GetWetGrip. GetWetGrip.There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetWetGrip',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '- GetWetGrip. GetWetGrip.There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.GetWetGrip',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20006,ls_ErrorMsg);          
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- GetWetGrip. GetWetGrip.An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.GetWetGrip',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);

  end GetWetGrip;             
 
 procedure Product_Save(  pi_SKUID in   NUMBER,
                          ps_SKU in   VARCHAR2,
                          ps_BRANDCODE in   VARCHAR2,
                          pi_TIRETYPEID in   NUMBER,
                          pi_NPRID in   NUMBER,
                          ps_SIZESTAMP in   VARCHAR2,
                          pd_DISCONTINUEDDATE in   TIMESTAMP,
                          ps_SPECNUMBER in   VARCHAR2,
                          ps_SPEEDRATING in   VARCHAR2,
                          ps_SINGLOADINDEX in   VARCHAR2,
                          ps_DUALLOADINDEX in   VARCHAR2,
                          ps_BELTEDRADIALYN in   VARCHAR2,
                          ps_TUBELESSYN in   VARCHAR2,
                          ps_REINFORCEDYN in   VARCHAR2,
                          ps_EXTRALOADYN in   VARCHAR2,                         
                          ps_UTQGTREADWEAR in   VARCHAR2,
                          ps_UTQGTRACTION in   VARCHAR2,
                          ps_UTQGTEMP in   VARCHAR2,
                          ps_MUDSNOWYN in   VARCHAR2,
                          pi_RIMDIAMETER in   NUMBER,
                          pd_SERIALDATE in   TIMESTAMP,
                          ps_BRANDDESC in   VARCHAR2,
                          ps_LOADRANGE in   VARCHAR2,
                          pi_MEARIMWIDTH in   NUMBER,
                          ps_REGROOVABLEIND in   VARCHAR2,
                          ps_PLANTPRODUCED in   VARCHAR2,
                          pd_MOSTRECENTTESTDATE in   TIMESTAMP,
                          ps_IMARK in   VARCHAR2,                         
                          ps_INFORMENUMBER in   VARCHAR2,
                          pd_FECHADATE in   TIMESTAMP,
                          ps_TREADPATTERN in   VARCHAR2,
                          ps_SPECIALPROTECTIVEBAND in   VARCHAR2,
                          ps_NOMINALTIREWIDTH in   VARCHAR2,
                          ps_ASPECTRADIO in   VARCHAR2,
                          ps_TREADWEARINDICATORS in   VARCHAR2,
                          ps_NAMEOFMANUFACTURER in   VARCHAR2,
                          ps_FAMILY in   VARCHAR2,
                          ps_DOTSERIALNUMBER in   VARCHAR2,
                          ps_OperatorName   in Varchar2) as
  --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006); 
      
      ls_skuExists varchar2(1);
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
  begin
       if ps_sku is null then
          raise li_ParametersAreNull;
       end if;
      
      if ps_sku = '' then
              raise li_ParametersAreInvalid;
        end if;
        
        if  ps_OperatorName   is not null or ps_OperatorName   <> '' then
            ls_OperatorId:=ps_OperatorName;
        end if;
     
     ls_skuExists:=  TESTRESULTS_CRUD.CHECKIFPRODUCTEXISTS(ps_SKU => ps_SKU,pi_SKUID => pi_SKUID);
     
     if ls_skuExists = 'y' then
           UPDATE  PRODUCT SET 
                      SIZESTAMP      = ps_sizestamp,
                      DISCONTINUEDDATE = pd_discontinueddate,
                      SPECNUMBER     = ps_specnumber,
                      SPEEDRATING    = ps_speedrating,
                      SINGLOADINDEX  = ps_SINGLOADINDEX,
                      DUALLOADINDEX  = ps_dualloadindex,
                      BELTEDRADIALYN = ps_BELTEDRADIALYN,
                      TUBELESSYN     = ps_tubelessyn,
                      REINFORCEDYN   = ps_reinforcedyn,
                      EXTRALOADYN    = ps_extraloadyn,                     
                      UTQGTREADWEAR  = ps_utqgtreadwear,
                      UTQGTRACTION   = ps_utqgtraction,
                      UTQGTEMP       = ps_utqgtemp,
                      MUDSNOWYN      = ps_mudsnowyn,
                      RIMDIAMETER    = pi_rimdiameter,
                      SERIALDATE     = PD_SERIALDATE,
                      BRANDDESC      = PS_BRANDDESC,
                      LOADRANGE      = PS_LOADRANGE,
                      MEARIMWIDTH    = pi_MEARIMWIDTH,
                      REGROOVABLEIND = PS_REGROOVABLEIND,
                      PLANTPRODUCED  = PS_PLANTPRODUCED,
                      MOSTRECENTTESTDATE=PD_MOSTRECENTTESTDATE,
                      IMARK          = PS_IMARK   ,                           
                      ModifiedOn     = Sysdate,
                      ModifiedBy     = ls_OperatorId,
                      INFORMENUMBER  = ps_INFORMENUMBER,
                      FECHADATE      = pd_FECHADATE,
                      TREADPATTERN   = ps_TREADPATTERN,
                      SPECIALPROTECTIVEBAND = ps_SPECIALPROTECTIVEBAND,
                      NOMINALTIREWIDTH      = ps_NOMINALTIREWIDTH,
                      ASPECTRADIO           = ps_ASPECTRADIO,
                      TREADWEARINDICATORS   = ps_TREADWEARINDICATORS,
                      NAMEOFMANUFACTURER    = ps_NAMEOFMANUFACTURER,
                      FAMILY                = ps_FAMILY,
                      DOTSERIALNUMBER       = ps_DOTSERIALNUMBER
           WHERE SKU   =  ps_SKU And
                 SKUID = pi_SKUID;
     else
          INSERT INTO  PRODUCT
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
                      CreatedBy
                    )
                    VALUES
                    (
                      pi_SKUID,
                      ps_SKU,
                      ps_BRANDCODE,
                      pi_TIRETYPEID	,
                      pi_NPRID ,
                      ps_SIZESTAMP,
                      pd_DISCONTINUEDDATE,
                      ps_SPECNUMBER,
                      ps_SPEEDRATING	,
                      ps_SINGLOADINDEX,
                      ps_DUALLOADINDEX	,
                      ps_BELTEDRADIALYN	,
                      ps_TUBELESSYN	    ,
                      ps_REINFORCEDYN	  ,
                      ps_EXTRALOADYN	,                      
                      ps_UTQGTREADWEAR,
                      ps_UTQGTRACTION	,
                      ps_UTQGTEMP	,
                      ps_MUDSNOWYN,
                      pi_RIMDIAMETER,
                      PD_SERIALDATE,
                      PS_BRANDDESC,
                      PS_LOADRANGE,
                      pi_MEARIMWIDTH,
                      PS_REGROOVABLEIND,
                      PS_PLANTPRODUCED,
                      PD_MOSTRECENTTESTDATE,
                      PS_IMARK,
                      ps_INFORMENUMBER,
                      pd_FECHADATE,
                      ps_TREADPATTERN,
                      ps_SPECIALPROTECTIVEBAND,
                      ps_NOMINALTIREWIDTH,
                      ps_ASPECTRADIO,
                      ps_TREADWEARINDICATORS,
                      ps_NAMEOFMANUFACTURER,
                      ps_FAMILY,
                      ps_DOTSERIALNUMBER,
                      ls_OperatorId
                     );
     
     end if;
  
   EXCEPTION
        when li_ParametersAreNull then
           
            ls_ErrorMsg:=  sqlerrm || '-Product_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.Product_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20005,sqlerrm);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '-Product_Save. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.Product_Save',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
            raise_application_error (-20006,sqlerrm);           
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-Product_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.Product_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
                 raise_application_error (-20007,ls_ErrorMsg);
  end Product_Save;
  
  Procedure Measure_Save( pi_MEASUREID out Number,
                          pi_CertificateID  in number,
                          ps_PROJECTNUMBER in Varchar2,
                          pi_TIRENUMBER in Number,
                          ps_TESTSPEC in Varchar2,
                          pd_COMPLETIONDATE in TimeStamp,
                          pi_INFLATIONPRESSURE in Number,
                          ps_MOLDDESIGN in Varchar2,
                          pi_RIMWIDTH in Number,
                          ps_DOTSERIALNUMBER in Varchar2,
                          pi_DIAMETER in Number,
                          pi_AVGSECTIONWIDTH in Number,
                          pi_AVGOVERALLWIDTH in Number,
                          pi_MAXOVERALLWIDTH in Number,
                          pi_SIZEFACTOR in Number,
                          pd_MOUNTTIME in TimeStamp,
                          pi_MOUNTTEMP in Number,
                          pd_SERIALDATE in TimeStamp,
                          pd_ENDTIME in TimeStamp,
                          pi_ACTSIZEFACTOR in Number,
                          pi_CERTIFICATIONTYPEID in Number,                          
                          pi_STARTINFLATIONPRESSURE in Number,
                          pi_ENDINFLATIONPRESSURE in Number,
                          ps_ADJUSTMENT in Varchar2,
                          pi_CIRCUNFERENCE in Number,
                          pi_NOMINALDIAMETER in Number,
                          pi_NOMINALWIDTH in Number,
                          ps_NOMINALWIDTHPASSFAIL in Varchar2,
                          pi_NOMINALWIDTHDIFERENCE in Number,
                          pi_NOMINALWIDTHTOLERANCE in Number,
                          pi_MAXOVERALLDIAMETER in Number,
                          pi_MINOVERALLDIAMETER in Number,
                          ps_OVERALLWIDTHPASSFAIL in Varchar2,
                          ps_OVERALLDIAMETERPASSFAIL in Varchar2,
                          pi_DIAMETERDIFERENCE in Number,
                          pi_DIAMETERTOLERANCE in Number,
                          pi_TEMPRESISTANCEGRADING in Number,
                          pi_TENSILESTRENGHT1 in Number,
                          pi_TENSILESTRENGHT2 in Number,
                          pi_ELONGATION1 in Number,
                          pi_ELONGATION2 in Number,
                          pi_TENSILESTRENGHTAFTERAGE1 in Number,
                          pi_TENSILESTRENGHTAFTERAGE2 in Number,
                          ps_OperatorName   in varchar2) as
   --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006); 
      
      
      ls_MeasureExists varchar2(1);
      li_certificationId integer;
      
      li_CurrentMeasureId number;
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
 begin
        if pi_CertificationTypeID is null  then
          raise li_ParametersAreNull;
        end if;
       if pi_CertificationTypeID <=0 then
          raise li_ParametersAreInvalid;
        end if;
       
       if ps_OperatorName   is not null or ps_OperatorName   <> '' then
       ls_OperatorId:=ps_OperatorName;
       end if;
      
      
       ls_MeasureExists:= TESTRESULTS_CRUD.CheckIfMeasureExists(pi_CertificateID => pi_CertificateID,                                                                      
                                                                PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                                               );
       if ls_MeasureExists='y' then    
            UPDATE  MeasureHdr M SET
                PROJECTNUMBER            = ps_PROJECTNUMBER,
                TIRENUMBER               = pi_TIRENUMBER,
                TESTSPEC                 = ps_TESTSPEC,
                COMPLETIONDATE           = pd_COMPLETIONDATE,
                INFLATIONPRESSURE        = pi_INFLATIONPRESSURE,
                MOLDDESIGN               = ps_MOLDDESIGN,
                RIMWIDTH                 = pi_RIMWIDTH,
                DOTSERIALNUMBER          = ps_DOTSERIALNUMBER,
                DIAMETER                 = pi_DIAMETER,
                AVGSECTIONWIDTH          = pi_AVGSECTIONWIDTH,
                AVGOVERALLWIDTH          = pi_AVGOVERALLWIDTH,
                MAXOVERALLWIDTH          = pi_MAXOVERALLWIDTH,
                SIZEFACTOR               = pi_SIZEFACTOR,
                MOUNTTIME                = pd_MOUNTTIME,
                MOUNTTEMP                = pi_MOUNTTEMP,
                SERIALDATE               = pd_SERIALDATE,
                ENDTIME                  = pd_ENDTIME,
                ACTSIZEFACTOR            = pi_ACTSIZEFACTOR,
                STARTINFLATIONPRESSURE   = pi_STARTINFLATIONPRESSURE,
                ENDINFLATIONPRESSURE     = pi_ENDINFLATIONPRESSURE,
                ADJUSTMENT               = ps_ADJUSTMENT,
                CIRCUNFERENCE            = pi_CIRCUNFERENCE,
                NOMINALDIAMETER          = pi_NOMINALDIAMETER,
                NOMINALWIDTH             = pi_NOMINALWIDTH,
                NOMINALWIDTHPASSFAIL     = ps_NOMINALWIDTHPASSFAIL,
                NOMINALWIDTHDIFERENCE    = pi_NOMINALWIDTHDIFERENCE,
                NOMINALWIDTHTOLERANCE    = pi_NOMINALWIDTHTOLERANCE,
                MAXOVERALLDIAMETER       = pi_MAXOVERALLDIAMETER,
                MINOVERALLDIAMETER       = pi_MINOVERALLDIAMETER,
                OVERALLWIDTHPASSFAIL     = ps_OVERALLWIDTHPASSFAIL,
                OVERALLDIAMETERPASSFAIL  = ps_OVERALLDIAMETERPASSFAIL,
                DIAMETERDIFERENCE        = pi_DIAMETERDIFERENCE,
                DIAMETERTOLERANCE        = pi_DIAMETERTOLERANCE,
                TEMPRESISTANCEGRADING    = pi_TEMPRESISTANCEGRADING,
                TENSILESTRENGHT1         = pi_TENSILESTRENGHT1,
                TENSILESTRENGHT2         = pi_TENSILESTRENGHT2,
                ELONGATION1              = pi_ELONGATION1,
                ELONGATION2              = pi_ELONGATION2,
                TENSILESTRENGHTAFTERAGE1 = pi_TENSILESTRENGHTAFTERAGE1,
                TENSILESTRENGHTAFTERAGE2 = pi_TENSILESTRENGHTAFTERAGE2,
                MODIFIEDBY               = ls_OperatorId,
                ModifiedOn               = Sysdate               
            WHERE M.CERTIFICATIONTYPEID = pi_CertificationTypeID And
                  M.CERTIFICATEID       = pi_CertificateID;
           
           SELECT Max(measureid) into li_CurrentMeasureId
            FROM  MeasureHdr m 
            WHERE m.certificationtypeid = pi_CertificationTypeID AND
                  M.CERTIFICATEID       = pi_CertificateID;   		   
                  
            Delete from  measuredtl where MEASUREID=li_currentmeasureid;   
            
             pi_MeasureId:=  li_CurrentMeasureId;  
                  
       else              
            
            INSERT  INTO  MeasureHdr
                  (
                    MEASUREID,
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
                    CERTIFICATIONTYPEID,                                   
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
                    TENSILESTRENGHTAFTERAGE2,
                    CERTIFICATEID
                  )
            VALUES
                  (
                    MeasureId_SEQ.NextVal,                    
                    ps_PROJECTNUMBER,
                    pi_TIRENUMBER,
                    ps_TESTSPEC,
                    pd_COMPLETIONDATE,
                    pi_INFLATIONPRESSURE,
                    ps_MOLDDESIGN,
                    pi_RIMWIDTH,
                    ps_DOTSERIALNUMBER,
                    pi_DIAMETER,
                    pi_AVGSECTIONWIDTH,
                    pi_AVGOVERALLWIDTH,
                    pi_MAXOVERALLWIDTH,
                    pi_SIZEFACTOR,
                    pd_MOUNTTIME,
                    pi_MOUNTTEMP,
                    pd_SERIALDATE,
                    pd_ENDTIME,
                    pi_ACTSIZEFACTOR,
                    pi_CERTIFICATIONTYPEID,                                       
                    pi_STARTINFLATIONPRESSURE,
                    pi_ENDINFLATIONPRESSURE,
                    ps_ADJUSTMENT,
                    pi_CIRCUNFERENCE,
                    pi_NOMINALDIAMETER,
                    pi_NOMINALWIDTH,
                    ps_NOMINALWIDTHPASSFAIL,
                    pi_NOMINALWIDTHDIFERENCE,
                    pi_NOMINALWIDTHTOLERANCE,
                    pi_MAXOVERALLDIAMETER,
                    pi_MINOVERALLDIAMETER,
                    ps_OVERALLWIDTHPASSFAIL,
                    ps_OVERALLDIAMETERPASSFAIL,
                    pi_DIAMETERDIFERENCE,
                    pi_DIAMETERTOLERANCE,
                    pi_TEMPRESISTANCEGRADING,
                    pi_TENSILESTRENGHT1,
                    pi_TENSILESTRENGHT2,
                    pi_ELONGATION1,
                    pi_ELONGATION2,
                    pi_TENSILESTRENGHTAFTERAGE1,
                    pi_TENSILESTRENGHTAFTERAGE2,
                    pi_certificateid
                  );
            --Gets the Id that just was inserted on the table to be returned
            SELECT Max(measureid) into li_CurrentMeasureId
            FROM  MeasureHdr m 
            WHERE m.certificationtypeid = pi_CertificationTypeID AND
                  M.CERTIFICATEID       = pi_CertificateID;    		   
          
                  
             pi_MeasureId:=  li_CurrentMeasureId;   
                  
       end if;
       
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:=  'Measure_Save. There is at least one parameters null.' || sqlerrm ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.Measure_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || 'Measure_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.Measure_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'Measure_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.Measure_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
              raise_application_error (-20007,ls_ErrorMsg);
              
 end Measure_Save;
 
Procedure MeasureDetail_Save(pi_SECTIONWIDTH in  MEASUREDTL.SECTIONWIDTH%Type,
                               pi_OVERALLWIDTH in  MEASUREDTL.OVERALLWIDTH%Type,
                               pi_MEASUREID in number,
                               PI_ITERATION IN NUMBER,
                               ps_OperatorName   in varchar2 )as 
       --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006); 
      --varible
      ls_MeasureDetailExists varchar2(1);
      li_certificationId integer;
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                        
  begin
      if pi_MEASUREID is null then
          raise li_ParametersAreNull;
      end if;
      if pi_MEASUREID <= 0 then
          raise li_ParametersAreInvalid;
      end if;
      
      if ps_OperatorName   is not null or ps_OperatorName   <> '' then
            ls_OperatorId:=ps_OperatorName ;
      end if;
     
       INSERT INTO  MeasureDtl
            (
              SECTIONWIDTH,
              OVERALLWIDTH,
              MEASUREID,
              ITERATION,
              CreatedBy
            )
            VALUES
            (
               pi_SECTIONWIDTH,
               pi_OVERALLWIDTH,
               pi_MEASUREID,
               PI_ITERATION,
               ls_OperatorId
            );
      
      
  
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || 'MeasureDetail_Save.There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.MeasureDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || 'MeasureDetail_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.MeasureDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'MeasureDetail_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.MeasureDetail_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
  end MeasureDetail_Save;
  
  procedure Endurance_Save( pi_ENDURANCEID out number,
                            ps_PROJECTNUMBER in varchar2,
                            pi_TIRENUMBER in number,
                            ps_TESTSPEC in varchar2,
                            pd_COMPLETIONDATE in date,
                            ps_DOTSERIALNUMBER in varchar2,
                            ps_MFGWWYY in varchar2,
                            pd_PRECONDSTARTDATE in date,
                            pi_PRECONDSTARTTEMP in number,
                            pi_RIMDIAMETER in number,
                            pi_RIMWIDTH in number,
                            pd_PRECONDENDDATE in date,
                            pi_PRECONDENDTEMP in number,
                            pi_INFLATIONPRESSURE in number,
                            pi_BEFOREDIAMETER in number,
                            pi_AFTERDIAMETER in number,
                            pi_BEFOREINFLATION in number,
                            pi_AFTERINFLATION in number,
                            pi_WHEELPOSITION in number,
                            pi_WHEELNUMBER in number,
                            pi_FINALTEMP in number,
                            pi_FINALDISTANCE in number,
                            pi_FINALINFLATION in number,
                            pd_POSTCONDSTARTDATE in date,
                            pd_POSTCONDENDDATE in date,
                            pi_POSTCONDENDTEMP in number,
                            ps_PASSYN in varchar2,                                                  
                            pi_CertificationTypeID in number,                           
                            pd_SerialDate in date,
                            pi_PreCondTime in number,
                            pi_PostCondTime  in number,
                            pi_DIAMETERTESTDRUM in Number,
                            pi_PRECONDTEMP in Number,
                            pi_INFLATIONPRESSUREREADJUSTED in Number,
                            pi_CIRCUNFERENCEBEFORETEST in Number,
                            ps_RESULTPASSFAIL in Varchar2,
                            pi_ENDURANCEHOURS in Number,
                            ps_POSSIBLEFAILURESFOUND in Varchar2,
                            pi_CIRCUNFERENCEAFTERTEST in Number,
                            pi_OUTERDIAMETERDIFERENCE in Number,
                            pi_ODDIFERENCETOLERANCE in Number,
                            ps_SERIENOM in Varchar2,
                            ps_FINALJUDGEMENT in Varchar2,
                            ps_APPROVER in Varchar2,
                            ps_OperatorName in  Varchar2,
                            pi_certificateid in number) as
       --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
      li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006); 
      
      ls_EnduranceExists varchar2(1);
      li_certificationTypeId  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%type;
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);
      
      li_CurrentEnduranceID  endurancehdr.enduranceid%type;
  
  begin
         if pi_CertificationTypeID is null then
            raise li_ParametersAreNull;
         end if;
         if pi_CertificationTypeID <= 0  then
            raise li_ParametersAreInvalid;
         end if;
         if ps_OperatorName is not null or ps_OperatorName <> '' then
            ls_operatorid:=ps_OperatorName;
         end if ;
         
        
        ls_EnduranceExists:= TESTRESULTS_CRUD.CHECKIFENDURANCEEXISTS(
                                                  PI_certificateid   => pi_certificateid ,
                                                  PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                                );
        
        if  ls_EnduranceExists = 'y' then
            UPDATE  EnduranceHdr SET                    
                    PROJECTNUMBER    = ps_PROJECTNUMBER,
                    TIRENUMBER       = pi_TIRENUMBER,
                    TESTSPEC         = ps_TESTSPEC,
                    COMPLETIONDATE   = pd_COMPLETIONDATE,
                    DOTSERIALNUMBER  = ps_DOTSERIALNUMBER ,
                    MFGWWYY          = ps_MFGWWYY,
                    PRECONDSTARTDATE = pd_PRECONDSTARTDATE,
                    PRECONDSTARTTEMP  = pi_PRECONDSTARTTEMP,
                    RIMDIAMETER      = pi_RIMDIAMETER,
                    RIMWIDTH         = pi_RIMWIDTH,
                    PRECONDENDDATE   = pd_PRECONDENDDATE,
                    PRECONDENDTEMP   = pi_PRECONDENDTEMP,
                    INFLATIONPRESSURE = pi_INFLATIONPRESSURE ,
                    BEFOREDIAMETER    = pi_beforediameter,
                    AFTERDIAMETER     = pi_afterdiameter,
                    BEFOREINFLATION   = pi_beforeinflation,
                    AFTERINFLATION    = pi_afterinflation,
                    WHEELPOSITION     = pi_wheelposition,
                    WHEELNUMBER       = pi_wheelnumber,
                    FINALTEMP         = pi_FINALTEMP ,
                    FINALDISTANCE     = pi_finaldistance ,
                    FINALINFLATION    = pi_finalinflation ,
                    POSTCONDSTARTDATE = pd_postcondstartdate,
                    POSTCONDENDDATE   = pd_postcondenddate,
                    POSTCONDENDTEMP   = pi_postcondendtemp,
                    PASSYN            = ps_passyn,
                    SERIALDATE        = PD_SERIALDATE ,
                    PreCondTime       = PI_PRECONDTIME  ,
                    PostCondTime      = PI_POSTCONDTIME,
                    DIAMETERTESTDRUM  = pi_DIAMETERTESTDRUM,
                    PRECONDTEMP       = pi_PRECONDTEMP,
                    INFLATIONPRESSUREREADJUSTED = pi_INFLATIONPRESSUREREADJUSTED,
                    CIRCUNFERENCEBEFORETEST     = pi_CIRCUNFERENCEBEFORETEST,
                    RESULTPASSFAIL              = ps_RESULTPASSFAIL,
                    ENDURANCEHOURS              = pi_ENDURANCEHOURS,
                    POSSIBLEFAILURESFOUND       = ps_POSSIBLEFAILURESFOUND,
                    CIRCUNFERENCEAFTERTEST      = pi_CIRCUNFERENCEAFTERTEST,
                    OUTERDIAMETERDIFERENCE      = pi_OUTERDIAMETERDIFERENCE,
                    ODDIFERENCETOLERANCE        = pi_ODDIFERENCETOLERANCE,
                    SERIENOM                    = ps_SERIENOM,
                    FINALJUDGEMENT              = ps_FINALJUDGEMENT,
                    APPROVER                    = ps_APPROVER,
                    MODIFIEDON        = SYSTIMESTAMP,
                    ModifiedBy        = ls_operatorid
             WHERE CertificateID = pi_certificateid   And                
                  CERTIFICATIONTYPEID = pi_CertificationTypeID; 
                  
             SELECT Max(ENDURANCEID) into li_CurrentEnduranceID
              FROM EnduranceHdr e 
             WHERE  e.certificateid = pi_certificateid and                  
                   e.CERTIFICATIONTYPEID = pi_CertificationTypeID;
                    
              Delete from  Endurancedtl where ENDURANCEID=li_CurrentEnduranceID;   
              
               pi_ENDURANCEID:=  li_CurrentEnduranceID;  
               
        else
        
            INSERT INTO  EnduranceHdr   (
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
                    CERTIFICATIONTYPEID,                                   
                    SerialDate,
                    PreCondTime,
                    PostCondTime,
                    DIAMETERTESTDRUM,
                    PRECONDTEMP,
                    INFLATIONPRESSUREREADJUSTED,
                    CIRCUNFERENCEBEFORETEST,
                    RESULTPASSFAIL,
                    ENDURANCEHOURS,
                    POSSIBLEFAILURESFOUND,
                    CIRCUNFERENCEAFTERTEST,
                    OUTERDIAMETERDIFERENCE ,
                    ODDIFERENCETOLERANCE ,
                    SERIENOM ,
                    FINALJUDGEMENT ,
                    APPROVER,
                    CREATEDBY,
                    CERTIFICATEID
              )
              VALUES
              (
                ENDURANCEID_SEQ.NEXTVAL,
                ps_PROJECTNUMBER,
                pi_TIRENUMBER,
                ps_TESTSPEC,
                pd_COMPLETIONDATE,
                ps_DOTSERIALNUMBER,
                ps_MFGWWYY,
                pd_PRECONDSTARTDATE,
                pi_PRECONDSTARTTEMP,
                pi_RIMDIAMETER,
                pi_RIMWIDTH ,
                pd_PRECONDENDDATE,
                pi_PRECONDENDTEMP,
                pi_INFLATIONPRESSURE,
                pi_BEFOREDIAMETER,
                pi_AFTERDIAMETER,
                pi_BEFOREINFLATION,
                pi_AFTERINFLATION,
                pi_WHEELPOSITION,
                pi_WHEELNUMBER,
                pi_FINALTEMP,
                pi_FINALDISTANCE,
                pi_FINALINFLATION ,
                pd_POSTCONDSTARTDATE,
                pd_POSTCONDENDDATE,
                pi_POSTCONDENDTEMP,
                ps_PASSYN,                
                pi_CertificationTypeID,
                pd_SerialDate,
                pi_PreCondTime,
                pi_PostCondTime,
                pi_DIAMETERTESTDRUM,
                pi_PRECONDTEMP,
                pi_INFLATIONPRESSUREREADJUSTED,
                pi_CIRCUNFERENCEBEFORETEST,
                ps_RESULTPASSFAIL,
                pi_ENDURANCEHOURS,
                ps_POSSIBLEFAILURESFOUND,
                pi_CIRCUNFERENCEAFTERTEST,
                pi_OUTERDIAMETERDIFERENCE,
                pi_ODDIFERENCETOLERANCE,
                ps_SERIENOM,
                ps_FINALJUDGEMENT,
                ps_APPROVER,
                ls_operatorid,
                pi_certificateid
              );
             SELECT Max(ENDURANCEID) into li_CurrentEnduranceID
              FROM EnduranceHdr e 
             WHERE  e.certificateid = pi_certificateid and                  
                   e.CERTIFICATIONTYPEID = pi_CertificationTypeID;
                   
              pi_ENDURANCEID:=  li_CurrentEnduranceID;    
              
        end if ;     
  
      EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || '- Endurance_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.Endurance_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm ||  '- Endurance_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.Endurance_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm ||  '- Endurance_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.Endurance_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
  end Endurance_Save;
  
   PROCEDURE ENDURANCEDETAIL_SAVE( PI_TESTSTEP IN NUMBER,
                                  pi_TIMEINMIN IN NUMBER,
                                  PI_SPEED IN NUMBER,
                                  PI_TOTMILES IN NUMBER,
                                  PI_LOAD IN NUMBER,
                                  PI_LOADPERCENT IN NUMBER,
                                  PI_SETINFLATION IN NUMBER,
                                  PI_AMBTEMP IN NUMBER,
                                  PI_INFPRESSURE IN NUMBER,
                                  PD_STEPCOMPLETIONDATE IN EnduranceDtl.STEPCOMPLETIONDATE%Type,
                                  PI_ENDURANCEID IN NUMBER) AS 
     --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      
      ls_EnduranceDetailExists varchar2(1);
      li_certificationId integer;
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   BEGIN
        if PI_ENDURANCEID is null then
          raise li_ParametersAreNull;
        end if;
        if PI_ENDURANCEID <= 0 then
            raise li_ParametersAreInvalid;
        end if;
        
            INSERT INTO  EnduranceDtl (
                  TestStep,
                  TIMEINMIN,
                  SPEED,
                  TOTMILES,
                  LOAD,
                  LOADPERCENT,
                  SETINFLATION,
                  AMBTEMP,
                  INFPRESSURE,
                  STEPCOMPLETIONDATE,
                  ENDURANCEID
            )
            VALUES
            (
                  PI_TESTSTEP,
                  pi_TIMEINMIN,
                  PI_SPEED,
                  PI_TOTMILES,
                  PI_LOAD,
                  PI_LOADPERCENT,
                  PI_SETINFLATION,
                  PI_AMBTEMP,
                  PI_INFPRESSURE,
                  PD_STEPCOMPLETIONDATE  ,
                  PI_ENDURANCEID
            );
        
       
       EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || '- ENDURANCEDETAIL_SAVE. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.ENDURANCEDETAIL_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || '- ENDURANCEDETAIL_SAVE. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.ENDURANCEDETAIL_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- ENDURANCEDETAIL_SAVE. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.ENDURANCEDETAIL_SAVE',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);

   END ENDURANCEDETAIL_SAVE;
  
  PROCEDURE TREADWEAR_SAVE(PI_TREADWEARID OUT NUMBER,
                           PS_PROJECTNUMBER  IN VARCHAR2,
                           PI_TIRENUMBER IN NUMBER,
                           PS_TESTSPEC  IN VARCHAR2,
                           PD_COMPLETIONDATE IN DATE,
                           PS_DOTSERIALNUMBER  IN VARCHAR2,
                           PI_LOWESTWEARBAR IN NUMBER,
                           PS_PASSYN  IN VARCHAR2,                          
                           pi_CertificationTypeID in number,                           
                           PD_SERIALDATE IN DATE,
                           ps_OperatorName in varchar2,
                           pi_INDICATORSREQUIREMENT in number,
                           pi_CertificateID in Number) as
 --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      
      ls_TreadWearExists varchar2(1);
      li_certificationTypeId integer;
      li_TREADWEARID  TREADWEARHDR.TREADWEARID%type;
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   BEGIN
         if  pi_CertificationTypeID is null  then
            raise li_ParametersAreNull;
          end if;
         if  pi_CertificationTypeID <=0  then
            raise li_ParametersAreInvalid;
          end if;
        if ps_OperatorName is not null or ps_OperatorName <> '' then 
            ls_OperatorId := ps_OperatorName; 
        end if;
       
       
        ls_TreadWearExists:=  TESTRESULTS_CRUD.CheckIfTreadWearExists(PI_certificateid   => pi_certificateid ,
                                                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                                                    );
        
        if ls_TreadWearExists='y' then
              UPDATE  TreadWearHdr SET                   
                  PROJECTNUMBER   = PS_PROJECTNUMBER ,
                  TIRENUMBER      = PI_TIRENUMBER,
                  TESTSPEC        = PS_TESTSPEC,
                  COMPLETIONDATE  = PD_COMPLETIONDATE,
                  DOTSERIALNUMBER = PS_DOTSERIALNUMBER,
                  LOWESTWEARBAR   = PI_LOWESTWEARBAR,
                  PASSYN          = PS_PASSYN,
                  SERIALDATE      = PD_SERIALDATE,
                  INDICATORSREQUIREMENT	=	pi_INDICATORSREQUIREMENT,
                  ModifiedOn     = Sysdate,
                  Modifiedby     = ls_OperatorId
              WHERE CERTIFICATEID = pi_CertificateID And
                    CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                    
             SELECT Max(TREADWEARID) into li_TREADWEARID
             FROM  TreadWearHdr tw                             
             WHERE CERTIFICATEID = pi_CertificateID AND
                   tw.CERTIFICATIONTYPEID = pi_CertificationTypeID;
           
            delete from  treadweardtl where  treadwearid=li_TREADWEARID;
           
             PI_TREADWEARID:=li_TREADWEARID;
        else
              INSERT INTO  TreadWearHdr
              (
                TREADWEARID,
                PROJECTNUMBER,
                TIRENUMBER,
                TESTSPEC,
                COMPLETIONDATE,
                DOTSERIALNUMBER,
                LOWESTWEARBAR,
                PASSYN, 
                CERTIFICATIONTYPEID,                
                SERIALDATE   ,
                INDICATORSREQUIREMENT,
                CERTIFICATEID 
              )
              VALUES
              (
                TREADWEARID_SEQ.NextVal,
                PS_PROJECTNUMBER,
                PI_TIRENUMBER,
                PS_TESTSPEC,
                PD_COMPLETIONDATE,
                PS_DOTSERIALNUMBER,
                PI_LOWESTWEARBAR,
                PS_PASSYN,
                pi_CertificationTypeID,                            
                PD_SERIALDATE,
                pi_INDICATORSREQUIREMENT,
                pi_CertificateID
              );
              
              SELECT Max(TREADWEARID) into li_TREADWEARID
              FROM  TreadWearHdr tw                             
              WHERE CERTIFICATEID = pi_CertificateID AND
                    tw.CERTIFICATIONTYPEID = pi_CertificationTypeID;
           
             PI_TREADWEARID:=li_TREADWEARID;            
              
        end if ;
        
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - TREADWEAR_SAVE. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.TREADWEAR_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - TREADWEAR_SAVE. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.TREADWEAR_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' - TREADWEAR_SAVE. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.TREADWEAR_SAVE',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
End TREADWEAR_SAVE;	
  
  PROCEDURE TREADWEARDETAIL_SAVE(PI_TREADWEARID in NUMBER, 
                                 PI_WEARBARHEIGHT IN  treadweardtl.wearbarheight%type,
                                 PI_ITERATION IN  treadweardtl.iteration%TYPE,
                                 ps_OperatorName in varchar2) as
 --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      
      ls_TreadWearDetilExists varchar2(1);    
      li_TotDetail number;
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   BEGIN
         if PI_TREADWEARID is null  or PI_WEARBARHEIGHT is null then
            raise li_ParametersAreNull;
          end if;
         if PI_TREADWEARID <= 0  then
            raise li_ParametersAreInvalid;
          end if;
          
         if ps_OperatorName is not null or ps_OperatorName <> '' then 
            ls_OperatorId := ps_OperatorName; 
        end if;
        
            INSERT INTO  TREADWEARDTL
            (
              TREADWEARID,
              WEARBARHEIGHT,
              ITERATION,
              createdby
            )
            VALUES
            (
              PI_TREADWEARID,
              PI_WEARBARHEIGHT  ,
              PI_ITERATION,
              ls_OperatorId
            );
       
        
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - TREADWEARDETAIL_SAVE. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.TREADWEARDETAIL_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - TREADWEARDETAIL_SAVE. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.TREADWEARDETAIL_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  ' - TREADWEARDETAIL_SAVE. An error have ocurred.(when others)' || sqlerrm ;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.TREADWEARDETAIL_SAVE',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
              raise_application_error (-20007,ls_ErrorMsg);
               
End TREADWEARDETAIL_SAVE;	
  
   procedure PLUNGER_Save(  pi_PLUNGERID out Number,
                            ps_PROJECTNUMBER in Varchar2,
                            pi_TIRENUMBER in Number,
                            ps_TESTSPEC in Varchar2,
                            pd_COMPLETIONDATE in TimeStamp,
                            ps_DOTSERIALNUMBER in Varchar2,
                            pi_AVGBREAKINGENERGY in Number,
                            ps_PASSYN in Varchar2,
                            pi_CERTIFICATIONTYPEID in Number,                            
                            pd_SERIALDATE in TimeStamp,
                            pi_MINPLUNGER in Number,
                            ps_OperatorName in varchar2,
                            pi_CertificateID in number) as
     --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      
      ls_PlungerExists varchar2(1);
      li_certificationTypeId integer;
      
      li_PlungerId number;
      
      --varible
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   BEGIN

         if pi_CertificationTypeID is null  then
            raise li_ParametersAreNull;
          end if;
         if  pi_CertificationTypeID <= 0  then
            raise li_ParametersAreInvalid;
          end if;
         if ps_OperatorName is not null or ps_OperatorName <> '' then 
            ls_OperatorId:= ps_OperatorName;
         end if ;
       

        
        ls_PlungerExists:=  TESTRESULTS_CRUD.CheckIfPlungerExists(PI_certificateid   => pi_certificateid ,
                                                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                                                    );
        
        if ls_PlungerExists='y' then
            UPDATE  PlungerHdr SET
                  PROJECTNUMBER      = PS_PROJECTNUMBER ,
                  TIRENUMBER         = PI_TIRENUMBER,
                  TESTSPEC           = PS_TESTSPEC,
                  COMPLETIONDATE     = PD_COMPLETIONDATE,
                  DOTSERIALNUMBER    = PS_DOTSERIALNUMBER,
                  AVGBREAKINGENERGY  = PI_AVGBREAKINGENERGY,
                  PASSYN             = PS_PASSYN, 
                  SerialDate         = PD_SERIALDATE ,
                  MinPlunger         = PI_MINPLUNGER,
                  modifiedon         = Sysdate,
                  ModifiedBy         = ls_OperatorId
           WHERE CERTIFICATEID = pi_CertificateID And
                 CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                  
           
           SELECT Max(p.plungerid) into li_PlungerId
           FROM  PlungerHdr p
           WHERE CERTIFICATEID = pi_CertificateID And
                 CERTIFICATIONTYPEID = pi_CertificationTypeID ;
           
           delete from  plungerdtl where plungerid=li_plungerid;
           pi_PLUNGERID:=li_PlungerId;
        
        else
            INSERT INTO  PlungerHdr
              (
                PLUNGERID,
                PROJECTNUMBER,
                TIRENUMBER,
                TESTSPEC,
                COMPLETIONDATE,
                DOTSERIALNUMBER,
                AVGBREAKINGENERGY,
                PASSYN, 
                CERTIFICATIONTYPEID,                              
                SerialDate,
                MinPlunger,
                CertificateID
              )
              VALUES
              (
                PLUNGERID_SEQ.NextVal,
                PS_PROJECTNUMBER,
                PI_TIRENUMBER,
                PS_TESTSPEC,
                PD_COMPLETIONDATE,
                PS_DOTSERIALNUMBER,
                PI_AVGBREAKINGENERGY,
                PS_PASSYN,
                pi_CertificationTypeID,                             
                PD_SerialDate,
                PI_MinPlunger,
                pi_CertificateID 
              );
               SELECT Max(p.plungerid) into li_PlungerId
               FROM  PlungerHdr p
               WHERE CERTIFICATEID = pi_CertificateID And
                    CERTIFICATIONTYPEID = pi_CertificationTypeID ;
           
           pi_PLUNGERID:=li_PlungerId;
        
        end if;
        
 EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || '-PLUNGER_Save . There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.PLUNGER_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || '-PLUNGER_Save . There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.PLUNGER_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-PLUNGER_Save . An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.PLUNGER_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);

end PLUNGER_Save;	
  
  procedure PLUNGERDETAIL_Save(PI_BREAKINGENERGY  IN NUMBER,
                               PI_PLUNGERID  IN NUMBER,
                               PI_ITERATION IN NUMBER,
                               ps_OperatorName in varchar2) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_PLUNGERDETAILExists varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   BEGIN
         if PI_PLUNGERID is null  then
            raise li_ParametersAreNull;
          end if;
         if PI_PLUNGERID <= 0 then
            raise li_ParametersAreInvalid;
          end if;
          
          if ps_OperatorName is not null or ps_OperatorName <> '' then 
            ls_OperatorId:= ps_OperatorName;
         end if ;
                  
         INSERT INTO  PlungerDtl
              (
                BREAKINGENERGY, 
                PLUNGERID, 
                ITERATION,
                CreatedBy
              )
              VALUES
              (
                  PI_BREAKINGENERGY,
                  PI_PLUNGERID ,
                  PI_ITERATION,
                  ls_OperatorId
              );
        
        
  EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - PLUNGERDETAIL_SAVE. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.PLUNGERDETAIL_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - PLUNGERDETAIL_SAVE. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.PLUNGERDETAIL_SAVE',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' - PLUNGERDETAIL_SAVE. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.PLUNGERDETAIL_SAVE',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
End PLUNGERDETAIL_SAVE;	


  Procedure BeadUnseat_Save(pi_BEADUNSEATID out Number,
                            ps_PROJECTNUMBER in Varchar2,
                            pi_TIRENUMBER in Number,
                            ps_TESTSPEC in Varchar2,
                            pd_COMPLETIONDATE in TimeStamp,
                            ps_DOTSERIALNUMBER in Varchar2,
                            pi_LOWESTUNSEATVALUE in Number,
                            ps_PASSYN in Varchar2,
                            pi_CERTIFICATIONTYPEID in Number,                            
                            pd_SERIALDATE in TimeStamp,
                            pi_MINBEADUNSEAT in Number,                            
                            ps_TESTPASSFAIL in Varchar2,
                            ps_OperatorName   in varchar2 ,
                            pi_CertificateID in number ) as
                            
    --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_BeadUnseatExists varchar2(1);
      li_BeadUnseatID  BEADUNSEATHDR.BEADUNSEATID%type;
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   begin
   
         if pi_CertificationTypeID is null  then
            raise li_ParametersAreNull;
          end if;
         if pi_CertificationTypeID <= 0  then
            raise li_ParametersAreInvalid;
          end if;
          
          if ps_OperatorName   is not null or ps_OperatorName   <> '' then 
                ls_OperatorId:=ps_OperatorName ;
          end if ;
          
          ls_BeadUnseatExists:= TESTRESULTS_CRUD.CheckIfBeadUnseatExists(PI_certificateid   => pi_certificateid ,
                                                                      PI_CERTIFICATIONTYPEID => pi_CertificationTypeID
                                                                    );
       
       if ls_BeadUnseatExists='y' then    
          UPDATE  BEADUNSEATHDR set               
                PROJECTNUMBER     = ps_PROJECTNUMBER,
                TIRENUMBER        = pi_TIRENUMBER,
                TESTSPEC          = ps_TESTSPEC,
                COMPLETIONDATE    = pd_COMPLETIONDATE,
                DOTSERIALNUMBER   = ps_DOTSERIALNUMBER,
                LOWESTUNSEATVALUE = pi_LOWESTUNSEATVALUE,
                PASSYN            = ps_PASSYN,
                CERTIFICATIONTYPEID = pi_CERTIFICATIONTYPEID,                
                SERIALDATE        = pd_SERIALDATE,
                MINBEADUNSEAT     = pi_MINBEADUNSEAT,
                TESTPASSFAIL      = ps_TESTPASSFAIL,
                Modifiedby        = ls_OperatorId,
                ModifiedOn        = Sysdate                        
            WHERE CERTIFICATEID = pi_CertificateID And
                  CERTIFICATIONTYPEID = pi_CertificationTypeID ;
           
           SELECT Max(BEADUNSEATID) into li_BeadUnseatID
            FROM  BEADUNSEATHDR buh
            WHERE CERTIFICATEID = pi_CertificateID And
                    CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                  
            delete from  beadunseatdtl where beadunseatid=li_beadunseatid;
                  
             pi_BEADUNSEATID:=  li_BeadUnseatID;   
                  
       else              
            Insert INTO  BEADUNSEATHDR
            (
              BEADUNSEATID,
              PROJECTNUMBER,
              TIRENUMBER,
              TESTSPEC,
              COMPLETIONDATE,
              DOTSERIALNUMBER,
              LOWESTUNSEATVALUE,
              PASSYN,
              CERTIFICATIONTYPEID,             
              SERIALDATE,
              MINBEADUNSEAT,
              CREATEDBY,
              TESTPASSFAIL,
              CERTIFICATEID 
            )            
            VALUES
            (
              BEADUNSEATID_SEQ.NextVal,
              ps_PROJECTNUMBER,
              pi_TIRENUMBER,
              ps_TESTSPEC,
              pd_COMPLETIONDATE,
              ps_DOTSERIALNUMBER,
              pi_LOWESTUNSEATVALUE,
              ps_PASSYN,
              pi_CERTIFICATIONTYPEID,              
              pd_SERIALDATE,
              pi_MINBEADUNSEAT,
              ls_OperatorId,
              ps_TESTPASSFAIL,
              pi_CertificateID
            );
            --Gets the Id that just was inserted on the table to be returned
           SELECT Max(BEADUNSEATID) into li_BeadUnseatID
            FROM  BEADUNSEATHDR buh
           WHERE CERTIFICATEID = pi_CertificateID And
                 CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                  
             pi_BEADUNSEATID:=  li_BeadUnseatID;    
                  
       end if;
   
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - BeadUnseat_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.BeadUnseat_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - BeadUnseat_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.BeadUnseat_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' - BeadUnseat_Save.  An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.BeadUnseat_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
   end BeadUnseat_Save; 
   
   Procedure BeadUnseatDetail_Save(pi_BEADUNSEATID in NUMBER,
                                   pi_UNSEATFORCE in NUMBER,
                                   PI_ITERATION IN NUMBER,
                                   ps_OperatorName   in varchar2) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_BeadUnseatDETAILExists varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);         
   begin
   
        if pi_BEADUNSEATID is null then 
          raise li_ParametersAreNull;
        end if;
        if pi_BEADUNSEATID <=0 then 
          raise li_ParametersAreInvalid;
        end if;
        
        if ps_OperatorName   is not null or ps_OperatorName   <> '' then 
                ls_OperatorId:=ps_OperatorName ;
          end if ;
       
         INSERT INTO BEADUNSEATDTL ( BEADUNSEATID, UNSEATFORCE,ITERATION,CreatedBy ) 
         VALUES   (pi_BEADUNSEATID, pi_UNSEATFORCE,PI_ITERATION,ls_OperatorId );
        
        
    EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - BeadUnseatDetail_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.BeadUnseatDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - BeadUnseatDetail_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.BeadUnseatDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' - BeadUnseatDetail_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.BeadUnseatDetail_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
   end BeadUnseatDetail_Save;
   
   
  procedure HighSpeedHdr_Save( pi_HIGHSPEEDID          out number,
                                ps_PROJECTNUMBER        in varchar2,
                                pi_TIRENUM              in number  ,
                                ps_TESTSPEC             in varchar2,
                                pd_COMPETIONDATE        in TIMESTAMP,
                                ps_DOTSERIALNUMBER      in varchar2,
                                ps_MFGWWYY              in varchar2,
                                pd_PRECONDSTARTDATE     in TIMESTAMP,
                                pi_PRECONDSARTTEMP      in number ,                                
                                pd_precondtime          in  highspeedhdr.precondtime%type,
                                pi_RIMDIAMETER          in  highspeedhdr.rimdiameter%type,
                                pi_RIMWIDTH             in  highspeedhdr.rimwidth%type,
                                pd_PRECONDENDDATE       in TIMESTAMP,
                                pi_PRECONDENDTEMP       in number ,
                                pi_INFLATIONPRESSURE    in number ,
                                pi_BEFOREDIAMETER       in  highspeedhdr.BEFOREDIAMETER%type,
                                pi_AFTERDIAMETER        in  highspeedhdr.AFTERDIAMETER%type,
                                pi_BEFOREINFLATION      in number ,
                                pi_AFTERINFLATION       in number ,
                                pi_WHEELPOSITION        in number ,
                                pi_WHEELNUMBER          in number ,
                                pi_FINALTEMP            in number ,
                                pi_FINALDISTANCE        in  highspeedhdr.FINALDISTANCE%type,
                                pi_FINALINFLATION       in number ,
                                pd_POSTCONDSTARTDATE    in TIMESTAMP,
                                pd_POSTCONDENDDATE      in TIMESTAMP,
                                pi_POSTCONDENDTEMP      in number ,
                                ps_PASSYN               in varchar2,
                                pd_SERIALDATE           in TIMESTAMP,
                                pi_POSTCONDTIME         in  highspeedhdr.POSTCONDTIME%type,
                                pi_CERTIFICATIONTYPEID  in number ,                               
                                pi_DIAMETERTESTDRUM in Number,                                
                                pi_PRECONDTEMP in Number,
                                pi_INFLATIONPRESSUREREADJUSTED in Number,
                                pi_CIRCUNFERENCEBEFORETEST in Number,
                                pi_WHEELSPEEDRPM in Number,
                                pi_WHEELSPEEDKMH in Number,
                                pi_CIRCUNFERENCEAFTERTEST in Number,
                                pi_ODDIFERENCE in Number,
                                pi_ODDIFERENCETOLERANCE in Number,
                                ps_SERIENOM in Varchar2,
                                ps_FINALJUDGEMENT in Varchar2,
                                ps_APPROVER in Varchar2,
                                pi_PASSATKMH in Number,
                                ps_SPEEDTTESTPASSFAIL in Varchar2,
                                pi_SPEEDTOTALTIME in Number,
                                pi_MAXSPEED in Number,
                                pi_MAXLOAD in Number,
                                ps_OperatorName in Varchar2,
                                pi_CertificateID in number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_HighSpeedExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);  
      
      li_CurrentHighSpeedID  HIGHSPEEDHDR.HIGHSPEEDID%type;
      
   begin
          if ps_OperatorName   is not null or ps_OperatorName   <> '' then
            ls_OperatorId:=ps_OperatorName ;
          end if;
          
          ls_HighSpeedExist:=  TESTRESULTS_CRUD.CHECKIFHIGHSPEEDEXISTS(
                                                        PI_certificateid   => pi_certificateid ,
                                                        PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                                      );
          
          if ls_HighSpeedExist = 'y' then 
              UPDATE  HIGHSPEEDHDR SET
                    PROJECTNUMBER               = ps_PROJECTNUMBER,
                    TIRENUM                     = pi_TIRENUM,
                    TESTSPEC                    = ps_TESTSPEC,
                    COMPETIONDATE               = pd_COMPETIONDATE,
                    DOTSERIALNUMBER             = ps_DOTSERIALNUMBER,
                    MFGWWYY                     = ps_MFGWWYY,
                    PRECONDSTARTDATE            = pd_PRECONDSTARTDATE,
                    PRECONDSARTTEMP             = pi_PRECONDSARTTEMP,
                    RIMDIAMETER                 = pi_RIMDIAMETER,
                    RIMWIDTH                    = pi_RIMWIDTH,
                    PRECONDENDDATE              = pd_PRECONDENDDATE,
                    PRECONDENDTEMP              = pi_PRECONDENDTEMP,
                    INFLATIONPRESSURE           = pi_INFLATIONPRESSURE,
                    BEFOREDIAMETER              = pi_BEFOREDIAMETER,
                    AFTERDIAMETER               = pi_AFTERDIAMETER,
                    BEFOREINFLATION             = pi_BEFOREINFLATION,
                    AFTERINFLATION              = pi_AFTERINFLATION,
                    WHEELPOSITION               = pi_WHEELPOSITION,
                    WHEELNUMBER                 = pi_WHEELNUMBER,
                    FINALTEMP                   = pi_FINALTEMP,
                    FINALDISTANCE               = pi_FINALDISTANCE,
                    FINALINFLATION              = pi_FINALINFLATION,
                    POSTCONDSTARTDATE           = pd_POSTCONDSTARTDATE,
                    POSTCONDENDDATE             = pd_POSTCONDENDDATE,
                    POSTCONDENDTEMP             = pi_POSTCONDENDTEMP,
                    PASSYN                      = ps_PASSYN,
                    SERIALDATE                  = pd_SERIALDATE,
                    POSTCONDTIME                = pi_POSTCONDTIME,                                
                    MODIFIEDBY                  = ls_OperatorId,
                    MODIFIEDON                  = SYSTIMESTAMP,
                    DIAMETERTESTDRUM            = pi_DIAMETERTESTDRUM,                    
                    PRECONDTEMP                 = pi_PRECONDTEMP,
                    INFLATIONPRESSUREREADJUSTED = pi_INFLATIONPRESSUREREADJUSTED,
                    CIRCUNFERENCEBEFORETEST     = pi_CIRCUNFERENCEBEFORETEST,
                    WHEELSPEEDRPM               = pi_WHEELSPEEDRPM,
                    WHEELSPEEDKMH               = pi_WHEELSPEEDKMH,
                    CIRCUNFERENCEAFTERTEST      = pi_CIRCUNFERENCEAFTERTEST,
                    ODDIFERENCE                 = pi_ODDIFERENCE,
                    ODDIFERENCETOLERANCE        = pi_ODDIFERENCETOLERANCE,
                    SERIENOM                    = ps_SERIENOM,
                    FINALJUDGEMENT              = ps_FINALJUDGEMENT,
                    APPROVER                    = ps_APPROVER,
                    PASSATKMH                   = pi_PASSATKMH,
                    SPEEDTTESTPASSFAIL          = ps_SPEEDTTESTPASSFAIL,
                    SPEEDTOTALTIME              = pi_SPEEDTOTALTIME,
                    MAXSPEED                    = pi_MAXSPEED,
                    MAXLOAD                     = pi_MAXLOAD
            WHERE CERTIFICATEID = pi_CertificateID And
                  CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                
            SELECT Max(HIGHSPEEDID) into li_CurrentHighSpeedID
             FROM  HIGHSPEEDHDR h
            WHERE CERTIFICATEID = pi_CertificateID And
                  CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                    
              Delete from  HIGHSPEEDDTL where HIGHSPEEDID=li_CurrentHighSpeedID;   
              
              Delete from  speedtestdetail where HIGHSPEEDID=li_CurrentHighSpeedID;
              
               pi_highspeedid :=  li_CurrentHighSpeedID;     
                
                
          else
              INSERT INTO  HIGHSPEEDHDR
                    (
                      HIGHSPEEDID,
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
                      CERTIFICATIONTYPEID,                                           
                      CREATEDBY,
                      CREATEDON,
                      DIAMETERTESTDRUM,                     
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
                      CertificateID
                      )
                VALUES
                    (
                      HIGHSPEEDID_SEQ.Nextval,
                      ps_PROJECTNUMBER,
                      pi_TIRENUM,
                      ps_TESTSPEC,
                      pd_COMPETIONDATE,
                      ps_DOTSERIALNUMBER,
                      ps_MFGWWYY,
                      pd_PRECONDSTARTDATE,
                      pi_PRECONDSARTTEMP,                     
                      pi_RIMDIAMETER,
                      pi_RIMWIDTH ,
                      pd_PRECONDENDDATE,
                      pi_PRECONDENDTEMP ,                     
                      pi_INFLATIONPRESSURE,
                      pi_BEFOREDIAMETER,
                      pi_AFTERDIAMETER,
                      pi_BEFOREINFLATION,
                      pi_AFTERINFLATION,
                      pi_WHEELPOSITION,
                      pi_WHEELNUMBER,
                      pi_FINALTEMP,
                      pi_FINALDISTANCE,
                      pi_FINALINFLATION,
                      pd_POSTCONDSTARTDATE,
                      pd_POSTCONDENDDATE ,
                      pi_POSTCONDENDTEMP ,
                      ps_PASSYN ,
                      pd_SERIALDATE ,
                      pi_POSTCONDTIME ,
                      pi_CERTIFICATIONTYPEID,                     
                      ls_OperatorId,
                      systimestamp,
                      pi_DIAMETERTESTDRUM,                     
                      pi_PRECONDTEMP,
                      pi_INFLATIONPRESSUREREADJUSTED,
                      pi_CIRCUNFERENCEBEFORETEST,
                      pi_WHEELSPEEDRPM,
                      pi_WHEELSPEEDKMH,
                      pi_CIRCUNFERENCEAFTERTEST,
                      pi_ODDIFERENCE,
                      pi_ODDIFERENCETOLERANCE,
                      ps_SERIENOM,
                      ps_FINALJUDGEMENT,
                      ps_APPROVER,
                      pi_PASSATKMH,
                      ps_SPEEDTTESTPASSFAIL,
                      pi_SPEEDTOTALTIME,
                      pi_MAXSPEED,
                      pi_MAXLOAD,
                      pi_CertificateID
                    );
                 SELECT Max(HIGHSPEEDID) into li_CurrentHighSpeedID
                 FROM  HIGHSPEEDHDR h
                 WHERE certificateid = pi_CertificateID AND
                       h.CERTIFICATIONTYPEID = pi_CertificationTypeID;
                 
                 Delete from  HIGHSPEEDDTL where HIGHSPEEDID=li_CurrentHighSpeedID;   
              
                 Delete from  speedtestdetail where HIGHSPEEDID=li_CurrentHighSpeedID;
              
                 pi_highspeedid :=  li_CurrentHighSpeedID;  
                    
          end if;
        
   
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || 'HighSpeedHdr_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.HighSpeedHdr_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || 'HighSpeedHdr_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.HighSpeedHdr_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'HighSpeedHdr_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.HighSpeedHdr_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
   
   end HighSpeedHdr_Save;
   
   procedure HighSpeedDetail_Save ( pi_HIGHSPEEDID in Number,
                                    pi_TESTSTEP in number,
                                    pi_TIMEINMIN in Number,
                                    pi_SPEED in  HighSpeedDtl.SPEED%Type,
                                    pi_TOTMILES in  HighSpeedDtl.TOTMILES%Type,
                                    pi_LOAD in  HighSpeedDtl.LOAD%Type,
                                    pi_LOADPERCENT in Number,
                                    pi_SETINFLATION in Number,
                                    pi_AMBTEMP in Number,
                                    pi_INFPRESSURE in Number,
                                    pd_STEPCOMPLETIONDATE in HighSpeedDtl.STEPCOMPLETIONDATE%Type,                                    
                                    ps_OperatorID in varchar2) as
                                    
     --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_HighSpeedExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);  
      
   begin
        if  ps_OperatorID is not null or ps_OperatorID <> '' then
          ls_OperatorId:=ps_OperatorID;
        end if;  
   
        INSERT INTO HIGHSPEEDDTL
          (
            HIGHSPEEDID,
            TESTSTEP,
            TIMEINMIN,
            SPEED,
            TOTMILES,
            LOAD,
            LOADPERCENT,
            SETINFLATION,
            AMBTEMP,
            INFPRESSURE,
            STEPCOMPLETIONDATE,            
            CREATEDBY
          )
          VALUES
          (
            pi_HIGHSPEEDID,
            pi_TESTSTEP ,
            pi_TIMEINMIN ,
            pi_SPEED ,
            pi_TOTMILES ,
            pi_LOAD ,
            pi_LOADPERCENT ,
            pi_SETINFLATION ,
            pi_AMBTEMP ,
            pi_INFPRESSURE ,
            pd_STEPCOMPLETIONDATE ,
            ls_OperatorId        
          );
  
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || '- HighSpeedDetail_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.HighSpeedDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || '- HighSpeedDetail_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.HighSpeedDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || '- HighSpeedDetail_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.HighSpeedDetail_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
   end HighSpeedDetail_Save;
   
   Procedure HIghSpeed_SpeedTestDetail_Save(pi_ITERATION in Number,
                                            pd_TIME in TimeStamp,
                                            pi_SPEED in Number,
                                            pi_HIGHSPEEDID in Number,
                                            ps_OperatorName in Varchar2) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_HighSpeedExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                                      
   begin
       if pi_HIGHSPEEDID is null or pi_HIGHSPEEDID <= 0 then 
          raise li_ParametersAreNull;
       end if; 
       
       if pi_HIGHSPEEDID <= 0 then 
          raise li_ParametersAreInvalid;
       end if;
       
       if  ps_OperatorName is not null or ps_OperatorName <> '' then
          ls_OperatorId:=ps_OperatorName;
        end if;  
   
         INSERT INTO  SPEEDTESTDETAIL
        (
          ITERATION,
          TIME,
          SPEED,
          HIGHSPEEDID,
          CREATEDBY
         
        )
        VALUES
        (
          pi_ITERATION,
          pd_TIME,
          pi_SPEED,
          pi_HIGHSPEEDID,
          ls_OperatorId          
        );
   
   
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - HIghSpeed_SpeedTestDetail_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.HIghSpeed_SpeedTestDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - HIghSpeed_SpeedTestDetail_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.HIghSpeed_SpeedTestDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg);
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' - HIghSpeed_SpeedTestDetail_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.HIghSpeed_SpeedTestDetail_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
   end HIghSpeed_SpeedTestDetail_Save;                                            
                                            
   
   procedure SoundHDR_Save(   ps_UserID                     in varchar2,
                              pi_SoundID                    out number,
                              ps_PROJECTNUMBER              in varchar2,
                              pi_TIRENUMBER                 in number,
                              ps_TESTSPEC                   in varchar2,
                              ps_TESTREPORTNUMBER           in varchar2,
                              ps_MANUFACTUREANDBRAND        in varchar2,
                              ps_TIRECLASS                  in varchar2,
                              ps_CATEGORYOFUSE              in varchar2,
                              pd_DATEOFTEST                 in TIMESTAMP,
                              ps_TESTVEHICULE               in varchar2,
                              ps_TESTVEHICULEWHEELBASE      in varchar2,
                              ps_LOCATIONOFTESTTRACK        in varchar2,
                              pd_DATETRACKCERTIFTOISO       in TIMESTAMP,
                              ps_TIRESIZEDESIGNATION        in varchar2,
                              ps_TIRESERVICEDESCRIPTION     in varchar2,
                              ps_TESTMASS_FRONTL            in varchar2,
                              ps_TESTMASS_FRONTR            in varchar2,
                              ps_TESTMASS_REARL             in varchar2,
                              ps_TESTMASS_REARR             in varchar2,
                              ps_TIRELOADINDEX_FRONTL       in varchar2,
                              ps_TIRELOADINDEX_FRONTR       in varchar2,
                              ps_TIRELOADINDEX_REARL        in varchar2,
                              ps_TIRELOADINDEX_REARR        in varchar2,
                              ps_INFLATIONPRESSURECO_FRONTL in varchar2,
                              ps_INFLATIONPRESSURECO_FRONTR in varchar2,
                              ps_INFLATIONPRESSURECO_REARL  in varchar2,
                              ps_INFLATIONPRESSURECO_REARR  in varchar2,
                              ps_TESTRIMWIDTHCODE           in varchar2,
                              ps_TEMPMEASURESENSORTYPE      in varchar2,
                              pi_CERTIFICATIONTYPEID        in number,                             
                              pi_SKUID                      in number,
                              ps_ReferenceInflationPressure in varchar2,
                              pi_CertificateID in number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_SoundExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);  
      
      li_CurrentSoundID  SOUNDHDR.SOUNDID%type;
      
   begin
        if  ps_UserID is not null or ps_UserID <> '' then
          ls_OperatorId:=ps_UserID;
        end if;
        
        ls_SoundExist:=  TESTRESULTS_CRUD.CheckIfSoundExixts(
                                                        PI_certificateid   => pi_certificateid ,
                                                        PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                                      );
        
        if ls_SoundExist = 'y' then
               UPDATE SOUNDHDR set                             
                      PROJECTNUMBER              = ps_PROJECTNUMBER   ,
                      TIRENUMBER                 = pi_TIRENUMBER   ,
                      TESTSPEC                   = ps_TESTSPEC   ,
                      TESTREPORTNUMBER           = ps_TESTREPORTNUMBER   ,
                      MANUFACTUREANDBRAND        = ps_MANUFACTUREANDBRAND   ,
                      TIRECLASS                  = ps_TIRECLASS   ,
                      CATEGORYOFUSE              = ps_CATEGORYOFUSE   ,
                      DATEOFTEST                 = pd_DATEOFTEST   ,
                      TESTVEHICULE               = ps_TESTVEHICULE   ,
                      TESTVEHICULEWHEELBASE      = ps_TESTVEHICULEWHEELBASE   ,
                      LOCATIONOFTESTTRACK        = ps_LOCATIONOFTESTTRACK   ,
                      DATETRACKCERTIFTOISO       = pd_DATETRACKCERTIFTOISO   ,
                      TIRESIZEDESIGNATION        = ps_TIRESIZEDESIGNATION   ,
                      TIRESERVICEDESCRIPTION     = ps_TIRESERVICEDESCRIPTION   ,
                      TESTMASS_FRONTL            = ps_TESTMASS_FRONTL   ,
                      TESTMASS_FRONTR            = ps_TESTMASS_FRONTR   ,
                      TESTMASS_REARL             = ps_TESTMASS_REARL   ,
                      TESTMASS_REARR             = ps_TESTMASS_REARR   ,
                      TIRELOADINDEX_FRONTL       = ps_TIRELOADINDEX_FRONTL   ,
                      TIRELOADINDEX_FRONTR       = ps_TIRELOADINDEX_FRONTR   ,
                      TIRELOADINDEX_REARL        = ps_TIRELOADINDEX_REARL   ,
                      TIRELOADINDEX_REARR        = ps_TIRELOADINDEX_REARR   ,
                      INFLATIONPRESSURECO_FRONTL = ps_INFLATIONPRESSURECO_FRONTL   ,
                      INFLATIONPRESSURECO_FRONTR = ps_INFLATIONPRESSURECO_FRONTR   ,
                      INFLATIONPRESSURECO_REARL  = ps_INFLATIONPRESSURECO_REARL   ,
                      INFLATIONPRESSURECO_REARR  = ps_INFLATIONPRESSURECO_REARR   ,
                      TESTRIMWIDTHCODE           = ps_TESTRIMWIDTHCODE   ,
                      TEMPMEASURESENSORTYPE      = ps_TEMPMEASURESENSORTYPE   ,                      
                      MODIFIEDBY                 = ls_OperatorId   ,
                      MODIFIEDON                 = SYSTIMESTAMP,
                      ReferenceInflationPressure = ps_ReferenceInflationPressure
               WHERE CERTIFICATEID = pi_CertificateID And
                    CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                
            SELECT Max(SOUNDID) into li_CurrentSoundID
             FROM  SOUNDHDR s
             WHERE CERTIFICATEID = pi_CertificateID AND                 
                   s.CERTIFICATIONTYPEID = pi_CertificationTypeID;
                    
              Delete from  SOUNDDETAIL where SOUNDID=li_CurrentSoundID;   
              
               pi_SoundID :=  li_CurrentSoundID;     
        else
              INSERT INTO SOUNDHDR
                            (
                              SOUNDID,
                              PROJECTNUMBER,
                              TIRENUMBER,
                              TESTSPEC,
                              TESTREPORTNUMBER,
                              MANUFACTUREANDBRAND,
                              TIRECLASS,
                              CATEGORYOFUSE,
                              DATEOFTEST,
                              TESTVEHICULE,
                              TESTVEHICULEWHEELBASE,
                              LOCATIONOFTESTTRACK,
                              DATETRACKCERTIFTOISO,
                              TIRESIZEDESIGNATION,
                              TIRESERVICEDESCRIPTION,
                              TESTMASS_FRONTL,
                              TESTMASS_FRONTR,
                              TESTMASS_REARL,
                              TESTMASS_REARR,
                              TIRELOADINDEX_FRONTL,
                              TIRELOADINDEX_FRONTR,
                              TIRELOADINDEX_REARL,
                              TIRELOADINDEX_REARR,
                              INFLATIONPRESSURECO_FRONTL,
                              INFLATIONPRESSURECO_FRONTR,
                              INFLATIONPRESSURECO_REARL,
                              INFLATIONPRESSURECO_REARR,
                              TESTRIMWIDTHCODE,
                              TEMPMEASURESENSORTYPE,
                              CERTIFICATIONTYPEID,                                                            
                              CREATEDBY,
                              CREATEDON,
                              MODIFIEDBY,
                              MODIFIEDON,
                              ReferenceInflationPressure,
                              CertificateID
                            )
                            VALUES
                            (
                              SOUNDID_SEQ.NextVAl,
                              ps_PROJECTNUMBER,
                              pi_TIRENUMBER,
                              ps_TESTSPEC,
                              ps_TESTREPORTNUMBER,
                              ps_MANUFACTUREANDBRAND,
                              ps_TIRECLASS,
                              ps_CATEGORYOFUSE,
                              pd_DATEOFTEST,
                              ps_TESTVEHICULE,
                              ps_TESTVEHICULEWHEELBASE,
                              ps_LOCATIONOFTESTTRACK,
                              pd_DATETRACKCERTIFTOISO,
                              ps_TIRESIZEDESIGNATION,
                              ps_TIRESERVICEDESCRIPTION,
                              ps_TESTMASS_FRONTL,
                              ps_TESTMASS_FRONTR,
                              ps_TESTMASS_REARL,
                              ps_TESTMASS_REARR,
                              ps_TIRELOADINDEX_FRONTL,
                              ps_TIRELOADINDEX_FRONTR,
                              ps_TIRELOADINDEX_REARL,
                              ps_TIRELOADINDEX_REARR,
                              ps_INFLATIONPRESSURECO_FRONTL,
                              ps_INFLATIONPRESSURECO_FRONTR,
                              ps_INFLATIONPRESSURECO_REARL,
                              ps_INFLATIONPRESSURECO_REARR,
                              ps_TESTRIMWIDTHCODE,
                              ps_TEMPMEASURESENSORTYPE,
                              pi_CERTIFICATIONTYPEID,                                                        
                              ls_OperatorId,
                              SYSTIMESTAMP,
                              ls_OperatorId,
                              SYSTIMESTAMP,
                              ps_ReferenceInflationPressure,
                              pi_CertificateID
                            );
                            
                             SELECT Max(SOUNDID) into li_CurrentSoundID
                             FROM  SOUNDHDR s
                             WHERE CERTIFICATEID = pi_CertificateID And
                                   CERTIFICATIONTYPEID = pi_CertificationTypeID ;                                          
                            
                            pi_SoundID :=  li_CurrentSoundID;  
                            
        end if;
        
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || 'SoundHDR_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.SoundHDR_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || 'SoundHDR_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.SoundHDR_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'SoundHDR_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.SoundHDR_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
   end SoundHDR_Save;
   
  procedure SoundDetail_Save( ps_UserID in varchar2,
                              pi_ITERATION in number,
                              ps_TESTSPEED  in varchar2,
                              ps_DIRECTIONOFRUN  in varchar2,
                              ps_SOUNDLEVELLEFT  in varchar2,
                              ps_SOUNDLEVELRIGHT  in varchar2,
                              ps_AIRTEMP  in varchar2,
                              ps_TRACKTEMP in varchar2,
                              ps_SOUNDLEVELLEFT_TEMPCOR in varchar2,
                              ps_SOUNDLEVELRIGHT_TEMPCOR  in varchar2,
                              pi_SOUNDID in number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_HighSpeedExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);  
      
   begin
        if  ps_UserID is not null or ps_UserID <> '' then
          ls_OperatorId:=ps_UserID;          
        end if;
        
        INSERT INTO SOUNDDETAIL
                    (
                      ITERATION,
                      TESTSPEED,
                      DIRECTIONOFRUN,
                      SOUNDLEVELLEFT,
                      SOUNDLEVELRIGHT,
                      AIRTEMP,
                      TRACKTEMP,
                      SOUNDLEVELLEFT_TEMPCORRECTED,
                      SOUNDLEVELRIGHT_TEMPCORRECTED,
                      SOUNDID,
                      CREATEDBY,
                      CREATEDON
                    
                    )
        VALUES
                    (
                      pi_ITERATION,
                      ps_TESTSPEED,
                      ps_DIRECTIONOFRUN,
                      ps_SOUNDLEVELLEFT,
                      ps_SOUNDLEVELRIGHT,
                      ps_AIRTEMP,
                      ps_TRACKTEMP,
                      ps_SOUNDLEVELLEFT_TEMPCOR,
                      ps_SOUNDLEVELRIGHT_TEMPCOR,
                      pi_SOUNDID,
                      ls_OperatorId,
                      SYSTIMESTAMP
                    );
        
        
        
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || 'SoundDetail_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.SoundDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || 'SoundDetail_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.SoundDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'SoundDetail_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.SoundDetail_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
  end SoundDetail_Save;

   procedure WetGripHDR_Save( ps_UserID in varchar2,
                              pi_WETGRIPID  out Number,
                              ps_PROJECTNUMBER  in Varchar2,
                              pi_TIRENUMBER  in Varchar2,
                              ps_TESTSPEC  in Varchar2,
                              pd_DATEOFTEST  in TIMESTAMP,
                              ps_TESTVEHICLE  in Varchar2,
                              ps_LOCATIONOFTESTTRACK  in Varchar2,
                              ps_TESTTRACKCHARACTERISTICS  in Varchar2,
                              ps_ISSUEBY  in Varchar2,
                              ps_METHODOFCERTIFICATION  in Varchar2,
                              ps_TESTTIREDETAILS  in Varchar2,
                              ps_TIRESIZEANDSERVICEDESC  in Varchar2,
                              ps_TIREBRANDANDTRADEDESC  in Varchar2,
                              ps_REFERENCEINFLATIONPRESSURE  in Varchar2,
                              ps_TESTRIMWITHCODE  in Varchar2,
                              ps_TEMPMEASURESENSORTYPE  in Varchar2,
                              ps_IDENTIFICATIONSRTT  in Varchar2,
                              ps_TESTTIRELOAD_SRTT  in Varchar2,
                              ps_TESTTIRELOAD_CANDIDATE  in Varchar2,
                              ps_TESTTIRELOAD_CONTROL  in Varchar2,
                              ps_WATERDEPTH_SRTT  in Varchar2,
                              ps_WATERDEPTH_CANDIDATE  in Varchar2,
                              ps_WATERDEPTH_CONTROL  in Varchar2,
                              ps_WETTEDTRACKTEMPAVG  in Varchar2,
                              pi_CERTIFICATIONTYPEID  in Number,                              
                              pi_SKUID  in Number,
                              pi_CertificateID in number) as
      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_WetGripExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000); 
      
      li_CurrentWETGRIPID  wetgriphdr.wetgripid%type;
      
   begin
        if  ps_UserID is not null or ps_UserID <> '' then
          ls_OperatorId:=ps_UserID;
        end if;       
      
          ls_wetGripExist := TESTRESULTS_CRUD.CHECKIFWETGRIPEXIXTS(
                                                pi_CertificateID => pi_CertificateID,
                                                 PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID
                                              );                                                      
        
        if ls_wetGripExist='y' then
              UPDATE WetGripHdr Set                     
                      PROJECTNUMBER              = ps_PROJECTNUMBER,
                      TIRENUMBER                 = pi_TIRENUMBER,
                      TESTSPEC                   = ps_TESTSPEC,
                      DATEOFTEST                 = pd_DATEOFTEST,
                      TESTVEHICLE                = ps_TESTVEHICLE,
                      LOCATIONOFTESTTRACK        = ps_LOCATIONOFTESTTRACK,
                      TESTTRACKCHARACTERISTICS   = ps_TESTTRACKCHARACTERISTICS,
                      ISSUEBY                    = ps_ISSUEBY,
                      METHODOFCERTIFICATION      = ps_METHODOFCERTIFICATION,
                      TESTTIREDETAILS            = ps_TESTTIREDETAILS,
                      TIRESIZEANDSERVICEDESC     = ps_TIRESIZEANDSERVICEDESC,
                      TIREBRANDANDTRADEDESC      = ps_TIREBRANDANDTRADEDESC,
                      REFERENCEINFLATIONPRESSURE = ps_REFERENCEINFLATIONPRESSURE,
                      TESTRIMWITHCODE            = ps_TESTRIMWITHCODE,
                      TEMPMEASURESENSORTYPE      = ps_TEMPMEASURESENSORTYPE,
                      IDENTIFICATIONSRTT         = ps_IDENTIFICATIONSRTT,
                      TESTTIRELOAD_SRTT          = ps_TESTTIRELOAD_SRTT,
                      TESTTIRELOAD_CANDIDATE     = ps_TESTTIRELOAD_CANDIDATE,
                      TESTTIRELOAD_CONTROL       = ps_TESTTIRELOAD_CONTROL,
                      WATERDEPTH_SRTT            = ps_WATERDEPTH_SRTT,
                      WATERDEPTH_CANDIDATE       = ps_WATERDEPTH_CANDIDATE,
                      WATERDEPTH_CONTROL         = ps_WATERDEPTH_CONTROL,
                      WETTEDTRACKTEMPAVG         = ps_WETTEDTRACKTEMPAVG,                                        
                      MODIFIEDBY                 = ls_OperatorId   ,
                      MODIFIEDON                 = SYSTIMESTAMP
                WHERE CERTIFICATEID = pi_CertificateID And
                      CERTIFICATIONTYPEID = pi_CertificationTypeID ;  
                      
                      
                SELECT Max(WETGRIPID) into li_CurrentWETGRIPID
                 FROM  WETGRIPHDR W
                 WHERE CERTIFICATEID = pi_CertificateID And
                    CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                    
                 Delete from  wetgripdetail where WETGRIPID=li_CurrentWETGRIPID;   
              
                 pi_WETGRIPID :=  li_CurrentWETGRIPID;      
                      
                  

        else
              INSERT INTO WETGRIPHDR
                                    (
                                      WETGRIPID,
                                      PROJECTNUMBER,
                                      TIRENUMBER,
                                      TESTSPEC,
                                      DATEOFTEST,
                                      TESTVEHICLE,
                                      LOCATIONOFTESTTRACK,
                                      TESTTRACKCHARACTERISTICS,
                                      ISSUEBY,
                                      METHODOFCERTIFICATION,
                                      TESTTIREDETAILS,
                                      TIRESIZEANDSERVICEDESC,
                                      TIREBRANDANDTRADEDESC,
                                      REFERENCEINFLATIONPRESSURE,
                                      TESTRIMWITHCODE,
                                      TEMPMEASURESENSORTYPE,
                                      IDENTIFICATIONSRTT,
                                      TESTTIRELOAD_SRTT,
                                      TESTTIRELOAD_CANDIDATE,
                                      TESTTIRELOAD_CONTROL,
                                      WATERDEPTH_SRTT,
                                      WATERDEPTH_CANDIDATE,
                                      WATERDEPTH_CONTROL,
                                      WETTEDTRACKTEMPAVG,
                                      CERTIFICATIONTYPEID,                                                                         
                                      CREATEDBY,
                                      CREATEDON,
                                      CertificateID
                                    )
              VALUES
                                    (
                                      WETGRIPID_SEQ.nextval,
                                      ps_PROJECTNUMBER,
                                      pi_TIRENUMBER,
                                      ps_TESTSPEC,
                                      pd_DATEOFTEST,
                                      ps_TESTVEHICLE,
                                      ps_LOCATIONOFTESTTRACK,
                                      ps_TESTTRACKCHARACTERISTICS,
                                      ps_ISSUEBY,
                                      ps_METHODOFCERTIFICATION,
                                      ps_TESTTIREDETAILS,
                                      ps_TIRESIZEANDSERVICEDESC,
                                      ps_TIREBRANDANDTRADEDESC,
                                      ps_REFERENCEINFLATIONPRESSURE,
                                      ps_TESTRIMWITHCODE,
                                      ps_TEMPMEASURESENSORTYPE,
                                      ps_IDENTIFICATIONSRTT,
                                      ps_TESTTIRELOAD_SRTT,
                                      ps_TESTTIRELOAD_CANDIDATE,
                                      ps_TESTTIRELOAD_CONTROL,
                                      ps_WATERDEPTH_SRTT,
                                      ps_WATERDEPTH_CANDIDATE,
                                      ps_WATERDEPTH_CONTROL,
                                      ps_WETTEDTRACKTEMPAVG,
                                      pi_CERTIFICATIONTYPEID,                                                                            
                                      ls_OperatorId,
                                      SYSTIMESTAMP,
                                      pi_CertificateID
                                      );

                SELECT Max(WETGRIPID) into li_CurrentWETGRIPID
                 FROM  WETGRIPHDR W
                 WHERE CERTIFICATEID = pi_CertificateID And
                    CERTIFICATIONTYPEID = pi_CertificationTypeID ;
                       
                 pi_WETGRIPID :=  li_CurrentWETGRIPID;                                         
        end if;
                
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || 'WetGripHDR_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.WetGripHDR_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || 'WetGripHDR_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.WetGripHDR_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || 'WetGripHDR_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.WetGripHDR_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
   end WetGripHDR_Save;  
   
   
   procedure WetGripDetail_Save( ps_UserID in varchar2,
                                 pi_ITERATION  in number,
                                  ps_TESTSPEED  in varchar2,
                                  ps_DIRECTIONOFRUN  in varchar2,
                                  ps_SRTT  in varchar2,
                                  ps_CANDIDATETIRE  in varchar2,
                                  ps_PEAKBREAKFORCECOEFICIENT  in varchar2,
                                  ps_MEANFULLYDEVDECELERATION  in varchar2,
                                  ps_WETGRIPINDEX  in varchar2,
                                  ps_COMMENTS  in varchar2,
                                  pi_WETGRIPID  in number) as
        --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);  
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);       
      --varible
      ls_HighSpeedExist varchar2(1); 
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);  
      
   begin
        if  ps_UserID is not null or ps_UserID <> '' then
          ls_OperatorId:=ps_UserID;
        end if;
        
        INSERT INTO WETGRIPDETAIL
                (
                  ITERATION,
                  TESTSPEED,
                  DIRECTIONOFRUN,
                  SRTT,
                  CANDIDATETIRE,
                  PEAKBREAKFORCECOEFICIENT,
                  MEANFULLYDEVELOPEDDECELERATION,
                  WETGRIPINDEX,
                  COMMENTS,
                  WETGRIPID,
                  CREATEDBY,
                  CREATEDON
                )
                VALUES
                (
                  pi_ITERATION ,
                  ps_TESTSPEED ,
                  ps_DIRECTIONOFRUN ,
                  ps_SRTT ,
                  ps_CANDIDATETIRE ,
                  ps_PEAKBREAKFORCECOEFICIENT ,
                  ps_MEANFULLYDEVDECELERATION ,
                  ps_WETGRIPINDEX ,
                  ps_COMMENTS ,
                  pi_WETGRIPID ,
                  ls_OperatorId ,
                  SYSTIMESTAMP
                );
        
   EXCEPTION
        when li_ParametersAreNull then           
            ls_ErrorMsg:= sqlerrm || ' - WetGripDetail_Save. There is at least one parameters null.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.WetGripDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:= sqlerrm || ' - WetGripDetail_Save. There is one parameters is invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' testresults_crud.WetGripDetail_Save',
                AX_RECORDDATA    => 'ps_sku is parameters invalid..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);   
            raise_application_error (-20006,ls_ErrorMsg); 
            
         when others then            
              ls_ErrorMsg:=  sqlerrm || ' - WetGripDetail_Save. An error have ocurred.(when others)';
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' testresults_crud.WetGripDetail_Save',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
               
   end WetGripDetail_Save;
   
   
  
   Function GetMeasureID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number) return Number as
  li_MeasureID  MeasureHdr.MEASUREID%TYPE;
  li_Total number;
  begin
        
        SELECT Count(*) into li_Total  
        FROM  Certificate ce 
                     inner join MeasureHdr m on
                         ce.certificateid = m.certificateid and
                         ce.certificationtypeid = m.certificationtypeid
        WHERE            
              M.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber); 
        
        if li_Total > 0 then 
        
              SELECT NVL(MeasureID,0) INTO li_MeasureID
               FROM  Certificate ce 
                     inner join MeasureHdr m on
                         ce.certificateid = m.certificateid and
                         ce.certificationtypeid = m.certificationtypeid
               WHERE    
                    M.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
                    ce.CERTIFICATENUMBER   = ps_CertificateNumber;  
        else
            li_MeasureID:=0;
        end if ;
        
        Return li_MeasureID;
        
  end GetMeasureID;
  
  Function GetTreadWearID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number as
  li_TreadWearID  plungerhdr.plungerid%type;
  begin
        SELECT TreadWearID INTO li_TreadWearID
        FROM  Certificate ce  inner join treadwearhdr m on
                    ce.certificateid = m.certificateid and
                    ce.certificationtypeid = m.certificationtypeid  
        WHERE 
              m.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber);
              
        Return li_TreadWearID;
        
   EXCEPTION
        when NO_DATA_FOUND THEN     
                 Return -1;
                raise_application_error (-20100,'GetPlungerID: No data was found with those search parameters....');
  end GetTreadWearID;
  
 Function GetPlungerID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number as
  li_PlungerId  plungerhdr.plungerid%type;
  begin
        SELECT PlungerID INTO li_PlungerId
        FROM  Certificate ce inner join plungerhdr p on
                  ce.certificateid = p.certificateid and
                  ce.certificationtypeid = p.certificationtypeid  
        WHERE 
              p.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber);
              
        Return li_PlungerId;
        
   EXCEPTION
        when NO_DATA_FOUND THEN     
                 Return -1;
                raise_application_error (-20100,'GetPlungerID: No data was found with those search parameters....');
  end GetPlungerID;
  
   Function GetEnduranceID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number as
  li_EnduranceID  EnduranceHdr.enduranceid%TYPE;
  begin
        SELECT EnduranceID INTO li_EnduranceID
        FROM  Certificate ce inner join ENDURANCEHdr e on
                  ce.certificateid = e.certificateid and
                  ce.certificationtypeid = e.certificationtypeid  
        WHERE 
              e.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber);
        
        Return li_EnduranceID;
        
   EXCEPTION
        when NO_DATA_FOUND THEN     
                 Return -1;
                raise_application_error (-20100,'GetEnduranceID: No data was found with those search parameters....');
  end GetEnduranceID;
  
  Function GetHighSpeedID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number ) return Number as
  li_HighSpeedID  highspeedhdr.highspeedid%TYPE;
  begin
        SELECT HighSpeedID INTO li_HighSpeedID
        FROM  Certificate ce inner join highspeedhdr h on
                  ce.certificateid = h.certificateid and
                  ce.certificationtypeid = h.certificationtypeid
        WHERE 
              h.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber);
        
        Return li_HighSpeedID;
        
   EXCEPTION
        when NO_DATA_FOUND THEN     
                 Return -1;
                raise_application_error (-20100,'GetHighSpeedID: No data was found with those search parameters....');
  end GetHighSpeedID;
  
  Function GetBeadUnseatID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number) return Number as
  li_BeadUnseatID  beadunseathdr.beadunseatid%TYPE;
  li_Total number;
  begin
        
        SELECT Count(*) into li_Total  
        FROM  Certificate ce inner join beadunseathdr bs on 
                   ce.certificateid = bs.certificateid and
                   ce.certificationtypeid = bs.certificationtypeid
        WHERE             
              bs.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber); 
        
        if li_Total > 0 then 
        
              SELECT NVL(BeadUnseatID,0) INTO li_BeadUnseatID
               FROM  Certificate ce inner join beadunseathdr bs on 
                   ce.certificateid = bs.certificateid and
                   ce.certificationtypeid = bs.certificationtypeid
              WHERE              
                    bs.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
                    Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber);
        else
            li_BeadUnseatID:=0;
        end if ;
        
        Return li_BeadUnseatID;
        
  end GetBeadUnseatID;
  
  function GetWetGripHDRID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number,pi_SKUId in number) return Number as
  li_WetGripID  WetGripHDR.WetGRIPID%TYPE;
  
  li_Total number;
  begin
        
        SELECT Count(*) into li_Total  
         FROM  Certificate ce inner join WetGripHDR W on
                   ce.certificateid = w.certificateid and
                   ce.certificationtypeid = w.certificationtypeid 
        WHERE            
              w.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber); 
        
        if li_Total > 0 then 
        
              SELECT NVL(WetGripID,0) INTO li_WetGripID
               FROM  Certificate ce inner join WetGripHDR W on
                   ce.certificateid = w.certificateid and
                   ce.certificationtypeid = w.certificationtypeid 
              WHERE            
                    w.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
                    Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber); 
        else
            li_WetGripID:=0;
        end if ;
        
        Return li_WetGripID;
        
  end GetWetGripHDRID;
  
  function GetSoundHDRID(ps_CertificateNumber	in VARCHAR2,pi_CertificationTypeId in number) return Number as
  li_SOUNDID  SOUNDHDR.SOUNDID%TYPE;
  
  li_Total number;
  begin
       
        SELECT Count(*) into li_Total  
        FROM  Certificate ce inner join SOUNDHDR s on
                 ce.certificateid = s.certificateid and
                 ce.certificationtypeid = s.certificationtypeid
        WHERE           
              s.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
              Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber); 
        
        if li_Total > 0 then 
        
              SELECT NVL(s.soundid,0) INTO li_SOUNDID
              FROM  Certificate ce inner join SOUNDHDR s on
                     ce.certificateid = s.certificateid and
                     ce.certificationtypeid = s.certificationtypeid
              WHERE           
                    s.CERTIFICATIONTYPEID = pi_CertificationTypeId AND
                    Lower(ce.CERTIFICATENUMBER)   = lower(ps_CertificateNumber); 
              
        else
            li_SOUNDID:=0;
        end if ;
        
        Return li_SOUNDID;
        
  end GetSoundHDRID;
  
  
  
  Function CheckIfProductExists(ps_SKU	in VARCHAR2,pi_SKUId in number) return Varchar2 as
   lc_Exist char:= 'n';
   li_totalProducts integer;   
   begin
        SELECT COUNT(1) INTO li_totalProducts
        FROM  PRODUCT p
        WHERE p.SKU   = PS_SKU And
              p.SKUId = pi_SKUId;
        
        if li_totalProducts > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
        
  end CheckIfProductExists;
  
   Function CheckIfEnduranceExists(pi_certificateid in Number,pi_CertificationTypeID in number) return Varchar2 as
   lc_Exist char:= 'n';
   li_totalEndurance integer;   
   begin
        SELECT COUNT(1) INTO li_totalEndurance
        FROM ENDURANCEHdr e 
        WHERE certificateid   = pi_certificateid  and
              certificationtypeid      =  pi_CertificationTypeID ;
              
        
        if li_totalEndurance > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
        
  end CheckIfEnduranceExists;
  
  Function CheckIfHighSpeedExists(pi_certificateid in Number,pi_CertificationTypeID in number) return Varchar2 as
   lc_Exist char:= 'n';
   li_totalHighSpeed integer;   
   begin
         SELECT COUNT(1) INTO li_totalHighSpeed
         FROM highspeedhdr h
         WHERE h.certificateid   = pi_certificateid  and
               h.certificationtypeid       =  pi_CertificationTypeID ;
      
        
        if li_totalHighSpeed > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
  end CheckIfHighSpeedExists;
  
  Function CheckIfMeasureExists(pi_CertificateID	in number,pi_CertificationTypeID in number) return Varchar2 as
   lc_Exist char:= 'n';
   li_totalMeasures number;   
   begin
        SELECT COUNT(1) INTO li_totalMeasures
        FROM  MeasureHdr m 
        WHERE m.certificationtypeid = pi_CertificationTypeID AND
              m.CertificateId = pi_CertificateID;   		   
        
        if li_totalMeasures > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
        
  end CheckIfMeasureExists;
  
 
 
  Function CheckIfBeadUnseatExists(pi_certificateid	in Number,pi_CertificationTypeID in number)  return Varchar2 as
   lc_Exist char:= 'n';
   li_totalBeadUnseat number;   
   begin
        SELECT COUNT(1) INTO li_totalBeadUnseat
        FROM   beadUnseatHdr bu 
        WHERE bu.certificationtypeid = pi_CertificationTypeID AND
              bu.certificateid   = pi_certificateid;  
       
        
        if li_totalBeadUnseat > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
        
  end CheckIfBeadUnseatExists;
  
  
 
  Function CheckIfTreadWearExists(pi_certificateid in Number,pi_CertificationTypeID in number)  return Varchar2 as
   lc_Exist char:= 'n';
   li_totalTreadWearid number;   
   begin
        SELECT COUNT(1) INTO li_totalTreadWearid
         FROM TreadWearHdr tw                 
        WHERE certificationtypeid = pi_CertificationTypeID AND
              certificateid   = pi_certificateid; 
       
        
        if li_totalTreadWearid > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
        
  end CheckIfTreadWearExists;
  
  
  
  Function CheckIfPlungerExists(pi_certificateid in Number,pi_CertificationTypeID in number)  return Varchar2 as
   lc_Exist char:= 'n';
   li_total number;   
   begin
        SELECT COUNT(1) INTO li_total
         FROM  PlungerHdr p
        WHERE p.certificationtypeid = pi_CertificationTypeID AND
              p.certificateid   = pi_certificateid; 
      
        
        if li_total > 0 THEN
            lc_Exist:='y';
        else
            lc_Exist:='n';
        end if;
        
        return lc_Exist;  
    end CheckIfPlungerExists;
    
 Function CheckIfSoundExixts(pi_certificateid in Number,pi_CertificationTypeID in number)  return Varchar2 as
       lc_Exist char:= 'n';
       li_total number;   
     begin
            SELECT COUNT(1) INTO li_total
             FROM SOUNDHDR h                 
            WHERE h.certificationtypeid = pi_CertificationTypeID AND
                  h.certificateid   = pi_certificateid; 
          
            
            if li_total > 0 THEN
                lc_Exist:='y';
            else
                lc_Exist:='n';
            end if;
            
            return lc_Exist;  
            
      end CheckIfSoundExixts;
    
 Function CheckIfWetGripExixts(pi_CertificateID	in Number,pi_CertificationTypeID in number)  return Varchar2 as
       lc_Exist char:= 'n';
       li_total number;   
     begin
            SELECT COUNT(1) INTO li_total
             FROM WetGripHdr h 
            WHERE h.certificationtypeid = pi_CertificationTypeID AND
                  CERTIFICATEID = pi_CertificateID ;
           
            if li_total > 0 THEN
                lc_Exist:='y';
            else
                lc_Exist:='n';
            end if;
            
            return lc_Exist;  
            
      end CheckIfWetGripExixts;
    

end testresults_crud;
/
