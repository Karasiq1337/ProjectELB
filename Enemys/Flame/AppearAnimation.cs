using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAnimation : MonoBehaviour
{
    EnemyBehevior enemyBehevior;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehevior = GetComponent<EnemyBehevior>();
        //enemyBehevior.isActive = false;
        StartCoroutine(PlayAppearAnimation());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator PlayAppearAnimation()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color targetColor = sprite.color;
        targetColor.a = 0;
        sprite.color = targetColor;
        while (sprite.color.a <= 1)
        {
            targetColor.a += 0.05f;
            sprite.color = targetColor;
            yield return new WaitForSeconds(0.1f);
        }
        //enemyBehevior.isActive = true;
    }
}
