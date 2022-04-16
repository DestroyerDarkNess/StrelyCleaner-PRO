''DISCLAIMER  : TESTED ON WINDOWS 10 Version 1909 , System Ver : 18363.778 , Language : French , Theme : Dark

''All of thoses projects are researches of others coder and their testing. Also some of those projects may have strange behavior on Windows 10.

''Advices to use (add this in form loading) :
''Dim j As MARGINS = New MARGINS()
''GlassTheme.SetAero7(Me.Handle, j)  ''Seems not to change anything
''GlassTheme.SetAero10(Me.Handle)
''Also use Black BackColor for better results
''I will also post results of strange behavior about projects above


''Most of those projects are made for wpf , not winform
''Currently translated in VBNET by Arsium for winform from working project there : https://gist.github.com/Trumeet/e5c4f35267464366aa6cd98b24e61346

''For my own tests , I've made a custom panel with many properties like draggable and gradient property.
''The way I've made my gradient property seems to override the blur effect (good think to make control not affected by this blur effect)

Imports System.Runtime.InteropServices

Module GlassTheme
    <DllImport("user32.dll")>
    Friend Function SetWindowCompositionAttribute(ByVal hwnd As IntPtr, ByRef data As WindowCompositionAttributeData) As Integer
    End Function

    <DllImport("DwmApi.dll")>
    Function DwmExtendFrameIntoClientArea(ByVal hwnd As IntPtr, ByRef pMarInset As MARGINS) As Integer
    End Function

    <DllImport("DwmApi.dll")>
    Function DwmExtendFrameIntoClientArea(ByVal hwnd As IntPtr, ByRef pMarInset As side) As Integer
    End Function

    Public Structure side
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure

    Function SetFixed(ByVal hwnd As IntPtr, ByRef pMarInset As side) As Boolean
        Try
            Return CBool(DwmExtendFrameIntoClientArea(hwnd, pMarInset))
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function SetAero10(ByVal hwnd As IntPtr) As Integer
        Dim accentPolicy As AccentPolicy = New AccentPolicy With {
            .AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND,
            .AccentFlags = 0,
            .GradientColor = 0,
            .AnimationId = 0
        }
        Dim data As WindowCompositionAttributeData = New WindowCompositionAttributeData With {
            .Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY
        }
        Dim accentSize As Integer = Marshal.SizeOf(accentPolicy)
        Dim accentPtr As IntPtr = Marshal.AllocHGlobal(accentSize)
        Marshal.StructureToPtr(accentPolicy, accentPtr, False)
        data.Data = accentPtr
        data.SizeOfData = accentSize
        Dim result As Integer = SetWindowCompositionAttribute(hwnd, data)
        Marshal.FreeHGlobal(accentPtr)
        Return result
    End Function

    Function SetAero7(ByVal mainWindowPtr As IntPtr, ByVal margins As MARGINS) As Integer
        Return DwmExtendFrameIntoClientArea(mainWindowPtr, margins)
    End Function
End Module

<StructLayout(LayoutKind.Sequential)>
Friend Structure WindowCompositionAttributeData
    Public Attribute As WindowCompositionAttribute
    Public Data As IntPtr
    Public SizeOfData As Integer
End Structure

Friend Enum WindowCompositionAttribute
    WCA_UNDEFINED = 0
    WCA_NCRENDERING_ENABLED = 1
    WCA_NCRENDERING_POLICY = 2
    WCA_TRANSITIONS_FORCEDISABLED = 3
    WCA_ALLOW_NCPAINT = 4
    WCA_CAPTION_BUTTON_BOUNDS = 5
    WCA_NONCLIENT_RTL_LAYOUT = 6
    WCA_FORCE_ICONIC_REPRESENTATION = 7
    WCA_EXTENDED_FRAME_BOUNDS = 8
    WCA_HAS_ICONIC_BITMAP = 9
    WCA_THEME_ATTRIBUTES = 10
    WCA_NCRENDERING_EXILED = 11
    WCA_NCADORNMENTINFO = 12
    WCA_EXCLUDED_FROM_LIVEPREVIEW = 13
    WCA_VIDEO_OVERLAY_ACTIVE = 14
    WCA_FORCE_ACTIVEWINDOW_APPEARANCE = 15
    WCA_DISALLOW_PEEK = 16
    WCA_CLOAK = 17
    WCA_CLOAKED = 18
    WCA_ACCENT_POLICY = 19
    WCA_FREEZE_REPRESENTATION = 20
    WCA_EVER_UNCLOAKED = 21
    WCA_VISUAL_OWNER = 22
    WCA_HOLOGRAPHIC = 23
    WCA_EXCLUDED_FROM_DDA = 24
    WCA_PASSIVEUPDATEMODE = 25
    WCA_LAST = 26
End Enum

Friend Enum AccentState
    ACCENT_DISABLED = 0
    ACCENT_ENABLE_GRADIENT = 1
    ACCENT_ENABLE_TRANSPARENTGRADIENT = 2
    ACCENT_ENABLE_BLURBEHIND = 3
    ACCENT_ENABLE_ACRYLICBLURBEHIND = 4
    ACCENT_ENABLE_HOSTBACKDROP = 5
    ACCENT_INVALID_STATE = 6
End Enum

<StructLayout(LayoutKind.Sequential)>
Friend Structure AccentPolicy
    Public AccentState As AccentState
    Public AccentFlags As Integer
    Public GradientColor As Integer
    Public AnimationId As Integer
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure MARGINS
    Public Left As Integer
    Public Right As Integer
    Public Top As Integer
    Public Bottom As Integer
End Structure

