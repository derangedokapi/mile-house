using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScenery : MonoBehaviour
{
    PlayerMovement player;

    [SerializeField] float backgroundScrollSpeed = 0.003f;
    Material myMaterial;
    Vector2 offSet;

    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        myMaterial = GetComponent<Renderer>().material;
        
    }
    void Update()
    {
        if (player.transform.hasChanged) {
            offSet = new Vector2(backgroundScrollSpeed, 0f);
            transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
            myMaterial.mainTextureOffset += offSet * Time.deltaTime;
        }
        
    }
}
