using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class CONTENT_NodeManager : MonoBehaviour
{
    public const int TotalFrames = 10;
//    public const int TotalFrames = 18;
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
        Random.InitState(0);

        var o = GameObject.FindWithTag("output");
//        print(o.GetComponent<CONTENT_Node>());

        Assert.IsTrue(o != null, "Node with tag 'output' must be set.");
        Assert.IsTrue(o.GetComponent<CONTENT_Node>() != null, "Node with tag 'output' must have a CONTENT_Node attached.");
        AddEquations(o.GetComponent<CONTENT_Node>());

        frames = new Frame[TotalFrames];
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = new Frame(nodes.Count);
            for (int n = 0; n < nodes.Count; n++)
            {
                frames[i].value[n] = nodes[n].value;
                frames[i].value[n] = Random.Range(-3f, 3f);
//                frames[i].value[n] = Random.Range(-0.5f, 0.5f);
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

//        print("--- " + node);

        for (int i = 0; i < node.input.Count; i++)
        {
            var item = node.input[i];
            if (currentTree.Contains(item))
            {
                if (currentFrame + 1 < TotalFrames)
                {
                    AddEquations(item, currentFrame + 1, null);
                    _input.Add(new DataPointer(currentFrame + 1, item.current.node));
//                    if (e)
//                    {
//                        _input.Add(e.Val);
//                    }
                }
            }
            else
            {
                AddEquations(item, currentFrame, currentTree);
                _input.Add(new DataPointer(currentFrame, item.current.node));
//                if (e)
//                {
//                    _input.Add(e.Val);
//                }
            }
        }
        if (!nodes.Contains(node))
        {
            node.current = new DataPointer(0, nodes.Count);
            nodes.Add(node);
        }
        {
            var g = new GameObject(node.name + " (" + node.type.ToString() + ")  f " + currentFrame + "  n " + node.current.node + "  i " + _input.Count);
//            print(g.name);
//            print(_input.Count);
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
        var o = GameObject.FindWithTag("output").GetComponent<CONTENT_Node>();

        for (int f = 0; f < frames.Length; f++)
        {
            for (int n = 0; n < nodes.Count; n++)
            {
                frames[f].derivative[n] = 0;
//                if (nodes[n].type == CONTENT_Node.Type.Input)
//                {
////                        print(f + " : " + n + " : " + nodes[n].value);
//                    frames[f].value[n] = nodes[n].value;
////                    print(frames[f].value[n]);
////                        frames[f].value[n] = 0.5f;
////                        //frames[f].value[n] = 
//                }
            }
        }
        for (int n = 0; n < nodes.Count; n++)
        {
            if (nodes[n].type == CONTENT_Node.Type.Input)
            {
                for (int f = 0; f < TotalFrames; f++)
                {
                    frames[f].value[n] = nodes[n].value;
                }
                for (int s = 0; s < nodes[n].setInput.Length; s++)
                {
                    var f = TotalFrames - 1 - nodes[n].setInput[s].frame;
                    f = Mathf.Clamp(f, 0, TotalFrames - 1);
                    frames[f].value[n] = nodes[n].setInput[s].value;
                }
            }
        }
        o.current.derivative = 1;
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
//        var error = o.value - System.Math.Sin(Mathf.PI * 2 * (Time.frameCount / 30.0));
//        error = -error;
//        print(error);
//        for (int i = 0; i < nodes.Count; i++)
//        {
//            if (nodes[i].type == CONTENT_Node.Type.Value)
//            {
//                for (int j = 0; j < nodes[i].derivative.Length; j++) 
//                {
//                    nodes[i].current.value += nodes[i].derivative[j] * error * 0.000001;
////                    nodes[i].value += nodes[i].derivative[j] * error * 0.001;
//                }
//            }
//        }
        for (int i = frames.Length - 1; i > 0; i--)
        {
            frames[i].value = frames[i - 1].value;
            frames[i].derivative = frames[i - 1].derivative;
        }
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
