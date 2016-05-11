using UnityEngine;
using System.Collections;

public class BoxNeuron : MonoBehaviour 
{
    public void OnCollisionEnter2D(Collision2D coll)
    {
        ManagerConv2.inst.AddConnection(gameObject, coll.gameObject);
    }
}
