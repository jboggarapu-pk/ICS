--Returns the default values for a recently created certificate  
Open pc_retCursor for
SELECT df.fieldid,
     DF.FIELDNAME,
     df.fieldtext,
     df.certificationtypeid,  
     null as certificateNumber,
     cdv.FIELDVALUE
FROM ICS.DEFAULTFIELD df 
   inner join ICS.CERTIFICATETYPEDEFAULTVALUE ctdv on 
              df.certificationtypeid = ctdv.certificationtypeid and
              df.fieldid = CTDV.FIELDID  
   left join ICS.CERTIFICATEDEFAULTVALUE cdv on
               df.fieldid = cdv.fieldid And
               cdv.certificatenumber = ps_certificateNumber
where df.certificationtypeid = li_CertificationTypeId
Order by DF.FIELDID;