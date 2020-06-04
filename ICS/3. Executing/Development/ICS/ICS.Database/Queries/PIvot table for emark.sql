 SELECT CERTIFICATENUMBER,
      MAX(DECODE(fieldid,1,fieldvalue,NULL)) ManufacturerName_E,
      MAX(DECODE(fieldid,2,fieldvalue,NULL)) ManufacturerNameAddress_E,
      MAX(DECODE(fieldid,3,fieldvalue,NULL))  TechnicalService_E ,
      MAX(DECODE(fieldid,4,fieldvalue,NULL)) Place_E,
      MAX(DECODE(fieldid,5,fieldvalue,NULL)) MeasureRim_E,
      MAX( DECODE(fieldid,6,fieldvalue,NULL)) InflationPressure_E,
      MAX(DECODE(fieldid,7,fieldvalue,NULL)) TestLaboratory_E
FROM (
        SELECT FIELDID,
          CERTIFICATIONTYPEID,
          CERTIFICATENUMBER,
          FIELDVALUE
        FROM CERTIFICATEDEFAULTVALUE 
        where CERTIFICATENUMBER='ecert002' and
               
    )
GROUP BY CERTIFICATENUMBER ;