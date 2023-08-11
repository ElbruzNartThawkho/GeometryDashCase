using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music instance;

    private void Awake()
    {
        if (instance is null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(instance);//muzi�in sahne ge�i�inden sonra yok olmamas�n� sa�lar
    }
}