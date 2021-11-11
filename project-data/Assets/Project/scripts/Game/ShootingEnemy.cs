using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootingEnemy : Enemy
{
    public float shootingInterval = 4f;

    private float shootingTimer;
    private player player;

    private NavMeshAgent agent;

    public float shootingdistance = 3f;
    public float chasingDistance = 12f;
    public float chasingInterval = 2f;
    public float chasingTimer;

    void Start()
    {
        player = GameObject.Find("player").GetComponent<player>();
        shootingTimer = Random.Range(0, shootingInterval);
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(player.transform.position);
    }

    void Update()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shootingdistance)
        {
            shootingTimer = shootingInterval;
            GameObject bullet = ObjectPooler.Instance.GetBullet(false);
            bullet.transform.position = this.transform.position;
            bullet.transform.forward = (player.transform.position - this.transform.position).normalized;    
        }
        chasingTimer -= Time.deltaTime;
        if (chasingTimer<= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasingDistance)
        {
            chasingTimer = chasingInterval;
            agent.SetDestination(player.transform.position);
        }
    }
    protected override void OnKill()
    {
        base.OnKill();
        agent.enabled = false;
        this.enabled = false;
        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);
    }

}
