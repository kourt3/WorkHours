Imports Mydatabase = FoundationLibrary.Database
Public Structure OptionsEnity
    Dim ErgatoWresSymbolaioy As TimeSpan
    Dim MoneyOfHour As Decimal
End Structure

Public Class Options
    Public Contract As OptionsEnity
    Dim MyDB As New Mydatabase.MSAccessOLEDB(Mydatabase.Tools.Providers.TypeProviders.ACE16, "WorkHours.accdb")

    Sub LoadData()
        Dim DT As New DataTable
        MyDB.Command(Mydatabase.Queries.QSelect("Options"), DT)
        Contract.ErgatoWresSymbolaioy = TimeSpan.Parse(Date.Parse(DT(0)(0)).Hour & ":" & Date.Parse(DT(0)(0)).Minute)
        Contract.MoneyOfHour = DT(0)(1)
    End Sub

    Sub Update(ContractLink As OptionsEnity)
        MyDB.Command(Mydatabase.Queries.QUpdate("Options", "WorkHours,MoneyOfHour", {ContractLink.ErgatoWresSymbolaioy.ToString, ContractLink.MoneyOfHour}))
        Contract = ContractLink
    End Sub
End Class
