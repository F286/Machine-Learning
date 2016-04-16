using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

[RequireComponent(typeof(CONTENT_Node))]
public class CONTENT_Connect : MonoBehaviour 
{
    public CONTENT_Node from;

    public void Awake()
    {
        Assert.IsTrue(from != null, "on CONTENT_Connect 'from' must be set.");

        gameObject.GetComponent<CONTENT_Node>().input.Add(from);
        from.output.Add(gameObject.GetComponent<CONTENT_Node>());

        // visualize
        var a = from;
        var b = GetComponent<CONTENT_Node>();

        var g = new GameObject("visualize", typeof(SpriteRenderer));
        g.GetComponent<SpriteRenderer>().sprite = Instantiate(Resources.Load<Sprite>("Arrow"));
        g.transform.SetParent(transform, false);
        g.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 1);//CONTENT_NodeManager.instance.gradient.Evaluate(Random.value);
        g.transform.position = (a.transform.position + b.transform.position) * 0.5f;
        g.transform.localScale = new Vector3(0.25f, 0.25f, 1);
        g.transform.rotation = Quaternion.Euler(0, 0, (b.transform.position - a.transform.position).Angle());
//        sprites.Add(g.GetComponent<SpriteRenderer>());
    }
}
