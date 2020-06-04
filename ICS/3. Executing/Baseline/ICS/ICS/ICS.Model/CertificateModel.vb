Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Certificate processing model
''' </summary>
''' <remarks></remarks>
Public Class CertificateModel

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region " Members "
#End Region

#Region "Properties"
#End Region

#Region " Constructors / Destructors "
#End Region

#Region "Methods"

    ''' <summary>
    ''' Audit entire certificate data with test results
    ''' </summary>
    ''' <param name="p_objCertificate"></param>
    ''' <param name="objTRModel"></param>
    ''' <param name="p_objProduct"></param>
    ''' <param name="p_objMeasure"></param>
    ''' <param name="p_objPlunger"></param>
    ''' <param name="p_objTreadwear"></param>
    ''' <param name="p_objBeadUnseat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AuditEntireCertificate(ByVal p_objCertificate As Certificate, _
                                            ByVal objTRModel As TestResultsModel, _
                                            ByVal p_objProduct As Product, ByVal p_objMeasure As Measure, _
                                            ByVal p_objPlunger As Plunger, ByVal p_objTreadwear As Treadwear, _
                                            ByVal p_objBeadUnseat As BeadUnSeat, ByVal p_objEndurance As Endurance, _
                                            ByVal p_objHighSpeed As HighSpeed, ByVal p_objSound As Sound, _
                                            ByVal p_objWetGrip As WetGrip) As List(Of AuditLogEntry)

        'Creates new instance of Audit Log to compare Original Certificate with currently mapped certificate entity
        Dim objAuditLog As New AuditLog(Of Certificate)(p_objCertificate.OriginalCertificate, p_objCertificate)

        Dim strAreaOfChange As String = AuditLogEntry.AreaOfChange.Certification.ToString() & "-"
        Dim listAuditLog As List(Of AuditLogEntry)
        Dim listTestDataAuditLog As List(Of AuditLogEntry)

        'jeseitz 4/12/16
        'If p_objCertificate.CertificationTypeName = NameAid.Certification.Imark.ToString() Then
        '    strAreaOfChange &= ImarkCertificateModel.ImarkCertNumber
        'Else
        strAreaOfChange &= p_objCertificate.CertificateNumber
        'End If

        listAuditLog = objAuditLog.RunAudit(strAreaOfChange)
        If listAuditLog.Count > 0 Then
            Return listAuditLog
        Else
            listTestDataAuditLog = objTRModel.AuditTestData(strAreaOfChange, p_objProduct, p_objMeasure, p_objPlunger, p_objTreadwear, p_objBeadUnseat, p_objEndurance, p_objHighSpeed, p_objSound, p_objWetGrip)
            Return listTestDataAuditLog
        End If

    End Function

    Public Function GetManufacturingLocationsList() As DataSet

        Dim dstLocs As DataSet = Depository.Current.GetManufacturingLocationsResults(String.Empty)

        Return dstLocs
    End Function

    Public Function GetQueryControlGridSource() As DataTable
        Dim dtbGridSource As DataTable = Depository.Current.GetQueryControlGridSource

        Return dtbGridSource
    End Function

    Public Function GetCompanyNameList() As DataSet

        Dim dstCompanyName As DataSet = Depository.Current.GetCompanyNameList()

        Return dstCompanyName
    End Function

    ''' <summary>
    ''' Save Material number association with certificate and certificate data
    ''' </summary>
    ''' <param name="p_objCertificate"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_objCustomer"></param>
    ''' <param name="p_objProduct"></param>
    ''' <param name="p_objMeasure"></param>
    ''' <param name="p_objPlunger"></param>
    ''' <param name="p_objTreadwear"></param>
    ''' <param name="p_objBeadUnseat"></param>
    ''' <param name="p_objEndurance"></param>
    ''' <param name="p_objHighSpeed"></param>
    ''' <param name="p_objSound"></param>
    ''' <param name="p_objWetGrip"></param>
    ''' <returns>NameAid.SaveResult</returns>
    ''' <remarks></remarks>
    Public Function SaveSKUAssociationAndCertificateData(ByVal p_objCertificate As Certificate, _
                                        ByVal p_strMatlNum As String, _
                                        ByVal p_objCustomer As Customer, _
                                        ByVal p_objProduct As Product, _
                                        ByVal p_objMeasure As Measure, _
                                        ByVal p_objPlunger As Plunger, _
                                        ByVal p_objTreadwear As Treadwear, _
                                        ByVal p_objBeadUnseat As BeadUnSeat, _
                                        ByVal p_objEndurance As Endurance, _
                                        ByVal p_objHighSpeed As HighSpeed, _
                                        ByVal p_objSound As Sound, _
                                        ByVal p_objWetGrip As WetGrip) As NameAid.SaveResult

        Debug.WriteLine("SaveSKUAssociationAndCertificateData")
        Dim enuSaveResult As NameAid.SaveResult

        enuSaveResult = NameAid.SaveResult.SaveError

        ' Make sure the Material number is associated with the Certificate
        Dim addCertModel As AddCertificationModel = New AddCertificationModel()
        ' Modified as per project 2706 technical specification
        Dim blnSaveSuccess As Boolean = addCertModel.SaveCertificateSKUAssociation(p_objCertificate.CertificateNumber, p_strMatlNum, Depository.Current.GetCertificationTypeID(p_objCertificate.CertificationTypeName), p_objCertificate.Importer_N, p_objCertificate.Customer_N, p_objCertificate.Extension_EN)
        If blnSaveSuccess Then
            enuSaveResult = NameAid.SaveResult.Sucess
        End If

        ' Save certificate and test results data:
        If enuSaveResult = NameAid.SaveResult.Sucess Then
            enuSaveResult = SaveCertificateData(p_objCertificate, p_objCustomer, p_objProduct, _
                                                p_objMeasure, p_objPlunger, p_objTreadwear, _
                                                p_objBeadUnseat, p_objEndurance, p_objHighSpeed, _
                                                p_objSound, p_objWetGrip)
        End If

        Return enuSaveResult

    End Function

    ''' <summary>
    ''' Save all Certificate related data
    ''' </summary>
    ''' <param name="p_objCertificate"></param>
    ''' <param name="p_objCustomer"></param>
    ''' <param name="p_objProduct"></param>
    ''' <param name="p_objMeasure"></param>
    ''' <param name="p_objPlunger"></param>
    ''' <param name="p_objTreadwear"></param>
    ''' <param name="p_objBeadUnseat"></param>
    ''' <param name="p_objEndurance"></param>
    ''' <param name="p_objHighSpeed"></param>
    ''' <param name="p_objSound"></param>
    ''' <param name="p_objWetGrip"></param>
    ''' <returns>NameAid.SaveResult</returns>
    ''' <remarks></remarks>
    Public Function SaveCertificateData(ByVal p_objCertificate As Certificate, _
                                        ByVal p_objCustomer As Customer, _
                                        ByVal p_objProduct As Product, _
                                        ByVal p_objMeasure As Measure, _
                                        ByVal p_objPlunger As Plunger, _
                                        ByVal p_objTreadwear As Treadwear, _
                                        ByVal p_objBeadUnseat As BeadUnSeat, _
                                        ByVal p_objEndurance As Endurance, _
                                        ByVal p_objHighSpeed As HighSpeed, _
                                        ByVal p_objSound As Sound, _
                                        ByVal p_objWetGrip As WetGrip) As NameAid.SaveResult

        Dim enuSaveResult As NameAid.SaveResult
        'Get the certificationTypeID based on the certification type name
        Dim intCertType As Integer = GetCertificateTypeID(p_objCertificate.CertificationTypeName)

        'Gets the certificate ID from the database and verifies that there is no duplication
        Dim intCertificateID As Integer = Depository.Current.GetCertificateID(p_objCertificate.CertificateNumber, intCertType, p_objCertificate.Extension_EN.ToString())
        If intCertificateID > 0 AndAlso intCertificateID <> p_objCertificate.CertificateNumberID Then
            enuSaveResult = NameAid.SaveResult.DuplicationError
            Return enuSaveResult
        End If

        Dim objTRModel As New TestResultsModel()
        Dim objCustomerModel As New CustomerModel()

        enuSaveResult = NameAid.SaveResult.Sucess


        ' ID before save, will be updated for new certificate only
        Dim intCertNumIDBeforeSave As Integer = p_objCertificate.CertificateNumberID

        'If enuSaveResult = NameAid.SaveResult.Sucess AndAlso Not p_objCertificate.ToBeRenewedCertificate_I Is Nothing Then
        '    ' Imark renew creates a new certificate (new ID)
        '    Dim strOldCertNumber As String = p_objCertificate.ToBeRenewedCertificate_I.CertificateNumber
        '    Dim strNewCertNumber As String = p_objCertificate.CertificateNumber
        '    Dim intImarkNewCertificateNumberId As Integer = 0
        '    'enuSaveResult = Depository.Current.RenewImarkCertificate(strOldCertNumber, strNewCertNumber, SecurityModel.GetUserName, intImarkNewCertificateNumberId)
        '    p_objCertificate.CertificateNumberID = intImarkNewCertificateNumberId
        '    ' To process further as a new certificate
        '    intCertNumIDBeforeSave = 0
        'End If

        If enuSaveResult = NameAid.SaveResult.Sucess Then
            enuSaveResult = SaveCertificate(p_objCertificate)
        End If

        'If enuCertType = NameAid.Certification.NOM AndAlso p_objCustomer IsNot Nothing AndAlso enuSaveResult = NameAid.SaveResult.Sucess Then
        '    enuSaveResult = objCustomerModel.SaveCustomerData(p_objCustomer)
        'End If
        ' For the new certificate its new ID is assigned now:
        If enuSaveResult = NameAid.SaveResult.Sucess AndAlso intCertNumIDBeforeSave = 0 Then
            ''Debug.Assert(p_objCertificate.CertificateNumberID > 0, "CertificateNumberID after save must be > 0.")
            p_objMeasure.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objPlunger.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objTreadwear.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objBeadUnseat.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objEndurance.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objHighSpeed.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objSound.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objWetGrip.CertificateNumberID = p_objCertificate.CertificateNumberID
        End If

        'GetCertificateID again in case Extension has been update, which means new certificateid
        If enuSaveResult = NameAid.SaveResult.Sucess AndAlso intCertNumIDBeforeSave > 0 Then
            p_objCertificate.CertificateNumberID = Depository.Current.GetCertificateID(p_objCertificate.CertificateNumber, intCertType, p_objCertificate.Extension_EN.ToString())
            p_objMeasure.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objPlunger.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objTreadwear.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objBeadUnseat.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objEndurance.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objHighSpeed.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objSound.CertificateNumberID = p_objCertificate.CertificateNumberID
            p_objWetGrip.CertificateNumberID = p_objCertificate.CertificateNumberID
        End If

        If enuSaveResult = NameAid.SaveResult.Sucess And p_objProduct IsNot Nothing Then
            'jeseitz 5/15/14  Imark and Family_I are saved above in SaveCertificate when certificationtypeid = 4, don't re-save here (commented out of stored procedure)
            'p_objProduct.IMark = p_objCertificate.MoldStamping
            'p_objProduct.Family = p_objCertificate.Family_I
            enuSaveResult = objTRModel.SaveTestResults(p_objProduct, False)
        End If

        If enuSaveResult = NameAid.SaveResult.Sucess Then
            enuSaveResult = objTRModel.SaveTestResults(p_objMeasure, p_objPlunger, p_objTreadwear, p_objBeadUnseat, p_objEndurance, p_objHighSpeed, p_objSound, p_objWetGrip, False)
        End If

        Return enuSaveResult

    End Function

    ''' <summary>
    ''' Validate entire certificate with its test results
    ''' </summary>
    ''' <param name="p_objCertificate"></param>
    ''' <param name="p_objMeasure"></param>
    ''' <param name="p_objPlunger"></param>
    ''' <param name="p_objTreadwear"></param>
    ''' <param name="p_objBeadUnseat"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateEntireCertificate(ByVal p_objCertificate As Certificate, _
                                            ByVal p_objCustomer As Customer, _
                                            ByVal p_objProduct As Product, _
                                            ByVal p_objMeasure As Measure, _
                                            ByVal p_objPlunger As Plunger, _
                                            ByVal p_objTreadwear As Treadwear, _
                                            ByVal p_objBeadUnseat As BeadUnSeat, _
                                            ByVal p_objEndurance As Endurance, _
                                            ByVal p_objHighSpeed As HighSpeed, _
                                            ByVal p_objSound As Sound, _
                                            ByVal p_objWetGrip As WetGrip) As NameAid.SaveResult

        Dim enumIsValid As NameAid.SaveResult = NameAid.SaveResult.Sucess

        If Not p_objCertificate.DoValidate() OrElse Not p_objCertificate.DoValidate(p_objCertificate.CertificationTypeName) Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If p_objCustomer IsNot Nothing AndAlso Not p_objCustomer.DoValidate() Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso Not p_objProduct.DoValidate() Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso (Not p_objMeasure.DoValidate(p_objCertificate.CertificationTypeName) Or Not p_objMeasure.DoValidate()) Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso Not p_objPlunger.DoValidate(p_objCertificate.CertificationTypeName) Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso Not p_objTreadwear.DoValidate(p_objCertificate.CertificationTypeName) Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso Not p_objBeadUnseat.DoValidate(p_objCertificate.CertificationTypeName) Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso Not p_objEndurance.DoValidate() Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If
        If enumIsValid = NameAid.SaveResult.Sucess AndAlso Not p_objHighSpeed.DoValidate() Then
            enumIsValid = NameAid.SaveResult.ValidationError
        End If

        Return enumIsValid

    End Function

    ''' <summary>
    ''' Save certificate data
    ''' </summary>
    ''' <param name="p_objCertificate">Certificate</param>
    ''' <returns></returns>
    ''' <remarks>non transactional</remarks>
    Public Function SaveCertificate(ByVal p_objCertificate As Certificate) As NameAid.SaveResult

        Dim enumTestResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

        Dim chrRenewalrequired As Char = IIf(p_objCertificate.RenewalRequired_CGIN, "y", "n")
        Dim chrActiveStatus As Char = IIf(p_objCertificate.ActiveStatus, "y", "n")
        Dim chrSupplementalRequired As Char = IIf(p_objCertificate.SupplementalRequired_EI, "y", "n")
        Dim chrCustomerSpecific As Char = IIf(p_objCertificate.CustomerSpecific_N, "y", "n")
        Dim chrActSigReq As Char = IIf(p_objCertificate.ActSigReq, "y", "n")
        Dim strUserName As String = SecurityModel.GetUserName

        'JBH_2.00 Project 5325: Added Mold Change Required and Operations Date Approved parameters
        enumTestResult = Depository.Current.SaveCertificate(p_objCertificate.SKUID, _
                                       p_objCertificate.MaterialNumber, _
                                       p_objCertificate.RemoveMatlNum, _
                                       p_objCertificate.CertificationTypeName, _
                                       p_objCertificate.CertificateNumber, _
                                       p_objCertificate.CertDateSubmitted, _
                                       p_objCertificate.CertDateApproved_CEGI, _
                                       p_objCertificate.DateSubmitted, _
                                       chrActiveStatus, _
                                       p_objCertificate.DateAssigned_EGI, _
                                       p_objCertificate.DateApproved_CEGI, _
                                       chrRenewalrequired, _
                                       p_objCertificate.JobReportNumber_CEN, _
                                       p_objCertificate.Extension_EN, _
                                       p_objCertificate.SupplementalMoldStamping_E, _
                                       p_objCertificate.EmarkReference_I, _
                                       p_objCertificate.ExpiryDate_I, _
                                       p_objCertificate.Family_I, _
                                       p_objCertificate.ProductLocation, _
                                       p_objCertificate.CountryOfManufacture_N, _
                                       p_objCertificate.AddNewCustomer, _
                                       chrActSigReq, _
                                       p_objCertificate.CustomerID, _
                                       p_objCertificate.Customer_N, _
                                       p_objCertificate.CustomerAddress_N, _
                                       chrCustomerSpecific, _
                                       p_objCertificate.AddNewImporter, _
                                       p_objCertificate.ImporterID, _
                                       p_objCertificate.Importer_N, _
                                       p_objCertificate.ImporterAddress_N, _
                                       p_objCertificate.ImporterRepresentative_N, _
                                       p_objCertificate.CountryLocation_N, _
                                       p_objCertificate.BatchNumber_G, _
                                       p_objCertificate.SupplementalDateAssigned_E, _
                                       p_objCertificate.SupplementalDateSubmitted_E, _
                                       p_objCertificate.SupplementalDateApproved_E, _
                                       p_objCertificate.CompanyName, _
                                       strUserName, _
                                       p_objCertificate.CertificateNumberID, _
                                       p_objCertificate.MoldStamping, _
                                       p_objCertificate.MoldChgRequired, _
                                       p_objCertificate.OperDateApproved, _
                                       p_objCertificate.AddInfo)

        Return enumTestResult

    End Function

    ''' <summary>
    ''' Mass Update Batch Numbers
    ''' </summary>
    ''' <param name="p_strTempBatchNum"></param>
    ''' <param name="p_strGSOBatchNum"></param>
    ''' <returns></returns>
    ''' <remarks>non transactional</remarks>
    Public Function BatchNumMassUpdate(ByVal p_strCertifName As String, ByVal p_strTempBatchNum As String, ByVal p_strGSOBatchNum As String) As NameAid.SaveResult

        Dim enumTestResult As NameAid.SaveResult = NameAid.SaveResult.SaveError

        Dim strUserName As String = SecurityModel.GetUserName

        enumTestResult = Depository.Current.BatchNumMassUpdate(p_strCertifName, _
                                       p_strTempBatchNum, _
                                       p_strGSOBatchNum, _
                                       strUserName)

        Return enumTestResult

    End Function

    ''' <summary>
    ''' Get certificate data from database and map to certificate entity class
    ''' </summary>
    ''' <param name="p_strCertificationNumber"></param>
    ''' <param name="p_strExtensionNo"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_strCertificationTypeID"></param>
    ''' <param name="p_iSKUID"></param>
    ''' <param name="p_blnTRsExist"></param>
    ''' <returns>objCertificate</returns>
    ''' <remarks></remarks>
    Public Function GetCertificate(ByVal p_strCertificationNumber As String, ByVal p_strExtensionNo As String, ByVal p_strMatlNum As String, ByVal p_strCertificationTypeID As Integer, ByVal p_iSKUID As Integer, ByRef p_blnTRsExist As Boolean) As Certificate

        Dim dtbCertificateData As DataTable = Depository.Current.GetCertificate(p_strCertificationNumber, p_strExtensionNo, p_strCertificationTypeID, p_iSKUID, p_blnTRsExist)
        Dim objCertificate As Certificate = MapDataTableToCertificate(dtbCertificateData, p_strMatlNum)

        Return objCertificate

    End Function

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Map datatable to cerficate entity
    ''' </summary>
    ''' <param name="p_dtbCertificateInfo"></param>
    ''' <param name="p_strMatlNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MapDataTableToCertificate(ByVal p_dtbCertificateInfo As DataTable, ByVal p_strMatlNum As String) As Certificate

        Dim objCertificate As Certificate = Nothing

        If p_dtbCertificateInfo.Rows.Count = 0 Then
            Return objCertificate
        End If

        ' Keep object defaults unless there is a value in table.
        objCertificate = New Certificate()

        Dim foundRow() As DataRow
        objCertificate.MaterialNumber = p_strMatlNum

        foundRow = p_dtbCertificateInfo.Select("MATL_NUM = '" & p_strMatlNum.PadLeft(18, "0") & "'")

        If foundRow.Length = 0 Then
            ' Due to data inconsistency in table ,checked the below condition also
            foundRow = p_dtbCertificateInfo.Select("MATL_NUM = '" & p_strMatlNum.TrimStart("0"c) & "'")
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("CertificateID").ToString()) Then
            objCertificate.CertificateNumberID = Convert.ToInt32(p_dtbCertificateInfo.Rows(0)("CertificateID"))
        End If

        If Not String.IsNullOrEmpty(foundRow(0)("SKUID").ToString()) Then
            objCertificate.SKUID = Convert.ToInt32(foundRow(0)("SKUID"))
        End If

        If foundRow.Length > 0 Then

            'Product Data
            If Not String.IsNullOrEmpty(foundRow(0)("SizeStamp").ToString()) Then
                objCertificate.lblSizeStamp = foundRow(0)("SizeStamp").ToString()
            End If
            If Not String.IsNullOrEmpty(foundRow(0)("SingLoadIndex").ToString()) Then
                If foundRow(0)("SingLoadIndex").ToString() <> "0" Then
                    objCertificate.lblSingLoadIndex = foundRow(0)("SingLoadIndex").ToString()
                Else
                    objCertificate.lblSingLoadIndex = " "
                End If
            End If
            If Not String.IsNullOrEmpty(foundRow(0)("DualLoadIndex").ToString()) Then
                If foundRow(0)("DualLoadIndex").ToString() <> "0" Then
                    objCertificate.lblDualLoadIndex = foundRow(0)("DualLoadIndex").ToString()
                Else
                    objCertificate.lblDualLoadIndex = " "
                End If
            End If
            If Not String.IsNullOrEmpty(foundRow(0)("SpeedRating").ToString()) Then
                objCertificate.lblSpeedRating = foundRow(0)("SpeedRating").ToString()
            End If
            If Not String.IsNullOrEmpty(foundRow(0)("BrandDesc").ToString()) Then
                objCertificate.lblBrandDesc = foundRow(0)("BrandDesc").ToString()
            End If
            If Not String.IsNullOrEmpty(foundRow(0)("TubelessYN").ToString()) Then
                objCertificate.lblTubelessYN = foundRow(0)("TubelessYN").ToString()
            End If

            If foundRow(0)("DateAssigned_EGI") IsNot DBNull.Value Then
                objCertificate.DateAssigned_EGI = CType(foundRow(0)("DateAssigned_EGI"), DateTime)
            End If

            If foundRow(0)("DateApproved_CEGI") IsNot DBNull.Value Then
                objCertificate.DateApproved_CEGI = CType(foundRow(0)("DateApproved_CEGI"), DateTime)
            End If

            If foundRow(0)("DateSubmitted") IsNot DBNull.Value Then
                objCertificate.DateSubmitted = CType(foundRow(0)("DateSubmitted"), DateTime)
            End If

            If foundRow(0)("DateRemoved") IsNot DBNull.Value Then
                objCertificate.RemoveMatlNum = True
            Else
                objCertificate.RemoveMatlNum = False
            End If

            'Actual Signature Required
            If foundRow(0)("SignatureInd") IsNot DBNull.Value And _
                foundRow(0)("SignatureInd").ToString().Equals("y") Then
                objCertificate.ActSigReq = True
            End If

            'Added as per project 2706 technical specification
            If foundRow(0)("DiscontinuedDate") IsNot DBNull.Value Then
                objCertificate.DiscontinuedDate = CType(foundRow(0)("DiscontinuedDate"), DateTime)
            End If

            If Not String.IsNullOrEmpty(foundRow(0)("TPN").ToString()) Then
                objCertificate.TPN = foundRow(0)("TPN").ToString()
            End If

            'JBH_2.00 Project 5325 - Add - Mold Change Required
            If foundRow(0)("mold_changed") IsNot DBNull.Value And _
                foundRow(0)("mold_changed").ToString().Equals("y") Then
                objCertificate.MoldChgRequired = True
            Else
                objCertificate.MoldChgRequired = False
            End If

            'JBH_2.00 Project 5325 - Add - Operation Approval Date
            If foundRow(0)("oper_date_approved") IsNot DBNull.Value Then
                objCertificate.OperDateApproved = CType(foundRow(0)("oper_date_approved"), DateTime)
            End If

            'jeseitz req 203625
            If Not String.IsNullOrEmpty(foundRow(0)("ADDITIONAL_INFO").ToString()) Then
                objCertificate.AddInfo = foundRow(0)("ADDITIONAL_INFO").ToString()
            End If

        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("CertificateNumber").ToString()) Then
            objCertificate.CertificateNumber = p_dtbCertificateInfo.Rows(0)("CertificateNumber").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)(NameAid.Column.CertificationTypeName).ToString()) Then
            objCertificate.CertificationTypeName = p_dtbCertificateInfo.Rows(0)(NameAid.Column.CertificationTypeName).ToString()
        End If

        If p_dtbCertificateInfo.Rows(0)("ActiveStatus") IsNot DBNull.Value And _
            p_dtbCertificateInfo.Rows(0)("ActiveStatus").ToString().Equals("y") Then
            objCertificate.ActiveStatus = True
        End If

        ' TODO Current DB implementation is varchar2 while use case is numeric, has to convert to integer
        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("Extension_EN").ToString()) Then
            objCertificate.Extension_EN = Convert.ToInt32(p_dtbCertificateInfo.Rows(0)("Extension_EN"))
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("JobReportNumber_CEN").ToString()) Then
            objCertificate.JobReportNumber_CEN = p_dtbCertificateInfo.Rows(0)("JobReportNumber_CEN").ToString()
        End If

        If p_dtbCertificateInfo.Rows(0)("CertDateSubmitted") IsNot DBNull.Value Then
            objCertificate.CertDateSubmitted = CType(p_dtbCertificateInfo.Rows(0)("CertDateSubmitted"), DateTime)
        End If

        If p_dtbCertificateInfo.Rows(0)("CertDateApproved") IsNot DBNull.Value Then
            objCertificate.CertDateApproved_CEGI = CType(p_dtbCertificateInfo.Rows(0)("CertDateApproved"), DateTime)
        End If

        If p_dtbCertificateInfo.Rows(0)("RenewalRequired_CGIN") IsNot DBNull.Value And _
            p_dtbCertificateInfo.Rows(0)("RenewalRequired_CGIN").ToString().Equals("y") Then
            objCertificate.RenewalRequired_CGIN = True
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("ProductLocation").ToString()) Then
            objCertificate.ProductLocation = p_dtbCertificateInfo.Rows(0)("ProductLocation").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("SupplementalMoldStamping_E").ToString()) Then
            objCertificate.SupplementalMoldStamping_E = p_dtbCertificateInfo.Rows(0)("SupplementalMoldStamping_E").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("BATCHNUMBER_G").ToString()) Then
            objCertificate.BatchNumber_G = p_dtbCertificateInfo.Rows(0)("BATCHNUMBER_G").ToString()
        End If

        If p_dtbCertificateInfo.Rows(0)("CertificationTypeId") = 4 Then
            If Not String.IsNullOrEmpty(foundRow(0)("EmarkReference_I").ToString()) Then
                objCertificate.EmarkReference_I = foundRow(0)("EmarkReference_I").ToString()
            End If

            If Not String.IsNullOrEmpty(foundRow(0)("Family").ToString()) Then
                objCertificate.Family_I = foundRow(0)("Family").ToString()
            End If
            If Not String.IsNullOrEmpty(foundRow(0)("FamilyDesc").ToString()) Then
                objCertificate.MoldStamping = foundRow(0)("FamilyDesc").ToString()
            End If
        End If

        If p_dtbCertificateInfo.Rows(0)("ExpiryDate_I") IsNot DBNull.Value Then
            objCertificate.ExpiryDate_I = CType(p_dtbCertificateInfo.Rows(0)("ExpiryDate_I"), DateTime)
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("CountryOfManufacture_N").ToString()) Then
            objCertificate.CountryOfManufacture_N = p_dtbCertificateInfo.Rows(0)("CountryOfManufacture_N")
        End If

        If p_dtbCertificateInfo.Rows(0)("CustomerSpecific_N") IsNot DBNull.Value And _
            p_dtbCertificateInfo.Rows(0)("CustomerSpecific_N").ToString().Equals("y") Then
            objCertificate.CustomerSpecific_N = True
        End If

        If Not String.IsNullOrEmpty(foundRow(0)("CustomerID").ToString()) Then
            objCertificate.CustomerID = Convert.ToInt32(foundRow(0)("CustomerID"))
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("Customer").ToString()) Then
            objCertificate.Customer_N = p_dtbCertificateInfo.Rows(0)("Customer").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("CustomerAddress").ToString()) Then
            objCertificate.CustomerAddress_N = p_dtbCertificateInfo.Rows(0)("CustomerAddress").ToString()
        End If

        If Not String.IsNullOrEmpty(foundRow(0)("ImporterID").ToString()) Then
            objCertificate.ImporterID = Convert.ToInt32(foundRow(0)("ImporterID"))
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("Importer").ToString()) Then
            objCertificate.Importer_N = p_dtbCertificateInfo.Rows(0)("Importer").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("ImporterAddress").ToString()) Then
            objCertificate.ImporterAddress_N = p_dtbCertificateInfo.Rows(0)("ImporterAddress").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("ImporterRepresentative").ToString()) Then
            objCertificate.ImporterRepresentative_N = p_dtbCertificateInfo.Rows(0)("ImporterRepresentative").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("CountryLocation").ToString()) Then
            objCertificate.CountryLocation_N = p_dtbCertificateInfo.Rows(0)("CountryLocation").ToString()
        End If

        If Not String.IsNullOrEmpty(p_dtbCertificateInfo.Rows(0)("CompanyName").ToString()) Then
            objCertificate.CompanyName = p_dtbCertificateInfo.Rows(0)("CompanyName").ToString()
        End If

        Return objCertificate

    End Function

    ''' <summary>
    ''' Check if similar tire is in SKUTRACS
    ''' </summary>
    ''' <param name="p_strMatlNum"></param>
    ''' <param name="p_intCertificationTypeID"></param>
    ''' <param name="p_strMessage"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckSimilarTireFromSKUTRACS(ByVal p_strCertificationNumber As String, ByVal p_strMatlNum As String, ByVal p_intCertificationTypeID As Integer, ByRef p_intImarkFamily As Integer, ByRef p_strECEReference As String, ByRef p_strMessage As String) As DataTable

        Dim strSimilarMatlNum As String = String.Empty
        Dim dtbCertificateData As DataTable = Nothing
        Dim li_result As Integer

        li_result = Depository.Current.CheckSimilarTire(p_intCertificationTypeID, p_strMatlNum, strSimilarMatlNum, p_intImarkFamily, p_strECEReference, p_strMessage)

        If Not String.IsNullOrEmpty(strSimilarMatlNum) And li_result = 0 Then
            ' Check if ICS have the certificate based on the Similar tire Material number
            dtbCertificateData = Depository.Current.GetSimilarCertificate(p_intCertificationTypeID, strSimilarMatlNum, p_strCertificationNumber)

        End If

        Return dtbCertificateData

    End Function
    ''' <summary>
    ''' Chcek whether Family exists and get the value of Family Desc
    ''' </summary>
    ''' <param name="p_intFamilyId">FamilyId</param>
    ''' <param name="p_strFamilyDesc">Family Desc</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Public Function CheckIsFamilyIdExist(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As String, _
                                               ByRef p_strFamilyDesc As String) As Boolean
        Return Depository.Current.CheckIsFamilyIdExist(p_intCertificateid, p_intFamilyId, p_strFamilyDesc)
    End Function

    Public Function GetCertificateTypeID(ByVal p_strCertficateName As String) As Integer
        Return Depository.Current.GetCertificationTypeID(p_strCertficateName)
    End Function

    Public Function GetCertificationName(ByVal p_intCertificateid As Integer) As String
        Return Depository.Current.GetCertificationNameByID(p_intCertificateid)
    End Function
#End Region

End Class
