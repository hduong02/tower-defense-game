using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 3;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;
    
    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(BlockNeutral baseBlock) 
    {
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit)
        {
            InstantiateNewTower(baseBlock);
        }
        else
        {
            MoveExistingTower(baseBlock);
        }
    }

    private void InstantiateNewTower(BlockNeutral baseBlock)
    {
        Vector3 towerPos = new Vector3(baseBlock.transform.position.x, baseBlock.transform.position.y + 10, baseBlock.transform.position.z );
        var newTower = Instantiate(towerPrefab, towerPos, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;

        newTower.baseBlock = baseBlock;   
        baseBlock.isPlaceable = false;

        towerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(BlockNeutral newBaseBlock)
    {
        Vector3 newTowerPos = new Vector3(newBaseBlock.transform.position.x, newBaseBlock.transform.position.y + 10, newBaseBlock.transform.position.z );
        var oldTower = towerQueue.Dequeue();

        oldTower.baseBlock.isPlaceable = true;
        newBaseBlock.isPlaceable = false;

        oldTower.baseBlock = newBaseBlock;

        oldTower.transform.position = newTowerPos;

        towerQueue.Enqueue(oldTower);
    }


}
