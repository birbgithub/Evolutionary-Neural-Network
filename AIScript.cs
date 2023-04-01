using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    public NeuralNetwork neuralNetwork;

    public float speed = 1;
    public float rotationSpeed = 20;

    public Transform target;

    void Start()
    {
        neuralNetwork.fitness = 0;
        target = GameObject.FindGameObjectWithTag("Target").transform;
    }

    void FixedUpdate()
    {
        float[] inputs = {Vector3.Angle(transform.up, transform.position - target.position)/360};
        List<float> outputs = neuralNetwork.FeedForward(inputs);

        if (outputs[0] >= 0)
        {
            transform.eulerAngles += new Vector3(0, 0, 1) * Time.deltaTime * rotationSpeed;
        }
        else
        {
            transform.eulerAngles += new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed;
        }

        transform.position += transform.up * Time.deltaTime * speed;

        neuralNetwork.fitness += 1/(Vector3.Distance(transform.position, target.position)); // The closer a boid is to the target, the greater fitness it recieves
    }
}
