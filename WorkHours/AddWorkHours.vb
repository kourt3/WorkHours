Module AddWorkHours
    Sub AddSickLeave()
        Dim WorkHour As New WorkHourEntity
        WorkHour.TypeWork = TypeWork.Adeia
        Console.Clear()
        Console.WriteLine("--------------- Εγραφή Αδειας -----------")
        Try
            Console.WriteLine("Δώσε την ημμερομηνία:")
            WorkHour.StartDate = Console.ReadLine

            Console.WriteLine("Δώσε το ποσο που θα εισπράξεις: ")
            WorkHour.WorkSignature = TimeSpan.Parse(Console.ReadLine())

            ConfigureAdd(WorkHour)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Sub AddLeaveSchedule()
        Dim WorkHour As New WorkHourEntity
        WorkHour.TypeWork = TypeWork.Adeia
        Console.Clear()
        Console.WriteLine("--------------- Εγραφή Αδειας -----------")
        Try
            Console.WriteLine("Δώσε την ημμερομηνία:")
            WorkHour.StartDate = Console.ReadLine

            Console.WriteLine("Δώσε της συνολικες εργασιμες ώρες του συμβολαιου :")
            WorkHour.WorkSignature = TimeSpan.Parse(Console.ReadLine())

            ConfigureAdd(WorkHour)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Sub AddPauseSchedule()
        Dim WorkHour As New WorkHourEntity
        WorkHour.TypeWork = TypeWork.Repo
        Console.Clear()
        Console.WriteLine("--------------- Εγραφή Ανάπαυσης -----------")
        Try
            Console.WriteLine("Δώσε την ημμερομηνία:")
            WorkHour.StartDate = Console.ReadLine
            ConfigureAdd(WorkHour)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub
    Sub AddWorkSchedule()

        Dim WorkHour As New WorkHourEntity
        WorkHour.TypeWork = TypeWork.NormalWork

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

            WorkHour.MoneyOfHour = Inc.CheckDate(WorkHour.StartDate)
            If WorkHour.MoneyOfHour = Nothing And Opt.Contract.MoneyOfHour = 0 Then
                Console.WriteLine("Δώσε την αξία της ώρας : ")
                WorkHour.MoneyOfHour = Console.ReadLine
            ElseIf WorkHour.MoneyOfHour = Nothing And Opt.Contract.MoneyOfHour > 0 Then
                WorkHour.MoneyOfHour = Opt.Contract.MoneyOfHour
            End If

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
            WorkHoursView.Show(WorkHour)
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
End Module
