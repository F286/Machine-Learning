using UnityEngine;
using System.Collections;

public class BoxNeuron : MonoBehaviour 
{
    public float value;

    public void OnCollisionEnter2D(Collision2D coll)
    {
        ManagerConv2.inst.AddConnection(gameObject, coll.gameObject);
    }

    public void FixedUpdate()
    {
        var r = GetComponent<Rigidbody2D>();
//        var d = value - r.angularVelocity;
        r.angularVelocity += (value * 120 - r.angularVelocity) * 0.08f;
//        r.AddTorque(r.angularVelocity - value);
    }

    public void Update()
    {
        GetComponent<SpriteRenderer>().color = 
            ManagerConv2.inst.color.Evaluate(Mathf.InverseLerp(-1, 1, value));
    }
}
