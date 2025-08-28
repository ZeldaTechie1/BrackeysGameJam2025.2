using UnityEngine;

public class AimIndicator : MonoBehaviour
{
    public void SetIndicatorPosition(Vector3 position)
    {
        Vector3 newPosition = position;
        newPosition.y = 0;
        transform.position = newPosition;
    }

    public void SetIndicatorSize(float radius) 
    {
        transform.localScale = new Vector3(radius, 0.1f, radius);
    }
}

