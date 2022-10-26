using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� ����
public class Gun : MonoBehaviour
{
    // ���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }

    public State state  { get; private set; } // ���� ���� ����

    public Transform fireTransform; // ź���� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffect; // �ѱ� ȭ�� ȿ��
    public ParticleSystem shellEffect; // ź�� ���� ȿ��

    private LineRenderer bulletLineRenderer; // ź�� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer; // �� �Ҹ� �����

    public GunData gunData; // ���� ���� ������

    private float fireDistance = 50f; // �����Ÿ�

    public int ammoRemain = 100; // ���� ��ü ź��
    public int magAmmo; // ���� źâ�� ���� �ִ� ź��

    private void Awake()
    {
        // ����� ������Ʈ�� ���� ��������
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        // ����� ���� �� ���� ����
        bulletLineRenderer.positionCount = 2;
        // ���� �������� ��Ȱ��ȭ
        bulletLineRenderer.enabled = false;
    }

    private void OnEnable()
    {

        // ��ü ���� ź�� ���� �ʱ�ȭ
        ammoRemain = gunData.startAmmoRemain;
        // ���� źâ�� ���� ä���
        magAmmo = gunData.magCapacity;

        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ����������  ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    // �߻�õ�
    public void Fire()
    {
        // ���� ���°� �߻� ������ ����
        // && ������ �� �߻� �������� timeBetFire �̻��� �ð��� ����
        if (state == State.Ready && Time.time >= lastFireTime + gunData.timeBetfire)
        {
            // ������ �� �߻� ���� ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            shot();
        }

        // ���� �߻� ó��
        private void Shot()
        {
            // ����ĳ��Ʈ�� ���� �浹 ������ �����ϴ� �����̳�
            RaycastHit hit;
            // ź���� ���� ���� ������ ����
            Vector3 hitPosition = Vector3.zero;

            // ����ĳ��Ʈ(���� ����, ����, �浹 ���� �����̳�, �����Ÿ�)
            if (Physics.Raycast(fireTransform.position, fireTransform.forward.magnitude\, out hit, fireDistance))
            {
                // ���̰� � ��ü�� �浹�� ���

                // �浹�� �������κ��� IDamageable ������Ʈ �������� �õ�
                IDamageable target = hit.collider.GetComponent<IDamageable>();

                // �������κ��� IDamageable ������Ʈ�� �������� �� �������ٸ�
                if (target != null)
                {
                    // ������ OnDamage �Լ��� ������� ���濡 ����� �ֱ�
                    target.OnDamage(gunData.damage, hit, point, hit.normal);
                }
                // ���̰� �浹�� ��ġ ����
                hitPosition = hit.point;
            }
            else
            {
                // ���̰� �ٸ� ��ü�� �浹���� �ʾҴٸ�
                // ź���� �ִ� �����Ÿ����� ���ư��� ���� ��ġ�� �浹 ��ġ�� ���
                hitPosition = fireTransform.position + fireTransform.forward * fireDistance;
            }

            // �߻� ����Ʈ ��� ����
            StartCoroutine(ShotEffect(hitPosition));

            // ���� ź�� ���� -1
            magAmmo--;
            if (magAmmo <= 0)
            {
                // źâ�� ���� ź���� ���ٸ� ���� ���� ���¸� Empty�� ����
                state = State.Empty;
            }
        }
   
      // �߻� ����Ʈ�� �Ҹ���

    