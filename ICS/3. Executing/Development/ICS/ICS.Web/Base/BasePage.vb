Imports CooperTire.ICS.Presenter

''' <summary>
''' Base page for an ICS views
''' </summary>
''' <remarks></remarks>
Public Class BasePage
    Inherits System.Web.UI.Page
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
            Dim masterICS As ICS = CType(Master, ICS)
            Dim milliseconds As Integer = 60000
            masterICS.SessionMonitor.TimerInterval = Session.Timeout * milliseconds
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
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Sub DataBindView() Implements IView.DataBindView

        Me.DataBind()

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
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item> 
    ''' <item>
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>12/19/2019</para>
    ''' <para>Fixed ICS Issues</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Sub ShowMainView() Implements IView.ShowMainView
        Try
            Dim strMainViewPath As String = "~/Home.aspx"
            If AppRelativeVirtualPath Is strMainViewPath Then
                Response.Redirect(strMainViewPath)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Method to Hide user's no-go menu items .
    ''' </summary> 
    ''' <param name="listNoGoMenuItemValues">List of No GO Menu Item Values</param>
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
    Public Sub HideUserMenuItems(ByVal listNoGoMenuItemValues As System.Collections.Generic.List(Of String)) Implements Presenter.IView.HideUserMenuItems
        Const MenuMarketingText As String = "menuMarketing"
        Const MenuQualityText As String = "menuQuality"

        Try
            Dim masterICS As ICS = CType(Master, ICS)
            Dim menu As Menu = Nothing

            Select Case masterICS.EffectiveMenu
                Case ICS.EffectiveMenuEnum.Marketing
                    menu = CType(masterICS.FindControl(MenuMarketingText), WebControls.Menu)
                Case ICS.EffectiveMenuEnum.Quality
                    menu = CType(masterICS.FindControl(MenuQualityText), WebControls.Menu)
            End Select

            If Not menu Is Nothing Then
                For Each mi As MenuItem In menu.Items
                    If listNoGoMenuItemValues.Contains(mi.Value) Then
                        mi.Enabled = False
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

End Class
