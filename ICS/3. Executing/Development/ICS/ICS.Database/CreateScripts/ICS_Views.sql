 CREATE OR REPLACE FORCE VIEW  "BRAND_VIEW" ("BRANDCODE", "BRANDNAME", "CERTIFICATENUMBER","EXTENSION_EN")
AS
   SELECT Distinct(B.BRANDCODE),
            B.BRANDNAME,
            CE.CERTIFICATENUMBER, 
             CE.EXTENSION_EN
   FROM  BRAND B
           INNER JOIN  PRODUCT P	ON 
                   B.BRANDCODE = P.BRANDCODE
           INNER JOIN  PRODUCTCERTIFICATE pe on
                   p.skuid = pe.skuid
           inner join  CERTIFICATE CE on
                   pe.CERTIFICATEID   = ce.CERTIFICATEID and
                   pe.certificationtypeid = ce.certificationtypeid;

 CREATE OR REPLACE VIEW  "SKULIST_VIEW"  as
   SELECT   B.BRANDCODE,
            B.BRANDNAME,
            CE.CERTIFICATENUMBER, 
            CE.EXTENSION_EN,
            p.Skuid,
            p.SKU,
            p.SizeStamp
   FROM  BRAND B
           INNER JOIN  PRODUCT P	ON 
                   B.BRANDCODE = P.BRANDCODE
           INNER JOIN  PRODUCTCERTIFICATE pe on
                   p.skuid = pe.skuid
           inner join  CERTIFICATE CE on
                   pe.CERTIFICATEID   = ce.CERTIFICATEID and 
                   pe.certificationtypeid = ce.certificationtypeid;                   
                   
  
	   
 CREATE OR REPLACE VIEW  "CERTIFICATE_VIEW"  AS 
  SELECT  PE.SKUID,
        ce.CERTIFICATeID,
        ce.CERTIFICATIONTYPEID,
        ct.certificationtypename,
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
        BATCHNUMBER_G      
FROM PRODUCT P 
            INNER JOIN  PRODUCTCERTIFICATE PE ON
                   P.SKUID = PE.SKUID 
            INNER JOIN  CERTIFICATE CE ON 
                   pe.CERTIFICATEID   = ce.CERTIFICATEID And
                   pe.certificationtypeid = ce.certificationtypeid
            INNER JOIN  CERTIFICATIONTYPE CT ON
                   CE.CERTIFICATIONTYPEID = ct.certificationtypeid;	   
	   
CREATE OR REPLACE FORCE VIEW  "DEFAULTVALUES_VIEW" ("FIELDID", "CERTIFICATIONTYPEID", "CERTIFICATENUMBER", "FIELDNAME", "FIELDVALUE")
AS
  SELECT df.fieldid,
		cdf.certificationtypeid,
		ce.certificatenumber,
		df.fieldname,
		cdf.fieldvalue
  FROM  certificate ce
		INNER JOIN  certificationtype cet
				ON ce.certificationtypeid = cet.certificationtypeid
		INNER JOIN  DEFAULTFIELD df
				ON cet.certificationtypeid = df.certificationtypeid
		INNER JOIN  certificatetypedefaultvalue ctdf
				ON df.fieldid              = ctdf.fieldid
					AND df.certificationtypeid = ctdf.certificationtypeid
		LEFT JOIN  CERTIFICATEDEFAULTVALUE cdf
				ON df.fieldid              = cdf.fieldid
					AND df.certificationtypeid = cdf.certificationtypeid
					AND ce.certificateid   = cdf.certificateid;	   
	   
CREATE or REPLACE VIEW  EMARKCERTIFICATIONREPORT_VIEW AS SELECT 
    PE.SKUID,
    CE.ACTIVESTATUS,
    CE.CERTIFICATIONTYPEID,
    CE.CERTIFICATENUMBER,
    DATESUBMITED,
    DATEASSIGNED_EGI,
    DATEAPPROVED_CEGI, 
    SUPPLEMENTALREQUIRED_EI,
    SUPPLEMENTALNUMBER_EI,
    JOBREPORTNUMBER_CEN,
    EXTENSION_EN,
    SupplementalExtension_EN,
    SUPPLEMENTALMOLDSTAMPING_E,
    SUPPLEMENTALDATEASSIGNED_E,
    SUPPLEMENTALDATESUBMITTED_E,
    SUPPLEMENTALDATEAPPROVED_E, 
    'Not defiened yet' as WheelTestReference,
    'Not defiened yet' as NoiseTestReference,
    'Not defiened yet' as WGTestReference,
    'Not defiened yet' as RRTestReference
FROM  PRODUCTCERTIFICATE PE INNER JOIN   CERTIFICATE ce ON
             pe.CERTIFICATEID   = ce.CERTIFICATEID and
             PE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID             
WHERE ce.certificationtypeid=1 AND
      ce.activestatus = 'y';      	   
	   
	   
