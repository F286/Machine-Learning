using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class CONTENT_GraphManager : MonoBehaviour 
{
    public float Target = 10;
    public float StepSize = 0.01f;
    public int Steps = 30;

    public List<ValueGate> Input;
    public Gate add;
    public Gate multiply;
    public Gate sigmoid;
//    public ValueGate Output;

    public List<Vector2> Red;
    public List<Vector2> Blue;

    public void Awake()
    {
        for (int i = 0; i < 40; i++)
        {
            Red.Add(Random.insideUnitCircle * 3);
        }
        for (int i = 0; i < 60; i++)
        {
            Blue.Add(Random.insideUnitCircle.normalized * Random.Range(5f, 8f));
        }

    }
    public void Update()
    {
        for (int i = 0; i < Steps; i++)
        {
            //forward
            add.Forward(Input[0], Input[1]);
            multiply.Forward(add, Input[2]);
            sigmoid.Forward(multiply, null);

            //backward
            sigmoid.gradient = 1;
            multiply.gradient = 0;
            add.gradient = 0;
            Input[0].gradient = 0;
            Input[1].gradient = 0;
            Input[2].gradient = 0;
            sigmoid.Backward();
            multiply.Backward();
            add.Backward();

            var force = Mathf.Sign(Target - sigmoid.value);
            Input[0].value += force * Input[0].gradient * StepSize;
            Input[1].value += force * Input[1].gradient * StepSize;
            Input[2].value += force * Input[2].gradient * StepSize;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var item in Red)
        {
            Gizmos.DrawSphere(item, 0.25f);
        }
        Gizmos.color = Color.blue;
        foreach (var item in Blue)
        {
            Gizmos.DrawSphere(item, 0.25f);
        }
    }
}
