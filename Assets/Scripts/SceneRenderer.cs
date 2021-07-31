using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRenderer : MonoBehaviour
{
    PlayerMovement player;
    [Header ("Render Reference")]
    public List<float> springTreePositions = new List<float>();

    [Header ("Middle Ground Objects")]
    public GameObject springTree;
    public GameObject springTreeParent;
    public int springTreeUpperLimit = 10;
    public float springTreeMinDistance = 1f;
    public float springTreeMaxDistance = 2f;
    public float starterAmountToSubtract = 20f;

    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        AdjustRender();
    }

    private void AdjustRender() {
        if (player.transform.hasChanged) {
            float playerX = player.GetPlayerXPos();
            // add onto the spring tree list
            float lastPos = playerX - starterAmountToSubtract;
            while (lastPos < (playerX + starterAmountToSubtract)) {
                if (springTreePositions.Count > 0) {
                    lastPos = springTreePositions[springTreePositions.Count - 1];
                }
                float newPos = lastPos + Random.Range(springTreeMinDistance,springTreeMaxDistance);
                springTreePositions.Add(newPos);
            }
            springTreePositions.Sort();
            RenderObjects(springTree,springTreePositions);
           // Debug.Log("player at "+playerX+" changed? "+player.transform.hasChanged);
            
        }
    }

    private void RenderObjects(GameObject obj, List<float> positionList) {
        Debug.Log("rendering "+obj+" last position "+positionList[positionList.Count - 1]);
        for (int i = 0; i < positionList.Count; i++) {
            GameObject newTree = Instantiate(obj, new Vector3(positionList[i],player.transform.position.y,player.transform.position.z),Quaternion.identity);
            newTree.transform.parent = springTreeParent.transform;
        }
        
    }
}
