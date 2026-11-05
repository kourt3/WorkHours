Module OptionView
    Sub Show()
        Do
            Console.Clear()
            Console.WriteLine("----------- Options ------------")
            Console.WriteLine("Εργασιμες ώρες Συμβολαιου: " & Opt.Contract.ErgatoWresSymbolaioy.ToString("hh\:mm"))
            Console.WriteLine("Ποσο εισπραξεις απο αναρωτική: " & Opt.Contract.Anarwtikh)
            Console.WriteLine("Αυτοματη συμπλίρωση κατα την εγραφή το ωρομισθιο: " & Opt.Contract.AutomathSimplirwshWrarioy)
            Console.WriteLine("---------------------------------")
            Console.WriteLine("1) Αλλαγή εργάσιμες ώρες συμβολαιου.")
            Console.WriteLine("2) Αλλαγή είσπραξει απο αναρωτική.")
            Console.WriteLine("3) Αλλαγή Συμπλήρωσης κατα την εγραφή.")
            Console.WriteLine("4) Exit.")
            Console.WriteLine("----------------------------------")
            Console.WriteLine("Επέλεξε ενα απο το μενου:")
            Dim choice As String = Console.ReadLine
            Select Case choice
                Case 1
                    ChangeContractHours()
                Case 2
                    ChangeSickLeave()
                Case 3
                    ChangeAutoHint()
                Case 4
                    Exit Sub
                Case Else
                    Continue Do
            End Select

            Console.ReadLine()

        Loop
    End Sub
    Sub ChangeAutoHint()

    End Sub
    Sub ChangeSickLeave()

    End Sub
    Sub ChangeContractHours()

    End Sub
End Module
