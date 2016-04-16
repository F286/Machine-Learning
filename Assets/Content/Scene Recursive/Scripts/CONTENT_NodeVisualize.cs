using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CONTENT_NodeVisualize : MonoBehaviour 
{
    public List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    public CONTENT_Node node;
    public void Awake()
    {
        node = GetComponent<CONTENT_Node>();
        var size = 1f / node.equations.Count;
        for (int i = 0; i < node.equations.Count; i++)
        {
            var g = new GameObject("visualize", typeof(SpriteRenderer));
            g.GetComponent<SpriteRenderer>().sprite = Instantiate(Resources.Load<Sprite>("Visualize"));
            g.transform.SetParent(node.transform, false);
            g.GetComponent<SpriteRenderer>().color = CONTENT_NodeManager.instance.gradient.Evaluate(Random.value);
            g.transform.localPosition = new Vector2(-0.5f + size * i, 0);
            g.transform.localScale = new Vector3(size, 1, 1);
            sprites.Add(g.GetComponent<SpriteRenderer>());
        }
    }
    public void LateUpdate()
    {
        
        for (int i = 0; i < node.equations.Count; i++)
        {
//            var index = node.index;
//            var v = CONTENT_NodeManager.instance.frames[i].value[index];
//            var d = CONTENT_NodeManager.instance.frames[i].derivative[index];
            var v = (float)node.equations[i].Val.value;
            var d = (float)node.equations[i].Val.derivative;
            d = Core.Tanh(d);//.1f;

            sprites[i].color = CONTENT_NodeManager.instance.gradient.Evaluate(0.5f + v / 2f);
//            sprites[i].color = CONTENT_NodeManager.instance.gradient.Evaluate(Core.Sigmoid((float)v / 2f));

            var s = sprites[i].transform.localScale;
            s.y = Mathf.Clamp(Mathf.Abs(v / 1f), 0, 1f);
//            s.y = Core.Sigmoid(Mathf.Abs(v / 2f) - 1.5f);

            var wiggle = Mathf.Sin(Time.time * 70 * d);// * 0.5f + 0.5f;
//            var wiggle = Mathf.Sin(Time.time * 25);// * 0.5f + 0.5f;
//            var wiggle = Mathf.Sin(Time.time * 15);// * 0.5f + 0.5f;
//            var wiggle = Mathf.Sin(Time.time * 20);// * 0.5f + 0.5f;
//            var wiggle = Mathf.Sin(Time.time * 20 * d);// * 0.5f + 0.5f;
            wiggle *= Core.Tanh(Mathf.Abs(d * 5)) * 0.064f * 0.3f;
//            wiggle *= Mathf.Abs(d) * 0.055f;
            s.y += 0.12f + wiggle;
//            s.y = 0.12f + s.y + d * 0.055f * wiggle;

//            s.y = Core.Sigmoid((float)d);
            sprites[i].transform.localScale = s;

            var p = sprites[i].transform.localPosition;
            p.y = wiggle * 0.5f * Mathf.Sign(d);
            sprites[i].transform.localPosition = p;
        }
//        for (int i = 0; i < node.equations.Count; i++)
//        {
//            CONTENT_NodeManager.FrameData
//        }
    }
}
