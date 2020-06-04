Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Duplicate correct certification presenter
''' </summary>
''' <remarks></remarks>
Public Class DupCorrectCertificationPresenter

#Region "Members"

    Private m_view As IDupCorrectCertificationView

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IDupCorrectCertificationView)

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
    ''' Duplicate correct certification presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()

        AddHandler m_view.LoadView, AddressOf OnLoadView
        AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
        AddHandler m_view.View, AddressOf OnView
        AddHandler m_view.Delete, AddressOf OnDelete

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
    ''' Reload data for the view - start anew
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnReloadViewData()

        Try
            ' Flush all properties:
            m_view.MaterialNumber = String.Empty
            m_view.SpeedRating = String.Empty
            m_view.CertId = 0
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty
            m_view.CertificationName = String.Empty
            m_view.AddCertificationTitle = String.Empty

            m_view.DuplicateCertificates = Nothing
            m_view.DataBindView()
           
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error re-loading form data."
        End Try

    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()

        m_view.AddCertificationTitle = "Material Maint Certificate"

    End Sub

    ''' <summary>
    ''' Loads the data that has been queried on dup correct certification search
    ''' </summary>    
    Private Sub OnView()

        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty
        m_view.CertId = 0

        If String.IsNullOrEmpty(m_view.MaterialNumber) Then
            m_view.ErrorText = "Material Number can not be empty."
            Return
        End If

        Dim dtResults As DataTable
        Dim objCertificationActionModel As New CertificationActionModel
        Try
            dtResults = objCertificationActionModel.GetDuplicateCertificates(m_view.MaterialNumber, _
                                                                             m_view.SpeedRating)

            If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                m_view.DuplicateCertificates = dtResults
                m_view.DataBindView()
            Else
                m_view.ErrorText = "No material exists."
            End If

        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), "ORA-20001") > 0 Then
                m_view.ErrorText = "Material " & m_view.MaterialNumber & " not exist."
            Else
                m_view.ErrorText = "Error fetching materials."
            End If
        End Try

    End Sub

    ''' <summary>
    ''' Deletes the data that has been selected as duplicate material record from all certificates
    ''' </summary>    
    Private Sub OnDelete()
        Dim blnDeleted As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Try
            blnDeleted = objCertificationActionModel.DeleteDuplicateCertificates(m_view.CertId)

            If blnDeleted Then
                m_view.SuccessText = "Successfully deleted materials(s)."
                m_view.ErrorText = String.Empty
            Else
                m_view.SuccessText = String.Empty
                m_view.ErrorText = "Failed to delete materials(s)."
            End If

            RePopulateDupCerts()

        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error deleting materials(s)."
        End Try
    End Sub

    ''' <summary>
    ''' Repopulates duplicate certificates
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RePopulateDupCerts()

        Dim dtResults As New DataTable
        Dim objCertificationActionModel As New CertificationActionModel
        Try
            m_view.DuplicateCertificates = Nothing
            m_view.DataBindView()

            dtResults = objCertificationActionModel.GetDuplicateCertificates(m_view.MaterialNumber, _
                                                                             m_view.SpeedRating)

        Catch exc As Exception
            EventLogger.Enter(exc)
            If Not InStr(exc.Message.ToString(), "ORA-20001") > 0 Then
                m_view.ErrorText = "Error fetching materials."
            End If
        End Try

        m_view.DuplicateCertificates = dtResults
        m_view.DataBindView()

    End Sub

#End Region

End Class
