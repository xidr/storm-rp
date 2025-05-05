using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField]
    private Transform customPivot;
    
    [SerializeField]
    private float rotationSpeed = 40f;

    [SerializeField] private bool ShouldRotate;
    private Vector3 getCustomPivot => customPivot.position;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
// transform.localRotation = Quaternion.Euler(0, transform.localRotation.y + 1 * Time.deltaTime, 0);
// transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime*20, Space.World);
        if (!ShouldRotate)
            return;
        
        transform.RotateAround(getCustomPivot, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}