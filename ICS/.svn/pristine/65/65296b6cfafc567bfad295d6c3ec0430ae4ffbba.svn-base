create or replace PROCEDURE           CERT_RENEWAL_NOTIFICATION IS
ls_certificatenumber              certificate.certificatenumber%TYPE;
li_certificationtypeid               certificate.certificationtypeid%TYPE;
ls_certificationtypename         certificationtype.certificationtypename%TYPE;
lt_dateapproved                    certificate.certdateapproved%TYPE;
ln_days                                NUMBER;
ls_From                                VARCHAR2(80) := 'ICS_NOTIFY@coopertire.com';
--ls_Recipient                          VARCHAR2(80) := 'ICS_NOTIFY@coopertire.com';
ls_Recipient                           VARCHAR2(80) := 'ICS_SUPPORT@coopertire.com';
---ls_RecipientCC                       VARCHAR2(80) := 'jlczerniak@coopertire.com'; 
ls_RecipientCC                       VARCHAR2(80) := 'ICS_SUPPORT@coopertire.com'; 
---ls_RecipientCC                       VARCHAR2(80) := 'ICS_ALERTS@coopertire.com';
ls_Subject                             VARCHAR2(80) := 'Certificate Renewals Required';
ls_Mail_Host                          VARCHAR2(30) := 'SMTP';
ls_Body                                 VARCHAR2(32767) := '';
v_Mail_Conn                         utl_smtp.Connection;
crlf                                       VARCHAR2(2)  := chr(13)||chr(10);

 CURSOR c1 IS 
     SELECT c.certificatenumber,
            c.certificationtypeid,
            ct.certificationtypename,
            c.certdateapproved
    FROM certificate c
        INNER JOIN certificationtype ct ON
            c.certificationtypeid = ct.certificationtypeid
    WHERE c.certificationtypeid NOT IN (1, 6)
        AND c.certdateapproved IS NOT NULL
        AND LOWER(c.activestatus) = 'y'
        AND LOWER(c.renewalrequired_cgin) = 'y'
        AND c.archivedate_cegi IS NULL
        AND LOWER(c.mostrecentcert) = 'y'
        ORDER BY C.CERTIFICATIONTYPEID, c.certdateapproved, CERTIFICATENUMBER;
/******************************************************************************
   NAME:       CERT_RENEWAL_NOTIFICATION
   PURPOSE:  Check if certificates are up for renewal and send notification  

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        2/28/2011   jlczerni       1. Created this procedure.

   NOTES:

   Automatically available Auto Replace Keywords:
      Object Name:     CERT_RENEWAL_NOTIFICATION
      Sysdate:         2/28/2011
      Date and Time:   2/28/2011, 7:12:08 PM, and 2/28/2011 7:12:08 PM
      Username:        jlczerni (set in TOAD Options, Procedure Editor)
      Table Name:       (set in the "New PL/SQL Object" dialog)

******************************************************************************/
BEGIN
    OPEN c1;
    LOOP
    FETCH c1 INTO ls_certificatenumber, li_certificationtypeid, ls_certificationtypename, lt_dateapproved;
        EXIT WHEN c1%NOTFOUND OR LENGTH(LS_BODY)> 32767-65;

        --ln_days := TRUNC(TO_NUMBER(SUBSTR((SYSDATE-lt_dateapproved),1, INSTR(SYSDATE-lt_dateapproved,' '))));
        ln_days := TO_NUMBER(sysdate - lt_dateapproved);
       --- DBMS_OUTPUT.PUT_LINE(ROUND(ln_days));
            
        CASE li_certificationtypeid
            WHEN 2 THEN
               ---jeseitz 3/19/2012 per R. Riedel - IF ln_days > 730 - 90 THEN -- 2 years - 90 days 730
                IF ln_days > 365 - 90 THEN -- 1 year - 90 days 
                    ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: GSO' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);
                END IF;
--            WHEN 3 THEN ---NOM -- stop checking for now. Will need to revisit.
--                ---jeseitz 3/19/2012 per R. Riedel - IF ln_days > 730 - 90 THEN -- 2 years - 90 days
--                IF ln_days > 365 - 90 THEN -- 1 year - 90 days
--                    ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: NOM' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);  
--                END IF;
            WHEN 4 THEN
                 ---jeseitz 3/19/2012 per R. Riedel - IF ln_days > 730 - 90 THEN -- 2 years - 90 days
                 IF ln_days > 365 - 90 THEN -- 1 year - 90 days
                    ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: Imark' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);
                END IF;
            WHEN 5 THEN
                 IF ln_days > 1825 - 90 THEN -- 5 years - 90 days 1825
                    ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: CCC' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);
                END IF;
             ELSE
                -- Emark does not require renewal.  This is just in case something accidently got set to make the system think it does.
                ls_Body := ls_Body;
        END CASE;
        IF LENGTH(LS_BODY)> 32767-65 THEN
          LS_BODY := LS_BODY||'...';
        END IF;
        --EXIT WHEN c1%NOTFOUND;
   END LOOP;
   CLOSE c1;
   
   
   --ls_body := 'This is a test e-mail from CERT_RENEWAL_NOTIFICATION';
   ---DBMS_OUTPUT.PUT_LINE(ls_body);
   
   IF LENGTH(ls_body) > 0 THEN
        v_Mail_Conn := utl_smtp.Open_Connection(ls_Mail_Host, 25);
        utl_smtp.Helo(v_Mail_Conn, ls_Mail_Host);
        utl_smtp.Mail(v_Mail_Conn, ls_From);
        utl_smtp.Rcpt(v_Mail_Conn, ls_Recipient);
        utl_smtp.Rcpt(v_Mail_Conn, ls_RecipientCC);
        utl_smtp.Data(v_Mail_Conn,
        'Date: '   || to_char(sysdate, 'Dy, DD Mon YYYY hh24:mi:ss') || crlf ||
        'From: '   || ls_From || crlf ||
        'Subject: '|| ls_Subject || crlf ||
        'To: '     || ls_Recipient || crlf ||
        'CC: '     || ls_RecipientCC || crlf ||
        crlf ||ls_Body || crlf );
        utl_smtp.Quit(v_mail_conn);

   END IF;
   
   EXCEPTION
     WHEN utl_smtp.Transient_Error OR utl_smtp.Permanent_Error then
     DBMS_OUTPUT.PUT_LINE('Unable to send mail: '||sqlerrm);
            raise_application_error(-20000, 'Unable to send mail: '||sqlerrm);
     WHEN OTHERS THEN
       -- Consider logging the error and then re-raise
       DBMS_OUTPUT.PUT_LINE(SQLCODE || SQLERRM);
       RAISE;
END CERT_RENEWAL_NOTIFICATION;
/

create or replace PROCEDURE           CertificateApprovalNotice IS
/******************************************************************************
   NAME:       CertificateApprovalNotice
   PURPOSE:  EMAILS  notifications of recent certificate approvals  

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        6/5/2015   jeseitz       1. Created this procedure.
   1.1        7/27/2015 jeseitz       corrected database name so correct distribution list is used on production.
               9/18/2015 jesetz         added production location for CCC and India certificates

 

******************************************************************************/
ls_certificatenumber              varchar2(50);
li_certificationtypeid               certificate.certificationtypeid%TYPE;
ls_certificationtypename         certificationtype.certificationtypename%TYPE;
lt_dateapproved                    certificate.certdateapproved%TYPE;
ls_location                            varchar2(30);
ls_description                        varchar2(20);
ln_recs                                 number;
ls_From                                VARCHAR2(80) := 'jrnelaturi@coopertire.com';
ls_Recipient                           VARCHAR2(80) := 'jrnelaturi@coopertire.com';
ls_RecipientCC                       VARCHAR2(80) := 'SCMallampalli@coopertire.com';
ls_Bcc                                   VARCHAR2(32767);
ls_Subject                             VARCHAR2(80) := 'International Certification Approvals';
ls_Mail_Host                          VARCHAR2(30) := 'SMTP';
ls_Body                                 VARCHAR2(32767) := '';
ls_Message                           VARCHAR2(32767);
ls_Note                                 VARCHAR2(100) := ' ';
ls_header                              VARCHAR2(32767);
ls_header2                            VARCHAR2(32767);
ls_MimeType                         VARCHAR2(200);
v_Mail_Conn                          utl_smtp.Connection;
crlf                                        VARCHAR2(2)  := chr(13)||chr(10);
ls_db_name                           VARCHAR2(50);

 CURSOR c1 IS 
     SELECT c.certificatenumber, C.EXTENSION_EN,
            c.certificationtypeid,
            ct.certificationtypename, 
            CASE TRIM(C.PRODUCTLOCATION)
               WHEN '1' THEN 'FIN'
               WHEN '2' THEN 'TEX'
               WHEN '5' THEN 'TUP'
               WHEN '12' THEN 'MLK'
               WHEN '76' THEN 'OCC'
               ELSE C.PRODUCTLOCATION
            END PRODUCTLOCATION,
            PC.DATEAPPROVED_CEGI,PC.OPER_DATE_APPROVED,		
            P.MATL_NUM, p.brand, P.BRAND_LINE, p.sizestamp, P.SPEEDRATING, P.SINGLOADINDEX,
            P.DUALLOADINDEX, p.sku
    FROM productcertificate pc
         INNER JOIN certificate c ON
            c.certificateid = pc.certificateid
        INNER JOIN product p ON
            P.SKUID = PC.SKUID
        INNER JOIN certificationtype ct ON
           CT.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
    WHERE
		 decode(c.certificationtypeid, 1, nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30),  --ECE3054
                                                  2,  nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30),  --GSO
                                                  3,  nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30),  --NOM
                                                  4,  nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30),  -- IMARK
                                                  5,  nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30), -- CCC
                                                  6,  nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30),  -- E117
                                                  7,  nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30), --India_mark
                                                   nvl(PC.DATE_APPROVAL_ENTERED,sysdate+30))  -- other
                between trunc(sysdate-7) and trunc(sysdate-1)
        AND LOWER(c.activestatus) = 'y' 
         AND c.archivedate_cegi IS NULL
        AND (LOWER(c.mostrecentcert) = 'y' or C.CERTIFICATIONTYPEID = 4)
        AND C.CERTIFICATIONTYPEID <> 3 ---  NOM certificates don't get an approval date.
        ORDER BY C.CERTIFICATIONTYPEID,   C.CERTIFICATENUMBER, C.EXTENSION_EN, P.MATL_NUM;
BEGIN

