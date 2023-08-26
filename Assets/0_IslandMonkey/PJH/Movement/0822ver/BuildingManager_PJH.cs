using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingManager_PJH : Singleton<BuildingManager_PJH>
{
    // 기능시설만 담긴 Building들 (building instansiate할 때 기능시설이면 여기에 추가해주어야 함.)
    public List<Building_PJH> FunctionalBuildings;

}
