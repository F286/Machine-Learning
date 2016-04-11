using UnityEngine;
using System.Collections;

public class CONTENT_DisplayConnection : CONTENT_Display 
{
    public Node from;
    public Node to;

    public override float value
    {
        set
        {
            s.color = CONTENT_ManagerNeuron.instance.gradient.Evaluate(Mathf.InverseLerp(-5, 5, value));
            v = value;
        }
    }
//    public SpriteRenderer s;
//    protected float deriv;

    float wiggle;
    float v;

    public override void LateUpdate()
    {
        wiggle += deriv * Time.deltaTime;

        transform.position = (from.transform.position + to.transform.position) / 2f;

        var s = transform.localScale;
        s.x = (from.transform.position - to.transform.position).magnitude;
        s.y = (1f / (1f + Mathf.Exp(-Mathf.Abs(v / 5)))) * 0.11f + Mathf.Sin(wiggle * 5) * 0.012f;
        transform.localScale = s;

        transform.rotation = Quaternion.Euler(0, 0, (to.transform.position - from.transform.position).Angle());

//        var r = transform.localEulerAngles;
//        r.z += deriv * -20 * Time.deltaTime;
//        transform.localEulerAngles = r;
    }
}
