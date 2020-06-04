--Creates the synonym
--Tables
CREATE or REPLACE  PUBLIC SYNONYM APP_MESSAGE FOR ICS.APP_MESSAGE;
CREATE or REPLACE  PUBLIC SYNONYM BRANDREGION FOR ICS.BRANDREGION;
CREATE or REPLACE  PUBLIC SYNONYM BEADUNSEATDTL FOR ICS.BEADUNSEATDTL;
CREATE or REPLACE  PUBLIC SYNONYM BEADUNSEATHDR FOR ICS.BEADUNSEATHDR;
CREATE or REPLACE  PUBLIC SYNONYM BRAND FOR ICS.BRAND;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATIONSEARCHTYPE FOR ICS.CERTIFICATIONSEARCHTYPE;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATE FOR ICS.CERTIFICATE;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATEDEFAULTVALUE FOR ICS.CERTIFICATEDEFAULTVALUE;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATETYPEDEFAULTVALUE FOR ICS.CERTIFICATETYPEDEFAULTVALUE;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATIONTYPE FOR ICS.CERTIFICATIONTYPE;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATION_AUDIT_LOG FOR ICS.CERTIFICATION_AUDIT_LOG;
CREATE or REPLACE  PUBLIC SYNONYM COUNTRY FOR ICS.COUNTRY;
CREATE or REPLACE  PUBLIC SYNONYM DEFAULTFIELD FOR ICS.DEFAULTFIELD;
CREATE or REPLACE  PUBLIC SYNONYM ENDURANCEDTL FOR ICS.ENDURANCEDTL;
CREATE or REPLACE  PUBLIC SYNONYM ENDURANCEHDR FOR ICS.ENDURANCEHDR;
CREATE or REPLACE  PUBLIC SYNONYM HIGHSPEEDDTL FOR ICS.HIGHSPEEDDTL;
CREATE or REPLACE  PUBLIC SYNONYM HIGHSPEEDHDR FOR ICS.HIGHSPEEDHDR;
CREATE or REPLACE  PUBLIC SYNONYM MEASUREDTL FOR ICS.MEASUREDTL;
CREATE or REPLACE  PUBLIC SYNONYM MEASUREHDR FOR ICS.MEASUREHDR;
CREATE or REPLACE  PUBLIC SYNONYM NPR_MASTER FOR ICS.NPR_MASTER;
CREATE or REPLACE  PUBLIC SYNONYM PLUNGERDTL FOR ICS.PLUNGERDTL;
CREATE or REPLACE  PUBLIC SYNONYM PLUNGERHDR FOR ICS.PLUNGERHDR;
CREATE or REPLACE  PUBLIC SYNONYM PRODUCT FOR ICS.PRODUCT;
CREATE or REPLACE  PUBLIC SYNONYM PRODUCTCERTIFICATE FOR ICS.PRODUCTCERTIFICATE;
CREATE or REPLACE  PUBLIC SYNONYM PRODUCTCOUNTRY FOR ICS.PRODUCTCOUNTRY;
CREATE or REPLACE  PUBLIC SYNONYM REGION FOR ICS.REGION;
CREATE or REPLACE  PUBLIC SYNONYM SKUMAIN FOR ICS.SKUMAIN;
CREATE or REPLACE  PUBLIC SYNONYM SEARCHTYPE FOR ICS.SEARCHTYPE;
CREATE or REPLACE  PUBLIC SYNONYM SOUNDDETAIL FOR ICS.SOUNDDETAIL;
CREATE or REPLACE  PUBLIC SYNONYM SOUNDHDR FOR ICS.SOUNDHDR;
CREATE or REPLACE  PUBLIC SYNONYM SPEEDTESTDETAIL FOR ICS.SPEEDTESTDETAIL;
CREATE or REPLACE  PUBLIC SYNONYM TIRETYPE FOR ICS.TIRETYPE;
CREATE or REPLACE  PUBLIC SYNONYM TREADWEARDTL FOR ICS.TREADWEARDTL;
CREATE or REPLACE  PUBLIC SYNONYM TREADWEARHDR FOR ICS.TREADWEARHDR;
CREATE or REPLACE  PUBLIC SYNONYM WETGRIPDETAIL FOR ICS.WETGRIPDETAIL;
CREATE or REPLACE  PUBLIC SYNONYM WETGRIPHDR FOR ICS.WETGRIPHDR;
CREATE or REPLACE  PUBLIC SYNONYM CUSTOMER FOR ICS.CUSTOMER;
--Sequences
CREATE or REPLACE  PUBLIC SYNONYM APP_MESSAGE_SEQ FOR ICS.APP_MESSAGE_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM BEADUNSEATID_SEQ FOR ICS.BEADUNSEATID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATIONID_SEQ FOR ICS.CERTIFICATIONID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM CHANGELOGID_SEQ FOR ICS.CHANGELOGID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM COUNTRYID_SEQ FOR ICS.COUNTRYID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM ENDURANCEID_SEQ FOR ICS.ENDURANCEID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM HIGHSPEEDID_SEQ FOR ICS.HIGHSPEEDID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM MEASUREID_SEQ FOR ICS.MEASUREID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM NPRID_SEQ FOR ICS.NPRID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM PLUNGERID_SEQ FOR ICS.PLUNGERID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM SKUID_SEQ FOR ICS.SKUID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM REGIONID_SEQ FOR ICS.REGIONID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM SEARCHTYPEID_SEQ FOR ICS.SEARCHTYPEID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM TREADWEARID_SEQ FOR ICS.TREADWEARID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM SOUNDID_SEQ FOR ICS.SOUNDID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM WETGRIPID_SEQ FOR ICS.WETGRIPID_SEQ;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATEID_SEQ  FOR ICS.CERTIFICATEID_SEQ;
--Views
CREATE or REPLACE  PUBLIC SYNONYM BRAND_VIEW FOR ICS.BRAND_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATE_VIEW FOR ICS.CERTIFICATE_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM DEFAULTVALUES_VIEW FOR ICS.DEFAULTVALUES_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM EMARKCERTIFICATIONREPORT_VIEW FOR ICS.EMARKCERTIFICATIONREPORT_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM IMARKCERTIFICATE_VIEW FOR ICS.IMARKCERTIFICATE_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM PRODUCT_MINUS_SKUMAIN_VIEW FOR ICS.PRODUCT_MINUS_SKUMAIN_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM PRODUCTDATA_REPORT_VIEW FOR ICS.PRODUCTDATA_REPORT_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM SKUMAIN_MINUS_PRODUCT_VIEW FOR ICS.SKUMAIN_MINUS_PRODUCT_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM SKUMAIN_PRODUCT_VIEW FOR ICS.SKUMAIN_PRODUCT_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM SKUMAINPRODUCT_LATEST_VIEW FOR ICS.SKUMAINPRODUCT_LATEST_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM TESTREFERENCE_VIEW FOR ICS.TESTREFERENCE_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM TRACEABILITY_VIEW FOR ICS.TRACEABILITY_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM SKULIST_VIEW FOR ICS.SKULIST_VIEW;
CREATE or REPLACE  PUBLIC SYNONYM PRODBRANDLIST_VIEW FOR ICS.PRODBRANDLIST_VIEW;

