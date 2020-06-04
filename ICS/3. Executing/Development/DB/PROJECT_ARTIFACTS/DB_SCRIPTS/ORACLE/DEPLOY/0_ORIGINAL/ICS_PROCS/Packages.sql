CREATE OR REPLACE PACKAGE	ICS_PROCS.APP_MESSAGE_OPERATIONS AS 
  
	TYPE retcursor IS REF CURSOR;  

	PROCEDURE GETALLEXCEPTIONS	(pc_retcursor OUT retcursor); 
	
	PROCEDURE APP_MESSAGE_INSERT	(as_machineid   	IN APP_MESSAGE.MACHINE_ID%TYPE,
									 ad_operatorid   	IN APP_MESSAGE.OPERATOR_ID%TYPE,
									 ad_daterecorded	IN APP_MESSAGE.DATE_RECORDED%TYPE,
									 as_processname  	IN APP_MESSAGE.PROCESS_NAME%TYPE,
									 ax_recorddata   	IN APP_MESSAGE.RECORDED_DATA%TYPE,
									 as_messagecode  	IN APP_MESSAGE.MESSAGE_CODE%TYPE,
									 as_message      	IN APP_MESSAGE.MESSAGE%TYPE);                          
END APP_MESSAGE_OPERATIONS;
/
  
CREATE OR REPLACE PACKAGE BODY ICS_PROCS.APP_MESSAGE_OPERATIONS AS

	PROCEDURE GETALLEXCEPTIONS	(pc_retcursor OUT retcursor) 
	AS 
	BEGIN
		OPEN pc_retcursor FOR			
			SELECT SEQ_NO,
				   OPERATOR_ID,
				   MACHINE_ID,
				   TO_CHAR(DATE_RECORDED,'YYYY-MM-DD HH24:MI:SS'),
				   PROCESS_NAME,
				   RECORDED_DATA,
				   MESSAGE_CODE,
				   MESSAGE
			FROM APP_MESSAGE;
		
	EXCEPTION
	WHEN OTHERS THEN
		pc_retcursor:=null;
			
	END GETALLEXCEPTIONS;	
	
	PROCEDURE APP_MESSAGE_INSERT	(as_machineid   	IN APP_MESSAGE.MACHINE_ID%TYPE,
									 ad_operatorid   	IN APP_MESSAGE.OPERATOR_ID%TYPE,
									 ad_daterecorded	IN APP_MESSAGE.DATE_RECORDED%TYPE,
									 as_processname  	IN APP_MESSAGE.PROCESS_NAME%TYPE,
									 ax_recorddata   	IN APP_MESSAGE.RECORDED_DATA%TYPE,
									 as_messagecode  	IN APP_MESSAGE.MESSAGE_CODE%TYPE,
									 as_message      	IN APP_MESSAGE.MESSAGE%TYPE)
	AS
	BEGIN
		INSERT INTO APP_MESSAGE (SEQ_NO,
								 OPERATOR_ID,
								 MACHINE_ID,
								 DATE_RECORDED,
								 PROCESS_NAME,
								 RECORDED_DATA,
								 MESSAGE_CODE,      
								 MESSAGE)
						  VALUES(APP_MESSAGE_SEQ.NEXTVAL,
								 as_machineid,
								 ad_operatorid,
								 ad_daterecorded,
								 as_processname,
								 ax_recorddata,
								 as_messagecode,
								 as_message);
		
		COMMIT;  
	
	EXCEPTION
		WHEN OTHERS THEN 
			NULL;
		
	END APP_MESSAGE_INSERT;

END APP_MESSAGE_OPERATIONS;
/

CREATE OR REPLACE PACKAGE	ICS_PROCS.BOM_ATTRIBUTES 
AS
	/******************************************************************************
	NAME:       BOM_ATTRIBUTES
	PURPOSE:
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        4/17/2011   arsherri       	1. Created this package.
	1.1        10/02/2012  Harini         	1. Commented Get_Brand_desc function
											2. Replaced ps_SKU with ps_Matl_Num in
											   Get_Load_Range,Get_Rim_Diameter,
											   Get_Product_Type,Get_Ece_Tire_Class,
											   Get_Imark_Family functions
	******************************************************************************/

	FUNCTION GET_PRODUCT_TYPE (ps_matl_num IN VARCHAR2) 
								RETURN NUMBER;
	
	FUNCTION GET_LOAD_RANGE	(ps_matl_num IN VARCHAR2) 
								RETURN VARCHAR2;
	
	FUNCTION GET_RIM_DIAMETER (ps_matl_num IN VARCHAR2) 
								RETURN NUMBER;
	
	FUNCTION GET_ASPECT_RATIO (ps_size_stamp 	IN VARCHAR2, 
							   pn_tire_type_id	IN NUMBER) 
								RETURN VARCHAR2;
	
	FUNCTION GET_IMARK_FAMILY (ps_matl_num 		IN VARCHAR2, 
							   pn_certificateid IN NUMBER) 
								RETURN VARCHAR2;
	
	FUNCTION GET_ECE_TIRE_CLASS	(ps_matl_num IN VARCHAR2) 
									RETURN VARCHAR2;

END BOM_ATTRIBUTES;
/

CREATE OR REPLACE PACKAGE BODY	ICS_PROCS.BOM_ATTRIBUTES 
AS
	/******************************************************************************
	NAME:       BOM_ATTRIBUTES
	PURPOSE:
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        4/17/2011   arsherri         1. Created this package body.
	1.1        10/02/2012  Harini           1. Commented Get_Brand_desc function
											2. Replaced ps_SKU with ps_Matl_Num in
											Get_Load_Range,Get_Rim_Diameter,Get_Product_Type,
											Get_Ece_Tire_Class functions and
											modified their functionalities by retrieving the outputs
											from CMDR_DATA.Material_Attribute table
	******************************************************************************/

	FUNCTION GET_PRODUCT_TYPE (ps_matl_num IN VARCHAR2) 
								RETURN NUMBER
	IS
	/*******************************************************************************
	NAME       : Get_Product_Type
	PURPOSE    : Returns the value of Product type for the given material
	REVISIONS  :
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1       10/02/2012   Harini           Replaced ps_SKU with ps_Matl_Num.
											Modified the logic by retrieving the
											Product_Type from CMDR_DATA.Material_Attribute
											table
	*******************************************************************************/
    ln_producttypecode   NUMBER(2) := 0;

	BEGIN
		SELECT CASE WHEN  MA.ATTRIB_VALUE = 'LIGHT TRUCK TIRE' THEN 3
					WHEN  MA.ATTRIB_VALUE = 'SPECIALTY TIRE'   THEN 4
					WHEN  MA.ATTRIB_VALUE = 'PASSENGER TIRE'   THEN 1
					WHEN  MA.ATTRIB_VALUE = 'CYCLE TIRE'       THEN 0
					WHEN  MA.ATTRIB_VALUE = 'TRUCK & BUS TIRE' THEN 7
				END
		INTO  ln_producttypecode
		FROM  CMDR_DATA.MATERIAL_ATTRIBUTE MA
		WHERE MA.MATL_NUM	= LPAD(ps_matl_num, 18, 0)
		AND MA.ATTRIB_NAME 	= 'PRODUCT_TYPE';
		
        RETURN (ln_producttypecode);

    EXCEPTION
        WHEN OTHERS THEN
			RETURN (0);
    
	END GET_PRODUCT_TYPE;

	FUNCTION GET_LOAD_RANGE	(ps_matl_num IN VARCHAR2) 
								RETURN VARCHAR2
	IS
	/*******************************************************************************
	NAME       : Get_Load_Range
	PURPOSE    : Returns the value of Load_Range for the given material
	REVISIONS  :
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/02/2012  Harini           Replaced ps_SKU with ps_Matl_Num.
											Modified the logic by retrieving the
											Load_Range from CMDR_DATA.Material_Attribute
											table
	*******************************************************************************/
	ls_loadrange  CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE;

	BEGIN
	
		ls_loadrange := '';
	
		SELECT MA.ATTRIB_VALUE INTO ls_loadrange
		FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA
		WHERE MA.MATL_NUM		=  LPAD(ps_matl_num, 18, 0)
			AND MA.ATTRIB_NAME  =  'LOAD_RANGE';
	
		RETURN (ls_loadrange);
	
	EXCEPTION
		WHEN OTHERS THEN
			RETURN(NULL);
	
	END GET_LOAD_RANGE;

	FUNCTION GET_RIM_DIAMETER (ps_matl_num IN VARCHAR2) 
								RETURN NUMBER
	IS
	/*******************************************************************************
	NAME       : Get_Rim_Diameter
	PURPOSE    : Returns the value of Rim_Diameter for the given material
	REVISIONS  :
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1       10/02/2012   Harini           Replaced ps_SKU with ps_Matl_Num.
											Modified the logic by retrieving the
											Rim_Diameter from CMDR_DATA.Material_Attribute
											table
	*******************************************************************************/
	ls_rimdiameter  CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE;

    BEGIN
	
		ls_rimdiameter  := '';
		
		SELECT MA.ATTRIB_VALUE INTO ls_rimdiameter
		FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA
		WHERE MA.MATL_NUM   =  LPAD(ps_matl_num, 18, 0)
			AND MA.ATTRIB_NAME  =  'RIM_DIAMETER';

        RETURN (TO_NUMBER(ls_rimdiameter));

    EXCEPTION
        WHEN OTHERS THEN
            RETURN(NULL);
    
	END GET_RIM_DIAMETER;

	FUNCTION GET_ASPECT_RATIO 	(ps_size_stamp 		IN VARCHAR2, 
								 pn_tire_type_id	IN NUMBER) 
									RETURN VARCHAR2
	IS
	ls_aspectratio  	VARCHAR2(4) := NULL;
	ln_aspectratiochk   NUMBER := NULL;

	BEGIN
	
		IF INSTR(ps_size_stamp, '/', 1, 1) = 0 THEN
			ls_aspectratio := NULL;
			
			IF pn_tire_type_id = 1 THEN
				ls_aspectratio := 82;
			END IF;
		
		ELSE
			
			ls_aspectratio := SUBSTR(ps_size_stamp,INSTR(ps_size_stamp,'/', 1, 1) + 1, 2);
			
			BEGIN
				ln_aspectratiochk := TO_NUMBER(ls_aspectratio);
				
				IF ln_aspectratiochk < 16 THEN
					ls_aspectratio := NULL;
				END IF;
			
			EXCEPTION
			WHEN OTHERS THEN
				ls_aspectratio := NULL;
			END;
		
		END IF;
	
		RETURN (ls_aspectratio);
	
	EXCEPTION
	WHEN OTHERS THEN
		RETURN NULL;
	
	END GET_ASPECT_RATIO;

	FUNCTION GET_IMARK_FAMILY (ps_matl_num 		IN VARCHAR2, 
							   pn_certificateid 	IN NUMBER) 
								RETURN VARCHAR2
	IS
	/*******************************************************************************
	NAME       : Get_Imark_Family
	PURPOSE    : Returns the value ofImark_Family for the given material
	REVISIONS  :
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/02/2012  Harini           Replaced ps_SKU with ps_Matl_Num.
	*******************************************************************************/
	ls_imarkfamily	VARCHAR2(5)		:= NULL;
	ls_familycode 	VARCHAR2(10)	:= NULL;
	ln_imarkfamily	NUMBER			:= NULL;
	ln_errornum   	NUMBER(2)		:= NULL;
	ls_errordesc  	VARCHAR2(300)	:= NULL;

	BEGIN	
		SIMILAR_TIRES.GET_IMARK_FAMILY (ps_matl_num, 
										pn_certificateid,
										ln_imarkfamily, 
										ls_familycode, 
										ln_errornum, 
										ls_errordesc);
	
		IF ln_errornum = 0 THEN
			ls_imarkfamily := TO_CHAR(ln_imarkfamily);
		ELSE
			ls_imarkfamily := 0;
		END IF;
	
		RETURN ls_imarkfamily;
	
	EXCEPTION
		WHEN OTHERS THEN
			RETURN NULL;
	
	END GET_IMARK_FAMILY;


	FUNCTION GET_ECE_TIRE_CLASS	(ps_matl_num IN VARCHAR2) 
									RETURN VARCHAR2
	IS
	/*******************************************************************************
	NAME       : Get_Ece_Tire_Class
	PURPOSE    : Returns the value of Ece_Tire_Class for the given material
	REVISIONS  :
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1       10/02/2012    Harini        Replaced ps_SKU with ps_Matl_Num.
											Modified the logic by retrieving the
											Ece_Tire_Class from
											CMDR_DATA.Material_Attribute table
	*******************************************************************************/
	ln_tiretypeid   	PRODUCT.TIRETYPEID%TYPE := NULL;
	ls_speedrating  	MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE := NULL;
	ln_singloadindex	MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE := NULL;
	ls_ecetireclass 	VARCHAR2(10) := NULL;
	ln_srindex      	QS_SPEED_RATING.SR_INDEX%TYPE := NULL;
	le_nodata       	EXCEPTION;

	BEGIN

		BEGIN
			SELECT MA1.ATTRIB_VALUE,
				   NVL(TO_NUMBER(MA2.ATTRIB_VALUE), 0) SLOAD_IDX
			INTO   ls_speedrating,
				   ln_singloadindex
			FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA1,
				 CMDR_DATA.MATERIAL_ATTRIBUTE MA2
			WHERE (MA1.MATL_NUM = LPAD(ps_matl_num, 18, 0) 
				AND MA1.ATTRIB_NAME = 'SPEED_RATING')
				AND (MA2.MATL_NUM = LPAD(ps_matl_num, 18, 0) 
				AND MA2.ATTRIB_NAME = 'STAMPED_SINGLE_LOAD_INDEX');
		
		EXCEPTION
			WHEN OTHERS THEN
				RAISE le_nodata;
		END;

		ln_tiretypeid := BOM_ATTRIBUTES.GET_PRODUCT_TYPE(ps_matl_num); -- As per PRJ3617,replaced SKU with Matl_Num

		IF ln_tiretypeid = 0 THEN
			RAISE le_nodata;
		END IF;

		IF ln_tiretypeid = 1 THEN
			ls_ecetireclass := 'C1';
		ELSIF ln_singloadindex >= 122 THEN
			ls_ecetireclass := 'C3';
		ELSE
			SELECT sr_index INTO ln_srindex
			FROM qs_speed_rating
			WHERE product_type_code = 2
				AND speed_rating = ls_SpeedRating;

			IF (ln_srindex >= 1) AND (ln_singloadindex <= 121) THEN
				ls_ecetireclass := 'C2';
			ELSE
				ls_ecetireclass := 'C3';
			END IF;

		END IF;

		RETURN ls_ecetireclass;

	EXCEPTION
		WHEN le_nodata THEN
			RETURN NULL;
		WHEN OTHERS THEN
			RETURN NULL;
	
	END GET_ECE_TIRE_CLASS;

END BOM_ATTRIBUTES;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.CERTIFICATION_CRUD 
AS
	/******************************************************************************
	NAME:       CERTIFICATION_CRUD
	PURPOSE:
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/02/2012    Harini         1. Replaced ps_SKU with ps_Matl_Num in
											   GetSimilarCertificateInfo,Certificate_Save,
											   CertificateBasicInfo_Save,CertificateSimTire_Save
											   procedures
	1.2        10/08/2012    Harini         1. Added ps_BrandLine as input in
											   GET_CERTIFICATIONSEARCHRESULT procedure
											2. Replaced ps_removeSKU with ps_Remove_Matl_Num
											   in Certificate_Save procedure
	1.3         03/04/2013   Venkat         1. Added product discontinueddate and TPN
											   to the output cursor of getCertificatesInfo procedure
											2. Modfied CertificateBasicInfo_Save to capture
											   Certificate Extension.
	1.4         10/22/2013   Harini         1. Added GetSKUDescriptors procedure to eliminate
											   Product data webservice from ICS application.
	1.5         10/24/2013   Harini         1. Modified Certificate_Save by adding new parameter
											   ps_Family_Desc to update product table as suggested by Rajiv
	1.6         11/22/2013   Guru           1. Changed logic so that if extension entered to look for is *,
											   it returns all skus on all extensions; if an extension is entered,
											   only gets what was added on that extension. If ‘H’ is entered,
											   only brings back skus on “highest” extension in GET_CERTIFICATIONSEARCHRESULT
											   and GetCertificatesInfo procedures.
											2. Modified the logic of Certificate_Save procedrue such that onlyGSO types
											   can modify the Cretificate numbers.
	1.7         11/27/2013   Harini         1. Added ps_InsertPC,pn_Error_Num, ps_ErrorMsg in CertificateBasicInfo_Save procedure
											   and modified the speedrating and extensions logic.
											2. Modified the CertificateSimTirE_Save procedures as the paramters are added in
											   ICS_Common_Functions pcakage.
	2.00        09/09/2014   Joe Hill       Added ps_MoldChangeRequest and pd_OperApprovalDate in Certificate_Save procedure
	******************************************************************************/
	
	TYPE retcursor IS REF CURSOR;
	
	--Creates the Log Entry on the Audit Log table
	PROCEDURE AUDITLOG_INSERT(pd_changedatetime 		IN DATE,
							  ps_changedby      		IN VARCHAR2,
							  ps_area           		IN VARCHAR2,
							  ps_changedfiled_element	IN VARCHAR2,
							  ps_oldvalue      			IN VARCHAR2,
							  ps_newvalue      			IN VARCHAR2,
							  pi_reasonid       		IN NUMBER,
							  ps_note             		IN VARCHAR2);
	
	PROCEDURE  AUDITLOG_UPDATEAPPROVALSTATUS(pi_changelogid 	IN NUMBER,
											 pd_changedatetime	IN DATE, 
											 ps_status 			IN VARCHAR2,
											 ps_approver 		IN VARCHAR2);
	
	--Get the information from the   Audit Log table depending on the search criteria
	PROCEDURE GET_AUDITLOG(pc_retcursor OUT retcursor);
	
	PROCEDURE GET_AUDITLOGFORDATE(pc_retcursor 		   OUT retcursor,
								  pd_changedatetime	IN DATE);
	
	PROCEDURE GET_AUDITLOGAFTERDATE(pc_retcursor 		   OUT retcursor,
									pd_changedatetime	IN DATE);
	
	PROCEDURE GET_AUDITLOGBYUSER(pc_retcursor	   OUT retcursor,
								 ps_changedby	IN VARCHAR2);
	
	PROCEDURE GET_AUDITLOGBYFIELDCHANGED(pc_retcursor				   OUT retcursor,
										 ps_changedfiled_element	IN VARCHAR2);
	
	PROCEDURE GET_AUDITLOGBYAREA(pc_retcursor	  OUT retcursor,
								 ps_area		IN VARCHAR2);
	
	-- Get the change approval reasons
	PROCEDURE GET_APPROVALREASONS(pi_certificationtypeid	IN NUMBER, 
								  pc_retcursor				   OUT retcursor);
	
	PROCEDURE GETAPPROVEDSUBSTITUTION(pi_certificationtypeid	IN NUMBER, 
									  ps_field 					IN VARCHAR2, 
									  pi_value 					IN NUMBER, 
									  pi_skuid 					IN NUMBER, 
									  pi_newvalue 				   OUT NUMBER);
	
	PROCEDURE GET_CERTIFICATIONBYBRANDCODE(pc_retcursor    OUT retcursor,
										   ps_brandcode	IN VARCHAR2);
	
	PROCEDURE GET_CERTIFICATIONSEARCHRESULT(pc_retcursor 		   OUT retcursor,
											ps_searchcriteria	IN VARCHAR2,
											ps_searchtype 		IN VARCHAR2,
											ps_extensionno 		IN VARCHAR2,
											ps_imarkfamily 		IN VARCHAR2,
											ps_brandline   		IN VARCHAR2);
	
	PROCEDURE GETCERTIFICATESINFO(pc_retcursor 				   OUT retcursor,
								  ps_certificationnumber	IN VARCHAR2,
								  ps_extensionno 			IN VARCHAR2,
								  pi_certificationtypeid 	IN NUMBER,
								  pi_skuid 					IN NUMBER,
								  ps_trexists 				   OUT VARCHAR2);
	
	PROCEDURE GETSIMILARCERTIFICATEINFO(pc_retcursor 			   OUT retcursor,
										ps_matl_num 			IN VARCHAR2,
										pi_certificationtypeid	IN NUMBER, 
										ps_certificationnumber	IN VARCHAR2);
	
	PROCEDURE CERTIFICATE_UPDATE_BATCH(ps_certificationtypename IN VARCHAR2, 
									   ps_temp_batchnumber_g	IN VARCHAR2, 
									   ps_batchnumber_g			IN VARCHAR2, 
									   ps_username				IN VARCHAR2);

	PROCEDURE CERTIFICATE_SAVE(pi_retid 						   OUT NUMBER,
							   pi_certificateid 				IN NUMBER,
							   pi_skuid 						IN NUMBER,
							   ps_matl_num 						IN VARCHAR2,
							   ps_remove_matl_num 				IN VARCHAR2,
							   ps_certificationtypename 		IN VARCHAR2,
							   ps_certificatenumber 			IN VARCHAR2,
							   pd_certdatesubmitted 			IN DATE,
							   pd_certdateapproved_cegi 		IN DATE,
							   pd_datesubmited 					IN DATE,
							   pc_activestatus 					IN VARCHAR2,
							   pd_dateassigned_egi 				IN DATE,
							   pd_dateapproved_cegi 			IN DATE,
							   pc_renewalrequired_cgin  		IN VARCHAR2,
							   ps_jobreportnumber_cen 			IN VARCHAR2,
							   ps_extension_en 					IN VARCHAR2,
							   ps_supplementalmoldstamping_e	IN VARCHAR2,
							   ps_emarkreference_i 				IN VARCHAR2,
							   pd_expirydate_i 					IN DATE,
							   ps_family_i 						IN VARCHAR2,
							   ps_family_desc 					IN VARCHAR2,
							   ps_productlocation 				IN VARCHAR2,
							   ps_countryofmanufacture_n 		IN VARCHAR2,
							   ps_addnewcustomer 				IN VARCHAR2,
							   ps_actsigreq 					IN VARCHAR2,
							   pi_customerid 					IN NUMBER,
							   ps_customer_n 					IN VARCHAR2,
							   ps_customeraddress_n 			IN VARCHAR2,
							   ps_customerspecific_n 			IN VARCHAR2,
							   ps_addnewimporter 				IN VARCHAR2,
							   pi_importerid 					IN NUMBER,
							   ps_importer_n 					IN VARCHAR2,
							   ps_importeraddress_n 			IN VARCHAR2,
							   ps_importerrepresentative_n 		IN VARCHAR2,
							   ps_countrylocation_n 			IN VARCHAR2,
							   ps_batchnumber_g  				IN VARCHAR2,
							   ps_companyname 					IN VARCHAR2,
							   ps_username 						IN VARCHAR2,
							   ps_mold_changed 					IN VARCHAR2,                 -- JBH_2.00
							   pd_oper_date_approved 			IN DATE,                     -- JBH_2.00
							   ps_additional_info 				IN VARCHAR2                  -- JESEITZ 10/30/2016
							   );

	PROCEDURE CERTIFICATEBASICINFO_SAVE(ps_matl_num            IN VARCHAR2,
										pi_certificationtypeid IN NUMBER,
										ps_certificatenumber   IN VARCHAR2,
										pi_importerid          IN NUMBER,
										pi_customerid          IN NUMBER,
										ps_operatorname        IN VARCHAR2,
										ps_extension_en        IN NUMBER,
										ps_insertpc            IN VARCHAR2,
										pn_error_num              OUT NUMBER,
										ps_errormsg               OUT VARCHAR2);
	
	PROCEDURE CERTIFICATESIMTIRE_SAVE(ps_matl_num            IN	VARCHAR2,
									  pi_certificationtypeid IN NUMBER,
									  ps_certificatenumber   IN VARCHAR2,
									  ps_operatorname        IN VARCHAR2);

	PROCEDURE CERTIFICATE_ARCHIVE(ps_certificatenumber 	IN VARCHAR2,
								  ps_operatorname 		IN VARCHAR2);
	
	PROCEDURE GETDEFAULTVALUES(pc_retcursor    OUT retcursor,
							   ps_number	   OUT CERTIFICATE.CERTIFICATENUMBER%TYPE,
							   ps_typename	IN CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%TYPE,
							   pi_numberid	IN CERTIFICATE.CERTIFICATEID%TYPE);

	PROCEDURE CERTIFICATEDEFAULTVALUE_SAVE(pi_fieldvalueid 			IN NUMBER, 
										   pi_certificationtypeid	IN NUMBER, 
										   ps_fieldvalue 			IN VARCHAR2, 
										   ps_certificatenumber 	IN VARCHAR2);
	
	PROCEDURE CERTIFICTYPEDEFAULTVALUE_SAVE(pi_fieldvalueid 		IN NUMBER,
											pi_certificationtypeid	IN NUMBER,
											ps_fieldvalue 			IN VARCHAR2);
	
	FUNCTION CHECKIFCERTIFICATEDFEXIST(pi_fieldid				IN NUMBER,
									   pi_certificationtypeid	IN NUMBER,
									   pi_certificateid			IN NUMBER) 
											RETURN VARCHAR2;
	
	PROCEDURE IMARKCERTIFICATE_RENEW(pi_newid 			   OUT NUMBER, 
									 pi_oldid 			IN NUMBER, 
									 ps_operatorname	IN VARCHAR2);
	
	PROCEDURE ADDNEWSKUSTOIMARKCERTIFICATE(pi_skuid		IN NUMBER,
										   pi_countryid	IN NUMBER);
	
	PROCEDURE GETCERTIFEXTENSION(pi_certificateid	IN NUMBER, 
								 ps_extensionnumber	   OUT VARCHAR2);
	
	PROCEDURE GETLATESTIMARKCERTIFID(pi_certificateid OUT NUMBER);
	
	PROCEDURE GETLATESTGSOCERTIFNUMBER(ps_certificatenumber	OUT VARCHAR2);
	
	Procedure GET_BRANDS(pc_retcursor OUT retcursor);

	Procedure GET_BRANDLINES(ps_brand		IN VARCHAR2,
							 pc_retcursor 	   OUT retcursor);

	PROCEDURE GETSKUDESCRIPTORS(ps_Matl_Num		IN VARCHAR2,
								pc_retcursor	   OUT retcursor);
							
END CERTIFICATION_CRUD;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.CERTIFICATION_CRUD 
AS
	/******************************************************************************
	NAME:       CERTIFICATION_CRUD
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/02/2012    Harini         1. Replaced ps_SKU with ps_Matl_Num in
											   GETSIMILARCERTIFICATEINFO,CERTIFICATE_SAVE,
											   CERTIFICATEBASICINFO_SAVE,
											   CERTIFICATESIMTIRE_SAVE  procedures.
											   Modified GETCERTIFICATESINFO procedure
											   such that it should have Matl_num in
											   the select list
										    2. Replaced ps_removeSKU with ps_Remove_Matl_Num
										       in CERTIFICATE_SAVE procedure
	1.2         11/15/2012   Harini         3. Adding SYSdate while inserting in ProductCertificate
											   table in CERTIFICATEBASICINFO_SAVE and ADDNEWSKUSTOIMARKCERTIFICATE sp's
	1.3         03/04/2013   Venkat         1. Added product discontinueddate and TPN
											   to the output cursor of GETCERTIFICATESINFO procedure
										    2. Modfied CERTIFICATEBASICINFO_SAVE to capture
										       Certificate Extension.
	1.4         10/22/2013  Harini          1. Added GETSKUDESCRIPTORS procedure to eliminate ProductDatWebService
										       from ICS application.
	1.5         10/24/2013   Harini         1. Modified CERTIFICATE_SAVE by adding new parameter
										       ps_Family_Desc to update product table as suggested by Rajiv
	1.6         11/22/2013   Guru           1. Changed logic so that if extension entered to look for is *,
										       it returns all skus on all extensions; if an extension is entered,
										       only gets what was added on that extension. If `H' is entered,
										       only brings back skus on "highest" extension in GET_CERTIFICATIONSEARCHRESULT
										       and GETCERTIFICATESINFO procedures.
										    2. Modified the logic of CERTIFICATE_SAVE procedrue such that onlyGSO types
										       can modify the Cretificate numbers.
	1.7         11/25/2013   Harini         1. Modified CERTIFICATEBASICINFO_SAVE,CERTIFICATESIMTIRE_SAVE procedures
										       to implement extension and speedratinigs logic.
										    2. Added ps_InsertPC,pn_Error_Num, ps_ErrorMsg in CERTIFICATEBASICINFO_SAVE procedure
	1.8         01/10/2014   Harini         1. Added exception when retrieving the speed rating and condition is modified such that both the speed ratings
											   should not be null in CertifiacteBasicInfo_Save procedure.
										    2. Commented the call to ProductCountry_Save and replace that call with deleting the Productcountry and call
										       ProductCertification_Save procedure in CERTIFICATE_SAVE procedure
										    3. Added CERTIFICATENUMBER,EXTENSION_EN in the select list else clause of Extension of Certifiation No search.
	1.9        01/23/2014    Harini         1. Modified CERTIFICATE_SAVE procedure by using pi_certificateid instead of li_CertificateId as it is not filled.
	1.10       03/05/2014    Harini         1. Modified CERTIFICATEBASICINFO_SAVE procedure,incrementing extension_no in case of existing certificate and insert
										       in Certificate table. Modified DatApproved and MatlApproved logic.
										    2. Took CertificateId seq.next val into a varibale and assigned it to CertificateId while inserting the records in Certificate table
	1.12      04/02/2014   Jeseitz          1. ordered by material NUMBER in search by Imark in get_certificationsearchresult
											2. truncated sysdate in certificate_udpate_batch update when assigning to date submitted.
											3. in search by Imark in get_certificationsearchresult changed status to case statement instead of function so that if picks up
											   individual material's status instead of certificate status.
	 1.13     04/15/2014  jeseitz           1. GETCERTIFICATESINFO - Imark part of IF clause was not getting by SKUID - need to be able to get different data for different
											   speedratings of same material.
											2. in search by Imark in get_certificationsearchresult changed to not use customer function since not applicable for IMARKS
	 1.14    07/08/2014 jeseitz             1. If certificationtypeid = 6 (E117) copy the supplemental mold stamping from the next lowest extension if present
											2.  If this a new E117 certificate NUMBER (certifidationtypeid = 6) allow the first extension to be greater than 0. This is due to the government renaming certificates.
	2.00       09/09/2014  Joe Hill (JBH)   Procedures Modified:  CERTIFICATE_SAVE, GETCERTIFICATESINFO, GETSIMILARCERTIFICATEINFO, IMARKCERTIFICATE_RENEW
										    Added processing for Mold Change Requested and Operations Approval Date fields
											added to Product Certificate table.
	2.01     03/23/2015  jeseitz            Get_CertificationSearchResult - changed case statement for status so that it checks  if the productcountry status is
											null, it also must have a skuid that is not 0.  This is because of the left outer join - if there is no productcountry record
											we do not want the status to be 'Requested'
			   10/30/2016 jeseitz           Procedures Modified:  CERTIFICATE_SAVE, GETCERTIFICATESINFO, GETSIMILARCERTIFICATEINFO, IMARKCERTIFICATE_RENEW
										    Added processing for ADDITIONAL_INFO
											added to Product Certificate table.
	******************************************************************************/
	PROCEDURE AUDITLOG_INSERT(pd_changedatetime 		IN DATE,
							  ps_changedby      		IN VARCHAR2,
							  ps_area           		IN VARCHAR2,
							  ps_changedfiled_element	IN VARCHAR2,
							  ps_oldvalue      			IN VARCHAR2,
							  ps_newvalue      			IN VARCHAR2,
							  pi_reasonid       		IN NUMBER,
							  ps_note             		IN VARCHAR2) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 		VARCHAR2(50)	:= NULL;
	ls_operatorid 		VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 		VARCHAR2(4000) 	:= NULL;
	ls_approved 		VARCHAR2(1) 	:= NULL;
	ls_approvalstatus 	VARCHAR2(1) 	:= NULL;
	
	BEGIN
		
		IF  ps_changedby IS NOT NULL THEN
			ls_operatorid := ps_changedby;
		END IF;
		
		SELECT approvedyn 
			INTO ls_approved
		FROM certification_approval_reasons
		WHERE reasonid = pi_reasonid;
		
		IF LOWER(ls_approved) = 'y' THEN
			ls_approvalstatus := 'a';
		ELSE
			ls_approvalstatus := 'p';
		END IF;
		
		INSERT INTO CERTIFICATION_AUDIT_LOG(CHANGELOGID,
											CHANGEDATETIME,
											CHANGEDBY,
											AREA,
											CHANGEDFILED_ELEMENT,
											OLDVALUE,
											NEWVALUE,
											APPROVALSTATUS,
											REASONID,
											NOTE)
									 VALUES(CHANGELOGID_SEQ.NEXTVAL,
											pd_changedatetime,
											ps_changedby,
											ps_area,
											ps_changedfiled_element,
											ps_oldvalue,
											ps_newvalue,
											ls_approvalstatus,
											pi_reasonid,
											ps_note);
	
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || '- AUDITLOG_INSERT.  There are NULL parameters.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     	=> ls_machineid,
													  ad_operatorid    	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' CERTIFICATION_CRUD.AUDITLOG_INSERT',
													  ax_recorddata    	=> 'pi_certificationId is Invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message      	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || '- AUDITLOG_INSERT.  An error have ocurred.(WHEN OTHERS)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid    	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=>' CERTIFICATION_CRUD.AUDITLOG_INSERT',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
				 
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
				 
	END AUDITLOG_INSERT;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE  AUDITLOG_UPDATEAPPROVALSTATUS(pi_changelogid 	IN NUMBER,
											 pd_changedatetime	IN DATE, 
											 ps_status 			IN VARCHAR2,
											 ps_approver 		IN VARCHAR2) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
		
		IF ps_status IS NULL OR pi_changelogid IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		UPDATE CERTIFICATION_AUDIT_LOG 
		SET APPROVALSTATUS = ps_status,
			CHANGEDATETIME = pd_changedatetime,
			APPROVER       = ps_approver
		WHERE CHANGELOGID  = pi_changelogid;
	
	EXCEPTION
		WHEN le_parametersnull THEN
		
			ls_errormsg := SQLERRM || ' - AUDITLOG_UPDATEAPPROVALSTATUS. There are NULL parameters.';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													ad_operatorid    	=> ls_operatorid,
													ad_daterecorded  	=> SYSDATE,
													as_processname   	=> ' CERTIFICATION_CRUD.AUDITLOG_UPDATEAPPROVALSTATUS',
													ax_recorddata    	=> 'pi_certificationId IS Invalid.',
													as_messagecode   	=> TO_CHAR(SQLCODE),
													as_message      	=> ls_errormsg);
				
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		
		WHEN OTHERS THEN
			
			ls_errormsg := SQLERRM || ' - AUDITLOG_UPDATEAPPROVALSTATUS. An error have ocurred.(WHEN OTHERS)';
			   
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => ls_machineid,
													  ad_operatorid    => ls_operatorid,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => ' CERTIFICATION_CRUD.AUDITLOG_UPDATEAPPROVALSTATUS',
													  ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_errormsg);
			
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
	END AUDITLOG_UPDATEAPPROVALSTATUS;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_AUDITLOG(pc_retcursor OUT retcursor) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
		
		OPEN pc_retcursor FOR
			SELECT CHANGELOGID,
				   CHANGEDATETIME,
				   CHANGEDBY,
				   AREA,
				   CHANGEDFILED_ELEMENT,
				   OLDVALUE,
				   NEWVALUE,
				   APPROVALSTATUS,
				   APPROVER
			FROM CERTIFICATION_AUDIT_LOG
			ORDER BY CHANGEDATETIME DESC;
	 
	EXCEPTION
	  
		WHEN OTHERS THEN			
			ls_errormsg := SQLERRM || ' - AUDITLOG_UPDATEAPPROVALSTATUS. An error have ocurred.(WHEN OTHERS)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD..GET_AUDITLOG',
													  ax_recorddata		=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
	END GET_AUDITLOG;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_APPROVALREASONS(pi_certificationtypeid	IN NUMBER, 
								  pc_retcursor				   OUT retcursor) 
	AS
	
	-- Declare variables
	ls_machineid	VARCHAR2(50) 	:= NULL;
	ls_operatorid	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg		VARCHAR2(4000) 	:= NULL;
	
	BEGIN
	  
		OPEN pc_retcursor FOR
			SELECT CAR.REASONID,
				   CAR.REASON,
				   CAR.APPROVEDYN
			FROM CERTIFICATION_APPROVAL_REASONS CAR
			WHERE CAR.CERTIFICATIONTYPEID = pi_certificationtypeid
			ORDER BY CAR.REASON ASC;
	
	EXCEPTION
	  
	  WHEN OTHERS THEN
		ls_errormsg := SQLERRM || ' - GET_APPROVALREASONS. An error have ocurred.(WHEN OTHERS)';
		   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded	=> SYSDATE,
												  as_processname 	=> '  CERTIFICATION_CRUD.GET_APPROVALREASONS',
												  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
												  as_messagecode 	=> TO_CHAR(SQLCODE),
												  as_message       	=> SQLERRM);
		
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
		
	END GET_APPROVALREASONS;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETAPPROVEDSUBSTITUTION(pi_certificationtypeid	IN NUMBER, 
									  ps_field 					IN VARCHAR2, 
									  pi_value 					IN NUMBER, 
									  pi_skuid 					IN NUMBER, 
									  pi_newvalue 				   OUT NUMBER) 
	AS
	
	-- Declare variables
	ls_machineid        VARCHAR2(50) 	:= NULL;
	ls_operatorid       VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg         VARCHAR2(4000) 	:= NULL;
	li_tiretypeid       NUMBER 			:= NULL;
	li_startvalue       NUMBER 			:= NULL;
	li_endvalue         NUMBER 			:= NULL;
	li_replacementvalue	NUMBER 			:= NULL;
	li_newvalue 		NUMBER 			:= NULL;
	
	-- Declare cursor
	CURSOR c_subs IS
		SELECT STARTVALUE, 
			   ENDVALUE, 
			   REPLACEMENTVALUE
		FROM APPROVEDSUBSTITUTIONS
		WHERE CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND UPPER(FIELD) = UPPER(ps_field);
	
	BEGIN
		-- Initialize new value to original value
		li_newvalue := pi_Value;
		
		-- Get tire type from product table
		SELECT P.TIRETYPEID INTO li_tiretypeid
		FROM PRODUCT P
		WHERE P.SKUID = pi_skuid;
		
		IF li_tiretypeid = 1 THEN  -- All current approved substitutions are only for passenger
			
			IF pi_certificationtypeid = 2  OR pi_certificationtypeid = 3 THEN -- GSO
				
				OPEN c_subs;
				
				LOOP
					FETCH c_subs INTO li_startvalue, li_endvalue, li_replacementvalue;
					EXIT WHEN c_subs%NOTFOUND;
					
					IF pi_Value BETWEEN li_startvalue AND li_endvalue THEN
						li_newvalue := li_replacementvalue;
					END IF;
				
				END LOOP;
				
				CLOSE c_subs;
			
			END IF;
		
		END IF;
		
		pi_newvalue := li_newvalue;
	
	EXCEPTION
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GETAPPROVEDSUBSTITUTIONS. An error have ocurred.(WHEN OTHERS)';
		   
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GETAPPROVEDSUBSTITUTIONS',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
	END GETAPPROVEDSUBSTITUTION;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_AUDITLOGFORDATE(pc_retcursor 		   OUT retcursor,
								  pd_changedatetime	IN DATE) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
		
		IF pd_changedatetime IS NULL THEN
			RAISE le_parametersnull;
		END IF;
	
		OPEN pc_retcursor FOR			
			SELECT CHANGELOGID,
				   CHANGEDATETIME,
				   CHANGEDBY,
				   AREA,
				   CHANGEDFILED_ELEMENT,
				   OLDVALUE,
				   NEWVALUE,
				   APPROVALSTATUS,
				   APPROVER
			FROM CERTIFICATION_AUDIT_LOG
			WHERE CHANGEDATETIME = pd_changedatetime
			ORDER BY CHANGEDATETIME DESC;
	
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGFORDATE. There are NULL parameters.';
				
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded	=> SYSDATE,
												  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGFORDATE',
												  ax_recorddata    	=> 'pd_changedatetime IS NULL.',
												  as_messagecode 	=> TO_CHAR(SQLCODE),
												  as_message       	=> SQLERRM);
			 
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
			
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGFORDATE. An error have ocurred.(WHEN OTHERS)';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGFORDATE',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
				
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
				
	END GET_AUDITLOGFORDATE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_AUDITLOGAFTERDATE(pc_retcursor 		   OUT retcursor,
									pd_changedatetime	IN DATE) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
	
		IF pd_changedatetime IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		OPEN pc_retcursor FOR
			SELECT CHANGELOGID,
				   CHANGEDATETIME,
				   CHANGEDBY,
				   AREA,
				   CHANGEDFILED_ELEMENT,
				   OLDVALUE,
				   NEWVALUE,
				   APPROVALSTATUS,
				   APPROVER,
				   NOTE
			FROM  CERTIFICATION_AUDIT_LOG
			WHERE CHANGEDATETIME >= pd_changedatetime
			ORDER BY CHANGEDATETIME DESC;
			
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGAFTERDATE. There are NULL parameters.';
					
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGAFTERDATE',
													  ax_recorddata    	=> 'pd_changedatetime IS NULL.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
				
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
				
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGAFTERDATE. An error have ocurred.(WHEN OTHERS)';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGAFTERDATE',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
				 
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
				
	END GET_AUDITLOGAFTERDATE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_AUDITLOGBYUSER(pc_retcursor	   OUT retcursor,
								 ps_changedby	IN VARCHAR2) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
	
		IF ps_changedby IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		OPEN pc_retcursor FOR
			SELECT CHANGELOGID,
				   CHANGEDATETIME,
				   CHANGEDBY,
				   AREA,
				   CHANGEDFILED_ELEMENT,
				   OLDVALUE,
				   NEWVALUE,
				   APPROVALSTATUS,
				   APPROVER
			FROM CERTIFICATION_AUDIT_LOG
			WHERE CHANGEDBY = ps_changedby
			ORDER BY CHANGEDATETIME DESC;
	
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGBYUSER. There are NULL parameters.';
					
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGBYUSER',
													  ax_recorddata    	=> 'ps_changedby IS NULL.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
			
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGBYUSER. An error have ocurred.(WHEN OTHERS)';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGBYUSER',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
	END GET_AUDITLOGBYUSER;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_AUDITLOGBYFIELDCHANGED(pc_retcursor				   OUT retcursor,
										 ps_changedfiled_element	IN VARCHAR2) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
	
		IF ps_changedfiled_element IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		OPEN pc_retcursor FOR
			SELECT CHANGELOGID,
				   CHANGEDATETIME,
				   CHANGEDBY,
				   AREA,
				   CHANGEDFILED_ELEMENT,
				   OLDVALUE,
				   NEWVALUE,
				   APPROVALSTATUS,
				   APPROVER
		FROM  CERTIFICATION_AUDIT_LOG
		WHERE CHANGEDFILED_ELEMENT = ps_changedfiled_element
		ORDER BY CHANGEDATETIME DESC;
	
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGBYFIELDCHANGED. There are NULL parameters.';
					
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGBYFIELDCHANGED',
													  ax_recorddata    	=> 'ps_changedby IS NULL.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
			
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGBYFIELDCHANGED. An error have ocurred.(WHEN OTHERS)';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGBYFIELDCHANGED',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
	END GET_AUDITLOGBYFIELDCHANGED;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_AUDITLOGBYAREA(pc_retcursor	   OUT retcursor,
								 ps_area		IN VARCHAR2) 
	AS
	
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 	:= NULL;
	
	BEGIN
	
		IF ps_area IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		OPEN pc_retcursor FOR
			SELECT CHANGELOGID,
				   CHANGEDATETIME,
				   CHANGEDBY,
				   AREA,
				   CHANGEDFILED_ELEMENT,
				   OLDVALUE,
				   NEWVALUE,
				   APPROVALSTATUS,
				   APPROVER
		FROM  CERTIFICATION_AUDIT_LOG
		WHERE AREA = ps_area
		ORDER BY CHANGEDATETIME DESC;
	
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGBYAREA. There are NULL parameters.';
					
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGBYAREA',
													  ax_recorddata    	=> 'ps_changedby IS NULL.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
													  
			RAISE_APPLICATION_ERROR (-20005,ls_errormsg);
				  
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GET_AUDITLOGBYAREA. An error have ocurred.(WHEN OTHERS)';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_AUDITLOGBYAREA',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
														  
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
				  
	END GET_AUDITLOGBYAREA;

	PROCEDURE GET_CERTIFICATIONBYBRANDCODE(pc_retcursor    OUT retcursor,
										   ps_brandcode	IN VARCHAR2)
	AS
	
	--NO LONGER USED JESEITZ 6/20/16
	le_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT( le_parametersnull,-20005);
	
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid 	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
	BEGIN
	
		IF ps_Brandcode IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		OPEN pc_retcursor FOR
			SELECT B.BRAND_CODE BRANDCODE,
				   P.SKU,
				   P.SIZESTAMP,
				   CE.CERTIFICATIONTYPEID AS CERTIFICATIONID,
				   CE.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME
			FROM BRAND_DETAILS_MV B 
				INNER JOIN PRODUCT P ON B.BRAND_CODE = P.BRANDCODE
				INNER JOIN PRODUCTCOUNTRY PC ON P.SKUID = PC.SKUID
				INNER JOIN COUNTRY CO ON PC.COUNTRYID = CO.COUNTRYID
				INNER JOIN CERTIFICATIONTYPE CE ON CO.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE B.BRAND_CODE = ps_Brandcode
			ORDER BY P.SKU,CE.CERTIFICATIONTYPENAME;
	
	EXCEPTION
		WHEN le_parametersnull THEN
			ls_errormsg := SQLERRM || ' - GET_CERTIFICATIONBYBRANDCODE. There are NULL parameters.';
					
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_CERTIFICATIONBYBRANDCODE',
													  ax_recorddata    	=> 'ps_changedby IS NULL.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
				  
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
			
		WHEN OTHERS THEN
			ls_errormsg := SQLERRM || ' - GET_CERTIFICATIONBYBRANDCODE. An error have ocurred.(WHEN OTHERS)';
				
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded 	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_CERTIFICATIONBYBRANDCODE',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
				  
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
		
	END GET_CERTIFICATIONBYBRANDCODE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_CERTIFICATIONSEARCHRESULT(pc_retcursor 		   OUT retcursor,
											ps_searchcriteria	IN VARCHAR2,
											ps_searchtype 		IN VARCHAR2,
											ps_extensionno 		IN VARCHAR2,
											ps_imarkfamily 		IN VARCHAR2,
											ps_brandline   		IN VARCHAR2) 
	AS
	/******************************************************************************
	NAME:       GET_CERTIFICATIONSEARCHRESULT
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/03/2012    Harini         1. Added ps_brandline as input paramter.
										    2. Replace BrandCode with Brand,BrandLine,
										       Added Matl_Num,SpeedRating,SingLoadIndex,
										       DualLoadIndex in the select list.
										    3. Commneted BrandDetails_MV
										    4. Use Brand instead of Brand code,Material No.
										       instead of SKU No.PSN  instead of NPR ID No.
										       in search types
	1.2        11/22/2013  Guru Gangadhar   1. Changed logic so that IF extension entered to look for IS *,
										       it returns all skus on all extensions; IF an extension IS entered,
										       only gets what was added on that extension. IF `H' IS entered,
										       only brings back skus on "highest" extension.
	1.3        01/10/2014   Harini          1. Added CERTIFICATENUMBER,EXTENSION_EN in the select list else clause of
											   Extension of Certifiation No search
	 4.       04.15.2014  jeseitz  in search by Imark in get_certificationsearchresult changed to not use customer function since not applicable for IMARKS
			   06/16/2016 jeseitz  changed to use productrequest instead of productcountry
	******************************************************************************/
	--EXCEPTION variables
	le_parametersnull EXCEPTION;
	li_parametersinvalid EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	PRAGMA EXCEPTION_INIT(li_parametersinvalid, -20006);
	
	--varible
	ls_machineid 	VARCHAR2(50)  	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
	BEGIN
		
		IF ps_searchcriteria IS NULL OR ps_searchtype IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		IF ps_searchcriteria = '' OR ps_searchtype = '' THEN
			RAISE li_parametersinvalid;
		END IF;
		
		-- Added as per PRJ3617
		IF ((ps_searchtype = 'Brand') AND (ps_searchcriteria IS NULL OR ps_brandline IS NULL)) THEN
			RAISE le_parametersnull;
		END IF;
	
		IF ps_searchtype = 'Brand' THEN  -- As per PRJ3617,Use Brand instead of Brand code.
			
			BEGIN
				OPEN pc_retcursor FOR
					SELECT DISTINCT P.BRAND,
									P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
									P.SKU,
									LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
									P.SPEEDRATING,
									P.SINGLOADINDEX,
									P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
									P.SIZESTAMP,
									C.CERTIFICATIONTYPEID AS CERTIFICATIONID,
									CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
									C.CERTIFICATENUMBER,
									P.SKUID,
									(CASE WHEN PR.REQUESTSTATUS = 'I' THEN 'InProgress'
										  WHEN PR.REQUESTSTATUS = 'A' THEN 'Approved'
										  WHEN PR.REQUESTSTATUS = 'R' THEN 'Requested'
										  WHEN PR.REQUESTSTATUS IS NULL AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
										  ELSE 'Uncertified'
									END) AS STATE,
									NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(C.CUSTOMERID), 'N/A') AS CUSTOMER
					FROM PRODUCT P			
						INNER JOIN PRODUCTCERTIFICATE PC ON P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID		   
						LEFT JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID  
							AND C.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
					WHERE (UPPER(P.BRAND) = UPPER(ps_searchcriteria) 
						AND UPPER(P.BRAND_LINE) = UPPER(ps_brandline)) -- Added this as per  UPPER(b.brand_code) = UPPER(ps_searchcriteria)
						AND C.ARCHIVEDATE_CEGI IS NULL
					ORDER BY LPAD(P.MATL_NUM, 18, 0), -- As per PRJ3617,Use matl_num instead of SKU
								  P.SKUID, 
								  CT.CERTIFICATIONTYPENAME, 
								  C.CERTIFICATENUMBER; --JES ADDED SKU, SKUID
			END;
		
		ELSIF ps_searchtype = 'Material No.' THEN  -- As per PRJ3617,Use Material No. instead of SKU No.
			
			BEGIN		
				OPEN pc_retcursor FOR
					SELECT DISTINCT P.BRAND,
									P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
									P.SKU,
									LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
									P.SPEEDRATING,
									P.SINGLOADINDEX,
									P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
									P.SIZESTAMP,
									C.CERTIFICATIONTYPEID AS CERTIFICATIONID,
									CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
									C.CERTIFICATENUMBER,
									P.SKUID,
									(CASE WHEN PR.REQUESTSTATUS = 'I' THEN 'InProgress'
										  WHEN PR.REQUESTSTATUS = 'A' THEN 'Approved'
										  WHEN PR.REQUESTSTATUS = 'R' THEN 'Requested'
										  WHEN PR.REQUESTSTATUS IS NULL AND PR.SKUID <> 0 THEN 'Requested' --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
										  ELSE 'Uncertified'
									END) AS STATE,
									NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(C.CUSTOMERID), 'N/A') AS CUSTOMER
					FROM PRODUCT P			
						INNER JOIN PRODUCTCERTIFICATE PC ON P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						LEFT JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID  
							AND C.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
					WHERE (UPPER(P.MATL_NUM) = UPPER(LPAD(ps_searchcriteria, 18, 0)) -- As per PRJ3617, Matl_Num should be added in Where Condition in place of SKU.
						OR UPPER(P.SKU) = UPPER(TRIM(ps_searchcriteria)))  -- JESEITZ 12/7/12
						AND C.ARCHIVEDATE_CEGI IS NULL
					ORDER BY P.SKUID,
							 CT.CERTIFICATIONTYPENAME, 
							 C.CERTIFICATENUMBER; --- jes added skuid
			END;
		
		ELSIF ps_searchtype = 'PSN' THEN   -- As per PRJ3617,Use PSN  instead of NPR ID No..
	  
			BEGIN
				OPEN pc_retcursor FOR
					SELECT DISTINCT	P.BRAND,
									P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
									P.SKU,
									LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
									P.SPEEDRATING,
									P.SINGLOADINDEX,
									P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
									P.SIZESTAMP,
									CT.CERTIFICATIONTYPEID     AS CERTIFICATIONID,
									CT.CERTIFICATIONTYPENAME   AS CERTIFICATIONNAME,
									CE.CERTIFICATENUMBER,
									P.SKUID,
									(CASE WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'I' THEN 'InProgress'
										  WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'A' THEN 'Approved'
										  WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'R' THEN 'Requested'
										  WHEN PR.REQUESTSTATUS IS NULL 	AND PR.SKUID <> 0 THEN 'Requested' --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
									ELSE 'Uncertified'
									END) AS STATE,
									NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(CE.CUSTOMERID), 'N/A') AS CUSTOMER
					FROM CERTIFICATE CE
						INNER JOIN PRODUCTCERTIFICATE PCE ON PCE.CERTIFICATEID = CE.CERTIFICATEID 
							AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
						INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
						INNER JOIN CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						INNER JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID 
							AND CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = CE.IMPORTERID
					WHERE P.PSN = ps_searchcriteria  -- AS per prj3617,removed to_number() for ps_searchcriteria
						AND CE.ARCHIVEDATE_CEGI IS NULL
					ORDER BY LPAD(P.MATL_NUM, 18, 0), -- As per PRJ3617,sku IS replaced with matl_num
								  P.SKUID, 
								  CT.CERTIFICATIONTYPENAME,
								  CE.CERTIFICATENUMBER; --JES ADDED P.SKUID
			END;
	
		ELSIF ps_searchtype = 'Certification No.' THEN
	
			BEGIN
				IF TRIM(ps_extensionno) = '*' THEN
					OPEN pc_retcursor FOR
						SELECT DISTINCT CE.CERTIFICATEID,
										CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
										CE.CERTIFICATENUMBER,
										CE.EXTENSION_EN AS EXTENSION,
										P.SKUID,
										P.BRAND,
										P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
										P.SKU,
										LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
										P.SPEEDRATING,
										P.SINGLOADINDEX,
										P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
										p.SIZESTAMP,
										CT.CERTIFICATIONTYPEID AS CERTIFICATIONID,
										(CASE WHEN PR.REQUESTSTATUS = 'I' THEN 'InProgress'
											  WHEN PR.REQUESTSTATUS = 'A' THEN 'Approved'
											  WHEN PR.REQUESTSTATUS = 'R' THEN 'Requested'
											  WHEN PR.REQUESTSTATUS IS NULL AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
											  ELSE 'Uncertified'
										END) AS STATE,
										NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(CE.CUSTOMERID), 'N/A') AS CUSTOMER
						FROM CERTIFICATE CE
							INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID 
								AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
							INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID					   
							LEFT JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID  
								AND CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
							INNER JOIN CERTIFICATIONTYPE CT ON
							 CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
							LEFT JOIN IMPORTER I ON I.IMPORTERID = CE.IMPORTERID
						WHERE (LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_searchcriteria)  
							OR LOWER(CE.CERTIFICATENUMBER) = REPLACE(LOWER(TRIM(ps_searchcriteria)), '-', ' ') 
							OR LOWER(CE.CERTIFICATENUMBER) = REPLACE(LOWER(TRIM(ps_searchcriteria)), ' ', '-')) 
							AND CE.ARCHIVEDATE_CEGI IS NULL
						ORDER BY LPAD(P.MATL_NUM, 18, 0), -- As per PRJ3617,replaced SKU with Matl_Num
									  P.SKUID, 
									  CT.CERTIFICATIONTYPENAME;-- JES ADDED P.SKU,P.SKUID
								 --jeseitz 1/6/2016
				ELSIF TRIM(ps_extensionno) = 'H' THEN
					OPEN pc_retcursor FOR
						SELECT DISTINCT CE.CERTIFICATEID,
										CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
										CE.CERTIFICATENUMBER,
										CE.EXTENSION_EN AS EXTENSION,
										P.SKUID,
										P.BRAND,
										P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
										P.SKU,
										LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
										P.SPEEDRATING,
										P.SINGLOADINDEX,
										P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
										P.SIZESTAMP,
										CT.CERTIFICATIONTYPEID AS CERTIFICATIONID,
										(CASE WHEN PR.REQUESTSTATUS = 'I' THEN 'InProgress'
											  WHEN PR.REQUESTSTATUS = 'A' THEN 'Approved'
											  WHEN PR.REQUESTSTATUS = 'R' THEN 'Requested'
											  WHEN PR.REQUESTSTATUS IS NULL AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
											  ELSE 'Uncertified'
										END) AS STATE,
										NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(CE.CUSTOMERID), 'N/A') AS CUSTOMER
						FROM CERTIFICATE ce
							INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID 
								AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
							INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
							LEFT JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID  
								AND CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
							INNER JOIN CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID					  
							LEFT JOIN IMPORTER I ON I.IMPORTERID = CE.IMPORTERID
						WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_searchcriteria) 
							AND UPPER(CE.MOSTRECENTCERT) = 'Y' 
							AND CE.ARCHIVEDATE_CEGI IS NULL
						ORDER BY LPAD(P.MATL_NUM, 18, 0), -- As per PRJ3617,replaced SKU with Matl_Num
									  P.SKUID, 
									  CT.CERTIFICATIONTYPENAME;-- JES ADDED P.SKU,P.SKUID
				ELSE
					OPEN pc_retcursor FOR
						SELECT DISTINCT CE.CERTIFICATEID,
										CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
										CE.CERTIFICATENUMBER,
										CE.EXTENSION_EN AS EXTENSION,
										P.SKUID,
										P.BRAND,
										P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
										P.SKU,
										LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
										P.SPEEDRATING,
										P.SINGLOADINDEX,
										P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
										p.SIZESTAMP,
										CT.CERTIFICATIONTYPEID AS CERTIFICATIONID,
										(CASE WHEN PR.REQUESTSTATUS = 'I' THEN 'InProgress'
											  WHEN PR.REQUESTSTATUS = 'A' THEN 'Approved'
											  WHEN PR.REQUESTSTATUS = 'R' THEN 'Requested'
											  WHEN PR.REQUESTSTATUS IS NULL AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
											  ELSE 'Uncertified'
										END) AS STATE,
										NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(CE.CUSTOMERID), 'N/A') AS CUSTOMER
						FROM CERTIFICATE CE
							INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID 
								AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
							INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
							LEFT JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID  
								AND CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
							INNER JOIN CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
							LEFT JOIN IMPORTER I ON I.IMPORTERID = CE.IMPORTERID
						WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_searchcriteria)  
							AND LOWER(CE.EXTENSION_EN) = LOWER(ps_extensionno) 
							AND CE.ARCHIVEDATE_CEGI IS NULL;
				END IF;
			END;
		
		ELSIF ps_searchtype = 'Batch No.' THEN
	 
			BEGIN
				OPEN pc_retcursor FOR
					SELECT DISTINCT	P.BRAND,
									P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
									P.SKU,
									LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
									P.SPEEDRATING,
									P.SINGLOADINDEX,
									P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
									P.SIZESTAMP,
									CT.CERTIFICATIONTYPEID AS CERTIFICATIONID,
									CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
									CE.CERTIFICATENUMBER,
									P.SKUID,
									(CASE WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'I' THEN 'InProgress'
										  WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'A' THEN 'Approved'
										  WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'R' THEN 'Requested'
										  WHEN PR.REQUESTSTATUS IS NULL  	AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
										  ELSE 'Uncertified'
									END) AS STATE,
									NVL(ICS_COMMON_FUNCTIONS.GETCUSTOMERBYID(CE.CUSTOMERID), 'N/A') AS CUSTOMER
					FROM CERTIFICATE CE
						INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID 
							AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
						INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
						INNER JOIN CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						LEFT JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID 
							AND CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = CE.IMPORTERID
					WHERE LOWER(CE.BATCHNUMBER_G) = LOWER(ps_searchcriteria) 
						AND CE.ARCHIVEDATE_CEGI IS NULL
					ORDER BY LPAD(P.MATL_NUM, 18, 0),  --As per PRJ3617,replaced SKU with Matl_Num
								  CT.CERTIFICATIONTYPENAME,
								  CE.CERTIFICATENUMBER;
			END;
		
		ELSIF ps_searchtype = 'Imark' THEN
			
			BEGIN
				OPEN pc_retcursor for
					SELECT DISTINCT CE.CERTIFICATEID,
									P.BRAND,
									P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
									P.SKU,
									LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
									P.SPEEDRATING,
									P.SINGLOADINDEX,
									P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
									P.SIZESTAMP,
									CT.CERTIFICATIONTYPEID AS CERTIFICATIONID,
									CT.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
									P.SKUID,
									CASE WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'I' THEN 'InProgress'
										 WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'A' THEN 'Approved'
										 WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'R' THEN 'Requested'
										 WHEN PR.REQUESTSTATUS IS NULL     AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
										 ELSE 'Uncertified'
									END AS STATE,
									'N/A' as CUSTOMER
					FROM CERTIFICATE CE
						INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID 
							AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
						INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
						INNER JOIN CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						INNER JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.SKUID = P.SKUID
							AND PIF.CERTIFICATEID = CE.CERTIFICATEID
						LEFT JOIN  PRODUCTREQUEST PR ON P.SKUID = PR.SKUID 
							AND CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
					WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_searchcriteria) 
						AND CE.ARCHIVEDATE_CEGI IS NULL 
						AND UPPER(PIF.FAMILYID) = UPPER(ps_imarkFamily)
					ORDER BY LPAD(P.MATL_NUM, 18, 0);

			END;

		ELSIF ps_searchtype = 'SimTire' THEN
		
			BEGIN
				OPEN pc_retcursor FOR
					SELECT * FROM 
						(SELECT DISTINCT P.BRAND,
										 P.BRAND_LINE,   -- As per PRJ3617,Use Brand and Brand Line as inputs  instead of Brand code.
										 P.SKU,
										 LPAD(P.MATL_NUM, 18, 0) MATL_NUM,
										 P.SPEEDRATING,
										 P.SINGLOADINDEX,
										 P.DUALLOADINDEX, -- As per PRJ3617,Matl_Num,speedrating,SingLoadIndex,dualloadindex should be added for all Search Types in Select Stmt.
										 P.SIZESTAMP,
										 PCE.CERTIFICATIONTYPEID AS CERTIFICATIONID,
										 CE.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
										 P.SKUID,
										 (CASE WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'I' THEN 'InProgress'
											   WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'A' THEN 'Approved'
											   WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'R' THEN 'Requested'
											   WHEN PR.REQUESTSTATUS IS NULL 	 AND PR.SKUID <> 0 THEN 'Requested'  --jeseitz 03/23/2015 to distinguish between record with NULL status and left outer join
											   ELSE 'Uncertified'
										 END) AS STATE
						FROM PRODUCT P
							INNER JOIN PRODUCTREQUEST PR ON P.SKUID = PR.SKUID
							LEFT JOIN CERTIFICATIONTYPE CE ON PR.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
							LEFT JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
						WHERE (P.MATL_NUM = LPAD(ps_searchcriteria, 18, 0)  -- As per PRJ3617, Matl_Num should be added in Where Condition in place of SKU.
							OR UPPER(P.SKU) = UPPER(TRIM(ps_searchcriteria)))  -- JESEITZ 12/7/12
						ORDER BY LPAD(P.MATL_NUM, 18, 0), -- As per PRJ3617,replaced SKU with Matl_Num
									 CE.CERTIFICATIONTYPENAME)
					WHERE CERTIFICATIONNAME IS NOT NULL;
			END;
			
		ELSE
		
			RAISE li_parametersinvalid;
			pc_retcursor:=NULL;
		
		END IF;
	
	EXCEPTION
		
		WHEN le_parametersnull THEN
			ls_errormsg:= SQLERRM || ' - GET_CERTIFICATIONSEARCHRESULT. There are NULL parameters.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT',
													  ax_recorddata    	=> 'ps_searchcriteria IS NULL OR ps_searchtype IS NULL.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			  
			RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
			  
		WHEN li_parametersinvalid THEN
			ls_errormsg:= SQLERRM || ' - GET_CERTIFICATIONSEARCHRESULT. There are invalid parameters.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT',
													  ax_recorddata    	=> 'ps_searchcriteria IS invalid OR ps_searchtype IS invalid.',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			  
			RAISE_APPLICATION_ERROR (-20006, ls_errormsg);
			
		WHEN OTHERS THEN
			ls_errormsg:= SQLERRM || ' - GET_CERTIFICATIONSEARCHRESULT. An error have ocurred.(WHEN OTHERS)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname 	=> '  CERTIFICATION_CRUD.GET_CERTIFICATIONSEARCHRESULT',
													  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
													  as_messagecode 	=> TO_CHAR(SQLCODE),
													  as_message       	=> SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
	END GET_CERTIFICATIONSEARCHRESULT;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICATE_UPDATE_BATCH(ps_certificationtypename IN VARCHAR2, 
									   ps_temp_batchnumber_g	IN VARCHAR2, 
									   ps_batchnumber_g			IN VARCHAR2, 
									   ps_username				IN VARCHAR2) 
	AS
	
	--EXCEPTION variables
	le_parametersnull EXCEPTION;
	li_parametersinvalid EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	PRAGMA EXCEPTION_INIT(li_parametersinvalid, -20006);
	
	-- Variables
	ls_machineid 			VARCHAR2(50) 	:= NULL;
	ls_operatorid 			VARCHAR2(50) 	:= 'ICSDEV';
	li_certificationtypeid 	INTEGER 		:= NULL;
	ls_errormsg 			VARCHAR2(4000)	:= NULL;
	ls_batchnumber_g 		VARCHAR2(30) 	:= NULL;
	li_certificateid 		NUMBER 			:= NULL;
	
	-- Cursor to get the all the certificateids in a batch
	CURSOR c_batchcertids(ls_batchnumber_g VARCHAR2) IS
		SELECT C.CERTIFICATEID
		FROM CERTIFICATE C
		WHERE C.BATCHNUMBER_G = ls_batchnumber_g 
			AND CERTIFICATIONTYPEID = 2;
	  
	BEGIN
	
		IF ps_certificationtypename IS NULL OR ps_temp_batchnumber_g IS NULL OR ps_batchnumber_g IS NULL THEN
			RAISE le_parametersnull;
		END IF;
		
		IF ps_certificationtypename =  '' OR ps_temp_batchnumber_g = '' OR ps_batchnumber_g = '' THEN
			RAISE li_parametersinvalid;
		END IF;
		
		IF ps_username IS NULL OR ps_username = '' THEN
			ls_operatorid:= ps_username;
		END IF;
				
		--Gets the certification id based on the certification name
		SELECT CERTIFICATIONTYPEID INTO li_certificationtypeid
			FROM CERTIFICATIONTYPE ce
		WHERE LOWER(CE.CERTIFICATIONTYPENAME) = LOWER(ps_certificationtypename);
		
		UPDATE CERTIFICATE
		SET BATCHNUMBER_G = ps_batchnumber_g,
			CERTDATESUBMITTED = TRUNC(SYSDATE),
			MODIFIEDON = SYSDATE,
			MODIFIEDBY = ps_username
		WHERE CERTIFICATIONTYPEID = LI_CERTIFICATIONTYPEID AND BATCHNUMBER_G = ps_temp_batchnumber_g;
		
		COMMIT;
		
		ls_batchnumber_g := ps_batchnumber_g;
		
		OPEN c_batchcertids(ls_batchnumber_g);
			LOOP
				FETCH c_batchcertids INTO li_certificateid;
				EXIT WHEN c_batchcertids%NOTFOUND;
				UPDATE PRODUCTCERTIFICATE
					SET DATESUBMITTED = TRUNC(SYSDATE)
				WHERE CERTIFICATEID = li_certificateid;
			END LOOP;
		CLOSE c_batchcertids;
		
		COMMIT;
		
	EXCEPTION
	
		WHEN le_parametersnull THEN
		ls_errormsg := SQLERRM || '- CERTIFICATE_SAVE. There IS at least one parameters NULL.';
					   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATE_SAVE',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
		
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		
		WHEN li_parametersinvalid THEN
		ls_errormsg := SQLERRM || '- CERTIFICATE_SAVE. There IS at least one parameters IS invalid.';
							  
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded	=> SYSDATE,
												   as_processname 	=> '  CERTIFICATION_CRUD.CERTIFICATE_SAVE',
												   ax_recorddata    => 'ps_searchcriteria IS invalid OR ps_searchtype IS invalid.',
												   as_messagecode 	=> TO_CHAR(SQLCODE),
												   as_message       => SQLERRM);
		
		RAISE_APPLICATION_ERROR (-20006, ls_errormsg);
		
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- CERTIFICATE_SAVE. An error have ocurred.(WHEN OTHERS)';
						 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATE_SAVE',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
		
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
		
	END CERTIFICATE_UPDATE_BATCH;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICATE_SAVE(pi_retid 						   OUT NUMBER,
							   pi_certificateid 				IN NUMBER,
							   pi_skuid 						IN NUMBER,
							   ps_matl_num 						IN VARCHAR2,
							   ps_remove_matl_num 				IN VARCHAR2,
							   ps_certificationtypename 		IN VARCHAR2,
							   ps_certificatenumber 			IN VARCHAR2,
							   pd_certdatesubmitted 			IN DATE,
							   pd_certdateapproved_cegi 		IN DATE,
							   pd_datesubmited 					IN DATE,
							   pc_activestatus 					IN VARCHAR2,
							   pd_dateassigned_egi 				IN DATE,
							   pd_dateapproved_cegi 			IN DATE,
							   pc_renewalrequired_cgin  		IN VARCHAR2,
							   ps_jobreportnumber_cen 			IN VARCHAR2,
							   ps_extension_en 					IN VARCHAR2,
							   ps_supplementalmoldstamping_e	IN VARCHAR2,
							   ps_emarkreference_i 				IN VARCHAR2,
							   pd_expirydate_i 					IN DATE,
							   ps_family_i 						IN VARCHAR2,
							   ps_family_desc 					IN VARCHAR2,
							   ps_productlocation 				IN VARCHAR2,
							   ps_countryofmanufacture_n 		IN VARCHAR2,
							   ps_addnewcustomer 				IN VARCHAR2,
							   ps_actsigreq 					IN VARCHAR2,
							   pi_customerid 					IN NUMBER,
							   ps_customer_n 					IN VARCHAR2,
							   ps_customeraddress_n 			IN VARCHAR2,
							   ps_customerspecific_n 			IN VARCHAR2,
							   ps_addnewimporter 				IN VARCHAR2,
							   pi_importerid 					IN NUMBER,
							   ps_importer_n 					IN VARCHAR2,
							   ps_importeraddress_n 			IN VARCHAR2,
							   ps_importerrepresentative_n 		IN VARCHAR2,
							   ps_countrylocation_n 			IN VARCHAR2,
							   ps_batchnumber_g  				IN VARCHAR2,
							   ps_companyname 					IN VARCHAR2,
							   ps_username 						IN VARCHAR2,
							   ps_mold_changed 					IN VARCHAR2,                 -- JBH_2.00
							   pd_oper_date_approved 			IN DATE,                     -- JBH_2.00
							   ps_additional_info 				IN VARCHAR2                  -- JESEITZ 10/30/2016
							   ) 
	AS
	/******************************************************************************
	NAME:       CERTIFICATE_SAVE
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/03/2012    Harini         1.Replaced ps_SKU with ps_matl_num.
	1.2        10/24/2013    Harini         1. Added ps_family_desc as new parameter
										  and updated the logic whiile updating the product table
	1.3        11/18/2013   Guru Gangadhar  1.GSO IS only certificate type that will allow the certificate
											to be renamed. Other certificate types can be renamed through
											the maintenance functions.
	1.4        01/10/2014   Harini          1. Comment the call to ProductCountry_Save and replace that with
											delete the record from Productcountry and call ProductCertification_Save
	1.5        01/22/2014   Harini          1.Use pi_certificateid instead of li_certificateid while updating the
											ProductCertificate table and while assigning the RetId field as
											li_CertificateId IS NOT filled.
	2.00       09/10/2014   Joe Hill        Added update of Mold Changed Flag and Operations Approval Date
											fields in the routines that update the PRODUCTCERTIFICATE table.
	******************************************************************************/
	--EXCEPTION variables
	le_parametersnull EXCEPTION;
	li_parametersinvalid EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(le_parametersnull, -20005);
	PRAGMA EXCEPTION_INIT(li_parametersinvalid, -20006);
	
	--varible
	ls_machineid 			VARCHAR2(50) 	:= NULL;
	ls_operatorid 			VARCHAR2(50)	:='ICSDEV';
	ls_errormsg 			VARCHAR2(4000)	:= NULL;
	li_certificationtypeid	INTEGER 		:= NULL;
	li_totalskufound 		INTEGER 		:= NULL;
	lc_Requeststatus 		CHAR(1) 		:= NULL;
	li_certificateid 		NUMBER 			:= NULL;
	li_newcertificateid 	NUMBER 			:= NULL;
	ls_extensionnumber 		VARCHAR2(30) 	:= NULL;
	ld_dateremoved 			DATE 			:= NULL;
	li_certificatefound 	NUMBER 			:= NULL;
	li_existingcert 		NUMBER 			:= NULL;
	ls_existingcertnumber	VARCHAR2(30) 	:= NULL;
	li_skufound 			NUMBER 			:= NULL;
	li_oldskuid 			PRODUCT.SKUID%TYPE := NULL;
	ld_dateapproved 		PRODUCTCERTIFICATE.DATEAPPROVED_CEGI%TYPE 	:= NULL;
	ld_dateassigned 		PRODUCTCERTIFICATE.DATEASSIGNED_EGI%TYPE	:= NULL;
	ld_datesubmitted		PRODUCTCERTIFICATE.DATESUBMITTED%TYPE 		:= NULL;
	li_importerid 			NUMBER 	:= NULL;
	li_customerid 			NUMBER 	:= NULL;
	li_skuid 				NUMBER 	:= NULL;
	ln_newcertcount 		NUMBER 	:= NULL;
	ln_newcertid 			NUMBER 	:= NULL;
	ln_errornum 			NUMBER 	:= NULL;

	CURSOR c_prodcert(ls_certificatenumber VARCHAR2)  
	IS
		SELECT PC.SKUID
		FROM   PRODUCTCERTIFICATE PC,
			   CERTIFICATE C
		WHERE C.CERTIFICATENUMBER = ls_certificatenumber 
			AND PC.CERTIFICATEID = C.CERTIFICATEID 
			AND PC.DATEREMOVED IS NULL;

	BEGIN

		IF ps_matl_num IS NULL OR ps_certificationtypename IS NULL THEN -- As per PRJ3617, replaced ps_SKU with Matl_Num
			RAISE le_parametersnull;
		END IF;

		IF ps_matl_num = '' OR ps_certificationtypename = '' THEN  -- As per PRJ3617, replaced ps_SKU with Matl_Num
			RAISE li_parametersinvalid;
		END IF;

		IF ps_username IS NOT NULL AND ps_username <> '' THEN
			ls_operatorid := ps_username;
		END IF;

		-- Gets the certification id based on the certification name
		SELECT CERTIFICATIONTYPEID
			INTO li_certificationtypeid
		FROM CERTIFICATIONTYPE CT
		WHERE LOWER(CT.CERTIFICATIONTYPENAME) = LOWER(ps_certificationtypename);

		IF li_certificationtypeid = 2 THEN
		--  We need to determine IF we are just updating the certificate NUMBER of an existing certificate
			SELECT COUNT(*)
				INTO li_existingcert
			FROM CERTIFICATE
			WHERE CERTIFICATEID = pi_certificateid;

			IF li_existingcert > 0 THEN
				SELECT CERTIFICATENUMBER
					INTO ls_existingcertnumber
				FROM CERTIFICATE
				WHERE CERTIFICATEID = pi_certificateid;

				IF  UPPER(ls_existingcertnumber) <> UPPER(ps_certificatenumber) THEN				
					--  We want to change the  certificate NUMBER
					--  jes 9/5/12 check to make sure we don't already have a certificate with the same NUMBER we passed in.
					SELECT COUNT(*)
						INTO ln_newcertcount
					FROM CERTIFICATE
					WHERE CERTIFICATENUMBER = ps_certificatenumber 
						AND CERTIFICATIONTYPEID = li_certificationtypeid; -- added certificationtype
				
					IF  ln_newcertcount = 0  THEN
						--- this certificate does not already exists -- ok to add - else will have to remove sku and add it.
						UPDATE CERTIFICATE
						SET CERTIFICATENUMBER = ps_certificatenumber
						WHERE CERTIFICATEID = pi_certificateid;

						COMMIT;
				  END IF;
				  
				END IF;
				
			END IF;
			
		END IF;

		IF li_certificationtypeid <> 3 THEN
			li_importerid := NULL;
			li_customerid := NULL;		
		ELSE
		--  We have a NOM certificate
		ICS_CRUD.MANAGE_IMPORTER(ps_addnewimporter,
								 pi_importerid,
								 ps_importer_n,
								 ps_importeraddress_n,
								 ps_importerrepresentative_n,
								 ps_countrylocation_n,
								 li_importerid);

		ICS_CRUD.MANAGE_CUSTOMER(ps_addnewcustomer,
								 pi_customerid,
								 ps_customer_n,
								 ps_customeraddress_n,
								 ps_actsigreq,
								 li_customerid);
		END IF;

		--  No change to extension so start by updating certificate
		UPDATE  CERTIFICATE
		SET ACTIVESTATUS                = LOWER(pc_activestatus),
			RENEWALREQUIRED_CGIN        = LOWER(pc_renewalrequired_cgin),
			JOBREPORTNUMBER_CEN         = ps_jobreportnumber_cen,
			EXTENSION_EN                = ps_extension_en,
			SUPPLEMENTALMOLDSTAMPING_E  = ps_supplementalmoldstamping_e,
			EXPIRYDATE_I                = pd_expirydate_i,
			PRODUCTLOCATION             = ps_productlocation,
			COUNTRYOFMANUFACTURE_N      = ps_countryofmanufacture_n,
			CUSTOMERID                  = li_customerid,
			CUSTOMERSPECIFIC_N          = LOWER(ps_customerspecific_n),
			IMPORTERID                  = li_importerid,
			BATCHNUMBER_G               = ps_batchnumber_g,
			MODIFIEDON                  = SYSDATE,
			MODIFIEDBY                  = ls_operatorid,
			certificatenumber           = CASE li_certificationtypeid
											WHEN 6 THEN ps_certificatenumber
											ELSE UPPER(ps_certificatenumber)
										  END,
			certdatesubmitted           = pd_certdatesubmitted,
			certdateapproved            = pd_certdateapproved_cegi,
			COMPANYNAME                 = ps_companyname
		WHERE CERTIFICATIONTYPEID = li_certificationtypeid 
			AND CERTIFICATEID     = pi_certificateid;

		--  ECE30/54 - IF Certificate has dateapproved THEN populate ECE-Reference #
		IF li_certificationtypeid = 1 
			AND pd_certdateapproved_cegi IS NOT NULL THEN

		--  OPEN cursor to get skus on previous extension
			OPEN c_prodcert(UPPER(ps_certificatenumber));

		--  Use the cursor to loop through SKUs
			LOOP
				FETCH c_prodcert
					INTO li_skuid;
				EXIT WHEN c_prodcert%NOTFOUND;

				UPDATE PRODUCT
				SET EMARKREFERENCE_I = UPPER(ps_certificatenumber)
				WHERE SKUID = li_skuid;

			END LOOP;

			CLOSE c_prodcert;

		END IF;

		--  Modified as per Defect 12385
		IF li_certificationtypeid = 1  
			AND pd_dateapproved_cegi IS NOT NULL THEN

			UPDATE PRODUCT
			SET EMARKREFERENCE_I = UPPER(ps_certificatenumber)
			WHERE SKUID = pi_skuid;

		END IF;

		IF  ps_remove_matl_num = 'y' THEN
			ld_dateremoved := TO_DATE(TO_CHAR(SYSDATE, 'mm/dd/yyyy'), 'mm/dd/yyyy');
		ELSE
			ld_dateremoved := NULL;
		END IF;

		UPDATE PRODUCTCERTIFICATE
		SET DATEASSIGNED_EGI    = COALESCE(pd_dateassigned_egi, dateassigned_egi),
			DATEAPPROVED_CEGI   = pd_dateapproved_cegi,
			DATESUBMITTED       = DECODE(li_certificationtypeid, 3, pd_certdatesubmitted, pd_datesubmited),
			DATEREMOVED         = ld_dateremoved,
			MODIFIEDON          = SYSDATE,
			MODIFIEDBY          = ls_operatorid,                              -- JBH_2.00
			MOLD_CHANGED        = ps_mold_changed,                            -- JBH_2.00
			OPER_DATE_APPROVED  = pd_oper_date_approved,                      -- JBH_2.00
			ADDITIONAL_INFO 	= ps_additional_info
		WHERE SKUID = pi_skuid 
			AND CERTIFICATIONTYPEID = li_certificationtypeid 
			AND CERTIFICATEID = pi_certificateid;

		--  IF Imark certificate THEN update Family and E-Mark Reference for current SKU
		IF li_certificationtypeid = 4 THEN

		UPDATE PRODUCT
		SET EMARKREFERENCE_I  = ps_emarkreference_i,
			MODIFIEDON        = SYSDATE,
			MODIFIEDBY        = ls_operatorid
		WHERE SKUID = pi_skuid;

		UPDATE PRODUCT_IMARK_FAMILY  -- jeseitz 4/4/16
		SET FAMILYID    = ps_family_i,
			IMARK       = ps_family_desc,
			MODIFIEDON  = SYSDATE,
			MODIFIEDBY	= ls_operatorid
		WHERE SKUID = pi_skuid
			AND CERTIFICATEID = pi_certificateid;

		END IF;

		DELETE
		FROM ICS.PRODUCTREQUEST
		WHERE CERTIFICATIONTYPEID = li_certificationtypeid
			AND SKUID = pi_skuid;

		COMMIT;

		ICS_CRUD.PRODUCTCERTIFICATION_SAVE (li_certificationtypeid,
											pi_skuid,
											ln_errornum);

		--  Return the new certificate id
		pi_retid := pi_certificateid;

		--  For Imark update SKU date submitted for the Imark Conformity Report
		IF li_certificationtypeid = 4 THEN
			ICS_CRUD.IMARKCONFORMITYUPDATE(pd_certdatesubmitted);
		END IF;

		COMMIT;

	EXCEPTION
	
		WHEN le_parametersnull THEN
		ls_errormsg := SQLERRM || '- CERTIFICATE_SAVE. There IS at least one parameters NULL.';
					   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATE_SAVE',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
		
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

		WHEN li_parametersinvalid THEN
		ls_errormsg := SQLERRM || '- CERTIFICATE_SAVE. There IS at least one parameters IS invalid.';
							  
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded 	=> SYSDATE,
												  as_processname 	=> '  CERTIFICATION_CRUD.CERTIFICATE_SAVE',
												  ax_recorddata    	=> 'ps_searchcriteria IS invalid OR ps_searchtype IS invalid.',
												  as_messagecode 	=> TO_CHAR(SQLCODE),
												  as_message       	=> SQLERRM);
					
		RAISE_APPLICATION_ERROR (-20006, ls_errormsg);

		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- CERTIFICATE_SAVE. An error have ocurred.(WHEN OTHERS)';
						 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  CERTIFICATION_CRUD.CERTIFICATE_SAVE',
												  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
					 
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);

	END CERTIFICATE_SAVE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETCERTIFICATESINFO(pc_retcursor 				   OUT retcursor,
								  ps_certificationnumber	IN VARCHAR2,
								  ps_extensionno 			IN VARCHAR2,
								  pi_certificationtypeid 	IN NUMBER,
								  pi_skuid 					IN NUMBER,
								  ps_trexists 				   OUT VARCHAR2) 
	AS
	/******************************************************************************
	NAME:       GETCERTIFICATESINFO
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/02/2012    Harini         1.Added Matl_Num in the select list
											wherever SKU IS found
	1.2        03/04/2013    Venkat         1.Added product discontinueddate and TPN
											 to the output cursor
	1.3        03/08/2013    Krishna         Replaced BrandDesc with Brand and Brandline
	1.4        10/22/2013    Harini         1. Retrieving p.Imark value from product table
											in all the queries.
	1.5        11/26/2013    Guru           1.Added else for ps_Extension =* to handle IF the
											input ps_Extension ='H' IS given
	  6.        04/15/2014  jeseitz  GETCERTIFICATESINFO - Imark part of IF clause was not getting by SKUID - need to be able to get different data for different
												speedratings of same material.
	2.00       09/10/2014   Joe Hill        Added Mold Changed Flag and Operations Approval Date
											fields from the PRODUCTCERTIFICATE table to the cursor
											returned to the source program
				10/29/2016  jeseitz  added Additional_Info fields from the PRODUCTCERTIFICATE table to the cursor returned to the source program
	******************************************************************************/
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
	
	--varible
	ls_machineid	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	ls_certificatenumber CERTIFICATE.CERTIFICATENUMBER%TYPE := NULL;
	ls_trexists 	VARCHAR2(1) 	:= NULL;
	li_totaltr 		NUMBER 			:= NULL;

	BEGIN
	
		IF pi_certificationtypeid IS NULL OR pi_skuid IS NULL THEN
			RAISE li_parametersarenull;
		END IF;

		IF pi_certificationtypeid = 4 THEN
			
			IF TRIM(ps_extensionno) = '*' THEN
				
				OPEN pc_retcursor FOR
					SELECT C.CERTIFICATEID,
						   C.CERTIFICATIONTYPEID,
						   PC.SKUID,
						   C.CERTIFICATENUMBER,
						   PC.DATESUBMITTED,
						   C.ACTIVESTATUS,
						   PC.DATEASSIGNED_EGI,
						   PC.DATEAPPROVED_CEGI,
						   C.RENEWALREQUIRED_CGIN,
						   C.JOBREPORTNUMBER_CEN,
						   C.EXTENSION_EN,
						   C.SUPPLEMENTALMOLDSTAMPING_E,
						   C.EXPIRYDATE_I,
						   C.PRODUCTLOCATION,
						   C.COUNTRYOFMANUFACTURE_N,
						   C.CUSTOMERID,
						   CU.SIGNATUREIND,
						   CU.CUSTOMER,
						   CU.CUSTOMERADDRESS,
						   C.CUSTOMERSPECIFIC_N,
						   C.IMPORTERID,
						   I.IMPORTER,
						   I.IMPORTERADDRESS,
						   I.IMPORTERREPRESENTATIVE,
						   I.COUNTRYLOCATION,
						   C.BATCHNUMBER_G,
						   CT.CERTIFICATIONTYPENAME,
						   PC.DATEREMOVED,
						   P.EMARKREFERENCE_I,
						   PIF.FAMILYID FAMILY,
						   P.SKU,
						   LPAD(P.MATL_NUM, 18, 0) MATL_NUM,  -- Added as per PRJ3617
						   C.CERTDATESUBMITTED,
						   C.CERTDATEAPPROVED,
						   C.COMPANYNAME,
						   P.SIZESTAMP,
						   P.SINGLOADINDEX,
						   P.DUALLOADINDEX,
						   P.SPEEDRATING,
						   P.BRAND||' '||P.BRAND_LINE AS BRANDDESC,
						   P.TUBELESSYN,
						   P.DISCONTINUEDDATE,
						   P.TPN,
						   PIF.IMARK AS FAMILYDESC,                                -- JBH_2.00
						   PC.MOLD_CHANGED,                                        -- JBH_2.00
						   PC.OPER_DATE_APPROVED,                                  -- JBH_2.00
						   PC.ADDITIONAL_INFO                                      --jeseitz 10/29/2016
					FROM PRODUCT P
						INNER JOIN PRODUCTCERTIFICATE PC ON P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						INNER JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.SKUID = P.SKUID
							AND PIF.CERTIFICATEID = C.CERTIFICATEID
						LEFT JOIN IMPORTER I ON  I.IMPORTERID = C.IMPORTERID
						LEFT JOIN CUSTOMER CU ON  CU.CUSTOMERID = C.CUSTOMERID
					WHERE C.CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
						AND PC.SKUID = pi_skuid;  -- jeseitz 4/15/2014 -- added skuid

			ELSIF TRIM(ps_extensionno) = 'H' THEN
				
				OPEN pc_retcursor FOR
					SELECT C.CERTIFICATEID,
						   C.CERTIFICATIONTYPEID,
						   PC.SKUID,
						   C.CERTIFICATENUMBER,
						   PC.DATESUBMITTED,
						   C.ACTIVESTATUS,
						   PC.DATEASSIGNED_EGI,
						   PC.DATEAPPROVED_CEGI,
						   C.RENEWALREQUIRED_CGIN,
						   C.JOBREPORTNUMBER_CEN,
						   C.EXTENSION_EN,
						   C.SUPPLEMENTALMOLDSTAMPING_E,
						   C.EXPIRYDATE_I,
						   C.PRODUCTLOCATION,
						   C.COUNTRYOFMANUFACTURE_N,
						   C.CUSTOMERID,
						   CU.SIGNATUREIND,
						   CU.CUSTOMER,
						   CU.CUSTOMERADDRESS,
						   C.CUSTOMERSPECIFIC_N,
						   C.IMPORTERID,
						   I.IMPORTER,
						   I.IMPORTERADDRESS,
						   I.IMPORTERREPRESENTATIVE,
						   I.COUNTRYLOCATION,
						   C.BATCHNUMBER_G,
						   CT.CERTIFICATIONTYPENAME,
						   PC.DATEREMOVED,
						   P.EMARKREFERENCE_I,
						   P.FAMILY,
						   P.SKU,
						   LPAD(P.MATL_NUM, 18, 0) MATL_NUM,  -- Added as per PRJ3617
						   C.CERTDATESUBMITTED,
						   C.CERTDATEAPPROVED,
						   C.COMPANYNAME,
						   P.SIZESTAMP,
						   P.SINGLOADINDEX,
						   P.DUALLOADINDEX,
						   P.SPEEDRATING,
						   P.BRAND||' '||P.BRAND_LINE AS BRANDDESC,
						   P.TUBELESSYN,
						   P.DISCONTINUEDDATE,
						   P.TPN,
						   P.IMARK AS FAMILYDESC,                                  -- JBH_2.00
						   PC.MOLD_CHANGED,                                        -- JBH_2.00
						   PC.OPER_DATE_APPROVED,                                  -- JBH_2.00
						   PC.ADDITIONAL_INFO                                   -- jeseitz 10/29/2016
					FROM PRODUCT P
						INNER JOIN PRODUCTCERTIFICATE PC ON P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON  PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON  C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = C.IMPORTERID
						LEFT JOIN CUSTOMER CU ON  CU.CUSTOMERID = C.CUSTOMERID
					WHERE C.CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
						AND LOWER(C.MOSTRECENTCERT) = 'y' 
						AND PC.SKUID = pi_skuid;-- jeseitz 4/15/2014 -- added skuid
			
			ELSE
				
				OPEN pc_retcursor FOR
					SELECT C.CERTIFICATEID,
						   C.CERTIFICATIONTYPEID,
						   PC.SKUID,
						   C.CERTIFICATENUMBER,
						   PC.DATESUBMITTED,
						   C.ACTIVESTATUS,
						   PC.DATEASSIGNED_EGI,
						   PC.DATEAPPROVED_CEGI,
						   C.RENEWALREQUIRED_CGIN,
						   C.JOBREPORTNUMBER_CEN,
						   C.EXTENSION_EN,
						   C.SUPPLEMENTALMOLDSTAMPING_E,
						   C.EXPIRYDATE_I,
						   C.PRODUCTLOCATION,
						   C.COUNTRYOFMANUFACTURE_N,
						   C.CUSTOMERID,
						   CU.SIGNATUREIND,
						   CU.CUSTOMER,
						   CU.CUSTOMERADDRESS,
						   C.CUSTOMERSPECIFIC_N,
						   C.IMPORTERID,
						   I.IMPORTER,
						   I.IMPORTERADDRESS,
						   I.IMPORTERREPRESENTATIVE,
						   I.COUNTRYLOCATION,
						   C.BATCHNUMBER_G,
						   CT.CERTIFICATIONTYPENAME,
						   PC.DATEREMOVED,
						   P.EMARKREFERENCE_I,
						   PIF.FAMILYID FAMILY,
						   P.SKU,
						   LPAD(P.MATL_NUM, 18, 0) MATL_NUM,  -- Added as per PRJ3617
						   C.CERTDATESUBMITTED,
						   C.CERTDATEAPPROVED,
						   C.COMPANYNAME,
						   P.SIZESTAMP,
						   P.SINGLOADINDEX,
						   P.DUALLOADINDEX,
						   P.SPEEDRATING,
						   P.BRAND||' '||P.BRAND_LINE AS BRANDDESC,
						   P.TUBELESSYN,
						   P.DISCONTINUEDDATE,
						   P.TPN,
						   PIF.IMARK AS FAMILYDESC,                                -- JBH_2.00
						   PC.MOLD_CHANGED,                                        -- JBH_2.00
						   PC.OPER_DATE_APPROVED,                                  -- JBH_2.00
						   PC.ADDITIONAL_INFO                                      --jeseitz 10/29/2016
					FROM PRODUCT P
						INNER JOIN PRODUCTCERTIFICATE PC ON P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						LEFT JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.SKUID = P.SKUID
							AND PIF.CERTIFICATEID = C.CERTIFICATEID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = C.IMPORTERID
						LEFT JOIN CUSTOMER CU ON CU.CUSTOMERID = C.CUSTOMERID
					WHERE C.CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
						AND LOWER(C.EXTENSION_EN) = LOWER(ps_extensionno) 
						AND PC.SKUID = pi_skuid; -- jeseitz 4/15/2014 -- added skuid
			END IF;
		
		ELSE
			
			IF TRIM(ps_extensionno) = '*' THEN
				
				OPEN pc_retcursor FOR
					SELECT P.SKUID,
						   C.*,
						   PC.DATEASSIGNED_EGI,
						   PC.DATESUBMITTED,
						   PC.DATEAPPROVED_CEGI,
						   PC.DATEREMOVED,
						   CT.CERTIFICATIONTYPENAME,
						   P.SKU,
						   LPAD(P.MATL_NUM, 18, 0) MATL_NUM, -- Added as per PRJ3617
						   C.CERTDATESUBMITTED,
						   C.CERTDATEAPPROVED,
						   I.IMPORTER,
						   I.IMPORTERADDRESS,
						   I.IMPORTERREPRESENTATIVE,
						   I.COUNTRYLOCATION,
						   CU.SIGNATUREIND,
						   CU.CUSTOMER,
						   CU.CUSTOMERADDRESS,
						   P.SIZESTAMP,
						   P.SINGLOADINDEX,
						   P.DUALLOADINDEX,
						   P.SPEEDRATING,
						   P.BRAND||' '||P.BRAND_LINE AS BRANDDESC,
						   P.TUBELESSYN,
						   P.DISCONTINUEDDATE,
						   P.TPN,
						   PIF.IMARK AS FAMILYDESC,                             -- JBH_2.00
						   PC.MOLD_CHANGED,                                     -- JBH_2.00
						   PC.OPER_DATE_APPROVED,                               -- JBH_2.00
						   PC.ADDITIONAL_INFO                                   -- jeseitz 10/29/2016
					FROM PRODUCT P
						INNER JOIN PRODUCTCERTIFICATE PC ON  P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						LEFT JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.CERTIFICATEID = C.CERTIFICATEID
							AND PIF.SKUID = PC.SKUID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = C.IMPORTERID
						LEFT JOIN CUSTOMER CU ON CU.CUSTOMERID = C.CUSTOMERID
					WHERE P.SKUID = pi_skuid 
						AND PC.CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) ;

			ELSIF TRIM(ps_extensionno) = 'H' THEN
				
				OPEN pc_retcursor FOR
					SELECT P.SKUID,
						   C.*,
						   PC.DATEASSIGNED_EGI,
						   PC.DATESUBMITTED,
						   PC.DATEAPPROVED_CEGI,
						   PC.DATEREMOVED,
						   CT.CERTIFICATIONTYPENAME,
						   P.SKU,
						   LPAD(P.MATL_NUM, 18, 0) MATL_NUM, -- Added as per PRJ3617
						   C.CERTDATESUBMITTED,
						   C.CERTDATEAPPROVED,
						   I.IMPORTER,
						   I.IMPORTERADDRESS,
						   I.IMPORTERREPRESENTATIVE,
						   I.COUNTRYLOCATION,
						   CU.SIGNATUREIND,
						   CU.CUSTOMER,
						   CU.CUSTOMERADDRESS,
						   P.SIZESTAMP,
						   P.SINGLOADINDEX,
						   P.DUALLOADINDEX,
						   P.SPEEDRATING,
						   P.BRAND||' '||P.BRAND_LINE AS BRANDDESC,
						   P.TUBELESSYN,
						   P.DISCONTINUEDDATE,
						   P.TPN,
						   PIF.IMARK AS FAMILYDESC,                              -- JBH_2.00
						   PC.MOLD_CHANGED,                                      -- JBH_2.00
						   PC.OPER_DATE_APPROVED,                                -- JBH_2.00
						   PC.ADDITIONAL_INFO                                    -- jeseitz 10/29/2016
					FROM PRODUCT P
						INNER JOIN PRODUCTCERTIFICATE PC ON P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						LEFT JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.CERTIFICATEID = C.CERTIFICATEID
							AND PIF.SKUID = PC.SKUID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = C.IMPORTERID
						LEFT JOIN CUSTOMER CU ON CU.CUSTOMERID = C.CUSTOMERID
					WHERE P.SKUID = pi_skuid 
						AND PC.CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
						AND LOWER(C.MOSTRECENTCERT) = 'y';
			
			ELSE
				
				OPEN pc_retcursor FOR
					SELECT P.SKUID,
						   C.*,
						   PC.DATEASSIGNED_EGI,
						   PC.DATESUBMITTED,
						   PC.DATEAPPROVED_CEGI,
						   PC.DATEREMOVED,
						   CT.CERTIFICATIONTYPENAME,
						   P.SKU,
						   LPAD(P.MATL_NUM, 18, 0) MATL_NUM,  -- Added as per PRJ3617
						   C.CERTDATESUBMITTED,
						   C.CERTDATEAPPROVED,
						   I.IMPORTER,
						   I.IMPORTERADDRESS,
						   I.IMPORTERREPRESENTATIVE,
						   I.COUNTRYLOCATION,
						   CU.SIGNATUREIND,
						   CU.CUSTOMER,
						   CU.CUSTOMERADDRESS,
						   P.SIZESTAMP,
						   P.SINGLOADINDEX,
						   P.DUALLOADINDEX,
						   P.SPEEDRATING,
						   P.BRAND||' '||P.BRAND_LINE AS BRANDDESC,
						   P.TUBELESSYN,
						   P.DISCONTINUEDDATE,
						   P.TPN,
						   PIF.IMARK AS FAMILYDESC,                                  -- JBH_2.00
						   PC.MOLD_CHANGED,                                        -- JBH_2.00
						   PC.OPER_DATE_APPROVED,                                   -- JBH_2.00
						   PC.ADDITIONAL_INFO                                           -- jeseitz 10/29/2016
					FROM PRODUCT P
						INNER JOIN PRODUCTCERTIFICATE PC ON  P.SKUID = PC.SKUID
						INNER JOIN CERTIFICATE C ON PC.CERTIFICATEID = C.CERTIFICATEID 
							AND PC.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
						INNER JOIN CERTIFICATIONTYPE CT ON C.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
						LEFT JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.CERTIFICATEID = C.CERTIFICATEID
							AND PIF.SKUID = PC.SKUID
						LEFT JOIN IMPORTER I ON I.IMPORTERID = C.IMPORTERID
						LEFT JOIN CUSTOMER CU ON CU.CUSTOMERID = C.CUSTOMERID
					WHERE P.SKUID = pi_skuid 
						AND PC.CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
						AND LOWER(C.EXTENSION_EN) = LOWER(ps_extensionno);
			
			END IF;
		
		END IF;

		IF TRIM(ps_extensionno) = '*' THEN
			
			SELECT COUNT(M.MEASUREID)
				INTO li_totaltr
			FROM PRODUCTCERTIFICATE PCE
				INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID 
					AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				INNER JOIN  MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE PCE.SKUID = pi_skuid 
				AND PCE.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificationNumber);

		ELSIF TRIM(ps_extensionno) = 'H' THEN
			
			SELECT COUNT(M.MEASUREID)
				into li_totaltr
			FROM PRODUCTCERTIFICATE PCE
				INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID 
					AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				INNER JOIN  MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE  PCE.SKUID = pi_skuid 
				AND PCE.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
				AND LOWER(CE.MOSTRECENTCERT) = 'y';

		ELSE
			
			SELECT COUNT(M.MEASUREID)
				INTO li_totaltr
			FROM PRODUCTCERTIFICATE PCE
				INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID 
					AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				INNER JOIN  MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE PCE.SKUID = pi_skuid 
				AND PCE.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificationNumber) 
				AND LOWER(CE.EXTENSION_EN) = LOWER(ps_extensionno);
		END IF;

		IF  li_totaltr > 0 THEN
			ps_trexists := 'y';
		ELSE
			ps_trexists := 'n';
		END IF;

	EXCEPTION

		WHEN li_parametersarenull THEN
		ls_errormsg := SQLERRM || 'GETCERTIFICATESINFO. There IS at least one parameters NULL.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded	=> SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.GETCERTIFICATESINFO',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
			
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- GETCERTIFICATESINFO. An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.GETCERTIFICATESINFO',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
			   
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

	END GETCERTIFICATESINFO;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETSIMILARCERTIFICATEINFO(pc_retcursor 			   OUT retcursor,
										ps_matl_num 			IN VARCHAR2,
										pi_certificationtypeid	IN NUMBER, 
										ps_certificationnumber	IN VARCHAR2) 
	AS
	/******************************************************************************
	NAME:       GETSIMILARCERTIFICATEINFO
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/03/2012    Harini         1.Replaced ps_SKU with ps_matl_num
										    2.Added Matl_Num in the select list
											wherever_SKU IS found
	2.00       09/11/2014   Joe Hill        Added Mold Changed Flag and Operations Approval Date
											fields from the PRODUCTCERTIFICATE table to the cursor
											returned to the source program
			  10/29/2016   JESEITZ          Added ADDITIONAL_INFO
											field from the PRODUCTCERTIFICATE table to the cursor
											returned to the source program

	******************************************************************************/
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
	
	--varible
	ls_machineid 	VARCHAR2(50) 		:= NULL;
	ls_operatorid	VARCHAR2(50)		:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000) 		:= NULL;
	li_skuid 		PRODUCT.SKUID%TYPE	:= NULL;

	BEGIN

		IF ps_matl_num IS NULL OR pi_certificationtypeid IS NULL THEN -- As per PRJ3617,replaced ps_SKU with ps_matl_num
			RAISE li_parametersarenull;
		END IF;

		li_skuid:= ICS_COMMON_FUNCTIONS.GETLATESTSKUIDBYSKU(ps_matl_num => ps_matl_num ); -- As per PRJ3617,replaced ps_SKU with ps_matl_num

		OPEN pc_retcursor FOR
			SELECT PCE.CERTIFICATEID,
				   CE.CERTIFICATIONTYPEID,
				   CE.CERTIFICATENUMBER,
				   DATESUBMITTED,
				   ACTIVESTATUS,
				   PCE.DATEASSIGNED_EGI,
				   DATEAPPROVED_CEGI,
				   RENEWALREQUIRED_CGIN,
				   JOBREPORTNUMBER_CEN,
				   EXTENSION_EN,
				   SUPPLEMENTALMOLDSTAMPING_E,
				   P.EMARKREFERENCE_I,
				   EXPIRYDATE_I,
				   PIF.FAMILYID FAMILY,
				   PRODUCTLOCATION,
				   COUNTRYOFMANUFACTURE_N,
				   CU.CUSTOMER,
				   CU.CUSTOMERADDRESS,
				   CUSTOMERSPECIFIC_N,
				   I.IMPORTER,
				   I.IMPORTERADDRESS,
				   I.IMPORTERREPRESENTATIVE,
				   I.COUNTRYLOCATION AS COUNTRYLOCATION_N,
				   BATCHNUMBER_G,
				   PCE.SKUID,
				   P.SKU,
				   LPAD(P.MATL_NUM, 18, 0) MATL_NUM, -- As per PRJ3617,added Matl_Num while retrieving
				   CER.CERTIFICATIONTYPENAME,
				   CE.CERTDATESUBMITTED,
				   CE.CERTDATEAPPROVED,
				   CE.COMPANYNAME,                                                   -- JBH_2.00
				   PCE.MOLD_CHANGED,                                                 -- JBH_2.00
				   PCE.OPER_DATE_APPROVED,                                           -- JBH_2.00
				   PCE.ADDITIONAL_INFO                                               -- jeseitz 10/29/2016
			FROM PRODUCTCERTIFICATE PCE
				INNER JOIN CERTIFICATE ce ON PCE.CERTIFICATEID = CE.CERTIFICATEID 
					AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				INNER JOIN CERTIFICATIONTYPE CER ON CE.CERTIFICATIONTYPEID = CER.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON P.SKUID = PCE.SKUID
				LEFT JOIN PRODUCT_IMARK_FAMILY PIF ON PIF.SKUID = P.SKUID
					AND PIF.CERTIFICATEID = CE.CERTIFICATEID
				LEFT JOIN IMPORTER I ON I.IMPORTERID = CE.IMPORTERID
				LEFT JOIN CUSTOMER CU ON CU.CUSTOMERID = CE.CUSTOMERID
			WHERE PCE.SKUID = li_skuid 
				AND CE.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND UPPER(CE.CERTIFICATENUMBER) <> UPPER(ps_CertificationNumber);

	EXCEPTION

		WHEN li_parametersarenull THEN
		ls_errormsg := SQLERRM || ' There IS at least one parameters NULL.';
				 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.GETCERTIFICATESINFO',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
					
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || ' An error have ocurred.(WHEN OTHERS)';
				   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.GETCERTIFICATESINFO',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				   
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

	END GETSIMILARCERTIFICATEINFO;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETDEFAULTVALUES(pc_retcursor    OUT retcursor,
							   ps_number	   OUT CERTIFICATE.CERTIFICATENUMBER%TYPE,
							   ps_typename	IN CERTIFICATIONTYPE.CERTIFICATIONTYPENAME%TYPE,
							   pi_numberid	IN CERTIFICATE.CERTIFICATEID%TYPE) 
	AS
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
	
	--varible
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	li_certificationtypeid  CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE	:= NULL;
	li_certificateid        CERTIFICATEDEFAULTVALUE.CERTIFICATEID%TYPE	:= NULL;
	ls_certificatenumber    CERTIFICATE.CERTIFICATENUMBER%TYPE			:= NULL;
	
	BEGIN
		
		IF  ps_TypeName IS NULL THEN
			RAISE li_parametersarenull;
		END IF;
		
		li_certificationtypeid := ICS_COMMON_FUNCTIONS.GETCERTIFICATIONID(ps_certificationtypename => ps_TypeName);
		
		IF pi_numberid > 0  THEN
			
			OPEN pc_retcursor FOR
				SELECT DF.FIELDID,
					   DF.REPORTNAME,
					   DF.FIELDNAME,
					   DF.FIELDTEXT,
					   DF.CERTIFICATIONTYPEID,
					   CDV.FIELDVALUE,
					   CDV.CERTIFICATEID
				FROM DEFAULTFIELD DF 
					INNER JOIN CERTIFICATEDEFAULTVALUE CDV ON DF.FIELDID = CDV.FIELDID 
						AND DF.CERTIFICATIONTYPEID = CDV.CERTIFICATIONTYPEID
				WHERE CDV.CERTIFICATEID = pi_numberid 
					AND CDV.CERTIFICATIONTYPEID = li_certificationtypeid
				ORDER BY DF.FIELDID;
			  
				SELECT CE.CERTIFICATENUMBER 
					INTO ls_certificatenumber
				FROM CERTIFICATE CE
				WHERE CE.CERTIFICATEID = pi_numberid;
			  
				ps_number := ls_certificatenumber;
				
		ELSE
		
			OPEN pc_retcursor FOR
				SELECT DF.FIELDID,
					   DF.REPORTNAME,
					   DF.FIELDNAME,
					   DF.FIELDTEXT,
					   DF.CERTIFICATIONTYPEID,
					   CTDV.FIELDVALUE,
					   NULL AS CERTIFICATEID
				FROM DEFAULTFIELD DF
					INNER JOIN CERTIFICATETYPEDEFAULTVALUE CTDV ON DF.CERTIFICATIONTYPEID = CTDV.CERTIFICATIONTYPEID 
						AND DF.FIELDID = CTDV.FIELDID
				WHERE DF.CERTIFICATIONTYPEID = li_certificationtypeid
				ORDER BY DF.FIELDID;
				
				ps_number := '';
				
		END IF;
	
	EXCEPTION
		
		WHEN li_parametersarenull THEN
		ls_errormsg := SQLERRM || '- GETDEFAULTVALUES. There IS at least one parameters NULL.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.GETDEFAULTVALUES',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		 
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- GETDEFAULTVALUES.  An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.GETDEFAULTVALUES',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
		
	END GETDEFAULTVALUES;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICATEBASICINFO_SAVE(ps_matl_num            IN VARCHAR2,
										pi_certificationtypeid IN NUMBER,
										ps_certificatenumber   IN VARCHAR2,
										pi_importerid          IN NUMBER,
										pi_customerid          IN NUMBER,
										ps_operatorname        IN VARCHAR2,
										ps_extension_en        IN NUMBER,
										ps_insertpc            IN VARCHAR2,
										pn_error_num              OUT NUMBER,
										ps_errormsg               OUT VARCHAR2)
	AS
	/******************************************************************************
	 NAME:       CERTIFICATEBASICINFO_SAVE
	 PURPOSE:
	 REVISIONS:
	 Ver        Date        Author           Description
	 ---------  ----------  ---------------  ------------------------------------
	 1.0
	 1.1        10/03/2012    Harini         1.Replaced ps_SKU with ps_matl_num
	 1.2        11/15/2012    Harini         2.Added DateAssigned_egi as SYSDATE in productcertificate
											   Insert statement
	 1.3        03/04/2013    Krishna
	 1.4        03/15/2013    Krishna        Added logic to update MostRecentCert column in
											 Certificate table by calling ICS_MAINTENANCE.SetMostRecentCert
	 1.5        11/21/2013    Harni          1.IF the certificate/ext doesnt exists,check for the existance of
											 certificate and get the most recent ext.IF the extension NUMBER passed
											 in IS more than one greater than the current highest extension for that
											 certificate NUMBER, THEN need to show error message "Extension NUMBER IS
											 OUT of sequence" for both certificatetypeid's 1 and 6.
											 2.Get the current speedrating, IF the product doesnt have current speedrating,
											 insert new record with current speedrating
											 3.IF Certificate already contains SKU THEN need to show errormessage as
											 "Material already exists on this certificate. Cannot add duplicate material to certificate."
											 4.IF the extension OR the product attached to the given extension THEN we need to show message
											 as "This extension has already been approved. Click ok to continue adding this material to it OR cancel to exit."
	 1.6        03/05/2014     Harini        1.Modified the logic of DateApproved and create a record in Certificate table with incremented extension in case
											 of Certificate exists. Merged the logic of certificate type 1 OR 6 into one condition.
											 2.Certificate NUMBER IS checked with upper(CertificateNumber) in all conditions.
											 3. Took CertificateId seq.next val into a varibale and assigned it to CertificateId while inserting the records
											  in Certificate table
	  1.7     07/08/2013      JESEITZ   1. IF certificationtypeid = 6 (E117) copy the supplemental mold stamping from the next lowest extension IF present
													2.  IF this a new E117 certificate NUMBER (certifidationtypeid = 6) allow the first extension to be greater than 0. This IS due to the government renaming certificates.
	******************************************************************************/		
	li_parametersnull EXCEPTION;
	li_paramexist     EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(li_parametersnull, -20005);

	-- New Variables
	ls_operatorid  VARCHAR2(50) := 'ICSDEV';
	li_importerid  NUMBER 		:= NULL;
	li_customerid  NUMBER 		:= NULL;
	li_skuid PRODUCT.SKUID%TYPE	:= NULL;
	ls_certificateexists      VARCHAR2(1)	:= NULL;
	li_certificateid          NUMBER		:= NULL;
	ls_certificateContainssku VARCHAR2(1)		:= NULL;
	ls_supplementalmoldstamping_e VARCHAR2(30)	:= NULL;
	li_pif_cnt	NUMBER			:= NULL;
	ls_imark	VARCHAR2(30)	:= NULL;
	ls_family   VARCHAR2(10)	:= NULL;
	
	-- Old Variables
	ls_machineid     	VARCHAR2(50)	:= NULL;
	ls_errormsg      	VARCHAR2(4000)	:= NULL;
	ls_recorddata    	VARCHAR2(4000)	:= NULL;
	ls_productexists	VARCHAR2(1)		:= NULL;
	ls_extension_en      CERTIFICATE.EXTENSION_EN%TYPE					:= NULL;
	ls_prodspeedrating   PRODUCT.SPEEDRATING%TYPE						:= NULL;
	ls_sapspeedrating    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
	ld_certdateapproved  CERTIFICATE.CERTDATEAPPROVED%TYPE				:= NULL;
	ld_matldateapproved  PRODUCTCERTIFICATE.DATEAPPROVED_CEGI%TYPE		:= NULL;
	ln_certificate_seq   CERTIFICATE.CERTIFICATEID%TYPE					:= NULL;

	BEGIN
		ls_extension_en := ps_extension_en;

		--  Check for any NULL parameters
		IF ps_matl_num IS NULL OR pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN -- As per PRJ3617,replaced ps_SKU with ps_matl_num
			RAISE li_parametersnull;
		END IF;

		--  Set operator name
		IF ps_operatorName IS NOT NULL OR ps_operatorName <> '' THEN
			ls_operatorid := ps_operatorName;
		END IF;

		--  IF this IS a NOM certificate, set the Importer and Customer
		IF pi_certificationtypeid = 3 THEN
			li_importerid := pi_importerid;
			li_customerid := pi_customerid;
		END IF;

		--  Get the most recent SkuId
		li_skuid := ICS_COMMON_FUNCTIONS.GETLATESTSKUIDBYSKU(ps_matl_num => ps_matl_num);  -- As per PRJ3617,replaced ps_SKU with ps_matl_num

		SELECT P.SPEEDRATING
			INTO ls_prodspeedrating
		FROM PRODUCT P
		WHERE P.SKUID = li_skuid;

		BEGIN

			SELECT MA.ATTRIB_VALUE
				INTO ls_sapspeedrating
			FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA
			WHERE MA.ATTRIB_NAME = 'SPEED_RATING' 
				AND MA.MATL_NUM = LPAD(ps_matl_num, 18, 0); -- jeseitz added lpad 1/2/14

		EXCEPTION
			WHEN NO_DATA_FOUND THEN
			ls_sapspeedrating:= NULL;
		END;

		--  IF ls_extension_en IS NULL, THEN just checks IF the CertificateNumber exists.
		ls_certificateexists := ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pi_certificationtypeid,
																		 ps_certificatenumber,
																		 ls_extension_en);

		IF LOWER(ls_certificateexists) = 'n' THEN
		  
			IF (pi_certificationtypeid = 1 OR pi_certificationtypeid = 6) THEN -- ECE 117  -- Not wrapping Certificate NUMBER in UPPER function

				IF ls_extension_en IS NULL THEN

				--  NULL extension passed in - get highest extension - return zero IF not found
				ls_extension_en := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pi_certificationtypeid, ps_certificatenumber);

				ELSE

				--  find the highest extension for certificate NUMBER -- returns NULL IF no existing most recent certification.
				ls_extension_en := ICS_MAINTENANCE.GETCERTIFICATERECENTEXTNUMBER(pi_certificationtypeid,ps_certificatenumber);

					IF (ls_extension_en IS NULL) THEN -- couldn't find an existing most recent certification.
					  
						IF pi_certificationtypeid = 6 THEN -- IF it IS a 117 certificate, we can start a new certificate with an extension higher than 0 -  ok to use what was passed in. -- added jeseitz 7/9/14
							ls_extension_en := ps_extension_en;
						ELSE
							ls_extension_en := 0;  -- start at zero
						END IF;

					ELSE -- we found an existing highest most recent extension for this certificate NUMBER

					--  check to make sure the extension passed in isn't too high (we can't skip an extensioin)
					ls_extension_en := ls_extension_en + 1;
					  
						IF (ps_extension_en > ls_extension_en) THEN
							pn_error_num := 5;
							
							ps_errormsg := 'Extension NUMBER IS OUT of sequence.';						  
							RAISE li_paramexist;
						ELSE
						    --  ok to use extension passed in.
							ls_extension_en := ps_extension_en;
						END IF;
					END IF;
				END IF;
		
			ELSE  -- not certificationtypeid = 1 OR 6
				ls_extension_en := 0;
			END IF;

			--  before creating new certificate,
			--  doublecheck to make sure it doesn't already exist
			--  it shouldn't, but it was duplicating certificates somehow.
			BEGIN
	
				SELECT CERTIFICATEID
					INTO LN_CERTIFICATE_SEQ
				FROM CERTIFICATE
				WHERE CERTIFICATIONTYPEID = pi_certificationtypeid      
					AND CERTIFICATENUMBER = UPPER(ps_certificatenumber) 
					AND EXTENSION_EN = ls_extension_en;
			
			EXCEPTION
	
				WHEN NO_DATA_FOUND THEN
				--  can't find extension = ok to add.
				ln_certificate_seq := 0;
	
				WHEN OTHERS THEN
				ln_certificate_seq:= -1;
	
			END;

			IF  ln_certificate_seq = 0 THEN
				--  create the new certificate/extension because it doesn't exist.
				SELECT CERTIFICATEID_SEQ.NEXTVAL
					INTO ln_certificate_seq
				FROM DUAL;
	
				INSERT INTO CERTIFICATE (CERTIFICATIONTYPEID,
										 CERTIFICATENUMBER,
										 CREATEDBY,
										 CERTIFICATEID,
										 EXTENSION_EN,
										 IMPORTERID,
										 CUSTOMERID,
										 COMPANYNAME)
								 VALUES (pi_certificationtypeid,
										 UPPER(ps_certificatenumber),
										 ls_operatorid,
										 ln_certificate_seq,
										 ls_extension_en,
										 li_importerid,
										 li_customerid,
										 'COOPER'); -- jeseitz 7/29/13 Added COOPER as default
	
				li_certificateid := ln_certificate_seq;

				--  Update the MostRecentCert column in Certificate table
				ICS_MAINTENANCE.SETMOSTRECENTCERT (pi_certificationtypeid,
												   UPPER(ps_certificatenumber),
												   ls_operatorid);

				--  IF this IS a 117 certificate and extension IS > 0,
				--  copy the mold stamping from the previous extension IF it exists. jeseitz 7/8/14 Incident 117016
				IF pi_certificationtypeid = 6 AND NVL(ls_extension_en, 0) > 0 THEN

					BEGIN

						SELECT SUPPLEMENTALMOLDSTAMPING_E
							INTO ls_supplementalmoldstamping_e
						FROM CERTIFICATE
						WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_certificatenumber) 
							AND EXTENSION_EN = ls_extension_en - 1;

						--- may not be an extension -1 IF 117 certificate started with NUMBER higher than 0,
						--  thus the EXCEPTION handling. will just leave the supplemental mold spacing NULL
						UPDATE CERTIFICATE
						SET SUPPLEMENTALMOLDSTAMPING_E = ls_supplementalmoldstamping_e
						WHERE CERTIFICATEID = li_certificateid;

					EXCEPTION
					
						WHEN OTHERS THEN
						-- IF it can't find a previous extension, we don't need to do anything.
						ls_supplementalmoldstamping_e := NULL;
				  
					END;
			  
				END IF;
		  
			END IF;

		ELSE  ---certificate/extension exists

			IF ls_extension_en IS NULL THEN  -- gets highest extension for certificate - return 0 IF NULL (doesn't exist) - it should exist at this point
				ls_extension_en := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pi_certificationtypeid, ps_certificatenumber);
			END IF;

			--  ECE 117 -- Not wrapping Certificate NUMBER in UPPER function
			IF (pi_certificationtypeid = 1 OR pi_certificationtypeid = 6) THEN -- ECE 117 -- Not wrapping Certificate NUMBER in UPPER function
				
				SELECT C.CERTIFICATEID
					INTO li_certificateid
				FROM CERTIFICATE C
				WHERE UPPER(C.CERTIFICATENUMBER) = UPPER(ps_certificatenumber) 
					AND C.CERTIFICATIONTYPEID = pi_certificationtypeid      
					AND C.EXTENSION_EN = ls_extension_en;

			ELSIF pi_certificationtypeid = 4 THEN
				
				SELECT C.CERTIFICATEID
					INTO li_certificateid
				FROM CERTIFICATE C
				WHERE UPPER(C.CERTIFICATENUMBER) = UPPER(ps_certificatenumber) 
					AND C.CERTIFICATIONTYPEID = pi_certificationtypeid;
			
			ELSE
				
				SELECT C.CERTIFICATEID
					INTO li_certificateid
				FROM CERTIFICATE C
				WHERE UPPER(C.CERTIFICATENUMBER) = UPPER(ps_certificatenumber) 
					AND C.CERTIFICATIONTYPEID = pi_certificationtypeid      
					AND UPPER(C.MOSTRECENTCERT) = 'Y';
			END IF;

			--  said wanted to add to next highest extension because the one passed in IS already approved and closed.
			IF (LOWER(ps_insertpc) = 'y') THEN

				IF (pi_certificationtypeid = 1 OR pi_certificationtypeid =6) THEN
					ls_extension_en := ls_extension_en +1;
				END IF;

			--  before creating new certificate, doublecheck to make sure it doesn't already exist
			--  - it shouldn't, but it was duplicating certificates somehow.
				BEGIN
				  
					SELECT CERTIFICATEID 
						into ln_certificate_seq 
					FROM CERTIFICATE
					WHERE CERTIFICATIONTYPEID = pi_certificationtypeid
						AND UPPER(CERTIFICATENUMBER) = UPPER(ps_certificatenumber)
						AND EXTENSION_EN = ls_extension_en;
				
				EXCEPTION
				
					WHEN NO_DATA_FOUND THEN
					-- can't find extension = ok to add.
					ln_certificate_seq := 0;
				 
					WHEN OTHERS THEN
					ln_certificate_seq := -1;
				
				END;

				IF ln_certificate_seq = 0 THEN

					SELECT CERTIFICATEID_SEQ.NEXTVAL
						INTO ln_certificate_seq
					FROM DUAL;

					INSERT INTO CERTIFICATE (CERTIFICATIONTYPEID,
											 CERTIFICATENUMBER,
											 CREATEDBY,
											 CERTIFICATEID,
											 EXTENSION_EN,
											 IMPORTERID,
											 CUSTOMERID,
											 COMPANYNAME)
							         VALUES (pi_certificationtypeid,
											 UPPER(ps_certificatenumber),
											 ls_operatorid,
											 ln_certificate_seq,
											 ls_extension_en,
											 li_importerid,
											 li_customerid,
											 'COOPER');

					li_certificateid := ln_certificate_seq;

					--  Update the MostRecentCert column in Certificate table
					ICS_MAINTENANCE.SETMOSTRECENTCERT (pi_certificationtypeid,
													   UPPER(ps_certificatenumber),
													   ls_operatorid);

					--  IF this IS a 117 certificate and extension > 0,
					--  copy the mold stamping from the previous extension IF it exists. jeseitz 7/8/14 Incident 117016
					IF  pi_certificationtypeid = 6 AND NVL(ls_extension_en,0) > 0 THEN
					  
						BEGIN

							SELECT SUPPLEMENTALMOLDSTAMPING_E 
								INTO ls_supplementalmoldstamping_e
							FROM CERTIFICATE
							WHERE CERTIFICATENUMBER = UPPER(ps_certificatenumber) AND
								  EXTENSION_EN = ls_extension_en - 1;

							UPDATE CERTIFICATE
							SET SUPPLEMENTALMOLDSTAMPING_E = ls_supplementalmoldstamping_e
							WHERE CERTIFICATEID = li_certificateid;

						EXCEPTION
							
							WHEN OTHERS THEN
							-- IF it can't find a previous extension, we don't need to do anything.
							ls_supplementalmoldstamping_e := NULL;
						
						END;
					
					END IF;
				
				END IF;

			END IF;
			
		END IF;

		IF  (ps_insertpc <> 'y' OR ps_insertpc IS NULL) THEN

			--  Check to see IF the certificate already contains the SKU
			ls_certificateContainssku := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATECONTAINSSKU (pi_skuid                => li_skuid,
																							 pi_certificationtypeid  => pi_certificationtypeid,
																							 ps_certificatenumber    => ps_certificatenumber);

			IF  ls_certificateContainssku = 'y' THEN
				pn_error_num := 6;
				ps_errormsg := 'Material already exists on this certificate. Cannot add duplicate material to certificate.';
				RAISE li_paramexist;
			ELSE

				IF (pi_certificationtypeid = 1 OR pi_certificationtypeid = 6) THEN

					BEGIN
					  
						SELECT MAX(DATEAPPROVED_CEGI)
							INTO ld_certdateapproved
						FROM CERTIFICATE C,
							 PRODUCTCERTIFICATE PC
						WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_certificatenumber)
							AND PC.CERTIFICATEID = C.CERTIFICATEID
							AND EXTENSION_EN = ls_extension_en
							AND DATEAPPROVED_CEGI IS NOT NULL;
				  
					EXCEPTION
					
						WHEN NO_DATA_FOUND THEN
						ld_certdateapproved := NULL;
				  
					END;

					BEGIN
					  
						SELECT DATEAPPROVED_CEGI
							INTO ld_matldateapproved
						FROM CERTIFICATE C,
							 PRODUCTCERTIFICATE PC
						WHERE UPPER(C.CERTIFICATENUMBER) = UPPER(ps_certificatenumber)
							  AND C.EXTENSION_EN = ls_extension_en
							  AND PC.CERTIFICATEID = C.CERTIFICATEID
							  AND PC.SKUID = li_skuid;
				  
					EXCEPTION
					
						WHEN NO_DATA_FOUND THEN
						ld_matldateapproved := NULL;
				  
					END;

				END IF;

				IF (ld_certdateapproved IS NOT NULL OR ld_matldateapproved IS NOT NULL) THEN
					pn_error_num := 7;
					ps_errormsg := 'This extension has already been approved. Click ok to add this material to new extenstion OR cancel to exit.';
					RAISE li_paramexist;
				END IF;

			END IF;

			IF ls_prodspeedrating IS NOT NULL 
				AND ls_sapspeedrating IS NOT NULL 
				AND ls_prodspeedrating <> ls_sapspeedrating THEN
				--- We need to create a new product record with the new speedrating.
				ICS_CRUD.PRODUCTREQUESTCERT_SAVE('N',
												 ps_matl_num, 
												 pi_certificationtypeid, 
												 0);
				
				--- Get the skuid we just created:
				SELECT MAX(SKUID)
					INTO li_skuid
				FROM PRODUCT
				WHERE MATL_NUM = LPAD(ps_matl_num, 18, 0);
			END IF;

			ls_productexists := ICS_COMMON_FUNCTIONS.CheckIfProductRequestExists(pi_skuid  				=> li_skuid,
																				 pi_certificationtypeid	=> pi_certificationtypeid);

			--  delete existing records from product request so they can correctly be re-added.
			IF ls_productexists = 'y' THEN
				DELETE
				FROM PRODUCTREQUEST
				WHERE SKUID = li_skuid 
					AND CERTIFICATIONTYPEID = pi_certificationtypeid;
			END IF;

			INSERT INTO PRODUCTREQUEST (CERTIFICATIONTYPEID, 
										SKUID,  
										REQUESTSTATUS)
					            VALUES (pi_certificationtypeid, 
										li_skuid, 
										NULL);

			IF li_certificateid > 0 THEN
			  
				INSERT INTO PRODUCTCERTIFICATE (SKUID,
											    CERTIFICATIONTYPEID,
											    CERTIFICATEID,
											    DATEASSIGNED_EGI)
						                VALUES (li_skuid,
											    pi_certificationtypeid,
											    li_certificateid,
											    TRUNC(SYSDATE));
			   
				IF  pi_certificationtypeid = 4 THEN  --- imark -  insert family info
					--check IF product_imark_certificate record already exists
					SELECT COUNT(*) 
						INTO li_pif_cnt 
					FROM ICS.PRODUCT_IMARK_FAMILY PIF
					WHERE PIF.SKUID = li_skuid 
						AND PIF.CERTIFICATEID = li_certificateid;

					IF li_pif_cnt = 0 THEN

						ls_family := BOM_ATTRIBUTES.GET_IMARK_FAMILY(LPAD(ps_matl_num, 18, 0), li_certificateid);

						BEGIN
							  
							SELECT ATTRIB_VALUE 
								INTO ls_Imark
							FROM CMDR_DATA.MATERIAL_ATTRIBUTE
							WHERE MATL_NUM = LPAD(ps_matl_num, 18, 0)
							AND ATTRIB_NAME = 'I_MARK';
						   
						EXCEPTION
						 
							WHEN NO_DATA_FOUND THEN
							ls_Imark  := NULL;
						 
						END;
						 
						INSERT INTO ICS.PRODUCT_IMARK_FAMILY (CERTIFICATEID, 
															  SKUID,  
															  FAMILYID,  
															  IMARK , 
															  CREATEDBY,  
															  CREATEDON,
															  MODIFIEDBY, 
															  MODIFIEDON)
						                              VALUES (li_certificateid, 
															  li_skuid, 
															  ls_family, 
															  ls_Imark , 
															  ls_operatorid,  
															  SYSDATE,
															  ls_operatorid,  
															  SYSDATE);
					END IF;

				END IF;

			END IF;

		ELSE

			ls_productexists := ICS_COMMON_FUNCTIONS.CHECKIFPRODUCTREQUESTEXISTS (pi_skuid 					=> li_skuid,
																				  pi_certificationtypeid 	=> pi_certificationtypeid);

			--  delete existing records from product country so they can correctly be re-added.
			IF ls_productexists = 'y' THEN				
				DELETE
				FROM PRODUCTREQUEST
				WHERE SKUID = li_skuid 
					AND CERTIFICATIONTYPEID = pi_certificationtypeid;
			END IF;

			INSERT INTO PRODUCTREQUEST (CERTIFICATIONTYPEID, 
										SKUID,  
										REQUESTSTATUS)
				                VALUES (pi_certificationtypeid,   
										li_skuid,  
										NULL);

			IF li_certificateid > 0 THEN
			  
				INSERT INTO PRODUCTCERTIFICATE (SKUID,
											    CERTIFICATIONTYPEID,
											    CERTIFICATEID,
											    DATEASSIGNED_EGI)
						                VALUES (li_skuid,
											    pi_certificationtypeid,
											    li_certificateid,
											    TRUNC(SYSDATE));

				IF pi_certificationtypeid = 4 THEN  --- imark -  insert family info
					
					--check IF product_imark_certificate record already exists
					SELECT COUNT(*) 
						INTO li_pif_cnt 
					FROM ICS.PRODUCT_IMARK_FAMILY PIF
					WHERE PIF.SKUID = li_skuid 
						AND PIF.CERTIFICATEID = li_certificateid;
						
				   -- calculate family
					ls_family := BOM_ATTRIBUTES.GET_IMARK_FAMILY (LPAD(ps_matl_num, 18, 0), 
																  li_certificateid);

					IF  li_pif_cnt = 0 THEN
					   
						BEGIN
						  
							SELECT ATTRIB_VALUE 
								INTO ls_Imark
							FROM CMDR_DATA.MATERIAL_ATTRIBUTE
							WHERE MATL_NUM = LPAD(ps_matl_num, 18, 0)
								AND ATTRIB_NAME = 'I_MARK';
					   
						EXCEPTION
						   
							WHEN NO_DATA_FOUND THEN
							ls_Imark := 0;
					   
						END;
						
						INSERT INTO ICS.PRODUCT_IMARK_FAMILY (CERTIFICATEID, 
															  SKUID,  
															  FAMILYID,  
															  IMARK, 
															  CREATEDBY,  
															  CREATEDON,
															  MODIFIEDBY, 
															  MODIFIEDON)
													  VALUES (li_certificateid, 
															  li_skuid, 
															  ls_family, 
															  ls_Imark, 
															  ls_operatorid,  
															  SYSDATE,
															  ls_operatorid,  
															  SYSDATE);
					
					ELSE
					
						UPDATE ICS.PRODUCT_IMARK_FAMILY
						SET FAMILYID = ls_family
						WHERE SKUID = li_skuid 
							AND CERTIFICATEID = li_certificateid;

					END IF;

				END IF;

			END IF;
			
		END IF;

		COMMIT;

		pn_error_num := 1;

	EXCEPTION

		WHEN li_parametersnull THEN
		pn_error_num := 0;
		ls_errormsg := SQLERRM || ' - CERTIFICATEBASICINFO_SAVE. There are NULL parameters.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid    => ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATEBASICINFO_SAVE',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);				

		WHEN li_paramexist THEN
		ls_errormsg := SQLERRM || ' - CERTIFICATEBASICINFO_SAVE.' || ps_errormsg;
			  
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid    => ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATEBASICINFO_SAVE',
												   ax_recorddata    => ls_recorddata,
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);				

		WHEN OTHERS THEN
		pn_error_num := 0;
		ls_errormsg := SQLERRM || ' - CERTIFICATEBASICINFO_SAVE. An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid    => ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATEBASICINFO_SAVE',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);

	END CERTIFICATEBASICINFO_SAVE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICATESIMTIRE_SAVE(ps_matl_num            IN	VARCHAR2,
									  pi_certificationtypeid IN NUMBER,
									  ps_certificatenumber   IN VARCHAR2,
									  ps_operatorname        IN VARCHAR2)
	AS
	/******************************************************************************
	 NAME:       CERTIFICATESIMTIRE_SAVE
	 PURPOSE:
	 REVISIONS:
	 Ver        Date        Author           Description
	 ---------  ----------  ---------------  ------------------------------------
	 1.0
	 1.1        10/03/2012    Harini         1.Replaced ps_SKU with ps_matl_num
	 1.2        11/21/2013    Harini         1.Replaced pi_certificateid to
											 ps_certificatenumber while calling
											 ICS_COMMON_Functions.CheckIfCertificateContainsSKU function
											 2. Added pi_certificationtypeid while calling
											 ICS_COMMON_Functions.CheckIfProductCountryExists and added the
											 logic for "IF ls_ProductExixts = 'y'"
	******************************************************************************/
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
	
	--varible
	ls_machineid 				VARCHAR2(50)		:= NULL;
	ls_operatorid 				VARCHAR2(50)		:= 'ICSDEV';
	ls_errormsg 				VARCHAR2(4000)		:= NULL;
	li_skuid 					PRODUCT.SKUID%TYPE	:= NULL;
	ls_certificateexists 		VARCHAR2(1)			:= NULL;
	ls_productexixts 			VARCHAR2(1)			:= NULL;
	li_certificateid 			NUMBER				:= NULL;
	ls_certificatecontainssku	VARCHAR2(1)			:= NULL;
	
	BEGIN
		 
		IF ps_matl_num IS NULL OR pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN -- As per PRJ3617,replaced ps_sku with ps_matl_num
			RAISE li_parametersarenull;
		END IF;
		 
		IF ps_OperatorName IS NOT NULL OR ps_OperatorName <> '' THEN
			ls_operatorid := ps_OperatorName;
		END IF;
		 
		li_skuid:= ICS_COMMON_FUNCTIONS.GETLATESTSKUIDBYSKU(ps_matl_num => ps_matl_num); --  -- As per PRJ3617,replaced ps_sku with ps_matl_num

		ls_certificateexists := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATEEXISTS(ps_certificatenumber 		=> ps_certificatenumber,
																			  pi_certificationtypeid	=> pi_certificationtypeid);
		IF ls_certificateexists = 'n' THEN
				 
			INSERT INTO CERTIFICATE(CERTIFICATIONTYPEID,
									CERTIFICATENUMBER,
									CREATEDBY ,
									CERTIFICATEID,
									EXTENSION_EN)
							 VALUES(pi_certificationtypeid,
								    UPPER(ps_certificatenumber),
									ls_operatorid,
									CERTIFICATEID_SEQ.NEXTVAL,
									0);
			   
			SELECT CERTIFICATEID_SEQ.CURRVAL 
				INTO li_certificateid
			FROM DUAL;
			
		ELSE
			li_certificateid := ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(ps_certificatenumber 		=> ps_certificatenumber,
																	  pi_certificationtypeid 	=> pi_certificationtypeid);
			
		END IF;
		   
		ls_certificatecontainssku := ICS_COMMON_FUNCTIONS.CHECKIFCERTIFICATECONTAINSSKU(pi_skuid 				=> li_skuid,
																						pi_certificationtypeid 	=> pi_certificationtypeid,
																						ps_certificatenumber 	=> ps_certificatenumber);

		IF ls_certificatecontainssku = 'n' THEN
			-- As per PRJ3617,Inserting SYSDATE
			INSERT INTO PRODUCTCERTIFICATE(SKUID, 
										   CERTIFICATIONTYPEID, 
										   CERTIFICATEID)
									VALUES(li_skuid, 
										   pi_certificationtypeid, 
										   li_certificateid);
		END IF;

		ls_productexixts := ICS_COMMON_FUNCTIONS.CHECKIFPRODUCTREQUESTEXISTS(pi_skuid => li_skuid,
																			pi_certificationtypeid => pi_certificationtypeid );
		IF ls_productexixts = 'y' THEN
			--delete existing records from product request so they can correctly be re-added.
			DELETE 
			FROM PRODUCTREQUEST 
			WHERE SKUID = li_skuid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
		END IF;

		INSERT INTO PRODUCTREQUEST(CERTIFICATIONTYPEID, 
								   SKUID, 
								   REQUESTSTATUS)
						    VALUES(pi_certificationtypeid,
								   li_skuid,
								   NULL);

	EXCEPTION
	
		WHEN li_parametersarenull THEN
		ls_errormsg := SQLERRM || ' - CERTIFICATESIMTIRE_SAVE. There are NULL parameters.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  CERTIFICATION_CRUD.CERTIFICATESIMTIRE_SAVE',
												  ax_recorddata    	=> 'There IS at least one parameters NULL..',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
				
		RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
		 
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || ' - CERTIFICATESIMTIRE_SAVE. An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  CERTIFICATION_CRUD.CERTIFICATESIMTIRE_SAVE',
												  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
					 
		RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
					 
	END CERTIFICATESIMTIRE_SAVE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICATE_ARCHIVE(ps_certificatenumber	IN  VARCHAR2,
								  ps_operatorname		IN  VARCHAR2) 
	AS
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT( li_parametersarenull,-20005);
	
	--varible
	ls_machineid 			VARCHAR2(50)	:= NULL;
	ls_operatorid 			VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 			VARCHAR2(4000)	:= NULL;
	ls_certificateexists	VARCHAR2(1)		:= NULL;
	li_certificateid 		NUMBER			:= NULL;
	
	BEGIN
		
		IF ps_certificatenumber IS NULL THEN
			RAISE li_parametersarenull;
		END IF;
		
		IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
		END IF;
		
		UPDATE CERTIFICATE C
		SET C.ARCHIVEDATE_CEGI = SYSDATE
		WHERE LOWER(C.CERTIFICATENUMBER) = LOWER(ps_certificatenumber);
	
	EXCEPTION
		
		WHEN li_parametersarenull THEN
		ls_errormsg :=  SQLERRM || ' - Certificate_Archive. There are NULL parameters.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.Certificate_Archive',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		 
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || ' - Certificate_Archive. An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.Certificate_Archive',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
					 
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END CERTIFICATE_ARCHIVE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICATEDEFAULTVALUE_SAVE(pi_fieldvalueid 			IN NUMBER, 
										   pi_certificationtypeid	IN NUMBER, 
										   ps_fieldvalue 			IN VARCHAR2, 
										   ps_certificatenumber 	IN VARCHAR2)
	AS
	--EXCEPTION variables
	li_parametersnull EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(li_parametersnull, -20005);
	
	--Variables
	ls_machineid 			VARCHAR2(50)					:= NULL;
	ls_operatorid 			VARCHAR2(50)					:= 'ICSDEV';
	ls_errormsg 			VARCHAR2(4000)					:= NULL;
	li_certificateid		CERTIFICATE.CERTIFICATEID%TYPE	:= NULL;
	ls_defaultvalueexist	VARCHAR2(1)						:= NULL;
	
	--- Cusor to get all certificate Ids for the certificate NUMBER
	CURSOR c_certificateids(pi_certificationtypeid 	NUMBER, 
							ps_certificatenumber 	VARCHAR2) 
	IS
		SELECT C.CERTIFICATEID
		FROM CERTIFICATE C
		WHERE C.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND C.CERTIFICATENUMBER = ps_certificatenumber;
	  
	ls_certificatenumber CERTIFICATE.CERTIFICATENUMBER%TYPE;
	
	BEGIN
		
		IF pi_fieldvalueid IS NULL OR pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
			RAISE li_parametersnull;
		END IF;
		
		IF pi_certificationtypeid = 4 THEN -- Imark Certificate
			
			li_certificateid := ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATEID();
			
			ls_defaultvalueexist := CERTIFICATION_CRUD.CHECKIFCERTIFICATEDFEXIST(pi_fieldvalueid, 
																				 pi_certificationtypeid, 
																				 li_certificateid);
			
			IF ls_defaultvalueexist = 'y' THEN
				UPDATE CERTIFICATEDEFAULTVALUE
				SET FIELDVALUE = ps_fieldvalue, 
					MODIFIEDON = SYSDATE
				WHERE FIELDID = pi_fieldvalueid 
					AND CERTIFICATIONTYPEID = pi_certificationtypeid 
					AND CERTIFICATEID = li_certificateid;
			
			ELSE
				
				INSERT INTO CERTIFICATEDEFAULTVALUE(FIELDID, 
													CERTIFICATIONTYPEID, 
													CERTIFICATEID, 
													FIELDVALUE)
											 VALUES(pi_fieldvalueid, 
													pi_certificationtypeid, 
													li_certificateid, 
													ps_fieldvalue);
			END IF;
		
		ELSE
			
			OPEN c_certificateids(pi_certificationtypeid, ps_certificatenumber);
				LOOP
				FETCH c_certificateids INTO li_certificateid;
					EXIT WHEN c_certificateids%NOTFOUND;
				
				ls_defaultvalueexist := CERTIFICATION_CRUD.CHECKIFCERTIFICATEDFEXIST(pi_fieldvalueid, 
																					 pi_certificationtypeid, 
																					 li_certificateid);
				
				IF ls_defaultvalueexist = 'y' THEN
					UPDATE CERTIFICATEDEFAULTVALUE
					SET FIELDVALUE = ps_fieldvalue, 
						MODIFIEDON = SYSDATE
					WHERE FIELDID = pi_fieldvalueid 
						AND CERTIFICATIONTYPEID = pi_certificationtypeid 
						AND CERTIFICATEID = li_certificateid;
				
				ELSE
					
					INSERT INTO CERTIFICATEDEFAULTVALUE(FIELDID, 
														CERTIFICATIONTYPEID, 
														CERTIFICATEID, 
														FIELDVALUE)
												 VALUES(pi_fieldvalueid, 
														pi_certificationtypeid, 
														li_certificateid, 
														ps_fieldvalue);
				END IF;
			
			END LOOP;
		
		END IF;
	
	EXCEPTION
		
		WHEN li_parametersnull THEN
		ls_errormsg := SQLERRM || '- CERTIFICATEDEFAULTVALUE_SAVE. There IS at least one parameters NULL.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATEDEFAULTVALUE_SAVE',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- CERTIFICATEDEFAULTVALUE_SAVE. An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICATEDEFAULTVALUE_SAVE',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				 
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END CERTIFICATEDEFAULTVALUE_SAVE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE CERTIFICTYPEDEFAULTVALUE_SAVE(pi_fieldvalueid 		IN NUMBER,
											pi_certificationtypeid	IN NUMBER,
											ps_fieldvalue 			IN VARCHAR2) 
	AS
	--This procedure should Update the Field value on the CERTIFICATETYPEDEFAULTVALUE table.
	
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
	
	--varible
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
	BEGIN
		 
		IF pi_fieldvalueid IS NULL OR pi_certificationtypeid IS NULL THEN
		  RAISE li_parametersarenull;
		END IF;
		
		UPDATE CERTIFICATETYPEDEFAULTVALUE 
		SET FIELDVALUE = ps_fieldvalue,
			MODIFIEDON = SYSDATE
		WHERE FIELDID = pi_fieldvalueid 
			AND CERTIFICATIONTYPEID = pi_certificationtypeid;
	
	EXCEPTION
		
		WHEN li_parametersarenull THEN
		ls_errormsg := SQLERRM || '- CERTIFICTYPEDEFAULTVALUE_SAVE. There IS at least one parameters NULL.';
			 
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICTYPEDEFAULTVALUE_SAVE',
												   ax_recorddata    => 'There IS at least one parameters NULL..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
			  
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);
		
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM ||  '- CERTIFICTYPEDEFAULTVALUE_SAVE. An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.CERTIFICTYPEDEFAULTVALUE_SAVE',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
					  
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END CERTIFICTYPEDEFAULTVALUE_SAVE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	FUNCTION CHECKIFCERTIFICATEDFEXIST(pi_fieldid				IN NUMBER,
									   pi_certificationtypeid	IN NUMBER,
									   pi_certificateid			IN NUMBER) 
											RETURN VARCHAR2 
	AS
	--This function should check IF the default value for the certificate exists...
	
	li_total 				INTEGER 	:= NULL;
	ls_defaultvalueexists 	VARCHAR2(1) := 'n';
	
	BEGIN
		SELECT COUNT(1) 
			INTO li_total
		FROM CERTIFICATEDEFAULTVALUE
		WHERE CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND FIELDID = pi_fieldid 
			AND CERTIFICATEID = pi_certificateid;
		
		IF li_total > 0 THEN
			ls_defaultvalueexists := 'y';
		END IF;
		
		RETURN ls_defaultvalueexists;
	
	END CHECKIFCERTIFICATEDFEXIST;

	/******************************************************************************/
	/************************   END of Function    ********************************/
	/******************************************************************************/

	PROCEDURE IMARKCERTIFICATE_RENEW(pi_newid 			   OUT NUMBER, 
									 pi_oldid 			IN NUMBER, 
									 ps_operatorname	IN VARCHAR2) 
	AS
	/******************************************************************************
	NAME:       CERTIFICATION_CRUD
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	2.00       09/10/2014   Joe Hill        Added Operations Approval Date to the
											c_prodIds cursor and the insert into
											PRODUCTCERTIFICATE sql statement
				 10/29/2016  jeseitz      added additional_info to the c_prodIDs cursor and the insert into PRODUCT CERTIFICATE sql statement
	******************************************************************************/

	-- EXCEPTION variables
	li_newcertificateexists EXCEPTION;
	
	PRAGMA EXCEPTION_INIT(li_newcertificateexists, -20010);
	
	-- Variables
	ls_machineid 	VARCHAR2(50) 	:= NULL;
	ls_operatorid 	VARCHAR2(50) 	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	li_oldextension	NUMBER 			:= NULL;

	-- Cursor to get the SKUID's attached to a specific certificate ID
	CURSOR c_prodids(pi_oldid NUMBER) 
	IS
		SELECT PC.SKUID,
			   P.SKU,
			   PC.DATEAPPROVED_CEGI,
			   PC.DATEASSIGNED_EGI,
			   PC.DATESUBMITTED,                                     -- JBH_2.00
			   PC.OPER_DATE_APPROVED,                                -- JBH_2.00
			   PC.ADDITIONAL_INFO                                    --jeseitz 10/29/2016
		FROM CERTIFICATE C 
			INNER JOIN PRODUCTCERTIFICATE PC ON C.CERTIFICATEID = PC.CERTIFICATEID 
				AND C.CERTIFICATIONTYPEID = PC.CERTIFICATIONTYPEID
			INNER JOIN PRODUCT P ON PC.SKUID = P.SKUID
		WHERE C.CERTIFICATEID = pi_oldid 
			AND PC.DATEREMOVED IS NULL;

	li_oldskuid           PRODUCT.SKUID%TYPE 							:= NULL;
	ls_sku                PRODUCT.SKU%TYPE 								:= NULL;
	ld_dateapproved       PRODUCTCERTIFICATE.DATEAPPROVED_CEGI%TYPE 	:= NULL;
	ld_dateassigned       PRODUCTCERTIFICATE.DATEASSIGNED_EGI%TYPE 		:= NULL;
	ld_datesubmitted      PRODUCTCERTIFICATE.DATESUBMITTED%TYPE 		:= NULL;
	ld_oper_date_approved PRODUCTCERTIFICATE.OPER_DATE_APPROVED%TYPE	:= NULL;           -- JBH_2.00
	ls_additional_info    PRODUCTCERTIFICATE.ADDITIONAL_INFO%TYPE 		:= NULL;           --JESEITZ 10/29/2016

	BEGIN

		IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
		END IF;

		SELECT TO_NUMBER(C.EXTENSION_EN)
			INTO li_oldextension
		FROM CERTIFICATE C
		WHERE CERTIFICATEID = pi_oldid 
			AND CERTIFICATIONTYPEID = 4;

		INSERT INTO CERTIFICATE (CERTIFICATIONTYPEID,
								 CERTIFICATENUMBER,
								 EXTENSION_EN,
								 CREATEDBY,
								 CERTIFICATEID)
						 VALUES (4,
								 'I033',
								 li_oldextension + 1,
								 ls_operatorid,
								 CERTIFICATEID_SEQ.NEXTVAL);

		SELECT CERTIFICATEID_SEQ.CURRVAL
			INTO pi_newid
		FROM DUAL;

		OPEN c_prodids(pi_oldid);

			LOOP
			FETCH c_prodids
				INTO li_oldskuid,
					 ls_sku,
					 ld_dateapproved,
					 ld_dateassigned,
					 ld_datesubmitted,                                           -- JBH_2.00
					 ld_oper_date_approved,                                      -- JBH_2.00
					 ls_additional_info;
				EXIT WHEN c_prodids%NOTFOUND;

				INSERT INTO PRODUCTCERTIFICATE (SKUID,
												CERTIFICATIONTYPEID,
												CERTIFICATEID,
												DATEAPPROVED_CEGI,
												DATEASSIGNED_EGI,
												DATESUBMITTED,                                  -- JBH_2.00
												OPER_DATE_APPROVED,                             -- JBH_2.00
												ADDITIONAL_INFO)                                -- JESEITZ 10/29.2016
										VALUES (li_oldskuid,
												4,
												pi_newid,
												ld_dateapproved,
												ld_dateassigned,
												ld_datesubmitted,                               -- JBH_2.00
												ld_oper_date_approved,                          -- JBH_2.00
												ls_additional_info);                            -- jesetiz 10/29/2016
				  
			END LOOP;

		CLOSE c_prodids;

		UPDATE CERTIFICATE
		SET MOSTRECENTCERT = 'n'
		WHERE CERTIFICATEID = pi_oldid 
			AND certificationtypeid = 4;

	EXCEPTION
		 
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || 'Renew_Imark_Certificate.  An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  CERTIFICATION_CRUD.RenewImarkCertificate',
												   ax_recorddata    => 'An error have ocurred.(WHEN OTHERS)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
			   
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

	END IMARKCERTIFICATE_RENEW;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE ADDNEWSKUSTOIMARKCERTIFICATE(pi_skuid      IN NUMBER,
										   pi_countryid  IN NUMBER) 
	AS
	/******************************************************************************
	 NAME:       ADDNEWSKUSTOIMARKCERTIFICATE
	 PURPOSE:
	 REVISIONS:
	 Ver        Date        Author           Description
	 ---------  ----------  ---------------  ------------------------------------
	 1.0                                      Initial Version
	 1.1        11/15/2012    Harini         1.Added DateAssigned_egi as SYSDATE in productcertificate
											   Insert statement
	******************************************************************************/
	--EXCEPTION variables
	li_parametersarenull EXCEPTION;
	
	-- link the EXCEPTION to the error NUMBER
	PRAGMA EXCEPTION_INIT(li_parametersarenull,-20005);
	
	--varible
	ls_machineid 				VARCHAR2(50)	:= NULL;
	ls_operatorid 				VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 				VARCHAR2(4000)	:= NULL;
	ls_skubelongstocertificate	VARCHAR(1)		:= NULL;
	ls_lastimarkcertificate 	VARCHAR2(20)	:= NULL;
	ld_submiteddate 			DATE			:= NULL;
	ls_requeststatus 			VARCHAR2(1)		:= NULL;
	li_certificateid 			NUMBER			:= NULL;
	ls_productexixts   			VARCHAR2(1)		:= NULL;

	BEGIN
		
		ls_skubelongstocertificate := ICS_COMMON_FUNCTIONS.CHECKIFSKUBELONGSTOCERTIFICATE(pi_skuid 					=> pi_skuid,
																						  pi_certificationtypeid	=> 4);

		li_certificateid := ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATEID();

		IF ls_skubelongstocertificate = 'n' THEN
		--  Insert on the
			INSERT INTO ICS.PRODUCTCERTIFICATE(SKUID,
											   CERTIFICATIONTYPEID,
											   CERTIFICATEID,
											   DATEASSIGNED_EGI)
										VALUES(pi_skuid,
											   4,
											   li_certificateid,
											   TRUNC(SYSDATE));

			IF ld_submiteddate IS NULL THEN
				ls_requeststatus := 'I';
			ELSE
				ls_requeststatus := 'A';
			END IF;

			ls_productexixts := ICS_COMMON_FUNCTIONS.CHECKIFPRODUCTREQUESTEXISTS(pi_skuid, 4);
		   
			IF ls_productexixts = 'y' THEN
				DELETE 
				FROM PRODUCTREQUEST 
				WHERE SKUID = pi_skuid 
					AND CERTIFICATIONTYPEID = 4;
			END IF;
			
			INSERT INTO PRODUCTREQUEST(CERTIFICATIONTYPEID, 
									   SKUID, 
									   REQUESTSTATUS)
								VALUES(4,
									   pi_skuid,  
									   ls_requeststatus);
		END IF;

	END ADDNEWSKUSTOIMARKCERTIFICATE;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETCERTIFEXTENSION(pi_certificateid IN NUMBER, 
								 ps_extensionnumber OUT VARCHAR2) 
	AS
	
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
	BEGIN
		
		SELECT C.EXTENSION_EN 
			INTO ps_extensionnumber
		FROM CERTIFICATE C
		WHERE C.CERTIFICATEID = pi_certificateid;
	
	EXCEPTION
	
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || 'GETCERTIFEXTENSION. An error has occurred.';
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
											      ad_daterecorded 	=> SYSDATE,
											      as_processname 	=> 'CERTIFICATION_CRUD.GETCERTIFEXTENSION',
											      ax_recorddata 	=> 'An error has occurred',
											      as_messagecode 	=> TO_CHAR(SQLCODE),
											      as_message 		=> ls_errormsg);
			
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END GETCERTIFEXTENSION;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETLATESTIMARKCERTIFID(pi_certificateid OUT NUMBER) 
	AS
	
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
	BEGIN
	
		pi_certificateid := ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATEID();
	
	EXCEPTION
	
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || 'GETLATESTIMARKCERTIFID.  An error has ocurred.(WHEN OTHERS)';
			  
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  CERTIFICATION_CRUD.GETLATESTIMARKCERTIFID',
												  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
	
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END GETLATESTIMARKCERTIFID;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETLATESTGSOCERTIFNUMBER(ps_certificatenumber OUT VARCHAR2) 
	AS
	
	ls_certificatenumber VARCHAR2(20)	:= NULL;
	ls_machineid VARCHAR2(50)			:= NULL;
	ls_operatorid VARCHAR2(50) 			:= 'ICSDEV';
	ls_errormsg VARCHAR2(4000)			:= NULL;
	
	BEGIN
	   
		ls_certificatenumber := ICS_COMMON_FUNCTIONS.GETLATESTGSOCERTIFICATENUM();
	   
		ps_certificatenumber := ls_certificatenumber;
	
	EXCEPTION
		 
		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || 'GETLATESTGSOCERTIFNUMBER.  An error have ocurred.(WHEN OTHERS)';
			   
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  CERTIFICATION_CRUD.GETLATESTGSOCERTIFNUMBER',
												  ax_recorddata    	=> 'An error have ocurred.(WHEN OTHERS)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
			   
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END GETLATESTGSOCERTIFNUMBER;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_BRANDS(pc_retcursor OUT retcursor)
	AS
	/******************************************************************************
	 NAME:       GET_BRANDS
	 PURPOSE:
	 REVISIONS:
	 Ver        Date        Author           Description
	 ---------  ----------  ---------------  ------------------------------------
	 1.0        2/18/2013   Krishna          - Added Brand IS NOT NULL condition
											 - Added Trim function in select
				 3/14/2013   jeseitz   --- added union with CMDR_DATA to also get new brands for marketing screen
	******************************************************************************/
	BEGIN

		OPEN pc_retcursor FOR
			
			SELECT DISTINCT UPPER(TRIM(BRAND)) AS BRAND
			FROM PRODUCT
			WHERE BRAND IS NOT NULL
			
			UNION
			
			SELECT DISTINCT UPPER(TRIM(ATTRIB_VALUE)) AS BRAND
			FROM CMDR_DATA.MATERIAL_ATTRIBUTE
			WHERE ATTRIB_NAME = 'BRAND'
			ORDER BY BRAND;

	END GET_BRANDS;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GET_BRANDLINES (ps_brand IN VARCHAR2,
						   pc_retcursor OUT retcursor) as
	/******************************************************************************
	 NAME:       GET_BRANDLINES
	 PURPOSE:
	 REVISIONS:
	 Ver        Date        Author           Description
	 ---------  ----------  ---------------  ------------------------------------
	 1.0        2/18/2013   Krishna          - Added Brand_Line IS NOT NULL condition
											 - Added Trim function in select
			  3/14/2013   jeseitz   --- added union with CMDR_DATA to also get new brand_lines for marketing screen
	******************************************************************************/
	BEGIN

		OPEN pc_retcursor FOR

			SELECT DISTINCT UPPER(TRIM(BRAND_LINE)) AS LINE   -- GET BRAND_LINES FROM PRODUCT TABLE
			FROM PRODUCT
			WHERE UPPER(TRIM(BRAND)) = UPPER(TRIM(ps_brand))
				AND BRAND_LINE IS NOT NULL
			
			UNION
		   
			SELECT DISTINCT UPPER(TRIM(ATTRIB_VALUE)) AS LINE   -- ALSO GET BRAND_LINES FROM CMDR IN CASE THERE ARE NEW ONES.
			FROM CMDR_DATA.MATERIAL_ATTRIBUTE
			WHERE ATTRIB_NAME = 'BRAND_LINE'
				AND MATL_NUM IN 
					(SELECT MATL_NUM 
					FROM CMDR_DATA.MATERIAL_ATTRIBUTE
					WHERE ATTRIB_NAME = 'BRAND' 
						AND UPPER(TRIM(ATTRIB_VALUE)) = UPPER(TRIM(ps_brand)))
			ORDER BY LINE;

	END GET_BRANDLINES;


	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

	PROCEDURE GETSKUDESCRIPTORS(ps_matl_num		IN  VARCHAR2,
								pc_retcursor	  OUT retcursor)
	AS
	/******************************************************************************
	 NAME:       GETSKUDESCRIPTORS
	 PURPOSE:
	 REVISIONS:
	 Ver        Date        Author           Description
	 ---------  ----------  ---------------  ------------------------------------
	 1.0        10/22/2013   Harini          Initial Version
	******************************************************************************/
	
	ls_errormsg 	VARCHAR2(4000)	:= NULL;
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	
	BEGIN
		
		OPEN pc_retcursor FOR

			SELECT MA.MATL_NUM,
				   MA.ATTRIB_NAME,
				   MA.ATTRIB_VALUE
			FROM  MATERIAL_ATTRIBUTE MA
			WHERE MA.MATL_NUM = ps_matl_num
				AND (UPPER(MA.ATTRIB_NAME) = 'TUBE_TYPE'
				OR UPPER(MA.ATTRIB_NAME)   = 'SIDEWALL_TYPE'
				OR UPPER(MA.ATTRIB_NAME)   = 'LEGACY_COOPER_SKU'
				OR UPPER(MA.ATTRIB_NAME)   = 'RMA_TIRE_PLY_CONSTRUCTION'
				OR UPPER(MA.ATTRIB_NAME)   = 'BRAND_LINE');
	
	EXCEPTION
	
	WHEN OTHERS THEN
	ls_errormsg := SQLERRM || 'GETSKUDESCRIPTORS. An error has occurred.';
		
	APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
											  ad_operatorid 	=> ls_operatorid,
											  ad_daterecorded	=> SYSDATE,
											  as_processname 	=> 'CERTIFICATION_CRUD.GETSKUDESCRIPTORS',
											  ax_recorddata 	=> 'An error has occurred',
											  as_messagecode 	=> TO_CHAR(SQLCODE),
											  as_message 		=> ls_errormsg);
			
	RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END GETSKUDESCRIPTORS;

	/******************************************************************************/
	/************************   END of Procedure   ********************************/
	/******************************************************************************/

END CERTIFICATION_CRUD;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.ICS_COMMON_FUNCTIONS 
AS
	/******************************************************************************
	NAME:       ICS_COMMON_FUNCTIONS
	PURPOSE:
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/04/2012    Harini         1.Replaced ps_SKU with ps_Matl_Num in
												GetLatestSkuIdBySKU,CheckIfSKUExists
												procedures.
												Replaced ps_SKUExist with ps_MatlExist
												in CheckIfSKUExists procedure.
	1.2        11/21/2013    Harini          1.Modified pi_CertificateID to  ps_certificatenumber
												in  CheckIfCertificateContainsSKU function
												2.Added pi_certificationtypeid parameter in
												CheckIfProductCountryExists function
				04/07/2016  JESEITZ  added  procedure GetCertificatesByType
	******************************************************************************/
	TYPE retcursor IS REF CURSOR;

	FUNCTION GETCERTIFICATIONID(ps_certificationtypename IN VARCHAR2) 
		RETURN INTEGER;
     
	FUNCTION GETCERTIFICATIONNAMEBYID(pn_certificationtypeid IN VARCHAR2) 
		RETURN STRING;
	
	FUNCTION GETCERTTEMPLATE(ps_certificationtypename IN VARCHAR2) 
		RETURN VARCHAR2;
	
	FUNCTION GETLATESTSKUIDBYSKU(ps_matl_num IN VARCHAR2) 
		RETURN NUMBER;
    
	FUNCTION GETCUSTOMERBYID(pi_customerid IN NUMBER) 
		RETURN VARCHAR2;

	FUNCTION GETREPORTNUMBER(ps_certificatenumber 	IN VARCHAR2,
							 pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2;

	PROCEDURE CHECKIFSKUEXISTS(ps_matl_num	IN 	  VARCHAR2,
							   ps_matlexist   OUT VARCHAR2);

	FUNCTION CHECKCERTDATESUBMITTED(pi_certificateid IN NUMBER) 
		RETURN VARCHAR2;

	FUNCTION CHECKIFCERTIFICATEEXISTS(ps_certificatenumber 		IN VARCHAR2, 
									  pi_certificationtypeid	IN INTEGER) 
		RETURN VARCHAR2;

	PROCEDURE CHECKIFCERTIFICATENUMBEREXISTS(ps_certificatenumber 		IN 	  VARCHAR2, 
											 ps_certificatenumberexists   OUT VARCHAR2);

	FUNCTION GETLATESTIMARKCERTIFICATEID 
		RETURN NUMBER;

	FUNCTION GETLATESTGSOCERTIFICATENUM 
		RETURN VARCHAR2;

	FUNCTION GETCERTIFIIDBYCOUNTRY(pi_countryid IN NUMBER) 
		RETURN NUMBER;
   
	FUNCTION CHECKIFPRODUCTCOUNTRYEXISTS(pi_skuid 				IN NUMBER, 
										 pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2;
   
	FUNCTION CHECKIFPRODUCTREQUESTEXISTS(pi_skuid 				IN NUMBER, 
										 pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2;
   
	FUNCTION CHECKIFSKUBELONGSTOCERTIFICATE(pi_skuid 				IN NUMBER,
											pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2;
   
	FUNCTION GETCERTIFTYPEIDBYCOUNTRYID(pi_countryid IN NUMBER) 
		RETURN NUMBER;

	FUNCTION GETCERTIFICATEID(ps_certificatenumber		IN VARCHAR2,
							  pi_certificationtypeid	IN INTEGER) 
		RETURN CERTIFICATE.CERTIFICATEID%TYPE;
   
	FUNCTION GETCERTIFICATENUMBER(pi_certificateid 			IN NUMBER,
								  pi_certificationtypeid	IN NUMBER) 
		RETURN CERTIFICATE.CERTIFICATENUMBER%TYPE;
   
	FUNCTION GETREQUESTSTATUS(ps_certificatenumber IN VARCHAR2) 
		RETURN VARCHAR2;
   
	PROCEDURE GETCERTIFICATEIDBYNUMBER(ps_certificatenumber 	IN 	  VARCHAR2,
                                       pi_certificationtypeid	IN 	  INTEGER,
                                       ps_extensionno 			IN 	  VARCHAR2,
                                       pi_certificateid 		  OUT NUMBER);
									   
	FUNCTION CHECKIFCERTIFICATECONTAINSSKU(pi_skuid               IN NUMBER,
										   pi_certificationtypeid IN NUMBER,
										   ps_certificatenumber   IN VARCHAR2) 
		RETURN VARCHAR2;

	PROCEDURE GETCERTIFICATESBYTYPE(pn_certificationtypeid 	IN 	  NUMBER,
									ps_all 					IN 	  VARCHAR2, 
									pc_retcursor 			  OUT retcursor);

END ICS_COMMON_FUNCTIONS;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.ICS_COMMON_FUNCTIONS 
AS
	/******************************************************************************
	NAME:       ICS_COMMON_FUNCTIONS
	PURPOSE:
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/04/2012    Harini         1.Replaced ps_SKU with ps_matl_num in
												GetLatestSkuIdBySKU,CheckIfSKUExists
												procedures.
												Replaced ps_SKUExist with ps_matlexist
												in CheckIfSKUExists procedure.
	1.2        11/21/2013    Harini         1.Modified pi_certificateid to ps_certificatenumber
												in  CHECKIFCERTIFICATECONTAINSSKU FUNCTION
											2.Added pi_certificationtypeid parameter in
												CHECKIFPRODUCTCOUNTRYEXISTS FUNCTION
	1.3        03/05/2014    Harini          1.Modified CHECKIFCERTIFICATECONTAINSSKU procedure
												where condition.
	******************************************************************************/

	FUNCTION GETCERTIFICATIONID(ps_certificationtypename IN VARCHAR2) 
		RETURN INTEGER 
	AS

    li_certificationid NUMBER := NULL;
    
	BEGIN
          
		SELECT CE.CERTIFICATIONTYPEID 
			INTO li_certificationid
        FROM CERTIFICATIONTYPE CE
        WHERE LOWER(CE.CERTIFICATIONTYPENAME) = LOWER(ps_certificationtypename);

        RETURN li_certificationid;

    END GETCERTIFICATIONID;


 	FUNCTION GETCERTIFICATIONNAMEBYID(pn_certificationtypeid IN VARCHAR2) 
		RETURN STRING 
	AS

    ls_certificationname VARCHAR2(50) := NULL;
    
	BEGIN
	
        SELECT CE.CERTIFICATIONTYPENAME 
			INTO ls_certificationname
        FROM CERTIFICATIONTYPE CE
        WHERE LOWER(CE.CERTIFICATIONTYPEID) = pn_certificationtypeid;

        RETURN  ls_certificationname;

    END GETCERTIFICATIONNAMEBYID;

	FUNCTION GETCERTTEMPLATE(ps_certificationtypename IN VARCHAR2) 
		RETURN VARCHAR2 
	AS

    ls_certtemplate CERTIFICATIONTYPE.CERTTEMPLATE%TYPE := NULL;
    
	BEGIN
        
		ls_certtemplate := '';
        
		BEGIN
			SELECT CE.CERTTEMPLATE 
				INTO ls_certtemplate
            FROM CERTIFICATIONTYPE CE
            WHERE LOWER(CE.CERTIFICATIONTYPENAME) = LOWER(ps_certificationtypename);
          
		EXCEPTION
            WHEN OTHERS THEN
			ls_certtemplate := '';
           
		END;

		RETURN ls_certtemplate;

    END GETCERTTEMPLATE;

 	FUNCTION GETCUSTOMERBYID(pi_customerid IN NUMBER) 
		RETURN VARCHAR2 
	AS

	ls_customer VARCHAR2(100) := NULL;

	BEGIN
		SELECT C.CUSTOMER 
			INTO ls_customer
		FROM CUSTOMER C
		WHERE C.CUSTOMERID = pi_customerid;

		RETURN ls_customer;

	END GETCUSTOMERBYID;

	FUNCTION CHECKIFSKUBELONGSTOCERTIFICATE(pi_skuid 				IN NUMBER,
											pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2
	AS
	
	lc_exist 			 CHAR	 := 'n';
	li_totalcertificates INTEGER := NULL;
   
	BEGIN
        SELECT COUNT(1) 
			INTO li_totalcertificates
        FROM PRODUCTCERTIFICATE PCE
        WHERE PCE.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND PCE.SKUID = pi_skuid;

        IF li_totalcertificates > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;

        RETURN lc_exist;

	END;

	FUNCTION CHECKIFCERTIFICATECONTAINSSKU(pi_skuid 					IN NUMBER,
                                           pi_certificationtypeid		IN NUMBER,
                                           ps_certificatenumber 		IN VARCHAR2) 
		RETURN VARCHAR2 
	AS
	/******************************************************************************
	NAME:       CHECKIFCERTIFICATECONTAINSSKU
	PURPOSE:    Check whether the Certificate contains SKU
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0                                      Initial Version
	1.1       11/21/2013   Harini            Modified pi_certificateid to  ps_certificatenumber
												in input parameters
	1.2       03/05/2014   Harini            Added Upper(Certificatenumber) in where condition
	******************************************************************************/
	lc_exist 				CHAR	:= 'n';
	li_totalcertificates	INTEGER := NULL;

	BEGIN
        SELECT COUNT(1) 
			INTO li_totalcertificates
        FROM PRODUCTCERTIFICATE PCE,
             CERTIFICATE CE
        WHERE UPPER(CE.CERTIFICATENUMBER) = UPPER(ps_certificatenumber)
			AND PCE.SKUID = pi_skuid
			AND CE.CERTIFICATIONTYPEID = pi_certificationtypeid
			AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			AND PCE.CERTIFICATEID = CE.CERTIFICATEID;

        IF li_totalcertificates > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;

        RETURN lc_exist;

	END CHECKIFCERTIFICATECONTAINSSKU;

	FUNCTION GETLATESTSKUIDBYSKU(ps_matl_num IN VARCHAR2) 
		RETURN NUMBER
	AS
	/******************************************************************************
	NAME:       GETLATESTSKUIDBYSKU
	PURPOSE:    Get the latest SKUID of the given matl_num
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/04/2012    Harini         1.Replaced ps_SKU with ps_matl_num.
												Replaced SKU with Matl_Num in where
												condition.
	******************************************************************************/
	li_skuid NUMBER;

	BEGIN
		SELECT NVL(MAX(SKUID), 0)
			INTO li_skuid
        FROM PRODUCT
        WHERE MATL_NUM = LPAD(ps_matl_num, 18, 0);

        RETURN li_skuid;
		
	END GETLATESTSKUIDBYSKU;

	FUNCTION GETREPORTNUMBER(ps_certificatenumber 	IN VARCHAR2,
							 pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2 
	AS
    --Exception variables
    li_parametersarenull EXCEPTION;
	
    -- link the exception to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);

    --varible
    ls_machineid 	VARCHAR2(50)	:= NULL;
    ls_operatorid 	VARCHAR2(50)	:= 'ICSDEV';
    ls_errormsg 	VARCHAR2(4000)	:= NULL;

    ls_reportnumber VARCHAR2(50)	:= NULL;

	BEGIN

		SELECT SUBSTR(SKU, 9, 4) || '-' || P.SINGLOADINDEX || 'H' || '-' ||
                (CASE
                    WHEN P.MUDSNOWYN = 'y' THEN 'Y'
                    WHEN P.MUDSNOWYN = 'n' THEN 'N'
                END) AS REPORTNUMBER 
					INTO ls_reportnumber
		FROM CERTIFICATE CE
            INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
            INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
		WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATIONTYPEID = pi_certificationtypeid;
			
		RETURN ls_reportnumber;

	END GETREPORTNUMBER;

 	PROCEDURE CHECKIFSKUEXISTS(ps_matl_num	IN 		VARCHAR2,
							   ps_matlexist    OUT	VARCHAR2)
	AS
    /******************************************************************************
	NAME:       CheckIfSKUExists
	PURPOSE:    Check whether the Matl_Num exists
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        10/04/2012    Harini         1.Replaced ps_SKU with ps_matl_num.
											  SKUMAIN_PRODUCT_VIEW is replaced with
											  the below query given in TD.
	******************************************************************************/
	--Exception variables
	li_parametersarenull EXCEPTION;
	
	-- link the exception to the error number
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
	
	--variable
	ls_machineid  	VARCHAR2(50) 			:= NULL;
	ls_operatorid	VARCHAR2(50) 			:= 'ICSDEV';
	ls_errormsg   	VARCHAR2(4000) 			:= NULL;
	ls_matl_num 	PRODUCT.MATL_NUM%TYPE	:= NULL;
	
	li_matlexist INTEGER := NULL;

	BEGIN
        
		IF ps_matl_num IS NULL THEN
            RAISE li_parametersarenull;
        END IF;

        ls_matl_num := LPAD(ps_matl_num, 18, 0);
         
		SELECT COUNT(MATL_NUM) 
			INTO li_matlexist
        FROM (SELECT MATL_NUM 
			  FROM PRODUCT
              WHERE MATL_NUM = ls_matl_num			  
              UNION			  
              SELECT MATL_NUM 
			  FROM CMDR_DATA.MATERIAL_CLASS_LINK
              WHERE MATL_NUM = ls_matl_num);

        IF li_matlexist > 0 THEN
            ps_matlexist := 'y';
        ELSE
            ps_matlexist := 'n';
		END IF;
    
	EXCEPTION
	
        WHEN li_parametersarenull THEN
        ls_errormsg := SQLERRM || ' - CheckIfSKUExists. There are null parameters.';
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid    => ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  ICS_COMMON_FUNCTIONS.CheckIfSKUExists',
												   ax_recorddata    => 'There is at least one parameters null..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
												   
		RAISE_APPLICATION_ERROR (-20005, ls_errormsg);

        WHEN OTHERS THEN
		ls_errormsg := SQLERRM || ' - CheckIfSKUExists.  An error have ocurred.(when others)';
               
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid    => ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  ICS_COMMON_FUNCTIONS.CheckIfSKUExists',
												   ax_recorddata    => 'An error have ocurred.(when others)',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
												   
		RAISE_APPLICATION_ERROR (-20007, ls_errormsg);

	END CHECKIFSKUEXISTS;

	FUNCTION CHECKCERTDATESUBMITTED(pi_certificateid IN NUMBER) 
		RETURN VARCHAR2 
	AS
    --Exception variables
	li_parametersarenull EXCEPTION;
	
	-- link the exception to the error number
	PRAGMA EXCEPTION_INIT(li_parametersarenull,-20005);

	--varible
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;

	li_datesubmittedexists 	INTEGER		:= NULL;
	ls_datesubmittedexists	VARCHAR2(1)	:= NULL;

	BEGIN
        
		IF pi_certificateid IS NULL THEN
            RAISE li_parametersarenull;
        END IF ;

        SELECT COUNT(1) 
			INTO li_datesubmittedexists
        FROM PRODUCTCERTIFICATE
        WHERE CERTIFICATEID = pi_certificateid 
			AND DATESUBMITTED IS NOT NULL;

        IF li_datesubmittedexists > 0 THEN
          ls_datesubmittedexists := 'y';
        ELSE
          ls_datesubmittedexists := 'n';
        END IF;

        RETURN ls_datesubmittedexists;

	EXCEPTION
        
		WHEN li_parametersarenull THEN
		ls_errormsg := SQLERRM || '- CHECKCERTDATESUBMITTED. There is at least one parameters null.';
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid 	=> ls_machineid,
												   ad_operatorid 	=> ls_operatorid,
												   ad_daterecorded  => SYSDATE,
												   as_processname   => '  ics_common_functions.CHECKCERTDATESUBMITTED',
												   ax_recorddata    => 'There is at least one parameters null..',
												   as_messagecode   => TO_CHAR(SQLCODE),
												   as_message       => ls_errormsg);
				
		RAISE_APPLICATION_ERROR(-20005, ls_errormsg);

		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- CHECKCERTDATESUBMITTED. An error have ocurred.(when others)';
               
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  ics_common_functions.CHECKCERTDATESUBMITTED',
												  ax_recorddata    	=> 'An error have ocurred.(when others)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
                      
		RAISE_APPLICATION_ERROR(-20007, ls_errormsg);

	END CHECKCERTDATESUBMITTED;

 	FUNCTION CHECKIFCERTIFICATEEXISTS(ps_certificatenumber 		IN VARCHAR2, 
									  pi_certificationtypeid	IN INTEGER) 
		RETURN VARCHAR2 
	AS

    lc_exists VARCHAR2(1) 		:= 'n';
    li_totalcertificates NUMBER	:= NULL;

    BEGIN
	
		SELECT COUNT(1) 
			INTO li_totalcertificates
        FROM CERTIFICATE C
		WHERE C.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND UPPER(C.CERTIFICATENUMBER) = UPPER(ps_certificatenumber);

        IF li_totalcertificates > 0 THEN
            lc_exists := 'y';
        ELSE
            lc_exists := 'n';
        END IF;

        RETURN lc_exists;

    END CHECKIFCERTIFICATEEXISTS;

	PROCEDURE CHECKIFCERTIFICATENUMBEREXISTS(ps_certificatenumber 		IN 		VARCHAR2, 
											 ps_certificatenumberexists   OUT	VARCHAR2)
	AS
    --Exception variables
	li_parametersarenull EXCEPTION;
	li_duplicatecertnumbers EXCEPTION;
      
	-- link the exception to the error number
	PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);

	--varible
	ls_machineid 	VARCHAR2(50)	:= NULL;
	ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
	ls_errormsg 	VARCHAR2(4000)	:= NULL;

	li_certificatenumberexists INTEGER := NULL;

	BEGIN
	
        IF ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;

        SELECT COUNT(*) 
			INTO li_certificatenumberexists
        FROM CERTIFICATE CE
        WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber);

        IF li_certificatenumberexists > 1 THEN
            RAISE li_duplicatecertnumbers;
        ELSIF li_certificatenumberexists < 1 THEN
            ps_certificatenumberexists := 'n';
        ELSE
            ps_certificatenumberexists := 'y';
        END IF;

    EXCEPTION
	
        WHEN li_parametersarenull THEN
        ls_errormsg := SQLERRM || '- CHECKIFCERTIFICATENUMBEREXISTS. There is at least one parameters null.';
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded	=> SYSDATE,
												  as_processname   	=> '  ics_common_functions.CHECKIFCERTIFICATENUMBEREXISTS',
												  ax_recorddata    	=> 'There is at least one parameters null..',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
                
		RAISE_APPLICATION_ERROR(-20005, ls_errormsg);

        WHEN li_duplicatecertnumbers THEN
		ls_errormsg := SQLERRM || '- CHECKIFCERTIFICATENUMBEREXISTS. Multiple certification numbers found.';
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  ics_common_functions.CHECKIFCERTIFICATENUMBEREXISTS',
												  ax_recorddata    	=> 'Multiple certification numbers found..',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
                
		RAISE_APPLICATION_ERROR(-20005, ls_errormsg);

		WHEN OTHERS THEN
		ls_errormsg := SQLERRM || '- CHECKIFCERTIFICATENUMBEREXISTS. An error have ocurred.(when others)';
               
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded  	=> SYSDATE,
												  as_processname   	=> '  ics_common_functions.CHECKIFCERTIFICATENUMBEREXISTS',
												  ax_recorddata    	=> 'An error have ocurred.(when others)',
												  as_messagecode   	=> TO_CHAR(SQLCODE),
												  as_message       	=> ls_errormsg);
                      
		RAISE_APPLICATION_ERROR(-20007, ls_errormsg);

	END CHECKIFCERTIFICATENUMBEREXISTS;

	FUNCTION GETLATESTIMARKCERTIFICATEID 
		RETURN NUMBER 
	AS

	li_certificateid NUMBER := NULL;
	li_total 		 NUMBER := NULL;

	BEGIN

        SELECT COUNT(C.CERTIFICATEID) 
			INTO li_total
        FROM CERTIFICATE C
        WHERE C.CERTIFICATIONTYPEID = 4;

        IF li_total > 0 THEN
            SELECT C.CERTIFICATEID 
				INTO li_certificateid
            FROM CERTIFICATE C
            WHERE C.CERTIFICATIONTYPEID = 4 
				AND UPPER(C.MOSTRECENTCERT) = 'Y' ;

            RETURN  li_certificateid;
        
		ELSE
            
			RETURN 0;
			
        END IF;

	END GETLATESTIMARKCERTIFICATEID;

	FUNCTION GETLATESTGSOCERTIFICATENUM 
		RETURN VARCHAR2 
	AS

	ls_certificatenumber	VARCHAR2(100)	:= NULL;
	ls_curryear 			VARCHAR(4) 		:= NULL;
	li_increment 			PLS_INTEGER 	:= NULL;
	li_total 				PLS_INTEGER 	:= NULL;

	BEGIN
	
        SELECT TO_CHAR(EXTRACT(YEAR FROM SYSDATE)) 
			INTO ls_curryear
        FROM DUAL;

        SELECT COUNT(C.CERTIFICATENUMBER) 
			INTO li_total
        FROM CERTIFICATE C
        WHERE C.CERTIFICATIONTYPEID = 2 
			AND LOWER(C.CERTIFICATENUMBER) LIKE 'gso-%' 
			AND LENGTH(C.CERTIFICATENUMBER) = 12 
			AND SUBSTR(C.CERTIFICATENUMBER, 5, 4) = ls_curryear;

        IF li_total > 0 THEN
            
			SELECT MAX(C.CERTIFICATENUMBER) 
				INTO ls_certificatenumber
            FROM CERTIFICATE C
            WHERE C.CERTIFICATIONTYPEID = 2 
				AND LOWER(C.CERTIFICATENUMBER) LIKE 'gso-%' 
				AND LENGTH(C.CERTIFICATENUMBER) = 12 
				AND SUBSTR(C.CERTIFICATENUMBER, 5, 4) = ls_curryear;

            li_increment := TO_NUMBER(SUBSTR(ls_certificatenumber, 10, 3));
            
			ls_certificatenumber := REPLACE(ls_certificatenumber, '-' || SUBSTR(ls_certificatenumber, 10, 3), '-' || TRIM(TO_CHAR(li_increment + 1, '009')));

            RETURN ls_certificatenumber;
        
		ELSE
		
            ls_certificatenumber := 'GSO-' || ls_curryear || '-000';

            RETURN ls_certificatenumber;
			
        END IF;

	END GETLATESTGSOCERTIFICATENUM;


	FUNCTION GETCERTIFIIDBYCOUNTRY(pi_countryid IN NUMBER) 
		RETURN NUMBER 
	AS
	
	li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;

	BEGIN
         SELECT NVL(C.CERTIFICATIONTYPEID, '0') 
			INTO li_certificationtypeid
         FROM COUNTRY C
         WHERE C.COUNTRYID = pi_countryid;

         RETURN li_certificationtypeid;
		 
	END GETCERTIFIIDBYCOUNTRY;

	FUNCTION CHECKIFPRODUCTCOUNTRYEXISTS(pi_skuid 				IN NUMBER, 
										 pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2 
	AS
	/******************************************************************************
	NAME:       CHECKIFPRODUCTCOUNTRYEXISTS
	PURPOSE:    Check whether the Product countru exists
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        11/21/2012    Harini         1.Added pi_certificationtypeid in input
											and where clause
	******************************************************************************/
	ls_totalskuids NUMBER;
	
	BEGIN
        
		SELECT COUNT(SKUID) 
			INTO ls_totalskuids
        FROM PRODUCTCOUNTRY
        WHERE SKUID = pi_skuid
			AND CERTIFICATIONTYPEID = pi_certificationtypeid;

        IF ls_totalskuids > 0 THEN
            RETURN 'y';
        ELSE
            RETURN 'n';
        END IF;

	END CHECKIFPRODUCTCOUNTRYEXISTS;

	FUNCTION CHECKIFPRODUCTREQUESTEXISTS(pi_skuid 				IN NUMBER, 
										 pi_certificationtypeid	IN NUMBER) 
		RETURN VARCHAR2 
	AS
	/******************************************************************************
	NAME:       CHECKIFPRODUCTREQUESTEXISTS
	PURPOSE:    Check whether the Product request exists
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0
	1.1        11/21/2012    Harini         1.Added pi_certificationtypeid in input
											and where clause
	******************************************************************************/
	ls_totalskuids NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(SKUID) 
			INTO ls_totalskuids
        FROM PRODUCTREQUEST
        WHERE SKUID = pi_skuid
			AND CERTIFICATIONTYPEID = pi_certificationtypeid;

        IF ls_totalskuids > 0 THEN
            RETURN 'y';
        ELSE
            RETURN 'n';
        END IF;

	END CHECKIFPRODUCTREQUESTEXISTS;

	FUNCTION GETCERTIFTYPEIDBYCOUNTRYID(pi_countryid IN NUMBER) 
		RETURN NUMBER 
	AS

	li_certificationtypeid NUMBER := NULL;
   
	BEGIN
	
        SELECT CO.CERTIFICATIONTYPEID 
			INTO li_certificationtypeid
        FROM COUNTRY CO
        WHERE CO.COUNTRYID = pi_countryid;

        RETURN li_certificationtypeid;

	END GETCERTIFTYPEIDBYCOUNTRYID;

	PROCEDURE GETCERTIFICATEIDBYNUMBER(ps_certificatenumber 	IN    VARCHAR2,
                                       pi_certificationtypeid	IN    INTEGER,
                                       ps_extensionno 			IN    VARCHAR2,
                                       pi_certificateid 		  OUT NUMBER)  
	AS

	li_certificateid CERTIFICATE.CERTIFICATEID%TYPE := NULL;
	li_total 		 NUMBER 						:= NULL;
	
	BEGIN

		SELECT COUNT(1) 
			INTO li_total
		FROM CERTIFICATE CE
		WHERE CE.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND LOWER(CE.EXTENSION_EN) = LOWER(ps_extensionno);

		IF li_total = 0 THEN
			li_certificateid := 0;
		ELSE
			SELECT CERTIFICATEID 
				INTO li_certificateid
			FROM CERTIFICATE CE
			WHERE CE.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND LOWER(CE.EXTENSION_EN) = LOWER(ps_extensionno);
		END IF;

        pi_certificateid:= li_certificateid;

	END GETCERTIFICATEIDBYNUMBER;

	FUNCTION GETCERTIFICATEID(ps_certificatenumber		IN VARCHAR2,
							  pi_certificationtypeid	IN INTEGER) 
		RETURN CERTIFICATE.CERTIFICATEID%TYPE
	AS

	li_certificateid CERTIFICATE.CERTIFICATEID%TYPE := NULL;
	
	BEGIN
		
		IF ((ps_certificatenumber <> '') 
			OR (ps_certificatenumber IS NOT NULL))
			AND (ps_certificatenumber <> 'NotFound') THEN

		SELECT CERTIFICATEID 
			INTO li_certificateid
		FROM CERTIFICATE CE
		WHERE CE.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber);
		ELSE
            li_certificateid := -1;
		END IF;

        RETURN li_certificateid;

	END GETCERTIFICATEID;

	FUNCTION GETCERTIFICATENUMBER(pi_certificateid 			IN NUMBER,
								  pi_certificationtypeid	IN NUMBER) 
		RETURN CERTIFICATE.CERTIFICATENUMBER%TYPE
	AS

	ls_certificatenumber CERTIFICATE.CERTIFICATENUMBER%TYPE := NULL;
	li_totalid 			 NUMBER 							:= NULL;
	
	BEGIN
	
        SELECT COUNT(CE.CERTIFICATEID) 
			INTO li_totalid
        FROM CERTIFICATE CE
        WHERE CE.CERTIFICATEID = pi_certificateid 
			AND CE.CERTIFICATIONTYPEID = pi_certificationtypeid;

        IF li_totalid  < 0 THEN
		
            SELECT CE.CERTIFICATENUMBER INTO ls_certificatenumber
            FROM CERTIFICATE CE
            WHERE CE.CERTIFICATEID = pi_certificateid 
				AND CE.CERTIFICATIONTYPEID = pi_certificationtypeid;

            RETURN ls_certificatenumber;
        
		ELSE
            
			RETURN 'NotFound';
			
        END IF;

	END GETCERTIFICATENUMBER;

	FUNCTION GETREQUESTSTATUS(ps_certificatenumber IN VARCHAR2) 
		RETURN VARCHAR2 
	AS

	ls_requeststatus 	   VARCHAR2(50) := NULL;
	ls_certificationtypeid NUMBER 		:= NULL;

	BEGIN

		SELECT CE.CERTIFICATIONTYPEID 
			INTO ls_certificationtypeid
		FROM CERTIFICATE CE
		WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND ROWNUM < 2;

		IF ls_certificationtypeid = 6 THEN
            ls_certificationtypeid := 1;
		END IF;

		SELECT DISTINCT(CASE
						WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'I' THEN 'InProgress'
						WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'A' THEN 'Approved'
						WHEN PR.REQUESTSTATUS IS NOT NULL AND PR.REQUESTSTATUS = 'R' THEN 'Requested'
						WHEN PR.REQUESTSTATUS IS NULL THEN 'Requested'
						END) AS STATE INTO ls_requeststatus
        FROM CERTIFICATE CE
			INNER JOIN CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = ls_certificationtypeid
			INNER JOIN ICS.PRODUCTREQUEST PR ON CE.CERTIFICATIONTYPEID = pr.certificationtypeid
		WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND ROWNUM < 2;

       RETURN ls_requeststatus;

	END GETREQUESTSTATUS;

	PROCEDURE GETCERTIFICATESBYTYPE(pn_certificationtypeid 	IN 	   NUMBER,
									ps_all 					IN 	   VARCHAR2, 
									pc_retcursor 			   OUT retcursor)
	AS
	
    ls_machineid 	VARCHAR2(50) 	:= NULL;
    ls_operatorid	VARCHAR2(50)	:='ICSDEV';
    ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
    BEGIN

        IF UPPER(ps_all) = 'Y' THEN --- then all certificates
			OPEN pc_retcursor FOR
            SELECT C.CERTIFICATEID, 
				   C.CERTIFICATENUMBER, 
				   C.EXTENSION_EN, 
				   C.MOSTRECENTCERT 
			FROM ICS.CERTIFICATE C
            WHERE C.CERTIFICATIONTYPEID = pn_certificationtypeid
			ORDER BY CERTIFICATENUMBER,
					 EXTENSION_EN;
        ELSE
			OPEN pc_retcursor FOR
			SELECT C.CERTIFICATEID, 
				   C.CERTIFICATENUMBER, 
				   C.EXTENSION_EN, 
				   C.MOSTRECENTCERT 
			FROM ICS.CERTIFICATE C
            WHERE C.CERTIFICATIONTYPEID = pn_certificationtypeid 
				AND C.ARCHIVEDATE_CEGI IS NULL
			ORDER BY CERTIFICATENUMBER,
					 EXTENSION_EN;
        END IF;

	EXCEPTION
        WHEN OTHERS THEN
		ls_errormsg:= SQLERRM || ' -  GETCERTIFICATESBYTYPE. An error has ocurred.(when others)';
             
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
												  ad_operatorid 	=> ls_operatorid,
												  ad_daterecorded	=> SYSDATE,
												  as_processname 	=> ' ICS_COMMON_FUNCTIONS. GETCERTIFICATESBYTYPE',
												  ax_recorddata    	=> 'An error haS ocurred.(when others)',
												  as_messagecode 	=> TO_CHAR(SQLCODE),
												  as_message       	=> SQLERRM);
            
		RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
		
    END GETCERTIFICATESBYTYPE;

END ICS_COMMON_FUNCTIONS;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.ICS_CRUD AS
	/******************************************************************************
	   NAME:       ICS_CRUD
	   PURPOSE:

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0
	   1.1        10/2/2012     Harini         1.Modified SearchBrand,CreateOrDeleteProductCountry,
											   ProductCountry_Save procedures by replacing BrandCode
											   with Brand and Brand_Line,SKu with Matl_num
											   Created new procedure GetTireCharacteristicsAll for getting
											   all the characteristics of tire for a given material.
	   1.2        11/04/2013    Harini         1.As per IDEA 2706,Added ps_SevereWeatherInd  as output parameter
											   in GetTireCharacteristicsAll procedure
	   1.3       06/02/2016   JESEITZ    Added Procedures ProductRequestCert_Save and ProductRequest_Save for
													  MarketingNew screen that shows all certifications on one screen.
	******************************************************************************/
	 
	 TYPE retCursor IS REF CURSOR;

	 PROCEDURE imarkconformityupdate(pd_certdatesubmitted	IN	DATE);
	 
	 PROCEDURE manage_customer(ps_addnewcustomer    IN		VARCHAR2, 
	                           pi_customerid        IN		NUMBER, 
							   ps_customer_n        IN		VARCHAR2, 
							   ps_customeraddress_n IN		VARCHAR2, 
							   ps_actsigreq         IN		VARCHAR2, 
							   ret_customerid         OUT   NUMBER);
	 
	 PROCEDURE manage_importer(ps_addnewimporter           IN	 VARCHAR2, 
	                           pi_importerid               IN	 NUMBER, 
							   ps_importer_n               IN	 VARCHAR2, 
							   ps_importeraddress_n        IN	 VARCHAR2, 
							   ps_importerrepresentative_n IN	 VARCHAR2, 
							   ps_countrylocation_n        IN	 VARCHAR2, 
							   ret_importerid 				 OUT NUMBER);
	 
	 PROCEDURE GetRegions(pc_retCursor OUT RETCURSOR);
	 
	 PROCEDURE getcustomers(pc_customers OUT RETCURSOR);
	 
	 PROCEDURE getimporters(pc_importers OUT RETCURSOR);
	 
	 PROCEDURE GetRegionsAndCountries(pc_retCursor OUT RETCURSOR);
	 
	 PROCEDURE GetCountriesByRegionID(pc_Countries OUT   RETCURSOR,
	                                  pi_RegionId     IN INTEGER);
	 
	 PROCEDURE GetCountriesByRegionName(pc_Countries OUT   RETCURSOR,
	                                    ps_RegionName   IN VARCHAR);
	 
	 PROCEDURE getsearchtypes(pc_retCursor OUT RETCURSOR);
	 
	 PROCEDURE GetManufacturingLocs(pc_retCursor OUT RETCURSOR);
	 
	 PROCEDURE GetCompanyNames(pc_retCursor OUT RETCURSOR);
	 
	 PROCEDURE GetCertifications(pc_retCursor OUT RETCURSOR);
	 
	 PROCEDURE SearchBrand(pc_BrandProduct       OUT    RETCURSOR, 
	                       pc_RegionsCertified   OUT    RETCURSOR,
						   pc_RegionNotCertified OUT    RETCURSOR, 
						   ps_Brand                 IN  VARCHAR2,
						   ps_Brand_Line            IN  VARCHAR2);
	 
	 PROCEDURE SearchBrandRequests(pc_BrandProduct OUT    RETCURSOR,
								   ps_Brand           IN  VARCHAR2,
								   ps_Brand_Line      IN  VARCHAR2);
	 
	 PROCEDURE CreateOrDeleteProductCountry(ps_DeleteMe  IN CHAR,
											 ps_Matl_Num  IN VARCHAR2,
											 pi_skuId     IN INTEGER,
											 pi_Countryid IN INTEGER);
	 
	 PROCEDURE ProductCountry_Save(ps_DeleteMe        IN CHAR,
								   ps_Matl_Num        IN VARCHAR2,
								   pi_CertificationId IN INTEGER,
								   pi_SKUId           IN INTEGER);
	 
	 PROCEDURE productcertification_save(pi_certificationtypeid    IN    NUMBER,
										 pi_skuid                  IN    NUMBER,
										 pi_error_num                OUT NUMBER);
	 
	 PROCEDURE ProductRequestCert_Save(ps_DeleteMe        IN CHAR,
									   ps_Matl_Num        IN VARCHAR2,
									   pi_CertificationId IN INTEGER,
									   pi_SKUId           IN INTEGER);
	 
	 PROCEDURE ProductRequest_Save(pi_certificationtypeid   IN    NUMBER,
								   pi_skuid                 IN    NUMBER,
								   pi_error_num               OUT NUMBER);
		 
	 PROCEDURE GetTireCharacteristicsAll(ps_matl_num           IN    PRODUCT.MATL_NUM%TYPE,
									     ps_Brand              	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_BrandLine          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SizeStamp          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_TireTypeId         	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_PSN                	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_DiscontinueDate    	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SpecNumber         	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SpeedRating        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SingleLoadIndex    	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_DualLoadIndex      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_TubelessSyn        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_ReinforcedYN       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_ExtraLoadYN        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_UTQGTreadWear      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_UTQGTraction       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_UTQGTemp           	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_MudSnowYN          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SevereWeatherInd   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_RimDiameter        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SerialDate         	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_LoadRange          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_MeaRimWidth        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_RegroovableInd     	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_PlantProduced      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_MostRecentDate     	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_IMark              	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_InformeNumber      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_FechaDate          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_TreadPattern       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SpecialProtBrand   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_NominalTireWidth   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_AspectRatio        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_TreadWearInd       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_NameOfManufac      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_Family             	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_DotSerialNumber    	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_TPN                	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_BiasBeltedRadial   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
									     ps_SKU                	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE);
END ICS_CRUD;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.ICS_CRUD AS
	/******************************************************************************
	   NAME:       ICS_CRUD
	   PURPOSE:
	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0
	   1.1        10/2/2012     Harini         1.Modified SearchBrand,CreateOrDeleteProductCountry,
											   ProductCountry_Save procedures by replacing BrandCode
											   with Brand and Brand_Line,SKu with Matl_num
											   Created new procedure GetTireCharacteristicsAll for getting
											   all the characteristics of tire for a given material.
	   1.2        11/14/2012    Harini         2.Modfied SearchBrand procedure by adding SingLoadIndex,
											   DualLoadIndex,SpeedRating in select list
	   1.3        11/19/2012    Harini         3.Modified "from" query for pc_BrandProduct cursor
											   as per Jill's email "material numbers and NEW PROBLEM"
																 in SearchBrand procedure.
											   4. Added Distinct and in "from" clause use Left outer join
											   instead of inner join the Product table for PC_REGIONSCERTIFIED
											   and  PC_REGIONNOTCERTIFIED cursors in SearchBrand procedure.
	   1.4        11/26/2012     Harini       5.As per PRJ3617, Removed Distinct and in from clause use
												inner join instead of Left outer join the Product table
	   1.5        12/03/2012     Harini       6. Retrieving the Aspect ratio by strip off leading zeros
	   1.6        8/16/2013      JESEITZ      1. fixed productcountry_save and createordeleteproductcountry to not duplicate
											  records if called multiple times in loop when more than one column is checked in grid
											  2. Convert discontinued date to date field before inserting into product table
											  3. process ECE3054 and ECE117 certification types separately.
	   1.7        10/16/2013    Harini        1.Modified ProductCountry_Save and CreateOrDeleteProductCountry procedures by adding an else clause
											  for updating the records if the records exists
	   1.8        11/04/2013    Harini        1. As per IDEA 2706,Modified GetTireCharacteristicsAll procedure -Added ps_SevereWeatherInd  as output parameter
											   in  procedure and retrieve it and pass the value to the declared variable
											  2.Modified the call of GetTireCharacteristicsAll in CreateOrDeleteProductCountry and ProductCountry_Save
												 procedures such that it retrieves ls_SevereWeatherInd and inserts/updates into
												 Product table
	   1.9       11/22/2013    Guru           1.Modified ProductCountry_Save,CreateOrDeleteProductCountry procedures for checking the current speed rating
	   1.10      01/10/2014    Harini         1. Added exception when retrieving the speed rating and condition is modified such that both the speed ratings
												should not be null in ProductCountry_Save,CreateOrDeleteProductCountry procedures.
											  2. Removed else part of ln_skuid =0 in both the ProductCountry_Save,CreateOrDeleteProductCountry procedures
	   1.11      02/20/2014    Harini         1. Modified GetTireCharacteristicsAll by retrieving the Tread_Pattern_Design field from DESIGN_NUM
												 attribute in the CMDR_DATA material_attribute view
	   1.12      02/24/2014    Harini         1. Modified CreateOrDeleteProductCountry and ProductCountry_Save procedures for passing ln_skuid=0 if no record
												exists with input matl_num
	   1.13       04/09/2014    jeseitz    fixed CreateOrDeleteProductCountry to not call GetTireCharacteristicsAll if  '999' legacy sku
	   1.14       03/23/2015    jeseitz   SearchBrand - changed case statement for status so that it checks  if the productcountry status is
													null, it also must have a skuid that is not 0.  This is because of the left outer join - if there is no productcountry record
													we do not want the status to be 'Requested'
					 04/09/2015    jeseitz   added hint to searchbrand(thanks John R.)
					 4/10/2015     jeseitz    took out hint and added WITH clause
	******************************************************************************/

	PROCEDURE GetRegions(pc_retCursor OUT RETCURSOR) 
	AS

		ls_MachineId    VARCHAR2(50)  :=NULL;
		ls_OperatorId   VARCHAR2(50)  :='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
	
	BEGIN
	
		OPEN pc_retCursor FOR
			SELECT REGIONID,
				   RegionName 
			FROM REGION;
		
	EXCEPTION
		WHEN OTHERS THEN
		
		ls_ErrorMsg:=  SQLERRM || ' - GetRegions. An error have ocurred.(when others)';
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
												  ad_OPERATORID    => ls_OperatorId,
												  AD_DATERECORDED  => SYSDATE,
												  AS_PROCESSNAME   =>' ICS_CRUD.GetRegions',
												  AX_RECORDDATA    => 'An error have ocurred.(when others)',
												  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
												  AS_MESSAGE       =>SQLERRM);
												  
		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END GetRegions;
	
	PROCEDURE imarkconformityupdate(pd_certdatesubmitted IN DATE) AS
	
		li_certificateId    NUMBER;
		li_skuid            NUMBER;
		ld_skudateassigned  DATE;
		ld_skudateremoved   DATE;
		ld_skudatesubmitted DATE;
		ls_imarkchange      VARCHAR2(1);
		
		-- Cusor to get the SKUID's attached to a specific certificate ID
		CURSOR c_prodcert(li_certificateId NUMBER) 
		IS
			SELECT PC.DATEASSIGNED_EGI, 
				   PC.DATEREMOVED, 
				   PC.DATESUBMITTED, 
				   PC.IMARKCHANGE
			FROM PRODUCTCERTIFICATE PC
			WHERE PC.CERTIFICATEID = li_certificateId;
	
	BEGIN
	
		IF pd_certdatesubmitted IS NOT NULL THEN
		
			-- Get the certificate id of the latest Imark extension
			SELECT MAX(C.CERTIFICATEID) INTO li_certificateId
			FROM CERTIFICATE C
			WHERE C.CERTIFICATIONTYPEID = 4;
			
				IF li_certificateId > 0 THEN
					OPEN c_prodcert(li_certificateId);
					LOOP
						FETCH c_prodcert INTO ld_skudateassigned, ld_skudateremoved, ld_skudatesubmitted, ls_imarkchange;
						EXIT WHEN c_prodcert%NOTFOUND;
						IF (ld_skudateassigned < pd_certdatesubmitted) OR (ld_skudateremoved < pd_certdatesubmitted) THEN
							IF UPPER(ls_imarkchange) = 'I' AND ld_skudatesubmitted IS NULL THEN
							  UPDATE productcertificate pc
							  SET pc.datesubmitted = pd_certdatesubmitted;
							END IF;
						END IF;
					END LOOP;
					CLOSE c_prodcert;
				END IF;
		END IF;
	END imarkconformityupdate;

	PROCEDURE manage_customer(ps_addnewcustomer    IN    VARCHAR2, 
	                          pi_customerid        IN    NUMBER, 
							  ps_customer_n        IN    VARCHAR2, 
							  ps_customeraddress_n IN    VARCHAR2, 
							  ps_actsigreq         IN    VARCHAR2, 
							  ret_customerid         OUT NUMBER) 
	AS
	
		li_customerId	NUMBER;
		
	BEGIN
	
		IF ps_addnewcustomer = 'y' OR pi_customerid = 0 THEN
			
			-- Insert new CUSTOMER record
			INSERT INTO CUSTOMER (CUSTOMERID, 
			                      CUSTOMER, 
								  CUSTOMERADDRESS, 
								  SIGNATUREIND)
			              VALUES (CUSTOMER_SEQ.NEXTVAL, 
						          ps_customer_n, 
								  ps_customeraddress_n, 
								  ps_actsigreq);
			
			-- Now get the importer id of the record we just inserted
			SELECT CUSTOMER_SEQ.CURRVAL INTO li_customerId
			FROM DUAL;
			
			ret_customerid := li_customerId;
			
		ELSE
			-- Update existing customer record
			UPDATE CUSTOMER
			SET CUSTOMER        = ps_customer_n,
			    CUSTOMERADDRESS = ps_customeraddress_n,
			    SIGNATUREIND    = ps_actsigreq
			WHERE CUSTOMERID    = pi_customerid;
			
			RET_CUSTOMERID := pi_customerid;
			
		END IF;
		
	END manage_customer;

	PROCEDURE manage_importer(ps_addnewimporter           IN    VARCHAR2, 
	                          pi_importerid               IN    NUMBER, 
							  ps_importer_n               IN    VARCHAR2, 
							  ps_importeraddress_n        IN    VARCHAR2, 
							  ps_importerrepresentative_n IN    VARCHAR2, 
							  ps_countrylocation_n        IN    VARCHAR2, 
							  ret_importerId                OUT NUMBER) 
	AS
	
	li_importerId	NUMBER:=NULL;
	
	BEGIN
	
		IF ps_addnewimporter = 'y' OR  pi_importerid = 0 THEN
			
			-- Insert new Importer record
			INSERT INTO IMPORTER(IMPORTERID, 
			                     IMPORTER, 
								 IMPORTERADDRESS, 
								 IMPORTERREPRESENTATIVE, 
								 COUNTRYLOCATION)
						  VALUES(IMPORTER_SEQ.NEXTVAL, 
								 ps_importer_n, 
								 ps_importeraddress_n, 
								 ps_importerrepresentative_n, 
								 ps_countrylocation_n);
					
			-- Now get the importer id of the record we just inserted
			SELECT IMPORTER_SEQ.CURRVAL INTO li_ImporterId
			FROM DUAL;
			
			ret_importerid := li_importerId;
			
			ELSE
				-- Update existing Importer record
				UPDATE IMPORTER
				SET IMPORTER               = ps_importer_n,
				    IMPORTERADDRESS        = ps_importeraddress_n,
				    IMPORTERREPRESENTATIVE = ps_importerrepresentative_n,
				    COUNTRYLOCATION        = ps_countrylocation_n
				WHERE IMPORTERID = pi_ImporterId;
				
				ret_importerid := pi_importerid;
		END IF;
		
	END manage_importer;

	PROCEDURE getcustomers(pc_customers OUT RETCURSOR) 
	AS
	
		ls_machineId   VARCHAR2(50)  :=NULL;
		ls_operatorId  VARCHAR2(50)  :='ICSDEV';
		ls_errorMsg    VARCHAR2(4000):=NULL;
		
	BEGIN
	
		OPEN pc_customers FOR
			SELECT DISTINCT CUSTOMER, 
						    CUSTOMERID
			FROM CUSTOMER
			ORDER BY customer ASC;
			
	EXCEPTION
		WHEN OTHERS THEN
		ls_errorMsg := SQLERRM || '- getcustomers.';
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
												  ad_OPERATORID    => ls_OperatorId,
												  AD_DATERECORDED  => SYSDATE,
												  AS_PROCESSNAME   =>' ICS_CRUD.GetCustomers',
												  AX_RECORDDATA    => 'An error have ocurred.(when others)',
												  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
												  AS_MESSAGE       =>SQLERRM);
												  
		RAISE_APPLICATION_ERROR(-20007,ls_ErrorMsg);
		
	END getcustomers;

	PROCEDURE getimporters(pc_importers OUT retCursor) 
	AS
		ls_machineId	VARCHAR2(50)  :=NULL;
		ls_operatorId   VARCHAR2(50)  :='ICSDEV';
		ls_errorMsg     VARCHAR2(4000):=NULL;
		
	BEGIN
	
		OPEN pc_importers FOR
		SELECT DISTINCT IMPORTER, 
						IMPORTERID
		FROM IMPORTER
		ORDER BY IMPORTER ASC;
		
	EXCEPTION
		WHEN OTHERS THEN
		ls_errorMsg := SQLERRM || '- getimporters.';
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
												  ad_OPERATORID    => ls_OperatorId,
												  AD_DATERECORDED  => sysdate,
												  AS_PROCESSNAME   =>' ICS_CRUD.GetImporters',
												  AX_RECORDDATA    => 'An error have ocurred.(when others)',
												  AS_MESSAGECODE   => to_char(sqlcode),
												  AS_MESSAGE       =>sqlerrm
													);
		
		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END getimporters;

	PROCEDURE GetRegionsAndCountries( pc_retCursor OUT RETCURSOR) 
	AS
		ls_MachineId	VARCHAR2(50)  :=NULL;
		ls_OperatorId   VARCHAR2(50)  :='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
	
	BEGIN
	
		OPEN pc_retCursor FOR
			SELECT COUNTRYID,
				   COUNTRYNAME ,
				   CE.CERTIFICATIONTYPEID,
				   CE.CERTIFICATIONTYPENAME,
				   R.REGIONID,
				   R.REGIONNAME
			FROM COUNTRY CO,
			     CERTIFICATIONTYPE CE,
			     REGION R
			WHERE R.REGIONID = CO.REGIONID And
				  CO.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			ORDER BY R.REGIONNAME,CO.COUNTRYNAME ASC;
	
	EXCEPTION
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || ' - GetRegionsAndCountries. An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
													  ad_OPERATORID => ls_OperatorId,
													  AD_DATERECORDED => SYSDATE,
													  AS_PROCESSNAME =>' ICS_CRUD.GetRegionsAndCountries',
													  AX_RECORDDATA    => 'An error have ocurred.(when others)',
													  AS_MESSAGECODE => TO_CHAR(SQLCODE),
													  AS_MESSAGE       =>SQLERRM);
													  
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END GetRegionsAndCountries;

	PROCEDURE GetCountriesByRegionID(pc_Countries OUT   RETCURSOR,
	                                 pi_RegionId     IN INTEGER) 
	AS
	
		--Exception variables
		li_IdNull exception;
		li_IdInvalid exception;
		-- link the exception to the error number
		pragma exception_init( li_IdNull,-20005);
		pragma exception_init( li_IdInvalid,-20006);
		ls_MachineId VARCHAR2(50):=null;
		ls_OperatorId VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg varchar2(4000);
	
	BEGIN
	
		IF  pi_RegionId IS NULL THEN
			RAISE li_IdNull;
		END IF;
		
		IF  pi_RegionId <=0 THEN
			RAISE li_IdInvalid;
		END IF;
		
		OPEN pc_Countries FOR
			SELECT CO.COUNTRYID,
			       CO.COUNTRYNAME,
			       CO.REGIONID,
			       CO.CertificationTypeId   AS CertificationID,
			       CE.CertificationTypeId   AS CertificationID,
			       CE.CERTIFICATIONTYPENAME AS CertificationName
			FROM  COUNTRY CO INNER JOIN  CERTIFICATIONTYPE CE 
				  ON CO.CertificationTypeId = ce.CertificationTypeId
			WHERE CO.REGIONID = pi_RegionId;
			
	EXCEPTION
		WHEN li_IdNull THEN
			ls_ErrorMsg:=  SQLERRM || ' - GetCountriesByRegion. pi_RegionId is null.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => SYSDATE,
													  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegion',
													  AX_RECORDDATA    => 'pi_RegionId is null.',
													  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
													  AS_MESSAGE       => ls_ErrorMsg);
													  
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);
		
		WHEN li_IdInvalid THEN
			ls_ErrorMsg:=  SQLERRM || ' - GetCountriesByRegion. pi_RegionId is Invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => SYSDATE,
													  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegion',
													  AX_RECORDDATA    => 'pi_RegionId is Invalid.',
													  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
													  AS_MESSAGE       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);
			
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || ' - GetCountriesByRegion. An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => sysdate,
													  AS_PROCESSNAME   =>' ICS_CRUD.GetCountriesByRegion',
													  AX_RECORDDATA    => 'An error have ocurred.(when others)',
													  AS_MESSAGECODE   => to_char(sqlcode),
													  AS_MESSAGE       =>ls_ErrorMsg);
													  
			RAISE_APPLICATION_ERROR(-20007,ls_ErrorMsg);
			
	end GetCountriesByRegionID;

	PROCEDURE GetCountriesByRegionName(pc_Countries OUT RETCURSOR,
	                                   ps_RegionName IN VARCHAR) 
	AS
		--Exception variables
		li_IdNull exception;
		li_IdInvalid exception;
		
		-- link the exception to the error number
		PRAGMA EXCEPTION_INIT( li_IdNull,-20005);
		pragma EXCEPTION_INIT( li_IdInvalid,-20006);
		
		ls_MachineId	VARCHAR2(50):=NULL;
		ls_OperatorId   VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
	
	BEGIN
	
		IF  ps_RegionName IS NULL THEN
			RAISE li_IdNull;
		END IF;
		
		OPEN pc_Countries FOR
		SELECT CO.COUNTRYID,
		       CO.COUNTRYNAME,
		       CO.REGIONID,
		       R.REGIONNAME,
		       CE.CERTIFICATIONTYPEID   AS CERTIFICATIONID,
		       CE.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME
		FROM COUNTRY CO INNER JOIN  REGION R             ON CO.REGIONID = R.REGIONID
			            LEFT  JOIN  CERTIFICATIONTYPE CE ON CO.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
		WHERE LOWER(R.REGIONNAME) LIKE  '%' || LOWER(ps_RegionName) || '%' ;
	
	EXCEPTION
		WHEN li_IdNull THEN
			ls_ErrorMsg:=  SQLERRM || ' ps_RegionName is null.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => SYSDATE,
													  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegionName',
													  AX_RECORDDATA    => 'ps_RegionName is null.',
													  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
													  AS_MESSAGE       => ls_ErrorMsg);
			
			raise_application_error (-20005,ls_ErrorMsg);
			
		WHEN li_IdInvalid THEN
		
			ls_ErrorMsg:=  SQLERRM || ' ps_RegionName is Invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => SYSDATE,
													  AS_PROCESSNAME   => ' ICS_CRUD.GetCountriesByRegionName',
													  AX_RECORDDATA    => ' ps_RegionName is Invalid.',
													  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
													  AS_MESSAGE       => ls_ErrorMsg);
			
			raise_application_error (-20006,ls_ErrorMsg);
		
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || ' An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => sysdate,
													  AS_PROCESSNAME   =>' ICS_CRUD.GetCountriesByRegionName',
													  AX_RECORDDATA    => 'An error have ocurred.(when others)',
													  AS_MESSAGECODE   => to_char(sqlcode),
													  AS_MESSAGE       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
	
	END GetCountriesByRegionName;

	PROCEDURE getsearchtypes(pc_retCursor OUT retCursor) 
	AS
		ls_machineId	VARCHAR2(50)   :=NULL;
		ls_operatorId	VARCHAR2(50)   :='ICSDEV';
		ls_errorMsg	    VARCHAR2(4000) :=NULL;
	
	BEGIN
	
	OPEN PC_RETCURSOR FOR
		SELECT S.TYPEID,
			   S.TYPENAME
		FROM SEARCHTYPE S;
		
	EXCEPTION
		WHEN OTHERS THEN
			ls_errorMsg := SQLERRM || ' - Error in ics_crud.getsearchtypes';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(ls_machineId, 
			                                          ls_operatorId, 
													  SYSDATE, 
													  'ics_crud.getsearchtypes', 
													  'Error', 
													  TO_CHAR(SQLCODE), 
													  SQLERRM);
			
			RAISE_APPLICATION_ERROR(-20007, ls_errorMsg);
			
	END getsearchtypes;

	PROCEDURE GetManufacturingLocs( pc_retCursor out retCursor) 
	AS
		ls_MachineId	VARCHAR2(50)  :=NULL;
		ls_OperatorId	VARCHAR2(50)  :='ICSDEV';
		ls_ErrorMsg		VARCHAR2(4000):=NULL;
		
	BEGIN
	
		OPEN pc_retCursor FOR
			SELECT PLANT_ID   AS locationid, 
			       PLANT_NAME AS locationname
			FROM BOM_DATA.PLANT;
			
	EXCEPTION
		WHEN OTHERS THEN
			ls_ErrorMsg:=  sqlerrm || ' - GetManufacturingLocs. An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => SYSDATE,
													  AS_PROCESSNAME   =>' ICS_CRUD.GetManufacturingLocs',
													  AX_RECORDDATA    => 'An error have ocurred.(when others)',
													  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
												      AS_MESSAGE       =>SQLERRM);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	end GetManufacturingLocs;

	PROCEDURE GetCompanyNames(pc_retCursor OUT retCursor) 
	AS
		ls_machineId	VARCHAR2(50)  := NULL;
		ls_operatorId   VARCHAR2(50)  :='ICSDEV';
		ls_errorMsg     VARCHAR2(4000):=NULL;
	
	BEGIN
	
		OPEN pc_retCursor FOR
			SELECT COMPANY AS COMPANYNAME
			FROM COMPANY;
			
	EXCEPTION
		WHEN OTHERS THEN
			ls_errorMsg := SQLERRM || ' - GetCompanyNames. An error has occurred.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_OPERATORID    => ls_OperatorId,
													  AD_DATERECORDED  => SYSDATE,
													  AS_PROCESSNAME   =>' ICS_CRUD.GetCompanyNames',
													  AX_RECORDDATA    => 'An error have ocurred.(when others)',
													  AS_MESSAGECODE   => TO_CHAR(SQLCODE),
													  AS_MESSAGE       =>SQLERRM);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	END GetCompanyNames;

	PROCEDURE getcertifications(pc_retCursor OUT RETCURSOR) 
	AS
		ls_machineId	VARCHAR2(50)  := NULL;
		ls_operatorId 	VARCHAR2(50)  :='ICSDEV';
		ls_errorMsg 	VARCHAR2(4000):=NULL;
		
	BEGIN
	
		OPEN pc_retCursor FOR
			SELECT C.CERTIFICATIONTYPEID   AS CERTIFICATIONID, 
				   C.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME
			FROM CERTIFICATIONTYPE C
			ORDER BY CERTIFICATIONTYPENAME ASC;
		
	EXCEPTION
		WHEN OTHERS THEN
			ls_errorMsg := SQLERRM || ' - Error in ics_crud.getcertifications';
			
			app_message_operations.app_message_insert(ls_machineId, 
													  ls_operatorId, 
													  SYSDATE, 
													  'ics_crud.getcertifications', 
													  'Error', 
													  TO_CHAR(SQLCODE), 
													  SQLERRM);
													  
			RAISE_APPLICATION_ERROR(-20007, ls_errorMsg);
			
	END getcertifications;

	PROCEDURE SearchBrand (pc_BrandProduct       OUT    RETCURSOR, 
						   pc_RegionsCertified   OUT    RETCURSOR,
						   pc_RegionNotCertified OUT    RETCURSOR,  
						   ps_Brand                 IN  VARCHAR2,
						   ps_Brand_Line            IN  VARCHAR2)
	AS
		/***************************************************************************************************
		NAME:       SearchBrand
		PURPOSE:
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0
		1.1        10/2/2012     Harini         1.Replaced ps_BrandCode with ps_Brand and ps_brandLine.
											   2.Removed referring to BrandDetails_Mv
											   and SKUMain_Latest_Views
		1.2        11/14/2012    Harini         3.Added SingleLoadIndex,DualLoadIndex,SpeedRating in
												 select list
		1.3        11/19/2012    Harini         4.Modified the "from" query for pc_BrandProduct cursor
											   as per Jill's email "material numbers and NEW PROBLEM".
											   5. Added Distinct and in "from" clause use Left outer join
											   instead of inner join the Product table for PC_REGIONSCERTIFIED
											   and  PC_REGIONNOTCERTIFIED cursors.
		1.4        11/26/2012     Harini       6.As per PRJ3617, Removed Distinct and in from clause use
												inner join instead of Left outer join the Product table
		1.5        3/23/2015      jeseitz    changed case statement for status so that it checks  if the productcountry status is
													null, it also must have a skuid that is not 0.  This is because of the left outer join - if there is no productcountry record
													we do not want the status to be 'Requested'
													also, commented out SKU - no longer needed.
				   4/8/2015       jeseitz  -- added hint to searh(thanks John R.)
				   4/10/2015       jeseitz -- took out hint and added WITH clause

		*************************************************************************************************/
		--Exception variables
		LI_IDNULL EXCEPTION;

		-- link the exception to the error number
		pragma EXCEPTION_INIT( li_IdNull,-20005);

		--varible
		ls_MachineId	VARCHAR2(50)  :=NULL;
		ls_OperatorId   VARCHAR2(50)  :='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
	  
	BEGIN
	
		IF  (PS_BRAND IS NULL OR PS_BRAND_LINE IS NULL) THEN     -- As per PRJ3617,Checking ps_Brand and ps_Brand_Line are null,instead of ps_BrandCode
			RAISE li_IdNull;
		END IF;
		
		-- As per PRJ3617,MOdified this cursor by removing Brand_Details_Mv and SKUMAINPRODUCT_LATEST_VIEW
		--Search the brand and the product based on the brand name
		
		OPEN pc_BrandProduct FOR
		WITH PRODUCT_DATA AS                            ---JESEITZ 4/10/15 added WITH to improve speed (a lot)
			(SELECT * FROM PRODUCT WHERE
			   BRAND = ps_Brand
			   AND BRAND_LINE = ps_Brand_Line)
			 SELECT MPV.MATL_NUM,
					MPV.BRAND,
					MPV.BRAND_LINE,
					MPV.SKUID ,
					'' SKU,
					MPV.SIZESTAMP,
					CE.CERTIFICATIONTYPEID   AS CERTIFICATIONID,
					CE.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
					C.COUNTRYID,
					C.COUNTRYNAME,
				 (CASE
					 WHEN PC.REQUESTSTATUS IS NOT NULL AND  PC.REQUESTSTATUS='I' THEN 'InProgress'
					 WHEN PC.REQUESTSTATUS IS NOT NULL AND  PC.REQUESTSTATUS='A' THEN 'Approved'
					 WHEN PC.REQUESTSTATUS IS NOT NULL AND  PC.REQUESTSTATUS='R' THEN 'Requested'
					 WHEN PC.REQUESTSTATUS IS NULL     AND  PC.SKUID <> 0        THEN 'Requested'  -- jeseitz added check for skuid  is not null to differentiate between
					 ELSE 'Uncertified'                                                                                                                   --   productcountry record having a null status and no productcountry record
				 END) AS STATE,
					 MPV.SINGLOADINDEX, -- As per PRJ3617,Added SingLoadIndex,DualLoadIndex,SpeedRating in select list
					 MPV.DUALLOADINDEX,
					 MPV.SPEEDRATING
			  FROM
				  ((SELECT LPAD(MA3.MATL_NUM,18,0) AS MATL_NUM,
						  MA2.ATTRIB_VALUE         AS SIZESTAMP,
						  MA3.ATTRIB_VALUE         AS BRAND,
						  MA3.BRAND_LINE,
						  NVL(PD.SKUID,0)          AS SKUID,
						  PD.BRANDDESC,
						  MA5.ATTRIB_VALUE AS SINGLOADINDEX, -- As per PRJ3617,Added SingLoadIndex,DualLoadIndex,SpeedRating in select list
						  MA6.ATTRIB_VALUE AS DUALLOADINDEX,
						  MA7.ATTRIB_VALUE AS SPEEDRATING
					   FROM (SELECT MA.*, MAA.ATTRIB_VALUE AS BRAND_LINE 
					         FROM CMDR_DATA.MATERIAL_CLASS_LINK CL, 
							      CMDR_DATA.MATERIAL_ATTRIBUTE MA,
								  CMDR_DATA.MATERIAL_ATTRIBUTE MAA
							 WHERE  MA.MATL_NUM      = CL.MATL_NUM
							    AND MAA.MATL_NUM     = MA.MATL_NUM
							    AND MA.ATTRIB_NAME   = 'BRAND' 
								AND MA.Attrib_Value  = ps_Brand
							    AND MAA.ATTRIB_NAME  = 'BRAND_LINE' 
								AND MAA.Attrib_Value = ps_Brand_Line
							    AND CL.CLASS_TYPE    = 'Z01'
							    AND (CL.CLASS_NAME = 'TIRE' OR CL.CLASS_NAME = 'TIRE_LEGACY')) MA3
					   LEFT OUTER JOIN CMDR_DATA.MATERIAL_ATTRIBUTE MA2 ON MA2.MATL_NUM = MA3.MATL_NUM AND MA2.ATTRIB_NAME = 'TIRE_SIZE'
					   LEFT OUTER JOIN CMDR_DATA.MATERIAL_ATTRIBUTE MA5 ON MA5.MATL_NUM = MA3.MATL_NUM AND MA5.ATTRIB_NAME = 'STAMPED_SINGLE_LOAD_INDEX'
					   LEFT OUTER JOIN CMDR_DATA.MATERIAL_ATTRIBUTE MA6 ON MA6.MATL_NUM = MA3.MATL_NUM AND MA6.ATTRIB_NAME = 'STAMPED_DUAL_LOAD_INDEX'
					   LEFT OUTER JOIN CMDR_DATA.MATERIAL_ATTRIBUTE MA7 ON MA7.MATL_NUM = MA3.MATL_NUM AND MA7.ATTRIB_NAME = 'SPEED_RATING'
					   LEFT OUTER JOIN  PRODUCT_DATA PD ON PD.MATL_NUM =MA3.MATL_NUM)
				    
					UNION ---ADDED JES 2/25/13
					
				    (SELECT LPAD(P.MATL_NUM,18,0) AS MATL_NUM,
						    TRIM(P.SIZESTAMP) AS SizeStamp,
						    P.BRAND Brand,
						    P.BRAND_LINE      AS Brand_Line,
						    P.SKUID SKUID,
						    P.BRANDDESC,
						    P.SINGLOADINDEX AS SingLoadIndex, -- As per PRJ3617,Added SingLoadIndex,DualLoadIndex,SpeedRating in select list
						    P.DUALLOADINDEX AS DualLoadIndex,
						    P.SPEEDRATING   AS SPEEDRATING
					FROM PRODUCT_DATA P ) )  MPV
			 LEFT JOIN PRODUCTCOUNTRY PC ON PC.SKUID = MPV.SKUID
			 LEFT JOIN COUNTRY C ON PC.COUNTRYID = C.COUNTRYID
			 LEFT JOIN CERTIFICATIONTYPE CE ON C.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
		  WHERE MPV.MATL_NUM  IS NOT NULL
		  ORDER BY MPV.MATL_NUM;
		  
		  --Regions that contains countries that use any certification
		  --As per PRJ3617, Removed Distinct and in from clause use inner join instead of Left outer join the Product table
		
		OPEN PC_REGIONSCERTIFIED FOR
			SELECT DISTINCT R.REGIONID,
			                R.REGIONNAME,
			                CO.COUNTRYID,
			                CO.COUNTRYNAME
			FROM REGION R
			INNER JOIN  COUNTRY CO ON R.REGIONID = CO.REGIONID
			INNER JOIN  PRODUCTCOUNTRY PC ON CO.COUNTRYID = PC.COUNTRYID
			INNER JOIN product p ON PC.SKUID=P.SKUID AND
			(P.BRAND = PS_BRAND AND P.BRAND_LINE = PS_BRAND_LINE) -- AS per PRJ3617,Brandcode is replaced with Brand and Brand_Line
			ORDER BY R.REGIONNAME,CO.COUNTRYNAME;
			 
			 --Regions that contains countries that don't use certifications
			  --As per PRJ3617, Removed Distinct and in from clause use inner join instead of Left outer join the Product table
			
			OPEN PC_REGIONNOTCERTIFIED FOR
				SELECT DISTINCT R.REGIONID,
				                R.REGIONNAME,
								CO.COUNTRYID,
								CO.COUNTRYNAME
				FROM REGION R,
				     COUNTRY CO
				WHERE R.REGIONID = CO.REGIONID AND
				R.REGIONID NOT IN (SELECT DISTINCT(R.REGIONID)
									FROM REGION R INNER JOIN  COUNTRY CO ON R.REGIONID = CO.REGIONID
									INNER JOIN  PRODUCTCOUNTRY PC ON CO.COUNTRYID = PC.COUNTRYID
									INNER JOIN  PRODUCT P ON PC.SKUID=P.SKUID AND P.BRAND = PS_BRAND AND P.BRAND_LINE = ps_Brand_Line) -- AS per PRJ3617,Brandcode is replaced with Brand and Brand_Line
				ORDER BY R.REGIONID;
	EXCEPTION
		 WHEN LI_IDNULL THEN
			ls_ErrorMsg:=  SQLERRM || ' ps_Brand or ps_BrandLine is null.';
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													 ad_OPERATORID    => ls_OperatorId,
													 AD_DATERECORDED  => SYSDATE,
													 AS_PROCESSNAME   => ' ICS_CRUD.SearchBrand',
													 AX_RECORDDATA    => 'ps_Brand or ps_BrandLine is null.',
													 AS_MESSAGECODE   => TO_CHAR(SQLCODE),
													 AS_MESSAGE       => ls_ErrorMsg);
													 
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);
				
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || ' An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_OPERATORID    => ls_OperatorId,
			                                          AD_DATERECORDED  => SYSDATE,
			                                          AS_PROCESSNAME   =>' ICS_CRUD.SearchBrand',
			                                          AX_RECORDDATA    => 'An error have ocurred.(when others)',
			                                          AS_MESSAGECODE   => TO_CHAR(SQLCODE),
			                                          AS_MESSAGE       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	END SearchBrand;
	
	PROCEDURE SearchBrandRequests(pc_BrandProduct  OUT RETCURSOR,  
	                              ps_Brand       IN    VARCHAR2,
								  ps_Brand_Line  IN    VARCHAR2)
	AS
		/***************************************************************************************************
		NAME:       SearchBrandRequests
		PURPOSE:
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0      5/25/2016   jeseitz            initial version

		*************************************************************************************************/
		--Exception variables
		LI_IDNULL EXCEPTION;
		-- link the exception to the error number
		PRAGMA EXCEPTION_INIT( li_IdNull,-20005);
		--varible
		ls_MachineId	VARCHAR2(50):=NULL;
		ls_OperatorId 	VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg 	VARCHAR2(4000):=NULL;
	
	BEGIN
	
		IF  (PS_BRAND IS NULL OR PS_BRAND_LINE IS NULL) THEN     -- As per PRJ3617,Checking ps_Brand and ps_Brand_Line are null,instead of ps_BrandCode
			RAISE li_IdNull;
		END IF;

		OPEN pc_BrandProduct FOR
			WITH PRODUCT_DATA AS                            ---JESEITZ 4/10/15 added WITH to improve speed (a lot)
			(SELECT * 
			 FROM PRODUCT 
			 WHERE BRAND = ps_Brand
			   AND BRAND_LINE = ps_Brand_Line)
			   
			SELECT MPV.MATL_NUM,
				   MPV.BRAND,
				   MPV.BRAND_LINE,
				   mpv.SkuId ,
				   '' SKU,
				   MPV.SIZESTAMP,
				   CE.CERTIFICATIONTYPEID   AS CERTIFICATIONID,
				   CE.CERTIFICATIONTYPENAME AS CERTIFICATIONNAME,
				   ' ' AS CountryId,
				   ' ' AS CountryName,
				  (CASE
						WHEN PR.REQUESTSTATUS IS NOT NULL AND  PR.REQUESTSTATUS='I' THEN 'InProgress'
						WHEN PR.REQUESTSTATUS IS NOT NULL AND  PR.REQUESTSTATUS='A' THEN 'Approved'
						WHEN PR.REQUESTSTATUS IS NOT NULL AND  PR.REQUESTSTATUS='R' THEN 'Requested'
						WHEN PR.REQUESTSTATUS IS NULL     AND PR.SKUID <> 0         THEN 'Requested'  -- jeseitz added check for skuid  is not null to differentiate between
						ELSE 'Uncertified'                                                                                                                   --   productcountry record having a null status and no productcountry record
					END ) AS STATE,
			MPV.SINGLOADINDEX, -- As per PRJ3617,Added SingLoadIndex,DualLoadIndex,SpeedRating in select list
			MPV.DUALLOADINDEX,
			MPV.SPEEDRATING
			FROM
			((SELECT LPAD(ma3.Matl_Num,18,0) AS Matl_Num,
					 MA2.ATTRIB_VALUE AS SizeStamp,
					 MA3.ATTRIB_VALUE AS Brand,
					 MA3.BRAND_LINE,
					 NVL(PD.SKUID,0) AS SKUID,
					 PD.BRANDDESC,
					 MA5.ATTRIB_VALUE AS SingLoadIndex, -- As per PRJ3617,Added SingLoadIndex,DualLoadIndex,SpeedRating in select list
					 MA6.ATTRIB_VALUE AS DualLoadIndex,
					 MA7.ATTRIB_VALUE AS SPEEDRATING
			FROM (SELECT  MA.*, MAA.ATTRIB_VALUE BRAND_LINE 
					FROM CMDR_DATA.MATERIAL_CLASS_LINK CL, 
					     CMDR_DATA.MATERIAL_ATTRIBUTE MA,
			             CMDR_DATA.MATERIAL_ATTRIBUTE MAA
			WHERE MA.MATL_NUM = CL.MATL_NUM
			  AND MAA.MATL_NUM = MA.MATL_NUM
			  AND  MA.ATTRIB_NAME = 'BRAND' AND MA.ATTRIB_VALUE = ps_Brand
			  AND  MAA.ATTRIB_NAME = 'BRAND_LINE' AND MAA.ATTRIB_VALUE = ps_Brand_Line
			  AND CL.CLASS_TYPE = 'Z01'
			  AND (CL.CLASS_NAME = 'TIRE' OR CL.CLASS_NAME = 'TIRE_LEGACY')) MA3
			LEFT OUTER JOIN  CMDR_DATA.MATERIAL_ATTRIBUTE MA2 ON MA2.MATL_NUM  = MA3.MATL_NUM
			AND MA2.ATTRIB_NAME = 'TIRE_SIZE' LEFT OUTER JOIN   CMDR_DATA.MATERIAL_ATTRIBUTE MA5
			ON  MA5.MATL_NUM  = MA3.MATL_NUM AND MA5.ATTRIB_NAME = 'STAMPED_SINGLE_LOAD_INDEX'
			LEFT OUTER JOIN   CMDR_DATA.MATERIAL_ATTRIBUTE MA6 ON MA6.MATL_NUM =MA3.MATL_NUM AND MA6.ATTRIB_NAME = 'STAMPED_DUAL_LOAD_INDEX'
			LEFT OUTER JOIN   CMDR_DATA.MATERIAL_ATTRIBUTE MA7 ON  ma7.Matl_Num=ma3.Matl_Num AND ma7.Attrib_Name = 'SPEED_RATING'
			LEFT OUTER JOIN  PRODUCT_DATA PD ON PD.Matl_Num =ma3.Matl_Num)
			
			UNION ---ADDED JES 2/25/13
			
			(SELECT LPAD(P.Matl_Num,18,0) AS MATL_NUM,
					TRIM(P.SIZESTAMP) AS SizeStamp,
					P.BRAND Brand,
					P.BRAND_LINE AS Brand_Line,
					P.SKUID SKUID,
					P.BRANDDESC,
					P.SINGLOADINDEX AS SingLoadIndex, -- As per PRJ3617,Added SingLoadIndex,DualLoadIndex,SpeedRating in select list
					P.DUALLOADINDEX AS DualLoadIndex,
					P.SPEEDRATING   AS SPEEDRATING
			FROM PRODUCT_DATA P ) )  MPV
			LEFT JOIN PRODUCTREQUEST PR ON PR.SKUID = MPV.SKUID
			LEFT JOIN CERTIFICATIONTYPE CE ON CE.CERTIFICATIONTYPEID = PR.CERTIFICATIONTYPEID
			WHERE MPV.MATL_NUM  IS NOT NULL
			ORDER BY MPV.MATL_NUM;

	EXCEPTION
		WHEN LI_IDNULL THEN
			
			ls_ErrorMsg:=  SQLERRM || ' ps_Brand or ps_BrandLine is null.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_OPERATORID    => ls_OperatorId,
			                                          AD_DATERECORDED  => SYSDATE,
			                                          AS_PROCESSNAME   => ' ICS_CRUD.SearchBrandRequests',
			                                          AX_RECORDDATA    => 'ps_Brand or ps_BrandLine is null.',
			                                          AS_MESSAGECODE   => TO_CHAR(SQLCODE),
			                                          AS_MESSAGE       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);
			
		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  SQLERRM || ' An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
			                                          ad_OPERATORID => ls_OperatorId,
			                                          AD_DATERECORDED  => SYSDATE,
			                                          AS_PROCESSNAME   =>' ICS_CRUD.SearchBrandRequests',
			                                          AX_RECORDDATA    => 'An error has ocurred.(when others)',
			                                          AS_MESSAGECODE   => TO_CHAR(SQLCODE),
			                                          AS_MESSAGE       =>ls_ErrorMsg);
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	END SearchBrandRequests;


	PROCEDURE CreateOrDeleteProductCountry (ps_DeleteMe  IN CHAR,
											ps_Matl_Num  IN VARCHAR2,
											pi_skuId     IN INTEGER,
											pi_Countryid IN INTEGER) 
	AS
		/******************************************************************************
		NAME:       CreateOrDeleteProductCountry
		PURPOSE:
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0
		1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_Matl_Num.
												2.Removed referring SKUMain_Latest_View and call GetTireCharactyeristicsAll proc
		1.2        10/16/2013    Harini         1.Modified the call of GetTireCharacteristicsAll before IF ln_skuid = 0 
												and added else part for this to update the data if record exists.
		1.3        11/04/2013    Harini         1.As per IDEA2706,Modified the call of GetTireCharacteristicsAll
												such that it retrieves ls_SevereWeatherInd and inserts/updates into
												Product table
		1.4        11/22/2013    Guru           1.In the else part of SKUID existance checking,check for the current
												speed rating.
		1.5        01/10/2014    Harini         1. Removed else part of ln_skuid= 0 after GetTireCharacteristicsAll call
												and checking for both product speedrating and Sap speedrating not null
		1.6        02/24/2014    Harini         1. If null is retrieved when getting the max(skuid) from product table,
												assign ln_skuid=0
		1.7         04/07/2014   jeseitz -      don't call GetTireCharacteristicsAll for a '999' legacy sku tire.
		******************************************************************************/
		--Exception variables
		li_ParametersAreNull EXCEPTION;
		li_ParametersArInvalid EXCEPTION;
		
		-- link the exception to the error number
		PRAGMA EXCEPTION_INIT( li_ParametersAreNull,-20005);
		PRAGMA EXCEPTION_INIT( li_ParametersArInvalid,-20006);
		
		--varible
		ls_MachineId                  VARCHAR2(50):=NULL;
		ls_OperatorId                 VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg                   VARCHAR2(4000):=NULL;
		li_TotalSkuWithCountryIdFound INTEGER:=NULL;
		li_TotalSKUID                 NUMBER:=NULL;
		LI_SKUID                      PRODUCT.SKUID%TYPE:=NULL;
		li_CertificationTypeId        NUMBER:=NULL;
		ls_CertificateNumber          VARCHAR2(200):=NULL;
		li_certificateId              NUMBER:=NULL;
		ln_skuid                      INTEGER:=NULL;
		
		ls_IMarkfamily         PRODUCT_IMARK_FAMILY.FAMILYID%TYPE:=NULL; --jeseitz 4/5/2016
		ls_Brand               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BrandLine           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SizeStamp           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TireTypeId          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PSN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DiscontinueDate     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecNumber          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpeedRating         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SingleLoadIndex     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DualLoadIndex       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TubelessSyn         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ReinforcedYN        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ExtraLoadYN         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTreadWear       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTraction        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTemp            CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MudSnowYN           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SevereWeatherInd    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RimDiameter         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SerialDate          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_LoadRange           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MeaRimWidth         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RegroovableInd      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PlantProduced       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MostRecentDate      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_IMark               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_InformeNumber       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_FechaDate           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadPattern        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecialProtBrand    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NominalTireWidth    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_AspectRatio         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadWearInd        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NameOfManufac       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_Family              CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DotSerialNumber     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TPN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BiasBeltedRadial    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SKU                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SAPSpeedRating      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ProdSpeedRating     PRODUCT.SPEEDRATING%TYPE:=NULL;
	
	BEGIN
	
		IF (ps_DeleteMe IS NULL OR pi_Countryid IS NULL)THEN
			RAISE li_ParametersAreNull;
		END IF;
		
		IF pi_skuId IS NULL THEN
			ln_skuId := 0;
		ELSE
			ln_skuid := pi_skuID;
		END IF;
		
		IF (ps_DeleteMe = '' OR pi_Countryid <=0) THEN
			RAISE li_ParametersArInvalid;
		END IF;
		
		---ADDED TO GET CERTIFICATIONTYPEID -  JESEITZ 11/26/2012
		li_CertificationTypeId:= ICS_COMMON_FUNCTIONS.GETCERTIFTYPEIDBYCOUNTRYID(PI_COUNTRYID => PI_COUNTRYID);
		
		IF  SUBSTR(ps_Matl_Num,1,8) <> '00000999' THEN  --- jeseitz added 4/8/2014 - can't get sap data for a 999 tire (doesn't exist in SAP)
			
			GetTireCharacteristicsAll(ps_Matl_Num,
									  ls_Brand,
									  ls_BrandLine,
									  ls_SizeStamp,
									  ls_TireTypeId,
									  ls_PSN,
									  ls_DiscontinueDate,
									  ls_SpecNumber,
									  ls_SpeedRating,
									  ls_SingleLoadIndex,
									  ls_DualLoadIndex ,
									  ls_TubelessSyn,
									  ls_ReinforcedYN ,
									  ls_ExtraLoadYN,
									  ls_UTQGTreadWear,
									  ls_UTQGTraction,
									  ls_UTQGTemp,
									  ls_MudSnowYN,
									  ls_SevereWeatherInd,
									  ls_RimDiameter,
									  ls_SerialDate,
									  ls_LoadRange,
									  ls_MeaRimWidth,
									  ls_RegroovableInd,
									  ls_PlantProduced,
									  ls_MostRecentDate,
									  ls_IMark ,
									  ls_InformeNumber,
									  ls_FechaDate,
									  ls_TreadPattern,
									  ls_SpecialProtBrand,
									  ls_NominalTireWidth,
									  ls_AspectRatio,
									  ls_TreadWearInd,
									  ls_NameOfManufac,
									  ls_Family ,
									  ls_DotSerialNumber,
									  ls_TPN ,
									  ls_BiasBeltedRadial,
									  ls_SKU);

			--Make sure material number wasn't j ust created (in marketing grid, the skuid would get created on the first checked column if there are multiple columns checked)
			IF ln_skuid = 0 THEN
				SELECT NVL(MAX(SKUID),0) INTO ln_skuid 
				FROM PRODUCT 
				WHERE MATL_NUM = LPAD(ps_Matl_Num,18,0);
			END IF;

			--need to check if the speedrating on the record passed in is current.
			IF ln_skuid <> 0 THEN
				
				SELECT P.SPEEDRATING INTO ls_ProdSpeedRating 
				FROM PRODUCT P 
				WHERE P.SKUID = ln_SKUID;

				---add exception block here
				BEGIN
				
					SELECT MA.ATTRIB_VALUE INTO ls_SAPSpeedRating 
					FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA 
					WHERE MA.ATTRIB_NAME = 'SPEED_RATING' 
					  AND MA.MATL_NUM = ps_Matl_Num;

				EXCEPTION
					WHEN NO_DATA_FOUND THEN
						ls_SAPSpeedRating:= NULL;
				END;

				IF ls_ProdSpeedRating IS NOT NULL AND ls_SAPSpeedRating IS NOT NULL AND ls_ProdSpeedRating <> ls_SAPSpeedRating THEN
				
					--- re-set ln_skuid = 0 so that a new current product record will be created.
					ln_skuid := 0;
				END IF;
			END IF;
		END IF;
		
		IF  ps_DeleteMe= 'y' OR ps_DeleteMe= 'Y' THEN
			DELETE FROM  PRODUCTCOUNTRY pc 
			WHERE PC.SKUID =ln_skuId  
			  AND COUNTRYID = pi_Countryid;
		ELSE
			--create the product record if it doesn't exist.
			IF ln_skuid = 0 THEN
				SELECT  SKUID_SEQ.NEXTVAL INTO  ln_SKUID 
				FROM DUAL;
				--Skuid Does not exist in Product,So we insert into product first

			-- As per PRJ3617,Modified the paramters
			INSERT INTO PRODUCT(SKUID, 
			                    BRAND,
								BRAND_LINE, 
								SKU,BRANDDESC,
								MATL_NUM, 
								SIZESTAMP, 
								TIRETYPEID, 
								PSN, 
								DISCONTINUEDDATE,
								SPECNUMBER, 
								SPEEDRATING,
								SINGLOADINDEX, 
								DUALLOADINDEX, 
								TUBELESSYN, 
								REINFORCEDYN,
								EXTRALOADYN, 
								UTQGTREADWEAR, 
								UTQGTRACTION, 
								UTQGTEMP,
								MUDSNOWYN, 
								RIMDIAMETER, 
								SERIALDATE,
								LOADRANGE, 
								MEARIMWIDTH, 
								REGROOVABLEIND, 
								PLANTPRODUCED, 
								MOSTRECENTTESTDATE,
								INFORMENUMBER, 
								FECHADATE, 
								TREADPATTERN, 
								SPECIALPROTECTIVEBAND, 
								NOMINALTIREWIDTH,
								ASPECTRATIO, 
								TREADWEARINDICATORS, 
								NAMEOFMANUFACTURER, 
								DOTSERIALNUMBER, 
								TPN, 
								BIASBELTEDRADIAL,
								SEVEREWEATHERIND)
						VALUES(ln_SKUID,
						       ls_Brand,
							   ls_BrandLine,
							   ls_SKU,
							   NULL,
							   LPAD(ps_Matl_Num,18,0),
							   ls_SizeStamp,
							   ls_TireTypeId,
							   ls_PSN,
						       DECODE(ls_DiscontinueDate,NULL,NULL,
							   TO_DATE(ls_DiscontinueDate,'MM/DD/YYYY')),
							   ls_SpecNumber,
							   ls_SpeedRating,
							   ls_SingleLoadIndex,
							   ls_DualLoadIndex ,
							   ls_TubelessSyn,
						       ls_ReinforcedYN ,
							   ls_ExtraLoadYN,
							   ls_UTQGTreadWear,
							   ls_UTQGTraction,
							   ls_UTQGTemp,
							   ls_MudSnowYN,
							   ls_RimDiameter,
						       ls_SerialDate,
							   ls_LoadRange,
							   ls_MeaRimWidth,
							   ls_RegroovableInd,
							   ls_PlantProduced,
							   ls_MostRecentDate,
							   ls_InformeNumber,
							   ls_FechaDate,
							   ls_TreadPattern,
							   ls_SpecialProtBrand,
							   ls_NominalTireWidth,
							   ls_AspectRatio,
							   ls_TreadWearInd,
							   ls_NameOfManufac,
							   ls_DotSerialNumber,
							   ls_TPN ,
							   ls_BiasBeltedRadial,
							   ls_SevereWeatherInd);
			COMMIT;

			END IF;

			--Checks if the sku and countryid already exists on the ProductCountry table
			SELECT COUNT(1) AS total INTO li_TotalSkuWithCountryIdFound
			FROM  PRODUCTCOUNTRY PC
			WHERE PC.SKUID=LN_SKUID  
			  AND PC.COUNTRYID = pi_Countryid;

			IF li_TotalSkuWithCountryIdFound = 0 THEN

			INSERT INTO PRODUCTCOUNTRY(SKUID, 
									   COUNTRYID,
									   CERTIFICATIONTYPEID)
								VALUES(ln_SKUID,
									   pi_Countryid,
									   NVL(li_CertificationTypeId,0));--JESEITZ ADDED CERTIFICATIONTYPEID 11/26/2012
			END IF;
		
		END IF;
		
	EXCEPTION
		WHEN li_ParametersAreNull THEN
			
			ls_ErrorMsg:=  SQLERRM || ' At least one of the parameters is.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
			                                          ad_OPERATORID => ls_OperatorId,
			                                          AD_DATERECORDED  => SYSDATE,
			                                          AS_PROCESSNAME   => ' ICS_CRUD.CreateOrDeleteProductCountry',
			                                          AX_RECORDDATA     => 'ps_DeleteMe  is null or pi_Countryid is null or ps_Matl_Num  is null.',
			                                          AS_MESSAGECODE   => TO_CHAR(SQLCODE),
			                                          AS_MESSAGE       =>ln_skuid||' '|| ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);
			
		WHEN li_ParametersArInvalid THEN
		
			ls_ErrorMsg:=  SQLERRM || ' At least one of the parameters isinvalid .';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_OPERATORID    => ls_OperatorId,
			                                          AD_DATERECORDED  => SYSDATE,
			                                          AS_PROCESSNAME   => ' ICS_CRUD.CreateOrDeleteProductCountry',
			                                          AX_RECORDDATA    => 'ps_DeleteMe  is null or pi_Countryid is invalid or ps_Matl_Num  is invalid.',
			                                          AS_MESSAGECODE   => TO_CHAR(SQLCODE),
			                                          AS_MESSAGE       =>ln_skuid||' '|| ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);
			
		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  SQLERRM || ' An error have ocurred.(when others)';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_OPERATORID    => ls_OperatorId,
			                                          AD_DATERECORDED  => SYSDATE,
			                                          AS_PROCESSNAME   =>' ICS_CRUD.CreateOrDeleteProductCountry',
			                                          AX_RECORDDATA    => 'An error have ocurred.(when others)',
			                                          AS_MESSAGECODE   => ln_skuid||' '|| pi_Countryid||' '|| TO_CHAR(SQLCODE),
			                                          AS_MESSAGE       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	END CreateOrDeleteProductCountry;


	PROCEDURE ProductCountry_Save (ps_DeleteMe        IN CHAR,
								   ps_Matl_Num        IN VARCHAR2,
								   pi_CertificationId IN INTEGER,
								   pi_SKUId           IN INTEGER) AS
		/******************************************************************************
		NAME:       ProductCountry_Save
		PURPOSE:
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0
		1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_Matl_Num.
												2.Removed referring SKUMain_Latest_View
												and call GetTireCharactyeristicsAl proc
		1.2        10/16/2013    Harini         1.Modified the call of GetTireCharacteristicsAll
												before IF ln_skuid = 0 and added else part for this
												to update the data if record exists.
		1.3        11/04/2013    Harini         1.As per IDEA2706,Modified the call of GetTireCharacteristicsAll
												such that it retrieves ls_SevereWeatherInd and inserts/updates into
												Product table
		1.4        11/22/2013    Guru           1.In the else part of SKUID existance checking,check for the current
												speed rating.
		1.5        01/10/2014    Harini         1. Removed else part of ln_skuid= 0 after GetTireCharacteristicsAll call
												and checking for both product speedrating and Sap speedrating not null
		1.6        02/24/2014    Harini         1. If null is retrieved when getting the max(skuid) from product table,
												assign ln_skuid=0
		1.7        04/07/2013  jeseitz  		don't call GetTireCharacteristicsAll for a '999' legacy sku tire.
		******************************************************************************/
		
		--Varible
		ls_MachineId           VARCHAR2(50):=NULL;
		ls_OperatorId          VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ln_ErrorNum            NUMBER(1):=NULL;
		ln_SkuID               PRODUCT.SKUID%TYPE:=NULL;
		ls_IMarkfamily         PRODUCT_IMARK_FAMILY.FAMILYID%type:=NULL;
		le_Done                EXCEPTION;
		ls_Brand               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BrandLine           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SizeStamp           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TireTypeId          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PSN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DiscontinueDate     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecNumber          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpeedRating         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SingleLoadIndex     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DualLoadIndex       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TubelessSyn         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ReinforcedYN        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ExtraLoadYN         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTreadWear       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTraction        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTemp            CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MudSnowYN           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SevereWeatherInd    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RimDiameter         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SerialDate          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_LoadRange           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MeaRimWidth         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RegroovableInd      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PlantProduced       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MostRecentDate      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_IMark               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_InformeNumber       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_FechaDate           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadPattern        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecialProtBrand    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NominalTireWidth    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_AspectRatio         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadWearInd        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NameOfManufac       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_Family              CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DotSerialNumber     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TPN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BiasBeltedRadial    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SKU                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ProdSpeedRating     PRODUCT.SPEEDRATING%TYPE:=NULL;
		ls_SAPSpeedRating      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		
	BEGIN

		--Make sure material number wasn't just created (in marketing grid, the skuid would get created on the first checked column if there are multiple columns checked)
		ln_skuid := NVL(pi_skuid,0);
		
		IF ln_skuid = 0 THEN
			SELECT NVL(MAX(SKUID),0) INTO ln_skuid 
			FROM PRODUCT 
			WHERE MATL_NUM = LPAD(ps_Matl_Num,18,0);
		END IF;

		--need to check if the speedrating on the record passed in is current.
		IF ln_skuid <> 0 AND SUBSTR(ps_Matl_Num,1,8) <> '00000999' THEN -- jeseitz added 4/7/2014 can't check against SAP for tire not in sap
		
			SELECT P.SPEEDRATING INTO ls_ProdSpeedRating FROM PRODUCT P WHERE P.SKUID = ln_SKUID;

			---add exception block here
			BEGIN
			SELECT MA.ATTRIB_VALUE INTO ls_SAPSpeedRating 
			FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA 
			WHERE MA.ATTRIB_NAME ='SPEED_RATING' 
			  AND MA.MATL_NUM = ps_Matl_Num;

			EXCEPTION
				WHEN NO_DATA_FOUND THEN
					ls_SAPSpeedRating := NULL;
			END;

			IF ls_ProdSpeedRating IS NOT NULL AND ls_SAPSpeedRating IS NOT NULL AND ls_ProdSpeedRating <> ls_SAPSpeedRating THEN
				--- re-set ln_skuid = 0 so that a new current product record will be created.
				ln_skuid := 0;
			END IF;
		END IF;
		
		DELETE 
		FROM PRODUCTCOUNTRY
		WHERE CERTIFICATIONTYPEID = pi_CertificationID
		  AND SKUID = ln_Skuid;
		
		COMMIT;
		
		--  If only deleting coutnries exit now
		IF UPPER(ps_deleteme) = 'Y' THEN
			RAISE le_Done;
		END IF;

		IF ln_skuid = 0 THEN
			--Skuid Does not exist in Product,So we insert into product first

			GetTireCharacteristicsAll(ps_Matl_Num,
									  ls_Brand,
									  ls_BrandLine,
									  ls_SizeStamp,
									  ls_TireTypeId,
									  ls_PSN,
									  ls_DiscontinueDate,
									  ls_SpecNumber,
									  ls_SpeedRating,
									  ls_SingleLoadIndex,
									  ls_DualLoadIndex ,
									  ls_TubelessSyn,
									  ls_ReinforcedYN ,
									  ls_ExtraLoadYN,
									  ls_UTQGTreadWear,
									  ls_UTQGTraction,
									  ls_UTQGTemp,
									  ls_MudSnowYN,
									  ls_SevereWeatherInd,
									  ls_RimDiameter,
									  ls_SerialDate,
									  ls_LoadRange,
									  ls_MeaRimWidth,
									  ls_RegroovableInd,
									  ls_PlantProduced,
									  ls_MostRecentDate,
									  ls_IMark ,
									  ls_InformeNumber,
									  ls_FechaDate,
									  ls_TreadPattern,
									  ls_SpecialProtBrand,
									  ls_NominalTireWidth,
									  ls_AspectRatio,
									  ls_TreadWearInd,
									  ls_NameOfManufac,
									  ls_Family ,
									  ls_DotSerialNumber,
									  ls_TPN ,
									  ls_BiasBeltedRadial,
									  ls_SKU);

			SELECT SKUID_SEQ.NEXTVAL INTO ln_SkuId 
			FROM DUAL;

			-- As per PRJ3617,Modified the paramters
			INSERT INTO PRODUCT (SKUID, 
								 BRAND,BRAND_LINE, 
								 SKU,
								 BRANDDESC,
								 MATL_NUM, 
								 SIZESTAMP, 
								 TIRETYPEID, 
								 PSN, 
								 DISCONTINUEDDATE,
								 SPECNUMBER, 
								 SPEEDRATING,
								 SINGLOADINDEX, 
								 DUALLOADINDEX, 
								 TUBELESSYN, 
								 REINFORCEDYN,
								 EXTRALOADYN, 
								 UTQGTREADWEAR, 
								 UTQGTRACTION, 
								 UTQGTEMP,
								 MUDSNOWYN, 
								 RIMDIAMETER, 
								 SERIALDATE,
								 LOADRANGE, 
								 MEARIMWIDTH, 
								 REGROOVABLEIND, 
								 PLANTPRODUCED, 
								 MOSTRECENTTESTDATE,
								 INFORMENUMBER, 
								 FECHADATE, 
								 TREADPATTERN, 
								 SPECIALPROTECTIVEBAND, 
								 NOMINALTIREWIDTH,
								 ASPECTRATIO, 
								 TREADWEARINDICATORS, 
								 NAMEOFMANUFACTURER,
								 DOTSERIALNUMBER, 
								 TPN, 
								 BIASBELTEDRADIAL,
								 SEVEREWEATHERIND)
						 VALUES(ln_SkuId,
								ls_Brand,
								ls_BrandLine,
								ls_SKU,
								NULL,
								LPAD(ps_Matl_Num,18,0),
								ls_SizeStamp,
								ls_TireTypeId,
								ls_PSN,
								DECODE(ls_DiscontinueDate,NULL,NULL,TO_DATE(ls_DiscontinueDate,'MM/DD/YYYY')),
								ls_SpecNumber,
								ls_SpeedRating,
								ls_SingleLoadIndex,
								ls_DualLoadIndex ,
								ls_TubelessSyn,
								ls_ReinforcedYN ,
								ls_ExtraLoadYN,
								ls_UTQGTreadWear,
								ls_UTQGTraction,
								ls_UTQGTemp,
								ls_MudSnowYN,
								ls_RimDiameter,
								ls_SerialDate,
								ls_LoadRange,
								ls_MeaRimWidth,
								ls_RegroovableInd,
								ls_PlantProduced,
								ls_MostRecentDate,
								ls_InformeNumber,
								ls_FechaDate,
								ls_TreadPattern,
								ls_SpecialProtBrand,
								ls_NominalTireWidth,
								ls_AspectRatio,
								ls_TreadWearInd,
								ls_NameOfManufac,
								ls_DotSerialNumber,
								ls_TPN,
								ls_BiasBeltedRadial,
								ls_SevereWeatherInd);
			COMMIT;
		
		END IF;
		
		ProductCertification_Save ( pi_CertificationId, ln_SkuId, ln_ErrorNum);
			
	EXCEPTION
		WHEN le_Done THEN
			COMMIT;
		WHEN OTHERS THEN
			DECLARE
				lsErrorMsg VARCHAR2(300) := SQLERRM;
			BEGIN
				INSERT INTO ICS.LOAD_ERROR(TABLE_LOADED, 
									       KEY_FIELD_DATA_1, 
									       KEY_FIELD_DATA_2, 
									       ERROR_DATE)
								   VALUES('PRODUCT_CERTIFICATE', 
									       TO_CHAR(pi_certificationid) || '-' || TO_CHAR(pi_Skuid),
									       SUBSTR(lsErrorMsg, 1, 50), 
									       SYSDATE);
			END;
	END ProductCountry_Save;

	PROCEDURE productcertification_save(pi_certificationtypeid     IN   NUMBER,
										pi_skuid                   IN   NUMBER,
										pi_error_num            OUT     NUMBER)
	IS
		ls_RequestStatus        VARCHAR2(1):=NULL;
		
		CURSOR lcr_Certs IS
			SELECT CER.CERTIFICATEID,
			(CASE
				---jeseitz 7/27/12 -- fixed case statement -- nom does not get date approved, only date submitted.
				WHEN pce.SKUID IS NOT NULL  AND  (PCE.dateapproved_cegi IS NOT NULL OR CER.CERTDATEAPPROVED  IS NOT NULL) THEN 'A'
				WHEN PCE.SKUID IS NOT NULL and ((PCE.DATESUBMITTED IS NOT NULL OR CER.CERTDATESUBMITTED IS NOT NULL) and PCE.CERTIFICATIONTYPEID = 3) THEN 'A'
				WHEN PCE.SKUID IS NOT NULL and (PCE.DATESUBMITTED IS NOT NULL OR  CER.CERTDATESUBMITTED  IS NOT NULL) THEN 'I'
				ELSE 'R'
				END) AS STATE
			FROM CERTIFICATE CER, 
				 PRODUCTCERTIFICATE PCE
			WHERE PCE.CERTIFICATIONTYPEID = PI_CERTIFICATIONTYPEID
			  AND PCE.SKUID=PI_SKUID
			  AND PCE.CERTIFICATIONTYPEID = CER.CERTIFICATIONTYPEID
			  AND PCE.CERTIFICATEID = CER.CERTIFICATEID
			  AND NVL(UPPER(cer.activestatus),'N') = 'Y'
			ORDER BY STATE;
	BEGIN
	
		pi_error_num := 0;
		
		ls_RequestStatus := 'R';
		
		FOR lcr_CertsRec IN lcr_Certs LOOP
			IF lcr_CertsRec.state = 'A' THEN
				ls_RequestStatus := 'A';
				EXIT;
			END IF;
			
			IF lcr_CertsRec.state =  'I' THEN
				ls_RequestStatus := 'I';
				EXIT;
			END IF;
			
		END LOOP;
		
		INSERT INTO ICS.PRODUCTREQUEST(CERTIFICATIONTYPEID, 
		                              SKUID, 
									  REQUESTSTATUS)
							   VALUES(pi_CertificationTypeId, 
							          pi_Skuid, 
									  ls_RequestStatus);

		COMMIT;
		
	EXCEPTION
		WHEN OTHERS THEN
		
			DECLARE
				lsErrorMsg VARCHAR2(300) := SQLERRM;
			BEGIN
				pi_error_num := 3;
				INSERT INTO ICS.LOAD_ERROR(table_loaded, 
									       key_field_data_1, 
									       key_field_data_2, 
									       error_date)
								    VALUES('PRODUCTREQUEST', 
										   TO_CHAR(pi_certificationtypeid) || '-' || TO_CHAR(pi_Skuid),
										   SUBSTR(lsErrorMsg, 1, 50), SYSDATE);
			END;
	END;

	PROCEDURE ProductRequestCert_Save(ps_DeleteMe IN CHAR,
									  ps_Matl_Num IN VARCHAR2,
									  pi_CertificationId IN INTEGER,
									  pi_SKUId IN INTEGER) 
    AS
		/******************************************************************************
		NAME:       ProductRequestCert_Save
		PURPOSE:
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0         06/02/2016  jeseitz        uses for MarketingNew screen that shows all certification types on one screen.

		******************************************************************************/

		ls_MachineId           VARCHAR2(50):=NULL;
		ls_OperatorId          VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ln_ErrorNum            NUMBER(1):=NULL;
		ln_SkuID               PRODUCT.SKUID%TYPE:=NULL;
		ls_IMarkfamily         PRODUCT_IMARK_FAMILY.FAMILYID%type:=NULL;
		le_Done                EXCEPTION;
		ls_Brand               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BrandLine           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SizeStamp           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TireTypeId          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PSN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DiscontinueDate     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecNumber          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpeedRating         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SingleLoadIndex     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DualLoadIndex       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TubelessSyn         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ReinforcedYN        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ExtraLoadYN         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTreadWear       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTraction        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTemp            CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MudSnowYN           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SevereWeatherInd    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RimDiameter         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SerialDate          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_LoadRange           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MeaRimWidth         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RegroovableInd      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PlantProduced       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MostRecentDate      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_IMark               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_InformeNumber       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_FechaDate           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadPattern        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecialProtBrand    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NominalTireWidth    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_AspectRatio         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadWearInd        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NameOfManufac       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_Family              CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DotSerialNumber     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TPN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BiasBeltedRadial    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SKU                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ProdSpeedRating     PRODUCT.SPEEDRATING%TYPE:=NULL;
		ls_SAPSpeedRating      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		
	BEGIN

		--Make sure material number wasn't just created (in marketing grid, the skuid would get created on the first checked column if there are multiple columns checked)
		ln_skuid := NVL(pi_skuid,0);
		
		IF ln_skuid = 0 THEN
			SELECT NVL(MAX(skuid),0) INTO ln_skuid 
			FROM PRODUCT 
			WHERE MATL_NUM = LPAD(ps_Matl_Num,18,0);
		END IF;

		--need to check if the speedrating on the record passed in is current.
		IF ln_skuid <> 0 AND SUBSTR(ps_Matl_Num,1,8) <> '00000999' THEN -- jeseitz added 4/7/2014 can't check against SAP for tire not in sap
			
			SELECT P.SPEEDRATING INTO ls_ProdSpeedRating FROM PRODUCT P WHERE P.SKUID = ln_SKUID;

			---add exception block here
			BEGIN
			
				SELECT MA.ATTRIB_VALUE INTO ls_SAPSpeedRating 
				FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA 
				WHERE MA.ATTRIB_NAME = 'SPEED_RATING' 
				  AND MA.MATL_NUM = ps_Matl_Num;
				  
			EXCEPTION
				WHEN NO_DATA_FOUND THEN
					ls_SAPSpeedRating := NULL;
			END;

			IF ls_ProdSpeedRating IS NOT NULL AND ls_SAPSpeedRating IS NOT NULL AND ls_ProdSpeedRating <> ls_SAPSpeedRating THEN
			--- re-set ln_skuid = 0 so that a new current product record will be created.
				ln_skuid := 0;
			END IF;
			
		END IF;

		DELETE FROM PRODUCTREQUEST
		WHERE CERTIFICATIONTYPEID = pi_CertificationID
		  AND SKUID = ln_Skuid;
		
		COMMIT;

		--  If only deleting certifications, exit now (not sure this is used)
		IF UPPER(ps_deleteme) = 'Y' THEN
			RAISE le_Done;
		END IF;



		IF ln_skuid = 0 THEN
			--Skuid Does not exist in Product,So we insert into product first

			GetTireCharacteristicsAll(ps_Matl_Num,
			                          ls_Brand,
									  ls_BrandLine,
									  ls_SizeStamp,
									  ls_TireTypeId,
									  ls_PSN,
									  ls_DiscontinueDate,
									  ls_SpecNumber,
									  ls_SpeedRating,
									  ls_SingleLoadIndex,
									  ls_DualLoadIndex ,
									  ls_TubelessSyn,
									  ls_ReinforcedYN ,
									  ls_ExtraLoadYN,
									  ls_UTQGTreadWear,
									  ls_UTQGTraction,
									  ls_UTQGTemp,
									  ls_MudSnowYN,
									  ls_SevereWeatherInd,
									  ls_RimDiameter,
									  ls_SerialDate,
									  ls_LoadRange,
									  ls_MeaRimWidth,
									  ls_RegroovableInd,
									  ls_PlantProduced,
									  ls_MostRecentDate,
									  ls_IMark ,
									  ls_InformeNumber,
									  ls_FechaDate,
									  ls_TreadPattern,
									  ls_SpecialProtBrand,
									  ls_NominalTireWidth,
									  ls_AspectRatio,
									  ls_TreadWearInd,
									  ls_NameOfManufac,
									  ls_Family ,
									  ls_DotSerialNumber,
									  ls_TPN ,
									  ls_BiasBeltedRadial,
									  ls_SKU);

			SELECT  SKUID_SEQ.NEXTVAL INTO ln_SkuId FROM DUAL;

			-- As per PRJ3617,Modified the paramters
			INSERT INTO PRODUCT(SKUID, 
			                    BRAND,BRAND_LINE, 
								SKU,BRANDDESC,
								MATL_NUM, 
								SIZESTAMP, 
								TIRETYPEID, 
								PSN, 
								DISCONTINUEDDATE,
								SPECNUMBER, 
								SPEEDRATING,
								SINGLOADINDEX, 
								DUALLOADINDEX, 
								TUBELESSYN, 
								REINFORCEDYN,
								EXTRALOADYN, 
								UTQGTREADWEAR, 
								UTQGTRACTION, 
								UTQGTEMP,
								MUDSNOWYN, 
								RIMDIAMETER, 
								SERIALDATE,
								LOADRANGE, 
								MEARIMWIDTH, 
								REGROOVABLEIND, 
								PLANTPRODUCED, 
								MOSTRECENTTESTDATE,
								INFORMENUMBER, 
								FECHADATE, 
								TREADPATTERN, 
								SPECIALPROTECTIVEBAND, 
								NOMINALTIREWIDTH,
								ASPECTRATIO, 
								TREADWEARINDICATORS, 
								NAMEOFMANUFACTURER,
								DOTSERIALNUMBER, 
								TPN, 
								BIASBELTEDRADIAL,
								SEVEREWEATHERIND)
						 VALUES(ln_SkuId,
						        ls_Brand,
								ls_BrandLine,
								ls_SKU,
								NULL,
								LPAD(ps_Matl_Num,18,0),
								ls_SizeStamp,
								ls_TireTypeId,
								ls_PSN,
								DECODE(ls_DiscontinueDate,NULL,NULL,
								TO_DATE(ls_DiscontinueDate,'MM/DD/YYYY')),
								ls_SpecNumber,
								ls_SpeedRating,
								ls_SingleLoadIndex,
								ls_DualLoadIndex ,
								ls_TubelessSyn,
								ls_ReinforcedYN ,
								ls_ExtraLoadYN,
								ls_UTQGTreadWear,
								ls_UTQGTraction,
								ls_UTQGTemp,
								ls_MudSnowYN,
								ls_RimDiameter,
								ls_SerialDate,
								ls_LoadRange,
								ls_MeaRimWidth,
								ls_RegroovableInd,
								ls_PlantProduced,
								ls_MostRecentDate,
								ls_InformeNumber,
								ls_FechaDate,
								ls_TreadPattern,
								ls_SpecialProtBrand,
								ls_NominalTireWidth,
								ls_AspectRatio,
								ls_TreadWearInd,
								ls_NameOfManufac,
								ls_DotSerialNumber,
								ls_TPN ,
								ls_BiasBeltedRadial,
								ls_SevereWeatherInd);
			
			COMMIT;

		END IF;
		
		--  Create Appropriate ProductRequest records
		ProductRequest_Save(pi_CertificationId, 
		                    ln_SkuId, 
							ln_ErrorNum);

	EXCEPTION
		WHEN le_Done THEN
			
			COMMIT;
			
		WHEN OTHERS THEN
			
			DECLARE
				lsErrorMsg VARCHAR2(300) := SQLERRM;
			BEGIN
				INSERT INTO ICS.LOAD_ERROR(TABLE_LOADED, 
									       KEY_FIELD_DATA_1, 
									       KEY_FIELD_DATA_2, 
									       ERROR_DATE)
								    VALUES('PRODUCT_REQUEST', 
										   TO_CHAR(pi_certificationid) || '-' || TO_CHAR(pi_Skuid),
										   SUBSTR(lsErrorMsg, 1, 50), 
										   SYSDATE);
		END;
		
	END ProductRequestCert_Save;

	PROCEDURE ProductRequest_Save(pi_certificationtypeid  IN    NUMBER,
								  pi_skuid                IN    NUMBER,
							 	  pi_error_num              OUT NUMBER)
		/******************************************************************************
		NAME:       ProductRequest_Save
		PURPOSE:
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0         06/02/2016  jeseitz        uses for MarketingNew screen that shows all certification types on one screen.

		******************************************************************************/
	IS
	
		ls_RequestStatus        VARCHAR2(1):=NULL;
		
		CURSOR lcr_Certs IS
			SELECT CER.CERTIFICATEID,
				  (Case
					---jeseitz 7/27/12 -- fixed case statement -- nom does not get date approved, only date submitted.
					WHEN pce.SKUID IS NOT NULL AND (PCE.DATEAPPROVED_CEGI IS NOT NULL OR CER.CERTDATEAPPROVED  IS NOT NULL) THEN 'A'
					WHEN PCE.SKUID IS NOT NULL AND ((PCE.DATESUBMITTED IS NOT NULL OR CER.CERTDATESUBMITTED IS NOT NULL) AND PCE.CERTIFICATIONTYPEID = 3) THEN 'A'
					WHEN PCE.SKUID IS NOT NULL AND (PCE.DATESUBMITTED IS NOT NULL OR  CER.CERTDATESUBMITTED  IS NOT NULL) THEN 'I'
					ELSE 'R'
					END ) AS STATE
			FROM CERTIFICATE CER, 
			     PRODUCTCERTIFICATE PCE
			WHERE PCE.CERTIFICATIONTYPEID = PI_CERTIFICATIONTYPEID
			  AND PCE.SKUID=PI_SKUID
			  AND PCE.CERTIFICATIONTYPEID = CER.CERTIFICATIONTYPEID
			  AND PCE.CERTIFICATEID = CER.CERTIFICATEID
			  AND NVL(UPPER(CER.ACTIVESTATUS),'N') = 'Y'
			ORDER BY STATE;
			
		BEGIN
		
			pi_error_num := 0;
			
			ls_RequestStatus := 'R';
			
			FOR lcr_CertsRec IN lcr_Certs LOOP
				
				-- Since request status (state) is ordered
				-- Then the following will provide best result
				-- ls_RequestStatus := lcr_CertsRec.state;
				
				IF lcr_CertsRec.state = 'A' THEN
					ls_RequestStatus := 'A';
					EXIT;
				END IF;
				
				IF lcr_CertsRec.state =  'I' THEN
					ls_RequestStatus := 'I';
					EXIT;
				END IF;
			
			END LOOP;
			
			INSERT INTO PRODUCTREQUEST(SKUID,  
									   CERTIFICATIONTYPEID, 
									   REQUESTSTATUS)
							   VALUES (pi_Skuid, 
									   pi_CertificationTypeId, 
									   ls_RequestStatus);

			COMMIT;
			
	EXCEPTION
		WHEN OTHERS THEN
			
			DECLARE
			lsErrorMsg VARCHAR2(300) := SQLERRM;
			BEGIN
				pi_error_num := 3;
				
				INSERT INTO ICS.LOAD_ERROR(TABLE_LOADED, 
									       KEY_FIELD_DATA_1, 
									       KEY_FIELD_DATA_2, 
									       ERROR_DATE)
								    VALUES('PRODUCT_COUNTRY', 
										   TO_CHAR(pi_certificationtypeid) || '-' || TO_CHAR(pi_Skuid),
										   SUBSTR(lsErrorMsg, 1, 50), 
										   SYSDATE);
				
			END;
			
	END  ProductRequest_Save;

	PROCEDURE GetTireCharacteristicsAll(ps_Matl_Num           IN     PRODUCT.MATL_NUM%TYPE,
										ps_Brand              	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_BrandLine          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SizeStamp          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_TireTypeId         	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_PSN                	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_DiscontinueDate    	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SpecNumber         	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SpeedRating        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SingleLoadIndex    	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_DualLoadIndex      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_TubelessSyn        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_ReinforcedYN       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_ExtraLoadYN        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_UTQGTreadWear      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_UTQGTraction       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_UTQGTemp           	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_MudSnowYN          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SevereWeatherInd   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_RimDiameter        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SerialDate         	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_LoadRange          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_MeaRimWidth        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_RegroovableInd     	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_PlantProduced      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_MostRecentDate     	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_IMark              	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_InformeNumber      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_FechaDate          	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_TreadPattern       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SpecialProtBrand   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_NominalTireWidth   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_AspectRatio        	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_TreadWearInd       	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_NameOfManufac      	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_Family             	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_DotSerialNumber    	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_TPN                	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_BiasBeltedRadial   	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE,
										ps_SKU                	 OUT CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE)
	IS
	/******************************************************************************
	   NAME:       GetTireCharacteristicsAll
	   PURPOSE:    This procedure is used to get all the tire characteristics of the
				   given Matl_Num.
	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        10/01/2012   Harini       1. Created this procedure.
	   1.1        10/06/2012   Harini       1. Added SKU as output to insert for new
											   material
	   1.2        12/03/2012   Harini       1. Retrieving the Aspect ratio by strip off
											  leading zeros
	   1.3        11/04/2013   Harini       1. As per IDEA 2706,Modified GetTireCharacteristicsAll
											procedure -Added ps_SevereWeatherInd as output parameter
											in  procedure and retrieve it and pass the value
											to the declared variable
	   1.4        02/20/2014   Harini       1.The Tread_Pattern_Design field should be retrieved from
											DESIGN_NUM attribute in the CMDR_DATA material_attribute view
	*******************************************************************************/
	BEGIN
	
		SELECT BRAND
			  ,BRAND_LINE
			  ,SIZE_STAMP
			  ,CASE WHEN  PRODUCT_TYPE = 'LIGHT TRUCK TIRE' THEN 3
				    WHEN  PRODUCT_TYPE = 'SPECIALTY TIRE'   THEN 4
					WHEN  PRODUCT_TYPE = 'PASSENGER TIRE'   THEN 1
					WHEN  PRODUCT_TYPE = 'CYCLE TIRE'       THEN 0
					WHEN  PRODUCT_TYPE = 'TRUCK & BUS TIRE' THEN 7
			  END  TIRETYPEID
			  ,PSN
			  ,DE_AUTHORIZATION_DATE
			  ,SPECNUMBER
			  ,SPEED_RATING
			  ,SINGLE_LOAD_INDEX
			  ,DUAL_LOAD_INDEX
			  ,DECODE(TUBE_TYPE,'TUBELESS','Y','N')
			  ,DECODE(NVL(REINFORCEDYN,' '),'RE','Y','N') AS REINFORCEDYN
			  ,UTQG_TREADWEAR
			  ,NVL(UTQG_TRACTION,0)
			  ,UTQG_TEMPERATURE
			  ,DECODE(MUD_SNOW_STAMPING,'M+S','Y','N')  AS MUD_SNOW_STAMPING
			  ,DECODE(SEVERE_WEATHER_SYMBOL,'YES','Y','N') AS SEVERE_WEATHER_SYMBOL
			  ,RIM_DIAMETER
			  ,SERIALDATE
			  ,LOAD_RANGE
			  ,MEASURING_RIM_WIDTH
			  ,DECODE(REGROOVABLE,'YES','Y','N')  AS REGROOVABLE
			  ,PLANT_PRODUCED
			  ,MOST_RECENT_DATE
			  ,I_MARK
			  ,INFORME_NUMBER
			  ,FECHADATE
			  ,TREAD_PATTERN_DESIGN
			  ,SPECIAL_PROTECTIVE_BAND
			  ,NOMINAL_SECTION_WIDTH
			  ,TRIM(LEADING 0 FROM ASPECT_RATIO) AS ASPECT_RATIO
			  ,TREADWEAR_INDICATORS
			  ,NAME_OF_MANUFACTURER
			  ,FAMILY
			  ,DOT_SERIAL_NUMBER
			  ,TECHNICAL_PLATFORM
			  ,DECODE(RMA_TIRE_PLY_CONSTRUCTION,'BIAS-BELTED','BELTED','R','RADIAL','2','BIAS','3','BELTED',RMA_TIRE_PLY_CONSTRUCTION)
			  ,NVL(LEGACY_SKU,SUBSTR( MATL_NUM,9)) LEGACY_SKU --- use last 10 characters of material number if sku is null -- jes - 1/24/13
			INTO
				ps_Brand,
				ps_BrandLine,
				ps_SizeStamp,
				ps_TireTypeId,
				ps_PSN,
				ps_DiscontinueDate,
				ps_SpecNumber,
				ps_SpeedRating,
				ps_SingleLoadIndex,
				ps_DualLoadIndex,
				ps_TubelessSyn,
				ps_ReinforcedYN,
				ps_UTQGTreadWear,
				ps_UTQGTraction,
				ps_UTQGTemp,
				ps_MudSnowYN,
				ps_SevereWeatherInd,
				ps_RimDiameter,
				ps_Serialdate,
				ps_LoadRange,
				ps_MeaRimWidth,
				ps_RegroovableInd,
				ps_PlantProduced,
				ps_MostRecentDate,
				ps_IMark,
				ps_InformeNumber,
				ps_FechaDate,
				ps_TreadPattern,
				ps_SpecialProtBrand,
				ps_NominalTireWidth,
				ps_AspectRatio,
				ps_TreadWearInd,
				ps_NameOfManufac,
				ps_Family,
				ps_DotSerialNumber,
				ps_TPN,
				ps_BiasBeltedRadial,
				ps_SKU
		FROM
		(SELECT LPAD(Matl_Num,18,0) Matl_Num
			   ,MAX(DECODE(ATTRIB_NAME,'BRAND',Attrib_Value))                     AS Brand
			   ,MAX(DECODE(ATTRIB_NAME,'BRAND_LINE',Attrib_Value))                AS Brand_Line
			   ,MAX(DECODE(ATTRIB_NAME,'TIRE_SIZE',Attrib_Value))                 AS Size_Stamp
			   ,MAX(DECODE(ATTRIB_NAME,'PRODUCT_TYPE',Attrib_Value))              AS Product_Type
			   ,MAX(DECODE(ATTRIB_NAME,'NPR_ID',attrib_Value))                    AS PSN  -- NPR_ID added in CMDR 1/23/2013 -- JES
			   ,MAX(DECODE(ATTRIB_NAME,'DE_AUTHORIZATION_DATE',Attrib_Value))     AS De_Authorization_Date
			   ,NULL                                                              AS SpecNumber
			   ,MAX(DECODE(ATTRIB_NAME,'SPEED_RATING',Attrib_Value))              AS Speed_Rating
			   ,MAX(DECODE(ATTRIB_NAME,'STAMPED_SINGLE_LOAD_INDEX',Attrib_Value)) AS Single_Load_Index
			   ,MAX(DECODE(ATTRIB_NAME,'STAMPED_DUAL_LOAD_INDEX',Attrib_Value))   AS Dual_Load_Index
			   ,MAX(DECODE(ATTRIB_NAME,'TUBE_TYPE',Attrib_Value))                 AS Tube_Type
			   ,MAX(DECODE(ATTRIB_NAME,'LOAD_RANGE',Attrib_Value))                AS ReinforcedYN
			   ,MAX(DECODE(ATTRIB_NAME,'UTQG_TREADWEAR',Attrib_Value))            AS UTQG_Treadwear
			   ,MAX(DECODE(ATTRIB_NAME,'UTQG_TRACTION',Attrib_Value))             AS UTQG_Traction
			   ,MAX(DECODE(ATTRIB_NAME,'UTQG_TEMPERATURE',Attrib_Value))          AS UTQG_Temperature
			   ,MAX(DECODE(ATTRIB_NAME,'MUD_SNOW_STAMPING',Attrib_Value))         AS Mud_Snow_Stamping
			   ,MAX(DECODE(ATTRIB_NAME,'SEVERE_WEATHER_SYMBOL',Attrib_Value))     AS Severe_Weather_Symbol
			   ,MAX(DECODE(ATTRIB_NAME,'RIM_DIAMETER',Attrib_Value))              AS Rim_Diameter
			   ,NULL                                                              AS SerialDate
			   ,MAX(DECODE(ATTRIB_NAME,'LOAD_RANGE',Attrib_Value))                AS Load_Range
			   ,MAX(DECODE(ATTRIB_NAME,'MEASURING_RIM_WIDTH',REPLACE(Attrib_Value,'MT','')))       AS Measuring_Rim_Width
			   ,MAX(DECODE(ATTRIB_NAME,'REGROOVABLE',Attrib_Value))               AS Regroovable
			   ,NULL                                                              AS Plant_Produced
			   ,NULL                                                              AS Most_Recent_Date
			   ,MAX(DECODE(ATTRIB_NAME,'I_MARK',Attrib_Value))                    AS I_Mark
			   ,NULL                                                              AS Informe_Number
			   ,NULL                                                              AS FechaDate
			   ,MAX(DECODE(Attrib_Name,'DESIGN_NUM',Attrib_Value))                AS Tread_Pattern_Design
			   ,NULL                                                              AS Special_Protective_Band
			   ,MAX(DECODE(Attrib_Name,'NOMINAL_SECTION_WIDTH',Attrib_Value))     AS Nominal_Section_Width
			   ,MAX(DECODE(Attrib_Name,'ASPECT_RATIO',Attrib_Value))              AS Aspect_Ratio
			   ,NULL                                                              AS Treadwear_Indicators
			   ,NULL                                                              AS Name_Of_Manufacturer
			   ,NULL                                                              AS Family
			   ,NULL                                                              AS Dot_Serial_Number
			   ,MAX(DECODE(Attrib_Name,'TECHNICAL_PLATFORM',Attrib_Value))        AS Technical_Platform
			   ,MAX(DECODE(Attrib_Name,'RMA_TIRE_PLY_CONSTRUCTION',Attrib_Value)) AS Rma_Tire_Ply_Construction
			   ,MAX(DECODE(Attrib_Name,'LEGACY_COOPER_SKU',Attrib_Value))         AS Legacy_SKU
		FROM (SELECT MA.*,
					 DENSE_RANK() OVER(PARTITION BY MA.MATL_NUM, MA.ATTRIB_NAME ORDER BY MA.COUNTER DESC) RK
			  FROM MATERIAL_ATTRIBUTE MA
			  WHERE ATTRIB_NAME IN ('BRAND','BRAND_LINE' ,'TIRE_SIZE','PRODUCT_TYPE','DE_AUTHORIZATION_DATE',
									'SPEED_RATING','STAMPED_SINGLE_LOAD_INDEX','STAMPED_DUAL_LOAD_INDEX','TUBE_TYPE',
									'LOAD_RANGE','UTQG_TREADWEAR','UTQG_TRACTION','UTQG_TEMPERATURE','MUD_SNOW_STAMPING',
									'SEVERE_WEATHER_SYMBOL','RIM_DIAMETER','MEASURING_RIM_WIDTH','REGROOVABLE','I_MARK' ,
									'DESIGN_NUM','NOMINAL_SECTION_WIDTH','ASPECT_RATIO','TECHNICAL_PLATFORM',
									'RMA_TIRE_PLY_CONSTRUCTION','LEGACY_COOPER_SKU','NPR_ID')
				AND MATL_NUM = LPAD(ps_Matl_Num,18,0))
		WHERE RK = 1
		GROUP BY LPAD(MATL_NUM,18,0));
		
		IF NVL(ps_LoadRange, ' ') = 'C' AND ps_TIRETYPEID =  1 THEN
			ps_EXTRALOADYN := 'Y';
		ELSIF NVL (ps_LoadRange, ' ') = 'XL'  THEN
			ps_EXTRALOADYN := 'Y';
		ELSE
			ps_EXTRALOADYN :=  'N';
		END IF;
		
		--Do we need to do this?
		
		ps_Family := BOM_ATTRIBUTES.GET_IMARK_FAMILY(ps_Matl_Num,0);
		
	EXCEPTION
		WHEN OTHERS THEN
	-- Consider logging the error and then re-raise
		RAISE;
		
	END GetTireCharacteristicsAll;

END ICS_CRUD;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.ICS_MAINTENANCE
AS
	/******************************************************************************
	   NAME:       ICS_MAINTENANCE

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	   1.1        10/16/2013  Harini           Created GetFamilies,SaveFamily,Delete Family
											   and  CheckIfFamilyExists procedures
	   1.2        11/19/2013  Guru             Added GET_MATERIALS,COPY_PRODUCT,EDIT_PRODUCT,
											   ATTACH_PRODUCT procedures
	   1.3        11/21/2013  Harini           Added new function to retrieve the most recent ext
											   number "GetCertificateRecentExtNumber"
	   1.4        01/10/2014  Harini           Added new procedure "Refresh_Product"
	******************************************************************************/

	FUNCTION GetCertificateExtNumber(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									 ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE)
	RETURN VARCHAR2;

	FUNCTION CheckIfCertificateExists(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									  ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE,
									  ps_Extension_En          IN  CERTIFICATE.EXTENSION_EN%TYPE)
	RETURN VARCHAR2;

	PROCEDURE SetMostRecentCert(pn_CertificationTypeId IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								ps_CertificateNumber   IN  CERTIFICATE.CERTIFICATENUMBER%TYPE,
								ps_OperatorName        IN  VARCHAR2);

	PROCEDURE GetCertificateMatlCount(pn_CertificationTypeId   IN    CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									  ps_CertificateNumber     IN    CERTIFICATE.CERTIFICATENUMBER%TYPE,
									  ps_Extension_En          IN    CERTIFICATE.EXTENSION_EN%TYPE,
									  pn_Matl_Cnt                OUT NUMBER);

	PROCEDURE RenameCertificate(pn_CertificationTypeId  IN CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								ps_OldCertificateNumber IN CERTIFICATE.CERTIFICATENUMBER%TYPE,
								ps_OldExtension_En      IN CERTIFICATE.EXTENSION_EN%TYPE,
								Ps_NewCertificateNumber IN CERTIFICATE.CERTIFICATENUMBER%TYPE,
								Ps_NewExtension_En      IN CERTIFICATE.EXTENSION_EN%TYPE,
								ps_OperatorName         IN VARCHAR2);

	PROCEDURE DeleteProductCountry(pn_SkuId                 IN  PRODUCTCERTIFICATE.SKUID%TYPE,
								   pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								   ps_OperatorName          IN  VARCHAR2);

	PROCEDURE DeleteCertReferences(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								   pn_CertificateId         IN  CERTIFICATE.CERTIFICATEID%TYPE,
								   ps_Table_Name            IN  VARCHAR2,
								   ps_OperatorName          IN  VARCHAR2);

	PROCEDURE DeleteCertificate(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE,
								ps_Extension_En          IN  CERTIFICATE.EXTENSION_EN%TYPE,
								ps_OperatorName          IN  VARCHAR2);

	PROCEDURE GetCertificateMatls(pn_CertificationTypeId   IN    CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								  ps_CertificateNumber     IN    CERTIFICATE.CERTIFICATENUMBER%TYPE,
								  ps_Extension_En          IN    CERTIFICATE.EXTENSION_EN%TYPE,
								  Pc_cursor                  OUT SYS_REFCURSOR);

	PROCEDURE DetachCertificate(pn_SkuId          IN  PRODUCT.SKUID%TYPE,
								pn_CertificateId  IN  CERTIFICATE.CERTIFICATEID%TYPE);

	PROCEDURE MoveCertificate(pn_CertificationTypeId   IN CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
							  ps_NewCertificateNumber  IN CERTIFICATE.CERTIFICATENUMBER%TYPE,
							  ps_NewExtension_En       IN CERTIFICATE.EXTENSION_EN%TYPE,
							  pn_SkuId                 IN PRODUCT.SKUID%TYPE,
							  pn_CertificateId         IN CERTIFICATE.CERTIFICATEID%TYPE,
							  ps_OperatorName          IN VARCHAR2);

	PROCEDURE GetDuplicateCert(ps_Matl_Num    IN    PRODUCT.MATL_NUM%TYPE,
							   ps_SpeedRating IN    PRODUCT.SPEEDRATING%TYPE,
							   ps_Result        OUT SYS_REFCURSOR) ;

	PROCEDURE RemoveDuplicateCert(pn_SkuId IN PRODUCT.SKUID%TYPE);

	PROCEDURE GetFamilies(pn_Certificateid IN     IMARK_FAMILY.CERTIFICATEID%type,
						  ps_Result          OUT  SYS_REFCURSOR);

	PROCEDURE SaveFamily(pn_Certificateid      IN IMARK_FAMILY.CERTIFICATEID%TYPE,
						 pn_FamilyID           IN IMARK_FAMILY.FAMILY_ID%TYPE,
						 ps_FamilyCode         IN IMARK_FAMILY.FAMILY_CODE%TYPE,
						 ps_FamilyDesc         IN IMARK_FAMILY.FAMILY_DESC%TYPE,
						 ps_ApplicationCat     IN IMARK_FAMILY.APPLICATION_CAT%TYPE,
						 ps_ConstructionType   IN IMARK_FAMILY.CONSTRUCTION_TYPE%TYPE,
						 ps_StructureType      IN IMARK_FAMILY.STRUCTURE_TYPE%TYPE,
						 ps_MountingType       IN IMARK_FAMILY.MOUNTING_TYPE%TYPE,
						 ps_AspectRatioCat     IN IMARK_FAMILY.ASPECT_RATIO_CAT%TYPE,
						 ps_SpeedRatingCat     IN IMARK_FAMILY.SPEED_RATING_CAT%TYPE,
						 ps_LoadIndexCat       IN IMARK_FAMILY.LOAD_INDEX_CAT%TYPE,
						 ps_UserName           IN IMARK_FAMILY.CREATEDBY%TYPE);

	PROCEDURE DeleteFamily(pn_Certificateid     IN IMARK_FAMILY.CERTIFICATEID%TYPE,
						   pn_FamilyID          IN IMARK_FAMILY.FAMILY_ID%TYPE);
										   
	 PROCEDURE CheckIfFamilyExists(pn_Certificateid     IN     IMARK_FAMILY.CERTIFICATEID%TYPE,
								   pn_FamilyID          IN     IMARK_FAMILY.FAMILY_ID%TYPE,
								   ps_Family_Exist        OUT  VARCHAR2,
								   ps_Family_Desc         OUT  VARCHAR2);
									
	PROCEDURE GET_MATERIALS(ps_Matl_num      IN     PRODUCT.MATL_NUM%TYPE,
							 pc_Cursor         OUT  SYS_REFCURSOR);

	PROCEDURE COPY_PRODUCT(ps_MATL_NUM       IN   PRODUCT.MATL_NUM%TYPE);

	PROCEDURE EDIT_PRODUCT(pn_SKUID            IN   PRODUCT.SKUID%TYPE,
						   ps_speedrating      IN   PRODUCT.SPEEDRATING%TYPE);

	PROCEDURE ATTACH_PRODUCT(pn_skuid                 IN     PRODUCT.SKUID%TYPE,
							 ps_certificateNumber     IN     CERTIFICATE.CERTIFICATENUMBER%TYPE,
							 ps_Extension_EN          IN     CERTIFICATE.EXTENSION_EN%TYPE,
							 pn_certificationtypeid   IN     CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
							 ps_ErrorMsg                OUT  VARCHAR2);

	FUNCTION GetCertificateRecentExtNumber(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
										   ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE)
	RETURN VARCHAR2;

	PROCEDURE Refresh_Product(ps_matl_num      IN     PRODUCT.MATL_NUM%TYPE,
							  pn_ErrorNum        OUT  NUMBER,
							  ps_ErrorMsg        OUT  VARCHAR2);

	PROCEDURE DeleteProductImarkFamily(pn_SkuId           IN  PRODUCTCERTIFICATE.SKUID%TYPE,
							           pn_CertificateId   IN  CERTIFICATE.CERTIFICATEID%TYPE);
									   
	PROCEDURE GET_PRODUCT_INFO (ps_matl_num		IN     PRODUCT.MATL_NUM%TYPE,
								pc_product   	  OUT  SYS_REFCURSOR,
								pn_error_num 	  OUT  NUMBER,
								ps_error_desc 	  OUT  VARCHAR2);

END ICS_MAINTENANCE;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.ICS_MAINTENANCE
AS

	FUNCTION GetCertificateExtNumber(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									 ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE)
	RETURN VARCHAR2
	AS
	/******************************************************************************
	   NAME:       GetCertificateExtNumber
	   PURPOSE:    Returns certificate extension  for given Certificate number and certificate type

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/
		
		ls_Extension_En CERTIFICATE.EXTENSION_EN%TYPE:=NULL;
		
	BEGIN

		SELECT MAX(TO_NUMBER(EXTENSION_EN))
			INTO ls_Extension_En
		FROM CERTIFICATE
		WHERE CERTIFICATIONTYPEID    = Pn_CertificationTypeId
		AND UPPER(CERTIFICATENUMBER) = UPPER(Ps_CertificateNumber);

		RETURN NVL(Ls_Extension_En,'0');

	EXCEPTION
		WHEN No_Data_Found THEN
			ls_Extension_En := '0';
			RETURN Ls_Extension_En;
	END Getcertificateextnumber;

	FUNCTION CheckIfCertificateExists(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									  ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE,
									  ps_Extension_En          IN  CERTIFICATE.EXTENSION_EN%TYPE)
	RETURN VARCHAR2
	AS
	/******************************************************************************
	   NAME:       CheckIfCertificateExists
	   PURPOSE:    Certificate exist return 'Y' else 'N'

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/
	
		ln_CertificateExist VARCHAR2(1):=NULL;
		ln_CertificateId    NUMBER     :=NULL;

	BEGIN

		IF (pn_CertificationTypeId = 1 OR pn_CertificationTypeId = 6) AND ps_Extension_En IS NOT NULL THEN
		
			-- Get the CertificateId
			ICS_COMMON_FUNCTIONS.GetCertificateIdByNumber(ps_CertificateNumber,
														  pn_CertificationTypeId,
														  ps_Extension_En,
														  ln_CertificateId);

			ln_CertificateExist := CASE WHEN ln_CertificateId = 0 THEN 'N'
										ELSE 'Y' END;
								   RETURN ln_CertificateExist;

		ELSE
		
			-- Check whether Certificate exist or not
			ln_CertificateExist :=ICS_COMMON_FUNCTIONS.CheckIfCertificateExists(ps_CertificateNumber,
																				pn_CertificationTypeId);
			RETURN ln_CertificateExist;
			
		END IF;

	END CheckIfCertificateExists;


	PROCEDURE SetMostRecentCert(pn_CertificationTypeId IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								ps_CertificateNumber   IN  CERTIFICATE.CERTIFICATENUMBER%TYPE,
								ps_OperatorName        IN  VARCHAR2)
	AS
	/******************************************************************************
	   NAME:       SetMostRecentCert
	   PURPOSE:    Update MostRecentCert to  Y when maximum of extension and given
				   certificatenumber and certificationtypeid else N

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/
	
	ln_Max_Ext    NUMBER:=NULL;
	ls_ErrorMsg   VARCHAR2(4000):=NULL;
	ls_OperatorId VARCHAR2(50) := 'ICSDEV';

	BEGIN

		IF ps_OperatorName IS NOT NULL THEN
			ls_OperatorId := ps_OperatorName;
		END IF;

		SELECT NVL(MAX(TO_NUMBER(EXTENSION_EN)),0)
			INTO ln_Max_Ext
		FROM CERTIFICATE
		WHERE UPPER(CERTIFICATENUMBER) = UPPER(Ps_CertificateNumber)
		  AND CERTIFICATIONTYPEID = pn_CertificationTypeId;

		UPDATE CERTIFICATE
		SET MOSTRECENTCERT =(CASE WHEN Extension_En = ln_Max_Ext THEN 'Y' ELSE 'N' END),
			MODIFIEDBY     = ls_OperatorId,
			MODIFIEDON     = SYSDATE
		WHERE UPPER(CERTIFICATENUMBER) = UPPER(Ps_CertificateNumber)
		  AND CERTIFICATIONTYPEID      = pn_CertificationTypeId ;

	EXCEPTION
		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  SQLERRM || '- SetMostRecentCert. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.SetMostRecentCert',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR(-20007,ls_ErrorMsg);

	END SetMostRecentCert;

	PROCEDURE GetCertificateMatlCount(pn_CertificationTypeId   IN    CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									  ps_CertificateNumber     IN    CERTIFICATE.CERTIFICATENUMBER%TYPE,
									  ps_Extension_En          IN    CERTIFICATE.EXTENSION_EN%TYPE,
									  pn_Matl_Cnt                OUT NUMBER)
	AS

	/******************************************************************************
	   NAME:       GetCertificateMatlCount
	   PURPOSE:    Returns material count for given Certificate number and certificate type

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/

		Ex_CertificateNotExist EXCEPTION;
		ln_CertificateExist    VARCHAR2(1):=NULL;
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ls_Extension_En        CERTIFICATE.EXTENSION_EN%TYPE:=NULL;

	BEGIN
		--Checks given certificate exist or not
		ln_CertificateExist := ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pn_CertificationTypeId,
																		ps_CertificateNumber,
																		ps_Extension_En);

		IF UPPER(ln_CertificateExist) = 'Y' THEN

			IF (pn_CertificationTypeId = 1 OR pn_CertificationTypeId = 6) THEN

				IF ps_Extension_En IS NULL THEN
					-- Get the Extension number
					ls_Extension_En := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pn_CertificationTypeId,
																			   ps_CertificateNumber);
				ELSE
					ls_Extension_En := ps_Extension_En;
				END IF;

				-- Pull Material count for given certificate
				SELECT COUNT(DISTINCT P.MATL_NUM)
					INTO pn_Matl_Cnt
				FROM CERTIFICATE C,
					 PRODUCT P,
					 PRODUCTCERTIFICATE PC
				WHERE C.CERTIFICATEID = PC.CERTIFICATEID
				  AND PC.SKUID = P.SKUID
				  AND UPPER(C.CERTIFICATENUMBER) = UPPER(ps_CertificateNumber)
				  AND C.CERTIFICATIONTYPEID = pn_CertificationTypeId
				  AND C.EXTENSION_EN = ls_Extension_En;

			ELSE
				-- Pull Material count for given certificate
				SELECT COUNT(DISTINCT P.MATL_NUM)
					INTO pn_Matl_Cnt
				FROM CERTIFICATE C,
				     PRODUCT P,
				     PRODUCTCERTIFICATE PC
				WHERE C.CERTIFICATEID = PC.CERTIFICATEID
				  AND PC.SKUID = P.SKUID
				  AND UPPER(C.CERTIFICATENUMBER) = UPPER(ps_CertificateNumber)
				  AND C.CERTIFICATIONTYPEID = pn_CertificationTypeId ;
			
			END IF;

		ELSE
			-- Raise an exception when certificate not found
			RAISE Ex_CertificateNotExist;
		
		END IF;
		
	EXCEPTION
		WHEN Ex_CertificateNotExist THEN
		
			ls_ErrorMsg:=  SQLERRM || '- GetCertificateMatlCount. Certificate Number - '||ps_CertificateNumber||' not exist.';

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- GetCertificateMatlCount. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.GetCertificateMatlCount',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	END GetCertificateMatlCount;


	PROCEDURE RenameCertificate(pn_CertificationTypeId  IN CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								ps_OldCertificateNumber IN CERTIFICATE.CERTIFICATENUMBER%TYPE,
								ps_OldExtension_En      IN CERTIFICATE.EXTENSION_EN%TYPE,
								Ps_NewCertificateNumber IN CERTIFICATE.CERTIFICATENUMBER%TYPE,
								Ps_NewExtension_En      IN CERTIFICATE.EXTENSION_EN%TYPE,
								ps_OperatorName         IN VARCHAR2)
	AS
	/******************************************************************************
	   NAME:       RenameCertificate
	   PURPOSE:    Rename Old Certificate Number to New certificate number

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/

		ex_CertificateIfExist  EXCEPTION;
		ls_OperatorId          VARCHAR2(50) := 'ICSDEV';
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ln_NewCertificateExist VARCHAR2(1):=NULL;
		ln_OldCertificateExist VARCHAR2(1):=NULL;
		ls_OldExtension_En     CERTIFICATE.EXTENSION_EN%TYPE:=NULL;
		ls_NewExtension_En     CERTIFICATE.EXTENSION_EN%TYPE:=NULL;

	BEGIN

			IF ps_OperatorName IS NOT NULL THEN
				ls_OperatorId := ps_OperatorName;
			END IF;

			-- Check old certificate number exist ot not
			ln_OldCertificateExist:=ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pn_CertificationTypeId,
																			  ps_OldCertificateNumber,
																			  ps_OldExtension_En);
			-- Check New certificate number exist ot not
			ln_NewCertificateExist:=ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pn_CertificationTypeId,
																			 ps_NewCertificateNumber,
																			 ps_NewExtension_En);

			IF UPPER(ln_OldCertificateExist) ='Y' AND UPPER(ln_NewCertificateExist) = 'N' THEN
				
				IF (pn_CertificationTypeId = 1 OR pn_CertificationTypeId = 6) THEN

					IF ps_OldExtension_En IS NULL THEN
						-- Get extension number for old certificate
						ls_OldExtension_En := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pn_CertificationTypeId,
																					  ps_OldCertificateNumber);
					ELSE
						ls_OldExtension_En := ps_OldExtension_En;
					END IF;

					IF ps_NewExtension_En IS NULL THEN
						-- Get extension number for new certificate
						ls_NewExtension_En := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pn_CertificationTypeId,
																					  ps_NewCertificateNumber);
					ELSE
						ls_NewExtension_En := Ps_NewExtension_En;
					END IF;

					-- Update old certificate number to new certificate number with extension
					UPDATE Certificate
						SET CERTIFICATENUMBER = Ps_NewCertificateNumber,
							EXTENSION_EN      = ls_NewExtension_En,
							MODIFIEDBY        = ls_OperatorId,
							MODIFIEDON        = SYSDATE
					WHERE UPPER(CertificateNumber) = UPPER(ps_OldCertificateNumber)
					  AND CERTIFICATIONTYPEID =pn_CertificationTypeId
					  AND EXTENSION_EN = ls_OldExtension_En ;

				ELSE
					-- Update old certificate number to new certificate number without extension
					UPDATE CERTIFICATE
					SET CERTIFICATENUMBER = Ps_NewCertificateNumber,
						MODIFIEDBY        = ls_OperatorId,
						MODIFIEDON        = SYSDATE
					WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_OldCertificateNumber)
					AND CERTIFICATIONTYPEID =pn_CertificationTypeId;
					
				END IF;

				-- Update the MostRecentCert column in Certificate table
				ICS_MAINTENANCE.SETMOSTRECENTCERT(pn_CertificationTypeId,
												  Ps_NewCertificateNumber,
												  ls_OperatorId);

				-- Update the MostRecentCert column in  Certificate table for old certificate in case we deleted a higher extension and there
				-- are still lower ones -- jeseitz 3/15/13
				ICS_MAINTENANCE.SETMOSTRECENTCERT(pn_CertificationTypeId,
												  ps_OldCertificateNumber,
												  ls_OperatorId);
			ELSE
				RAISE  ex_CertificateIfExist;
			END IF;
			
			COMMIT;
	EXCEPTION
		WHEN ex_CertificateIfExist THEN
			--Insert into excpetion record
			 ls_errormsg:=  SQLERRM || '- RenameCertificate. New Certificate Number - '||Ps_NewCertificateNumber||' exist.'
									||' OR Old Certificate Number - '||ps_OldCertificateNumber||' not exist'      ;

			 --Insert record into exception table
			 APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													   ad_operatorid    => ls_operatorId,
													   ad_daterecorded  => SYSDATE,
													   as_processname   => 'ICS_MAINTENANCE.RenameCertificate',
													   ax_recorddata    => 'pn_CertificationTypeId - '||pn_CertificationTypeId||',ps_OldCertificateNumber - '||ps_OldCertificateNumber
																			||', Ps_NewCertificateNumber - '||Ps_NewCertificateNumber||',Ps_NewExtension_En - '||Ps_NewExtension_En
																			||',ps_OldExtension_En - '||ps_OldExtension_En,
													   as_messagecode   => TO_CHAR(SQLCODE),
													   as_message       => ls_ErrorMsg);

			  RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- RenameCertificate. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => ls_operatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.RenameCertificate',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END Renamecertificate;

	PROCEDURE DeleteProductCountry(pn_SkuId                 IN  PRODUCTCERTIFICATE.SKUID%TYPE,
								   pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								   ps_OperatorName          IN  VARCHAR2)
	AS
	/******************************************************************************
	   NAME:       DeleteProductCountry
	   PURPOSE:    Delete records from productreqeust table for given skuid and certification type

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
					06/20/2016 jeseitz            changed to use productrequest table
	******************************************************************************/
	 CURSOR lcr_Certs IS
			SELECT CER.CERTIFICATEID,
				   (CASE
					---JESEITZ 7/27/12 -- FIXED CASE STATEMENT -- NOM DOES NOT GET DATE APPROVED, ONLY DATE SUBMITTED.
					WHEN PCE.SKUID  IS NOT NULL AND  (PCE.DATEAPPROVED_CEGI IS NOT NULL OR CER.CERTDATEAPPROVED  IS NOT NULL)
						THEN 'A'
					WHEN PCE.SKUID IS NOT NULL AND ((PCE.DATESUBMITTED IS NOT NULL OR CER.CERTDATESUBMITTED IS NOT NULL) AND PCE.CERTIFICATIONTYPEID = 3)
						THEN 'A'
					WHEN PCE.SKUID IS NOT NULL AND (PCE.DATESUBMITTED IS NOT NULL OR  CER.CERTDATESUBMITTED  IS NOT NULL)
						THEN 'I'
					ELSE 'R'
					END) AS STATE
			  FROM CERTIFICATE CER,
				   PRODUCTCERTIFICATE PCE
			  WHERE PCE.CERTIFICATIONTYPEID = Pn_Certificationtypeid
				AND PCE.SKUID = pn_SkuId
				AND PCE.CERTIFICATIONTYPEID = CER.CERTIFICATIONTYPEID
				AND PCE.CERTIFICATEID = CER.CERTIFICATEID
				AND NVL(UPPER(CER.ACTIVESTATUS),'N') = 'Y'
			ORDER BY STATE;

	 ln_Cnt    NUMBER:=NULL;
	
	  LS_REQUESTSTATUS PRODUCTREQUEST.REQUESTSTATUS%TYPE:=NULL;

	BEGIN

		DELETE FROM PRODUCTREQUEST
		WHERE SKUID = pn_SkuId
		  AND CERTIFICATIONTYPEID = Pn_Certificationtypeid;
		
		---now check if that material is on other certificates of the same type.
		---keep a count of records in cursor - we just need to know if there are any.
		
		ln_Cnt := 0;
		
		FOR lcr_CertsRec IN lcr_Certs LOOP
			
			-- Since request status (state) is ordered
			-- Then the following will provide best result
			-- ls_RequestStatus := lcr_CertsRec.state;
			IF lcr_CertsRec.STATE = 'A' THEN
				ls_RequestStatus := 'A';
				ln_Cnt := ln_Cnt + 1;
				EXIT;
			END IF;
			
			IF lcr_CertsRec.STATE =  'I' THEN
				ls_RequestStatus := 'I';
				ln_Cnt := ln_Cnt + 1;
				EXIT;
			END IF;

		END LOOP;
		
		IF ln_Cnt > 0 THEN

			INSERT INTO PRODUCTREQUEST(CERTIFICATIONTYPEID,
									   SKUID, 
									   REQUESTSTATUS,
			                           CREATEDBY, 
									   CREATEDON, 
									   MODIFIEDBY, 
									   MODIFIEDON)
								VALUES(pn_Certificationtypeid, 
								       pn_SkuId,  
									   ls_RequestStatus,
			                           ps_OperatorName,  
									   SYSDATE, 
									   ps_OperatorName,
									   SYSDATE);

		END IF;

	END DeleteProductCountry;


	PROCEDURE DeleteCertReferences(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								   pn_CertificateId         IN  CERTIFICATE.CERTIFICATEID%TYPE,
								   ps_Table_Name            IN  VARCHAR2,
								   ps_OperatorName          IN  VARCHAR2)
	AS
	/******************************************************************************
	   NAME:       DeleteCertReferences
	   PURPOSE:    Delete records from HDR or Details or Certificate or ProductCertificate tables
				   based on table name, certificate id and type

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/
	
		ls_Sql          VARCHAR2(2000):=NULL;
		ls_HdrColName   VARCHAR2(30)  :=NULL;
		ln_HdrColValue  NUMBER        :=NULL;
		ls_DtlTableName VARCHAR2(30)  :=NULL;
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
		ln_Count        NUMBER := 0;

		CURSOR lcr_SkuId IS
			SELECT SKUID
			FROM PRODUCTCERTIFICATE
			WHERE CERTIFICATIONTYPEID =pn_CertificationTypeId
			  AND CERTIFICATEID = pn_CertificateId;

	BEGIN

		IF UPPER(ps_Table_Name) <> 'CERTIFICATE' AND UPPER(ps_Table_Name) <> 'PRODUCTCERTIFICATE' THEN

			IF UPPER(ps_Table_Name) = 'ENDURANCEHDR' THEN
				ls_HdrColName   := 'ENDURANCEID';
				ls_DtlTableName := 'ENDURANCEDTL';
			ELSIF UPPER(ps_Table_Name) = 'MEASUREHDR' THEN
				ls_HdrColName   := 'MEASUREID';
				ls_DtlTableName := 'MEASUREDTL';
			ELSIF UPPER(ps_Table_Name) = 'PLUNGERHDR' THEN
				ls_HdrColName   := 'PLUNGERID';
				ls_DtlTableName := 'PLUNGERDTL';
			ELSIF UPPER(ps_Table_Name) = 'SOUNDHDR' THEN
				ls_HdrColName   := 'SOUNDID';
				ls_DtlTableName := 'SOUNDDETAIL';
			ELSIF UPPER(ps_Table_Name) = 'TREADWEARHDR' THEN
				ls_HdrColName   := 'TREADWEARID';
				ls_DtlTableName := 'TREADWEARDTL';
			ELSIF UPPER(ps_Table_Name) = 'WETGRIPHDR' THEN
				ls_HdrColName   := 'WETGRIPID';
				ls_DtlTableName := 'WETGRIPHDR';
			ELSIF UPPER(ps_Table_Name) = 'HIGHSPEEDHDR' THEN
				ls_HdrColName   := 'HIGHSPEEDID';
				ls_DtlTableName := 'HIGHSPEEDDTL';
			ELSIF UPPER(ps_Table_Name) = 'BEADUNSEATHDR' THEN
				ls_HdrColName   := 'BEADUNSEATID';
				ls_DtlTableName := 'BEADUNSEATDTL';
			END IF;

			ls_Sql := '  SELECT '||ls_HdrColName||
			'    FROM '||ps_Table_Name||
			'   WHERE CERTIFICATIONTYPEID = '||pn_CertificationTypeId||
			'     AND CERTIFICATEID = '||pn_CertificateId;

			BEGIN
				EXECUTE IMMEDIATE ls_Sql INTO ln_HdrColValue;
			EXCEPTION
				WHEN NO_DATA_FOUND THEN
				ln_HdrColValue := NULL;
			END;

			IF ln_HdrColValue IS NOT NULL THEN

				ls_Sql := '  DELETE FROM '||ls_DtlTableName||'        WHERE '||ls_HdrColName||' = '||ln_HdrColValue;

				EXECUTE IMMEDIATE ls_Sql;

				ls_Sql := '  DELETE FROM '||ps_Table_Name||'        WHERE '||ls_HdrColName||' = '||ln_HdrColValue;
				
				EXECUTE IMMEDIATE ls_Sql;

			END IF;

		ELSE

			IF UPPER(ps_Table_Name) = 'PRODUCTCERTIFICATE' THEN

				FOR lcr_SkuIdRec IN lcr_SkuId LOOP
				
					BEGIN
						ICS_MAINTENANCE.DELETEPRODUCTCOUNTRY (lcr_SkuIdRec.SkuId,
															  pn_CertificationTypeId,
															  ps_OperatorName); --deletes from productrequest
					EXCEPTION
						WHEN OTHERS THEN
							ls_ErrorMsg:=  SQLERRM || '- DeleteCertReferences. An error have ocurred.(when others)';

							--Insert record into exception table
							APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => NULL,
																	  ad_OperatorId    => ps_OperatorName,
																	  ad_DateRecorded  => SYSDATE,
																	  as_ProcessName   => 'ICS_MAINTENANCE.DeleteCertReferences',
																	  ax_RecordData    => 'An error have ocurred.(when others)'||', SkuId - '||
																	  lcr_SkuIdRec.SkuId||',CertificationTypeId'||pn_CertificationTypeId,
																	  as_MessageCode   => TO_CHAR(SQLCODE),
																	  as_Message       => ls_ErrorMsg);

							ln_Count := ln_Count + 1 ;
					END;
					
					IF pn_CertificationTypeId = 4 THEN -- Imark certificate -- jeseitz 4/14/2016
					
						BEGIN
							ICS_MAINTENANCE.DeleteProductImarkFamily (lcr_SkuIdRec.SkuId,pn_CertificateId);
						EXCEPTION
							WHEN OTHERS THEN
							
								ls_ErrorMsg:=  SQLERRM || '- DeleteCertReferences. An error has ocurred.(when others)';

								--Insert record into exception table
								APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => NULL,
																		  ad_OperatorId    => ps_OperatorName,
																		  ad_DateRecorded  => SYSDATE,
																		  as_ProcessName   => 'ICS_MAINTENANCE.DeleteCertReferences',
																		  ax_RecordData    => 'An error have ocurred.(when others) DeleteProductImarkFamily'||', SkuId = '||
																		  lcr_SkuIdRec.SkuId||', CertificateId='||pn_CertificateId,
																		  as_MessageCode   => TO_CHAR(SQLCODE),
																		  as_Message       => ls_ErrorMsg);

								ln_Count := ln_Count + 1 ;
						END;
						
					END IF;
				
				END LOOP;

				IF ln_Count > 0 THEN
					RAISE_APPLICATION_ERROR (-20001,'There is an exception while executing sub procedures - Ics_Maintenance.Deleteproductcountry or DeleteProductImarkFamily');
				END IF;

			END IF;
			
			ls_Sql := ' DELETE FROM '|| ps_Table_Name ||' WHERE CertificateId = '||  pn_CertificateId;

			EXECUTE IMMEDIATE ls_Sql;

		END IF;

	END DeleteCertReferences;


	PROCEDURE DeleteCertificate(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE,
								ps_Extension_En          IN  CERTIFICATE.EXTENSION_EN%TYPE,
								ps_OperatorName          IN  VARCHAR2)
	/******************************************************************************
	   NAME:       DeleteCertificate
	   PURPOSE:    Delete records from Certificate table

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/
	AS

		ex_CertificateNotExist EXCEPTION;
		ls_OperatorId          VARCHAR2(50) := 'ICSDEV';
		ls_CertificateId       CERTIFICATE.CERTIFICATEID%TYPE:=NULL;
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ls_Extension_En        CERTIFICATE.EXTENSION_EN%TYPE:=NULL;
		ln_CertificateExist    VARCHAR2(1):=NULL;

	BEGIN

		IF ps_OperatorName IS NOT NULL THEN
			ls_OperatorId := ps_OperatorName;
		END IF;

		-- Checks whether certificate exist or not
		ln_CertificateExist := ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pn_CertificationTypeId,
																		ps_CertificateNumber,
																		ps_Extension_En);
		IF UPPER(ln_CertificateExist)='Y' THEN

			IF (pn_CertificationTypeId = 1 OR pn_CertificationTypeId = 6) THEN

				IF ps_Extension_En IS NULL THEN
				
					ls_Extension_En:=ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pn_CertificationTypeId,
																			 ps_CertificateNumber);
				ELSE
				
					ls_Extension_En := ps_Extension_En ;
					
				END IF;
				
				-- Get the certificate Id
				SELECT CertificateId
					INTO ls_CertificateId
				FROM CERTIFICATE
				WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_CertificateNumber)
				  AND CERTIFICATIONTYPEID = pn_CertificationTypeId
				  AND EXTENSION_EN =  ls_Extension_En;

			ELSE

				ls_Extension_En :=   NULL;
				-- Get the certificate Id
				SELECT CERTIFICATEID
					INTO ls_CertificateId
				FROM CERTIFICATE
				WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_CertificateNumber)
				  AND CERTIFICATIONTYPEID = pn_CertificationTypeId;

			END IF;

			-- Deleting Header and detail Records
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'ENDURANCEHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'MEASUREHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'PLUNGERHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'SOUNDHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'TREADWEARHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'WETGRIPHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'HIGHSPEEDHDR',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'BEADUNSEATHDR',ls_OperatorId);
			-- Deleting ProductCertificate and Certificate Records
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'PRODUCTCERTIFICATE',ls_OperatorId);
			ICS_MAINTENANCE.DELETECERTREFERENCES (pn_CertificationTypeId,ls_CertificateId,'CERTIFICATE',ls_OperatorId);

			-- Update the MostRecentCert column in Certificate table
			ICS_MAINTENANCE.SETMOSTRECENTCERT(pn_CertificationTypeId,ps_CertificateNumber,ls_OperatorId);

			COMMIT;

		ELSE
			RAISE ex_CertificateNotExist;
		END IF;

		EXCEPTION

		WHEN ex_CertificateNotExist THEN
		
			ls_ErrorMsg:=  SQLERRM || '- DeleteCertificate. Certificate - '||ps_CertificateNumber||' Not Found';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.DeleteCertificate',
													  ax_recorddata    => 'ps_CertificateNumber - '||ps_CertificateNumber||',pn_CertificationTypeId - '
													  ||pn_certificationTypeId||',ps_Extension_En -'||ps_Extension_En ,
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  SQLERRM || '- DeleteCertificate. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
												      ad_operatorid    => ls_OperatorId,
												      ad_daterecorded  => SYSDATE,
												      as_processname   => 'ICS_MAINTENANCE.DeleteCertificate',
												      ax_recorddata    => 'An error have ocurred.(when others)',
												      as_messagecode   => TO_CHAR(SQLCODE),
												      as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END DeleteCertificate;


	PROCEDURE GetCertificateMatls(pn_CertificationTypeId   IN    CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
								  ps_CertificateNumber     IN    CERTIFICATE.CERTIFICATENUMBER%TYPE,
								  ps_Extension_En          IN    CERTIFICATE.EXTENSION_EN%TYPE,
								  Pc_cursor                  OUT SYS_REFCURSOR)
	AS

	/******************************************************************************
	   NAME:       GetCertificateMatls
	   PURPOSE:    Returns material for given Certificate number and certificate type

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/

		ex_CertificateNotExist EXCEPTION;

		ln_CertificateExist    VARCHAR2(1):=NULL;
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ln_CertificateId       NUMBER:=NULL;
		ls_Extension_En        CERTIFICATE.EXTENSION_EN%TYPE:=NULL;

	BEGIN
		-- Checks whether certificate exist or not
		ln_CertificateExist := ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pn_CertificationTypeId,
																		ps_CertificateNumber,
																		ps_Extension_En);

		IF UPPER(ln_CertificateExist) = 'Y' THEN

			IF (pn_CertificationTypeId = 1 OR pn_CertificationTypeId = 6) THEN

				IF ps_Extension_En IS NULL THEN
					-- Get the extension for certificate
					ls_Extension_En := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pn_CertificationTypeId,ps_CertificateNumber);
				ELSE
					ls_Extension_En := ps_Extension_En;
				END IF;

				-- Get the Certificate id
				ICS_COMMON_FUNCTIONS.GETCERTIFICATEIDBYNUMBER(ps_CertificateNumber,
															  pn_CertificationTypeId,
															  ls_Extension_En,
															  ln_CertificateId);

				-- Returns material for the certificate
				OPEN pc_Cursor FOR
					SELECT  T.CERTIFICATIONTYPENAME,
							C.CERTIFICATENUMBER,
							C.CERTIFICATEID,
							C.EXTENSION_EN,
							LTRIM( P.MATL_NUM, '0') AS MATL_NUM,   -- JESEITZ ADDED LTRIM 3/14/13
							P.SPEEDRATING,
							P.SKUID
					FROM CERTIFICATE C,
					     CERTIFICATIONTYPE T,
					     PRODUCT P,
					      PRODUCTCERTIFICATE PC
					WHERE C.CERTIFICATEID = PC.CERTIFICATEID
					  AND P.SKUID = PC.SKUID
					  AND T.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
					  AND C.CERTIFICATEID = LN_CERTIFICATEID
					  AND C.EXTENSION_EN = ls_Extension_En
					ORDER BY MATL_NUM;

			ELSE
				-- Get the Certificate Id
				ln_CertificateId := ICS_COMMON_FUNCTIONS.GETCERTIFICATEID(ps_CertificateNumber,pn_CertificationTypeId);
				
				-- Returns material for the certificate
				OPEN pc_Cursor  FOR
					SELECT  T.CERTIFICATIONTYPENAME,
							C.CERTIFICATENUMBER,
							C.CERTIFICATEID,
							C.EXTENSION_EN,
							LTRIM(P.MATL_NUM,'0') AS MATL_NUM,     -- JESEITZ ADDED LTRIM 3/14/13
							P.SPEEDRATING,
							P.SKUID
					FROM CERTIFICATE C,
						 CERTIFICATIONTYPE T,
						 PRODUCT P,
						 PRODUCTCERTIFICATE PC
					WHERE C.CERTIFICATEID = PC.CERTIFICATEID
					  AND P.SKUID = PC.SKUID
					  AND T.CERTIFICATIONTYPEID = C.CERTIFICATIONTYPEID
					  AND C.CERTIFICATEID = ln_CertificateId
					ORDER BY MATL_NUM;
			END IF;
			
		ELSE
			-- Raise an exception when certificate not found
			RAISE ex_CertificateNotExist;

		END IF;

	EXCEPTION
		WHEN ex_CertificateNotExist THEN
			
			ls_ErrorMsg:=  SQLERRM || '- GetCertificateMatls. Certificate NUMBER - '||ps_CertificateNumber||' not exist.';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.GetCertificateMatls',
													  ax_recorddata    => 'pn_CertificationTypeId - '||pn_CertificationTypeId||',ps_CertificateNumber - '||
													  ps_certificatenumber||',ps_Extension_En - '||ps_Extension_En,
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  SQLERRM || '- GetCertificateMatls. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.GetCertificateMatls',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetCertificateMatls;

	PROCEDURE DetachCertificate(pn_SkuId          IN  PRODUCT.SKUID%TYPE,
								pn_CertificateId  IN  CERTIFICATE.CERTIFICATEID%TYPE)
	AS
	
	/******************************************************************************
	   NAME:       DetachCertificate
	   PURPOSE:    Detach materials from certificate

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        02/28/2013  Krishna          Initial Version
	   1.1       04/08/2014   JESEITZ        added call to delete product country records if detaching from certificate.
	   1.2       04/14/16      JESEITZ         added call to delete product imark family records if detaching from certificate
	******************************************************************************/
	
	ex_CertificateNotFound EXCEPTION;

	ln_CertificationTypeID  NUMBER:=NULL;
	ln_Cnt 					NUMBER:=NULL;
	ls_ErrorMsg 			VARCHAR2(4000):=NULL;

	BEGIN

		SELECT COUNT(*)
			INTO ln_Cnt
		FROM PRODUCTCERTIFICATE
		WHERE SKUID = pn_SkuId
		AND CERTIFICATEID = pn_CertificateId ;

		IF ln_Cnt > 0 THEN
			
			SELECT CERTIFICATIONTYPEID 
			  INTO ln_CertificationTypeID 
			FROM PRODUCTCERTIFICATE  -- JESEITZ added 4/7/2014
			WHERE SKUID = pn_SkuId
			AND CERTIFICATEID = pn_CertificateId;

			--jeseitz added 4/14/2016 to clear out productimarkfamily records.
			IF  ln_CertificationTypeID = 4 THEN
				ICS_MAINTENANCE.DELETEPRODUCTIMARKFAMILY(pn_skuid,pn_CertificateId);
			END IF;
			
			COMMIT;
			
			DELETE FROM 
			PRODUCTCERTIFICATE
			WHERE SKUID = pn_SkuId
			  AND CERTIFICATEID = pn_CertificateId;

			COMMIT;
			
			--jeseitz added 4/7/14 to clear out productrequestrecords.
			ICS_MAINTENANCE.DeleteProductCountry (pn_SkuId,ln_CertificationTypeID,'ICSDEV');

		ELSE
			RAISE Ex_Certificatenotfound;
		END IF;

	EXCEPTION
		WHEN Ex_CertificateNotFound THEN
			ls_ErrorMsg:=  SQLERRM || '- DetachCertificate. Skuid - '||pn_SkuId||',Certificateid - '|| pn_CertificateId||' not exist.';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
												      ad_operatorid    => 'ICSDEV',
												      ad_daterecorded  => SYSDATE,
												      as_processname   => 'ICS_MAINTENANCE.Detachcertificate',
												      ax_recorddata    => 'pn_SkuId - '||pn_SkuId||',pn_CertificateId - '|| pn_CertificateId,
												      as_messagecode   => TO_CHAR(SQLCODE),
												      as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- Detachcertificate. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.Detachcertificate',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END DetachCertificate;

	PROCEDURE MoveCertificate(pn_CertificationTypeId   IN CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
							  ps_NewCertificateNumber  IN CERTIFICATE.CERTIFICATENUMBER%TYPE,
							  ps_NewExtension_En       IN CERTIFICATE.EXTENSION_EN%TYPE,
							  pn_SkuId                 IN PRODUCT.SKUID%TYPE,
							  pn_CertificateId         IN CERTIFICATE.CERTIFICATEID%TYPE,
							  ps_OperatorName          IN VARCHAR2)
	IS
	/******************************************************************************
	NAME:       MoveCertificate
	PURPOSE:    Move certificate to another certificate

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        02/28/2013  Krishna          Initial Version
	******************************************************************************/
		
		ex_CertificateNotExist EXCEPTION;
		ls_OperatorId          VARCHAR2(50) := 'ICSDEV';
		ln_CertificateId       NUMBER := 0;
		ls_ErrorMsg            VARCHAR2(4000):=NULL;
		ln_CertificateExist    VARCHAR2(1);
		ls_NewExtension_En     CERTIFICATE.EXTENSION_EN%TYPE:=NULL;

	BEGIN

		IF ps_OperatorName IS NOT NULL THEN
			ls_OperatorId := ps_OperatorName;
		END IF;

		-- Checks whether given certificate exist or not
		ln_CertificateExist :=ICS_MAINTENANCE.CHECKIFCERTIFICATEEXISTS(pn_CertificationTypeId,
																	   ps_NewCertificateNumber,
																	   ps_NewExtension_En);

		IF UPPER(ln_CertificateExist) = 'Y' THEN

			IF (pn_CertificationTypeId = 1 OR pn_CertificationTypeId = 6) THEN

				IF ps_NewExtension_En IS NULL THEN
					-- Get the extension for given certificate
					ls_NewExtension_En := ICS_MAINTENANCE.GETCERTIFICATEEXTNUMBER(pn_CertificationTypeId,ps_NewCertificateNumber);
				ELSE
					ls_NewExtension_En :=   ps_NewExtension_En ;
				END IF;

				-- Get the certificate id
				SELECT CertificateId
				  INTO ln_CertificateId
				FROM CERTIFICATE
				WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_NewCertificateNumber)
				  AND CERTIFICATIONTYPEID =pn_CertificationTypeId
				  AND EXTENSION_EN =  ls_NewExtension_En;


			ELSE
				-- Get the certificate id
				SELECT CERTIFICATEID
				INTO ln_CertificateId
				FROM CERTIFICATE
				WHERE UPPER(CERTIFICATENUMBER) = UPPER(ps_NewCertificateNumber)
				  AND CERTIFICATIONTYPEID =pn_CertificationTypeId;

			END IF;

			-- Update certificate id with new certificate id
			UPDATE PRODUCTCERTIFICATE
			SET CERTIFICATEID = ln_CertificateId,
				MODIFIEDBY    = ls_OperatorId,
				MODIFIEDON    = SYSDATE
			WHERE CERTIFICATEID = pn_CertificateId
			  AND SKUID = pn_SkuId;

			IF  PN_CertificationTypeId  =  4 then
				DELETEPRODUCTIMARKFAMILY(pn_SkuId,ln_CertificateId);
			END IF;

			COMMIT;
		
		ELSE
			RAISE  ex_CertificateNotExist;

		END IF;

	EXCEPTION
		WHEN ex_CertificateNotExist THEN
			--Insert into excpetion record
			ls_ErrorMsg:=  SQLERRM || '- MoveCertificate. Certificate Number - '||ps_NewCertificateNumber||' exist.';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.MoveCertificate',
													  ax_recorddata    => 'pn_CertificationTypeId - '||pn_CertificationTypeId
													  				||', ps_NewCertificateNumber - '||ps_NewCertificateNumber||',pn_NewExtension_en - '||ps_NewExtension_En
													  				||',pn_SkuId    - '||pn_SkuId ||',pn_CertificateId  - '||pn_CertificateId    ,
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

	WHEN OTHERS THEN
		ls_ErrorMsg:=  SQLERRM || '- Movecertificate. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.MoveCertificate',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END MoveCertificate;

	PROCEDURE GetDuplicateCert(ps_Matl_Num    IN    PRODUCT.MATL_NUM%TYPE,
							   ps_SpeedRating IN    PRODUCT.SPEEDRATING%TYPE,
							   ps_Result        OUT SYS_REFCURSOR)
	/******************************************************************************
	   NAME:       GetDuplicateCert
	   PURPOSE:    Returns Assigned certificate details for given Matl_Num and Speedratings.

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        03/06/2013  Krishna          Initial Version
	   1.1        01/07/2014  Guru Gangadhar   Removed Speed Rating
	******************************************************************************/
	AS
		ln_Exist_Count      NUMBER(4):=NULL;
		ex_Record_Not_Exist EXCEPTION;
		ls_ErrorMsg         VARCHAR2(4000):=NULL;

	BEGIN
		--Counts the materials in product
		IF(ps_SpeedRating IS NOT NULL) THEN
			SELECT COUNT(*)
				INTO ln_Exist_Count
			FROM PRODUCT
			WHERE MATL_NUM = LPAD(ps_Matl_Num,18,0)
			AND SPEEDRATING = ps_SpeedRating;
		ELSE
			SELECT COUNT(*)
			INTO ln_Exist_Count
			FROM PRODUCT
			WHERE MATL_NUM = LPAD(ps_Matl_Num,18,0);
		END IF;

		IF ln_Exist_Count < 1 THEN
			RAISE ex_Record_Not_Exist;
		ELSE
		
			IF(ps_SpeedRating IS NOT NULL) THEN
				-- Pull the duplicate materials
				OPEN ps_result FOR
					SELECT P.SKUID ID,
						   LTRIM( P.MATL_NUM,'0') AS MATL_NUM,  -- JESEITZ ADDED LTRIM 3/14/13
						   P.SPEEDRATING,
						   T.CERTIFICATIONTYPENAME ,
						   C.CERTIFICATENUMBER
					FROM PRODUCT P,
						 PRODUCTCERTIFICATE PC,
						 CERTIFICATE C,
						 CERTIFICATIONTYPE T
					WHERE P.SKUID = PC.SKUID(+)
					  AND C.CERTIFICATEID(+) = PC.CERTIFICATEID
					  AND C.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID(+)
					  AND P.MATL_NUM = LPAD(ps_Matl_Num,18,0)
					  AND P.SPEEDRATING = ps_SpeedRating
					ORDER BY Id;
			ELSE
				OPEN ps_result FOR
					SELECT  P.SKUID ID,
							LTRIM( P.MATL_NUM,'0') AS MATL_NUM,  -- JESEITZ ADDED LTRIM 3/14/13
							P.SPEEDRATING,
							T.CERTIFICATIONTYPENAME ,
							C.CERTIFICATENUMBER
					FROM PRODUCT P,
						 PRODUCTCERTIFICATE PC,
						 CERTIFICATE C,
						 CERTIFICATIONTYPE T
					WHERE P.SKUID = PC.SKUID(+)
					  AND C.CERTIFICATEID(+) = PC.CERTIFICATEID
					  AND C.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID(+)
					  AND P.MATL_NUM = LPAD(ps_Matl_Num,18,0)
					ORDER BY ID;
			END IF;
		END IF;
		
	EXCEPTION
		WHEN ex_Record_Not_Exist  THEN
			ls_ErrorMsg:=  SQLERRM || '- GetDuplicateCert. Matl_Num - '||ps_Matl_Num||' And Speedrating - '||ps_Speedrating||' are not exist.';

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- GetDupCert. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.GetDuplicateCert',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
	
	End GetDuplicateCert;

	PROCEDURE RemoveDuplicateCert(pn_SkuId IN PRODUCT.SKUID%TYPE)
	/******************************************************************************
	   NAME:       RemoveDuplicateCert
	   PURPOSE:    Deletes the data from below tabls for given Skuid.

	   1) Productcertificate
	   2) Productrequest
	   3) Product

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        03/06/2013  Krishna          Initial Version
	******************************************************************************/
	AS
		ln_Skuid_Exist_Count NUMBER(4):=NULL;
		ex_Record_Not_Exist  EXCEPTION;
		ls_ErrorMsg          VARCHAR2(4000):=NULL;
		
	BEGIN

		SELECT COUNT(*)
			INTO ln_SkuId_Exist_Count
		FROM PRODUCT
		WHERE SKUID = pn_SkuId;

		IF ln_Skuid_Exist_Count < 1 THEN
			RAISE ex_Record_Not_Exist;
		ELSE
			DELETE FROM PRODUCTCERTIFICATE WHERE SKUID = pn_Skuid;

			--jeseitz 4/13/2016 - delete Product_imark_family (only present if on IMARK)
			DELETE FROM PRODUCT_IMARK_FAMILY WHERE SKUID = pn_skuid;

			--DELETE FROM Productcountry
			DELETE FROM PRODUCTREQUEST WHERE SKUID = pn_Skuid;

			DELETE FROM PRODUCT WHERE SKUID = pn_Skuid;

			COMMIT;
		END IF;

	EXCEPTION
		WHEN  ex_Record_Not_Exist THEN
			ls_ErrorMsg:=  SQLERRM || '- RemoveDuplicateCert. SkuId - '||pn_SkuId||' is not exist.';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
			                                          ad_operatorid    => 'ICSDEV',
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => 'ICS_MAINTENANCE.RemoveDuplicateCert',
			                                          ax_recorddata    => 'pn_Skuid - '||pn_Skuid,
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);
	WHEN OTHERS THEN
		ls_ErrorMsg:=  SQLERRM || '- RemoveDuplicateCert. An error have ocurred.(when others)';

		--Insert record into exception table
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
											      ad_operatorid    => 'ICSDEV',
											      ad_daterecorded  => SYSDATE,
											      as_processname   => 'ICS_MAINTENANCE.RemoveDuplicateCert',
											      ax_recorddata    => 'An error have ocurred.(when others)',
											      as_messagecode   => TO_CHAR(SQLCODE),
											      as_message       => ls_ErrorMsg);

		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
	END RemoveDuplicateCert;

	PROCEDURE GetFamilies (pn_Certificateid IN     IMARK_FAMILY.CERTIFICATEID%TYPE,
                           ps_Result          OUT  SYS_REFCURSOR)
	/******************************************************************************
	   NAME:       GetFamilies
	   PURPOSE:    Returns all data from IMark_Family table

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        10/16/2013  Harini          Initial Version
	   1.1        11/20/2013  Harini          Added Orderby Family id condition
	******************************************************************************/
	AS
		ls_ErrorMsg         VARCHAR2(4000):=NULL;
	BEGIN
	
		OPEN ps_Result FOR
		SELECT  *
		FROM  IMARK_FAMILY
		WHERE CERTIFICATEID =pn_Certificateid
		ORDER BY FAMILY_ID;

	EXCEPTION
		WHEN OTHERS THEN
		ls_ErrorMsg:=  SQLERRM || '- GetFamilies. An error have ocurred.(when others)';

		--Insert record into exception table
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
												  ad_operatorid    => 'ICSDEV',
												  ad_daterecorded  => SYSDATE,
												  as_processname   => 'ICS_MAINTENANCE.GetFamilies',
												  ax_recorddata    => 'An error have ocurred.(when others). Certificateid = '||pn_certificateid,
												  as_messagecode   => TO_CHAR(SQLCODE),
												  as_message       => ls_ErrorMsg);

		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END GetFamilies;

	PROCEDURE SaveFamily(pn_Certificateid      IN IMARK_FAMILY.CERTIFICATEID%TYPE,
						 pn_FamilyID           IN IMARK_FAMILY.FAMILY_ID%TYPE,
						 ps_FamilyCode         IN IMARK_FAMILY.FAMILY_CODE%TYPE,
						 ps_FamilyDesc         IN IMARK_FAMILY.FAMILY_DESC%TYPE,
						 ps_ApplicationCat     IN IMARK_FAMILY.APPLICATION_CAT%TYPE,
						 ps_ConstructionType   IN IMARK_FAMILY.CONSTRUCTION_TYPE%TYPE,
						 ps_StructureType      IN IMARK_FAMILY.STRUCTURE_TYPE%TYPE,
						 ps_MountingType       IN IMARK_FAMILY.MOUNTING_TYPE%TYPE,
						 ps_AspectRatioCat     IN IMARK_FAMILY.ASPECT_RATIO_CAT%TYPE,
						 ps_SpeedRatingCat     IN IMARK_FAMILY.SPEED_RATING_CAT%TYPE,
						 ps_LoadIndexCat       IN IMARK_FAMILY.LOAD_INDEX_CAT%TYPE,
						 ps_UserName           IN IMARK_FAMILY.CREATEDBY%TYPE)
	/******************************************************************************
	   NAME:       SaveFamily
	   PURPOSE:    Saving changes to the IMARK_FAMILY table.
	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        10/16/2013  Harini          Initial Version
	   1.1        5/15/2014    JESEITZ      don't automatically change imark in product table when family desc. changes.
	******************************************************************************/
	AS
		ln_RecordExist      NUMBER:=NULL;
		ls_Family_Desc_Old  IMARK_FAMILY.FAMILY_DESC%TYPE:=NULL;
		ls_ErrorMsg         VARCHAR2(4000):=NULL;
	BEGIN
		-- Checking whether the record exists in IMark_Family table for the given Family_Id
		SELECT COUNT(1)
		INTO ln_RecordExist
		FROM IMARK_FAMILY
		WHERE FAMILY_ID = pn_FamilyID
		  AND CERTIFICATEID = pn_Certificateid;

		-- If record doesn't exist then insert a new record with the input values given
		IF(ln_RecordExist < 1) THEN
		
			INSERT INTO IMARK_FAMILY(FAMILY_ID
									,FAMILY_CODE
									,FAMILY_DESC
									,APPLICATION_CAT
									,CONSTRUCTION_TYPE
									,STRUCTURE_TYPE
									,MOUNTING_TYPE
									,ASPECT_RATIO_CAT
									,SPEED_RATING_CAT
									,LOAD_INDEX_CAT
									,CREATEDBY
									,CREATEDON
									,CERTIFICATEID)
							  VALUES(pn_FamilyID
									,ps_FamilyCode
									,ps_FamilyDesc
									,ps_ApplicationCat
									,ps_ConstructionType
									,ps_StructureType
									,ps_MountingType
									,ps_AspectRatioCat
									,ps_SpeedRatingCat
									,ps_LoadIndexCat
									,ps_UserName
									,SYSDATE
									,pn_Certificateid);
		ELSE
			-- Retrieve the Family_Desc from IMark_Family table
			SELECT FAMILY_DESC
			  INTO ls_Family_Desc_Old
			FROM IMARK_FAMILY
			WHERE FAMILY_ID    = pn_FamilyID
			  AND CERTIFICATEID = pn_Certificateid;

			-- If the record exists then update with the given inputs
			UPDATE IMARK_FAMILY
			SET FAMILY_CODE       = ps_FamilyCode,
			    FAMILY_DESC       = ps_FamilyDesc,
			    APPLICATION_CAT   = ps_ApplicationCat,
			    CONSTRUCTION_TYPE = ps_ConstructionType,
			    STRUCTURE_TYPE    = ps_StructureType,
			    MOUNTING_TYPE     = ps_MountingType,
			    ASPECT_RATIO_CAT  = ps_AspectRatioCat ,
			    SPEED_RATING_CAT  =  ps_SpeedRatingCat,
			    LOAD_INDEX_CAT    = ps_LoadIndexCat ,
			    MODIFIEDBY        = ps_UserName,
			    MODIFIEDON        = SYSDATE
			WHERE FAMILY_ID       = pn_FamilyID
			  AND CERTIFICATEID = pn_certificateid;
		
		END IF;

		COMMIT;

	EXCEPTION
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- SaveFamily. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.SaveFamily',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END SaveFamily;

	PROCEDURE DeleteFamily(pn_Certificateid IN IMARK_FAMILY.CERTIFICATEID%TYPE,
	                       pn_FamilyID      IN IMARK_FAMILY.FAMILY_ID%TYPE)
	/******************************************************************************
	NAME:       DeleteFamily
	PURPOSE:    Deletes the data from IMark_Family table for given FamilyId.
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        10/16/2013  Harini          Initial Version
	******************************************************************************/
	AS
		ln_FamilyId_Exist_Count NUMBER(4):=NULL;
		ex_Record_Not_Exist     EXCEPTION;
		ls_ErrorMsg             VARCHAR2(4000):=NULL;
		
	BEGIN
	
		SELECT COUNT(1)
		INTO ln_FamilyId_Exist_Count
		FROM IMARK_FAMILY
		WHERE FAMILY_ID = pn_FamilyID
		  AND CERTIFICATEID = pn_Certificateid;

		IF ln_FamilyId_Exist_Count < 1 THEN
			RAISE ex_Record_Not_Exist;
		ELSE
			DELETE FROM IMARK_FAMILY
			WHERE FAMILY_ID  = pn_FamilyID
			  AND CERTIFICATEID= pn_certificateid;
			COMMIT;
		END IF;
		
	EXCEPTION
		WHEN ex_Record_Not_Exist THEN
			ls_ErrorMsg:=  SQLERRM || '- DeleteFamily. FamilyId - '||pn_FamilyID||' is not exist.';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
			                                          ad_operatorid    => 'ICSDEV',
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => 'ICS_MAINTENANCE.DeleteFamily',
			                                          ax_recorddata    => 'Family_Id - '||pn_FamilyID,
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);
			
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- DeleteFamily. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.DeleteFamily',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
	END DeleteFamily;

	PROCEDURE CheckIfFamilyExists (pn_Certificateid     IN     IMARK_FAMILY.CERTIFICATEID%TYPE,
								   pn_FamilyID          IN     IMARK_FAMILY.FAMILY_ID%TYPE,
								   ps_Family_Exist        OUT  VARCHAR2,
								   ps_Family_Desc         OUT  VARCHAR2)
	/******************************************************************************
	NAME:       CheckIfFamilyExists
	PURPOSE:    Check whether the Family exists or not and get the Desc

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        10/24/2013  Harini          Initial Version
	1.0        11/05/2013  Ajit            To get Family Desc from
								  IMark_Family table
	******************************************************************************/
	AS
		
		ls_Family_Desc          IMARK_FAMILY.FAMILY_DESC%TYPE:=NULL;
		ls_ErrorMsg             VARCHAR2(4000):=NULL;
		
	BEGIN

		SELECT FAMILY_DESC
		  INTO ls_Family_Desc
		FROM IMARK_FAMILY
		WHERE FAMILY_ID = pn_FamilyID
		  AND CERTIFICATEID = pn_certificateid;

		ps_Family_Exist := 'Y';
		ps_Family_Desc := ls_Family_Desc;

	EXCEPTION
		WHEN NO_DATA_FOUND THEN
			ps_Family_Exist := 'N';
			ps_Family_Desc := '';

		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  SQLERRM || '- CheckIfFamilyExists. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => NULL,
												      AD_OPERATORID    => 'ICSDEV',
												      AD_DATERECORDED  => SYSDATE,
												      AS_PROCESSNAME   => 'ICS_MAINTENANCE.CheckIfFamilyExists',
												      AX_RECORDDATA    => 'An error have ocurred.(when others)',
												      AS_MESSAGECODE   => TO_CHAR(SQLCODE),
												      AS_MESSAGE       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
			
	END CheckIfFamilyExists;

	PROCEDURE EDIT_PRODUCT(pn_SKUID       IN   PRODUCT.SKUID%TYPE, 
	                       ps_speedrating IN PRODUCT.SPEEDRATING%TYPE)

	AS
	/******************************************************************************
	NAME:       EDIT_PRODUCT
	PURPOSE:    This procedure accepts SKUID and SpeedRating as inputs and updates the
	Product table with the given SpeedRating for the given SKUID

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        11/19/2013  Guru Gangadhar    Initial Version
	******************************************************************************/

		ls_ErrorMsg VARCHAR2(4000):=NULL;

	BEGIN

		UPDATE PRODUCT SET SPEEDRATING = ps_speedrating where skuid = pn_SKUID;
		COMMIT;
	
	EXCEPTION
		WHEN OTHERS THEN
		ls_ErrorMsg:=  SQLERRM || '- EDIT_PRODUCT. An error have ocurred.(when others)';

		--Insert record into exception table
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
												  ad_operatorid    => 'ICSDEV',
												  ad_daterecorded  => SYSDATE,
												  as_processname   => 'ICS_MAINTENANCE.EDIT_PRODUCT',
												  ax_recorddata    => 'An error have ocurred.(when others)',
												  as_messagecode   => TO_CHAR(SQLCODE),
												  as_message       => ls_ErrorMsg);

		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END EDIT_PRODUCT;

	PROCEDURE COPY_PRODUCT(ps_MATL_NUM IN   PRODUCT.MATL_NUM%TYPE)
	AS
	/******************************************************************************
	NAME:       COPY_PRODUCT
	PURPOSE:    This procedure accepts Matl_Num of product record to copy.  Get the max skuid
	of the particular matl_num. Then insert an exact copy of the record into the product
	table (will get the next sequence number as the skuid).

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        11/19/2013  Guru Gangadhar    Initial Version
	******************************************************************************/
	
		ln_orig_skuid       NUMBER:=NULL;
		ln_SkuId            NUMBER:=NULL;
		ls_ErrorMsg         VARCHAR2(4000):=NULL;
		ex_Record_Not_Exist EXCEPTION;

	BEGIN

		SELECT MAX(SKUID)
		  INTO ln_orig_skuid
		FROM PRODUCT
		WHERE MATL_NUM = LPAD(ps_matl_num,18,0);

		IF ln_orig_skuid IS  NULL THEN
			RAISE ex_Record_Not_Exist;
		END IF;

		SELECT SKUID_SEQ.NEXTVAL INTO ln_SkuId FROM DUAL;

		INSERT INTO PRODUCT(SKUID, 
		                    BRAND, 
							BRAND_LINE, 
							SKU, 
							BRANDDESC, 
							MATL_NUM, 
							SIZESTAMP, 
							TIRETYPEID,
							PSN, 
							DISCONTINUEDDATE,
							SPECNUMBER,
							SPEEDRATING,
							SINGLOADINDEX, 
							DUALLOADINDEX, 
							TUBELESSYN, 
							REINFORCEDYN, 
							EXTRALOADYN, 
							UTQGTREADWEAR,
							UTQGTRACTION,
							UTQGTEMP, 
							MUDSNOWYN, 
							RIMDIAMETER, 
							SERIALDATE, 
							LOADRANGE, 
							MEARIMWIDTH, 
							REGROOVABLEIND,
							PLANTPRODUCED,
							MOSTRECENTTESTDATE, 
							IMARK,INFORMENUMBER, 
							FECHADATE, 
							TREADPATTERN, 
							SPECIALPROTECTIVEBAND,
							NOMINALTIREWIDTH,
							ASPECTRATIO, 
							TREADWEARINDICATORS, 
							NAMEOFMANUFACTURER,
							FAMILY, 
							DOTSERIALNUMBER,
							TPN, 
							BIASBELTEDRADIAL,
							SEVEREWEATHERIND)
					(SELECT LN_SKUID, 
					        BRAND,BRAND_LINE,
							SKU,BRANDDESC,
							MATL_NUM, 
							SIZESTAMP, 
							TIRETYPEID, 
							PSN, 
							DISCONTINUEDDATE,
							SPECNUMBER, 
							SPEEDRATING,
							SINGLOADINDEX, 
							DUALLOADINDEX, 
							TUBELESSYN, 
							REINFORCEDYN,
							EXTRALOADYN, 
							UTQGTREADWEAR, 
							UTQGTRACTION, 
							UTQGTEMP,
							MUDSNOWYN, 
							RIMDIAMETER, 
							SERIALDATE,
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
							TPN,
							BIASBELTEDRADIAL,
							SEVEREWEATHERIND
					FROM PRODUCT 
					WHERE SKUID = ln_orig_skuid);
		COMMIT;

	EXCEPTION
		WHEN  ex_Record_Not_Exist THEN
			ls_ErrorMsg:=  SQLERRM || '- COPY_PRODUCT. Materail Number - '||ps_matl_num||' does not exist.';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(AS_MACHINEID     => NULL,
			AD_OPERATORID    => 'ICSDEV',
			AD_DATERECORDED  => SYSDATE,
			AS_PROCESSNAME   => 'ICS_MAINTENANCE.COPY_PRODUCT',
			AX_RECORDDATA    => 'Matl_Num - '||ps_matl_num,
			AS_MESSAGECODE   => TO_CHAR(SQLCODE),
			AS_MESSAGE       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20001,ls_ErrorMsg);

		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- COPY_PRODUCT. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.COPY_PRODUCT',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END COPY_PRODUCT;

	PROCEDURE GET_MATERIALS(ps_MATL_NUM IN     PRODUCT.MATL_NUM%TYPE,
							pc_Cursor     OUT  SYS_REFCURSOR)
	AS
	/******************************************************************************
	NAME:       GET_MATERIALS
	PURPOSE:    This procedure will return skuid, sku, speedrating, matl_num
		   for the given material.

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        11/19/2013  Guru Gangadhar    Initial Version
	******************************************************************************/
		
		ln_orig_skuid  NUMBER:=NULL;
		ln_SkuId       NUMBER:=NULL;
		ls_ErrorMsg    VARCHAR2(4000):=NULL;

	BEGIN

		OPEN pc_Cursor  FOR
			SELECT  SKUID,
					SKU,
					SPEEDRATING,
					MATL_NUM
			FROM PRODUCT
			WHERE MATL_NUM = LPAD(ps_matl_num,18,'0');

	EXCEPTION
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- GET_MATERIALS. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.GET_MATERIALS',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GET_MATERIALS;

	PROCEDURE ATTACH_PRODUCT(pn_skuid                 IN    PRODUCT.SKUID%TYPE,
							 ps_certificateNumber     IN    CERTIFICATE.CERTIFICATENUMBER%TYPE,
							 ps_Extension_EN          IN    CERTIFICATE.EXTENSION_EN%TYPE,
							 pn_certificationtypeid   IN    CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
							 ps_ErrorMsg               OUT  VARCHAR2)
	AS
	/******************************************************************************
	   NAME:       ATTACH_PRODUCT
	   PURPOSE:   This procedure will   allow user to correct an older version of a product record
				  to attach it to a certificate .Inputs should be  SKUID , CERTIFICATE NAME,EXTENSION
				  and CERTIFICATIONTYPE and create a productcertificate record.  The certificate and skuid must already exist.

	   REVISIONS:
	   Ver        Date        Author           Description
	   ---------  ----------  ---------------  ------------------------------------
	   1.0        11/19/2013  Guru Gangadhar    Initial Version
	******************************************************************************/

		ln_skuid_cnt      NUMBER:=NULL;
		ln_certificateId  NUMBER:=NULL;
		ls_ErrorMsg       VARCHAR2(4000):=NULL;
	BEGIN

		--Verify that is a valid skuid:
		SELECT COUNT(*) INTO ln_skuid_cnt 
		FROM PRODUCT  
		WHERE SKUID = pn_skuid;

		IF ln_skuid_cnt = 0 THEN
			ps_ErrorMsg := 'skuid does not exist';
		ELSE

			--Verify that it is a valid certificate and get the certificateid
			SELECT CERTIFICATEID
				INTO ln_certificateid
			FROM CERTIFICATE
			WHERE CERTIFICATENUMBER = ps_certificatenumber
			  AND EXTENSION_EN      = ps_extension_en
			  AND CERTIFICATIONTYPEID = pn_certificationtypeid;

			INSERT INTO PRODUCTCERTIFICATE(SKUID, 
			                               CERTIFICATIONTYPEID, 
										   CERTIFICATEID,
										   DATEASSIGNED_EGI)
								    VALUES(pn_skuid, 
									       pn_certificationtypeid, 
										   ln_certificateid, 
										   TRUNC(SYSDATE));
			COMMIT;
			
			--jeseitz 4/13/16 - if Imark, need to copy the Product_imark_family record too.
			IF pn_certificationtypeid = 4 then
			INSERT INTO product_imark_family (  certificateid,skuid,familyid, imark)
			VALUES ( ln_certificateId,PN_skuId,0,' ' );
		END IF;

		END IF ;

	EXCEPTION
		WHEN NO_DATA_FOUND THEN
			ps_ErrorMsg:= 'invalid certificate';
		WHEN DUP_VAL_ON_INDEX THEN
			ps_ErrorMsg:= 'duplicate record';
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- ATTACH_PRODUCT. An error have ocurred.(when others)';

			--Insert record into exception table
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.ATTACH_PRODUCT',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END ATTACH_PRODUCT;

	FUNCTION GetCertificateRecentExtNumber(pn_CertificationTypeId   IN  CERTIFICATE.CERTIFICATIONTYPEID%TYPE,
									       ps_CertificateNumber     IN  CERTIFICATE.CERTIFICATENUMBER%TYPE)
	RETURN VARCHAR2
	AS
	/******************************************************************************
	NAME:       GetCertificateRecentExtNumber
	PURPOSE:    Returns certificate Recent extension  for given
		   Certificate number and certificate type

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        11/21/2013  Harini          Initial Version
	1.1        01/10/2014  Harini          Returning null if no record found
	******************************************************************************/
	
		ls_Extension_En CERTIFICATE.EXTENSION_EN%TYPE:=NULL;
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
		
	BEGIN

		SELECT MAX(TO_NUMBER(Extension_En))
		INTO ls_Extension_En
		FROM Certificate
		WHERE CertificationTypeId      = Pn_CertificationTypeId
		AND UPPER(CertificateNumber) = UPPER(Ps_CertificateNumber)
		AND UPPER(MostRecentCert) = 'Y';

		RETURN Ls_Extension_En;

	EXCEPTION
		WHEN No_Data_Found THEN
			RETURN NULL;
	WHEN OTHERS THEN
		ls_ErrorMsg:=  SQLERRM || '- GetCertificateRecentExtNumber. An error have ocurred.(when others)';

		--Insert record into exception table
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
												  ad_operatorid    => 'ICSDEV',
												  ad_daterecorded  => SYSDATE,
												  as_processname   => 'ICS_MAINTENANCE.GetCertificateRecentExtNumber',
												  ax_recorddata    => 'An error have ocurred.(when others)',
												  as_messagecode   => TO_CHAR(SQLCODE),
												  as_message       => ls_ErrorMsg);

		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END  GetCertificateRecentExtNumber;

	PROCEDURE Refresh_Product (ps_matl_num IN      PRODUCT.MATL_NUM%TYPE,
							   pn_ErrorNum   OUT   NUMBER,
							   ps_ErrorMsg   OUT   VARCHAR2)
	AS
	/******************************************************************************
	NAME:       Refresh_Product
	PURPOSE:    This procedure accepts Matl_Num and updates all the attaribute values.

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        01/10/2014  Harini           Initial Version
	******************************************************************************/
	
		ln_SkuId NUMBER:=NULL;
		ls_ErrorMsg VARCHAR2(4000):=NULL;

		ls_Brand               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BrandLine           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SizeStamp           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TireTypeId          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PSN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DiscontinueDate     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecNumber          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpeedRating         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SingleLoadIndex     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DualLoadIndex       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TubelessSyn         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ReinforcedYN        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_ExtraLoadYN         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTreadWear       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTraction        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_UTQGTemp            CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MudSnowYN           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SevereWeatherInd    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RimDiameter         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SerialDate          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_LoadRange           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MeaRimWidth         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_RegroovableInd      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_PlantProduced       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_MostRecentDate      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_IMark               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_InformeNumber       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_FechaDate           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadPattern        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SpecialProtBrand    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NominalTireWidth    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_AspectRatio         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TreadWearInd        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_NameOfManufac       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_Family              CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_DotSerialNumber     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_TPN                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_BiasBeltedRadial    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ls_SKU                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE:=NULL;
		ln_imarkcount          NUMBER:=NULL;
		ln_imarkcertid         CERTIFICATE.CERTIFICATEID%TYPE:=NULL;
		
	BEGIN

		SELECT MAX(skuid)
		INTO ln_skuid
		FROM  PRODUCT
		WHERE MATL_NUM = LPAD(ps_Matl_Num,18,0);

		ICS_CRUD.GETTIRECHARACTERISTICSALL(ps_Matl_Num,
		                                   ls_Brand,
										   ls_BrandLine,
										   ls_SizeStamp,
										   ls_TireTypeId,
										   ls_PSN,
										   ls_DiscontinueDate,
										   ls_SpecNumber,
										   ls_SpeedRating,
										   ls_SingleLoadIndex,
										   ls_DualLoadIndex ,
										   ls_TubelessSyn,
										   ls_ReinforcedYN ,
										   ls_ExtraLoadYN,
										   ls_UTQGTreadWear,
										   ls_UTQGTraction,
										   ls_UTQGTemp,
										   ls_MudSnowYN,
										   ls_SevereWeatherInd,
										   ls_RimDiameter,
										   ls_SerialDate,
										   ls_LoadRange,
										   ls_MeaRimWidth,
										   ls_RegroovableInd,
										   ls_PlantProduced,
										   ls_MostRecentDate,
										   ls_IMark ,
										   ls_InformeNumber,
										   ls_FechaDate,
										   ls_TreadPattern,
										   ls_SpecialProtBrand,
										   ls_NominalTireWidth,
										   ls_AspectRatio,
										   ls_TreadWearInd,
										   ls_NameOfManufac,
										   ls_Family ,
										   ls_DotSerialNumber,
										   ls_TPN ,
										   ls_BiasBeltedRadial,
										   ls_SKU);


		UPDATE PRODUCT
		   SET BRAND             = ls_Brand,
			   BRAND_LINE        = ls_BrandLine,
			   SKU               = ls_SKU,
			   BRANDDESC         = NULL,
			   SIZESTAMP         = ls_SizeStamp,
			   TIRETYPEID        = ls_TireTypeId,
			   PSN               = ls_PSN,
			   DISCONTINUEDDATE  = DECODE(ls_DiscontinueDate,NULL,NULL,TO_DATE(ls_DiscontinueDate,'MM/DD/YYYY')),
			   SINGLOADINDEX     = ls_SingleLoadIndex,
			   DUALLOADINDEX     = ls_DualLoadIndex,
			   TUBELESSYN        = ls_TubelessSyn,
			   REINFORCEDYN      = ls_ReinforcedYN,
			   EXTRALOADYN       = ls_ExtraLoadYN,
			   UTQGTREADWEAR     = ls_UTQGTreadWear,
			   UTQGTRACTION      = ls_UTQGTraction,
			   UTQGTEMP          = ls_UTQGTemp,
			   MUDSNOWYN         = ls_MudSnowYN,
			   SEVEREWEATHERIND  = ls_SevereWeatherInd,
			   RIMDIAMETER       = ls_RimDiameter,
			   LOADRANGE         = ls_LoadRange,
			   MEARIMWIDTH       = ls_MeaRimWidth,
			   REGROOVABLEIND    = ls_RegroovableInd,
			   PLANTPRODUCED     = ls_PlantProduced,
			   TREADPATTERN      = ls_TreadPattern,
			   NOMINALTIREWIDTH  = ls_NominalTireWidth,
			   ASPECTRATIO       = ls_AspectRatio,
			   TPN               = ls_TPN ,
			   BIASBELTEDRADIAL  = ls_BiasBeltedRadial,
			   MODIFIEDBY        = 'PRODUCTUPDATE',
			   MODIFIEDON        = SYSDATE
		WHERE  SKUID             = ln_SkuId;

	COMMIT;

		BEGIN
		  
			ls_Family := NULL;
			ls_IMark  := NULL;

			--      jeseitz 4/13/16
			SELECT COUNT(*) INTO LN_IMARKCOUNT
			FROM PRODUCT_IMARK_FAMILY PIF
			WHERE SKUID = LN_SKUID;
			
			IF LN_IMARKCOUNT > 0 THEN
			
				SELECT MAX(CERTIFICATEID) INTO LN_IMARKCERTID
				FROM PRODUCT_IMARK_FAMILY PIF
				WHERE SKUID = LN_SKUID;

				SELECT PIF.IMARK , PIF.FAMILYID
				 INTO  ls_IMark,ls_Family
				FROM PRODUCT_IMARK_FAMILY PIF
				WHERE SKUID = ln_SkuID
				AND CERTIFICATEID =  ln_imarkcertid;

				IF(ls_Family IS NULL AND ls_IMark IS NULL) THEN
				
					ls_family := BOM_ATTRIBUTES.GET_IMARK_FAMILY(LPAD(ps_Matl_Num,18,0),ln_imarkcertid);

				UPDATE PRODUCT_imark_family
				SET FAMILYID  = ls_family,
					IMARK     = ls_imark
				WHERE SKUID =  ln_SkuId 
				  AND CERTIFICATEID =  ln_imarkcertid;
				COMMIT;
				
				END IF;

			END IF;

		END;
		
		pn_ErrorNum := 1;
		ps_ErrorMsg := 'Updated successfully';
		
	EXCEPTION
		WHEN NO_DATA_FOUND THEN
			pn_ErrorNum := 0;
			ps_ErrorMsg := ' Material Number '||ps_Matl_Num ||' does not exists';
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '- Refresh_Product. An error have ocurred.(when others)';
			pn_ErrorNum := 0;
			ps_ErrorMsg := ' An error have ocurred.(when others)';
			--Insert record into exception table
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
													  ad_operatorid    => 'ICSDEV',
													  ad_daterecorded  => SYSDATE,
													  as_processname   => 'ICS_MAINTENANCE.Refresh_Product',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);

			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END Refresh_Product;
	
	PROCEDURE DeleteProductImarkFamily(pn_SkuId           IN  PRODUCTCERTIFICATE.SKUID%TYPE,
									   pn_CertificateId   IN  CERTIFICATE.CERTIFICATEID%TYPE)

	AS
	/******************************************************************************
	NAME:       DeleteProductImarkFamily
	PURPOSE:    Delete records from productImarkFamily table for given skuid and certificate

	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        04/13/2016  jeseitz         Initial Version
	******************************************************************************/
		
		ls_ErrorMsg VARCHAR2(4000):=NULL;
		
	BEGIN
		
		BEGIN

			DELETE FROM PRODUCT_IMARK_FAMILY
			WHERE SKUID = pn_SkuId
			AND CERTIFICATEID = Pn_CertificateID;

		EXCEPTION
			WHEN OTHERS THEN
				ls_ErrorMsg:=  SQLERRM || '- DeleteProductImarkFamily. An error has ocurred.(when others)';

				--Insert record into exception table
				APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
														  ad_operatorid    => 'ICSDEV',
														  ad_daterecorded  => SYSDATE,
														  as_processname   => 'ICS_MAINTENANCE.DeleteProductImarkFamily',
														  ax_recorddata    => 'certificateid ='||pn_Certificateid||' skuid ='||pn_skuid,
														  as_messagecode   => TO_CHAR(SQLCODE),
														  as_message       => ls_ErrorMsg);

				RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		END;

	END DELETEPRODUCTIMARKFAMILY;
	
	PROCEDURE GET_PRODUCT_INFO (ps_matl_num		IN     PRODUCT.MATL_NUM%TYPE,
								pc_product   	  OUT  SYS_REFCURSOR,
								pn_error_num 	  OUT  NUMBER,
								ps_error_desc 	  OUT  VARCHAR2)
	AS
	/******************************************************************************
		NAME:       GET_PRODUCT_INFO
		PURPOSE:  
		REVISIONS:
		Ver        Date        Author           Description
		---------  ----------  ---------------  ------------------------------------
		1.0                                      Initial Version  
		1.1        2/18/2013   Krishna          - Converted SERIALDATE and MOSTRECENTTESTDATE  
												columns data into MM/DD/YYYY
												- Converted RIMDIAMETER and MEARIMWIDTH    
													columns data into character
		1.2        02/21/2014   Guru            - Added TREADPATTERN column in select query.  
		1.3        09/12/2019   JESEITZ         - Moved from TRACS.ics_support package on technical database                                      
	******************************************************************************/    
 
	ln_orig_skuid	NUMBER := NULL;
	ln_skuid 		NUMBER := NULL;
	ls_errormsg 	VARCHAR2(4000) := NULL;
  
	BEGIN

		SELECT MAX(SKUID) INTO ln_skuid 
		FROM ICS.PRODUCT 
		WHERE MATL_NUM = ps_matl_num;
   
		OPEN pc_product FOR
			SELECT SKU "SKU",
				   BRAND_LINE "BrandDesc",
				   NVL(TO_CHAR(P.SERIALDATE,'MM/DD/YYYY'),'')  "SerialDate",
				   P.MFGWWYY "MFGWWYY",
				   P.DOTSERIALNUMBER   "DOTSerialNumber",
				   P.SIZESTAMP "SizeStamp",
				   P.SPEEDRATING "SpeedRating",
				   P.SINGLOADINDEX "SingLoadIndex",
				   P.DUALLOADINDEX "DualLoadIndex",
				   P.BIASBELTEDRADIAL "BiasBeltedRadial",
				   P.TUBELESSYN "Tubeless", 
				   P.REINFORCEDYN  "ReinforcedYN",
				   P.EXTRALOADYN   "ExtraLoadYN",
				   P.UTQGTREADWEAR "UTQGTreadwear",
				   P.UTQGTRACTION "UTQGTraction",
				   P.UTQGTEMP "UTQGTemp",
				   P.MUDSNOWYN "MudSnowYN",
				   P.SEVEREWEATHERIND "SevereWeatherInd",
				   NVL(TO_CHAR(P.RIMDIAMETER),'') "RimDiameter",
				   P.LOADRANGE  "LoadRange",
				   NVL(TO_CHAR(P.MEARIMWIDTH),'') "MeaRimWidth",
				   P.REGROOVABLEIND "RegroovableInd",
				   P.PLANTPRODUCED  "PlantProduced",
				   NVL(TO_CHAR(P.MOSTRECENTTESTDATE,'MM/DD/YYYY'),'') "MostRecentTestDate",
				   P.IMARK "IMark",
				   P.TPN "TechnicalPlatform",
				   P.Aspectratio  "AspectRatio",
				   P.TireTypeID "TireTypeID",
				   P.TREADPATTERN "TreadPattern"
            FROM PRODUCT P 
			WHERE P.SKUID = Ln_Skuid;
			
			pn_error_num := 0;
        
			ps_error_desc := 'Success';
     
	EXCEPTION    
  
		WHEN OTHERS THEN
    
		---Return empty dataset
		OPEN pc_product FOR 'SELECT * FROM ICS.PRODUCT WHERE SKUID = -10';
		
		--Insert record into exception table  
		ls_errormsg:= SQLERRM || '- GET_PRODUCT_INFO. An error has ocurred.(when others)';
     
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => NULL,
                                                  ad_operatorid    => 'ICSDEV',
                                                  ad_daterecorded  => SYSDATE,
                                                  as_processname   => 'ICS_MAINTENANCE.GET_PRODUCT_INFO',
                                                  ax_recorddata    => 'An error has ocurred.(when others)',
                                                  as_messagecode   => TO_CHAR(SQLCODE),
                                                  as_message       => ls_errormsg);

       RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
  
	END GET_PRODUCT_INFO;

END ICS_MAINTENANCE;
/

CREATE OR REPLACE PACKAGE REPORTS_PACKAGE AS
	/************************************************************************************************
	 package Name - Reports_Package
	 Change History
	  --------------------------------------------------------------------------
	  Version No  Date          Author    Description
	  ---------------------------------------------------------------------------
		  1.0                             Intial Version
		  1.1     10/02/2012     Krishna   - As per PRJ3617 Added
										   1.GetCertificateReportInfoBySKU
											  - Replaced ps_Sku with ps_Matl_Num
											  - Replaced ps_BrandCode with ps_Brand and ps_Brand_Line
										   2.getEceSimilarCertificates
											 - Replaced ps_in_sku with ps_Matl_Num
										   3. GetImarkSamplingAndTestsInfo
											   - Replaced ps_Sku with ps_Matl_Num
		  1.2     11/11/2013    Harini      - Added new paramter ps_IncludeArchived in GetTraceabilityReportInfo
											procedure
	*************************************************************************************************/


	TYPE retCursor IS REF CURSOR;
	
	PROCEDURE GetEmarkReportPassengerInfo(pc_Certificate            OUT RETCURSOR,
								          pc_Brand                  OUT RETCURSOR,
								          pc_Product                OUT RETCURSOR,
								          pc_CertificateDfValue     OUT RETCURSOR,
								          pc_skudetailscount        OUT RETCURSOR,
								          ps_certificateNumber    IN    VARCHAR2,
								          ps_extension            IN    VARCHAR2,
								          pi_certificationTypeID  IN    NUMBER,
								          ps_Operatorid           IN    VARCHAR2,
								          pi_TireTypeID           IN    NUMBER);
										  
	PROCEDURE GetImarkReportPassengerInfo(pc_Certificate        OUT RETCURSOR,
										  pc_Brand              OUT RETCURSOR,
										  pc_Product            OUT RETCURSOR,
										  pc_CertificateDfValue OUT RETCURSOR,
										  pc_ProdBrandList      OUT RETCURSOR);

	PROCEDURE GetImarkSamplingAndTestsInfo(pc_Certificate        OUT   RETCURSOR,
										   pc_Product            OUT   RETCURSOR,
										   pc_MeasureHdr         OUT   RETCURSOR,
										   pc_MeasureDtl         OUT   RETCURSOR,
										   pc_TreadWearHdr       OUT   RETCURSOR,
										   pc_TreadWearDtl       OUT   RETCURSOR,
										   pc_HighSpeedHdr       OUT   RETCURSOR,
										   pc_HighSpeedDtl       OUT   RETCURSOR,
										   pc_CertificateDfValue OUT   RETCURSOR,
										   ps_Matl_Num              IN VARCHAR2); -- Added as per PRJ3617

	PROCEDURE GetCCCSequentialReportInfo(pc_Certificate           OUT RETCURSOR,
										 pc_Brand                 OUT RETCURSOR,
										 pc_Product               OUT RETCURSOR,
										 pc_CertificateDfValue    OUT RETCURSOR,
										 ps_certificateNumber   IN    VARCHAR2,
										 ps_extension           IN    VARCHAR2,
										 pi_certificationTypeID IN    NUMBER,
										 ps_Operatorid          IN    VARCHAR2);

	PROCEDURE GetCCCProductDescReportInfo(pc_Certificate           OUT RETCURSOR,
										  pc_Brand                 OUT RETCURSOR,
										  pc_Product               OUT RETCURSOR,
										  pc_CertificateDfValue    OUT RETCURSOR,
										  ps_certificateNumber   IN    VARCHAR2,
										  ps_extension           IN    VARCHAR2,
										  pi_certificationTypeID IN    NUMBER,
										  ps_Operatorid          IN    VARCHAR2);


	PROCEDURE GetGSOPassengerReport(pc_Certificate            OUT RETCURSOR,
									pc_Brand                  OUT RETCURSOR,
									pc_SkuList                OUT RETCURSOR,
									pc_Product                OUT RETCURSOR,
									pc_CertificateDfValue     OUT RETCURSOR,
									pc_MeasureHDR             OUT RETCURSOR,
									pc_PlungerHDR             OUT RETCURSOR,
									pc_beadunseathdr          OUT RETCURSOR,
									pc_treadwearhdr           OUT RETCURSOR,
									pc_endurance              OUT RETCURSOR,
									pc_highspeedhdr           OUT RETCURSOR,
									ps_certificateNumber   IN     VARCHAR2,
									ps_extension           IN     VARCHAR2,
									pi_certificationTypeID IN     NUMBER,
									ps_Operatorid          IN     VARCHAR2,
									pi_TireTypeId          IN     NUMBER);

	PROCEDURE GetCertificateReportInfoBySKU(pc_Product         OUT RETCURSOR,
											pc_Certificate     OUT RETCURSOR,
											PC_TESTREFERENCE   OUT RETCURSOR,
											ps_Matl_Num      IN    VARCHAR2, -- Added as per PRJ3617
											ps_Operatorid    IN    VARCHAR2,
											ps_Brand         IN    VARCHAR2,-- Added as per PRJ3617
											ps_Brand_Line    IN    VARCHAR2,-- Added as per PRJ3617
											ps_CertType      IN    VARCHAR2);


	PROCEDURE GetImarkCertificationInfo(pc_ImarkCertification OUT   RETCURSOR,
	                                    pd_DateSearchCriteria    IN DATE) ;


	PROCEDURE GetEmarkCertificationInfo(pc_EmarkCertification  OUT RETCURSOR,
									    pc_Product             OUT RETCURSOR,
									    ps_certificateNumber IN    VARCHAR2,
									    ps_BrandCode         IN    VARCHAR2) ;

	PROCEDURE GetEmarkPassengerWithTR(ps_CertificateNumber IN    VARCHAR2,
									 pi_tiretypeid         IN    NUMBER,
									 pc_CertificateDfValue   OUT RETCURSOR,
									 pc_CertificateInfo      OUT RETCURSOR,
									 pc_Product              OUT RETCURSOR,
									 pc_MeasureHDR           OUT RETCURSOR,
									 pc_measureDtl           OUT RETCURSOR,
									 pc_BEADUNSEATHDR        OUT RETCURSOR,
									 pc_BEADUNSEATDTL        OUT RETCURSOR,
									 pc_PLUNGERHDR           OUT RETCURSOR,
									 pc_PLUNGERDTL           OUT RETCURSOR,
									 pc_TREADWEARHDR         OUT RETCURSOR,
									 pc_TREADWEARDTL         OUT RETCURSOR,
									 pc_ENDURANCEHDR         OUT RETCURSOR,
									 pc_ENDURANCEDTL         OUT RETCURSOR,
									 pc_HIGHSPEEDHDR         OUT RETCURSOR,
									 pc_HIGHSPEEDDTL         OUT RETCURSOR,
									 pc_SPEEDTESTDETAIL      OUT RETCURSOR,
									 pc_Brand                OUT RETCURSOR) ;

	PROCEDURE GetTraceabilityReportInfo(pc_Traceability          OUT RETCURSOR,
									    ps_CertificateNumber   IN    VARCHAR2,
									    pi_certificationTypeID IN    NUMBER,
									    ps_IncludeArchived     IN    VARCHAR2 );
	
	PROCEDURE GetAuthenticityReportInfo(pc_Authenticity OUT RETCURSOR);
	
	PROCEDURE GetExceptionReportInfo(pc_Exception OUT RETCURSOR);
	
	PROCEDURE CompareSKUMainProductColumns ;

	PROCEDURE GetEmark117Info(pc_Certificate           OUT RETCURSOR,
							  pc_Brand                 OUT RETCURSOR,
							  pc_Product               OUT RETCURSOR,
							  pc_CertificateDfValue    OUT RETCURSOR,
							  pc_skudetailscount       OUT RETCURSOR,
							  ps_certificateNumber   IN    VARCHAR2,
							  ps_extension           IN    VARCHAR2,
							  pi_certificationTypeID IN    NUMBER,
							  ps_Operatorid          IN    VARCHAR2,
							  pi_TireTypeID          IN    NUMBER);
	
	PROCEDURE GETEMARKTESTREPORTINFO(PS_CERTIFICATENUMBER IN    VARCHAR2,
									 PI_TIRETYPEID        IN    NUMBER,
									 PC_CERTIFICATEDFVALUE  OUT RETCURSOR,
									 PC_CERTIFICATEINFO     OUT RETCURSOR,
									 PC_PRODUCT             OUT RETCURSOR,
									 PC_MEASUREHDR          OUT RETCURSOR,
									 PC_MEASUREDTL          OUT RETCURSOR,
									 PC_BEADUNSEATHDR       OUT RETCURSOR,
									 PC_BEADUNSEATDTL       OUT RETCURSOR,
									 PC_PLUNGERHDR          OUT RETCURSOR,
									 PC_PLUNGERDTL          OUT RETCURSOR,
									 PC_TREADWEARHDR        OUT RETCURSOR,
									 PC_TREADWEARDTL        OUT RETCURSOR,
									 PC_ENDURANCEHDR        OUT RETCURSOR,
									 PC_ENDURANCEDTL        OUT RETCURSOR,
									 PC_HIGHSPEEDHDR        OUT RETCURSOR,
									 PC_HIGHSPEEDDTL        OUT RETCURSOR,
									 PC_SPEEDTESTDETAIL     OUT RETCURSOR,
									 PC_BRAND               OUT RETCURSOR) ;

	PROCEDURE GetNOMCertification(ps_CertificateNumber IN    VARCHAR2,
								  pi_tiretypeid        IN    NUMBER,
								  pc_CertificateDfValue  OUT RETCURSOR,
								  pc_CertificateInfo     OUT RETCURSOR,
								  pc_Product             OUT RETCURSOR,
								  pc_MeasureHDR          OUT RETCURSOR,
								  pc_measureDtl          OUT RETCURSOR,
								  pc_BEADUNSEATHDR       OUT RETCURSOR,
								  pc_BEADUNSEATDTL       OUT RETCURSOR,
								  pc_PLUNGERHDR          OUT RETCURSOR,
								  pc_PLUNGERDTL          OUT RETCURSOR,
								  pc_TREADWEARHDR        OUT RETCURSOR,
								  pc_TREADWEARDTL        OUT RETCURSOR,
								  pc_ENDURANCEHDR        OUT RETCURSOR,
								  pc_ENDURANCEDTL        OUT RETCURSOR,
								  pc_HIGHSPEEDHDR        OUT RETCURSOR,
								  pc_HIGHSPEEDDTL        OUT RETCURSOR,
								  pc_SPEEDTESTDETAIL     OUT RETCURSOR,
								  pc_Brand               OUT RETCURSOR);
	
	PROCEDURE GetEmarkApplicationInfo(ps_CertificateNumber  IN    VARCHAR2,
									  pi_tiretypeid         IN    NUMBER,
									  pc_CertificateDfValue   OUT RETCURSOR,
									  pc_CertificateInfo      OUT RETCURSOR,
									  pc_Product              OUT RETCURSOR,
									  pc_MeasureHDR           OUT RETCURSOR,
									  pc_HIGHSPEEDHDR         OUT RETCURSOR,
									  pc_Brand                OUT RETCURSOR) ;

	PROCEDURE GetGSOConformityReport(pc_Certificate           OUT RETCURSOR,
									 pc_Brand                 OUT RETCURSOR,
									 pc_SkuList               OUT RETCURSOR,
									 ps_BatchNumber         IN    VARCHAR2,
									 pi_certificationTypeID IN    NUMBER,
									 ps_Operatorid          IN    VARCHAR2,
									 pi_TireTypeId          IN    NUMBER) ;

    PROCEDURE getEceSimilarCertificates(ps_matl_num           IN    VARCHAR2, -- Replaced ps_in_sku with ps_matl_num as per PRJ3617
										pc_SimilarCertificates  OUT RETCURSOR);
    
	PROCEDURE SETIMARKFLAG(pi_certificateid IN NUMBER);
			 
END REPORTS_PACKAGE;
/

CREATE OR REPLACE PACKAGE BODY REPORTS_PACKAGE AS

	PROCEDURE GetEmarkReportPassengerInfo(pc_Certificate            OUT RETCURSOR,
								          pc_Brand                  OUT RETCURSOR,
								          pc_Product                OUT RETCURSOR,
								          pc_CertificateDfValue     OUT RETCURSOR,
								          pc_skudetailscount        OUT RETCURSOR,
								          ps_certificateNumber    IN    VARCHAR2,
								          ps_extension            IN    VARCHAR2,
								          pi_certificationTypeID  IN    NUMBER,
								          ps_Operatorid           IN    VARCHAR2,
								          pi_TireTypeID           IN    NUMBER)
	AS
	/************************************************************************************************
	 Procedure Name - GetEmarkReportPassengerInfo
	 Change History
	  --------------------------------------------------------------------------
	  Version No  Date          Author    Description
	  ---------------------------------------------------------------------------
		  1.0                             Intial Version
		  1.1     9/17/2012     Krishna   - As per PRJ3617 Added
											- Matl_Num wherever SKU is available in Select list of the query
											- Replaced Brand_View with Query
		  1.2     11/20/2013     Guru    - 1.Change the cursor pc_Certificate such that if extension is null or empty
										  find the current extension else take the input extension.
										  2. In pc_brand,pc_skudetailscount,pc_Product cursors add this
										  ' AND lower(extension_en) <= lower(ls_extension) ' in where clause.
	*************************************************************************************************/

		--Exception variables
		li_ParametersAreNull EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_extension         VARCHAR2(30):=NULL;
		ln_CertficateCount   NUMBER:=NULL;
		ls_MachineId         VARCHAR2(50):=NULL;
		ls_OperatorId        VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg          VARCHAR2(4000):=NULL;
	
	BEGIN
	
		IF ps_certificateNumber IS NULL OR pi_certificationTypeID IS NULL THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_certificateNumber ='' OR pi_certificationTypeID  <= 0 THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_Operatorid IS NOT NULL OR ps_Operatorid <> '' THEN
			ls_OperatorId:=ps_Operatorid;
		END IF;



		---Make sure certificate number is of correct type and tire type
		
		SELECT COUNT(*) INTO ln_CertficateCount 
		FROM CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PC ON CE.CERTIFICATEID = PC.CERTIFICATEID
			INNER JOIN PRODUCT P ON PC.SKUID = P.SKUID
		WHERE CE.CERTIFICATIONTYPEID = 1 
		  AND UPPER(CE.MOSTRECENTCERT) = 'Y' 
		  AND P.TIRETYPEID = pi_tiretypeid;
		  
		IF ln_CertficateCount > 0 THEN

			IF ps_extension IS NULL or ps_extension = '' THEN

				---find current extension
				SELECT EXTENSION_EN INTO ls_extension
				FROM CERTIFICATE CE
				WHERE LOWER(CE.CERTIFICATENUMBER)=LOWER(ps_certificateNumber) 
				  AND CE.CERTIFICATIONTYPEID=PI_CERTIFICATIONTYPEID 
				  AND LOWER(CE.MOSTRECENTCERT)='y';
			ELSE
				ls_extension := ps_extension;

			END IF;

			OPEN pc_Certificate FOR
				SELECT DISTINCT(CE.CERTIFICATENUMBER),
				                CE.EXTENSION_EN,
				                CE.SUPPLEMENTALMOLDSTAMPING_E,
								CE.CERTDATESUBMITTED,
				                CE.CERTDATEAPPROVED
				FROM CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID  
				  AND LOWER(CE.EXTENSION_EN) <= LOWER(ls_extension) ;

			-- Gets the brand information
			OPEN pc_brand FOR
				-- As per PRJ3617 added query instead of Brand_View
				SELECT *
				FROM (SELECT DISTINCT P.BRAND,
				                      P.BRAND_LINE,
									  C.CERTIFICATENUMBER,
									  C.EXTENSION_EN
					  FROM PRODUCT P,
						   PRODUCTCERTIFICATE PC,
						   CERTIFICATE C
					  WHERE C.CERTIFICATEID = PC.CERTIFICATEID
					    AND PC.SKUID = P.SKUID)
				WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber)
				AND LOWER(EXTENSION_EN) <= LOWER(ls_extension)  ;

			OPEN pc_skudetailscount FOR
				SELECT ps_certificateNumber certificateNumber, 
				       COUNT(*)             numdistinctattrib 
				FROM(SELECT DISTINCT P.SIZESTAMP, 
				                     P.SPEEDRATING, 
									 NVL(P.SINGLOADINDEX,0),
					                 NVL(P.DUALLOADINDEX,0), 
									 P.MUDSNOWYN, 
									 P.BIASBELTEDRADIAL
					 FROM PRODUCT P, 
					      PRODUCTCERTIFICATE PC, 
						  CERTIFICATE CE
					 WHERE P.SKUID = PC.SKUID
					   AND PC.CERTIFICATEID = CE.CERTIFICATEID
					   AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber)
					   AND LOWER(EXTENSION_EN) <= LOWER(ls_extension)) ;

			OPEN pc_Product FOR
				-- Added Matl_Num as per PRJ3617
				SELECT CE.CERTIFICATENUMBER,
				       EXTENSION_EN,
					   ROWNUM, 
					   P.SKUID, 
					   SKU, 
					   LPAD(P.MATL_NUM,18,0) AS MATL_NUM, 
					   P.SIZESTAMP, 
					   P.SPEEDRATING,
				       NVL(P.SINGLOADINDEX,0) SINGLOADINDEX,
				       NVL(P.DUALLOADINDEX,0)  DUALLOADINDEX, 
					   P.MUDSNOWYN, 
					   P.BIASBELTEDRADIAL,
				       P.REINFORCEDYN, 
					   P.EXTRALOADYN
				FROM PRODUCT P, 
				     PRODUCTCERTIFICATE PC, 
					 CERTIFICATE CE
				WHERE P.SKUID = PC.SKUID
				  AND PC.CERTIFICATEID = CE.CERTIFICATEID
				  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber)
				  AND LOWER(EXTENSION_EN) <= LOWER(ls_extension) ;

			--Gets the default values information.
			OPEN pc_CertificateDfValue FOR
				SELECT  CERTIFICATENUMBER,
						MAX(DECODE(FIELDID,1,FIELDVALUE,NULL))   AS MANUFACTURERNAME_E,
						MAX(DECODE(FIELDID,2,FIELDVALUE,NULL))   AS MANUFACTURERNAMEADDRESS_E,
						MAX(DECODE(FIELDID,3,FIELDVALUE,NULL))   AS TECHNICALSERVICE_E,
						MAX(DECODE(FIELDID,4,FIELDVALUE,NULL))   AS PLACE_E,
						MAX(DECODE(FIELDID,5,FIELDVALUE,NULL))   AS MEASURERIM_E,
						MAX(DECODE(FIELDID,6,FIELDVALUE,NULL))   AS INFLATIONPRESSURE_E,
						MAX(DECODE(FIELDID,7,FIELDVALUE,NULL))   AS TESTLABORATORY_E,
						MAX(DECODE(FIELDID,8,FIELDVALUE,NULL))   AS REPRESENTATIVENAME_E,
						MAX(DECODE(FIELDID,9,FIELDVALUE,NULL))   AS REPRESENTATIVEADDRESS_E,
						MAX(DECODE(FIELDID,10,FIELDVALUE,NULL))  AS REASONOFEXTENSION_E,
						MAX(DECODE(FIELDID,11,FIELDVALUE,NULL))  AS REMARKS_E,
						MAX(DECODE(FIELDID,175,FIELDVALUE,NULL)) AS PPNPROFILEFAMILY_E,
						MAX(DECODE(FIELDID,176,FIELDVALUE,NULL)) AS RIMSMOUNTED_E,
						MAX(DECODE(FIELDID,177,FIELDVALUE,NULL)) AS OVERALLDIMENSIONSTYPE_E,
						MAX(DECODE(FIELDID,178,FIELDVALUE,NULL)) AS REFERENCETIRE_E
				FROM (SELECT FIELDID,
				             CERTIFICATIONTYPEID,
				             CERTIFICATENUMBER,
				             FIELDVALUE
					  FROM DEFAULTVALUES_VIEW DV
						WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
						  AND CERTIFICATIONTYPEID = pi_certificationTypeID)
				GROUP BY CERTIFICATENUMBER;
			
		END IF;

	EXCEPTION
		WHEN LI_PARAMETERSARENULL THEN
			
			ls_ErrorMsg:=  ' There is at least one parameters null.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => ' Reports_Package.GetInfoReportPassenger',
													  ax_recorddata    => 'ps_sku is parameters null..',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);
													  
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN LI_PARAMETERSAREINVALID THEN
			
			ls_ErrorMsg:=  sqlerrm || ' There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => sysdate,
			                                          as_processname   => ' Reports_Package.GetInfoReportPassenger',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => to_char(sqlcode),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN OTHERS THEN
			
			ls_ErrorMsg:= sqlerrm ||  'An error have ocurred.(when others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   =>' Reports_Package.GetInfoReportPassenger',
													  ax_recorddata    => 'An error have ocurred.(when others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetEmarkReportPassengerInfo;

	PROCEDURE GetEmark117Info(pc_Certificate           OUT RETCURSOR,
							  pc_Brand                 OUT RETCURSOR,
							  pc_Product               OUT RETCURSOR,
							  pc_CertificateDfValue    OUT RETCURSOR,
							  pc_skudetailscount       OUT RETCURSOR,
							  ps_certificateNumber   IN    VARCHAR2,
							  ps_extension           IN    VARCHAR2,
							  pi_certificationTypeID IN    NUMBER,
							  ps_Operatorid          IN    VARCHAR2,
							  pi_TireTypeID          IN    NUMBER)
    AS
	/************************************************************************************************
	 Procedure Name - GetEmark117Info
	 Change History
	  --------------------------------------------------------------------------
	  Version No  Date          Author    Description
	  ---------------------------------------------------------------------------
		  1.0                             Intial Version
		  1.1     11/20/2013     Guru    - 1.Change the cursor pc_Certificate such that IF extension IS NULL or empty
										  find the current extension else take the input extension.
										  2. In pc_brand,pc_skudetailscount,pc_Product cursors add this
										  ' AND LOWER(extension_en) <= LOWER(ls_extension) ' in WHERE clause.
	*************************************************************************************************/
      
		--Exception variables
		li_ParametersAreNull EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);
		
		ls_extension       VARCHAR2(30):=NULL;
		ls_MachineId       VARCHAR2(50):=NULL;
		ls_OperatorId      VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg        VARCHAR2(4000):=NULL;

	BEGIN
		
		IF ps_certificateNumber IS NULL OR pi_certificationTypeID IS NULL THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_certificateNumber ='' OR pi_certificationTypeID  <= 0 THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_Operatorid IS NOT NULL OR ps_Operatorid <> '' THEN
			ls_OperatorId:=ps_Operatorid;
		END IF;

		IF ps_extension IS NULL OR ps_extension = '' THEN

			---find current extension
			SELECT EXTENSION_EN INTO ls_extension
			FROM CERTIFICATE CE
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			  AND LOWER(CE.MOSTRECENTCERT) = 'y' ;
		ELSE
			ls_extension := ps_extension;
		END IF ;

		OPEN pc_Certificate FOR
			SELECT DISTINCT(CE.CERTIFICATENUMBER),
			                CE.EXTENSION_EN,
							CE.SUPPLEMENTALMOLDSTAMPING_E,
							CE.CERTDATESUBMITTED,
							CE.CERTDATEAPPROVED
			FROM CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			      AND CE.CERTIFICATIONTYPEID = pi_certificationtypeid  
				  AND LOWER(CE.EXTENSION_EN) <= LOWER(ls_extension) ;
		

		-- Gets the brand information
		OPEN pc_brand FOR
			SELECT DISTINCT BRANDCODE, 
			                BRANDNAME, 
							CERTIFICATENUMBER
			FROM BRAND_VIEW
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
			  AND LOWER(EXTENSION_EN) <= LOWER(ls_extension)  ;

		OPEN PC_SKUDETAILSCOUNT FOR
			SELECT PS_CERTIFICATENUMBER AS CERTIFICATENUMBER, 
			       COUNT(*) AS NUMDISTINCTATTRIB 
		    FROM(SELECT DISTINCT P.SIZESTAMP, 
			                     P.SPEEDRATING, 
								 NVL(P.SINGLOADINDEX,0) ,
				                 NVL(P.DUALLOADINDEX,0), 
								 P.MUDSNOWYN, 
								 P.BIASBELTEDRADIAL
				 FROM PRODUCT P, 
				      PRODUCTCERTIFICATE PC, 
					  CERTIFICATE CE
				 WHERE P.SKUID = PC.SKUID
				   AND PC.CERTIFICATEID = CE.CERTIFICATEID
				   AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
				   AND LOWER(EXTENSION_EN) <= LOWER(ls_extension));

		Open pc_Product for
			SELECT CE.CERTIFICATENUMBER,
			       EXTENSION_EN,
				   ROWNUM, 
				   P.SKUID, 
				   SKU, 
				   P.SIZESTAMP, 
				   P.SPEEDRATING,
			       NVL(P.SINGLOADINDEX,0) AS SINGLOADINDEX,
			       NVL(P.DUALLOADINDEX,0) AS DUALLOADINDEX, 
				   P.MUDSNOWYN, 
				   P.BIASBELTEDRADIAL,
			       P.RIMDIAMETER,
			       P.MEARIMWIDTH, P.NOMINALTIREWIDTH, P.ASPECTRATIO
			FROM PRODUCT P, 
			     PRODUCTCERTIFICATE PC, 
				 CERTIFICATE CE
			WHERE P.SKUID = PC.SKUID
			  AND PC.CERTIFICATEID = CE.CERTIFICATEID
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
			  AND LOWER(EXTENSION_EN) <= LOWER(ls_extension) ;


		--Gets the default values information.
		Open pc_CertificateDfValue for
			SELECT CERTIFICATENUMBER,
			       MAX(DECODE(FIELDID,1,FIELDVALUE,NULL))      AS MANUFACTURERNAME_E,
			       MAX(DECODE(FIELDID,2,FIELDVALUE,NULL))      AS MANUFACTURERNAMEADDRESS_E,
			       MAX(DECODE(FIELDID,3,FIELDVALUE,NULL))      AS TECHNICALSERVICE_E,
			       MAX(DECODE(FIELDID,4,FIELDVALUE,NULL))      AS PLACE_E,
			       MAX(DECODE(FIELDID,5,FIELDVALUE,NULL))      AS MEASURERIM_E,
			       MAX(DECODE(FIELDID,6,FIELDVALUE,NULL))      AS INFLATIONPRESSURE_E,
			       MAX(DECODE(FIELDID,7,FIELDVALUE,NULL))      AS TESTLABORATORY_E,
			       MAX(DECODE(FIELDID,8,FIELDVALUE,NULL))      AS REPRESENTATIVENAME_E,
			       MAX(DECODE(FIELDID,9,FIELDVALUE,NULL))      AS REPRESENTATIVEADDRESS_E,
			       MAX(DECODE(FIELDID,10,FIELDVALUE,NULL))     AS REASONOFEXTENSION_E,
			       MAX(DECODE(FIELDID,11,FIELDVALUE,NULL))     AS REMARKS_E,
			       MAX(DECODE(FIELDID,165,FIELDVALUE,NULL))    AS REPRTNUMBERISSUEDBYSERVICE_E,
			       MAX(DECODE(FIELDID,166,FIELDVALUE,NULL))    AS SOUNDLEVEL_E,
			       MAX(DECODE(FIELDID,167,FIELDVALUE,NULL))    AS REFERENCESPEED_E,
			       MAX(DECODE(FIELDID,168,FIELDVALUE,NULL))    AS APPLICANTNAMEADDRESS_E,
			       MAX(DECODE(FIELDID,169,FIELDVALUE,NULL))    AS PERFORMANCECHARACTERISTICS_E,
			       MAX(DECODE(FIELDID,170,FIELDVALUE,NULL))    AS PLANTSADDRESSES_E,
			       MAX(DECODE(FIELDID,171,FIELDVALUE,NULL))    AS TIRESIZEDESIGNATIONS_E,
			       MAX(DECODE(FIELDID,172,FIELDVALUE,NULL))    AS ZONEA_E,
			       MAX(DECODE(FIELDID,173,FIELDVALUE,NULL))    AS ZONEB_E,
			       MAX(DECODE(FIELDID,174,FIELDVALUE,NULL))    AS ZONEC_E,
			       MAX(DECODE(FIELDID,175,FIELDVALUE,NULL))    AS PPNPROFILEFAMILY_E,
			       MAX(DECODE(FIELDID,176,FIELDVALUE,NULL))    AS RIMSMOUNTED_E,
			       MAX(DECODE(FIELDID,177,FIELDVALUE,NULL))    AS OVERALLDIMENSIONSTYPE_E,
			       MAX(DECODE(FIELDID,178,FIELDVALUE,NULL))    AS REFERENCETIRE_E,
			       MAX(DECODE(FIELDID,184,FIELDVALUE,NULL))    AS WETADHESION_E,
			       MAX(DECODE(FIELDID,185,FIELDVALUE,NULL))    AS SECTIONWIDTHRANGE_E,
			       MAX(DECODE(FIELDID,186,FIELDVALUE,NULL))    AS LOADRANGE_E
			FROM(SELECT FIELDID,
				        CERTIFICATIONTYPEID,
				        CERTIFICATENUMBER,
				        FIELDVALUE
				 FROM DEFAULTVALUES_VIEW DV
				 WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				    AND CERTIFICATIONTYPEID = pi_certificationTypeID)
			GROUP BY CERTIFICATENUMBER;

	EXCEPTION
		WHEN li_ParametersAreNull THEN
			
			ls_ErrorMsg:= SQLERRM ||  ' - GetEmark117Info. There is at least one parameters null.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => ' Reports_Package.GetEmark117Info',
													  ax_recorddata    => 'ps_sku is parameters null..',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
			
			ls_ErrorMsg:=  SQLERRM ||  ' - GetEmark117Info. There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetEmark117Info',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN OTHERS THEN
			
			ls_ErrorMsg:= SQLERRM ||  ' - GetEmark117Info. An error have ocurred.(when others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_OPERATORID    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetEmark117Info',
			                                          ax_recorddata    => 'An error have ocurred.(when others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
	
	END GetEmark117Info;

	PROCEDURE GetImarkReportPassengerInfo(pc_Certificate        OUT RETCURSOR,
										  pc_Brand              OUT RETCURSOR,
										  pc_Product            OUT RETCURSOR,
										  pc_CertificateDfValue OUT RETCURSOR,
										  pc_ProdBrandList      OUT RETCURSOR)
    AS
	/************************************************************************************************
	Procedure Name - GetImarkReportPassengerInfo
	Change History
	--------------------------------------------------------------------------
	Version No  Date          Author    Description
	---------------------------------------------------------------------------
		1.0                             Intial Version
		1.1     9/17/2012     Krishna   - As per PRJ3617
										  - Replaced NPRID with PSN
										  - Replaced Brand_View with Query
										  - Added Matl_Num WHERE SKU exist
										  - Added Brand, Brand_Line columns instead of Brandcode and BrandDesc
	*************************************************************************************************/

		--Exception variables
		li_ParametersAreNull EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId                 VARCHAR2(50):=NULL;
		ls_OperatorId                VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg                  VARCHAR2(4000):=NULL;
		ls_LatestImarkCertificateNum VARCHAR2(20):=NULL;
		ln_LatestImarkCertificateID  NUMBER:=NULL;
		ln_extension                 NUMBER:=NULL;
		
	BEGIN
		
		
		ln_LatestImarkCertificateID:= ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATEID();
		
		IF ln_LatestImarkCertificateID IS NULL OR ln_LatestImarkCertificateID = 0 THEN
			ln_LatestImarkCertificateID:=0;
		END IF;

		SELECT CE.EXTENSION_EN INTO ln_extension
		FROM CERTIFICATE CE
		WHERE CERTIFICATEID = ln_LatestImarkCertificateID 
		  AND CE.CERTIFICATIONTYPEID = 4 ;
		
		SELECT CE.CERTIFICATENUMBER INTO ls_LatestImarkCertificateNum
		FROM CERTIFICATE CE
		WHERE CERTIFICATEID = Ln_LatestImarkCertificateID 
		  AND CE.CERTIFICATIONTYPEID = 4 ;

		OPEN pc_Certificate FOR
			SELECT CE.CERTIFICATENUMBER,
			       CE.EXTENSION_EN,
				   ' ' AS FAMILY_I 
			FROM CERTIFICATE CE
			WHERE CERTIFICATEID = ln_latestimarkcertificateid 
			AND CE.CERTIFICATIONTYPEID = 4 ;

		-- Gets the brand information
		-- As per PRJ3617, Replaced query instead of Brand_View
		
		OPEN pc_brand FOR
			SELECT *
			FROM(SELECT DISTINCT  P.BRAND,
			                      P.BRAND_LINE,
								  C.CERTIFICATENUMBER,
								  C.EXTENSION_EN
				FROM PRODUCT P,
					 PRODUCTCERTIFICATE PC,
					 CERTIFICATE C
				WHERE C.CERTIFICATEID = PC.CERTIFICATEID
				  AND PC.SKUID = P.SKUID)
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ls_LatestImarkCertificateNum)
			  AND EXTENSION_EN = ln_extension ;

		OPEN pc_Product FOR
			SELECT P.SKUID,
				   SKU,
				   LPAD(MATL_NUM,18,0) AS MATL_NUM, -- Added as per PRJ3617
				   BRAND,       -- Added as per PRJ3617
				   BRAND_LINE,  -- Added as per PRJ3617
				   TIRETYPEID,
				   PSN, -- Added As per PRJ3617
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
				   FAMILY||' -'||FAMILY_CODE FAMILY,
				   DOTSERIALNUMBER,
				   CERTIFICATENUMBER,
				   EXTENSION_EN,
				   CASE
					WHEN  PCE.DATEREMOVED > PCE.DATESUBMITTED  THEN 'REMOVED'
					WHEN PCE.DATEASSIGNED_EGI IS NOT NULL THEN 'ADDED'
				   ELSE ' '
				   END SKU_STATUS
			FROM PRODUCT P INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
			               INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			               INNER JOIN IMARK_FAMILY IF ON P.FAMILY = IF.FAMILY_ID
			WHERE CE.CERTIFICATEID = ln_LatestImarkCertificateID
			  AND P.EMARKREFERENCE_I IS NOT NULL
			  AND ((PCE.DATESUBMITTED IS NULL AND PCE.DATEASSIGNED_EGI IS NOT NULL) OR
			       PCE.DATEASSIGNED_EGI > PCE.DATESUBMITTED OR PCE.DATEREMOVED > PCE.DATESUBMITTED);


		--Gets the default values information.
		OPEN pc_CertificateDfValue FOR
			SELECT CERTIFICATENUMBER,
			       MAX(DECODE(FIELDID,91,FIELDVALUE,NULL))  AS SUPPLIERNAME_I,
			       MAX(DECODE(FIELDID,92,FIELDVALUE,NULL))  AS COMPLETEADDRESS_I,
			       MAX(DECODE(FIELDID,93,FIELDVALUE,NULL))  AS COUNTRYOFORIGIN_I,
			       MAX(DECODE(FIELDID,94,FIELDVALUE,NULL))  AS TELEPHONE_I,
			       MAX(DECODE(FIELDID,95,FIELDVALUE,NULL))  AS FAX_I,
			       MAX(DECODE(FIELDID,96,FIELDVALUE,NULL))  AS MANUFACTURERNAME_I,
			       MAX(DECODE(FIELDID,97,FIELDVALUE,NULL))  AS TECHNICALDEVELOPMENTCENTER_I,
			       MAX(DECODE(FIELDID,98,FIELDVALUE,NULL))  AS APPLICANTNAME_I,
			       MAX(DECODE(FIELDID,99,FIELDVALUE,NULL))  AS APPLICANTTITLE_I,
			       MAX(DECODE(FIELDID,100,FIELDVALUE,NULL)) AS ASSOCIATEDPLANT_I,
			       MAX(DECODE(FIELDID,101,FIELDVALUE,NULL)) AS OTHERASPECTS_I
			FROM(SELECT FIELDID,
						CERTIFICATIONTYPEID,
						CERTIFICATENUMBER,
						FIELDVALUE
				 FROM DEFAULTVALUES_VIEW
				 WHERE LOWER(certificatenumber) = LOWER(ls_LatestImarkCertificateNum) 
				   AND CERTIFICATIONTYPEID = 4)
			GROUP BY CERTIFICATENUMBER ;

		--Added Brand, Brand_Line instead of BrandDesc as per PRJ3617
		OPEN pc_ProdBrandList FOR
			SELECT SIZESTAMP,
			       BRAND, -- Added as per PRJ3617
			       BRAND_LINE,-- Added as per PRJ3617
			       SPEEDRATING,
			       SINGLOADINDEX,
			       DUALLOADINDEX,
			       CE.CERTIFICATENUMBER,
			       CE.CERTIFICATIONTYPEID
			FROM PRODUCT P INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
						   INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER)= LOWER(ls_LatestImarkCertificateNum)
			  AND CE.EXTENSION_EN =  ln_extension
			  AND CE.CERTIFICATIONTYPEID = 4 ;

		---SET THE IMARKFLAG on the product certificate table so that we know they
		---printed the report at least once.  When the date submitted is set on the certificate,
		---it only populates down to the product certificate records of the IMARKFLAG is set.
		
		SETIMARKFLAG(ln_LatestImarkCertificateID);

	EXCEPTION
		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  'An error have ocurred.(when others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetIMarkReportPassengerInfo',
			                                          ax_recorddata    => 'An error have ocurred.(when others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetImarkReportPassengerInfo;


	PROCEDURE GetImarkSamplingAndTestsInfo(pc_Certificate        OUT   RETCURSOR,
										   pc_Product            OUT   RETCURSOR,
										   pc_MeasureHdr         OUT   RETCURSOR,
										   pc_MeasureDtl         OUT   RETCURSOR,
										   pc_TreadWearHdr       OUT   RETCURSOR,
										   pc_TreadWearDtl       OUT   RETCURSOR,
										   pc_HighSpeedHdr       OUT   RETCURSOR,
										   pc_HighSpeedDtl       OUT   RETCURSOR,
										   pc_CertificateDfValue OUT   RETCURSOR,
										   ps_Matl_Num              IN VARCHAR2) -- Added as per PRJ3617
    AS
    /************************************************************************************************
     Procedure Name - GetImarkSamplingAndTestsInfo
     Change History
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - As per PRJ3617
                                            - Replaced NPRID with PSN
                                            - Added ps_Matl_Num instead of ps_sku
                                            - Added Matl_Num WHERE Sku exist
                                            - Added Brand, Brand_Line columns instead of Brandcode and BrandDesc
    *************************************************************************************************/

		--Exception variables
		li_ParametersAreNull EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		li_MeasureId        MEASUREHDR.MEASUREID%TYPE:=NULL;
		li_TreadWearId      TREADWEARHDR.TREADWEARID%TYPE:=NULL;
		li_HighSpeedId      HIGHSPEEDHDR.HIGHSPEEDID%TYPE:=NULL;

		ls_MachineId                 VARCHAR2(50):=NULL;
		ls_OperatorId                VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg                  VARCHAR2(4000):=NULL;
		ls_LatestImarkCertificateNum VARCHAR2(20):=NULL;
		ls_LatestImarkCertificateID  NUMBER:=NULL;
	  
    BEGIN

		--IF ps_sku IS NULL  then
		IF ps_Matl_Num IS NULL  THEN
			RAISE li_ParametersAreNull ;
		END IF;

		ls_LatestImarkCertificateID := ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATEID();
		
		IF  ls_LatestImarkCertificateID = 0 OR ls_LatestImarkCertificateID IS NULL THEN
			ls_LatestImarkCertificateNum:='NotFound';
		ELSE

			-- Gets Certificate information
			OPEN pc_Certificate FOR
				SELECT CE.CERTIFICATENUMBER,
				       CE.EXTENSION_EN
				FROM CERTIFICATE CE
				WHERE CERTIFICATEID =ls_LatestImarkCertificateID 
				  AND CE.CERTIFICATIONTYPEID = 4 ;

			SELECT CE.CERTIFICATENUMBER INTO ls_LatestImarkCertificateNum
			FROM CERTIFICATE CE
			WHERE CERTIFICATEID =ls_LatestImarkCertificateID 
			  AND CE.CERTIFICATIONTYPEID = 4 ;

			-- Gets the Product information

			OPEN pc_product FOR
				SELECT P.SKUID,
					   SKU,
					   LPAD(MATL_NUM,18,0) AS MAtl_num, -- ADDED AS PER PRJ3617
					   BRAND, -- ADDED AS PER PRJ3617
					   BRAND_LINE, -- ADDED AS Per PRJ3617
					   TIRETYPEID,
					   PSN, -- ADDED AS PER PRJ3617
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
					   CE.CERTIFICATENUMBER,
					   CE.EXTENSION_EN
				FROM PRODUCT P INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
							   INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATEID =ls_LatestImarkCertificateID  
				  AND UPPER(P.MATL_NUM) =UPPER(LPAD(ps_Matl_Num,18,0)); -- Added as per PRJ3617



			--Gets MeasureHdr information.
			OPEN pc_measurehdr FOR
				SELECT MEASUREID     AS Mea_ID,
				       PROJECTNUMBER AS PROJECTNUM,
				       TIRENUMBER    AS TIRENUM,
				       TESTSPEC      AS TESTSPEC,
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
				       m.CERTIFICATIONTYPEID,
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
				       SKU,
				       LPAD(MATL_NUM,18,0) AS MATL_NUM -- Added as per PRJ3617
				FROM  CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATEID =ls_LatestImarkCertificateID  
				  AND UPPER(M.MATL_NUM) =UPPER(LPAD(ps_Matl_Num,18,0)); -- Added as per PRJ3617

			BEGIN
				-- Get MeasureId
				li_MeasureId:=0;
				
				SELECT MEASUREID INTO li_MeasureId
				FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATEID =ls_LatestImarkCertificateID  
				  AND UPPER(M.MATL_NUM) =upper(LPAD(ps_Matl_Num,18,0)); -- Added as per PRJ3617

			EXCEPTION
				WHEN OTHERS THEN
					li_MeasureId:=0;
			END;


			--Gets MeasureDtl information.
			OPEN pc_MeasureDtl FOR
			SELECT SECTIONWIDTH, 
			       OVERALLWIDTH, 
				   MEASUREID AS Mea_ID,Iteration
			FROM  MEASUREDTL MD
			WHERE MD.MEASUREID = li_MeasureId;

			--Gets TreadWearHdr information.
			OPEN pc_TreadWearHdr FOR
				SELECT  TREADWEARID   AS TW_ID,
						PROJECTNUMBER AS ProjectNum,
						TIRENUMBER    AS TireNum,
						TESTSPEC      AS TestSpec,
						COMPLETIONDATE,
						DOTSERIALNUMBER,
						LOWESTWEARBAR,
						PASSYN,
						CE.CERTIFICATIONTYPEID,
						CE.CERTIFICATENUMBER,
						SERIALDATE,
						INDICATORSREQUIREMENT,
						SKU,
						LPAD(MATL_NUM,18,0) AS MATL_NUM--Added as per PRJ3617
				FROM  CERTIFICATE CE INNER JOIN  TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATEID =ls_LatestImarkCertificateID  
				  AND UPPER(T.MATL_NUM) =UPPER(LPAD(ps_Matl_Num,18,0)); -- Added as per PRJ3617

			BEGIN
				--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
				li_TreadWearId:=0;
				
				SELECT TREADWEARID INTO li_TreadWearId
				FROM  CERTIFICATE CE INNER JOIN  TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATEID =ls_LatestImarkCertificateID  
				  AND UPPER(T.MATL_NUM) =UPPER(LPAD(ps_Matl_Num,18,0)); -- Added as per PRJ3617

			EXCEPTION
				WHEN OTHERS THEN
					li_TreadWearId:=0;
			END;

			--Gets TreadWearDtl information.
			OPEN pc_TreadWearDtl FOR
				SELECT  TREADWEARID AS TW_ID, 
				        WEARBARHEIGHT ,
						ITERATION
				FROM  TREADWEARDTL td
				WHERE TD.TREADWEARID = li_TreadWearId;

			--Gets HighSpeedHdr information.
			OPEN pc_HighSpeedHdr FOR
				SELECT HIGHSPEEDID        AS HS_ID,
				       PROJECTNUMBER      AS ProjectNum,
				       TIRENUM            AS TireNum,
				       TESTSPEC           AS TestSpec,
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
				       ce.CERTIFICATIONTYPEID,
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
				       MAXLOAD,
				       SKU,
				       LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added as per PRJ3617
				FROM CERTIFICATE CE INNER JOIN  HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATEID =ls_LatestImarkCertificateID  
				  AND UPPER(H.MATL_NUM) =UPPER(LPAD(ps_Matl_Num,18,0)) -- Added as per PRJ3617
				  AND ce.certificationtypeid = 4;

			BEGIN
			
				--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
				li_HighSpeedId:= 0;
				
				SELECT HIGHSPEEDID INTO li_HighSpeedId
				FROM  CERTIFICATE CE INNER JOIN  HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID
				  AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID AND CE.CERTIFICATEID =ls_LatestImarkCertificateID
				  AND UPPER(H.MATL_NUM) =UPPER(LPAD(ps_Matl_Num,18,0)); -- Added as per PRJ3617

			EXCEPTION
				WHEN OTHERS THEN
					li_HighSpeedId:= 0;
			END;

			--Gets HighSpeedDtl information.
			OPEN pc_HighSpeedDtl FOR
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
				       HIGHSPEEDID  AS HS_ID
				FROM HIGHSPEEDDTL H
				WHERE H.HIGHSPEEDID = li_HighSpeedId;

				--Gets the default values information.
				OPen pc_CertificateDfValue for
					SELECT CERTIFICATENUMBER,
					       MAX(DECODE(FIELDID,102,FIELDVALUE,NULL)) AS SAMPLINGDATE_I,
					       MAX(DECODE(FIELDID,103,FIELDVALUE,NULL)) AS CUSTOMERDATA_I,
					       MAX(DECODE(FIELDID,104,FIELDVALUE,NULL)) AS SAMPLINGOBJECTIVE_I,
					       MAX(DECODE(FIELDID,105,FIELDVALUE,NULL)) AS SAMPLINGLOCATION_I,
					       MAX(DECODE(FIELDID,106,FIELDVALUE,NULL)) AS SAMPLINGLOCATIONCONDITION_I,
					       MAX(DECODE(FIELDID,107,FIELDVALUE,NULL)) AS SAMPLINGLOCATIONCOMMENTS_I,
					       MAX(DECODE(FIELDID,108,FIELDVALUE,NULL)) AS STORAGELOCATIONCONDITION_I,
					       MAX(DECODE(FIELDID,109,FIELDVALUE,NULL)) AS STORAGELOCATIONCOMMENTS_I,
					       MAX(DECODE(FIELDID,110,FIELDVALUE,NULL)) AS REFERENCESTANDARD_I,
					       MAX(DECODE(FIELDID,111,FIELDVALUE,NULL)) AS SAMPLINGOBSERVATION_I,
					       MAX(DECODE(FIELDID,112,FIELDVALUE,NULL)) AS TESTRESULTSOBSERVATION_I
					FROM (SELECT FIELDID,
								 CERTIFICATIONTYPEID,
								 CERTIFICATENUMBER,
								 FIELDVALUE
						  FROM DEFAULTVALUES_VIEW
						  WHERE CERTIFICATENUMBER =ls_LatestImarkCertificateNum
							AND CERTIFICATIONTYPEID = 4)
					GROUP BY CERTIFICATENUMBER ;

		END IF;

	EXCEPTION
		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  'An error has ocurred.(when others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetIMarkReportPassengerInfo',
			                                          ax_recorddata    => 'An error has ocurred.(when others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	end GetImarkSamplingAndTestsInfo;

	PROCEDURE GetCCCSequentialReportInfo(pc_Certificate           OUT RETCURSOR,
										 pc_Brand                 OUT RETCURSOR,
										 pc_Product               OUT RETCURSOR,
										 pc_CertificateDfValue    OUT RETCURSOR,
										 ps_certificateNumber   IN    VARCHAR2,
										 ps_extension           IN    VARCHAR2,
										 pi_certificationTypeID IN    NUMBER,
										 ps_Operatorid          IN    VARCHAR2)
	AS
	/************************************************************************************************
	Procedure Name - GetCCCSequentialReportInfo
	Change History
	--------------------------------------------------------------------------
	Version No  Date          Author    Description
	---------------------------------------------------------------------------
	  1.0                             Intial Version
	  1.1     10/18/2012     Krishna   - As per PRJ3617
										- Replaced BRND_VIEW with query
	*************************************************************************************************/

		--Exception variables
		li_ParametersAreNull EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the exception to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId     VARCHAR2(50):=NULL;
		ls_OperatorId    VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg      VARCHAR2(4000):=NULL;
		
	BEGIN
	
		IF ps_certificateNumber IS NULL OR pi_certificationTypeID IS NULL THEN
			RAISE li_ParametersAreNull ;
		end IF;

		IF ps_certificateNumber ='' OR pi_certificationTypeID  <= 0 THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_Operatorid IS NOT NULL OR ps_Operatorid <> '' THEN
			ls_OperatorId:=ps_Operatorid;
		END IF;

		IF ps_extension IS NULL OR ps_extension = '' THEN

			OPEN pc_Certificate FOR
				SELECT DISTINCT(CE.CERTIFICATENUMBER),
				                CE.EXTENSION_EN
				FROM CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID;
		
		ELSE
			
			OPEN pc_Certificate FOR
				SELECT DISTINCT(CE.CERTIFICATENUMBER),
				                CE.EXTENSION_EN
				FROM CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID  And
				LOWER(CE.EXTENSION_EN) = LOWER(ps_extension);
			
		END IF ;

		-- Gets the brand information
		-- Replaced Brand_View with Query as per PRJ3617
		
		OPEN pc_brand FOR
			SELECT *
			FROM (SELECT DISTINCT P.BRAND,
			                      P.BRAND_LINE,
								  C.CERTIFICATENUMBER,
								  C.EXTENSION_EN
			FROM PRODUCT P,
			     PRODUCTCERTIFICATE PC,
			      CERTIFICATE C
			WHERE C.CERTIFICATEID = PC.CERTIFICATEID
			  AND PC.SKUID = P.SKUID)
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);

		-- Gets the pc_Product information
		OPEN pc_Product FOR
			SELECT  *
			FROM  PRODUCTDATA_REPORT_VIEW
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);

		--Gets the default values information.
		OPen pc_CertificateDfValue for
			SELECT CERTIFICATENUMBER,
			       MAX(DECODE(FIELDID,12,FIELDVALUE,NULL)) AS PRODUCTCATEGORY_C,
			       MAX(DECODE(FIELDID,13,FIELDVALUE,NULL)) AS APPLICATION_C,
			       MAX(DECODE(FIELDID,14,FIELDVALUE,NULL)) AS TOPIC_C,
                   
			       MAX(DECODE(FIELDID,15,FIELDVALUE,NULL)) AS APPLICANTNATIONALITY_C,
			       MAX(DECODE(FIELDID,16,FIELDVALUE,NULL)) AS APPLICANTCOMPANYNAMECHINESE_C,
			       MAX(DECODE(FIELDID,17,FIELDVALUE,NULL)) AS APPLICANTCOMPANYNAMEENGLISH_C,
			       MAX(DECODE(FIELDID,18,FIELDVALUE,NULL)) AS APPLICANTPAYERNAME_C,
			       MAX(DECODE(FIELDID,19,FIELDVALUE,NULL)) AS APPLICANTPAYERADDRESS_C,
			       MAX(DECODE(FIELDID,20,FIELDVALUE,NULL)) AS APPLICANTADDRESSCHINESE_C,
			       MAX(DECODE(FIELDID,21,FIELDVALUE,NULL)) AS APPLICANTADDRESSENGLISH_C,
			       MAX(DECODE(FIELDID,22,FIELDVALUE,NULL)) AS APPLICANTORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,23,FIELDVALUE,NULL)) AS APPLICANTPOSTCODE_C,
			       MAX(DECODE(FIELDID,24,FIELDVALUE,NULL)) AS APPLICANTPERSONTOBECONTACT_C,
			       MAX(DECODE(FIELDID,25,FIELDVALUE,NULL)) AS APPLICANTCONTACTPERSON_C,
			       MAX(DECODE(FIELDID,26,FIELDVALUE,NULL)) AS APPLICANTTELEPHONE_C,
			       MAX(DECODE(FIELDID,27,FIELDVALUE,NULL)) AS APPLICANTFAX_C,
			       MAX(DECODE(FIELDID,28,FIELDVALUE,NULL)) AS APPLICANTEMAIL_C,
			       MAX(DECODE(FIELDID,29,FIELDVALUE,NULL)) AS APPLICANTMOBILEPHONE_C,
                   
			       MAX(DECODE(FIELDID,30,FIELDVALUE,NULL)) AS AGENCYNATIONALITY_C,
			       MAX(DECODE(FIELDID,31,FIELDVALUE,NULL)) AS AGENCYPROVINCE_C,
			       MAX(DECODE(FIELDID,32,FIELDVALUE,NULL)) AS AGENCYCITY_C,
			       MAX(DECODE(FIELDID,33,FIELDVALUE,NULL)) AS AGENCYCOUNTY_C,
			       MAX(DECODE(FIELDID,34,FIELDVALUE,NULL)) AS AGENCYRCOMPANYNAME_C,
			       MAX(DECODE(FIELDID,35,FIELDVALUE,NULL)) AS AGENCYRCOMPANYADDRESS_C,
			       MAX(DECODE(FIELDID,36,FIELDVALUE,NULL)) AS AGENCYORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,37,FIELDVALUE,NULL)) AS AGENCYAPPROVALNUMBER_C,
			       MAX(DECODE(FIELDID,38,FIELDVALUE,NULL)) AS AGENCYPOSTCODE_C,
			       MAX(DECODE(FIELDID,39,FIELDVALUE,NULL)) AS AGENCYCONTACTPERSON_C,
			       MAX(DECODE(FIELDID,40,FIELDVALUE,NULL)) AS AGENCYEMAIL_C,
			       MAX(DECODE(FIELDID,41,FIELDVALUE,NULL)) AS AGENCYTELEPHONE_C,
			       MAX(DECODE(FIELDID,42,FIELDVALUE,NULL)) AS AGENCYFAX_C,
			       MAX(DECODE(FIELDID,43,FIELDVALUE,NULL)) AS AGENCYMOBILE_C,
                   
			       MAX(DECODE(FIELDID,44,FIELDVALUE,NULL)) AS MANUFACTURESAMEASAPPLICANT_C,
			       MAX(DECODE(FIELDID,45,FIELDVALUE,NULL)) AS MANUFACTURENATIONALITY_C,
			       MAX(DECODE(FIELDID,46,FIELDVALUE,NULL)) AS MANUFACTURECOMPANYNAMECH_C,
			       MAX(DECODE(FIELDID,47,FIELDVALUE,NULL)) AS MANUFACTURECOMPANYNAMEEN_C,
			       MAX(DECODE(FIELDID,48,FIELDVALUE,NULL)) AS MANUFACTUREADDRESSCHINESE_C,
			       MAX(DECODE(FIELDID,49,FIELDVALUE,NULL)) AS MANUFACTUREADDRESSENGLISH_C,
			       MAX(DECODE(FIELDID,50,FIELDVALUE,NULL)) AS MANUFACTUREORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,51,FIELDVALUE,NULL)) AS MANUFACTUREPOSTCODE_C,
			       MAX(DECODE(FIELDID,52,FIELDVALUE,NULL)) AS MANUFACTUREPERSONTOBECONTACT_C,
			       MAX(DECODE(FIELDID,53,FIELDVALUE,NULL)) AS MANUFACTURECONTACTPERSON_C,
			       MAX(DECODE(FIELDID,54,FIELDVALUE,NULL)) AS MANUFACTURETELEPHONE_C,
			       MAX(DECODE(FIELDID,55,FIELDVALUE,NULL)) AS MANUFACTUREFAX_C,
			       MAX(DECODE(FIELDID,56,FIELDVALUE,NULL)) AS MANUFACTUREEMAIL_C,
			       MAX(DECODE(FIELDID,57,FIELDVALUE,NULL)) AS MANUFACTUREMOBILEPHONE_C,
                   
			       MAX(DECODE(FIELDID,58,FIELDVALUE,NULL)) AS FACTORYSAMEASAPPLICANT_C,
			       MAX(DECODE(FIELDID,59,FIELDVALUE,NULL)) AS FACTORYSAMEASMANUFACTURER_C,
			       MAX(DECODE(FIELDID,60,FIELDVALUE,NULL)) AS FACTORYNATIONALITY_C,
			       MAX(DECODE(FIELDID,61,FIELDVALUE,NULL)) AS FACTORYNAMECHINESE_C,
			       MAX(DECODE(FIELDID,62,FIELDVALUE,NULL)) AS FACTORYNAMEENGLISH_C,
			       MAX(DECODE(FIELDID,63,FIELDVALUE,NULL)) AS FACTORYADDRESSCHINESE_C,
			       MAX(DECODE(FIELDID,64,FIELDVALUE,NULL)) AS FACTORYADDRESSENGLISH_C,
			       MAX(DECODE(FIELDID,65,FIELDVALUE,NULL)) AS FACTORYORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,66,FIELDVALUE,NULL)) AS FACTORYNUMBER_C,
			       MAX(DECODE(FIELDID,67,FIELDVALUE,NULL)) AS FACTORYPOSTCODE_C,
			       MAX(DECODE(FIELDID,68,FIELDVALUE,NULL)) AS FACTORYCONTACTPERSONCHINESE_C,
			       MAX(DECODE(FIELDID,69,FIELDVALUE,NULL)) AS FACTORYCONTACTPERSONENGLISH_C,
			       MAX(DECODE(FIELDID,70,FIELDVALUE,NULL)) AS FACTORYEMAIL_C,
			       MAX(DECODE(FIELDID,71,FIELDVALUE,NULL)) AS FACTORYTELEPHONE_C,
			       MAX(DECODE(FIELDID,72,FIELDVALUE,NULL)) AS FACTORYFAX_C,
			       MAX(DECODE(FIELDID,73,FIELDVALUE,NULL)) AS FACTORYMOBILE_C,
                   
			       MAX(DECODE(FIELDID,74,FIELDVALUE,NULL)) AS REMARK_C,
			       MAX(DECODE(FIELDID,75,FIELDVALUE,NULL)) AS GBSAFETYSTANDARDNUMBER_C,
			       MAX(DECODE(FIELDID,76,FIELDVALUE,NULL)) AS GBEMCSTANDARDNUMBER_C,
			       MAX(DECODE(FIELDID,77,FIELDVALUE,NULL)) AS CBTESTCERTIFICATEYN_C,
			       MAX(DECODE(FIELDID,78,FIELDVALUE,NULL)) AS CBCERTIFICATENUMBER_C,
			       MAX(DECODE(FIELDID,79,FIELDVALUE,NULL)) AS CBCERTIFICATEISSUEDDATE_C,
			       MAX(DECODE(FIELDID,80,FIELDVALUE,NULL)) AS CBCERTIFICATENBCNAME_C,
			       MAX(DECODE(FIELDID,81,FIELDVALUE,NULL)) AS CCCCERTIFIACTENUMBER_C,
			       MAX(DECODE(FIELDID,82,FIELDVALUE,NULL)) AS CERTIFICATEMODEL_C
			FROM (SELECT FIELDID,
						 CERTIFICATIONTYPEID,
						 CERTIFICATENUMBER,
						 FIELDVALUE
						FROM DEFAULTVALUES_VIEW DV
						WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
						  AND CERTIFICATIONTYPEID = pi_certificationTypeID)
			GROUP BY CERTIFICATENUMBER;

	EXCEPTION
		WHEN li_ParametersAreNull THEN
			
			ls_ErrorMsg:=  ' There is at least one parameters null.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetInfoReportPassenger',
			                                          ax_recorddata    => 'ps_sku is parameters null..',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
			
			ls_ErrorMsg:=  SQLERRM || ' There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetInfoReportPassenger',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  'An error have ocurred.(when others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetInfoReportPassenger',
			                                          ax_recorddata    => 'An error have ocurred.(when others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetCCCSequentialReportInfo;

	PROCEDURE GetCCCProductDescReportInfo(pc_Certificate           OUT RETCURSOR,
										  pc_Brand                 OUT RETCURSOR,
										  pc_Product               OUT RETCURSOR,
										  pc_CertificateDfValue    OUT RETCURSOR,
										  ps_certificateNumber   IN    VARCHAR2,
										  ps_extension           IN    VARCHAR2,
										  pi_certificationTypeID IN    NUMBER,
										  ps_Operatorid          IN    VARCHAR2)
	AS
	
		--Exception variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId  VARCHAR2(50):=NULL;
		ls_OperatorId VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg   VARCHAR2(4000):=NULL;
	
	BEGIN
	
		IF ps_certificateNumber IS NULL OR pi_certificationTypeID IS NULL THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_certificateNumber ='' OR pi_certificationTypeID  <= 0 THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_Operatorid IS NOT NULL OR ps_Operatorid <> '' THEN
			ls_OperatorId:=ps_Operatorid;
		END IF;

		IF ps_extension IS NULL OR ps_extension = '' THEN

			OPEN pc_Certificate FOR
				SELECT DISTINCT(CE.CERTIFICATENUMBER),
				                CE.EXTENSION_EN
				FROM  CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID= PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID;
				
		ELSE
			
			OPEN pc_Certificate FOR
				SELECT DISTINCT(CE.CERTIFICATENUMBER),
				                CE.EXTENSION_EN
				FROM CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID= PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				AND CE.CERTIFICATIONTYPEID = PI_CERTIFICATIONTYPEID  
				AND LOWER(CE.EXTENSION_EN) = LOWER(ps_extension);
			
		END IF ;

		-- Gets the brand information
		OPEN pc_brand FOR
			SELECT *
			FROM  BRAND_VIEW
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber)  ;

		-- Gets the pc_Product information
		OPEN pc_Product FOR
			SELECT  ROWNUM AS "NO", CRV.*
			FROM  CCCPRODUCTDESC_REPORT_VIEW CRV
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);

		--Gets the default values information.
		OPEN pc_CertificateDfValue FOR
			SELECT CERTIFICATENUMBER,
			       MAX(DECODE(FIELDID,12,FIELDVALUE,NULL)) AS PRODUCTCATEGORY_C,
			       MAX(DECODE(FIELDID,13,FIELDVALUE,NULL)) AS APPLICATION_C,
			       MAX(DECODE(FIELDID,14,FIELDVALUE,NULL)) AS TOPIC_C,
                   
			       MAX(DECODE(FIELDID,15,FIELDVALUE,NULL)) AS APPLICANTNATIONALITY_C,
			       MAX(DECODE(FIELDID,16,FIELDVALUE,NULL)) AS APPLICANTCOMPANYNAMECHINESE_C,
			       MAX(DECODE(FIELDID,17,FIELDVALUE,NULL)) AS APPLICANTCOMPANYNAMEENGLISH_C,
			       MAX(DECODE(FIELDID,18,FIELDVALUE,NULL)) AS APPLICANTPAYERNAME_C,
			       MAX(DECODE(FIELDID,19,FIELDVALUE,NULL)) AS APPLICANTPAYERADDRESS_C,
			       MAX(DECODE(FIELDID,20,FIELDVALUE,NULL)) AS APPLICANTADDRESSCHINESE_C,
			       MAX(DECODE(FIELDID,21,FIELDVALUE,NULL)) AS APPLICANTADDRESSENGLISH_C,
			       MAX(DECODE(FIELDID,22,FIELDVALUE,NULL)) AS APPLICANTORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,23,FIELDVALUE,NULL)) AS APPLICANTPOSTCODE_C,
			       MAX(DECODE(FIELDID,24,FIELDVALUE,NULL)) AS APPLICANTPERSONTOBECONTACT_C,
			       MAX(DECODE(FIELDID,25,FIELDVALUE,NULL)) AS APPLICANTCONTACTPERSON_C,
			       MAX(DECODE(FIELDID,26,FIELDVALUE,NULL)) AS APPLICANTTELEPHONE_C,
			       MAX(DECODE(FIELDID,27,FIELDVALUE,NULL)) AS APPLICANTFAX_C,
			       MAX(DECODE(FIELDID,28,FIELDVALUE,NULL)) AS APPLICANTEMAIL_C,
			       MAX(DECODE(FIELDID,29,FIELDVALUE,NULL)) AS APPLICANTMOBILEPHONE_C,
                   
			       MAX(DECODE(FIELDID,30,FIELDVALUE,NULL)) AS AGENCYNATIONALITY_C,
			       MAX(DECODE(FIELDID,31,FIELDVALUE,NULL)) AS AGENCYPROVINCE_C,
			       MAX(DECODE(FIELDID,32,FIELDVALUE,NULL)) AS AGENCYCITY_C,
			       MAX(DECODE(FIELDID,33,FIELDVALUE,NULL)) AS AGENCYCOUNTY_C,
			       MAX(DECODE(FIELDID,34,FIELDVALUE,NULL)) AS AGENCYRCOMPANYNAME_C,
			       MAX(DECODE(FIELDID,35,FIELDVALUE,NULL)) AS AGENCYRCOMPANYADDRESS_C,
			       MAX(DECODE(FIELDID,36,FIELDVALUE,NULL)) AS AGENCYORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,37,FIELDVALUE,NULL)) AS AGENCYAPPROVALNUMBER_C,
			       MAX(DECODE(FIELDID,38,FIELDVALUE,NULL)) AS AGENCYPOSTCODE_C,
			       MAX(DECODE(FIELDID,39,FIELDVALUE,NULL)) AS AGENCYCONTACTPERSON_C,
			       MAX(DECODE(FIELDID,40,FIELDVALUE,NULL)) AS AGENCYEMAIL_C,
			       MAX(DECODE(FIELDID,41,FIELDVALUE,NULL)) AS AGENCYTELEPHONE_C,
			       MAX(DECODE(FIELDID,42,FIELDVALUE,NULL)) AS AGENCYFAX_C,
			       MAX(DECODE(FIELDID,43,FIELDVALUE,NULL)) AS AGENCYMOBILE_C,
                   
			       MAX(DECODE(FIELDID,44,FIELDVALUE,NULL)) AS MANUFACTURESAMEASAPPLICANT_C,
			       MAX(DECODE(FIELDID,45,FIELDVALUE,NULL)) AS MANUFACTURENATIONALITY_C,
			       MAX(DECODE(FIELDID,46,FIELDVALUE,NULL)) AS MANUFACTURECOMPANYNAMECH_C,
			       MAX(DECODE(FIELDID,47,FIELDVALUE,NULL)) AS MANUFACTURECOMPANYNAMEEN_C,
			       MAX(DECODE(FIELDID,48,FIELDVALUE,NULL)) AS MANUFACTUREADDRESSCHINESE_C,
			       MAX(DECODE(FIELDID,49,FIELDVALUE,NULL)) AS MANUFACTUREADDRESSENGLISH_C,
			       MAX(DECODE(FIELDID,50,FIELDVALUE,NULL)) AS MANUFACTUREORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,51,FIELDVALUE,NULL)) AS MANUFACTUREPOSTCODE_C,
			       MAX(DECODE(FIELDID,52,FIELDVALUE,NULL)) AS MANUFACTUREPERSONTOBECONTACT_C,
			       MAX(DECODE(FIELDID,53,FIELDVALUE,NULL)) AS MANUFACTURECONTACTPERSON_C,
			       MAX(DECODE(FIELDID,54,FIELDVALUE,NULL)) AS MANUFACTURETELEPHONE_C,
			       MAX(DECODE(FIELDID,55,FIELDVALUE,NULL)) AS MANUFACTUREFAX_C,
			       MAX(DECODE(FIELDID,56,FIELDVALUE,NULL)) AS MANUFACTUREEMAIL_C,
			       MAX(DECODE(FIELDID,57,FIELDVALUE,NULL)) AS MANUFACTUREMOBILEPHONE_C,
                   
			       MAX(DECODE(FIELDID,58,FIELDVALUE,NULL)) AS FACTORYSAMEASAPPLICANT_C,
			       MAX(DECODE(FIELDID,59,FIELDVALUE,NULL)) AS FACTORYSAMEASMANUFACTURER_C,
			       MAX(DECODE(FIELDID,60,FIELDVALUE,NULL)) AS FACTORYNATIONALITY_C,
			       MAX(DECODE(FIELDID,61,FIELDVALUE,NULL)) AS FACTORYNAMECHINESE_C,
			       MAX(DECODE(FIELDID,62,FIELDVALUE,NULL)) AS FACTORYNAMEENGLISH_C,
			       MAX(DECODE(FIELDID,63,FIELDVALUE,NULL)) AS FACTORYADDRESSCHINESE_C,
			       MAX(DECODE(FIELDID,64,FIELDVALUE,NULL)) AS FACTORYADDRESSENGLISH_C,
			       MAX(DECODE(FIELDID,65,FIELDVALUE,NULL)) AS FACTORYORGNIZATIONCODE_C,
			       MAX(DECODE(FIELDID,66,FIELDVALUE,NULL)) AS FACTORYNUMBER_C,
			       MAX(DECODE(FIELDID,67,FIELDVALUE,NULL)) AS FACTORYPOSTCODE_C,
			       MAX(DECODE(FIELDID,68,FIELDVALUE,NULL)) AS FACTORYCONTACTPERSONCHINESE_C,
			       MAX(DECODE(FIELDID,69,FIELDVALUE,NULL)) AS FACTORYCONTACTPERSONENGLISH_C,
			       MAX(DECODE(FIELDID,70,FIELDVALUE,NULL)) AS FACTORYEMAIL_C,
			       MAX(DECODE(FIELDID,71,FIELDVALUE,NULL)) AS FACTORYTELEPHONE_C,
			       MAX(DECODE(FIELDID,72,FIELDVALUE,NULL)) AS FACTORYFAX_C,
			       MAX(DECODE(FIELDID,73,FIELDVALUE,NULL)) AS FACTORYMOBILE_C,
                   
			       MAX(DECODE(FIELDID,74,FIELDVALUE,NULL)) AS REMARK_C,
			       MAX(DECODE(FIELDID,75,FIELDVALUE,NULL)) AS GBSAFETYSTANDARDNUMBER_C,
			       MAX(DECODE(FIELDID,76,FIELDVALUE,NULL)) AS GBEMCSTANDARDNUMBER_C,
			       MAX(DECODE(FIELDID,77,FIELDVALUE,NULL)) AS CBTESTCERTIFICATEYN_C,
			       MAX(DECODE(FIELDID,78,FIELDVALUE,NULL)) AS CBCERTIFICATENUMBER_C,
			       MAX(DECODE(FIELDID,79,FIELDVALUE,NULL)) AS CBCERTIFICATEISSUEDDATE_C,
			       MAX(DECODE(FIELDID,80,FIELDVALUE,NULL)) AS CBCERTIFICATENBCNAME_C,
			       MAX(DECODE(FIELDID,81,FIELDVALUE,NULL)) AS CCCCERTIFIACTENUMBER_C,
			       MAX(DECODE(FIELDID,82,FIELDVALUE,NULL)) AS CERTIFICATEMODEL_C
			FROM (SELECT FIELDID,
						 CERTIFICATIONTYPEID,
						 CERTIFICATENUMBER,
						 FIELDVALUE
						FROM DEFAULTVALUES_VIEW DV
						WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
						  AND CERTIFICATIONTYPEID = pi_certificationTypeID)
			GROUP BY CERTIFICATENUMBER;

	EXCEPTION
		WHEN li_ParametersAreNull THEN
			
			ls_ErrorMsg:=  ' There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT  ( as_MachineId => ls_MachineId,
			ad_OPERATORID => ls_OperatorId,
			AD_DATERECORDED  => SYSDATE,
			AS_PROCESSNAME   => ' Reports_Package.GetInfoReportPassenger',
			AX_RECORDDATA    => 'ps_sku is parameters NULL..',
			AS_MESSAGECODE   => TO_CHAR(sqlcode),
			AS_MESSAGE       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
			
			ls_ErrorMsg:=  SQLERRM || ' There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => LS_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetInfoReportPassenger',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN others THEN
			
			ls_ErrorMsg:=  'An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId => ls_MachineId,
			                                          ad_operatorid => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetInfoReportPassenger',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			                                         
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetCCCProductDescReportInfo;

	PROCEDURE GetGSOPassengerReport(pc_Certificate            OUT RETCURSOR,
									pc_Brand                  OUT RETCURSOR,
									pc_SkuList                OUT RETCURSOR,
									pc_Product                OUT RETCURSOR,
									pc_CertificateDfValue     OUT RETCURSOR,
									pc_MeasureHDR             OUT RETCURSOR,
									pc_PlungerHDR             OUT RETCURSOR,
									pc_beadunseathdr          OUT RETCURSOR,
									pc_treadwearhdr           OUT RETCURSOR,
									pc_endurance              OUT RETCURSOR,
									pc_highspeedhdr           OUT RETCURSOR,
									ps_certificateNumber   IN     VARCHAR2,
									ps_extension           IN     VARCHAR2,
									pi_certificationTypeID IN     NUMBER,
									ps_Operatorid          IN     VARCHAR2,
									pi_TireTypeId          IN     NUMBER)
	AS
    /************************************************************************************************
     Procedure Name - GetGSOPassengerReport
     Change History
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/24/2012     Krishna   - As per PRJ3617
                                            - Replaced NPRID with PSN
                                            - Added Matl_Num wherever SKU is available in SELECT list of the query
                                            - Replaced SKULIST_VIEW AND BRND_VIEW with queries
                                            - Added Brand, Brand_Line columns instead of Brandcode
          1.2     11/04/2013    Harini     - As per IDEA2706,Add tiretypename instead of Tiretypeid in product cursor
                                             by joining with TireType table.Added MFGWWYY in the SELECT list of pc_product
    *************************************************************************************************/


		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId    VARCHAR2(50):=NULL;
		ls_OperatorId   VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
		
	BEGIN
	
		IF ps_certificateNumber IS NULL OR pi_certificationTypeID IS NULL THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_certificateNumber ='' OR pi_certificationTypeID  <= 0 THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_Operatorid IS NOT NULL OR ps_Operatorid <> '' THEN
			ls_OperatorId:=ps_Operatorid;
		END IF;

		IF ps_extension IS NULL OR ps_extension = '' THEN

			OPEN pc_Certificate FOR
				SELECT DISTINCT(CE.CERTIFICATENUMBER),
				       CE.EXTENSION_EN
				FROM CERTIFICATE CE
				INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID;
				
				-- Gets the brand information
				-- Replaced Brand_View with qeury as per PRJ3617
				
				OPEN pc_brand FOR
					SELECT *
					FROM (SELECT DISTINCT P.BRAND,
					                      P.BRAND_LINE,
										  C.CERTIFICATENUMBER,
										  C.EXTENSION_EN
						  FROM PRODUCT P,
						       PRODUCTCERTIFICATE PC,
							   CERTIFICATE C
							WHERE C.CERTIFICATEID = PC.CERTIFICATEID
							AND PC.SKUID = P.SKUID)
					WHERE LOWER(certificatenumber) = LOWER(ps_certificateNumber) 
					  AND ROWNUM < 2 ;

			-- Replaced SKULIST_VIEW with query As per PRJ3617
			-- Replacedwith BrandCode,BrandName with Brand, Brand_Line
			-- Added Matl_Num in SELECT list
			
			OPEN pc_SkuList FOR
				SELECT  BRAND,
						BRAND_LINE,
						CERTIFICATENUMBER,
						EXTENSION_EN,
						SKUID,
						SKU,
						LPAD(MATL_NUM,18,0) AS MATL_NUM,
						SIZESTAMP
				FROM (SELECT DISTINCT P.BRAND,
									  P.BRAND_LINE ,
									  C.CERTIFICATENUMBER,
									  C.EXTENSION_EN ,
									  P.SKUID,
									  P.SKU,
									  P.MATL_NUM,
									  P.SIZESTAMP
					  FROM PRODUCT P,
						   PRODUCTCERTIFICATE PC,
						   CERTIFICATE C
					  WHERE C.CERTIFICATEID = PC.CERTIFICATEID
						AND PC.SKUID = P.SKUID)
				WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);


				OPEN pc_Product FOR
					SELECT SKUID,
					       SKU,
					       LPAD(MATL_NUM,18,0) AS MATL_NUM, --Added as per PRJ3617
					       BRAND, -- Added as per PRJ3617
					       BRAND_LINE, -- Added as per PRJ3617
					       TireTypeName, -- Modified to TiretypeName FROM TiretypeId as per IDEA2706
					       PSN, -- Added as per PRJ3617
					       SIZESTAMP,
					       DISCONTINUEDDATE,
					       SPECNUMBER,
					       SPEEDRATING,
					       TRIM(SINGLOADINDEX)||' ('||IL1.METRIC_LOAD||' kg)'   SINGLOADINDEX ,
					       TRIM(DUALLOADINDEX)||' ('||IL2.METRIC_LOAD||' kg)'  DUALLOADINDEX,
					       BIASBELTEDRADIAL, 
					       TUBELESSYN,
					       REINFORCEDYN,
					       EXTRALOADYN,
					       UTQGTREADWEAR,
					       UTQGTRACTION,
					       UTQGTEMP,
					       MUDSNOWYN,
					       RIMDIAMETER,
					       CERTIFICATENUMBER,
					       EXTENSION_EN,
					       SERIALDATE,
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
					       DATEASSIGNED_EGI,
					       MFGWWYY  -- Added as per IDEA2706
					FROM PRODUCTDATA_REPORT_VIEW PRV
					LEFT OUTER JOIN TRACS.INDEXED_LOADS IL1 ON IL1.LOAD_INDEX =  SINGLOADINDEX
					LEFT OUTER JOIN TRACS.INDEXED_LOADS IL2 ON IL2.LOAD_INDEX =  DUALLOADINDEX
					LEFT OUTER JOIN TIRETYPE T ON PRV.TIRETYPEID = T.TIRETYPEID
					WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) AND rownum < 2;

			ELSE
			
				OPEN pc_Certificate FOR
					SELECT DISTINCT(CE.certificatenumber),ce.extension_en
					FROM  CERTIFICATE CE
					INNER JOIN productcertificate pce ON
					ce.certificateid   = pce.certificateid AND
					ce.certificationtypeid = pce.certificationtypeid
					INNER JOIN product p ON
					pce.skuid = p.skuid
					WHERE LOWER(ce.certificatenumber) = LOWER(ps_certificateNumber) AND
					ce.certificationtypeid = pi_certificationTypeID AND
					LOWER(CE.EXTENSION_EN) = LOWER(ps_extension);
					-- Gets the brand information
					-- Replaced query with Brand_View as per PRJ3617
					OPEN pc_brand FOR
					SELECT *
					FROM  (SELECT DISTINCT  p.Brand
					,p.Brand_Line
					,c.CertificateNumber
					,c.Extension_En
					FROM  Product p,
					ProductCertificate pc,
					Certificate c
					WHERE c.CertificateId = pc.CertificateId
					AND pc.SkuId = p.SkuId)
					WHERE LOWER(certificatenumber) = LOWER(ps_certificateNumber) AND
					LOWER(EXTENSION_EN) = LOWER(ps_extension);

				--Replaced query with SKULIST_VIEW as per PRJ3617
				--Replaced BrandCode, BrandName with Brand, Brand_Line
				--Added Matl_Num in SELECT list
				OPEN pc_SkuList FOR
					SELECT  BRAND,
					BRAND_LINE,
					CERTIFICATENUMBER,
					EXTENSION_EN,
					SKUID,
					SKU,
					LPAD(MATL_NUM,18,0) AS MATL_NUM,
					SIZESTAMP
					FROM (SELECT DISTINCT p.Brand,
					p.Brand_Line ,
					c.CertificateNumber,
					c.Extension_En ,
					p.SkuId,
					p.Sku,
					p.Matl_Num,
					p.SizeStamp
					FROM Product p,
					ProductCertificate pc,
					Certificate c
					WHERE c.CertificateId = pc.CertificateId
					AND pc.SkuId = p.SkuId)
					WHERE LOWER(certificatenumber) = LOWER(ps_certificateNumber)  AND
					LOWER(EXTENSION_EN) = LOWER(ps_extension);

				-- Gets the pc_Product information
				OPEN pc_Product FOR
					SELECT SKUID,
					       SKU,
					       LPAD(MATL_NUM,18,0) AS MATL_NUM , -- Added as per PRJ3617
					       BRAND, -- As per PRJ3617
					       BRAND_LINE,-- As per PRJ3617
					       TireTypeName, -- Modified to TiretypeName FROM TiretypeId as per IDEA2706
					       PSN, -- Added as per PRJ3617
					       SIZESTAMP,
					       DISCONTINUEDDATE,
					       SPECNUMBER,
					       SPEEDRATING,
					       TRIM(SINGLOADINDEX)||' ('||IL1.METRIC_LOAD||' kg)'   SINGLOADINDEX ,
					       TRIM(DUALLOADINDEX)||' ('||IL2.METRIC_LOAD||' kg)'   DUALLOADINDEX,
					       BIASBELTEDRADIAL, 
					       TUBELESSYN,
					       REINFORCEDYN,
					       EXTRALOADYN,
					       UTQGTREADWEAR,
					       UTQGTRACTION,
					       UTQGTEMP,
					       MUDSNOWYN,
					       RIMDIAMETER,
					       CERTIFICATENUMBER,
					       EXTENSION_EN,
					       SERIALDATE,
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
					       DATEASSIGNED_EGI,
					       MFGWWYY-- Added as per IDEA2706
					FROM PRODUCTDATA_REPORT_VIEW PRV
					LEFT OUTER JOIN TRACS.INDEXED_LOADS IL1 ON IL1.LOAD_INDEX = SINGLOADINDEX
					LEFT OUTER JOIN TRACS.INDEXED_LOADS IL2 ON IL2.LOAD_INDEX = DUALLOADINDEX
					LEFT OUTER JOIN TIRETYPE T ON PRV.TIRETYPEID = T.TIRETYPEID
					WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
					  AND ROWNUM < 2;

		END IF ;

		-- Gets the default values information
		OPEN pc_CertificateDfValue FOR
			SELECT certificatenumber,
			       MAX(DECODE(FIELDID,83,FIELDVALUE,NULL)) AS COUNTRYOFPRODUCTION_G,
			       MAX(DECODE(FIELDID,85,FIELDVALUE,NULL)) AS NAMEOFMANUFACTURER_G,
			       MAX(DECODE(FIELDID,87,FIELDVALUE,NULL)) AS TO_G,
			       MAX(DECODE(FIELDID,89,FIELDVALUE,NULL)) AS REGULATIONNOPASSENGER_G,
			       MAX(DECODE(FIELDID,90,FIELDVALUE,NULL)) AS REGULATIONNOLIGHTTRUCK_G,
			       MAX(DECODE(FIELDID,91,FIELDVALUE,NULL)) AS RAYONOTHER_G
			FROM (SELECT FIELDID,
						 CERTIFICATIONTYPEID,
						 CERTIFICATENUMBER,
						 FIELDVALUE
			      FROM DEFAULTVALUES_VIEW DV
			      WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				    AND CERTIFICATIONTYPEID = pi_certificationTypeID)
			GROUP BY CERTIFICATENUMBER;

		OPEN pc_MeasureHDR FOR
			SELECT MEASUREID,
			       NVL(PROJECTNUMBER,'')                              AS PROJECTNUMBER,
			       NVL(TIRENUMBER,0)                                  AS TIRENUMBER,
			       NVL(TESTSPEC,'')                                   AS TESTSPEC,
			       NVL(COMPLETIONDATE ,TO_DATE('2000/01/01','yyyy/mm/dd')) AS COMPLETIONDATE,
			       NVL(INFLATIONPRESSURE,0)                           AS INFLATIONPRESSURE,
			       NVL( MOLDDESIGN,'.')                               AS MOLDDESIGN,
			       NVL(RIMWIDTH,'0')                                  AS RIMWIDTH,
			       NVL(DOTSERIALNUMBER,'.')                           AS DOTSERIALNUMBER,
			       NVL(DIAMETER,0)                                    AS DIAMETER,
			       NVL(AVGSECTIONWIDTH,0)                             AS AVGSECTIONWIDTH,
			       NVL(AVGOVERALLWIDTH,0)                             AS AVGOVERALLWIDTH,
			       NVL(MAXOVERALLWIDTH,0)                             AS MAXOVERALLWIDTH,
			       NVL(SIZEFACTOR,0)                                  AS SIZEFACTOR,
			       NVL(MOUNTTIME, TO_DATE('2000/01/01','yyyy/mm/dd')) AS MOUNTTIME,
			       NVL(MOUNTTEMP, 0)                                  AS MOUNTTEMP,
			       NVL(SERIALDATE,TO_DATE('2000/01/01','yyyy/mm/dd')) AS SERIALDATE,
			       NVL(ENDTIME,TO_DATE('2000/01/01','yyyy/mm/dd'))    AS ENDTIME,
			       NVL(ACTSIZEFACTOR,0)                               AS ACTSIZEFACTOR,
			       NVL(STARTINFLATIONPRESSURE,0)                      AS STARTINFLATIONPRESSURE,
			       NVL(ENDINFLATIONPRESSURE,0)                        AS ENDINFLATIONPRESSURE,
			       NVL(ADJUSTMENT,'')                                 AS ADJUSTMENT,
			       NVL(CIRCUMFERENCE, 0)                              AS CIRCUNFERENCE,
			       NVL(NOMINALDIAMETER, 0)                            AS NOMINALDIAMETER,
			       NVL(NOMINALWIDTH, 0)                               AS NOMINALWIDTH,
			       NVL(NOMINALWIDTHPASSFAIL, 'y')                     AS NOMINALWIDTHPASSFAIL,
			       NVL(NOMINALWIDTHDIFERENCE, 0)                      AS NOMINALWIDTHDIFERENCE,
			       NVL(NOMINALWIDTHTOLERANCE, 0)                      AS NOMINALWIDTHTOLERANCE,
			       NVL(MAXOVERALLDIAMETER, 0)                         AS MAXOVERALLDIAMETER,
			       NVL(MINOVERALLDIAMETER, 0)                         AS MINOVERALLDIAMETER,
			       NVL(OVERALLWIDTHPASSFAIL, 'y')                     AS OVERALLWIDTHPASSFAIL,
			       NVL(OVERALLDIAMETERPASSFAIL, 'y')                  AS OVERALLDIAMETERPASSFAIL,
			       NVL(DIAMETERDIFERENCE, 0)                          AS DIAMETERDIFERENCE,
			       NVL(DIAMETERTOLERANCE, 0)                          AS DIAMETERTOLERANCE,
			       NVL(TEMPRESISTANCEGRADING, 0)                      AS TEMPRESISTANCEGRADING,
			       NVL(TENSILESTRENGHT1, 0)                           AS TENSILESTRENGHT1,
			       NVL(TENSILESTRENGHT2, 0)                           AS TENSILESTRENGHT2,
			       NVL(ELONGATION1, 0)                                AS ELONGATION1,
			       NVL(ELONGATION2, 0)                                AS ELONGATION2,
			       NVL(TENSILESTRENGHTAFTERAGE1, 0)                   AS TENSILESTRENGHTAFTERAGE1,
			       NVL(TENSILESTRENGHTAFTERAGE2, 0)                   AS TENSILESTRENGHTAFTERAGE2,
			       CE.CERTIFICATIONTYPEID,
			       CE.CERTIFICATENUMBER,
			       PCE.SKUID
			FROM CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID= PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN CERTIFICATIONTYPE CT   ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
			INNER JOIN MEASUREHDR M           ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			   AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			   AND ROWNUM < 2 ;

		OPEN pc_PlungerHDR FOR
			SELECT PLUNGERID,
			       PROJECTNUMBER,
			       TIRENUMBER,
			       TESTSPEC,
			       COMPLETIONDATE,
			       DOTSERIALNUMBER,
			       AVGBREAKINGENERGY,
			       PASSYN,
			       SERIALDATE,
			       MINPLUNGER,
			       CE.CERTIFICATIONTYPEID,
			       CE.CERTIFICATENUMBER,
			       PCE.SKUID
			FROM  CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID= PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN  CERTIFICATIONTYPE CT  ON CE.CERTIFICATIONTYPEID=CT.CERTIFICATIONTYPEID
			INNER JOIN  PLUNGERHDR  P         ON CE.CERTIFICATEID = P.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			  AND ROWNUM < 2  ;

		OPEN pc_beadunseathdr FOR
			SELECT  BEADUNSEATID,
					PROJECTNUMBER,
					TIRENUMBER,
					TESTSPEC,
					COMPLETIONDATE,
					DOTSERIALNUMBER,
					LOWESTUNSEATVALUE,
					PASSYN,
					SERIALDATE,
					MINBEADUNSEAT,
					PASSYN,
					CE.CERTIFICATIONTYPEID,
					CE.CERTIFICATENUMBER,
					PCE.SKUID
			FROM  CERTIFICATE CE
			INNER JOIN  PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN  CERTIFICATIONTYPE CT   ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID 
			INNER JOIN  BEADUNSEATHDR B        ON CE.CERTIFICATEID = B.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			  AND ROWNUM < 2;

		OPEN pc_treadwearhdr FOR
			SELECT  TREADWEARID,
					PROJECTNUMBER,
					TIRENUMBER,
					TESTSPEC,
					COMPLETIONDATE,
					DOTSERIALNUMBER,
					LOWESTWEARBAR,
					PASSYN,
					SERIALDATE,
					INDICATORSREQUIREMENT,
					CE.CERTIFICATIONTYPEID,
					CE.CERTIFICATENUMBER,
					PCE.SKUID
			FROM  CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID   = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN  CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
			INNER JOIN  TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			  AND ROWNUM < 2  ;

		OPEN  pc_endurance FOR
			SELECT ENDURANCEID,
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
			       CE.CERTIFICATIONTYPEID,
			       CE.CERTIFICATENUMBER,
			       PCE.SKUID
			FROM CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN  CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
			INNER JOIN  ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			  AND ROWNUM < 2  ;

		OPEN pc_highspeedhdr FOR
			SELECT HIGHSPEEDID,
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
				   MAXLOAD,
				   CE.CERTIFICATIONTYPEID,
				   CE.CERTIFICATENUMBER,
				   PCE.SKUID
			FROM  CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN  CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
			INNER JOIN  HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID 
			  AND ROWNUM < 2 ;

	EXCEPTION
		WHEN li_ParametersAreNull THEN
		
			ls_ErrorMsg:=  ' There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetGSOPassengerReport',
			                                          ax_recorddata    => 'ps_sku is parameters NULL..',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
		
			ls_ErrorMsg:=  SQLERRM || ' There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT( as_machineid     => ls_MachineId,
			                                           ad_operatorid    => ls_OperatorId,
			                                           ad_daterecorded  => SYSDATE,
			                                           as_processname   => ' Reports_Package.GetGSOPassengerReport',
			                                           ax_recorddata    => 'There is at least one parameters invalid.',
			                                           as_messagecode   => TO_CHAR(SQLCODE),
			                                           as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN others THEN
		
			ls_ErrorMsg:=  'An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetGSOPassengerReport',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
													  
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetGSOPassengerReport;

	PROCEDURE GetCertificateReportInfoBySKU(pc_Product         OUT RETCURSOR,
											pc_Certificate     OUT RETCURSOR,
											PC_TESTREFERENCE   OUT RETCURSOR,
											ps_Matl_Num      IN    VARCHAR2, -- Added as per PRJ3617
											ps_Operatorid    IN    VARCHAR2,
											ps_Brand         IN    VARCHAR2,-- Added as per PRJ3617
											ps_Brand_Line    IN    VARCHAR2,-- Added as per PRJ3617
											ps_CertType      IN    VARCHAR2)
	AS
    /************************************************************************************************
     Procedure Name - GetCertificateReportInfoBySKU
     Change History
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - AS per PRJ3617
                                            - Added Matl_num WHERE Sku exist
                                            - Replaced ps_Sku with ps_Matl_Num
                                            - Replaced BrandCode with Brand AND Brand_Line
                                            - Replaced NPRID with PSN
                   10/31/2016  JESEITZ - fixed bug WHERE IMARK_FAMILY not joined correctly FOR GETCERTIFICATEREPORTINFOBYSKU
    *************************************************************************************************/

		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId     VARCHAR2(50):=NULL;
		ls_OperatorId    VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg      VARCHAR2(4000):=NULL;

		li_LatestSkuId         PRODUCT.SKUID%TYPE:=NULL;
		li_certificationtypeid CERTIFICATE.CERTIFICATIONTYPEID%TYPE:=NULL;
		
  BEGIN

	BEGIN
	---Find the certificationtype code selected.
		SELECT CERTIFICATIONTYPEID INTO li_certificationtypeid 
		FROM CERTIFICATIONTYPE CT
		WHERE UPPER(PS_CERTTYPE) = UPPER(CT.CERTIFICATIONTYPENAME);
	EXCEPTION
		WHEN others THEN
			li_certificationtypeid := 0;
	END;

	IF ps_Operatorid IS NOT NULL OR ps_Operatorid <> '' THEN
		ls_OperatorId:=ps_Operatorid;
	END IF;

	IF ps_matl_num IS NOT NULL THEN
		li_LatestSkuId := ICS_COMMON_FUNCTIONS.GETLATESTSKUIDBYSKU(PS_Matl_Num => ps_matl_num);
	ELSE
		li_LatestSkuId := NULL;
	END IF;


	OPEN pc_Product FOR
		SELECT DISTINCT SKUID,
		                SKU,
		                LPAD(MATL_NUM,18,0) AS MATL_NUM, -- Added AS per PRJ3617
		                BRAND,  -- Added AS per PRJ3617
		                BRAND_LINE, -- Added AS per PRJ3617
		                TIRETYPEID,
		                PSN, -- Added AS per PRJ3617
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
		                MEARIMWIDTH,
		                UTQGTREADWEAR,
		                UTQGTRACTION,
		                UTQGTEMP,
		                MUDSNOWYN,
		                RIMDIAMETER,
		                CERTIFICATENUMBER,
		                EXTENSION_EN
		FROM  PRODUCTDATA_REPORT_VIEW
		WHERE (SKUID = LI_LATESTSKUID OR LI_LATESTSKUID IS NULL)
		  AND (BRAND = PS_BRAND OR PS_BRAND IS NULL) -- ADDED AS PER PRJ3617
		  AND (BRAND_LINE = PS_BRAND_LINE OR PS_BRAND_LINE IS NULL) -- ADDED AS PER PRJ3617
		   AND SKUID = (SELECT MAX(SKUID) FROM PRODUCT WHERE PRODUCT.MATL_NUM = LPAD(PRODUCTDATA_REPORT_VIEW.Matl_Num,18,0));-- Added AS per PRJ3617


	-- Gets the Certificate information
	OPEN pc_Certificate FOR
		SELECT PCE.SKUID,
		       CE.CERTIFICATIONTYPEID,
		       CT.CERTIFICATIONTYPENAME,
		       CE.CERTIFICATENUMBER,
		       DATESUBMITTED,
		       ACTIVESTATUS,
		       PCE.DATEASSIGNED_EGI,
		       DATEAPPROVED_CEGI,
		       RENEWALREQUIRED_CGIN,
		       ' ' SUPPLEMENTALREQUIRED_EI, --JES 4/4/11
		       ' ' SUPPLEMENTALNUMBER_EI,    --JES 4/4/11
		       JOBREPORTNUMBER_CEN,
		       EXTENSION_EN,
		       SUPPLEMENTALMOLDSTAMPING_E,
		       CT.CERTIFICATIONTYPENAME CERTTYPE,
		       EMARKREFERENCE_I,
		       EXPIRYDATE_I,
		       IFAM.FAMILY_CODE FAMILY_I,
		       PRODUCTLOCATION,
		       COUNTRYOFMANUFACTURE_N,
		       CUSTOMER CUSTOMER_N,
		       CUSTOMERSPECIFIC_N,
		       IMPORTER IMPORTER_N, --JES 4/4/11
		       IMPORTERADDRESS IMPORTERADDRESS_N,   --JES 4/4/11
		       IMPORTERREPRESENTATIVE IMPORTERREPRESENTATIVE_N, --JES 4/4/11
		       COUNTRYLOCATION COUNTRYLOCATION_N, -- JES 4/6/11 -- we'll check to see IF we need this FOR the report.
		       BATCHNUMBER_G
		FROM  CERTIFICATE CE
		INNER JOIN  PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
		INNER JOIN  CERTIFICATIONTYPE CT ON CE.CERTIFICATIONTYPEID = CT.CERTIFICATIONTYPEID
		INNER JOIN PRODUCT P ON P.SKUID = PCE.SKUID
		LEFT OUTER JOIN IMARK_FAMILY IFAM ON IFAM.FAMILY_ID = P.FAMILY AND IFAM.CERTIFICATEID = CE.CERTIFICATEID  --JES 10/31/2016
		LEFT OUTER JOIN IMPORTER IM ON CE.IMPORTERID = IM.IMPORTERID
		LEFT OUTER JOIN CUSTOMER CU ON CE.CUSTOMERID = CU.CUSTOMERID
		WHERE (PCE.SKUID = li_LatestSkuId OR li_LatestSkuId IS NULL)
		  AND (li_certificationtypeid = 0 OR  li_certificationtypeid = CE.CERTIFICATIONTYPEID)
		--Added AS per PRJ3617 BrandCode with Brand AND Brand_Line, Sku with Matl_Num,
		AND (PS_BRAND IS NULL OR ps_Brand_Line IS NULL OR PCE.SKUID IN (SELECT SKUID 
		                                                                FROM PRODUCT
																		 WHERE BRAND = ps_Brand
																		   AND BRAND_LINE = ps_Brand_Line
																		   AND SKUID = (SELECT MAX(SKUID) 
																		                FROM PRODUCT P2
																						WHERE P2.Matl_Num = LPAD(PRODUCT.Matl_Num,18,0))));

	OPEN PC_TESTREFERENCE FOR
		SELECT CERTIFICATENUMBER,
		       SKUID,
		       MEASUREMENT_TESTREFERENCE,
		       PLUNGER_TESTREFERENCE,
		       BEADUNSEAT_TESTREFERENCE,
		       ENDURANCE_TESTREFERENCE,
		       HIGHSPEED_TESTREFERENCE,
		       LAB_TESTREFERENCE,
		       WHEEL_TESTREFERENCE,
		       NOISE_TESTREFERENCE,
		       WG_TESTREFERENCE,
		       RR_TESTREFERENCE
		FROM  TESTREFERENCE_VIEW T
		WHERE (T.SKUID = LI_LATESTSKUID OR LI_LATESTSKUID IS NULL)
		-- ADDED AS PER PRJ3617 BRANDCODE WITH BRAND AND BRAND_LINE, SKU WITH MATL_NUM
		   AND (PS_BRAND IS NULL OR PS_BRAND_LINE IS NULL OR T.SKUID IN (SELECT SKUID FROM PRODUCT
																		   WHERE BRAND = PS_BRAND
																		   AND   BRAND_LINE = ps_Brand_Line
																		   AND SKUID = (SELECT MAX(SKUID) 
																		                FROM PRODUCT P2
																						WHERE P2.Matl_Num = LPAD(PRODUCT.Matl_Num,18,0))));

	EXCEPTION
		WHEN li_ParametersAreNull THEN
		
			ls_ErrorMsg:=  ' There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT (as_machineid     => ls_MachineId,
													   ad_operatorid    => ls_OperatorId,
													   ad_daterecorded  => SYSDATE,
													   as_processname   => ' Reports_Package.GetCertificateReportInfo',
													   ax_recorddata    => 'ps_sku is parameters NULL..',
													   as_messagecode   => TO_CHAR(SQLCODE),
													   as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
		
			ls_ErrorMsg:=  SQLERRM || ' There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId    => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => ' Reports_Package.GetCertificateReportInfo',
													  ax_recorddata    => 'There is at least one parameters invalid.',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  'An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetCertificateReportInfo',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetCertificateReportInfoBySKU;

   	PROCEDURE GetImarkCertificationInfo(pc_ImarkCertification OUT   RETCURSOR,
	                                    pd_DateSearchCriteria    IN DATE) 
	AS
    /************************************************************************************************
     Procedure Name - GetImarkCertificationInfo
     Change History
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - AS per PRJ3617
                                            - Added Matl_num WHERE Sku exist
                                            - Removed brand_details_mv in queries, used Brand, Brand_Line
                                              FOR BrandDesc

    *************************************************************************************************/

		ls_MachineId VARCHAR2(50) :=NULL;
		ls_OperatorId VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg VARCHAR2(4000):=NULL;
		
   BEGIN
   
	IF pd_DateSearchCriteria IS NULL THEN
		
		OPEN pc_ImarkCertification FOR
			SELECT DISTINCT TO_NUMBER(PIF.familyID)  AS family,
			                P.SKU,
			                LPAD(P.MATL_NUM,18,0) AS MATL_NUM, -- Added AS per PRJ3617
			                P.EMARKREFERENCE_I AS EMARKREFERENCE,
			                P.SIZESTAMP,
			                P.BRAND, -- ADDED AS PER PRJ3617
			                P.BRAND_LINE,-- ADDED AS PER PRJ3617
			                P.SINGLOADINDEX,
			                P.DUALLOADINDEX,
			                P.SPEEDRATING,
			                PCE.DATESUBMITTED,
			                PCE.DATEAPPROVED_CEGI AS DATEAPPROVED,
			                P.DISCONTINUEDDATE,
			                PCE.DATEASSIGNED_EGI AS DATEASSIGNED
			FROM PRODUCT P
			INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
			INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			LEFT OUTER JOIN PRODUCT_IMARK_FAMILY PIF ON P.SKUID = PIF.SKUID AND PCE.CERTIFICATEID = PIF.CERTIFICATEID
			WHERE CE.CERTIFICATIONTYPEID = 4
			  AND LOWER(CE.MOSTRECENTCERT) = 'y'
			  AND PCE.DATEAPPROVED_CEGI IS NOT NULL
			  AND PCE.DATEREMOVED IS NULL;



	ELSE
	
		OPEN pc_ImarkCertification FOR
			SELECT DISTINCT to_number(PIF.familyID) AS family,
			                P.SKU,
			                LPAD(P.MATL_NUM,18,0) AS MATL_NUM, -- Added AS per PRJ3617
			                P.EMARKREFERENCE_I    AS EMARKREFERENCE,
			                P.SIZESTAMP,
			                P.BRAND, -- ADDED AS PER PRJ3617
			                P.BRAND_LINE, -- ADDED AS PER PRJ3617
			                P.SINGLOADINDEX,
			                P.DUALLOADINDEX,
			                P.SPEEDRATING,
			                PCE.DATESUBMITTED,
			                PCE.DATEAPPROVED_CEGI AS DATEAPPROVED,
			                P.DISCONTINUEDDATE,
			                PCE.DATEASSIGNED_EGI  AS DATEASSIGNED
			FROM PRODUCT P
			INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
			INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			LEFT OUTER JOIN PRODUCT_IMARK_FAMILY PIF ON P.SKUID = PIF.SKUID AND PCE.CERTIFICATEID = PIF.CERTIFICATEID
			WHERE CE.CERTIFICATIONTYPEID = 4
			  AND LOWER(CE.MOSTRECENTCERT) = 'y'
			  AND DATESUBMITTED >= PD_DATESEARCHCRITERIA
			  AND PCE.DATEAPPROVED_CEGI IS NOT NULL
			  AND PCE.DATEREMOVED IS NULL;

	END IF ;

	EXCEPTION
		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  'An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId    => ls_MachineId,
													 ad_operatorid    => ls_OperatorId,
													 ad_daterecorded  => SYSDATE,
													 as_processname   =>' Reports_Package.GetImarkCertificationInfo',
													 ax_recorddata    => 'An error have ocurred.(WHEN others)',
													 as_messagecode   => TO_CHAR(SQLCODE),
													 as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END GetImarkCertificationInfo;

	PROCEDURE GetEmarkCertificationInfo(pc_EmarkCertification  OUT RETCURSOR,
									    pc_Product             OUT RETCURSOR,
									    ps_certificateNumber IN    VARCHAR2,
									    ps_BrandCode         IN    VARCHAR2) 
	AS


	BEGIN

		OPEN pc_Product FOR
			SELECT P.SKUID,
			       SKU,
			       BRANDCODE,
			       TIRETYPEID,
			       NPRID,
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
			       MEARIMWIDTH,
			       UTQGTREADWEAR,
			       UTQGTRACTION,
			       UTQGTEMP,
			       MUDSNOWYN,
			       RIMDIAMETER
			FROM PRODUCT P
			INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
			INNER JOIN CERTIFICATE CE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			WHERE CE.CERTIFICATIONTYPEID = 1
			  AND LOWER( CE.MOSTRECENTCERT) = 'y'
			  AND PCE.DATEAPPROVED_CEGI IS NOT NULL
			  AND(PS_CERTIFICATENUMBER IS NULL 
			  OR LOWER(CE.CERTIFICATENUMBER) =LOWER(ps_certificateNumber))
			  AND (PS_BRANDCODE IS NULL 
			  OR LOWER(P.BRANDCODE) = LOWER( ps_BrandCode ) );


		OPEN pc_EmarkCertification FOR
			SELECT pe.SKUID,
			       CERTIFICATENUMBER,
			       ce.CERTIFICATIONTYPEID,
			       DATESUBMITTED,
			       ACTIVESTATUS,
			       DATEASSIGNED_EGI,
			       DATEAPPROVED_CEGI,
			       JOBREPORTNUMBER_CEN,
			       EXTENSION_EN,
			       SUPPLEMENTALMOLDSTAMPING_E,
			       'Not defiened yet' AS WHEELTESTREFERENCE,
			       'Not defiened yet' AS NOISETESTREFERENCE,
			       'Not defiened yet' AS WGTESTREFERENCE,
			       'Not defiened yet' AS RRTESTREFERENCE
			FROM  PRODUCTCERTIFICATE PE
			INNER JOIN CERTIFICATE CE ON PE.CERTIFICATEID = CE.CERTIFICATEID AND PE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			INNER JOIN PRODUCT P ON PE.SKUID = P.SKUID
			WHERE CE.CERTIFICATIONTYPEID = 1
			  AND LOWER(CE.MOSTRECENTCERT) = 'y'
			  AND PE.DATEAPPROVED_CEGI IS NOT NULL
			  AND( PS_CERTIFICATENUMBER IS NULL 
			  OR LOWER(CE.CERTIFICATENUMBER) =LOWER(ps_certificateNumber))
			  AND (ps_BrandCode IS NULL OR LOWER(P.BRANDCODE)= LOWER( ps_BrandCode ) );

	END GetEmarkCertificationInfo;


	PROCEDURE GetEmarkPassengerWithTR(ps_CertificateNumber  IN    VARCHAR2,
									  pi_tiretypeid         IN    NUMBER,
									  pc_CertificateDfValue   OUT RETCURSOR,
									  pc_CertificateInfo      OUT RETCURSOR,
									  pc_Product              OUT RETCURSOR,
									  pc_MeasureHDR           OUT RETCURSOR,
									  pc_measureDtl           OUT RETCURSOR,
									  pc_BEADUNSEATHDR        OUT RETCURSOR,
									  pc_BEADUNSEATDTL        OUT RETCURSOR,
									  pc_PLUNGERHDR           OUT RETCURSOR,
									  pc_PLUNGERDTL           OUT RETCURSOR,
									  pc_TREADWEARHDR         OUT RETCURSOR,
									  pc_TREADWEARDTL         OUT RETCURSOR,
									  pc_ENDURANCEHDR         OUT RETCURSOR,
									  pc_ENDURANCEDTL         OUT RETCURSOR,
									  pc_HIGHSPEEDHDR         OUT RETCURSOR,
									  pc_HIGHSPEEDDTL         OUT RETCURSOR,
									  pc_SPEEDTESTDETAIL      OUT RETCURSOR,
									  pc_Brand                OUT RETCURSOR)
	AS

		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId     VARCHAR2(50):=NULL;
		ls_OperatorId    VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg      VARCHAR2(4000):=NULL;
		
	BEGIN

		IF ps_CertificateNumber IS NULL THEN
			RAISE li_ParametersAreNull;
		END IF ;
		
		IF ps_CertificateNumber = '' THEN
			RAISE li_ParametersAreInvalid;
		END IF ;


		-- Gets the brand information
		OPEN pc_brand FOR
			SELECT DISTINCT BRANDCODE, ---get all brands that have ever been associated with this certificate, because
							BRANDNAME,           ---the sks's that were ON the original test specs may have since been discontinued
							CERTIFICATENUMBER---but just get each brand once, even though it is multiple extensions.
			FROM  BRAND_VIEW
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);

		--Gets the default values information.
		OPEN pc_CertificateDfValue FOR
			SELECT CERTIFICATENUMBER,
			       MAX(DECODE(FIELDID,1,FIELDVALUE,NULL)) AS MANUFACTURERNAME_E,
			       MAX(DECODE(FIELDID,2,FIELDVALUE,NULL)) AS MANUFACTURERNAMEADDRESS_E,
			       MAX(DECODE(FIELDID,3,FIELDVALUE,NULL)) AS TECHNICALSERVICE_E,
			       MAX(DECODE(FIELDID,4,FIELDVALUE,NULL)) AS PLACE_E,
			       MAX(DECODE(FIELDID,5,FIELDVALUE,NULL)) AS MEASURERIM_E,
			       MAX(DECODE(FIELDID,6,FIELDVALUE,NULL)) AS INFLATIONPRESSURE_E,
			       MAX(DECODE(FIELDID,7,FIELDVALUE,NULL)) AS TESTLABORATORY_E,
			       MAX(DECODE(FIELDID,8,FIELDVALUE,NULL)) AS REPRESENTATIVENAME_E,
			       MAX(DECODE(FIELDID,9,FIELDVALUE,NULL)) AS REPRESENTATIVEADDRESS_E,
			       MAX(DECODE(FIELDID,10,FIELDVALUE,NULL)) AS REASONOFEXTENSION_E,
			       MAX(DECODE(FIELDID,11,FIELDVALUE,NULL)) AS REMARKS_E
			FROM (SELECT FIELDID,
						 CERTIFICATIONTYPEID,
						 CERTIFICATENUMBER,
						 FIELDVALUE
				  FROM DEFAULTVALUES_VIEW
				  WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				  AND CERTIFICATIONTYPEID = 1)
			GROUP BY CERTIFICATENUMBER ;

		OPEN pc_CertificateInfo FOR
			SELECT CERTIFICATEID,
			       CE.CERTIFICATIONTYPEID,
			       CERTIFICATIONTYPENAME,
			       CERTIFICATENUMBER,
			       ACTIVESTATUS,
			       RENEWALREQUIRED_CGIN,
			       JOBREPORTNUMBER_CEN,
			       EXTENSION_EN,
			       SUPPLEMENTALMOLDSTAMPING_E,
			       EXPIRYDATE_I,
			       ' ' "FAMILY_I"
			FROM CERTIFICATE CE
			INNER JOIN  CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE CE.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
			  AND LOWER(CE.MOSTRECENTCERT) = 'y'  ;


		OPEN pc_Product FOR
			SELECT DISTINCT SKUID,  
			                SKU, 
							BRANDCODE, 
							TIRETYPEID, 
							NPRID,
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
							CERTIFICATENUMBER,   
							SUPPLEMENTALEXTENSION_EN
			FROM  PRODUCTDATA_REPORT_VIEW
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber)  
			  AND TIRETYPEID = pi_tiretypeid;

		OPEN pc_MeasureHDR FOR
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
			        CE.CERTIFICATIONTYPEID,
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
			FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE CE.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND UPPER(CE.MOSTRECENTCERT) = 'Y';

		OPEN pc_measuredtl FOR
			SELECT MD.MEASUREID,
			       SECTIONWIDTH,
			       OVERALLWIDTH,
			       ITERATION
			FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
			 AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID 
			 AND LOWER(CE.MOSTRECENTCERT) = 'y' 
			INNER JOIN MEASUREDTL MD ON M.MEASUREID = MD.MEASUREID
			WHERE M.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber);

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
			FROM CERTIFICATE CE INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
			WHERE B.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_BEADUNSEATDTL FOR
			SELECT BD.BEADUNSEATID,
			       UNSEATFORCE,
			       ITERATION
			FROM CERTIFICATE CE
			INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
			INNER JOIN BEADUNSEATDTL BD ON B.BEADUNSEATID = BD.BEADUNSEATID
			WHERE B.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
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
			       P.CERTIFICATIONTYPEID,
			       CERTIFICATENUMBER,
			       SERIALDATE,
			       MINPLUNGER,
			       SKU
			FROM CERTIFICATE CE
			INNER JOIN  PLUNGERHDR P ON CE.CERTIFICATEID = P.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
			WHERE P.CERTIFICATIONTYPEID = 1 AND
			LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_plungerdtl FOR
			SELECT PD.PLUNGERID,
				   BREAKINGENERGY,
			       ITERATION
			FROM CERTIFICATE CE
			INNER JOIN PLUNGERHDR PH ON CE.CERTIFICATEID = PH.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PH.CERTIFICATIONTYPEID
			INNER JOIN PLUNGERDTL PD ON PH.PLUNGERID = PD.PLUNGERID
			WHERE PH.CERTIFICATIONTYPEID = 1 
			 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			 AND LOWER(CE.MOSTRECENTCERT) = 'y';


		OPEN pc_treadwearhdr FOR
			SELECT TREADWEARID,
			       PROJECTNUMBER,
			       TIRENUMBER,
			       TESTSPEC,
			       COMPLETIONDATE,
			       DOTSERIALNUMBER,
			       LOWESTWEARBAR,
			       PASSYN,
			       SERIALDATE,
			       T.CERTIFICATIONTYPEID,
			       CERTIFICATENUMBER,
			       INDICATORSREQUIREMENT,
			       SKU
			FROM CERTIFICATE CE
			INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			WHERE T.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_treadweardtl FOR
			SELECT TD.TREADWEARID,
				   WEARBARHEIGHT,
				  ITERATION
			FROM CERTIFICATE CE
			INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			INNER JOIN TREADWEARDTL TD ON T.TREADWEARID = TD.TREADWEARID
			WHERE T.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_endurancehdr FOR
			SELECT ENDURANCEID,
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
			       SKU
			FROM CERTIFICATE CE INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
			WHERE E.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_endurancedtl FOR
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
			INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
			INNER JOIN ENDURANCEDTL ED ON E.ENDURANCEID = ED.ENDURANCEID
			WHERE E.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_highspeedhdr FOR
			SELECT HIGHSPEEDID,
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
			       MAXLOAD,
			       SKU
			FROM CERTIFICATE CE INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			WHERE H.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_highspeeddtl FOR
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
			       HD.HIGHSPEEDID
			FROM CERTIFICATE CE
			INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			INNER JOIN HIGHSPEEDDTL HD ON H.HIGHSPEEDID = HD.HIGHSPEEdid
			WHERE H.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_speedtestdetail FOR
			SELECT ITERATION,
			       TIME,
			       SPEED,
			       S.HIGHSPEEDID
			 FROM CERTIFICATE CE
			INNER JOIN HIGHSPEEDHDR H  ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			INNER JOIN SPEEDTESTDETAIL S ON H.HIGHSPEEDID = S.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 1
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';


	EXCEPTION
		WHEN li_ParametersAreNull THEN
		
		ls_ErrorMsg:=  SQLERRM ||  '-GetEmarkPassengerWithTR.There is at least one parameters NULL.'  ;
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
												  ad_operatorid    => ls_OperatorId,
												  ad_daterecorded  => SYSDATE,
												  as_processname   => ' Reports_Package.GetInfoReportPassenger',
												  ax_recorddata    => 'ps_sku is parameters NULL..',
												  as_messagecode   => TO_CHAR(SQLCODE),
												  as_message       => ls_ErrorMsg);
		
		RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

	WHEN li_ParametersAreInvalid THEN
		
		ls_ErrorMsg:=  SQLERRM || '-GetEmarkPassengerWithTR. There is at least one parameters invalid.';
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
		                                          ad_operatorid    => ls_OperatorId,
		                                          ad_daterecorded  => SYSDATE,
		                                          as_processname   => ' Reports_Package.GetInfoReportPassenger',
		                                          ax_recorddata    => 'There is at least one parameters invalid.',
		                                          as_messagecode   => TO_CHAR(SQLCODE),
		                                          as_message       => ls_ErrorMsg);
		
		RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);
		
	WHEN others THEN
		ls_ErrorMsg:=  SQLERRM || '-GetEmarkPassengerWithTR. An error have ocurred.(WHEN others)' || SQLERRM;
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
		                                          ad_operatorid    => ls_OperatorId,
		                                          ad_daterecorded  => SYSDATE,
		                                          as_processname   =>' Reports_Package.GetEmarkPassengerWithTR',
		                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
		                                          as_messagecode   => TO_CHAR(SQLCODE),
		                                          as_message       =>ls_ErrorMsg);
		
		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END GetEmarkPassengerWithTR;

	PROCEDURE GetTraceabilityReportInfo(pc_Traceability          OUT RETCURSOR,
									    ps_CertificateNumber   IN    VARCHAR2,
									    pi_certificationTypeID IN    NUMBER,
									    ps_IncludeArchived     IN    VARCHAR2 )
	AS
    /************************************************************************************************
     Procedure Name - GetTraceabilityReportInfo
     Change History
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1       9/17/2012    Krishna    - AS per PRJ3617
                                            - Added Matl_Num wherever SKU is available in SELECT list of the query
                                            - Added TireSize,ServiceDescription in SELECT list
          1.2      11/14/2012    Krishna    - Added Brand,Brand_Line to the SELECT query
          1.3      11/6/2013     Jill Seitz - rewrote to eliminate TRAC
          1.4      11/11/2013    Harini     - Added new paramter ps_IncludeArchived AND replaced the query that is given
                                              by Jill FOR Traceability_Vw
    *************************************************************************************************/

		ls_MachineId    VARCHAR2(50):=NULL;
		ls_OperatorId   VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000);
		ld_ArchiveDate  DATE:=NULL;

	BEGIN

		IF upper(ps_IncludeArchived) = 'Y' THEN
			ld_ArchiveDate := SYSDATE; -- THIS WILL GET ALL
		ELSE
			ld_ArchiveDate := TO_DATE('01/01/1902','MM/DD/YYYY');--NULL VALUES WILL BE CONVERTED TO 1901
		END IF;

		IF (ps_CertificateNumber IS NOT NULL  OR ps_CertificateNumber <> '' ) AND pi_certificationTypeID > 0 THEN
			
			OPEN pc_Traceability FOR
				SELECT DISTINCT  CE.CertificateNumber,
				                 CT.CERTIFICATIONTYPEID,
				                 CT.CERTIFICATIONTYPENAME,
				                 P.SKU,
				                 LPAD(P.MATL_NUM,18,0) AS MATL_NUM, -- ADded AS per PRJ3617
				                 PCE.DATESUBMITTED,
				                 PCE.DATEASSIGNED_EGI,
				                 PCE.DATEAPPROVED_CEGI,
				                 (CASE
				                 WHEN CE.CERTIFICATENUMBER IS NOT NULL THEN 'Y'
				                 WHEN CE.CERTIFICATENUMBER IS NULL THEN 'N'
				                 END)  CERTIFICATEREQUESTED,
				                 P.SIZESTAMP TIRESIZE, -- ADDED AS PER PRJ3617
				                 CASE WHEN P.DUALLOADINDEX IS NULL OR P.DUALLOADINDEX = '0'
				                 THEN P.SINGLOADINDEX||P.SPEEDRATING
				                 ELSE P.SINGLOADINDEX||'/'||P.DUALLOADINDEX||P.SPEEDRATING
				                 END SERVICEDESCRIPTION,-- ADDED AS PER PRJ3617
				                 P.BRAND,-- ADDED AS PER PRJ3617
				                 P.BRAND_LINE -- ADDED AS PER PRJ3617
				FROM  ICS.PRODUCT P
				INNER JOIN ICS.PRODUCTCERTIFICATE PCE ON PCE.SKUID = P.SKUID
				LEFT OUTER JOIN ICS.CERTIFICATE CE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				LEFT OUTER JOIN ICS.CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
				  AND CE.CERTIFICATIONTYPEID = pi_CertificationTypeId
				  AND NVL(CE.ARCHIVEDATE_CEGI,TO_DATE('01/01/1901','MM/DD/YYYY')) < ld_ArchiveDate
				ORDER BY  DATEAPPROVED_CEGI DESC,DATESUBMITTED DESC;

		ELSIF (ps_CertificateNumber IS NULL  OR ps_CertificateNumber = '' ) AND pi_certificationTypeID  > 0 THEN
			
			OPEN pc_Traceability FOR
				SELECT DISTINCT  CE.CertificateNumber,
				                 CT.CERTIFICATIONTYPEID,
				                 CT.CERTIFICATIONTYPENAME,
				                 P.SKU,
				                 LPAD(P.MATL_NUM,18,0) AS MATL_NUM, -- ADDED AS per PRJ3617
				                 PCE.DATESUBMITTED,
				                 PCE.DATEASSIGNED_EGI,
				                 PCE.DATEAPPROVED_CEGI,
				                 (CASE
				                 WHEN CE.CERTIFICATENUMBER IS NOT NULL THEN 'Y'
				                 WHEN CE.CERTIFICATENUMBER IS NULL THEN 'N'
				                 END)  CERTIFICATEREQUESTED,
				                 P.SIZESTAMP TIRESIZE, -- ADDED AS PER PRJ3617
				                 CASE WHEN P.DUALLOADINDEX IS NULL OR P.DUALLOADINDEX = '0'
				                 THEN P.SINGLOADINDEX||P.SPEEDRATING
				                 ELSE P.SINGLOADINDEX||'/'||P.DUALLOADINDEX||P.SPEEDRATING
				                 END SERVICEDESCRIPTION,-- ADDED AS PER PRJ3617
				                 P.BRAND,-- ADDED AS PER PRJ3617
				                 P.BRAND_LINE -- ADDED AS PER PRJ3617
				FROM  ICS.PRODUCT P
				INNER JOIN ICS.PRODUCTCERTIFICATE PCE ON PCE.SKUID = P.SKUID
				LEFT OUTER JOIN ICS.CERTIFICATE CE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				LEFT OUTER JOIN ICS.CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				WHERE CT.CERTIFICATIONTYPEID = pi_CertificationTypeId
				  AND NVL(CE.ARCHIVEDATE_CEGI,TO_DATE('01/01/1901','MM/DD/YYYY')) <  ld_ArchiveDate
				ORDER BY  DATEAPPROVED_CEGI DESC,DATESUBMITTED DESC;

		elsif  (ps_CertificateNumber IS NOT NULL  OR ps_CertificateNumber <> '' ) AND pi_certificationTypeID  = 0 THEN
		
			OPEN pc_Traceability FOR
				SELECT DISTINCT  CE.CertificateNumber,
				                 CT.CERTIFICATIONTYPEID,
				                 CT.CERTIFICATIONTYPENAME,
				                 P.SKU,
				                 LPAD(P.MATL_NUM,18,0) AS MATL_NUM, -- ADDED AS PER PRJ3617
				                 PCE.DATESUBMITTED,
				                 PCE.DATEASSIGNED_EGI,
				                 PCE.DATEAPPROVED_CEGI,
				                 (CASE
				                 WHEN CE.CERTIFICATENUMBER IS NOT NULL THEN 'Y'
				                 WHEN CE.CERTIFICATENUMBER IS NULL THEN 'N'
				                 END)  CERTIFICATEREQUESTED,
				                 P.SIZESTAMP TIRESIZE, -- ADDED AS PER PRJ3617
				                 CASE WHEN P.DUALLOADINDEX IS NULL OR P.DUALLOADINDEX = '0'
				                 THEN P.SINGLOADINDEX||P.SPEEDRATING
				                 ELSE P.SINGLOADINDEX||'/'||P.DUALLOADINDEX||P.SPEEDRATING
				                 END SERVICEDESCRIPTION,-- ADDED AS PER PRJ3617
				                 P.BRAND,-- ADDED AS PER PRJ3617
				                 P.BRAND_LINE -- Added AS per PRJ3617
				FROM ICS.PRODUCT P
				INNER JOIN ICS.PRODUCTCERTIFICATE PCE ON PCE.SKUID = P.SKUID
				LEFT OUTER JOIN ICS.CERTIFICATE CE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
				LEFT OUTER JOIN ICS.CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
				  AND NVL(CE.ARCHIVEDATE_CEGI,TO_DATE('01/01/1901','MM/DD/YYYY' )) <  ld_ArchiveDate
				  ORDER BY  DATEAPPROVED_CEGI DESC,DATESUBMITTED DESC;
		
		END IF;

	EXCEPTION
		WHEN OTHERS THEN
		
		ls_ErrorMsg:=  SQLERRM || '-GetTraceabilityReportInfo. An error have ocurred.(WHEN others)' || SQLERRM;
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
		                                          ad_operatorid    => ls_OperatorId,
		                                          ad_daterecorded  => SYSDATE,
		                                          as_processname   =>' Reports_Package.GetTraceabilityReportInfo',
		                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
		                                          as_messagecode   => TO_CHAR(SQLCODE),
		                                          as_message       =>ls_ErrorMsg);
		
		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetTraceabilityReportInfo;

    PROCEDURE GetAuthenticityReportInfo(pc_Authenticity OUT RETCURSOR)
	AS
		ls_ErrorMsg       VARCHAR2(4000):=NULL;
		ls_MachineId      VARCHAR2(50):=NULL;
		ls_OperatorId     VARCHAR2(50):='ICSDEV';
    
	BEGIN

		OPEN pc_Authenticity FOR
			SELECT DISTINCT  p.emarkreference_i  AS CERTIFICATENUMBER
			FROM ICS.CERTIFICATE C, 
			     ICS.PRODUCTCERTIFICATE PC, 
				 ICS.PRODUCT P
			WHERE C.CERTIFICATEID = PC.CERTIFICATEID
			  AND C.CERTIFICATIONTYPEID = 4
			  AND PC.DATESUBMITTED  IS NULL
			  AND PC.SKUID =  P.SKUID
			  AND P.EMARKREFERENCE_I IS NOT NULL;

	EXCEPTION
		WHEN OTHERS THEN
		
		ls_ErrorMsg:=  SQLERRM || '-AuthenticityReportInfo. An error have ocurred.(WHEN others)' || SQLERRM;
		
		APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => ls_MachineId,
		                                          ad_operatorid    => ls_OperatorId,
		                                          ad_daterecorded  => SYSDATE,
		                                          as_processname   =>' Reports_Package.AuthenticityReportInfo',
		                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
		                                          as_messagecode   => TO_CHAR(SQLCODE),
		                                          as_message       =>ls_ErrorMsg);
		
		RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
    END GetAuthenticityReportInfo;

  	PROCEDURE GetExceptionReportInfo(pc_Exception OUT RETCURSOR)
	AS
   /************************************************************************************************
     Procedure Name - GetExceptionReportInfo
     Change History
      --------------------------------------------------------------------------
      Version No  Date          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - AS per PRJ3617
                                            - Added Matl_Num wherever SKU is available in SELECT list of the query
    *************************************************************************************************/

		ls_MachineId    VARCHAR2(50):=NULL;
		ls_OperatorId   VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
   BEGIN

		---LOAD EXCEPTION TABLE.
		CompareSKUMainProductColumns;

		---EXTRACT CURSOR FOR REPORT
		OPEN pc_Exception FOR
		  SELECT DISTINCT
				 ER.SKU,
				 LPAD(ER.MATL_NUM,18,0) AS MATL_NUM, -- ADDED AS PER PRJ3617
				 ER.PRODUCTDATAFIELDNAME,
				 ER.LASTMODIFIED,
				 ER.ICSVALUE,
				 ER.SKUMASTERVALUE,
				 P.SIZESTAMP AS TIRESIZE,
				 CASE WHEN P.DUALLOADINDEX IS NULL OR P.DUALLOADINDEX = 0
					  THEN P.SINGLOADINDEX||P.SPEEDRATING
				 ELSE P.SINGLOADINDEX||'/'|| P.DUALLOADINDEX|| P.SPEEDRATING END AS ServiceDescription
		 FROM ICS.EXCEPTIONREPORT ER,
			  ICS.PRODUCT P
		WHERE ER.MATL_NUM = LPAD(P.MATL_NUM,18,0)
		ORDER BY LPAD(ER.MATL_NUM,18,0),ER.PRODUCTDATAFIELDNAME ;

	EXCEPTION
		WHEN OTHERS THEN
			ls_ErrorMsg:=  SQLERRM || '-GetExceptionReportInfo. An error have ocurred.(WHEN others)' || SQLERRM;

			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   =>' Reports_Package.GetExceptionReportInfo',
													  ax_recorddata    => 'An error have ocurred.(WHEN others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       =>ls_ErrorMsg);
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END  GetExceptionReportInfo;

	PROCEDURE CompareSKUMainProductColumns 
	AS
	
	/************************************************************************************************
	Procedure Name - CompareSKUMainProductColumns
	Change History
	--------------------------------------------------------------------------
	Version No  Date          Author    Description
	---------------------------------------------------------------------------
	1.0                             Intial Version
	1.1     9/17/2012     Krishna
	11/1/2018      Jill  Seitz - fixed problem in differences report WHERE it was not finding speed rating changes.
	*************************************************************************************************/

		ls_MachineId      VARCHAR2(50):=NULL;
		ls_OperatorId     VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg       VARCHAR2(4000):=NULL;

		-- Added matl_num, brand, brand_line instead of sku AND brandcode
		CURSOR Prod IS
			SELECT P.SKUID,
				   P.SKU,P.MATL_NUM AS MATL_NUM,
				   P.BRAND,P.BRAND_LINE
			FROM PRODUCT P
			WHERE P.SKUID =(SELECT MAX(P2.SKUID)  ---  MOST RECENT PRODUCT RECORD
							FROM PRODUCT P2
							WHERE P2.MATL_NUM = LPAD(P.MATL_NUM,18,0))
			 AND P.SKUID IN (SELECT SKUID 
			                 FROM PRODUCTCERTIFICATE PC, CERTIFICATE C
							 WHERE PC.CERTIFICATEID = C.CERTIFICATEID 
							   AND UPPER(MOSTRECENTCERT) = 'Y') --- skuid is ON a current certificate
			 AND P.DISCONTINUEDDATE IS NULL -- only care about differences in active sku's
			 AND SUBSTR(LTRIM(p.MATL_NUM,'0'),1,3) <> '999' ;

		ls_Sku                      VARCHAR(30):=NULL;

		ls_Matl_Num                 PRODUCT.MATL_NUM%TYPE:=NULL;
		ls_Brand                    PRODUCT.BRAND%TYPE:=NULL;
		ls_Brand_Line               PRODUCT.BRAND_LINE%TYPE:=NULL;

		ls_SMColNames               VARCHAR2(200):=NULL;
		ls_ProdColNames             VARCHAR2(200):=NULL;
		ls_Sql                      VARCHAR2(5000):=NULL;

		ls_SkuMainValue             VARCHAR2(200):=NULL;
		ls_ProductValue             VARCHAR2(200):=NULL;
	
		ld_ModifiedOn               DATE:=NULL;
		ln_count                    INTEGER:=NULL;
		ln_reccnt                   INTEGER:=NULL;
		ls_matchfound               VARCHAR2 (5):='N';
		ln_skuid                    NUMBER:=NULL;
		ln_p_TIRETYPEID             NUMBER:=NULL;
		ls_p_SIZESTAMP              VARCHAR2(30):=NULL;
		ls_p_SPEEDRATING            VARCHAR2(10):=NULL;
		ls_p_SINGLOADINDEX          VARCHAR2(10):=NULL;
		ls_p_DUALLOADINDEX          VARCHAR2(10):=NULL;
		ls_p_TUBELESSYN             VARCHAR2(1):=NULL;
		ls_p_REINFORCEDYN           VARCHAR2(1):=NULL;
		ls_p_EXTRALOADYN            VARCHAR2(1):=NULL;
		ls_p_UTQGTEMP               VARCHAR2(10):=NULL;
		ls_p_UTQGTRACTION           VARCHAR2(10):=NULL;
		ls_p_UTQGTREADWEAR          VARCHAR2(10):=NULL;
		ls_p_MudSnowYN              VARCHAR2(5):=NULL;
		ls_p_LOADRANGE              VARCHAR2(30):=NULL;
		ls_p_RegroovableInd         VARCHAR2(5):=NULL;
		ls_p_TreadPattern           VARCHAR2(30):=NULL;

		ln_p_TPN                    PRODUCT.TPN%TYPE:=NULL; -- Added AS per PRJ3617
		ls_p_BIASBELTEDRADIAL       VARCHAR2(10):=NULL;
		ls_p_SevereWeatherInd       VARCHAR2(5):=NULL;

		ln_sm_TIRETYPEID            NUMBER:=NULL;
		ls_sm_SPEEDRATING           VARCHAR2(10):=NULL;
		ls_sm_SINGLOADINDEX         VARCHAR2(10):=NULL;
		ls_sm_TUBELESSYN            VARCHAR2(1):=NULL;
		ls_sm_REINFORCEDYN          VARCHAR2(1):=NULL;
		ls_sm_EXTRALOADYN           VARCHAR2(1):=NULL;
		ls_sm_UTQGTEMP              VARCHAR2(10):=NULL;
		ls_sm_UTQGTRACTION          VARCHAR2(10):=NULL;
		ls_sm_UTQGTREADWEAR         VARCHAR2(10):=NULL;
		ls_sm_LOADRANGE             VARCHAR2(30):=NULL;
		ln_sm_TPN                   PRODUCT.TPN%TYPE:=NULL;
		ls_sm_BIASBELTEDRADIAL      VARCHAR2(10):=NULL;
		--
		ls_SM_Brand                 VARCHAR2(30):=NULL;
		ls_SM_BrandLine             VARCHAR2(30):=NULL;
		ls_SM_SizeStamp             VARCHAR2(30):=NULL;
		ls_SM_PSN                   VARCHAR2(30):=NULL;
		ls_SM_DiscontinueDate       VARCHAR2(30):=NULL;
		ls_SM_SpecNumber            VARCHAR2(30):=NULL;
		ls_SM_DualLoadIndex         VARCHAR2(30):=NULL;
		ls_SM_MudSnowYN             VARCHAR2(5):=NULL;
		ls_SM_SevereWeatherInd      VARCHAR2(5):=NULL;
		ls_SM_RimDiameter           VARCHAR2(10):=NULL;
		ls_SM_SerialDate            VARCHAR2(30):=NULL;
		ls_SM_MeaRimWidth           VARCHAR2(10):=NULL;
		ls_SM_RegroovableInd        VARCHAR2(5):=NULL;
		ls_SM_PlantProduced         VARCHAR2(10):=NULL;
		ls_SM_MostRecentDate        VARCHAR2(30):=NULL;
		ls_SM_IMark                 VARCHAR2(30):=NULL;
		ls_SM_InformeNumber         VARCHAR2(30):=NULL;
		ls_SM_FechaDate             VARCHAR2(30):=NULL;
		ls_SM_TreadPattern          VARCHAR2(30):=NULL;
		ls_SM_SpecialProtBrand      VARCHAR2(30):=NULL;
		ls_SM_NominalTireWidth      VARCHAR2(30):=NULL;
		ls_SM_AspectRatio           VARCHAR2(30):=NULL;
		ls_SM_TreadWearInd          VARCHAR2(30):=NULL;
		ls_SM_NameOfManufac         VARCHAR2(30):=NULL;
		ls_SM_Family                VARCHAR2(30):=NULL;
		ls_SM_DotSerialNumber       VARCHAR2(30):=NULL;
		ls_SM_SKU                   VARCHAR2(30):=NULL;

	BEGIN

		DELETE FROM EXCEPTIONREPORT;
		COMMIT;

		OPEN Prod;
		LOOP
			FETCH Prod INTO ln_skuid, ls_Sku ,ls_Matl_Num,ls_Brand,ls_Brand_Line;
			EXIT WHEN Prod%NOTFOUND;
			
			IF SUBSTR(ls_Matl_Num,1,3) <> '999' THEN

				-- Replaced sku wtih Matl_num AND BrandCode with Brand AND Brand_Line
				--   AND PPN with TPN
				SELECT  P.TIRETYPEID,
				        P.SIZESTAMP, 
						P.SPEEDRATING, 
						P.SINGLOADINDEX,
						P.DUALLOADINDEX,
						P.TUBELESSYN,
						P.REINFORCEDYN, 
						P.EXTRALOADYN, 
						P.UTQGTEMP, 
						P.UTQGTRACTION,
						P.UTQGTREADWEAR, 
						P.MUDSNOWYN, 
						P.LOADRANGE,  
						P.REGROOVABLEIND,
						P.TREADPATTERN,
						P.TPN, 
						P.BIASBELTEDRADIAL, 
						P.MODIFIEDON
				    INTO
						ln_p_TIRETYPEID,
						ls_p_SIZESTAMP, 
						ls_p_SPEEDRATING, 
						ls_p_SINGLOADINDEX,
						ls_p_DUALLOADINDEX ,
						ls_p_TUBELESSYN, 
						ls_p_REINFORCEDYN, 
						ls_p_EXTRALOADYN,
						ls_p_UTQGTEMP, 
						ls_p_UTQGTRACTION, 
						ls_p_UTQGTREADWEAR,
						ls_p_MudSnowYN, 
						ls_p_LOADRANGE,  
						ls_p_RegroovableInd,
						ls_p_TreadPattern,
						ln_p_TPN, 
						ls_p_BIASBELTEDRADIAL,  
						ld_ModifiedOn
				FROM PRODUCT P
				WHERE P.SKUID = ln_skuid;


				BEGIN

					-- Called GetTireCharacteristics procedure instead of SkuMain_Vw AS per PRJ3617
					ICS_PROCS.ICS_CRUD.GetTireCharacteristicsAll(ls_Matl_Num,
					                                             ls_SM_Brand,
																 ls_SM_BrandLine, 
																 ls_SM_SizeStamp, 
																 ln_SM_TireTypeId,
																 ls_SM_PSN,
																 ls_SM_DiscontinueDate,
																 ls_SM_SpecNumber,
																 ls_SM_SpeedRating,
																 ls_SM_SingLoadIndex,
																 ls_SM_DualLoadIndex,
																 ls_SM_Tubelessyn,
																 ls_SM_ReinforcedYN,
																 ls_SM_ExtraLoadYN,
																 ls_SM_UTQGTreadWear,
																 ls_SM_UTQGTraction, 
																 ls_SM_UTQGTemp,
																 ls_SM_MudSnowYN,
																 ls_SM_SevereWeatherInd, 
																 ls_SM_RimDiameter,
																 ls_SM_SerialDate,
																 ls_SM_LoadRange,
																 ls_SM_MeaRimWidth,
																 ls_SM_RegroovableInd,
																 ls_SM_PlantProduced,
																 ls_SM_MostRecentDate,
																 ls_SM_IMark,
																 ls_SM_InformeNumber,
																 ls_SM_FechaDate, 
																 ls_SM_TreadPattern,
																 ls_SM_SpecialProtBrand, 
																 ls_SM_NominalTireWidth, 
																 ls_SM_AspectRatio,
																 ls_SM_TreadWearInd,
																 ls_SM_NameOfManufac,
																 ls_SM_Family,
																 ls_SM_DotSerialNumber,
																 ln_SM_TPN,
																 ls_SM_BiasBeltedRadial,
																 ls_SM_SKU);

					IF  ls_sm_SIZESTAMP IS NOT NULL AND  NVL(UPPER(TRIM(ls_p_SIZESTAMP)),'IsNull') <> NVL(UPPER(TRIM(ls_sm_SIZESTAMP)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											        'SIZESTAMP',
													ld_ModifiedOn, 
													NVL(ls_p_SIZESTAMP,NULL), 
													NVL(ls_sm_SIZESTAMP,NULL) );
						COMMIT;
						
					END IF;

					IF  ls_sm_SPEEDRATING IS NOT NULL AND NVL(UPPER(TRIM(ls_p_SPEEDRATING)),'IsNull') <> NVL(UPPER(TRIM(ls_sm_SPEEDRATING)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											        'SPEEDRATING',
													ld_ModifiedOn, 
													NVL(ls_p_SPEEDRATING,NULL), 
													NVL(ls_sm_SPEEDRATING,NULL) );
						
						COMMIT;
					
					END IF;

					IF  NOT ls_sm_SINGLOADINDEX IS NULL AND NVL(UPPER(ls_p_SINGLOADINDEX),'IsNull') <> NVL(UPPER(ls_sm_SINGLOADINDEX),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
					                        VALUES (ls_Matl_Num,ls_Sku,
											       'SINGLOADINDEX',
												    ld_ModifiedOn, 
													NVL(ls_p_SINGLOADINDEX,NULL), 
													NVL(ls_sm_SINGLOADINDEX,NULL) );
						COMMIT;
					
					END IF;

					IF  NOT ls_sm_DUALLOADINDEX IS NULL AND NVL(UPPER(ls_p_DUALLOADINDEX),'IsNull') <> NVL(UPPER(ls_sm_DUALLOADINDEX),'IsNull') THEN
					
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
					                        VALUES (ls_Matl_Num,ls_Sku,
											       'DUALLOADINDEX',
												    ld_ModifiedOn, 
												    NVL(ls_p_DUALLOADINDEX,NULL), 
													NVL(ls_sm_DUALLOADINDEX,NULL) );
						COMMIT;
					
					END IF;

					IF  NOT ls_sm_TUBELESSYN IS NULL AND NVL(UPPER(ls_p_TUBELESSYN),'IsNull') <> NVL(UPPER(ls_sm_TUBELESSYN),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
					                        VALUES (ls_Matl_Num,ls_Sku,
											        'TUBELESSYN',
													ld_ModifiedOn, 
													NVL(ls_p_TUBELESSYN,NULL), 
													NVL(ls_sm_TUBELESSYN,NULL) );
						COMMIT;
					
					END IF;

					IF NOT  ls_sm_REINFORCEDYN IS NULL AND NVL(UPPER(ls_p_REINFORCEDYN),'IsNull') <> NVL(UPPER(ls_sm_REINFORCEDYN),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
					                        VALUES (ls_Matl_Num,ls_Sku,'REINFORCEDYN',
											        ld_ModifiedOn, 
													NVL(ls_p_REINFORCEDYN,NULL), 
													NVL(ls_sm_REINFORCEDYN,NULL) );
						COMMIT;
						
					END IF;

					IF  NOT ls_sm_EXTRALOADYN IS NULL AND NVL(UPPER(ls_p_EXTRALOADYN),'IsNull') <> NVL(UPPER(ls_sm_EXTRALOADYN),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
					                        VALUES (ls_Matl_Num,ls_Sku,
											        'EXTRALOADYN',
													ld_ModifiedOn, 
													NVL(ls_p_EXTRALOADYN,NULL), 
													NVL(ls_sm_EXTRALOADYN,NULL) );
						COMMIT;
					
					END IF;

					--JESEITZ ADDED 11/03/2012
					IF ls_sm_UTQGTEMP = '0' OR UPPER(ls_sm_UTQGTEMP) = 'N/A' THEN
					ls_sm_UTQGTEMP := 'IsNull'   ;
					END IF;
					--

					IF NOT ls_sm_UTQGTEMP IS NULL AND NVL(TRIM(UPPER(ls_p_UTQGTEMP)),'IsNull') <> NVL(TRIM(UPPER(ls_sm_UTQGTEMP)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
											VALUES (ls_Matl_Num,ls_Sku,
											        'UTQGTEMP',
													ld_ModifiedOn, 
													NVL(ls_p_UTQGTEMP,NULL), 
													NVL(ls_sm_UTQGTEMP,NULL) );
						COMMIT;
					
					END IF;

					IF NOT  ls_sm_UTQGTRACTION IS NULL AND NVL(TRIM(UPPER(ls_p_UTQGTRACTION)),'0') <> NVL(TRIM(UPPER(ls_sm_UTQGTRACTION)),'0') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
											VALUES (ls_Matl_Num,ls_Sku,
											        'UTQGTRACTION',
													ld_ModifiedOn, 
													NVL(ls_p_UTQGTRACTION,NULL), 
													NVL(ls_sm_UTQGTRACTION,NULL) );
					
						COMMIT;
					
					END IF;

					--JESEITZ ADDED 11/03/2012
					IF ls_sm_UTQGTREADWEAR = '0' OR UPPER(ls_sm_UTQGTREADWEAR) = 'N/A' THEN
						ls_sm_UTQGTREADWEAR := ''   ;
					END IF;
					--
					IF  NOT ls_sm_UTQGTREADWEAR IS NULL AND NVL(TRIM(UPPER(ls_p_UTQGTREADWEAR)),'IsNull') <> NVL(TRIM(UPPER(ls_sm_UTQGTREADWEAR)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                     VALUES (ls_Matl_Num,ls_Sku,
											        'UTQGTREADWEAR',
													 ld_ModifiedOn, 
													 NVL(ls_p_UTQGTREADWEAR,NULL), 
													 NVL(ls_sm_UTQGTREADWEAR,NULL));
						
						COMMIT;
						
					END IF;

					IF  NOT ls_SM_MudSnowYN IS NULL AND NVL(TRIM(UPPER(ls_p_MudSnowYN)),'IsNull') <> NVL(TRIM(UPPER(ls_SM_MudSnowYN)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
					                        VALUES (ls_Matl_Num,ls_Sku,
											       'MUDSNOWYN',
												   ld_ModifiedOn, 
												   NVL(ls_p_MudSnowYN,NULL), 
												   NVL(ls_SM_MudSnowYN,NULL));
					     COMMIT;
						 
					END IF;

					IF NOT  ls_sm_LOADRANGE IS NULL AND NVL(UPPER(ls_p_LOADRANGE),'IsNull') <> NVL(UPPER(ls_sm_LOADRANGE),'IsNull') THEN
					
						INSERT INTO EXCEPTIONREPORT (MATL_NUM,
													 SKU,
													 PRODUCTDATAFIELDNAME,
													 LASTMODIFIED,
													 ICSVALUE,
													 SKUMASTERVALUE)
						                     VALUES (ls_Matl_Num,ls_Sku,
											         'LOADRANGE',
													 ld_ModifiedOn, 
													 NVL(ls_p_LOADRANGE,NULL), 
													 NVL(ls_sm_LOADRANGE,NULL) );
						COMMIT;
					
					END IF;

					IF  NOT ls_SM_RegroovableInd IS NULL AND NVL(TRIM(UPPER( ls_p_RegroovableInd)),'IsNull') <> NVL(TRIM(UPPER( ls_SM_RegroovableInd)),'IsNull') THEN
					
						INSERT INTO EXCEPTIONREPORT (MATL_NUM,
													 SKU,
													 PRODUCTDATAFIELDNAME,
													 LASTMODIFIED,
													 ICSVALUE,
													 SKUMASTERVALUE)
											 VALUES (ls_Matl_Num,ls_Sku,
													 'REGROOVABLEIND',
													 ld_ModifiedOn, 
													 NVL( ls_p_RegroovableInd,NULL), 
													 NVL( ls_SM_RegroovableInd,NULL));
						COMMIT;
						
					END IF;

					IF  NOT  ls_SM_TreadPattern IS NULL AND NVL(TRIM(UPPER( ls_p_TreadPattern)),'IsNull') <> NVL(TRIM(UPPER( ls_SM_TreadPattern)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT (MATL_NUM,
													 SKU,
													 PRODUCTDATAFIELDNAME,
													 LASTMODIFIED,
													 ICSVALUE,
													 SKUMASTERVALUE)
											 VALUES (ls_Matl_Num,ls_Sku,
													'TREADPATTERN',
													 ld_ModifiedOn, 
													 NVL( ls_p_TreadPattern,NULL), 
													 NVL( ls_SM_TreadPattern,NULL));
						COMMIT;
						
					END IF;

					-- Replaced PPN with TPN
					IF NOT ln_sm_TPN IS NULL AND NVL(UPPER(ln_p_TPN),'IsNull') <> NVL(UPPER(ln_sm_TPN),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											       'TPN',ld_ModifiedOn, 
												    NVL(ln_p_TPN,NULL), 
													NVL(ln_sm_TPN,NULL));
						COMMIT;
						
					END IF;

					IF NOT ls_sm_BIASBELTEDRADIAL IS NULL AND NVL(UPPER(ls_p_BIASBELTEDRADIAL),'IsNull') <> NVL(UPPER(ls_sm_BIASBELTEDRADIAL),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											        'BIASBELTEDRADIAL',
													ld_ModifiedOn, 
													NVL(ls_p_BIASBELTEDRADIAL,NULL), 
													NVL(ls_sm_BIASBELTEDRADIAL,NULL) );
						COMMIT;
						
					END IF;

					IF  NOT ls_SM_BRAND IS NULL AND NVL(UPPER(ls_BRAND),'IsNull') <> NVL(UPPER(ls_sm_BRAND),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											       'BRAND',
												   ld_ModifiedOn, 
												   NVL(ls_BRAND,NULL), 
												   NVL(ls_sm_BRAND,NULL));
						COMMIT;
						
					END IF;

					IF NOT  ls_SM_BrandLine IS NULL AND NVL(UPPER(  ls_Brand_Line),'IsNull') <> NVL(UPPER(ls_SM_BrandLine),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											        'BRANDLINE',
													ld_ModifiedOn, 
													NVL(ls_BRAND_LINE,NULL), 
													NVL(ls_SM_BrandLine,NULL));
						COMMIT;
						
					END IF;

					IF NOT ls_SM_SevereWeatherInd IS NULL AND NVL(TRIM(UPPER( ls_p_SevereWeatherInd)),'IsNull') <> NVL(TRIM(UPPER (ls_p_SevereWeatherInd)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                    VALUES (ls_Matl_Num,ls_Sku,
											        'SEVEREWEATHERIND',
													ld_ModifiedOn, 
													NVL( ls_p_SevereWeatherInd,NULL), 
													NVL(ls_SM_SevereWeatherInd,NULL) );
						COMMIT;
						
					END IF;

					-- JESEITZ added 4/27/2018 - per REQ0029020
					IF  NOT ls_SM_SKU IS NULL AND NVL(TRIM(UPPER( ls_Sku)),'IsNull') <> NVL(TRIM(UPPER (ls_SM_SKU)),'IsNull') THEN
						
						INSERT INTO EXCEPTIONREPORT(MATL_NUM,
						                            SKU,
													PRODUCTDATAFIELDNAME,
													LASTMODIFIED,
													ICSVALUE,
													SKUMASTERVALUE)
						                     VALUES (ls_Matl_Num,
											         ls_Sku,
													 'SKU',
													 ld_ModifiedOn, 
													 NVL(ls_sku,NULL), 
													 NVL(ls_SM_SKU,NULL) );
						COMMIT;
						
					END IF;

					EXCEPTION
						WHEN OTHERS THEN
					NULL;
				END;
			END IF;
		END loop;
		close Prod;

	EXCEPTION
		WHEN OTHERS THEN
			DBMS_OUTPUT.PUT_LINE(ls_Matl_Num||'-'||ls_brand ||' - '||ls_brand_line ||'-' ||ls_SMColNames||'-'||ls_SkuMainValue||'-'||ls_ProductValue);
			
			ls_ErrorMsg:=  SQLERRM || '-CompareSKUMainProductColumns. An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   =>' Reports_Package.CompareSKUMainProductColumns',
													  ax_recorddata    => 'An error has ocurred.(WHEN others)',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END CompareSKUMainProductColumns;
	
	PROCEDURE GETEMARKTESTREPORTINFO(PS_CERTIFICATENUMBER IN    VARCHAR2,
									 PI_TIRETYPEID        IN    NUMBER,
									 PC_CERTIFICATEDFVALUE  OUT RETCURSOR,
									 PC_CERTIFICATEINFO     OUT RETCURSOR,
									 PC_PRODUCT             OUT RETCURSOR,
									 PC_MEASUREHDR          OUT RETCURSOR,
									 PC_MEASUREDTL          OUT RETCURSOR,
									 PC_BEADUNSEATHDR       OUT RETCURSOR,
									 PC_BEADUNSEATDTL       OUT RETCURSOR,
									 PC_PLUNGERHDR          OUT RETCURSOR,
									 PC_PLUNGERDTL          OUT RETCURSOR,
									 PC_TREADWEARHDR        OUT RETCURSOR,
									 PC_TREADWEARDTL        OUT RETCURSOR,
									 PC_ENDURANCEHDR        OUT RETCURSOR,
									 PC_ENDURANCEDTL        OUT RETCURSOR,
									 PC_HIGHSPEEDHDR        OUT RETCURSOR,
									 PC_HIGHSPEEDDTL        OUT RETCURSOR,
									 PC_SPEEDTESTDETAIL     OUT RETCURSOR,
									 PC_BRAND               OUT RETCURSOR)
	AS
    
	/************************************************************************************************
     Procedure Name - GetEmarkTestReportInfo
     Change History
      --------------------------------------------------------------------------
      Version No  DATE          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - AS per PRJ3617
                                            - Replaced NPRID with PSN
                                            - Added Matl_Num wherever SKU exist
                                            - Replaced query instead of Brand_View
                                            - Added Brand, Brand_Line columns instead of Brandcode
    *************************************************************************************************/

		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId      VARCHAR2(50):=NULL;
		ls_OperatorId     VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg       VARCHAR2(4000):=NULL;
	
	BEGIN

		IF ps_CertificateNumber IS NULL THEN
			RAISE li_ParametersAreNull;
		END IF ;
		
		IF ps_CertificateNumber = '' THEN
			RAISE li_ParametersAreInvalid;
		END IF ;


		-- Gets the brand information
		-- Added query instead of Brand_View
		-- Added Brand AND Brand_Line instead of BrandCode AND BrandName
		OPEN pc_brand FOR
			SELECT DISTINCT BRAND,
					        BRAND_LINE,
					        CERTIFICATENUMBER---but just get each brand once, even though it is multiple extensions.
			FROM (SELECT DISTINCT p.Brand,
			                      p.Brand_Line,
								  c.CertificateNumber,
								  c.Extension_En
				  FROM PRODUCT P,
					   PRODUCTCERTIFICATE PC,
					   CERTIFICATE C
				  WHERE C.CERTIFICATEID = PC.CERTIFICATEID
				   AND PC.SKUID = p.SkuId)
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);

			--Gets the default values information.
			OPEN pc_CertificateDfValue FOR
				SELECT CERTIFICATENUMBER,
				       MAX(DECODE(FIELDID,1,FIELDVALUE,NULL))   AS MANUFACTURERNAME_E,
				       MAX(DECODE(FIELDID,2,FIELDVALUE,NULL))   AS MANUFACTURERNAMEADDRESS_E,
				       MAX(DECODE(FIELDID,3,FIELDVALUE,NULL))   AS TECHNICALSERVICE_E,
				       MAX(DECODE(FIELDID,4,FIELDVALUE,NULL))   AS PLACE_E,
				       MAX(DECODE(FIELDID,5,FIELDVALUE,NULL))   AS MEASURERIM_E,
				       MAX(DECODE(FIELDID,6,FIELDVALUE,NULL))   AS INFLATIONPRESSURE_E,
				       MAX(DECODE(FIELDID,7,FIELDVALUE,NULL))   AS TESTLABORATORY_E,
				       MAX(DECODE(FIELDID,8,FIELDVALUE,NULL))   AS REPRESENTATIVENAME_E,
				       MAX(DECODE(FIELDID,9,FIELDVALUE,NULL))   AS REPRESENTATIVEADDRESS_E,
				       MAX(DECODE(FIELDID,10,FIELDVALUE,NULL))  AS REASONOFEXTENSION_E,
				       MAX(DECODE(FIELDID,11,FIELDVALUE,NULL))  AS REMARKS_E,
				       MAX(DECODE(FIELDID,175,FIELDVALUE,NULL)) AS PPNPROFILEFAMILY_E,
				       MAX(DECODE(FIELDID,176,FIELDVALUE,NULL)) AS RIMSMOUNTED_E,
				       MAX(DECODE(FIELDID,177,FIELDVALUE,NULL)) AS OVERALLDIMENSIONSTYPE_E,
				       MAX(DECODE(FIELDID,178,FIELDVALUE,NULL)) AS REFERENCETIRE_E,
				       MAX(DECODE(FIELDID,179,FIELDVALUE,NULL)) AS NOMINALRIMSIZE_E,
				       MAX(DECODE(FIELDID,180,FIELDVALUE,NULL)) AS NOMINALLOAD_E,
				       MAX(DECODE(FIELDID,181,FIELDVALUE,NULL)) AS ADDITIONALNOMSPEED_E,
				       MAX(DECODE(FIELDID,182,FIELDVALUE,NULL)) AS ADDITIONALLOADINDEX_E,
				       MAX(DECODE(FIELDID,183,FIELDVALUE,NULL)) AS APPLICATIONCOMMENT_E,
				       MAX(DECODE(FIELDID,189,FIELDVALUE,NULL)) AS NOMTESTDRUMSPEED_E
				FROM (SELECT FIELDID,
							 CERTIFICATIONTYPEID,
							 CERTIFICATENUMBER,
							 FIELDVALUE
				      FROM DEFAULTVALUES_VIEW
				      WHERE LOWER(CERTIFICATENUMBER)=LOWER(ps_certificateNumber) 
					    AND CERTIFICATIONTYPEID = 1)
				GROUP BY CERTIFICATENUMBER ;

			OPEN pc_CertificateInfo FOR
				SELECT CERTIFICATEID,
				       CE.CERTIFICATIONTYPEID,
				       CERTIFICATIONTYPENAME,
				       CERTIFICATENUMBER,
				       ACTIVESTATUS,
				       RENEWALREQUIRED_CGIN,
				       JOBREPORTNUMBER_CEN,
				       EXTENSION_EN,
				       SUPPLEMENTALMOLDSTAMPING_E,
				       CERTDATESUBMITTED,
				       CERTDATEAPPROVED
				FROM CERTIFICATE CE
				INNER JOIN CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATIONTYPEID = 1 
				  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
				  AND LOWER(CE.MOSTRECENTCERT) = 'y'  ;

		OPEN pc_Product FOR
			SELECT DISTINCT SKUID,  
			                SKU, 
							LPAD(MATL_NUM,18,0) AS MATL_NUM, 
							BRAND,BRAND_LINE, 
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
							CERTIFICATENUMBER,  
							SUPPLEMENTALEXTENSION_EN,
							ROWNUM
			FROM  PRODUCTDATA_REPORT_VIEW
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber)  
			 AND TIRETYPEID = pi_tiretypeid;

		OPEN pc_MeasureHDR FOR
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
			        SKU,
			        LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
			FROM CERTIFICATE CE
			INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE CE.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND UPPER(CE.MOSTRECENTCERT) = 'Y';

		OPEN pc_measureDtl FOR
			SELECT MD.MEASUREID,
				   SECTIONWIDTH,
				   OVERALLWIDTH,
				   ITERATION
			FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID 
			AND LOWER(CE.MOSTRECENTCERT) = 'y'
			INNER JOIN MEASUREDTL MD ON M.MEASUREID = MD.MEASUREID
			WHERE M.CERTIFICATIONTYPEID = 1 
			 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber);

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
			       SKU,
			       LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
			FROM CERTIFICATE CE INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
			WHERE B.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_BEADUNSEATDTL FOR
			SELECT BD.BEADUNSEATID,
				   UNSEATFORCE,
				   ITERATION
			FROM CERTIFICATE CE
			INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
			INNER JOIN BEADUNSEATDTL BD ON B.BEADUNSEATID = BD.BEADUNSEATID
			WHERE B.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_PLUNGERHDR FOR
			SELECT  PLUNGERID,
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
					SKU,
					LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
			FROM CERTIFICATE CE
			INNER JOIN  PLUNGERHDR P ON CE.CERTIFICATEID = P.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
			WHERE P.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
			  AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_PLUNGERDTL FOR
			SELECT  pd.PLUNGERID,
					BREAKINGENERGY,
					ITERATION
			FROM CERTIFICATE CE
			INNER JOIN PLUNGERHDR PH ON CE.CERTIFICATEID = PH.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PH.CERTIFICATIONTYPEID
			INNER JOIN PLUNGERDTL PD ON PH.PLUNGERID = PD.PLUNGERID
			WHERE PH.CERTIFICATIONTYPEID = 1 
			  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
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
			       SKU,
			       LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
			FROM CERTIFICATE CE
			INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			WHERE T.CERTIFICATIONTYPEID = 1 AND
			LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
			AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_TREADWEARDTL FOR
			SELECT TD.TREADWEARID,
				   WEARBARHEIGHT,
				   ITERATION
			FROM CERTIFICATE CE
			INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID  = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			INNER JOIN TREADWEARDTL TD ON T.TREADWEARID = TD.TREADWEARID
			WHERE T.CERTIFICATIONTYPEID = 1 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
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
			       SKU,
			       LPAD(MATL_NUM,18,0) AS MATL_NUM, -- Added AS per PRJ3617
			       ROUND(ENS.SPEED*.62)*5 DRUMSPEED
			FROM CERTIFICATE CE INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
			LEFT OUTER JOIN (SELECT ENDURANCEID, MAX(SPEED) SPEED FROM ENDURANCEDTL ENDL GROUP BY ENDURANCEID) ENS
			ON ENS.ENDURANCEID = E.ENDURANCEID
			WHERE E.CERTIFICATIONTYPEID = 1 
			 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
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
			INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
			INNER JOIN ENDURANCEDTL ED ON E.ENDURANCEID = ED.ENDURANCEID
			WHERE E.CERTIFICATIONTYPEID = 1 
			 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
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
			       HSL.LOAD MAXLOAD,
			       SKU,
			       LPAD(MATL_NUM,18,0) AS MATL_NUM-- ADDED AS PER PRJ3617
			FROM CERTIFICATE CE INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			LEFT OUTER JOIN (SELECT HIGHSPEEDID, MAX(LOAD) LOAD FROM HIGHSPEEDDTL HSD GROUP BY HIGHSPEEDID) HSL
			ON HSL.HIGHSPEEDID = H.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 1 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
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
			       HD.HIGHSPEEDID
			FROM CERTIFICATE CE
			INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			INNER JOIN HIGHSPEEDDTL HD ON H.HIGHSPEEDID = HD.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 1 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			 AND LOWER(CE.MOSTRECENTCERT) = 'y';

		OPEN pc_SPEEDTESTDETAIL FOR
			SELECT  ITERATION,
					TIME,
					SPEED,
					s.HIGHSPEEDID
			FROM CERTIFICATE CE
			INNER JOIN HIGHSPEEDHDR H  ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
			INNER JOIN SPEEDTESTDETAIL S ON H.HIGHSPEEDID = S.HIGHSPEEDID
			WHERE H.CERTIFICATIONTYPEID = 1 
			 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
			 AND LOWER(CE.MOSTRECENTCERT) = 'y';


	EXCEPTION
		WHEN li_ParametersAreNull THEN
		
			ls_ErrorMsg:=  SQLERRM ||  '-GetEmarkTestReportInfo.There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
													  ad_operatorid    => ls_OperatorId,
													  ad_daterecorded  => SYSDATE,
													  as_processname   => ' Reports_Package.GetEmarkTestReportInfor',
													  ax_recorddata    => 'ps_sku is parameters NULL..',
													  as_messagecode   => TO_CHAR(SQLCODE),
													  as_message       => ls_ErrorMsg);
		
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
		
			ls_ErrorMsg:=  SQLERRM || '-GetEmarkTestReportInfo. There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetEmarkTestReportInfo',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			raise_application_error (-20006,ls_ErrorMsg);
		
		WHEN others THEN
			
			ls_ErrorMsg:=  SQLERRM || '-GetEmarkTestReportInfo. An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetEmarkTestReportInfo',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			raise_application_error (-20007,ls_ErrorMsg);
	
	END GetEmarkTestReportInfo;

	PROCEDURE GetNOMCertification(ps_CertificateNumber IN    VARCHAR2,
								  pi_tiretypeid        IN    NUMBER,
								  pc_CertificateDfValue  OUT RETCURSOR,
								  pc_CertificateInfo     OUT RETCURSOR,
								  pc_Product             OUT RETCURSOR,
								  pc_MeasureHDR          OUT RETCURSOR,
								  pc_measureDtl          OUT RETCURSOR,
								  pc_BEADUNSEATHDR       OUT RETCURSOR,
								  pc_BEADUNSEATDTL       OUT RETCURSOR,
								  pc_PLUNGERHDR          OUT RETCURSOR,
								  pc_PLUNGERDTL          OUT RETCURSOR,
								  pc_TREADWEARHDR        OUT RETCURSOR,
								  pc_TREADWEARDTL        OUT RETCURSOR,
								  pc_ENDURANCEHDR        OUT RETCURSOR,
								  pc_ENDURANCEDTL        OUT RETCURSOR,
								  pc_HIGHSPEEDHDR        OUT RETCURSOR,
								  pc_HIGHSPEEDDTL        OUT RETCURSOR,
								  pc_SPEEDTESTDETAIL     OUT RETCURSOR,
								  pc_Brand               OUT RETCURSOR)
	AS
    /************************************************************************************************
     Procedure Name - GetNOMCertification
     Change History
      --------------------------------------------------------------------------
      Version No  DATE          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - AS per PRJ3617
                                            - Replaced NPRID with PSN
                                            - Added Matl_Num wherever SKU is available in SELECT list of the query
    *************************************************************************************************/

		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId     VARCHAR2(50):=NULL;
		ls_OperatorId    VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg      VARCHAR2(4000):=NULL;

		-- Added AS per PRJ3617
		ls_Brand         PRODUCT.BRAND%TYPE:=NULL;
		ls_Brand_Line    PRODUCT.BRAND_LINE%TYPE:=NULL;
		
	BEGIN

		IF ps_CertificateNumber IS NULL THEN
			RAISE li_ParametersAreNull;
		END IF ;
		
		IF ps_CertificateNumber = '' THEN
			RAISE li_ParametersAreInvalid;
		END IF ;


		--Gets the default values information.
		OPEN pc_CertificateDfValue FOR
			SELECT  CERTIFICATENUMBER,
					MAX(DECODE(FIELDID,153,FIELDVALUE,NULL)) Dimension_N,
					MAX(DECODE(FIELDID,120,FIELDVALUE,NULL)) EquipoEmpleadoRinDesc1_N,
					MAX(DECODE(FIELDID,123,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC2_N,
					MAX(DECODE(FIELDID,126,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC3_N ,
					MAX(DECODE(FIELDID,129,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC4_N ,
					MAX(DECODE(FIELDID,132,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC5_N,
					MAX(DECODE(FIELDID,135,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC6_N,
					MAX(DECODE(FIELDID,138,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC7_N,
					MAX(DECODE(FIELDID,141,FIELDVALUE,NULL)) EquipoEmpleadoRinDESC8_N,
					MAX(DECODE(FIELDID,121,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca1_N,
					MAX(DECODE(FIELDID,124,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca2_N,
					MAX(DECODE(FIELDID,127,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca3_N,
					MAX(DECODE(FIELDID,130,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca4_N ,
					MAX(DECODE(FIELDID,133,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca5_N,
					MAX(DECODE(FIELDID,136,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca6_N,
					MAX(DECODE(FIELDID,139,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca7_N,
					MAX(DECODE(FIELDID,142,FIELDVALUE,NULL)) EquipoEmpleadoRinMarca8_N,
					MAX(DECODE(FIELDID,122,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo1_N,
					MAX(DECODE(FIELDID,125,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo2_N ,
					MAX(DECODE(FIELDID,128,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo3_N,
					MAX(DECODE(FIELDID,131,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo4_N ,
					MAX(DECODE(FIELDID,134,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo5_N,
					MAX(DECODE(FIELDID,137,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo6_N,
					MAX(DECODE(FIELDID,140,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo7_N,
					MAX(DECODE(FIELDID,143,FIELDVALUE,NULL)) EquipoEmpleadoRinModelo8_N,
					MAX(DECODE(FIELDID,144,FIELDVALUE,NULL)) EquipoPruebaResistencia_N,
					MAX(DECODE(FIELDID,150,FIELDVALUE,NULL)) EvaluationDate_N,
					MAX(DECODE(FIELDID,159,FIELDVALUE,NULL)) FinalPressure_N,
					MAX(DECODE(FIELDID,152,FIELDVALUE,NULL)) Height_N,
					MAX(DECODE(FIELDID,146,FIELDVALUE,NULL)) IdentificationKey_N,
					MAX(DECODE(FIELDID,158,FIELDVALUE,NULL)) LoadBehavior_N,
					MAX(DECODE(FIELDID,147,FIELDVALUE,NULL)) Loadcapacity_N,
					MAX(DECODE(FIELDID,155,FIELDVALUE,NULL)) MeasurementFactor_N,
					MAX(DECODE(FIELDID,148,FIELDVALUE,NULL)) Model_N,
					MAX(DECODE(FIELDID,157,FIELDVALUE,NULL)) PenetrationResistence_N,
					MAX(DECODE(FIELDID,156,FIELDVALUE,NULL)) RimResistence_N,
					MAX(DECODE(FIELDID,160,FIELDVALUE,NULL)) RoomTemp_N,
					MAX(DECODE(FIELDID,119,FIELDVALUE,NULL)) SinalpAddress_N,
					MAX(DECODE(FIELDID,118,FIELDVALUE,NULL)) SinalpCentroEvaluacion_N,
					MAX(DECODE(FIELDID,114,FIELDVALUE,NULL)) SinalpDomicilio_N,
					MAX(DECODE(FIELDID,113,FIELDVALUE,NULL)) SinalpEmpresa_N,
					MAX(DECODE(FIELDID,116,FIELDVALUE,NULL)) SinalpHulera_N,
					MAX(DECODE(FIELDID,117,FIELDVALUE,NULL)) SinalpManufacturerName_N,
					MAX(DECODE(FIELDID,115,FIELDVALUE,NULL)) SinalpRepresentante_N,
					MAX(DECODE(FIELDID,161,FIELDVALUE,NULL)) SpeedBehavior_N,
					MAX(DECODE(FIELDID,162,FIELDVALUE,NULL)) TestInfo_N,
					MAX(DECODE(FIELDID,164,FIELDVALUE,NULL)) TestReport_N,
					MAX(DECODE(FIELDID,163,FIELDVALUE,NULL)) TestSerie_N,
					MAX(DECODE(FIELDID,145,FIELDVALUE,NULL)) TireIdentification_N,
					MAX(DECODE(FIELDID,149,FIELDVALUE,NULL)) Type_N,
					MAX(DECODE(FIELDID,151,FIELDVALUE,NULL)) WearingDownIndicator_N,
					MAX(DECODE(FIELDID,187,FIELDVALUE,NULL)) SIGNATURENAME_N,
					MAX(DECODE(FIELDID,188,FIELDVALUE,NULL)) SIGNATURENTITLE_N,
					MAX(DECODE(FIELDID,190,FIELDVALUE,NULL)) NominalBeadUnseat_N,
					MAX(DECODE(FIELDID,191,FIELDVALUE,NULL)) NominalPlunger_N,
					MAX(DECODE(FIELDID,192,FIELDVALUE,NULL)) LowPressEndurInitInfl_N
			FROM (SELECT FIELDID,
			             CERTIFICATENUMBER,
			             FIELDVALUE
			      FROM DEFAULTVALUES_VIEW
			      WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber)  
			AND CERTIFICATIONTYPEID = 3)   
			GROUP BY CERTIFICATENUMBER ;

			OPEN pc_CertificateInfo FOR
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
			 AND LOWER(ce.certificatenumber) = LOWER(ps_certificateNumber) 
			 AND LOWER(CE.MOSTRECENTCERT) = 'y'  ;
				 
		OPEN pc_Product FOR
				SELECT DISTINCT P.SKUID,  
				                SKU,LPAD(MATL_NUM,18,0) AS MATL_NUM, 
								BRAND,BRAND_LINE, 
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
				WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber);

		BEGIN
			-- Added  ls_Brand,ls_Brand_Line instead of ls_brandcode
			SELECT DISTINCT BRAND, BRAND_LINE
			           INTO ls_Brand,ls_Brand_Line
			FROM PRODUCT P
			INNER JOIN PRODUCTCERTIFICATE PCE ON P.SKUID = PCE.SKUID
			INNER JOIN CERTIFICATE CE ON PCE.CERTIFICATEID = CE.CERTIFICATEID
			AND PCE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber)  
			  AND LOWER(CE.MOSTRECENTCERT)='y';
		EXCEPTION
			WHEN OTHERS THEN
			ls_Brand := NULL;
			ls_Brand_Line := NULL;
		END;

		OPEN pc_brand FOR
			SELECT DISTINCT p.Brand,
							p.Brand_Line,
							Size_Stamp||' '||DECODE(NVL(Load_Range,' '),'XL','XL', 'RE','RE',' ') Size_Stamp,
							CASE WHEN(pi_tiretypeid= 1)
							THEN 1
							WHEN(pi_tiretypeid= 3)
							THEN
							( CASE WHEN (Sload_Idx>112)
							THEN 2
							ELSE 1
							END)
							ELSE 1
							END Sload_Idx
			FROM (SELECT LPAD(Matl_Num,18,0) AS Matl_Num,
			             MAX(DECODE(Attrib_Name,'LOAD_RANGE',Attrib_Value))  AS LOAD_RANGE,
						 MAX(DECODE(Attrib_Name,'STAMPED_SINGLE_LOAD_INDEX',Attrib_Value))  AS Sload_Idx,
						 MAX(DECODE(Attrib_Name,'TIRE_SIZE',Attrib_Value))                  AS Size_Stamp
				  FROM (SELECT MA.*,DENSE_RANK() OVER(PARTITION BY LPAD(MA.Matl_Num,18,0), MA.Attrib_Name ORDER BY MA.Counter DESC) AS RK
						  FROM MATERIAL_ATTRIBUTE MA
						  WHERE Attrib_Name IN ('STAMPED_SINGLE_LOAD_INDEX' ,'TIRE_SIZE' ,'LOAD_RANGE'))
				  WHERE RK = 1
				  GROUP BY Matl_Num) MA,
			     PRODUCT P
			WHERE P.MATL_NUM = LPAD(MA.MATL_NUM,18,0)
			  AND P.BRAND = ls_brand
			  AND P.BRAND_LINE = ls_brand_line
			  AND NVL (P.DISCONTINUEDDATE, SYSDATE) > '01-NOV-1991';

            OPEN pc_MeasureHDR FOR
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
						SKU,
						LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
				  FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
				  WHERE CE.CERTIFICATIONTYPEID = 3 
				    AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
				    AND UPPER(CE.MOSTRECENTCERT) = 'Y';

			OPEN pc_measureDtl FOR
				SELECT MD.MEASUREID,
				       SECTIONWIDTH,
				       OVERALLWIDTH,
				       ITERATION
				FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID 
				AND LOWER(CE.MOSTRECENTCERT) = 'y'
				INNER JOIN MEASUREDTL MD ON M.MEASUREID = MD.MEASUREID
				WHERE M.CERTIFICATIONTYPEID = 3 AND
				LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber);

			OPEN pc_BEADUNSEATHDR FOR
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
				       SKU,
				       LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
				FROM CERTIFICATE CE INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID 
				 AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
				WHERE B.CERTIFICATIONTYPEID = 3 
				 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
				 AND LOWER(CE.MOSTRECENTCERT) = 'y';

			OPEN pc_beadunseatdtl FOR
				SELECT BD.BEADUNSEATID,
					   UNSEATFORCE,
					   ITERATION
				FROM CERTIFICATE CE
				INNER JOIN BEADUNSEATHDR B ON CE.CERTIFICATEID = B.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = B.CERTIFICATIONTYPEID
				INNER JOIN BEADUNSEATDTL BD ON B.BEADUNSEATID = BD.BEADUNSEATID
				WHERE B.CERTIFICATIONTYPEID = 3 
				  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
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
				       SKU,
				       LPAD(MATL_NUM,18,0) AS MATL_NUM -- Added AS per PRJ3617
				FROM CERTIFICATE CE
				INNER JOIN  PLUNGERHDR P ON CE.CERTIFICATEID = P.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
				WHERE P.CERTIFICATIONTYPEID = 3 AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
				  AND LOWER(CE.MOSTRECENTCERT) = 'y';

			OPEN pc_PLUNGERDTL FOR
				SELECT pd.PLUNGERID,
						BREAKINGENERGY,
						ITERATION
				FROM CERTIFICATE CE
				INNER JOIN PLUNGERHDR PH ON CE.CERTIFICATEID = PH.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PH.CERTIFICATIONTYPEID
			    INNER JOIN PLUNGERDTL PD ON PH.PLUNGERID = PD.PLUNGERID
				WHERE PH.CERTIFICATIONTYPEID = 3 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
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
				       T.CERTIFICATIONTYPEID,
				       CERTIFICATENUMBER,
				       INDICATORSREQUIREMENT,
				       SKU,
				       LPAD(MATL_NUM,18,0) AS MATL_NUM -- Added AS per PRJ3617
				FROM CERTIFICATE CE
				INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
				WHERE T.CERTIFICATIONTYPEID = 3 
				  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
				  AND LOWER(CE.MOSTRECENTCERT) = 'y';

				OPEN pc_TREADWEARDTL FOR
					SELECT  TD.TREADWEARID,
							WEARBARHEIGHT,
							ITERATION
					FROM CERTIFICATE CE
					INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID  = T.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
					INNER JOIN TREADWEARDTL TD ON T.TREADWEARID = TD.TREADWEARID
					WHERE T.CERTIFICATIONTYPEID = 3 
					  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
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
					       LPAD(MATL_NUM,18,0) AS MATL_NUM, -- ADDED AS PER PRJ3617
					       LOWPRESSURESTARTINFLATION,
					       LOWPRESSUREENDINFLATION,
					       LOWPRESSUREENDTEMP
					FROM CERTIFICATE CE INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
					INNER JOIN (SELECT ENDURANCEID, MAX(SPEED) SPEED FROM ENDURANCEDTL ED WHERE ED.TESTSTEP <= 1 GROUP BY ENDURANCEID) EDS
					ON EDS.ENDURANCEID = E.ENDURANCEID
					WHERE E.CERTIFICATIONTYPEID = 3 
					  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
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
						       ed.ENDURANCEID
						FROM CERTIFICATE CE
								  INNER JOIN ENDURANCEHDR E ON
										 CE.CERTIFICATEID = E.CERTIFICATEID AND
										 CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
								  INNER JOIN ENDURANCEDTL ED ON
										 E.ENDURANCEID = ED.ENDURANCEID
						WHERE E.CERTIFICATIONTYPEID = 3 AND
							  LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
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
						        HDM.LOAD MAXLOAD,
						       SKU,
						       LPAD(MATL_NUM,18,0) AS MATL_NUM -- Added AS per PRJ3617
						FROM CERTIFICATE CE INNER JOIN HIGHSPEEDHDR H ON
								   CE.CERTIFICATEID = H.CERTIFICATEID AND
								   CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
							   INNER JOIN (SELECT HD.HIGHSPEEDID, MAX(LOAD) LOAD  FROM HIGHSPEEDDTL HD
									WHERE HD.TESTSTEP <= 1 ---Mario wanted this added so it just pulls the load FROM the first step, so that he only
																			---has to change one of them, AND they are really all the same. - had to do it with
																			---the  <= so that IF it doesn't have a step one, it won't error out.
																			 ---jes 5/10/11
									GROUP BY HD.HIGHSPEEDID) HDM ON
									H.HIGHSPEEDID = HDM.HIGHSPEEDID
						WHERE H.CERTIFICATIONTYPEID = 3 AND
							  LOWER(CE.CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)
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
						     HD.HIGHSPEEDID
					  FROM CERTIFICATE CE
								  INNER JOIN HIGHSPEEDHDR H ON
										 CE.CERTIFICATEID = H.CERTIFICATEID AND
										 CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
								  INNER JOIN HIGHSPEEDDTL HD ON
										 H.HIGHSPEEDID = HD.HIGHSPEEDID
						WHERE H.CERTIFICATIONTYPEID = 3 AND
							  LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
							  AND LOWER(CE.MOSTRECENTCERT) = 'y';

                  OPEN pc_speedtestdetail FOR
					  SELECT ITERATION,
						     TIME,
							 SPEED,
							 S.HIGHSPEEDID
					  FROM CERTIFICATE CE
								  INNER JOIN HIGHSPEEDHDR H  ON
										 CE.CERTIFICATEID = H.CERTIFICATEID AND
										 CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
								  INNER JOIN SPEEDTESTDETAIL S ON
										 H.HIGHSPEEDID = S.HIGHSPEEDID
					  WHERE H.CERTIFICATIONTYPEID = 3 
					    AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber)
					   AND LOWER(CE.MOSTRECENTCERT) = 'y';


	EXCEPTION
		WHEN li_ParametersAreNull THEN
		
			ls_ErrorMsg:=  SQLERRM ||  '-GetNOMCertification.There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetNOMCertification',
			                                          ax_recorddata    => 'ps_sku is parameters NULL..',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
			
			ls_ErrorMsg:=  SQLERRM || '-GetNOMCertification. There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetNOMCertification',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);
	
		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  SQLERRM || '-GetNOMCertification. An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.GetNOMCertification',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
		
	END GetNOMCertification;

	PROCEDURE GetEmarkApplicationInfo(ps_CertificateNumber  IN    VARCHAR2,
									  pi_tiretypeid         IN    NUMBER,
									  pc_CertificateDfValue   OUT RETCURSOR,
									  pc_CertificateInfo      OUT RETCURSOR,
									  pc_Product              OUT RETCURSOR,
									  pc_MeasureHDR           OUT RETCURSOR,
									  pc_HIGHSPEEDHDR         OUT RETCURSOR,
									  pc_Brand                OUT RETCURSOR) 
    AS
    
	/************************************************************************************************
     Procedure Name - GetEmarkApplicationInfo
     Change History
      --------------------------------------------------------------------------
      Version No  DATE          Author    Description
      ---------------------------------------------------------------------------
          1.0                             Intial Version
          1.1     9/17/2012     Krishna   - AS per PRJ3617
                                            - Replaced NPRID with PSN
                                            - Added Matl_Num wherever SKU is available in SELECT list of the query
                                            - Added Query instead of Brand_View
                                            - Added Brand, Brand_Line columns instead of Brandcode
          1.2    11/20/2013     Guru      - 1.Remove UPPER(CE.MOSTRECENTCERT) = 'Y' in the first SELECT clause.
                                            2.WHEN finding the max measureid's AND Highspeedid set that Id=0 IF no data is found
    *************************************************************************************************/

		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId         VARCHAR2(50):=NULL;
		ls_OperatorId        VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg          VARCHAR2(4000);
		ln_CertficateCount   NUMBER:=NULL;
		ln_maxid             NUMBER:=NULL;
		
	BEGIN

		IF ps_CertificateNumber IS NULL THEN
			RAISE li_ParametersAreNull;
		END IF ;
		
		IF ps_CertificateNumber = '' THEN
			RAISE li_ParametersAreInvalid;
		END IF ;

		SELECT COUNT(*) INTO ln_CertficateCount 
		FROM CERTIFICATE CE
		INNER JOIN PRODUCTCERTIFICATE PC ON CE.CERTIFICATEID = PC.CERTIFICATEID
		INNER JOIN PRODUCT P ON PC.SKUID = P.SKUID
		WHERE CE.CERTIFICATIONTYPEID = 1 
		  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
		  AND P.TIRETYPEID = pi_tiretypeid;

		IF     ln_CertficateCount  = 0 THEN
			RAISE li_ParametersAreInvalid;
		ELSE

			OPEN pc_CertificateInfo FOR
				SELECT CE.CERTIFICATEID,
					   CE.CERTIFICATIONTYPEID,
					   CERTIFICATIONTYPENAME,
					   CERTIFICATENUMBER,
					   ACTIVESTATUS,
					   CERTDATESUBMITTED,
					   CERTDATEAPPROVED,
					   RENEWALREQUIRED_CGIN,
					   JOBREPORTNUMBER_CEN,
					   EXTENSION_EN,
					   SUPPLEMENTALMOLDSTAMPING_E
				FROM CERTIFICATE CE
				INNER JOIN CERTIFICATIONTYPE CT ON CT.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATIONTYPEID = 1 
				  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificateNumber ) 
				  AND LOWER(CE.MOSTRECENTCERT) = 'y'  ;

			-- Gets the brand information
			-- Replaced query instead of Brand_View
			OPEN pc_brand FOR
			SELECT DISTINCT BRAND,
							BRAND_LINE,
							CERTIFICATENUMBER---but just get each brand once, even though it is multiple extensions.
			FROM  (SELECT DISTINCT P.BRAND ,
									P.BRAND_LINE,
									C.CERTIFICATENUMBER,
									C.EXTENSION_EN
				   FROM PRODUCT P,
						 PRODUCTCERTIFICATE PC,
						 CERTIFICATE C
				   WHERE C.CERTIFICATEID = PC.CERTIFICATEID
					  AND PC.SKUID = P.SKUID)
			WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) ;

			--Gets the default values information.
			OPEN pc_CertificateDfValue FOR
				SELECT CERTIFICATENUMBER,
						MAX(DECODE(FIELDID,1,FIELDVALUE,NULL))   AS MANUFACTURERNAME_E,
						MAX(DECODE(FIELDID,2,FIELDVALUE,NULL))   AS MANUFACTURERNAMEADDRESS_E,
						MAX(DECODE(FIELDID,3,FIELDVALUE,NULL))   AS TECHNICALSERVICE_E,
						MAX(DECODE(FIELDID,4,FIELDVALUE,NULL))   AS PLACE_E,
						MAX(DECODE(FIELDID,5,FIELDVALUE,NULL))   AS MEASURERIM_E,
						MAX(DECODE(FIELDID,6,FIELDVALUE,NULL))   AS INFLATIONPRESSURE_E,
						MAX(DECODE(FIELDID,7,FIELDVALUE,NULL))   AS TESTLABORATORY_E,
						MAX(DECODE(FIELDID,8,FIELDVALUE,NULL))   AS REPRESENTATIVENAME_E,
						MAX(DECODE(FIELDID,9,FIELDVALUE,NULL))   AS REPRESENTATIVEADDRESS_E,
						MAX(DECODE(FIELDID,10,FIELDVALUE,NULL))  AS REASONOFEXTENSION_E,
						MAX(DECODE(FIELDID,11,FIELDVALUE,NULL))  AS REMARKS_E,
						MAX(DECODE(FIELDID,171,FIELDVALUE,NULL)) AS TIRESIZEDESIGNATIONS_E,
						MAX(DECODE(FIELDID,167,FIELDVALUE,NULL)) AS REFERENCESPEED_E,
						MAX(DECODE(FIELDID,169,FIELDVALUE,NULL)) AS PERFORMANCECHARACTERISTICS_E,
						MAX(DECODE(FIELDID,170,FIELDVALUE,NULL)) AS PLANTSADDRESSES_E,
						MAX(DECODE(FIELDID,175,FIELDVALUE,NULL)) AS PPNPROFILEFAMILY_E,
						MAX(DECODE(FIELDID,176,FIELDVALUE,NULL)) AS RIMSMOUNTED_E,
						MAX(DECODE(FIELDID,177,FIELDVALUE,NULL)) AS OVERALLDIMENSIONSTYPE_E,
						MAX(DECODE(FIELDID,178,FIELDVALUE,NULL)) AS REFERENCETIRE_E,
						MAX(DECODE(FIELDID,179,FIELDVALUE,NULL)) AS NOMINALRIMSIZE_E,
						MAX(DECODE(FIELDID,180,FIELDVALUE,NULL)) AS NOMINALLOAD_E,
						MAX(DECODE(FIELDID,181,FIELDVALUE,NULL)) AS ADDITIONALNOMSPEED_E,
						MAX(DECODE(FIELDID,182,FIELDVALUE,NULL)) AS ADDITIONALLOADINDEX_E,
						MAX(DECODE(FIELDID,183,FIELDVALUE,NULL)) AS APPLICATIONCOMMENT_E
				FROM (SELECT FIELDID,
							 CERTIFICATIONTYPEID,
							 CERTIFICATENUMBER,
							 FIELDVALUE
					  FROM DEFAULTVALUES_VIEW
					  WHERE LOWER(CERTIFICATENUMBER) = LOWER(ps_certificateNumber) 
					  AND CERTIFICATIONTYPEID = 1)
				GROUP BY CERTIFICATENUMBER ;

			OPEN pc_Product FOR
				SELECT ROWNUM, 
					   SKUID,  
					   SKU, 
					   MATL_NUM,BRAND,
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
					   CERTIFICATENUMBER 
					   FROM 
					   (SELECT DISTINCT SKUID,
										SKU,
										LPAD(MATL_NUM,18,0) AS MATL_NUM, 
										BRAND,BRAND_lINE, 
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
										CERTIFICATENUMBER
						FROM  PRODUCTDATA_REPORT_VIEW
						WHERE LOWER(CERTIFICATENUMBER) = LOWER(PS_CERTIFICATENUMBER)  
						  AND TIRETYPEID = pi_tiretypeid
						 ORDER BY LPAD(MATL_NUM,18,0));

			BEGIN
				SELECT MAX(m.MEASUREID) INTO ln_maxid
				FROM  MEASUREHDR M, 
					  CERTIFICATE C
				WHERE C.CERTIFICATENUMBER = ps_certificatenumber 
				  AND M.CERTIFICATEID = C.CERTIFICATEID;
			EXCEPTION
				WHEN NO_DATA_FOUND THEN
				ln_maxid := 0;
			END;
			
			OPEN pc_MeasureHDR FOR
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
						SKU,
						LPAD(MATL_NUM,18,0) AS MATL_NUM -- Added AS per PRJ3617
				FROM CERTIFICATE CE INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
				WHERE CE.CERTIFICATIONTYPEID = 1 
				  AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
				  AND m.MEASUREID = ln_maxid;

			BEGIN
				SELECT MAX(h.HIGHSPEEDID) INTO ln_maxid
				FROM HIGHSPEEDHDR H, 
					 CERTIFICATE C
				WHERE C.CERTIFICATENUMBER = ps_certificatenumber 
				AND H.CERTIFICATEID = C.CERTIFICATEID;
			EXCEPTION
				WHEN NO_DATA_FOUND THEN
					ln_maxid := 0;
			END;
			
			OPEN pc_HIGHSPEEDHDR FOR
				SELECT HIGHSPEEDID,
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
				       MAXLOAD,
				       SKU,
				       LPAD(MATL_NUM,18,0) AS MATL_NUM-- Added AS per PRJ3617
				FROM CERTIFICATE CE INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
				WHERE H.CERTIFICATIONTYPEID = 1 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_CertificateNumber)
				 AND H.HIGHSPEEDID = ln_maxid;
		END IF;

	EXCEPTION
		WHEN li_ParametersAreNull THEN
			
			ls_ErrorMsg:=  SQLERRM ||  '-GetEmarkApplicationInfo.There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetEmarkApplicationInfo',
			                                          ax_recorddata    => 'ps_sku is parameters NULL..',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
			
			ls_ErrorMsg:=  SQLERRM || 'GetEmarkApplicationInfo. There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetEmarkApplicationInfor',
			                                          ax_recorddata    => 'There is at least one parameters invalid*'||ps_CertificateNumber||'*'|| pi_tiretypeid ,
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);
	
		WHEN OTHERS THEN
			
			ls_ErrorMsg:=  SQLERRM || 'GetEmarkApplicationInfo. An error have ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_PackageGetEmarkApplicationInfo',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);
	
	END GetEmarkApplicationInfo;

	PROCEDURE GetGSOConformityReport(pc_Certificate           OUT RETCURSOR,
									 pc_Brand                 OUT RETCURSOR,
									 pc_SkuList               OUT RETCURSOR,
									 ps_BatchNumber         IN    VARCHAR2,
									 pi_certificationTypeID IN    NUMBER,
									 ps_Operatorid          IN    VARCHAR2,
									 pi_TireTypeId          IN    NUMBER)
    AS
		--EXCEPTION variables
		li_ParametersAreNull EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreNull,-20005);
		li_ParametersAreInvalid EXCEPTION;
		-- link the EXCEPTION to the error number
		PRAGMA exception_init( li_ParametersAreInvalid,-20006);

		ls_MachineId    VARCHAR2(50):=NULL;
		ls_OperatorId   VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg     VARCHAR2(4000):=NULL;
		
	BEGIN
		
		IF ps_BatchNumber IS NULL OR
			pi_certificationTypeID IS NULL THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_BatchNumber ='' OR
			pi_certificationTypeID  <= 0 THEN
			RAISE li_ParametersAreNull ;
		END IF;

		IF ps_Operatorid IS NOT NULL OR
			ps_Operatorid <> '' THEN
			ls_OperatorId:=ps_Operatorid;
		END IF;


		---THIS CURSOR DOES NOT APPEAR TO BE USED -- JESEITZ 3/15/2013
		OPEN pc_Certificate FOR
			SELECT DISTINCT(CE.CERTIFICATENUMBER),
			                CE.EXTENSION_EN
			FROM CERTIFICATE CE
			INNER JOIN PRODUCTCERTIFICATE PCE ON CE.CERTIFICATEID = PCE.CERTIFICATEID AND CE.CERTIFICATIONTYPEID = PCE.CERTIFICATIONTYPEID
			INNER JOIN PRODUCT P ON PCE.SKUID = P.SKUID
			WHERE LOWER(CE.BATCHNUMBER_G) = LOWER(ps_BatchNumber) 
			  AND CE.CERTIFICATIONTYPEID = pi_certificationTypeID
			  AND CE.ARCHIVEDATE_CEGI IS NULL  ; -- added archive DATE check jeseitz 8/20/2012

		---THIS CURSOR DOES NOT APPEAR TO BE USED -- JESEITZ 3/15/2013
		-- Gets the brand information
		OPEN pc_brand FOR
			SELECT DISTINCT (P.BRANDCODE) AS BRANDCODE,
							P.BRANDDESC   AS BRANDNAME,
							CE.CERTIFICATENUMBER,
							CE.EXTENSION_EN
			FROM PRODUCT P
			INNER JOIN PRODUCTCERTIFICATE PE ON P.SKUID = PE.SKUID
			INNER JOIN CERTIFICATE CE ON PE.CERTIFICATEID = CE.CERTIFICATEID AND PE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE LOWER(CE.BATCHNUMBER_G) = LOWER(ps_BatchNumber)
			  AND CE.ARCHIVEDATE_CEGI IS NULL  ; -- added archive DATE check jeseitz 8/20/2012


		OPEN pc_SkuList FOR
			SELECT  P.BRANDCODE BRANDCODE,
			        P.BRAND||' '||P.BRAND_LINE BRANDNAME, ----JESEITZ 3/15/2013
			        CERTIFICATENUMBER,
			        EXTENSION_EN,
			        P.SKUID,
			        SKU,
			        P.SIZESTAMP||
			        CASE WHEN (P.TIRETYPEID = 1 AND UPPER(P.EXTRALOADYN) = 'Y')  THEN ' XL'
			        WHEN (P.TIRETYPEID = 1 AND UPPER(P.REINFORCEDYN) = 'Y') THEN ' RE'
			        ELSE ' '
			        END SIZESTAMP,
			        SINGLOADINDEX,
			        DUALLOADINDEX,
			        SPEEDRATING
			FROM PRODUCT P
			INNER JOIN PRODUCTCERTIFICATE PE ON P.SKUID = PE.SKUID
			INNER JOIN CERTIFICATE CE ON PE.CERTIFICATEID = CE.CERTIFICATEID AND PE.CERTIFICATIONTYPEID = CE.CERTIFICATIONTYPEID
			WHERE LOWER(BATCHNUMBER_G) = LOWER(ps_batchnumber)
			AND CE.ARCHIVEDATE_CEGI IS NULL  ; -- added archive DATE check jeseitz 8/20/2012;

	EXCEPTION
		WHEN li_ParametersAreNull THEN
		
			ls_ErrorMsg:=  ' There is at least one parameters NULL.'  ;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetGSOConformityReport',
			                                          ax_recorddata    => 'ps_sku is parameters NULL..',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20005,ls_ErrorMsg);

		WHEN li_ParametersAreInvalid THEN
		
			ls_ErrorMsg:=  SQLERRM || ' There is at least one parameters invalid.';
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   => ' Reports_Package.GetGSOConformityReport',
			                                          ax_recorddata    => 'There is at least one parameters invalid.',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       => ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20006,ls_ErrorMsg);

		WHEN OTHERS THEN
		
			ls_ErrorMsg:=  'An error has ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId    => ls_MachineId,
			                                         ad_operatorid    => ls_OperatorId,
			                                         ad_daterecorded  => SYSDATE,
			                                         as_processname   =>' Reports_Package.GetGSOConformityReport',
			                                         ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                         as_messagecode   => TO_CHAR(SQLCODE),
			                                         as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END GetGSOConformityReport;

	PROCEDURE getEceSimilarCertificates(ps_matl_num            IN    VARCHAR2,
	                                    pc_SimilarCertificates   OUT RETCURSOR)

	IS
	/************************************************************************************************
	Procedure Name - getEceSimilarCertificates
	Change History
	--------------------------------------------------------------------------
	Version No  DATE          Author    Description
	---------------------------------------------------------------------------
	1.0                             Intial Version
	1.1     9/17/2012     Krishna   - AS per PRJ3617 Added
	- Replaced ps_in_sku with ps_Matl_Num
	- Matl_Num wherever SKU is available in SELECT list of the query
	- Replaced BrandCode with Brand AND Brand_Line AND removed
	BrandDesc
	1.2    11/20/2013     Guru    - Remove UPPER(C2.MOSTRECENTCERT) = 'Y') in pc_SimilarTires cursor
	*************************************************************************************************/

		ln_RecCount        NUMBER(1);
		ls_MudSnowYN       PRODUCT.MUDSNOWYN%TYPE:=NULL;
		ls_SpeedRating     PRODUCT.SPEEDRATING%TYPE:=NULL;
		ls_SingLoadIndex   PRODUCT.SINGLOADINDEX%TYPE:=NULL;
		ls_SizeStamp       PRODUCT.SIZESTAMP%TYPE:=NULL;
		sku_not_found      VARCHAR2(1):=NULL;

		ls_MachineId      VARCHAR2(50):=NULL;
		ls_OperatorId     VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg       VARCHAR2(4000):=NULL;

	BEGIN

		sku_not_found := ' ';
	
		BEGIN

			-- Added below logic by removing Sku_Master_MV AS per PRJ3617
			SELECT DECODE(Mud_Snow,'M+S', 'Y', 'N') AS Mud_Snow,
				   SPEED_RATING,SLOAD_IDX,
				   TRIM(UPPER(SIZE_STAMP)) AS SIZE_STAMP
			  INTO LS_MUDSNOWYN,
				   LS_SPEEDRATING,
				   LS_SINGLOADINDEX,
				   LS_SIZESTAMP
			FROM (SELECT  LPAD(MATL_NUM,18,0) AS MATL_NUM,
			              MAX(DECODE(ATTRIB_NAME,'MUD_SNOW_STAMPING',ATTRIB_VALUE))          AS Mud_Snow,
						  MAX(DECODE(ATTRIB_NAME,'SPEED_RATING',ATTRIB_VALUE))               AS Speed_Rating,
						  MAX(DECODE(ATTRIB_NAME,'STAMPED_SINGLE_LOAD_INDEX',ATTRIB_VALUE))  AS Sload_Idx,
						  MAX(DECODE(ATTRIB_NAME,'TIRE_SIZE',ATTRIB_VALUE))                  AS Size_Stamp
				   FROM (SELECT MA.*,DENSE_RANK() OVER(PARTITION BY LPAD(MA.MATL_NUM,18,0), MA.ATTRIB_NAME ORDER BY MA.COUNTER DESC) AS RK
						 FROM MATERIAL_ATTRIBUTE MA
						 WHERE MA.ATTRIB_NAME IN ('MUD_SNOW_STAMPING' ,'SPEED_RATING' ,'STAMPED_SINGLE_LOAD_INDEX','TIRE_SIZE')
						   AND MATL_NUM = LPAD(ps_matl_num,18,0) )
					WHERE rk = 1
					GROUP BY Matl_Num);

		EXCEPTION
			WHEN NO_DATA_FOUND THEN
				sku_not_found := 'Y';
		END;

	IF sku_not_found = 'Y' THEN
		
		OPEN pc_SimilarCertificates FOR
			SELECT -9999 CERTIFICATEID, 
			      ' ' CERTIFICATENUMBER, 
				  ' ' ON_CERTIFICATE 
			FROM DUAL;
	ELSE
	
		OPEN pc_SimilarCertificates FOR
			SELECT DISTINCT C.CERTIFICATEID, 
			                C.CERTIFICATENUMBER,
							P.SKU,
							LPAD(P.MATL_NUM,18,0) AS MATL_NUM,
							P.BRAND,
							P.BRAND_LINE,
							PC.DATEASSIGNED_EGI DATE_ON, 
							PC.DATEREMOVED DATE_OFF,
							P.DISCONTINUEDDATE,
							(CASE
							WHEN UPPER(P.MATL_NUM) = UPPER(LPAD(ps_matl_num,18,0)) THEN '*' -- Changed SKU to MATL_NUM AS per PRJ3617
							ELSE ' '
							END) ON_CERTIFICATE
			FROM PRODUCT P, 
			     PRODUCTCERTIFICATE PC,
			     CERTIFICATE C
			WHERE P.SKUID = PC.SKUID
			  AND PC.CERTIFICATEID = C.CERTIFICATEID
			  AND C.CERTIFICATIONTYPEID = 1
			  AND P.MUDSNOWYN = ls_mudsnowyn
			  AND P.SINGLOADINDEX = ls_singloadindex
			  AND P.SPEEDRATING = ls_SpeedRating
			  AND TRIM(UPPER(P.SIZESTAMP)) = ls_SizeStamp
			ORDER BY CERTIFICATENUMBER, LPAD(P.MATL_NUM,18,0);

	END IF;

	EXCEPTION
		WHEN OTHERS THEN
	
			ls_ErrorMsg:=  'An error has ocurred.(WHEN others)' || SQLERRM;
			
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_MachineId     => ls_MachineId,
			                                          ad_operatorid    => ls_OperatorId,
			                                          ad_daterecorded  => SYSDATE,
			                                          as_processname   =>' Reports_Package.get_ece_similar_certificates',
			                                          ax_recorddata    => 'An error have ocurred.(WHEN others)',
			                                          as_messagecode   => TO_CHAR(SQLCODE),
			                                          as_message       =>ls_ErrorMsg);
			
			RAISE_APPLICATION_ERROR (-20007,ls_ErrorMsg);

	END getEceSimilarCertificates;

	PROCEDURE SETIMARKFLAG (pi_certificateid in number)
	AS
	BEGIN
	
		UPDATE PRODUCTCERTIFICATE
		SET IMARKCHANGE = 'I'
		WHERE CERTIFICATEID = pi_certificateid
		 AND ((DATESUBMITTED IS NULL 
		 AND DATEASSIGNED_EGI IS NOT NULL) 
		  OR DATEASSIGNED_EGI >  DATESUBMITTED 
		  OR DATEREMOVED > DATESUBMITTED  );
	END SETIMARKFLAG;

	PROCEDURE GetWeeklyReportInfo 
	AS
	/************************************************************************************************
	Procedure Name - GetWeeklyReportInfo
	Change History
	--------------------------------------------------------------------------
	Version No  DATE          Author    Description
	---------------------------------------------------------------------------
	1.0                             Intial Version
	*************************************************************************************************/

		ls_MachineId     VARCHAR2(50):=NULL;
		ls_OperatorId    VARCHAR2(50):='ICSDEV';
		ls_ErrorMsg      VARCHAR2(4000);
		
		--Email variables
		ln_recs                            NUMBER:=NULL;
		ls_From                            VARCHAR2(80) := 'SCMallampalli@coopertire.com';
		ls_Recipient                       VARCHAR2(80) := 'SCMallampalli@coopertire.com';
		ls_RecipientCC                     VARCHAR2(80) := 'jrnelatu@coopertire.com';
		ls_Bcc                             VARCHAR2(32767);
		ls_Subject                         VARCHAR2(80) := 'ICS - Weekly Report';
		ls_Mail_Host                       VARCHAR2(30) := 'SMTP';
		ls_Body                            VARCHAR2(32767) := '';
		ls_Message                         VARCHAR2(32767):=NULL;
		ls_Note                            VARCHAR2(100) := ' ';
		ls_header                          VARCHAR2(32767):=NULL;
		ls_header2                         VARCHAR2(32767):=NULL;
		ls_MimeType                        VARCHAR2(200):=NULL;
		v_Mail_Conn                        utl_smtp.Connection;
		crlf                               VARCHAR2(2)  := chr(13)||chr(10);
		ls_db_name                         VARCHAR2(50);

		a        EXCEPTIONREPORT.SKU%TYPE:=NULL;
		b        EXCEPTIONREPORT.MATL_NUM%TYPE:=NULL;
		c        EXCEPTIONREPORT.PRODUCTDATAFIELDNAME%TYPE:=NULL;
		d        EXCEPTIONREPORT.LASTMODIFIED%TYPE:=NULL;
		e        EXCEPTIONREPORT.ICSVALUE%TYPE:=NULL;
		f        EXCEPTIONREPORT.SKUMASTERVALUE%TYPE:=NULL;
		g        PRODUCT.SIZESTAMP%TYPE:=NULL;
		h        PRODUCT.SINGLOADINDEX%TYPE:=NULL;

	BEGIN

		CompareSKUMainProductColumns;

		FOR c1 IN (SELECT DISTINCT ER.SKU,
						           LPAD(ER.MATL_NUM,18,0) AS MATL_NUM, -- ADDED AS PER PRJ3617
						           ER.PRODUCTDATAFIELDNAME,
						           ER.LASTMODIFIED,
						           ER.ICSVALUE,
						           ER.SKUMASTERVALUE,
						           P.SIZESTAMP AS TIRESIZE,
						           CASE WHEN P.DUALLOADINDEX IS NULL OR P.DUALLOADINDEX = 0
						           THEN P.SINGLOADINDEX||P.SPEEDRATING
						           ELSE P.SINGLOADINDEX||'/'|| P.DUALLOADINDEX|| P.SPEEDRATING END AS SERVICEDESCRIPTION
						FROM EXCEPTIONREPORT ER,
						     PRODUCT P
						WHERE ER.MATL_NUM = LPAD(P.MATL_NUM,18,0) 
						  AND ER.LASTMODIFIED >= ADD_MONTHS(SYSDATE,-10)
						ORDER BY LPAD(er.Matl_Num,18,0),er.ProductDataFieldName) loop

			ls_mimetype := 'text/html; charset=us-ascii';
			ln_recs :=0;

			ls_header2 := '<TABLE BORDER=''1'' BGCOLOR=''#EEEEEE'' style=''font-size:12px; font-family : Arial'' >';
			ls_header2 :=  ls_header2 || '<TR BGCOLOR=''BLACK''>';
			ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>Material Number</FONT></TH>';
			ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>Product Data Field</FONT></TH>';
			ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>Last Modified</FONT></TH>';
			ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>ICS Value</FONT></TH>';
			ls_header2 :=  ls_header2 ||'<TH STYLE=''TEXT-ALIGN:CENTER''><FONT COLOR=''WHITE''>Material Master Value</FONT></TH>';
			ls_header2 :=  ls_header2 ||'</TR>';


			ls_Body := ls_Body || '<TR>';
			ls_Body := ls_Body || '<TD>'||ltrim(c1.Matl_Num,'0')|| '</TD>';
			ls_Body := ls_Body || '<TD>'||c1.ProductDataFieldName|| '</TD>';
			ls_Body := ls_Body || '<TD>'||c1.LastModified|| '</TD>';
			ls_Body := ls_Body || '<TD>'||c1.IcsValue|| '</TD>';
			ls_Body := ls_Body || '<TD>'||c1.SkuMasterValue|| '</TD>';
			ls_Body := ls_Body || '</TR>' ;

			ln_recs := ln_recs + 1;

		END loop;

		IF LENGTH(LS_BODY)> 32767-65 THEN
			LS_BODY := LS_BODY||'...'||crlf;
		END IF;
		
		--check FOR  email that needs sent
		IF ln_recs > 0 THEN

			ls_message := '<div>'
						|| '<table width=''100%'' height=''182'' border=''0'' cellpadding=''3'' cellspacing=''1'' bgcolor=''#006699'' style=''font-size:12px; font-family : Arial''>'
						|| ' <tr><td bgcolor=''#FFFFFF''>' || '<br />'
						|| '<span style=''font-size:12px; font-family : Arial''>'|| 'Weekly Report: '
						|| '</span>' || '<br /><br />'|| '<table cellspacing=''0'' cellpadding=''0'' > '
						|| '<tr><td style=''text-align:left; FONT-SIZE: 12px;  font-family : Arial''>'||  ls_header2 || ls_Body ||'</TABLE></td></tr>' || '</table> ' || '<br />'
						|| '<b><span style=''font-size:12px; font-family : Arial''>' || ls_note
						|| '</span></b><br /><br />' || ' Thanks,<br />'
						|| '<b><span style=''font-size:12px; font-family : Arial''>' || 'Quality Group'
						|| '</span></b><br /><br />' || ' </td></tr></table></div>';

			
			UTL_MAIL.SEND(sender => ls_from,   
			              recipients => ls_recipient,   
						  cc =>  ls_RecipientCC,   
						  bcc => ls_bcc,
			              subject => ls_subject,   
						  message => ls_message,   
						  mime_type => ls_mimetype,   
						  priority => '3');

		END IF;

	END  GETWEEKLYREPORTINFO;

END REPORTS_PACKAGE;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.SIMILAR_TIRES 
AS
/******************************************************************************
   NAME:       similar_tires
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        3/13/2011     arsherri       1. Created this package.
   1.1        10/2/2012     Harini         1.Modified Get_Similar_Sku,Get_Ece_Similar_Sku
                                           Get_Gso_Similar_Sku,Get_Nom_Similar_Sku,Get_Imark_Similar_Sku,
                                           Get_Ccc_Similar_sku,Get_E117_Similar_Sku,Get_Imark_Family
                                           such that if sku is present ,replace with Matl_num
******************************************************************************/

	PROCEDURE GET_SIMILAR_SKU(pn_cert_type           IN     NUMBER,
							  ps_in_matl_num         IN     VARCHAR2,
							  ps_similar_matl_num      OUT  VARCHAR2,
							  ps_imark_family          OUT  NUMBER,
							  ps_ece_reference         OUT  VARCHAR2,    
							  pn_error_num             OUT  NUMBER,
							  ps_error_desc            OUT  VARCHAR2);   

	PROCEDURE GET_ECE_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2);

	PROCEDURE GET_GSO_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2);

	PROCEDURE GET_NOM_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2);

	PROCEDURE GET_IMARK_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
									ps_in_matl_num         IN    VARCHAR2,
									ps_similar_matl_num      OUT VARCHAR2,
									ps_imark_family          OUT NUMBER,    
									ps_ece_reference         OUT VARCHAR2,    
									pn_error_num             OUT NUMBER,
									ps_error_desc            OUT VARCHAR2);

	PROCEDURE GET_CCC_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2);

	PROCEDURE GET_E117_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								   ps_in_matl_num         IN    VARCHAR2,
								   ps_similar_matl_num      OUT VARCHAR2,
								   pn_error_num             OUT NUMBER,
								   ps_error_desc            OUT VARCHAR2);

	PROCEDURE GET_IMARK_FAMILY(ps_matl_num            IN    VARCHAR2,
							   pn_certificateid       IN    NUMBER,
							   pn_family_id             OUT NUMBER,
							   ps_family_code           OUT VARCHAR2,    
							   pn_error_num             OUT NUMBER,
							   ps_error_desc            OUT VARCHAR2);
     
END SIMILAR_TIRES;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.SIMILAR_TIRES 
AS
/******************************************************************************
   NAME:       similar_tires
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        3/13/2011     arsherri       1. Created this package body.
   1.1        10/2/2012     Harini         1.Modified Get_Similar_Sku,Get_Ece_Similar_Sku
                                           Get_Gso_Similar_Sku,Get_Nom_Similar_Sku,Get_Imark_Similar_Sku,
                                           Get_Ccc_Similar_sku,Get_E117_Similar_Sku,Get_Imark_Family
                                            such that if sku is present ,replace with Matl_num
   1.2        11/19/2013    Harini         Changed logic in For loop for the SP Get_Imark_Family
   1.3        04/04/2016    jeseitz         added product_imark_family table.
******************************************************************************/

	PROCEDURE GET_SIMILAR_SKU(pn_cert_type           IN     NUMBER,
							  ps_in_matl_num         IN     VARCHAR2,
							  ps_similar_matl_num      OUT  VARCHAR2,
							  ps_imark_family          OUT  NUMBER,
							  ps_ece_reference         OUT  VARCHAR2,    
							  pn_error_num             OUT  NUMBER,
							  ps_error_desc            OUT  VARCHAR2)  
	IS
	/******************************************************************************
	NAME:       Get_Similar_Sku
	PURPOSE:    Get the similar material of the given matl_num
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_Matl_Num,
                                            ps_similar_sku with ps_Similar_Matl_Num.
                                             Replaced SKU with Matl_Num in where
                                             condition.
	******************************************************************************/
		ln_tiretypeid         PRODUCT.TIRETYPEID%TYPE 				:= NULL;
		ls_similarmatl        PRODUCT.MATL_NUM%TYPE					:= NULL;  -- As per PRJ3617,replaced ls_SimilarSKU with ls_SimilarMatl
		ln_imarkfamily        PRODUCT_IMARK_FAMILY.FAMILYID%TYPE	:= NULL; -- new table 4/4/16 jeseitz
		ls_ecereference       PRODUCT.EMARKREFERENCE_I%TYPE			:= NULL;
		ln_errornum           NUMBER(2)								:= NULL;
		ls_errordesc          VARCHAR2(150)							:= NULL;    

	BEGIN

		pn_error_num := 0;
		ps_error_desc := '';

		ln_tiretypeid := BOM_ATTRIBUTES.GET_PRODUCT_TYPE(ps_in_matl_num);

		IF pn_cert_type = 1 THEN
			GET_ECE_SIMILAR_SKU(ln_tiretypeid, 
								ps_in_matl_num, 
								ls_similarmatl, 
								ln_errornum, 
								ls_errordesc);    
		ELSIF pn_cert_type = 2 THEN
			GET_GSO_SIMILAR_SKU(ln_tiretypeid, 
								ps_in_matl_num, 
								ls_similarmatl, 
								ln_errornum, 
								ls_errordesc);                
		ELSIF pn_cert_type = 3 THEN 
			GET_NOM_SIMILAR_SKU(ln_tiretypeid, 
								ps_in_matl_num, 
								ls_similarmatl, 
								ln_errornum, 
								ls_errordesc);    
		ELSIF pn_cert_type = 4 THEN
			GET_IMARK_SIMILAR_SKU(ln_tiretypeid, 
								  ps_in_matl_num, 
								  ls_similarmatl, 
								  ln_imarkfamily, 
								  ls_ecereference, 
								  ln_errornum, 
								  ls_errordesc);    
		ELSIF pn_cert_type = 5 THEN
			GET_CCC_SIMILAR_SKU(ln_tiretypeid, 
								ps_in_matl_num, 
								ls_similarmatl, 
								ln_errornum, 
								ls_errordesc);                
		ELSIF pn_cert_type = 6 THEN
			GET_E117_SIMILAR_SKU(ln_tiretypeid, 
								 ps_in_matl_num, 
								 ls_similarmatl, 
								 ln_errornum, 
								 ls_errordesc);                 
		ELSE
			ls_similarmatl := '';
		END IF;
    
		ps_similar_matl_num := ls_similarmatl;
		
		IF pn_cert_type = 4 THEN
			ps_imark_family := ln_imarkfamily;
			ps_ece_reference := ls_ecereference;
		END IF;    
    
	END GET_SIMILAR_SKU;

	PROCEDURE GET_ECE_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2)
	IS
	/******************************************************************************
	NAME:       Get_Ece_Similar_Sku
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_matl_num,
											  ps_similar_sku with ps_Similar_Matl_Num.
											  Replaced SKU with Matl_Num in where
											  condition.Query is replaced with the 
											  query in TD
	******************************************************************************/

    ln_reccount        	NUMBER(1)					:= NULL;
    ls_mudsnowyn       	PRODUCT.MUDSNOWYN%TYPE		:= NULL;
    ls_speedrating     	PRODUCT.SPEEDRATING%TYPE	:= NULL;
    ls_singloadindex	PRODUCT.SINGLOADINDEX%TYPE	:= NULL;
    ls_sizestamp       	PRODUCT.SIZESTAMP%TYPE		:= NULL;
    e_nomatlfound       EXCEPTION;   -- As per PRJ3617,Replace SKU with Matl_Num
    e_nosimmatlfound    EXCEPTION;   -- As per PRJ3617,Replace SKU with Matl_Num 

    CURSOR LCR_SKUS IS
        SELECT P.SKU,
			   LPAD(P.MATL_NUM, 18, 0) AS MATL_NUM, 
			   PC.DATEAPPROVED_CEGI  -- As per PRJ3617,Retrieving Matl_Num along with SKU
		FROM PRODUCT P, 
			 PRODUCTCERTIFICATE PC,
             CERTIFICATE C
        WHERE P.SKUID = PC.SKUID
			AND PC.CERTIFICATEID = C.CERTIFICATEID
			AND C.CERTIFICATIONTYPEID = 1
			AND P.MATL_NUM <> LPAD(ps_in_matl_num, 18, 0)    -- As per PRJ3617,Replace SKU with Matl_Num
			AND P.DISCONTINUEDDATE IS NULL
			AND P.TIRETYPEID = pn_tire_type_id
			AND P.MUDSNOWYN = ls_mudsnowyn
			AND P.SINGLOADINDEX = ls_singloadindex
			AND P.SPEEDRATING = ls_speedrating
			AND P.SIZESTAMP = ls_sizestamp
        ORDER BY PC.DATEAPPROVED_CEGI DESC;

	BEGIN
    
		pn_error_num 		:= 0;
		ps_error_desc 		:= '';
		ps_similar_matl_num := '';    --As per PRJ3617,Replace ps_Similar_SKu with ps_similar_matl_num

		BEGIN
			-- As per PRJ3617,Query was modified as per TD
			SELECT DECODE(MUD_SNOW_STAMPING, 'M+S', 'Y', 'N'),
                   SPEED_RATING,
                   STAMPED_SINGLE_LOAD_INDEX,
                   TIRE_SIZE
			INTO ls_mudsnowyn,
                 ls_speedrating, 
                 ls_singloadindex, 
                 ls_sizestamp
			FROM (
				SELECT MATL_NUM 
                      ,MAX(DECODE(ATTRIB_NAME, 'MUD_SNOW_STAMPING', ATTRIB_VALUE))          AS MUD_SNOW_STAMPING
                      ,MAX(DECODE(ATTRIB_NAME, 'SPEED_RATING', ATTRIB_VALUE))               AS SPEED_RATING
                      ,MAX(DECODE(ATTRIB_NAME, 'STAMPED_SINGLE_LOAD_INDEX', ATTRIB_VALUE))  AS STAMPED_SINGLE_LOAD_INDEX
                      ,MAX(DECODE(ATTRIB_NAME, 'TIRE_SIZE', ATTRIB_VALUE))                  AS TIRE_SIZE
				FROM (
					SELECT MA.*,
                           DENSE_RANK() OVER(PARTITION BY MA.MATL_NUM, MA.ATTRIB_NAME ORDER BY MA.COUNTER DESC) rk
                    FROM MATERIAL_ATTRIBUTE MA
					WHERE ATTRIB_NAME IN ('MUD_SNOW_STAMPING', 'SPEED_RATING', 'STAMPED_SINGLE_LOAD_INDEX', 'TIRE_SIZE')
						AND MATL_NUM = LPAD(ps_in_matl_num, 18, 0))
			WHERE rk = 1
			GROUP BY MATL_NUM);

		EXCEPTION
			WHEN NO_DATA_FOUND THEN
            RAISE e_nomatlfound;
		END;

		ln_reccount := 0;    

		FOR lcr_SKUSRec IN lcr_SKUS LOOP
			ln_reccount := ln_reccount + 1;			
			ps_similar_matl_num := lcr_SKUSRec.MATL_NUM;  --As per PRJ3617,Replaced SKu with MATL_NUM
			EXIT;       
		END LOOP;    
        
		IF ln_reccount = 0 THEN
			RAISE e_nosimmatlfound;    
		END IF;

	EXCEPTION
		WHEN e_nomatlfound THEN
			pn_error_num := 1;
			ps_error_desc := 'SKU not in Matl Master';
		WHEN e_nosimmatlfound THEN
			pn_error_num := 2;
			ps_error_desc := 'No Similar Matl Found in Product Table';
		WHEN OTHERS THEN
			pn_error_num := 3;
			ps_error_desc := 'Unexpected Error Occurred';
	
	END GET_ECE_SIMILAR_SKU;

	PROCEDURE GET_GSO_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2)
	IS
	/******************************************************************************
	NAME:       Get_Gso_Similar_Sku
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_matl_num,
												ps_similar_sku with ps_similar_matl_num.
	******************************************************************************/
	BEGIN
		-- Can not return similar sku because
		-- do not know the country of manufacture. 
		ps_similar_matl_num := '';
		pn_error_num 		:= 2;
		ps_error_desc 		:= '';    
	END;

	PROCEDURE GET_NOM_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2)
	IS

	/******************************************************************************
	NAME:       Get_Nom_Similar_Sku
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_matl_num,
												ps_similar_sku with ps_similar_matl_num.
	******************************************************************************/
	BEGIN
		ps_similar_matl_num	:= '';
		pn_error_num 		:= 2;
		ps_error_desc 		:= '';   
	END;

	PROCEDURE GET_IMARK_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
									ps_in_matl_num         IN    VARCHAR2,
									ps_similar_matl_num      OUT VARCHAR2,
									ps_imark_family          OUT NUMBER,    
									ps_ece_reference         OUT VARCHAR2,    
									pn_error_num             OUT NUMBER,
									ps_error_desc            OUT VARCHAR2)
	IS
	/******************************************************************************
	NAME:       Get_Imark_Similar_Sku
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_matl_num,
											  ps_similar_sku with ps_similar_matl_num
	1.2        10/16/2012    Harini         1.checking now with Lower(c.activestatus)='y'
	******************************************************************************/
    ln_familyid             product_imark_family.familyid%TYPE;
    ls_familycode           VARCHAR2(10);
    ln_errornum             NUMBER(2);
    ls_errordesc            VARCHAR2(150);
    ln_reccount             NUMBER(1);
    ln_certificateid           certificate.certificateid%type;
    ln_imark_count           NUMBER;
    e_nofamilyfound         EXCEPTION;    
    e_nosimskufound         EXCEPTION;   
    ln_skuid                   PRODUCT.SKUID%TYPE; 

    CURSOR LCR_SKUS IS
        SELECT P.SKU,
		       LPAD(P.MATL_NUM, 18, 0) AS MATL_NUM, 
			   PC.DATEAPPROVED_CEGI
        FROM PRODUCT P, 
			 PRODUCTCERTIFICATE PC,
             CERTIFICATE C, 
			 PRODUCT_IMARK_FAMILY PIF              ---jeseitz
        WHERE P.SKUID = PC.SKUID
			AND PC.CERTIFICATEID = C.CERTIFICATEID
			and C.CERTIFICATIONTYPEID = 4
			AND P.MATL_NUM <> LPAD(ps_in_matl_num, 18, 0) --As per PRJ3617,replaced SKU with MATL_NUM
			--  Comment out for testing only <<<<<<<<<<<<<<<<<<<<
			AND LOWER(C.ACTIVESTATUS) = 'y'
			AND pc.dateapproved_cegi IS NOT NULL
			--<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
			AND P.DISCONTINUEDDATE IS NULL
			AND PIF.SKUID = P.SKUID
			AND PIF.CERTIFICATEID = C.CERTIFICATEID
			AND PIF.FAMILYID = ln_familyid
			AND UPPER(C.MOSTRECENTCERT) = 'Y'
        ORDER BY PC.DATEAPPROVED_CEGI DESC;

    CURSOR lcr_ECESkus IS
        SELECT P.SKU,
			   LPAD(P.MATL_NUM, 18, 0) AS MATL_NUM, 
			   PC.DATEAPPROVED_CEGI, 
			   C.CERTIFICATENUMBER
        FROM PRODUCT P, 
			 PRODUCTCERTIFICATE PC,
             CERTIFICATE C
        WHERE P.MATL_NUM = LPAD(ps_similar_matl_num, 18, 0)
			AND P.SKUID = PC.SKUID
			AND PC.CERTIFICATEID = C.CERTIFICATEID
			AND C.CERTIFICATIONTYPEID = 1
			--  Comment out for testing only <<<<<<<<<<<<<<<<<<<<
			AND LOWER(C.ACTIVESTATUS) = 'y'
			AND PC.DATEAPPROVED_CEGI IS NOT NULL
			-- <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
			AND P.DISCONTINUEDDATE IS NULL
        ORDER BY PC.DATEAPPROVED_CEGI DESC;
    
	BEGIN
		ln_familyid     := 0;
		ls_familycode   := '';
		ln_errornum     := 0;
		ls_errordesc    := '';
		ln_reccount     := 0;    
  
		GET_IMARK_FAMILY(ps_in_matl_num,
						 0, 
						 ln_familyid, 
						 ls_familycode, 
						 ln_errornum, 
						 ls_errordesc); 
		
		IF ln_errornum > 0 THEN
			RAISE e_nofamilyfound;
		END IF;    

		ps_imark_family := ln_familyid;
 
		FOR lcr_SKUSRec IN lcr_SKUS LOOP
			ln_reccount := ln_reccount + 1;
			ps_similar_matl_num := lcr_SKUSRec.MATL_NUM; 
			EXIT;       
		END LOOP;    
        
		IF ln_reccount = 0 THEN
			RAISE e_nosimskufound;    
		END IF;
    
		ln_reccount := 0;
    
		FOR lcr_ECESkusRec IN lcr_ECESkus LOOP
			ln_reccount := ln_reccount + 1;
			ps_ece_reference  := lcr_ECESkusRec.certificatenumber; 
			EXIT;       
		END LOOP;    

		IF ln_reccount = 0 THEN
			RAISE e_nosimskufound;    
		END IF;

	EXCEPTION
		WHEN e_nofamilyfound THEN
			pn_error_num := 1;
			ps_error_desc := 'Imark Family was not found';
		WHEN e_nosimskufound THEN
			pn_error_num := 2;
			ps_error_desc := 'No Similar SKU Found in Product Table';
		WHEN OTHERS THEN
			pn_error_num := 3;
			ps_error_desc := 'Unexpected Error Occurred';
	
	END GET_IMARK_SIMILAR_SKU;

	PROCEDURE GET_CCC_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								  ps_in_matl_num         IN    VARCHAR2,
								  ps_similar_matl_num      OUT VARCHAR2,
								  pn_error_num             OUT NUMBER,
								  ps_error_desc            OUT VARCHAR2)
	IS
	/******************************************************************************
	NAME:       Get_Ccc_Similar_sku
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_matl_num,
											  ps_similar_sku with ps_similar_matl_num.
	******************************************************************************/
		ln_familyid		PRODUCT_IMARK_FAMILY.FAMILYID%TYPE := NULL;
		ls_familycode	VARCHAR2(10)	:= NULL;
		ln_errornum		NUMBER(2)		:= NULL;
		ls_errordesc	VARCHAR2(150)	:= NULL;

	BEGIN
		-- Can not return similar sku because
		-- do not know the manufactureing plant at this point 
		ps_similar_matl_num := '';
	
	END GET_CCC_SIMILAR_SKU;

	PROCEDURE GET_E117_SIMILAR_SKU(pn_tire_type_id        IN    NUMBER,
								   ps_in_matl_num         IN    VARCHAR2,
								   ps_similar_matl_num      OUT VARCHAR2,
								   pn_error_num             OUT NUMBER,
								   ps_error_desc            OUT VARCHAR2)
	IS
	/******************************************************************************
	NAME:       Get_E117_Similar_Sku
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_in_SKU with ps_in_matl_num,
											  ps_similar_sku with ps_similar_matl_num.
	******************************************************************************/
		ls_ecetireclass    MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE := NULL;
		ln_tpn             NUMBER		:= NULL;
		ln_reccount        NUMBER(3)	:= NULL;
		e_nomatlfound      EXCEPTION;    -- As per PRJ3617,replaced SKU with MATL_NUM
		e_nosimmatlfound   EXCEPTION;    -- As per PRJ3617,replaced SKU with MATL_NUM

	CURSOR LCR_SKUS IS
        SELECT P.SKU,
			   LPAD(P.MATL_NUM, 18, 0) AS MATL_NUM, 
			   PC.DATEAPPROVED_CEGI -- As per PRJ3617,Retrieving MATL_NUM along with SKU
        FROM PRODUCT P, 
		     PRODUCTCERTIFICATE PC,
             CERTIFICATE C
        WHERE P.SKUID = PC.SKUID
			AND PC.CERTIFICATEID = C.CERTIFICATEID
			and C.CERTIFICATIONTYPEID = 6
			AND P.MATL_NUM <> LPAD(ps_in_matl_num, 18, 0)    -- As per PRJ3617,replaced SKU with MATL_NUM
			AND P.DISCONTINUEDDATE IS NULL
			AND P.ECETIRECLASS = ls_ecetireclass
        ORDER BY PC.DATEAPPROVED_CEGI DESC;     
            
	BEGIN
		ps_similar_matl_num	:= '';
		pn_error_num 		:= 0;
		ps_error_desc 		:= '';
    
		BEGIN
			ln_tpn := 0;
			ls_ecetireclass := BOM_ATTRIBUTES.GET_ECE_TIRE_CLASS(ps_in_matl_num); -- As per PRJ3617,replaced query by calling BOM_ATTRIBUTES.Get_Ece_Tire_Class
		EXCEPTION
			WHEN NO_DATA_FOUND THEN          
            RAISE e_nomatlfound;
		END;
    
		FOR lcr_SKUSRec IN lcr_SKUS LOOP
			ln_reccount := ln_reccount + 1;
			ps_similar_matl_num := lcr_SKUSRec.MATL_NUM;   -- As per PRJ3617,replaced SKU with MATL_NUM
			EXIT;       
		END LOOP;    
        
		IF ln_reccount = 0 THEN
			RAISE e_nosimmatlfound;    
		END IF; 
	
	EXCEPTION            
		WHEN e_nomatlfound THEN  -- As per PRJ3617,replaced SKU with MATL_NUM
			pn_error_num := 1;
			ps_error_desc := 'Matl not in Matl Master';
		WHEN e_nosimmatlfound THEN  -- As per PRJ3617,replaced SKU with MATL_NUM
			pn_error_num := 2;
			ps_error_desc := 'No Similar Matl Found in Product Table';
		WHEN OTHERS THEN
			pn_error_num := 3;
			ps_error_desc := 'Unexpected Error Occurred';
	
	END GET_E117_SIMILAR_SKU;        

	PROCEDURE GET_IMARK_FAMILY(ps_matl_num            IN    VARCHAR2,
							   pn_certificateid       IN    NUMBER,
							   pn_family_id             OUT NUMBER,
							   ps_family_code           OUT VARCHAR2,    
							   pn_error_num             OUT NUMBER,
							   ps_error_desc            OUT VARCHAR2)
	IS
	/******************************************************************************
	NAME:       Get_Imark_Family
	PURPOSE:    
	
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/04/2012    Harini         1.Replaced ps_SKU with ps_Matl_Num
	1.2        11/19/2013    Harini           Changed logic in For loop
	******************************************************************************/
		ln_skuid            PRODUCT.SKUID%TYPE					:= NULL;
		ls_matl_num         PRODUCT.MATL_NUM%TYPE				:= NULL;
		ls_appcat           IMARK_FAMILY.APPLICATION_CAT%TYPE	:= NULL;
		ls_aspectratiocat   IMARK_FAMILY.ASPECT_RATIO_CAT%TYPE	:= NULL;
		ls_constructiontype	IMARK_FAMILY.CONSTRUCTION_TYPE%TYPE	:= NULL;
		ls_familycode       IMARK_FAMILY.FAMILY_CODE%TYPE		:= NULL;
		ls_loadindexcat     IMARK_FAMILY.LOAD_INDEX_CAT%TYPE	:= NULL;
		ls_mountingtype     IMARK_FAMILY.MOUNTING_TYPE%TYPE		:= NULL;
		ls_speedratingcat   IMARK_FAMILY.SPEED_RATING_CAT%TYPE	:= NULL;
		ls_structuretype    IMARK_FAMILY.STRUCTURE_TYPE%TYPE	:= NULL;
		ln_certificateid    CERTIFICATE.CERTIFICATEID%TYPE		:= NULL;

		ln_loadindex    NUMBER	:= NULL;
		ln_aspectratio	NUMBER	:= NULL;
		ln_reccount     NUMBER	:= NULL;

    CURSOR LCR_SKUS IS
		SELECT SKU,
			   LPAD(MATL_NUM, 18, 0) AS MATL_NUM, 
			   TIRETYPEID, 
			   ASPECTRATIO, 
			   BIASBELTEDRADIAL, 
			   EXTRALOADYN, 
			   REINFORCEDYN,
			   TUBELESSYN, 
			   SPEEDRATING, 
			   SINGLOADINDEX, 
			   LOADRANGE 
        FROM PRODUCT
        WHERE SKUID = ln_skuid;

	BEGIN
		--  Initialize return variables
		ps_error_desc 	:= '';
		pn_error_num 	:= 0;    
		ln_reccount 	:= 0;    
		ln_loadindex 	:= 0;
		ln_aspectratio	:= 0;
    
		IF  pn_certificateid = 0 THEN
			SELECT MAX(CERTIFICATEID) INTO ln_certificateid  
			FROM PRODUCT_IMARK_FAMILY;
		ELSE
			ln_certificateid := pn_certificateid;
		END IF;
 
		BEGIN
			SELECT NVL( MAX(SKUID),0) INTO ln_skuid
			FROM PRODUCT
			WHERE MATL_NUM = LPAD(ps_matl_num, 18, 0);
		EXCEPTION
			WHEN NO_DATA_FOUND THEN
            ln_skuid := 0;
		END;             
   
		FOR lcr_SKUSRec IN lcr_SKUS LOOP    
        ln_reccount := ln_reccount + 1;

			IF ln_reccount > 1 THEN
				pn_error_num := 2;
				ps_error_desc := 'Too many SKUS for ID';
				EXIT;
			END IF;    

			IF lcr_SKUSRec.TIRETYPEID = 1 THEN
				ls_appcat := 2;
			ELSIF lcr_SKUSRec.TIRETYPEID IN (3, 4) THEN
				ls_appcat := 3;
			ELSIF lcr_SKUSRec.TIRETYPEID =  7 THEN
				ls_appcat := 4;
			ELSE
				ls_appcat := 0;    
			END IF;

			IF lcr_SKUSRec.BIASBELTEDRADIAL = 'BIAS' THEN
				ls_constructiontype := 'A1';
			ELSIF lcr_SKUSRec.BIASBELTEDRADIAL = 'RADIAL' THEN
				ls_constructiontype := 'A2';
			ELSIF lcr_SKUSRec.BIASBELTEDRADIAL = 'BELTED' THEN
				ls_constructiontype := '*';
			ELSE
				ls_constructiontype := '*';
			END IF;             
    
			IF ls_appcat = 2 THEN

				IF (lcr_SKUSRec.EXTRALOADYN = 'Y') 
					or (lcr_SKUSRec.REINFORCEDYN = 'Y')
					or (lcr_SKUSRec.LOADRANGE = 'C') 
					THEN
					ls_structuretype := 'B2';
				ELSE
					ls_structuretype := 'B1';
				END IF;

				ln_aspectratio := NVL(TO_NUMBER(lcr_SKUSRec.aspectratio),200);
          
				ls_aspectratiocat :=
					CASE 
						WHEN ln_aspectratio >= 85 THEN               'C1'
						WHEN ln_aspectratio BETWEEN 80 and 82 THEN   'C2'
						WHEN ln_aspectratio = 75 THEN                'C3'
						WHEN ln_aspectratio = 70 THEN                'C4' 
						WHEN ln_aspectratio = 65 THEN                'C5'
						WHEN ln_aspectratio BETWEEN 55 and 60 THEN   'C6'
						WHEN ln_aspectratio < 55 THEN                'C7'
					ELSE '*'
					END;           

				ls_speedratingcat := 
					CASE 
						WHEN lcr_SKUSRec.speedrating IN ('F','G','L','M', 'N') THEN 'D1'
						WHEN lcr_SKUSRec.speedrating IN ('P','Q','R') THEN          'D2'
						WHEN lcr_SKUSRec.speedrating IN ('S','T') THEN              'D3'
						WHEN lcr_SKUSRec.speedrating IN ('U','H') THEN              'D4'         
						WHEN lcr_SKUSRec.speedrating IN ('V','W','Y','Z') THEN      'D5'
					ELSE '*'                
					END;

				BEGIN 
					SELECT FAMILY_ID, 
						   FAMILY_CODE
						INTO pn_family_id, 
						     ps_family_code
					FROM IMARK_FAMILY
					WHERE APPLICATION_CAT = ls_appcat
						AND CONSTRUCTION_TYPE = ls_constructiontype
						AND STRUCTURE_TYPE = ls_structuretype    
						AND ASPECT_RATIO_CAT = ls_aspectratiocat
						AND SPEED_RATING_CAT = ls_speedratingcat
						AND CERTIFICATEID = ln_certificateid;
				EXCEPTION    
					WHEN NO_DATA_FOUND THEN
						pn_error_num := 1;
						ps_error_desc := 'No Imark Family Found';
				END;

			END IF;

			IF ls_appcat = 3 THEN        
				ln_loadindex := NVL(TO_NUMBER(lcr_SKUSRec.singloadIndex),400);
     
				ls_loadindexcat := 
					CASE
						WHEN ln_loadindex < 94 THEN                'B1'
						WHEN ln_loadindex BETWEEN 94 and 104 THEN  'B2'     
						WHEN ln_loadindex BETWEEN 105 and 113 THEN 'B3' 
						WHEN ln_loadindex BETWEEN 114 and 300 THEN 'B4' 
					ELSE '*'
					END;
    
				IF lcr_SKUSRec.tubelessYN = 'Y' THEN
					ls_mountingtype := 'C2';
				ELSE
					ls_mountingtype := 'C1';    
				END IF;

				BEGIN 
					SELECT FAMILY_ID, 
						   FAMILY_CODE
						INTO pn_family_ID, 
							 ps_family_code
					FROM IMARK_FAMILY
					WHERE APPLICATION_CAT = ls_appcat
						AND LOAD_INDEX_CAT = ls_loadindexcat
						AND MOUNTING_TYPE = ls_mountingtype
						AND CONSTRUCTION_TYPE = ls_constructiontype
						AND CERTIFICATEID = ln_certificateid;
				EXCEPTION    
					WHEN NO_DATA_FOUND THEN
						pn_error_num := 1;
						ps_error_desc := 'No Imark Family Found';
				END;

			END IF;

			IF ls_appcat = 4 THEN
				ln_loadindex := NVL(TO_NUMBER(lcr_SKUSRec.singloadIndex),600);
  
				ls_loadindexcat := 
					CASE
						WHEN ln_loadindex < 126 THEN                'B1'
						WHEN ln_loadindex BETWEEN 126 and 130 THEN  'B2'     
						WHEN ln_loadindex BETWEEN 131 and 135 THEN  'B3' 
						WHEN ln_loadindex BETWEEN 136 and 141 THEN  'B4'     
						WHEN ln_loadindex BETWEEN 142 and 146 THEN  'B5' 
						WHEN ln_loadindex BETWEEN 147 and 151 THEN  'B6'     
						WHEN ln_loadindex BETWEEN 152 and 156 THEN  'B7' 
						WHEN ln_loadindex BETWEEN 157 and 161 THEN  'B8'     
						WHEN ln_loadindex BETWEEN 162 and 166 THEN  'B9' 
						WHEN ln_loadindex BETWEEN 167 and 500 THEN  'B10' 
					ELSE '*'
					END;
    
				IF lcr_SKUSRec.TUBELESSYN = 'Y' THEN
					ls_mountingtype := 'C2';
				ELSE
					ls_mountingtype := 'C1';    
				END IF;

				BEGIN 
					SELECT FAMILY_ID, 
						   FAMILY_CODE
						INTO pn_family_ID, 
					         ps_family_code
					FROM IMARK_FAMILY
					WHERE APPLICATION_CAT = ls_appcat
						AND CONSTRUCTION_TYPE = ls_constructiontype
						AND MOUNTING_TYPE = ls_mountingtype
						AND LOAD_INDEX_CAT =  ls_loadindexcat
						AND CERTIFICATEID = ln_certificateid;
				EXCEPTION    
					WHEN NO_DATA_FOUND THEN
						pn_error_num := 1;
						ps_error_desc := 'No Imark Family Found';
				END;

			END IF;                  
        
		END LOOP;

		IF pn_family_id IS NULL THEN
			pn_family_id 	:= 0;
			pn_error_num 	:= 1;
			ps_error_desc 	:= 'No Family Found';
		END IF;

	EXCEPTION
		WHEN OTHERS THEN
        DECLARE
            lserrormsg VARCHAR2(80) := SQLERRM;
        BEGIN
            pn_error_num := 3;
            ps_error_desc := SUBSTR(lserrormsg, 1, 80);
            
			INSERT INTO ICS.LOAD_ERROR(TABLE_LOADED, 
									   KEY_FIELD_DATA_1, 
									   ERROR_DATE, 
									   ERROR_DESC)
								VALUES('IMARK_FAMILY', 
									   ps_matl_num, 
									   SYSDATE, 
									   ps_error_desc);
        END;

	END GET_IMARK_FAMILY;
  
END SIMILAR_TIRES;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.SKU_MAINTENANCE 
AS
/******************************************************************************
   NAME:       SKU_MAINTENANCE
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        2/10/2013      jeseitz       1. Created this package.
******************************************************************************/
  
	PROCEDURE SKU_RELOAD_FROM_CMDR;
	
	PROCEDURE SPLITBRAND(vs_branddesc	IN 	  VARCHAR2, 
					     vs_brand 		  OUT VARCHAR2, 
						 vs_brand_line 	  OUT VARCHAR2);
	
END SKU_MAINTENANCE;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.SKU_MAINTENANCE 
AS
/******************************************************************************
   NAME:       SKU_MAINTENANCE
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        2/10/2013      jeseitz       1. This package contains routines that will be scheduled to run to take care of the situation
                                                         where a sku has been created in sku_master, but does not exist in SAP.  We need to allow these 
                                                         materials to be brought into ICS before they have been created in SAP. (This is believed to be an issue
                                                         with China sku's that were not loaded into SAP yet.)
   2.0       4/7/1014        jeseitz        1. SKU_RELOAD_FROM_CMDR modified to run weekly to pull in new material numbers created for 999 skus that
                                                         didn't have an SAP number originally.
******************************************************************************/

	PROCEDURE SKU_RELOAD_FROM_CMDR
	--This procedure will be scheduled to run daily to find the correct SAP number for any sku's that are already in the product table
	--but do not have a SAP material number assigned.
	IS
    ln_commitcnt      	   NUMBER(4)										:= NULL;
    ln_skuid               PRODUCT.SKUID%TYPE								:= NULL;
    ln_count               NUMBER(3)										:= NULL;
    ln_firstspace          NUMBER											:= NULL;
    ln_rec_count       	   NUMBER											:= NULL;
    ln_error_count   	   NUMBER											:= NULL;
    ls_machineid           VARCHAR2(50)										:= NULL;
    ls_operatorid          VARCHAR2(50)										:= 'ICSDEV';
    ls_errormsg            VARCHAR2(4000)									:= NULL;
    ln_errornum            NUMBER(1)										:= NULL;    
    ls_imarkfamily         ICS.PRODUCT.FAMILY%TYPE							:= NULL;
    le_done                EXCEPTION;
    ls_brand               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_brandline           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_sizestamp           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_tiretypeid          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_psn                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_discontinuedate     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_specnumber          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_speedrating         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_singleloadindex     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_dualloadindex       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_tubelesssyn         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_reinforcedyn        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_extraloadyn         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_utqgtreadwear       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_utqgtraction        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_utqgtemp            CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_mudsnowyn           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_severeweatherind    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_rimdiameter         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_serialdate          CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_loadrange           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_mearimwidth         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_regroovableind      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_plantproduced       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_mostrecentdate      CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_imark               CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_informenumber       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_fechadate           CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_treadpattern        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_specialprotbrand    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_nominaltirewidth    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_aspectratio         CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_treadwearind        CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_nameofmanufac       CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_family              CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_dotserialnumber     CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_tpn                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL;
    ls_biasbeltedradial    CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL; 
    ls_sku                 CMDR_DATA.MATERIAL_ATTRIBUTE.ATTRIB_VALUE%TYPE	:= NULL; 
  
	-- JESEITZ - 2/19/2014 - changed to find material numbers from cmdr for sku's that don't have actual material numbers.            
	CURSOR lcr_skus IS         
		SELECT P.*, 
			   MA.MATL_NUM AS SAP_MATL_NUM 
		FROM CMDR_DATA.MATERIAL_ATTRIBUTE MA, 
			 ICS.PRODUCT P
		WHERE MA.ATTRIB_NAME = 'LEGACY_COOPER_SKU'
			AND MA.ATTRIB_VALUE = P.SKU
			AND SUBSTR(P.MATL_NUM,1,8) = '00000999' 
			AND SUBSTR(MA.MATL_NUM,1,9 ) = '000000090';
			
	BEGIN
		-- Initialize return variables
		ln_rec_count := 0;     
		ln_commitcnt := 0;
		ln_error_count :=0;    

        FOR lcr_skusRec IN lcr_skus LOOP
      
			BEGIN
				ln_skuid := lcr_skusRec.skuid;
       
				---get tire characteristics
				ics_crud.GetTireCharacteristicsAll(lcr_skusRec.SAP_MATL_NUM,
												   ls_brand,
												   ls_brandline,
												   ls_sizestamp,
												   ls_tiretypeid,
												   ls_psn,
												   ls_discontinuedate,
												   ls_specnumber,
												   ls_speedrating,
												   ls_singleloadindex,
												   ls_dualloadindex,
												   ls_tubelesssyn, 
												   ls_reinforcedyn,
												   ls_extraloadyn,
												   ls_utqgtreadwear,
												   ls_utqgtraction,
												   ls_utqgtemp,
												   ls_mudsnowyn,
												   ls_severeweatherind,
												   ls_rimdiameter,
												   ls_serialdate,
												   ls_loadrange,
												   ls_mearimwidth,
												   ls_regroovableind,
												   ls_plantproduced,
												   ls_mostrecentdate,
												   ls_imark,
												   ls_informenumber,
												   ls_fechadate,
												   ls_treadpattern,
												   ls_specialprotbrand,
												   ls_nominaltirewidth,
												   ls_aspectratio,
												   ls_treadwearind,
												   ls_nameofmanufac,
												   ls_family,
												   ls_dotserialnumber,
												   ls_tpn,
												   ls_biasbeltedradial,
												   ls_sku);
				
				-- As per PRJ3617,Modified the paramters
				update Product 
				set MATL_NUM 				= lcr_skusRec.SAP_MATL_NUM, 
                    BRAND 					= NVL(ls_brand, lcr_skusRec.BRAND),
                    BRAND_LINE 				= NVL(ls_brandline, lcr_skusRec.BRAND_LINE),
                    SIZESTAMP 				= NVL(ls_sizestamp, lcr_skusRec.SIZESTAMP),
                    TIRETYPEID 				= NVL(ls_tiretypeid, lcr_skusRec.TIRETYPEID),
                    PSN						= NVL(ls_psn, lcr_skusRec.PSN),--- ls_psn,
                    DISCONTINUEDDATE 		= DECODE(ls_discontinuedate, NULL, lcr_skusRec.DISCONTINUEDDATE, TO_DATE(ls_discontinuedate, 'MM/DD/YYYY')),
                    SPECNUMBER 				= NVL(ls_specnumber, lcr_skusRec.SPECNUMBER),
                    SPEEDRATING 			= NVL(ls_speedrating, lcr_skusRec.SPEEDRATING)    ,                                                                                                         
                    SINGLOADINDEX  			= COALESCE(ls_singleloadindex, lcr_skusRec.SINGLOADINDEX),---ls_SingleLoadIndex,
                    DUALLOADINDEX  			= COALESCE(ls_dualloadindex, lcr_skusRec.DUALLOADINDEX),---ls_DualLoadIndex,
                    TUBELESSYN 				= COALESCE(lcr_skusRec.TUBELESSYN, ls_tubelesssyn),----ls_tubelessSyn,
                    REINFORCEDYN  			= COALESCE(ls_reinforcedyn, lcr_skusRec.REINFORCEDYN),---ls_reinforcedyn,
                    EXTRALOADYN  			= COALESCE(ls_extraloadyn, lcr_skusRec.EXTRALOADYN),---ls_extraloadyn,
                    UTQGTREADWEAR  			= COALESCE(ls_utqgtreadwear, lcr_skusRec.UTQGTREADWEAR),---ls_utqgtreadwear,
                    UTQGTRACTION  			= DECODE(NVL(ls_utqgtraction, '0'), '0', lcr_skusRec.UTQGTRACTION, ls_utqgtraction),---ls_utqgtraction,
                    UTQGTEMP 				= COALESCE(ls_utqgtemp, lcr_skusRec.UTQGTEMP),--- ls_utqgtemp,
                    MUDSNOWYN  				= COALESCE(lcr_skusRec.MUDSNOWYN, ls_mudsnowyn),----ls_mudsnowyn,
                    SEVEREWEATHERIND 		= COALESCE(lcr_skusRec.SEVEREWEATHERIND, ls_severeweatherind) ,
                    RIMDIAMETER  			= NVL(ls_rimdiameter, lcr_skusRec.RIMDIAMETER),
                    SERIALDATE  			= NVL(ls_serialdate, lcr_skusRec.SERIALDATE),
                    LOADRANGE  				= COALESCE(ls_loadrange, lcr_skusRec.LOADRANGE),--- ls_loadrange,
                    MEARIMWIDTH  			= NVL(ls_mearimwidth, lcr_skusRec.MEARIMWIDTH),
                    REGROOVABLEIND 		 	= COALESCE(ls_regroovableind, lcr_skusRec.REGROOVABLEIND),---lcr_skusRec.regroovableind,
                    PLANTPRODUCED  			= COALESCE(ls_plantproduced, lcr_skusRec.PLANTPRODUCED),
                    MOSTRECENTTESTDATE  	= ls_mostrecentdate,---COALESCE(ls_MostRecentDate, lcr_skusRec.mostrecenttestdate),
                    TREADPATTERN  			= COALESCE(ls_treadpattern, lcr_skusRec.TREADPATTERN),
                    SPECIALPROTECTIVEBAND	= COALESCE(ls_specialprotbrand, lcr_skusRec.SPECIALPROTECTIVEBAND),
                    NOMINALTIREWIDTH 		= COALESCE(ls_nominaltirewidth, lcr_skusRec.NOMINALTIREWIDTH),
                    ASPECTRATIO  			= COALESCE(ls_aspectratio, lcr_skusRec.ASPECTRATIO),---ls_aspectratio,
                    TREADWEARINDICATORS		= COALESCE(ls_treadwearind, lcr_skusRec.TREADWEARINDICATORS),
                    NAMEOFMANUFACTURER  	= COALESCE(ls_nameofmanufac, lcr_skusRec.NAMEOFMANUFACTURER), 
                    DOTSERIALNUMBER  		= COALESCE(ls_dotserialnumber, lcr_skusRec.DOTSERIALNUMBER),
                    TPN  					= COALESCE(ls_tpn, lcr_skusRec.TPN),
                    BIASBELTEDRADIAL  		= COALESCE(lcr_skusRec.BIASBELTEDRADIAL, ls_biasbeltedradial),---ls_biasbeltedradial,
                    MODIFIEDBY 				= 'SKU_RELOAD_FROM_CMDR',
                    MODIFIEDON 				= SYSDATE
				WHERE SKUID = lcr_skusRec.SKUID;
                    
				COMMIT;                 
               
			EXCEPTION
				WHEN OTHERS THEN
				DECLARE
					lserrormsg VARCHAR2(300) := SQLERRM;
				BEGIN
				INSERT INTO ICS.LOAD_ERROR(TABLE_LOADED, 
										   KEY_FIELD_DATA_1, 
										   KEY_FIELD_DATA_2, 
										   ERROR_DATE,
										   DB_ERROR_NO, 
										   ERROR_DESC)
                                    VALUES('PRODUCT',
										   'sku_maintenance.SKU_RELOAD_FROM_CMDR',
                                           'Error inserting '||lcr_skusRec.SKU||' '||lcr_skusRec.SKUID,
                                           SYSDATE,
										   '3',
										   SUBSTR(lserrormsg, 1, 80) || ' (DB)');           
				END;
				
			END;
			
		END LOOP;                       

	END SKU_RELOAD_FROM_CMDR;
	
	PROCEDURE SPLITBRAND(vs_branddesc	IN 	  VARCHAR2, 
					     vs_brand 		  OUT VARCHAR2, 
						 vs_brand_line 	  OUT VARCHAR2)
	IS
		ln_paren 		NUMBER			:= NULL;
		ln_len  		NUMBER			:= NULL;
		ls_brand  		VARCHAR2(100)	:= NULL;
		ls_brandfound	VARCHAR2(1)		:= NULL;
   
    CURSOR lc_brands IS
		SELECT DISTINCT ATTRIB_VALUE 
		FROM CMDR_DATA.MATERIAL_ATTRIBUTE
		WHERE ATTRIB_NAME = 'BRAND' ;
	
	BEGIN 
		--check for parenthesis
		ln_paren := INSTR(vs_branddesc, '(');
			
		IF 
			ln_paren = 0 THEN --no parenthesis
			ls_brand := vs_branddesc;
		ELSE 
			ls_brand := substr(vs_branddesc,1,ln_paren-1);
		END IF;   
      
		ls_brandfound := 'N'; 
      
		FOR lcr_BRANDS IN lc_BRANDS LOOP
			--- check for the brand vs brand_line  -- check for the brand in the material_attribute table
			IF 
				ls_brandfound = 'N' AND INSTR(ls_brand, lcr_BRANDS.ATTRIB_VALUE) > 0 THEN
				vs_brand := lcr_BRANDS.ATTRIB_VALUE;
                vs_brand_line := SUBSTR(TRIM(REPLACE(ls_brand, lcr_BRANDS.ATTRIB_VALUE)), 1, 30);
                ls_brandfound := 'Y';
           END IF;
		END LOOP;
      
		IF 
			ls_brandfound = 'N' THEN
			---just split based on the first space
            vs_brand := TRIM(SUBSTR(ls_brand, 1, INSTR(ls_brand, ' ')-1));
            vs_brand_line := SUBSTR(TRIM(SUBSTR(ls_brand, INSTR(ls_brand, ' ')+1)), 1, 30) ;
            ls_brandfound := 'Y';       
		END IF; 
           
	END SPLITBRAND;
 
END SKU_MAINTENANCE;
/

CREATE OR REPLACE PACKAGE ICS_PROCS.TESTRESULTS_CRUD 
AS
	/******************************************************************************
	NAME:       TESTRESULTS_CRUD
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/02/2012    Harini         1.Replaced ps_SKU with ps_Matl_Num in
												GetProductData,Product_Save,Measure_Save,
												Endurance_Save,TREADWEAR_SAVE,PLUNGER_Save,
												BeadUnseat_Save,HighSpeedHdr_Save
												CheckIfProductExists procedures  
	1.2       10/18/2012    Harini          2.Added new input parameter ps_Operation in
											Measure_Save,Endurance_Save,TREADWEAR_SAVE,
											PLUNGER_Save,BeadUnseat_Save,HighSpeedHdr_Save,
											SoundHDR_Save,WetGripHDR_Save procedures
	1.3       11/04/2013    Harini          1.As per IDEA2706,Modified Product_Save procedure by adding 
											ps_SEVEREWEATHERIND and ps_MFGWWYY parameters
											2. Added ps_MFGWWYY,ps_GTSPEC parameters to Measure_Save,
											TREADWEAR_SAVE,PLUNGER_Save,BeadUnseat_Save,SoundHDR_Save,
											WetGripHDR_Save procedures.
											3. Added ps_GTSPEC parameter to Endurance_Save and HighSpeedHdr_Save
											procedures
	1.4       11/06/2013     Ajit           1. As per IDEA 2706,Added GetTireTypes procedure
	******************************************************************************/
	TYPE retcursor IS REF CURSOR;
  
	PROCEDURE GETPRODUCTDATA(pc_retcursor	OUT   retcursor,
							 ps_matl_num 	   IN VARCHAR2,
							 pi_skuid 		   IN NUMBER);
  
	PROCEDURE PRODUCT_SAVE(pi_skuid 				IN  NUMBER,
                          ps_matl_num 				IN  VARCHAR2,
                          ps_brand 					IN  VARCHAR2,
                          ps_brand_line 			IN  VARCHAR2,
                          pi_tiretypeid 			IN  NUMBER,
                          ps_psn 					IN  VARCHAR2,
                          ps_sizestamp 				IN  VARCHAR2,
                          pd_discontinueddate 		IN  DATE,
                          ps_specnumber 			IN  VARCHAR2,
                          ps_speedrating 			IN  VARCHAR2,
                          ps_singloadindex 			IN  VARCHAR2,
                          ps_dualloadindex 			IN  VARCHAR2,
                          ps_biasbeltedradial 		IN  VARCHAR2,
                          ps_tubelessyn 			IN  VARCHAR2,
                          ps_reinforcedyn 			IN  VARCHAR2,
                          ps_extraloadyn 			IN  VARCHAR2,
                          ps_utqgtreadwear 			IN  VARCHAR2,
                          ps_utqgtraction 			IN  VARCHAR2,
                          ps_utqgtemp 				IN  VARCHAR2,
                          ps_mudsnowyn 				IN  VARCHAR2,
                          ps_severeweatherind 		IN 	VARCHAR2,
                          pi_rimdiameter 			IN  NUMBER,
                          pd_serialdate 			IN  DATE,
                          ps_mfgwwyy   				IN 	VARCHAR2,
                          ps_branddesc 				IN  VARCHAR2,
                          ps_loadrange 				IN  VARCHAR2,
                          pi_mearimwidth 			IN  NUMBER,
                          ps_regroovableind 		IN  VARCHAR2,
                          ps_plantproduced 			IN  VARCHAR2,
                          pd_mostrecenttestdate 	IN  DATE,
                          ps_imark 					IN  VARCHAR2,
                          ps_informenumber 			IN  VARCHAR2,
                          pd_fechadate 				IN  DATE,
                          ps_treadpattern 			IN  VARCHAR2,
                          ps_specialprotectiveband	IN  VARCHAR2,
                          ps_nominaltirewidth 		IN  VARCHAR2,
                          ps_aspectradio 			IN  VARCHAR2,
                          ps_treadwearindicators 	IN  VARCHAR2,
                          ps_nameofmanufacturer 	IN  VARCHAR2,
                          ps_family 				IN  VARCHAR2,
                          ps_dotserialnumber 		IN  VARCHAR2,
                          ps_tpn 					IN 	VARCHAR2,
                          ps_operatorname   		IN 	VARCHAR2);
                          
	PROCEDURE GETMEASURE(pc_measurecursor 		  OUT retcursor,
                         pc_measuredetailcursor   OUT retcursor,
                         pi_skuid 				IN 	  NUMBER,
                         ps_certificatenumber 	IN    VARCHAR2,
                         pi_certificationtypeid	IN    NUMBER,
                         pi_certificatenumberid IN    NUMBER);
                         
	PROCEDURE GETPLUNGER(pc_plungerhdrcursor 	  OUT retcursor,
                         pc_plungerdtlcursor 	  OUT retcursor,
                         pi_skuid 				IN    NUMBER,
                         ps_certificatenumber 	IN    VARCHAR2,
                         pi_certificationtypeid	IN    NUMBER,
                         pi_certificatenumberid IN    NUMBER);
                         
	PROCEDURE GETTREADWEAR(pc_treadwearhdrcursor      OUT retcursor,
                           pc_treadweardtlcursor      OUT retcursor,
                           pi_skuid 				IN    NUMBER,
                           ps_certificatenumber 	IN    VARCHAR2,
                           pi_certificationtypeid 	IN    NUMBER,
                           pi_certificatenumberid 	IN    NUMBER);
                         
	PROCEDURE GETBEADUNSEAT(pc_beadunseathdrcursor    OUT retcursor,
                            pc_beadunseatdtlcursor    OUT retcursor,
                            pi_skuid 				IN    NUMBER,
                            ps_certificatenumber 	IN    VARCHAR2,
                            pi_certificationtypeid 	IN    NUMBER,
                            pi_certificatenumberid 	IN    NUMBER);
                         
	PROCEDURE GETENDURANCE(pc_endurancehdrcursor      OUT retcursor,
                           pc_endurancedtlcursor      OUT retcursor,
                           pi_skuid 				IN    NUMBER,
                           ps_certificatenumber 	in    varchar,
                           pi_certificationtypeid 	IN    NUMBER,
                           pi_certificatenumberid 	IN    NUMBER);
                         
	PROCEDURE GETHIGHSPEED(pc_highspeedcursor         OUT retcursor,
                           pc_highspeeddetailcursor   OUT retcursor,
                           pc_hsspeedtestdetail 	  OUT retcursor,
                           pi_skuid 				IN 	  NUMBER,
                           ps_certificatenumber 	IN 	  VARCHAR2,
                           pi_certificationtypeid 	IN 	  NUMBER,
                           pi_certificatenumberid 	IN 	  NUMBER);
                         
	PROCEDURE GETSOUND(pc_soundhdrcursor 		  OUT retcursor,
                       pc_sounddetailcursor 	  OUT retcursor,
                       pi_skuid 				IN    NUMBER,
                       ps_certificatenumber		IN    VARCHAR2,
                       pi_certificationtypeid 	IN    NUMBER,
                       pi_certificatenumberid 	IN    NUMBER);
                         
	PROCEDURE GETWETGRIP(pc_wetgriphdrcursor 	  OUT retcursor,
                         pc_wetgripdetailcursor   OUT retcursor,
                         pi_skuid 				IN    NUMBER,
                         ps_certificatenumber 	IN    VARCHAR2,
                         pi_certificationtypeid IN    NUMBER,
                         pi_certificatenumberid IN    NUMBER);
                         
	PROCEDURE GETTESTRESULTS(pc_measurecursor 		  OUT retcursor,
                             pc_measuredetailcursor   OUT retcursor,
                             pc_plungerhdrcursor      OUT retcursor,
                             pc_plungerdtlcursor      OUT retcursor,
                             pc_treadwearhdrcursor    OUT retcursor,
                             pc_treadweardtlcursor    OUT retcursor,
                             pc_beadunseathdrcursor   OUT retcursor,
                             pc_beadunseatdtlcursor   OUT retcursor,
                             pc_endurancehdrcursor 	  OUT retcursor,
                             pc_endurancedtlcursor 	  OUT retcursor,
                             pc_highspeedcursor  	  OUT retcursor,
                             pc_highspeeddetailcursor OUT retcursor,
                             pc_hsspeedtestdetail     OUT retcursor,
                             pc_soundhdrcursor  	  OUT retcursor,
                             pc_sounddetailcursor  	  OUT retcursor,
                             pc_wetgriphdrcursor  	  OUT retcursor,
                             pc_wetgripdetailcursor   OUT retcursor,
                             pi_certificationtypeid IN    NUMBER,
                             pi_skuid 				IN    NUMBER,
                             ps_certificatenumber 	IN    VARCHAR,
                             pi_certificatenumberid	IN    NUMBER);
                           
	PROCEDURE MEASURE_SAVE(pi_measureid 				OUT NUMBER,
                           pi_certificateid  			IN NUMBER,
                           ps_projectnumber 			IN VARCHAR2,
                           pi_tirenumber 				IN NUMBER,
                           ps_testspec 					IN VARCHAR2,
                           pd_completiondate 			IN DATE,
                           pi_inflationpressure 		IN NUMBER,
                           ps_molddesign 				IN VARCHAR2,
                           pi_rimwidth 					IN NUMBER,
                           ps_dotserialnumber 			IN VARCHAR2,
                           pi_diameter 					IN NUMBER,
                           pi_avgsectionwidth 			IN NUMBER,
                           pi_avgoverallwidth 			IN NUMBER,
                           pi_maxoverallwidth 			IN NUMBER,
                           pi_sizefactor 				IN NUMBER,
                           pd_mounttime 				IN DATE,
                           pi_mounttemp 				IN NUMBER,
                           pd_serialdate 				IN DATE,
                           ps_mfgwwyy    				IN VARCHAR2,
                           pd_endtime 					IN DATE,
                           pi_actsizefactor 			IN NUMBER,
                           pi_certificationtypeid 		IN NUMBER,
                           pi_startinflationpressure 	IN NUMBER,
                           pi_endinflationpressure 		IN NUMBER,
                           ps_adjustment 				IN VARCHAR2,
                           pi_circunference 			IN NUMBER,
                           pi_nominaldiameter 			IN NUMBER,
                           pi_nominalwidth 				IN NUMBER,
                           ps_nominalwidthpassfail 		IN VARCHAR2,
                           pi_nominalwidthdiference 	IN NUMBER,
                           pi_nominalwidthtolerance 	IN NUMBER,
                           pi_maxoveralldiameter 		IN NUMBER,
                           pi_minoveralldiameter 		IN NUMBER,
                           ps_overallwidthpassfail 		IN VARCHAR2,
                           ps_overalldiameterpassfail 	IN VARCHAR2,
                           pi_diameterdiference 		IN NUMBER,
                           pi_diametertolerance 		IN NUMBER,
                           pi_tempresistancegrading 	IN VARCHAR2,
                           pi_tensilestrenght1 			IN NUMBER,
                           pi_tensilestrenght2 			IN NUMBER,
                           pi_elongation1 				IN NUMBER,
                           pi_elongation2 				IN NUMBER,
                           pi_tensilestrenghtafterage1	IN NUMBER,
                           pi_tensilestrenghtafterage2 	IN NUMBER,
                           ps_operatorname   			IN VARCHAR2,
                           ps_matl_num 					IN VARCHAR2,
                           ps_operation 				IN VARCHAR2,
                           ps_gtspec 					IN VARCHAR2);
                          
	PROCEDURE MEASUREDETAIL_SAVE(pi_sectionwidth 	IN MEASUREDTL.SECTIONWIDTH%TYPE,
                                 pi_overallwidth 	IN MEASUREDTL.OVERALLWIDTH%TYPE,
                                 pi_measureid 		IN NUMBER,
                                 pi_iteration 		IN NUMBER,
                                 ps_operatorname	IN VARCHAR2);
                               
	PROCEDURE ENDURANCE_SAVE(pi_enduranceid 				  OUT NUMBER,
                             ps_projectnumber 				IN    VARCHAR2,
                             pi_tirenumber 					IN    NUMBER,
                             ps_testspec 					IN    VARCHAR2,
                             pd_completiondate 				IN    DATE,
                             ps_dotserialnumber 			IN    VARCHAR2,
                             ps_mfgwwyy 					IN    VARCHAR2,
                             pd_precondstartdate 			IN    DATE,
                             pi_precondstarttemp 			IN    NUMBER,
                             pi_rimdiameter 				IN    NUMBER,
                             pi_rimwidth 					IN    NUMBER,
                             pd_precondenddate 				IN    DATE,
                             pi_precondendtemp 				IN    NUMBER,
                             pi_inflationpressure 			IN    NUMBER,
                             pi_beforediameter 				IN    NUMBER,
                             pi_afterdiameter 				IN    NUMBER,
                             pi_beforeinflation 			IN    NUMBER,
                             pi_afterinflation 				IN    NUMBER,
                             pi_wheelposition 				IN    NUMBER,
                             pi_wheelnumber 				IN    NUMBER,
                             pi_finaltemp 					IN    NUMBER,
                             pi_finaldistance 				IN    NUMBER,
                             pi_finalinflation 				IN    NUMBER,
                             pd_postcondstartdate 			IN    DATE,
                             pd_postcondenddate 			IN    DATE,
                             pi_postcondendtemp 			IN    NUMBER,
                             ps_passyn 						IN    VARCHAR2,
                             pi_certificationtypeid 		IN    NUMBER,
                             pd_serialdate 					IN    DATE,
                             pi_precondtime 				IN    NUMBER,
                             pi_postcondtime  				IN    NUMBER,
                             pi_diametertestdrum 			IN    NUMBER,
                             pi_precondtemp 				IN    NUMBER,
                             pi_inflationpressurereadjusted	IN    NUMBER,
                             pi_circunferencebeforetest 	IN    NUMBER,
                             ps_resultpassfail 				IN    VARCHAR2,
                             pi_endurancehours 				IN    NUMBER,
                             ps_possiblefailuresfound 		IN    VARCHAR2,
                             pi_circunferenceaftertest 		IN    NUMBER,
                             pi_outerdiameterdiference 		IN    NUMBER,
                             pi_oddiferencetolerance 		IN    NUMBER,
                             ps_serienom 					IN    VARCHAR2,
                             ps_finaljudgement 				IN    VARCHAR2,
                             ps_approver 					IN    VARCHAR2,
                             ps_operatorname 				IN    VARCHAR2,
                             pi_certificateid 				IN    NUMBER,
                             ps_matl_num 					IN    VARCHAR2,
                             pn_lowInfstartinflation 		IN    NUMBER,
                             pn_lowInfendinflation 			IN    NUMBER,
                             pn_lowInfendtemp 				IN    NUMBER,
                             ps_operation 					IN    VARCHAR2,
                             ps_gtspec 						IN    VARCHAR2);
                            
	PROCEDURE ENDURANCEDETAIL_SAVE(pi_teststep 			IN NUMBER,
                                  pi_timeinmin 			IN NUMBER,
                                  pi_speed 				IN NUMBER,
                                  pi_totmiles 			IN NUMBER,
                                  pi_load 				IN NUMBER,
                                  pi_loadpercent 		IN NUMBER,
                                  pi_setinflation 		IN NUMBER,
                                  pi_ambtemp 			IN NUMBER,
                                  pi_infpressure 		IN NUMBER,
                                  pd_stepcompletiondate	IN ENDURANCEDTL.STEPCOMPLETIONDATE%Type,
                                  pi_enduranceid 		IN NUMBER);
                                  
	PROCEDURE TREADWEAR_SAVE(pi_treadwearid 		  OUT NUMBER,
                           ps_projectnumber  		IN    VARCHAR2,
                           pi_tirenumber 			IN    NUMBER,
                           ps_testspec  			IN    VARCHAR2,
                           pd_completiondate 		IN    DATE,
                           ps_dotserialnumber  		IN    VARCHAR2,
                           pi_lowestwearbar 		IN    NUMBER,
                           ps_passyn  				IN    VARCHAR2,
                           pi_certificationtypeid 	IN    NUMBER,
                           pd_serialdate 			IN    DATE,
                           ps_mfgwwyy    			IN    VARCHAR2,
                           ps_operatorname 			IN    VARCHAR2,
                           pi_indicatorsrequirement	IN    NUMBER,
                           pi_certificateid 		IN    NUMBER,
                           ps_matl_num 				IN    VARCHAR2,
                           ps_operation 			IN    VARCHAR2,
                           ps_gtspec 				IN    VARCHAR2);
                           
	PROCEDURE TREADWEARDETAIL_SAVE(pi_treadwearid 	IN NUMBER,
                                   pi_wearbarheight	IN TREADWEARDTL.WEARBARHEIGHT%TYPE,
                                   pi_iteration 	IN TREADWEARDTL.ITERATION%TYPE,
                                   ps_operatorname 	IN VARCHAR2);
                                 
	PROCEDURE PLUNGER_SAVE(pi_plungerid 			  OUT NUMBER,
                           ps_projectnumber 		IN    VARCHAR2,
                           pi_tirenumber 			IN    NUMBER,
                           ps_testspec 				IN    VARCHAR2,
                           pd_completiondate 		IN    DATE,
                           ps_dotserialnumber 		IN    VARCHAR2,
                           pi_avgbreakingenergy 	IN    NUMBER,
                           ps_passyn 				IN    VARCHAR2,
                           pi_certificationtypeid	IN    NUMBER,
                           pd_serialdate 			IN    DATE,
                           ps_mfgwwyy    			IN    VARCHAR2,
                           pi_minplunger 			IN    NUMBER,
                           ps_operatorname 			IN    VARCHAR2 ,
                           pi_certificateid 		IN    NUMBER,
                           ps_matl_num 				IN    VARCHAR2,
                           ps_operation 			IN    VARCHAR2,
                           ps_gtspec 				IN    VARCHAR2);
                            
	PROCEDURE PLUNGERDETAIL_SAVE(pi_breakingenergy	IN NUMBER,
                                 pi_plungerid  		IN NUMBER,
                                 pi_iteration 		IN NUMBER,
                                 ps_operatorname 	IN VARCHAR2);
                               
	PROCEDURE BEADUNSEAT_SAVE(pi_beadunseatid 			  OUT NUMBER,
                              ps_projectnumber 			IN    VARCHAR2,
                              pi_tirenumber 			IN    NUMBER,
                              ps_testspec 				IN    VARCHAR2,
                              pd_completiondate 		IN    DATE,
                              ps_dotserialnumber 		IN    VARCHAR2,
                              pi_lowestunseatvalue 		IN    NUMBER,
                              ps_passyn 				IN    VARCHAR2,
                              pi_certificationtypeid	IN    NUMBER,
                              pd_serialdate 			IN    DATE,
                              ps_mfgwwyy    			IN    VARCHAR2,
                              pi_minbeadunseat 			IN    NUMBER,
                              ps_testpassfail 			IN    VARCHAR2,
                              ps_operatorname   		IN    VARCHAR2,
                              pi_certificateid 			IN    NUMBER,
                              ps_matl_num 				IN    VARCHAR2,
                              ps_operation 				IN    VARCHAR2,                            
                              ps_gtspec 				IN    VARCHAR2);
                            
	PROCEDURE BEADUNSEATDETAIL_SAVE(pi_beadunseatid IN NUMBER,
                                    pi_unseatforce 	IN NUMBER,
                                    pi_iteration 	IN NUMBER,
                                    ps_operatorname	IN VARCHAR2);
                                  
	PROCEDURE HIGHSPEEDHDR_SAVE(pi_highspeedid          		  OUT NUMBER,
                                ps_projectnumber        		IN    VARCHAR2,
                                pi_tirenum              		IN    NUMBER,
                                ps_testspec             		IN    VARCHAR2,
                                pd_competiondate        		IN    DATE,
                                ps_dotserialnumber      		IN    VARCHAR2,
                                ps_mfgwwyy              		IN    VARCHAR2,
                                pd_precondstartdate     		IN    DATE,
                                pi_precondsarttemp      		IN    NUMBER,
                                pd_precondtime          		IN    HIGHSPEEDHDR.PRECONDTIME%TYPE,
                                pi_rimdiameter          		IN    HIGHSPEEDHDR.RIMDIAMETER%TYPE,
                                pi_rimwidth             		IN    HIGHSPEEDHDR.RIMWIDTH%TYPE,
                                pd_precondenddate       		IN    DATE,
                                pi_precondendtemp       		IN    NUMBER,
                                pi_inflationpressure    		IN    NUMBER,
                                pi_beforediameter       		IN    HIGHSPEEDHDR.BEFOREDIAMETER%TYPE,
                                pi_afterdiameter        		IN    HIGHSPEEDHDR.AFTERDIAMETER%TYPE,
                                pi_beforeinflation      		IN    NUMBER,
                                pi_afterinflation       		IN    NUMBER,
                                pi_wheelposition        		IN    NUMBER,
                                pi_wheelnumber          		IN    NUMBER,
                                pi_finaltemp            		IN    NUMBER,
                                pi_finaldistance        		IN    HIGHSPEEDHDR.FINALDISTANCE%TYPE,
                                pi_finalinflation       		IN    NUMBER ,
                                pd_postcondstartdate    		IN    DATE,
                                pd_postcondenddate      		IN    DATE,
                                pi_postcondendtemp      		IN    NUMBER ,
                                ps_passyn               		IN    VARCHAR2,
                                pd_serialdate           		IN    DATE,
                                pi_postcondtime         		IN    HIGHSPEEDHDR.POSTCONDTIME%TYPE,
                                pi_certificationtypeid  		IN    NUMBER,
                                pi_diametertestdrum     		IN    NUMBER,
                                pi_precondtemp          		IN    NUMBER,
                                pi_inflationpressurereadjusted	IN    NUMBER,
                                pi_circunferencebeforetest     	IN    NUMBER,
                                pi_wheelspeedrpm        		IN    NUMBER,
                                pi_wheelspeedkmh        		IN    NUMBER,
                                pi_circunferenceaftertest 		IN    NUMBER,
                                pi_oddiference            		IN    NUMBER,
                                pi_oddiferencetolerance   		IN    NUMBER,
                                ps_serienom               		IN    VARCHAR2,
                                ps_finaljudgement         		IN    VARCHAR2,
                                ps_approver               		IN    VARCHAR2,
                                pi_passatkmh              		IN    NUMBER,
                                ps_speedttestpassfail     		IN    VARCHAR2,
                                pi_speedtotaltime         		IN    NUMBER,
                                pi_maxspeed     				IN    NUMBER,
                                pi_maxload      				IN    NUMBER,
                                ps_operatorname 				IN    VARCHAR2,
                                pi_certificateid 				IN    NUMBER,
                                ps_matl_num  					IN    VARCHAR2,
                                ps_operation 					IN    VARCHAR2,
                                ps_gtspec 						IN    VARCHAR2);
                                
	PROCEDURE HIGHSPEEDDETAIL_SAVE(pi_highspeedid 			IN NUMBER,
                                   pi_teststep 				IN NUMBER,
                                   pi_timeinmin 			IN NUMBER,
                                   pi_speed 				IN HIGHSPEEDDTL.SPEED%TYPE,
                                   pi_totmiles 				IN HIGHSPEEDDTL.TOTMILES%TYPE,
                                   pi_load 					IN HIGHSPEEDDTL.LOAD%TYPE,
                                   pi_loadpercent 			IN NUMBER,
                                   pi_setinflation 			IN NUMBER,
                                   pi_ambtemp 				IN NUMBER,
                                   pi_infpressure 			IN NUMBER,
                                   pd_stepcompletiondate	IN HIGHSPEEDDTL.STEPCOMPLETIONDATE%TYPE,
                                   ps_operatorid 			IN VARCHAR2);
                                    
	PROCEDURE HIGHSPEED_SPEEDTESTDETAIL_SAVE(pi_iteration 		IN NUMBER,
                                             pd_time 			IN DATE,
                                             pi_speed 			IN NUMBER,
                                             pi_highspeedid 	IN NUMBER,
                                             ps_operatorname	IN VARCHAR2);
                                            
	PROCEDURE SOUNDHDR_SAVE(ps_userid                     IN VARCHAR2,
                            pi_soundid                    OUT NUMBER,
                            ps_projectnumber              IN VARCHAR2,
                            pi_tirenumber                 IN NUMBER,
                            ps_testspec                   IN VARCHAR2,
                            ps_testreportnumber           IN VARCHAR2,
                            ps_manufactureandbrand        IN VARCHAR2,
                            ps_tireclass                  IN VARCHAR2,
                            ps_categoryofuse              IN VARCHAR2,
                            pd_dateoftest                 IN DATE,
                            ps_testvehicule               IN VARCHAR2,
                            ps_testvehiculewheelbase      IN VARCHAR2,
                            ps_locationoftesttrack        IN VARCHAR2,
                            pd_datetrackcertiftoiso       IN DATE,
                            ps_tiresizedesignation        IN VARCHAR2,
                            ps_tireservicedescription     IN VARCHAR2,
                            ps_testmass_frontl            IN VARCHAR2,
                            ps_testmass_frontr            IN VARCHAR2,
                            ps_testmass_rearl             IN VARCHAR2,
                            ps_testmass_rearr             IN VARCHAR2,
                            ps_tireloadindex_frontl       IN VARCHAR2,
                            ps_tireloadindex_frontr       IN VARCHAR2,
                            ps_tireloadindex_rearl        IN VARCHAR2,
                            ps_tireloadindex_rearr        IN VARCHAR2,
                            ps_inflationpressureco_frontl IN VARCHAR2,
                            ps_inflationpressureco_frontr IN VARCHAR2,
                            ps_inflationpressureco_rearl  IN VARCHAR2,
                            ps_inflationpressureco_rearr  IN VARCHAR2,
                            ps_testrimwidthcode           IN VARCHAR2,
                            ps_tempmeasuresensortype      IN VARCHAR2,
                            pi_certificationtypeid        IN NUMBER,
                            pi_skuid                      IN NUMBER,
                            ps_referenceinflationpressure IN VARCHAR2,
                            pi_certificateid              IN NUMBER,
                            ps_operation                  IN VARCHAR2,
                            ps_mfgwwyy                    IN VARCHAR2,
                            ps_gtspec                     IN VARCHAR2);
                              
	PROCEDURE SOUNDDETAIL_SAVE(ps_userid 					IN VARCHAR2,
                               pi_iteration 				IN NUMBER,
                               ps_testspeed  				IN VARCHAR2,
                               ps_directionofrun  			IN VARCHAR2,
                               ps_soundlevelleft  			IN VARCHAR2,
                               ps_soundlevelright  			IN VARCHAR2,
                               ps_airtemp  					IN VARCHAR2,
                               ps_tracktemp 				IN VARCHAR2,
                               ps_soundlevelleft_tempcor 	IN VARCHAR2,
                               ps_soundlevelright_tempcor	IN VARCHAR2,
                               pi_soundid 					IN NUMBER);
                              
	PROCEDURE WETGRIPHDR_SAVE(ps_userid 					IN VARCHAR2,
                              pi_wetgripid  				OUT NUMBER,
                              ps_projectnumber  			IN VARCHAR2,
                              pi_tirenumber  				IN VARCHAR2,
                              ps_testspec  					IN VARCHAR2,
                              pd_dateoftest  				IN DATE,
                              ps_testvehicle  				IN VARCHAR2,
                              ps_locationoftesttrack  		IN VARCHAR2,
                              ps_testtrackcharacteristics  	IN VARCHAR2,
                              ps_issueby  					IN VARCHAR2,
                              ps_methodofcertification  	IN VARCHAR2,
                              ps_testtiredetails  			IN VARCHAR2,
                              ps_tiresizeandservicedesc  	IN VARCHAR2,
                              ps_tirebrandandtradedesc  	IN VARCHAR2,
                              ps_referenceinflationpressure	IN VARCHAR2,
                              ps_testrimwithcode  			IN VARCHAR2,
                              ps_tempmeasuresensortype  	IN VARCHAR2,
                              ps_identificationsrtt  		IN VARCHAR2,
                              ps_testtireload_srtt  		IN VARCHAR2,
                              ps_testtireload_candidate  	IN VARCHAR2,
                              ps_testtireload_control  		IN VARCHAR2,
                              ps_waterdepth_srtt  			IN VARCHAR2,
                              ps_waterdepth_candidate  		IN VARCHAR2,
                              ps_waterdepth_control  		IN VARCHAR2,
                              ps_wettedtracktempavg  		IN VARCHAR2,
                              pi_certificationtypeid  		IN NUMBER,
                              pi_skuid  					IN NUMBER,
                              pi_certificateid 				IN NUMBER,
                              ps_operation  				IN VARCHAR2,
                              ps_mfgwwyy    				IN VARCHAR2,
                              ps_gtspec     				IN VARCHAR2);
                               
	PROCEDURE WETGRIPDETAIL_SAVE(ps_userid 						IN VARCHAR2,
                                 pi_iteration  					IN NUMBER,
                                 ps_testspeed  					IN VARCHAR2,
                                 ps_directionofrun  			IN VARCHAR2,
                                 ps_srtt  						IN VARCHAR2,
                                 ps_candidatetire  				IN VARCHAR2,
                                 ps_peakbreakforcecoeficient	IN VARCHAR2,
                                 ps_meanfullydevdeceleration  	IN VARCHAR2,
                                 ps_wetgripindex  				IN VARCHAR2,
                                 ps_comments  					IN VARCHAR2,
                                 pi_wetgripid  					IN NUMBER);
             
	PROCEDURE GETTIRETYPES(pc_tiretypes	OUT retcursor);
                         
	FUNCTION GETMEASUREID(ps_certificatenumber		IN VARCHAR2,
						  pi_certificationtypeid	IN NUMBER, 
						  pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER;
	
	FUNCTION GETPLUNGERID(ps_certificatenumber		IN VARCHAR2,
						  pi_certificationtypeid	IN NUMBER, 
						  pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER;
	
	FUNCTION GETTREADWEARID(ps_certificatenumber	IN VARCHAR2,
							pi_certificationtypeid	IN NUMBER, 
							pi_certificatenumberid 	IN NUMBER ) 
	RETURN NUMBER;
	
	FUNCTION GETENDURANCEID(ps_certificatenumber	IN VARCHAR2,
							pi_certificationtypeid 	IN NUMBER, 
							pi_certificatenumberid 	IN NUMBER ) 
	RETURN NUMBER;
	
	FUNCTION GETBEADUNSEATID(ps_certificatenumber	IN VARCHAR2,
							 pi_certificationtypeid IN NUMBER, 
							 pi_certificatenumberid IN NUMBER) 
	RETURN NUMBER;
	
	FUNCTION GETHIGHSPEEDID(ps_certificatenumber	IN VARCHAR2,
							pi_certificationtypeid 	IN NUMBER, 
							pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER;
	
	FUNCTION GETWETGRIPHDRID(ps_certificatenumber	IN VARCHAR2,
							 pi_certificationtypeid	IN NUMBER,
							 pi_skuid 				IN NUMBER, 
							 pi_certificatenumberid IN NUMBER) 
	RETURN NUMBER;
	
	FUNCTION GETSOUNDHDRID(ps_certificatenumber		IN VARCHAR2,
						   pi_certificationtypeid 	IN NUMBER, 
						   pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER;
	
	FUNCTION CHECKIFPRODUCTEXISTS(ps_matl_num	IN VARCHAR2,
								  pi_skuid 		IN NUMBER) 
	RETURN VARCHAR2;
	
	FUNCTION CHECKIFBEADUNSEATEXISTS(pi_certificateid		IN NUMBER,
									 pi_certificationtypeid	IN NUMBER) 
	RETURN VARCHAR2;
	
	FUNCTION CHECKIFENDURANCEEXISTS(pi_certificateid		IN NUMBER,
									pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2;
	
	FUNCTION CHECKIFHIGHSPEEDEXISTS(pi_certificateid		IN NUMBER,
									pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2;
	
	FUNCTION CHECKIFTREADWEAREXISTS(pi_certificateid		IN NUMBER,
									pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2;
	
	FUNCTION CHECKIFPLUNGEREXISTS(pi_certificateid			IN NUMBER, 
								  pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2;
  
	FUNCTION CHECKIFMEASUREEXISTS(pi_certificateid			IN NUMBER,
								  pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2;
 
	FUNCTION CHECKIFSOUNDEXIXTS(pi_certificateid		IN NUMBER, 
								pi_certificationtypeid 	IN NUMBER)  
	RETURN VARCHAR2;
  
	FUNCTION CHECKIFWETGRIPEXIXTS(pi_certificateid			IN NUMBER, 
								  pi_certificationtypeid 	IN NUMBER)  
	RETURN VARCHAR2;
  
END TESTRESULTS_CRUD;
/

CREATE OR REPLACE PACKAGE BODY ICS_PROCS.TESTRESULTS_CRUD 
AS
	/******************************************************************************
	NAME:       TESTRESULTS_CRUD
	PURPOSE:
	REVISIONS:
	Ver        Date        Author           Description
	---------  ----------  ---------------  ------------------------------------
	1.0        
	1.1        10/02/2012    Harini         1.Replaced ps_SKU with ps_matl_num in
												GetProductData,Product_Save,Measure_Save,
												Endurance_Save,TREADWEAR_SAVE,PLUNGER_Save,
												BeadUnseat_Save,HighSpeedHdr_Save,
												CheckIfProductExists procedures   
	1.2       10/18/2012    Harini          2.Added new input parameter ps_operation in
											Measure_Save,Endurance_Save,TREADWEAR_SAVE,
											PLUNGER_Save,BeadUnseat_Save,HighSpeedHdr_Save,
											SoundHDR_Save,WetGripHDR_Save procedures
	1.3       10/30/2012    Harini         3.Included NO_DATA_FOUND exception block when no 
												SKU available for given Matl_Num and assign empty 
												for ls_SKU parameter in Measure_Save,Endurance_Save,
												TREADWEAR_SAVE,PLUNGER_Save,BeadUnseat_Save,
												HighSpeedHdr_Save procedures
	1.4       11/04/2013    Harini          1.As per IDEA2706, Modified Product_Save procedure by adding 
											ps_SEVEREWEATHERIND and ps_mfgwwyy parameters
											2. Added SEVEREWEATHERIND and MFGWWYY fields in the select list
											of GetProductData procedure
											3.Added ps_mfgwwyy,ps_gtspec parameters to Measure_Save,
											TREADWEAR_SAVE,PLUNGER_Save,BeadUnseat_Save,SoundHDR_Save,
											WetGripHDR_Save procedures.
											4.Added ps_gtspec parameter to Endurance_Save and HighSpeedHdr_Save
											procedures
		1.5       11/06/2013     Ajit           1. As per IDEA 2706,Added GetTireTypes procedure
		1.6       11/11/2013    Harini        1. Added TiretypeId for updating in Product table in Product_Save procedure
	******************************************************************************/
	PROCEDURE GETPRODUCTDATA(pc_retcursor	OUT   retcursor,
							 ps_matl_num 	   IN VARCHAR2,
							 pi_skuid 		   IN NUMBER)
    AS
    /******************************************************************************
     NAME:       GetProductData
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num,
                                             BrandCode with Brand and Brand_Line,
                                             nprid instead of psn,Tpn instead of PPn.
                                             Added Lpad(matl_num) in the select list
     1.2        11/04/2013    Harini        1.Added SEVEREWEATHERIND and MFGWWYY fields in 
                                            the select list
    ******************************************************************************/
	--Exception variables
    li_parametersarenull EXCEPTION;
	
    -- link the exception to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parameterisinvalid EXCEPTION;
	
    -- link the exception to the error number
    PRAGMA EXCEPTION_INIT(li_parameterisinvalid, -20006);
	
    --varible
    ls_machineid 	VARCHAR2(50)	:= NULL;
    ls_operatorid	VARCHAR2(50)	:= 'ICSDEV';
    ls_errormsg 	VARCHAR2(4000)	:= NULL;
	
	BEGIN
	
        IF ps_matl_num IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		
        IF ps_matl_num = '' THEN
              RAISE li_parameterisinvalid;
        END IF;
		
		-- Use Brand and Brand_Line instead of Brandcode,nprid instead of psn,ppn instead of psn
        OPEN pc_retcursor FOR
			SELECT SKUID,
				   SKU,
				   LPAD(MATL_NUM, 18, 0) AS MATL_NUM, -- AS per PRJ3617,Matl_Num is added in select list
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
				   SEVEREWEATHERIND,
				   RIMDIAMETER,
				   SERIALDATE,
				   MFGWWYY,
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
                   TPN
			FROM PRODUCT
			WHERE MATL_NUM = LPAD(ps_matl_num, 18, 0) -- Replaced sku with Matl_num in where condition 
				AND SKUID = pi_skuid;
	
	EXCEPTION
        
		WHEN li_parametersarenull THEN
            ls_errormsg:= SQLERRM || '- GetProductData.  There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetProductData',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, SQLERRM);
        
		WHEN li_parameterisinvalid THEN
            ls_errormsg:= SQLERRM || '- GetProductData.  There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetProductData',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, SQLERRM);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetProductData. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetProductData',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, SQLERRM);
	
	END GETPRODUCTDATA;
  
	PROCEDURE GETTESTRESULTS(pc_measurecursor 		  OUT retcursor,
                             pc_measuredetailcursor   OUT retcursor,
                             pc_plungerhdrcursor      OUT retcursor,
                             pc_plungerdtlcursor      OUT retcursor,
                             pc_treadwearhdrcursor    OUT retcursor,
                             pc_treadweardtlcursor    OUT retcursor,
                             pc_beadunseathdrcursor   OUT retcursor,
                             pc_beadunseatdtlcursor   OUT retcursor,
                             pc_endurancehdrcursor 	  OUT retcursor,
                             pc_endurancedtlcursor 	  OUT retcursor,
                             pc_highspeedcursor  	  OUT retcursor,
                             pc_highspeeddetailcursor OUT retcursor,
                             pc_hsspeedtestdetail     OUT retcursor,
                             pc_soundhdrcursor  	  OUT retcursor,
                             pc_sounddetailcursor  	  OUT retcursor,
                             pc_wetgriphdrcursor  	  OUT retcursor,
                             pc_wetgripdetailcursor   OUT retcursor,
                             pi_certificationtypeid IN    NUMBER,
                             pi_skuid 				IN    NUMBER,
                             ps_certificatenumber 	IN    VARCHAR,
                             pi_certificatenumberid	IN    NUMBER) 
	AS
    --Exception variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);	
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_skuexists 		VARCHAR2(1) := NULL;
    ls_measureexists	VARCHAR2(1) := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_lastimarkcertnum CERTIFICATE.CERTIFICATENUMBER%type := NULL;
    li_measureid  MEASUREHDR.MEASUREID%TYPE := NULL;
    li_certificateid NUMBER := 0;
  
	BEGIN
        
		IF pi_skuid IS NULL OR pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
			RAISE li_parametersarenull;
		END IF;
      
		IF pi_skuid <= 0 OR pi_certificationtypeid <= 0 OR ps_certificatenumber = '' THEN
              RAISE li_parametersareinvalid;
		END IF;
		
		/*
		Since Imark has one and only one certificate number,
		I am getting the latest one that has the I033 prefix and
		using it to get the information regarding the tests results.
		*/
		IF pi_certificationtypeid = 4 THEN
			li_certificateid:= ICS_COMMON_FUNCTIONS.GETLATESTIMARKCERTIFICATEID();
			ls_lastimarkcertnum := ps_certificatenumber ; --'I033';
			
			--Gets the Measure Table                 
            TESTRESULTS_CRUD.GETMEASURE(pc_measurecursor 		=> pc_measurecursor,
                                        pc_measuredetailcursor 	=> pc_measuredetailcursor,
                                        pi_skuid 				=> pi_skuid,
                                        ps_certificatenumber 	=> ls_lastimarkcertnum,
                                        pi_certificationtypeid	=> pi_certificationtypeid,
                                        pi_certificatenumberid 	=> li_certificateid);
			
			TESTRESULTS_CRUD.GETPLUNGER(pc_plungerhdrcursor 	=> pc_plungerhdrcursor,
                                        pc_plungerdtlcursor 	=> pc_plungerdtlcursor,
                                        pi_skuid 				=> pi_skuid,
                                        ps_certificatenumber 	=> ls_lastimarkcertnum,
                                        pi_certificationtypeid	=> pi_certificationtypeid,
                                        pi_certificatenumberid 	=> li_certificateid);
			
			TESTRESULTS_CRUD.GETTREADWEAR(pc_treadwearhdrcursor 	=> pc_treadwearhdrcursor,
										  pc_treadweardtlcursor 	=> pc_treadweardtlcursor,
										  pi_skuid 					=> pi_skuid,
										  ps_certificatenumber 		=> ls_lastimarkcertnum,
										  pi_certificationtypeid	=> pi_certificationtypeid,
										  pi_certificatenumberid 	=> li_certificateid);
            
			TESTRESULTS_CRUD.GETBEADUNSEAT(pc_beadunseathdrcursor 	=> pc_beadunseathdrcursor,
										   pc_beadunseatdtlcursor 	=> pc_beadunseatdtlcursor,
										   pi_skuid 				=> pi_skuid,
										   ps_certificatenumber 	=> ls_lastimarkcertnum,
										   pi_certificationtypeid	=> pi_certificationtypeid,
										   pi_certificatenumberid 	=> li_certificateid);
			
			TESTRESULTS_CRUD.GETENDURANCE(pc_endurancehdrcursor 	=> pc_endurancehdrcursor,
										  pc_endurancedtlcursor 	=> pc_endurancedtlcursor,
										  pi_skuid 					=> pi_skuid,
										  ps_certificatenumber 		=> ps_certificatenumber,
										  pi_certificationtypeid	=> pi_certificationtypeid,
										  pi_certificatenumberid 	=> li_certificateid);
			
			TESTRESULTS_CRUD.GETHIGHSPEED(pc_highspeedcursor 		=> pc_highspeedcursor,
										  pc_highspeeddetailcursor	=> pc_highspeeddetailcursor,
										  pc_hsspeedtestdetail 		=> pc_hsspeedtestdetail,
										  pi_skuid 					=> pi_skuid,
										  ps_certificatenumber 		=> ps_certificatenumber,
										  pi_certificationtypeid 	=> pi_certificationtypeid,
										  pi_certificatenumberid 	=> li_certificateid);
			
			TESTRESULTS_CRUD.GETSOUND(pc_soundhdrcursor 		=> pc_soundhdrcursor,
                                      pc_sounddetailcursor 		=> pc_sounddetailcursor,
                                      pi_skuid 					=> pi_skuid,
                                      ps_certificatenumber 		=> ps_certificatenumber,
                                      pi_certificationtypeid 	=> pi_certificationtypeid,
                                      pi_certificatenumberid 	=> li_certificateid);
			
			TESTRESULTS_CRUD.GETWETGRIP(pc_wetgriphdrcursor 	=> pc_wetgriphdrcursor,
										pc_wetgripdetailcursor	=> pc_wetgripdetailcursor,
										pi_skuid 				=> pi_skuid,
										ps_certificatenumber 	=> ps_certificatenumber,
										pi_certificationtypeid 	=> pi_certificationtypeid,
										pi_certificatenumberid 	=> li_certificateid);
		ELSE
            --Gets the Measure Table
            TESTRESULTS_CRUD.GETMEASURE(pc_measurecursor 		=> pc_measurecursor,
                                      pc_measuredetailcursor 	=> pc_measuredetailcursor,
                                      pi_skuid 					=> pi_skuid,
                                      ps_certificatenumber 		=> ps_certificatenumber,
                                      pi_certificationtypeid 	=> pi_certificationtypeid,
                                      pi_certificatenumberid 	=> pi_certificatenumberid);
			
			TESTRESULTS_CRUD.GETPLUNGER(pc_plungerhdrcursor 	=> pc_plungerhdrcursor,
                                        pc_plungerdtlcursor 	=> pc_plungerdtlcursor,
                                        pi_skuid 				=> pi_skuid,
                                        ps_certificatenumber 	=> ps_certificatenumber,
                                        pi_certificationtypeid	=> pi_certificationtypeid,
                                        pi_certificatenumberid 	=> pi_certificatenumberid);
			
			TESTRESULTS_CRUD.GETTREADWEAR(pc_treadwearhdrcursor => pc_treadwearhdrcursor,
                                      pc_treadweardtlcursor 	=> pc_treadweardtlcursor,
                                      pi_skuid 					=> pi_skuid,
                                      ps_certificatenumber 		=> ps_certificatenumber,
                                      pi_certificationtypeid	=> pi_certificationtypeid,
                                      pi_certificatenumberid 	=> pi_certificatenumberid);
			
			TESTRESULTS_CRUD.GETBEADUNSEAT(pc_beadunseathdrcursor 	=> pc_beadunseathdrcursor,
										   pc_beadunseatdtlcursor 	=> pc_beadunseatdtlcursor,
										   pi_skuid 				=> pi_skuid,
										   ps_certificatenumber 	=> ps_certificatenumber,
										   pi_certificationtypeid	=> pi_certificationtypeid,
										   pi_certificatenumberid 	=> pi_certificatenumberid);
			
			TESTRESULTS_CRUD.GETENDURANCE(pc_endurancehdrcursor 	=> pc_endurancehdrcursor,
										  pc_endurancedtlcursor 	=> pc_endurancedtlcursor,
										  pi_skuid 					=> pi_skuid,
										  ps_certificatenumber 		=> ps_certificatenumber,
										  pi_certificationtypeid 	=> pi_certificationtypeid,
										  pi_certificatenumberid	=> pi_certificatenumberid);
            
			TESTRESULTS_CRUD.GETHIGHSPEED(pc_highspeedcursor 		=> pc_highspeedcursor,
										  pc_highspeeddetailcursor	=> pc_highspeeddetailcursor,
										  pc_hsspeedtestdetail 		=> pc_hsspeedtestdetail,
										  pi_skuid 					=> pi_skuid,
										  ps_certificatenumber 		=> ps_certificatenumber,
										  pi_certificationtypeid 	=> pi_certificationtypeid,
										  pi_certificatenumberid 	=> pi_certificatenumberid);
            
			TESTRESULTS_CRUD.GETSOUND(pc_soundhdrcursor 		=> pc_soundhdrcursor,
                                      pc_sounddetailcursor 		=> pc_sounddetailcursor,
                                      pi_skuid 					=> pi_skuid,
                                      ps_certificatenumber 		=> ps_certificatenumber,
                                      pi_certificationtypeid 	=> pi_certificationtypeid,
                                      pi_certificatenumberid	=> pi_certificatenumberid);
            
			TESTRESULTS_CRUD.GETWETGRIP(pc_wetgriphdrcursor 	=> pc_wetgriphdrcursor,
                                        pc_wetgripdetailcursor	=> pc_wetgripdetailcursor,
                                        pi_skuid 				=> pi_skuid,
                                        ps_certificatenumber 	=> ps_certificatenumber,
                                        pi_certificationtypeid 	=> pi_certificationtypeid,
                                        pi_certificatenumberid 	=> pi_certificatenumberid);
		END IF;
     
	EXCEPTION
        WHEN li_parametersarenull THEN
            ls_errormsg := '- GetTestresults. There is at least one parameters null.'  ;
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTestresults',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- GetTestresults. There is at least one parameters invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTestresults',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := '- GetTestresults. An error have ocurred.(when others)' || SQLERRM;
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTestresults',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END GETTESTRESULTS;
  
	PROCEDURE GETMEASURE(pc_measurecursor 		  OUT retcursor,
                         pc_measuredetailcursor   OUT retcursor,
                         pi_skuid 				IN 	  NUMBER,
                         ps_certificatenumber 	IN    VARCHAR2,
                         pi_certificationtypeid	IN    NUMBER,
                         pi_certificatenumberid IN    NUMBER) 
	AS
	/******************************************************************************
     NAME:       GetMeasure
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012    Harini         1.Added Lpad(matl_num) in the select list
     1.2        10/18/2012   Harini         1.Added Operation in the select list
     1.3        11/04/2013   Harini         1.Added MFGWWYY and GTSpec in the select list
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50):= 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_measureid MEASUREHDR.MEASUREID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
    ls_measureexists VARCHAR2(1) := NULL;
    
	BEGIN
        
		IF pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
        
		IF pi_certificationtypeid <=0 OR ps_certificatenumber = '' THEN
            RAISE li_parametersareinvalid;
        END IF;
            
		--Return Measure records
		Open pc_measurecursor FOR
			SELECT MEASUREID AS MEA_ID,
                   PROJECTNUMBER AS PROJECTNUM,
                   TIRENUMBER AS TIRENUM,
                   TESTSPEC AS TESTSPEC,
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
                   M.MFGWWYY,
                   ENDTIME,
                   ACTSIZEFACTOR,
                   M.CERTIFICATIONTYPEID,
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
                   M.SKU,
                   LPAD(M.MATL_NUM, 18, 0) AS MATL_NUM,   -- As per PRJ3617,added Matl_Num in select list
                   M.OPERNUM AS OPERATION,         -- As per PRJ3617,added Operation in select list
                   M.GTSPEC
            FROM CERTIFICATE CE 
				INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
                   
			--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
			li_measureid := TESTRESULTS_CRUD.GETMEASUREID(ps_certificatenumber 		=> ps_certificatenumber,
														  pi_certificationtypeid	=> pi_certificationtypeid,
														  pi_certificatenumberid 	=> pi_certificatenumberid);
                      
		--Return the Measure Detail records
        Open pc_measuredetailcursor FOR
			SELECT SECTIONWIDTH, 
				   OVERALLWIDTH, 
				   MEASUREID AS MEA_ID,
				   ITERATION
			FROM MEASUREDTL MD
			WHERE MD.MEASUREID = li_measureid
			ORDER BY ITERATION ASC;
    
	EXCEPTION
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- GetMeasure. There is at least one parameters null.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetMeasure',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- GetMeasure.  There is at least one parameters invalid.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetMeasure',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetMeasure.  An error have ocurred.(when others)';
               
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetMeasure',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
                 
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
			
    END GETMEASURE;
    
	PROCEDURE GETPLUNGER(pc_plungerhdrcursor 	  OUT retcursor,
                         pc_plungerdtlcursor 	  OUT retcursor,
                         pi_skuid 				IN    NUMBER,
                         ps_certificatenumber 	IN    VARCHAR2,
                         pi_certificationtypeid	IN    NUMBER,
                         pi_certificatenumberid IN    NUMBER) 
	AS
	/******************************************************************************
     NAME:       GetPlunger
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Added Lpad(matl_num) in the select list
     1.2        10/18/2012    Harini         1.Added Operation in the select list
     1.3        11/04/2013    Harini         1.Added MFGWWYY and GTSpec in the select list
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_plungerid PLUNGERHDR.PLUNGERID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
    ls_measureexists VARCHAR2(1) := NULL;
	
    BEGIN
	
        IF pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		
        IF pi_certificationtypeid <=0 OR ps_certificatenumber = '' THEN
            RAISE li_parametersareinvalid;
        END IF;
		
		--Return Measure records
		OPEN pc_plungerhdrcursor FOR
			SELECT PLUNGERID AS PLG_ID,
                   PROJECTNUMBER AS PROJECTNUM,
                   TIRENUMBER AS TIRENUM,
                   TESTSPEC AS TESTSPEC,
                   COMPLETIONDATE,
                   DOTSERIALNUMBER,
                   AVGBREAKINGENERGY,
                   PASSYN,
                   P.CERTIFICATIONTYPEID,
                   CERTIFICATENUMBER,
                   SERIALDATE,
                   P.MFGWWYY,
                   MINPLUNGER,
                   P.SKU,
                   LPAD(P.MATL_NUM, 18, 0) AS MATL_NUM,   -- As per PRJ3617,added Matl_Num in select list
                   P.OPERNUM AS OPERATION,         -- As per PRJ3617,added Operation in select list
                   P.GTSPEC
            FROM CERTIFICATE CE
				INNER JOIN PLUNGERHDR P ON CE.CERTIFICATEID = P.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
			WHERE P.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
                   
			--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
            li_plungerid := TESTRESULTS_CRUD.GETPLUNGERID(ps_certificatenumber 		=> ps_certificatenumber,
														  pi_certificationtypeid 	=> pi_certificationtypeid,
														  pi_certificatenumberid 	=> pi_certificatenumberid);
                      
		--Return the Measure Detail records
		Open pc_plungerdtlcursor FOR
			SELECT PLUNGERID AS PLG_ID,
				   BREAKINGENERGY,
				   ITERATION
			FROM PLUNGERDTL p
			WHERE p.PLUNGERID = li_plungerid
			ORDER BY ITERATION ASC;
			
    EXCEPTION
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- GetPlunger. There is at least one parameters null.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetPlunger',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- GetPlunger.  There is at least one parameters invalid.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetPlunger',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetPlunger. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetPlunger',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
    
	END GETPLUNGER;
    
	PROCEDURE GETTREADWEAR(pc_treadwearhdrcursor      OUT retcursor,
                           pc_treadweardtlcursor      OUT retcursor,
                           pi_skuid 				IN    NUMBER,
                           ps_certificatenumber 	IN    VARCHAR2,
                           pi_certificationtypeid 	IN    NUMBER,
                           pi_certificatenumberid 	IN    NUMBER) 
	AS
	/******************************************************************************
     NAME:       GetTreadWear
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Added Lpad(matl_num) in the select list
     1.2        10/18/2012    Harini         1.Added Operation in the select list
     1.3        11/04/2013    Harini         1.Added MFGWWYY and GTSpec in the select list
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_plungerid PLUNGERHDR.PLUNGERID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
    ls_measureexists VARCHAR2(1) := NULL;
	
    BEGIN
	
        IF pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		
        IF pi_certificationtypeid <=0  OR ps_certificatenumber = '' THEN
            RAISE li_parametersareinvalid;
        END IF;
		  
		--Return Measure records
		Open pc_treadwearhdrcursor FOR
			SELECT TREADWEARID AS TW_ID,
                   PROJECTNUMBER AS PROJECTNUM,
                   TIRENUMBER AS TIRENUM,
                   TESTSPEC AS TESTSPEC,
                   COMPLETIONDATE,
                   DOTSERIALNUMBER,
                   LOWESTWEARBAR,
                   PASSYN,
                   T.CERTIFICATIONTYPEID,
                   CERTIFICATENUMBER,
                   SERIALDATE,
                   T.MFGWWYY,
                   INDICATORSREQUIREMENT,
                   T.SKU,
                   LPAD(T.MATL_NUM, 18, 0) AS MATL_NUM,  -- As per PRJ3617,added Matl_Num in select list 
                   T.OPERNUM AS OPERATION,         -- As per PRJ3617,added Operation in select list
                   T. GTSPEC
			FROM CERTIFICATE CE
				INNER JOIN TREADWEARHDR T ON CE.CERTIFICATEID = T.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = T.CERTIFICATIONTYPEID
			WHERE T.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
                   
			--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
			li_plungerid := TESTRESULTS_CRUD.GETTREADWEARID(ps_certificatenumber 	=> ps_certificatenumber,
															pi_certificationtypeid 	=> pi_certificationtypeid,
															pi_certificatenumberid 	=> pi_certificatenumberid);
                      
		--Return the Measure Detail records
        Open pc_treadweardtlcursor FOR
			SELECT TREADWEARID AS TW_ID, 
				   WEARBARHEIGHT,
				   ITERATION
			FROM TREADWEARDTL TD
			WHERE TD.TREADWEARID = li_plungerid
			ORDER BY ITERATION ASC;
    
	EXCEPTION
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '-GetTreadWear. There is at least one parameters null.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTreadWear',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '-GetTreadWear.  There is at least one parameters invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTreadWear',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '-GetTreadWear.  An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTreadWear',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
    END GETTREADWEAR;
    
	PROCEDURE GETBEADUNSEAT(pc_beadunseathdrcursor    OUT retcursor,
                            pc_beadunseatdtlcursor    OUT retcursor,
                            pi_skuid 				IN    NUMBER,
                            ps_certificatenumber 	IN    VARCHAR2,
                            pi_certificationtypeid 	IN    NUMBER,
                            pi_certificatenumberid 	IN    NUMBER) 
	AS
    /******************************************************************************
     NAME:       GetBeadUnseat
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Added Lpad(matl_num) in the select list
     1.2        10/18/2012    Harini         1.Added Operation in the select list
     1.3        11/04/2013    Harini         1.Added MFGWWYY and GTSpec in the select list
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_beadunseatid BEADUNSEATHDR.BEADUNSEATID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
    ls_measureexists VARCHAR2(1) := NULL;
    
	BEGIN
	
        IF pi_skuid IS NULL OR pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		
        IF pi_skuid <=0 OR pi_certificationtypeid <=0 OR ps_certificatenumber = '' THEN
            RAISE li_parametersareinvalid;
        END IF;
		  
		--Return Measure records
		Open pc_beadunseathdrcursor FOR
			SELECT BEADUNSEATID AS BU_ID,
                   PROJECTNUMBER AS PROJECTNUM,
                   TIRENUMBER AS TIRENUM,
                   TESTSPEC AS TESTSPEC,
                   COMPLETIONDATE,
                   DOTSERIALNUMBER,
                   LOWESTUNSEATVALUE,
                   PASSYN,
                   BS.CERTIFICATIONTYPEID,
                   CERTIFICATENUMBER,
                   SERIALDATE,
                   BS.MFGWWYY,
                   BS.SKU,
                   LPAD(BS.MATL_NUM,18,0) MATL_NUM,  -- As per PRJ3617,added Matl_Num in select list
                   BS.OPERNUM AS OPERATION,  -- As per PRJ3617,added Operation in select list
                   BS.GTSPEC
            FROM CERTIFICATE CE
				INNER JOIN BEADUNSEATHDR BS ON CE.CERTIFICATEID = BS.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = BS.CERTIFICATIONTYPEID
			WHERE BS.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
                   
			--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
            li_beadunseatid := TESTRESULTS_CRUD.GETBEADUNSEATID(ps_certificatenumber 	=> ps_certificatenumber,
																pi_certificationtypeid	=> pi_certificationtypeid,
																pi_certificatenumberid 	=> pi_certificatenumberid);
		
		--Return the Measure Detail records
        OPEN pc_beadunseatdtlcursor FOR
			SELECT BEADUNSEATID AS BU_ID, 
				   UNSEATFORCE,
				   ITERATION
			FROM BEADUNSEATDTL BS
			WHERE bs.BEADUNSEATID = li_beadunseatid
			ORDER BY ITERATION ASC;
    
	EXCEPTION
        when li_parametersarenull then
            ls_errormsg := SQLERRM || '- GetBeadUnseat. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetBeadUnseat',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		when li_parametersareinvalid then
            ls_errormsg := SQLERRM || '- GetBeadUnseat. There is at least one parameters invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetBeadUnseat',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetBeadUnseat. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetBeadUnseat',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
  
	END GETBEADUNSEAT;
  
	PROCEDURE GETENDURANCE(pc_endurancehdrcursor      OUT retcursor,
                           pc_endurancedtlcursor      OUT retcursor,
                           pi_skuid 				IN    NUMBER,
                           ps_certificatenumber 	in    VARCHAR,
                           pi_certificationtypeid 	IN    NUMBER,
                           pi_certificatenumberid 	IN    NUMBER) 
	AS
    /******************************************************************************
     NAME:       GetENDURANCE
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Added Lpad(matl_num) in the select list
     1.2        10/18/2012    Harini         1.Added Operation in the select list
     1.3        11/04/2013   Harini          1.Added GTSpec in the select list
    ******************************************************************************/
	--EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parameterisinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parameterisinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
    li_enduranceid ENDURANCEHDR.ENDURANCEID%TYPE := NULL;
    ls_enduranceexists VARCHAR2(1) := NULL;
	
	BEGIN
  
        IF pi_skuid IS NULL OR ps_certificatenumber IS NULL OR  pi_certificationtypeid IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		
        IF pi_skuid <= 0 OR ps_certificatenumber = '' OR pi_certificationtypeid <= 0 THEN
            RAISE li_parameterisinvalid;
        END IF;
		
		Open pc_endurancehdrcursor FOR
            SELECT ENDURANCEID AS END_ID,
                   PROJECTNUMBER AS PROJECTNUM,
                   TIRENUMBER AS TIRENUM,
                   TESTSPEC AS TESTSPEC,
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
                   WHEELNUMBER AS WHEELNUM,
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
                   E.SKU,
                   LPAD(E.MATL_NUM, 18, 0) AS MATL_NUM, -- As per PRJ3617,added Matl_Num in the select list
                   E.LOWPRESSURESTARTINFLATION,
                   E.LOWPRESSUREENDINFLATION,
                   E.LOWPRESSUREENDTEMP,
                   E.OPERNUM AS OPERATION,         -- As per PRJ3617,added Operation in select list
                   e.GTSpec
            FROM CERTIFICATE CE
				INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
            WHERE LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND E.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
            
			li_enduranceid := TESTRESULTS_CRUD.GETENDURANCEID(ps_certificatenumber		=> ps_certificatenumber,
                                                              pi_certificationtypeid	=> pi_certificationtypeid,
                                                              pi_certificatenumberid 	=> pi_certificatenumberid);
                  
		OPEN pc_endurancedtlcursor FOR
			SELECT ED.TESTSTEP,
                   ED.TIMEINMIN,
                   ED.SPEED,
                   ED.TOTMILES,
                   ED.LOAD,
                   ED.LOADPERCENT,
                   ED.SETINFLATION,
                   ED.AMBTEMP,
                   ED.INFPRESSURE,
                   ED.STEPCOMPLETIONDATE,
                   ED.ENDURANCEID AS END_ID
			FROM ENDURANCEDTL ED
			WHERE ED.ENDURANCEID = li_enduranceid
			ORDER BY ED.TESTSTEP ASC;
	
	EXCEPTION
        
		WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- GetENDURANCE. There is at least one parameters null.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetENDURANCE',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		when li_parameterisinvalid then
            ls_errormsg := SQLERRM || '- GetENDURANCE. There is one parameters is invalid.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetENDURANCE',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := '- GetENDURANCE. An error have ocurred.(when others)' || SQLERRM ;
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetENDURANCE',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
	
	END GETENDURANCE;
 
	PROCEDURE GETHIGHSPEED(pc_highspeedcursor         OUT retcursor,
                           pc_highspeeddetailcursor   OUT retcursor,
                           pc_hsspeedtestdetail 	  OUT retcursor,
                           pi_skuid 				IN 	  NUMBER,
                           ps_certificatenumber 	IN 	  VARCHAR2,
                           pi_certificationtypeid 	IN 	  NUMBER,
                           pi_certificatenumberid 	IN 	  NUMBER) 
	AS
    /******************************************************************************
     NAME:       GetHighSpeed
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Added Lpad(matl_num) in the select list
     1.2        10/18/2012    Harini         1.Added Operation in the select list
     1.3        11/04/2013    Harini         1.Added GTSpec in the select list
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50):= 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_highspeedid HIGHSPEEDHDR.HIGHSPEEDID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
    ls_measureexists VARCHAR2(1) := NULL;
	
    BEGIN
	
        IF pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		
        if pi_certificationtypeid <=0 OR ps_certificatenumber = '' then
            RAISE li_parametersareinvalid;
        END IF;
		  
		--Return Measure records
		OPEN pc_highspeedcursor FOR
			SELECT HIGHSPEEDID AS HS_ID,
                   PROJECTNUMBER AS PROJECTNUM,
                   TIRENUM AS TIRENUM,
                   TESTSPEC AS TESTSPEC,
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
                   M.CERTIFICATIONTYPEID,
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
                   MAXLOAD,
                   M.SKU,
                   LPAD(M.MATL_NUM, 18, 0) AS MATL_NUM,  -- As per PRJ3617,added Matl_Num in the select list
                   M.OPERNUM AS OPERATION,  -- As per PRJ3617,added Operation in select list
                   M.GTSPEC
			FROM CERTIFICATE CE
				INNER JOIN HIGHSPEEDHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
			WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
                   
			--Get's the MeasureId Based on the Certification Type ID,SKU and CertificateNumber
			li_highspeedid := TESTRESULTS_CRUD.GETHIGHSPEEDID(ps_certificatenumber 		=> ps_certificatenumber,
															  pi_certificationtypeid 	=> pi_certificationtypeid,
															  pi_certificatenumberid 	=> pi_certificatenumberid);
                      
		--Return the Measure Detail records
		OPEN pc_highspeeddetailcursor FOR
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
                   HIGHSPEEDID AS HS_ID
            FROM  HIGHSPEEDDTL H
            WHERE H.HIGHSPEEDID = li_highspeedid
            ORDER BY TESTSTEP;
                     
		OPEN pc_hsspeedtestdetail FOR
            SELECT ITERATION,
                   TIME,
                   SPEED,
                   HIGHSPEEDID AS HS_ID
            FROM SPEEDTESTDETAIL S
            WHERE S.HIGHSPEEDID = li_highspeedid
            ORDER BY ITERATION ASC;
    
	EXCEPTION
	
        WHEN li_parametersarenull then
            ls_errormsg := SQLERRM || '- GetHighSpeed . There is at least one parameters null.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetHighSpeed',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
			
        WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- GetHighSpeed . There is at least one parameters invalid.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetHighSpeed',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
				
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
			
        WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' An error have ocurred.(when others)';
               
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetHighSpeed',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
    END GETHIGHSPEED;
    
	PROCEDURE GETSOUND(pc_soundhdrcursor 		  OUT retcursor,
                       pc_sounddetailcursor 	  OUT retcursor,
                       pi_skuid 				IN    NUMBER,
                       ps_certificatenumber		IN    VARCHAR2,
                       pi_certificationtypeid 	IN    NUMBER,
                       pi_certificatenumberid 	IN    NUMBER) 
	AS
	/******************************************************************************
     NAME:       GetSound
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/18/2012    Harini         1.Added Operation in the select list
     1.2        11/04/2013    Harini         1.Added MFGWWYY and GTSpec in the select list
    ******************************************************************************/
	--EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_soundid SOUNDHDR.SOUNDID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
	
	BEGIN
  
        IF pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		  
        IF pi_certificationtypeid <=0 OR ps_certificatenumber = '' THEN
            RAISE li_parametersareinvalid;
        END IF;
		  
        OPEN pc_soundhdrcursor FOR
			SELECT SOUNDID,
                   PROJECTNUMBER,
                   TIRENUMBER,
                   TESTSPEC,
                   TESTREPORTNUMBER,
                   MANUFACTUREANDBRAND,
                   TIRECLASS,
                   CATEGORYOFUSE,
                   DATEOFTEST,
                   TESTVEHICULE,
                   TESTVEHICULEWHEELBASE,
                   LOCATIONOFTESTTRACK,
                   DATETRACKCERTIFTOISO,
                   TIRESIZEDESIGNATION,
                   TIRESERVICEDESCRIPTION,
                   TESTMASS_FRONTL,
                   TESTMASS_FRONTR,
                   TESTMASS_REARL,
                   TESTMASS_REARR,
                   TIRELOADINDEX_FRONTL,
                   TIRELOADINDEX_FRONTR,
                   TIRELOADINDEX_REARL,
                   TIRELOADINDEX_REARR,
                   INFLATIONPRESSURECO_FRONTL,
                   INFLATIONPRESSURECO_FRONTR,
                   INFLATIONPRESSURECO_REARL,
                   INFLATIONPRESSURECO_REARR,
                   TESTRIMWIDTHCODE,
                   TEMPMEASURESENSORTYPE,
                   S.CERTIFICATIONTYPEID,
                   CERTIFICATENUMBER,
                   S.OPERNUM AS OPERATION,         -- As per PRJ3617,added Operation in select list
                   S.MFGWWYY,
                   S.GTSPEC
            FROM CERTIFICATE CE
				INNER JOIN SOUNDHDR S ON CE.CERTIFICATEID = S.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = S.CERTIFICATIONTYPEID
            WHERE S.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
               
			li_soundid := TESTRESULTS_CRUD.GETSOUNDHDRID(ps_certificatenumber 		=> ps_certificatenumber,
														 pi_certificationtypeid 	=> pi_certificationtypeid,
														 pi_certificatenumberid 	=> pi_certificatenumberid);
            
			OPEN pc_sounddetailcursor FOR
				SELECT ITERATION,
					   TESTSPEED,
					   DIRECTIONOFRUN,
					   SOUNDLEVELLEFT,
					   SOUNDLEVELRIGHT,
					   AIRTEMP,
					   TRACKTEMP,
					   SOUNDLEVELLEFT_TEMPCORRECTED,
					   SOUNDLEVELRIGHT_TEMPCORRECTED,
					   SOUNDID
				FROM SOUNDDETAIL
				WHERE SOUNDID = li_soundid
				ORDER BY ITERATION ASC;
        
	EXCEPTION
        
		WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- GetSound.  There is at least one parameters null.';
             
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetSound',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- GetSound. There is at least one parameters invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetSound',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetSound. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetSound',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
  
	END GETSOUND;
  
	PROCEDURE GETWETGRIP(pc_wetgriphdrcursor 	  OUT retcursor,
                         pc_wetgripdetailcursor   OUT retcursor,
                         pi_skuid 				IN    NUMBER,
                         ps_certificatenumber 	IN    VARCHAR2,
                         pi_certificationtypeid IN    NUMBER,
                         pi_certificatenumberid IN    NUMBER) 
	AS
    /******************************************************************************
     NAME:       GetWetGrip
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/18/2012    Harini         1.Added Operation in the select list
     1.2        11/04/2013    Harini         1.Added MFGWWYY and GTSpec in the select list
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_wetgripid WETGRIPHDR.WETGRIPID%TYPE := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE := NULL;
	
	begin
  
        IF pi_certificationtypeid IS NULL OR ps_certificatenumber IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		 
        if  pi_certificationtypeid <=0 OR ps_certificatenumber = '' THEN
            RAISE li_parametersareinvalid;
        END IF;
		 
        OPEN pc_wetgriphdrcursor FOR
			SELECT WETGRIPID ,
                   PROJECTNUMBER,
                   TIRENUMBER,
                   TESTSPEC,
                   DATEOFTEST,
                   TESTVEHICLE,
                   LOCATIONOFTESTTRACK,
                   TESTTRACKCHARACTERISTICS,
                   ISSUEBY,
                   METHODOFCERTIFICATION,
                   TESTTIREDETAILS,
                   TIRESIZEANDSERVICEDESC,
                   TIREBRANDANDTRADEDESC,
                   REFERENCEINFLATIONPRESSURE,
                   TESTRIMWITHCODE,
                   TEMPMEASURESENSORTYPE,
                   IDENTIFICATIONSRTT,
                   TESTTIRELOAD_SRTT,
                   TESTTIRELOAD_CANDIDATE,
                   TESTTIRELOAD_CONTROL,
                   WATERDEPTH_SRTT,
                   WATERDEPTH_CANDIDATE,
                   WATERDEPTH_CONTROL,
                   WETTEDTRACKTEMPAVG,
                   W.CERTIFICATIONTYPEID,
                   CERTIFICATENUMBER,
                   W.OPERNUM AS OPERATION,         -- As per PRJ3617,added Operation in select list
                   W.MFGWWYY,
                   W.GTSPEC
            FROM CERTIFICATE CE
				INNER JOIN WETGRIPHDR  W ON CE.CERTIFICATEID = W.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = W.CERTIFICATIONTYPEID
			WHERE W.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
               
			li_wetgripid := TESTRESULTS_CRUD.GETWETGRIPHDRID(ps_certificatenumber 		=> ps_certificatenumber,
															 pi_certificationtypeid 	=> pi_certificationtypeid,
															 pi_skuid 					=> pi_skuid,
															 pi_certificatenumberid 	=> pi_certificatenumberid);
            OPEN pc_wetgripdetailcursor FOR
				SELECT ITERATION,
					   TESTSPEED,
					   DIRECTIONOFRUN,
					   SRTT,
					   CANDIDATETIRE,
					   PEAKBREAKFORCECOEFICIENT,
					   MEANFULLYDEVELOPEDDECELERATION,
					   WETGRIPINDEX,
					   COMMENTS,
					   WETGRIPID
				FROM WETGRIPDETAIL
				WHERE WETGRIPID = li_wetgripid
				ORDER BY ITERATION ASC;
    
	EXCEPTION
	
        when li_parametersarenull then
            ls_errormsg := SQLERRM || '- GetWetGrip. GetWetGrip.There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid => ls_machineid,
                ad_operatorid => ls_operatorid,
                ad_daterecorded  => SYSDATE,
                as_processname   => ' testresults_crud.GetWetGrip',
                ax_recorddata    => 'ps_sku is parameters null..',
                as_messagecode   => TO_CHAR(SQLCODE),
                as_message       => ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		when li_parametersareinvalid then
            ls_errormsg := SQLERRM || '- GetWetGrip. GetWetGrip.There is at least one parameters invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetWetGrip',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetWetGrip. GetWetGrip.An error have ocurred.(when others)';
               
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetWetGrip',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
                 
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
  
	END GETWETGRIP;
  
	PROCEDURE PRODUCT_SAVE(pi_skuid 				IN  NUMBER,
                          ps_matl_num 				IN  VARCHAR2,
                          ps_brand 					IN  VARCHAR2,
                          ps_brand_line 			IN  VARCHAR2,
                          pi_tiretypeid 			IN  NUMBER,
                          ps_psn 					IN  VARCHAR2,
                          ps_sizestamp 				IN  VARCHAR2,
                          pd_discontinueddate 		IN  DATE,
                          ps_specnumber 			IN  VARCHAR2,
                          ps_speedrating 			IN  VARCHAR2,
                          ps_singloadindex 			IN  VARCHAR2,
                          ps_dualloadindex 			IN  VARCHAR2,
                          ps_biasbeltedradial 		IN  VARCHAR2,
                          ps_tubelessyn 			IN  VARCHAR2,
                          ps_reinforcedyn 			IN  VARCHAR2,
                          ps_extraloadyn 			IN  VARCHAR2,
                          ps_utqgtreadwear 			IN  VARCHAR2,
                          ps_utqgtraction 			IN  VARCHAR2,
                          ps_utqgtemp 				IN  VARCHAR2,
                          ps_mudsnowyn 				IN  VARCHAR2,
                          ps_severeweatherind 		IN 	VARCHAR2,
                          pi_rimdiameter 			IN  NUMBER,
                          pd_serialdate 			IN  DATE,
                          ps_mfgwwyy   				IN 	VARCHAR2,
                          ps_branddesc 				IN  VARCHAR2,
                          ps_loadrange 				IN  VARCHAR2,
                          pi_mearimwidth 			IN  NUMBER,
                          ps_regroovableind 		IN  VARCHAR2,
                          ps_plantproduced 			IN  VARCHAR2,
                          pd_mostrecenttestdate 	IN  DATE,
                          ps_imark 					IN  VARCHAR2,
                          ps_informenumber 			IN  VARCHAR2,
                          pd_fechadate 				IN  DATE,
                          ps_treadpattern 			IN  VARCHAR2,
                          ps_specialprotectiveband	IN  VARCHAR2,
                          ps_nominaltirewidth 		IN  VARCHAR2,
                          ps_aspectradio 			IN  VARCHAR2,
                          ps_treadwearindicators 	IN  VARCHAR2,
                          ps_nameofmanufacturer 	IN  VARCHAR2,
                          ps_family 				IN  VARCHAR2,
                          ps_dotserialnumber 		IN  VARCHAR2,
                          ps_tpn 					IN 	VARCHAR2,
                          ps_operatorname   		IN 	VARCHAR2) 
	AS
    /******************************************************************************
     NAME:       Product_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_Brandcode with ps_brand and 
                                              ps_brand_line,pi_NPRID with ps_PSN,
                                              pi_PPN with ps_TPN.
                                              Added Lpad(matl_num) in the select list
                                             2. Added logic to get SKU to insert in Product table
     1.2        11/04/2013    Harini         1.As per IDEA2706, Added  ps_SEVEREWEATHERIND and
                                              ps_mfgwwyy parameters in Input and add these paramters
                                              while inserting/Updating
     1.3        11/11/2013    Harini         1. Added TiretypeId in update statement as it is required to 
                                              update from UI
                                              
     1.4        5/15/2014      jeseitz   1. commented out saving of imark and family, bcause these saves are done in
                                            certification_crud.certificate_save.  Was blanking out these fields when the save was done here too.
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_skuexists VARCHAR2(1) := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_tpn PRODUCT.TPN%TYPE := NULL;   --AS per PRJ3617,Replaced li_PPN with li_TPN
    ls_sku Product.SKU%TYPE := NULL;
    ls_matl_num Product.Matl_Num%TYPE := NULL;
	
	BEGIN
  
		IF ps_matl_num IS NULL THEN  --AS per PRJ3617,Replaced ps_SKU with ps_matl_num
			RAISE li_parametersarenull;
		END IF;
	   
		IF ps_matl_num = '' THEN   --AS per PRJ3617,Replaced ps_SKU with ps_matl_num
            RAISE li_parametersareinvalid;
        END IF;
		
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
            ls_operatorid := ps_operatorname;
        END IF;
      
		ls_matl_num := LPAD(ps_matl_num,18,0);
		
		ls_skuexists:= TESTRESULTS_CRUD.CHECKIFPRODUCTEXISTS(ps_matl_num	=> ls_matl_num,
															 pi_skuid 		=> pi_skuid);
		
		IF ls_skuexists = 'y' THEN
			UPDATE PRODUCT 
			SET SIZESTAMP 			  = ps_sizestamp,
                DISCONTINUEDDATE 	  = pd_discontinueddate,
                SPECNUMBER     		  = ps_specnumber,
                SPEEDRATING    		  = ps_speedrating,
                SINGLOADINDEX  		  = ps_singloadindex,
                DUALLOADINDEX  		  = ps_dualloadindex,
                BIASBELTEDRADIAL 	  = ps_biasbeltedradial,
                TUBELESSYN     		  = ps_tubelessyn,
                REINFORCEDYN   		  = ps_reinforcedyn,
                EXTRALOADYN    		  = ps_extraloadyn,
                UTQGTREADWEAR  		  = ps_utqgtreadwear,
                UTQGTRACTION   		  = ps_utqgtraction,
                UTQGTEMP       		  = ps_utqgtemp,
                MUDSNOWYN      		  = ps_mudsnowyn,
                SEVEREWEATHERIND      = ps_severeweatherind,
                RIMDIAMETER    		  = pi_rimdiameter,
                SERIALDATE     		  = pd_serialdate,
                MFGWWYY        		  = ps_mfgwwyy,
                BRANDDESC      		  = ps_branddesc,
                LOADRANGE      		  = ps_loadrange,
                MEARIMWIDTH    		  = pi_mearimwidth,
                REGROOVABLEIND 		  = ps_regroovableind,
                PLANTPRODUCED  		  = ps_plantproduced,
                MOSTRECENTTESTDATE 	  = pd_mostrecenttestdate,
                ModifiedOn     		  = SYSDATE,
                ModifiedBy     		  = ls_operatorid,
                INFORMENUMBER  		  = ps_informenumber,
                FECHADATE      		  = pd_fechadate,
                TREADPATTERN   		  = ps_treadpattern,
                SPECIALPROTECTIVEBAND = ps_specialprotectiveband,
                NOMINALTIREWIDTH      = ps_nominaltirewidth,
                ASPECTRATIO           = ps_aspectradio,
                TREADWEARINDICATORS   = ps_treadwearindicators,
                NAMEOFMANUFACTURER    = ps_nameofmanufacturer,
                DOTSERIALNUMBER       = ps_dotserialnumber,
                TPN                   = ps_tpn,
                TIRETYPEID            = pi_tiretypeid -- As per IDEA2706
			WHERE MATL_NUM = ls_matl_num   --AS per PRJ3617,Replaced ps_SKU with ps_matl_num
                AND SKUID  = pi_skuid;
		ELSE
			-- As per PRJ3617,Added this logic to get SKU to insert in Product table
            SELECT Attrib_value 
				INTO ls_sku 
            FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE, ' ')) AS ATTRIB_VALUE 
				  FROM MATERIAL_ATTRIBUTE
				  WHERE ATTRIB_NAME = 'LEGACY_COOPER_SKU'
					AND MATL_NUM = ls_matl_num);

			INSERT INTO PRODUCT(SKUID,--AS per PRJ3617,Added Matl_Num,replaced BrandCode with Brand and Brand_Line,NPRID with PSN and PPN with TPN
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
								SEVEREWEATHERIND,
								RIMDIAMETER,
								SERIALDATE,
								MFGWWYY,
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
								TPN,
								CREATEDBY)
						 VALUES(pi_skuid,
								ls_sku,
								ls_matl_num,
								ps_brand,
								ps_brand_line,
								pi_tiretypeid,
								ps_psn,
								ps_sizestamp,
								pd_discontinueddate,
								ps_specnumber,
								ps_speedrating,
								ps_singloadindex,
								ps_dualloadindex,
								ps_biasbeltedradial,
								ps_tubelessyn,
								ps_reinforcedyn,
								ps_extraloadyn,
								ps_utqgtreadwear,
								ps_utqgtraction,
								ps_utqgtemp,
								ps_mudsnowyn,
								ps_severeweatherind,
								pi_rimdiameter,
								pd_serialdate,
								ps_mfgwwyy,
								ps_branddesc,
								ps_loadrange,
								pi_mearimwidth,
								ps_regroovableind,
								ps_plantproduced,
								pd_mostrecenttestdate,
								ps_imark,
								ps_informenumber,
								pd_fechadate,
								ps_treadpattern,
								ps_specialprotectiveband,
								ps_nominaltirewidth,
								ps_aspectradio,
								ps_treadwearindicators,
								ps_nameofmanufacturer,
								ps_family,
								ps_dotserialnumber,
								ps_tpn,
								ls_operatorid);
		END IF;
		
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '-Product_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Product_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, SQLERRM);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '-Product_Save. There is at least one parameters invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Product_Save',
													  ax_recorddata    	=> 'There is at least one parameters invalid.',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, SQLERRM);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '-Product_Save. An error have ocurred.(when others)';
               
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Product_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END PRODUCT_SAVE;
  
	PROCEDURE MEASURE_SAVE(pi_measureid 				OUT NUMBER,
                           pi_certificateid  			IN NUMBER,
                           ps_projectnumber 			IN VARCHAR2,
                           pi_tirenumber 				IN NUMBER,
                           ps_testspec 					IN VARCHAR2,
                           pd_completiondate 			IN DATE,
                           pi_inflationpressure 		IN NUMBER,
                           ps_molddesign 				IN VARCHAR2,
                           pi_rimwidth 					IN NUMBER,
                           ps_dotserialnumber 			IN VARCHAR2,
                           pi_diameter 					IN NUMBER,
                           pi_avgsectionwidth 			IN NUMBER,
                           pi_avgoverallwidth 			IN NUMBER,
                           pi_maxoverallwidth 			IN NUMBER,
                           pi_sizefactor 				IN NUMBER,
                           pd_mounttime 				IN DATE,
                           pi_mounttemp 				IN NUMBER,
                           pd_serialdate 				IN DATE,
                           ps_mfgwwyy    				IN VARCHAR2,
                           pd_endtime 					IN DATE,
                           pi_actsizefactor 			IN NUMBER,
                           pi_certificationtypeid 		IN NUMBER,
                           pi_startinflationpressure 	IN NUMBER,
                           pi_endinflationpressure 		IN NUMBER,
                           ps_adjustment 				IN VARCHAR2,
                           pi_circunference 			IN NUMBER,
                           pi_nominaldiameter 			IN NUMBER,
                           pi_nominalwidth 				IN NUMBER,
                           ps_nominalwidthpassfail 		IN VARCHAR2,
                           pi_nominalwidthdiference 	IN NUMBER,
                           pi_nominalwidthtolerance 	IN NUMBER,
                           pi_maxoveralldiameter 		IN NUMBER,
                           pi_minoveralldiameter 		IN NUMBER,
                           ps_overallwidthpassfail 		IN VARCHAR2,
                           ps_overalldiameterpassfail 	IN VARCHAR2,
                           pi_diameterdiference 		IN NUMBER,
                           pi_diametertolerance 		IN NUMBER,
                           pi_tempresistancegrading 	IN VARCHAR2,
                           pi_tensilestrenght1 			IN NUMBER,
                           pi_tensilestrenght2 			IN NUMBER,
                           pi_elongation1 				IN NUMBER,
                           pi_elongation2 				IN NUMBER,
                           pi_tensilestrenghtafterage1	IN NUMBER,
                           pi_tensilestrenghtafterage2 	IN NUMBER,
                           ps_operatorname   			IN VARCHAR2,
                           ps_matl_num 					IN VARCHAR2,
                           ps_operation 				IN VARCHAR2,
                           ps_gtspec 					IN VARCHAR2) 
	AS
    /******************************************************************************
     NAME:       Measure_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
                                              Added Lpad(matl_num) in the update,insert
                                              and in where condition of select list
                                            2. Added logic to get SKU to insert in Product table
     1.2        10/18/2012     Harini        1.Added Operation while updating and inserting.
     1.3        10/30/2012     Harini        1.Included NO_DATA_FOUND EXCEPTION block when no 
                                             SKU available for given Matl_Num and assign empty 
                                             for ls_sku parameter
     1.4        11/04/2013     Harini       1.As per IDEA2706,Modified procedure by adding 
                                           ps_gtspec and ps_mfgwwyy parameters in Input and add 
                                           these paramters while inserting/Updating
    ******************************************************************************/
	--EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	 
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_measureexists VARCHAR2(1) := NULL;
    li_certificationid INTEGER := NULL;
    li_currentmeasureid NUMBER := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_sku PRODUCT.SKU%TYPE := NULL;
    ls_matl_num PRODUCT.MATL_NUM%TYPE := NULL;
	
	BEGIN
	
        IF pi_certificationtypeid IS NULL  THEN
			RAISE li_parametersarenull;
        END IF;
		
		IF pi_certificationtypeid <=0 THEN
			RAISE li_parametersareinvalid;
        END IF;
		
		IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
		END IF;
	   
		ls_matl_num := LPAD(ps_matl_num,18,0);
	   
		ls_measureexists:= TESTRESULTS_CRUD.CHECKIFMEASUREEXISTS(pi_certificateid 			=> pi_certificateid,
                                                                 pi_certificationtypeid 	=> pi_certificationtypeid);
		
		BEGIN		
			SELECT ATTRIB_VALUE 
				INTO ls_sku 
			FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE,' ')) AS ATTRIB_VALUE 
                  FROM MATERIAL_ATTRIBUTE
                  WHERE ATTRIB_NAME = 'LEGACY_COOPER_SKU'
					AND MATL_NUM = ls_matl_num);					
		EXCEPTION		
			WHEN NO_DATA_FOUND THEN
				ls_sku := '';
		END;
      
		IF ls_measureexists = 'y' THEN
            UPDATE MeasureHdr M 
				SET PROJECTNUMBER            = ps_projectnumber,
                    TIRENUMBER               = pi_tirenumber,
                    TESTSPEC                 = ps_testspec,
                    COMPLETIONDATE           = pd_completiondate,
                    INFLATIONPRESSURE        = pi_inflationpressure,
                    MOLDDESIGN               = ps_molddesign,
                    RIMWIDTH                 = pi_rimwidth,
                    DOTSERIALNUMBER          = ps_dotserialnumber,
                    DIAMETER                 = pi_diameter,
                    AVGSECTIONWIDTH          = pi_avgsectionwidth,
                    AVGOVERALLWIDTH          = pi_avgoverallwidth,
                    MAXOVERALLWIDTH          = pi_maxoverallwidth,
                    SIZEFACTOR               = pi_sizefactor,
                    MOUNTTIME                = pd_mounttime,
                    MOUNTTEMP                = pi_mounttemp,
                    SERIALDATE               = pd_serialdate,
                    MFGWWYY                  = ps_mfgwwyy,
                    ENDTIME                  = pd_endtime,
                    ACTSIZEFACTOR            = pi_actsizefactor,
                    STARTINFLATIONPRESSURE   = pi_startinflationpressure,
                    ENDINFLATIONPRESSURE     = pi_endinflationpressure,
                    ADJUSTMENT               = ps_adjustment,
                    CIRCUMFERENCE            = pi_circunference,
                    NOMINALDIAMETER          = pi_nominaldiameter,
                    NOMINALWIDTH             = pi_nominalwidth,
                    NOMINALWIDTHPASSFAIL     = ps_nominalwidthpassfail,
                    NOMINALWIDTHDIFERENCE    = pi_nominalwidthdiference,
                    NOMINALWIDTHTOLERANCE    = pi_nominalwidthtolerance,
                    MAXOVERALLDIAMETER       = pi_maxoveralldiameter,
                    MINOVERALLDIAMETER       = pi_minoveralldiameter,
                    OVERALLWIDTHPASSFAIL     = ps_overallwidthpassfail,
                    OVERALLDIAMETERPASSFAIL  = ps_overalldiameterpassfail,
                    DIAMETERDIFERENCE        = pi_diameterdiference,
                    DIAMETERTOLERANCE        = pi_diametertolerance,
                    TEMPRESISTANCEGRADING    = pi_tempresistancegrading,
                    TENSILESTRENGHT1         = pi_tensilestrenght1,
                    TENSILESTRENGHT2         = pi_tensilestrenght2,
                    ELONGATION1              = pi_elongation1,
                    ELONGATION2              = pi_elongation2,
                    TENSILESTRENGHTAFTERAGE1 = pi_tensilestrenghtafterage1,
                    TENSILESTRENGHTAFTERAGE2 = pi_tensilestrenghtafterage2,
                    MODIFIEDBY               = ls_operatorid,
                    MODIFIEDON               = SYSDATE,
                    MATL_NUM                 = ls_matl_num, --As per PRJ3617,commneted sku and added Matl_Num
                    SKU                      = ls_sku,
                    OPERNUM                  = ps_operation, --As per PRJ3617, added OperNum while updating
                    GTSPEC                   = ps_gtspec
            WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND M.CERTIFICATEID       = pi_certificateid;
           
			SELECT MAX(MEASUREID) 
				INTO li_currentmeasureid
            FROM MEASUREHDR M
            WHERE M.CERTIFICATIONTYPEID   = pi_certificationtypeid 
				AND M.CERTIFICATEID       = pi_certificateid;
            
			DELETE 
			FROM MEASUREDTL 
			WHERE MEASUREID = li_currentmeasureid;
			
            pi_measureid := li_currentmeasureid;
			 
		ELSE
		
            INSERT INTO MEASUREHDR(MEASUREID,
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
								   MFGWWYY,
								   ENDTIME,
								   ACTSIZEFACTOR,
								   CERTIFICATIONTYPEID,
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
								   CERTIFICATEID,
								   MATL_NUM, 
								   SKU,
								   OPERNUM,
								   GTSPEC)
							VALUES(MEASUREID_SEQ.NEXTVAL,
								   ps_projectnumber,
								   pi_tirenumber,
								   ps_testspec,
								   pd_completiondate,
								   pi_inflationpressure,
								   ps_molddesign,
								   pi_rimwidth,
								   ps_dotserialnumber,
								   pi_diameter,
								   pi_avgsectionwidth,
								   pi_avgoverallwidth,
								   pi_maxoverallwidth,
								   pi_sizefactor,
								   pd_mounttime,
								   pi_mounttemp,
								   pd_serialdate,
								   ps_mfgwwyy,
								   pd_endtime,
								   pi_actsizefactor,
								   pi_certificationtypeid,
								   pi_startinflationpressure,
								   pi_endinflationpressure,
								   ps_adjustment,
								   pi_circunference,
								   pi_nominaldiameter,
								   pi_nominalwidth,
								   ps_nominalwidthpassfail,
								   pi_nominalwidthdiference,
								   pi_nominalwidthtolerance,
								   pi_maxoveralldiameter,
								   pi_minoveralldiameter,
								   ps_overallwidthpassfail,
								   ps_overalldiameterpassfail,
								   pi_diameterdiference,
								   pi_diametertolerance,
								   pi_tempresistancegrading,
								   pi_tensilestrenght1,
								   pi_tensilestrenght2,
								   pi_elongation1,
								   pi_elongation2,
								   pi_tensilestrenghtafterage1,
								   pi_tensilestrenghtafterage2,
								   pi_certificateid,
								   ls_matl_num,     -- As per PRJ3617,inserting matl_num 
								   ls_sku,
								   ps_operation,     -- As per PRJ3617,inserting operation 
								   ps_gtspec);
            
			--Gets the Id that just was inserted on the table to be returned
            SELECT MAX(MEASUREID) 
				INTO li_currentmeasureid
            FROM MEASUREHDR M
            WHERE M.CERTIFICATIONTYPEID   = pi_certificationtypeid 
				AND M.CERTIFICATEID       = pi_certificateid;
				
             pi_measureid := li_currentmeasureid;
			 
		END IF;
		
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := 'Measure_Save. There is at least one parameters null.' || SQLERRM ;
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Measure_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
			
        WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || 'Measure_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Measure_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || 'Measure_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Measure_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END MEASURE_SAVE;
 
	PROCEDURE MEASUREDETAIL_SAVE(pi_sectionwidth 	IN MEASUREDTL.SECTIONWIDTH%TYPE,
                                 pi_overallwidth 	IN MEASUREDTL.OVERALLWIDTH%TYPE,
                                 pi_measureid 		IN NUMBER,
                                 pi_iteration 		IN NUMBER,
                                 ps_operatorname	IN VARCHAR2)
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	 
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_measuredetailexists VARCHAR2(1) 	:= NULL;
    li_certificationid INTEGER 			:= NULL;
    ls_machineid VARCHAR2(50) 			:= NULL;
    ls_operatorid VARCHAR2(50) 			:= 'ICSDEV';
    ls_errormsg VARCHAR2(4000) 			:= NULL;
	
	BEGIN
	
		IF pi_measureid IS NULL THEN
			RAISE li_parametersarenull;
		END IF;
		
		IF pi_measureid <= 0 THEN
			RAISE li_parametersareinvalid;
		END IF;
		
		IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname ;
		END IF;
	  
		INSERT INTO MEASUREDTL(SECTIONWIDTH,
							   OVERALLWIDTH,
							   MEASUREID,
							   ITERATION,
							   CREATEDBY)
						VALUES(pi_sectionwidth,
							   pi_overallwidth,
							   pi_measureid,
							   pi_iteration,
							   ls_operatorid);
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || 'MeasureDetail_Save.There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.MeasureDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
			
        WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || 'MeasureDetail_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.MeasureDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || 'MeasureDetail_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.MeasureDetail_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END MEASUREDETAIL_SAVE;
  
	PROCEDURE ENDURANCE_SAVE(pi_enduranceid 				  OUT NUMBER,
                             ps_projectnumber 				IN    VARCHAR2,
                             pi_tirenumber 					IN    NUMBER,
                             ps_testspec 					IN    VARCHAR2,
                             pd_completiondate 				IN    DATE,
                             ps_dotserialnumber 			IN    VARCHAR2,
                             ps_mfgwwyy 					IN    VARCHAR2,
                             pd_precondstartdate 			IN    DATE,
                             pi_precondstarttemp 			IN    NUMBER,
                             pi_rimdiameter 				IN    NUMBER,
                             pi_rimwidth 					IN    NUMBER,
                             pd_precondenddate 				IN    DATE,
                             pi_precondendtemp 				IN    NUMBER,
                             pi_inflationpressure 			IN    NUMBER,
                             pi_beforediameter 				IN    NUMBER,
                             pi_afterdiameter 				IN    NUMBER,
                             pi_beforeinflation 			IN    NUMBER,
                             pi_afterinflation 				IN    NUMBER,
                             pi_wheelposition 				IN    NUMBER,
                             pi_wheelnumber 				IN    NUMBER,
                             pi_finaltemp 					IN    NUMBER,
                             pi_finaldistance 				IN    NUMBER,
                             pi_finalinflation 				IN    NUMBER,
                             pd_postcondstartdate 			IN    DATE,
                             pd_postcondenddate 			IN    DATE,
                             pi_postcondendtemp 			IN    NUMBER,
                             ps_passyn 						IN    VARCHAR2,
                             pi_certificationtypeid 		IN    NUMBER,
                             pd_serialdate 					IN    DATE,
                             pi_precondtime 				IN    NUMBER,
                             pi_postcondtime  				IN    NUMBER,
                             pi_diametertestdrum 			IN    NUMBER,
                             pi_precondtemp 				IN    NUMBER,
                             pi_inflationpressurereadjusted	IN    NUMBER,
                             pi_circunferencebeforetest 	IN    NUMBER,
                             ps_resultpassfail 				IN    VARCHAR2,
                             pi_endurancehours 				IN    NUMBER,
                             ps_possiblefailuresfound 		IN    VARCHAR2,
                             pi_circunferenceaftertest 		IN    NUMBER,
                             pi_outerdiameterdiference 		IN    NUMBER,
                             pi_oddiferencetolerance 		IN    NUMBER,
                             ps_serienom 					IN    VARCHAR2,
                             ps_finaljudgement 				IN    VARCHAR2,
                             ps_approver 					IN    VARCHAR2,
                             ps_operatorname 				IN    VARCHAR2,
                             pi_certificateid 				IN    NUMBER,
                             ps_matl_num 					IN    VARCHAR2,
                             pn_lowInfstartinflation 		IN    NUMBER,
                             pn_lowInfendinflation 			IN    NUMBER,
                             pn_lowInfendtemp 				IN    NUMBER,
                             ps_operation 					IN    VARCHAR2,
                             ps_gtspec 						IN    VARCHAR2) 
	AS     
    /******************************************************************************
     NAME:       Endurance_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
                                              Added Lpad(matl_num) in the update,insert
                                              and in where condition of select list
                                            2. Added logic to get SKU to insert in Product table
     1.2        10/18/2012    Harini        1.Added Operation while updating and inserting.
     1.3        10/30/2012    Harini        1.Included NO_DATA_FOUND EXCEPTION block when no 
                                             SKU available for given Matl_Num and assign empty 
                                             for ls_sku parameter
     1.4        11/04/2013     Harini       1.As per IDEA2706,Modified procedure by adding 
                                              ps_gtspec parameter in Input and add this
                                              paramter while inserting/Updating
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_enduranceexists VARCHAR2(1) := NULL;
    li_certificationtypeid CERTIFICATIONTYPE.CERTIFICATIONTYPEID%TYPE;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_currentenduranceid ENDURANCEHDR.ENDURANCEID%TYPE := NULL;
    li_postcondtime ENDURANCEHDR.POSTCONDTIME%TYPE := NULL; -- jeseitz added 7/29/13
    li_precondtime ENDURANCEHDR.PreCondTime%type := NULL; -- jeseitz added 7/29/13
    ls_sku PRODUCT.SKU%TYPE := NULL;
    ls_matl_num PRODUCT.MATL_NUM%TYPE := NULL;
	
	BEGIN
	
        IF pi_certificationtypeid IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		
        IF pi_certificationtypeid <= 0  then
			RAISE li_parametersareinvalid;
        END IF;
		
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
        END IF;
         
        ls_matl_num := LPAD(ps_matl_num, 18, 0);
		
        ls_enduranceexists := TESTRESULTS_CRUD.CHECKIFENDURANCEEXISTS(pi_certificateid   		=> pi_certificateid ,
																	  pi_certificationtypeid 	=> pi_certificationtypeid);
       
		BEGIN                                                         
			SELECT ATTRIB_VALUE 
				INTO ls_sku 
			FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE,' ')) AS ATTRIB_VALUE 
                  FROM MATERIAL_ATTRIBUTE
                  WHERE ATTRIB_NAME = 'LEGACY_COOPER_SKU'
					AND MATL_NUM = ls_matl_num);   
		EXCEPTION
			WHEN NO_DATA_FOUND THEN
				ls_sku := '';
		END;
      
		--jeseitz added 7/29/13 because SAP was bringing back and incorrectly formatted
        IF pi_postcondtime > 9999 THEN
            li_postcondtime := 0;
        ELSE
            li_postcondtime := pi_postcondtime;
        END IF;
        
        IF pi_precondtime > 9999 THEN
            li_precondtime := 0;
        ELSE
            li_precondtime := pi_precondtime;
        END IF;
        
        IF ls_enduranceexists = 'y' THEN
            UPDATE EnduranceHdr 
			SET PROJECTNUMBER    = ps_projectnumber,
                TIRENUMBER       = pi_tirenumber,
                TESTSPEC         = ps_testspec,
                COMPLETIONDATE   = pd_completiondate,
                DOTSERIALNUMBER  = ps_dotserialnumber ,
                MFGWWYY          = ps_mfgwwyy,
                PRECONDSTARTDATE = pd_precondstartdate,
                PRECONDSTARTTEMP  = pi_precondstarttemp,
                RIMDIAMETER      = pi_rimdiameter,
                RIMWIDTH         = pi_rimwidth,
                PRECONDENDDATE   = pd_precondenddate,
                PRECONDENDTEMP   = pi_precondendtemp,
                INFLATIONPRESSURE = pi_inflationpressure ,
                BEFOREDIAMETER    = pi_beforediameter,
                AFTERDIAMETER     = pi_afterdiameter,
                BEFOREINFLATION   = pi_beforeinflation,
                AFTERINFLATION    = pi_afterinflation,
                WHEELPOSITION     = pi_wheelposition,
                WHEELNUMBER       = pi_wheelnumber,
                FINALTEMP         = pi_finaltemp ,
                FINALDISTANCE     = pi_finaldistance ,
                FINALINFLATION    = pi_finalinflation ,
                POSTCONDSTARTDATE = pd_postcondstartdate,
                POSTCONDENDDATE   = pd_postcondenddate,
                POSTCONDENDTEMP   = pi_postcondendtemp,
                PASSYN            = ps_passyn,
                SERIALDATE        = pd_serialdate ,
                PreCondTime       = li_precondtime  ,
                PostCondTime      = li_postcondtime,
                DIAMETERTESTDRUM  = pi_diametertestdrum,
                PRECONDTEMP       = pi_precondtemp,
                INFLATIONPRESSUREREADJUSTED = pi_inflationpressurereadjusted,
                CIRCUNFERENCEBEFORETEST     = pi_circunferencebeforetest,
                RESULTPASSFAIL              = ps_resultpassfail,
                ENDURANCEHOURS              = pi_endurancehours,
                POSSIBLEFAILURESFOUND       = ps_possiblefailuresfound,
                CIRCUNFERENCEAFTERTEST      = pi_circunferenceaftertest,
                OUTERDIAMETERDIFERENCE      = pi_outerdiameterdiference,
                ODDIFERENCETOLERANCE        = pi_oddiferencetolerance,
                SERIENOM                    = ps_serienom,
                FINALJUDGEMENT              = ps_finaljudgement,
                APPROVER                    = ps_approver,
                MODIFIEDON        = SYSDATE,
                MODIFIEDBY        = ls_operatorid,
                MATL_NUM          = ls_matl_num,   -- As per PRJ3617,updating Matl_Num
                SKU               = ls_sku,  
                LOWPRESSURESTARTINFLATION = pn_lowInfstartinflation,
                LOWPRESSUREENDINFLATION = pn_lowInfendinflation,
                LOWPRESSUREENDTEMP = pn_lowInfendtemp,
                OPERNUM                  = ps_operation, --As per PRJ3617, added OperNum while updating
                GTSPEC                   = ps_gtspec
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
            
			SELECT Max(ENDURANCEID) 
				INTO li_currentenduranceid
            FROM ENDURANCEHDR E
            WHERE E.CERTIFICATEID = pi_certificateid 
				AND E.CERTIFICATIONTYPEID = pi_certificationtypeid;
            
			DELETE 
			FROM ENDURANCEDTL 
			WHERE ENDURANCEID = li_currentenduranceid;
            
			pi_enduranceid := li_currentenduranceid;
			
        ELSE
		
            INSERT INTO ENDURANCEHDR(ENDURANCEID,
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
									 CERTIFICATIONTYPEID,
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
									 OUTERDIAMETERDIFERENCE ,
									 ODDIFERENCETOLERANCE ,
									 SERIENOM ,
									 FINALJUDGEMENT ,
									 APPROVER,
									 CREATEDBY,
									 CERTIFICATEID,
									 MATL_NUM,
									 SKU,
									 LOWPRESSURESTARTINFLATION,
									 LOWPRESSUREENDINFLATION,
									 LOWPRESSUREENDTEMP,
									 OPERNUM,
									 GTSPEC)
							  VALUES(ENDURANCEID_SEQ.NEXTVAL,
									 ps_projectnumber,
									 pi_tirenumber,
									 ps_testspec,
									 pd_completiondate,
									 ps_dotserialnumber,
									 ps_mfgwwyy,
									 pd_precondstartdate,
									 pi_precondstarttemp,
									 pi_rimdiameter,
									 pi_rimwidth ,
									 pd_precondenddate,
									 pi_precondendtemp,
									 pi_inflationpressure,
									 pi_beforediameter,
									 pi_afterdiameter,
									 pi_beforeinflation,
									 pi_afterinflation,
									 pi_wheelposition,
									 pi_wheelnumber,
									 pi_finaltemp,
									 pi_finaldistance,
									 pi_finalinflation ,
									 pd_postcondstartdate,
									 pd_postcondenddate,
									 pi_postcondendtemp,
									 ps_passyn,
									 pi_certificationtypeid,
									 pd_serialdate,
									 li_precondtime,
									 li_postcondtime,
									 pi_diametertestdrum,
									 pi_precondtemp,
									 pi_inflationpressurereadjusted,
									 pi_circunferencebeforetest,
									 ps_resultpassfail,
									 pi_endurancehours,
									 ps_possiblefailuresfound,
									 pi_circunferenceaftertest,
									 pi_outerdiameterdiference,
									 pi_oddiferencetolerance,
									 ps_serienom,
									 ps_finaljudgement,
									 ps_approver,
									 ls_operatorid,
									 pi_certificateid,
									 ls_matl_num,  -- As per PRJ3617,inserting Matl_Num
									 ls_sku,
									 pn_lowinfstartinflation,
									 pn_lowinfendinflation,
									 pn_lowinfendtemp,
									 ps_operation,     -- As per PRJ3617,inserting operation
									 ps_gtspec);
            
			SELECT MAX(ENDURANCEID) 
				INTO li_currentenduranceid
            FROM ENDURANCEHDR E
            WHERE E.CERTIFICATEID = pi_certificateid 
				AND E.CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            pi_enduranceid := li_currentenduranceid;
			  
        END IF;
		
        COMMIT;
		
    EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- Endurance_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Endurance_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- Endurance_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Endurance_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- Endurance_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.Endurance_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END ENDURANCE_SAVE;
  
	PROCEDURE ENDURANCEDETAIL_SAVE(pi_teststep 			IN NUMBER,
                                  pi_timeinmin 			IN NUMBER,
                                  pi_speed 				IN NUMBER,
                                  pi_totmiles 			IN NUMBER,
                                  pi_load 				IN NUMBER,
                                  pi_loadpercent 		IN NUMBER,
                                  pi_setinflation 		IN NUMBER,
                                  pi_ambtemp 			IN NUMBER,
                                  pi_infpressure 		IN NUMBER,
                                  pd_stepcompletiondate	IN ENDURANCEDTL.STEPCOMPLETIONDATE%TYPE,
                                  pi_enduranceid 		IN NUMBER) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_EnduranceDetailExists VARCHAR2(1) := NULL;
    li_certificationid INTEGER := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF pi_enduranceid IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		
        IF pi_enduranceid <= 0 THEN
            RAISE li_parametersareinvalid;
        END IF;
		
		INSERT INTO ENDURANCEDTL(TESTSTEP,
								 TIMEINMIN,
								 SPEED,
								 TOTMILES,
								 LOAD,
								 LOADPERCENT,
								 SETINFLATION,
								 AMBTEMP,
								 INFPRESSURE,
								 STEPCOMPLETIONDATE,
								 ENDURANCEID)
						  VALUES(pi_teststep,
								 pi_timeinmin,
								 pi_speed,
								 pi_totmiles,
								 pi_load,
								 pi_loadpercent,
								 pi_setinflation,
								 pi_ambtemp,
								 pi_infpressure,
								 pd_stepcompletiondate,
								 pi_enduranceid);
        COMMIT;
		
    EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- ENDURANCEDETAIL_SAVE. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.ENDURANCEDETAIL_SAVE',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg:= SQLERRM || '- ENDURANCEDETAIL_SAVE. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.ENDURANCEDETAIL_SAVE',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg:= SQLERRM || '- ENDURANCEDETAIL_SAVE. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.ENDURANCEDETAIL_SAVE',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
               
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END ENDURANCEDETAIL_SAVE;
   
	PROCEDURE TREADWEAR_SAVE(pi_treadwearid 		  OUT NUMBER,
                           ps_projectnumber  		IN    VARCHAR2,
                           pi_tirenumber 			IN    NUMBER,
                           ps_testspec  			IN    VARCHAR2,
                           pd_completiondate 		IN    DATE,
                           ps_dotserialnumber  		IN    VARCHAR2,
                           pi_lowestwearbar 		IN    NUMBER,
                           ps_passyn  				IN    VARCHAR2,
                           pi_certificationtypeid 	IN    NUMBER,
                           pd_serialdate 			IN    DATE,
                           ps_mfgwwyy    			IN    VARCHAR2,
                           ps_operatorname 			IN    VARCHAR2,
                           pi_indicatorsrequirement	IN    NUMBER,
                           pi_certificateid 		IN    NUMBER,
                           ps_matl_num 				IN    VARCHAR2,
                           ps_operation 			IN    VARCHAR2,
                           ps_gtspec 				IN    VARCHAR2) 
	AS
	/******************************************************************************
     NAME:       TREADWEAR_SAVE
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
                                              Added Lpad(matl_num) in the update,insert
                                              and in where condition of select list
                                             2. Added logic to get SKU to insert in Product table
     1.2        10/18/2012    Harini         1.Added Operation while updating and inserting.
     1.3        10/30/2012    Harini         1.Included NO_DATA_FOUND EXCEPTION block when no 
                                             SKU available for given Matl_Num and assign empty 
                                             for ls_sku parameter
    
     1.4        11/04/2013     Harini       1.As per IDEA2706,Modified procedure by adding 
                                            ps_gtspec and ps_mfgwwyy parameters in Input and add 
                                            these paramters while inserting/Updating
    ******************************************************************************/
	--EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_treadwearexists VARCHAR2(1) := NULL;
    li_certificationtypeid INTEGER := NULL;
    li_treadwearid TREADWEARHDR.TREADWEARID%TYPE := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_sku PRODUCT.SKU%TYPE := NULL;
    ls_matl_num PRODUCT.MATL_NUM%TYPE := NULL;
	
	BEGIN
   
        IF pi_certificationtypeid IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		  
        IF pi_certificationtypeid <=0 then
            RAISE li_parametersareinvalid;
        END IF;
		  
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
        END IF;
        
        ls_matl_num := LPAD(ps_matl_num, 18, 0);
		
        ls_treadwearexists := TESTRESULTS_CRUD.CHECKIFTREADWEAREXISTS(pi_certificateid   		=> pi_certificateid ,
                                                                      pi_certificationtypeid 	=> pi_certificationtypeid);
																	
       BEGIN                                                         
			SELECT ATTRIB_VALUE 
				INTO ls_sku 
			FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE, ' ')) AS ATTRIB_VALUE 
                  FROM MATERIAL_ATTRIBUTE
                  WHERE ATTRIB_NAME = 'LEGACY_COOPER_SKU'
					AND MATL_NUM = ls_matl_num);   
		EXCEPTION
			WHEN NO_DATA_FOUND THEN
				ls_sku := '';
		END;
      
        IF ls_treadwearexists = 'y' THEN
			UPDATE TREADWEARHDR 
			SET PROJECTNUMBER   = ps_projectnumber ,
                TIRENUMBER      = pi_tirenumber,
                TESTSPEC        = ps_testspec,
                COMPLETIONDATE  = pd_completiondate,
                DOTSERIALNUMBER = ps_dotserialnumber,
                LOWESTWEARBAR   = pi_lowestwearbar,
                PASSYN          = ps_passyn,
                SERIALDATE      = pd_serialdate,
                MFGWWYY         = ps_mfgwwyy,
                INDICATORSREQUIREMENT = pi_indicatorsrequirement,
                MODIFIEDON     = SYSDATE,
                MODIFIEDBY     = ls_operatorid,
                MATL_NUM       = ls_matl_num,  -- As per PRJ3617,updating Matl_Num
                SKU            = ls_sku,
                OPERNUM        = ps_operation, --As per PRJ3617, added OperNum while updating
                GTSPEC         = ps_gtspec
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
             
			SELECT MAX(TREADWEARID) 
				INTO li_treadwearid
            FROM TREADWEARHDR TW
            WHERE CERTIFICATEID = pi_certificateid 
				AND TW.CERTIFICATIONTYPEID = pi_certificationtypeid;
            
			DELETE 
			FROM TREADWEARDTL 
			WHERE TREADWEARID = li_treadwearid;
			
            pi_treadwearid := li_treadwearid;
			 
		ELSE
		
            INSERT INTO TREADWEARHDR(TREADWEARID,
									 PROJECTNUMBER,
									 TIRENUMBER,
									 TESTSPEC,
									 COMPLETIONDATE,
									 DOTSERIALNUMBER,
									 LOWESTWEARBAR,
									 PASSYN,
									 CERTIFICATIONTYPEID,
									 SERIALDATE   ,
									 MFGWWYY,
									 INDICATORSREQUIREMENT,
									 CERTIFICATEID,
									 MATL_NUM,
									 SKU,
									 OPERNUM,
									 GTSPEC)
							  VALUES(TREADWEARID_SEQ.NEXTVAL,
									 ps_projectnumber,
									 pi_tirenumber,
									 ps_testspec,
									 pd_completiondate,
									 ps_dotserialnumber,
									 pi_lowestwearbar,
									 ps_passyn,
									 pi_certificationtypeid,
									 pd_serialdate,
									 ps_mfgwwyy,
									 pi_indicatorsrequirement,
									 pi_certificateid,
									 ls_matl_num,  -- As per PRJ3617,inserting Matl_Num
									 ls_sku,
									 ps_operation,  -- As per PRJ3617,inserting operation
									 ps_gtspec);
            
			SELECT MAX(TREADWEARID) 
				INTO li_treadwearid
            FROM TREADWEARHDR TW
            WHERE CERTIFICATEID = pi_certificateid 
				AND TW.CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            pi_treadwearid := li_treadwearid;
			 
        END IF;
		
        COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - TREADWEAR_SAVE. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.TREADWEAR_SAVE',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		when li_parametersareinvalid then
            ls_errormsg := SQLERRM || ' - TREADWEAR_SAVE. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.TREADWEAR_SAVE',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' - TREADWEAR_SAVE. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.TREADWEAR_SAVE',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END TREADWEAR_SAVE;

	PROCEDURE TREADWEARDETAIL_SAVE(pi_treadwearid 	IN NUMBER,
                                   pi_wearbarheight	IN TREADWEARDTL.WEARBARHEIGHT%TYPE,
                                   pi_iteration 	IN TREADWEARDTL.ITERATION%TYPE,
                                   ps_operatorname 	IN VARCHAR2) 
	AS
	--EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_treadweardetilexists VARCHAR2(1) := NULL;
    li_totdetail NUMBER := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF pi_treadwearid IS NULL OR pi_wearbarheight IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		
        IF pi_treadwearid <= 0 THEN
            RAISE li_parametersareinvalid;
        END IF;
		
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
            ls_operatorid := ps_operatorname;
        END IF;
		
		INSERT INTO TREADWEARDTL(TREADWEARID,
								 WEARBARHEIGHT,
								 ITERATION,
								 createdby)
						  VALUES(pi_treadwearid,
								 pi_wearbarheight,
								 pi_iteration,
								 ls_operatorid);
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - TREADWEARDETAIL_SAVE. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.TREADWEARDETAIL_SAVE',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || ' - TREADWEARDETAIL_SAVE. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.TREADWEARDETAIL_SAVE',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := ' - TREADWEARDETAIL_SAVE. An error have ocurred.(when others)' || SQLERRM ;
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.TREADWEARDETAIL_SAVE',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END TREADWEARDETAIL_SAVE;

	PROCEDURE PLUNGER_SAVE(pi_plungerid 			  OUT NUMBER,
                           ps_projectnumber 		IN    VARCHAR2,
                           pi_tirenumber 			IN    NUMBER,
                           ps_testspec 				IN    VARCHAR2,
                           pd_completiondate 		IN    DATE,
                           ps_dotserialnumber 		IN    VARCHAR2,
                           pi_avgbreakingenergy 	IN    NUMBER,
                           ps_passyn 				IN    VARCHAR2,
                           pi_certificationtypeid	IN    NUMBER,
                           pd_serialdate 			IN    DATE,
                           ps_mfgwwyy    			IN    VARCHAR2,
                           pi_minplunger 			IN    NUMBER,
                           ps_operatorname 			IN    VARCHAR2 ,
                           pi_certificateid 		IN    NUMBER,
                           ps_matl_num 				IN    VARCHAR2,
                           ps_operation 			IN    VARCHAR2,
                           ps_gtspec 				IN    VARCHAR2) 
	AS
	/******************************************************************************
     NAME:       PLUNGER_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
                                              Added Lpad(matl_num) in the update,insert
                                              and in where condition of select list
                                             2. Added logic to get SKU to insert in Product table
    1.2        10/18/2012    Harini         1.Added Operation while updating and inserting.
    1.3        10/30/2012    Harini         1.Included NO_DATA_FOUND EXCEPTION block when no 
                                             SKU available for given Matl_Num and assign empty 
                                             for ls_sku parameter
    1.4        11/04/2013     Harini       1.As per IDEA2706,Modified procedure by adding 
                                           ps_gtspec and ps_mfgwwyy parameters in Input and add 
                                           these paramters while inserting/Updating
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
    ls_plungerexists VARCHAR2(1) := NULL;
    li_certificationtypeid INTEGER := NULL;
    li_plungerid NUMBER := NULL;
	
    --varible
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_sku PRODUCT.SKU%TYPE := NULL;
    ls_matl_num PRODUCT.MATL_NUM%TYPE := NULL;
	
	BEGIN
	
        IF pi_certificationtypeid IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		 
        IF pi_certificationtypeid <= 0 THEN
			RAISE li_parametersareinvalid;
        END IF;
		 
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid:= ps_operatorname;
        END IF;
         
        ls_matl_num := LPAD(ps_matl_num, 18, 0);
		
        ls_plungerexists := TESTRESULTS_CRUD.CHECKIFPLUNGEREXISTS(pi_certificateid   		=> pi_certificateid ,
                                                                  pi_certificationtypeid 	=> pi_certificationtypeid);
																	
		BEGIN                                                         
			SELECT ATTRIB_VALUE 
				INTO ls_sku 
			FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE, ' ')) AS ATTRIB_VALUE 
                  FROM MATERIAL_ATTRIBUTE
                  WHERE ATTRIB_NAME = 'LEGACY_COOPER_SKU'
					AND MATL_NUM = ls_matl_num);   
        EXCEPTION
			WHEN NO_DATA_FOUND THEN
				ls_sku := '';
        END; 
      
        IF ls_plungerexists = 'y' THEN
            UPDATE PLUNGERHDR 
			SET PROJECTNUMBER      = ps_projectnumber ,
                TIRENUMBER         = pi_tirenumber,
                TESTSPEC           = ps_testspec,
                COMPLETIONDATE     = pd_completiondate,
                DOTSERIALNUMBER    = ps_dotserialnumber,
                AVGBREAKINGENERGY  = pi_avgbreakingenergy,
                PASSYN             = ps_passyn,
                SERIALDATE         = pd_serialdate ,
                MFGWWYY            = ps_mfgwwyy,
                MINPLUNGER         = pi_minplunger,
                MODIFIEDON         = SYSDATE,
                MODIFIEDBY         = ls_operatorid,
                MATL_NUM           = ls_matl_num,    -- As per PRJ 3617,updating Matl_Num
                SKU                = ls_sku,
                OPERNUM            = ps_operation, --As per PRJ3617, added OperNum while updating
                GTSPEC             = ps_gtspec
			WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
           
			SELECT MAX(P.PLUNGERID) 
				INTO li_plungerid
			FROM PLUNGERHDR P
			WHERE CERTIFICATEID = pi_certificateid 
				And CERTIFICATIONTYPEID = pi_certificationtypeid;
           
			DELETE 
			FROM PLUNGERDTL 
			WHERE PLUNGERID = li_plungerid;
           
			pi_plungerid := li_plungerid;
			
        ELSE
            INSERT INTO PLUNGERHDR(PLUNGERID,
								   PROJECTNUMBER,
								   TIRENUMBER,
								   TESTSPEC,
								   COMPLETIONDATE,
								   DOTSERIALNUMBER,
								   AVGBREAKINGENERGY,
								   PASSYN,
								   CERTIFICATIONTYPEID,
								   SERIALDATE,
								   MFGWWYY,
								   MINPLUNGER,
								   CERTIFICATEID,
								   MATL_NUM,
								   SKU,
								   OPERNUM,
								   GTSPEC)
							VALUES(PLUNGERID_SEQ.NEXTVAL,
								   ps_projectnumber,
								   pi_tirenumber,
								   ps_testspec,
								   pd_completiondate,
								   ps_dotserialnumber,
								   pi_avgbreakingenergy,
								   ps_passyn,
								   pi_certificationtypeid,
								   pd_serialdate,
								   ps_mfgwwyy,
								   pi_minplunger,
								   pi_certificateid,
								   ls_matl_num,       -- As per PRJ 3617,Inserting Matl_Num
								   ls_sku,
								   ps_operation,     -- As per PRJ3617,inserting operation 
								   ps_gtspec);
            
			SELECT MAX(P.PLUNGERID) 
				INTO li_plungerid
            FROM PLUNGERHDR P
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
				
			pi_plungerid:=li_plungerid;
			
        END IF;
		
        COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '-PLUNGER_Save . There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.PLUNGER_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
			
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '-PLUNGER_Save . There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.PLUNGER_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '-PLUNGER_Save . An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.PLUNGER_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END PLUNGER_SAVE;

	PROCEDURE PLUNGERDETAIL_SAVE(pi_breakingenergy	IN NUMBER,
                                 pi_plungerid  		IN NUMBER,
                                 pi_iteration 		IN NUMBER,
                                 ps_operatorname 	IN VARCHAR2) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_PLUNGERDETAILExists VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF pi_plungerid IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		  
        IF pi_plungerid <= 0 THEN
			RAISE li_parametersareinvalid;
        END IF;
		  
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
        END IF;
		 
        INSERT INTO PLUNGERDTL(BREAKINGENERGY,
							   PLUNGERID,
							   ITERATION,
							   CREATEDBY)
						VALUES(pi_breakingenergy,
							   pi_plungerid ,
							   pi_iteration,
							   ls_operatorid);
							   
		COMMIT;
			  
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - PLUNGERDETAIL_SAVE. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.PLUNGERDETAIL_SAVE',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
         
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || ' - PLUNGERDETAIL_SAVE. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.PLUNGERDETAIL_SAVE',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' - PLUNGERDETAIL_SAVE. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.PLUNGERDETAIL_SAVE',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END PLUNGERDETAIL_SAVE;

	PROCEDURE BEADUNSEAT_SAVE(pi_beadunseatid 			  OUT NUMBER,
                              ps_projectnumber 			IN    VARCHAR2,
                              pi_tirenumber 			IN    NUMBER,
                              ps_testspec 				IN    VARCHAR2,
                              pd_completiondate 		IN    DATE,
                              ps_dotserialnumber 		IN    VARCHAR2,
                              pi_lowestunseatvalue 		IN    NUMBER,
                              ps_passyn 				IN    VARCHAR2,
                              pi_certificationtypeid	IN    NUMBER,
                              pd_serialdate 			IN    DATE,
                              ps_mfgwwyy    			IN    VARCHAR2,
                              pi_minbeadunseat 			IN    NUMBER,
                              ps_testpassfail 			IN    VARCHAR2,
                              ps_operatorname   		IN    VARCHAR2,
                              pi_certificateid 			IN    NUMBER,
                              ps_matl_num 				IN    VARCHAR2,
                              ps_operation 				IN    VARCHAR2,                            
                              ps_gtspec 				IN    VARCHAR2) 
	AS
    /******************************************************************************
     NAME:       BeadUnseat_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
                                              Added Lpad(matl_num) in the update,insert
                                              and in where condition of select list
                                             2. Added logic to get SKU to insert in Product table
     1.2        10/18/2012    Harini         1.Added Operation while updating and inserting. 
     1.3        10/30/2012    Harini         1.Included NO_DATA_FOUND EXCEPTION block when no 
                                             SKU available for given Matl_Num and assign empty 
                                             for ls_sku parameter
     1.4        11/04/2013     Harini       1.As per IDEA2706,Modified procedure by adding 
                                           ps_gtspec and ps_mfgwwyy parameters in Input and add 
                                           these paramters while inserting/Updating
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_beadunseatexists VARCHAR2(1) := NULL;
    li_beadunseatid BEADUNSEATHDR.BEADUNSEATID%TYPE := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_sku PRODUCT.SKU%TYPE := NULL;
    ls_matl_num PRODUCT.MATL_NUM%TYPE := NULL;
	
	BEGIN
   
        IF pi_certificationtypeid IS NULL THEN
            RAISE li_parametersarenull;
        END IF;
		  
        IF pi_certificationtypeid <= 0 THEN
            RAISE li_parametersareinvalid;
        END IF;
		  
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
            ls_operatorid := ps_operatorname;
        END IF;
		  
        ls_matl_num := LPAD(ps_matl_num, 18, 0);
		  
        ls_beadunseatexists := TESTRESULTS_CRUD.CHECKIFBEADUNSEATEXISTS(pi_certificateid   		=> pi_certificateid,
                                                                        pi_certificationtypeid 	=> pi_certificationtypeid);
																	
		BEGIN                                                         
			SELECT ATTRIB_VALUE 
				INTO ls_sku 
			FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE, ' ')) AS ATTRIB_VALUE 
                  FROM MATERIAL_ATTRIBUTE
                  WHERE ATTRIB_NAME = 'LEGACY_COOPER_SKU'
					AND MATL_NUM = ls_matl_num);   
		EXCEPTION
			WHEN NO_DATA_FOUND THEN
				ls_sku := '';
		END;
      
		IF ls_beadunseatexists = 'y' THEN
			UPDATE BEADUNSEATHDR 
			SET PROJECTNUMBER     = ps_projectnumber,
                TIRENUMBER        = pi_tirenumber,
                TESTSPEC          = ps_testspec,
                COMPLETIONDATE    = pd_completiondate,
                DOTSERIALNUMBER   = ps_dotserialnumber,
                LOWESTUNSEATVALUE = pi_lowestunseatvalue,
                PASSYN            = ps_passyn,
                CERTIFICATIONTYPEID = pi_certificationtypeid,
                SERIALDATE        = pd_serialdate,
                MFGWWYY           = ps_mfgwwyy,
                MINBEADUNSEAT     = pi_minbeadunseat,
                TESTPASSFAIL      = ps_testpassfail,
                MODIFIEDBY        = ls_operatorid,
                MODIFIEDON        = SYSDATE,
                MATL_NUM          = ls_matl_num,  -- As per PRJ3617,Updating Matl_Num
                SKU               = ls_sku,
                OPERNUM           = ps_operation, --As per PRJ3617, added OperNum while updating
                GTSPEC            = ps_gtspec
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid ;
			
			SELECT MAX(BEADUNSEATID) 
				INTO li_beadunseatid
            FROM BEADUNSEATHDR BUH
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid ;
            
			DELETE 
			FROM BEADUNSEATDTL 
			WHERE BEADUNSEATID = li_beadunseatid;
			
            pi_beadunseatid:=  li_beadunseatid;
			 
		ELSE
            INSERT INTO BEADUNSEATHDR(BEADUNSEATID,
									  PROJECTNUMBER,
									  TIRENUMBER,
									  TESTSPEC,
									  COMPLETIONDATE,
									  DOTSERIALNUMBER,
									  LOWESTUNSEATVALUE,
									  PASSYN,
									  CERTIFICATIONTYPEID,
									  SERIALDATE,
									  MFGWWYY,
									  MINBEADUNSEAT,
									  CREATEDBY,
									  TESTPASSFAIL,
									  CERTIFICATEID,
									  MATL_NUM,
									  SKU,
									  OPERNUM,
									  GTSPEC)
							   VALUES(BEADUNSEATID_SEQ.NEXTVAL,
									  ps_projectnumber,
									  pi_tirenumber,
									  ps_testspec,
									  pd_completiondate,
									  ps_dotserialnumber,
									  pi_lowestunseatvalue,
									  ps_passyn,
									  pi_certificationtypeid,
									  pd_serialdate,
									  ps_mfgwwyy,
									  pi_minbeadunseat,
									  ls_operatorid,
									  ps_testpassfail,
									  pi_certificateid,
									  ls_matl_num, -- As per PRJ3617,Inserting Matl_Num
									  ls_sku,
									  ps_operation, -- As per PRJ3617,inserting operation
									  ps_gtspec);
            
			--Gets the Id that just was inserted on the table to be returned
			SELECT MAX(BEADUNSEATID) 
				INTO li_beadunseatid
            FROM BEADUNSEATHDR BUH
			WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            pi_beadunseatid:=  li_beadunseatid;
			 
		END IF;
		
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - BeadUnseat_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.BeadUnseat_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || ' - BeadUnseat_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.BeadUnseat_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' - BeadUnseat_Save.  An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.BeadUnseat_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END BEADUNSEAT_SAVE;
   
	PROCEDURE BEADUNSEATDETAIL_SAVE(pi_beadunseatid IN NUMBER,
                                    pi_unseatforce 	IN NUMBER,
                                    pi_iteration 	IN NUMBER,
                                    ps_operatorname	IN VARCHAR2) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_beadunseatdetailexists VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF pi_beadunseatid IS NULL THEN
			RAISE li_parametersarenull;
        END IF;
		
        IF pi_beadunseatid <=0 THEN
			RAISE li_parametersareinvalid;
        END IF;
		
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
            ls_operatorid := ps_operatorname ;
        END IF;
		  
        INSERT INTO BEADUNSEATDTL(BEADUNSEATID, 
								  UNSEATFORCE,
								  ITERATION,
								  CREATEDBY)
						   VALUES(pi_beadunseatid, 
								  pi_unseatforce,
								  pi_iteration,
								  ls_operatorid);
								  
        COMMIT;
		
    EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - BeadUnseatDetail_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.BeadUnseatDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || ' - BeadUnseatDetail_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.BeadUnseatDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' - BeadUnseatDetail_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.BeadUnseatDetail_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
   
	END BEADUNSEATDETAIL_SAVE;
   
	PROCEDURE HIGHSPEEDHDR_SAVE(pi_highspeedid          		  OUT NUMBER,
                                ps_projectnumber        		IN    VARCHAR2,
                                pi_tirenum              		IN    NUMBER,
                                ps_testspec             		IN    VARCHAR2,
                                pd_competiondate        		IN    DATE,
                                ps_dotserialnumber      		IN    VARCHAR2,
                                ps_mfgwwyy              		IN    VARCHAR2,
                                pd_precondstartdate     		IN    DATE,
                                pi_precondsarttemp      		IN    NUMBER,
                                pd_precondtime          		IN    HIGHSPEEDHDR.PRECONDTIME%TYPE,
                                pi_rimdiameter          		IN    HIGHSPEEDHDR.RIMDIAMETER%TYPE,
                                pi_rimwidth             		IN    HIGHSPEEDHDR.RIMWIDTH%TYPE,
                                pd_precondenddate       		IN    DATE,
                                pi_precondendtemp       		IN    NUMBER,
                                pi_inflationpressure    		IN    NUMBER,
                                pi_beforediameter       		IN    HIGHSPEEDHDR.BEFOREDIAMETER%TYPE,
                                pi_afterdiameter        		IN    HIGHSPEEDHDR.AFTERDIAMETER%TYPE,
                                pi_beforeinflation      		IN    NUMBER,
                                pi_afterinflation       		IN    NUMBER,
                                pi_wheelposition        		IN    NUMBER,
                                pi_wheelnumber          		IN    NUMBER,
                                pi_finaltemp            		IN    NUMBER,
                                pi_finaldistance        		IN    HIGHSPEEDHDR.FINALDISTANCE%TYPE,
                                pi_finalinflation       		IN    NUMBER ,
                                pd_postcondstartdate    		IN    DATE,
                                pd_postcondenddate      		IN    DATE,
                                pi_postcondendtemp      		IN    NUMBER ,
                                ps_passyn               		IN    VARCHAR2,
                                pd_serialdate           		IN    DATE,
                                pi_postcondtime         		IN    HIGHSPEEDHDR.POSTCONDTIME%TYPE,
                                pi_certificationtypeid  		IN    NUMBER,
                                pi_diametertestdrum     		IN    NUMBER,
                                pi_precondtemp          		IN    NUMBER,
                                pi_inflationpressurereadjusted	IN    NUMBER,
                                pi_circunferencebeforetest     	IN    NUMBER,
                                pi_wheelspeedrpm        		IN    NUMBER,
                                pi_wheelspeedkmh        		IN    NUMBER,
                                pi_circunferenceaftertest 		IN    NUMBER,
                                pi_oddiference            		IN    NUMBER,
                                pi_oddiferencetolerance   		IN    NUMBER,
                                ps_serienom               		IN    VARCHAR2,
                                ps_finaljudgement         		IN    VARCHAR2,
                                ps_approver               		IN    VARCHAR2,
                                pi_passatkmh              		IN    NUMBER,
                                ps_speedttestpassfail     		IN    VARCHAR2,
                                pi_speedtotaltime         		IN    NUMBER,
                                pi_maxspeed     				IN    NUMBER,
                                pi_maxload      				IN    NUMBER,
                                ps_operatorname 				IN    VARCHAR2,
                                pi_certificateid 				IN    NUMBER,
                                ps_matl_num  					IN    VARCHAR2,
                                ps_operation 					IN    VARCHAR2,
                                ps_gtspec 						IN    VARCHAR2) 
	AS
	/******************************************************************************
     NAME:       HighSpeedHdr_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
                                              Added Lpad(matl_num) in the update,insert
                                              and in where condition of select list
                                             2. Added logic to get SKU to insert in Product table
     1.2        10/18/2012    Harini         1.Added Operation while updating and inserting.
     1.3        10/30/2012    Harini         1.Included NO_DATA_FOUND EXCEPTION block when no 
                                             SKU available for given Matl_Num and assign empty 
                                             for ls_sku parameter
     1.4        11/04/2013     Harini       1.As per IDEA2706,Modified procedure by adding 
                                              ps_gtspec parameter in Input and add this
                                              paramter while inserting/Updating 
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull,-20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid,-20006);
	
    --varible
    ls_highspeedexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_currenthighspeedid HIGHSPEEDHDR.HIGHSPEEDID%TYPE := NULL;
    li_postcondtime HIGHSPEEDHDR.POSTCONDTIME%TYPE := NULL;
    ls_sku PRODUCT.SKU%TYPE := NULL;
    ls_matl_num PRODUCT.MATL_NUM%TYPE := NULL;
	
	BEGIN
	
        IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname ;
        END IF;
		  
        ls_matl_num := LPAD(ps_matl_num, 18, 0);
		  
        ls_highspeedexist := TESTRESULTS_CRUD.CHECKIFHIGHSPEEDEXISTS(pi_certificateid   		=> pi_certificateid ,
																	 pi_certificationtypeid 	=> pi_certificationtypeid);
													  
      
		BEGIN                                                         
			SELECT ATTRIB_VALUE 
				INTO ls_sku 
			FROM (SELECT TO_CHAR(NVL(ATTRIB_VALUE,' ')) AS ATTRIB_VALUE 
                  FROM MATERIAL_ATTRIBUTE
                  WHERE ATTRIB_NAME ='LEGACY_COOPER_SKU'
					AND MATL_NUM =ls_matl_num);   
        EXCEPTION
			WHEN NO_DATA_FOUND THEN
				ls_sku := '';
        END;
   
		--jeseitz added 7/29/13 because SAP was bringing back and incorrectly formatted
        IF pi_postcondtime > 9999 THEN
            li_postcondtime := 0;
        ELSE
            li_postcondtime := pi_postcondtime;
        END IF;           
   
        IF ls_highspeedexist = 'y' THEN
            UPDATE HIGHSPEEDHDR 
			SET PROJECTNUMBER               = ps_projectnumber,
                TIRENUM                     = pi_tirenum,
                TESTSPEC                    = ps_testspec,
                COMPETIONDATE               = pd_competiondate,
                DOTSERIALNUMBER             = ps_dotserialnumber,
                MFGWWYY                     = ps_mfgwwyy,
                PRECONDSTARTDATE            = pd_precondstartdate,
                PRECONDSARTTEMP             = pi_precondsarttemp,
                RIMDIAMETER                 = pi_rimdiameter,
                RIMWIDTH                    = pi_rimwidth,
                PRECONDENDDATE              = pd_precondenddate,
                PRECONDENDTEMP              = pi_precondendtemp,
                INFLATIONPRESSURE           = pi_inflationpressure,
                BEFOREDIAMETER              = pi_beforediameter,
                AFTERDIAMETER               = pi_afterdiameter,
                BEFOREINFLATION             = pi_beforeinflation,
                AFTERINFLATION              = pi_afterinflation,
                WHEELPOSITION               = pi_wheelposition,
                WHEELNUMBER                 = pi_wheelnumber,
                FINALTEMP                   = pi_finaltemp,
                FINALDISTANCE               = pi_finaldistance,
                FINALINFLATION              = pi_finalinflation,
                POSTCONDSTARTDATE           = pd_postcondstartdate,
                POSTCONDENDDATE             = pd_postcondenddate,
                POSTCONDENDTEMP             = pi_postcondendtemp,
                PASSYN                      = ps_passyn,
                SERIALDATE                  = pd_serialdate,
                POSTCONDTIME                = li_postcondtime,
                MODIFIEDBY                  = ls_operatorid,
                MODIFIEDON                  = SYSDATE,
                DIAMETERTESTDRUM            = pi_diametertestdrum,
                PRECONDTEMP                 = pi_precondtemp,
                INFLATIONPRESSUREREADJUSTED = pi_inflationpressurereadjusted,
                CIRCUNFERENCEBEFORETEST     = pi_circunferencebeforetest,
                WHEELSPEEDRPM               = pi_wheelspeedrpm,
                WHEELSPEEDKMH               = pi_wheelspeedkmh,
                CIRCUNFERENCEAFTERTEST      = pi_circunferenceaftertest,
                ODDIFERENCE                 = pi_oddiference,
                ODDIFERENCETOLERANCE        = pi_oddiferencetolerance,
                SERIENOM                    = ps_serienom,
                FINALJUDGEMENT              = ps_finaljudgement,
                APPROVER                    = ps_approver,
                PASSATKMH                   = pi_passatkmh,
                SPEEDTTESTPASSFAIL          = ps_speedttestpassfail,
                SPEEDTOTALTIME              = pi_speedtotaltime,
                MAXSPEED                    = pi_maxspeed,
                MAXLOAD                     = pi_maxload,
                MATL_NUM                    = ls_matl_num,  -- As per PRJ3617,updating Matl_Num
                SKU                         = ls_sku,
                OPERNUM                     = ps_operation, --As per PRJ3617, added OperNum while updating
                GTSPEC                      = ps_gtspec
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
            
			SELECT MAX(HIGHSPEEDID) 
				INTO li_currenthighspeedid
            FROM  HIGHSPEEDHDR H
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            DELETE 
			FROM HIGHSPEEDDTL 
			WHERE HIGHSPEEDID = li_currenthighspeedid;
			
            DELETE 
			FROM SPEEDTESTDETAIL 
			WHERE HIGHSPEEDID = li_currenthighspeedid;
			  
               pi_highspeedid :=  li_currenthighspeedid;
			   
        ELSE
            INSERT INTO HIGHSPEEDHDR(HIGHSPEEDID,
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
									 CERTIFICATIONTYPEID,
									 CREATEDBY,
									 CREATEDON,
									 DIAMETERTESTDRUM,
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
									 MAXLOAD,
									 CERTIFICATEID,
									 MATL_NUM,
									 SKU,
									 OPERNUM,
									 GTSPEC)
							  VALUES(HIGHSPEEDID_SEQ.NEXTVAL,
									 ps_projectnumber,
									 pi_tirenum,
									 ps_testspec,
									 pd_competiondate,
									 ps_dotserialnumber,
									 ps_mfgwwyy,
									 pd_precondstartdate,
									 pi_precondsarttemp,
									 pi_rimdiameter,
									 pi_rimwidth ,
									 pd_precondenddate,
									 pi_precondendtemp ,
									 pi_inflationpressure,
									 pi_beforediameter,
									 pi_afterdiameter,
									 pi_beforeinflation,
									 pi_afterinflation,
									 pi_wheelposition,
									 pi_wheelnumber,
									 pi_finaltemp,
									 pi_finaldistance,
									 pi_finalinflation,
									 pd_postcondstartdate,
									 pd_postcondenddate ,
									 pi_postcondendtemp ,
									 ps_passyn ,
									 pd_serialdate ,
									 li_postcondtime ,
									 pi_certificationtypeid,
									 ls_operatorid,
									 SYSDATE,
									 pi_diametertestdrum,
									 pi_precondtemp,
									 pi_inflationpressurereadjusted,
									 pi_circunferencebeforetest,
									 pi_wheelspeedrpm,
									 pi_wheelspeedkmh,
									 pi_circunferenceaftertest,
									 pi_oddiference,
									 pi_oddiferencetolerance,
									 ps_serienom,
									 ps_finaljudgement,
									 ps_approver,
									 pi_passatkmh,
									 ps_speedttestpassfail,
									 pi_speedtotaltime,
									 pi_maxspeed,
									 pi_maxload,
									 pi_certificateid,
									 ls_matl_num, -- As per PRJ3617,INserting Matl_Num
									 ls_sku,
									 ps_operation,   -- As per PRJ3617,inserting operation
									 ps_gtspec);
                 
			SELECT MAX(HIGHSPEEDID) 
				INTO li_currenthighspeedid
            FROM HIGHSPEEDHDR H
            WHERE CERTIFICATEID = pi_certificateid 
				AND H.CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            DELETE 
			FROM HIGHSPEEDDTL 
			WHERE HIGHSPEEDID = li_currenthighspeedid;
				 
            DELETE 
			FROM SPEEDTESTDETAIL 
			WHERE HIGHSPEEDID = li_currenthighspeedid;
				 
            pi_highspeedid :=  li_currenthighspeedid;
				 
        END IF;
		  
        COMMIT;
		  
	EXCEPTION
        
		WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || 'HighSpeedHdr_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HighSpeedHdr_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || 'HighSpeedHdr_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HighSpeedHdr_Save',
													  ax_recorddata    	=> 'ps_matl_num is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || 'HighSpeedHdr_Save. An error have ocurred.(when others)';
               
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HighSpeedHdr_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
   
	END HIGHSPEEDHDR_SAVE;
   
	PROCEDURE HIGHSPEEDDETAIL_SAVE(pi_highspeedid 			IN NUMBER,
                                   pi_teststep 				IN NUMBER,
                                   pi_timeinmin 			IN NUMBER,
                                   pi_speed 				IN HIGHSPEEDDTL.SPEED%TYPE,
                                   pi_totmiles 				IN HIGHSPEEDDTL.TOTMILES%TYPE,
                                   pi_load 					IN HIGHSPEEDDTL.LOAD%TYPE,
                                   pi_loadpercent 			IN NUMBER,
                                   pi_setinflation 			IN NUMBER,
                                   pi_ambtemp 				IN NUMBER,
                                   pi_infpressure 			IN NUMBER,
                                   pd_stepcompletiondate	IN HIGHSPEEDDTL.STEPCOMPLETIONDATE%TYPE,
                                   ps_operatorid 			IN VARCHAR2) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull,-20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid,-20006);
	
    --varible
    ls_highspeedexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF ps_operatorid IS NOT NULL OR ps_operatorid <> '' THEN
			ls_operatorid := ps_operatorid;
        END IF;
		
        INSERT INTO HIGHSPEEDDTL(HIGHSPEEDID,
								 TESTSTEP,
								 TIMEINMIN,
								 SPEED,
								 TOTMILES,
								 LOAD,
								 LOADPERCENT,
								 SETINFLATION,
								 AMBTEMP,
								 INFPRESSURE,
								 STEPCOMPLETIONDATE,
								 CREATEDBY)
						  VALUES(pi_highspeedid,
								 pi_teststep,
								 pi_timeinmin,
								 pi_speed,
								 pi_totmiles,
								 pi_load,
								 pi_loadpercent,
								 pi_setinflation,
								 pi_ambtemp,
								 pi_infpressure,
								 pd_stepcompletiondate,
								 ls_operatorid);
        COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || '- HighSpeedDetail_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HighSpeedDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || '- HighSpeedDetail_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HighSpeedDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
		
		WHEN DUP_VAL_ON_INDEX THEN
            ---this happens when inserting multiple highspeed recoreds with step of 0
            ---no error if step = 0
            IF pi_teststep <> 0 THEN
				ls_errormsg := SQLERRM || '- HighSpeedDetail_Save. Duplicate step number.';
				
				APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
														  ad_operatorid 	=> ls_operatorid,
														  ad_daterecorded  	=> SYSDATE,
														  as_processname   	=> ' testresults_crud.HighSpeedDetail_Save '||pi_teststep,
														  ax_recorddata    	=> ' unique constraint error ',
														  as_messagecode   	=> TO_CHAR(SQLCODE),
														  as_message       	=> ls_errormsg);
				
				RAISE_APPLICATION_ERROR (-20007, ls_errormsg);           
			END IF;   
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- HighSpeedDetail_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HighSpeedDetail_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR (-20007, ls_errormsg);
   
	END HIGHSPEEDDETAIL_SAVE;
   
	PROCEDURE HIGHSPEED_SPEEDTESTDETAIL_SAVE(pi_iteration 		IN NUMBER,
                                             pd_time 			IN DATE,
                                             pi_speed 			IN NUMBER,
                                             pi_highspeedid 	IN NUMBER,
                                             ps_operatorname	IN VARCHAR2) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_highspeedexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
		IF pi_highspeedid IS NULL OR pi_highspeedid <= 0 THEN
			RAISE li_parametersarenull;
		END IF;
		
		IF pi_highspeedid <= 0 THEN
			RAISE li_parametersareinvalid;
		END IF;
		
		IF ps_operatorname IS NOT NULL OR ps_operatorname <> '' THEN
			ls_operatorid := ps_operatorname;
        END IF;
		
        INSERT INTO SPEEDTESTDETAIL(ITERATION,
									TIME,
									SPEED,
									HIGHSPEEDID,
									CREATEDBY)
							 VALUES(pi_iteration,
									pd_time,
									pi_speed,
									pi_highspeedid,
									ls_operatorid);
        COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - HIghSpeed_SpeedTestDetail_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HIghSpeed_SpeedTestDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || ' - HIghSpeed_SpeedTestDetail_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HIghSpeed_SpeedTestDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' - HIghSpeed_SpeedTestDetail_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.HIghSpeed_SpeedTestDetail_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END HIGHSPEED_SPEEDTESTDETAIL_SAVE;
	
	PROCEDURE SOUNDHDR_SAVE(ps_userid                     IN VARCHAR2,
                            pi_soundid                    OUT NUMBER,
                            ps_projectnumber              IN VARCHAR2,
                            pi_tirenumber                 IN NUMBER,
                            ps_testspec                   IN VARCHAR2,
                            ps_testreportnumber           IN VARCHAR2,
                            ps_manufactureandbrand        IN VARCHAR2,
                            ps_tireclass                  IN VARCHAR2,
                            ps_categoryofuse              IN VARCHAR2,
                            pd_dateoftest                 IN DATE,
                            ps_testvehicule               IN VARCHAR2,
                            ps_testvehiculewheelbase      IN VARCHAR2,
                            ps_locationoftesttrack        IN VARCHAR2,
                            pd_datetrackcertiftoiso       IN DATE,
                            ps_tiresizedesignation        IN VARCHAR2,
                            ps_tireservicedescription     IN VARCHAR2,
                            ps_testmass_frontl            IN VARCHAR2,
                            ps_testmass_frontr            IN VARCHAR2,
                            ps_testmass_rearl             IN VARCHAR2,
                            ps_testmass_rearr             IN VARCHAR2,
                            ps_tireloadindex_frontl       IN VARCHAR2,
                            ps_tireloadindex_frontr       IN VARCHAR2,
                            ps_tireloadindex_rearl        IN VARCHAR2,
                            ps_tireloadindex_rearr        IN VARCHAR2,
                            ps_inflationpressureco_frontl IN VARCHAR2,
                            ps_inflationpressureco_frontr IN VARCHAR2,
                            ps_inflationpressureco_rearl  IN VARCHAR2,
                            ps_inflationpressureco_rearr  IN VARCHAR2,
                            ps_testrimwidthcode           IN VARCHAR2,
                            ps_tempmeasuresensortype      IN VARCHAR2,
                            pi_certificationtypeid        IN NUMBER,
                            pi_skuid                      IN NUMBER,
                            ps_referenceinflationpressure IN VARCHAR2,
                            pi_certificateid              IN NUMBER,
                            ps_operation                  IN VARCHAR2,
                            ps_mfgwwyy                    IN VARCHAR2,
                            ps_gtspec                     IN VARCHAR2) 
	AS
	/******************************************************************************
     NAME:       SoundHDR_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/18/2012    Harini         1.Added Operation while updating and inserting.
     1.4        11/04/2013    Harini         1.As per IDEA2706,Modified procedure by adding 
                                             ps_gtspec and ps_mfgwwyy parameters in Input and add 
                                             these paramters while inserting/Updating
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_soundexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50):= 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_currentsoundid SOUNDHDR.SOUNDID%TYPE := NULL;
	
	BEGIN
	
        IF ps_userid IS NOT NULL OR ps_userid <> '' THEN
			ls_operatorid := ps_userid;
        END IF;
		
        ls_soundexist := TESTRESULTS_CRUD.CHECKIFSOUNDEXIXTS(pi_certificateid   => pi_certificateid ,
															 pi_certificationtypeid => pi_certificationtypeid);
													  
        IF ls_soundexist = 'y' THEN
            UPDATE SOUNDHDR 
			SET PROJECTNUMBER              = ps_projectnumber,
                TIRENUMBER                 = pi_tirenumber,
                TESTSPEC                   = ps_testspec,
                TESTREPORTNUMBER           = ps_testreportnumber,
                MANUFACTUREANDBRAND        = ps_manufactureandbrand,
                TIRECLASS                  = ps_tireclass,
                CATEGORYOFUSE              = ps_categoryofuse,
                DATEOFTEST                 = pd_dateoftest,
                TESTVEHICULE               = ps_testvehicule,
                TESTVEHICULEWHEELBASE      = ps_testvehiculewheelbase,
                LOCATIONOFTESTTRACK        = ps_locationoftesttrack,
                DATETRACKCERTIFTOISO       = pd_datetrackcertiftoiso,
                TIRESIZEDESIGNATION        = ps_tiresizedesignation,
                TIRESERVICEDESCRIPTION     = ps_tireservicedescription,
                TESTMASS_FRONTL            = ps_testmass_frontl,
                TESTMASS_FRONTR            = ps_testmass_frontr,
                TESTMASS_REARL             = ps_testmass_rearl,
                TESTMASS_REARR             = ps_testmass_rearr,
                TIRELOADINDEX_FRONTL       = ps_tireloadindex_frontl,
                TIRELOADINDEX_FRONTR       = ps_tireloadindex_frontr,
                TIRELOADINDEX_REARL        = ps_tireloadindex_rearl,
                TIRELOADINDEX_REARR        = ps_tireloadindex_rearr,
                INFLATIONPRESSURECO_FRONTL = ps_inflationpressureco_frontl,
                INFLATIONPRESSURECO_FRONTR = ps_inflationpressureco_frontr,
                INFLATIONPRESSURECO_REARL  = ps_inflationpressureco_rearl,
                INFLATIONPRESSURECO_REARR  = ps_inflationpressureco_rearr,
                TESTRIMWIDTHCODE           = ps_testrimwidthcode,
                TEMPMEASURESENSORTYPE      = ps_tempmeasuresensortype,
                MODIFIEDBY                 = ls_operatorid,
                MODIFIEDON                 = SYSDATE,
                REFERENCEINFLATIONPRESSURE = ps_referenceinflationpressure,
                OPERNUM                    = ps_operation, --As per PRJ3617, added OperNum while updating
                MFGWWYY                    = ps_mfgwwyy,
                GTSPEC                     = ps_gtspec
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
            
			SELECT MAX(SOUNDID) 
				INTO li_currentsoundid
            FROM SOUNDHDR S
            WHERE CERTIFICATEID = pi_certificateid 
				AND S.CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            DELETE 
			FROM SOUNDDETAIL 
			WHERE SOUNDID = li_currentsoundid;
			  
            pi_soundid :=  li_currentsoundid;
			   
        ELSE
            INSERT INTO SOUNDHDR(SOUNDID,
								 PROJECTNUMBER,
								 TIRENUMBER,
								 TESTSPEC,
								 TESTREPORTNUMBER,
								 MANUFACTUREANDBRAND,
								 TIRECLASS,
								 CATEGORYOFUSE,
								 DATEOFTEST,
								 TESTVEHICULE,
								 TESTVEHICULEWHEELBASE,
								 LOCATIONOFTESTTRACK,
								 DATETRACKCERTIFTOISO,
								 TIRESIZEDESIGNATION,
								 TIRESERVICEDESCRIPTION,
								 TESTMASS_FRONTL,
								 TESTMASS_FRONTR,
								 TESTMASS_REARL,
								 TESTMASS_REARR,
								 TIRELOADINDEX_FRONTL,
								 TIRELOADINDEX_FRONTR,
								 TIRELOADINDEX_REARL,
								 TIRELOADINDEX_REARR,
								 INFLATIONPRESSURECO_FRONTL,
								 INFLATIONPRESSURECO_FRONTR,
								 INFLATIONPRESSURECO_REARL,
								 INFLATIONPRESSURECO_REARR,
								 TESTRIMWIDTHCODE,
								 TEMPMEASURESENSORTYPE,
								 CERTIFICATIONTYPEID,
								 CREATEDBY,
								 CREATEDON,
								 MODIFIEDBY,
								 MODIFIEDON,
								 REFERENCEINFLATIONPRESSURE,
								 CERTIFICATEID,
								 OPERNUM,
								 MFGWWYY,
								 GTSPEC)
                          VALUES(SOUNDID_SEQ.NEXTVAL,
								 ps_projectnumber,
								 pi_tirenumber,
								 ps_testspec,
								 ps_testreportnumber,
								 ps_manufactureandbrand,
								 ps_tireclass,
								 ps_categoryofuse,
								 pd_dateoftest,
								 ps_testvehicule,
								 ps_testvehiculewheelbase,
								 ps_locationoftesttrack,
								 pd_datetrackcertiftoiso,
								 ps_tiresizedesignation,
								 ps_tireservicedescription,
								 ps_testmass_frontl,
								 ps_testmass_frontr,
								 ps_testmass_rearl,
								 ps_testmass_rearr,
								 ps_tireloadindex_frontl,
								 ps_tireloadindex_frontr,
								 ps_tireloadindex_rearl,
								 ps_tireloadindex_rearr,
								 ps_inflationpressureco_frontl,
								 ps_inflationpressureco_frontr,
								 ps_inflationpressureco_rearl,
								 ps_inflationpressureco_rearr,
								 ps_testrimwidthcode,
								 ps_tempmeasuresensortype,
								 pi_certificationtypeid,
								 ls_operatorid,
								 SYSDATE,
								 ls_operatorid,
								 SYSDATE,
								 ps_referenceinflationpressure,
								 pi_certificateid,
								 ps_operation,     -- As per PRJ3617,inserting operation 
								 ps_mfgwwyy,
								 ps_gtspec);
                             
			SELECT MAX(SOUNDID) 
				INTO li_currentsoundid
			FROM SOUNDHDR S
			WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
				
			pi_soundid := li_currentsoundid;
							
        END IF;
		
        COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || 'SoundHDR_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.SoundHDR_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || 'SoundHDR_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.SoundHDR_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || 'SoundHDR_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.SoundHDR_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
			
	END SOUNDHDR_SAVE;
	
	PROCEDURE SOUNDDETAIL_SAVE(ps_userid 					IN VARCHAR2,
                               pi_iteration 				IN NUMBER,
                               ps_testspeed  				IN VARCHAR2,
                               ps_directionofrun  			IN VARCHAR2,
                               ps_soundlevelleft  			IN VARCHAR2,
                               ps_soundlevelright  			IN VARCHAR2,
                               ps_airtemp  					IN VARCHAR2,
                               ps_tracktemp 				IN VARCHAR2,
                               ps_soundlevelleft_tempcor 	IN VARCHAR2,
                               ps_soundlevelright_tempcor	IN VARCHAR2,
                               pi_soundid 					IN NUMBER) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_highspeedexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF ps_userid IS NOT NULL OR ps_userid <> '' THEN
			ls_operatorid := ps_userid;
        END IF;
		
        INSERT INTO SOUNDDETAIL(ITERATION,
								TESTSPEED,
								DIRECTIONOFRUN,
								SOUNDLEVELLEFT,
								SOUNDLEVELRIGHT,
								AIRTEMP,
								TRACKTEMP,
								SOUNDLEVELLEFT_TEMPCORRECTED,
								SOUNDLEVELRIGHT_TEMPCORRECTED,
								SOUNDID,
								CREATEDBY,
								CREATEDON)
						 VALUES(pi_iteration,
								ps_testspeed,
								ps_directionofrun,
								ps_soundlevelleft,
								ps_soundlevelright,
								ps_airtemp,
								ps_tracktemp,
								ps_soundlevelleft_tempcor,
								ps_soundlevelright_tempcor,
								pi_soundid,
								ls_operatorid,
								SYSDATE);
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || 'SoundDetail_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.SoundDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
				
            RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || 'SoundDetail_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.SoundDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || 'SoundDetail_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.SoundDetail_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
  
	END SOUNDDETAIL_SAVE;
	
	PROCEDURE WETGRIPHDR_SAVE(ps_userid 					IN VARCHAR2,
                              pi_wetgripid  				OUT NUMBER,
                              ps_projectnumber  			IN VARCHAR2,
                              pi_tirenumber  				IN VARCHAR2,
                              ps_testspec  					IN VARCHAR2,
                              pd_dateoftest  				IN DATE,
                              ps_testvehicle  				IN VARCHAR2,
                              ps_locationoftesttrack  		IN VARCHAR2,
                              ps_testtrackcharacteristics  	IN VARCHAR2,
                              ps_issueby  					IN VARCHAR2,
                              ps_methodofcertification  	IN VARCHAR2,
                              ps_testtiredetails  			IN VARCHAR2,
                              ps_tiresizeandservicedesc  	IN VARCHAR2,
                              ps_tirebrandandtradedesc  	IN VARCHAR2,
                              ps_referenceinflationpressure	IN VARCHAR2,
                              ps_testrimwithcode  			IN VARCHAR2,
                              ps_tempmeasuresensortype  	IN VARCHAR2,
                              ps_identificationsrtt  		IN VARCHAR2,
                              ps_testtireload_srtt  		IN VARCHAR2,
                              ps_testtireload_candidate  	IN VARCHAR2,
                              ps_testtireload_control  		IN VARCHAR2,
                              ps_waterdepth_srtt  			IN VARCHAR2,
                              ps_waterdepth_candidate  		IN VARCHAR2,
                              ps_waterdepth_control  		IN VARCHAR2,
                              ps_wettedtracktempavg  		IN VARCHAR2,
                              pi_certificationtypeid  		IN NUMBER,
                              pi_skuid  					IN NUMBER,
                              pi_certificateid 				IN NUMBER,
                              ps_operation  				IN VARCHAR2,
                              ps_mfgwwyy    				IN VARCHAR2,
                              ps_gtspec     				IN VARCHAR2) 
	AS
    /******************************************************************************
     NAME:       WetGripHDR_Save
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/18/2012    Harini         1.Added Operation while updating and inserting.
     1.4        11/04/2013    Harini         1.As per IDEA2706,Modified procedure by adding 
                                             ps_gtspec and ps_mfgwwyy parameters in Input and add 
                                             these paramters while inserting/Updating
    ******************************************************************************/
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull,-20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid,-20006);
	
    --varible
    ls_wetgripexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    li_currentwetgripid WETGRIPHDR.WETGRIPID%TYPE := NULL;
	
	BEGIN
	
        IF ps_userid IS NOT NULL OR ps_userid <> '' THEN
			ls_operatorid:=ps_userid;
        END IF;
		
        ls_wetgripexist := TESTRESULTS_CRUD.CHECKIFWETGRIPEXIXTS(pi_certificateid => pi_certificateid,
																 pi_certificationtypeid => pi_certificationtypeid);
											  
        IF ls_wetgripexist = 'y' THEN
            UPDATE WETGRIPHDR 
			SET PROJECTNUMBER              = ps_projectnumber,
                TIRENUMBER                 = pi_tirenumber,
                TESTSPEC                   = ps_testspec,
                DATEOFTEST                 = pd_dateoftest,
                TESTVEHICLE                = ps_testvehicle,
                LOCATIONOFTESTTRACK        = ps_locationoftesttrack,
                TESTTRACKCHARACTERISTICS   = ps_testtrackcharacteristics,
                ISSUEBY                    = ps_issueby,
                METHODOFCERTIFICATION      = ps_methodofcertification,
                TESTTIREDETAILS            = ps_testtiredetails,
                TIRESIZEANDSERVICEDESC     = ps_tiresizeandservicedesc,
                TIREBRANDANDTRADEDESC      = ps_tirebrandandtradedesc,
                REFERENCEINFLATIONPRESSURE = ps_referenceinflationpressure,
                TESTRIMWITHCODE            = ps_testrimwithcode,
                TEMPMEASURESENSORTYPE      = ps_tempmeasuresensortype,
                IDENTIFICATIONSRTT         = ps_identificationsrtt,
                TESTTIRELOAD_SRTT          = ps_testtireload_srtt,
                TESTTIRELOAD_CANDIDATE     = ps_testtireload_candidate,
                TESTTIRELOAD_CONTROL       = ps_testtireload_control,
                WATERDEPTH_SRTT            = ps_waterdepth_srtt,
                WATERDEPTH_CANDIDATE       = ps_waterdepth_candidate,
                WATERDEPTH_CONTROL         = ps_waterdepth_control,
                WETTEDTRACKTEMPAVG         = ps_wettedtracktempavg,
                MODIFIEDBY                 = ls_operatorid   ,
                MODIFIEDON                 = SYSDATE,
                OPERNUM                    = ps_operation, --As per PRJ3617, added OperNum while updating
                MFGWWYY                    = ps_mfgwwyy,
                GTSPEC                     = ps_gtspec
                WHERE CERTIFICATEID = pi_certificateid 
					AND CERTIFICATIONTYPEID = pi_certificationtypeid;
                
			SELECT MAX(WETGRIPID) 
				INTO li_currentwetgripid
            FROM WETGRIPHDR W
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
                 
			DELETE 
			FROM WETGRIPDETAIL 
			WHERE WETGRIPID = li_currentwetgripid;
				 
			pi_wetgripid := li_currentwetgripid;
				 
        ELSE
            INSERT INTO WETGRIPHDR(WETGRIPID,
                                   PROJECTNUMBER,
                                   TIRENUMBER,
                                   TESTSPEC,
                                   DATEOFTEST,
                                   TESTVEHICLE,
                                   LOCATIONOFTESTTRACK,
                                   TESTTRACKCHARACTERISTICS,
                                   ISSUEBY,
                                   METHODOFCERTIFICATION,
                                   TESTTIREDETAILS,
                                   TIRESIZEANDSERVICEDESC,
                                   TIREBRANDANDTRADEDESC,
                                   REFERENCEINFLATIONPRESSURE,
                                   TESTRIMWITHCODE,
                                   TEMPMEASURESENSORTYPE,
                                   IDENTIFICATIONSRTT,
                                   TESTTIRELOAD_SRTT,
                                   TESTTIRELOAD_CANDIDATE,
                                   TESTTIRELOAD_CONTROL,
                                   WATERDEPTH_SRTT,
                                   WATERDEPTH_CANDIDATE,
                                   WATERDEPTH_CONTROL,
                                   WETTEDTRACKTEMPAVG,
                                   CERTIFICATIONTYPEID,
                                   CREATEDBY,
                                   CREATEDON,
                                   CERTIFICATEID,
                                   OPERNUM,
                                   MFGWWYY,
                                   GTSPEC)
							VALUES(WETGRIPID_SEQ.NEXTVAL,
                                   ps_projectnumber,
                                   pi_tirenumber,
                                   ps_testspec,
                                   pd_dateoftest,
                                   ps_testvehicle,
                                   ps_locationoftesttrack,
                                   ps_testtrackcharacteristics,
                                   ps_issueby,
                                   ps_methodofcertification,
                                   ps_testtiredetails,
                                   ps_tiresizeandservicedesc,
                                   ps_tirebrandandtradedesc,
                                   ps_referenceinflationpressure,
                                   ps_testrimwithcode,
                                   ps_tempmeasuresensortype,
                                   ps_identificationsrtt,
                                   ps_testtireload_srtt,
                                   ps_testtireload_candidate,
                                   ps_testtireload_control,
                                   ps_waterdepth_srtt,
                                   ps_waterdepth_candidate,
                                   ps_waterdepth_control,
                                   ps_wettedtracktempavg,
                                   pi_certificationtypeid,
                                   ls_operatorid,
                                   SYSDATE,
                                   pi_certificateid,
                                   ps_operation,     -- As per PRJ3617,inserting operation 
                                   ps_mfgwwyy,
                                   ps_gtspec);
            
			SELECT MAX(WETGRIPID) INTO li_currentwetgripid
            FROM WETGRIPHDR W
            WHERE CERTIFICATEID = pi_certificateid 
				AND CERTIFICATIONTYPEID = pi_certificationtypeid;
				
            pi_wetgripid := li_currentwetgripid;
				 
        END IF;
		
        COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || 'WetGripHDR_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.WetGripHDR_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005,ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg := SQLERRM || 'WetGripHDR_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.WetGripHDR_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || 'WetGripHDR_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.WetGripHDR_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
   
	END WETGRIPHDR_SAVE;
	
	PROCEDURE WETGRIPDETAIL_SAVE(ps_userid 						IN VARCHAR2,
                                 pi_iteration  					IN NUMBER,
                                 ps_testspeed  					IN VARCHAR2,
                                 ps_directionofrun  			IN VARCHAR2,
                                 ps_srtt  						IN VARCHAR2,
                                 ps_candidatetire  				IN VARCHAR2,
                                 ps_peakbreakforcecoeficient	IN VARCHAR2,
                                 ps_meanfullydevdeceleration  	IN VARCHAR2,
                                 ps_wetgripindex  				IN VARCHAR2,
                                 ps_comments  					IN VARCHAR2,
                                 pi_wetgripid  					IN NUMBER) 
	AS
    --EXCEPTION variables
    li_parametersarenull EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersarenull, -20005);
    li_parametersareinvalid EXCEPTION;
	
    -- link the EXCEPTION to the error number
    PRAGMA EXCEPTION_INIT(li_parametersareinvalid, -20006);
	
    --varible
    ls_highspeedexist VARCHAR2(1) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
	
	BEGIN
	
        IF ps_userid IS NOT NULL OR ps_userid <> '' THEN
			ls_operatorid := ps_userid;
        END IF;
		
        INSERT INTO WETGRIPDETAIL(ITERATION,
								  TESTSPEED,
								  DIRECTIONOFRUN,
								  SRTT,
								  CANDIDATETIRE,
								  PEAKBREAKFORCECOEFICIENT,
								  MEANFULLYDEVELOPEDDECELERATION,
								  WETGRIPINDEX,
								  COMMENTS,
								  WETGRIPID,
								  CREATEDBY,
								  CREATEDON)
						   VALUES(pi_iteration ,
								  ps_testspeed ,
								  ps_directionofrun ,
								  ps_srtt ,
								  ps_candidatetire ,
								  ps_peakbreakforcecoeficient ,
								  ps_meanfullydevdeceleration ,
								  ps_wetgripindex ,
								  ps_comments ,
								  pi_wetgripid ,
								  ls_operatorid ,
								  SYSDATE);
		COMMIT;
		
	EXCEPTION
	
        WHEN li_parametersarenull THEN
            ls_errormsg := SQLERRM || ' - WetGripDetail_Save. There is at least one parameters null.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.WetGripDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters null..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20005, ls_errormsg);
        
		WHEN li_parametersareinvalid THEN
            ls_errormsg:= SQLERRM || ' - WetGripDetail_Save. There is one parameters is invalid.';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.WetGripDetail_Save',
													  ax_recorddata    	=> 'ps_sku is parameters invalid..',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20006, ls_errormsg);
        
		WHEN OTHERS THEN
            ls_errormsg := SQLERRM || ' - WetGripDetail_Save. An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.WetGripDetail_Save',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
	
	END WETGRIPDETAIL_SAVE;
   
	PROCEDURE GETTIRETYPES(pc_tiretypes   OUT retcursor)
	AS
	/******************************************************************************
     NAME:       GetTireTypes
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------     
     1.0        11/6/2013    Ajit         Created     
    ******************************************************************************/  
    -- EXCEPTION Variables
    ls_operatorid VARCHAR2(50) := 'ICSDEV';
    ls_errormsg VARCHAR2(4000) := NULL;
    ls_machineid VARCHAR2(50) := NULL;
    
    BEGIN
	
        OPEN pc_tiretypes FOR 
			SELECT DISTINCT TT.TIRETYPEID,
							TT.TIRETYPENAME 
			FROM TIRETYPE TT
				INNER JOIN PRODUCT P ON TT.TIRETYPEID=P.TIRETYPEID
			ORDER BY TT.TIRETYPEID;
			
    EXCEPTION
       
        WHEN OTHERS THEN
            ls_errormsg := SQLERRM || '- GetTireTypes.  An error have ocurred.(when others)';
            
			APP_MESSAGE_OPERATIONS.APP_MESSAGE_INSERT(as_machineid 		=> ls_machineid,
													  ad_operatorid 	=> ls_operatorid,
													  ad_daterecorded  	=> SYSDATE,
													  as_processname   	=> ' testresults_crud.GetTireTypes',
													  ax_recorddata    	=> 'An error have ocurred.(when others)',
													  as_messagecode   	=> TO_CHAR(SQLCODE),
													  as_message       	=> ls_errormsg);
            
			RAISE_APPLICATION_ERROR(-20007, ls_errormsg);
    
	END GETTIRETYPES;
   
 	FUNCTION GETMEASUREID(ps_certificatenumber		IN VARCHAR2,
						  pi_certificationtypeid	IN NUMBER, 
						  pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER 
	AS
	li_measureid MEASUREHDR.MEASUREID%TYPE := NULL;
	li_total number := NULL;
	
	BEGIN
	
        SELECT COUNT(*) INTO li_total
        FROM CERTIFICATE CE
			INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
        WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND ce.certificateid = pi_certificatenumberid;
        
		IF li_total > 0 THEN
            SELECT NVL(MEASUREID, 0) INTO li_measureid
            FROM CERTIFICATE CE
                INNER JOIN MEASUREHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
            WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
        ELSE
            li_measureid := 0;
        END IF;
		
        RETURN li_measureid;
		
	END GETMEASUREID;
  
	FUNCTION GETTREADWEARID(ps_certificatenumber	IN VARCHAR2,
							pi_certificationtypeid	IN NUMBER, 
							pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER 
	AS
	li_treadwearid PLUNGERHDR.PLUNGERID%TYPE := NULL;
	
	BEGIN
	
        SELECT TREADWEARID INTO li_treadwearid
        FROM CERTIFICATE CE 
			INNER JOIN TREADWEARHDR M ON CE.CERTIFICATEID = M.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = M.CERTIFICATIONTYPEID
        WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
			
        RETURN li_treadwearid;
		
	EXCEPTION
        WHEN NO_DATA_FOUND THEN
			RETURN -1;
                
		RAISE_APPLICATION_ERROR (-20100, 'GetPlungerID: No data was found with those search parameters....');
		
	END GETTREADWEARID;
	
 	FUNCTION GETPLUNGERID(ps_certificatenumber		IN VARCHAR2,
						  pi_certificationtypeid	IN NUMBER, 
						  pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER
	AS
	li_plungerid PLUNGERHDR.PLUNGERID%TYPE := NULL;
	
	BEGIN
	
        SELECT PLUNGERID INTO li_plungerid
        FROM CERTIFICATE CE 
			INNER JOIN PLUNGERHDR P ON CE.CERTIFICATEID = P.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = P.CERTIFICATIONTYPEID
        WHERE P.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
			
        RETURN li_plungerid;
		
	EXCEPTION
        WHEN NO_DATA_FOUND THEN
			RETURN -1;
				 
		RAISE_APPLICATION_ERROR (-20100,'GetPlungerID: No data was found with those search parameters....');
				
	END GETPLUNGERID;
	
	FUNCTION GETENDURANCEID(ps_certificatenumber	IN VARCHAR2,
							pi_certificationtypeid 	IN NUMBER, 
							pi_certificatenumberid 	IN NUMBER ) 
	RETURN NUMBER
	AS
	li_enduranceid ENDURANCEHDR.ENDURANCEID%TYPE := NULL;
	
	BEGIN
	
        SELECT ENDURANCEID INTO li_enduranceid
        FROM CERTIFICATE CE 
			INNER JOIN ENDURANCEHDR E ON CE.CERTIFICATEID = E.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = E.CERTIFICATIONTYPEID
        WHERE E.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
			
        RETURN li_enduranceid;
		
	EXCEPTION
        WHEN NO_DATA_FOUND THEN
            RETURN -1;
			
		RAISE_APPLICATION_ERROR (-20100,'GetEnduranceID: No data was found with those search parameters....');
				
	END GETENDURANCEID;
  
	FUNCTION GETHIGHSPEEDID(ps_certificatenumber	IN VARCHAR2,
							pi_certificationtypeid 	IN NUMBER, 
							pi_certificatenumberid 	IN NUMBER ) 
	RETURN NUMBER
	AS
	li_highspeedid HIGHSPEEDHDR.HIGHSPEEDID%TYPE := NULL;
	
	BEGIN
	
        SELECT HIGHSPEEDID INTO li_highspeedid
        FROM CERTIFICATE CE 
			INNER JOIN HIGHSPEEDHDR H ON CE.CERTIFICATEID = H.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = H.CERTIFICATIONTYPEID
        WHERE H.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
			
        RETURN li_highspeedid;
		
	EXCEPTION
        WHEN NO_DATA_FOUND THEN
			RETURN -1;
				 
		RAISE_APPLICATION_ERROR (-20100, 'GetHighSpeedID: No data was found with those search parameters....');
				
	END GETHIGHSPEEDID;
  
  	FUNCTION GETBEADUNSEATID(ps_certificatenumber	IN VARCHAR2,
							 pi_certificationtypeid IN NUMBER, 
							 pi_certificatenumberid IN NUMBER) 
	RETURN NUMBER
	AS
	li_beadunseatid BEADUNSEATHDR.BEADUNSEATID%TYPE := NULL;
	li_total NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(*) INTO li_total
        FROM CERTIFICATE CE 
			INNER JOIN BEADUNSEATHDR BS ON CE.CERTIFICATEID = BS.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = BS.CERTIFICATIONTYPEID
        WHERE BS.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
        
		IF li_total > 0 THEN
			SELECT NVL(BEADUNSEATID,0) INTO li_beadunseatid
            FROM CERTIFICATE CE 
				INNER JOIN BEADUNSEATHDR BS ON CE.CERTIFICATEID = BS.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = BS.CERTIFICATIONTYPEID
            WHERE BS.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
        ELSE
            li_beadunseatid:=0;
        END IF;
		
        RETURN li_beadunseatid;
		
	END GETBEADUNSEATID;
  
	FUNCTION GETWETGRIPHDRID(ps_certificatenumber	IN VARCHAR2,
							 pi_certificationtypeid	IN NUMBER,
							 pi_skuid 				IN NUMBER, 
							 pi_certificatenumberid IN NUMBER) 
	RETURN NUMBER
	AS
	li_wetgripid WETGRIPHDR.WetGRIPID%TYPE := NULL;
	li_total NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(*) INTO li_total
        FROM CERTIFICATE CE 
			INNER JOIN WETGRIPHDR W ON CE.CERTIFICATEID = W.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = W.CERTIFICATIONTYPEID
        WHERE W.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
			
        IF li_total > 0 THEN
			SELECT NVL(WETGRIPID, 0) INTO li_wetgripid
            FROM CERTIFICATE CE 
				INNER JOIN WETGRIPHDR W ON CE.CERTIFICATEID = W.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = W.CERTIFICATIONTYPEID
            WHERE W.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
        ELSE
            li_wetgripid := 0;
        END IF;
		
        RETURN li_wetgripid;
		
	END GETWETGRIPHDRID;
	
  	FUNCTION GETSOUNDHDRID(ps_certificatenumber		IN VARCHAR2,
						   pi_certificationtypeid 	IN NUMBER, 
						   pi_certificatenumberid 	IN NUMBER) 
	RETURN NUMBER
	AS
	li_soundid SOUNDHDR.SOUNDID%TYPE := NULL;
	li_total NUMBER := NULL;
	
	BEGIN
        SELECT COUNT(*) INTO li_total
        FROM CERTIFICATE CE 
			INNER JOIN SOUNDHDR S ON CE.CERTIFICATEID = S.CERTIFICATEID 
				AND CE.CERTIFICATIONTYPEID = S.CERTIFICATIONTYPEID
        WHERE S.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
			AND CE.CERTIFICATEID = pi_certificatenumberid;
        
		IF li_total > 0 THEN
			SELECT NVL(S.SOUNDID,0) INTO li_soundid
            FROM CERTIFICATE CE 
				INNER JOIN SOUNDHDR S ON CE.CERTIFICATEID = S.CERTIFICATEID 
					AND CE.CERTIFICATIONTYPEID = S.CERTIFICATIONTYPEID
            WHERE S.CERTIFICATIONTYPEID = pi_certificationtypeid 
				AND LOWER(CE.CERTIFICATENUMBER) = LOWER(ps_certificatenumber) 
				AND CE.CERTIFICATEID = pi_certificatenumberid;
        ELSE
            li_soundid := 0;
        END IF;
		
        RETURN li_soundid;
		
	END GETSOUNDHDRID;
  
   	FUNCTION CHECKIFPRODUCTEXISTS(ps_matl_num	IN VARCHAR2,
								  pi_skuid 		IN NUMBER) 
	RETURN VARCHAR2
	AS
	/******************************************************************************
     NAME:       CheckIfProductExists
     PURPOSE:
     REVISIONS:
     Ver        Date        Author           Description
     ---------  ----------  ---------------  ------------------------------------
     1.0
     1.1        10/2/2012     Harini         1.Replaced ps_SKU with ps_matl_num
    ******************************************************************************/
	lc_exist CHAR := 'n';
	li_totalproducts INTEGER := NULL;
	
	BEGIN
	
		SELECT COUNT(1) INTO li_totalproducts
        FROM PRODUCT P
        WHERE P.MATL_NUM = LPAD(ps_matl_num, 18, 0)    -- As per PRJ3617,Replaced SKU with Matl_Num
          AND p.SKUId = pi_skuid;
        
		IF li_totalproducts > 0 THEN
			lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFPRODUCTEXISTS;
  
	FUNCTION CHECKIFENDURANCEEXISTS(pi_certificateid		IN NUMBER,
									pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2
	AS
	lc_exist CHAR := 'n';
	li_totalendurance INTEGER := NULL;
	
	BEGIN
	
        SELECT COUNT(1) INTO li_totalendurance
        FROM ENDURANCEHDR E
        WHERE CERTIFICATEID = pi_certificateid  
			AND CERTIFICATIONTYPEID = pi_certificationtypeid;
			
        IF li_totalendurance > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFENDURANCEEXISTS;
  
	FUNCTION CHECKIFHIGHSPEEDEXISTS(pi_certificateid		IN NUMBER,
									pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2
	AS
	lc_exist CHAR := 'n';
	li_totalhighspeed INTEGER := NULL;
	
	BEGIN
	
        SELECT COUNT(1) INTO li_totalhighspeed
        FROM HIGHSPEEDHDR H
        WHERE H.CERTIFICATEID = pi_certificateid  
			AND H.CERTIFICATIONTYPEID = pi_certificationtypeid ;
        
		IF li_totalhighspeed > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFHIGHSPEEDEXISTS;
	
  	FUNCTION CHECKIFMEASUREEXISTS(pi_certificateid			IN NUMBER,
								  pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2 
	AS
	lc_exist CHAR := 'n';
	li_totalmeasures NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(1) INTO li_totalmeasures
        FROM MEASUREHDR M
        WHERE M.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND M.CERTIFICATEID = pi_certificateid;
        
		IF li_totalmeasures > 0 THEN
            lc_exist:='y';
        ELSE
            lc_exist:='n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFMEASUREEXISTS;
  
	FUNCTION CHECKIFBEADUNSEATEXISTS(pi_certificateid		IN NUMBER,
									 pi_certificationtypeid	IN NUMBER) 
	RETURN VARCHAR2
	AS
	lc_exist CHAR := 'n';
	li_totalbeadunseat NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(1) INTO li_totalbeadunseat
        FROM BEADUNSEATHDR BU
        WHERE BU.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND BU.CERTIFICATEID = pi_certificateid;
        
		IF li_totalbeadunseat > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFBEADUNSEATEXISTS;
	
	FUNCTION CHECKIFTREADWEAREXISTS(pi_certificateid		IN NUMBER,
									pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2
	AS
	lc_exist CHAR := 'n';
	li_totaltreadwearid NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(1) INTO li_totaltreadwearid
        FROM TREADWEARHDR TW
        WHERE CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND CERTIFICATEID = pi_certificateid;
        
		IF li_totaltreadwearid > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFTREADWEAREXISTS;
  
	FUNCTION CHECKIFPLUNGEREXISTS(pi_certificateid			IN NUMBER, 
								  pi_certificationtypeid 	IN NUMBER) 
	RETURN VARCHAR2
	AS
	lc_exist CHAR := 'n';
	li_total NUMBER := NULL;
	
	BEGIN
	
        SELECT COUNT(1) INTO li_total
        FROM PLUNGERHDR P
        WHERE P.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND P.CERTIFICATEID = pi_certificateid;
        
		IF li_total > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFPLUNGEREXISTS;
	
 	FUNCTION CHECKIFSOUNDEXIXTS(pi_certificateid		IN NUMBER, 
								pi_certificationtypeid 	IN NUMBER)  
	RETURN VARCHAR2
	AS
    lc_exist CHAR := 'n';
    li_total NUMBER := NULL;
    
	BEGIN
	
        SELECT COUNT(1) INTO li_total
        FROM SOUNDHDR H
        WHERE H.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND H.CERTIFICATEID = pi_certificateid;
        
		IF li_total > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
	END CHECKIFSOUNDEXIXTS;
	
 	FUNCTION CHECKIFWETGRIPEXIXTS(pi_certificateid			IN NUMBER, 
								  pi_certificationtypeid 	IN NUMBER)  
	RETURN VARCHAR2 
	AS
    lc_exist CHAR := 'n';
    li_total NUMBER := NULL;
     
	BEGIN
	
        SELECT COUNT(1) INTO li_total
        FROM WETGRIPHDR H
        WHERE H.CERTIFICATIONTYPEID = pi_certificationtypeid 
			AND CERTIFICATEID = pi_certificateid;
			  
        IF li_total > 0 THEN
            lc_exist := 'y';
        ELSE
            lc_exist := 'n';
        END IF;
		
        RETURN lc_exist;
		
    END CHECKIFWETGRIPEXIXTS;
	  
END TESTRESULTS_CRUD;
/

DROP PACKAGE ICS_PROCS.IMARK_LOAD;
/

DROP PACKAGE ICS_PROCS.PROD_COUNTRY_LOAD;
/