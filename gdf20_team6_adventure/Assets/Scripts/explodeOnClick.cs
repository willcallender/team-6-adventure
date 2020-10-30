using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnClick : MonoBehaviour
{
    public ParticleSystem ps;
    private void OnMouseDown() {
        ps.Play();
        Renderer renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
}
