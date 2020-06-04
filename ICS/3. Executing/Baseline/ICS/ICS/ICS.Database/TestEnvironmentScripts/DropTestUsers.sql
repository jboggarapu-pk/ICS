set serveroutput ON
DECLARE
  li_total integer;
  ls_userName varchar2(30);
  ls_userName2 varchar2(30);  
BEGIN
   li_total:=0;
   ls_userName:='ICS';
   ls_userName2 :='ICSTestDEV';  

    Select count(1) into li_total
    from all_users au
    where au.username =ls_userName;
    
    if li_total != 0 then
      EXECUTE IMMEDIATE ('DROP USER ' || ls_userName || '  CASCADE');
      DBMS_OUTPUT.put_line ('User droped!!!!');
    else 
      DBMS_OUTPUT.put_line ('User does not exist');
    end if;
    
    li_total :=0 ;
    
     Select count(1) into li_total
    from all_users au
    where au.username =ls_userName2;
    
    if li_total != 0 then
      EXECUTE IMMEDIATE ('DROP USER ' || ls_userName2 || '  CASCADE');
      DBMS_OUTPUT.put_line ('User droped!!!!');
    else 
      DBMS_OUTPUT.put_line ('User does not exist');
    end if;  
    
    
    
    EXCEPTION
      WHEN OTHERS
      THEN
         DBMS_OUTPUT.put_line (SQLERRM);         
END; 