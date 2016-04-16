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

            sprites[i].color = CONTENT_NodeManager.instance.gradient.Evaluate(Core.Sigmoid((float)v));

            var s = sprites[i].transform.localScale;
            s.y = Core.Sigmoid(Mathf.Abs(v) - 1.5f);

            var wiggle = Mathf.Sin(Time.time * 30 * d) * 0.5f + 0.5f;
            s.y = s.y * 0.5f + (Core.Sigmoid(Mathf.Abs(d * 0.3f)) - 0.5f) * wiggle;

//            s.y = Core.Sigmoid((float)d);
            sprites[i].transform.localScale = s;
        }
//        for (int i = 0; i < node.equations.Count; i++)
//        {
//            CONTENT_NodeManager.FrameData
//        }
    }
}