CREATE OR REPLACE FORCE VIEW  "IMARKCERTIFICATE_VIEW" ("FAMILY", "SKU", "EMARKREFERENCE", "SIZESTAMP", "BRANDNAME", "SINGLOADINDEX", "DUALLOADINDEX", "SPEEDRATING", "DATESUBMITED", "DATEAPPROVED", "DISCONTINUEDDATE") AS 
  SELECT   
           ce.family_i as Family,
           p.SKU,
           ce.emarkreference_i as EmarkReference,
           p.SizeStamp,
           b.BrandName,
           p.SINGLOADINDEX,
           p.DUALLOADINDEX,
           p.SpeedRating,
           ce.DateSubmited,
           ce.dateapproved_cegi as DateApproved,
           p.DiscontinuedDate
  FROM  BRAND B 
         INNER JOIN  PRODUCT P ON  
               b.brandcode = p.brandcode
         INNER JOIN  PRODUCTCERTIFICATE PCE ON        
               p.skuid = PCE.skuid
          INNER JOIN  CERTIFICATE CE ON 
               pce.CERTIFICATEID   = ce.CERTIFICATEID and
               pce.certificationtypeid = ce.certificationtypeid
  WHERE CE.CERTIFICATIONTYPEID = 4;
	               
	               
 CREATE OR REPLACE FORCE VIEW  "PRODUCT_MINUS_SKUMAIN_VIEW" ("SKU", "SIZESTAMP", "BRANDCODE") AS 
  select SKU, SIZESTAMP, BRANDCODE 
  from   product
  Minus
  select SKU, SIZESTAMP, BRANDCODE 
  from   skumain;
  
CREATE or REPLACE VIEW PRODUCTDATA_REPORT_VIEW AS 
SELECT
      p.SKUID,
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
      ce.certificatenumber,
      ce.SupplementalExtension_EN,
      ce.extension_en
FROM  product p
    INNER JOIN  ProductCertificate pce on
              p.skuid = pce.skuid
    INNER JOIN  certificate ce On
              pce.CERTIFICATEID   = ce.CERTIFICATEID and
              pce.certificationtypeid = ce.certificationtypeid;

  

CREATE OR REPLACE FORCE VIEW  "SKUMAIN_MINUS_PRODUCT_VIEW" ("SKU", "SIZESTAMP", "BRANDCODE") AS 
  select SKU, SIZESTAMP, BRANDCODE 
  from  skumain
  minus
  select SKU, SIZESTAMP, BRANDCODE 
  from  product;

  CREATE OR REPLACE FORCE VIEW  "SKUMAIN_PRODUCT_VIEW" ("SKU", "SIZESTAMP", "BRANDCODE") AS 
  select sm.sku,sm.sizestamp,sm.brandcode
from  SKUMain sm
union
select p.sku,p.sizestamp,p.brandcode
from  product p
order by 1;
 
CREATE OR REPLACE FORCE VIEW  "SKUMAINPRODUCT_LATEST_VIEW" ("SKUID", "SKU", "SIZESTAMP", "BRANDCODE") AS 
  SELECT NVL(Max(p2.skuid),0) SKUID, sp.SKU, sp.SIZESTAMP, sp.BRANDCODE
  FROM  SKUMAIN_PRODUCT_VIEW sp 
           left join  product p2 on 
                       sp.brandcode = p2.brandcode and
                       sp.sku       = p2.sku 
   GROUP BY sp.SKU, sp.SIZESTAMP, sp.BRANDCODE
   order by sp.SKU;
  
  CREATE OR REPLACE FORCE VIEW  "TESTREFERENCE_VIEW" ("CERTIFICATENUMBER", "SKUID", "MEASUREMENT_TESTREFERENCE", "PLUNGER_TESTREFERENCE", "BEADUNSEAT_TESTREFERENCE", "ENDURANCE_TESTREFERENCE", "HIGHSPEED_TESTREFERENCE", "LAB_TESTREFERENCE", "WHEEL_TESTREFERENCE", "NOISE_TESTREFERENCE", "WG_TESTREFERENCE", "RR_TESTREFERENCE") AS 
  SELECT  
        ce.certificatenumber ,
        pce.skuid,
        m.testspec        AS MEASUREMENT_TESTREFERENCE,
        pl.testspec       AS PLUNGER_TESTREFERENCE,
        b.testspec        AS BEADUNSEAT_TESTREFERENCE,
        e.testspec        AS ENDURANCE_TESTREFERENCE,
        h.testspec        AS HIGHSPEED_TESTREFERENCE,
        'Not Defined yet' AS LAB_TESTREFERENCE,
        'Not Defined yet' AS WHEEL_TESTREFERENCE,
        'Not Defined yet' AS NOISE_TESTREFERENCE,
        'Not Defined yet' AS WG_TESTREFERENCE,
        'Not Defined yet' AS RR_TESTREFERENCE
  FROM  ProductCertificate pce 
          inner join   certificate ce on
               pce.CERTIFICATEID   = ce.CERTIFICATEID and
               pce.certificationtypeid = ce.certificationtypeid
          LEFT JOIN  measurehdr m   ON 
               ce.CERTIFICATEID    = m.CERTIFICATEID
                AND ce.certificationtypeid = m.certificationtypeid               
          LEFT JOIN  plungerhdr pl  ON 
                ce.CERTIFICATEID    = pl.CERTIFICATEID
                AND ce.certificationtypeid = pl.certificationtypeid               
          LEFT JOIN  beadunseathdr b  ON 
                ce.CERTIFICATEID    = b.CERTIFICATEID
                AND ce.certificationtypeid = b.certificationtypeid               
          LEFT JOIN  endurancehdr e  ON 
                ce.CERTIFICATEID   = e.CERTIFICATEID
                AND ce.certificationtypeid = e.certificationtypeid               
          LEFT JOIN  highspeedhdr h  ON 
                ce.CERTIFICATEID    = h.CERTIFICATEID
                AND ce.certificationtypeid = h.certificationtypeid;


