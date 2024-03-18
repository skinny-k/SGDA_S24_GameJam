using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A list of quests. An enum is used instead of a string to prevent errors with typos. Try to keep quest labels short and sweet.
public enum Quest
{
    None,
    WrangleCows,
    SaveCat,
    Fish,
    SitTown,
    SitWoods,
    SitPond,
    PlayTag,
    PetDog,
    TeaParty,
    Fetch
}

// Represents the actual status of the quest. Do not edit this enum without approval and oversight from a programmer
public enum QuestStatus
{
    NotStarted,
    InProgress,
    Completed,
    Failed
}
