
Imports System.Collections.Specialized

Namespace Core


    Public Class ExType
        Public Property FilePath As String = String.Empty
        Public Property IsSolve As Boolean = False
    End Class

    Public Class ExclusionManager

        Public Shared Function GetAll() As List(Of ExType)
            Try
                Dim SavedTask As StringCollection = My.Settings.Exclusion
                Dim TaskListDo As New List(Of ExType)
                Dim TaskList As List(Of String) = New List(Of String)
                If SavedTask IsNot Nothing Then
                    TaskList = SavedTask.Cast(Of String)().ToList()
                End If
                Try
                    For Each Tasks As String In TaskList
                        Dim TaskInfo As String() = Tasks.Split("|")
                        If Not TaskInfo.Count = 0 Then
                            Dim Item As New ExType
                            Item.FilePath = TaskInfo(0)
                            Item.IsSolve = TaskInfo(1)
                            TaskListDo.Add(Item)
                        End If
                    Next
                Catch ex As Exception

                End Try
                Return TaskListDo
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function Add(ByVal Task As ExType)
            Dim SavedTask As StringCollection = My.Settings.Exclusion
            Dim TaskList As List(Of String)
            If SavedTask Is Nothing Then
                TaskList = New List(Of String)
            Else
                TaskList = SavedTask.Cast(Of String)().ToList()
            End If
            Dim FormatStr As String = Task.FilePath & "|" & Task.IsSolve
            TaskList.Add(FormatStr)
            Dim collection As StringCollection = New StringCollection()
            collection.AddRange(TaskList.ToArray())
            My.Settings.Exclusion = collection
            My.Settings.Save()
            Return True
        End Function

        Public Shared Function RemoveByFile(ByVal Item As ExType)
            Dim SavedTask As StringCollection = My.Settings.Exclusion
            Dim TaskList As List(Of String)
            If SavedTask Is Nothing Then
                TaskList = New List(Of String)
            Else
                TaskList = SavedTask.Cast(Of String)().ToList()
            End If

            Try
                For Each Tasks As String In TaskList
                    Dim TaskInfo As String() = Tasks.Split("|")
                    If Not TaskInfo.Count = 0 Then
                        If TaskInfo(0) = Item.FilePath Then
                            TaskList.Remove(Tasks)
                        End If
                    End If
                Next
            Catch ex As Exception

            End Try

            Dim collection As StringCollection = New StringCollection()
            collection.AddRange(TaskList.ToArray())
            My.Settings.Exclusion = collection
            My.Settings.Save()
            Return True
        End Function

    End Class

End Namespace

