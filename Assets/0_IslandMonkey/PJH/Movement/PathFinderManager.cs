using System.Collections;
using System.Collections.Generic;
using Assets.IslandMonkey.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinderManager : Singleton<PathFinderManager>
{
    public GameObject[] originDestinations;
    public int DestinationCount
    {
        get { return destinationCount; }   // get method
        set { destinationCount = value; }  // set method
    }

    private int destinationCount; // field


    public void Awake()
    {
        destinationCount = originDestinations.Length; // field
    }
}
