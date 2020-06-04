 procedure App_Message_Insert(
		  as_Machineid    in app_message.machine_id%Type,
          ad_OperatorID   in app_message.operator_id%Type,
		  ad_DateRecorded in app_message.date_recorded%Type,
		  as_ProcessName  in app_message.process_name%Type,
		  ax_RecordData   in app_message.redorded_data%Type,
		  as_MessageCode  in app_message.message_code%Type,
		  as_Message      in app_message.message%Type) as
begin
	INSERT INTO APP_MESSAGE(
			seq_no,
			OPERATOR_ID,
			machine_id,
			date_recorded,
			process_name,
			redorded_data,
			message_code,      
			MESSAGE)
	VALUES(APP_MESSAGE_SEQ.NextVal,
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
/
