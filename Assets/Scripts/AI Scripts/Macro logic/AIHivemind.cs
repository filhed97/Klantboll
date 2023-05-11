using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActiveRagdoll;

public class AIHivemind : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] public GameObject Player;

    [Header("Friendly Bots")]
    [SerializeField] public GameObject Bot0;
    [SerializeField] public GameObject Bot1;

    [Header("Enemy Bots")]
    [SerializeField] public GameObject Bot2;
    [SerializeField] public GameObject Bot3;
    [SerializeField] public GameObject Bot4;

    [Header("Ball")]
    [SerializeField] public GameObject Ball;

    //List of targetable gameobjects (such as powerups, opponents, or the ball).
    private List<GameObject> Team1Bots;
    private List<GameObject> Team2Bots;
    private List<GameObject> Team1ValidTargets;
    private List<GameObject> Team2ValidTargets;


    // Start is called before the first frame update
    void Start()
    {
        Team1Bots = new List<GameObject> { Bot0, Bot1 };
        Team2Bots = new List<GameObject> { Bot2, Bot3, Bot4 };

        Team1ValidTargets = new List<GameObject> { Bot2, Bot3, Bot4 };
        Team2ValidTargets = new List<GameObject> { Player, Bot0, Bot1 };
    }

    // Update is called once per frame
    void Update()
    {
        assignTargets(Team1Bots, Team2ValidTargets);
        assignTargets(Team2Bots, Team1ValidTargets);
    }

    private void assignTargets(List<GameObject> team, List<GameObject> targets)
    {
        //set targets for each bot
        int ballChaser = getClosestToBall(team);
        foreach (GameObject bot in team)
        {
            if (team.IndexOf(bot) == ballChaser) 
            { 
                bot.GetComponent<JensAiForcedMovement>().target = Ball.transform;
                bot.GetComponent<JensAiForcedMovement>().targetIsBall = true;
            }
            else
            {
                GameObject closestTarget = getClosestOpponent(bot,targets);
                setTarget(bot, closestTarget);
            }
        }
    }

    private void setTarget(GameObject actor, GameObject target)
    {
        Transform targetTransform = target.transform.Find("Physical").transform;
        actor.GetComponent<JensAiForcedMovement>().target = targetTransform;
        actor.GetComponent<JensAiForcedMovement>().targetIsBall = false;
    }

    //get index of whichever bot is closest to the ball
    private int getClosestToBall(List<GameObject> team)
    {
        int closest = 0;
        float shortestDistance = float.MaxValue;
        for(int i = 0; i < team.Count; i++)
        {
            Vector3 botPosition = team[i].transform.Find("Physical").transform.position;
            Vector3 ballPosition = Ball.transform.position;
            float distance = Vector3.Distance(botPosition, ballPosition);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = i;
            }
        }
        return closest;
    }

    private GameObject getClosestOpponent(GameObject actor, List<GameObject> validTargets)
    {
        Vector3 botPosition = actor.transform.Find("Physical").transform.position;

        int closest = 0;
        float shortestDistance = float.MaxValue;
        for (int i = 0; i < validTargets.Count; i++)
        {
            Vector3 targetPosition = validTargets[i].transform.Find("Physical").transform.position;
            float distance = Vector3.Distance(botPosition, targetPosition);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = i;
            }
        }
        return validTargets[closest];
    }
}
