using UnityEngine;

public class SafeAreaHelper : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;
    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform= GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        if (_rectTransform == null)
            return;

        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= _parentCanvas.pixelRect.width;
        anchorMin.y /= _parentCanvas.pixelRect.height;
        anchorMax.x /= _parentCanvas.pixelRect.width;
        anchorMax.y /= _parentCanvas.pixelRect.height;

        _rectTransform.anchorMin = anchorMin;
        _rectTransform.anchorMax = anchorMax;
    }

}