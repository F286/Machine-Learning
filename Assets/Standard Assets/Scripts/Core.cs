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
        var width = 3 * 3;
        var height = (values.row() - 2) * (values.column() - 2);

        var r = new float[width, height];

        for (int row = 0; row < height; row++)
        {
            var columnOffset = 1 + (row % (values.row() - 2));
            var rowOffset = 1 + (row / (values.row() - 2));

            r[0, row] = values[rowOffset - 1, columnOffset - 1];
            r[1, row] = values[rowOffset - 1, columnOffset + 0];
            r[2, row] = values[rowOffset - 1, columnOffset + 1];
            r[3, row] = values[rowOffset + 0, columnOffset - 1];
            r[4, row] = values[rowOffset + 0, columnOffset + 0];
            r[5, row] = values[rowOffset + 0, columnOffset + 1];
            r[6, row] = values[rowOffset + 1, columnOffset - 1];
            r[7, row] = values[rowOffset + 1, columnOffset + 0];
            r[8, row] = values[rowOffset + 1, columnOffset + 1];
        }
        return r;
    }
    public static float[,] im2col(float[,,] values)
    {
//        var width = 3 * 3;
//        var height = (values.row() - 2) * (values.column() - 2);
//        Debug.Log(values.Print());

        var r = new float[3 * 3 * values.layer(), (values.row() - 2) * (values.column() - 2)];

        for (int column = 0; column < r.column(); column++)
        {
            var columnOffset = 1 + (column % (values.row() - 2));
            var rowOffset = 1 + (column / (values.row() - 2));

            for (int layer = 0; layer < values.layer(); layer++)
            {
                r[layer * 9 + 0, column] = values[rowOffset - 1, columnOffset - 1, layer];
                r[layer * 9 + 1, column] = values[rowOffset - 1, columnOffset + 0, layer];
                r[layer * 9 + 2, column] = values[rowOffset - 1, columnOffset + 1, layer];
                r[layer * 9 + 3, column] = values[rowOffset + 0, columnOffset - 1, layer];
                r[layer * 9 + 4, column] = values[rowOffset + 0, columnOffset + 0, layer];
                r[layer * 9 + 5, column] = values[rowOffset + 0, columnOffset + 1, layer];
                r[layer * 9 + 6, column] = values[rowOffset + 1, columnOffset - 1, layer];
                r[layer * 9 + 7, column] = values[rowOffset + 1, columnOffset + 0, layer];
                r[layer * 9 + 8, column] = values[rowOffset + 1, columnOffset + 1, layer];   
            }
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

    public static int row(this float[,,] v)
    {
        return v.GetLength(0);
    }
    public static int column(this float[,,] v)
    {
        return v.GetLength(1);
    }
    public static int layer(this float[,,] v)
    {
        return v.GetLength(2);
    }

    public static string Print(this float[,] v)
    {
        var returnValue = "";

        for (int row = 0; row < v.GetLength(0); row++)
        {
            var _row = "";
            for (int column = 0; column < v.GetLength(1); column++)
            {
                _row += v[row, column].ToString("0.00") + " ";
            }
            returnValue += _row + '\n';
        }
        return returnValue;
    }
    public static string Print(this float[,,] v)
    {
        var returnValue = "";

        for (int row = 0; row < v.GetLength(0); row++)
        {
            var _row = "";
            for (int column = 0; column < v.GetLength(1); column++)
            {
                var _column = "";
                for (int layer = 0; layer < v.GetLength(2); layer++)
                {
                    _column += v[row, column, layer].ToString("0.0") + " ";
                }
                _row += _column + '\n';
            }
            returnValue += _row + ", " + '\n';
        }
        return returnValue;
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
