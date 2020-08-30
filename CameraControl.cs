using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject Ship;
    public float DistanciaCamara;
    public float VelocitatCamara;

    // Start is called before the first frame update
    void Start()
    {
        if (!Ship)
            Debug.LogError("No hi ha cap nau definida!");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 Offset = Ship.transform.up * DistanciaCamara;
        Vector3 Target = Ship.transform.position;
        Target += Offset;
        Target.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, Target, Time.deltaTime * VelocitatCamara);

    }
}
