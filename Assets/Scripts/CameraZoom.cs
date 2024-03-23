using System.Net;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float baseZoomSpeed = 10f; // 확대/축소 속도 조절 변수
    public float zoomSpeedMultiplier = 0.1f; // orthographicSize에 따라 zoomSpeed가 얼마나 변할지 결정하는 비율
    public float panSpeed = 1000f; // 팬닝 속도
    public float minOrthoSize = 5f; // 최소 카메라 orthographicSize
    public float maxOrthoSize = 100f; // 최대 카메라 orthographicSize

    private Camera cam;

    void Start()
    {
        cam = Camera.main; // 카메라 컴포넌트 가져오기
    }

    void Update()
    {
        // 마우스 휠 입력을 받아 확대/축소 구현
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll == 0.0f) return;

        float zoomSpeed = baseZoomSpeed + cam.orthographicSize * zoomSpeedMultiplier; // zoomSpeed 동적 계산
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scroll * zoomSpeed, minOrthoSize, maxOrthoSize);
        
        // 마우스의 현재 위치를 월드 좌표로 변환합니다.
        Vector3 worldMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // 카메라 확대/축소를 마우스 위치에 따라 팬닝을 추가합니다.
        Vector3 dir = (worldMousePos - cam.transform.position)*cam.orthographicSize;
        if (scroll > 0) // 확대
        {
            cam.transform.position += dir * panSpeed * Time.deltaTime;
        }
        else if (scroll < 0) // 축소
        {
            // 카메라가 최소나 최대 사이즈에 도달하면 더 이상 이동하지 않게 합니다.
            if (!(cam.orthographicSize <= minOrthoSize && scroll < 0) && !(cam.orthographicSize >= maxOrthoSize && scroll > 0))
            {
                cam.transform.position -= dir * panSpeed * Time.deltaTime;
            }
        }
    }
}
