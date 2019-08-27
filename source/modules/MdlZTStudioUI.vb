﻿Imports System.IO


''' <summary>
''' Methods related to ZT Studio UI
''' </summary>

Module MdlZTStudioUI

    ''' <summary>
    ''' Load graphic and show
    ''' </summary>
    ''' <param name="StrFileName">Source file name</param>
    Sub LoadGraphic(StrFileName As String)

        MdlZTStudio.Trace("MdlZTStudioUI", "LoadGraphic", "Loading " & StrFileName)

        If System.IO.File.Exists(StrFileName) = False Then

            Dim StrMessage As String = "File does not exist."
            MdlZTStudio.ExpectedError("MdlZTStudioUI", "LoadGraphic", StrMessage)
            Exit Sub

        Else

            If Path.GetExtension(StrFileName) <> vbNullString Then

                Dim StrErrorMessage As String = "" &
                            "You selected a file with the extension '" & Path.GetExtension(StrFileName) & "'." & vbCrLf &
                            "ZT Studio expects you to select a ZT1 Graphic file, which shouldn't have a file extension."
                MdlZTStudio.ExpectedError("MdlZTStudioUI", "LoadGraphic", StrErrorMessage)

                Exit Sub


            ElseIf StrFileName.ToLower().Contains(Cfg_path_Root.ToLower()) = False Then

                Dim StrErrorMessage As String = "Only select a file in the root directory, which is currently:" & vbCrLf &
                               Cfg_path_Root & vbCrLf & vbCrLf &
                               "Would you like to change the root directory?"

                If MsgBox(StrErrorMessage, MsgBoxStyle.YesNo + MsgBoxStyle.Critical + MsgBoxStyle.ApplicationModal, "ZT1 Graphic not within root folder") = MsgBoxResult.Yes Then

                    ' Allow user to quickly change settings -> root directory
                    FrmSettings.Show()

                End If

                Exit Sub


            Else



                ' Reset any previous info.
                EditorGraphic = New ClsGraphic

                ' OK
                EditorGraphic.Read(StrFileName)

                ' Keep filename
                FrmMain.ssFileName.Text = Now.ToString("yyyy-MM-dd HH:mm:ss") & ": opened " & StrFileName

                ' Draw first frame 
                MdlZTStudioUI.UpdatePreview(True, True, 0)

                ' Add time indication
                FrmMain.LblAnimTime.Text = ((EditorGraphic.Frames.Count - EditorGraphic.HasBackgroundFrame) * EditorGraphic.AnimationSpeed) & " ms "
                FrmMain.LblFrames.Text = (EditorGraphic.Frames.Count - EditorGraphic.HasBackgroundFrame) & " frames. "

                ' Show default palette
                EditorGraphic.ColorPalette.FillPaletteGrid(FrmMain.DgvPaletteMain)

                ' Set editorframe
                EditorFrame = EditorGraphic.Frames(0)
                FrmMain.TbFrames.Value = 1


                ' Remember
                Cfg_path_recentZT1 = StrFileName
                MdlConfig.Write()


            End If

        End If



    End Sub


    ''' <summary>
    ''' <para>
    '''     Updates info in main window.
    ''' </para>
    ''' <para>
    '''     Updates shown info such as animation speed, number of frames, current frame, ...
    ''' </para>
    ''' <para>
    '''     Enables/disables certain controls (for example, button to render background frame)
    ''' </para>
    ''' </summary>
    ''' <param name="StrReason"></param>
    Sub UpdateGUI(StrReason As String)

        ' Displays updated info.
        ' 20190816: note: before today, it relied on .indexOf(), which might return incorrect results if there are similar frames. Now intFrameIndex is added and required.

        MdlZTStudio.Trace("MdlZTStudio", "UpdateGUI", "Reason: " & StrReason & ". Non-background frames: " & (EditorGraphic.Frames.Count - EditorGraphic.HasBackgroundFrame) & " - background frame: " & EditorGraphic.HasBackgroundFrame.ToString())

        Dim IntFrameIndex As Integer = FrmMain.TbFrames.Value - 1


        With FrmMain

            .TstZT1_AnimSpeed.Text = EditorGraphic.AnimationSpeed

            ' == Graphic
            .TsbGraphic_ExtraFrame.Enabled = (EditorGraphic.Frames.Count > 1) ' Background frame can only be enabled if there's more than one frame
            .TsbGraphic_ExtraFrame.Checked = (EditorGraphic.HasBackgroundFrame = 1) ' Is background frame enabled for this graphic? Then toggle button.

            ' == Frame
            .TsbFrame_Delete.Enabled = (EditorGraphic.Frames.Count > 1)
            .TsbFrame_ExportPNG.Enabled = False

            '(IsNothing(editorGraphic.frames(0).cachedFrame) = False)

            If IsNothing(EditorFrame) = False Then
                If EditorFrame.CoreImageHex.Count > 0 Then
                    .TsbFrame_ExportPNG.Enabled = True
                End If
            End If

            .TsbFrame_ImportPNG.Enabled = (EditorGraphic.Frames.Count > 0)

            .TsbFrame_OffsetDown.Enabled = (EditorGraphic.Frames.Count > 0)
            .TsbFrame_OffsetUp.Enabled = (EditorGraphic.Frames.Count > 0)
            .TsbFrame_OffsetLeft.Enabled = (EditorGraphic.Frames.Count > 0)
            .TsbFrame_OffsetRight.Enabled = (EditorGraphic.Frames.Count > 0)

            .TsbFrame_IndexIncrease.Enabled = (EditorGraphic.Frames.Count > 1 And IntFrameIndex < (EditorGraphic.Frames.Count - 1 - EditorGraphic.HasBackgroundFrame))
            .TsbFrame_IndexDecrease.Enabled = (EditorGraphic.Frames.Count > 1 And IntFrameIndex > 0)

            .PicBox.BackColor = Cfg_grid_BackGroundColor


        End With



101:
        ' 20190825 - new feature: show other ZT1 views in the same folder
        If EditorGraphic.FileName <> vbNullString Then

            ' Get path
            Dim ObjFileInfo As New System.IO.FileInfo(EditorGraphic.FileName)
            Dim StrDirectoryName As String = ObjFileInfo.Directory.FullName
            MdlZTStudio.Trace("MdlZTStudioUI", "UpdateInfo", "Path of graphic is " & StrDirectoryName)

        End If

    End Sub


    ''' <summary>
    ''' Updates shown info such as number of frames, current frame, ...
    ''' </summary>
    ''' <param name="StrReason"></param>
    Sub UpdateFrameInfo(StrReason As String)

        MdlZTStudio.Trace("MdlZTStudio", "UpdateFrameInfo", "Reason: " & StrReason & ". Non-background frames: " & (EditorGraphic.Frames.Count - EditorGraphic.HasBackgroundFrame) & " - background frame: " & EditorGraphic.HasBackgroundFrame.ToString())

        Dim IntFrameIndex As Integer = FrmMain.TbFrames.Value - 1

        With FrmMain

            ' NOT using a 0-based frame index visual indication, to avoid confusing
            .TslFrame_Index.Text = IIf(EditorGraphic.Frames.Count = 0, "-", (IntFrameIndex + 1) & " / " & (EditorGraphic.Frames.Count - EditorGraphic.HasBackgroundFrame))

            With .TbFrames
                .Minimum = 1
                .Maximum = (EditorGraphic.Frames.Count - EditorGraphic.HasBackgroundFrame) ' for actually generated files: - editorGraphic.extraFrame)

                If .Maximum < 1 Then
                    .Minimum = 1
                    .Maximum = 1
                End If
                If .Value < .Minimum Then
                    .Value = .Minimum
                End If

            End With

            If IsNothing(EditorFrame) Then
                .TslFrame_Offset.Text = "0 , 0"

            Else
                '.tbFrames.Value = editorGraphic.frames.IndexOf(editorFrame) + 1
                .TslFrame_Offset.Text = EditorFrame.OffsetX & " , " & EditorFrame.OffsetY
            End If

        End With


    End Sub

    ''' <summary>
    ''' Updates all sort of info.
    ''' </summary>
    ''' <param name="BlnUpdateFrameInfo">Boolean. Update frame info.</param>
    ''' <param name="BlnUpdateUI">Boolean. Update UI (buttons), animation speed, file list...).</param>
    ''' <param name="IntIndexFrameNumber">Optional frame index number. Defaults to value of slider in main window.</param>
    Public Sub UpdatePreview(BlnUpdateFrameInfo As Boolean, BlnUpdateUI As Boolean, Optional IntIndexFrameNumber As Integer = -1)



10:
        ' Can't update if there are no frames.
        If EditorGraphic.Frames.Count = 0 Then
            Exit Sub
        End If

20:
        ' Shortcut. If no index number for the frame was specified, assume the currently visible frame needs to be updated.
        If IntIndexFrameNumber = -1 Then
            IntIndexFrameNumber = FrmMain.TbFrames.Value - 1
        End If


125:
        ' 20190816: some aspects weren't managed properly, for instance when toggling extra frame or adding/removing frames.
        ' Previous/next frame; current And max value of progress bar, ...
        ' Update preview is called from lots of places, so this may be a bit of an overkill, but better safe.
        If BlnUpdateFrameInfo = True Then
            MdlZTStudioUI.UpdateFrameInfo("MdlSettings::UpdatePreview()")
        End If

126:
        If BlnUpdateUI = True Then
            MdlZTStudioUI.UpdateGUI("MdlSettings::UpdatePreview()")
        End If


130:
        EditorFrame = EditorGraphic.Frames(IntIndexFrameNumber)

300:
        ' The sub gets triggered when a new frame has been added, but no .PNG has been loaded yet, so frame contains no data.
        ' However, the picbox may need to be cleared (previous frame would still be shown otherwise)
        If EditorGraphic.Frames(IntIndexFrameNumber).CoreImageHex.Count = 0 Then
            FrmMain.PicBox.Image = MdlBitMap.DrawGridFootPrintXY(Cfg_grid_footPrintX, Cfg_grid_footPrintY)
            Exit Sub
        End If

320:
        FrmMain.PicBox.Image = EditorGraphic.Frames(IntIndexFrameNumber).GetImage(True)



    End Sub

End Module