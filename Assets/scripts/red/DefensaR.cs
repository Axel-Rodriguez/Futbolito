using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class DefensaR : Agent
{
    //Declaraci?n del componente Rigidbody para mover los palos
    Rigidbody rbody;

    //parametros torque del palo, incrementar en Unity si se requiere giros m?s r?pidos
    public float maxAngVel;

    //Pelota
    public Rigidbody ball;

    float posX = 0, velX = 0;

    // Start es llamado en el primer frame del juego
    void Start()
    {
        //conectamos el rbody con el componente rigidbody del palo
        rbody = GetComponent<Rigidbody>();
        //asignamos la velocidad del giro
        rbody.maxAngularVelocity = maxAngVel;
        posX = ball.position.x;
        velX = ball.velocity.x;
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
        rbody.AddForce(0, 0, v * 50f);
        rbody.AddTorque(transform.forward * h * 1500);
        float newPosX = ball.position.x;
        float newVelX = ball.velocity.x;
        if(newPosX > 7.5f)
        {
            if (newPosX > 11)
            {
                AddReward(-0.25f);
                EndEpisode();
            }
            else if (newPosX < posX)
            {
                AddReward(1f);
                posX = newPosX;
            }
            else
            {
                AddReward(-0.005f);
            }
        }
        else
        {
            EndEpisode();
        }
        if (ball.position.y < 2.25f)
        {
            if (ball.position.x > 10)
            {
                Debug.Log("GOL");
                AddReward(-3f);
                EndEpisode();
            }
            else if (ball.position.x < -13)
            {
                AddReward(2f);
                EndEpisode();
            }
        }
    }
}
