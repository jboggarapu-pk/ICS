SELECT BRANDID,BRANDNAME,P.SKU,P.TIRESIZE ,PC.COUNTRY_FK,C.COUNTRYNAME,c.certification_fk,ce.certificationname
FROM ICS.BRAND B INNER JOIN PRODUCT P ON B.BRANDID = P.BRAND_FK
               LEFT JOIN PRODUCTCOUNTRY PC ON
                       P.BRAND_FK = PC.BRAND_FK AND
                       P.SKU      = PC.SKU_FK
               LEFT JOIN COUNTRY C ON PC.COUNTRY_FK = C.COUNTRYID
               LEFT JOIN certification ce on c.certification_fk = ce.certificationid
WHERE B.BRANDNAME LIKE '%AVON%'               
order by BRANDNAME,P.SKU;   