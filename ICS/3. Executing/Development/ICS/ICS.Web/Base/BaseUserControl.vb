Imports CooperTire.ICS.Presenter

''' <summary>
''' Base control for an ICS view
''' </summary>
''' <remarks></remarks>
Public Class BaseUserControl
    Inherits System.Web.UI.UserControl
    Implements IView

#Region "Members"
    ''' <summary>
    ''' variable to hold load View.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event LoadView As EventHandler Implements IView.LoadView

    ''' <summary>
    ''' variable to hold InitView.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event InitView As CustomEvents.PlainEventHandler Implements IView.InitView

    ''' <summary>
    ''' variable to hold Unload View.
    ''' </summary>
    ''' <remarks></remarks>
    Public Event UnloadView As EventHandler Implements IView.UnloadView

#End Region

#Region "Methods"

    ''' <summary>
    ''' Method to Analog of respective Page property.
    ''' </summary> 
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public ReadOnly Property IsPostBackView() As Boolean Implements IView.IsPostBackView
        Get
            Return Me.IsPostBack
        End Get
    End Property

    ''' <summary>
    ''' Raises Event to be processed by subscribed presenter.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks> 
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Try
            RaiseEvent InitView()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raises Event to be processed by subscribed presenter.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            RaiseEvent LoadView(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Raises Event to be processed by subscribed presenter.
    ''' </summary>
    ''' <param name="sender">sender</param>
    ''' <param name="e">e</param>
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
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            RaiseEvent UnloadView(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Analog of respective Page method.
    ''' </summary> 
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub DataBindView() Implements IView.DataBindView
        Try
            Me.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Show ics main entry view.
    ''' </summary> 
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Sub ShowMainView() Implements IView.ShowMainView
        Try
            DirectCast(Page, BasePage).ShowMainView()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Hide user's no-go menu items .
    ''' </summary> 
    ''' <param name="listMenuItemValues">List of Menu Item Values</param>
    ''' <exception cref="Exception">
    ''' Throws the exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/07/2019</para>
    ''' <para>Implemented code standarization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Sub HideUserMenuItems(ByVal listMenuItemValues As System.Collections.Generic.List(Of String)) Implements Presenter.IView.HideUserMenuItems
        Try
            DirectCast(Page, BasePage).HideUserMenuItems(listMenuItemValues)
        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class
