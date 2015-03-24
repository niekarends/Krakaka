'Matt Schoen
'5-29-2013
'
'In the absence of a legitimate license (that I know of) that fits my needs, here goes:  This software is the 
'copyrighted material of its author, Matt Schoen, and his comapny Defective Studios.  It is available for sale on 
'the Unity Asset store and is subject to their restrictions and limitations, as well as the following: You shall not
'reproduce or re-distribute this software with the express written (e-mail is fine) permission of the author. If 
'permission is granted, the code (this file and related files) must bear this license in its entirety.  Anyone who 
'purchases the script is welcome to modify end re-use the code at their personal risk and under the condition that 
'it not be included in any disribution builds.  The software is provided as-is without warranty and the author bears 
'no responsiblity for damages or losses caused by the software.  Enjoy it, it's yours, but just don't try to profit 
'from it, OK?

strComputer = "."
Set objWMIService = GetObject("winmgmts:" _
	& "{impersonationLevel=impersonate}!\\" & strComputer & "\root\cimv2")
Set colProcesses = objWMIService.ExecQuery _
	("Select * from Win32_Process Where Name = 'unity.exe'")


dim objArgs,num,sMyDoc,sTheirDoc,ProjectBase

Set objArgs = WScript.Arguments
num = objArgs.Count
if num < 2 then
    MsgBox "Usage: [CScript | WScript] merge-unity.vbs %mine %theirs", vbExclamation, "Invalid arguments"
    WScript.Quit 1
end if

sMyDoc=objArgs(0)
sTheirDoc=objArgs(1)

ProjectBase = ".\Unity 4\"

'If my doc is not in project path, copy to project
If InStr(sMyDoc, ProjectBase) = 0 Then
	Set FSObj = CreateObject("Scripting.FileSystemObject")
	sourceParts = Split(sMyDoc, "\")
	destinationFile = ProjectBase & "\Assets\" & sourceParts(UBound(sourceParts))
	If FSObj.FileExists(sMyDoc) Then
		If FSObj.FileExists(destinationFile) Then
			WScript.Echo("Cannot copy " + sMyDoc + " to " + destinationFile + " because the destination file exists")
		Else
			FSObj.MoveFile sMyDoc, destinationFile
		End If
		sMyDoc = destinationFile
	End If
End If

'If their doc is not in project path, copy to project
If InStr(sTheirDoc, ProjectBase) = 0 Then
	Set FSObj = CreateObject("Scripting.FileSystemObject")
	sourceParts = Split(sTheirDoc, "\")
	destinationFile = ProjectBase & "\Assets\" & sourceParts(UBound(sourceParts))
	If FSObj.FileExists(sTheirDoc) Then
		If FSObj.FileExists(destinationFile) Then
			WScript.Echo("Cannot copy " + sTheirDoc + " to " + destinationFile + " because the destination file exists")
		Else
			FSObj.MoveFile sTheirDoc, destinationFile
		End If
		sTheirDoc = destinationFile
	End If
End If

If colProcesses.Count > 0 Then
	Set FSObj = CreateObject("Scripting.FileSystemObject")
	Set FileObj = FSObj.CreateTextFile(ProjectBase + "\Assets\merges.txt")
	FileObj.WriteLine(sMyDoc)
	FileObj.WriteLine(sTheirDoc)
Else
	Dim objShell
	Set objShell = WScript.CreateObject ("WScript.shell")
	objShell.Run """C:\Program Files\Editor\Unity.exe"" -executeMethod SceneMerge.CLIIn """ + sMyDoc + """ """ + sTheirDoc
	Set objShell = Nothing
End If