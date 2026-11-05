Module WorkHoursView

    Sub ChoiceSchedule()
        Do
            Console.Clear()
            Console.WriteLine("---------- Choice schedule -------------")
            Console.WriteLine("1) Κανονική Eργατοώρα.")
            Console.WriteLine("2) Ρεπό.")
            Console.WriteLine("3) Άδεια.")
            Console.WriteLine("4) αναρωτική.")
            Console.WriteLine("5) Exit.")
            Console.WriteLine("----------------------------------------")
            Console.WriteLine("επέλεξε :")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case 1
                    AddWorkSchedule()
                Case 2
                    AddPauseSchedule()
                Case 3
                    AddLeaveSchedule()
                Case 4
                    AddSickLeave()
                Case 5
                    Exit Sub
                Case Else
                    Continue Do
            End Select
            Exit Sub
        Loop
    End Sub

    Sub AnalyticSchedule()
        Do

            Console.Clear()
            Console.WriteLine("------------ Αναλυτικά εργατοώρας ----------")
            For i = 0 To Summarize.ListWorkHours.Count - 1
                Console.WriteLine("-------------- Επιλογη (" & i + 1 & ")  ---------------------")
                Show(Summarize.ListWorkHours(i))
                Console.WriteLine("--------------------------------------------------")
            Next

            If Summarize.ListWorkHours.Count > 0 Then
                Console.WriteLine(1 & " To " & Summarize.ListWorkHours.Count & ") Επιλογη.")
                Console.WriteLine(Summarize.ListWorkHours.Count + 1 & ") Αναζήτηση.")
                Console.WriteLine(Summarize.ListWorkHours.Count + 2 & ") Προσθήκη Νεου.")
                Console.WriteLine(Summarize.ListWorkHours.Count + 3 & ") Exit.")
                Dim choice As Integer = Console.ReadLine() - 1
                Select Case choice
                    Case 0 To Summarize.ListWorkHours.Count - 1
                        ShowSchedule(Summarize.ListWorkHours(choice))
                    Case Summarize.ListWorkHours.Count
                        Search()
                    Case Summarize.ListWorkHours.Count + 1
                        ChoiceSchedule()
                    Case Summarize.ListWorkHours.Count + 2
                        Exit Do
                    Case Else
                        Continue Do
                End Select
            Else
                Console.WriteLine(1 & ") Προσθήκη Νεου.")
                Console.WriteLine(2 & ") Exit.")
                Dim Choice As String = Console.ReadLine
                Select Case Choice
                    Case 1
                        ChoiceSchedule()
                    Case 2
                        Exit Do
                    Case Else
                        Continue Do
                End Select
            End If
        Loop
    End Sub

    Public Sub Show(WorkHours As WorkHourEntity)
        If WorkHours.ID <> Nothing Then
            Console.WriteLine("ID: " & WorkHours.ID)
        End If
        If WorkHours.TypeWork = TypeWork.NormalWork Then
            Console.WriteLine("Start Date and Time: " & WorkHours.StartWork)
            Console.WriteLine("End Date And Time: " & WorkHours.EndWork)
            Console.WriteLine("Break Time: " & WorkHours.BreakTime.ToString)
            Console.WriteLine("Η κατασταση είναι: Εργασία.")
            Console.WriteLine("-------------------------")
            Console.WriteLine("Συνολικα Δούλεψες: " & WorkHours.Sum.Days & " Days " & WorkHours.Sum.Hours & " Hours " & WorkHours.Sum.Minutes & " Minutes ")
            Console.WriteLine("Συνολικες Ωρες που Δούλεψες: " & WorkHours.Sum.TotalHours)
            Console.WriteLine("Συνολικα Λεπτά που Δούλεψες: " & WorkHours.Sum.TotalMinutes)
            Console.WriteLine("--------------------------")
            Console.WriteLine("Αξία ανα ώρα: " & WorkHours.MoneyOfHour)
            Console.WriteLine("Συνολικη αξία εργασίας: " & WorkHours.Amount)
        ElseIf WorkHours.TypeWork = TypeWork.Repo Then
            Console.WriteLine("Date: " & WorkHours.StartDate)
            Console.WriteLine("Η κατασταση είναι: Ανάπαυση.")
        ElseIf WorkHours.TypeWork = TypeWork.Adeia Then
            Console.WriteLine("Date: " & WorkHours.StartDate)
            Console.WriteLine("Ωράριο εργασίας συμβολαιου: " & WorkHours.WorkSignature.ToString("hh\:mm"))
            Console.WriteLine("Η κατασταση είναι: Άδεια.")
        ElseIf WorkHours.TypeWork = TypeWork.Anarwtikh Then
            Console.WriteLine("Date: " & WorkHours.StartDate)
            Console.WriteLine("To Ποσο που θα εισπράξεις: " & WorkHours.Money)
            Console.WriteLine("Η κατασταση είναι: Αναρωτική Αδεία.")
        End If
    End Sub

    Sub ShowSchedule(WorkHours As WorkHourEntity)
        Do
            Console.Clear()
            Console.WriteLine("------------------------")
            Show(WorkHours)
            Console.WriteLine("--------------------------")
            Console.WriteLine("1) Επεξεργασία.")
            Console.WriteLine("2) Διαγραφή.")
            Console.WriteLine("3) Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case 1
                    If WorkHours.TypeWork = TypeWork.NormalWork Then
                        UpdateSchedule(WorkHours)
                    ElseIf WorkHours.TypeWork = TypeWork.Repo Then
                        UpdatePause(WorkHours)
                    ElseIf WorkHours.TypeWork = TypeWork.Adeia Then
                        UpdateLeave(WorkHours)
                    ElseIf WorkHours.TypeWork = TypeWork.Anarwtikh Then
                        UpdateSickLeave(WorkHours)
                    End If

                Case 2
                    If DeleteSchedule(WorkHours) Then Exit Sub
                Case 3
                    Exit Do
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub

    Function DeleteSchedule(Workhours As WorkHourEntity) As Boolean
        Do
            Console.Clear()
            Console.WriteLine("------------------------")
            Show(Workhours)
            Console.WriteLine("--------------------------")
            Console.WriteLine("Θέλεις να διαγράψεις την Εργατοώρα?")
            Console.WriteLine("Y) ΝΑΙ.")
            Console.WriteLine("O) OXI - Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case "Y"
                    Summarize.Delete(Workhours)
                    Return True
                Case "O"
                    Return False
                Case Else
                    Continue Do
            End Select
        Loop
        Return False
    End Function


    Sub Search()
        Dim Startdate, Enddate As Date
        Do
            Console.Clear()
            Console.WriteLine("------------ Αναζήτηση ---------")
            Console.WriteLine("Δωσε την Περιοδος ξεκινημα:")
            Startdate = Console.ReadLine
            Console.WriteLine("Δώσε την περιοδος τελειώματος:")
            Enddate = Console.ReadLine 'να προσθεσω αμα είναι κενο να περνει ολα τα παραπανω
            Dim listWork As List(Of WorkHourEntity) = Summarize.Search(Startdate, Enddate)
Label1:
            Console.Clear()
            Console.WriteLine("---------------Αποτέλεσμα αναζήτησης:-----------")
            For i = 0 To listWork.Count - 1
                Console.WriteLine("----------------- Επιλογή(" & i + 1 & ") ---------------------")
                Console.WriteLine("ID: " & listWork(i).ID)
                Console.WriteLine("Start Date and Time: " & listWork(i).StartWork)
                Console.WriteLine("End Date And Time: " & listWork(i).EndWork)
                Console.WriteLine("Break Time: " & listWork(i).BreakTime.ToString)
                Console.WriteLine("--------------------------------------------")
            Next
            Console.WriteLine("-------------------------------------------------")
            Console.WriteLine(1 & " To " & listWork.Count & ") Επιλογή.")
            Console.WriteLine(listWork.Count + 1 & ") Νεα Αναζήτηση.")
            Console.WriteLine(listWork.Count + 2 & ") Exit.")
            Dim Choice As String = Console.ReadLine - 1
            Select Case Choice
                Case 0 To listWork.Count - 1
                    ShowSchedule(listWork(Choice))
                Case listWork.Count
                    Continue Do
                Case listWork.Count + 1
                    Exit Do
                Case Else
                    GoTo Label1
            End Select
        Loop
    End Sub
End Module
