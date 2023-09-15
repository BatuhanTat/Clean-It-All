using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarCleaner : MonoBehaviour
{
    public static EarCleaner instance { get; private set; }

    [SerializeField] List<GameObject> dirtyObjects = new List<GameObject>();

    private LimitedMove limitedMove;
    private DirtCleaner dirtCleaner;
    private int cleanedObjects = 0;




    private List<BoxCollider> dirtList = new List<BoxCollider>();

    private void Start()
    {
        limitedMove = GetComponent<LimitedMove>();
        dirtCleaner = GetComponent<DirtCleaner>();
    }

    public void IncreaseCleanedObjects()
    {
        dirtyObjects[cleanedObjects].GetComponentInChildren<VFX_Handler>().PlayEffect();
        dirtyObjects[cleanedObjects].GetComponent<TargetMover>().BacktoInitialPosition(0.6f);
        cleanedObjects++;
        if (cleanedObjects >= dirtyObjects.Count)
        {
            GameManager.instance.CompleteLevel();

            Debug.Log("GameManager complete");
        }

        else if (dirtyObjects[cleanedObjects] != null)
        {
            limitedMove.UpdateTargetObject(dirtyObjects[cleanedObjects]);
            //Debug.Log(dirtyObjects[cleanedObjects]);

            dirtyObjects[cleanedObjects].GetComponent<TargetMover>().doMove = true;

            GameObject dirtParent = dirtyObjects[cleanedObjects].transform.Find("Dirt Objects").gameObject;
            dirtCleaner.UpdateDirtList(dirtParent);
        }
    }

}
