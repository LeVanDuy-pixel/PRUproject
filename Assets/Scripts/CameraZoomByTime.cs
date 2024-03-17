using UnityEngine;

public class CameraZoomByTime : MonoBehaviour
{
    public float zoomInTime = 0.1f; // Thời gian phóng to
    public float zoomOutTime = 0.1f; // Thời gian thu nhỏ
    public float minZoom = 1f; // Tỉ lệ thu nhỏ nhỏ nhất (đơn vị màn hình)
    public float maxZoom = 2f; // Tỉ lệ phóng to lớn nhất (đơn vị màn hình)
    public float zoomSpeed = 30f; // Tốc độ zoom
    public bool isBeat = false;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (isBeat)
        {
            float currentTime = Time.time % (zoomOutTime * 2); // Lấy currentTime modulo của (zoomOutTime * 2)

            // Tính toán mức độ zoom dựa trên thời gian
            float zoomLevel = 0f;
            if (currentTime < zoomInTime)
            {
                float normalizedTime = currentTime / zoomInTime; // Chuẩn hóa thời gian trong khoảng từ 0 đến 1
                zoomLevel = Mathf.Lerp(minZoom, maxZoom, Mathf.Pow(normalizedTime, zoomSpeed)); // Tăng tốc độ zoom
            }
            else if (currentTime > zoomOutTime)
            {
                float normalizedTime = (currentTime - zoomOutTime) / zoomOutTime; // Chuẩn hóa thời gian trong khoảng từ 0 đến 1
                zoomLevel = Mathf.Lerp(maxZoom, minZoom, Mathf.Pow(normalizedTime, zoomSpeed)); // Tăng tốc độ zoom
            }
            else
            {
                zoomLevel = maxZoom; // Giữ mức độ zoom là maxZoom
            }

            // Áp dụng mức độ zoom vào màn hình game
            mainCamera.orthographicSize = zoomLevel;
        }
    }
    public void beatIt(float min, float max, float time)
    {
        minZoom = min;
        maxZoom = max;
        isBeat = true;
        Invoke("beatOff", time);
    }
    private void beatOff()
    {
        isBeat = false;
    }
}
