CREATE OR REPLACE PROCEDURE ICS_PROCS.CERT_RENEWAL_NOTIFICATION 
IS
	ls_certificatenumber        CERTIFICATE.CERTIFICATENUMBER%TYPE			:= NULL;
	li_certificationtypeid      CERTIFICATE.CERTIFICATIONTYPEID%TYPE		:= NULL;
	ls_certificationtypename	CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%TYPE := NULL;
	lt_dateapproved             CERTIFICATE.CERTDATEAPPROVED%TYPE			:= NULL;
	ln_days                     NUMBER	:= NULL;
	ls_from                     VARCHAR2(80) := 'ICS_NOTIFY@coopertire.com';
	ls_recipient                VARCHAR2(80) := 'ICS_SUPPORT@coopertire.com';
	ls_recipientcc              VARCHAR2(80) := 'ICS_SUPPORT@coopertire.com'; 
	ls_subject                  VARCHAR2(80) := 'Certificate Renewals Required';
	ls_mail_host                VARCHAR2(30) := 'SMTP';
	ls_body                     VARCHAR2(32767) := '';
	v_mail_conn                 UTL_SMTP.CONNECTION;
	crlf                        VARCHAR2(2) := CHR(13)||CHR(10);

	CURSOR c1 IS 
		SELECT C.CERTIFICATENUMBER,
               C.CERTIFICATIONTYPEID,
               CT.CERTIFICATIONTYPENAME,
               C.CERTDATEAPPROVED
		FROM CERTIFICATE C
			INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
		WHERE C.CERTIFICATIONTYPEID NOT IN (1, 6)
			AND C.CERTDATEAPPROVED IS NOT NULL
			AND LOWER(C.ACTIVESTATUS) = 'y'
			AND LOWER(C.RENEWALREQUIRED_CGIN) = 'y'
			AND C.ARCHIVEDATE_CEGI IS NULL
			AND LOWER(C.MOSTRECENTCERT) = 'y'
        ORDER BY C.CERTIFICATIONTYPEID, 
				 C.CERTDATEAPPROVED, 
				 CERTIFICATENUMBER;
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
			FETCH c1 INTO ls_certificatenumber, 
						  li_certificationtypeid, 
						  ls_certificationtypename, 
						  lt_dateapproved;
			EXIT WHEN c1%NOTFOUND OR LENGTH(LS_BODY)> 32767-65;

			ln_days := TO_NUMBER(SYSDATE - lt_dateapproved);
            
			CASE li_certificationtypeid
				WHEN 2 THEN
				---jeseitz 3/19/2012 per R. Riedel - IF ln_days > 730 - 90 THEN -- 2 years - 90 days 730
					IF 
						ln_days > 365 - 90 THEN -- 1 year - 90 days 
						ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: GSO' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);
					END IF;
				WHEN 4 THEN
                 ---jeseitz 3/19/2012 per R. Riedel - IF ln_days > 730 - 90 THEN -- 2 years - 90 days
					IF 
						ln_days > 365 - 90 THEN -- 1 year - 90 days
						ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: Imark' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);
					END IF;
				WHEN 5 THEN
					IF 
						ln_days > 1825 - 90 THEN -- 5 years - 90 days 1825
						ls_Body := ls_Body || crlf || 'Certificate: ' || ls_certificatenumber || '; Type: CCC' || '; ' || 'Days Since Approval: ' || ROUND(ln_days);
					END IF;
				ELSE
					-- Emark does not require renewal.  This is just in case something accidently got set to make the system think it does.
					ls_Body := ls_Body;
			END CASE;
        
			IF 
				LENGTH(LS_BODY)> 32767-65 THEN
				LS_BODY := LS_BODY||'...';
			END IF;
		END LOOP;
		CLOSE c1;
   
		IF 
			LENGTH(ls_body) > 0 THEN
			v_Mail_Conn := UTL_SMTP.OPEN_CONNECTION(ls_Mail_Host, 25);
			UTL_SMTP.HELO(v_Mail_Conn, ls_Mail_Host);
			UTL_SMTP.MAIL(v_Mail_Conn, ls_From);
			UTL_SMTP.RCPT(v_Mail_Conn, ls_Recipient);
			UTL_SMTP.RCPT(v_Mail_Conn, ls_RecipientCC);
			UTL_SMTP.DATA(v_Mail_Conn,
			'Date: '   || TO_CHAR(SYSDATE, 'Dy, DD Mon YYYY hh24:mi:ss') || crlf ||
			'From: '   || ls_From || crlf ||
			'Subject: '|| ls_Subject || crlf ||
			'To: '     || ls_Recipient || crlf ||
			'CC: '     || ls_RecipientCC || crlf ||
			crlf ||ls_Body || crlf );
        
			UTL_SMTP.QUIT(v_mail_conn);

		END IF;
   
	EXCEPTION
		WHEN UTL_SMTP.TRANSIENT_ERROR OR UTL_SMTP.PERMANENT_ERROR THEN
		DBMS_OUTPUT.PUT_LINE('Unable to send mail: '||SQLERRM);
            
		RAISE_APPLICATION_ERROR(-20000, 'Unable to send mail: '||SQLERRM);
		
		WHEN OTHERS THEN
		-- Consider logging the error and then re-raise
		DBMS_OUTPUT.PUT_LINE(SQLCODE || SQLERRM);
		RAISE;
		
	END CERT_RENEWAL_NOTIFICATION;
	/
	
