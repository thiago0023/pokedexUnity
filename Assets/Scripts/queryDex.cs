﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class queryDex : MonoBehaviour
{
    public Filter filter = Filter.AtoZ;

    public GameObject ClickMask;

    public Fade ListFade;
    public void OrderBy(int i){
        PokeDataFromJSON.dex.ClickMask.SetActive(true);
        
        StartCoroutine(Sort());
    }

    IEnumerator Sort(){
        GameObject button = EventSystem.current.currentSelectedGameObject;
        ListFade.FadeOut();
        yield return new WaitUntil(()=>ListFade.fadedOut);
        yield return null;
        PokeDataFromJSON.dex.content.position = new Vector3(PokeDataFromJSON.dex.content.position.x,0, PokeDataFromJSON.dex.content.position.z);
        IEnumerable<PokemonData> query;
        switch (filter)
        {
            case Filter.AtoZ:
                Debug.Log("teste");
                query = PokeDataFromJSON.dex.pokemon.OrderBy(poke => poke.N);
                PokeDataFromJSON.dex.DrawReorderedDex(query);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "Z - A";
                
                filter = Filter.ZtoA;
                    
                break;
                
            case Filter.ZtoA:
                Debug.Log("teste2");
                query = PokeDataFromJSON.dex.pokemon.OrderByDescending(poke => poke.N);
                PokeDataFromJSON.dex.DrawReorderedDex(query);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "N. Asc.";
                filter = Filter.NumberAsc;                    
                
                 break;

            case Filter.NumberAsc:
                Debug.Log("teste3");
                query = PokeDataFromJSON.dex.pokemon.OrderBy(poke => poke.id);
                PokeDataFromJSON.dex.DrawReorderedDex(query);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "N. Desc";
                filter = Filter.NumberDesc;
                    
                
                break;
            case Filter.NumberDesc:
                Debug.Log("teste4");
                query = PokeDataFromJSON.dex.pokemon.OrderByDescending(poke => poke.id);
                PokeDataFromJSON.dex.DrawReorderedDex(query);
                button.transform.GetChild(0).GetComponent<TMP_Text>().text = "A - Z";
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<Button>().onClick.AddListener(()=> OrderBy(0));
                filter = Filter.AtoZ;
                    
                    
                
                break;
                
        }

        ListFade.FadeIn();
        ListFade.fadedOut = false;
        yield return new WaitUntil(()=>ListFade.fadedIn);
        ListFade.fadedIn = false;
        PokeDataFromJSON.dex.ClickMask.SetActive(false);
        
    }

    
}

public enum Filter{
    AtoZ=0, ZtoA=1, NumberAsc=2, NumberDesc=3 
}
