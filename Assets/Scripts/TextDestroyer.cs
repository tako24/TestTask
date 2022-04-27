using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestroyer : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            Destroy(this.gameObject);
    }
}
