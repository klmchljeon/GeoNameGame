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
        if (Input.GetMouseButtonDown(0)) // 0�� ���콺 ���� ��ư �Ǵ� ��ġ ��ũ�� ��ġ�� �ǹ�
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.Log(1);
            // ����ĳ��Ʈ�� � �ݶ��̴��� ��Ҵ��� Ȯ��
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

        // �̺�Ʈ ������ �߰�
        inputField.onEndEdit.RemoveAllListeners(); // ���� ���� �����ʸ� ��� ����
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
            Debug.Log("�¾ҽ��ϴ�!");
        }
        else
        {
            Debug.Log("Ʋ�Ƚ��ϴ�!");
        }
    }
    private void DeactivateInputField(string input)
    {
        // �Է� �ʵ带 ��Ȱ��ȭ�մϴ�.
        inputField.gameObject.SetActive(false);
    }
}
