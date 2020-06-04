DECLARE
  PS_SKU VARCHAR2(200);
  PI_CERTIFICATIONID NUMBER;
  PS_CERTIFICATIONNUMBER VARCHAR2(200);
  PD_DATEASSIGNED DATE;
  PD_DATESUBMITED DATE;
  PD_DATEAPPROVED DATE;
  PS_ACTIVESTATUS VARCHAR2(200);
  PS_RENEWALREQUIRED CHAR(1);
  PS_IMARK_SUPPLEMENTALNUMBER VARCHAR2(200);
  PS_IMARK_SUPPLEMENTALREQUIRED VARCHAR2(200);
  PS_IMARK_EMARKREFERENCE VARCHAR2(200);
  PS_IMARK_FAMILY VARCHAR2(200);
BEGIN
  PS_SKU := 'W11-3456-32453';
  PI_CERTIFICATIONID := 5;
  PS_CERTIFICATIONNUMBER := 'PS_CERTIFICATIONNUMBER';
  PD_DATEASSIGNED := sysdate;
  PD_DATESUBMITED := sysdate;
  PD_DATEAPPROVED := sysdate;
  PS_ACTIVESTATUS := 'PS_ACTIVESTATUS';
  PS_RENEWALREQUIRED := 'y';
  PS_IMARK_SUPPLEMENTALNUMBER := 'SUPPLEMENTALNUMBER';
  PS_IMARK_SUPPLEMENTALREQUIRED := 'SUPPLEMENTALREQUIRED';
  PS_IMARK_EMARKREFERENCE := 'EMARKREFERENCE';
  PS_IMARK_FAMILY := 'PS_IMARK_FAMILY';

  CERTIFICATION_CRUD.CERTIFICATES_IMARK_INSERT(
    PS_SKU => PS_SKU,
    PI_CERTIFICATIONID => PI_CERTIFICATIONID,
    PS_CERTIFICATIONNUMBER => PS_CERTIFICATIONNUMBER,
    PD_DATEASSIGNED => PD_DATEASSIGNED,
    PD_DATESUBMITED => PD_DATESUBMITED,
    PD_DATEAPPROVED => PD_DATEAPPROVED,
    PS_ACTIVESTATUS => PS_ACTIVESTATUS,
    PS_RENEWALREQUIRED => PS_RENEWALREQUIRED,
    PS_IMARK_SUPPLEMENTALNUMBER => PS_IMARK_SUPPLEMENTALNUMBER,
    PS_IMARK_SUPPLEMENTALREQUIRED => PS_IMARK_SUPPLEMENTALREQUIRED,
    PS_IMARK_EMARKREFERENCE => PS_IMARK_EMARKREFERENCE,
    PS_IMARK_FAMILY => PS_IMARK_FAMILY
  );
END;