SELECT SYS.DATABASE_NAME
  INTO ls_db_name
  FROM DUAL;
  
  --don't send emails out to everone if not on production system
  IF INSTR(ls_db_name,'FA0017_TECH') = 0 THEN -- not on PRODUCTION
     ls_Recipient := 'ICS_SUPPORT@COOPERTIRE.COM';
     ls_RecipientCC   := '';
  END IF;
      

  ls_mimetype := 'text/html; charset=us-ascii';
   ln_recs :=0;
   li_certificationtypeid  := 0; 
   
   ls_header2 := '<TABLE BORDER=''1'' BGCOLOR=''#EEEEEE'' style=''font-size:12px; font-family : Arial'' >';
   ls_header2 :=  ls_header2 || '<TR BGCOLOR=''BLACK''>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>MATERIAL</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>SKU</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>BRAND</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>SIZE</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>LOAD INDEX</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>SPEED RATING</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>TYPE</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>CERTIFICATE</FONT></TH>';
   ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>DATE APPROVED</FONT></TH>';
   ls_header2 :=  ls_header2 ||'</TR>'; 
                    
   FOR lcr_C1 IN c1 LOOP
--         IF lcr_c1.certificationtypeid <>   li_certificationtypeid then
--            ls_Body :=  ls_Body || crlf ;
--            li_certificationtypeid :=  lcr_c1.certificationtypeid;
--         end if;
--        
         ls_description := lcr_c1.SINGLOADINDEX;
         if lcr_c1.DUALLOADINDEX is null or trim(lcr_c1.DUALLOADINDEX) = '' then
               ls_description :=  ls_description;
         else
                ls_description :=  ls_description||'/'||lcr_c1.DUALLOADINDEX;
         end if;
         
         lt_dateapproved := lcr_c1.OPER_DATE_APPROVED;
         if lcr_c1.certificationtypeid = 2 or  lcr_c1.certificationtypeid = 4 then -- GSO or IMARK
             lt_dateapproved :=  lcr_c1.DATEAPPROVED_CEGI;
         end if;
       
        ls_certificatenumber := lcr_c1.certificatenumber;
        if lcr_c1.certificationtypeid = 5 OR lcr_c1.certificationtypeid = 7 then --For CCC or India certifcates, include the production location.
         if lcr_c1.productlocation is not null then   
            ls_certificatenumber := ls_certificatenumber||' ('||lcr_c1.productlocation||')';
         end if;
        end if;
   
         ls_Body := ls_Body || '<TR>';
         ls_Body := ls_Body || '<TD>'||ltrim(lcr_c1.matl_num,'0')|| '</TD>';
         ls_Body := ls_Body || '<TD>'||lcr_c1.SKU|| '</TD>';
         ls_Body := ls_Body || '<TD>'||lcr_c1.brand|| ' ';
         ls_Body := ls_Body || lcr_c1.brand_line|| '</TD>';
         ls_Body := ls_Body || '<TD>'||lcr_c1.sizestamp|| '</TD>';
         ls_Body := ls_Body || '<TD>'||ls_description|| '</TD>';
         ls_Body := ls_Body || '<TD>'||lcr_c1.speedrating|| '</TD>';
         ls_Body := ls_Body || '<TD>'|| lcr_c1.certificationtypename|| '</TD>';
         ls_Body := ls_Body || '<TD>'||ls_certificatenumber|| '</TD>';
         ls_Body := ls_Body || '<TD>'||to_char( lcr_c1.DATEAPPROVED_CEGI,'MM/DD/YYYY')|| '</TD>';
         ls_Body := ls_Body || '</TR>' ;
           
          ln_recs := ln_recs + 1;

        IF LENGTH(LS_BODY)> 32767-65 THEN
          LS_BODY := LS_BODY||'...'||crlf;
        END IF;

   END LOOP;

   --check for  email that needs sent
    if ln_recs > 0 then
               
        ls_message := '<div>' 
                  || '<table width=''100%'' height=''182'' border=''0'' cellpadding=''3'' cellspacing=''1'' bgcolor=''#006699'' style=''font-size:12px; font-family : Arial''>'
                  || ' <tr><td bgcolor=''#FFFFFF''>' || '<br />'
                  || '<span style=''font-size:12px; font-family : Arial''>'|| 'The following approvals were recently received: '
                  || '</span>' || '<br /><br />'|| '<table cellspacing=''0'' cellpadding=''0'' > '            
                  || '<tr><td style=''text-align:left; FONT-SIZE: 12px;  font-family : Arial''>'||  ls_header2 || ls_Body ||'</TABLE></td></tr>' || '</table> ' || '<br />' 
                  || '<b><span style=''font-size:12px; font-family : Arial''>' || ls_note 
                  || '</span></b><br /><br />' || ' Thanks,<br />' 
                  || '<b><span style=''font-size:12px; font-family : Arial''>' || 'Quality Group' 
                  || '</span></b><br /><br />' || ' </td></tr></table></div>';
               
--               
        utl_mail.send(sender => ls_from,   recipients => ls_recipient,   cc =>  ls_RecipientCC,   bcc => ls_bcc, 
        subject => ls_subject,   message => ls_message,   mime_type => ls_mimetype,   priority => '3');

    end if;
   
    ---DBMS_OUTPUT.PUT_LINE(ls_body);
   
   EXCEPTION
       WHEN utl_smtp.Transient_Error OR utl_smtp.Permanent_Error then
           DBMS_OUTPUT.PUT_LINE('Unable to send mail: '||sqlerrm);
            raise_application_error(-20000, 'Unable to send mail: '||sqlerrm);   
            APP_MESSAGE_OPERATIONS.app_message_insert( 
                                           ls_db_name,
                                             'ICS_PROCS',
                                             SYSDATE,
                                             'CertificateApprovalNotice',
                                             ' ',
                                             SQLCODE,
                                             SQLERRM)       ;
            
       WHEN OTHERS THEN
       -- Consider logging the error and then re-raise
                 DBMS_OUTPUT.PUT_LINE(SQLCODE || SQLERRM);
                  APP_MESSAGE_OPERATIONS.app_message_insert( 
                                            ls_db_name,
                                             'ICS_PROCS',
                                             SYSDATE,
                                             'CertificateApprovalNotice',
                                             ' ',
                                             SQLCODE,
                                             SQLERRM)       ;
       RAISE;
END CertificateApprovalNotice;
/

create or replace PROCEDURE CMS_NOTIFY_EMAIL_FOR_SCANNER (as_Press_Name IN  VARCHAR2,
														  as_toggle IN  VARCHAR2,
														  as_side IN  VARCHAR2)
 IS
/************************************************************************************************
     Procedure Name - CMS_NOTIFY_EMAIL_FOR_SCANNER
     Change History 
      --------------------------------------------------------------------------
      Version No  Date          Author              Description
      ---------------------------------------------------------------------------
      1.0         03/28/2017    Jaimie Nelaturi     Original Version
    *************************************************************************************************/
	
  ls_From                            VARCHAR2(80) := 'jrnelaturi@coopertire.com';
  ls_Recipient                       VARCHAR2(80) := 'jrnelaturi@coopertire.com';
  ls_RecipientCC                     VARCHAR2(80) := 'SCMallampalli@coopertire.com';
  --ls_Bcc                             VARCHAR2(32767) := 'SCMallampalli@coopertire.com';
  ls_Subject                         VARCHAR2(80) := 'ALERT: barcode scanning '|| as_toggle||' on press '|| as_Press_Name;
  ls_Mail_Host                       VARCHAR2(30) := 'SMTP';
  ls_Body                            VARCHAR2(32767) := '';
  ls_Message                         VARCHAR2(32767);
  ls_Note                            VARCHAR2(100) := ' ';
  ls_MimeType                        VARCHAR2(200);  
  crlf                               VARCHAR2(2)  := CHR(13)||CHR(10);
  
  BEGIN
	ls_mimetype := 'text/html; charset=us-ascii';
       
	  /*BEGIN
	    SELECT ITEM_VALUE 
		   INTO ls_From
		FROM PROCESS_VARIABLES 
		WHERE UPPER(ITEM_DESC) = UPPER('From Email');
		
		 SELECT ITEM_VALUE 
		   INTO ls_Recipient
		FROM PROCESS_VARIABLES 
		WHERE UPPER(ITEM_DESC) = UPPER('ENABLE_SCANNER email distribution list');
		
	  EXCEPTION WHEN NO_DATA_FOUND
	    THEN
            ls_From := NULL;
			ls_Recipient := NULL;
	  END;*/
	  
         ls_Body := '<BGCOLOR=''#EEEEEE'' style=''font-size:12px; font-family : Arial'' >'; 
         ls_Body := ls_Body || '<TR>';
         ls_Body := ls_Body || '<TD>'|| 'Please note that a change has been made to the Cure Network configuration to ' ||as_toggle||' <br /> barcode scanning on '|| as_side||' side(s) of press ' ;
         ls_Body := ls_Body || as_Press_Name||'.It can take up to 10 minutes for this <br /> change to be present at the press. <br />';
		 ls_Body := ls_Body || ' If this change was made in error, please contact your System Administrator to correct the'||CHR(13)||'setting.';
		 ls_Body := ls_Body || '</TD>' ;
         ls_Body := ls_Body || '</TR>' ;
			IF LENGTH(LS_BODY)> 32767-65 THEN
			  LS_BODY := LS_BODY||'...'||crlf;
			END IF;   
    --check for  email that needs sent                 
        ls_message := '<div>'                  
                  || ' <tr><td bgcolor=''#FFFFFF''>' || '<br />'
                  || '<span style=''font-size:12px; font-family : Arial''>'
                  || '</span>' || '<br /><br />'|| '<table cellspacing=''0'' cellpadding=''0'' > '            
                  || '<tr><td style=''text-align:left; FONT-SIZE: 12px;  font-family : Arial''>' || ls_Body ||'</TABLE></td></tr>' || '</table> ' || '<br />' 
                  || '<b><span style=''font-size:12px; font-family : Arial''>' || ls_note 
                  || '</span></b><br /><br />' || ' Thanks,<br />' 
                  || '<b><span style=''font-size:12px; font-family : Arial''>' || 'Cure Network Configuration Group' 
                  || '</span></b><br /><br />' || ' </td></tr></table></div>';
				  
		 utl_mail.send(sender => ls_from,   recipients => ls_recipient,   cc =>  ls_RecipientCC,  
        subject => ls_subject,   message => ls_message,   mime_type => ls_mimetype,   priority => '3');
		
END CMS_NOTIFY_EMAIL_FOR_SCANNER;
/

