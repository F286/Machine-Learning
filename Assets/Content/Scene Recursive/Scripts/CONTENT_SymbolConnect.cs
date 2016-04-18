using UnityEngine;
using System.Collections;

public class CONTENT_SymbolConnect : MonoBehaviour 
{
    public GameObject from;

    public void Awake()
    {
//        print("connect");
        var inIndex = 0;
        foreach (var _in in gameObject.FindInChildrenWithTag("in"))
        {
            inIndex++;
            var outIndex = 0;
//            print(_in);
            foreach (var _out in from.FindInChildrenWithTag("out")) 
            {
                outIndex++;
                //                print(_out);
                var _outNode = _out.GetComponent<CONTENT_Node>();
//                CONTENT_Connect.Create(_in, _outNode);
//                _in.AddComponent<CONTENT_Connect>().from = f;

                var p = _in.transform.position;

                var m = new GameObject("connect multiply");
                m.transform.position = p + new Vector3(0, -0.16f + -0.25f * outIndex);
                m.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                var mult = m.AddComponent<CONTENT_Node>();
                mult.type = CONTENT_Node.Type.Multiply;

                var v = new GameObject("connect value");
                v.transform.position = p + new Vector3(-0.25f, -0.16f + -0.25f * outIndex);
                v.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
                var val = v.AddComponent<CONTENT_Node>();
                val.type = CONTENT_Node.Type.Value;
                val.value = 1;
                CONTENT_Connect.Create(mult.gameObject, val);

                CONTENT_Connect.Create(_in, mult);
                CONTENT_Connect.Create(mult.gameObject, _outNode);
            }
        }
    }
    public void Start()
    {
    }
}
