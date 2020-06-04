Public Class EditMaterialMaint

#Region "Members"

    Private m_intSKUID As Integer
    Private m_strSKU As String = String.Empty
    Private m_strSpeedrating As String = String.Empty
    Private m_strMaterialNumber As String = String.Empty
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

    Public Property SKU() As String
        Get
            Return m_strSKU
        End Get
        Set(ByVal value As String)
            m_strSKU = value
        End Set
    End Property

    Public Property Speedrating() As String
        Get
            Return m_strSpeedrating
        End Get
        Set(ByVal value As String)
            m_strSpeedrating = value
        End Set
    End Property

    Public Property MaterialNumber() As String
        Get
            Return m_strMaterialNumber
        End Get
        Set(ByVal value As String)
            m_strMaterialNumber = value
        End Set
    End Property

#End Region

#Region "Constructors"

    Public Sub New()
    End Sub

    Public Sub New(ByVal p_objEditMaterialMaint As EditMaterialMaint)
        m_intSKUID = p_objEditMaterialMaint.SKUID
        m_strSKU = p_objEditMaterialMaint.SKU
        m_strSpeedrating = p_objEditMaterialMaint.Speedrating
        m_strMaterialNumber = p_objEditMaterialMaint.MaterialNumber
    End Sub

    Public Sub New(ByVal p_skuId As Integer, ByVal p_sku As String, _
                        ByVal p_speedrating As String, ByVal p_materialNumber As String)
        m_intSKUID = p_skuId
        m_strSKU = p_sku
        m_strSpeedrating = p_speedrating
        m_strMaterialNumber = p_materialNumber
    End Sub
#End Region

End Class
