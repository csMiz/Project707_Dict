
''' <summary>
''' 词典在线源分析接口
''' </summary>
Public Interface DictAnalyzer

    ''' <summary>
    ''' 源名称
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property Id As String


    ''' <summary>
    ''' 搜索单词，空格需要用%20替换
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns></returns>
    Function SearchWord(input As String) As String



End Interface
