using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class CONTENT_GraphManager : MonoBehaviour 
{
    public float target = 0.5f;
    public float stepSize = 0.001f;
    public int steps = 30;

    public List<Gate> input;
//    public List<ValueGate> Output;
    public List<Gate> all;
    public List<Gate> variables;
    public Gate output;
//    public Gate add;
//    public Gate multiply;
//    public Gate sigmoid;
//    public ValueGate Output;
//    public Gate[] all;

    public List<Vector2> red;
    public List<Vector2> blue;

    public void Awake()
    {
        for (int i = 0; i < 15; i++)
//        for (int i = 0; i < 40; i++)
        {
            red.Add(Random.insideUnitCircle * 2);
        }
        for (int i = 0; i < 15; i++)
//        for (int i = 0; i < 60; i++)
        {
//            blue.Add(Random.insideUnitCircle.normalized * Random.Range(5f, 8f));
            blue.Add(Random.insideUnitCircle * 2 + new Vector2(5, 0));
        }

    }
    public void Update()
    {
        for (int s = 0; s < steps; s++)
        {
            Evaluate(new Vector2(-2, 5));
            Train(target);
//            //reset
//            for (int i = 0; i < all.Count; i++) 
//            {
//                all[i].gradient = 0;
//            }
//            output.gradient = 1;
////            for (int i = 0; i < Output.Count; i++) 
////            {
////                Output[i].gradient = 1;
////            }
//
//            //forward
//            for (int index = 0; index < all.Count; index++) 
//            {
//                all[index].Forward(all[index].input);
//            }
////            //forward
////            add.Forward(Input[0], Input[1]);
////            multiply.Forward(add, Input[2]);
////            sigmoid.Forward(multiply, null);
//
//            for (int index = all.Count - 1; index >= 0; index--) 
//            {
//                all[index].Backward();
//            }

//            //backward
//            sigmoid.gradient = 1;
//            multiply.gradient = 0;
//            add.gradient = 0;
//            Input[0].gradient = 0;
//            Input[1].gradient = 0;
//            Input[2].gradient = 0;
//            sigmoid.Backward();
//            multiply.Backward();
//            add.Backward();

//            var force = Mathf.Sign(Target - sigmoid.value);
//            Input[0].value += force * Input[0].gradient * StepSize;
//            Input[1].value += force * Input[1].gradient * StepSize;
//            Input[2].value += force * Input[2].gradient * StepSize;
        }
    }
    public float Evaluate(Vector2 v)
    {
        input[0].value = v.x;
        input[1].value = v.y;
        //reset
        for (int i = 0; i < all.Count; i++) 
        {
            all[i].gradient = 0;
        }
        output.gradient = 1;

        //forward
        for (int index = 0; index < all.Count; index++) 
        {
            all[index].Forward(all[index].input);
        }
        for (int index = all.Count - 1; index >= 0; index--) 
        {
            all[index].Backward();
        }
        return output.value;
    }
    public void Train(float value)
    {
        var force = Mathf.Sign(value - output.value);
        for (int i = 0; i < variables.Count; i++)
        {
            variables[i].value += force * variables[i].gradient * stepSize;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var item in red)
        {
            Gizmos.DrawSphere(item, 0.25f);
        }
        Gizmos.color = Color.blue;
        foreach (var item in blue)
        {
            Gizmos.DrawSphere(item, 0.25f);
        }
    }
}
