CREATE OR REPLACE package app_message_operations as 
  type retCursor is ref cursor;
  

  procedure GetAllExceptions(pc_RetCursor out retCursor); 
  Procedure App_Message_Insert(  as_Machineid    in app_message.machine_id%Type,
                                 ad_OperatorID   in app_message.operator_id%Type,
                                 ad_DateRecorded in app_message.date_recorded%Type,
                                 as_ProcessName  in app_message.process_name%Type,
                                 ax_RecordData   in app_message.recorded_data%Type,
                                 as_MessageCode  in app_message.message_code%Type,
                                 as_Message      in app_message.message%Type);                          
  end app_message_operations;
/


CREATE OR REPLACE PACKAGE BODY app_message_operations as

		procedure GetAllExceptions(pc_RetCursor out retCursor) as 
		  begin
			Open pc_retCursor for
				SELECT 
						SEQ_NO,
						OPERATOR_ID,
						MACHINE_ID,
						TO_CHAR(DATE_RECORDED,'yyyy-MM-DD HH24:MI:SS'),
						PROCESS_NAME,
						RECORDED_DATA,
						MESSAGE_CODE,
						MESSAGE
				FROM APP_MESSAGE ;
		    
			EXCEPTION
			  when others then
				pc_retCursor:=null;
		        
		 end GetAllExceptions;
  
  
		    Procedure App_Message_Insert(    as_Machineid    in app_message.machine_id%Type,
											 ad_OperatorID   in app_message.operator_id%Type,
											 ad_DateRecorded in app_message.date_recorded%Type,
											 as_ProcessName  in app_message.process_name%Type,
											 ax_RecordData   in app_message.recorded_data%Type,
											 as_MessageCode  in app_message.message_code%Type,
											 as_Message      in app_message.message%Type) as
		  begin
				INSERT INTO APP_MESSAGE(
										seq_no,
										OPERATOR_ID,
										machine_id,
										date_recorded,
										process_name,
										recorded_data,
										message_code,      
										MESSAGE)
				VALUES( APP_MESSAGE_SEQ.NextVal,
						as_machineid,
						ad_OperatorID,
						ad_daterecorded,
						as_processname,
						ax_recorddata,
						as_messagecode,
						as_message);
		      
		   COMMIT;  
		   
		   Exception
			when others then
			  null;
		  end App_Message_Insert;


end app_message_operations;
/
