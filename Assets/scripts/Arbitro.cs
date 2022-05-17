using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbitro : MonoBehaviour
{
    int redGoals = 0;
    int blueGoals = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Random.value > 0.5f) ballBlue();
        else ballRed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider ball)
    {
        if (ball.gameObject.tag == "Pelota")
        {
            ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.gameObject.GetComponent<SphereCollider>().enabled = false;
            Vector3 collisionPosition = ball.gameObject.GetComponent<Rigidbody>().position;
            if (collisionPosition[0] > 10f)
            {
                blueGoals++;
                Debug.Log("Blue team scores. Score: red " + redGoals + " - " + blueGoals + " blue");
                ballRed();
                if (blueGoals >= 5)
                {
                    Debug.Log("Blue team wins!");
                    //Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
            else if (collisionPosition[0] < -13f)
            {
                redGoals++;
                Debug.Log("Red team scores. Score: red " + redGoals + " - " + blueGoals +" blue");
                ballBlue();
                if(redGoals >= 5)
                {
                    Debug.Log("Red team wins!");
                    //Application.Quit();
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
            ball.gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

     void ballRed()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Pelota");
        if (ball.gameObject.tag == "Pelota")
        {
            ball.gameObject.GetComponent<Rigidbody>().transform.localPosition = new Vector3(1.92f, 5.0f, 3.7f);
        }
    }
    void ballBlue()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Pelota");
        if (ball.gameObject.tag == "Pelota")
        {
            ball.gameObject.GetComponent<Rigidbody>().transform.localPosition = new Vector3(-1f, 5.0f, 3.7f);
        }
    }
}
