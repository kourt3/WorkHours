Module OptionView
    Sub Show()
        Do
            Console.Clear()
            Console.WriteLine("----------- Options ------------")
            Console.WriteLine("Εργασιμες ώρες Συμβολαιου: " & Opt.Contract.ErgatoWresSymbolaioy.ToString("hh\:mm"))
            Console.WriteLine("Αξία εργασίας ανα ώρα: " & Opt.Contract.MoneyOfHour)
            Console.WriteLine("---------------------------------")
            Console.WriteLine("1) Αλλαγή εργάσιμες ώρες συμβολαιου.")
            Console.WriteLine("2) Αλλαγή αξίας εργασίας.")
            Console.WriteLine("3) Exit.")
            Console.WriteLine("----------------------------------")
            Console.WriteLine("Επέλεξε ενα απο το μενου:")
            Dim choice As String = Console.ReadLine
            Select Case choice
                Case 1
                    ChangeContractHours()
                Case 2
                    ChangeMoneyOfHour(Opt.Contract)
                Case 3
                    Exit Sub
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub

    Sub ChangeContractHours()

    End Sub
    Sub ChangeMoneyOfHour(OptionsLink As OptionsEnity)
        Do
            Dim Options As New OptionsEnity
            Options = Opt.Contract
            Console.Clear()
            Console.WriteLine("------------ Αλλαγή αξίας ανα ώρα -----------")
            Console.WriteLine("Δώσε την αξία ανα ώρα εργασίας:")
            Options.MoneyOfHour = Console.ReadLine
Again:
            Console.Clear()
            Console.WriteLine("--------- Επιβεβαίωση αλλαγής αξίας ανα ώρα ---------")
            Console.WriteLine("Παλία Αξίας ανα ώρα εργασίας: " & OptionsLink.MoneyOfHour)
            Console.WriteLine("Καινούργια Αξίας ανα ώρα εργασίας: " & Options.MoneyOfHour)
            Console.WriteLine()
            Console.WriteLine("Θέλεις να συνεχίσεις με την αλλαγή;")
            Console.WriteLine("Y) NAI")
            Console.WriteLine("N) OXI")
            Dim choice As String = Console.ReadLine
            Select Case choice
                Case "Y"
                    Opt.Update(Options)
                    OptionsLink = Options
                    Exit Sub
                Case "N"
                    Exit Sub
                Case Else
                    GoTo Again
            End Select



        Loop
    End Sub
End Module
