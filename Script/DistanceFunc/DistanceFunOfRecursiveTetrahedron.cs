using UnityEngine;
using Unity.Mathematics;
using static Unity.Mathematics.math;

public class DistanceFunOfRecursiveTetrahedron : MonoBehaviour, IDistanceFunc
{
    int ITERATIONS = 8;

    float deRecursiveTetrahedron(float3 p, float3 offset, float scale) {
        float4 z = float4(p, 1);
        for (int i = 0; i < ITERATIONS; i++) {
            if (z.x + z.y < 0.0) z.xy = -z.yx;
            if (z.x + z.z < 0.0) z.xz = -z.zx;
            if (z.y + z.z < 0.0) z.zy = -z.yz;
            z *= scale;
            z.xyz -= offset * (scale - 1);
        }
        return (length(z.xyz) - 1.5f) / z.w;
    }

    // 2Dの回転行列の生成
    float2x2 rotate(in float a) {
        float s = sin(a), c = cos(a);
        return float2x2(c, s, -s, c);
    }

    // 回転のfold
    // https://www.shadertoy.com/view/Mlf3Wj
    float2 foldRotate(float2 p, in float s) {
        float a = PI / s - atan2(p.x, p.y);
        float n = PI / s;
        a = floor(a / n) * n;
        p = mul(rotate(a), p);
        return p;
    }

    float3 onRep(float3 p, float interval){
        return fmod(p, interval) - interval * 0.5f;
    }

    float _x;
    float _y;
    float _z;
    float _vecRate;
    float _scale;
    float _rotateX;
    float _rotateY;
    float _rotateZ;
    float _lp;

    public Material _material;

    public float DistanceFunction(Vector3 p) {
        _rotateX = _material.GetFloat("_rotateX");
        _rotateY = _material.GetFloat("_rotateY");
        _rotateZ = _material.GetFloat("_rotateZ");
        _x = _material.GetFloat("_x");
        _y = _material.GetFloat("_y");
        _z = _material.GetFloat("_z");
        _scale = _material.GetFloat("_scale");
        _lp = _material.GetFloat("_lp");
        _vecRate = _material.GetFloat("_vecRate");

        float3 pos = p;
        pos.yz = foldRotate(pos.yz, _rotateX);
        pos.xz = foldRotate(pos.xz, _rotateY);
        pos.xy = foldRotate(pos.xy, _rotateZ);
        return deRecursiveTetrahedron(onRep(pos, _lp), float3(_x*_vecRate, _y*_vecRate, _z*_vecRate), _scale);
    }
}
