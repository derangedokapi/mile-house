using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScenery : MonoBehaviour
{
    PlayerMovement player;
    public bool moveWithPlayer = true;

    [SerializeField] float backgroundScrollSpeed = 0.003f;
    Material myMaterial;
    Vector2 offSet;

    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        myMaterial = GetComponent<Renderer>().material;
    }
    void Update()
    {
        if (player.IsPlayerMoving() || !moveWithPlayer) {

            transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
            float moveSpeed = backgroundScrollSpeed;
            if (moveWithPlayer) {
                moveSpeed = moveSpeed * player.GetPlayerVelocitySign();
            }
            offSet = new Vector2(moveSpeed, 0f);
            myMaterial.mainTextureOffset += offSet * Time.deltaTime;
        }
    }
}
