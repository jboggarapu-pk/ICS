June 02, 2010
Keith D. DuPont

The purpose of this example is to show one way in which a DALC can be built prior to integration of 
CooperTire.Security, yet still allow the integration of CooperTire.Security with only minor code changes.

I use this methodology since it allows me to develop the application and address all coding issues
prior to implementing CTS.  If I then implement CTS and start seeing issues I am fairly confident that
the issues are related to the CTS configuration as opposed to coding-specific issues.

  PRIOR TO CTS INTEGRATION
    1) Set the m_blnUseCooperTireSecurity member to FALSE
    2) Set the value of your temporary connection string in the CtsDalc.vb 
		a)  Line 34 in CtsDalc.vb
	3) Review the sample GetData method for usage example
		
		
  AFTER CTS INTEGRATION
    1) Delete the following
		a)  m_oraConn object declaration and all referencing code 
		b)  m_strTempConnString declaration and all referencing code
		c)  m_blnUseCooperTireSecurity declaration and all referencing code
		d)  ConnectNonCTS method and all referencing code
		e)  DisConnectNonCTS method and all referencing code
		
Refer to the TODO: comments in the code for further direction


June 04, 2010
Keith D. DuPont
Minor changes 