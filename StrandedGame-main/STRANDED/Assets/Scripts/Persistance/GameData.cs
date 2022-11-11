using System;
using System.Collections.Generic;

[Serializable]

public class GameData
{
    public List<SlotData> SlotDatas;
    public List<PlaceableData> PlaceableDatas;
    public List<InspectableData> InspectableDatas;

    public GameData()
    {
        SlotDatas = new List<SlotData>();
        PlaceableDatas = new List<PlaceableData>();
        InspectableDatas = new List<InspectableData>();
    }
}
