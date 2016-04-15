using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CONTENT_NodeManager : MonoBehaviour
{
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

    public CONTENT_Node[] nodes;
    public FrameData[] data;
    public List<CONTENT_Equation> equations;

    public void Start()
    {
//        nodes = GameObject.FindObjectsOfType<CONTENT_Node>();
//        var connect = GameObject.FindObjectsOfType<CONTENT_Connect>();

        AddEquations(GameObject.FindWithTag("output").GetComponent<CONTENT_Node>());
    }

    void AddEquations(CONTENT_Node node, int age = 0, List<CONTENT_Node> currentTree = null)
    {
        if (age > 10)
        {
            return;
        }
        if (currentTree == null)
        {
            currentTree = new List<CONTENT_Node>();
        }
        foreach (var item in node.input)
        {
            if ((item.added & 1U << age) == 0)
            {
                item.added |= 1U << age;
                if (currentTree.Contains(item))
                {
                    // this node is recursive, increase age and reset tree
                    AddEquations(item, age + 1, null);
                }
                else
                {  
                    AddEquations(item, age, currentTree);
                }
            }
        }

        var g = new GameObject(item.type.ToString());
        g.AddComponent<CONTENT_Equation>().type = item.type;
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
