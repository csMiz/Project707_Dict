Imports System.IO
Imports System.Net
Imports System.Text.RegularExpressions
Imports MizDict

Public Class YoudaoAnalyzer
    Implements DictAnalyzer

    Public ReadOnly Property Id As String Implements DictAnalyzer.Id
        Get
            Return "有道"
        End Get
    End Property

    Public Function SearchWord(input As String) As String Implements DictAnalyzer.SearchWord

        Dim root_content As String = vbNullString
        Dim root_request As HttpWebRequest = WebRequest.Create("http://youdao.com/w/eng/" & input & "/#keyfrom=dict2.top")
        root_request.Method = "GET"
        root_request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3907.0 Safari/537.36 Edg/79.0.279.0"
        Try
            Dim root_response As HttpWebResponse = root_request.GetResponse()
            Dim root_response_stream As Stream = root_response.GetResponseStream()
            Using sr As New StreamReader(root_response_stream)
                root_content = sr.ReadToEnd
            End Using
        Catch ex As Exception
            Return ""
        End Try

        Dim result As String = ""
        '匹配第一次出现
        Dim mat_range As Match = Regex.Match(root_content, "class=""trans-container""(.[\s\S]*?)</div>")
        If mat_range.Success Then
            Dim range As String = mat_range.Groups(1).Value

            '释义
            'Dim mat_basic As Match = Regex.Match(range, "<ul>(.[\s\S]*?)</ul>")
            'If mat_basic.Success Then
            'Dim mat_basic2 As MatchCollection = Regex.Matches(mat_basic.Groups(1).Value, "<li>(.[\s\S]*?)</li>")
            Dim mat_basic2 As MatchCollection = Regex.Matches(range, "<li>(.[\s\S]*?)</li>")
            If mat_basic2.Count > 0 Then
                For i = 0 To mat_basic2.Count - 1
                    If i <> 0 Then
                        result &= vbCrLf
                    End If
                    result = result & mat_basic2(i).Groups(1).Value
                Next
            End If
            'End If

            '附加（不一定有）
            'TODO

        End If
        Return result

    End Function


End Class
