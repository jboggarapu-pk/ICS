 set serveroutput ON
DECLARE

  PC_MEASURECURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_MEASUREDETAILCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_ENDURANCEHDRCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_ENDURANCEDTLCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  
  PI_CERTIFICATIONTYPEID NUMBER;
  PS_SKU VARCHAR2(200);
  PI_SKUID NUMBER;
  PS_CERTIFICATENUMBER VARCHAR2(200);
 
  /*
  PC_TREADWEARCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_TREADWEARDETAILCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_PLUNGERCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_PLUNGERDETAILCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_HIGHSPEEDCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_HIGHSPEEDDETAILCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_BEADUNSEATCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  PC_BEADUNSEATDETAILCURSOR ICS.TESTRESULTS_CRUD.retcursor;
  */ 
--Measure
MEASUREID      ICS.MEASUREHDR.MEASUREID%Type;
PROJECTNUMBER_M  ICS.measurehdr.projectnumber%Type;
TIRENUMBER_M     ICS.measurehdr.tirenumber%Type;
TESTSPEC_M       ICS.measurehdr.testspec%Type;
COMPLETIONDATE     ICS.measurehdr.completiondate%Type;
INFLATIONPRESSURE  ICS.measurehdr.inflationpressure%Type;
MOLDDESIGN         ICS.measurehdr.molddesign%Type;
RIMWIDTH           ICS.measurehdr.rimwidth%Type;
DOTSERIALNUMBER    ICS.measurehdr.dotserialnumber%Type;
DIAMETER           ICS.measurehdr.diameter%Type;
AVGSECTIONWIDTH    ICS.measurehdr.avgsectionwidth%Type;
AVGOVERALLWIDTH    ICS.measurehdr.avgoverallwidth%Type;
MAXOVERALLWIDTH    ICS.measurehdr.maxoverallwidth%Type;
SIZEFACTOR         ICS.measurehdr.sizefactor%Type;
MOUNTTIME          ICS.measurehdr.mounttime%Type;
MOUNTTEMP          ICS.measurehdr.mounttemp%Type;
CERTIFICATIONTYPEID ICS.measurehdr.certificationtypeid%Type;
CERTIFICATENUMBER   ICS.measurehdr.certificatenumber%Type;
SKUID               ICS.measurehdr.skuid%Type;
  
  li_MeasureID_Dtl ICS.MEASUREDTL.MEASUREID%type;
  li_OVERALLWIDTH ICS.MEASUREDTL.OVERALLWIDTH%type;
  li_SECTIONWIDTH ICS.MEASUREDTL.SECTIONWIDTH%type;
  
  
    --Endurance Header
