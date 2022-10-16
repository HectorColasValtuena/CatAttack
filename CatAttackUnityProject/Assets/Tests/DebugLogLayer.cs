using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PHATASS.Utils.Extensions.LayerMaskExtensions;

public class DebugLogLayer : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMaskA;

    [SerializeField]
    private LayerMask layerMaskB;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("this layer: " + this.gameObject.layer);
        Debug.Log("this layer's footprint: " + (1 << this.gameObject.layer));
        Debug.Log("layerMaskA: " + this.layerMaskA.EToString());
        Debug.Log("layerMaskB: " + this.layerMaskB.EToString());
        Debug.Log("layerMaskA contains this: " + this.layerMaskA.EContainsLayer(this.gameObject.layer));
        Debug.Log("layerMaskB contains this: " + this.layerMaskB.EContainsLayer(this.gameObject.layer));
        Debug.Log("layerMaskA contains B: " + this.layerMaskA.EContainsLayerMask(this.layerMaskB));
        Debug.Log("layerMaskB contains A: " + this.layerMaskB.EContainsLayerMask(this.layerMaskA));
        Debug.Log("intersection A && B:" + this.layerMaskA.EIntersection(this.layerMaskB).EToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
