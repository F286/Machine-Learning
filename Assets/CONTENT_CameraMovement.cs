using UnityEngine;
using System.Collections;

public class CONTENT_CameraMovement : MonoBehaviour 
{
    public void Update()
    {
        transform.localPosition = new Vector3(
            Mathf.PerlinNoise(Time.time / 1.8f, 000) * 0.1f, 
            Mathf.PerlinNoise(Time.time / 1.8f, 100) * 0.1f, 
            Mathf.PerlinNoise(Time.time / 1.5f, 200) * 0.3f);
    }
}