ENDURANCEID         ICS.endurancehdr.enduranceid%type;
PROJECTNUMBER_EH    ICS.endurancehdr.projectnumber%type;
TIRENUMBER_EH       ICS.endurancehdr.tirenumber%type;
TESTSPEC_EH          ICS.endurancehdr.testspec%type;
COMPLETIONDATE_EH      ICS.endurancehdr.completiondate%type;
DOTSERIALNUMBER_EH     ICS.endurancehdr.dotserialnumber%type;
MFGWWYY             ICS.endurancehdr.mfgwwyy%type;
PRECONDSTARTDATE    ICS.endurancehdr.precondstartdate%type;
PRECONDSTARTTEMP    ICS.endurancehdr.precondstarttemp%type;
RIMDIAMETER         ICS.endurancehdr.rimdiameter%type;
RIMWIDTH_EH            ICS.endurancehdr.rimwidth%type;
PRECONDENDDATE      ICS.endurancehdr.precondenddate%type;
PRECONDENDTEMP      ICS.endurancehdr.precondendtemp%type;
INFLATIONPRESSURE_EH   ICS.endurancehdr.inflationpressure%type;
BEFOREDIAMETER      ICS.endurancehdr.beforediameter%type;
AFTERDIAMETER       ICS.endurancehdr.afterdiameter%type;
BEFOREINFLATION     ICS.endurancehdr.beforeinflation%type;
AFTERINFLATION      ICS.endurancehdr.afterinflation%type;
WHEELPOSITION       ICS.endurancehdr.wheelposition%type;
WHEELNUMBER         ICS.endurancehdr.wheelnumber%type;
FINALTEMP           ICS.endurancehdr.finaltemp%type;
FINALDISTANCE       ICS.endurancehdr.finaldistance%type;
FINALINFLATION      ICS.endurancehdr.finalinflation%type;
POSTCONDSTARTDATE   ICS.endurancehdr.postcondstartdate%type;
POSTCONDENDDATE     ICS.endurancehdr.postcondenddate%type;
POSTCONDENDTEMP     ICS.endurancehdr.postcondendtemp%type;
PASSYN              ICS.endurancehdr.passyn%type;
CERTIFICATIONTYPEID_EH ICS.endurancehdr.certificationtypeid%type;
CERTIFICATENUMBER_EH   ICS.endurancehdr.certificatenumber%type;
--EnduranceDetail
 STEP                ICS.ENDURANCEDTL.STEP%Type;
 TIME                ICS.ENDURANCEDTL.LOAD%Type;
 SPEED               ICS.ENDURANCEDTL.SPEED%Type;
 TOTMILES            ICS.ENDURANCEDTL.TOTMILES%Type;
 LOAD                ICS.ENDURANCEDTL.LOAD%Type;
 LOADPERCENT         ICS.ENDURANCEDTL.LOADPERCENT%Type;
 SETINFLATION        ICS.ENDURANCEDTL.SETINFLATION%Type;
 AMBTEMP             ICS.ENDURANCEDTL.AMBTEMP%Type;
 INFPRESSURE         ICS.ENDURANCEDTL.INFPRESSURE%Type;
 STEPCOMPLETIONDATE  ICS.ENDURANCEDTL.STEPCOMPLETIONDATE%Type;
 ENDURANCEID_DTL     ICS.ENDURANCEDTL.ENDURANCEID%Type;
 
