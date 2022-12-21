using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originPosition = transform.localPosition;
        float countdown = 0f;
        while (countdown < duration)
        {
            float x = originPosition.x + Random.Range(-1, 1) * magnitude;
            float y = originPosition.y + Random.Range(-1, 1) * magnitude;
            float z = originPosition.z;
            transform.localPosition = new Vector3(x, y, z);
            countdown += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPosition;
    }
}
