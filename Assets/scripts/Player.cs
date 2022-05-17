using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Player : Agent
{
    //Declaraci�n del componente Rigidbody para mover los palos
    Rigidbody rbody;

    //parametros torque del palo, incrementar en Unity si se requiere giros m�s r�pidos
    public float maxAngVel;

    //Pelota
    public Rigidbody ball;

    // Start es llamado en el primer frame del juego
    void Start()
    {
        //conectamos el rbody con el componente rigidbody del palo
        rbody = GetComponent<Rigidbody>();
        //asignamos la velocidad del giro
        rbody.maxAngularVelocity = maxAngVel;
    }

    public override void OnEpisodeBegin()
    {
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.transform.localPosition);
        sensor.AddObservation(ball.velocity);
    }

    public override void Heuristic(in ActionBuffers accionesSalida)
    {
        var acciones = accionesSalida.ContinuousActions;
        acciones[0] = Input.GetAxis("Horizontal");
        acciones[1] = Input.GetAxis("Vertical");
    }

    public override void OnActionReceived(ActionBuffers accion)
    {
        float h = accion.ContinuousActions[0] * 10000f * Time.deltaTime;
        float v = accion.ContinuousActions[1];
        //Debug.Log(rbody.position);
        if (rbody.position.z < 4.85f && rbody.position.z > 2.4f)
        {
            rbody.AddForce(0, 0, v * 50f);
        }
        rbody.AddTorque(transform.forward * h * 1000);
        if (rbody.position.z >= 4.85f)
        {
            rbody.transform.position = new Vector3(rbody.position.x, rbody.position.y, 4.8f);
        }
        if (rbody.position.z <= 2.4f)
        {
            rbody.transform.position = new Vector3(rbody.position.x, rbody.position.y, 2.45f);
        }
        if (ball.position.x > 3.45f)
        {
            AddReward(-0.2f);
        }
        if (ball.position.y < 2.25f)
        {
            if (ball.position.x > 13)
            {
                SetReward(0.8f);
                EndEpisode();
            }
            else if (ball.position.x < -12)
            {
                SetReward(-0.8f);
                EndEpisode();
            }
        }
    }

    // Update es llamado durante cada frame del juego
    void Update()
    {
        float h = Input.GetAxis("Horizontal") * 10000f * Time.deltaTime;
        float v = Input.GetAxis("Vertical");
        //Debug.Log(rbody.position);
        if (rbody.position.z < 4.85f && rbody.position.z > 2.4f)
        {
            rbody.AddForce(0, 0, v * 50f);
        }
        rbody.AddTorque(transform.forward * h * 1000);
        if (rbody.position.z >= 4.85f)
        {
            rbody.transform.position = new Vector3(rbody.position.x, rbody.position.y, 4.8f);
        }
        if (rbody.position.z <= 2.4f)
        {
            rbody.transform.position = new Vector3(rbody.position.x, rbody.position.y, 2.45f);
        }
    }
}
