using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPropagator : MonoBehaviour {
    public event Action<Collider> _OnTriggerEnter;
    public event Action<Collider> _OnTriggerExit;

    void OnTriggerEnter(Collider collider)
    {
        _OnTriggerEnter?.Invoke(collider);
    }

    void OnTriggerExit(Collider collider)
    {
        _OnTriggerExit?.Invoke(collider);
    }
}
