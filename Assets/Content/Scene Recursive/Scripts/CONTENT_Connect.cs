using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

//[RequireComponent(typeof(CONTENT_Node))]
public class CONTENT_Connect : MonoBehaviour 
{
    public CONTENT_Node from;
    public bool generateGraphic = true;

    static CONTENT_Node set;
    static bool? gen = null;
    public static void Create(GameObject g, CONTENT_Node _from, bool generateGraphic = true)
    {
        set = _from;
        gen = generateGraphic;
        g.AddComponent<CONTENT_Connect>();
        gen = null;
        set = null;
    }

    public void Awake()
    {
        if (set)
        {
            from = set;
        }
        if (gen.HasValue)
        {
            generateGraphic = gen.Value;
        }
        Assert.IsTrue(from != null, gameObject.name + " : on CONTENT_Connect 'from' must be set.");
        Assert.IsTrue(gameObject.GetComponent<CONTENT_Node>() != null, gameObject.name +" : CONTENT_Connect must have a CONTENT_Node on it.");

        gameObject.GetComponent<CONTENT_Node>().input.Add(from);
        from.output.Add(gameObject.GetComponent<CONTENT_Node>());

        if (generateGraphic)
        {
            // visualize
            var a = from;
            var b = GetComponent<CONTENT_Node>();

            var g = new GameObject("visualize", typeof(SpriteRenderer));
            g.GetComponent<SpriteRenderer>().sprite = Instantiate(Resources.Load<Sprite>("Arrow"));
            g.transform.SetParent(transform, false);
            g.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.4f);
            g.GetComponent<SpriteRenderer>().sortingOrder = 10;
            g.transform.position = (a.transform.position + b.transform.position) * 0.5f;
            g.transform.localScale = new Vector3(0.2f, 0.2f, 1);
            g.transform.rotation = Quaternion.Euler(0, 0, (b.transform.position - a.transform.position).Angle());

            switch (GetComponent<CONTENT_Node>().type)
            {
                case CONTENT_Node.Type.Add:
                    g.GetComponent<SpriteRenderer>().color *= Color.yellow;
                    break;
                case CONTENT_Node.Type.Subtract:
                    break;
                case CONTENT_Node.Type.Multiply:
                    g.GetComponent<SpriteRenderer>().color *= Color.red;
                    break;
                case CONTENT_Node.Type.Divide:
                    break;
                case CONTENT_Node.Type.Sigmoid:
                    g.GetComponent<SpriteRenderer>().color *= Color.green;
                    break;
                case CONTENT_Node.Type.Tanh:
                    g.GetComponent<SpriteRenderer>().color *= Color.magenta;
                    break;
                case CONTENT_Node.Type.Value:
                    break;
                case CONTENT_Node.Type.Input:
                    break;
            }
        }
    }
}
