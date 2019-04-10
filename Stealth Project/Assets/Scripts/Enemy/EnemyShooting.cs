using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float maximumDamage = 120f;
    public float minimumDamage = 45f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private Transform player;
    private PlayerHealth playerHealth;
    private bool shooting;
    private float scaledDamage;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        hash = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<HashIDs>();

        laserShotLine.enabled = false;
        laserShotLight.intensity = 0;

        scaledDamage = maximumDamage - minimumDamage;
    }

    private void Update()
    {
        //射击动画Curve中shot的参数值
        float shot = anim.GetFloat(hash.shotFloat);

        if (shot > 0.5f && !shooting)
        {
            Shoot();
        }

        if (shot < 0.5f)
        {
            shooting = false;
            laserShotLine.enabled = false;
        }

        laserShotLight.intensity = Mathf.Lerp(laserShotLight.intensity, 0, fadeSpeed * Time.deltaTime);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        float aimWeight = anim.GetFloat(hash.aimWeightFloat);
        anim.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * 1.5f);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, aimWeight);
    }

    //根据玩家和敌人的距离来判断伤害比例的范围，离得越近伤害越高
    void Shoot()
    {
        shooting = true;
        //计算触发器半径和(玩家与敌人距离的差)再除以触发器半径
        float fractionanDistance = (col.radius - Vector3.Distance(transform.position, player.position)) / col.radius;
        //然后计算射击伤害=伤害浮动范围*刚计算的分数+最小伤害值
        float damage = scaledDamage * fractionanDistance + minimumDamage;
        playerHealth.TakeDamage(damage);
        ShotEffects();
    }

    void ShotEffects()
    {
        laserShotLine.SetPosition(0, laserShotLine.transform.position);
        laserShotLine.SetPosition(1, player.position + Vector3.up * 1.5f);
        laserShotLine.enabled = true;
        laserShotLight.intensity = flashIntensity;
        AudioSource.PlayClipAtPoint(shotClip, laserShotLight.transform.position);
    }

}
