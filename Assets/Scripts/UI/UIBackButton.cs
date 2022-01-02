using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackButton : MonoBehaviour
{
    PlayerMovement player;
    CanvasGroup canvasGroup;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsPlayerMoving()) {
            float playerX = player.GetPlayerXPos();
            if (playerX <= 0) {
                canvasGroup.alpha = 0;
                canvasGroup.blocksRaycasts = false;
            } else {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
            }
        }
    }
}