create or replace Procedure GetNOMCertification( ps_CertificateNumber in varchar2,
                                     pi_tiretypeid        in number,
                                     pc_CertificateDfValue  out retCursor,
                                     pc_CertificateInfo   out retCursor,
                                     pc_Product           out retcursor, 
                                     pc_MeasureHDR        out retCursor,
                                     pc_measureDtl        out retCursor,
                                     pc_BEADUNSEATHDR     out retCursor,
                                     pc_BEADUNSEATDTL     out retCursor,
                                     pc_PLUNGERHDR        out retCursor,
                                     pc_PLUNGERDTL        out retCursor,
                                     pc_TREADWEARHDR      out retCursor,
                                     pc_TREADWEARDTL      out retCursor,
                                     pc_ENDURANCEHDR      out retCursor,
                                     pc_ENDURANCEDTL      out retCursor,
                                     pc_HIGHSPEEDHDR      out retCursor,
                                     pc_HIGHSPEEDDTL      out retCursor,
                                     pc_SPEEDTESTDETAIL   out retCursor,
                                     pc_Brand             out retCursor) as 
    /************************************************************************************************
     Procedure Name - GetNOMCertification
     Change History 
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - As per PRJ3617 
                                            - Replaced NPRID with PSN
                                            - Added Matl_Num wherever SKU is available in Select list of the query
                                            - Added Brand, Brand_Line columns instead of Brandcode
    *************************************************************************************************/                                     

      --Exception variables
      li_ParametersAreNull exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreNull,-20005);   
       li_ParametersAreInvalid exception;      
      -- link the exception to the error number
      pragma exception_init( li_ParametersAreInvalid,-20006);   
      
      ls_MachineId VARCHAR2(50):=null;
      ls_OperatorId VARCHAR2(50):='ICSDEV';
      ls_ErrorMsg varchar2(4000);                 
      
      ls_brandcode VARCHAR2(10):= NULL;                  
  begin
      
      if ps_CertificateNumber is null then
          raise li_ParametersAreNull;
      end if ;
      if ps_CertificateNumber = '' then
          raise li_ParametersAreInvalid;
      end if ;
            
      
      -- Gets the brand information
--          Open pc_brand for
--          SELECT distinct  BRANDCODE, ---get all brands that have ever been associated with this certificate, because
--                        BRANDNAME,           ---the sks's that were on the original test specs may have since been discontinued
--                        CERTIFICATENUMBER---but just get each brand once, even though it is multiple extensions.
--          FROM  brand_view
--          where lower(certificatenumber) = lower(ps_certificateNumber)  ; 
     --- use this cursor to return the sizes for this brand


          
      --Gets the default values information.         
            OPen pc_CertificateDfValue for
            SELECT CERTIFICATENUMBER,
                   MAX(DECODE(fieldid,153,fieldvalue,NULL)) Dimension_N,
                   MAX(DECODE(FIELDID,120,fieldvalue,NULL)) EquipoEmpleadoRinDesc1_N,
                   MAX(DECODE(FIELDID,123,fieldvalue,NULL)) EquipoEmpleadoRinDESC2_N,
                   MAX(DECODE(FIELDID,126,fieldvalue,NULL)) EquipoEmpleadoRinDESC3_N ,
                   MAX(DECODE(FIELDID,129,fieldvalue,NULL)) EquipoEmpleadoRinDESC4_N ,
                   MAX(DECODE(FIELDID,132,fieldvalue,NULL)) EquipoEmpleadoRinDESC5_N,
                   MAX(DECODE(FIELDID,135,fieldvalue,NULL)) EquipoEmpleadoRinDESC6_N,
                   MAX(DECODE(FIELDID,138,fieldvalue,NULL)) EquipoEmpleadoRinDESC7_N,
                   MAX(DECODE(FIELDID,141,fieldvalue,NULL)) EquipoEmpleadoRinDESC8_N,
                   MAX(DECODE(FIELDID,121,fieldvalue,NULL)) EquipoEmpleadoRinMarca1_N,
                   MAX(DECODE(FIELDID,124,fieldvalue,NULL)) EquipoEmpleadoRinMarca2_N,
                   MAX(DECODE(FIELDID,127,fieldvalue,NULL)) EquipoEmpleadoRinMarca3_N,
                   MAX(DECODE(FIELDID,130,fieldvalue,NULL)) EquipoEmpleadoRinMarca4_N ,
                   MAX(DECODE(FIELDID,133,fieldvalue,NULL)) EquipoEmpleadoRinMarca5_N,
                   MAX(DECODE(FIELDID,136,fieldvalue,NULL)) EquipoEmpleadoRinMarca6_N,
                   MAX(DECODE(FIELDID,139,fieldvalue,NULL)) EquipoEmpleadoRinMarca7_N,
                   MAX(DECODE(FIELDID,142,fieldvalue,NULL)) EquipoEmpleadoRinMarca8_N,
                   MAX(DECODE(FIELDID,122,fieldvalue,NULL)) EquipoEmpleadoRinModelo1_N,
                   MAX(DECODE(FIELDID,125,fieldvalue,NULL)) EquipoEmpleadoRinModelo2_N ,
                   MAX(DECODE(FIELDID,128,fieldvalue,NULL)) EquipoEmpleadoRinModelo3_N,
                   MAX(DECODE(FIELDID,131,fieldvalue,NULL)) EquipoEmpleadoRinModelo4_N ,
                   MAX(DECODE(FIELDID,134,fieldvalue,NULL)) EquipoEmpleadoRinModelo5_N,
                   MAX(DECODE(FIELDID,137,fieldvalue,NULL)) EquipoEmpleadoRinModelo6_N,
                   MAX(DECODE(FIELDID,140,fieldvalue,NULL)) EquipoEmpleadoRinModelo7_N,
                   MAX(DECODE(FIELDID,143,fieldvalue,NULL)) EquipoEmpleadoRinModelo8_N,
                   MAX(DECODE(FIELDID,144,fieldvalue,NULL)) EquipoPruebaResistencia_N,
                   MAX(DECODE(FIELDID,150,fieldvalue,NULL)) EvaluationDate_N,
                   MAX(DECODE(FIELDID,159,fieldvalue,NULL)) FinalPressure_N,
                   MAX(DECODE(FIELDID,152,fieldvalue,NULL)) Height_N,
                   MAX(DECODE(FIELDID,146,fieldvalue,NULL)) IdentificationKey_N,
                   MAX(DECODE(FIELDID,158,fieldvalue,NULL)) LoadBehavior_N,
                   MAX(DECODE(FIELDID,147,fieldvalue,NULL)) Loadcapacity_N,
                   MAX(DECODE(FIELDID,155,fieldvalue,NULL)) MeasurementFactor_N,
                   MAX(DECODE(FIELDID,148,fieldvalue,NULL)) Model_N,
                   MAX(DECODE(FIELDID,157,fieldvalue,NULL)) PenetrationResistence_N,
                   MAX(DECODE(FIELDID,156,fieldvalue,NULL)) RimResistence_N,
                   MAX(DECODE(FIELDID,160,fieldvalue,NULL)) RoomTemp_N,
                  --- MAX(DECODE(FIELDID,154,fieldvalue,NULL)) SectionWidth_N,
                   MAX(DECODE(FIELDID,119,fieldvalue,NULL)) SinalpAddress_N,
                   MAX(DECODE(FIELDID,118,fieldvalue,NULL)) SinalpCentroEvaluacion_N,
                   MAX(DECODE(FIELDID,114,fieldvalue,NULL)) SinalpDomicilio_N,
                   MAX(DECODE(FIELDID,113,fieldvalue,NULL)) SinalpEmpresa_N,
                   MAX(DECODE(FIELDID,116,fieldvalue,NULL)) SinalpHulera_N,
                   MAX(DECODE(FIELDID,117,fieldvalue,NULL)) SinalpManufacturerName_N,
                   MAX(DECODE(FIELDID,115,fieldvalue,NULL)) SinalpRepresentante_N,
                   MAX(DECODE(FIELDID,161,fieldvalue,NULL)) SpeedBehavior_N,
                   MAX(DECODE(FIELDID,162,fieldvalue,NULL)) TestInfo_N,
                   MAX(DECODE(FIELDID,164,fieldvalue,NULL)) TestReport_N,
                   MAX(DECODE(FIELDID,163,fieldvalue,NULL)) TestSerie_N,
                   MAX(DECODE(FIELDID,145,fieldvalue,NULL)) TireIdentification_N,
                   MAX(DECODE(FIELDID,149,fieldvalue,NULL)) Type_N,
                   MAX(DECODE(FIELDID,151,fieldvalue,NULL)) WearingDownIndicator_N,
                   MAX(DECODE(FIELDID,187,fieldvalue,NULL)) SIGNATURENAME_N,
                   MAX(DECODE(FIELDID,188,fieldvalue,NULL)) SIGNATURENTITLE_N,
                   MAX(DECODE(FIELDID,190,fieldvalue,NULL)) NominalBeadUnseat_N,
                   MAX(DECODE(FIELDID,191,fieldvalue,NULL)) NominalPlunger_N,
                   MAX(DECODE(FIELDID,192,fieldvalue,NULL)) LowPressEndurInitInfl_N
        FROM (
                SELECT FIELDID,
                   CERTIFICATENUMBER,
                   FIELDVALUE
                FROM defaultvalues_view
                WHERE lower(certificatenumber) =lower(ps_certificateNumber)  and
                      CERTIFICATIONTYPEID = 3
              )   group by CERTIFICATENUMBER ;
            
             Open pc_CertificateInfo for
                          Select   
                    CERTIFICATEID,
                    ce.CERTIFICATIONTYPEID,
                    CERTIFICATIONTYPENAME,
                    CERTIFICATENUMBER,
                     ACTIVESTATUS,
                     RENEWALREQUIRED_CGIN,
                     EXTENSION_EN,
                                  ---PRODUCTLOCATION PRODUCTLOCATION_C,
                     COUNTRYOFMANUFACTURE_N,
                     CUSTOMER CUSTOMER_N,
                     CUSTOMERSPECIFIC_N,
                     IMPORTER,
                    IMPORTERREPRESENTATIVE,
                    IMPORTERADDRESS,
                    CERTDATESUBMITTED,
                    CERTDATEAPPROVED,
                    CU.SIGNATUREIND
                        From Certificate ce 
                          inner join  certificationtype ct on
                            CT.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
                           LEFT OUTER JOIN IMPORTER IM ON 
                             CE.IMPORTERID = IM.IMPORTERID
                           LEFT OUTER JOIN CUSTOMER CU ON
                             CE.CUSTOMERID = CU.CUSTOMERID
                          Where ce.CERTIFICATIONTYPEID = 3 And
                           lower(ce.certificatenumber) = lower(ps_certificateNumber) and
                           lower(CE.MOSTRECENTCERT) = 'y'  ;
             --As per PRJ3617, 
             -- Added PSN instead of NPRID
             -- Added Matl_Num
             -- Added brand and brand_line instead of brandcode
             Open pc_Product for
                         SELECT distinct  P.SKUID,  SKU,MATL_NUM, BRAND,BRAND_LINE, TIRETYPEID, PSN,
                  SIZESTAMP, 
                   DISCONTINUEDDATE,   SPECNUMBER,   SPEEDRATING,
                 SINGLOADINDEX,   DUALLOADINDEX,   BIASBELTEDRADIAL,   TUBELESSYN,
                 REINFORCEDYN,   EXTRALOADYN,   UTQGTREADWEAR,   UTQGTRACTION,
                 UTQGTEMP,   MUDSNOWYN,   RIMDIAMETER,   SERIALDATE,   BRANDDESC,
                 LOADRANGE,   MEARIMWIDTH,   REGROOVABLEIND,   PLANTPRODUCED,
                 MOSTRECENTTESTDATE,   IMARK,   INFORMENUMBER,   FECHADATE,
                 TREADPATTERN,   SPECIALPROTECTIVEBAND,   NOMINALTIREWIDTH,
                 ASPECTRATIO,   TREADWEARINDICATORS,   NAMEOFMANUFACTURER,
                 FAMILY,   DOTSERIALNUMBER,  ce.CERTIFICATENUMBER
             ----EXTENSION_EN -- we just want the unique product records that have ever been assoc. with 
             ---this certificate.
