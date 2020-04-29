using UnityEngine;

/// <summary>
/// Mathfの拡張クラス
/// </summary>
public static class Vector3Extension{

  /// <summary>
  /// 偶数に丸める(小数点二桁)
  /// </summary>
  public static Vector3 Round(this Vector3 vec) {
    vec *= 100;
    vec.x = Mathf.Round(vec.x);
    vec.y = Mathf.Round(vec.y);
    vec.z = Mathf.Round(vec.z);
    vec /= 100;
    return vec;
  }  

  /// <summary>
  /// 切り捨て(小数点1桁)
  /// </summary>
  public static Vector3 Floor(this Vector3 vec) {
    vec *= 10;
    vec.x = Mathf.Floor(vec.x);
    vec.y = Mathf.Floor(vec.y);
    vec.z = Mathf.Floor(vec.z);
    vec /= 10;
    return vec;
  }  

}
