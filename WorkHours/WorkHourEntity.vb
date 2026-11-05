Imports FoundationLibrary
Public Enum TypeWork
    NormalWork
    Repo
    Adeia
    Anarwtikh
End Enum
Public Structure WorkHourEntity
    Public ID As Integer
    Public StartDate As Date
    Public StartTime As TimeSpan
    Public EndDate As Date
    Public EndTime As TimeSpan
    Public BreakTime As TimeSpan
    Public MoneyOfHour As Decimal
    Public WorkSignature As TimeSpan
    Public Money As Decimal
    Public TypeWork As TypeWork
    Public ReadOnly Property Amount As Decimal
        Get
            If TypeWork = TypeWork.NormalWork Then Return (MoneyOfHour / 60) * Sum.TotalMinutes
            If TypeWork = TypeWork.Repo Then Return Nothing
            If TypeWork = TypeWork.Adeia Then Return (MoneyOfHour / 60) * WorkSignature.TotalMinutes
            If TypeWork = TypeWork.Anarwtikh Then Return Money
            Return Nothing
        End Get
    End Property
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
            If TypeWork = TypeWork.NormalWork Then
                Dim StartWork As Date, EndWork As Date

                StartWork = StartDate.Date.Add(StartTime)
                EndWork = EndDate.Date.Add(EndTime)

                If EndWork < StartWork Then
                    Throw New Exception("Η λήξη δεν μπορεί να είναι πριν από την έναρξη.")
                End If

                Return EndWork - StartWork - BreakTime
            End If

        End Get
    End Property
End Structure