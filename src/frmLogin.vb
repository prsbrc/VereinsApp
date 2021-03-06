﻿'frmLogin
'Control user and password to login at this app. Use md5 hash for password check
'Copyright (C)2019,2020 by Christian Brunner

Public Class frmLogin

    Private checkLogin As New service_CheckLogin
    Private checkAppVersion As New service_CheckVersion
    Private currentAppVersion As New service_ReturnAppVersion
    Private readAppSettings As New service_ReadAppSettings
    Public AppSettings As StructDataBaseConnect

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AppSettings = readAppSettings.readSettings()
        Me.KeyPreview = True
        Me.Text = "Anmeldung am VereinsApp (GAR14 1.Batterie)"
        lblUserName.Text = "Benutzer"
        lblPassword.Text = "Passwort"
        btnLogin.Text = "&Anmelden"
        btnCancel.Text = "&Beenden (F3)"
        txtBoxUserName.Text = ""
        txtBoxPassword.Text = ""
        lblVersion.Text = currentAppVersion._appVersion()
        checkAppVersion._checkAppVersion()
        If Not checkAppVersion._returnCheckResult Then
            MessageBox.Show("App und Datenbank haben unterschiedliche Versionen", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnLogin.Enabled = False
            btnCancel.Select()
        End If
    End Sub

    Private Sub frmLogin_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        e.Handled = True
        Select Case e.KeyCode
            Case Keys.F3
                Me.Close()
        End Select
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtBoxUserName.Text = "" Then
            MessageBox.Show("Keinen Benutzer angegeben", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBoxUserName.Select()
        ElseIf txtBoxPassword.Text = "" Then
            MessageBox.Show("Kein Passwort angegeben", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtBoxPassword.Select()
        Else
            checkLogin._checkLogin(txtBoxUserName.Text, txtBoxPassword.Text)
            If checkLogin._returnCheckLogin Then
                frmMain.User = txtBoxUserName.Text
                frmMain.Show()
                txtBoxUserName.Text = ""
                txtBoxPassword.Text = ""
                Me.Hide()
            Else
                txtBoxUserName.Select()
                txtBoxPassword.Text = ""
                MessageBox.Show("Anmeldung fehlgeschlagen", "Anmeldung", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
