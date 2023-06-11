using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNRD;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCSO npcSO;
    [SerializeField] private SerializableInterface<IQuestEventChannelSO> _questEventChannel;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(npcSO.isQuestGiver)
            {
                // TODO: give quest here, QuestManager will handle finding quest given info
                return;
            }
        }
    }
}
