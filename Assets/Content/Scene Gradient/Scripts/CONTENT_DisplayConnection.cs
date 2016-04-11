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
        }
    }
//    public SpriteRenderer s;
//    protected float deriv;

    float wiggle;

    public override void LateUpdate()
    {
        wiggle += deriv * Time.deltaTime;

        transform.position = (from.transform.position + to.transform.position) / 2f;

        var s = transform.localScale;
        s.x = (from.transform.position - to.transform.position).magnitude;
        s.y = 0.05f + Mathf.Sin(wiggle * 5) * 0.015f;
        transform.localScale = s;

        transform.rotation = Quaternion.Euler(0, 0, (to.transform.position - from.transform.position).Angle());

//        var r = transform.localEulerAngles;
//        r.z += deriv * -20 * Time.deltaTime;
//        transform.localEulerAngles = r;
    }
}
