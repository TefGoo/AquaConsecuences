using UnityEngine;
using UnityEngine.UI;

public class MoveUIImage : MonoBehaviour
{
    public RectTransform uiImage;
    public float startX;
    public float endX;
    public float speed = 1f;

    private bool movingToEnd = true;

    void Update()
    {
        Vector3 position = uiImage.localPosition;
        if (movingToEnd)
        {
            position.x = Mathf.MoveTowards(position.x, endX, speed * Time.deltaTime);
            if (position.x == endX) movingToEnd = false;
        }
        else
        {
            position.x = Mathf.MoveTowards(position.x, startX, speed * Time.deltaTime);
            if (position.x == startX) movingToEnd = true;
        }
        uiImage.localPosition = position;
    }
}