CREATE OR REPLACE PROCEDURE ICS_PROCS.CERTIFICATEAPPROVALNOTICE 
IS
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
	ls_certificatenumber        VARCHAR2(50) := NULL;
	li_certificationtypeid		CERTIFICATE.CERTIFICATIONTYPEID%TYPE := NULL;
	ls_certificationtypename	CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%TYPE := NULL;
	lt_dateapproved             CERTIFICATE.CERTDATEAPPROVED%TYPE := NULL;
	ls_location                 VARCHAR2(30) := NULL;
	ls_description              VARCHAR2(20) := NULL;
	ln_recs                     NUMBER := NULL;
	ls_from                     VARCHAR2(80) := 'jrnelaturi@coopertire.com';
	ls_recipient                VARCHAR2(80) := 'jrnelaturi@coopertire.com';
	ls_recipientcc              VARCHAR2(80) := 'SCMallampalli@coopertire.com';
	ls_bcc                      VARCHAR2(32767) := NULL;
	ls_subject                  VARCHAR2(80) := 'International Certification Approvals';
	ls_mail_host                VARCHAR2(30) := 'SMTP';
	ls_body                     VARCHAR2(32767) := '';
	ls_message                  VARCHAR2(32767) := NULL;
	ls_note                     VARCHAR2(100) := ' ';
	ls_header                   VARCHAR2(32767) := NULL;
	ls_header2                  VARCHAR2(32767) := NULL;
	ls_mimetype                 VARCHAR2(200) := NULL;
	v_mail_conn                 UTL_SMTP.CONNECTION;
	crlf                        VARCHAR2(2) := CHR(13)||CHR(10);
	ls_db_name                  VARCHAR2(50);

	CURSOR c1 IS 
		SELECT C.CERTIFICATENUMBER, 
			   C.EXTENSION_EN,
			   C.CERTIFICATIONTYPEID,
               CT.CERTIFICATIONTYPENAME, 
               CASE TRIM(C.PRODUCTLOCATION)
					WHEN '1' THEN 'FIN'
					WHEN '2' THEN 'TEX'
					WHEN '5' THEN 'TUP'
					WHEN '12' THEN 'MLK'
					WHEN '76' THEN 'OCC'
               ELSE C.PRODUCTLOCATION
			   END PRODUCTLOCATION,
               PC.DATEAPPROVED_CEGI,
			   PC.OPER_DATE_APPROVED,		
               P.MATL_NUM, 
			   P.BRAND, 
			   P.BRAND_LINE, 
			   P.SIZESTAMP, 
			   P.SPEEDRATING, 
			   P.SINGLOADINDEX,
               P.DUALLOADINDEX, 
			   P.SKU
		FROM PRODUCTCERTIFICATE PC
			INNER JOIN CERTIFICATE C ON C.CERTIFICATEID = PC.CERTIFICATEID
			INNER JOIN PRODUCT P ON P.SKUID = PC.SKUID
			INNER JOIN CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
		WHERE DECODE(C.CERTIFICATIONTYPEID, 1, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30),  --ECE3054
                                            2, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30),  --GSO
                                            3, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30),  --NOM
                                            4, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30),  -- IMARK
                                            5, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30), -- CCC
                                            6, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30),  -- E117
                                            7, NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30), --India_mark
                                               NVL(PC.DATE_APPROVAL_ENTERED,SYSDATE+30))  -- other
        BETWEEN TRUNC(SYSDATE-7) AND TRUNC(SYSDATE-1)
			AND LOWER(C.ACTIVESTATUS) = 'y' 
			AND C.ARCHIVEDATE_CEGI IS NULL
			AND (LOWER(C.MOSTRECENTCERT) = 'y' or C.CERTIFICATIONTYPEID = 4)
			AND C.CERTIFICATIONTYPEID <> 3 ---  NOM certificates don't get an approval date.
        ORDER BY C.CERTIFICATIONTYPEID,   
				 C.CERTIFICATENUMBER, 
				 C.EXTENSION_EN, 
				 P.MATL_NUM;

	BEGIN

		SELECT SYS.DATABASE_NAME INTO ls_db_name FROM DUAL;
  
		--don't send emails out to everone if not on production system
		IF INSTR(ls_db_name, 'FA0017_TECH') = 0 THEN -- not on PRODUCTION
			ls_recipient := 'ICS_SUPPORT@COOPERTIRE.COM';
			ls_recipientcc   := '';
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
    
        ls_description := lcr_c1.SINGLOADINDEX;
        
		IF lcr_c1.DUALLOADINDEX IS NULL OR TRIM(lcr_c1.DUALLOADINDEX) = '' THEN
            ls_description :=  ls_description;
        ELSE
            ls_description :=  ls_description||'/'||lcr_c1.DUALLOADINDEX;
        END IF;
         
        lt_dateapproved := lcr_c1.OPER_DATE_APPROVED;
		
        IF lcr_c1.certificationtypeid = 2 OR lcr_c1.certificationtypeid = 4 THEN -- GSO or IMARK
            lt_dateapproved :=  lcr_c1.DATEAPPROVED_CEGI;
        END IF;
       
        ls_certificatenumber := lcr_c1.certificatenumber;
		
        IF lcr_c1.certificationtypeid = 5 OR lcr_c1.certificationtypeid = 7 THEN --For CCC or India certifcates, include the production location.
			IF lcr_c1.productlocation IS NOT NULL THEN   
				ls_certificatenumber := ls_certificatenumber||' ('||lcr_c1.productlocation||')';
			END IF;
        END IF;
   
        ls_body := ls_body || '<TR>';
        ls_body := ls_body || '<TD>'||ltrim(lcr_c1.matl_num,'0')|| '</TD>';
        ls_body := ls_body || '<TD>'||lcr_c1.SKU|| '</TD>';
        ls_body := ls_body || '<TD>'||lcr_c1.brand|| ' ';
        ls_body := ls_body || lcr_c1.brand_line|| '</TD>';
        ls_body := ls_body || '<TD>'||lcr_c1.sizestamp|| '</TD>';
        ls_body := ls_body || '<TD>'||ls_description|| '</TD>';
        ls_body := ls_body || '<TD>'||lcr_c1.speedrating|| '</TD>';
        ls_body := ls_body || '<TD>'|| lcr_c1.certificationtypename|| '</TD>';
        ls_body := ls_body || '<TD>'||ls_certificatenumber|| '</TD>';
        ls_body := ls_body || '<TD>'||to_char( lcr_c1.DATEAPPROVED_CEGI,'MM/DD/YYYY')|| '</TD>';
        ls_body := ls_body || '</TR>' ;
           
        ln_recs := ln_recs + 1;

        IF LENGTH(ls_body)> 32767-65 THEN
			ls_body := ls_body||'...'||crlf;
        END IF;

		END LOOP;

		--check for  email that needs sent
		IF ln_recs > 0 THEN
               
        ls_message := '<div>' 
                  || '<table width=''100%'' height=''182'' border=''0'' cellpadding=''3'' cellspacing=''1'' bgcolor=''#006699'' style=''font-size:12px; font-family : Arial''>'
                  || ' <tr><td bgcolor=''#FFFFFF''>' || '<br />'
                  || '<span style=''font-size:12px; font-family : Arial''>'|| 'The following approvals were recently received: '
                  || '</span>' || '<br /><br />'|| '<table cellspacing=''0'' cellpadding=''0'' > '            
                  || '<tr><td style=''text-align:left; FONT-SIZE: 12px;  font-family : Arial''>'||  ls_header2 || ls_body ||'</TABLE></td></tr>' || '</table> ' || '<br />' 
                  || '<b><span style=''font-size:12px; font-family : Arial''>' || ls_note 
                  || '</span></b><br /><br />' || ' Thanks,<br />' 
                  || '<b><span style=''font-size:12px; font-family : Arial''>' || 'Quality Group' 
                  || '</span></b><br /><br />' || ' </td></tr></table></div>';
               
--               
        UTL_MAIL.SEND(sender 		=> ls_from,   
					  recipients	=> ls_recipient,   
					  cc 			=> ls_recipientcc,   
					  bcc 			=> ls_bcc, 
					  subject 		=> ls_subject,   
					  message 		=> ls_message,   
					  mime_type 	=> ls_mimetype,   
					  priority 		=> '3');
		END IF;
   
	EXCEPTION
		WHEN UTL_SMTP.TRANSIENT_ERROR OR UTL_SMTP.PERMANENT_ERROR THEN
			DBMS_OUTPUT.PUT_LINE('Unable to send mail: '||SQLERRM);
            
			RAISE_APPLICATION_ERROR(-20000, 'Unable to send mail: '||SQLERRM);   
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(ls_db_name,
													  'ICS_PROCS',
													  SYSDATE,
													  'CertificateApprovalNotice',
													  ' ',
													  SQLCODE,
													  SQLERRM);
            
		WHEN OTHERS THEN
			-- Consider logging the error and then re-raise
            DBMS_OUTPUT.PUT_LINE(SQLCODE || SQLERRM);
                  
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(ls_db_name,
													  'ICS_PROCS',
													  SYSDATE,
													  'CertificateApprovalNotice',
													  ' ',
													  SQLCODE,
													  SQLERRM);
		RAISE;
		
END CERTIFICATEAPPROVALNOTICE;
/

CREATE OR REPLACE PROCEDURE ICS_PROCS.GETNOMCERTIFICATION(ps_certificatenumber IN    VARCHAR2,
														  pi_tiretypeid        IN    NUMBER,
														  pc_certificatedfvalue  OUT SYS_REFCURSOR,
														  pc_certificateinfo     OUT SYS_REFCURSOR,
														  pc_product             OUT SYS_REFCURSOR, 
														  pc_measurehdr          OUT SYS_REFCURSOR,
														  pc_measuredtl          OUT SYS_REFCURSOR,
														  pc_beadunseathdr       OUT SYS_REFCURSOR,
														  pc_beadunseatdtl       OUT SYS_REFCURSOR,
														  pc_plungerhdr          OUT SYS_REFCURSOR,
														  pc_plungerdtl          OUT SYS_REFCURSOR,
														  pc_treadwearhdr        OUT SYS_REFCURSOR,
														  pc_treadweardtl        OUT SYS_REFCURSOR,
														  pc_endurancehdr        OUT SYS_REFCURSOR,
														  pc_endurancedtl        OUT SYS_REFCURSOR,
														  pc_highspeedhdr        OUT SYS_REFCURSOR,
														  pc_highspeeddtl        OUT SYS_REFCURSOR,
														  pc_speedtestdetail     OUT SYS_REFCURSOR,
														  pc_brand               OUT SYS_REFCURSOR) 
