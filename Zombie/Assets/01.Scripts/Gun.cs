using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public AudioClip audioClip; // �߻� �Ҹ�
    public AudioClip reloadClip; // ������ �Ҹ�

    public float damage = 25; // ���ݷ�
    public int magCapacity = 25; // źâ �뷮

    public float timeBetFire = 0.12f; // ź�� �߻� ����
    public float trloadTime = 1.8f; // ������ �ҿ� �ð�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
