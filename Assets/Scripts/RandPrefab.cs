using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPrefab : MonoBehaviour
{
    // Tucker Burke
    // Local variables *******************************************************************************

    public int          numVariants = 2;                        // Number of prefabs to choose from    
    private GameObject  selectedPrefab;                         // The randomly choosen prefab
    public GameObject[] possiblePrefabs = new GameObject[2];    // Array of all varations
    System.Random rand = new System.Random();                   // For generating random numbers

    public GameObject PickPrefab() //*****************************************************************
    { 
        // Pick a random number based on the number of variations
        int index = rand.Next(numVariants);

        // Pluck from array of prefabs
        selectedPrefab = possiblePrefabs[index];

        // Return the prefab to be instantiated
        return selectedPrefab;

    }// END PickPrefab()
}
