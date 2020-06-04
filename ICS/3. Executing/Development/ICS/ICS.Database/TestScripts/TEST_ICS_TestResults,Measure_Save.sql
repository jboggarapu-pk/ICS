 set serveroutput ON
DECLARE
  PS_PROJECTNUMBER VARCHAR2(200);
  PI_TIRENUMBER NUMBER;
  PS_TESTSPEC VARCHAR2(200);
  PD_COMPLETIONDATE DATE;
  PI_INFLATIONPRESSURE NUMBER;
  PS_MOLDDESIGN VARCHAR2(200);
  PI_RIMWIDTH NUMBER;
  PS_DOTSERIALNUMBER VARCHAR2(200);
  PI_DIAMETER NUMBER;
  PI_AVGSECTIONWIDTH NUMBER;
  PI_AVGOVERALLWIDTH NUMBER;
  PI_MAXOVERALLWIDTH NUMBER;
  PI_SIZEFACTOR NUMBER;
  PD_MOUNTTIME DATE;
  PI_MOUNTTEMP NUMBER;
  PS_SKU VARCHAR2(200);
  PS_CERTIFICATIONNAME VARCHAR2(200);
  PS_CERTIFICATENUMBER VARCHAR2(200);
  PI_MEASUREID NUMBER;
BEGIN
  PS_PROJECTNUMBER := 'PROJNUM';
  PI_TIRENUMBER := 1;
  PS_TESTSPEC := 'TESTSPEC';
  PD_COMPLETIONDATE := sysdate;
  PI_INFLATIONPRESSURE := NULL;
  PS_MOLDDESIGN := NULL;
  PI_RIMWIDTH := NULL;
  PS_DOTSERIALNUMBER := NULL;
  PI_DIAMETER := NULL;
  PI_AVGSECTIONWIDTH := NULL;
  PI_AVGOVERALLWIDTH := NULL;
  PI_MAXOVERALLWIDTH := NULL;
  PI_SIZEFACTOR := NULL;
  PD_MOUNTTIME := NULL;
  PI_MOUNTTEMP := NULL;
  PS_SKU := '';
  PS_CERTIFICATIONNAME := '';
  PS_CERTIFICATENUMBER := '';
 --PS_SKU := '83W-206-4130';
 -- PS_CERTIFICATIONNAME := 'Emark';
  --PS_CERTIFICATENUMBER := 'Ecert83Wi';

  TESTRESULTS_CRUD.MEASURE_SAVE(
    PS_PROJECTNUMBER => PS_PROJECTNUMBER,
    PI_TIRENUMBER => PI_TIRENUMBER,
    PS_TESTSPEC => PS_TESTSPEC,
    PD_COMPLETIONDATE => PD_COMPLETIONDATE,
    PI_INFLATIONPRESSURE => PI_INFLATIONPRESSURE,
    PS_MOLDDESIGN => PS_MOLDDESIGN,
    PI_RIMWIDTH => PI_RIMWIDTH,
    PS_DOTSERIALNUMBER => PS_DOTSERIALNUMBER,
    PI_DIAMETER => PI_DIAMETER,
    PI_AVGSECTIONWIDTH => PI_AVGSECTIONWIDTH,
    PI_AVGOVERALLWIDTH => PI_AVGOVERALLWIDTH,
    PI_MAXOVERALLWIDTH => PI_MAXOVERALLWIDTH,
    PI_SIZEFACTOR => PI_SIZEFACTOR,
    PD_MOUNTTIME => PD_MOUNTTIME,
    PI_MOUNTTEMP => PI_MOUNTTEMP,
    PS_SKU => PS_SKU,
    PS_CERTIFICATIONNAME => PS_CERTIFICATIONNAME,
    PS_CERTIFICATENUMBER => PS_CERTIFICATENUMBER,
    PI_MEASUREID => PI_MEASUREID
  );
  DBMS_OUTPUT.PUT_LINE('PI_MEASUREID = ' || PI_MEASUREID);
END;