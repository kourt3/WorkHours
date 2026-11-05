Module IncreaseView
    Sub AddIncrease()
        Do
            Dim Increase As IncreaseValueEntity
            Console.Clear()
            Console.WriteLine("------------- Add Increase -------------")

            With Increase
                Console.WriteLine("Δώσε Ημμερομηνία προσαυξηση:")
                .IncDate = Console.ReadLine

                Console.WriteLine("Δώσε την αξια των χρημάτων (Ανα ώρα): ")
                .MoneyOfHour = Console.ReadLine
            End With

            Console.Clear()
            Console.WriteLine("-----------------------")
            Show(Increase)
            Console.WriteLine("-----------------------")
            Console.WriteLine("Θέλεις να συνεχίσεις με την εγγραφή?")
            Console.WriteLine("Y) NAI.")
            Console.WriteLine("N) OXI - Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case "Y"
                    Inc.Add(Increase)
                    Dim MoneyOfhour As Decimal = Nothing
                    For i = 0 To Summarize.ListWorkHours.Count - 1
                        Dim WorkHour As New WorkHourEntity
                        WorkHour = Summarize.ListWorkHours(i)

                        MoneyOfhour = Inc.CheckDate(WorkHour.StartDate)
                        If MoneyOfhour = Nothing And Opt.Contract.MoneyOfHour = Nothing Then
                            Continue Do
                        ElseIf MoneyOfhour = Nothing And Opt.Contract.MoneyOfHour > 0 Then
                            WorkHour.MoneyOfHour = Opt.Contract.MoneyOfHour
                        Else
                            WorkHour.MoneyOfHour = MoneyOfhour
                        End If
                        Summarize.Update(Summarize.ListWorkHours(i), WorkHour)
                    Next

                    Exit Sub
                Case "N"
                    Exit Do
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub
    Sub UpdateIncrease(ByRef OldIncrease As IncreaseValueEntity)
        Do
            Dim NewIncrease As IncreaseValueEntity
            Console.Clear()
            Console.WriteLine("--------------- Επεξεργασία Προσαυξησης ---------------")
            Show(OldIncrease)
            Console.WriteLine("-------------------------------------------------------")

            With NewIncrease
                .ID = OldIncrease.ID

                Console.WriteLine("Δώσε Ημμερομηνία προσαυξηση:")
                .IncDate = Console.ReadLine

                Console.WriteLine("Δώσε την αξια των χρημάτων (Ανα ώρα): ")
                .MoneyOfHour = Console.ReadLine
            End With

            Console.Clear()

            Console.WriteLine("--------- Old Increase ----------")
            Show(OldIncrease)
            Console.WriteLine("-----------------------")
            Console.WriteLine()
            Console.WriteLine("--------- New Increase ----------")
            Show(NewIncrease)
            Console.WriteLine("-----------------------")

            Console.WriteLine("Θέλεις να συνεχίσεις με την Αλλαγή?")
            Console.WriteLine("Y) NAI.")
            Console.WriteLine("N) OXI - Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case "Y"
                    If Inc.Update(OldIncrease, NewIncrease) Then
                        OldIncrease = NewIncrease
                    End If

                    Dim MoneyOfhour As Decimal = Nothing
                    For i = 0 To Summarize.ListWorkHours.Count - 1
                        Dim WorkHour As New WorkHourEntity
                        WorkHour = Summarize.ListWorkHours(i)

                        MoneyOfhour = Inc.CheckDate(WorkHour.StartDate)
                        If MoneyOfhour = Nothing And Opt.Contract.MoneyOfHour = Nothing Then
                            Continue Do
                        ElseIf MoneyOfhour = Nothing And Opt.Contract.MoneyOfHour > 0 Then
                            WorkHour.MoneyOfHour = Opt.Contract.MoneyOfHour
                        Else
                            WorkHour.MoneyOfHour = MoneyOfhour
                        End If
                        Summarize.Update(Summarize.ListWorkHours(i), WorkHour)
                    Next


                    Exit Sub
                Case "N"
                    Exit Do
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub
    Function DeleteIncrease(Increase As IncreaseValueEntity) As Boolean
        Do
            Console.Clear()
            Console.WriteLine("----------- Διαγραφή προσαυξησης -------------")
            Show(Increase)
            Console.WriteLine("----------------------------------------------")
            Console.WriteLine("Θέλεις να διαγράψεις την εγραφή?")
            Console.WriteLine("Y) NAI.")
            Console.WriteLine("N) OXI - Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case "Y"
                    Inc.Delete(Increase)

                    Dim MoneyOfhour As Decimal = Nothing
                    For i = 0 To Summarize.ListWorkHours.Count - 1
                        Dim WorkHour As New WorkHourEntity
                        WorkHour = Summarize.ListWorkHours(i)

                        MoneyOfhour = Inc.CheckDate(WorkHour.StartDate)
                        If MoneyOfhour = Nothing And Opt.Contract.MoneyOfHour = Nothing Then
                            Continue Do
                        ElseIf MoneyOfhour = Nothing And Opt.Contract.MoneyOfHour > 0 Then
                            WorkHour.MoneyOfHour = Opt.Contract.MoneyOfHour
                        Else
                            WorkHour.MoneyOfHour = MoneyOfhour
                        End If
                        Summarize.Update(Summarize.ListWorkHours(i), WorkHour)
                    Next

                    Return True

                Case "N"
                    Return False
                Case Else
                    Continue Do
            End Select
        Loop
    End Function
    Sub AnalyzIncrease()
        Do
            Console.Clear()
            Console.WriteLine("------------ Αναλυτικα προσαυξησεις ----------")

            If Inc.Increase.Count = 0 Then

                Console.WriteLine("Δεν βρεθηκαν εγραφές.")
                Console.WriteLine("------------------------------------------")
                Console.WriteLine("1) Add προσαυξηση")
                Console.WriteLine("2) Exit")

                Dim choice As String = Console.ReadLine
                Select Case choice
                    Case 1
                        AddIncrease()
                    Case 2
                        Exit Do
                    Case Else
                        Continue Do
                End Select

            Else

                For i = 0 To Inc.Increase.Count - 1

                    With Inc.Increase(i)
                        Console.WriteLine("------------- Επιλογή: (" & i + 1 & ") ----------------")
                        Show(Inc.Increase(i))
                        Console.WriteLine("-----------------------------------")
                        Console.WriteLine()
                    End With

                    Console.WriteLine(1 & " To " & Inc.Increase.Count & ") Επιλογή.")
                    Console.WriteLine(Inc.Increase.Count + 1 & ") Προσθήκη Νεου.")
                    Console.WriteLine(Inc.Increase.Count + 2 & ") Exit.")

                    Dim Choice As String = Console.ReadLine - 1
                    Select Case Choice
                        Case 0 To Inc.Increase.Count - 1
                            Open(Inc.Increase(Choice))
                        Case Inc.Increase.Count
                            AddIncrease()
                        Case Inc.Increase.Count + 1
                            Exit Do
                        Case Else
                            Continue Do
                    End Select

                Next

            End If

        Loop

    End Sub

    Sub Show(Increace As IncreaseValueEntity)
        With Increace
            If .ID <> Nothing Then
                Console.WriteLine("ID: " & .ID)
            End If
            Console.WriteLine("Date: " & .IncDate)
            Console.WriteLine("Money Of Hour: " & .MoneyOfHour)
            Console.WriteLine("Money Of Minutes: " & .MoneyOfMinute)
        End With
    End Sub


    Sub Open(Increase As IncreaseValueEntity)
        Do
            Console.Clear()
            Console.WriteLine("---------- Open Increase ----------")
            Show(Increase)
            Console.WriteLine("-----------------------------------")
            Console.WriteLine("1) Επεξεργασία.")
            Console.WriteLine("2) Διαγραφή.")
            Console.WriteLine("3) Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case 1
                    UpdateIncrease(Increase)
                Case 2
                    If DeleteIncrease(Increase) Then
                        Exit Sub
                    End If
                Case 3
                    Exit Do
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub
End Module
