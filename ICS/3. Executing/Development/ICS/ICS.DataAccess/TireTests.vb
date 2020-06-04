Imports System.configuration
Imports System.Globalization

''' <summary>
''' Class contains tire test data operation methods.
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>Srinivas.S</term>
''' <description>
''' <para>11/19/2019</para>
''' <para>Original Code.</para>
''' </description>
''' </item> 
''' </list>
''' </remarks>
Public Class Business
    Implements IDisposable


#Region " Members "
 
    '***this will need changed to point to ics.depository layers to get to procdures in  ICS_PROCS
    'Dim m_clsData As TRACSSupportDLL.DBaseData

    ' Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
    Dim cooperServiceBroker As New cooperservicebroker.CsbPublicPI

    'Const securityKey As String = "DS3e/Yu4CC2foYQp4RG0vKD3ZIkaS0gbVV3q5rEQM3ik3NKBRuOPKA==" 'QA
    Const securityKey As String = "FxIzRezd/NokJFC0bjMrISJ4K50TPuepN9+zlNP6lrhTG8NRBmmPcQ==" 'QA new
    '    Const securityKey As String = "KjRsmGHlZShDT8DsstR23qF+UBkQC4wzNVAamKEMLevw8YHPUrP4Rw==" 'PROD
    'Const securityKey As String = ConfigurationManager.AppSettings("securitykey")

    'Private cerificationTypeList As New Hashtable()
    'Private productLocationList As New Hashtable()
#End Region

#Region " Constructors / Destructors "

#End Region

#Region " IDisposable Support "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                'free managed resources when explicitly called
                'm_clsData.Dispose()
            End If

            'free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub


    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub


#End Region

#Region " Business Calls "

    ''' <summary>
    '''  Method to Get Client Tests
    ''' </summary>
    ''' <returns>TRProductSectionData</returns> 
    ''' <param name="p_intCertType">Certificate Type</param>
    ''' <param name="p_intTireType">Tire Type</param>
    ''' <param name="p_strUseSap">Use Sap</param>
    ''' <param name="p_strUseTracs">Use Tracs</param>
    ''' <param name="p_oSet">ClientRequest Object</param>
    ''' <param name="p_blnSuccess">Success Flag</param>
    ''' <exception cref="Exception">
    '''  Logs the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function GetClientTests(ByVal p_intCertType As Integer, ByVal p_intTireType As Integer, _
                                   ByVal p_strUseSap As String, ByVal p_strUseTracs As String, _
            ByRef p_oSet As ICS.Datasets.ClientRequest, ByRef p_blnSuccess As Boolean) As ICS.Datasets.TRACStoICSDataset

        Dim strProjectNum As String = ""
        Dim intTireNum As Integer
        Dim intTestSequence As Integer
        'Dim strTestType As String
        Dim lbSuccess As Boolean = False
        'Dim lnIndex As Integer

        'Dim oData As New TRACSSupportDLL.DBaseData
        Dim stTRACSSet As New ICS.Datasets.TRACStoICSDataset

        ' Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
        Dim oClientTests As Data.DataSet
        Dim oSap As New Data.DataSet
        Dim lbSapData As Boolean = False

        Try

            ' Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
            'Split ClientRequest into SAP and Tracs
            SplitSapAndTracsDataset(p_oSet, oSap)

            'Gets from SAP
            If (String.Compare(p_strUseSap, "Y", True) = 0) Then
                If ((Not oSap Is Nothing) AndAlso (oSap.Tables.Count > 0) AndAlso (oSap.Tables(0).Rows.Count > 0)) Then

                    'obtain data from SAP
                    Dim cooperServiceBroker As New CooperServiceBroker.CsbPublicPI
                    oClientTests = cooperServiceBroker.GetClientTests(securityKey, CStr(p_intCertType), "0", oSap)

                    If ((Not oClientTests Is Nothing) AndAlso (oClientTests.Tables.Count > 0)) Then
                        If ((oClientTests.Tables("Error").Rows.Count > 0) AndAlso (Convert.ToInt16(oClientTests.Tables("Error").Rows(0).Item("ERRORNUM")) = 1)) Then

                            p_blnSuccess = True
                            lbSapData = True

                            For Each table As DataTable In oClientTests.Tables
                                'Add HighSpeed results
                                If ((String.Compare(table.TableName, "HighSpeedHdr", True) = 0 Or _
                                    String.Compare(table.TableName, "HighSpeedDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                    AddSAPHighSpeedToTRACSData(table, stTRACSSet)
                                End If

                                ' Add Endurance results
                                If ((String.Compare(table.TableName, "EnduranceHdr", True) = 0 Or _
                                   String.Compare(table.TableName, "EnduranceDtl", True) = 0 Or _
                                   String.Compare(table.TableName, "EndLowInfDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                    AddSAPEnduranceToTRACSData(table, stTRACSSet)
                                End If

                                '  Add Measurement Results
                                If ((String.Compare(table.TableName, "MeasureHdr", True) = 0 Or _
                                    String.Compare(table.TableName, "MeasureDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                    AddSAPMeasureToTRACSData(table, stTRACSSet)
                                End If

                                '  Add Treadwear Indicator Results
                                If ((String.Compare(table.TableName, "TreadWearHdr", True) = 0 Or _
                                    String.Compare(table.TableName, "TreadWearDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                    AddSAPTreadWearToTRACSData(table, stTRACSSet)
                                End If

                                '  Add Plunger Results
                                If ((String.Compare(table.TableName, "PlungerHdr", True) = 0 Or _
                                    String.Compare(table.TableName, "PlungerDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                    AddSAPPlungerToTRACSData(table, stTRACSSet)
                                End If

                                '  Add BeadUnseat Results
                                If ((String.Compare(table.TableName, "BeadUnseatHdr", True) = 0 Or _
                                    String.Compare(table.TableName, "BeadUnseatDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                    AddSAPBeadUnseatToTRACSData(table, stTRACSSet)
                                End If
                            Next

                        Else
                            p_blnSuccess = False
                        End If
                    End If
                End If
            End If

        Catch AppErr As Exception
            p_blnSuccess = False
            Dim AppData As String
            AppData = "CertType:" & CStr(p_intCertType) & " TireType:" & CStr(p_intTireType) & " Proj:" & strProjectNum & " Tire:" & CStr(intTireNum) & " Seq:" & CStr(intTestSequence)
            'AppError(AppData, AppErr)
        Finally
        End Try
        Return stTRACSSet
    End Function

    ''' <summary>
    '''  Method to Get TRACS Data
    ''' </summary>
    ''' <returns>TRProductSectionData</returns> 
    ''' <param name="p_intCertType">intCertType</param>
    ''' <param name="p_intTireType">intTireType</param>
    ''' <param name="p_strSKU">strSKU</param>
    ''' <param name="p_intProdLocNum">intProdLocNum</param>
    ''' <param name="p_strUseSap">strUseSap</param>
    ''' <param name="p_strUseTracs">strUseTracs</param>
    ''' <param name="p_blnSuccess">blnSuccess</param>
    ''' <exception cref="Exception">
    '''  Logs the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function GetTRACSData(ByVal p_intCertType As Integer, ByVal p_intTireType As Integer, ByVal p_strSKU As _
                            String, ByVal p_intProdLocNum As Integer, ByVal p_strUseSap As String, ByVal p_strUseTracs As String, _
                            ByRef p_blnSuccess As Boolean) As ICS.Datasets.TRACStoICSDataset
        Const ErrorText As String = "Error"
        Const ErrorNumText As String = "ERRORNUM"
        Const HighSpeedHdrText As String = "HighSpeedHdr"
        Const HighSpeedDtlText As String = "HighSpeedDtl"
        Const YText As String = "Y"
        Const EnduranceHdrText As String = "EnduranceHdr"
        Const EnduranceDtlText As String = "EnduranceDtl"
        Const EndLowInfDtltext As String = "EndLowInfDtl"
        Const MeasureHdrText As String = "MeasureHdr"

        ' Dim oData As New TRACSSupportDLL.DBaseData
        Dim stTRACSSet As New ICS.Datasets.TRACStoICSDataset
        Dim stSKUSet As New ICS.Datasets.SKUtoICSDataset
        Dim oSet As Data.DataSet
        Dim lnRowIndex As Int16
        Dim lnTableIndex As Int16
        Dim oTable As DataTable
        Dim ldLatestSerialDate As Date
        Dim ldLatestTestDate As Date = #1/1/1800#
        Dim lsDOTSerialNumber As String = String.Empty
        Dim lnMeaRimWidth As Single
        Dim lsDOTPlant As String = String.Empty
        Dim lsPlantName As String = String.Empty
        Dim lsSKU As String = String.Empty

        ' Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
        Dim lbHSSap As Boolean = False
        Dim lbENDSap As Boolean = False
        Dim lbMEASap As Boolean = False
        Dim lbTWISap As Boolean = False
        Dim lbPLGSap As Boolean = False
        Dim lbBDUSap As Boolean = False

        Dim lbSapData As Boolean = False

        Try
            ' Added as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
            'Gets from SAP
            If (String.Compare(p_strUseSap, YText, True) = 0) Then
                Dim cooperServiceBroker As New CooperServiceBroker.CsbPublicPI

                oSet = cooperServiceBroker.GetTestingData(securityKey, CStr(p_intCertType), p_strSKU, CStr(0))

                'Check for each test returned, and add to TracsDataSet.  
                If ((Not oSet Is Nothing) AndAlso (oSet.Tables.Count > 0)) Then
                    If ((oSet.Tables(ErrorText).Rows.Count > 0) AndAlso (Convert.ToInt16(oSet.Tables(ErrorText).Rows(0).Item(ErrorNumText)) = 1)) Then

                        p_blnSuccess = True
                        lbSapData = True

                        For Each table As DataTable In oSet.Tables
                            'Add HighSpeed results
                            If ((String.Compare(table.TableName, HighSpeedHdrText, True) = 0 Or _
                                     String.Compare(table.TableName, HighSpeedDtlText, True) = 0) AndAlso table.Rows.Count > 0) Then
                                lbHSSap = True
                                AddSAPHighSpeedToTRACSData(table, stTRACSSet)
                            End If

                            ' Add Endurance results
                            If ((String.Compare(table.TableName, EnduranceHdrText, True) = 0 Or _
                                String.Compare(table.TableName, EnduranceDtlText, True) = 0 Or _
                                String.Compare(table.TableName, EndLowInfDtltext, True) = 0) AndAlso table.Rows.Count > 0) Then
                                lbENDSap = True
                                AddSAPEnduranceToTRACSData(table, stTRACSSet)
                            End If

                            '  Add Measure results
                            If ((String.Compare(table.TableName, MeasureHdrText, True) = 0 Or _
                                String.Compare(table.TableName, "MeasureDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                lbMEASap = True
                                AddSAPMeasureToTRACSData(table, stTRACSSet)
                            End If

                            '  Add Treadwear results
                            If ((String.Compare(table.TableName, "TreadWearHdr", True) = 0 Or _
                                String.Compare(table.TableName, "TreadWearDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                lbTWISap = True
                                AddSAPTreadWearToTRACSData(table, stTRACSSet)
                            End If

                            '  Add Plunger results
                            If ((String.Compare(table.TableName, "PlungerHdr", True) = 0 Or _
                                String.Compare(table.TableName, "PlungerDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                lbPLGSap = True
                                AddSAPPlungerToTRACSData(table, stTRACSSet)
                            End If

                            '  Add BeadUnseat results
                            If ((String.Compare(table.TableName, "BeadUnseatHdr", True) = 0 Or _
                                String.Compare(table.TableName, "BeadUnseatDtl", True) = 0) AndAlso table.Rows.Count > 0) Then
                                lbBDUSap = True
                                AddSAPBeadUnseatToTRACSData(table, stTRACSSet)
                            End If
                        Next
                    Else
                        p_blnSuccess = False
                    End If
                End If
            End If

            '  Add logic to cycle through header tables for greatest serial date
            '  Loop Thru TRACS Data and Find Latest Serial Date with corresponding 
            '  Serial Number and Rim Width
            For lnTableIndex = 0 To CShort(stTRACSSet.Tables.Count - 1)
                oTable = stTRACSSet.Tables(lnTableIndex)
                If Mid(oTable.TableName, oTable.TableName.Length - 2, 3) = "Hdr" Then
                    For lnRowIndex = 0 To CShort(oTable.Rows.Count - 1)
                        If oTable.TableName = MeasureHdrText Then
                            lnMeaRimWidth = CSng(oTable.Rows(lnRowIndex).Item("RimWidth"))
                        End If
                        If (Not oTable.Rows(lnRowIndex).Item("CompletionDate") Is System.DBNull.Value _
                                AndAlso Not String.IsNullOrEmpty(CStr(oTable.Rows(lnRowIndex).Item("CompletionDate")))) Then
                            ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
                            If CDate(ConvertToDate(CStr(oTable.Rows(lnRowIndex).Item("CompletionDate")))) > ldLatestTestDate Then
                                If (oTable.Rows(lnRowIndex).Item("SerialDate").ToString <> "") Then
                                    ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
                                    ldLatestSerialDate = CDate(ConvertToDate(CStr(oTable.Rows(lnRowIndex).Item("SerialDate"))))
                                End If
                                lsDOTSerialNumber = CStr(oTable.Rows(lnRowIndex).Item("DOTSerialNumber"))
                                ' Changed as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265) SAP to ICS.
                                ldLatestTestDate = CDate(ConvertToDate(CStr(oTable.Rows(lnRowIndex).Item("CompletionDate"))))
                                lsDOTPlant = Mid(CStr(oTable.Rows(lnRowIndex).Item("DOTSerialNumber")), 1, 2)
                            End If
                        End If
                    Next
                End If
            Next

            'GetPlantLocation(lsDOTPlant, lsPlantName)
            lsPlantName = "Unknown"

            '  Update Recent Test Data Table with information found above
            Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.RecentTestDataRow = stTRACSSet.RecentTestData.NewRecentTestDataRow
            sTRACSHdrRow.PlantProduced = lsPlantName
            sTRACSHdrRow.MostRecentTestDate = CStr(ldLatestTestDate)
            sTRACSHdrRow.DOTSerialNumber = lsDOTSerialNumber
            sTRACSHdrRow.SerialDate = CStr(ldLatestSerialDate)
            sTRACSHdrRow.MeaRimWidth = lnMeaRimWidth
            stTRACSSet.RecentTestData.AddRecentTestDataRow(sTRACSHdrRow)

        Catch AppErr As Exception
            p_blnSuccess = False
            Dim AppData As String
            AppData = "CertType:" & CStr(p_intCertType) & " TireType:" & CStr(p_intTireType) & " SKU:" & p_strSKU & " ProdLoc:" & CStr(p_intProdLocNum)
            'AppError(AppData, AppErr)
        Finally
            stSKUSet.Dispose()
        End Try
        Return stTRACSSet
    End Function

    'Changed as per Incident # 31208 and Change Order # 6074
    ''' <summary>
    '''  Method to add Product data to TRACS
    ''' </summary>
    ''' <param name="p_oSet">DataSet Object</param>
    ''' <param name="p_stSKU">SKUtoICSDataset Object</param>    
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddProductDataToTRACSData(ByVal p_oSet As System.Data.DataSet, ByRef p_stSKU As ICS.Datasets.SKUtoICSDataset)

        Dim i As Int16

        Try
            '   Add TreadWear Indicators Header to Result Set
            If p_oSet.Tables(0).TableName = "ProductData" Then
                ConvertNulls(p_oSet.Tables(0))
                For i = 0 To CShort(p_oSet.Tables(0).Rows.Count - 1)
                    Dim sSKUHdrRow As ICS.Datasets.SKUtoICSDataset.ProductDataRow = p_stSKU.ProductData.NewProductDataRow
                    sSKUHdrRow.SKU = CStr(p_oSet.Tables(0).Rows(i).Item("SKU"))
                    sSKUHdrRow.BrandDesc = CStr(p_oSet.Tables(0).Rows(i).Item("BRANDDESC"))
                    sSKUHdrRow.SerialDate = CStr(p_oSet.Tables(0).Rows(i).Item("SERIALDATE"))
                    sSKUHdrRow.DOTSerialNumber = CStr(p_oSet.Tables(0).Rows(i).Item("DOTSERIALNUMBER"))
                    sSKUHdrRow.SizeStamp = CStr(p_oSet.Tables(0).Rows(i).Item("SIZESTAMP"))
                    sSKUHdrRow.SpeedRating = CStr(p_oSet.Tables(0).Rows(i).Item("SPEEDRATING"))
                    sSKUHdrRow.SingLoadIndex = CStr(p_oSet.Tables(0).Rows(i).Item("SINGLOADINDEX"))
                    sSKUHdrRow.DualLoadIndex = CStr(p_oSet.Tables(0).Rows(i).Item("DUALLOADINDEX"))
                    sSKUHdrRow.BiasBeltedRadial = CStr(p_oSet.Tables(0).Rows(i).Item("BIASBELTEDRADIAL"))
                    sSKUHdrRow.TubelessYN = CStr(p_oSet.Tables(0).Rows(i).Item("TUBELESS"))
                    sSKUHdrRow.ReinforcedYN = CStr(p_oSet.Tables(0).Rows(i).Item("REINFORCEDYN"))
                    sSKUHdrRow.ExtraLoadYN = CStr(p_oSet.Tables(0).Rows(i).Item("EXTRALOADYN"))
                    sSKUHdrRow.UTQGTreadwear = CStr(p_oSet.Tables(0).Rows(i).Item("UTQGTREADWEAR"))
                    sSKUHdrRow.UTQGTraction = CStr(p_oSet.Tables(0).Rows(i).Item("UTQGTRACTION"))
                    sSKUHdrRow.UTQGTemp = CStr(p_oSet.Tables(0).Rows(i).Item("UTQGTEMP"))
                    sSKUHdrRow.MudSnowYN = CStr(p_oSet.Tables(0).Rows(i).Item("MUDSNOWYN"))
                    sSKUHdrRow.RimDiameter = ConvertToSingle(CStr(p_oSet.Tables(0).Rows(i).Item("RIMDIAMETER")))
                    sSKUHdrRow.LoadRange = CStr(p_oSet.Tables(0).Rows(i).Item("LOADRANGE"))
                    sSKUHdrRow.MeaRimWidth = ConvertToSingle(CStr(p_oSet.Tables(0).Rows(i).Item("MEARIMWIDTH")))
                    sSKUHdrRow.RegroovableInd = CStr(p_oSet.Tables(0).Rows(i).Item("REGROOVABLEIND"))
                    sSKUHdrRow.PlantProduced = CStr(p_oSet.Tables(0).Rows(i).Item("PLANTPRODUCED"))
                    sSKUHdrRow.MostRecentTestDate = CStr(p_oSet.Tables(0).Rows(i).Item("MOSTRECENTTESTDATE"))
                    sSKUHdrRow.IMark = CStr(p_oSet.Tables(0).Rows(i).Item("IMARK"))
                    sSKUHdrRow.PPN = CStr(p_oSet.Tables(0).Rows(i).Item("TECHNICALPLATFORM"))
                    sSKUHdrRow.AspectRatio = CStr(p_oSet.Tables(0).Rows(i).Item("ASPECTRATIO"))
                    sSKUHdrRow.MFGWWYY = CStr(p_oSet.Tables(0).Rows(i).Item("MFGWWYY"))
                    sSKUHdrRow.SevereWeatherInd = CStr(p_oSet.Tables(0).Rows(i).Item("SevereWeatherInd"))
                    sSKUHdrRow.TireTypeID = CStr(p_oSet.Tables(0).Rows(i).Item("TireTypeID"))
                    sSKUHdrRow.TreadPattern = CStr(p_oSet.Tables(0).Rows(i).Item("TreadPattern"))
                    p_stSKU.ProductData.AddProductDataRow(sSKUHdrRow)
                Next
            End If
        Catch
            Throw
        End Try
       
    End Sub

    ''' <summary>
    '''  Method to add SAP High Speed To TRACS Data
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <param name="p_stTRACS">TRACStoICSDataset Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddSAPHighSpeedToTRACSData(ByVal p_oTable As System.Data.DataTable, ByRef p_stTRACS As ICS.Datasets.TRACStoICSDataset)
        Dim rowCount As Int16

        Try
            If p_oTable.TableName = "HighSpeedHdr" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.HighSpeedHdrRow = p_stTRACS.HighSpeedHdr.NewHighSpeedHdrRow
                    sTRACSHdrRow.ProjectNum = CStr(p_oTable.Rows(rowCount).Item("PROJECTNUM"))
                    sTRACSHdrRow.TireNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TIRENUM")))
                    sTRACSHdrRow.TestSpec = CStr(p_oTable.Rows(rowCount).Item("TESTSPEC"))
                    sTRACSHdrRow.TestSKU = CStr(p_oTable.Rows(rowCount).Item("TESTSKU"))
                    sTRACSHdrRow.CompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("COMPLETIONDATE"))))
                    sTRACSHdrRow.DOTSerialNumber = CStr(p_oTable.Rows(rowCount).Item("DOTSERIALNUMBER"))
                    sTRACSHdrRow.SerialDate = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                    sTRACSHdrRow.PreCondStartDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("PRECONDSTARTDATE"))))
                    sTRACSHdrRow.PreCondSartTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("PRECONDSTARTTEMP")))
                    sTRACSHdrRow.RimDiameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("RIMDIAMETER")))
                    sTRACSHdrRow.RimWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("RIMWIDTH")))
                    sTRACSHdrRow.PreCondEndDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("PRECONDENDDATE"))))
                    sTRACSHdrRow.PreCondEndTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("PRECONDENDTEMP")))
                    sTRACSHdrRow.PreCondTime = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("PRECONDTIME")))
                    sTRACSHdrRow.InflationPressure = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("INFLATIONPRESSURE")))
                    sTRACSHdrRow.BeforeDiameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("BEFOREDIAMETER")))
                    sTRACSHdrRow.AfterDiameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("AFTERDIAMETER")))
                    sTRACSHdrRow.BeforeInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("BEFOREINFLATION")))
                    sTRACSHdrRow.AfterInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("AFTERINFLATION")))
                    sTRACSHdrRow.WheelPosition = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("WHEELPOSITION")))
                    sTRACSHdrRow.WheelNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("WHEELNUM")))
                    sTRACSHdrRow.FinalTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("FINALTEMP")))
                    sTRACSHdrRow.FinalDistance = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("FINALDISTANCE")))
                    sTRACSHdrRow.FinalInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("FINALINFLATION")))
                    sTRACSHdrRow.PostCondStartDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("POSTCONDSTARTDATE"))))
                    sTRACSHdrRow.PostCondEndDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("POSTCONDENDDATE"))))
                    sTRACSHdrRow.PostCondTime = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("POSTCONDTIME")))
                    sTRACSHdrRow.PostCondEndTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("POSTCONDENDTEMP")))
                    sTRACSHdrRow.PassYN = CStr(p_oTable.Rows(rowCount).Item("PASSYN"))
                    sTRACSHdrRow.AnalysisCode = CStr(p_oTable.Rows(rowCount).Item("ANALYSISCODE"))
                    sTRACSHdrRow.CompletedBy = CStr(p_oTable.Rows(rowCount).Item("COMPLETEDBY"))
                    sTRACSHdrRow.Operation = CStr(p_oTable.Rows(rowCount).Item("OperationDescription"))
                    sTRACSHdrRow.MFGWWYY = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                    sTRACSHdrRow.GTSPEC = CStr(p_oTable.Rows(rowCount).Item("GTSpec"))
                    p_stTRACS.HighSpeedHdr.AddHighSpeedHdrRow(sTRACSHdrRow)
                Next
            End If

            '   Add High Speed Detail to Result Set
            If p_oTable.TableName = "HighSpeedDtl" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.HighSpeedDtlRow = p_stTRACS.HighSpeedDtl.NewHighSpeedDtlRow
                    sTRACSDtlRow.TestStep = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TestStep")))
                    sTRACSDtlRow.TimeInMin = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TimeInMin")))
                    sTRACSDtlRow.Speed = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Speed")))
                    sTRACSDtlRow.TotMiles = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("TotDistance")))
                    sTRACSDtlRow.Load = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Load")))
                    sTRACSDtlRow.LoadPercent = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("LoadPercent")))
                    sTRACSDtlRow.SetInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("SetInflation")))
                    sTRACSDtlRow.AmbTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("AmbTemp")))
                    sTRACSDtlRow.InfPressure = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("InfPressure")))
                    sTRACSDtlRow.StepCompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("StepCompletionDate"))))
                    p_stTRACS.HighSpeedDtl.AddHighSpeedDtlRow(sTRACSDtlRow)
                Next
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to add SAP Endurance To TRACS Data
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <param name="p_stTRACS">TRACStoICSDataset Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddSAPEnduranceToTRACSData(ByVal p_oTable As System.Data.DataTable, ByRef p_stTRACS As ICS.Datasets.TRACStoICSDataset)
        Dim rowCount As Int16

        Dim iLowStep As Integer

        Try
            '   Add Endurance Header to Result Set
            If p_oTable.TableName = "EnduranceHdr" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.EnduranceHdrRow = p_stTRACS.EnduranceHdr.NewEnduranceHdrRow
                    sTRACSHdrRow.ProjectNum = CStr(p_oTable.Rows(rowCount).Item("ProjectNum"))
                    sTRACSHdrRow.TireNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TireNum")))
                    sTRACSHdrRow.TestSpec = CStr(p_oTable.Rows(rowCount).Item("TestSpec"))
                    sTRACSHdrRow.TestSKU = CStr(p_oTable.Rows(rowCount).Item("TestSKU"))
                    sTRACSHdrRow.CompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("CompletionDate"))))
                    sTRACSHdrRow.DOTSerialNumber = CStr(p_oTable.Rows(rowCount).Item("DOTSerialNumber"))
                    sTRACSHdrRow.SerialDate = CStr(p_oTable.Rows(rowCount).Item("SerialDate"))
                    sTRACSHdrRow.PreCondStartDate = CStr(p_oTable.Rows(rowCount).Item("PreCondStartDate"))
                    sTRACSHdrRow.PreCondSartTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("PreCondSartTemp")))
                    sTRACSHdrRow.RimDiameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("RimDiameter")))
                    sTRACSHdrRow.RimWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("RimWidth")))
                    sTRACSHdrRow.PreCondEndDate = CStr(p_oTable.Rows(rowCount).Item("PreCondEndDate"))
                    sTRACSHdrRow.PreCondEndTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("PreCondEndTemp")))
                    sTRACSHdrRow.InflationPressure = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("InflationPressure")))
                    sTRACSHdrRow.BeforeDiameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("BeforeDiameter")))
                    sTRACSHdrRow.AfterDiameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("AfterDiameter")))
                    sTRACSHdrRow.BeforeInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("BeforeInflation")))
                    sTRACSHdrRow.AfterInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("AfterInflation")))
                    sTRACSHdrRow.WheelPosition = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("WheelPosition")))
                    sTRACSHdrRow.WheelNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("WheelNum")))
                    sTRACSHdrRow.LowInfEndTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("LowInfEndTemp")))
                    sTRACSHdrRow.LowInfTotalDistance = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("LowInfTotalDistance")))
                    sTRACSHdrRow.LowInfStartInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("LowInfStartInflation")))
                    sTRACSHdrRow.LowInfEndInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("LowInfEndInflation")))
                    sTRACSHdrRow.FinalTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("FinalTemp")))
                    sTRACSHdrRow.FinalDistance = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("FinalDistance")))
                    sTRACSHdrRow.FinalInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("FinalInflation")))
                    sTRACSHdrRow.PostCondStartDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("PostCondStartDate"))))
                    sTRACSHdrRow.PostCondEndDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("PostCondEndDate"))))
                    sTRACSHdrRow.PostCondEndTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("PostCondEndTemp")))
                    sTRACSHdrRow.PostCondTime = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("PostCondTime")))
                    If sTRACSHdrRow.PostCondTime > 32767 Then 'checking for invalid data from SAP.
                        sTRACSHdrRow.PostCondTime = 0
                    End If
                    sTRACSHdrRow.PassYN = CStr(p_oTable.Rows(rowCount).Item("PassYN"))
                    sTRACSHdrRow.AnalysisCode = CStr(p_oTable.Rows(rowCount).Item("AnalysisCode"))
                    sTRACSHdrRow.CompletedBy = CStr(p_oTable.Rows(rowCount).Item("CompletedBy"))
                    sTRACSHdrRow.Operation = CStr(p_oTable.Rows(rowCount).Item("OperationDescription"))
                    sTRACSHdrRow.MFGWWYY = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                    sTRACSHdrRow.GTSPEC = CStr(p_oTable.Rows(rowCount).Item("GTSpec"))
                    p_stTRACS.EnduranceHdr.AddEnduranceHdrRow(sTRACSHdrRow)
                Next
            End If

            'Need to split out low inflation steps

            '   Add Endurance Detail to Result Set
            If p_oTable.TableName = "EnduranceDtl" Then
                iLowStep = 0
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    If ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TestStep"))) <> 0 And _
                       p_oTable.Rows(rowCount).Item("TimeInMin") Is "" And _
                        p_oTable.Rows(rowCount).Item("Speed") Is "" And _
                        p_oTable.Rows(rowCount).Item("TotDistance") Is "" Then
                        'skip steps that don't have values (steps being returned even though they weren't completed.jeseitz 6/25/2014
                    Else

                        Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.EnduranceDtlRow = p_stTRACS.EnduranceDtl.NewEnduranceDtlRow
                        sTRACSDtlRow.TestStep = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TestStep")))
                        sTRACSDtlRow.TimeInMin = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TimeInMin")))
                        sTRACSDtlRow.Speed = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Speed")))
                        sTRACSDtlRow.TotMiles = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("TotDistance")))
                        sTRACSDtlRow.Load = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Load")))
                        sTRACSDtlRow.LoadPercent = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("LoadPercent")))
                        sTRACSDtlRow.SetInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("SetInflation")))
                        sTRACSDtlRow.AmbTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("AmbTemp")))
                        sTRACSDtlRow.InfPressure = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("InfPressure")))
                        sTRACSDtlRow.StepCompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("StepCompletionDate"))))
                        'Don't add it if set inflation > 0 after the first step - this is the start of the low inflation
                        If iLowStep = 0 And (rowCount > 0 And sTRACSDtlRow.SetInflation > 0) Then
                            iLowStep = rowCount
                        End If
                        If iLowStep = 0 Then
                            p_stTRACS.EnduranceDtl.AddEnduranceDtlRow(sTRACSDtlRow)
                        End If
                    End If
                Next
            End If

            '   Add Endurance Low Inflation Detail to Result Set
            If p_oTable.TableName = "EndLowInfDtl" Then
                If iLowStep > 0 Then
                    ConvertNulls(p_oTable)
                    For rowCount = CShort(iLowStep) To CShort(p_oTable.Rows.Count - 1)
                        If ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TestStep"))) <> 0 And _
                           p_oTable.Rows(rowCount).Item("TimeInMin") Is "" And _
                            p_oTable.Rows(rowCount).Item("Speed") Is "" And _
                            p_oTable.Rows(rowCount).Item("TotDistance") Is "" Then
                        Else

                            Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.EndLowInfDtlRow = p_stTRACS.EndLowInfDtl.NewEndLowInfDtlRow
                            sTRACSDtlRow.TestStep = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TestStep")))
                            sTRACSDtlRow.TimeInMin = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TimeInMin")))
                            sTRACSDtlRow.Speed = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Speed")))
                            sTRACSDtlRow.TotMiles = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("TotDistance")))
                            sTRACSDtlRow.Load = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Load")))
                            sTRACSDtlRow.LoadPercent = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("LoadPercent")))
                            sTRACSDtlRow.SetInflation = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("SetInflation")))
                            sTRACSDtlRow.AmbTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("AmbTemp")))
                            sTRACSDtlRow.InfPressure = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("InfPressure")))
                            sTRACSDtlRow.StepCompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("StepCompletionDate"))))
                            p_stTRACS.EndLowInfDtl.AddEndLowInfDtlRow(sTRACSDtlRow)
                        End If
                    Next
                End If
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to add SAP Measure To TRACS Data
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <param name="p_stTRACS">TRACStoICSDataset Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddSAPMeasureToTRACSData(ByVal p_oTable As System.Data.DataTable, ByRef p_stTRACS As ICS.Datasets.TRACStoICSDataset)
        'JESEITZ 6/22/2015 - REQUEST 153828 - Add new fields MAXIMUM_OVERALL_GROWN, MAXIMUM_OUTER_DIAMETER_NEW,MIN_OUTER_DIAMETER_NEW
        Dim rowCount As Int16

        '   Add Measure Header to Result Set
        If p_oTable.TableName = "MeasureHdr" Then
            ConvertNulls(p_oTable)
            For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.MeasureHdrRow = p_stTRACS.MeasureHdr.NewMeasureHdrRow
                sTRACSHdrRow.ProjectNum = CStr(p_oTable.Rows(rowCount).Item("ProjectNum"))
                sTRACSHdrRow.TireNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TireNum")))
                sTRACSHdrRow.TestSpec = CStr(p_oTable.Rows(rowCount).Item("TestSpec"))
                sTRACSHdrRow.TestSKU = CStr(p_oTable.Rows(rowCount).Item("TestSKU"))
                sTRACSHdrRow.CompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("CompletionDate"))))
                sTRACSHdrRow.InflationPressure = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("InflationPressure")))
                sTRACSHdrRow.MoldDesign = CStr(p_oTable.Rows(rowCount).Item("MoldDesign"))
                sTRACSHdrRow.RimWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("RimWidth")))
                sTRACSHdrRow.DOTSerialNumber = CStr(p_oTable.Rows(rowCount).Item("DOTSerialNumber"))
                sTRACSHdrRow.SerialDate = CStr(p_oTable.Rows(rowCount).Item("SerialDate"))
                sTRACSHdrRow.Diameter = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("Diameter")))
                sTRACSHdrRow.AvgSectionWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("AvgSectionWidth")))
                sTRACSHdrRow.AvgOverallWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("AvgOverallWidth")))
                sTRACSHdrRow.MAXOVERALLWIDTHNEW = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("MaxOverallWidth")))
                If p_oTable.Columns.Contains("MAXIMUM_OVERALL_WIDTH_GROWN") Then
                    sTRACSHdrRow.MaxOverallWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("MAXIMUM_OVERALL_WIDTH_GROWN")))
                Else
                    sTRACSHdrRow.MaxOverallWidth = ConvertToSingle(Nothing)
                End If
                sTRACSHdrRow.MinSizeFactor = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("MinSizeFactor")))
                sTRACSHdrRow.MountTime = CStr(p_oTable.Rows(rowCount).Item("MountTime"))
                sTRACSHdrRow.MountTemp = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("MountTemp")))
                sTRACSHdrRow.EndTime = CStr(p_oTable.Rows(rowCount).Item("EndTime"))
                sTRACSHdrRow.ActSizeFactor = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("ActSizeFactor")))
                sTRACSHdrRow.PassYN = CStr(p_oTable.Rows(rowCount).Item("PassYN"))
                sTRACSHdrRow.Operation = CStr(p_oTable.Rows(rowCount).Item("OperationDescription"))
                sTRACSHdrRow.MFGWWYY = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                sTRACSHdrRow.GTSPEC = CStr(p_oTable.Rows(rowCount).Item("GTSpec"))
                If p_oTable.Columns.Contains("MAXIMUM_OUTER_DIAMETER_NEW") Then
                    sTRACSHdrRow.MAXOVERALLDIAMETER = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("MAXIMUM_OUTER_DIAMETER_NEW")))
                Else
                    sTRACSHdrRow.MAXOVERALLDIAMETER = ConvertToSingle(Nothing)
                End If
                If p_oTable.Columns.Contains("MIN_OUTER_DIAMETER_NEW") Then
                    sTRACSHdrRow.MINOVERALLDIAMETER = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("MIN_OUTER_DIAMETER_NEW")))
                Else
                    sTRACSHdrRow.MINOVERALLDIAMETER = ConvertToSingle(Nothing)
                End If
                p_stTRACS.MeasureHdr.AddMeasureHdrRow(sTRACSHdrRow)
            Next
        End If

        '   Add Measure Detail to Result Set
        If p_oTable.TableName = "MeasureDtl" Then
            ConvertNulls(p_oTable)
            For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.MeasureDtlRow = p_stTRACS.MeasureDtl.NewMeasureDtlRow
                sTRACSDtlRow.Iteration = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("Iteration")))
                sTRACSDtlRow.SectionWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("SectionWidth")))
                sTRACSDtlRow.OverallWidth = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("OverallWidth")))
                p_stTRACS.MeasureDtl.AddMeasureDtlRow(sTRACSDtlRow)
            Next
        End If
    End Sub

    ''' <summary>
    '''  Method to add SAP Tread Wear To TRACS Data
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <param name="p_stTRACS">TRACStoICSDataset Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddSAPTreadWearToTRACSData(ByVal p_oTable As System.Data.DataTable, ByRef p_stTRACS As ICS.Datasets.TRACStoICSDataset)
        Dim rowCount As Int16
        Dim nLowestTreadwearInd As Single = 10

        Try
            '   Add TreadWear Indicators Header to Result Set
            If p_oTable.TableName = "TreadWearHdr" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.TreadWearHdrRow = p_stTRACS.TreadWearHdr.NewTreadWearHdrRow
                    sTRACSHdrRow.ProjectNum = CStr(p_oTable.Rows(rowCount).Item("ProjectNum"))
                    sTRACSHdrRow.TireNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TireNum")))
                    sTRACSHdrRow.TestSpec = CStr(p_oTable.Rows(rowCount).Item("TestSpec"))
                    sTRACSHdrRow.TestSKU = CStr(p_oTable.Rows(rowCount).Item("TestSKU"))
                    sTRACSHdrRow.CompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("CompletionDate"))))
                    sTRACSHdrRow.DOTSerialNumber = CStr(p_oTable.Rows(rowCount).Item("DOTSerialNumber"))
                    sTRACSHdrRow.SerialDate = CStr(p_oTable.Rows(rowCount).Item("SerialDate"))
                    sTRACSHdrRow.LowestWearbar = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("LowestWearbar")))
                    sTRACSHdrRow.PassYN = CStr(p_oTable.Rows(rowCount).Item("PassYN"))
                    sTRACSHdrRow.Operation = CStr(p_oTable.Rows(rowCount).Item("OperationDescription"))
                    sTRACSHdrRow.MFGWWYY = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                    'sTRACSHdrRow.GTSPEC = oTable.Rows(rowCount).Item("GTSpec")
                    sTRACSHdrRow.GTSPEC = ""
                    p_stTRACS.TreadWearHdr.AddTreadWearHdrRow(sTRACSHdrRow)
                Next
            End If

            '   Add TreadWear Indicators Detail to Result Set
            If p_oTable.TableName = "TreadWearDtl" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.TreadWearDtlRow = p_stTRACS.TreadWearDtl.NewTreadWearDtlRow
                    sTRACSDtlRow.ITERATION = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("ITERATION")))
                    sTRACSDtlRow.WearbarHeight = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("WearbarHeight")))
                    If CDbl(p_oTable.Rows(rowCount).Item("WearbarHeight")) < nLowestTreadwearInd Then
                        nLowestTreadwearInd = ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("WearbarHeight")))
                    End If
                    p_stTRACS.TreadWearDtl.AddTreadWearDtlRow(sTRACSDtlRow)
                Next
            End If
            p_stTRACS.TreadWearHdr(0).LowestWearbar = nLowestTreadwearInd
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    '''  Method to add SAP Plunger To TRACS Data
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <param name="p_stTRACS">TRACStoICSDataset Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddSAPPlungerToTRACSData(ByVal p_oTable As System.Data.DataTable, ByRef p_stTRACS As ICS.Datasets.TRACStoICSDataset)
        Dim rowCount As Int16

        Try
            '   Add Plunger Header to Result Set
            If p_oTable.TableName = "PlungerHdr" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.PlungerHdrRow = p_stTRACS.PlungerHdr.NewPlungerHdrRow
                    sTRACSHdrRow.ProjectNum = CStr(p_oTable.Rows(rowCount).Item("ProjectNum"))
                    sTRACSHdrRow.TireNum = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("TireNum")))
                    sTRACSHdrRow.TestSpec = CStr(p_oTable.Rows(rowCount).Item("TestSpec"))
                    sTRACSHdrRow.TestSKU = CStr(p_oTable.Rows(rowCount).Item("TestSKU"))
                    sTRACSHdrRow.CompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("CompletionDate"))))
                    sTRACSHdrRow.DOTSerialNumber = CStr(p_oTable.Rows(rowCount).Item("DOTSerialNumber"))
                    sTRACSHdrRow.SerialDate = CStr(p_oTable.Rows(rowCount).Item("SerialDate"))
                    sTRACSHdrRow.AvgBreakingEnergy = ConvertToInt(CStr(p_oTable.Rows(rowCount).Item("AvgBreakingEnergy")))
                    sTRACSHdrRow.PassYN = CStr(p_oTable.Rows(rowCount).Item("PassYN"))
                    sTRACSHdrRow.MinPlunger = ConvertToLong(CStr(p_oTable.Rows(rowCount).Item("MinPlunger")))
                    sTRACSHdrRow.Operation = CStr(p_oTable.Rows(rowCount).Item("OperationDescription"))
                    sTRACSHdrRow.MFGWWYY = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                    sTRACSHdrRow.GTSPEC = CStr(p_oTable.Rows(rowCount).Item("GTSpec"))
                    p_stTRACS.PlungerHdr.AddPlungerHdrRow(sTRACSHdrRow)
                Next
            End If

            '   Add Plunger Detail to Result Set
            If p_oTable.TableName = "PlungerDtl" Then
                ConvertNulls(p_oTable)
                For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                    Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.PlungerDtlRow = p_stTRACS.PlungerDtl.NewPlungerDtlRow
                    sTRACSDtlRow.ITERATION = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("ITERATION")))
                    sTRACSDtlRow.BreakingEnergy = ConvertToInt(CStr(p_oTable.Rows(rowCount).Item("BreakingEnergy")))
                    p_stTRACS.PlungerDtl.AddPlungerDtlRow(sTRACSDtlRow)
                Next
            End If
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Method to add SAP Bead Unseat To TRACS Data
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <param name="p_stTRACS">TRACStoICSDataset Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub AddSAPBeadUnseatToTRACSData(ByVal p_oTable As System.Data.DataTable, ByRef p_stTRACS As ICS.Datasets.TRACStoICSDataset)
        Dim rowCount As Int16

        '   Add Bead Unseat Header to Result Set
        If p_oTable.TableName = "BeadUnseatHdr" Then
            ConvertNulls(p_oTable)
            For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                Dim sTRACSHdrRow As ICS.Datasets.TRACStoICSDataset.BeadUnseatHdrRow = p_stTRACS.BeadUnseatHdr.NewBeadUnseatHdrRow
                sTRACSHdrRow.ProjectNum = CStr(p_oTable.Rows(rowCount).Item("ProjectNum"))
                sTRACSHdrRow.TireNum = CShort(p_oTable.Rows(rowCount).Item("TireNum"))
                sTRACSHdrRow.TestSpec = CStr(p_oTable.Rows(rowCount).Item("TestSpec"))
                sTRACSHdrRow.TestSKU = CStr(p_oTable.Rows(rowCount).Item("TestSKU"))
                sTRACSHdrRow.CompletionDate = CStr(ConvertToDate(CStr(p_oTable.Rows(rowCount).Item("CompletionDate"))))
                sTRACSHdrRow.DOTSerialNumber = CStr(p_oTable.Rows(rowCount).Item("DOTSerialNumber"))
                sTRACSHdrRow.SerialDate = CStr(p_oTable.Rows(rowCount).Item("SerialDate"))
                sTRACSHdrRow.LowestUnseatValue = ConvertToInt(CStr(p_oTable.Rows(rowCount).Item("LowestUnseatValue")))
                sTRACSHdrRow.PassYN = CStr(p_oTable.Rows(rowCount).Item("PassYN"))
                sTRACSHdrRow.MinBeadUnseat = ConvertToInt(CStr(p_oTable.Rows(rowCount).Item("MinBeadUnseat")))
                sTRACSHdrRow.Operation = CStr(p_oTable.Rows(rowCount).Item("OperationDescription"))
                sTRACSHdrRow.MFGWWYY = CStr(p_oTable.Rows(rowCount).Item("SERIALDATE"))
                sTRACSHdrRow.GTSPEC = CStr(p_oTable.Rows(rowCount).Item("GTSpec"))
                p_stTRACS.BeadUnseatHdr.AddBeadUnseatHdrRow(sTRACSHdrRow)
            Next
        End If

        '   Add Bead Detail to Result Set
        If p_oTable.TableName = "BeadUnseatDtl" Then
            ConvertNulls(p_oTable)
            For rowCount = 0 To CShort(p_oTable.Rows.Count - 1)
                Dim sTRACSDtlRow As ICS.Datasets.TRACStoICSDataset.BeadUnseatDtlRow = p_stTRACS.BeadUnseatDtl.NewBeadUnseatDtlRow
                sTRACSDtlRow.ITERATION = ConvertToShort(CStr(p_oTable.Rows(rowCount).Item("ITERATION")))
                sTRACSDtlRow.UnseatForce = CInt(ConvertToSingle(CStr(p_oTable.Rows(rowCount).Item("UnseatForce")))) 'changed to ConvertToSingle instead of ConvertToInt -value is being passed from SAP with decimal places.
                p_stTRACS.BeadUnseatDtl.AddBeadUnseatDtlRow(sTRACSDtlRow)
            Next
        End If
    End Sub

    '*********************************************************************
    '*  This routine is used to insure that no nulls are included in the
    '*  data set.  It replaces all nulls in a table with empty strings.
    '*********************************************************************
    ''' <summary>
    '''  Method is used to replaces all nulls in a table with empty strings.
    ''' </summary>
    ''' <param name="p_oTable">DataTable Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub ConvertNulls(ByRef p_oTable As System.Data.DataTable)
        Dim nRowIndex As Integer
        Dim nColIndex As Integer
        Try
            For nRowIndex = 0 To p_oTable.Rows.Count - 1
                For nColIndex = 0 To p_oTable.Columns.Count - 1
                    If (p_oTable.Rows(nRowIndex).Item(nColIndex) Is System.DBNull.Value) Then
                        p_oTable.Rows(nRowIndex).Item(nColIndex) = ""
                    End If
                Next
            Next
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    '''  Converts string value to Short
    ''' </summary>
    ''' <param name="p_value">String Value</param>
    ''' <returns>Returns converted value</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function ConvertToShort(ByVal p_value As String) As Short
        Dim convertedValue As Short

        Try
            Short.TryParse(p_value, convertedValue)
            Return convertedValue
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Converts string value to Int
    ''' </summary>
    ''' <param name="p_value">String Value</param>
    ''' <returns>Returns converted integer value</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function ConvertToInt(ByVal p_value As String) As Integer
        Dim convertedValue As Integer
        Try
            Integer.TryParse(p_value, convertedValue)
            Return convertedValue
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Converts string value to Long
    ''' </summary>
    ''' <param name="p_value">String Value</param>
    ''' <returns>Returns converted long value</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function ConvertToLong(ByVal p_value As String) As Long
        Dim convertedValue As Long
        Try
            Long.TryParse(p_value, convertedValue)
            Return convertedValue
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Converts string value to Single.
    ''' </summary>
    ''' <param name="p_value">String Value</param>
    ''' <returns>Returns converted single value</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function ConvertToSingle(ByVal p_value As String) As Single
        Dim convertedValue As Single
        Try
            Single.TryParse(p_value, convertedValue)
            Return convertedValue
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Converts string value to Date.
    ''' </summary>
    ''' <param name="p_value">String Value</param>
    ''' <returns>Returns converted date value</returns>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Function ConvertToDate(ByVal p_value As String) As Date
        Dim convertedValue As Date

        Try
            If (Not String.IsNullOrEmpty(p_value)) Then

                Select Case p_value.Trim().Length
                    Case 8
                        convertedValue = CDate(Format(DateTime.ParseExact(p_value, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture), "MM/dd/yyyy"))
                    Case 14
                        convertedValue = CDate(Format(DateTime.ParseExact(p_value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture), "MM/dd/yyyy"))
                End Select
            End If
        Catch
            Throw
        End Try

        Return convertedValue
    End Function

    ''' <summary>
    '''  Splits into 2 separate datasets for Sap And Tracs
    ''' </summary>
    ''' <param name="p_oSet">ClientRequest Object</param>
    ''' <param name="p_oSap">DataSet Object</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Private Sub SplitSapAndTracsDataset(ByRef p_oSet As ICS.Datasets.ClientRequest, ByRef p_oSap As DataSet)

        Dim index As Integer = 0
        Dim dtSap As New DataTable
        Dim deleteSapItems As New Stack

        dtSap.Columns.Add(New DataColumn("LotNumber"))
        dtSap.Columns.Add(New DataColumn("Operation"))
        dtSap.Columns.Add(New DataColumn("OperationDescription"))
        dtSap.Columns.Add(New DataColumn("TireNumber"))

        If ((Not p_oSet Is Nothing) AndAlso (p_oSet.Tables.Count > 0)) Then
            For index = 0 To p_oSet.Tables(0).Rows.Count - 1

                If (p_oSet.Tables(0).Rows(index).Item("ProjectNum").ToString().Length = 12 And Not p_oSet.Tables(0).Rows(index).Item("Operation") Is DBNull.Value) Then
                    Dim drSapNewRow As DataRow = dtSap.NewRow()
                    drSapNewRow.Item("LotNumber") = p_oSet.Tables(0).Rows(index).Item("ProjectNum")
                    drSapNewRow.Item("TireNumber") = p_oSet.Tables(0).Rows(index).Item("TireNum")
                    drSapNewRow.Item("Operation") = p_oSet.Tables(0).Rows(index).Item("Operation")
                    drSapNewRow.Item("OperationDescription") = p_oSet.Tables(0).Rows(index).Item("TestSpec")
                    dtSap.Rows.Add(drSapNewRow)

                    'Add Sap rowindex
                    deleteSapItems.Push(index)
                End If
            Next

            'Delete Sap rowindexes from the dataset
            While deleteSapItems.Count > 0
                p_oSet.Tables(0).Rows.RemoveAt(CInt(deleteSapItems.Pop()))
            End While

            p_oSet.AcceptChanges()
            p_oSap.Tables.Add(dtSap)

        End If
    End Sub

#End Region

    ''' <summary>
    '''  Finalize the resources
    ''' </summary>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>11/28/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
