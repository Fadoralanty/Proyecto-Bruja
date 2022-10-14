using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> _bag = new List<GameObject>();
    [SerializeField] GameObject Inv;
    bool _bagActivate;
    [SerializeField] GameObject _selector;
    int ID;

    // Update is called once per frame
    void Update()
    {
        Navegation();
        if(_bagActivate)
        {
            Inv.SetActive(true);
        }
        else
        {
            Inv.SetActive(false);
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            _bagActivate = !_bagActivate;
            //Hacer una funcion que llame a activar y desactivar el inventario
            //Las 2 cosas que estan arriba
        }
    }

    public void Navegation()
    {
        if(Input.GetKeyDown(KeyCode.D) && ID<_bag.Count-1)
        {
            ID++;
        }
        if(Input.GetKeyDown(KeyCode.A) && ID>0)
        {
            ID--;
        }
        if(Input.GetKeyDown(KeyCode.W) && ID>3)
        {
            ID -= 4;
        }
        if(Input.GetKeyDown(KeyCode.S) && ID<8)
        {
            ID += 4;
        }
        _selector.transform.position = _bag[ID].transform.position;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("item"))
        {
            for (int i = 0; i < _bag.Count; i++)
            {
                if(_bag[i].GetComponent<Image>().enabled == false)
                {
                    _bag[i].GetComponent<Image>().enabled = true;
                    _bag[i].GetComponent<Image>().sprite = coll.GetComponent<SpriteRenderer>().sprite;
                    Debug.Log("i touched");
                    break;
                }
            }
        }
    }
}
