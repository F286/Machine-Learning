using UnityEngine;
using System.Collections;

public class SCR_ManagerConvNet : MonoBehaviour 
{
    public Gradient gradient;
    public CONTENT_ConvDisplay template;
//    [System.Serializable]
//    public class Array
//    {
//        public float[] values;
//    }
    [System.Serializable]
    public class Layer
    {
        // organized in grid
        public float[,] values;
        public float[,] valuesD;
        // im2col organized in rows columns?
        public float[,] conv;
        public float[,] convD;

        public CONTENT_ConvDisplay[,] display;

        public void initialize()
        {
            
        }

        public static void forward(Layer a, Layer b)
        {
            
        }
        public static void backward(Layer a, Layer b)
        {

        }
    }

    public Layer[] layers;

    [ContextMenu("SetLayers")]
    public void SetLayers()
    {
        layers = new Layer[2];
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i] = new Layer();
            layers[i].values = new float[2, 2];
            layers[i].valuesD = new float[2, 2];
            layers[i].conv = new float[2 * 3, 2 * 3];
            layers[i].convD = new float[2 * 3, 2 * 3];

            layers[i].display = new CONTENT_ConvDisplay[2, 2];
            for (int x = 0; x < 2; x++) 
            {
                for (int y = 0; y < 2; y++) 
                {
                    var inst = GameObject.Instantiate(template);
                    inst.transform.parent = transform;
                    inst.transform.localPosition = new Vector3(i * 3 + x, y);
                    layers[i].display[x, y] = inst;
                }
            } 
        }

        template.gameObject.SetActive(false);
    }

	public void Start () 
    {
        for (int i = 1; i < layers.Length; i++)
        {
            Layer.forward(layers[i - 1], layers[i]);
        }
	}
}