--     FROM  productdata_report_view
--              where lower(certificatenumber) = lower(ps_certificateNumber)  and
--                    TireTypeID = pi_tiretypeid; 
--                    ----and rownum < 2;   took this out -- JES 2/25/2010
               FROM product p
           INNER JOIN productcertificate pce
             ON p.skuid = pce.skuid
          INNER JOIN certificate ce
             ON pce.certificateid = ce.certificateid
                AND pce.certificationtypeid = ce.certificationtypeid
          where lower(certificatenumber) = lower(ps_certificateNumber);
            ---and TireTypeID = pi_tiretypeid;  
            
       BEGIN  
          SELECT DISTINCT BRANDCODE   INTO ls_brandcode
             FROM product p
          INNER JOIN productcertificate pce
             ON p.skuid = pce.skuid
          INNER JOIN certificate ce
             ON pce.certificateid = ce.certificateid
                AND pce.certificationtypeid = ce.certificationtypeid
          where lower(ce.certificatenumber) = lower(ps_certificateNumber)  and
              ---TireTypeID = pi_tiretypeid AND
               LOWER(CE.MOSTRECENTCERT)='y'               ; 
       EXCEPTION
                       WHEN OTHERS THEN
                              ls_brandcode:=NULL;
       END;
               
                -- Gets the brand information
