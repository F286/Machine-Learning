using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class CONTENT_NeuronManager : MonoBehaviour 
{
    [System.Serializable]
    public class NodeInput
    {
        public int[] inputs;

        public NodeInput(int[] i)
        {
            inputs = i;
        }
    }

    public List<Node> nodes;
    public List<CONTENT_Connection> connections;

    public List<NodeInput> leftToRight = new List<NodeInput>();

    public void Start()
    {
//        nodes = gameObject.GetComponentsInChildren<INode>();
//        connections = gameObject.GetComponentsInChildren<CONTENT_Connection>();
//
        AddConnections(GameObject.FindGameObjectWithTag("output").GetComponent<Node>());

//        print(leftToRight.Count);
//        print(leftToRight[0]);
//        print(leftToRight[0][0]);
//        print(leftToRight[0][1]);
    }

    List<Node> alreadyAdded = new List<Node>();
    void AddConnections(Node node)
    {
        if (!alreadyAdded.Contains(node))
        {
//            print(node);
            alreadyAdded.Add(node);
//            nodes.Add(node);
            var inputs = new List<int>();
            foreach (var item in GetInputs(node))
            {
//                print(GetIndex(item));
                inputs.Add(GetIndex(item));
                AddConnections(item);
            }
            print(inputs.Count);
            leftToRight.Add(new NodeInput(inputs.ToArray()));
        }
//        connections = 
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

    static CONTENT_NeuronManager _inst;
    public static CONTENT_NeuronManager instance
    {
        get
        {
            if(_inst == null)
            {
                _inst = GameObject.FindObjectOfType<CONTENT_NeuronManager>();
            }
            return _inst;
        }
    }
}
