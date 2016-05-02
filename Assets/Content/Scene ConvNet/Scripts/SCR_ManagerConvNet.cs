using UnityEngine;
using System.Collections;

public class SCR_ManagerConvNet : MonoBehaviour 
{
    const int size = 4;
    readonly Vector2 offset = new Vector2(0, 4.5f);

    public Gradient gradient;
    public CONTENT_ConvDisplay template;

    [System.Serializable]
    public class Layer
    {
        // organized in grid
        public float[,] values;
        public float[,] valuesD;
        // im2col organized in rows columns?
        public float[,] conv;
        public float[,] convD;

        public float[,] weights;

        public CONTENT_ConvDisplay[,] display;

        public void update(SCR_ManagerConvNet manager)
        {
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    display[row, column].pixel.color = 
                        manager.gradient.Evaluate(Mathf.InverseLerp(-3, 3, values[row, column]));
                }
            }
        }

        public static void forward(Layer a, Layer b)
        {
            var kernelMatrix = Core.im2col(a.values);
            print(kernelMatrix.Print());
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
            layers[i].values = new float[size, size];
            layers[i].valuesD = new float[size, size];
            layers[i].conv = new float[size * 3, size * 3];
            layers[i].convD = new float[size * 3, size * 3];
//            layers[i].weights = new float[, (size - 2) * (size - 2)];

            var set = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    set++;
                    layers[i].values[row, column] = set / 10f;
//                    layers[i].values[x, y] = Random.Range(-0.1f, 0.1f);
                }
            }

//            var set = 0;
//            for (int row = 0; row < size; row++)
//            {
//                for (int column = 0; column < size; column++)
//                {
//                    set++;
//                    layers[i].values[row, column] = set / 10f;
//                    //                    layers[i].values[x, y] = Random.Range(-0.1f, 0.1f);
//                }
//            }

            layers[i].display = new CONTENT_ConvDisplay[size, size];
            for (int row = 0; row < size; row++) 
            {
                for (int column = 0; column < size; column++) 
                {
                    var inst = GameObject.Instantiate(template);
                    inst.transform.parent = transform;
                    inst.transform.localPosition = new Vector3(column, row) + (Vector3)offset * i;
                    layers[i].display[row, column] = inst;
                }
            } 
        }

        template.gameObject.SetActive(false);
    }

	public void Start () 
    {
        SetLayers();

        for (int i = 1; i < layers.Length; i++)
        {
            Layer.forward(layers[i - 1], layers[i]);
        }
	}

    public void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].update(this);
        }
    }
}
