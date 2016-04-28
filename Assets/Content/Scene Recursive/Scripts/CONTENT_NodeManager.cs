using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class CONTENT_NodeManager : MonoBehaviour
{
    public const int TotalFrames = 30;
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
    public bool iterateTime;

    public void Start()
    {
        Random.InitState(0);

        var o = GameObject.FindWithTag("output");

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
            }
        }
        for (int i = 0; i < nodes.Count; i++)
        {
            var item = nodes[i];
            if (item.generateGraphic)
            {
                item.gameObject.AddComponent<CONTENT_NodeVisualize>();
                if (nodes[i].type == CONTENT_Node.Type.Input)
                {
                    input.Add(nodes[i]);
                }
            }
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

        for (int i = 0; i < node.input.Count; i++)
        {
            var item = node.input[i];
            if (currentTree.Contains(item))
            {
                if (currentFrame + 1 < TotalFrames)
                {
                    AddEquations(item, currentFrame + 1, null);
                    _input.Add(new DataPointer(currentFrame + 1, item.current.node));
                }
            }
            else
            {
                AddEquations(item, currentFrame, currentTree);
                _input.Add(new DataPointer(currentFrame, item.current.node));
            }
        }
        if (!nodes.Contains(node))
        {
            node.current = new DataPointer(0, nodes.Count);
            nodes.Add(node);
        }
        {
            var g = new GameObject(node.name + " (" + node.type.ToString() + ")  f " + currentFrame + "  n " + node.current.node + "  i " + _input.Count);
            var e = g.AddComponent<CONTENT_Equation>();
            e.type = node.type;
            e.Val = new DataPointer(currentFrame, node.current.node);
            e.In = _input.ToArray();
            equations.Add(e);
            node.equations.Add(e);
            return e;
        }
    }
    int t = 0;
    public List<CONTENT_Node> input = new List<CONTENT_Node>();
    public bool train = true;
    public double trainRate = 0.00001f;
    public int iterations = 50;
    public void Update()
    {
        for (int iterate = 0; iterate < iterations; iterate++)
        {
            t++;
            
            for (int i = 0; i < input.Count; i++)
            {
                input[i].current.value = System.Math.Sin(Mathf.PI * 2 * (Time.frameCount / 60.0) * 4 / (i + 1));
            }
            if (iterateTime)
            {
                for (int f = TotalFrames - 1; f > 0; f--)
                {
                    for (int v = 0; v < frames[f].value.Length; v++)
                    {
                        frames[f].value[v] = frames[f - 1].value[v];
                    }
                }
            }
            var o = GameObject.FindWithTag("output").GetComponent<CONTENT_Node>();

            for (int f = 0; f < frames.Length; f++)
            {
                for (int n = 0; n < nodes.Count; n++)
                {
                    frames[f].derivative[n] = 0;
                }
            }
            o.current.derivative = 1;

            for (int i = 0; i < equations.Count; i++)
            {
                var e = equations[i];
                e.forward();
            }
            for (int i = equations.Count - 1; i >= 0; i--)
            {
                equations[i].backward();
            }
            if (train)
            {
                var target = System.Math.Sin(t * 0.02f);
                var error = target - o.value;
                print(target + "   " + error);
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].type == CONTENT_Node.Type.Value)
                    {
                        for (int j = 0; j < nodes[i].derivative.Length; j++)
                        {
                            nodes[i].current.value += (nodes[i].derivative[j] * error * trainRate) / TotalFrames;
                        }
                    }
                }
            }
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
