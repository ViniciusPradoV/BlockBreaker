using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // State
    [SerializeField] int breakableBlocks; // Serialized for debbuging

    // Cached Reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void CountBrokenBlock()
    {
        breakableBlocks--;

        if(breakableBlocks == 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
