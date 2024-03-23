using System;
using System.Linq;
using System.Collections.Generic;

public class RandomSelector
{
    public static List<int> PickNumbers(int n, int m)
    {
        Random rng = new Random(); // 랜덤 객체 생성
        List<int> numbers = Enumerable.Range(0, n).ToList(); // 0부터 n-1까지의 숫자 리스트 생성

        // numbers 리스트를 랜덤하게 정렬하고, 앞에서부터 m개의 숫자를 선택
        List<int> selectedNumbers = numbers.OrderBy(x => rng.Next()).Take(m).ToList();

        return selectedNumbers;
    }
}