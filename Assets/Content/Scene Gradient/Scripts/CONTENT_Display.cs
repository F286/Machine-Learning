using UnityEngine;
using System.Collections;

public class CONTENT_Display : MonoBehaviour 
{
    public virtual float value
    {
        set
        {
            var c = CONTENT_ManagerNeuron.instance.gradient.Evaluate(Mathf.InverseLerp(-5, 5, value));
            s.color = c;
            var scale = 0.05f + (1f / (1f + Mathf.Exp(-Mathf.Abs(value / 5)))) * 0.3f;
//            scale = value;
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }
    public float derivative
    {
        set
        {
            deriv = value;
//            s.color = CONTENT_ManagerNeuron.instance.gradient.Evaluate(1f - value);
        }
    }
//    public float size
//    {
//        set
//        {
//            transform.localScale = new Vector3(value * 0.25f, value * 0.25f, 1);
//        }
//    }
    public SpriteRenderer s;
    protected float deriv;

    public void Awake()
    {
        s = gameObject.AddComponent<SpriteRenderer>();
        s.sprite = Resources.Load<Sprite>("Box Neuron");
//        s.color = Color.yellow;
//        transform.localScale = new Vector3(0.5f, 0.5f, 1);
        value = 0.5f;
//        size = 1;
    }
    public virtual void LateUpdate()
    {
        var r = transform.localEulerAngles;
        r.z += deriv * -20 * Time.deltaTime;
        transform.localEulerAngles = r;
    }
}
