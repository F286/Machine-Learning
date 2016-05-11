using UnityEngine;
using System.Collections;

public interface IOnInteract
{
    void OnInteract(InteractData eventData);
}
public class InteractData
{
    public Vector2 deltaPosition;
}
public class CONTENT_Gesture : MonoBehaviour 
{
    public SpringJoint2D Drag;

    Collider2D overlap;
    Vector2 mousePositionPrev;

    public void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            overlap = Physics2D.OverlapCircle(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), 0.1f, int.MaxValue);

            print(overlap);
//            if (overlap)
//            {
//                print(overlap);
//            }
            if (overlap && overlap.attachedRigidbody)
            {
                Drag.connectedBody = overlap.attachedRigidbody;
            }
        }
        if (overlap && Input.GetMouseButton(0))
        {
            var d = new InteractData();
            d.deltaPosition = (Vector2)Input.mousePosition - mousePositionPrev;
            overlap.SendMessage("OnInteract", d, SendMessageOptions.DontRequireReceiver);
        }
        if (overlap)
        {
            Drag.transform.position = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Drag.connectedBody = null;
        }



        mousePositionPrev = Input.mousePosition;
    }
}
