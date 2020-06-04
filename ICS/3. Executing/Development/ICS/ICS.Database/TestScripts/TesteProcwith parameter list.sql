set serveroutput ON
DECLARE
  PS_DELETEME CHAR(200);
  PS_SKU VARCHAR2(200);
  IDLIST ICS.ICS_CRUD.COUNTRY_ID_TAB_TYPE;
BEGIN
  PS_DELETEME := 'y';
  PS_SKU := '98T-2657-88007';
  -- Modify the code to initialize the variable
  IDLIST :=ICS.ICS_CRUD.COUNTRY_ID_TAB_TYPE(1,2,3,4,5,6,7,8,9,10);

  ICS_CRUD.PRODUCTCOUNTRYSAVELIST(
    PS_DELETEME => PS_DELETEME,
    PS_SKU => PS_SKU,
    IDLIST => IDLIST
  );
END;
--select p.sku from product p where  not exists ( select pc.sku from productcountry pc);
--delete from productcountry;
--select * from productcountry;
