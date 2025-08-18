using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> WinObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        DetermineWinPoint();
    }

    private void DetermineWinPoint()
    {
        int index = Random.Range(0, WinObjects.Count);
        WinObjects[index].SetActive(true);
    }
}
