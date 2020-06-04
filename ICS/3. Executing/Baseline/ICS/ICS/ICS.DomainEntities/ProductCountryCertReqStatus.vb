''' <summary>
''' Keeps status values for audit purposes
''' </summary>
''' <remarks></remarks>
Public Class ProductCountryCertReqStatus

    ' Changed sku to material number as per PRJ3617 SAP Interface to International Certification System (OPQ.I.8265)Remediation

#Region "Members"

    Private m_intSKUID As Integer = 0
    Private m_strMaterialNum As String = String.Empty
    Private m_strSizeStamp As String = String.Empty
    Private m_intCountryID As Integer = 0
    Private m_strCountryName As String = String.Empty
    Private m_strCertTypeName As String = String.Empty
    Private m_blnReqStatus As Boolean = False

#End Region

#Region "Properties"

    Public Property SKUID() As Integer
        Get
            Return m_intSKUID
        End Get
        Set(ByVal value As Integer)
            m_intSKUID = value
        End Set
    End Property

    Public Property MaterialNumber() As String
        Get
            Return m_strMaterialNum
        End Get
        Set(ByVal value As String)
            m_strMaterialNum = value
        End Set
    End Property

    Public Property SizeStamp() As String
        Get
            Return m_strSizeStamp
        End Get
        Set(ByVal value As String)
            m_strSizeStamp = value
        End Set
    End Property

    Public Property CountryID() As Integer
        Get
            Return m_intCountryID
        End Get
        Set(ByVal value As Integer)
            m_intCountryID = value
        End Set
    End Property

    Public Property CountryName() As String
        Get
            Return m_strCountryName
        End Get
        Set(ByVal value As String)
            m_strCountryName = value
        End Set
    End Property

    Public Property CertTypeName() As String
        Get
            Return m_strCertTypeName
        End Get
        Set(ByVal value As String)
            m_strCertTypeName = value
        End Set
    End Property

    Public Property ReqStatus() As Boolean
        Get
            Return m_blnReqStatus
        End Get
        Set(ByVal value As Boolean)
            m_blnReqStatus = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New(ByVal p_intSKUID As Integer, ByVal p_strMatlNum As String, ByVal p_strSizeStamp As String, _
                                    ByVal p_intCountryID As Integer, ByVal p_strCountryName As String, _
                                    ByVal p_strCertTypeName As String, ByVal p_blnReqStatus As Boolean)

        m_intSKUID = p_intSKUID
        m_strMaterialNum = p_strMatlNum
        m_strSizeStamp = p_strSizeStamp
        m_intCountryID = p_intCountryID
        m_strCountryName = p_strCountryName
        m_strCertTypeName = p_strCertTypeName
        m_blnReqStatus = p_blnReqStatus

    End Sub

#End Region

End Class
