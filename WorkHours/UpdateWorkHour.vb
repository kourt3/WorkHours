Module UpdateWorkHour

    Sub UpdateSickLeave(ByRef WorkHours As WorkHourEntity)
        Console.Clear()
        Console.WriteLine("------------- Update Αναρωτική -------------")
        WorkHoursView.Show(WorkHours)
        Console.WriteLine("---------------------------------------")
        Dim newWorkHour As New WorkHourEntity
        newWorkHour.ID = WorkHours.ID
        newWorkHour.TypeWork = TypeWork.Anarwtikh
        Try
            Console.WriteLine("Δώσε την ημμερομηνία που έχεις ρεπό:")
            newWorkHour.StartDate = Console.ReadLine

            Console.WriteLine("Δώσε το ποσό που θα εισπράξεις:")
            newWorkHour.Money = Console.ReadLine

            If ConfingureUpdate(WorkHours, newWorkHour) Then
                WorkHours = newWorkHour
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Sub UpdateLeave(ByRef WorkHours As WorkHourEntity)
        Console.Clear()
        Console.WriteLine("------------- Update Άδεια -------------")
        WorkHoursView.Show(WorkHours)
        Console.WriteLine("---------------------------------------")
        Dim newWorkHour As New WorkHourEntity
        newWorkHour.ID = WorkHours.ID
        newWorkHour.TypeWork = TypeWork.Adeia
        Try
            Console.WriteLine("Δώσε την ημμερομηνία που έχεις ρεπό:")
            newWorkHour.StartDate = Console.ReadLine

            Console.WriteLine("Δώσε της ώρες εργασίας μεσα απο το συμβόλαιο:")
            newWorkHour.WorkSignature = TimeSpan.Parse(Console.ReadLine)

            If ConfingureUpdate(WorkHours, newWorkHour) Then
                WorkHours = newWorkHour
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Sub UpdatePause(ByRef WorkHours As WorkHourEntity)
        Console.Clear()
        Console.WriteLine("------------- Update Ρεπο -------------")
        WorkHoursView.Show(WorkHours)
        Console.WriteLine("---------------------------------------")
        Dim newWorkHour As New WorkHourEntity
        newWorkHour.ID = WorkHours.ID
        newWorkHour.TypeWork = TypeWork.Repo
        Try
            Console.WriteLine("Δώσε την ημμερομηνία που έχεις ρεπό:")
            newWorkHour.StartDate = Console.ReadLine

            If ConfingureUpdate(WorkHours, newWorkHour) Then
                WorkHours = newWorkHour
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try

    End Sub
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
            NewWorkhour.ID = WorkHours.ID
            NewWorkhour.TypeWork = TypeWork.NormalWork

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

            NewWorkhour.MoneyOfHour = Inc.CheckDate(NewWorkhour.StartDate)
            If NewWorkhour.MoneyOfHour = Nothing Then
                Console.WriteLine("Δώσε την αξία της ώρας : ")
                NewWorkhour.MoneyOfHour = Console.ReadLine
            ElseIf NewWorkhour.MoneyOfHour = Nothing And Opt.Contract.MoneyOfHour > 0 Then
                NewWorkhour.MoneyOfHour = Opt.Contract.MoneyOfHour
            End If

            If ConfingureUpdate(WorkHours, NewWorkhour) = True Then
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
            WorkHoursView.Show(OldWorkHours)
            Console.WriteLine()
            Console.WriteLine("------------- New Schedule ------------")
            WorkHoursView.Show(NewWorkHours)
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
End Module