--          Open pc_brand for
--          SELECT distinct  BRANDCODE, ---get all brands that have ever been associated with this certificate, because
--                        BRANDNAME,           ---the sks's that were on the original test specs may have since been discontinued
--                        CERTIFICATENUMBER---but just get each brand once, even though it is multiple extensions.
--          FROM  brand_view
--          where lower(certificatenumber) = lower(ps_certificateNumber)  ; 
     --- use this cursor to return the sizes for this brand
            Open pc_brand for
                SELECT DISTINCT BRAND_CODE,
                SIZE_STAMP||' '||DECODE(nvl(S.LOAD_RANGE,' '),'XL','XL', 'RE','RE',' ')  SIZESTAMP,
                CASE WHEN(pi_tiretypeid= 1) THEN 1
                        WHEN(pi_tiretypeid= 3) THEN 
                               ( CASE WHEN (SLOAD_IDX>112) THEN 2
                                          ELSE 1
                               END)
                          ELSE 1
                   END SLOAD_IDX
                   FROM  BOM_DATA.SKU_MASTER_MV S
                   WHERE BRAND_CODE =  ls_brandcode
                   AND NVL (disc_date, SYSDATE) > '01-NOV-1991'; --lets only get the more recent sku's
                  --- and disc_date is null; -- hasn't been discontined 
                   -- nope - now Mario said to include everything, even ones that are discontinued - 4/28/2011
          
          
            Open pc_MeasureHDR for
            SELECT  MEASUREID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    INFLATIONPRESSURE,
                    MOLDDESIGN,
                    RIMWIDTH,
                    DOTSERIALNUMBER,
                    DIAMETER,
                    AVGSECTIONWIDTH,
                    AVGOVERALLWIDTH,
                    MAXOVERALLWIDTH,
                    SIZEFACTOR,
                    MOUNTTIME,
                    MOUNTTEMP,
                    SERIALDATE,
                    ENDTIME,
                    ACTSIZEFACTOR,
                    ce.CERTIFICATIONTYPEID,
                    CERTIFICATENUMBER,                   
                    STARTINFLATIONPRESSURE,
                    ENDINFLATIONPRESSURE,
                    ADJUSTMENT,
                    CIRCUMFERENCE,
                    NOMINALDIAMETER,
                    NOMINALWIDTH,
                    NOMINALWIDTHPASSFAIL,
                    NOMINALWIDTHDIFERENCE,
                    NOMINALWIDTHTOLERANCE,
                    MAXOVERALLDIAMETER,
                    MINOVERALLDIAMETER,
                    OVERALLWIDTHPASSFAIL,
                    OVERALLDIAMETERPASSFAIL,
                    DIAMETERDIFERENCE,
                    DIAMETERTOLERANCE,
                    TEMPRESISTANCEGRADING,
                    TENSILESTRENGHT1,
                    TENSILESTRENGHT2,
                    ELONGATION1,
                    ELONGATION2,
                    TENSILESTRENGHTAFTERAGE1,
                    TENSILESTRENGHTAFTERAGE2,
                    SKU
              FROM Certificate ce inner join MEASUREHDR m on 
                       ce.certificateid = m.certificateid and
                       ce.certificationtypeid = m.certificationtypeid
                   where ce.certificationtypeid = 3 and
                    lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                    and upper(CE.MOSTRECENTCERT) = 'Y';
              
              open pc_measureDtl for
              SELECT md.MEASUREID,
                    SECTIONWIDTH,
                    OVERALLWIDTH,
                    ITERATION
              FROM Certificate ce inner join MEASUREHDR m on
                       ce.certificateid = m.certificateid and
                       ce.certificationtypeid = m.certificationtypeid and
                       lower(CE.MOSTRECENTCERT) = 'y'
                    inner join MEASUREDTL md on 
                           m.measureid = md.measureid
              where m.certificationtypeid = 3 and
                    lower(ce.certificatenumber) = lower(ps_CertificateNumber);
              
              open pc_BEADUNSEATHDR for
              SELECT BEADUNSEATID,
                    PROJECTNUMBER,
                    TIRENUMBER,
                    TESTSPEC,
                    COMPLETIONDATE,
                    DOTSERIALNUMBER,
                    LOWESTUNSEATVALUE,
                    PASSYN,
                    b.CERTIFICATIONTYPEID,
                    CERTIFICATENUMBER,
                    SERIALDATE,
                    MINBEADUNSEAT,                    
                    TESTPASSFAIL,
                    SKU
             FROM Certificate ce inner join BEADUNSEATHDR b on
                       ce.certificateid = b.certificateid and
                       ce.certificationtypeid = b.certificationtypeid
                     where b.certificationtypeid = 3 and
                     lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                    AND lower(CE.MOSTRECENTCERT) = 'y';
                    
               open pc_BEADUNSEATDTL for   
               SELECT bd.BEADUNSEATID,
                      UNSEATFORCE,
                      ITERATION
               FROM Certificate ce 
                             inner join BEADUNSEATHDR b on
                                    ce.certificateid = b.certificateid and
                                    ce.certificationtypeid = b.certificationtypeid
                             inner join BEADUNSEATDTL bd on
                                   b.beadunseatid = bd.beadunseatid                                   
               where b.certificationtypeid = 3 and
                     lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                     AND lower(CE.MOSTRECENTCERT) = 'y'; 
                          
               open pc_PLUNGERHDR for 
               SELECT PLUNGERID,
                      PROJECTNUMBER,
                      TIRENUMBER,
                      TESTSPEC,
                      COMPLETIONDATE,
                      DOTSERIALNUMBER,
                      AVGBREAKINGENERGY,
                      PASSYN,
                      p.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER,
                      SERIALDATE,
                      MINPLUNGER,
                      SKU
                 FROM Certificate ce 
                             inner join  PLUNGERHDR p on
                                    ce.certificateid = p.certificateid and
                                    ce.certificationtypeid = p.certificationtypeid
                where p.certificationtypeid = 3 and
                       lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                       AND lower(CE.MOSTRECENTCERT) = 'y';
                       
                open pc_PLUNGERDTL for
                SELECT pd.PLUNGERID,
                        BREAKINGENERGY,
                        ITERATION 
                 FROM Certificate ce 
                             inner join PLUNGERHDR ph on
                                   ce.certificateid       = ph.certificateid and
                                   ce.certificationtypeid = ph.certificationtypeid
                             inner join PLUNGERDTL pd on
                                  ph.plungerid = pd.plungerid
                where ph.certificationtypeid = 3 and
                      lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                      AND lower(CE.MOSTRECENTCERT) = 'y';           
                                   
                
                open pc_TREADWEARHDR for
                SELECT TREADWEARID,
                      PROJECTNUMBER,
                      TIRENUMBER,
                      TESTSPEC,
                      COMPLETIONDATE,
                      DOTSERIALNUMBER,
                      LOWESTWEARBAR,
                      PASSYN,
                      SERIALDATE,
                      t.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER,
                      INDICATORSREQUIREMENT,
                      SKU
                FROM Certificate ce 
                             inner join TREADWEARHDR t on
                                   ce.certificateid       = t.certificateid and
                                   ce.certificationtypeid = t.certificationtypeid
                where t.certificationtypeid = 3 and
                       lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                        and lower(CE.MOSTRECENTCERT) = 'y';
                
                 open pc_TREADWEARDTL for
                  SELECT  td.TREADWEARID,
                          WEARBARHEIGHT,
                          ITERATION
                 FROM Certificate ce 
                             inner join TREADWEARHDR t on
                                   ce.certificateid       = t.certificateid and
                                   ce.certificationtypeid = t.certificationtypeid
                            inner join TREADWEARDTL td on
                                   t.treadwearid = td.treadwearid
                where t.certificationtypeid = 3 and
                      lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                      AND lower(CE.MOSTRECENTCERT) = 'y'; 
              
                open pc_ENDURANCEHDR for
                SELECT e.ENDURANCEID,
                      PROJECTNUMBER,
                      TIRENUMBER,
                      TESTSPEC,
                      COMPLETIONDATE,
                      DOTSERIALNUMBER,
                      MFGWWYY,
                      PRECONDSTARTDATE,
                      PRECONDSTARTTEMP,
                      RIMDIAMETER,
                      RIMWIDTH,
                      PRECONDENDDATE,
                      PRECONDENDTEMP,
                      INFLATIONPRESSURE,
                      BEFOREDIAMETER,
                      AFTERDIAMETER,
                      BEFOREINFLATION,
                      AFTERINFLATION,
                      WHEELPOSITION,
                      WHEELNUMBER,
                      FINALTEMP,
                      FINALDISTANCE,
                      FINALINFLATION,
                      POSTCONDSTARTDATE,
                      POSTCONDENDDATE,
                      POSTCONDENDTEMP,
                      PASSYN,
                      e.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER,
                      SERIALDATE,
                      PRECONDTIME,
                      POSTCONDTIME,                     
                      DIAMETERTESTDRUM,
                      PRECONDTEMP,
                      INFLATIONPRESSUREREADJUSTED,
                      CIRCUNFERENCEBEFORETEST,
                      RESULTPASSFAIL,
                      ENDURANCEHOURS,
                      POSSIBLEFAILURESFOUND,
                      CIRCUNFERENCEAFTERTEST,
                      OUTERDIAMETERDIFERENCE,
                      ODDIFERENCETOLERANCE,
                      SERIENOM,
                      FINALJUDGEMENT,
                      APPROVER,
                      SKU, SPEED,
                      LOWPRESSURESTARTINFLATION,
                      LOWPRESSUREENDINFLATION,
                      LOWPRESSUREENDTEMP
                    FROM Certificate ce inner join ENDURANCEHDR e on
                               ce.certificateid = e.certificateid and
                               ce.certificationtypeid = e.certificationtypeid 
                          INNER JOIN (select enduranceid, max(speed) SPEED from
                              ENDURANCEDTL ed 
                              where ed.TESTSTEP <= 1 group by enduranceid
                                ) eds
                              on eds.enduranceid = e.ENDURANCEID
                              
                    where e.certificationtypeid = 3 and
                       lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                       AND lower(CE.MOSTRECENTCERT) = 'y';
                       
                    open pc_ENDURANCEDTL for
                    SELECT TESTSTEP,
                      TIMEINMIN,
                      SPEED,
                      TOTMILES,
                      LOAD,
                      LOADPERCENT,
                      SETINFLATION,
                      AMBTEMP,
                      INFPRESSURE,
                      STEPCOMPLETIONDATE,
                      ed.ENDURANCEID
                    FROM Certificate ce 
                              inner join ENDURANCEHDR e on
                                     ce.certificateid = e.certificateid and
                                     ce.certificationtypeid = e.certificationtypeid 
                              inner join ENDURANCEDTL ed on 
                                     e.enduranceid = ed.enduranceid
                    where e.certificationtypeid = 3 and
                          lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                          AND lower(CE.MOSTRECENTCERT) = 'y';              

                    open pc_HIGHSPEEDHDR for
                    SELECT H.HIGHSPEEDID,
                      PROJECTNUMBER,
                      TIRENUM,
                      TESTSPEC,
                      COMPETIONDATE,
                      DOTSERIALNUMBER,
                      MFGWWYY,
                      PRECONDSTARTDATE,
                      PRECONDSARTTEMP,
                      RIMDIAMETER,
                      RIMWIDTH,
                      PRECONDENDDATE,
                      PRECONDENDTEMP,
                      INFLATIONPRESSURE,
                      BEFOREDIAMETER,
                      AFTERDIAMETER,
                      BEFOREINFLATION,
                      AFTERINFLATION,
                      WHEELPOSITION,
                      WHEELNUMBER,
                      FINALTEMP,
                      FINALDISTANCE,
                      FINALINFLATION,
                      POSTCONDSTARTDATE,
                      POSTCONDENDDATE,
                      POSTCONDENDTEMP,
                      PASSYN,
                      SERIALDATE,
                      POSTCONDTIME,
                      h.CERTIFICATIONTYPEID,
                      CERTIFICATENUMBER, 
                      DIAMETERTESTDRUM,
                      PRECONDTIME,
                      PRECONDTEMP,
                      INFLATIONPRESSUREREADJUSTED,
                      CIRCUNFERENCEBEFORETEST,
                      WHEELSPEEDRPM,
                      WHEELSPEEDKMH,
                      CIRCUNFERENCEAFTERTEST,
                      ODDIFERENCE,
                      ODDIFERENCETOLERANCE,
                      SERIENOM,
                      FINALJUDGEMENT,
                      APPROVER,
                      PASSATKMH,
                      SPEEDTTESTPASSFAIL,
                      SPEEDTOTALTIME,
                      MAXSPEED,
                       HDM.load MAXLOAD,
                      SKU
                    FROM Certificate ce inner join HIGHSPEEDHDR h on
                               ce.certificateid = h.certificateid and
                               ce.certificationtypeid = h.certificationtypeid 
                           inner join (select hd.highspeedid, max(load) LOAD  from highspeeddtl hd
                                where HD.TESTSTEP <= 1 ---Mario wanted this added so it just pulls the load from the first step, so that he only
                                                                        ---has to change one of them, and they are really all the same. - had to do it with
                                                                        ---the  <= so that if it doesn't have a step one, it won't error out.
                                                                         ---jes 5/10/11   
                                group by hd.highspeedid) hdm on
                                H.HIGHSPEEDID = HDM.HIGHSPEEDID
                    where h.certificationtypeid = 3 and
                          lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                          AND lower(CE.MOSTRECENTCERT) = 'y';
                            


                  open pc_HIGHSPEEDDTL for
                  SELECT TESTSTEP,
                    TIMEINMIN,
                    SPEED,
                    TOTMILES,
                    LOAD,
                    LOADPERCENT,
                    SETINFLATION,
                    AMBTEMP,
                    INFPRESSURE,
                    STEPCOMPLETIONDATE,
                    hd.HIGHSPEEDID
                  FROM Certificate ce 
                              inner join HIGHSPEEDHDR h on
                                     ce.certificateid = h.certificateid and
                                     ce.certificationtypeid = h.certificationtypeid 
                              inner join HIGHSPEEDDTL hd on 
                                     h.highspeedid = hd.highspeedid
                    where h.certificationtypeid = 3 and
                          lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                          AND lower(CE.MOSTRECENTCERT) = 'y';
               
               
                  
                  open pc_SPEEDTESTDETAIL for
                  SELECT ITERATION,
                      TIME,
                        SPEED,
                        s.HIGHSPEEDID
                  FROM Certificate ce 
                              inner join HIGHSPEEDHDR h  on
                                     ce.certificateid = h.certificateid and
                                     ce.certificationtypeid = h.certificationtypeid 
                              inner join SPEEDTESTDETAIL s on
                                     h.highspeedid = s.highspeedid
                  WHERE h.certificationtypeid = 3 and
                        lower(ce.certificatenumber) = lower(ps_CertificateNumber)
                        AND lower(CE.MOSTRECENTCERT) = 'y';
                        
        
  EXCEPTION     
          when li_ParametersAreNull then           
            ls_ErrorMsg:=  sqlerrm ||  '-GetNOMCertification.There is at least one parameters null.'  ;
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetNOMCertification',
                AX_RECORDDATA    => 'ps_sku is parameters null..',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20005,ls_ErrorMsg);
            
         when li_ParametersAreInvalid then           
            ls_ErrorMsg:=  sqlerrm || '-GetNOMCertification. There is at least one parameters invalid.';
             APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
                ad_OPERATORID => ls_OperatorId,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   => ' Reports_Package.GetNOMCertification',
                AX_RECORDDATA    => 'There is at least one parameters invalid.',
                AS_MESSAGECODE   => to_char(sqlcode),
                AS_MESSAGE       => ls_ErrorMsg);  
           raise_application_error (-20006,ls_ErrorMsg);
         when others then            
              ls_ErrorMsg:=  sqlerrm || '-GetNOMCertification. An error have ocurred.(when others)' || sqlerrm;
               APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
                      ad_OPERATORID => ls_OperatorId,
                      AD_DATERECORDED  => sysdate,
                      AS_PROCESSNAME   =>' Reports_Package.GetNOMCertification',
                      AX_RECORDDATA    => 'An error have ocurred.(when others)',
                      AS_MESSAGECODE   => to_char(sqlcode),
                      AS_MESSAGE       =>ls_ErrorMsg);
               raise_application_error (-20007,ls_ErrorMsg);
  end GetNOMCertification;
  /
  
  create or replace PROCEDURE           GetTireCharacteristics(
          ps_Matl_Num          IN  PRODUCT.MATL_NUM%type,
          pn_TIRETYPEID        OUT PRODUCT.TIRETYPEID%TYPE,
          ps_SPEEDRATING       OUT PRODUCT.SPEEDRATING %TYPE,
          ps_SINGLOADINDEX     OUT PRODUCT.SINGLOADINDEX%TYPE,
          ps_TUBELESSYN        OUT PRODUCT.TUBELESSYN%TYPE,
          ps_REINFORCEDYN      OUT PRODUCT.REINFORCEDYN%TYPE,
          ps_EXTRALOADYN       OUT PRODUCT.EXTRALOADYN%TYPE,
          ps_UTQGTEMP          OUT PRODUCT.UTQGTEMP%TYPE,
          ps_UTQGTRACTION      OUT PRODUCT.UTQGTRACTION%TYPE,
          ps_UTQGTREADWEAR     OUT PRODUCT.UTQGTREADWEAR%TYPE,
          ps_LOADRANGE         OUT PRODUCT.LOADRANGE%TYPE,
          ps_TPN               OUT PRODUCT.TPN%TYPE,
          ps_BIASBELTEDRADIAL  OUT PRODUCT.BIASBELTEDRADIAL%TYPE)
IS

/******************************************************************************
   NAME:       GetTireCharacteristics
   PURPOSE:    

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        9/20/2012   jeseitz          Created this procedure.
   1.1        10/3/2012   Harini           Modified the procedure such that it
                                           multiple queries are removed and 
                                           consolidated to single query.
******************************************************************************/

 tmpVar NUMBER;
 ls_ErrorMsg App_Message.Message%TYPE;
 
