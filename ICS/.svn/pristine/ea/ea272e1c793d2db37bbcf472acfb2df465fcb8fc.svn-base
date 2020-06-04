Imports CooperTire.ICS.DomainEntities

''' <summary>
''' IEditMaterialMaint interface to the EditMaterialMaint User control view
''' </summary>
''' <remarks></remarks>
Public Interface IEditMaterialMaintView
    Inherits IView

    Property SuccessText() As String
    Property ErrorText() As String

    Property SKUID() As Integer
    Property SKU() As String
    Property MaterialMaint() As DataTable
    Property Speedrating() As String
    Property MaterialNumber() As String
    Property MatNumberInput() As String
    
    Event ReloadViewData As CustomEvents.PlainEventHandler
    Event ShowMaterial As CustomEvents.PlainEventHandler
    Event UpdateMaterial As CustomEvents.PlainEventHandler
    Event CancelMaterial As CustomEvents.PlainEventHandler
    Event EditMaterial As CustomEvents.PlainArgumentEventHandler
    Sub SetupViewData(ByVal p_blnAddNew As Boolean)
    Sub HideMatlMaintPanel(ByVal p_blnGridStatus As Boolean, ByVal p_blnDetailsStatus As Boolean)

End Interface

