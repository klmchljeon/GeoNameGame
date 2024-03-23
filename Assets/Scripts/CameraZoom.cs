using System.Net;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float baseZoomSpeed = 10f; // Ȯ��/��� �ӵ� ���� ����
    public float zoomSpeedMultiplier = 0.1f; // orthographicSize�� ���� zoomSpeed�� �󸶳� ������ �����ϴ� ����
    public float panSpeed = 1000f; // �Ҵ� �ӵ�
    public float minOrthoSize = 5f; // �ּ� ī�޶� orthographicSize
    public float maxOrthoSize = 100f; // �ִ� ī�޶� orthographicSize

    private Camera cam;

    void Start()
    {
        cam = Camera.main; // ī�޶� ������Ʈ ��������
    }

    void Update()
    {
        // ���콺 �� �Է��� �޾� Ȯ��/��� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0.0f) return;

        float zoomSpeed = baseZoomSpeed + cam.orthographicSize * zoomSpeedMultiplier; // zoomSpeed ���� ���
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minOrthoSize, maxOrthoSize);
        
        // ���콺�� ���� ��ġ�� ���� ��ǥ�� ��ȯ�մϴ�.
        Vector3 worldMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // ī�޶� Ȯ��/��Ҹ� ���콺 ��ġ�� ���� �Ҵ��� �߰��մϴ�.
        Vector3 dir = (worldMousePos - cam.transform.position)*cam.orthographicSize;
        if (scroll > 0) // Ȯ��
        {
            cam.transform.position += dir * panSpeed * Time.deltaTime;
        }
        else if (scroll < 0) // ���
        {
            // ī�޶� �ּҳ� �ִ� ����� �����ϸ� �� �̻� �̵����� �ʰ� �մϴ�.
            if (!(cam.orthographicSize <= minOrthoSize && scroll < 0) && !(cam.orthographicSize >= maxOrthoSize && scroll > 0))
            {
                cam.transform.position -= dir * panSpeed * Time.deltaTime;
            }
        }
    }
}
