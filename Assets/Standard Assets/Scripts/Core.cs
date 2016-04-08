using UnityEngine;
using System.Collections;

public static class Core 
{
    public static float Squared(this float v)
    {
        return v * v;
    }

    public static Vector4 Mask(this Vector4 value, Vector4 mask)
    {
        return new Vector4(value.x * mask.x, value.y * mask.y, value.z * mask.z, value.w * mask.w);
    }

    public static Vector4 InverseMask(this Vector4 value, Vector4 mask)
    {
        mask.x = Mathf.Max(mask.x - 1f, 0);
        mask.y = Mathf.Max(mask.y - 1f, 0);
        mask.z = Mathf.Max(mask.z - 1f, 0);
        mask.w = Mathf.Max(mask.w - 1f, 0);
        return new Vector4(value.x * mask.x, value.y * mask.y, value.z * mask.z, value.w * mask.w);
    }
    public static float MaskSum(this Vector4 value, Vector4 mask)
    {
        return value.x * mask.x + value.y * mask.y + value.z * mask.z + value.w * mask.w;
    }
}
