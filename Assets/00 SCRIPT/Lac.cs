using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lac : MonoBehaviour
{
    [Header("Settings")]
    public float lac;
    public float smoothing;

    private Vector3 origin;
    void Start()
    {
        origin = transform.localPosition;
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        input.x = Mathf.Clamp(input.x, -lac, lac);
        input.y = Mathf.Clamp(input.y, -lac, lac);

        Vector3 targetPosition = new Vector3(-input.x, -input.y, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + origin, smoothing * Time.deltaTime);

    }
}
