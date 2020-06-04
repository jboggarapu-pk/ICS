set serveroutput ON
DECLARE
  PC_RETCURSOR ICS.ICS_CRUD.retCursor;
  PI_CERTIFICATIONID NUMBER;
  
  li_CertId     integer;
  ls_CertName   varchar2(200);
  li_TYPEID     integer;
  ls_TYPEName   varchar2(200);
BEGIN
          PI_CERTIFICATIONID :=3;
        
          ICS_CRUD.GETSEARCHTYPESBYCERTIFICATION(
            PC_RETCURSOR => PC_RETCURSOR,
            PI_CERTIFICATIONID => PI_CERTIFICATIONID
          );   
          
          if  PC_RETCURSOR is null then
          
             DBMS_OUTPUT.PUT_LINE('ret cursor is null');          
          else
               FETCH PC_RETCURSOR INTO li_CertId,ls_CertName,li_TYPEID,LS_TYPENAME ;
               if nvl(li_certid,'') = ''  then
                  
                  DBMS_OUTPUT.PUT_LINE('li_certid is empty');
                  DBMS_OUTPUT.PUT_LINE(li_CertId || ' | ' || ls_CertName || ' | ' || li_TYPEID || ' | ' || LS_TYPENAME); 
               else
                    LOOP        
                        EXIT WHEN PC_RETCURSOR%NOTFOUND; 
                        DBMS_OUTPUT.PUT_LINE(li_CertId || ' | ' || ls_CertName || ' | ' || li_TYPEID || ' | ' || LS_TYPENAME); 
                        FETCH PC_RETCURSOR INTO li_CertId,ls_CertName,li_TYPEID,LS_TYPENAME ;
                    END LOOP; 
                     CLOSE PC_RETCURSOR;  
               end if;
          
          end if;
          
 
   
END;
SKU No.
SELECT c.CERTIFICATIONID,C.CERTIFICATIONNAME,st.typeid,st.typename
FROM CERTIFICATION C INNER JOIN CERTIFICATIONSEARCHTYPE  cst on
                   c.CERTIFICATIONID=cst.CERTIFICATION_FK
              INNER JOIN SEARCHTYPE ST on 
                  cst.SEARCHTYPE_FK = st.TYPEID
WHERE cst.searchtype_fk=4


