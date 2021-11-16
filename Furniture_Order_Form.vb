Option Strict On

Public Class Furniture_Order_Form
    'Declare Constants
    Const dblCHAIR_PRICE As Double = 350
    Const dblSOFA_PRICE As Double = 925
    Const dblSALE_TAX As Double = 0.05

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        'Declare variables that will hold the values that are put in the textboxes by the user.
        Dim strCustName As String = txtName.Text
        Dim strStreet As String = txtAddress.Text
        Dim strCity As String = txtCity.Text
        Dim intChairs As Integer
        Dim intSofas As Integer

        'Declare variables to calculate the total.
        Dim dblTotalDue As Double
        Dim dblTotalTax As Double
        Dim dblOrderPrice As Double

        Dim strInvoiceId As String
        Dim strReversedName As String

        'Validate the input into the textboxes.
        If ValidateInput(strCustName, strStreet, strCity) Then
            intChairs = CInt(txtChairs.Text)
            intSofas = CInt(txtSofas.Text)

            dblTotalDue = Calculate(intChairs, intSofas, dblTotalTax, dblOrderPrice)

            strInvoiceId = InvoiceNumber(strCustName, strCity)
            strReversedName = ReverseName(strCustName)

            Call DisplayBill(intChairs, intSofas, dblOrderPrice, dblTotalTax, dblTotalDue, strInvoiceId, strReversedName, strStreet, strCity)
        End If
    End Sub

    'Clear all textboxes and listbox.
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        For Each ctrl In Controls.OfType(Of TextBox)
            ctrl.Clear()
        Next

        lstDisplay.Items.Clear()
        txtName.Focus()
    End Sub

    'End program.
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ''''''''''''''''''''''''''''''''SUBPROCEDURES AND FUNCTIONS'''''''''''''''''''''''''''''''''''''

    Private Sub DisplayBill(intChairs As Integer, intSofas As Integer, dblOrderPrice As Double, dblTotalTax As Double, dblTotalDue As Double,
                            strInvoiceId As String, strReversedName As String, strStreet As String, strCity As String)
        lstDisplay.Items.Add("Invoice Number: " + strInvoiceId)
        lstDisplay.Items.Add("")
        lstDisplay.Items.Add("Name: " + strReversedName)
        lstDisplay.Items.Add("Street Address: " + strStreet)
        lstDisplay.Items.Add("City, State, Zip Code: " + strCity)
        lstDisplay.Items.Add("")
        lstDisplay.Items.Add("Number of Chairs: " + intChairs.ToString)
        lstDisplay.Items.Add("Number of Sofas: " + intSofas.ToString)
        lstDisplay.Items.Add("")
        lstDisplay.Items.Add("Order Price: " + FormatCurrency(dblOrderPrice))
        lstDisplay.Items.Add("Sales Tax: " + FormatCurrency(dblTotalTax))
        lstDisplay.Items.Add("              --------------------")
        lstDisplay.Items.Add("Total Price: " + FormatCurrency(dblTotalDue))
    End Sub


    Function ValidateInput(strCustName As String, strStreet As String, strCity As String) As Boolean
        If Not strCustName.Contains(",") Or strCustName.Length < 4 Then
            MessageBox.Show("Invalid Name. Make sure the Last Name and First Name are separated by a comma", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf strStreet = "" Or strCity = "" Then
            MessageBox.Show("Invalid City info", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf Not IsNumeric(txtChairs.Text) Or Not IsNumeric(txtSofas.Text) Then
            MessageBox.Show("Invalid number of chairs or sofas. Please enter numeric values", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Else
            Return True
        End If
    End Function


    Function Calculate(intChairs As Integer, intSofas As Integer, ByRef dblTotalTax As Double, ByRef dblOrderPrice As Double) As Double
        dblOrderPrice = (intChairs * dblCHAIR_PRICE) + (intSofas * dblSOFA_PRICE)
        dblTotalTax = dblOrderPrice * dblSALE_TAX

        Return dblOrderPrice + dblTotalTax
    End Function


    Function InvoiceNumber(strCustName As String, strCity As String) As String
        Dim strZipDigits As String

        strZipDigits = strCity.Substring(strCity.Length - 4)

        Return strCustName.Substring(0, 2).ToUpper() & strZipDigits 'Start at the beginning of the string (0) and go forward (2) characters. Convert the characters to uppercase. Concatenate with the zip code digits.
    End Function


    Function ReverseName(strCustName As String) As String
        Dim strFirstName As String
        Dim strLastName As String
        Dim intCommaPosition As Integer

        intCommaPosition = strCustName.IndexOf(",")
        strFirstName = strCustName.Substring(intCommaPosition + 2)
        strLastName = strCustName.Substring(0, intCommaPosition)

        Return strFirstName & ", " & strLastName
    End Function
End Class