AS 
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
    li_parametersarenull EXCEPTION;  
    
    -- link the exception to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);   
    li_parametersareinvalid EXCEPTION;
	 
    -- link the exception to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);   
    
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;                 
    
    ls_brandcode VARCHAR2(10) := NULL;                  
  
	BEGIN
      
		IF ps_certificatenumber IS NULL THEN
			RAISE li_parametersarenull;
		END IF;
      
		IF ps_certificatenumber = '' THEN
			RAISE li_parametersareinvalid;
		END IF;
            
			--Gets the default values information.         
            OPEN pc_certificatedfvalue FOR
				SELECT CERTIFICATENUMBER,
					   MAX(DECODE(FIELDID, 153 ,FIELDVALUE, NULL)) AS DIMENSION_N,
					   MAX(DECODE(FIELDID, 120 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC1_N,
					   MAX(DECODE(FIELDID, 123 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC2_N,
					   MAX(DECODE(FIELDID, 126 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC3_N ,
					   MAX(DECODE(FIELDID, 129 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC4_N ,
					   MAX(DECODE(FIELDID, 132 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC5_N,
					   MAX(DECODE(FIELDID, 135 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC6_N,
					   MAX(DECODE(FIELDID, 138 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC7_N,
					   MAX(DECODE(FIELDID, 141 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINDESC8_N,
					   MAX(DECODE(FIELDID, 121 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA1_N,
					   MAX(DECODE(FIELDID, 124 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA2_N,
					   MAX(DECODE(FIELDID, 127 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA3_N,
					   MAX(DECODE(FIELDID, 130 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA4_N ,
					   MAX(DECODE(FIELDID, 133 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA5_N,
					   MAX(DECODE(FIELDID, 136 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA6_N,
					   MAX(DECODE(FIELDID, 139 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA7_N,
					   MAX(DECODE(FIELDID, 142 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMARCA8_N,
					   MAX(DECODE(FIELDID, 122 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO1_N,
					   MAX(DECODE(FIELDID, 125 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO2_N ,
					   MAX(DECODE(FIELDID, 128 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO3_N,
					   MAX(DECODE(FIELDID, 131 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO4_N ,
					   MAX(DECODE(FIELDID, 134 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO5_N,
					   MAX(DECODE(FIELDID, 137 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO6_N,
					   MAX(DECODE(FIELDID, 140 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO7_N,
					   MAX(DECODE(FIELDID, 143 ,FIELDVALUE, NULL)) AS EQUIPOEMPLEADORINMODELO8_N,
					   MAX(DECODE(FIELDID, 144 ,FIELDVALUE, NULL)) AS EQUIPOPRUEBARESISTENCIA_N,
					   MAX(DECODE(FIELDID, 150 ,FIELDVALUE, NULL)) AS EVALUATIONDATE_N,
					   MAX(DECODE(FIELDID, 159 ,FIELDVALUE, NULL)) AS FINALPRESSURE_N,
					   MAX(DECODE(FIELDID, 152 ,FIELDVALUE, NULL)) AS HEIGHT_N,
					   MAX(DECODE(FIELDID, 146 ,FIELDVALUE, NULL)) AS IDENTIFICATIONKEY_N,
					   MAX(DECODE(FIELDID, 158 ,FIELDVALUE, NULL)) AS LOADBEHAVIOR_N,
					   MAX(DECODE(FIELDID, 147 ,FIELDVALUE, NULL)) AS LOADCAPACITY_N,
					   MAX(DECODE(FIELDID, 155 ,FIELDVALUE, NULL)) AS MEASUREMENTFACTOR_N,
					   MAX(DECODE(FIELDID, 148 ,FIELDVALUE, NULL)) AS MODEL_N,
					   MAX(DECODE(FIELDID, 157 ,FIELDVALUE, NULL)) AS PENETRATIONRESISTENCE_N,
					   MAX(DECODE(FIELDID, 156 ,FIELDVALUE, NULL)) AS RIMRESISTENCE_N,
					   MAX(DECODE(FIELDID, 160 ,FIELDVALUE, NULL)) AS ROOMTEMP_N,
					   MAX(DECODE(FIELDID, 119 ,FIELDVALUE, NULL)) AS SINALPADDRESS_N,
					   MAX(DECODE(FIELDID, 118 ,FIELDVALUE, NULL)) AS SINALPCENTROEVALUACION_N,
					   MAX(DECODE(FIELDID, 114 ,FIELDVALUE, NULL)) AS SINALPDOMICILIO_N,
					   MAX(DECODE(FIELDID, 113 ,FIELDVALUE, NULL)) AS SINALPEMPRESA_N,
					   MAX(DECODE(FIELDID, 116 ,FIELDVALUE, NULL)) AS SINALPHULERA_N,
					   MAX(DECODE(FIELDID, 117 ,FIELDVALUE, NULL)) AS SINALPMANUFACTURERNAME_N,
					   MAX(DECODE(FIELDID, 115 ,FIELDVALUE, NULL)) AS SINALPREPRESENTANTE_N,
					   MAX(DECODE(FIELDID, 161 ,FIELDVALUE, NULL)) AS SPEEDBEHAVIOR_N,
					   MAX(DECODE(FIELDID, 162 ,FIELDVALUE, NULL)) AS TESTINFO_N,
					   MAX(DECODE(FIELDID, 164 ,FIELDVALUE, NULL)) AS TESTREPORT_N,
					   MAX(DECODE(FIELDID, 163 ,FIELDVALUE, NULL)) AS TESTSERIE_N,
					   MAX(DECODE(FIELDID, 145 ,FIELDVALUE, NULL)) AS TIREIDENTIFICATION_N,
					   MAX(DECODE(FIELDID, 149 ,FIELDVALUE, NULL)) AS TYPE_N,
					   MAX(DECODE(FIELDID, 151 ,FIELDVALUE, NULL)) AS WEARINGDOWNINDICATOR_N,
					   MAX(DECODE(FIELDID, 187 ,FIELDVALUE, NULL)) AS SIGNATURENAME_N,
					   MAX(DECODE(FIELDID, 188 ,FIELDVALUE, NULL)) AS SIGNATURENTITLE_N,
					   MAX(DECODE(FIELDID, 190 ,FIELDVALUE, NULL)) AS NOMINALBEADUNSEAT_N,
					   MAX(DECODE(FIELDID, 191 ,FIELDVALUE, NULL)) AS NOMINALPLUNGER_N,
					   MAX(DECODE(FIELDID, 192 ,FIELDVALUE, NULL)) AS LOWPRESSENDURINITINFL_N
				FROM (SELECT FIELDID,
							 CERTIFICATENUMBER,
							 FIELDVALUE
					  FROM defaultvalues_view
					  WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificatenumber)  
						AND CERTIFICATIONTYPEID = 3)   
				GROUP BY CERTIFICATENUMBER ;
            
            OPEN pc_certificateinfo FOR
                SELECT CERTIFICATEID,
                       CE.CERTIFICATIONTYPEID,
                       CERTIFICATIONTYPENAME,
                       CERTIFICATENUMBER,
                       ACTIVESTATUS,
                       RENEWALREQUIRED_CGIN,
                       EXTENSION_EN,
                       COUNTRYOFMANUFACTURE_N,
                       CUSTOMER CUSTOMER_N,
                       CUSTOMERSPECIFIC_N,
                       IMPORTER,
                       IMPORTERREPRESENTATIVE,
                       IMPORTERADDRESS,
                       CERTDATESUBMITTED,
                       CERTDATEAPPROVED,
                       CU.SIGNATUREIND
                FROM CERTIFICATE CE 
                    INNER JOIN CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
                    LEFT OUTER JOIN IMPORTER IM ON CE.IMPORTERID = IM.IMPORTERID
                    LEFT OUTER JOIN CUSTOMER CU ON CE.CUSTOMERID = CU.CUSTOMERID
                WHERE CE.CERTIFICATIONTYPEID = 3 
					AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
					AND LOWER(CE.MOSTRECENTCERT) = 'y';
            
			--As per PRJ3617, 
            -- Added PSN instead of NPRID
            -- Added Matl_Num
            -- Added brand and brand_line instead of brandcode
            OPEN pc_product FOR
				SELECT DISTINCT P.SKUID,  
								SKU,
								MATL_NUM, 
								BRAND,
								BRAND_LINE, 
								TIRETYPEID, 
								PSN,
							    SIZESTAMP, 
								DISCONTINUEDDATE,   
								SPECNUMBER,   
								SPEEDRATING,
								SINGLOADINDEX,   
								DUALLOADINDEX,   
								BIASBELTEDRADIAL,   
								TUBELESSYN,
								REINFORCEDYN,   
								EXTRALOADYN,   
								UTQGTREADWEAR,   
								UTQGTRACTION,
								UTQGTEMP,   
								MUDSNOWYN,   
								RIMDIAMETER,   
								SERIALDATE,   
								BRANDDESC,
								LOADRANGE,   
								MEARIMWIDTH,   
								REGROOVABLEIND,   
								PLANTPRODUCED,
								MOSTRECENTTESTDATE,   
								IMARK,   
								INFORMENUMBER,   
								FECHADATE,
								TREADPATTERN,   
								SPECIALPROTECTIVEBAND,   
								NOMINALTIREWIDTH,
								ASPECTRATIO,   
								TREADWEARINDICATORS,   
								NAMEOFMANUFACTURER,
								FAMILY,   
								DOTSERIALNUMBER,  
								CE.CERTIFICATENUMBER
				FROM PRODUCT P
					INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
					INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID
						AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificatenumber);
            
		BEGIN  
			SELECT DISTINCT BRANDCODE INTO ls_brandcode
            FROM PRODUCT P
				INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
				INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID
					AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)  
				AND LOWER(CE.MOSTRECENTCERT) = 'y'; 
		EXCEPTION
			WHEN OTHERS THEN
				ls_brandcode:=NULL;
		END;
               
		-- Gets the brand information
		--- use this cursor to return the sizes for this brand
        OPEN pc_brand FOR
			SELECT DISTINCT BRAND_CODE,
							SIZE_STAMP||' '||DECODE(nvl(S.LOAD_RANGE,' '),'XL','XL', 'RE','RE',' ') AS SIZESTAMP,
							CASE WHEN(pi_tiretypeid = 1) THEN 1
								 WHEN(pi_tiretypeid = 3) THEN 
                                (CASE WHEN (SLOAD_IDX > 112) THEN 2
                                 ELSE 1
								 END)
							ELSE 1
							END SLOAD_IDX
			FROM  BOM_DATA.SKU_MASTER_MV S
			WHERE BRAND_CODE = ls_brandcode
				AND NVL(DISC_DATE, SYSDATE) > '01-NOV-1991'; --lets only get the more recent sku's
            --- and disc_date is null; -- hasn't been discontined 
            -- nope - now Mario said to include everything, even ones that are discontinued - 4/28/2011          
          
		OPEN pc_measurehdr FOR
            SELECT MEASUREID,
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
            FROM CERTIFICATE CE 
				INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE CE.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
                AND UPPER(CE.MOSTRECENTCERT) = 'Y';
              
		OPEN pc_measureDtl FOR
			SELECT md.MEASUREID,
                   SECTIONWIDTH,
                   OVERALLWIDTH,
                   ITERATION
            FROM CERTIFICATE CE 
				INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID 
					AND LOWER(CE.MOSTRECENTCERT) = 'y'
                INNER JOIN MEASUREDTL MD ON M.MEASUREID = MD.MEASUREID
            WHERE M.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber);
              
		OPEN pc_BEADUNSEATHDR FOR
			SELECT BEADUNSEATID,
                   PROJECTNUMBER,
                   TIRENUMBER,
                   TESTSPEC,
                   COMPLETIONDATE,
                   DOTSERIALNUMBER,
                   LOWESTUNSEATVALUE,
                   PASSYN,
                   B.CERTIFICATIONTYPEID,
                   CERTIFICATENUMBER,
                   SERIALDATE,
                   MINBEADUNSEAT,                    
                   TESTPASSFAIL,
                   SKU
            FROM CERTIFICATE CE 
				INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
			WHERE B.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
                AND LOWER(CE.MOSTRECENTCERT) = 'y';
                    
		OPEN pc_BEADUNSEATDTL FOR   
			SELECT BD.BEADUNSEATID,
                   UNSEATFORCE,
                   ITERATION
            FROM CERTIFICATE CE 
				INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
				INNER JOIN BEADUNSEATDTL BD ON B.BEADUNSEATID = BD.BEADUNSEATID                                   
            WHERE B.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
                AND LOWER(CE.MOSTRECENTCERT) = 'y'; 
                          
		OPEN pc_PLUNGERHDR FOR 
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
            FROM CERTIFICATE CE 
				INNER JOIN  PLUNGERHDR P ON CE.CERTIFICATEID = P.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
            WHERE P.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
                AND LOWER(CE.MOSTRECENTCERT) = 'y';
                       
		OPEN pc_PLUNGERDTL FOR
			SELECT PD.PLUNGERID,
                   BREAKINGENERGY,
                   ITERATION 
			FROM CERTIFICATE CE 
				INNER JOIN PLUNGERHDR PH ON CE.CERTIFICATEID = PH.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = PH.CERTIFICATIONTYPEID
                INNER JOIN PLUNGERDTL PD ON PH.PLUNGERID = PD.PLUNGERID
            WHERE PH.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
                AND LOWER(CE.MOSTRECENTCERT) = 'y';         
                                   
		OPEN pc_TREADWEARHDR FOR
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
			FROM CERTIFICATE CE 
				INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			WHERE T.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				AND LOWER(CE.MOSTRECENTCERT) = 'y';
                
		OPEN pc_TREADWEARDTL FOR
			SELECT TD.TREADWEARID,
                   WEARBARHEIGHT,
                   ITERATION
			FROM CERTIFICATE CE 
				INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
				INNER JOIN TREADWEARDTL TD ON T.TREADWEARID = TD.TREADWEARID
			WHERE T.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				AND LOWER(CE.MOSTRECENTCERT) = 'y'; 
              
		OPEN pc_ENDURANCEHDR FOR
                SELECT E.ENDURANCEID,
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
                       E.CERTIFICATIONTYPEID,
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
				FROM CERTIFICATE CE 
					INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID 
						AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID 
                    INNER JOIN (SELECT ENDURANCEID, MAX(SPEED) AS SPEED 
								FROM ENDURANCEDTL ED 
								WHERE ED.TESTSTEP <= 1 
								GROUP BY ENDURANCEID) EDS ON EDS.ENDURANCEID = E.ENDURANCEID                              
                WHERE E.CERTIFICATIONTYPEID = 3 
					AND LOWER(CE.CERTIFICATENUMBER) = lower(ps_certificatenumber)
                    AND LOWER(CE.MOSTRECENTCERT) = 'y';
                       
		OPEN pc_ENDURANCEDTL FOR
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
                   ED.ENDURANCEID
			FROM CERTIFICATE CE 
				INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID 
				INNER JOIN ENDURANCEDTL ED ON E.ENDURANCEID = ED.ENDURANCEID
			WHERE E.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				AND LOWER(CE.MOSTRECENTCERT) = 'y';              

		OPEN pc_HIGHSPEEDHDR FOR
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
                   H.CERTIFICATIONTYPEID,
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
                   HDM.LOAD AS MAXLOAD,
                   SKU
			FROM CERTIFICATE CE 
				INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID 
				INNER JOIN (SELECT HD.HIGHSPEEDID, 
								   MAX(LOAD) AS LOAD  
							FROM HIGHSPEEDDTL HD
                            where HD.TESTSTEP <= 1 ---Mario wanted this added so it just pulls the load from the first step, so that he only
                                                                        ---has to change one of them, and they are really all the same. - had to do it with
                                                                        ---the  <= so that if it doesn't have a step one, it won't error out.
                                                                         ---jes 5/10/11   
                            GROUP BY HD.HIGHSPEEDID) HDM ON H.HIGHSPEEDID = HDM.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				AND LOWER(CE.MOSTRECENTCERT) = 'y';
                            
		OPEN pc_HIGHSPEEDDTL FOR
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
			FROM CERTIFICATE CE 
				INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID 
				INNER JOIN HIGHSPEEDDTL HD ON H.HIGHSPEEDID = HD.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				AND LOWER(CE.MOSTRECENTCERT) = 'y';
               
		OPEN pc_SPEEDTESTDETAIL FOR
			SELECT ITERATION,
                   TIME,
                   SPEED,
                   S.HIGHSPEEDID
			FROM CERTIFICATE CE 
				INNER JOIN HIGHSPEEDHDR H  ON CE.CERTIFICATEID = H.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID 
				INNER JOIN SPEEDTESTDETAIL S ON H.HIGHSPEEDID = S.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				AND LOWER(CE.MOSTRECENTCERT) = 'y';
                        
	EXCEPTION     
        WHEN li_parametersarenull THEN           
        ls_errormsg :=  SQLERRM ||  '-GetNOMCertification.There is at least one parameters null.'  ;
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> ' Reports_Package.GetNOMCertification',
												  ax_recorddata    	=> 'ps_sku is parameters null..',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);  
           
		RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
            
        WHEN li_parametersareinvalid THEN           
        ls_errormsg := SQLERRM || '-GetNOMCertification. There is at least one parameters invalid.';
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> ' Reports_Package.GetNOMCertification',
												  ax_recorddata    	=> 'There is at least one parameters invalid.',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);  
           
		RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
         
		WHEN OTHERS THEN            
        ls_errormsg := SQLERRM || '-GetNOMCertification. An error have ocurred.(when others)' || SQLERRM;
               
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> ' Reports_Package.GetNOMCertification',
												  ax_recorddata    	=> 'An error have ocurred.(when others)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
					  
		RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			   
END GETNOMCERTIFICATION;
/


CREATE OR REPLACE PROCEDURE ICS_PROCS.GETTIRECHARACTERISTICS(ps_matl_num          IN    PRODUCT.MATL_NUM%TYPE,
															 pn_tiretypeid          OUT PRODUCT.TIRETYPEID%TYPE,
															 ps_speedrating         OUT PRODUCT.SPEEDRATING %TYPE,
															 ps_singloadindex       OUT PRODUCT.SINGLOADINDEX%TYPE,
															 ps_tubelessyn          OUT PRODUCT.TUBELESSYN%TYPE,
															 ps_reinforcedyn        OUT PRODUCT.REINFORCEDYN%TYPE,
															 ps_extraloadyn         OUT PRODUCT.EXTRALOADYN%TYPE,
															 ps_utqgtemp            OUT PRODUCT.UTQGTEMP%TYPE,
															 ps_utqgtraction        OUT PRODUCT.UTQGTRACTION%TYPE,
															 ps_utqgtreadwear       OUT PRODUCT.UTQGTREADWEAR%TYPE,
															 ps_loadrange           OUT PRODUCT.LOADRANGE%TYPE,
															 ps_tpn                 OUT PRODUCT.TPN%TYPE,
															 ps_biasbeltedradial    OUT PRODUCT.BIASBELTEDRADIAL%TYPE)
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

	tmpvar NUMBER := NULL;
	ls_errormsg APP_MESSAGE.MESSAGE%TYPE := NULL;
 
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
          ,DECODE(TUBE_TYPE,'TUBELESS','Y','N') -- JESEITZ 10/31/12
         ,LOAD_RANGE
         ,DECODE(NVL(REINFORCEDYN,' '),'RE','Y','N')
         ,UTQG_TEMPERATURE
         ,UTQG_TRACTION
         ,UTQG_TREADWEAR
         ,TECHNICAL_PLATFORM
         ,DECODE(RMA_TIRE_PLY_CONSTRUCTION,'BIAS-BELTED','BELTED','R','RADIAL','2','BIAS','3','BELTED',RMA_TIRE_PLY_CONSTRUCTION) 
    INTO 
          pn_tiretypeid,
          ps_speedrating,
          ps_singloadindex,
          ps_tubelessyn,
          ps_loadrange,
          ps_reinforcedyn,
          ps_utqgtemp,
          ps_utqgtraction,
          ps_utqgtreadwear,
          ps_tpn,
          ps_biasbeltedradial
    FROM
		(SELECT LPAD(MATL_NUM,18,0) AS MATL_NUM 
             ,MAX(DECODE(ATTRIB_NAME,'PRODUCT_TYPE',ATTRIB_VALUE))     AS  PRODUCT_TYPE
             ,MAX(DECODE(ATTRIB_NAME,'SPEED_RATING',ATTRIB_VALUE))     AS  SPEED_RATING
             ,MAX(DECODE(ATTRIB_NAME,'STAMPED_SINGLE_LOAD_INDEX',ATTRIB_VALUE))  AS SINGLE_LOAD_INDEX
             ,MAX(DECODE(ATTRIB_NAME,'TUBE_TYPE',ATTRIB_VALUE))        AS  TUBE_TYPE
             ,MAX(DECODE(ATTRIB_NAME,'LOAD_RANGE',ATTRIB_VALUE))       AS  LOAD_RANGE
             ,MAX(DECODE(ATTRIB_NAME,'LOAD_RANGE',ATTRIB_VALUE))       AS  REINFORCEDYN
             ,MAX(DECODE(ATTRIB_NAME,'UTQG_TEMPERATURE',ATTRIB_VALUE)) AS  UTQG_TEMPERATURE
             ,MAX(DECODE(ATTRIB_NAME,'UTQG_TRACTION',ATTRIB_VALUE))    AS  UTQG_TRACTION
             ,MAX(DECODE(ATTRIB_NAME,'UTQG_TREADWEAR',ATTRIB_VALUE))   AS  UTQG_TREADWEAR
             ,MAX(DECODE(ATTRIB_NAME,'TECHNICAL_PLATFORM',ATTRIB_VALUE))AS  TECHNICAL_PLATFORM
             ,MAX(DECODE(ATTRIB_NAME,'RMA_TIRE_PLY_CONSTRUCTION',ATTRIB_VALUE)) AS RMA_TIRE_PLY_CONSTRUCTION
		FROM 
			(SELECT MA.*,
                DENSE_RANK() OVER(PARTITION BY LPAD(MA.MATL_NUM,18,0), MA.ATTRIB_NAME ORDER BY MA.COUNTER DESC) RK
              FROM MATERIAL_ATTRIBUTE MA
             WHERE ATTRIB_NAME IN ('PRODUCT_TYPE' ,'SPEED_RATING' ,'STAMPED_SINGLE_LOAD_INDEX','TUBE_TYPE','LOAD_RANGE',
                                   'UTQG_TEMPERATURE','UTQG_TRACTION','UTQG_TREADWEAR','TECHNICAL_PLATFORM',
                                   'RMA_TIRE_PLY_CONSTRUCTION')
               AND MATL_NUM = LPAD(ps_matl_num,18,0) )
		WHERE RK = 1
		GROUP BY LPAD(Matl_Num,18,0));
     
		IF NVL(ps_loadrange, ' ') = 'C' AND pn_tiretypeid =  1 THEN
				ps_extraloadyn := 'Y';
		ELSIF NVL(ps_loadrange, ' ') = 'XL'  THEN
				ps_extraloadyn := 'Y';
		ELSE
				ps_extraloadyn := 'N';
		END IF;  

	EXCEPTION
		WHEN OTHERS THEN
		-- Consider logging the error and then re-raise
        ls_errormsg := SQLERRM ||  ' - GetTireCharacteristics. An error have ocurred.(when others)' || SQLERRM;
		
        APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> NULL,
                                                  ad_operatorid 	=> 'ICSDEV',
                                                  ad_daterecorded  	=> SYSDATE,
                                                  as_processname   	=> ' GetTireCharacteristics',
                                                  ax_recorddata    	=> 'ps_matl_num - '||ps_Matl_Num,
                                                  as_messagecode   	=> TO_CHAR(SQLCODE),
                                                  as_message       	=> ls_errormsg);
													
        RAISE_APPLICATION_ERROR(-20010, ls_errormsg);
		   
END GETTIRECHARACTERISTICS;
/

CREATE OR REPLACE PROCEDURE ICS_PROCS.UNLOAD_CERTIFICATEACTIVITY(ad_processdate     IN DATE,
																 as_extractfilename IN VARCHAR2,
																 as_okfilename      IN VARCHAR2)
--unloads certificate information for loading into SAP
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
    ls_recordcode                   VARCHAR2(4) := NULL;
    ls_textfile                     UTL_FILE.FILE_TYPE;
    ls_textline                     VARCHAR2(400) := NULL;
    ls_outtextfile                  UTL_FILE.FILE_TYPE;
    ls_outfilename                  VARCHAR2(12) := NULL;

    --  miscellaneous variables
    ln_reccnt                       NUMBER := NULL;
    lr_rowid                        ROWID;
    ld_processdate                  DATE := NULL;

    --  error processing variables
    ln_errcnt                       NUMBER := 0;
    ln_morecursor                   NUMBER := 0;
    ls_errordata                    VARCHAR2(2000) := NULL;
    ls_errorcode                    VARCHAR2(20) := NULL;
    ls_errormsg                     VARCHAR2(4000) := NULL;
    ln_numchars                     NUMBER := 0;
    ls_eventmsg                     VARCHAR2(100) := NULL;
    ln_count                        NUMBER := 0;

    ld_maxenddate                   DATE := NULL;
    ld_startdate                    DATE := NULL;
    ls_holdcertificationtypeid      NUMBER := 0;
    ln_holdmatlnum                  VARCHAR2(18) := NULL;
    ls_machineid                    VARCHAR2(50) := NULL;
    ls_operatorid                   VARCHAR2(50) := 'ICSDEV';
    ln_activecerts                  NUMBER := 0;
    ln_activedeletes                NUMBER := 0;                                                     --JBH_2.0

    --  THIS SQL STATEMENT EXTRACTS RECORDS FOR EXPORTING TO SAP.
    --  TABLES PRODUCT, PRODUCTCERTIFICATE, CERTIFICATE, CERTIFICATETYPE, AND COUNTRY ARE JOINED.
    --  IF A RECORD IN ANY ONE OF THESE TABLES HAS CHANGED (MODIFIEDON DATE = TODAY) OR 
    --  IF THE CERTIFICATE BECAME INVALID FOR THE PRODUCT YESTERDAY (NO LONGER IN EFFECT)  
    --  THEN WE EXTRACT A RECORD BECAUSE SOMETHING MAY HAVE CHANGED THAT AFFECTS THE VALIDITY OF THE CERTIFICATE FOR THIS PRODUCT.
    CURSOR lc_certs IS
        SELECT DISTINCT 'US' COUNTRY,
                        CT.CERTIFICATIONTYPEID,
                        UPPER(CT.CERTIFICATIONTYPENAME) CERTIFICATIONTYPENAME, 
                        ' ' CERTIFICATIONDESCRIPTION, 
                        'Y' CERTIFICATIONREQUIRED,
                        ' ' LANGUAGE,
                        P.MATL_NUM MATERIALNUMBER, 
                        P.SKU, 
                        P.SIZESTAMP,
                        (CASE
                            WHEN (NVL(P.BRAND,'X') <> 'X' )THEN P.BRAND||' '||P.BRAND_LINE  --USE BRAND + BRAND_LINE IF AVAILABLE
                            ELSE P.BRANDDESC
                        END) BRANDDESC,
                        (CASE                                                                       --JBH_2.0
                            WHEN  CT.CERTIFICATIONTYPEID = 2 OR CT.CERTIFICATIONTYPEID = 3          --JBH_2.0
								THEN  COALESCE(PC.DATEAPPROVED_CEGI, C.CERTDATEAPPROVED)            --JBH_2.0
                            ELSE  PC.OPER_DATE_APPROVED                                             --JBH_2.0
                        END) START_DATE,                                                            --JBH_2.0
						(CASE
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
                        END) END_DATE,
						'U' CHG_FLAG
        FROM PRODUCT P, 
             PRODUCTCERTIFICATE PC,
             CERTIFICATE C,   
             CERTIFICATIONTYPE CT, 
             COUNTRY CTY
        WHERE PC.CERTIFICATEID = C.CERTIFICATEID
            AND P.SKUID = PC.SKUID
            AND CT.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
            AND CTY.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
            AND PC.DATEAPPROVED_CEGI IS NOT NULL
			AND (CASE                                                                    --JBH_2.0
					WHEN CT.CERTIFICATIONTYPEID = 2 OR CT.CERTIFICATIONTYPEID = 3        --JBH_2.0
                        THEN COALESCE(PC.DATEAPPROVED_CEGI, C.CERTDATEAPPROVED)          --JBH_2.0
                    ELSE PC.OPER_DATE_APPROVED                                           --JBH_2.0
                END) IS NOT NULL                                                         --JBH_2.0
            AND ((CTY.MODIFIEDON) >= ld_processdate 
            OR (P.MODIFIEDON) >=  ld_processdate
            OR (CT.MODIFIEDON) >= ld_processdate
            OR (PC.MODIFIEDON) >= ld_processdate
            OR (c.modifiedon) >= ld_processdate)
            and c.certificationtypeid <>  3             --  don't extract NOM
            AND SUBSTR(P.MATL_NUM, 1, 9 ) = '000000090'  --  make sure this is a real material number
            AND SUBSTR(CTY.SAP_COUNTRY_KEY, 1, 1) <> '*' 
        ORDER BY P.MATL_NUM, 
                 CT.CERTIFICATIONTYPEID;

    lcr_certs lc_certs%ROWTYPE;

     --  JBH_2.0 - Added lc_deletes cursor
    CURSOR  lc_deletes IS
        SELECT DISTINCT 'US' COUNTRY, -- COUNTRY CODE IS NOT USED.
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
        FROM PRODUCT P, 
             PRODUCTCERTIFICATE PC,
             CERTIFICATE C,   
             CERTIFICATIONTYPE CT, 
             COUNTRY CTY
        WHERE PC.CERTIFICATEID = C.CERTIFICATEID
            AND P.SKUID = PC.SKUID
            AND CT.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
            AND CTY.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
            AND ((CTY.MODIFIEDON) >= ld_processdate 
            OR (P.MODIFIEDON)  >=  ld_processdate
            OR (CT.MODIFIEDON) >=  ld_processdate
            OR (PC.MODIFIEDON) >=  ld_processdate
            OR (C.MODIFIEDON)  >=  ld_processdate)
            AND C.CERTIFICATIONTYPEID <> 3              --  Exclude NOM Certificates
            AND SUBSTR(P.MATL_NUM,1,9)  = '000000090'   --  Finished Goods Only
            AND SUBSTR(CTY.SAP_COUNTRY_KEY,1,1) <>  '*' 
            AND ((CT.CERTIFICATIONTYPEID <> 2 
				AND PC.OPER_DATE_APPROVED IS NULL)  
				OR PC.DATEAPPROVED_CEGI IS NULL
				OR LOWER(C.ACTIVESTATUS) = 'n')
        ORDER BY P.MATL_NUM, 
                 CT.CERTIFICATIONTYPEID;

    lcr_deletes lc_deletes%ROWTYPE;

	BEGIN
		ld_processdate := NVL(ad_ProcessDate,SYSDATE);
		ld_processdate := TRUNC(ld_processdate);
		/*==========================================================================*/
		/*  Process SAP Additions and Updates 
		==========================================================================*/

		--  Initialize the Row Counter
		ln_reccnt := 0;

		--  Initialize Key Change Work Variables 
		ls_holdcertificationtypeid  := 0;
		ln_holdmatlnum              := 0;
      
		--  Open the SAP Extract File 
		ls_textfile := UTL_FILE.FOPEN('ICS_OUTPUT_DIR', as_ExtractFileName,'W');

		--  Read Through Changed Certifications
		OPEN lc_certs;
		LOOP
			FETCH lc_certs INTO lcr_certs;
			EXIT WHEN lc_certs%NOTFOUND;

			--  Write One Line per Certification Type and Material Number
			IF 
				ls_holdcertificationtypeid <> lcr_certs.certificationtypeid OR
				ln_holdmatlnum <> lcr_certs.materialnumber 
				THEN

				--  Initialize Start Date and End Date Work Variables
				ld_maxenddate := NULL;
				ld_startdate  := NULL;

				--  Check To See If There Are Any Currently Active Certificates
				SELECT COUNT(*) 
					INTO ln_activecerts
				FROM CERTIFICATE C1, 
                     PRODUCTCERTIFICATE PC1, 
                     PRODUCT P1
				WHERE C1.CERTIFICATEID = PC1.CERTIFICATEID
                    AND PC1.SKUID = P1.SKUID
                    AND P1.MATL_NUM = lcr_certs.MATERIALNUMBER
                    AND PC1.CERTIFICATIONTYPEID = lcr_certs.CERTIFICATIONTYPEID
                    AND UPPER(C1.ACTIVESTATUS)  = 'Y'
                    AND (PC1.DATEAPPROVED_CEGI IS NOT NULL or C1.CERTDATEAPPROVED IS NOT NULL)
                    AND (CASE                                                                     --JBH_2.0
                            WHEN  PC1.CERTIFICATIONTYPEID = 2 or PC1.CERTIFICATIONTYPEID = 3      --JBH_2.0
                            THEN  COALESCE(PC1.DATEAPPROVED_CEGI, C1.CERTDATEAPPROVED)            --JBH_2.0
                            ELSE  PC1.OPER_DATE_APPROVED                                          --JBH_2.0
                        END)    IS NOT NULL                                                       --JBH_2.0
                    AND PC1.DATEREMOVED IS NULL ;
           
					--  If the Material Id has Multiple Active Certificates  
					--  Retrieve the Earliest Start Date and the Latest Expiration Date 
					IF ln_activecerts > 0 THEN

						SELECT  
							MAX(CASE  -- See the lc_certs Cursor Definition for an Explanation of Following Date Calculations  
									WHEN C1.CERTIFICATIONTYPEID = 1 THEN  NULL
									WHEN C1.CERTIFICATIONTYPEID = 2 THEN  PC1.DATEAPPROVED_CEGI + 365
									WHEN C1.CERTIFICATIONTYPEID = 4 THEN  COALESCE(C1.EXPIRYDATE_I,PC1.DATEAPPROVED_CEGI + 1460) 
									WHEN C1.CERTIFICATIONTYPEID = 5 THEN  C1.EXPIRYDATE_I
									WHEN C1.CERTIFICATIONTYPEID = 6 THEN  NULL
									WHEN C1.CERTIFICATIONTYPEID = 7 THEN  COALESCE( C1.EXPIRYDATE_I,PC1.DATEAPPROVED_CEGI + 365) 
								END),
							MIN(CASE                                                                 --JBH_2.0
                                WHEN PC1.CERTIFICATIONTYPEID = 2 or PC1.CERTIFICATIONTYPEID = 3      --JBH_2.0
                                THEN COALESCE(PC1.DATEAPPROVED_CEGI, C1.CERTDATEAPPROVED)            --JBH_2.0
                                ELSE PC1.OPER_DATE_APPROVED                                          --JBH_2.0
								END)                                                                 --JBH_2.0
							INTO ld_maxenddate, 
                          ld_startdate
						FROM CERTIFICATE C1, 
							 PRODUCTCERTIFICATE PC1, 
							 PRODUCT P1
						WHERE C1.CERTIFICATEID  = PC1.CERTIFICATEID
							AND PC1.SKUID     = P1.SKUID
							AND P1.MATL_NUM   = lcr_certs.MATERIALNUMBER
							AND PC1.CERTIFICATIONTYPEID = lcr_certs.CERTIFICATIONTYPEID
							AND UPPER(C1.ACTIVESTATUS) = 'Y'
							AND (PC1.DATEAPPROVED_CEGI IS NOT NULL or C1.CERTDATEAPPROVED IS NOT NULL)
							AND (CASE                                                                    --JBH_2.0
									WHEN PC1.CERTIFICATIONTYPEID = 2 or PC1.CERTIFICATIONTYPEID = 3      --JBH_2.0
									THEN COALESCE(PC1.DATEAPPROVED_CEGI, C1.CERTDATEAPPROVED)            --JBH_2.0
									ELSE PC1.OPER_DATE_APPROVED                                          --JBH_2.0
								END) IS NOT NULL                                                     --JBH_2.0
							AND PC1.DATEREMOVED IS NULL ;

					ELSE
						--  Use the Cursor Dates When the Material Id has Only One Certificate  
						ld_maxenddate := lcr_certs.end_date;
						ld_startdate  := lcr_certs.start_date;
					END IF;
             
				--  Insert SAP Interface File Row  
				ls_textline := RPAD(NVL(lcr_certs.COUNTRY, ' '), 70, ' ');
				ls_textline := ls_textline || SUBSTR(TO_CHAR(NVL(lcr_certs.CERTIFICATIONTYPEID  ,'0'), '99'), 2, 2);
				ls_textline := ls_textline || RPAD(NVL(lcr_certs.CERTIFICATIONTYPENAME, ' '), 50, ' ');
				ls_textline := ls_textline || RPAD(' ', 30, ' '); --  CERTIFICATIONDESCRIPTION
				ls_textline := ls_textline || 'Y';                --  CERTIFICATIONREQUIRED
				ls_textline := ls_textline || RPAD(' ', 30, ' '); --  LANGUAGE
				ls_textline := ls_textline || RPAD(NVL(lcr_certs.MATERIALNUMBER, ' '), 18,' ');
				ls_textline := ls_textline || RPAD(NVL(lcr_certs.SKU, ' '), 10,' ');
				ls_textline := ls_textline || RPAD(NVL(lcr_certs.SIZESTAMP, ' '), 20,' ');
				ls_textline := ls_textline || RPAD(NVL(lcr_certs.BRANDDESC, ' '),100,' ');
				ls_textline := ls_textline || RPAD(NVL(TO_CHAR(ld_startdate, 'MM/DD/YYYY'),'01/01/1901'), 10, '0');
				ls_textline := ls_textline || RPAD(NVL(TO_CHAR(ld_maxenddate, 'MM/DD/YYYY'),'12/31/9999'), 10, '0');
				ls_textline := ls_textline || NVL(lcr_certs.CHG_FLAG, ' ');
                  
				UTL_FILE.PUT_LINE(ls_textfile, ls_textline);

				ln_reccnt := ln_reccnt + 1;
            
				--  Save Prior Key 
				ls_holdcertificationtypeid := lcr_certs.CERTIFICATIONTYPEID;
				ln_holdmatlnum := lcr_certs.MATERIALNUMBER;

			END IF;

		END LOOP;
        
		CLOSE lc_certs;

		--  JBH_2.0 - Added Deletions Processing Routine
		/*==========================================================================*/
		/*  Process SAP Deletions 
		==========================================================================*/

		--  Initialize Key Change Work Variables 
		ls_holdcertificationtypeid := 0;
		ln_holdmatlnum             := 0;     

		--  Initialize Date Variables 
		ld_maxenddate := NULL;
		ld_startdate  := NULL;

		OPEN lc_deletes;
		LOOP
			FETCH lc_deletes into lcr_deletes;
			EXIT  WHEN lc_deletes%NOTFOUND;

			--  Write One Line Per Certification Type And Material Id Combination
			IF 
				ls_holdcertificationtypeid <> lcr_deletes.CERTIFICATIONTYPEID OR
				ln_holdmatlnum <> lcr_certs.MATERIALNUMBER THEN

				--  Check To See If There Are Any Currently Active Certificates
				SELECT COUNT(*) 
				INTO ln_activedeletes
				FROM CERTIFICATE C1, 
                     PRODUCTCERTIFICATE PC1, 
                     PRODUCT P1
				WHERE C1.CERTIFICATEID = PC1.CERTIFICATEID
                    AND PC1.SKUID = P1.SKUID
                    AND P1.MATL_NUM = lcr_deletes.MATERIALNUMBER
                    AND PC1.CERTIFICATIONTYPEID = lcr_deletes.CERTIFICATIONTYPEID
                    AND UPPER(C1.ACTIVESTATUS) = 'Y'
                    AND PC1.OPER_DATE_APPROVED IS NOT NULL
                    AND PC1.DATEREMOVED IS NULL;

					--  Delete All CCC(5) and India(7) Certificates but Only Delete the  
					--  Other Certificates If They Don't have Another Active Certificate
					IF  
						(lcr_deletes.CERTIFICATIONTYPEID = '1' AND ln_activedeletes = 0) OR 
						(lcr_deletes.CERTIFICATIONTYPEID = '2' AND ln_activedeletes = 0) OR
						(lcr_deletes.CERTIFICATIONTYPEID = '4' AND ln_activedeletes = 0) OR
						(lcr_deletes.CERTIFICATIONTYPEID = '5') OR
						(lcr_deletes.CERTIFICATIONTYPEID = '6' AND ln_activedeletes = 0) OR
						(lcr_deletes.CERTIFICATIONTYPEID = '7')
						THEN 
						ls_textline := RPAD(NVL(lcr_deletes.COUNTRY, ' '), 70, ' ');
						ls_textline := ls_textline ||  SUBSTR(TO_CHAR(NVL(lcr_deletes.CERTIFICATIONTYPEID  ,'0'), '99'),2,2);
						ls_textline := ls_textline ||  RPAD(NVL(lcr_deletes.CERTIFICATIONTYPENAME  , ' '), 50, ' ');
						ls_textline := ls_textline ||  RPAD(' ', 30, ' '); -- CERTIFICATION DESCRIPTION
						ls_textline := ls_textline ||  'Y';                -- CERTIFICATION REQUIRED
						ls_textline := ls_textline ||  RPAD(' ', 30, ' '); -- LANGUAGE
						ls_textline := ls_textline ||  RPAD(NVL(lcr_deletes.MATERIALNUMBER , ' '),  18, ' ');
						ls_textline := ls_textline ||  RPAD(NVL(lcr_deletes.SKU, ' '),  10, ' ');
						ls_textline := ls_textline ||  RPAD(NVL(lcr_deletes.SIZESTAMP, ' '),  20, ' ');
						ls_textline := ls_textline ||  RPAD(NVL(lcr_deletes.BRANDDESC, ' '), 100, ' ');
						ls_textline := ls_textline ||  RPAD(NVL(TO_CHAR(ld_startdate, 'MM/DD/YYYY'),'01/01/1901'), 10, '0');
						ls_textline := ls_textline ||  RPAD(NVL(TO_CHAR(ld_maxenddate, 'MM/DD/YYYY'),'12/31/9999'), 10, '0');
						ls_textline := ls_textline ||  NVL(lcr_deletes.CHG_FLAG , 'D');
                    
						UTL_FILE.PUT_LINE(ls_textfile, ls_textline);

						ln_reccnt := ln_reccnt + 1;

					END IF;

					--  Save Prior Key 
					ls_holdcertificationtypeid  := lcr_deletes.certificationtypeid;
					ln_holdmatlnum  := lcr_deletes.materialnumber;

			END IF;

		END LOOP;
        
		CLOSE lc_deletes;

		/*==========================================================================*/
		/*  Completion Processing 
		==========================================================================*/

		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => ls_machineid,
												  ad_operatorid    => ls_operatorid,
												  ad_daterecorded  => SYSDATE,
												  as_processname   =>' UNLOAD_CERTIFICATEACTIVITY ',
												  ax_recorddata    => 'SUCCESS - '||ln_reccnt||' records exported',
												  as_messagecode   => 'SUCCESS',
												  as_message       => 'SUCCESS'); 
    
		UTL_FILE.FCLOSE_ALL;

	EXCEPTION
		WHEN NO_DATA_FOUND THEN
            NULL;

		WHEN OTHERS THEN
            ls_errormsg := SQLERRM;
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => ls_machineid,
													  ad_operatorid    => ls_operatorid,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => ' UNLOAD_CERTIFICATEACTIVITY ',
													  ax_recorddata    => 'An error has occurred at record '||ln_reccnt ,
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_errormsg);
            
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);

			RAISE;

	END UNLOAD_CERTIFICATEACTIVITY;
/

CREATE OR REPLACE PROCEDURE ICS_PROCS.UPDATEDISCSKU  
IS

	ln_reccount		NUMBER(6)	:= NULL;
    ln_errorcount	NUMBER(6)	:= NULL;
    ls_errorfound	VARCHAR2(1)	:= NULL;
 
	CURSOR lcr_DiscSkus IS
	SELECT DISTINCT P.SKUID,
		   P.SKU,
		   TRUNC(P.DISCONTINUEDDATE) AS PRODUCT_DISC_DATE,
		   "A"."DISC_DATE" AS SKU_DISC_DATE
	FROM PRODUCT P, 
		 AUTH_PROD_MV@BOM_DEVL.WORLD A
	WHERE P.SKU = "A"."SKU_ID" AND
		 TRUNC(NVL(TO_DATE(P.DISCONTINUEDDATE, 'DD-MON-YY'), SYSDATE)) <> NVL(TO_CHAR(TO_DATE("A"."DISC_DATE", 'MM/DD/YYYY'), 'DD-MON-YY'), SYSDATE);

	BEGIN
		--  Initialize variables  
		ln_errorcount  := 0;
		ln_reccount   := 0;
 
		---- GO THROUGH AND update the discontinued date
		FOR lcr_rec IN lcr_DiscSkus LOOP
			BEGIN
				UPDATE PRODUCT
                    SET DISCONTINUEDDATE = lcr_rec.SKU_DISC_DATE,
                        MODIFIEDON = SYSDATE,
                        MODIFIEDBY = 'UpdateDiscSku'
					WHERE SKUID = lcr_rec.skuid;
                      
				ln_reccount := ln_reccount + 1;
				COMMIT;
			EXCEPTION
                WHEN OTHERS THEN
                DECLARE
					lserrormsg VARCHAR2(300) := SQLERRM;
                BEGIN           
				lserrormsg := SUBSTR(lserrormsg, 1, 250) || ' (DB)';
				
				APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> 'fa0017',
														  ad_operatorid 	=> 'UpdateDiscSku',
														  ad_daterecorded 	=> SYSDATE,
														  as_processname 	=> 'Procedure UpdateDiscSku',
														  ax_recorddata 	=> 'An error has occurred',
														  as_messagecode	=> TO_CHAR(SQLCODE),
														  as_message 		=> lserrormsg );
                          
				ln_errorcount := ln_errorcount + 1;				
                END;
			END;
		END LOOP;

		IF ln_errorcount > 1 THEN
  
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineId 		=> ' ',
												  ad_operatorId 	=> 'UpdateDiscSku',
												  ad_dateRecorded 	=> SYSDATE,
												  as_processName 	=> 'Procedure UpdateDiscSku',
												  ax_recordData 	=> ' ',
												  as_messageCode	=> ' ',
												  as_message 		=> 'error count = '||ln_errorcount|| ' record count = '||ln_reccount);   
		END IF; 

	END UPDATEDISCSKU;
/

DROP PROCEDURE ICS_PROCS.CREATE_NEW_FAMILIES;
/

DROP PROCEDURE ICS_PROCS.FIX_ASPECT_RATIO;
/

DROP PROCEDURE ICS_PROCS.FIX_IMARK;
/

DROP PROCEDURE ICS_PROCS.FIX_INSERT;
/

DROP PROCEDURE ICS_PROCS.FIX_LOADRANGE;
/

DROP PROCEDURE ICS_PROCS.FIX_PRODUCT_FAMILY;
/

DROP PROCEDURE ICS_PROCS.FIX_PRODUCT_FAMILY_2;
/

DROP PROCEDURE ICS_PROCS.FIX_PRODUCT_FAMILY_3;
/

DROP PROCEDURE ICS_PROCS.FIX_RE_XL;
/

DROP PROCEDURE ICS_PROCS.FIX_SPEED_RATING;
/

DROP PROCEDURE ICS_PROCS.FIX_TIRETYPE;
/

DROP PROCEDURE ICS_PROCS.FIXEMARKDATES;
/

DROP PROCEDURE ICS_PROCS.FIXEXTENSIONS;
/

DROP PROCEDURE ICS_PROCS.FIXTEMPCERT;
/

DROP PROCEDURE ICS_PROCS.GETEMARKPASSENGERWITHTR_JILL;
/

DROP PROCEDURE ICS_PROCS.GSO_DATA_LOADER;
/

DROP PROCEDURE ICS_PROCS.LOAD_CCC;
/

DROP PROCEDURE ICS_PROCS.LOAD_CERT;
/

DROP PROCEDURE ICS_PROCS.LOAD_CERT_117;
/

DROP PROCEDURE ICS_PROCS.LOAD_IMARK;
/

DROP PROCEDURE ICS_PROCS.LOAD_JES_FIX;
/

DROP PROCEDURE ICS_PROCS.NOM_DATA_LOADER;
/

DROP PROCEDURE ICS_PROCS.TESTWRITE;
/
