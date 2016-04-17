using UnityEngine;
using System.Collections;

public class CONTENT_SymbolConnect : MonoBehaviour 
{
    public GameObject from;

    public void Awake()
    {
//        print("connect");
        foreach (var _in in gameObject.FindInChildrenWithTag("in"))
        {
//            print(_in);
            foreach (var _out in from.FindInChildrenWithTag("out")) 
            {
                //                print(_out);
                var f = _out.GetComponent<CONTENT_Node>();
                CONTENT_Connect.Create(_in, f);
//                _in.AddComponent<CONTENT_Connect>().from = f;
            }
        }
    }
}
