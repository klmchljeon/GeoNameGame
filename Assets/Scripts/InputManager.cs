using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public TMP_InputField inputField;
    private Transform hitTransform;

    // Start is called before the first frame update
    void Start()
    {
        inputField.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0은 마우스 왼쪽 버튼 또는 터치 스크린 터치를 의미
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log(1);
            // 레이캐스트가 어떤 콜라이더에 닿았는지 확인
            if (hit.collider != null)
            {
                Debug.Log(2);
                hitTransform = hit.collider.transform;

                ActivateInputField();

            }
        }
    }
    void ActivateInputField()
    {
        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField();

        // 이벤트 리스너 추가
        inputField.onEndEdit.RemoveAllListeners(); // 먼저 이전 리스너를 모두 제거
        inputField.onEndEdit.AddListener(SetCollider);
        inputField.onEndEdit.AddListener(SetRenderer);
        inputField.onEndEdit.AddListener(ScoreCheck);
        inputField.onEndEdit.AddListener(DeactivateInputField);
    }

    private void SetCollider(string input)
    {
        PolygonCollider2D collider = hitTransform.GetComponent<PolygonCollider2D>();
        collider.enabled = false;
    }
    private void SetRenderer(string input)
    {
        LineRenderer renderer = hitTransform.GetComponent<LineRenderer>();
        renderer.material.color = Color.gray;
        renderer.startWidth = 1f;
        renderer.endWidth = 1f;
    }
    private void ScoreCheck(string input)
    {
        Debug.Log(input);
        Debug.Log(hitTransform.name);
        Score.cnt++;
        if (hitTransform.name == input)
        {
            Score.score++;
            Debug.Log("맞았습니다!");
        }
        else
        {
            Debug.Log("틀렸습니다!");
        }
    }
    private void DeactivateInputField(string input)
    {
        // 입력 필드를 비활성화합니다.
        inputField.gameObject.SetActive(false);
    }
}
