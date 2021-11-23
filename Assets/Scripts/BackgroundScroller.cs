using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float scrollSpeed = 0.5f;

    float calculatedScrollSpeed;
    Material material;
    Vector2 offSet;

    // Start is called before the first frame update
    private void Start() {
        
        this.calculatedScrollSpeed = this.scrollSpeed / 100;
        this.material = GetComponent<Renderer>().material;
        this.offSet = new Vector2(this.calculatedScrollSpeed, 0f);
    }

    // Update is called once per frame
    private void Update() {
        this.material.mainTextureOffset += 
            (this.offSet * Time.deltaTime);
    }
}
