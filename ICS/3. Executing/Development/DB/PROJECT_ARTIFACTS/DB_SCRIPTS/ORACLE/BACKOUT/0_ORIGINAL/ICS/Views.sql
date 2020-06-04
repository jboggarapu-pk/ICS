  -- ICS Views backout
  
  CREATE OR REPLACE FORCE VIEW "ICS"."SKUMAIN_VW" ("SKU", "SIZESTAMP", "BRANDCODE", "TIRETYPEID", "NPRID", "DISCONTINUEDATE", "SPECNUMBER", "SPEEDRATING", "SINGLOADINDEX", "DUALLOADINDEX", "TUBELESSYN", "REINFORCEDYN", "EXTRALOADYN", "MEASURINGRIM", "UTQGTREADWEAR", "UTQGTRACTION", "UTQGTEMP", "MUDSNOWYN", "RIMDIAMETER", "SERIALDATE", "BRANDDESC", "LOADRANGE", "MEARIMWIDTH", "REGROOVABLEIND", "PLANTPRODUCED", "MOSTRECENTTESTDATE", "IMARK", "INFORMENUMBER", "FECHADATE", "TREADPATTERN", "SPECIALPROTECTIVEBAND", "NOMINALTIREWIDTH", "ASPECTRATIO", "TREADWEARINDICATORS", "NAMEOFMANUFACTURER", "FAMILY", "ECETIRECLASS", "DOTSERIALNUMBER", "CREATEDBY", "CREATEDON", "MODIFIEDBY", "MODIFIEDON", "BIASBELTEDRADIAL", "PPN") AS 
  SELECT sku_cqbs SKU,
             SIZE_STAMP
          || ' '
          || DECODE (NVL (LOAD_RANGE, ' '),  'XL', 'XL',  'RE', 'RE',  ' ')
             SIZESTAMP,
          brand_code BRANDCODE,
          ICS_PROCS.BOM_ATTRIBUTES.get_product_type (sku_cqbs) TIRETYPEID,
          npr_id NPRID,
          disc_date DISCONTINUEDATE,
          CAST ('' AS VARCHAR2 (30)) SPECNUMBER,
          SUBSTR (speed_rating, 1, 1) SPEEDRATING,
          sload_idx SINGLOADINDEX,
          dload_idx DUALLOADINDEX,
          DECODE (tubl_conv,  'C', 'N',  'T', 'Y',  'Y') TUBELESSYN,
          ----JES DECODE (INSTR (size_stamp, 'RE'), 0, 'N', 'Y') REINFORCEDYN,
          DECODE (NVL (LOAD_RANGE, ' '), 'RE', 'Y', 'N') REINFORCEDYN,
          ---JES DECODE (INSTR (size_stamp, 'XL'), 0, 'N', 'Y') EXTRALOADYN,
          (CASE
              WHEN NVL (LOAD_RANGE, ' ') = 'C'
                   AND ICS_PROCS.BOM_ATTRIBUTES.get_product_type (sku_cqbs) =
                          1
              THEN
                 'Y'
              WHEN NVL (LOAD_RANGE, ' ') = 'XL'
              THEN
                 'Y'
              ELSE
                 'N'
           END)
             EXTRALOADYN,
          CAST ('' AS VARCHAR2 (10)) MEASURINGRIM,
          utqg_wear UTQGTREADWEAR,
          utqg_traction UTQGTRACTION,
          utqg_temp UTQGTEMP,
          DECODE (mud_snow, 'T', 'Y', 'N') MUDSNOWYN,
          ICS_PROCS.BOM_ATTRIBUTES.get_rim_diameter (sku_cqbs) RIMDIAMETER,
          TO_DATE (NULL) SERIALDATE,
          ICS_PROCS.BOM_ATTRIBUTES.get_brand_desc (brand_code) BRANDDESC,
          ICS_PROCS.BOM_ATTRIBUTES.get_load_range (sku_cqbs) LOADRANGE,
          CAST ('' AS NUMBER (10, 3)) MEARIMWIDTH,
          DECODE (regroovable_ind, 'T', 'Y', 'N') REGROOVABLEIND,
          CAST ('' AS VARCHAR2 (50)) PLANTPRODUCED,
          TO_DATE (NULL) MOSTRECENTTESTDATE,
          i_mark IMARK,
          CAST ('' AS VARCHAR2 (50)) INFORMENUMBER,
          TO_DATE (NULL) FECHADATE,
          CAST ('' AS VARCHAR2 (50)) TREADPATTERN,
          CAST ('' AS VARCHAR2 (50)) SPECIALPROTECTIVEBAND,
          CAST ('' AS VARCHAR2 (50)) NOMINALTIREWIDTH,
          ICS_PROCS.BOM_ATTRIBUTES.get_aspect_ratio (
             size_stamp,
             ICS_PROCS.BOM_ATTRIBUTES.get_product_type (sku_cqbs))
             ASPECTRATIO,
          CAST ('' AS VARCHAR2 (50)) TREADWEARINDICATORS,
          CAST ('' AS VARCHAR2 (100)) NAMEOFMANUFACTURER,
          ICS_PROCS.BOM_ATTRIBUTES.get_imark_family (sku_cqbs) FAMILY,
          ICS_PROCS.BOM_ATTRIBUTES.get_ece_tire_class (sku_cqbs) ECETIRECLASS,
          CAST ('' AS VARCHAR2 (15)) DOTSERIALNUMBER,
          CAST ('' AS VARCHAR2 (50)) CREATEDBY,
          TO_DATE (NULL) CREATEDON,
          CAST ('' AS VARCHAR2 (50)) MODIFIEDBY,
          TO_DATE (NULL) MODIFIEDON,
          DECODE (belted_radial,  'X', 'BIAS',  'B', 'BELTED',  'RADIAL')
             BIASBELTEDRADIAL,
          ppn
     FROM sku_master_mv
    WHERE product_type = 'TI' AND NVL (disc_date, SYSDATE) > '01-NOV-1991'
 ;
 
/


  CREATE OR REPLACE FORCE VIEW "ICS"."SKUMAIN_PRODUCT_VIEW" ("SKU", "SIZESTAMP", "BRANDCODE") AS 
  SELECT sm.sku, sm.sizestamp, sm.brandcode
     FROM skumain_vw sm
   UNION
   SELECT p.sku, p.sizestamp, p.brandcode
     FROM product p
   ORDER BY 1
 ;
 
/


  CREATE OR REPLACE FORCE VIEW "ICS"."SKUMAIN_MINUS_PRODUCT_VIEW" ("SKU", "SIZESTAMP", "BRANDCODE") AS 
  SELECT SKU, SIZESTAMP, BRANDCODE FROM skumain_vw
   MINUS
   SELECT SKU, SIZESTAMP, BRANDCODE FROM product
 ;
 
/


  CREATE OR REPLACE FORCE VIEW "ICS"."PRODUCT_MINUS_SKUMAIN_VIEW" ("SKU", "SIZESTAMP", "BRANDCODE") AS 
  SELECT sku, sizestamp, brandcode FROM product
   MINUS
   SELECT sku, sizestamp, brandcode FROM skumain_vw
 ;
 
/
