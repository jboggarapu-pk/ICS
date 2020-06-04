set serveroutput ON
DECLARE
  PS_SKU VARCHAR2(200);
  PS_CERTIFICATIONNAME VARCHAR2(200);
  PS_CERTIFICATIONNUMBER VARCHAR2(200);
  PD_DATEASSIGNED DATE;
  PD_DATESUBMITED DATE;
  PD_DATEAPPROVED DATE;
  PS_ACTIVESTATUS CHAR(200);
  PS_EMARK_SUPPLEMENTALREQUIRED VARCHAR2(200);
  PS_EMARK_SUPPLEMENTALNUMBER VARCHAR2(200);
  PS_EMARK_JOBREPORTNUMBER VARCHAR2(200);
  PS_EMARK_EXTENTION VARCHAR2(200);
  PS_EMARK_SUPPLEMENTALMOLD VARCHAR2(200);
BEGIN
  PS_SKU :='80V-0582-19507';
  PS_CERTIFICATIONNAME := 'Emark';
  PS_CERTIFICATIONNUMBER := NULL;
  PD_DATEASSIGNED := NULL;
  PD_DATESUBMITED := NULL;
  PD_DATEAPPROVED := NULL;
  PS_ACTIVESTATUS := NULL;
  PS_EMARK_SUPPLEMENTALREQUIRED := NULL;
  PS_EMARK_SUPPLEMENTALNUMBER := NULL;
  PS_EMARK_JOBREPORTNUMBER := NULL;
  PS_EMARK_EXTENTION := NULL;
  PS_EMARK_SUPPLEMENTALMOLD := 'mold';

  CERTIFICATION_CRUD.CERTIFICATES_EMARK_SAVE(
    PS_SKU => PS_SKU,
    PS_CERTIFICATIONNAME => PS_CERTIFICATIONNAME,
    PS_CERTIFICATIONNUMBER => PS_CERTIFICATIONNUMBER,
    PD_DATEASSIGNED => PD_DATEASSIGNED,
    PD_DATESUBMITED => PD_DATESUBMITED,
    PD_DATEAPPROVED => PD_DATEAPPROVED,
    PS_ACTIVESTATUS => PS_ACTIVESTATUS,
    PS_EMARK_SUPPLEMENTALREQUIRED => PS_EMARK_SUPPLEMENTALREQUIRED,
    PS_EMARK_SUPPLEMENTALNUMBER => PS_EMARK_SUPPLEMENTALNUMBER,
    PS_EMARK_JOBREPORTNUMBER => PS_EMARK_JOBREPORTNUMBER,
    PS_EMARK_EXTENTION => PS_EMARK_EXTENTION,
    PS_EMARK_SUPPLEMENTALMOLD => PS_EMARK_SUPPLEMENTALMOLD
  );
  commit;
END;
 

 