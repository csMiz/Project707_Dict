Class MainWindow

    ''' <summary>
    ''' 英文搜索源
    ''' </summary>
    Private Source_Eng As New List(Of DictAnalyzer)

    Private SearchResultBuffer As New List(Of String)


    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        InitializeAnalysers()
        box_search.Focus()

    End Sub

    ''' <summary>
    ''' 初始化搜索源
    ''' </summary>
    Public Sub InitializeAnalysers()
        '有道词典
        Dim youdao As New YoudaoAnalyzer
        Source_Eng.Add(youdao)


    End Sub

    Public Sub RefreshResultUI()

        sp_content.Children.Clear()
        For Each tmpContent As String In SearchResultBuffer
            Dim grid_ResultBox As New Grid
            With grid_ResultBox
                .Background = Brushes.AliceBlue
            End With
            Dim tb_Result As New TextBlock
            With tb_Result
                .FontSize = 14
                .Text = tmpContent
                .TextWrapping = TextWrapping.Wrap
                .Margin = New Thickness(10)
            End With
            grid_ResultBox.Children.Add(tb_Result)

            sp_content.Children.Add(grid_ResultBox)
        Next

    End Sub

    Private Sub box_search_KeyDown(sender As Object, e As KeyEventArgs) Handles box_search.KeyDown
        If e.Key = Key.Enter Then
            e.Handled = True
            SearchButtonPressed()
        End If
    End Sub

    Public Sub SearchButtonPressed() Handles btn_search.Click

        SearchResultBuffer.Clear()
        Dim content As String = box_search.Text
        content = content.Trim
        If content.Length = 0 Then Return

        If content.Length <= 30 Then
            '短文本查词
            content = content.Replace(" ", "%20")
            For Each source As DictAnalyzer In Source_Eng
                Dim sub_search = Sub()
                                     Dim result As String = source.SearchWord(content)
                                     SearchResultBuffer.Add(result)
                                     Dispatcher.Invoke(AddressOf RefreshResultUI)    '是这样嘛？ HACK
                                 End Sub
                Dim tmpTask As New Task(sub_search)
                tmpTask.Start()
            Next

        Else
            '长文本翻译

        End If

    End Sub


End Class
