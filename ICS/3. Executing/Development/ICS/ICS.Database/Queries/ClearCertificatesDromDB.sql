 select * from ICS.productcountry pc where pc.skuid in (select skuid from certificate );
update ICS.productcountry pc set pc.REQUESTSTATUS=null
where pc.skuid in (select skuid from certificate );

delete from beadunseatdtl ;
delete FROM BEADUNSEATHDR ;

delete from plungerdtl ;
delete FROM plungerhdr ;

delete from measuredtl ;
delete FROM measurehdr ;

delete from treadweardtl ;
delete FROM treadwearhdr ;

delete FROM certificate ;

--read information on the DB
SELECT * FROM certificate where certificatenumber = 'ccc001';
select * FROM BEADUNSEATHDR where certificatenumber = 'ccc001';
select * from beadunseatdtl where BEADUNSEATID = (select BEADUNSEATID FROM BEADUNSEATHDR where certificatenumber = 'ccc001');

select * FROM plungerhdr where certificatenumber = 'ccc001';
select * from plungerdtl where plungerID = (select plungerID FROM plungerhdr where certificatenumber = 'ccc001');

select * FROM measurehdr where certificatenumber = 'ccc001';
select * from measuredtl where measureid = (select measureid FROM measurehdr where certificatenumber = 'ccc001');

select * FROM treadwearhdr where certificatenumber = 'ccc001';
select * from treadweardtl where treadwearid = (select treadwearid FROM treadwearhdr where certificatenumber = 'ccc001');
