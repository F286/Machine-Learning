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

        public void initialize()
        {
            values = new float[size, size];
            valuesD = new float[size, size];
            conv = new float[size * 3, size * 3];
            convD = new float[size * 3, size * 3];
            weights = new float[(size - 2) * (size - 2), 3 * 3];

            var set = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    set++;
                    values[row, column] = set / 10f;
                    //                    values[x, y] = Random.Range(-0.1f, 0.1f);
                }
            }

            set = 0;
            for (int row = 0; row < weights.GetLength(0); row++)
            {
                for (int column = 0; column < weights.GetLength(1); column++)
                {
                    set++;
                    weights[row, column] = set / 10f;
                    //                    values[x, y] = Random.Range(-0.1f, 0.1f);
                }
            }
        }
        public void initGraphics(SCR_ManagerConvNet manager)
        {
//            var inst = GameObject.Instantiate(manager.template);
//            inst.transform.parent = transform;
//            inst.transform.localPosition = new Vector3(column, row) + (Vector3)offset * i;
//            display = inst;
        }
        public void update(SCR_ManagerConvNet manager)
        {
//            for (int row = 0; row < size; row++)
//            {
//                for (int column = 0; column < size; column++)
//                {
//                    display[row, column].pixel.color = 
//                        manager.gradient.Evaluate(Mathf.InverseLerp(-3, 3, values[row, column]));
//                }
//            }
        }

        public static void forward(Layer a, Layer b)
        {
            var kernelMatrix = Core.im2col(a.values);
            print(b.weights.Print());
            print(kernelMatrix.Print());
            var multiply = Core.multiply(kernelMatrix, b.weights);
            print(multiply.Print());
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
            layers[i].initialize();
            layers[i].initGraphics(this);
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