CREATE OR REPLACE VIEW TRACEABILITY_VIEW AS 
 SELECT 
        Distinct(ce.CERTIFICATENUMBER),
        ce.CERTIFICATIONTYPEID,
        ct.certificationtypename,
        p.sku,  
        DATESUBMITED, 
        DATEASSIGNED_EGI,
        DATEAPPROVED_CEGI,
        SUPPLEMENTALDATEASSIGNED_E,
        SUPPLEMENTALDATESUBMITTED_E,
        SUPPLEMENTALDATEAPPROVED_E, 
         ( Case
               WHEN ce.CERTIFICATENUMBER  is not null  THEN 'Y'
               WHEN ce.CERTIFICATENUMBER  is null  THEN 'N'
          END ) as CertificateRequested
 FROM CERTIFICATE ce inner join certificationtype ct on 
          ce.certificationtypeid = ct.certificationtypeid
          inner join productCertificate pce on 
                ce.CERTIFICATEID = pce.CERTIFICATEID and
                ce.certificationtypeid = pce.certificationtypeid
         inner join product p on 
                p.skuid = pce.skuid
         inner join productcountry pc on 
                p.skuid = pc.skuid;
                
 CREATE OR REPLACE VIEW EMARKECE117_VIEW AS 
  SELECT            
         CERTIFICATENUMBER,
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
        MAX(DECODE(fieldid,11,fieldvalue,NULL)) REMARKS_E,
        MAX(DECODE(fieldid,165,fieldvalue,NULL)) ReprtNumberIssuedByService_E,
        MAX(DECODE(fieldid,166,fieldvalue,NULL)) SoundLevel_E,
        MAX(DECODE(fieldid,167,fieldvalue,NULL)) referenceSpeed_E,
        MAX(DECODE(fieldid,168,fieldvalue,NULL)) ApplicantNameAddress_E,
        MAX(DECODE(fieldid,169,fieldvalue,NULL)) PerformanceCharacteristics_E,
        MAX(DECODE(fieldid,170,fieldvalue,NULL)) PlantsAddresses_E,
        MAX(DECODE(fieldid,171,fieldvalue,NULL)) TireSizeDesignations_E,
        MAX(DECODE(fieldid,172,fieldvalue,NULL)) ZoneA_E,
        MAX(DECODE(fieldid,173,fieldvalue,NULL)) ZoneB_E,
        MAX(DECODE(fieldid,174,fieldvalue,NULL)) ZoneC_E,
        MAX(DECODE(fieldid,175,fieldvalue,NULL)) PPNProfileFamily_E
FROM (
       SELECT 
              FIELDID,
			  cdv.CERTIFICATIONTYPEID,
			  ce.CERTIFICATENUMBER,
			  FIELDVALUE
        FROM CERTIFICATEDEFAULTVALUE cdv inner join certificate ce on 
                       cdv.CERTIFICATEID = ce.CERTIFICATEID
        WHERE cdv.CERTIFICATIONTYPEID = 1
      )
group by CERTIFICATENUMBER;

 CREATE OR REPLACE FORCE VIEW "ICS"."PRODBRANDLIST_VIEW"
AS
  SELECT SIZESTAMP,
    BRANDDESC,
    SPEEDRATING,
    SINGLOADINDEX,
    DUALLOADINDEX,
    ce.certificateNUmber,
    ce.certificationtypeid
  FROM product p
  INNER JOIN productcertificate pce
  ON p.skuid=pce.skuid
  INNER JOIN certificate ce
  ON pce.certificateid        = ce.certificateid
  AND pce.certificationtypeid = ce.certificationtypeid;
 
 Commit;
 
 
 
 			