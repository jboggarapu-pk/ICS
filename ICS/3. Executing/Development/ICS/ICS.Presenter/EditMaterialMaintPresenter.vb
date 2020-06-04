Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common

''' <summary>
'''  Edit Material Maint Presenter
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
''' <term>Jhansi</term>
''' <description>
''' <para>10/17/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks> 
Public Class EditMaterialMaintPresenter

#Region "Members"
    ''' <summary>
    ''' Interface to the EditMaterialMaint user control view.
    ''' </summary>
    Private Shared m_view As IEditMaterialMaintView = Nothing

#End Region

#Region "Constructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IEditMaterialMaintView)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception(ErrorCreatingText + Me.ToString())
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Edit Material Maint presenter to view�s events.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
            AddHandler m_view.ShowMaterial, AddressOf OnShowMaterial
            AddHandler m_view.EditMaterial, AddressOf OnEditmaterial
            AddHandler m_view.UpdateMaterial, AddressOf OnUpdateMaterial
            AddHandler m_view.CancelMaterial, AddressOf OnCancelMaterial
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Reload data for the view - Refresh. 
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnReloadViewData()
        Const ErrorLoadMaterialData As String = "Error loading Material Maint data."
        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty
            m_view.MaterialMaint = Nothing
            m_view.MatNumberInput = String.Empty

            m_view.HideMatlMaintPanel(False, False)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.MaterialMaint = Nothing
            m_view.ErrorText = ErrorLoadMaterialData
        End Try
    End Sub

    ''' <summary>
    ''' Display all the records from Product.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnShowMaterial()
        Const ErrorMaterialNumber As String = "Please enter Material Number."
        Const NoRecordExistError As String = "No record(s) exist for Material Number '"
        Const ErrorFetchMaterial As String = "Error fetching Material Maint."

        If (String.IsNullOrEmpty(m_view.MatNumberInput)) Then
            m_view.ErrorText = ErrorMaterialNumber
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
                m_view.ErrorText = NoRecordExistError + m_view.MatNumberInput + "' " + "."
                m_view.HideMatlMaintPanel(False, False)
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.MaterialMaint = Nothing
            m_view.ErrorText = ErrorFetchMaterial
        End Try
    End Sub

    ''' <summary>
    ''' Assign values to controls on Edit.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnEditmaterial(ByVal e As Object)
        Dim skuId As Integer = CInt(e)
        Dim dtResults As DataTable = Nothing
        m_view.ErrorText = String.Empty
        m_view.SuccessText = String.Empty
        Const RowSKUID As String = "SKUID"
        Const SKU As String = "SKU"
        Const SpeedRating As String = "SPEEDRATING"
        Const MaterialNumber As String = "MATL_NUM"
        Const ErrorEditMaterial As String = "Error editing Material Maint."

        Try
            dtResults = m_view.MaterialMaint
            Dim rows As DataRow() = dtResults.Select(String.Format("SkuId = {0} ", skuId))

            If rows.Length > 0 Then
                Dim row As DataRow = rows(0)
                m_view.SKUID = CInt(row.Item(RowSKUID))
                m_view.SKU = ConvertNullToString(row.Item(SKU))
                m_view.Speedrating = ConvertNullToString(row.Item(SpeedRating))
                m_view.MaterialNumber = ConvertNullToString(row.Item(MaterialNumber))
                m_view.HideMatlMaintPanel(True, True)
            End If
            
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorEditMaterial
        End Try
    End Sub

    ''' <summary>
    ''' Edit Material Maint.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnUpdateMaterial()
        Dim objEditMaterialMaintModel As EditMaterialMaintModel = Nothing
        Dim result As Boolean = False
        Const SuccessUpdateMaterial As String = "Successfully updated Material Maint"
        Const ErrorSaveMaterial As String = "Error saving Material Maint."

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            objEditMaterialMaintModel = New EditMaterialMaintModel
            result = objEditMaterialMaintModel.EditMaterial(m_view.SKUID, m_view.Speedrating)

            If result Then
                OnShowMaterial()
                m_view.SuccessText = SuccessUpdateMaterial
                m_view.HideMatlMaintPanel(True, False)
            Else
                m_view.ErrorText = SuccessUpdateMaterial
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveMaterial
        End Try
    End Sub

    ''' <summary>
    ''' Cancel Material Maint.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnCancelMaterial()
        Const ErrorCancelMaterial As String = "Error while canceling Material Maint."
        Try
            m_view.HideMatlMaintPanel(True, False)
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorCancelMaterial
        End Try
    End Sub

    ''' <summary>
    ''' Convert null to string.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>String</returns>
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function ConvertNullToString(ByVal obj As Object) As String
        Try
            If IsDBNull(obj) Then
                Return String.Empty
            Else
                Return CStr(obj)
            End If
        Catch
            Throw
        End Try
    End Function
#End Region

End Class
