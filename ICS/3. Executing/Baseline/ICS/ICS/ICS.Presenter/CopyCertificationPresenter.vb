Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' copy certification presenter
''' </summary>
''' <remarks></remarks>
Public Class CopyCertificationPresenter

#Region "Members"

    Private Shared m_view As ICopyCertification = Nothing

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As ICopyCertification)
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
    ''' Attach presenter to view’s events.
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
        m_view.CopyCertificationTitle = "Copy Material"
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
    ''' Archives the certificate
    ''' </summary>    
    Private Sub OnSave()
        Dim blnSaveSuccess As Boolean
        Dim objCopyCertificationModel As CopyCertificationModel = Nothing

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            If String.IsNullOrEmpty(m_view.MaterialNumber) Then
                m_view.ErrorText = "Material Number can not be empty."
                Return
            End If

            objCopyCertificationModel = New CopyCertificationModel
            blnSaveSuccess = objCopyCertificationModel.CopyCertification(m_view.MaterialNumber)

            If blnSaveSuccess Then
                m_view.SuccessText = "Saved."
            Else
                m_view.ErrorText = "Save Failed."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error copying certification."
        End Try
    End Sub

    ''' <summary>
    ''' Check Certificate Number exists.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckMaterialNumberExists() As String
        Dim strErrorMessage = String.Empty
        Dim objCopyCertificationModel As CopyCertificationModel = Nothing
        Dim blnCertNumExists As Boolean = False

        Try
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty

            If String.IsNullOrEmpty(m_view.MaterialNumber.Trim) Then
                strErrorMessage = "Material number cannot be Empty"
                Return strErrorMessage
            End If

            objCopyCertificationModel = New CopyCertificationModel
            blnCertNumExists = objCopyCertificationModel.CheckMaterialNumberExists(m_view.MaterialNumber)

            If Not blnCertNumExists Then
                strErrorMessage = "Material number not found"
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            strErrorMessage = "Error validating Material number."
        End Try

        Return strErrorMessage
    End Function

#End Region

End Class