BEGIN

    SELECT 
        CASE  WHEN  PRODUCT_TYPE = 'LIGHT TRUCK TIRE' THEN 3
              WHEN  PRODUCT_TYPE = 'SPECIALTY TIRE'   THEN 4
              WHEN  PRODUCT_TYPE = 'PASSENGER TIRE'   THEN 1
              WHEN  PRODUCT_TYPE = 'CYCLE TIRE'       THEN 0
              WHEN  PRODUCT_TYPE LIKE 'TRUCK _ BUS TIRE' THEN 7
         END TIRETYPEID 
         ,SPEED_RATING
         ,SINGLE_LOAD_INDEX
         ---,DECODE(TUBE_TYPE,'TUBELESS','N','Y')
          ,DECODE(TUBE_TYPE,'TUBELESS','Y','N') -- JESEITZ 10/31/12
         ,LOAD_RANGE
         ,DECODE(NVL(REINFORCEDYN,' '),'RE','Y','N')
         ,UTQG_TEMPERATURE
         ,UTQG_TRACTION
         ,UTQG_TREADWEAR
         ,TECHNICAL_PLATFORM
         ,DECODE(RMA_TIRE_PLY_CONSTRUCTION,'BIAS-BELTED','BELTED','R','RADIAL','2','BIAS','3','BELTED',RMA_TIRE_PLY_CONSTRUCTION) 
     INTO 
          pn_TIRETYPEID,
          ps_SpeedRating,
          ps_SINGLOADINDEX,
          ps_TUBELESSYN,
          ps_LoadRange,
          ps_REINFORCEDYN,
          ps_UTQGTEMP,
          ps_UTQGTRACTION,
          ps_UTQGTREADWEAR,
          ps_TPN,
          ps_BIASBELTEDRADIAL
     FROM
     (SELECT  LPAD(Matl_Num,18,0) AS Matl_Num 
             ,MAX(DECODE(Attrib_Name,'PRODUCT_TYPE',Attrib_Value))     AS  PRODUCT_TYPE
             ,MAX(DECODE(Attrib_Name,'SPEED_RATING',Attrib_Value))     AS  SPEED_RATING
             ,MAX(DECODE(Attrib_Name,'STAMPED_SINGLE_LOAD_INDEX',Attrib_Value))  AS SINGLE_LOAD_INDEX
             ,MAX(DECODE(Attrib_Name,'TUBE_TYPE',Attrib_Value))        AS  TUBE_TYPE
             ,MAX(DECODE(Attrib_Name,'LOAD_RANGE',Attrib_Value))       AS  LOAD_RANGE
             ,MAX(DECODE(Attrib_Name,'LOAD_RANGE',Attrib_Value))       AS  REINFORCEDYN
             ,MAX(DECODE(Attrib_Name,'UTQG_TEMPERATURE',Attrib_Value)) AS  UTQG_TEMPERATURE
             ,MAX(DECODE(Attrib_Name,'UTQG_TRACTION',Attrib_Value))    AS  UTQG_TRACTION
             ,MAX(DECODE(Attrib_Name,'UTQG_TREADWEAR',Attrib_Value))   AS  UTQG_TREADWEAR
             ,MAX(DECODE(Attrib_Name,'TECHNICAL_PLATFORM',Attrib_Value))AS  TECHNICAL_PLATFORM
             ,MAX(DECODE(Attrib_Name,'RMA_TIRE_PLY_CONSTRUCTION',Attrib_Value))  AS RMA_TIRE_PLY_CONSTRUCTION
     FROM ( SELECT ma.*,
                   DENSE_RANK() OVER(PARTITION BY LPAD(ma.Matl_Num,18,0), ma.Attrib_Name ORDER BY ma.Counter DESC) rk
              FROM Material_Attribute ma
             WHERE Attrib_Name IN ('PRODUCT_TYPE' ,'SPEED_RATING' ,'STAMPED_SINGLE_LOAD_INDEX','TUBE_TYPE','LOAD_RANGE',
                                   'UTQG_TEMPERATURE','UTQG_TRACTION','UTQG_TREADWEAR','TECHNICAL_PLATFORM',
                                   'RMA_TIRE_PLY_CONSTRUCTION')
               AND Matl_Num = LPAD(ps_Matl_Num,18,0) )
    WHERE rk = 1
    GROUP BY LPAD(Matl_Num,18,0));
     
    IF NVL(ps_LoadRange, ' ') = 'C' AND pn_TIRETYPEID =  1 THEN
              ps_EXTRALOADYN := 'Y';
     ELSIF NVL (ps_LoadRange, ' ') = 'XL'  THEN
              ps_EXTRALOADYN := 'Y';
     ELSE
             ps_EXTRALOADYN :=  'N';
     END IF;  

   EXCEPTION
      WHEN OTHERS THEN
       -- Consider logging the error and then re-raise
         ls_ErrorMsg:= sqlerrm ||  ' - GetTireCharacteristics. An error have ocurred.(when others)' || sqlerrm;
         APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => NULL,
                                                    ad_OPERATORID => 'ICSDEV',
                                                    AD_DATERECORDED  => SYSDATE,
                                                    AS_PROCESSNAME   =>' GetTireCharacteristics',
                                                    AX_RECORDDATA    => 'ps_matl_num - '||ps_Matl_Num,
                                                    AS_MESSAGECODE   => to_char(SQLCODE),
                                                    AS_MESSAGE       =>ls_ErrorMsg);
           RAISE_APPLICATION_ERROR(-20010,ls_ErrorMsg);
END GetTireCharacteristics;
/

create or replace PROCEDURE           Unload_CertificateActivity  (
--unloads certificate information for loading into SAP
     ad_ProcessDate     IN DATE,
     as_ExtractFileName IN VARCHAR2,
     as_OKFileName      IN VARCHAR2)
IS

/***************************************************************************************************
   NAME:       Unload_CertificateActivity
   PURPOSE:    

   REVISIONS:
   Ver        Date        Author          Description
   ---------  ----------  --------------  ----------------------------------------------------------
   1.0         2/8/2012   jeseitz         1. Created this procedure.

   1.1         4/7/2014   jeseitz         correct handling of emark extensions per project 5056, 
                                            tech spec 4
   2.0        10/8/2014   Joe Hill        Added processing for Operations Approval Date
   2,1        11/10/2014  Jill Seitz      removed 'date_removed is not null ' from lc_deletes cursor selection
****************************************************************************************************/
        ls_recordcode                   VARCHAR2(4);
        ls_TextFile                     UTL_FILE.FILE_TYPE;
        ls_TextLine                     VARCHAR2(400);
        ls_OutTextFile                  UTL_FILE.FILE_TYPE;
        ls_OutFileName                  VARCHAR2(12);

    --  miscellaneous variables
        ln_RecCnt                       NUMBER;
        lr_Rowid                        ROWID;
        ld_ProcessDate                  DATE;

    --  error processing variables
        ln_ErrCnt                       NUMBER := 0;
        ln_MoreCursor                   NUMBER := 0;
        ls_ErrorData                    VARCHAR2(2000);
        ls_ErrorCode                    VARCHAR2(20);
        ls_ErrorMsg                     VARCHAR2(4000);
        ln_NumChars                     NUMBER;
        ls_EventMsg                     VARCHAR2(100);
        ln_count                        NUMBER;

        ld_MaxEndDate                   DATE;
        ld_StartDate                    DATE;
        ls_HoldCertificationTypeId      NUMBER;
        ln_HoldMatlNum                  VARCHAR2(18);
        ls_MACHINEID                    VARCHAR2(50):=null;
        ls_OPERATORID                   VARCHAR2(50):='ICSDEV';
        ln_activecerts                  NUMBER;
        ln_activeDeletes                NUMBER;                                                     --JBH_2.0

    --  THIS SQL STATEMENT EXTRACTS RECORDS FOR EXPORTING TO SAP.
    --  TABLES PRODUCT, PRODUCTCERTIFICATE, CERTIFICATE, CERTIFICATETYPE, AND COUNTRY ARE JOINED.
    --  IF A RECORD IN ANY ONE OF THESE TABLES HAS CHANGED (MODIFIEDON DATE = TODAY) OR 
    --  IF THE CERTIFICATE BECAME INVALID FOR THE PRODUCT YESTERDAY (NO LONGER IN EFFECT)  
    --  THEN WE EXTRACT A RECORD BECAUSE SOMETHING MAY HAVE CHANGED THAT AFFECTS THE VALIDITY OF THE CERTIFICATE FOR THIS PRODUCT.
    CURSOR  lc_certs  IS
            SELECT      DISTINCT
                        'US' COUNTRY,
                        CT.CERTIFICATIONTYPEID,
                        UPPER(CT.CERTIFICATIONTYPENAME) CERTIFICATIONTYPENAME, 
                        ' ' CERTIFICATIONDESCRIPTION, 
                        'Y' CERTIFICATIONREQUIRED,
                        ' ' LANGUAGE,
                        P.MATL_NUM MATERIALNUMBER, 
                        P.SKU, 
                        P.SIZESTAMP,
                        ( CASE
                            WHEN  (NVL(P.BRAND,'X') <> 'X' )THEN  
                                  P.BRAND||' '||P.BRAND_LINE      --USE BRAND + BRAND_LINE IF AVAILABLE
                            ELSE  P.BRANDDESC
                            END)  BRANDDESC,

--                        COALESCE(PC.DATEAPPROVED_CEGI, C.CERTDATEAPPROVED) START_DATE,            --JBH_2.0
                        ( case                                                                      --JBH_2.0
                            WHEN  CT.CERTIFICATIONTYPEID = 2 OR CT.CERTIFICATIONTYPEID = 3          --JBH_2.0
                            THEN  COALESCE(PC.DATEAPPROVED_CEGI, C.CERTDATEAPPROVED)                --JBH_2.0
                            ELSE  PC.OPER_DATE_APPROVED                                             --JBH_2.0
                          end)    START_DATE,                                                       --JBH_2.0

                        ( case
                            --  ECE30/54 Certificates Do Not Expire - Use Entered Date Removed
                            WHEN CT.CERTIFICATIONTYPEID = 1 THEN PC.DATEREMOVED
                            --  GSO Certificates are Valid for One Year - Use Date Removed or One Year after Approval Date
                            WHEN CT.CERTIFICATIONTYPEID = 2 THEN COALESCE(PC.DATEREMOVED, PC.DATEAPPROVED_CEGI + 365)
                            --  Imark Certificates are valid for 4 years - Use Date Removed, Expiration Date, or 4 Years from Approval Date
                            WHEN CT.CERTIFICATIONTYPEID = 4 THEN COALESCE(PC.DATEREMOVED, C.EXPIRYDATE_I, PC.DATEAPPROVED_CEGI + 1460) 
                            --  CCC Certificates Do Not Automatically Expire - Use Date Removed or Entered Expiration Date
                            WHEN CT.CERTIFICATIONTYPEID = 5 THEN COALESCE(PC.DATEREMOVED, C.EXPIRYDATE_I)
                            --  ECE117 Certificates Do Not Expire - Use Entered Date Removed
                            WHEN CT.CERTIFICATIONTYPEID = 6 THEN PC.DATEREMOVED
                            --  India Mark Certificates are Valid for One Year - Use Date Removed, Expiration Date, or One Year after Approval Date
                            WHEN CT.CERTIFICATIONTYPEID = 7 THEN COALESCE(PC.DATEREMOVED, C.EXPIRYDATE_I, PC.DATEAPPROVED_CEGI + 365) 
                          end)    END_DATE,
                      'U'         CHG_FLAG
            FROM      PRODUCT P, 
                      PRODUCTCERTIFICATE PC,
                      CERTIFICATE C,   
                      certificationtype ct, 
                      country cty
            WHERE     PC.CERTIFICATEID = C.CERTIFICATEID
                      AND P.SKUID = PC.SKUID
                      AND CT.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
                      AND CTY.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
                      and PC.DATEAPPROVED_CEGI is not null
                      AND ( case                                                                    --JBH_2.0
                              WHEN  CT.CERTIFICATIONTYPEID = 2 OR CT.CERTIFICATIONTYPEID = 3        --JBH_2.0
                              THEN  COALESCE(PC.DATEAPPROVED_CEGI, C.CERTDATEAPPROVED)              --JBH_2.0
                              ELSE  PC.OPER_DATE_APPROVED                                           --JBH_2.0
                            end)    IS NOT NULL                                                     --JBH_2.0
                      and ( (CTY.MODIFIEDON)    >=  ld_ProcessDate 
                            or  (P.MODIFIEDON)  >=  ld_ProcessDate
                            or  (CT.MODIFIEDON) >=  ld_ProcessDate
                            or  (PC.MODIFIEDON) >=  ld_ProcessDate
                            or  (c.modifiedon)  >=  ld_ProcessDate)
                      and c.certificationtypeid <>  3             --  don't extract NOM
                      and substr( P.MATL_NUM,1,9 ) = '000000090'  --  make sure this is a real material number
                      and SUBSTR(CTY.SAP_COUNTRY_KEY,1,1) <> '*' 
            ORDER BY  P.MATL_NUM, 
                      CT.CERTIFICATIONTYPEID;

    lcr_certs lc_certs%ROWTYPE;

