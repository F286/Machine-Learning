using UnityEngine;
using System.Collections;

public class CONTENT_SymbolConnect : MonoBehaviour 
{
    public GameObject from;

    public void Awake()
    {
//        print("connect");
        var inIndex = 0;
        foreach (var _in in gameObject.FindInChildrenWithName("IN"))
        {
            inIndex++;
            var outIndex = 0;
//            print(_in);
            foreach (var _out in from.FindInChildrenWithName("OUT")) 
            {
                //                print(_out);
                var _outNode = _out.GetComponent<CONTENT_Node>();
//                CONTENT_Connect.Create(_in, _outNode);
//                _in.AddComponent<CONTENT_Connect>().from = f;

                var p = _in.transform.position;

                var pos = new Vector3(0, -0.16f + -0.25f * _in.transform.localScale.y + -0.25f * outIndex);

                var m = new GameObject("connect multiply");
                m.transform.position = p + pos;
                m.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                var mult = m.AddComponent<CONTENT_Node>();
                mult.type = CONTENT_Node.Type.Multiply;

                var v = new GameObject("connect value");
                v.transform.position = p + pos + new Vector3(-0.25f, 0);
                v.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                var val = v.AddComponent<CONTENT_Node>();
                val.type = CONTENT_Node.Type.Value;
//                val.value = 1;
                CONTENT_Connect.Create(mult.gameObject, val);

                CONTENT_Connect.Create(_in, mult);
                CONTENT_Connect.Create(mult.gameObject, _outNode);

                outIndex++;
            }
        }
    }
    public void Start()
    {
    }
}
