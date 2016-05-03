using UnityEngine;
using System.Collections;

public class SCR_ManagerConvNet : MonoBehaviour 
{
    const int size = 4;
    const int numNeurons = 10;
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

//        public CONTENT_ConvDisplay[,] display;
        public Texture2D[] textures;

        public void initialize()
        {
            values = new float[size, size];
            valuesD = new float[size, size];
            conv = new float[size * 3, size * 3];
            convD = new float[size * 3, size * 3];
            weights = new float[numNeurons, 3 * 3];

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
        public void initGraphics(SCR_ManagerConvNet manager, int index)
        {
            textures = new Texture2D[numNeurons];

            for (int i = 0; i < textures.Length; i++)
            {
                var square = GameObject.Instantiate(manager.template);
                square.transform.localPosition = new Vector3(index * 1.1f, i * 1.1f, 0);//new Vector3(column, row) + (Vector3)offset * i;

                // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
                var texture = new Texture2D(3, 3, TextureFormat.ARGB32, false);
                texture.filterMode = FilterMode.Point;

//            // set the pixel values
//            texture.SetPixel(0, 0, new Color(0.5f, 0.5f, 0.5f, 0.5f));
//            texture.SetPixel(1, 0, Color.clear);
//            texture.SetPixel(0, 1, Color.white);
//            texture.SetPixel(1, 1, Color.black);

                // Apply all SetPixel calls
//                texture.Apply();

                // connect texture to material of GameObject this script is attached to
                square.image.GetComponent<Renderer>().material.mainTexture = texture;

                textures[i] = texture;
            }
        }
        public void update(SCR_ManagerConvNet m)
        {
            for (int row = 0; row < weights.GetLength(0); row++)
            {
                var t = textures[row];
                for (int column = 0; column < weights.GetLength(1); column++)
                {
                    var c = m.gradient.Evaluate(Mathf.InverseLerp(-2, 2, weights[row, column]));
                    t.SetPixel(column % 3, column / 3, c);
                }
                t.Apply();
            }
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
