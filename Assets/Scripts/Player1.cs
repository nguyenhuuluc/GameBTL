using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float fireRate;
    float m_curFireRate;
    public GameObject viewFinder;

    bool m_isShooted;

    GameObject m_viewFinderClone;

    private void Awake()
    {
        m_curFireRate = fireRate;
    }
    private void Start()
    {
        if(viewFinder)
        {
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            Shot(mousePos);
        }

        if(m_isShooted )
        {
            m_curFireRate -= Time.deltaTime;
            // do tre giua cac khung hinh
            if (m_curFireRate <= 0)
            {
                m_isShooted = false;

                m_curFireRate = fireRate;
            }
            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate);
        }
        if (m_viewFinderClone)
        {
            m_viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
        }
    }
    void Shot(Vector3 mousePos)
    {
        m_isShooted = true;
        
        Vector3 shootDir = Camera.main.transform.position - mousePos;

        shootDir.Normalize();

        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                if (hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();

                    if(bird)
                    {
                        bird.Die();
                    }
                }

            }
        }

        CineController.Ins.ShakeTrigger();
        
        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }
}
