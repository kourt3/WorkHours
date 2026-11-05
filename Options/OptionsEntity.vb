Imports Mydatabase = FoundationLibrary.Database
Public Structure OptionsEnity
    Dim AutomathSimplirwshWrarioy As Boolean
    Dim Anarwtikh As Double
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
        Contract.Anarwtikh = DT(0)(1)
        Contract.AutomathSimplirwshWrarioy = DT(0)(2)
        Contract.MoneyOfHour = DT(0)(3)
    End Sub

    Sub Update(ContractLink As OptionsEnity)
        MyDB.Command(Mydatabase.Queries.QUpdate("Options", "WorkHours,AnarwtikhPersent,AytoHint,MoneyOfHour", {ContractLink.ErgatoWresSymbolaioy.ToString, ContractLink.Anarwtikh, ContractLink.AutomathSimplirwshWrarioy, ContractLink.MoneyOfHour}))
        Contract = ContractLink
    End Sub
End Class
