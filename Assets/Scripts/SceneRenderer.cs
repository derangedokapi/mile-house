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
    public float springTreeStarterY = 1.67f;
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
            Debug.Log("going to render tree structure");

            float playerX = player.GetPlayerXPos();
            // add onto the spring tree list
            float lastPos = playerX - starterAmountToSubtract;
            if (springTreePositions.Count > 0) {
                lastPos = springTreePositions[springTreePositions.Count - 1];
            }
            while (lastPos < (playerX + starterAmountToSubtract)) {
                if (springTreePositions.Count > 0) {
                    lastPos = springTreePositions[springTreePositions.Count - 1];
                }
                float newPos = lastPos + Random.Range(springTreeMinDistance,springTreeMaxDistance);
                springTreePositions.Add(newPos);
            }
            springTreePositions.Sort();
            
            List<float> springTreesToRender = new List<float>();
            
            foreach (float treeFloat in springTreePositions) {
                float lowerBound = playerX - starterAmountToSubtract;
                float upperBound = playerX + starterAmountToSubtract;
                if ((treeFloat > lowerBound) && (treeFloat < upperBound)) {
                    springTreesToRender.Add(treeFloat);
                } else {
                    Destroy(GameObject.Find(treeFloat.ToString()));
                }
            }
            
            RenderObjects(springTree,springTreesToRender);
           // Debug.Log("player at "+playerX+" changed? "+player.transform.hasChanged);
            
        }
    }

    private void RenderObjects(GameObject obj, List<float> positionList) {
        Debug.Log("rendering "+obj+" last position "+positionList[positionList.Count - 1]);
        for (int i = 0; i < positionList.Count; i++) {
            if (!GameObject.Find(positionList[i].ToString())) {
                float newY = springTreeStarterY + Random.Range(-0.2f,0.2f);
                GameObject newTree = Instantiate(obj, new Vector3(positionList[i],newY,player.transform.position.z),Quaternion.identity);
                newTree.transform.parent = springTreeParent.transform;
                newTree.name = positionList[i].ToString();
            } else {
                Debug.Log(" already exists");
            } 
        }
        player.transform.hasChanged = false;
    }
}
