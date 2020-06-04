Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

Public Class RefreshProductPresenter

#Region "Members"

    Private Shared m_view As IRefresbProductView = Nothing

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IRefresbProductView)
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception("Error refreshing " + Me.ToString())
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Refresh Product presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()
        AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
        AddHandler m_view.Save, AddressOf OnSave
        AddHandler m_view.LoadView, AddressOf OnLoadView
    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()
        m_view.RefreshProductTitle = "Refresh Product"
    End Sub

    ''' <summary>
    ''' Load data for the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try
    End Sub

    ''' <summary>
    ''' Reoad data for the view - start anew
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnReloadViewData()
        ' Flush all properties:
        m_view.MaterialNumber = String.Empty
        m_view.ErrorText = String.Empty
        m_view.SuccessText = String.Empty
    End Sub

    ''' <summary>
    ''' Refresh product data.
    ''' </summary>    
    Private Sub OnSave()
        Dim resNum As Integer
        Dim objRefreshProduct As RefreshProductModel = Nothing
        Dim errorDesc As String = String.Empty
        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            If String.IsNullOrEmpty(m_view.MaterialNumber) Then
                m_view.ErrorText = "Material Number can not be empty."
                Return
            End If

            objRefreshProduct = New RefreshProductModel()
            resNum = objRefreshProduct.RefreshProduct(m_view.MaterialNumber, errorDesc)

            If resNum = 1 Then
                m_view.SuccessText = "Product refreshed successfully."
            ElseIf resNum = 0 Then
                m_view.ErrorText = errorDesc
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error refreshing Product."
        End Try
    End Sub

    ''' <summary>
    ''' Check if Material exists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsMaterialExists() As String
        Dim strErrorMessage = String.Empty
        Dim objCopyCertificationModel As CopyCertificationModel = Nothing
        Dim blnCertNumExists As Boolean = False

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            If String.IsNullOrEmpty(m_view.MaterialNumber.Trim) Then
                strErrorMessage = "Material number cannot be Empty."
                Return strErrorMessage
            End If

            objCopyCertificationModel = New CopyCertificationModel
            blnCertNumExists = objCopyCertificationModel.CheckMaterialNumberExists(m_view.MaterialNumber)

            If Not blnCertNumExists Then
                strErrorMessage = "Material number not found."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            strErrorMessage = "Error validating Material number."
        End Try

        Return strErrorMessage
    End Function

#End Region

End Class
