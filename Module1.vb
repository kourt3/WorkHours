Imports Microsoft.VisualBasic.Devices
Imports MyDatabase = FoundationLibrary.Database

Module Module1
    Dim Summarize As New SummarizeWork
    Sub Main()
        Do
            Summarize.LoadData()

            Console.Clear()
            Console.WriteLine("---------- Menu -----------")
            Console.WriteLine("Συνολικα Δούλεψες: " & Summarize.SumWorkDuration.Days & " Days " & Summarize.SumWorkDuration.Hours & " Hours " & Summarize.SumWorkDuration.Minutes & " Minutes ")
            Console.WriteLine("Συνολικες Ωρες που Δούλεψες: " & Summarize.SumWorkDuration.TotalHours)
            Console.WriteLine("Συνολικα Λεπτά που Δούλεψες: " & Summarize.SumWorkDuration.TotalMinutes)
            Console.WriteLine("----------------------------")
            Console.WriteLine("1) Προσθήκη Έργατοώρας.")
            Console.WriteLine("2) Αναλυτικά δεδομένα.")
            Console.WriteLine("3) Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case 1
                    AddWorkSchedule()
                Case 2
                    AnalyticWorkHours()
                Case 3
                    Exit Do
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub



    Sub AnalyticWorkHours()
        Do

            Console.Clear()
            Console.WriteLine("------------ Analyticks ----------")
            For i = 0 To Summarize.ListWorkHours.Count - 1
                Console.WriteLine("-------------- Επιλογη (" & i + 1 & ")  ---------------------")
                Console.WriteLine("Star : " & Summarize(i).StartWork)
                Console.WriteLine("End  : " & Summarize(i).EndWork)
                Console.WriteLine("BreakTime : " & Summarize(i).BreakTime.TotalMinutes)
                Console.WriteLine("Sum : " & Summarize(i).Sum.Days & " Days " & Summarize(i).Sum.Hours & " Hours " & Summarize(i).Sum.Minutes & " Minutes ")
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
                        AddWorkSchedule()
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
                        AddWorkSchedule()
                    Case 2
                        Exit Do
                    Case Else
                        Continue Do
                End Select
            End If
        Loop
    End Sub

    Sub Show(WorkHours As WorkHourEntity)
        If WorkHours.ID <> Nothing Then
            Console.WriteLine("ID: " & WorkHours.ID)
        End If
        Console.WriteLine("Start Date and Time: " & WorkHours.StartWork)
        Console.WriteLine("End Date And Time: " & WorkHours.EndWork)
        Console.WriteLine("Break Time: " & WorkHours.BreakTime.ToString)
        Console.WriteLine("-------------------------")
        Console.WriteLine("Συνολικα Δούλεψες: " & WorkHours.Sum.Days & " Days " & WorkHours.Sum.Hours & " Hours " & WorkHours.Sum.Minutes & " Minutes ")
        Console.WriteLine("Συνολικες Ωρες που Δούλεψες: " & WorkHours.Sum.TotalHours)
        Console.WriteLine("Συνολικα Λεπτά που Δούλεψες: " & WorkHours.Sum.TotalMinutes)
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
                    UpdateSchedule(WorkHours)
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
    Sub UpdateSchedule(ByRef WorkHours As WorkHourEntity)

        Console.Clear()
        Console.WriteLine("------------- Update Schedule:----------------")
        Console.WriteLine("ID: " & WorkHours.ID)
        Console.WriteLine("Start Date and Time: " & WorkHours.StartWork)
        Console.WriteLine("End Date And Time: " & WorkHours.EndWork)
        Console.WriteLine("Break Time: " & WorkHours.BreakTime.ToString)
        Console.WriteLine("----------------------------------------------")
        Dim NewWorkhour As New WorkHourEntity

        Try
            Console.WriteLine("Δώσε την Ημμερομηνία που ξεκίνησες:(DD/MM/YYYY)")
            NewWorkhour.StartDate = Console.ReadLine

            Console.WriteLine("Δώσε την ώρα που ξεκίνησες:(HH:MM)")
            NewWorkhour.StartTime = TimeSpan.Parse(Console.ReadLine())


            Console.WriteLine("Δώσε την Ημμερομηνία που Τελειώσες:(DD/MM/YYYY)")
            NewWorkhour.EndDate = Console.ReadLine

            Console.WriteLine("Δώσε την ώρα που Τελείωσες:(HH:MM)")
            NewWorkhour.EndTime = TimeSpan.Parse(Console.ReadLine())

            Console.WriteLine("Ποσες ώρες διαλλημα έχετε κάνει(HH:MM):")
            NewWorkhour.BreakTime = TimeSpan.Parse(Console.ReadLine)
            Console.WriteLine("-----------------------------------------------")

            Console.WriteLine("Δουλεψες συνολικά: " & NewWorkhour.Sum.Days & "# " & NewWorkhour.Sum.Hours & ":" & NewWorkhour.Sum.Minutes)
            Console.WriteLine("Δουλεψες συνολικά(Ωρες): " & NewWorkhour.Sum.TotalHours)
            Console.WriteLine("Δουλεψες συνολικά(Λεπτά):" & NewWorkhour.Sum.TotalMinutes)
            Console.WriteLine("------------------------------------------------")

            If ConfingureUpdate(WorkHours, NewWorkhour) = True Then
                NewWorkhour.ID = WorkHours.ID
                WorkHours = NewWorkhour
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try

    End Sub
    Function ConfingureUpdate(OldWorkHours As WorkHourEntity, NewWorkHours As WorkHourEntity) As Boolean
        Do
            Console.Clear()
            Console.WriteLine("------------- OLD Schedule ------------")
            Show(OldWorkHours)
            Console.WriteLine()
            Console.WriteLine("------------- New Schedule ------------")
            Show(NewWorkHours)
            Console.WriteLine("---------------------------------------")
            Console.WriteLine("Θέλεις να συνεχίσεις με την αλλαγή?")
            Console.WriteLine("Y) ΝΑΙ")
            Console.WriteLine("N) ΟΧΙ - Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case "Y"
                    Summarize.Update(OldWorkHours, NewWorkHours)

                    Return True
                Case "N"
                    Return False
                Case Else
                    Continue Do
            End Select

        Loop
    End Function
    Sub AddWorkSchedule()

        Dim WorkHour As New WorkHourEntity

        Console.Clear()
        Console.WriteLine("--------------- Ypologismos Ωραριου --------------")
        Try
            Console.WriteLine("Δώσε την Ημμερομηνία που ξεκίνησες:(DD/MM/YYYY)")
            WorkHour.StartDate = Console.ReadLine

            Console.WriteLine("Δώσε την ώρα που ξεκίνησες:(HH:MM)")
            WorkHour.StartTime = TimeSpan.Parse(Console.ReadLine())


            Console.WriteLine("Δώσε την Ημμερομηνία που Τελειώσες:(DD/MM/YYYY)")
            WorkHour.EndDate = Console.ReadLine

            Console.WriteLine("Δώσε την ώρα που Τελείωσες:(HH:MM)")
            WorkHour.EndTime = TimeSpan.Parse(Console.ReadLine())

            Console.WriteLine("Ποσες ώρες διαλλημα έχετε κάνει(HH:MM):")
            WorkHour.BreakTime = TimeSpan.Parse(Console.ReadLine)
            Console.WriteLine("-----------------------------------------------")

            ConfigureAdd(WorkHour)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Sub ConfigureAdd(WorkHour As WorkHourEntity)
        Do
            Console.Clear()
            Console.WriteLine("------------- Add schedule -----------")
            Show(WorkHour)
            Console.WriteLine("--------------------------------------")
            Console.WriteLine("Θέλεις να σύνεχήσεις με την εγραφή?")
            Console.WriteLine("Y) NAI.")
            Console.WriteLine("N) OXI - Exit")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case "Y"
                    Summarize.Add(WorkHour)
                    Exit Sub
                Case "N"
                    Exit Sub
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub
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
