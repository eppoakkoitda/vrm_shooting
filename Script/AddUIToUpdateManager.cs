using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddUIToUpdateManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UpdateManager.UI = gameObject;
    }
}