--  JBH_2.0 - Added lc_deletes cursor
    CURSOR  lc_deletes IS
            SELECT    DISTINCT
                      'US' COUNTRY, -- COUNTRY CODE IS NOT USED.
                      CT.CERTIFICATIONTYPEID,
                      UPPER(CT.CERTIFICATIONTYPENAME) CERTIFICATIONTYPENAME,
                      ' ' CERTIFICATIONDESCRIPTION, 
                      'Y' CERTIFICATIONREQUIRED,
                      ' ' LANGUAGE,
                      P.MATL_NUM MATERIALNUMBER, P.SKU, P.SIZESTAMP,
                      (CASE WHEN (NVL(P.BRAND,'X') <> 'X' ) 
                            THEN  P.BRAND||' '||P.BRAND_LINE -- Use Brand/Brand_Line If Available
                            ELSE  P.BRANDDESC
                       END) BRANDDESC,
                      COALESCE(PC.DATEAPPROVED_CEGI, C.CERTDATEAPPROVED) START_DATE,
                      (CASE 
                            --  ECE30/54 Certificates Do Not Expire - Use Entered Date Removed
                            WHEN CT.CERTIFICATIONTYPEID = 1 THEN  PC.DATEREMOVED
                            --  GSO Certificates are Valid for One Year - Use Date Removed or One Year after Approval Date
                            WHEN CT.CERTIFICATIONTYPEID = 2 THEN  COALESCE(PC.DATEREMOVED, PC.DATEAPPROVED_CEGI + 365)
                            --  Imark Certificates are valid for 4 years - Use Date Removed, Expiration Date, or 4 Years from Approval Date
                            WHEN CT.CERTIFICATIONTYPEID = 4 THEN  COALESCE(PC.DATEREMOVED, C.EXPIRYDATE_I,PC.DATEAPPROVED_CEGI + 1460) 
                            --  CCC Certificates Do Not Automatically Expire - Use Date Removed or Entered Expiration Date
                            WHEN CT.CERTIFICATIONTYPEID = 5 THEN  COALESCE(PC.DATEREMOVED,  C.EXPIRYDATE_I)
                            --  ECE117 Certificates Do Not Expire - Use Entered Date Removed
                            WHEN CT.CERTIFICATIONTYPEID = 6 THEN  PC.DATEREMOVED
                            --  India Mark Certificates are Valid for One Year - Use Date Removed, Expiration Date, or One Year after Approval Date
                            WHEN CT.CERTIFICATIONTYPEID = 7 THEN  COALESCE(PC.DATEREMOVED, C.EXPIRYDATE_I,PC.DATEAPPROVED_CEGI + 365) 
                       END) END_DATE,
                      PC.OPER_DATE_APPROVED,
                      'D' CHG_FLAG --- setup for deleting
            FROM      PRODUCT P, 
                      PRODUCTCERTIFICATE PC,
                      CERTIFICATE C,   
                      CERTIFICATIONTYPE CT, 
                      COUNTRY CTY
            WHERE     PC.CERTIFICATEID = C.CERTIFICATEID
                      AND P.SKUID = PC.SKUID
                      AND CT.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
                      AND  CTY.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
                      AND ( (CTY.MODIFIEDON)    >=  ld_ProcessDate 
                            or  (P.MODIFIEDON)  >=  ld_ProcessDate
                            or  (CT.MODIFIEDON) >=  ld_ProcessDate
                            or  (PC.MODIFIEDON) >=  ld_ProcessDate
                            or  (C.MODIFIEDON)  >=  ld_ProcessDate
                          )
                      AND C.CERTIFICATIONTYPEID <> 3              --  Exclude NOM Certificates
                      AND SUBSTR(P.MATL_NUM,1,9)  = '000000090'   --  Finished Goods Only
                      AND SUBSTR(CTY.SAP_COUNTRY_KEY,1,1) <>  '*' 
                      AND ( (CT.CERTIFICATIONTYPEID <> 2 and PC.OPER_DATE_APPROVED IS NULL)  
                            or  PC.DATEAPPROVED_CEGI is null
                        --    or  PC.DATEREMOVED IS NOT NULL --- jeseitz 11/10/2014 date removed gets set as an end date in first cursor, so these do not need deleted.
                            or  Lower(C.ACTIVESTATUS) = 'n'
                          )
            ORDER BY  P.MATL_NUM, 
                      CT.CERTIFICATIONTYPEID;

    lcr_deletes lc_deletes%ROWTYPE;


  BEGIN
      ld_ProcessDate              := nvl(ad_ProcessDate,sysdate);
      ld_ProcessDate              := trunc(ld_ProcessDate);


  /*==========================================================================*/
  /*  Process SAP Additions and Updates 
  ==========================================================================*/

  --  Initialize the Row Counter
      ln_RecCnt := 0;

  --  Initialize Key Change Work Variables 
      ls_HoldCertificationTypeId  := 0;
      ln_HoldMatlNum              := 0;
      
  --  Open the SAP Extract File 
      ls_TextFile := UTL_FILE.FOPEN('ICS_OUTPUT_DIR', as_ExtractFileName,'W');

  --  Read Through Changed Certifications
      OPEN lc_certs;
      LOOP
          FETCH lc_certs into lcr_certs;
          EXIT WHEN lc_certs%NOTFOUND;

      --  Write One Line per Certification Type and Material Number
          IF  ls_HoldCertificationTypeId  <>  lcr_certs.certificationtypeid or
              ln_HoldMatlNum              <>  lcr_certs.materialnumber 
          THEN

          --  Initialize Start Date and End Date Work Variables
              ld_MaxEndDate := null;
              ld_StartDate  := null;

          --  Check To See If There Are Any Currently Active Certificates
              SELECT  count(*) 
              INTO    ln_activecerts
              FROM    CERTIFICATE C1, 
                      PRODUCTCERTIFICATE PC1, 
                      PRODUCT P1
              WHERE   C1.CERTIFICATEID  = PC1.CERTIFICATEID
                      AND PC1.SKUID     = P1.SKUID
                      AND P1.MATL_NUM   = lcr_certs.MATERIALNUMBER
                      AND PC1.CERTIFICATIONTYPEID = lcr_certs.CERTIFICATIONTYPEID
                      AND UPPER(C1.ACTIVESTATUS)  = 'Y'
                      AND (PC1.DATEAPPROVED_CEGI IS NOT NULL or C1.CERTDATEAPPROVED IS NOT NULL)
                      AND ( CASE                                                                    --JBH_2.0
                              WHEN  PC1.CERTIFICATIONTYPEID = 2 or PC1.CERTIFICATIONTYPEID = 3      --JBH_2.0
                              THEN  COALESCE(PC1.DATEAPPROVED_CEGI, C1.CERTDATEAPPROVED)            --JBH_2.0
                              ELSE  PC1.OPER_DATE_APPROVED                                          --JBH_2.0
                            END)    IS NOT NULL                                                     --JBH_2.0
                      AND PC1.DATEREMOVED IS NULL ;
           
          --  If the Material Id has Multiple Active Certificates  
          --  Retrieve the Earliest Start Date and the Latest Expiration Date 
              IF  ln_activecerts > 0 THEN

                  SELECT  
                          MAX(CASE  -- See the lc_certs Cursor Definition for an Explanation of Following Date Calculations  
                                WHEN C1.CERTIFICATIONTYPEID = 1 THEN  NULL
                                WHEN C1.CERTIFICATIONTYPEID = 2 THEN  PC1.DATEAPPROVED_CEGI + 365
                                WHEN C1.CERTIFICATIONTYPEID = 4 THEN  COALESCE(C1.EXPIRYDATE_I,PC1.DATEAPPROVED_CEGI + 1460) 
                                WHEN C1.CERTIFICATIONTYPEID = 5 THEN  C1.EXPIRYDATE_I
                                WHEN C1.CERTIFICATIONTYPEID = 6 THEN  NULL
                                WHEN C1.CERTIFICATIONTYPEID = 7 THEN  COALESCE( C1.EXPIRYDATE_I,PC1.DATEAPPROVED_CEGI + 365) 
                              END),
                          MIN(CASE                                                                    --JBH_2.0
                                WHEN  PC1.CERTIFICATIONTYPEID = 2 or PC1.CERTIFICATIONTYPEID = 3      --JBH_2.0
                                THEN  COALESCE(PC1.DATEAPPROVED_CEGI, C1.CERTDATEAPPROVED)            --JBH_2.0
                                ELSE  PC1.OPER_DATE_APPROVED                                          --JBH_2.0
                              END)                                                                    --JBH_2.0
                  INTO    ld_MaxEndDate, 
                          ld_StartDate
                  FROM    CERTIFICATE C1, 
                          PRODUCTCERTIFICATE PC1, 
                          PRODUCT P1
                  WHERE   C1.CERTIFICATEID  = PC1.CERTIFICATEID
                          AND PC1.SKUID     = P1.SKUID
                          AND P1.MATL_NUM   = lcr_certs.MATERIALNUMBER
                          AND PC1.CERTIFICATIONTYPEID = lcr_certs.CERTIFICATIONTYPEID
                          AND UPPER(C1.ACTIVESTATUS) = 'Y'
                          AND (PC1.DATEAPPROVED_CEGI IS NOT NULL or C1.CERTDATEAPPROVED IS NOT NULL)
                          AND (CASE                                                                    --JBH_2.0
                                 WHEN  PC1.CERTIFICATIONTYPEID = 2 or PC1.CERTIFICATIONTYPEID = 3      --JBH_2.0
                                 THEN  COALESCE(PC1.DATEAPPROVED_CEGI, C1.CERTDATEAPPROVED)            --JBH_2.0
                                 ELSE  PC1.OPER_DATE_APPROVED                                          --JBH_2.0
                               END)    IS NOT NULL                                                     --JBH_2.0
                          AND PC1.DATEREMOVED IS NULL ;

              ELSE
              --  Use the Cursor Dates When the Material Id has Only One Certificate  
                  ld_MaxEndDate := lcr_certs.end_date;
                  ld_StartDate  := lcr_certs.start_date;
              END IF;
             
          --  Insert SAP Interface File Row  
              ls_TextLine :=  RPAD(NVL( lcr_certs.COUNTRY  , ' '), 70, ' ');
              ls_TextLine :=  ls_TextLine ||  substr(TO_CHAR(NVL(lcr_certs.CERTIFICATIONTYPEID  ,'0'), '99'),2,2);
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(lcr_certs.CERTIFICATIONTYPENAME, ' '), 50, ' ');
              ls_TextLine :=  ls_TextLine ||  RPAD(' ', 30, ' '); --  CERTIFICATIONDESCRIPTION
              ls_TextLine :=  ls_TextLine ||  'Y';                --  CERTIFICATIONREQUIRED
              ls_TextLine :=  ls_TextLine ||  RPAD(' ', 30, ' '); --  LANGUAGE
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(lcr_certs.MATERIALNUMBER,  ' '), 18,' ');
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(lcr_certs.SKU,             ' '), 10,' ');
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(lcr_certs.SIZESTAMP,       ' '), 20,' ');
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(lcr_certs.BRANDDESC,       ' '),100,' ');
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(TO_CHAR(ld_StartDate,  'MM/DD/YYYY'),'01/01/1901'),10,'0');
              ls_TextLine :=  ls_TextLine ||  RPAD(NVL(TO_CHAR(ld_MaxEndDate, 'MM/DD/YYYY'),'12/31/9999'),10,'0');
              ls_TextLine := ls_TextLine  ||  NVL( lcr_certs.CHG_FLAG , ' ');
                  
              UTL_FILE.PUT_LINE(ls_TextFile, ls_TextLine);

              ln_RecCnt := ln_RecCnt + 1;
            
          --  Save Prior Key 
              ls_HoldCertificationTypeId  := lcr_certs.certificationtypeid;
              ln_HoldMatlNum  := lcr_certs.materialnumber;

          END IF;

      END LOOP;
        
      CLOSE lc_certs;

  --  JBH_2.0 - Added Deletions Processing Routine
  /*==========================================================================*/
  /*  Process SAP Deletions 
  ==========================================================================*/

  --  Initialize Key Change Work Variables 
      ls_HoldCertificationTypeId  := 0;
      ln_HoldMatlNum              := 0;     

  --  Initialize Date Variables 
      ld_MaxEndDate := NULL;
      ld_StartDate  := NULL;

      OPEN lc_deletes;
      LOOP

          FETCH lc_deletes into lcr_deletes;
          EXIT  WHEN lc_deletes%NOTFOUND;

      --  Write One Line Per Certification Type And Material Id Combination
          IF  ls_HoldCertificationTypeId  <> lcr_deletes.certificationtypeid OR
              ln_HoldMatlNum <>  lcr_certs.materialnumber THEN

          --  Check To See If There Are Any Currently Active Certificates
              SELECT  count(*) 
              INTO    ln_ActiveDeletes
              FROM    CERTIFICATE C1, 
                      PRODUCTCERTIFICATE PC1, 
                      PRODUCT P1
              WHERE   C1.CERTIFICATEID            = PC1.CERTIFICATEID
                      AND PC1.SKUID               = P1.SKUID
                      AND P1.MATL_NUM             = lcr_deletes.MATERIALNUMBER
                      AND PC1.CERTIFICATIONTYPEID = lcr_deletes.CERTIFICATIONTYPEID
                      AND UPPER(C1.ACTIVESTATUS)  = 'Y'
                      AND PC1.OPER_DATE_APPROVED  IS NOT NULL
                      AND PC1.DATEREMOVED         IS NULL;

          --  Delete All CCC(5) and India(7) Certificates but Only Delete the  
          --  Other Certificates If They Don't have Another Active Certificate
              IF  (lcr_deletes.CERTIFICATIONTYPEID = '1' and ln_ActiveDeletes = 0)  or 
                  (lcr_deletes.CERTIFICATIONTYPEID = '2' and ln_ActiveDeletes = 0)  or
                  (lcr_deletes.CERTIFICATIONTYPEID = '4' and ln_ActiveDeletes = 0)  or
                  (lcr_deletes.CERTIFICATIONTYPEID = '5')                           or
                  (lcr_deletes.CERTIFICATIONTYPEID = '6' and ln_ActiveDeletes = 0)  or
                  (lcr_deletes.CERTIFICATIONTYPEID = '7')
                  THEN 

                  ls_TextLine :=  RPAD(NVL( lcr_deletes.COUNTRY  , ' '), 70, ' ');
                  ls_TextLine :=  ls_TextLine ||  SUBSTR(TO_CHAR(NVL( lcr_deletes.CERTIFICATIONTYPEID  ,'0'), '99'),2,2);
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL( lcr_deletes.CERTIFICATIONTYPENAME  , ' '), 50, ' ');
                  ls_TextLine :=  ls_TextLine ||  RPAD(' ', 30, ' '); -- CERTIFICATION DESCRIPTION
                  ls_TextLine :=  ls_TextLine ||  'Y';                -- CERTIFICATION REQUIRED
                  ls_TextLine :=  ls_TextLine ||  RPAD(' ', 30, ' '); -- LANGUAGE
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL( lcr_deletes.MATERIALNUMBER  , ' '),  18, ' ');
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL( lcr_deletes.SKU             , ' '),  10, ' ');
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL( lcr_deletes.SIZESTAMP       , ' '),  20, ' ');
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL( lcr_deletes.BRANDDESC       , ' '), 100, ' ');
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL(TO_CHAR(ld_StartDate,  'MM/DD/YYYY'),'01/01/1901'), 10, '0');
                  ls_TextLine :=  ls_TextLine ||  RPAD(NVL(TO_CHAR(ld_MaxEndDate, 'MM/DD/YYYY'),'12/31/9999'), 10, '0');
                  ls_TextLine :=  ls_TextLine ||  NVL( lcr_deletes.CHG_FLAG , 'D');
                    
                  UTL_FILE.PUT_LINE(ls_TextFile, ls_TextLine);

                  ln_RecCnt := ln_RecCnt + 1;

              END IF;

          --  Save Prior Key 
              ls_HoldCertificationTypeId  := lcr_deletes.certificationtypeid;
              ln_HoldMatlNum  := lcr_deletes.materialnumber;

          END IF;

      END LOOP;
        
      CLOSE lc_deletes;

  /*==========================================================================*/
  /*  Completion Processing 
  ==========================================================================*/

      APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => ls_MACHINEID,
                AD_OPERATORID    => ls_OPERATORID,
                AD_DATERECORDED  => sysdate,
                AS_PROCESSNAME   =>' UNLOAD_CERTIFICATEACTIVITY ',
                AX_RECORDDATA    => 'SUCCESS - '||ln_RecCnt||' records exported',
                AS_MESSAGECODE   => 'SUCCESS',
                AS_MESSAGE       => 'SUCCESS'); 
    
      UTL_FILE.FCLOSE_ALL;

  EXCEPTION
      WHEN  NO_DATA_FOUND THEN
            NULL;

      WHEN  OTHERS THEN
            ls_ErrorMsg :=  sqlerrm;
            APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT
                  ( AS_MACHINEID     => ls_MACHINEID,
                    AD_OPERATORID    => ls_OPERATORID,
                    AD_DATERECORDED  => sysdate,
                    AS_PROCESSNAME   => ' UNLOAD_CERTIFICATEACTIVITY ',
                    AX_RECORDDATA    => 'An error has occurred at record '||ln_RecCnt ,
                    AS_MESSAGECODE   => to_char(sqlcode),
                    AS_MESSAGE       => ls_ErrorMsg);
            raise_application_error (-20007,ls_ErrorMsg);

       RAISE;

  END Unload_CertificateActivity;
  /
  
  create or replace PROCEDURE           UpdateDiscSku
  
