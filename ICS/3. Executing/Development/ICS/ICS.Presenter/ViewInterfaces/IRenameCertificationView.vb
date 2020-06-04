''' <summary>
''' Interface to Rename certification User control view.
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
''' <term>Sujitha</term>
''' <description>
''' <para>11/12/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Interface IRenameCertificationView
    Inherits IView
    ''' <summary>
    ''' Variable to hold Success Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property SuccessText() As String

    ''' <summary>
    ''' Variable to hold Error Text.
    ''' </summary>
    ''' <remarks></remarks>
    Property ErrorText() As String

    ''' <summary>
    ''' Variable to hold Certification Name.
    ''' </summary>
    ''' <remarks></remarks>
    Property CertificationName() As String

    ''' <summary>
    ''' Variable to hold Add Certification Title.
    ''' </summary>
    ''' <remarks></remarks>
    Property AddCertificationTitle() As String

    ''' <summary>
    ''' Variable to hold Old Certificate Number.
    ''' </summary>
    ''' <remarks></remarks>
    Property OldCertificateNumber() As String

    ''' <summary>
    ''' Variable to hold New Certificate Number.
    ''' </summary>
    ''' <remarks></remarks>
    Property NewCertificateNumber() As String

    ''' <summary>
    ''' Variable to hold Old Extension.
    ''' </summary>
    ''' <remarks></remarks>
    Property OldExtension() As String

    ''' <summary>
    ''' Variable to hold New Extension.
    ''' </summary>
    ''' <remarks></remarks>
    Property NewExtension() As String

    ''' <summary>
    ''' Variable to hold Old Certificate Number Error Message Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property OldCertNumErrMsgFlag() As Boolean

    ''' <summary>
    ''' Variable to hold New Certificate Number Error Message Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property NewCertNumErrMsgFlag() As Boolean

    ''' <summary>
    ''' Variable to hold Old Extension Error Message Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property OldExtensionErrMsgFlag() As Boolean

    ''' <summary>
    ''' Variable to hold New Extension Error Message Flag.
    ''' </summary>
    ''' <remarks></remarks>
    Property NewExtensionErrMsgFlag() As Boolean

    ''' <summary>
    ''' Variable to hold Warning Message.
    ''' </summary>
    ''' <remarks></remarks>
    Property WarningMessage() As String

    ''' <summary>
    ''' Event to Check For Certified Materials.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event CheckForCertifiedMaterials As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Event to Save.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event Save As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Event to Reload View Data.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Event ReloadViewData As CustomEvents.PlainEventHandler

    ''' <summary>
    ''' Event to Setup View Data.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupViewData(ByVal p_strCertificationName As String, ByVal p_blnAnew As Boolean)

    ''' <summary>
    ''' Event to Setup Rename Certification View.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupRenameCertificationView()

    ''' <summary>
    ''' Event to Setup Old Certificate Num Error Message.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupOldCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)

    ''' <summary>
    ''' Event to Setup New Certificate Number Error Message.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupNewCertNumErrMsg(ByVal p_blnDuplicateFlag As Boolean)

    ''' <summary>
    ''' Event to Setup Old Extension Error Message.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupOldExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

    ''' <summary>
    ''' Event to Setup New Extension Error Message.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>11/15/2018</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Sub SetupNewExtensionErrMsg(ByVal p_blnDuplicateFlag As Boolean)

End Interface
