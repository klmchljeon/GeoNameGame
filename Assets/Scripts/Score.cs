using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cnt == 25)
        {
            Debug.Log("끝났습니다.");
            Debug.Log($"점수 : {score}");
            cnt++;
        }
    }
}
