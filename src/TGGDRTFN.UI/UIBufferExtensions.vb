Imports System.Runtime.CompilerServices
Imports TGGD.UI
Imports TGGDRTFN.Business

Friend Module UIBufferExtensions
    Public Const CharacterCount = 256
    <Extension>
    Friend Sub Fill(
                   buffer As IUIBuffer(Of Integer),
                   Optional character As Byte = 0,
                   Optional foregroundColor As Integer = 0,
                   Optional backgroundColor As Integer = 0)
        buffer.Fill(character + foregroundColor * CharacterCount + backgroundColor * HueCount * CharacterCount)
    End Sub
End Module
