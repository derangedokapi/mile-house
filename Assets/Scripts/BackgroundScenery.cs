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
        if (player.IsPlayerMoving()) {

            transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
            offSet = new Vector2(backgroundScrollSpeed * player.GetPlayerVelocitySign(), 0f);
            myMaterial.mainTextureOffset += offSet * Time.deltaTime;
        }
    }
}
