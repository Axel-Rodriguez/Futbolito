using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class TeamR : Agent
{
    //Declaraci?n del componente Rigidbody para mover los palos
    Rigidbody rbody;

    //parametros torque del palo, incrementar en Unity si se requiere giros m?s r?pidos
    public float maxAngVel;

    //Pelota
    public Rigidbody ball;

    float posX = 0;

    // Start es llamado en el primer frame del juego
    void Start()
    {
        //conectamos el rbody con el componente rigidbody del palo
        rbody = GetComponent<Rigidbody>();
        //asignamos la velocidad del giro
        rbody.maxAngularVelocity = maxAngVel;
        posX = ball.position.x;
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
        rbody.AddForce(0, 0, v * 50f);
        rbody.AddTorque(transform.forward * h * 1000);
        float newPosX = ball.position.x;
        if (newPosX < posX)
        {
            AddReward(Mathf.Abs(newPosX-10) * 0.005f);
            posX = newPosX;
        }
        else
        {
            AddReward(Mathf.Abs(newPosX + 13) * -0.005f);
        }
        if (ball.position.y < 2.25f)
        {
            if (ball.position.x > 10)
            {
                AddReward(-1f);
                EndEpisode();
            }
            else if (ball.position.x < -13)
            {
                AddReward(1f);
                EndEpisode();
            }
        }
    }

    // Update es llamado durante cada frame del juego
    void Update()
    {

    }
}
