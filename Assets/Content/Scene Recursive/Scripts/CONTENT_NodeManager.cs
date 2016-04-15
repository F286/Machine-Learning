using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CONTENT_NodeManager : MonoBehaviour
{
    public const int frames = 5;

    [System.Serializable]
    public class FrameData
    {
        public float[] value;
        public float[] derivative;

        public FrameData(int length)
        {
            value = new float[length];
            derivative = new float[length];
        }
    }

    public List<CONTENT_Node> nodes;
//    public CONTENT_Node[] nodes;
    public FrameData[] data;
    public List<CONTENT_Equation> equations;
    public Gradient gradient;

    public void Start()
    {
//        nodes = GameObject.FindObjectsOfType<CONTENT_Node>();
//        var connect = GameObject.FindObjectsOfType<CONTENT_Connect>();

        AddEquations(GameObject.FindWithTag("output").GetComponent<CONTENT_Node>());

        data = new FrameData[frames];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = new FrameData(frames);
        }

        foreach (var item in nodes)
        {
            item.gameObject.AddComponent<CONTENT_NodeVisualize>();
        }
    }

    CONTENT_Equation AddEquations(CONTENT_Node node, int age = 0, List<CONTENT_Node> currentTree = null)
    {
        if (age >= frames)
        {
            return null;
        }
        if (currentTree == null)
        {
            currentTree = new List<CONTENT_Node>();
        }
        currentTree.Add(node);
        CONTENT_Equation[] input = new CONTENT_Equation[node.input.Count];
        for (int i = 0; i < node.input.Count; i++)
        {
            var item = node.input[i];
            if ((item.added & 1U << age) == 0)
            {
                item.added |= 1U << age;
                if (currentTree.Contains(item))
                {
                    // this node is recursive, increase age and reset tree
                    input[i] = AddEquations(item, age + 1, null);
                }
                else
                {  
                    input[i] = AddEquations(item, age, currentTree);
                }
            }
        }
        if (!nodes.Contains(node))
        {
            nodes.Add(node);
        }
        var g = new GameObject(node.name + " (" + node.type.ToString() + ")");
        var e = g.AddComponent<CONTENT_Equation>();
        e.type = node.type;
        e.input = input;
        equations.Add(e);
        node.equations.Add(e);
        return e;
    }
    public void Update()
    {
        
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
