using System;
using UnityEngine;

[Serializable]
public abstract class SKC_RangeExtensions<T>
{
    public T glyphs;
    public T Min;
    public T Max;
    public T myString;
    public int charAmount;

    public abstract T RandomInRange();
}

[Serializable]
public class RangeInt : SKC_RangeExtensions<int> {
    public override int RandomInRange()
    {
        if (Min <= Max) return UnityEngine.Random.Range(Min, Max);
        return UnityEngine.Random.Range(Max, Min);
    }
}

[Serializable]
public class RangeFloat : SKC_RangeExtensions<float> {
    public RangeFloat()
    {
        Min = 0;
        Max = 1;
    }

    public override float RandomInRange()
    {
        if (Min <= Max) return UnityEngine.Random.Range(Min, Max);
        return UnityEngine.Random.Range(Max, Min);
    }
}

[Serializable]
public class RangeString : SKC_RangeExtensions<string> {
    public RangeString()
    {
        glyphs= "";
    }
    public override string RandomInRange()
    {
        myString = "";
        for(int i=0; i< charAmount ; i++)
        {
            myString += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }
        return myString;
    }
}

[Serializable]
public class RangeColor : SKC_RangeExtensions<Color> {
    public RangeColor()
    {
        Min = Color.HSVToRGB(0f, 0f, 0f);
        Max = Color.HSVToRGB(0.999f, 1f, 1f);
    }

    public override Color RandomInRange()
    {
        float minH, minS, minV, maxH, maxS, maxV;
        Color.RGBToHSV(Min, out minH, out minS, out minV);
        Color.RGBToHSV(Max, out maxH, out maxS, out maxV);

        return UnityEngine.Random.ColorHSV(minH, maxH, minS, maxS, minV, maxV);
    }
}