using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class CONTENT_Symbol : MonoBehaviour 
{
    public GameObject template;

    public void Awake()
    {
//        print(name);
        Assert.IsTrue(template != null, "'template' must be set");

        template.SetActive(false);
        var copy = GameObject.Instantiate(template);
        copy.transform.SetParent(transform, false);
        copy.transform.localPosition = Vector3.zero;
        copy.SetActive(true);

        transform.localScale = new Vector3(0.5f, 0.5f, 1);

    }
}
