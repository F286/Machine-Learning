using UnityEngine;
using System.Collections;

public class SCR_ManagerConvNet : MonoBehaviour 
{
//    const int size = 4;
    const int neuronsNum = 3;

    public Gradient gradient;
    public MeshRenderer template;

    [System.Serializable]
    public class Layer
    {
        // organized in grid
        public float[,,] value;
        public float[,,] valueGradient;

        public float[,] convolution;
        public float[,] convolutionGradient;

        public MeshRenderer[,,] valueDisplay;
        public MeshRenderer[,] convolutionDisplay;

        public void initialize(Layer previous)
        {
            if (previous == null)
            {
//                value = new float[4, 4, 4];
                value = new float[3, 3, 3];
                valueGradient = new float[value.row(), value.column(), value.layer()];

                convolution = new float[0, 0];
                convolutionGradient = new float[0, 0];
            }
            else
            {
                value = new float[previous.value.row() - 2, previous.value.column() - 2, neuronsNum];
                valueGradient = new float[value.row(), value.column(), value.layer()];

                convolution = new float[neuronsNum,
                    previous.value.row() * previous.value.column() * previous.value.column()];
                convolutionGradient = new float[convolution.row(), convolution.column()];
            }
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
            set = -12;
            for (int row = 0; row < convolution.row(); row++)
            {
                for (int column = 0; column < convolution.column(); column++)
                {
                    set++;
                    convolution[row, column] = set / 10f;
                    convolutionGradient[row, column] = set / 10f;
                }
            }
        }
        public void initGraphics(SCR_ManagerConvNet manager, int index)
        {
//            valueDisplay = new Texture2D[value.layer()];
            //            convolutionDisplay = new Texture2D[convolution.row()];
            convolutionDisplay = new MeshRenderer[convolution.row(), convolution.column()];
            valueDisplay = new MeshRenderer[value.row(), value.column(), value.layer()];

            print(convolution.Print());
            print(convolution.row());

            for (int row = 0; row < convolution.row(); row++)
            {
                for (int column = 0; column < convolution.column(); column++)
                {
                    var s = GameObject.Instantiate(manager.template);
                    var layer = column / 9;
                    var c = column - layer * 9;
                    s.transform.localPosition = new Vector3(c % 3, c / 3 - row * 4, layer);
                    s.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    s.name = "conv " + row + ", " + column;
                    convolutionDisplay[row, column] = s;
                }
            }
            for (int row = 0; row < value.row(); row++)
            {
                for (int column = 0; column < value.column(); column++)
                {
                    for (int layer = 0; layer < value.column(); layer++)
                    {
                        var s = GameObject.Instantiate(manager.template);
//                        var layer = column / 9;
//                        var c = column - layer * 9;
                        s.transform.localPosition = new Vector3(4 + row, column, layer);
                        s.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        s.name = "value " + row + ", " + column;
                        valueDisplay[row, column, layer] = s;
                    }
                }
            }

//            for (int layer = 0; layer < convolutionDisplay.Length; layer++)
//            {
//                var p = new Vector2(0.0f + index * 2.4f, 1.1f * layer);
//                var image = GameObject.Instantiate(manager.template, p, Quaternion.identity);
//                image.name = "convolution";
//
//                var t = new Texture2D(3, 3, TextureFormat.ARGB32, false);
//                t.filterMode = FilterMode.Point;
//
//                ((CONTENT_ConvDisplay)image).image.GetComponent<Renderer>().material.mainTexture = t;
//                convolutionDisplay[layer] = t;
//            }
//            for (int layer = 0; layer < valueDisplay.Length; layer++)
//            {
//                var p = new Vector2(1.1f + index * 2.4f, 1.1f * layer);
//                var image = GameObject.Instantiate(manager.template, p, Quaternion.identity);
//                image.name = "value";
//
//                var t = new Texture2D(value.column(), value.row(), TextureFormat.ARGB32, false);
//                t.filterMode = FilterMode.Point;
//
//                ((CONTENT_ConvDisplay)image).image.GetComponent<Renderer>().material.mainTexture = t;
//                valueDisplay[layer] = t;
//            }
        }
        public void updateGraphics(SCR_ManagerConvNet manager)
        {
            for (int row = 0; row < convolution.row(); row++)
            {
                for (int column = 0; column < convolution.column(); column++)
                {
                    var v = convolution[row, column];
                    var c = manager.gradient.Evaluate(Mathf.InverseLerp(-2, 2, v));
                    c.a = 0.5f;
                    var s = Mathf.Clamp01(0.05f + Mathf.Abs(v / 4));
                    convolutionDisplay[row, column].material.color = c;
                    convolutionDisplay[row, column].transform.localScale = new Vector3(s, s, s);
                }
            }
            for (int row = 0; row < value.row(); row++)
            {
                for (int column = 0; column < value.column(); column++)
                {
                    for (int layer = 0; layer < value.column(); layer++)
                    {
                        var v = value[row, column, layer];
                        var c = manager.gradient.Evaluate(Mathf.InverseLerp(-2, 2, v));
                        c.a = 0.5f;
                        var s = Mathf.Clamp01(0.05f + Mathf.Abs(v / 4));
                        valueDisplay[row, column, layer].material.color = c;
                        valueDisplay[row, column, layer].transform.localScale = new Vector3(s, s, s);
                    }
                }
            }
//            for (int row = 0; row < convolution.row(); row++)
//            {
//                var t = convolutionDisplay[row];
//                for (int column = 0; column < convolution.column() / 3; column++)
//                {
////                    var c = m.gradient.Evaluate(Mathf.InverseLerp(-2, 2, convolution[row, column]));
//                    var c = new Color(convolution[row, column], convolution[row, column + 9], convolution[row, column + 18]);
//                    t.SetPixel(column % t.width, column / t.width, c);
//                }
//                t.Apply();
//            }
//            for (int layer = 0; layer < valueDisplay.Length; layer++)
//            {
//                var t = valueDisplay[layer];
//                for (int row = 0; row < value.row(); row++)
//                {
//                    for (int column = 0; column < value.column(); column++)
//                    {
//                        var c = m.gradient.Evaluate(Mathf.InverseLerp(-2, 2, value[row, column, layer]));
//                        t.SetPixel(row, column, c);
//                    }
//                }
//                t.Apply();
//            }
        }
        public static void forward(Layer a, Layer b)
        {
            var kernelMatrix = Core.im2col(a.value);
            var multiply = Core.multiply(kernelMatrix, b.convolution);
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
            layers[i].updateGraphics(this);
        }
    }
}
