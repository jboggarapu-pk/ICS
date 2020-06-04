Imports System.Xml
Imports System.IO

Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Certification Search - business process model
''' </summary>
''' <remarks></remarks>
Public Class CertificationSearch

    ' Added methods to retreive data from web service ,added ps_BrandLine parameter. Also added material number while generating xml document.
    ' as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 

#Region "Methods"

    ''' <summary>
    ''' Get certification type names
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Calls into data layer</remarks>
    Public Function GetCertificationNames() As List(Of String)

        Dim listCertNames As New List(Of String)

        Dim dstTest As DataSet = Depository.Current.GetCertifications()

        For Each drwCert As DataRow In dstTest.Tables(0).Rows
            listCertNames.Add(drwCert("CERTIFICATIONNAME"))
        Next

        Return listCertNames

    End Function
    ' Made PSN as last search type and Brand as First Search type as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation. 
    Public Function GetSearchTypesList() As List(Of String)

        Dim listSearchTypesNames As New List(Of String)

        Dim dstTest As DataSet = Depository.Current.GetSearchTypeResults()

        For Each searchTypeName As DataRow In dstTest.Tables(0).Rows
            listSearchTypesNames.Add(searchTypeName("TypeName"))
        Next
        If listSearchTypesNames.Contains("PSN") Then
            listSearchTypesNames.Remove("PSN")
            listSearchTypesNames.Add("PSN")
        End If

        Return listSearchTypesNames

    End Function

    ' Added GetBrands function as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Function GetBrands(ByVal p_strBrand As String) As List(Of String)
        Dim listBrands As New List(Of String)
        Dim dstTest As DataTable = Depository.Current.GetBrands(p_strBrand)

        For Each brand As DataRow In dstTest.Rows
            listBrands.Add(brand("BRAND"))
        Next
        Return listBrands
    End Function

    ' Added GetBrandLines function as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Function GetBrandLines(ByVal p_strBrandLine As String) As List(Of String)
        Dim listBrandLines As New List(Of String)
        Dim dstTest As DataTable = Depository.Current.GetBrandLines(p_strBrandLine)

        For Each brandLine As DataRow In dstTest.Rows
            listBrandLines.Add(brandLine("Line"))
        Next
        Return listBrandLines
    End Function

    ' Added GetMaterialAttribs function as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    Public Function GetMaterialAttribs(ByVal p_strMaterialIdList As String) As DataTable
        Dim xDoc As XmlDocument = New XmlDocument()
        Dim dstTest As DataTable

        dstTest = Depository.Current.GetMaterialAttribs(p_strMaterialIdList)

        Return dstTest
    End Function

    ''' <summary>
    ''' Gets the certification search results.
    ''' </summary>
    ''' <param name="ps_SearchCriteria">The PS_ search criteria.</param>
    ''' <param name="p_sSearchType">Type of the search.(Brand,Material No, PSN and etc)</param>
    ''' <returns></returns>
    Public Function GetCertificationSearchResults(ByVal ps_SearchCriteria As String, ByVal p_sSearchType As String, ByVal ps_ExtensionNo As String, ByVal ps_ImarkFamily As String, ByVal ps_BrandLine As String) As XmlDocument

        Dim xDoc As XmlDocument = New XmlDocument()
        Dim xeRoot, xeMatlNum, xeCertName As XmlElement
        'Creates the declaration section of the xmldocument
        Dim xDeclaration As XmlDeclaration = xDoc.CreateXmlDeclaration("1.0", Nothing, Nothing)
        'Get's data from the databse
        Dim dbtCertificationSearch As DataTable = Depository.Current.GetCertificationSearchResults(ps_SearchCriteria, p_sSearchType, ps_ExtensionNo, ps_ImarkFamily, ps_BrandLine)

        'Add declaration to the xDocument
        xDoc.AppendChild(xDeclaration)

        'Create the ROOT Element
        xeRoot = xDoc.CreateElement("ROOT", "Root", "")

        Dim sLastSKU As String = String.Empty
        Dim sLastSKUid As String = String.Empty
        Dim currentSKU As String = String.Empty
        Dim currentSKUid As String = String.Empty
        Dim currentCertif As String = String.Empty
        Dim currentCertifNumber As String = String.Empty
        Dim sLastCertif As String = String.Empty
        Dim sLastCertifNumber As String = String.Empty

        For Each row As DataRow In dbtCertificationSearch.Rows
            'Get the Current SKU
            currentSKU = row(NameAid.Column.SKU).ToString()
            currentSKUid = row(NameAid.Column.SKUID).ToString() 'jes

            'JES 09/10/12 If p_sSearchType = "SKU No." And currentSKU.Equals(sLastSKU) Then Continue For
            If currentSKUid.Equals(sLastSKUid) Then Continue For

            'Add Node that contain the SKU
            xeMatlNum = xDoc.CreateElement(NameAid.Column.SKU)

            'Add the atribute with the SKU to the SKU Node
            xeMatlNum.SetAttribute("SKU", row(NameAid.Column.SKU).ToString())

            'Creates a node with the SKU INfo
            Me.AddElementWithAtribute(row(NameAid.Column.SKU).ToString(), "SKU", "SKUID", row(NameAid.Column.SKUID).ToString(), xeMatlNum, xDoc)

            'Creates a node with the matlnum info
            Me.AddTextToElement(row(NameAid.Column.MaterialNumber).ToString(), "MaterialNumber", xeMatlNum, xDoc)

            'Creates a node with the Single Load Index info
            Me.AddTextToElement(row(NameAid.Column.SingleLoadIndex).ToString(), "SINGLOADINDEX", xeMatlNum, xDoc)

            'Creates a node with the Dual Load Index info
            Me.AddTextToElement(row(NameAid.Column.DualLoadIndex).ToString(), "DUALLOADINDEX", xeMatlNum, xDoc)

            'Creates a node with the Speed Rating info
            Me.AddTextToElement(row(NameAid.Column.SpeedRating).ToString(), "SPEEDRATING", xeMatlNum, xDoc)

            'Creates a node with the tiresize info
            Me.AddTextToElement(row(NameAid.Column.SizeStamp).ToString(), "TireSize", xeMatlNum, xDoc)

            Dim sCriteria As String

            'JES 09/10/12 sCriteria = String.Concat("SKU='", row(NameAid.Column.SKU).ToString(), "'")
            sCriteria = String.Concat("SKUID='", row(NameAid.Column.SKUID).ToString(), "'")

            Dim toolText As String = String.Empty

            If Not (row("MATL_NUM") Is Nothing Or IsDBNull(row("MATL_NUM"))) Then
                ' Added script to get the attribute list for showing tooltip for Material Number.
                Dim dstTest As DataTable = GetMaterialAttribs(row("MATL_NUM"))
                toolText = FormatMaterialAttribsForToolTip(dstTest)
            End If

            'Add Node that contain the ToolTip
            Me.AddTextToElement(toolText, "ToolTip", xeMatlNum, xDoc)

            Dim foundSKURows() As DataRow
            foundSKURows = dbtCertificationSearch.Select(sCriteria)
            For i As Integer = 0 To foundSKURows.GetUpperBound(0)
                currentCertif = foundSKURows(i)(NameAid.Column.CertificationName).ToString()
                If currentCertif.Equals(sLastCertif) Then Continue For
                'Creates the Nodes for certification
                Select Case p_sSearchType
                    Case "Material No.", _
                         "Brand", _
                         "PSN", _
                         "Batch No.", _
                         "Spec No."

                        'Add Node that contain the certification name
                        xeCertName = xDoc.CreateElement(NameAid.Column.CertificationName)

                        'Add the attribute with the certification name to the certification name node
                        xeCertName.SetAttribute(NameAid.Column.CertificationName, currentCertif)

                        Me.AddElementWithAtribute(foundSKURows(i)(NameAid.Column.CertificationName).ToString(), "CertificationName", foundSKURows(i)(NameAid.Column.CertificationName).ToString(), xeCertName, xDoc)
                        xeMatlNum.AppendChild(xeCertName)

                        'JES 09/10/12 sCriteria = String.Concat("CertificationName='", foundSKURows(i)(NameAid.Column.CertificationName).ToString(), "'", " AND ", "SKU='", currentSKU, "'")
                        sCriteria = String.Concat("CertificationName='", foundSKURows(i)(NameAid.Column.CertificationName).ToString(), "'", " AND ", "SKUID='", currentSKUid, "'")

                        Dim foundCertRows() As DataRow
                        foundCertRows = dbtCertificationSearch.Select(sCriteria)

                        'Clear last certification number
                        sLastCertifNumber = String.Empty
                        For j As Integer = 0 To foundCertRows.GetUpperBound(0)
                            currentCertifNumber = foundCertRows(j)(NameAid.Column.CertificateNo).ToString()
                            If currentCertifNumber.Equals(sLastCertifNumber) Then Continue For

                            Me.AddElementWithAtribute(foundCertRows(j)(NameAid.Column.CertificateNo).ToString(), "CertificateNo", foundCertRows(j)(NameAid.Column.State).ToString(), xeCertName, xDoc)
                            If currentCertif.ToLower().Equals("nom") And Not foundSKURows(i)(NameAid.Column.Customer).Equals(DBNull.Value) Then
                                Me.AddElement(foundCertRows(j)(NameAid.Column.Customer).ToString(), "Customer", xeCertName, xDoc)
                            End If

                            sLastCertifNumber = foundCertRows(j)(NameAid.Column.CertificateNo).ToString()
                        Next

                        'Get the last SKU processed
                        sLastSKU = foundSKURows(i)(NameAid.Column.SKU).ToString()
                        sLastSKUid = foundSKURows(i)(NameAid.Column.SKUID).ToString()
                        sLastCertif = foundSKURows(i)(NameAid.Column.CertificationName).ToString()
                    Case Else
                        Me.AddElementWithAtribute(foundSKURows(i)(NameAid.Column.CertificationName).ToString(), NameAid.Column.CertificationName.ToString(), foundSKURows(i)(NameAid.Column.State).ToString(), xeMatlNum, xDoc)
                        If p_sSearchType <> "SimTire" Then
                            If currentCertif.ToLower().Equals("nom") And Not foundSKURows(i)(NameAid.Column.Customer).Equals(DBNull.Value) Then
                                Me.AddElement(foundSKURows(i)(NameAid.Column.Customer).ToString(), "Customer", xeMatlNum, xDoc)
                            End If
                        End If
                        'Get the last SKU processed
                        sLastSKU = foundSKURows(i)(NameAid.Column.SKU).ToString()
                        sLastSKUid = foundSKURows(i)(NameAid.Column.SKUID).ToString()
                        sLastCertif = foundSKURows(i)(NameAid.Column.CertificationName).ToString()
                End Select
            Next
            sLastCertif = String.Empty
            'Add Child node to parent node
            xeRoot.AppendChild(xeMatlNum)
        Next
        'Add's the root node and its nodes to the document
        xDoc.AppendChild(xeRoot)
        'xDoc.Save("c:\TestXDoc.xml")
        Return xDoc

    End Function

    ''' <summary>
    ''' Adds the text to element.
    ''' </summary>
    ''' <param name="nodeText">The node text.</param>
    ''' <param name="sElementText">The element text.</param>
    ''' <param name="xParentNode">The parent node.</param>
    ''' <param name="xDoc">The Xmldocument.</param>
    Private Sub AddTextToElement(ByVal nodeText As String, ByVal sElementText As String, ByRef xParentNode As XmlNode, ByVal xDoc As XmlDocument)

        ' Create the element
        Dim xElement As XmlElement = xDoc.CreateElement(sElementText)
        'Creates ther xmlTextNode with the text passed as paramenter
        '        this will be the text of the node
        Dim xText As XmlText = xDoc.CreateTextNode(nodeText)
        'Add the TextNode to the element
        xElement.AppendChild(xText)
        xParentNode.AppendChild(xElement)

    End Sub

    ''' <summary>
    ''' Gets the node by inner text.
    ''' </summary>
    ''' <param name="xParentNode">The x parent node.</param>
    ''' <param name="xDoc">The x doc.</param>
    ''' <returns></returns>
    Private Function GetNodeByInnerText(ByVal xParentNode As XmlNode, ByVal xDoc As XmlDocument) As XmlNode

        Dim xNomNode As XmlNode = Nothing

        For Each childNode As XmlNode In xParentNode.ChildNodes
            If childNode.InnerText.ToLower().Equals("nom") Then
                xNomNode = childNode
                Exit For
            End If
        Next

        Return xNomNode
    End Function

    ''' <summary>
    ''' Adds the element with atribute.
    ''' </summary>
    ''' <param name="nodeText">The node text.</param>
    ''' <param name="sElementText">The  element text.</param>
    ''' <param name="atributeText">The atribute text.</param>
    ''' <param name="xParentNode">The  parent node.</param>
    ''' <param name="xDoc">The Xmldocument.</param>
    Private Sub AddElementWithAtribute(ByVal nodeText As String, ByVal sElementText As String, ByVal atributeText As String, ByRef xParentNode As XmlNode, ByVal xDoc As XmlDocument)

        ' Create the element
        Dim xElement As XmlElement = xDoc.CreateElement(sElementText)
        'Creates ther xmlTextNode with the text passed as paramenter
        '        this will be the text of the node
        Dim xText As XmlText = xDoc.CreateTextNode(nodeText)
        'Add the TextNode to the element
        xElement.AppendChild(xText)
        'Add State Atribute to the element
        xElement.SetAttribute("State", atributeText)
        'Add the child to the parent node
        xParentNode.AppendChild(xElement)

    End Sub

    ''' <summary>
    ''' Adds the element.
    ''' </summary>
    ''' <param name="nodeText">The node text.</param>
    ''' <param name="sElementText">The s element text.</param>
    ''' <param name="xParentNode">The x parent node.</param>
    ''' <param name="xDoc">The x doc.</param>
    Private Sub AddElement(ByVal nodeText As String, ByVal sElementText As String, ByRef xParentNode As XmlNode, ByVal xDoc As XmlDocument)

        ' Create the element
        Dim xElement As XmlElement = xDoc.CreateElement(sElementText)
        'Creates ther xmlTextNode with the text passed as paramenter
        '        this will be the text of the node
        Dim xText As XmlText = xDoc.CreateTextNode(nodeText)
        'Add the TextNode to the element
        xElement.AppendChild(xText)
        'Add the child to the parent node
        xParentNode.AppendChild(xElement)

    End Sub

    ''' <summary>
    ''' Adds the element with atribute.
    ''' </summary>
    ''' <param name="nodeText">The node text.</param>
    ''' <param name="sElementText">The s element text.</param>
    ''' <param name="atributeTitle">The atribute title.</param>
    ''' <param name="atributeText">The atribute text.</param>
    ''' <param name="xParentNode">The x parent node.</param>
    ''' <param name="xDoc">The x doc.</param>
    Private Sub AddElementWithAtribute(ByVal nodeText As String, ByVal sElementText As String, ByVal atributeTitle As String, ByVal atributeText As String, ByRef xParentNode As XmlNode, ByVal xDoc As XmlDocument)

        ' Create the element
        Dim xElement As XmlElement = xDoc.CreateElement(sElementText)
        'Creates ther xmlTextNode with the text passed as paramenter
        '        this will be the text of the node
        Dim xText As XmlText = xDoc.CreateTextNode(nodeText)
        'Add the TextNode to the element
        xElement.AppendChild(xText)
        'Add State Atribute to the element
        xElement.SetAttribute(atributeTitle, atributeText)
        'Add the child to the parent node
        xParentNode.AppendChild(xElement)

    End Sub

    Private Function CompareSKUText(ByVal currentSKU As String, ByVal previusSKU As String) As Boolean

        Return currentSKU.Equals(previusSKU)

    End Function

    ' Added FormatMaterialAttribsForToolTip function as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation 
    ''' <summary>
    ''' Concatenate material attributes.
    ''' </summary>
    ''' <param name="dtTable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks> 
    Public Function FormatMaterialAttribsForToolTip(ByVal dtTable As DataTable) As String
        Dim ToolText As String = String.Empty

        Dim foundRow() As DataRow
        foundRow = dtTable.Select("ATTRIB_NAME = 'TUBE_TYPE'")
        If foundRow.Length > 0 Then
            ToolText = String.Format("{0} ", foundRow(0)("ATTRIB_VALUE").ToString())
        End If
        foundRow = dtTable.Select("ATTRIB_NAME = 'SIDEWALL_TYPE'")
        If foundRow.Length > 0 Then
            ToolText += String.Format("{0} ", foundRow(0)("ATTRIB_VALUE").ToString())
        End If
        foundRow = dtTable.Select("ATTRIB_NAME = 'LEGACY_COOPER_SKU'")
        If foundRow.Length > 0 Then
            ToolText += String.Format("{0} ", foundRow(0)("ATTRIB_VALUE").ToString())
        End If
        foundRow = dtTable.Select("ATTRIB_NAME = 'RMA_TIRE_PLY_CONSTRUCTION'")
        If foundRow.Length > 0 Then
            ToolText += String.Format("{0} ", foundRow(0)("ATTRIB_VALUE").ToString())
        End If
        foundRow = dtTable.Select("ATTRIB_NAME = 'BRAND_LINE'")
        If foundRow.Length > 0 Then
            ToolText += String.Format("{0} ", foundRow(0)("ATTRIB_VALUE").ToString())
        End If

        Return ToolText
    End Function

    ''' <summary>
    ''' Gets certification search results for Certificate No. Search Type
    ''' </summary>
    ''' <param name="ps_SearchCriteria">The PS_ search criteria.</param>
    ''' <param name="p_sSearchType">Type of the search.(Brand,Material No, PSN and etc)</param>
    ''' <returns></returns>
    Public Function GetCertificationSearchResultsForCertificateNumber(ByVal ps_SearchCriteria As String, _
                                                               ByVal p_sSearchType As String, _
                                                               ByVal ps_ExtensionNo As String, _
                                                               ByVal ps_ImarkFamily As String, _
                                                               ByVal ps_BrandLine As String, _
                                                               ByRef dbtCertSearch As DataTable) As XmlDocument
        Dim xeRoot As XmlElement = Nothing
        Dim xeMatlNum As XmlElement = Nothing
        Dim xDoc As XmlDocument = New XmlDocument()
        Dim sLastSKU As String = String.Empty
        Dim sLastSKUid As String = String.Empty
        Dim currentSKU As String = String.Empty
        Dim currentSKUid As String = String.Empty
        Dim currentCertif As String = String.Empty
        Dim currentCertIdif As String = String.Empty
        Dim currentCertifNumber As String = String.Empty
        Dim sLastCertif As String = String.Empty
        Dim sLastCertIdif As String = String.Empty
        Dim sLastCertifNumber As String = String.Empty
        Dim sCriteria As String = String.Empty
        Dim toolText As String = String.Empty
        Dim dstTest As DataTable = Nothing
        Dim foundSKURows() As DataRow = Nothing

        Dim xDeclaration As XmlDeclaration = xDoc.CreateXmlDeclaration("1.0", Nothing, Nothing)
        'Add declaration to the xDocument
        xDoc.AppendChild(xDeclaration)
        xeRoot = xDoc.CreateElement("ROOT", "Root", "")

        'Get's data from the databse
        dbtCertSearch = Depository.Current.GetCertificationSearchResults(ps_SearchCriteria, _
                                                                         p_sSearchType, _
                                                                         ps_ExtensionNo, _
                                                                         ps_ImarkFamily, _
                                                                         ps_BrandLine)

        For Each row As DataRow In dbtCertSearch.Rows
            currentSKU = row(NameAid.Column.SKU).ToString()
            currentSKUid = row(NameAid.Column.SKUID).ToString()
            xeMatlNum = xDoc.CreateElement(NameAid.Column.SKU)
            xeMatlNum.SetAttribute("SKU", row(NameAid.Column.SKU).ToString())
            Me.AddElementWithAtribute(row(NameAid.Column.SKU).ToString(), "SKU", "SKUID", row(NameAid.Column.SKUID).ToString(), xeMatlNum, xDoc)
            Me.AddTextToElement(row(NameAid.Column.MaterialNumber).ToString(), "MaterialNumber", xeMatlNum, xDoc)
            Me.AddTextToElement(row(NameAid.Column.SingleLoadIndex).ToString(), "SINGLOADINDEX", xeMatlNum, xDoc)
            Me.AddTextToElement(row(NameAid.Column.DualLoadIndex).ToString(), "DUALLOADINDEX", xeMatlNum, xDoc)
            Me.AddTextToElement(row(NameAid.Column.SpeedRating).ToString(), "SPEEDRATING", xeMatlNum, xDoc)
            Me.AddTextToElement(row(NameAid.Column.SizeStamp).ToString(), "TireSize", xeMatlNum, xDoc)
            Me.AddTextToElement(row(NameAid.Column.CertificateID).ToString(), "CERTIFICATEID", xeMatlNum, xDoc)

            sCriteria = String.Concat("SKUID='", row(NameAid.Column.SKUID).ToString(), "'")
            toolText = String.Empty

            If Not (row("MATL_NUM") Is Nothing Or IsDBNull(row("MATL_NUM"))) Then
                dstTest = GetMaterialAttribs(row("MATL_NUM"))
                toolText = FormatMaterialAttribsForToolTip(dstTest)
            End If

            Me.AddTextToElement(toolText, "ToolTip", xeMatlNum, xDoc)
            foundSKURows = dbtCertSearch.Select(sCriteria)
            For i As Integer = 0 To foundSKURows.GetUpperBound(0)
                Me.AddElementWithAtribute(foundSKURows(i)(NameAid.Column.CertificationName).ToString(), _
                                          NameAid.Column.CertificationName.ToString(), _
                                          foundSKURows(i)(NameAid.Column.State).ToString(), xeMatlNum, xDoc)
                If p_sSearchType <> "SimTire" Then
                    If currentCertif.ToLower().Equals("nom") And Not foundSKURows(i)(NameAid.Column.Customer).Equals(DBNull.Value) Then
                        Me.AddElement(foundSKURows(i)(NameAid.Column.Customer).ToString(), "Customer", xeMatlNum, xDoc)
                    End If
                End If
                sLastSKU = foundSKURows(i)(NameAid.Column.SKU).ToString()
                sLastSKUid = foundSKURows(i)(NameAid.Column.SKUID).ToString()
                sLastCertif = foundSKURows(i)(NameAid.Column.CertificationName).ToString()
            Next
            xeRoot.AppendChild(xeMatlNum)
        Next
        xDoc.AppendChild(xeRoot)
        Return xDoc
    End Function


    Function GetCertTemplate(ByVal p_strCertificationTypeName As String) As String
        Dim strCertTemplate As String

        strCertTemplate = Depository.Current.GetCertTemplate(p_strCertificationTypeName)

        Return strCertTemplate

    End Function

#End Region

End Class