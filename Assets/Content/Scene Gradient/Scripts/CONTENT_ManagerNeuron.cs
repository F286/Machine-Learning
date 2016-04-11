using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class CONTENT_ManagerNeuron : MonoBehaviour 
{
    [System.Serializable]
    public class NodeInput
    {
        public int[] inputs;
        public int node;

        public NodeInput(int[] i, int n)
        {
            inputs = i;
            node = n;
        }
    }

    public List<Node> nodes;
    public List<CONTENT_Connection> connections;

    public List<NodeInput> leftToRight = new List<NodeInput>();
    public Gradient gradient;

    public void Awake()
    {
        var input = new List<CONTENT_NodeValue>();
        var layer1 = new List<CONTENT_Neuron>();
        var layer2 = new List<CONTENT_Neuron>();
        CONTENT_NodeAdd output;

        // input
        for (int i = 0; i < 1; i++)
        {
            var g = new GameObject("input 0 (" + i + ")");
            g.transform.localPosition = new Vector3(0, i);
            var v = g.AddComponent<CONTENT_NodeValue>();
            v.value = Random.Range(-0.5f, 0.5f);
            v.display = v.gameObject.AddComponent<CONTENT_Display>();
            input.Add(v);
        }

        // layer 1
        for (int i = 0; i < 4; i++)
        {
            var g = new GameObject("layer 1 (" + i + ")");
            g.transform.localPosition = new Vector3(1, i * 0.5f);
            var n = g.AddComponent<CONTENT_Neuron>();
            foreach (var item in input)
            {
                CONTENT_ConnectionWeighted.Create(item, n.input);
            }
            layer1.Add(n);
        }
        // layer 2
        for (int i = 0; i < 4; i++)
        {
            var g = new GameObject("layer 2 (" + i + ")");
            g.transform.localPosition = new Vector3(2, i * 0.5f);
            var n = g.AddComponent<CONTENT_Neuron>();
            foreach (var item in layer1)
            {
                CONTENT_ConnectionWeighted.Create(item.output, n.input);
            }
            layer2.Add(n);
        }

        // output
        {
            var g = new GameObject("output");
            g.transform.localPosition = new Vector3(3, 0);
            output = g.AddComponent<CONTENT_NodeAdd>();
            output.tag = "output";
            output.display = output.gameObject.AddComponent<CONTENT_Display>();
            foreach (var item in layer2)
            {
                CONTENT_Connection.Create(item.output, output, true);
            }
        }
//        CONTENT_ConnectionWeighted.Create(input[0], output);
    }
    public void Start()
    {
        EvaluateConnections(GameObject.FindGameObjectWithTag("output").GetComponent<Node>());
    }
    public void Update()
    {
        //forward
        for (int i = 0; i < leftToRight.Count; i++)
        {
            var pass = new Node[leftToRight[i].inputs.Length];
            for (int n = 0; n < pass.Length; n++) 
            {
                pass[n] = nodes[leftToRight[i].inputs[n]];
            }
            nodes[leftToRight[i].node].DebugInputs = pass;
            nodes[leftToRight[i].node].forward(pass);
            nodes[leftToRight[i].node].derivative = 0;
        }
        GameObject.FindGameObjectWithTag("output").GetComponent<Node>().derivative = 1;

        //backwards
        for (int i = leftToRight.Count - 1; i >= 0; i--)
        {
            var pass = new Node[leftToRight[i].inputs.Length];
            for (int n = 0; n < pass.Length; n++) 
            {
                pass[n] = nodes[leftToRight[i].inputs[n]];
            }
            nodes[leftToRight[i].node].backward(pass);
        }
    }
    List<Node> alreadyAdded = new List<Node>();
    void EvaluateConnections(Node node)
    {
        if (!alreadyAdded.Contains(node))
        {
            alreadyAdded.Add(node);
            var inputs = new List<int>();
            foreach (var item in GetInputs(node))
            {
                inputs.Add(GetIndex(item));
                EvaluateConnections(item);
            }
            if (inputs.Count > 0)
            {
                print(node + " - " + inputs.Count);
                leftToRight.Add(new NodeInput(inputs.ToArray(), GetIndex(node)));
            }
        }
    }
    public IEnumerable<Node> GetInputs(Node to)
    {
        foreach (var item in connections)
        {
            if (item.to == to)
            {
                yield return item.from;
            }
        }
    }
    public int GetIndex(Node item)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i] == item)
            {
                return i;
            }
        }
        return -1;
    }
     
    static CONTENT_ManagerNeuron _inst;
    public static CONTENT_ManagerNeuron instance
    {
        get
        {
            if(_inst == null)
            {
                _inst = GameObject.FindObjectOfType<CONTENT_ManagerNeuron>();
            }
            return _inst;
        }
    }
}
