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
    Collider2D overlap;
    Vector2 mousePositionPrev;
    public void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            overlap = Physics2D.OverlapCircle(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), 0.1f, int.MaxValue);

//            print(overlap);
//            if (overlap)
//            {
//                print(overlap);
//            }
        }
        if (overlap && Input.GetMouseButton(0))
        {
            var d = new InteractData();
            d.deltaPosition = (Vector2)Input.mousePosition - mousePositionPrev;
            overlap.SendMessage("OnInteract", d, SendMessageOptions.DontRequireReceiver);
        }
        mousePositionPrev = Input.mousePosition;
    }
}
