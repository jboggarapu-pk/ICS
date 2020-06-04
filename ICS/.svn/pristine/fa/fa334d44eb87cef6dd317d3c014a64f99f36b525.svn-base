 set serveroutput ON

DECLARE
  PC_MEASURECURSOR ICS.TESTRESULTS_CRUD.retCursor;
  PC_MEASUREDETAILCURSOR ICS.TESTRESULTS_CRUD.retCursor;
  PS_CERTIFICATIONTYPEID number;
  PS_SKU VARCHAR2(200);
  PI_SKUID number;
  PS_CERTIFICATENUMBER VARCHAR2(200);
  
  li_MEASUREID ICS.MeasureHDR.measureid%type;
  ls_PROJECTNUMBER ICS.MeasureHDR.projectnumber%type;
  li_TIRENUMBER ICS.MeasureHDR.projectnumber%type;
  ls_TESTSPEC ICS.MeasureHDR.TESTSPEC%type;
  ld_COMPLETIONDATE ICS.MeasureHDR.COMPLETIONDATE%type;
  li_INFLATIONPRESSURE ICS.MeasureHDR.INFLATIONPRESSURE%type;
  ls_MOLDDESIGN ICS.MeasureHDR.MOLDDESIGN%type;
  li_RIMWIDTH ICS.MeasureHDR.RIMWIDTH%type;
  ls_DOTSERIALNUMBER ICS.MeasureHDR.DOTSERIALNUMBER%type;
  li_DIAMETER ICS.MeasureHDR.DIAMETER%type;
  li_AVGSECTIONWIDTH ICS.MeasureHDR.AVGSECTIONWIDTH%type;
  li_AVGOVERALLWIDTH ICS.MeasureHDR.AVGOVERALLWIDTH%type;
  li_MAXOVERALLWIDTH ICS.MeasureHDR.MAXOVERALLWIDTH%type;
  li_SIZEFACTOR ICS.MeasureHDR.SIZEFACTOR%type;
  ld_MOUNTTIME ICS.MeasureHDR.MOUNTTIME%type;
  li_MOUNTTEMP ICS.MeasureHDR.MOUNTTEMP%type;               
  li_SKUID ICS.MEASUREHDR.SKUID%type;
  li_CERTIFICATIONTYPEID ICS.MeasureHDR.CERTIFICATIONTYPEID%type;
  ls_CERTIFICATENUMBER  ICS.MeasureHDR.CERTIFICATENUMBER%type;
   PI_CERTIFICATIONTYPEID ICS.certificationtype.certificationtypeid%type;
   
   
  li_MeasureID_Dtl ICS.MEASUREDTL.MEASUREID%type;
  li_OVERALLWIDTH ICS.MEASUREDTL.OVERALLWIDTH%type;
  li_SECTIONWIDTH ICS.MEASUREDTL.SECTIONWIDTH%type;
   
   v_Return NUMBER;
    ls_MeasureExists varchar2(1);
BEGIN 
  PS_CERTIFICATIONTYPEID := 1;
  PS_SKU := '36S-205-1279';
  PI_SKUID:=7;
  PS_CERTIFICATENUMBER := 'Ecert001';
  
   ls_MeasureExists :=ICS.TESTRESULTS_CRUD.CheckIfMeasureExists(PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER); 
   DBMS_OUTPUT.PUT_LINE('ls_MeasureExists = ' || ls_MeasureExists);
   
   v_Return :=  TESTRESULTS_CRUD.GETMEASUREID(
                                        PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER, 
                                        PI_CERTIFICATIONTYPEID => PS_CERTIFICATIONTYPEID,
                                        PI_SKUID =>PI_SKUID
                                      );
  
  DBMS_OUTPUT.PUT_LINE('v_Return = ' || v_Return);
  
  DBMS_OUTPUT.PUT_LINE('PI_SKUID = ' || PI_SKUID);
  DBMS_OUTPUT.PUT_LINE('ps_CertificateNumber = ' || PS_CERTIFICATENUMBER);
  DBMS_OUTPUT.PUT_LINE('pi_CertificationTypeID = ' || PS_CERTIFICATIONTYPEID);
  
  TESTRESULTS_CRUD.GETMEASURE(
    pc_MeasureCursor => PC_MEASURECURSOR,
    pc_MeasureDetailCursor => PC_MEASUREDETAILCURSOR,
    pi_SKUId => PI_SKUID,
    ps_CertificateNumber => PS_CERTIFICATENUMBER,
    pi_CertificationTypeID => PS_CERTIFICATIONTYPEID
  );

   if  PC_MEASURECURSOR is null then
           DBMS_OUTPUT.PUT_LINE('PC_MEASURECURSOR returned null' ); 
      else      
          LOOP       
                FETCH PC_MEASURECURSOR INTO   li_MEASUREID,
                                             ls_PROJECTNUMBER,
                                             li_TIRENUMBER,
                                             ls_TESTSPEC,
                                            ld_COMPLETIONDATE ,
                                            li_INFLATIONPRESSURE ,
                                            ls_MOLDDESIGN,
                                            li_RIMWIDTH,
                                            ls_DOTSERIALNUMBER,
                                            li_DIAMETER,
                                            li_AVGSECTIONWIDTH,
                                            li_AVGOVERALLWIDTH,
                                            li_MAXOVERALLWIDTH ,
                                            li_SIZEFACTOR ,
                                            ld_MOUNTTIME,
                                            li_MOUNTTEMP,         
                                            li_SKUID,
                                            li_CERTIFICATIONTYPEID,
                                            ls_CERTIFICATENUMBER   ;
                EXIT WHEN PC_MEASURECURSOR%NOTFOUND; 
                DBMS_OUTPUT.PUT_LINE(li_MEASUREID  || ' | ' || 
                                             ls_PROJECTNUMBER  || ' | ' || 
                                             li_TIRENUMBER  || ' | ' || 
                                             ls_TESTSPEC  || ' | ' || 
                                            ld_COMPLETIONDATE   || ' | ' || 
                                            li_INFLATIONPRESSURE   || ' | ' || 
                                            ls_MOLDDESIGN  || ' | ' || 
                                            li_RIMWIDTH  || ' | ' || 
                                            ls_DOTSERIALNUMBER  || ' | ' || 
                                            li_DIAMETER  || ' | ' || 
                                            li_AVGSECTIONWIDTH  || ' | ' || 
                                            li_AVGOVERALLWIDTH  || ' | ' || 
                                            li_MAXOVERALLWIDTH   || ' | ' || 
                                            li_SIZEFACTOR   || ' | ' || 
                                            ld_MOUNTTIME  || ' | ' || 
                                            li_MOUNTTEMP  || ' | ' ||          
                                            li_SKUID  || ' | ' || 
                                            li_CERTIFICATIONTYPEID  || ' | ' || 
                                            ls_CERTIFICATENUMBER ); 
            END LOOP; 
            CLOSE PC_MEASURECURSOR;
        
        end if;

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
        
    
END;


