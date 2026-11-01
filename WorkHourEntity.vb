Imports FoundationLibrary
Imports MyDatabase = FoundationLibrary.Database
Public Class WorkHourEntity
    Public ID As Integer
    Public StartDate As Date
    Public StartTime As TimeSpan
    Public EndDate As Date
    Public EndTime As TimeSpan
    Public BreakTime As TimeSpan

    Public ReadOnly Property StartWork As Date
        Get
            Return StartDate.Date.Add(StartTime)
        End Get
    End Property
    Public ReadOnly Property EndWork As Date
        Get
            Return EndDate.Date.Add(EndTime)
        End Get
    End Property
    Public ReadOnly Property Sum As TimeSpan
        Get
            Dim StartWork As Date, EndWork As Date

            StartWork = StartDate.Date.Add(StartTime)
            EndWork = EndDate.Date.Add(EndTime)

            If EndWork < StartWork Then
                Throw New Exception("Η λήξη δεν μπορεί να είναι πριν από την έναρξη.")
            End If

            Return EndWork - StartWork - BreakTime
        End Get
    End Property
End Class

Public Class SummarizeWork

    Private _ListWorkHours As New List(Of WorkHourEntity)

    Dim MyDB As New MyDatabase.MSAccessOLEDB(MyDatabase.Tools.Providers.TypeProviders.ACE16, "WorkHours.accdb")

    Public ReadOnly Property ListWorkHours As IReadOnlyCollection(Of WorkHourEntity)
        Get
            Return _ListWorkHours.AsReadOnly
        End Get
    End Property

    Public ReadOnly Property SumWorkDuration As TimeSpan
        Get
            Dim Sum As TimeSpan = Nothing
            For i = 0 To ListWorkHours.Count - 1
                Sum += ListWorkHours(i).Sum
            Next
            Return Sum
        End Get
    End Property

    Default ReadOnly Property WorkHour(Index As Integer) As WorkHourEntity
        Get
            Return _ListWorkHours(Index)
        End Get
    End Property

    Sub LoadData()
        _ListWorkHours.Clear()
        Dim DT As New DataTable
        MyDB.Command(MyDatabase.Queries.QSelect("WorkHours"), DT)
        For i = 0 To DT.Rows.Count - 1
            Dim Work As New WorkHourEntity
            With Work
                .ID = DT(i)(0)
                .StartDate = DT(i)(1)
                .StartTime = TimeSpan.Parse(Date.Parse(DT(i)(2)).Hour & ":" & Date.Parse(DT(i)(2)).Minute)
                .EndDate = DT(i)(3)
                .EndTime = TimeSpan.Parse(Date.Parse(DT(i)(4)).Hour & ":" & Date.Parse(DT(i)(4)).Minute)
                .BreakTime = TimeSpan.Parse(Date.Parse(DT(i)(5)).Hour & ":" & Date.Parse(DT(i)(5)).Minute)
            End With
            _ListWorkHours.Add(Work)
        Next
    End Sub

    Function Add(WorkHour As WorkHourEntity) As Boolean
        MyDB.Command(MyDatabase.Queries.QInsert("WorkHours", "StartDate,StartTime,EndDate,EndTime,BreakTime", {WorkHour.StartDate, WorkHour.StartTime.ToString, WorkHour.EndDate, WorkHour.EndTime.ToString, WorkHour.BreakTime.ToString}), WorkHour.ID)
        _ListWorkHours.Add(WorkHour)
        Return True
    End Function

    Function Delete(WorkHours As WorkHourEntity) As Boolean
        MyDB.Command(MyDatabase.Queries.QDelete("WorkHours", "[ID]=" & WorkHours.ID))
        _ListWorkHours.Remove(WorkHours)
        Return True
    End Function

    Function Update(OldWorkHours As WorkHourEntity, NewWorkHours As WorkHourEntity) As Boolean
        MyDB.Command(MyDatabase.Queries.QUpdate("WorkHours", "[ID]=" & OldWorkHours.ID, "StartDate,StartTime,EndDate,EndTime,BreakTime", {NewWorkHours.StartDate, NewWorkHours.StartTime.ToString, NewWorkHours.EndDate, NewWorkHours.EndTime.ToString, NewWorkHours.BreakTime.ToString}))
        NewWorkHours.ID = OldWorkHours.ID
        _ListWorkHours(_ListWorkHours.IndexOf(OldWorkHours)) = NewWorkHours
        Return True
    End Function

    Function Search(StartDate As Date?, Optional EndDate As Date? = Nothing) As List(Of WorkHourEntity)
        Dim ListWork As New List(Of WorkHourEntity)
        For i = 0 To ListWorkHours.Count - 1
            If StartDate IsNot Nothing And EndDate IsNot Nothing Then
                If ListWorkHours(i).StartDate >= StartDate And ListWorkHours(i).StartDate <= EndDate Then
                    ListWork.Add(ListWorkHours(i))
                End If
            ElseIf StartDate IsNot Nothing And EndDate Is Nothing Then
                If ListWorkHours(i).StartDate >= StartDate Then
                    ListWork.Add(ListWorkHours(i))
                End If
            ElseIf StartDate Is Nothing And EndDate IsNot Nothing Then
                If ListWorkHours(i).StartDate <= EndDate Then
                    ListWork.Add(ListWorkHours(i))
                End If
            Else
                Return _ListWorkHours
            End If
        Next
        Return ListWork
    End Function
End Class