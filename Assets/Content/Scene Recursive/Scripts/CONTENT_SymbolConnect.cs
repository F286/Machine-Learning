using UnityEngine;
using System.Collections;

public class CONTENT_SymbolConnect : MonoBehaviour 
{
    public GameObject from;

    public void Awake()
    {
        var inIndex = 0;
        foreach (var _in in gameObject.FindInChildrenWithName("IN"))
        {
            inIndex++;
            var outIndex = 0;
            foreach (var _out in from.FindInChildrenWithName("OUT")) 
            {
                var _outNode = _out.GetComponent<CONTENT_Node>();
                var p = _in.transform.position;
                var pos = new Vector3(0, -0.16f + -0.25f * _in.transform.localScale.y + -0.45f * outIndex);

                var m = new GameObject("connect multiply");
                m.transform.position = p + pos;
                m.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                var mult = m.AddComponent<CONTENT_Node>();
                mult.type = CONTENT_Node.Type.Multiply;
                mult.generateGraphic = false;

                var v = new GameObject("connect value");
                v.transform.position = p + pos + new Vector3(-0.45f, 0);
                v.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                var val = v.AddComponent<CONTENT_Node>();
                val.type = CONTENT_Node.Type.Value;
                val.value = Mathf.Sign(Random.Range(0.8f, 1.2f));
                val.generateGraphic = false;
                CONTENT_Connect.Create(mult.gameObject, val, false);

                CONTENT_Connect.Create(_in, mult, false);
                CONTENT_Connect.Create(mult.gameObject, _outNode, false);


//                CONTENT_Connect.Create(_in.gameObject, _outNode);

                outIndex++;
            }
        }
    }
    public void Start()
    {
    }
}
