Imports CooperTire.ICS.DomainEntities

''' <summary>
''' IEditMaterialMaint interface to the EditMaterialMaint User control view
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
''' <para>11/13/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IEditMaterialMaintView
    Inherits IView

    ''' <summary>
    '''  Success Text
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String
    ''' <summary>
    '''  Error Text
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String
    ''' <summary>
    '''  SKUID
    ''' </summary>
    ''' <remarks></remarks>
    Property SKUID() As Integer
    ''' <summary>
    '''  SKU
    ''' </summary>
    ''' <remarks></remarks>
    Property SKU() As String
    ''' <summary>
    '''  MaterialMaint
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialMaint() As DataTable
    ''' <summary>
    '''  Speedrating
    ''' </summary>
    ''' <remarks></remarks>
    Property Speedrating() As String
    ''' <summary>
    '''  Material Number
    ''' </summary>
    ''' <remarks></remarks>
    Property MaterialNumber() As String
    ''' <summary>
    '''  MatNumberInput
    ''' </summary>
    ''' <remarks></remarks>
    Property MatNumberInput() As String
    ''' <summary>
    '''  ReloadViewData
    ''' </summary>
    ''' <remarks></remarks>
    Event ReloadViewData As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  ShowMaterial
    ''' </summary>
    ''' <remarks></remarks>
    Event ShowMaterial As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  UpdateMaterial
    ''' </summary>
    ''' <remarks></remarks>
    Event UpdateMaterial As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  CancelMaterial
    ''' </summary>
    ''' <remarks></remarks>
    Event CancelMaterial As CustomEvents.PlainEventHandler
    ''' <summary>
    '''  EditMaterial
    ''' </summary>
    ''' <remarks></remarks>
    Event EditMaterial As CustomEvents.PlainArgumentEventHandler
    ''' <summary>
    '''  SetupViewData
    ''' </summary>
    ''' <param name="p_blnAddNew">Add New</param>
    ''' <remarks></remarks>
    Sub SetupViewData(ByVal p_blnAddNew As Boolean)
    ''' <summary>
    '''  HideMatlMaintPanel
    ''' </summary>
    ''' <param name="p_blnGridStatus">Grid Status</param>
    ''' <param name="p_blnDetailsStatus">Detail Status</param>
    ''' <remarks></remarks>
    Sub HideMatlMaintPanel(ByVal p_blnGridStatus As Boolean, ByVal p_blnDetailsStatus As Boolean)

End Interface

