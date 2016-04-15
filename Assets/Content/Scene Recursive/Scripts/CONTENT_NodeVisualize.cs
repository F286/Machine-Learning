using UnityEngine;
using System.Collections;

public class CONTENT_NodeVisualize : MonoBehaviour 
{
    public void Awake()
    {
        var n = GetComponent<CONTENT_Node>();
        var size = 1f / n.equations.Count;
        for (int i = 0; i < n.equations.Count; i++)
        {
            var g = new GameObject("visualize", typeof(SpriteRenderer));
            g.GetComponent<SpriteRenderer>().sprite = Instantiate(Resources.Load<Sprite>("Visualize"));
            g.transform.SetParent(n.transform, false);
            g.GetComponent<SpriteRenderer>().color = CONTENT_NodeManager.instance.gradient.Evaluate(Random.value);
            g.transform.localPosition = new Vector2(-0.5f + size * i, 0);
            g.transform.localScale = new Vector3(size, 1, 1);
        }
    }
    public void LateUpdate()
    {
        
    }
}
