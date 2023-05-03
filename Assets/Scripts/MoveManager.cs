using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveManager : MonoBehaviour
{
    public static MoveManager Instance{ get; private set;}
    [SerializeField] private List<Player> players;
    public const float WAY_TIME = 4F;
    
    private void Awake() 
    {
        Instance = this; 
        Time.timeScale = 1f;
    }
    public void WaysPassed()
    {
        foreach(var pl in players)
        {
            if(!pl.GetToExit) return;
        }

        SceneBehavior.Instance.PlayersWon();
    }
    public void CheckAllWays()
    {
        foreach(var pl in players)
        {
            if(!pl.HaveWay) return;
        }

        foreach(var pl in players)
        {
            pl.Move = true;
            StartCoroutine(pl.MoveCharacter());
        }
    }
}
