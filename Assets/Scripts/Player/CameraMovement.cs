using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float Smoothing;
    [SerializeField] private Vector3 diff;
    private GameObject camera;
    private Transform cameraT;
    private void Awake()
    {
        camera = GameObject.FindObjectOfType<Camera>().gameObject;
        cameraT = camera.transform;
        Debug.Log("kamera bulundu");
    }
    private void Update()
    {
        cameraT.position = Vector3.Lerp(cameraT.position,
            transform.position + diff,
            Smoothing * Time.deltaTime);
    }
}
