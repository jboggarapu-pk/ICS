/* Certified Regions and its countries */
Select distinct(r.regionname) as Cert_Region
from Region r,Country c 
where r.regionid = c.regionid and
      c.certificationid is not null
Order by r.regionname;
 /* UnCertified Regions and its countries */     
Select distinct(r.regionname) as UnCert_Region
from Region r,Country c 
where r.regionid = c.regionid and
      c.certificationid is null
Order by r.regionname;
/* certified regions minus uncertified regions */
Select distinct(r.regionname) as Cert_Region
from Region r,Country c 
where r.regionid = c.regionid and
      c.certificationid is not null and
      not exists ( Select r.regionname
                   from Region r,Country c 
                   where r.regionid = c.regionid and
                         c.certificationid is null)      
Order by r.regionname;