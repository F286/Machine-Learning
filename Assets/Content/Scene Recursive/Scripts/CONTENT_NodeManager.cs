using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CONTENT_NodeManager : MonoBehaviour
{
    public const int TotalFrames = 2;
    [System.Serializable]
    public class Frame
    {
        public double[] value;
        public double[] derivative;

        public Frame(int length)
        {
            value = new double[length];
            derivative = new double[length];
        }
    }
    public List<CONTENT_Node> nodes;
    public Frame[] frames;
    public List<CONTENT_Equation> equations;
    public Gradient gradient;

    public void Start()
    {
        AddEquations(GameObject.FindWithTag("output").GetComponent<CONTENT_Node>());

        frames = new Frame[TotalFrames];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame(nodes.Count);
            for (int n = 0; n < nodes.Count; n++) 
            {
                frames[i].value[n] = nodes[n].value;
            }
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            var item = nodes[i];
            item.gameObject.AddComponent<CONTENT_NodeVisualize>();
        }
    }

    CONTENT_Equation AddEquations(CONTENT_Node node, int currentFrame = 0, List<CONTENT_Node> currentTree = null)
    {
        if ((node.addedBitmask & (1U << currentFrame)) != 0)
        {
            return null;
        }
        node.addedBitmask |= 1U << currentFrame;
        if (currentTree == null)
        {
            currentTree = new List<CONTENT_Node>();
        }
        currentTree.Add(node);
        List<DataPointer> _input = new List<DataPointer>();

        print("--- " + node);

        for (int i = 0; i < node.input.Count; i++)
        {
            var item = node.input[i];
            if (currentTree.Contains(item) && currentFrame + 1 < TotalFrames)
            {
                var e = AddEquations(item, currentFrame + 1, null);
                if (e)
                {
                    _input.Add(e.Val);
                }
            }
            else
            {
                var e = AddEquations(item, currentFrame, currentTree);
                if (e)
                {
                    _input.Add(e.Val);
                }
            }
        }
        if (!nodes.Contains(node))
        {
            node.current = new DataPointer(0, nodes.Count);
            nodes.Add(node);
        }
        {
            var g = new GameObject(node.name + " (" + node.type.ToString() + ")  f " + currentFrame + "  n " + node.current.node);
            print(g.name);
            print(_input.Count);
            var e = g.AddComponent<CONTENT_Equation>();
            e.type = node.type;
            e.Val = new DataPointer(currentFrame, node.current.node);
            e.In = _input.ToArray();
            equations.Add(e);
            node.equations.Add(e);
            return e;
        }
    }
//    bool hasUpdated = false;
    public void Update()
    {
//        if (!hasUpdated)
//        {
//            hasUpdated = true;

            for (int f = 0; f < frames.Length; f++)
            {
                for (int n = 0; n < nodes.Count; n++)
                {
                    frames[f].derivative[n] = 0;
                    if (nodes[n].type == CONTENT_Node.Type.Input)
                    {
//                        print(f + " : " + n + " : " + nodes[n].value);
                        frames[f].value[n] = nodes[n].value;
//                    print(frames[f].value[n]);
//                        frames[f].value[n] = 0.5f;
//                        //frames[f].value[n] = 
                    }
                }
            }
        GameObject.FindWithTag("output").GetComponent<CONTENT_Node>().current.derivative = 1;
//            for (int i = 0; i < inputs.Count; i++)
//            {
//                frames[0].value[inputs[i].index] = inputs[i].value;
////                frames[TotalFrames - 3].value[inputs[i].index] = inputs[i].value / 2f;
////                frames[TotalFrames - 4].value[inputs[i].index] = inputs[i].value / -2f;
//            }
            for (int i = 0; i < equations.Count; i++)
            {
                var e = equations[i];
                e.forward();//ref frames[e.valueP.frame].value[e.valueP.node]);
//                e.forward(ref frames[e.age].value[e.);
            }
            for (int i = equations.Count - 1; i >= 0; i--)
            {
                equations[i].backward();
            }
//        }
    }

    static CONTENT_NodeManager _inst;
    public static CONTENT_NodeManager instance
    {
        get
        {
            if(_inst == null)
            {
                _inst = GameObject.FindObjectOfType<CONTENT_NodeManager>();
            }
            return _inst;
        }
    }
}
