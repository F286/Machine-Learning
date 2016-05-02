using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public static class Core 
{
    public static float[,] multiply(float[,] a, float[,] b)
    {
        // row = 0, column = 1
        Assert.AreEqual(a.row(), b.column(), "Matrix A number of rows must be equal Matrix B number of columns.");

        // size of output matrix is 
        var r = new float[b.row(), a.column()];
        var multiplyLength = a.row();

        for (int row = 0; row < r.row(); row++)
        {
            for (int column = 0; column < r.column(); column++)
            {
                r[row, column] = 0;

                for (int i = 0; i < multiplyLength; i++)
                {
                    r[row, column] += a[i, column] * b[row, i];
                }
            }
        }
        return r;
    }
    public static float[,] im2col(float[,] values)
    {
//        var w = values.column();
//        var h = values.row();
        // width and height for kernel array
        var width = 3 * 3;
        var height = (values.row() - 2) * (values.column() - 2);

        var r = new float[width, height];

//        var w = values.GetLength(0);
//        Debug.Log(r.GetLength(0));
//        Debug.Log(r.GetLength(1));

        Debug.Log(height);

        for (int y = 0; y < height; y++)
        {
            var xOffset = 1 + (y % (values.row() - 2));
            var yOffset = 1 + (y / (values.row() - 2));

//            Debug.Log(xOffset + "  " + yOffset);
//            Debug.Log((xOffset - 1) + "  " + (yOffset + 1));

            r[0, y] = values[xOffset - 1, yOffset - 1];
            r[1, y] = values[xOffset + 0, yOffset - 1];
            r[2, y] = values[xOffset + 1, yOffset - 1];
            r[3, y] = values[xOffset - 1, yOffset + 0];
            r[4, y] = values[xOffset + 0, yOffset + 0];
            r[5, y] = values[xOffset + 1, yOffset + 0];
            r[6, y] = values[xOffset - 1, yOffset + 1];
            r[7, y] = values[xOffset + 0, yOffset + 1];
            r[8, y] = values[xOffset + 1, yOffset + 1];
        }
        return r;
    }

    public static int row(this float[,] v)
    {
        return v.GetLength(0);
    }
    public static int column(this float[,] v)
    {
        return v.GetLength(1);
    }

    public static string Print(this float[,] v)
    {
        var r = "";

        for (int y = 0; y < v.GetLength(1); y++)
        {
            var app = "";
            for (int x = 0; x < v.GetLength(0); x++)
            {
                app += v[x, y].ToString("0.00") + " ";
            }
            r += app + '\n';
        }
        return r;
    }

    public static IEnumerable<GameObject> FindInChildrenWithTag(this GameObject g, string tag)
    {
        foreach (var item in g.GetComponentsInChildren<Transform>())
        {
            if (item.CompareTag(tag))
            {
                yield return item.gameObject;
            }
        }
    }
    public static IEnumerable<GameObject> FindInChildrenWithTag(this GameObject g, params string[] tag)
    {
        foreach (var item in g.GetComponentsInChildren<Transform>())
        {
            for (int i = 0; i < tag.Length; i++)
            {
                if (item.CompareTag(tag[i]))
                {
                    yield return item.gameObject;
                }
            }
        }
    }
    public static IEnumerable<GameObject> FindInChildrenWithName(this GameObject g, params string[] n)
    {
        foreach (var item in g.GetComponentsInChildren<Transform>())
        {
            for (int i = 0; i < n.Length; i++)
            {
                if (item.name.Contains(n[i]))
                {
                    yield return item.gameObject;
                }
            }
        }
    }
    public static float Tanh(float v)
    {
        return (float)System.Math.Tanh(v);
    }
    public static double Tanh(double v)
    {
        return System.Math.Tanh(v);
    }
    public static float Sigmoid(float v)
    {
        return 1 / (1 + (float)System.Math.Exp(-v));
    }
    public static double Sigmoid(double v)
    {
        return 1 / (1 + System.Math.Exp(-v));
    }
    public static float Angle(this Vector3 v)
    {
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }
    public static float Squared(this float v)
    {
        return v * v;
    }

    public static double Squared(this double v)
    {
        return v * v;
    }

    public static Vector4 Mask(this Vector4 value, Vector4 mask)
    {
        return new Vector4(value.x * mask.x, value.y * mask.y, value.z * mask.z, value.w * mask.w);
    }

    public static Vector4 InverseMask(this Vector4 value, Vector4 mask)
    {
        mask.x += (0.5f - mask.x) * 2;
        mask.y += (0.5f - mask.y) * 2;
        mask.z += (0.5f - mask.z) * 2;
        mask.w += (0.5f - mask.w) * 2;
        return new Vector4(value.x * mask.x, value.y * mask.y, value.z * mask.z, value.w * mask.w);
    }
    public static float MaskSum(this Vector4 value, Vector4 mask)
    {
        return value.x * mask.x + value.y * mask.y + value.z * mask.z + value.w * mask.w;
    }
}
