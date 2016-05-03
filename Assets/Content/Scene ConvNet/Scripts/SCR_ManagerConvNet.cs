using UnityEngine;
using System.Collections;

public class SCR_ManagerConvNet : MonoBehaviour 
{
    const int size = 4;
    const int neuronsNum = 3;
//    readonly Vector2 offset = new Vector2(0, 4.5f);

    public Gradient gradient;
    public CONTENT_ConvDisplay template;

    [System.Serializable]
    public class Layer
    {
        // organized in grid
        public float[,,] value;
        public float[,,] valueGradient;
        // im2col organized in rows columns?
//        public float[,] conv;
//        public float[,] convD;

        public float[,] convolution;
        public float[,] convolutionGradient;

//        public CONTENT_ConvDisplay[,] display;
        public Texture2D[] valueDisplay;
        public Texture2D[] convolutionDisplay;

        public void initialize(Layer previous)
        {
            if (previous == null)
            {
                value = new float[3, 3, 3];
                valueGradient = new float[value.row(), value.column(), value.layer()];
//                value = new float[32, 32, 3];
//                valueGradient = new float[32, 32, 3];

                convolution = new float[0, 0];
                convolutionGradient = new float[0, 0];
            }
            else
            {
//                print(previous.value.row());
//                print(previous.value.column());
//                print(previous.value.layer());
                value = new float[previous.value.row() - 2, previous.value.column() - 2, neuronsNum];
                valueGradient = new float[value.row(), value.column(), value.layer()];

                convolution = new float[(previous.value.row() - 2) * (previous.value.column() - 2),
                    previous.value.row() * previous.value.column() * previous.value.column()];
                convolutionGradient = new float[convolution.row(), convolution.column()];
            }
//            value = new float[size, size];
//            valueGradient = new float[size, size];
////            conv = new float[size * 3, size * 3];
////            convD = new float[size * 3, size * 3];
//            convolution = new float[numNeurons, 3 * 3];

            var set = -1;
            for (int row = 0; row < value.row(); row++)
            {
                for (int column = 0; column < value.column(); column++)
                {
                    for (int layer = 0; layer < value.layer(); layer++)
                    {
                        set++;
                        value[row, column, layer] = set / 10f;
                        valueGradient[row, column, layer] = set / 10f;
                    }
                }
            }

            set = -1;
            for (int row = 0; row < convolution.row(); row++)
            {
                for (int column = 0; column < convolution.column(); column++)
                {
                    set++;
                    convolution[row, column] = set / 10f;
                    convolutionGradient[row, column] = set / 10f;
                }
            }
//            print(value.Print());
//            print(convolution.Print());
        }
        public void initGraphics(SCR_ManagerConvNet manager, int index)
        {
            valueDisplay = new Texture2D[value.layer()];
            convolutionDisplay = new Texture2D[convolution.row()];
//            textures = new Texture2D[neuronsNum, 2];
            print(convolution.Print());

            for (int layer = 0; layer < convolutionDisplay.Length; layer++)
            {
                var p = new Vector2(0.0f + index * 2.4f, 1.1f * layer);
                var image = GameObject.Instantiate(manager.template, p, Quaternion.identity);
                image.name = "convolution";

                var t = new Texture2D(convolution.column(), convolution.row(), TextureFormat.ARGB32, false);
                t.filterMode = FilterMode.Point;

                ((CONTENT_ConvDisplay)image).image.GetComponent<Renderer>().material.mainTexture = t;
                convolutionDisplay[layer] = t;
            }
            for (int layer = 0; layer < valueDisplay.Length; layer++)
            {
                var p = new Vector2(1.1f + index * 2.4f, 1.1f * layer);
                var image = GameObject.Instantiate(manager.template, p, Quaternion.identity);
                image.name = "value";

                var t = new Texture2D(value.column(), value.row(), TextureFormat.ARGB32, false);
                t.filterMode = FilterMode.Point;

                ((CONTENT_ConvDisplay)image).image.GetComponent<Renderer>().material.mainTexture = t;
                valueDisplay[layer] = t;
            }
        }
        public void update(SCR_ManagerConvNet m)
        {
//            print(convolutionDisplay.GetLength(1));
            for (int row = 0; row < convolution.row(); row++)
            {
                var t = convolutionDisplay[row];
                for (int column = 0; column < convolution.column(); column++)
                {
                    var c = m.gradient.Evaluate(Mathf.InverseLerp(-2, 2, convolution[row, column]));
                    t.SetPixel(column % t.width, column / t.width, c);
//                    print((column % 3) + ", " + (column / 3));
                }
                t.Apply();
            }
            for (int layer = 0; layer < valueDisplay.Length; layer++)
            {
                var t = valueDisplay[layer];
                for (int row = 0; row < value.row(); row++)
                {
                    for (int column = 0; column < value.column(); column++)
                    {
                        var c = m.gradient.Evaluate(Mathf.InverseLerp(-2, 2, value[row, column, layer]));
                        t.SetPixel(row, column, c);
                    }
                }
                t.Apply();
            }

//            for (int row = 0; row < convolution.GetLength(0); row++)
//            {
//                var t = textures[row];
//                for (int column = 0; column < convolution.GetLength(1); column++)
//                {
//                    var c = m.gradient.Evaluate(Mathf.InverseLerp(-2, 2, convolution[row, column]));
//                    t.SetPixel(column % 3, column / 3, c);
//                }
//                t.Apply();
//            }
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
            var kernelMatrix = Core.im2col(a.value);
//            print("kernelMatrix.Print()");
//            print(kernelMatrix.Print());
//            print("b.convolution.Print()");
//            print(b.convolution.Print());
            var multiply = Core.multiply(kernelMatrix, b.convolution);
//            print(multiply.Print());
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
            layers[i].initialize(i == 0 ? null : layers[i - 1]);
            layers[i].initGraphics(this, i);
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
