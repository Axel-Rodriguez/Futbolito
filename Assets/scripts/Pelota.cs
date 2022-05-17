using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelota : MonoBehaviour
{
    Rigidbody rbody;
    Vector3 vel = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVel = rbody.velocity;
        if(newVel == vel)
        {
            rbody.transform.rotation = Quaternion.identity;
            rbody.AddForce(Random.Range(-10,10), 1, Random.Range(-10, 10));
        }
    }
}