BEGIN

  PI_CERTIFICATIONTYPEID := 1;
  PS_SKU := '36S-205-1279';
  PI_SKUID:=7;
  PS_CERTIFICATENUMBER := 'Ecert001';
  
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('------ Parameters Values -----------------------');
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('PS_CERTIFICATIONTYPEID:' || PI_CERTIFICATIONTYPEID);
  DBMS_OUTPUT.PUT_LINE('PS_SKU:' || PS_SKU);
  DBMS_OUTPUT.PUT_LINE('PS_CERTIFICATENUMBER:' || PS_CERTIFICATENUMBER);
  DBMS_OUTPUT.PUT_LINE('PI_SKUID:' || PI_SKUID);
  DBMS_OUTPUT.PUT_LINE('PS_CERTIFICATENUMBER:' || PS_CERTIFICATENUMBER);
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
  
  ICS.TESTRESULTS_CRUD.GETTESTRESULTS(
    PC_MEASURECURSOR => PC_MEASURECURSOR,
    PC_MEASUREDETAILCURSOR => PC_MEASUREDETAILCURSOR,
    PC_ENDURANCEHDRCURSOR => PC_ENDURANCEHDRCURSOR,
    PC_ENDURANCEDTLCURSOR => PC_ENDURANCEDTLCURSOR,
    PI_CERTIFICATIONTYPEID => PI_CERTIFICATIONTYPEID,
    PS_SKU => PS_SKU,
    PI_SKUID => PI_SKUID,
    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER
  );
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('----------Testing PC_MEASURECURSOR -------------');
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
   IF PC_MEASURECURSOR IS NULL THEN
    DBMS_OUTPUT.PUT_LINE('PC_MEASURECURSOR is null');
   ELSE
        LOOP              
              FETCH PC_MEASURECURSOR INTO MEASUREID,
                                      PROJECTNUMBER_m,
                                      TIRENUMBER_m,
                                      TESTSPEC_m,
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
                                      CERTIFICATIONTYPEID,
                                      CERTIFICATENUMBER,
                                      SKUID ;
              
              DBMS_OUTPUT.PUT_LINE(MEASUREID || '  | ' || 
                                    PROJECTNUMBER_m || '  | ' || 
                                    TIRENUMBER_m || '  | ' || 
                                    TESTSPEC_m || '  | ' || 
                                    COMPLETIONDATE || '  | ' || 
                                    INFLATIONPRESSURE || '  | ' || 
                                    MOLDDESIGN || '  | ' || 
                                    RIMWIDTH || '  | ' || 
                                    DOTSERIALNUMBER || '  | ' || 
                                    DIAMETER || '  | ' || 
                                    AVGSECTIONWIDTH || '  | ' || 
                                    AVGOVERALLWIDTH || '  | ' || 
                                    MAXOVERALLWIDTH || '  | ' || 
                                    SIZEFACTOR || '  | ' || 
                                    MOUNTTIME || '  | ' || 
                                    MOUNTTEMP || '  | ' ||   
                                    CERTIFICATIONTYPEID || '  | ' || 
                                    CERTIFICATENUMBER || '  | ' || 
                                    SKUID );
              EXIT WHEN PC_MEASURECURSOR%NOTFOUND;
        END LOOP;
        CLOSE PC_MEASURECURSOR;
   END IF;
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-------- End Testing PC_MEASURECURSOR ----------');
  DBMS_OUTPUT.PUT_LINE('------------------------------------------------');
       if  pc_MeasureDetailCursor is null then
           DBMS_OUTPUT.PUT_LINE('pc_MeasureDetailCursor returned null' ); 
      else      
          LOOP       
                FETCH pc_MeasureDetailCursor INTO   li_MeasureID_Dtl,li_OVERALLWIDTH,li_SECTIONWIDTH ;
                EXIT WHEN pc_MeasureDetailCursor%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(li_MeasureID_Dtl  || ' | ' || 
                                     li_OVERALLWIDTH   || ' | ' || 
                                     li_SECTIONWIDTH   ); 
            END LOOP; 
            CLOSE pc_MeasureDetailCursor;
        
        end if;
  DBMS_OUTPUT.PUT_LINE('-------------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-------- Testing PC_ENDURANCEHDRCURSOR ----------------');
  DBMS_OUTPUT.PUT_LINE('-------------------------------------------------------');
    if  PC_ENDURANCEHDRCURSOR is null then
           DBMS_OUTPUT.PUT_LINE('PC_ENDURANCEHDRCURSOR returned null' ); 
      else      
          LOOP       
                FETCH PC_ENDURANCEHDRCURSOR INTO  ENDURANCEID        ,
                                                  PROJECTNUMBER_EH     ,
                                                  TIRENUMBER_EH         ,
                                                  TESTSPEC_EH           ,
                                                  COMPLETIONDATE_EH     ,
                                                  DOTSERIALNUMBER_EH    ,
                                                  MFGWWYY            ,
                                                  PRECONDSTARTDATE   ,
                                                  PRECONDSTARTTEMP   ,
                                                  RIMDIAMETER        ,
                                                  RIMWIDTH_EH           ,
                                                  PRECONDENDDATE     ,
                                                  PRECONDENDTEMP     ,
                                                  INFLATIONPRESSURE_EH  ,
                                                  BEFOREDIAMETER     ,
                                                  AFTERDIAMETER      ,
                                                  BEFOREINFLATION    ,
                                                  AFTERINFLATION     ,
                                                  WHEELPOSITION      ,
                                                  WHEELNUMBER        ,
                                                  FINALTEMP          ,
                                                  FINALDISTANCE      ,
                                                  FINALINFLATION     ,
                                                  POSTCONDSTARTDATE  ,
                                                  POSTCONDENDDATE    ,
                                                  POSTCONDENDTEMP    ,
                                                  PASSYN             ,
                                                  CERTIFICATIONTYPEID_EH,
                                                  CERTIFICATENUMBER_EH    ;
                EXIT WHEN PC_ENDURANCEHDRCURSOR%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(ENDURANCEID        || ' | ' ||
                                    PROJECTNUMBER_EH       || ' | ' ||
                                    TIRENUMBER_EH          || ' | ' ||
                                    TESTSPEC_EH            || ' | ' ||
                                    COMPLETIONDATE_EH      || ' | ' ||
                                    DOTSERIALNUMBER_EH     || ' | ' ||
                                    MFGWWYY             || ' | ' ||
                                    PRECONDSTARTDATE    || ' | ' ||
                                    PRECONDSTARTTEMP    || ' | ' ||
                                    RIMDIAMETER         || ' | ' ||
                                    RIMWIDTH_EH            || ' | ' ||
                                    PRECONDENDDATE      || ' | ' ||
                                    PRECONDENDTEMP      || ' | ' ||
                                    INFLATIONPRESSURE_EH   || ' | ' ||
                                    BEFOREDIAMETER      || ' | ' ||
                                    AFTERDIAMETER       || ' | ' ||
                                    BEFOREINFLATION     || ' | ' ||
                                    AFTERINFLATION      || ' | ' ||
                                    WHEELPOSITION       || ' | ' ||
                                    WHEELNUMBER         || ' | ' ||
                                    FINALTEMP           || ' | ' ||
                                    FINALDISTANCE       || ' | ' ||
                                    FINALINFLATION      || ' | ' ||
                                    POSTCONDSTARTDATE   || ' | ' ||
                                    POSTCONDENDDATE     || ' | ' ||
                                    POSTCONDENDTEMP     || ' | ' ||
                                    PASSYN              || ' | ' ||
                                    CERTIFICATIONTYPEID_EH || ' | ' ||
                                    CERTIFICATENUMBER_EH  ); 
            END LOOP; 
            CLOSE PC_ENDURANCEHDRCURSOR;
        
        end if;
  DBMS_OUTPUT.PUT_LINE('-------------------------------------------------------');
  DBMS_OUTPUT.PUT_LINE('-------- Testing PC_ENDURANCEDTLCURSOR ----------------');
  DBMS_OUTPUT.PUT_LINE('-------------------------------------------------------');
    if  PC_ENDURANCEDTLCURSOR is null then
           DBMS_OUTPUT.PUT_LINE('PC_ENDURANCEDTLCURSOR returned null' ); 
      else      
          LOOP       
                FETCH PC_ENDURANCEDTLCURSOR INTO  STEP,
                                                   TIME,
                                                   SPEED,
                                                   TOTMILES,
                                                   LOAD,
                                                   LOADPERCENT,
                                                   SETINFLATION,
                                                   AMBTEMP,
                                                   INFPRESSURE,
                                                   STEPCOMPLETIONDATE,
                                                   ENDURANCEID_DTL   ;
                EXIT WHEN PC_ENDURANCEDTLCURSOR%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(STEP                || ' | ' ||
                                     TIME                || ' | ' ||
                                     SPEED               || ' | ' ||
                                     TOTMILES            || ' | ' ||
                                     LOAD                || ' | ' ||
                                     LOADPERCENT         || ' | ' ||
                                     SETINFLATION        || ' | ' ||
                                     AMBTEMP             || ' | ' ||
                                     INFPRESSURE         || ' | ' ||
                                     STEPCOMPLETIONDATE  || ' | ' ||
                                     ENDURANCEID_DTL ); 
            END LOOP; 
            CLOSE PC_ENDURANCEDTLCURSOR;
        
        end if;
  
END;

