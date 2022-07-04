using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer_ItemController : MonoBehaviour
{
    public ParticleSystem thunderHit;
    public Vector3 posStop;
    public IEnumerator TouchHammer() {
        Pikachu_IngameManager.instance.thunderAppear.Play();
        yield return Yielders.Get(0.5f);
        Pikachu_IngameManager.instance.thunderAppear.Stop();
        yield return Yielders.Get(0.5f);
        LeanTween.move(gameObject, posStop, 1f);
        yield return Yielders.Get(1f);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.position = new Vector3(0f,-2.26f,-5f);
        LeanTween.rotateX(gameObject, -40, 0.5f);
        LeanTween.scale(gameObject, new Vector3(5, 5, 5), 0.5f);
        yield return Yielders.Get(0.5f);
        LeanTween.rotateX(gameObject, 0, 0.1f);
        LeanTween.scale(gameObject, new Vector3(4, 4, 4), 0.1f);
        yield return Yielders.Get(0.1f);
        Pikachu_IngameManager.instance.Shake();
        Pikachu_IngameManager.instance.ShakeBg();
        thunderHit.Play();
        yield return Yielders.Get(0.1f);
        thunderHit.Stop();

        LeanTween.rotateX(gameObject, -40, 0.3f);
        LeanTween.scale(gameObject, new Vector3(5, 5, 5), 0.3f);
        yield return Yielders.Get(0.3f);
        LeanTween.rotateX(gameObject, 0, 0.1f);
        LeanTween.scale(gameObject, new Vector3(4, 4, 4), 0.1f);
        yield return Yielders.Get(0.1f);
        Pikachu_IngameManager.instance.Shake();
        Pikachu_IngameManager.instance.ShakeBg();
        thunderHit.Play();
        yield return Yielders.Get(0.1f);
        thunderHit.Stop();

        LeanTween.rotateX(gameObject, -40, 0.2f);
        LeanTween.rotateY(gameObject, 40, 0.2f);
        yield return Yielders.Get(0.3f);

        LeanTween.rotateX(gameObject, -40, 0.2f);
        LeanTween.scale(gameObject, new Vector3(5, 5, 5), 0.2f);
        yield return Yielders.Get(0.2f);
        LeanTween.rotateX(gameObject, 0, 0.1f);
        LeanTween.scale(gameObject, new Vector3(4, 4, 4), 0.1f);
        yield return Yielders.Get(0.1f);
        Pikachu_IngameManager.instance.Shake();
        Pikachu_IngameManager.instance.ShakeBg();
        thunderHit.Play();
        yield return Yielders.Get(0.1f);
        thunderHit.Stop();

        LeanTween.rotateX(gameObject, -40, 0.2f);
        LeanTween.rotateY(gameObject, -40, 0.2f);
        LeanTween.scale(gameObject, new Vector3(5, 5, 5), 0.2f);
        yield return Yielders.Get(0.2f);
        LeanTween.rotateX(gameObject, 0, 0.1f);
        LeanTween.scale(gameObject, new Vector3(4, 4, 4), 0.1f);
        yield return Yielders.Get(0.1f);
        Pikachu_IngameManager.instance.Shake();
        Pikachu_IngameManager.instance.ShakeBg();
        thunderHit.Play();
        yield return Yielders.Get(0.1f);
        thunderHit.Stop();

        
        LeanTween.rotateX(gameObject, -40, 0.5f);
        LeanTween.rotateY(gameObject, 0, 0.5f);
        LeanTween.scale(gameObject, new Vector3(7, 7,7), 0.5f);
        yield return Yielders.Get(0.5f);
        thunderHit.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        LeanTween.rotateX(gameObject, 0, 0.1f);
        LeanTween.scale(gameObject, new Vector3(4, 4, 4), 0.1f);
        yield return Yielders.Get(0.1f);
        Pikachu_IngameManager.instance.Shake();
        Pikachu_IngameManager.instance.ShakeBg();
        thunderHit.Play();
        yield return Yielders.Get(0.1f);
        thunderHit.Stop();

        yield return Yielders.Get(0.3f);
        Destroy(gameObject);
        Pikachu_IngameManager.instance.hammerWall.SetActive(false);
        Pikachu_IngameManager.instance.state = Pikachu_IngameManager.StateGame.Playing;
    }
    public void Init() {
        thunderHit.Stop();
        gameObject.transform.position = Pikachu_IngameManager.instance.thunderAppear.transform.position;
        StartCoroutine(TouchHammer());
    }
}
