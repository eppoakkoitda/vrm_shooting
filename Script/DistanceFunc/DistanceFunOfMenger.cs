using UnityEngine;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class DistanceFunOfMenger : MonoBehaviour, IDistanceFunc
{
    private float deMengerSponge2(float3 p, float3 offset, float scale) {
        float4 z = float4(p, 1);
        for (int i = 0; i < 8; i++) {
            z = abs(z);
            if (z.x < z.y) z.xy = z.yx;
            if (z.x < z.z) z.xz = z.zx;
            if (z.y < z.z) z.yz = z.zy;
            z *= scale;
            z.xyz -= offset * (scale - 1.0f);
            if (z.z < -0.5 * offset.z * (scale - 1.0f))
                z.z += offset.z * (scale - 1.0f);
        }
        return (length(max(abs(z.xyz) - float3(1.0f, 1.0f, 1.0f), 0.0f))) / z.w;
    }

    // 2Dの回転行列の生成
    private float2x2 rotate(in float a) {
        float s = sin(a), c = cos(a);
        return float2x2(c, s, -s, c);
    }

    // 回転のfold
    // https://www.shadertoy.com/view/Mlf3Wj
    private float2 foldRotate(float2 p, in float s) {
        float a = PI / s - atan2(p.x, p.y);
        float n = PI / s;
        a = floor(a / n) * n;
        p = mul(rotate(a), p);
        return p;
    }

    float3 onRep(float3 p, float interval){
        return fmod(p, interval) - interval * 0.5f;
    }
    private float _x;
    private float _y;
    private float _z;
    private float _scale;
    private float _rotateX;
    private float _rotateY;
    private float _rotateZ;
    public Material _material;   

    public float DistanceFunction(Vector3 p)
    {
        _rotateX = _material.GetFloat("_rotateX");
        _rotateY = _material.GetFloat("_rotateY");
        _rotateZ = _material.GetFloat("_rotateZ");
        _x = _material.GetFloat("_x");
        _y = _material.GetFloat("_y");
        _z = _material.GetFloat("_z");
        _scale = _material.GetFloat("_scale");

        float3 pos = p;
        pos.yz = foldRotate(pos.yz, _rotateX);
        pos.xz = foldRotate(pos.xz, _rotateY);
        pos.xy = foldRotate(pos.xy, _rotateZ);

        return  deMengerSponge2(onRep(pos, 4.0f), float3(_x, _y, _z), _scale);
    }
}
