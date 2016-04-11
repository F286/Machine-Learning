using UnityEngine;
using System.Collections;

public class CONTENT_DisplayConnection : CONTENT_Display 
{
    public Node from;
    public Node to;

//    public override float value
//    {
//        set
//        {
//            var c = CONTENT_ManagerNeuron.instance.gradient.Evaluate(Mathf.InverseLerp(-1, 1, value));
//            if (value == 0)
//            {
//                c = Color.grey;
//            }
//            c.a = 0.6f;
//            s.color = c;
//            v = value;
//        }
//    }
//    public SpriteRenderer s;
//    protected float deriv;

    float wiggle;
//    float v;
    float valueCached;
    public override void LateUpdate()
    {
        if (_valueCount > 0)
        {
            _value /= _valueCount;
            valueCached = _value;

            var c = CONTENT_ManagerNeuron.instance.gradient.Evaluate(Mathf.InverseLerp(-1, 1, _value));
            if (_value == 0)
            {
                c = new Color(0.7f, 0.7f, 0.7f);
            }
            c.a = 0.6f;
            sprite.color = c;
            sprite.sortingOrder = -10;

            _valueCount = 0;
            _value = 0;
        }
        if (_derivativeCount > 0)
        {
            _derivative /= _derivativeCount;

            wiggle += _derivative * Time.deltaTime * 10;

            transform.position = (from.transform.position + to.transform.position) / 2f;

            var s = transform.localScale;
            s.x = (from.transform.position - to.transform.position).magnitude;
            s.y = (1f / (1f + Mathf.Exp(-Mathf.Abs(valueCached / 5)))) * 0.11f + Mathf.Sin(wiggle * 5) * 0.012f;
            transform.localScale = s;

            transform.rotation = Quaternion.Euler(0, 0, (to.transform.position - from.transform.position).Angle());
            _derivativeCount = 0;
            _derivative = 0;
        }

//        var r = transform.localEulerAngles;
//        r.z += deriv * -20 * Time.deltaTime;
//        transform.localEulerAngles = r;
    }
}
