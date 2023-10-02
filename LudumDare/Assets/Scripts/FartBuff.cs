using System.Collections;
using UnityEngine;

public class FartBuff : MonoBehaviour
{
    public int Stacks = 0;
    public float Inverval = 5;
    public float BaseForce = 3;
    public float ForcePerStack = 2;
    public float InvulnerabilityTime = 1;
    public ParticleSystem fartParticles;
    public static FartBuff Singleton;
    void Start()
    {
        Singleton = this;
        Stacks = 0;
        StartCoroutine(FartTime());
    }

    private IEnumerator FartTime()
    {
        yield return null;
        float lastFartTime = Time.time;
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
            if (Time.time - lastFartTime > Inverval) {
                ObjectRegistry.Singleton.Player.GetComponent<Rigidbody2D>().AddForce(ObjectRegistry.Singleton.Player.GetComponent<Rigidbody2D>().velocity.normalized*(BaseForce+Stacks*ForcePerStack), ForceMode2D.Impulse);
                lastFartTime = Time.time;
                fartParticles.Play();
                ObjectRegistry.Singleton.Player.LastFartTime = Time.time;
            }
            yield return null;
        }
    }
}