Imports MyDataBase = FoundationLibrary.Database

Public Structure IncreaseValueEntity
    Dim ID As Integer
    Dim IncDate As Date
    Dim MoneyOfHour As Decimal
    ReadOnly Property MoneyOfMinute As Decimal
        Get
            Return MoneyOfHour / 60
        End Get
    End Property
End Structure

Public Class IncreaseWorks
    Private _Increase As New List(Of IncreaseValueEntity)
    Private MyDB As New MyDataBase.MSAccessOLEDB(MyDataBase.Tools.Providers.TypeProviders.ACE16, "WorkHours.accdb")

    ReadOnly Property Increase As IReadOnlyCollection(Of IncreaseValueEntity)
        Get
            Return _Increase
        End Get
    End Property

    Default ReadOnly Property Inc(index As Integer) As IncreaseValueEntity
        Get
            Return _Increase(index)
        End Get
    End Property


    Sub LoadData()
        Dim DT As New DataTable
        MyDB.Command(MyDataBase.Queries.QSelect("IncreaseValue"), DT)
        For i = 0 To DT.Rows.Count - 1
            Dim Increase As New IncreaseValueEntity
            With Increase
                .ID = DT(i)(0)
                .IncDate = DT(i)(1)
                .MoneyOfHour = DT(i)(2)
            End With
            _Increase.Add(Increase)
        Next
    End Sub

    Function Add(IncreaseWork As IncreaseValueEntity) As Boolean
        MyDB.Command(MyDataBase.Queries.QInsert("IncreaseValue", "[IncDate],[Money]", {IncreaseWork.IncDate.ToString("yyyy-MM-dd"), IncreaseWork.MoneyOfHour}), IncreaseWork.ID)
        _Increase.Add(IncreaseWork)
        Return True
    End Function
    Function Update(OldIncWork As IncreaseValueEntity, NewIncWork As IncreaseValueEntity) As Boolean
        MyDB.Command(MyDataBase.Queries.QUpdate("IncreaseValue", "[ID]=" & OldIncWork.ID, "[IncDate],[Money]", {NewIncWork.IncDate, NewIncWork.MoneyOfHour}))
        NewIncWork.ID = OldIncWork.ID
        _Increase(_Increase.IndexOf(OldIncWork)) = NewIncWork
        Return True
    End Function
    Function Delete(IncWork As IncreaseValueEntity) As Boolean
        MyDB.Command(MyDataBase.Queries.QDelete("IncreaseValue", "[ID]=" & IncWork.ID))
        _Increase.Remove(IncWork)
        Return True
    End Function

    Function CheckDate(DateWork As Date) As Single
        Dim Money As Single = Nothing
        For Each IncEach In _Increase
            If IncEach.IncDate <= DateWork Then
                Money = IncEach.MoneyOfHour
            End If
        Next
        Return Money
    End Function
End Class



