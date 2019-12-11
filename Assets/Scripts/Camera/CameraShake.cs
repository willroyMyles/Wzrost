using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update


    float magnitude = .4f;
    float duration = .08f;
    float initialDuration = .08f;
    bool shouldShake = false;
    Vector3 originalPosition;

    


    public void Shake()
    {
        if (shouldShake) return;
        originalPosition = Camera.allCameras[0].transform.localPosition;
        shouldShake = true;
    }

    private void Update()
    {
        if (shouldShake)
        {
            if(duration >= 0)
            {
                Camera.allCameras[0].transform.localPosition = originalPosition + Random.insideUnitSphere * magnitude;
                duration -= Time.deltaTime;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                Camera.allCameras[0].transform.localPosition = originalPosition;
            }
        }
    }
}
