Imports Microsoft.VisualBasic.Devices
Imports MyDatabase = FoundationLibrary.Database

Module Main
    Public Summarize As New SummarizeWork
    Public Inc As New IncreaseWorks
    Public Opt As New Options
    Sub Main()
        Do
            Summarize.LoadData()
            Inc.LoadData()
            Opt.LoadData()

            Console.Clear()
            Console.WriteLine("---------- Menu -----------")
            Console.WriteLine("Συνολικα Δούλεψες: " & Summarize.SumWorkDuration.Days & " Days " & Summarize.SumWorkDuration.Hours & " Hours " & Summarize.SumWorkDuration.Minutes & " Minutes ")
            Console.WriteLine("Συνολικες Ωρες που Δούλεψες: " & Summarize.SumWorkDuration.TotalHours)
            Console.WriteLine("Συνολικα Λεπτά που Δούλεψες: " & Summarize.SumWorkDuration.TotalMinutes)
            Console.WriteLine("----------------------------")
            Console.WriteLine("Συνολική αξία που συμπληρώθηκε: " & Summarize.SumAmmount)
            Console.WriteLine("----------------------------")

            Console.WriteLine("1) Προσθήκη Έργατοώρας.")
            Console.WriteLine("2) Αναλυτικά δεδομένα.")
            Console.WriteLine("3) Αναλυτικα αξίας εργατοώρα.")
            Console.WriteLine("4) Ρυθμίσεις συμβολαιου.")
            Console.WriteLine("5) Exit.")
            Dim Choice As String = Console.ReadLine
            Select Case Choice
                Case 1
                    ChoiceSchedule()
                Case 2
                    AnalyticSchedule()
                Case 3
                    AnalyzIncrease()
                Case 4
                    OptionView.Show()
                Case 5
                    Exit Do
                Case Else
                    Continue Do
            End Select
        Loop
    End Sub



End Module
