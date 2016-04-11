using UnityEngine;
using System.Collections;

public class CONTENT_CreateWeightedConnection : MonoBehaviour 
{
    public Node from;
    public Node to;

	public void Start () 
    {
        CONTENT_ConnectionWeighted.Create(from, to);
	}
}
