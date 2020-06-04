 Select SKU,SIZESTAMP,BrandCode,To_Char(Max(ModifiedOn),'yyyy/mm/dd - hh:mi:ss:am')
FROM (Select SKU,SIZESTAMP,BrandCode,ModifiedOn 
      From skumain
      UNION 
      Select SKU,SIZESTAMP,BrandCode,ModifiedOn 
      From PRODUCT) Prod2
GROUP BY SKU, SIZESTAMP, BrandCode      
Order by 1;

in(Select SKU
                            FROM (Select SKU 
                                  From skumain
                                  UNION 
                                  Select SKU
                                  From PRODUCT) Prod2                            
                            GROUP BY SKU )