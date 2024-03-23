using System;
using System.Linq;
using System.Collections.Generic;

public class RandomSelector
{
    public static List<int> PickNumbers(int n, int m)
    {
        Random rng = new Random(); // ���� ��ü ����
        List<int> numbers = Enumerable.Range(0, n).ToList(); // 0���� n-1������ ���� ����Ʈ ����

        // numbers ����Ʈ�� �����ϰ� �����ϰ�, �տ������� m���� ���ڸ� ����
        List<int> selectedNumbers = numbers.OrderBy(x => rng.Next()).Take(m).ToList();

        return selectedNumbers;
    }
}