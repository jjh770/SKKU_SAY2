using System.Numerics;
using UnityEngine;

public class BigNumber : MonoBehaviour
{
    private static readonly string[] BaseUnits = {
        "", "K", "M", "B", "T"
    };

    public BigInteger Value;

    private void Start()
    {
        BigNumber gold = new BigNumber(BigInteger.Parse("1000000000000000000000000000"));
        Debug.Log(gold.ToFormatString()); // 예시 출력: "100ab"
        BigNumber ultra = new BigNumber(BigInteger.Pow(10, 96));
        Debug.Log(ultra.ToFormatString()); // 예시 출력: "1.0aaaa"
    }

    public BigNumber(BigInteger value)
    {
        Value = value;
    }

    // 자동 확장 단위 이름 생성 함수
    private string GetUnitName(int unitIndex)
    {
        if (unitIndex < BaseUnits.Length)
            return BaseUnits[unitIndex];

        unitIndex -= BaseUnits.Length;

        int alphabetLen = 26;
        string result = "";
        do
        {
            result = (char)('a' + (unitIndex % alphabetLen)) + result;
            unitIndex /= alphabetLen;
        }
        while (unitIndex > 0);

        return result;
    }

    public string ToFormatString()
    {
        if (Value < 1000) return Value.ToString();

        int unitIndex = 0;
        BigInteger temp = Value;

        while (temp >= 1000)
        {
            temp /= 1000;
            unitIndex++;
        }

        // 소수 첫째자리 추출
        BigInteger remainder = Value % BigInteger.Pow(1000, unitIndex);
        int decimalPart = (int)(remainder / BigInteger.Pow(1000, unitIndex - 1) / 100);

        return $"{temp}.{decimalPart}{GetUnitName(unitIndex)}";
    }
}
