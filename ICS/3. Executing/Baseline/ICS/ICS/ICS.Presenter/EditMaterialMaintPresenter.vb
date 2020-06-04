Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common

''' <summary>
'''  Edit Material Maint Presenter
''' </summary>
''' <remarks></remarks>
''' 
Public Class EditMaterialMaintPresenter

#Region "Members"

    Private Shared m_view As IEditMaterialMaintView = Nothing

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IEditMaterialMaintView)
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception("Error creating " + Me.ToString())
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Edit Material Maint presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()
        AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
        AddHandler m_view.ShowMaterial, AddressOf OnShowMaterial
        AddHandler m_view.EditMaterial, AddressOf OnEditmaterial
        AddHandler m_view.UpdateMaterial, AddressOf OnUpdateMaterial
        AddHandler m_view.CancelMaterial, AddressOf OnCancelMaterial
    End Sub

    ''' <summary>
    ''' Reload data for the view - Refresh 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnReloadViewData()
        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty
            m_view.MaterialMaint = Nothing
            m_view.MatNumberInput = String.Empty

            m_view.HideMatlMaintPanel(False, False)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.MaterialMaint = Nothing
            m_view.ErrorText = "Error loading Material Maint data."
        End Try
    End Sub

    ''' <summary>
    ''' Display all the records from Product
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnShowMaterial()
        If (String.IsNullOrEmpty(m_view.MatNumberInput)) Then
            m_view.ErrorText = "Please enter Material Number."
            Return
        End If

        Dim dtResults As DataTable = Nothing
        Dim objEditMaterialMaintModel As EditMaterialMaintModel = Nothing

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            objEditMaterialMaintModel = New EditMaterialMaintModel
            dtResults = objEditMaterialMaintModel.GetMaterial(m_view.MatNumberInput)

            If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                m_view.MaterialMaint = dtResults
                m_view.DataBindView()
                m_view.HideMatlMaintPanel(True, False)
            Else
                m_view.MaterialMaint = Nothing
                m_view.ErrorText = "No record(s) exist for Material Number '" + m_view.MatNumberInput + "' " + "."
                m_view.HideMatlMaintPanel(False, False)
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.MaterialMaint = Nothing
            m_view.ErrorText = "Error fetching Material Maint."
        End Try
    End Sub

    ''' <summary>
    ''' Assign values to contorls on Edit
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnEditmaterial(ByVal e As Object)
        Dim skuId As Integer = CInt(e)
        Dim dtResults As DataTable = Nothing
        m_view.ErrorText = String.Empty
        m_view.SuccessText = String.Empty
        Try
            dtResults = m_view.MaterialMaint
            Dim rows As DataRow() = dtResults.Select(String.Format("SkuId = {0} ", skuId))

            If rows.Length > 0 Then
                Dim row As DataRow = rows(0)
                m_view.SKUID = row.Item("SKUID")
                m_view.SKU = ConvertNullToString(row.Item("SKU"))
                m_view.Speedrating = ConvertNullToString(row.Item("SPEEDRATING"))
                m_view.MaterialNumber = ConvertNullToString(row.Item("MATL_NUM"))
                m_view.HideMatlMaintPanel(True, True)
            End If
            
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error editing Material Maint."
        End Try
    End Sub

    ''' <summary>
    ''' Edit Material Maint
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnUpdateMaterial()
        Dim objEditMaterialMaintModel As EditMaterialMaintModel = Nothing
        Dim result As Boolean = False

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            objEditMaterialMaintModel = New EditMaterialMaintModel
            result = objEditMaterialMaintModel.EditMaterial(m_view.SKUID, m_view.Speedrating)

            If result Then
                OnShowMaterial()
                m_view.SuccessText = "Successfully updated Material Maint"
                m_view.HideMatlMaintPanel(True, False)
            Else
                m_view.ErrorText = "Failed to save Material Maint."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error saving Material Maint."
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Material Maint
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnCancelMaterial()
        Try
            m_view.HideMatlMaintPanel(True, False)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error while canceling Material Maint."
        End Try
    End Sub

    ''' <summary>
    ''' Convert null to string
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function ConvertNullToString(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return String.Empty
        Else
            Return CStr(obj)
        End If
    End Function

#End Region

End Class
