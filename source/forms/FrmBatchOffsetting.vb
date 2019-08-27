﻿

''' <summary>
''' <para>
'''     Form which allows to specify offsets which are applied to all views within the selected folder.
''' </para>
''' <para>
'''     This is specifically meant for importing images from a different program, such as Blender; 
'''     where it is possible the animal's (Y) offset needs to be adjusted for each frame in every view.
''' </para>
''' </summary>
Public Class FrmBatchOffsetFix

    Private Sub FrmBatchRotationFix_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Assume default folder
        If TxtFolder.Text = "" Then
            TxtFolder.Text = Cfg_path_Root
        End If



    End Sub

    Private Sub BtnBatchOffsetting_Click(sender As Object, e As EventArgs) Handles BtnBatchOffsettFix.Click

        ' Runs procedure
        MdlTasks.Batch_RotationFix_Folder_ZT1(TxtFolder.Text, New Point(numLeftRight.Value, numUpDown.Value), PBProgress)


    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click

        ' Allows user to select a different folder
        With dlgBrowseFolder

            .SelectedPath = TxtFolder.Text

            .ShowNewFolderButton = False ' 20190825 this used to be true?! Why would a new folder be created at this point?
            .Description = "Select the folder which contains the ZT1 Graphics."
            .ShowDialog()

            TxtFolder.Text = .SelectedPath

        End With

    End Sub
End Class