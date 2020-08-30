using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControls : MonoBehaviour
{
    public Vector3 LocalForward = Vector3.up;    //Vector que indica la orientació de la nau
    public float VelocitatNau = 5f;     //La velocitat a la que pot avançar la nau
    public float VelocitatGir = 3f;     //La velocitat a la que la nau s'orientarà al cursor
    public GameObject SmokeEffect;

    Rigidbody2D Rb;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameObject.GetComponent<Rigidbody2D>())
        {
            Debug.LogError("Falta un component Rigidbody2D!");
        }
        else
        {
            Rb = gameObject.GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CursorPos.z = transform.position.z;     //Negligim la profunditat!

        Vector3 RelativeDir = (CursorPos - transform.position).normalized;      //Vector que indica cap a on es troba el cursor respecte la nau
        Quaternion Look = Quaternion.FromToRotation(transform.up, RelativeDir);     //El gir que hem de fer 

        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation*Look, Time.deltaTime*VelocitatGir);

        if (Input.GetButton("Fire1")) {
            Rb.AddForce(transform.up * VelocitatNau);
            SmokeEffect.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            SmokeEffect.GetComponent<ParticleSystem>().Stop();
        }
    }
}
