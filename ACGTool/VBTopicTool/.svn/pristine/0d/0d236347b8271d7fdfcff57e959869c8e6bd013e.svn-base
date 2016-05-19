Public Class ConvertNumberToString
    Public Function docchu(ByVal so1 As Integer) As String

        Dim tram As Integer, chuc As Integer, dv As Integer, so As Integer
        Dim chu(0 To 9) As String, chuoi1, chuoi2, chuoi
        so = so1
        chu(0) = "không"
        chu(1) = "một"
        chu(2) = "hai"
        chu(3) = "ba"
        chu(4) = "bốn"
        chu(5) = "năm"
        chu(6) = "sáu"
        chu(7) = "bảy"
        chu(8) = "tám"
        chu(9) = "chín"
        tram = so \ 100
        so = so Mod 100
        chuc = so \ 10
        dv = so Mod 10
        Select Case chuc
            Case 0
                chuoi2 = IIf(tram = 0, "", IIf(dv = 0, "", "lẻ"))
            Case 1
                chuoi2 = "mười"
            Case Else
                chuoi2 = chu(chuc) + " mươi"
        End Select
        Select Case tram
            Case 0
                chuoi1 = ""
            Case Else
                chuoi1 = chu(tram) + " trăm "
        End Select
        Select Case dv
            Case 0
                chuoi = ""
            Case 1
                chuoi = IIf((chuoi2 = "") Or (chuoi2 = "mười") Or (chuoi2 = "lẻ"), "một", "mốt")
            Case 5
                chuoi = IIf((chuoi2 = "lẻ") Or (chuoi2 = ""), "năm", "lăm")
            Case Else
                chuoi = chu(dv)
        End Select

        docchu = Trim(chuoi1 + IIf(chuoi2 <> "", chuoi2, "") + " " + chuoi)
    End Function

    Public Function doc(ByVal so1 As Double) As String
        Dim chuoi1, chuoi2, chuoi3, chuoi4 As String
        chuoi1 = ""
        If so1 = 0 Then
            doc = "không"
            Exit Function
        End If
        Dim so As Double
        so = so1
        Dim ty As Integer, trieu As Integer, ngan As Integer, dv As Integer, rr As Double
        ty = Int(so / 1000000000)
        rr = ty
        so = so - CDbl(1000000000) * CDbl(ty)
        trieu = so \ 1000000
        so = so Mod 1000000
        ngan = so \ 1000
        dv = so Mod 1000
        If ty > 1000 Then
            chuoi1 = doc(rr) + " tỷ"
        ElseIf ty > 0 Then
            chuoi1 = docchu(ty) + " tỷ"
        End If
        chuoi2 = IIf(trieu > 0, docchu(trieu) + " triệu", "")
        chuoi3 = IIf(ngan > 0, docchu(ngan) + " ngàn", "")
        chuoi4 = IIf(dv > 0, docchu(dv), "")
        doc = Trim(chuoi1 + IIf(chuoi2 <> "", " " + chuoi2, "") + IIf(chuoi3 <> "", " " + chuoi3, "") + IIf(chuoi4 <> "", " " + chuoi4, ""))
    End Function

    Public Function DoiSoRaChu(ByVal so As Double)
        Dim so1 As Double, le As String, chuoi As String
        chuoi = Format(so, "000000000000000000.00")
        so1 = Val(Left(chuoi, Len(chuoi) - 3))
        le = Right(chuoi, 2)
        If Val(le) > 0 Then
            If Val(le) < 10 Then
                chuoi = doc(so1) + " phẩy không " + doc(Val(le))
            Else
                chuoi = doc(so1) + " phẩy " + doc(Val(le))
            End If
        Else
            chuoi = doc(so1)
        End If
        If (chuoi.Length <> 0) Then
            Dim c As String
            c = chuoi.Substring(0, 1)
            chuoi = c.ToUpper() + chuoi.Substring(1)
        End If
        DoiSoRaChu = chuoi + " đồng"
    End Function
End Class
