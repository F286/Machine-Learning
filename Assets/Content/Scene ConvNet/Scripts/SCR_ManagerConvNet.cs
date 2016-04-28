using UnityEngine;
using System.Collections;

public class SCR_ManagerConvNet : MonoBehaviour 
{
    public Gradient gradient;
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

        public CONTENT_ConvDisplay display;

//        // organized in grid
//        public Array[] values;
//        public Array[] valuesD;
//        // im2col organized in rows columns?
//        public Array[] conv;
//        public Array[] convD;

        public static void forward(Layer a, Layer b)
        {
            
        }
        public static void backward(Layer a, Layer b)
        {

        }
    }

    public Layer[] layers;

    public void Reset()
    {
        layers = new Layer[2];
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].values = new float[2, 2];
            layers[i].valuesD = new float[2, 2];
            layers[i].conv = new float[2 * 3, 2 * 3];
            layers[i].convD = new float[2 * 3, 2 * 3];
        }
    }

	public void Start () 
    {
        for (int i = 1; i < layers.Length; i++)
        {
            Layer.forward(layers[i - 1], layers[i]);
        }
	}
}