CREATE OR REPLACE PUBLIC SYNONYM "EMARKECE117_VIEW" FOR "ICS"."EMARKECE117_VIEW";

CREATE or REPLACE  PUBLIC SYNONYM EXCEPTIONREPORT FOR ICS.EXCEPTIONREPORT;

--gives the grants to objects
grant select,insert,update, delete ON  APP_MESSAGE to ICS_PROCS;
grant select,insert,update, delete ON  BRANDREGION to ICS_PROCS;
grant select,insert,update, delete ON  BEADUNSEATDTL to ICS_PROCS;
grant select,insert,update, delete ON  BEADUNSEATHDR to ICS_PROCS;
grant select,insert,update, delete ON  BRAND to ICS_PROCS;
grant select,insert,update, delete ON  CERTIFICATIONSEARCHTYPE to ICS_PROCS;
grant select,insert,update, delete ON  CERTIFICATE to ICS_PROCS;
grant select,insert,update, delete ON  CERTIFICATEDEFAULTVALUE to ICS_PROCS;
grant select,insert,update, delete ON  CERTIFICATETYPEDEFAULTVALUE to ICS_PROCS;
grant select,insert,update, delete ON  CERTIFICATIONTYPE to ICS_PROCS;
grant select,insert,update, delete ON  CERTIFICATION_AUDIT_LOG to ICS_PROCS;
grant select,insert,update, delete ON  COUNTRY to ICS_PROCS;
grant select,insert,update, delete ON  DEFAULTFIELD to ICS_PROCS;
grant select,insert,update, delete ON  ENDURANCEDTL to ICS_PROCS;
grant select,insert,update, delete ON  ENDURANCEHDR to ICS_PROCS;
grant select,insert,update, delete ON  HIGHSPEEDDTL to ICS_PROCS;
grant select,insert,update, delete ON  HIGHSPEEDHDR to ICS_PROCS;
grant select,insert,update, delete ON  MEASUREDTL to ICS_PROCS;
grant select,insert,update, delete ON  MEASUREHDR to ICS_PROCS;
grant select,insert,update, delete ON  NPR_MASTER to ICS_PROCS;
grant select,insert,update, delete ON  PLUNGERDTL to ICS_PROCS;
grant select,insert,update, delete ON  PLUNGERHDR to ICS_PROCS;
grant select,insert,update, delete ON  PRODUCT to ICS_PROCS;
grant select,insert,update, delete ON  PRODUCTCERTIFICATE to ICS_PROCS;
grant select,insert,update, delete ON  PRODUCTCOUNTRY to ICS_PROCS;
grant select,insert,update, delete ON  REGION to ICS_PROCS;
grant select,insert,update, delete ON  SKUMAIN to ICS_PROCS;
grant select,insert,update, delete ON  SEARCHTYPE to ICS_PROCS;
grant select,insert,update, delete ON  SOUNDDETAIL to ICS_PROCS;
grant select,insert,update, delete ON  SOUNDHDR to ICS_PROCS;
grant select,insert,update, delete ON  SPEEDTESTDETAIL to ICS_PROCS;
grant select,insert,update, delete ON  TIRETYPE to ICS_PROCS;
grant select,insert,update, delete ON  TREADWEARDTL to ICS_PROCS;
grant select,insert,update, delete ON  TREADWEARHDR to ICS_PROCS;
grant select,insert,update, delete ON  WETGRIPDETAIL to ICS_PROCS;
grant select,insert,update, delete ON  WETGRIPHDR to ICS_PROCS;

