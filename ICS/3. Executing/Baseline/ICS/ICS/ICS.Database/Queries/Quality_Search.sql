/*
	Quality certification navigation search that fill the tree
*/
 if ps_SearchType = 'Brand Code' then
          begin
            Open pc_retCursor for
             select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
              from 
                   brand b inner join product p on
                                b.brandcode = p.brandcode
                           inner join productcountry pc on 
                                p.sku = pc.sku
                           inner join country co on
                                pc.countryid = co.countryid
                           inner join certification ce on
                                co.certificationid = ce.certificationid 
                           LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid 
              where b.brandcode =  To_Number(ps_SearchCriteria)
              order by p.sku,ce.certificationname;
           end; 
       -- End if ;
      elsif ps_SearchType = 'SKU No.' then
              begin
                    Open pc_retCursor for
                    select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
              from 
                   brand b inner join product p on
                                b.brandcode = p.brandcode
                           inner join productcountry pc on 
                                p.sku = pc.sku
                           inner join country co on
                                pc.countryid = co.countryid
                           inner join certification ce on
                                co.certificationid = ce.certificationid 
                           LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid     
                    where p.sku = ps_SearchCriteria
                    order by p.sku,ce.certificationname;
                end;
        elsif ps_SearchType = 'NPR ID No.' then           
                 Open pc_retCursor for
                 select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
                 from 
                     brand b inner join product p on
                                b.brandcode = p.brandcode
                             inner join productcountry pc on 
                                p.sku = pc.sku
                             inner join country co on
                                pc.countryid = co.countryid
                             inner join certification ce on
                                co.certificationid = ce.certificationid 
                             LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid     
                    where p.NPRID = ps_SearchCriteria
                    order by p.sku,ce.certificationname;
            
        elsif ps_searchtype = 'Certification No.' then
                Open pc_retCursor for
                 select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
                 from 
                     brand b inner join product p on
                                b.brandcode = p.brandcode
                             inner join productcountry pc on 
                                p.sku = pc.sku
                             inner join country co on
                                pc.countryid = co.countryid
                             inner join certification ce on
                                co.certificationid = ce.certificationid 
                             LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid     
                    where cer.certificatenumber = ps_SearchCriteria
                    order by p.sku,ce.certificationname;
        elsif ps_searchtype = 'Batch No.' then
             Open pc_retCursor for
                 select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
                 from 
                     brand b inner join product p on
                                b.brandcode = p.brandcode
                             inner join productcountry pc on 
                                p.sku = pc.sku
                             inner join country co on
                                pc.countryid = co.countryid
                             inner join certification ce on
                                co.certificationid = ce.certificationid 
                             LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid     
                    where cer.gso_batchnumber = ps_SearchCriteria
                    order by p.sku,ce.certificationname;
        elsif ps_searchtype = 'Spec No.' then
                 Open pc_retCursor for
                 select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
                 from 
                     brand b inner join product p on
                                b.brandcode = p.brandcode
                             inner join productcountry pc on 
                                p.sku = pc.sku
                             inner join country co on
                                pc.countryid = co.countryid
                             inner join certification ce on
                                co.certificationid = ce.certificationid 
                             LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid     
                    where p.SpecNumber = ps_SearchCriteria
                    order by p.sku,ce.certificationname;
        elsif ps_searchtype = 'Importer' then
                Open pc_retCursor for
                 select 
                    b.brandcode,
                    p.sku,
                    p.TireSize,
                    ce.certificationid,
                    ce.certificationname ,
                    ( Case
                          WHEN cer.SKU is not null and ( cer.DATEAPPROVED IS NULL  or cer.datesubmited is null  )       THEN 'InProgress'
                          WHEN cer.SKU is not null and (cer.DATEAPPROVED IS NOT NULL or cer.datesubmited is not null )  THEN 'Approved'
                          WHEN cer.SKU is null then 'Requested'     
                       END ) AS State                 
                 from 
                     brand b inner join product p on
                                b.brandcode = p.brandcode
                             inner join productcountry pc on 
                                p.sku = pc.sku
                             inner join country co on
                                pc.countryid = co.countryid
                             inner join certification ce on
                                co.certificationid = ce.certificationid 
                             LEFT JOIN CERTIFICATES cer on p.sku = cer.sku and cer.certificationid = ce.certificationid     
                    where cer.nom_importer = ps_SearchCriteria
                    order by p.sku,ce.certificationname;
        else
            pc_retCursor:=null;
        End if ;