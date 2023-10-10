using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] public MushroomType Type;
    [SerializeField] public Sprite Sprite;
    [SerializeField] public bool IsEdible;
	public float lifetime = 10.5f;

    void Start()
    {
		Destroy(gameObject, lifetime);
    }

    public static string Name(MushroomType type)
    {
        return type switch
        {
            MushroomType.brown_cap_boletus => "Подберезовик",
            MushroomType.cep => "Боровик",
            MushroomType.moss_fly_mushroom => "Моховик",
            MushroomType.oily_mushroom => "Маслёнок",
            MushroomType.orange_cap_boletus => "Подосиновик",
            MushroomType.saffron_milk_cap => "Рыжик",
            MushroomType.yellow_mushroom => "Лисичка",
            MushroomType.amanita => "Мухомор",
            MushroomType.umbrella_mushroom => "Бледная поганка",
            _ => "Не гриб",
        };
    }

    public static int Price(MushroomType type)
    {
        return type switch
        {
            MushroomType.brown_cap_boletus => 1000,
            MushroomType.cep => 5000,
            MushroomType.moss_fly_mushroom => 2500,
            MushroomType.oily_mushroom => 900,
            MushroomType.orange_cap_boletus => 1500,
            MushroomType.saffron_milk_cap => 600,
            MushroomType.yellow_mushroom => 300,
            MushroomType.amanita => 10000,
            MushroomType.umbrella_mushroom => 99900,
            _ => 0,
        };
    }
}

public enum MushroomType
{
    brown_cap_boletus,
    cep,
    moss_fly_mushroom,
    oily_mushroom,
    orange_cap_boletus,
    saffron_milk_cap,
    yellow_mushroom,
    amanita,
    umbrella_mushroom
}