grant select,insert,update, delete ON  EXCEPTIONREPORT to ICS_PROCS;
grant select,insert,update, delete ON  CUSTOMER to ICS_PROCS;

grant select ON  APP_MESSAGE_SEQ to ICS_PROCS;
grant select ON  BEADUNSEATID_SEQ to ICS_PROCS;
grant select ON  CERTIFICATIONID_SEQ to ICS_PROCS;
grant select ON  CHANGELOGID_SEQ to ICS_PROCS;
grant select ON  COUNTRYID_SEQ to ICS_PROCS;
grant select ON  ENDURANCEID_SEQ to ICS_PROCS;
grant select ON  HIGHSPEEDID_SEQ to ICS_PROCS;
grant select ON  MEASUREID_SEQ to ICS_PROCS;
grant select ON  NPRID_SEQ to ICS_PROCS;
grant select ON  PLUNGERID_SEQ to ICS_PROCS;
grant select ON  SKUID_SEQ to ICS_PROCS;
grant select ON  REGIONID_SEQ to ICS_PROCS;
grant select ON  SEARCHTYPEID_SEQ to ICS_PROCS;
grant select ON  TREADWEARID_SEQ to ICS_PROCS;
grant select ON  SOUNDID_SEQ to ICS_PROCS;
grant select ON  WETGRIPID_SEQ to ICS_PROCS;
grant select ON  CERTIFICATEID_SEQ to ICS_PROCS;

GRANT SELECT ON  BRAND_VIEW to ICS_PROCS;


GRANT SELECT ON CERTIFICATE_VIEW to ICS_PROCS;
GRANT SELECT ON DEFAULTVALUES_VIEW to ICS_PROCS;
GRANT SELECT ON EMARKCERTIFICATIONREPORT_VIEW to ICS_PROCS;
GRANT SELECT ON IMARKCERTIFICATE_VIEW to ICS_PROCS;
GRANT SELECT ON PRODUCT_MINUS_SKUMAIN_VIEW to ICS_PROCS;
GRANT SELECT ON PRODUCTDATA_REPORT_VIEW to ICS_PROCS;
GRANT SELECT ON SKUMAIN_MINUS_PRODUCT_VIEW to ICS_PROCS;
GRANT SELECT ON SKUMAIN_PRODUCT_VIEW to ICS_PROCS;
GRANT SELECT ON SKUMAINPRODUCT_LATEST_VIEW to ICS_PROCS;
GRANT SELECT ON TESTREFERENCE_VIEW to ICS_PROCS;
GRANT SELECT ON TRACEABILITY_VIEW to ICS_PROCS;
GRANT SELECT ON SKULIST_VIEW to ICS_PROCS;

GRANT SELECT ON PRODBRANDLIST_VIEW to ICS_PROCS;

GRANT SELECT ON EMARKECE117_VIEW to ICS_PROCS;

set serveroutput on format wrapped;
begin
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
    DBMS_OUTPUT.put_line('Creating the synonim to the procs and giving the grants');
    DBMS_OUTPUT.put_line('-------------------------------------------------------------------------');
end;
/
CREATE or REPLACE  PUBLIC SYNONYM APP_MESSAGE_OPERATIONS FOR ICS_Procs.APP_MESSAGE_OPERATIONS;
CREATE or REPLACE  PUBLIC SYNONYM CERTIFICATION_CRUD FOR ICS_Procs.CERTIFICATION_CRUD;
CREATE or REPLACE  PUBLIC SYNONYM ICS_COMMON_FUNCTIONS FOR ICS_Procs.ICS_COMMON_FUNCTIONS;
CREATE or REPLACE  PUBLIC SYNONYM ICS_CRUD FOR ICS_Procs.ICS_CRUD;
CREATE or REPLACE  PUBLIC SYNONYM REPORTS_PACKAGE FOR ICS_Procs.REPORTS_PACKAGE;
CREATE or REPLACE  PUBLIC SYNONYM TESTRESULTS_CRUD FOR ICS_Procs.TESTRESULTS_CRUD;

commit;