IS

    ln_RecCount              NUMBER(6);
    ln_ErrorCount            NUMBER(6);
    ls_ErrorFound            VARCHAR2(1);
 
   CURSOR lcr_DiscSkus IS
     SELECT P.SKUID,P.SKU,TRUNC(P.DISCONTINUEDDATE) PRODUCT_DISC_DATE,TRUNC( S.DISC_DATE ) SKU_DISC_DATE
             FROM PRODUCT P, SKU_MASTER_MV S
             WHERE
                 P.SKU = S.SKU_CQBS
                 AND TRUNC(NVL( P.DISCONTINUEDDATE,SYSDATE)) <> TRUNC(NVL(S.DISC_DATE,SYSDATE));
--       SELECT P.SKUID,P.SKU,TRUNC(P.DISCONTINUEDDATE) PRODUCT_DISC_DATE,TRUNC( S.DISCONTINUEDATE) SKU_DISC_DATE
--             FROM PRODUCT P, SKUMAIN_VW S
--             WHERE
--                 P.SKU = S.SKU
--                 AND TRUNC(NVL( P.DISCONTINUEDDATE,SYSDATE)) <> TRUNC(NVL(S.DISCONTINUEDATE,SYSDATE));
BEGIN

    --  Initialize variables

  
    ln_ErrorCount  := 0;
    ln_RecCount   := 0;
 
      ---- GO THROUGH AND update the discontinued date
    FOR lcr_rec IN lcr_DiscSkus LOOP
         BEGIN
               update product
                    set discontinueddate = lcr_rec.SKU_DISC_DATE,
                         modifiedon = sysdate,
                         modifiedby = 'UpdateDiscSku'
                      where skuid = lcr_rec.skuid;
                      ln_RecCount := ln_RecCount + 1;
                    COMMIT;
        EXCEPTION
                WHEN OTHERS THEN
                DECLARE
                  lsErrorMsg VARCHAR2(300) := SQLERRM;
                BEGIN
           
                  lsErrorMsg := SUBSTR(lsErrorMsg, 1, 250) || ' (DB)';
                 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineId => 'fa0017',
                           ad_operatorId => 'UpdateDiscSku',
                          ad_dateRecorded => SYSDATE,
                          as_processName => 'Procedure UpdateDiscSku',
                          ax_recordData => 'An error has occurred',
                          as_messageCode => TO_CHAR(SQLCODE),
                          as_message =>lsErrorMsg );
                          ln_ErrorCount := ln_ErrorCount + 1;
                END;
        END;
     END LOOP;

  if ln_ErrorCount > 1 then
  
       APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineId => ' ',
            ad_operatorId => 'UpdateDiscSku',
            ad_dateRecorded => SYSDATE,
            as_processName => 'Procedure UpdateDiscSku',
            ax_recordData => ' ',
            as_messageCode => ' ',
            as_message =>'error count = '||ln_ErrorCount|| ' record count = '||ln_RecCount ); 
  
  END IF;
 
 

END UpdateDiscSku;
/
