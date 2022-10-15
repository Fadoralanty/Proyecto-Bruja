using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<GameObject> _bag = new List<GameObject>();
    [SerializeField] GameObject[] Inv;
    [SerializeField] GameObject _gameObject;
    bool _bagActivate;
    [SerializeField] GameObject _selector;
    int ID;

    [SerializeField] List<Image> _equipo = new List<Image>();
    int _ID_equipo;
    int fases_inv;

    [SerializeField] GameObject _options;
    [SerializeField] Image[] _selection;
    [SerializeField] Sprite[] selection_sprite;
    int _ID_Select;

    void Update()
    {
        Navegation();
        if(_bagActivate)
        {
            Inv[0].SetActive(true);
        }
        else
        {
            Inv[0].SetActive(false);
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            _bagActivate = !_bagActivate;
            fases_inv = 0;
            //Hacer una funcion que llame a activar y desactivar el inventario
            //Las 2 cosas que estan arriba
        }
    }

    public void Navegation()
    {
        
        switch (fases_inv)
        {
            case 0:
                _selector.SetActive(true);
                _options.SetActive(false);

                Inv[1].SetActive(false);
                if (Input.GetKeyDown(KeyCode.W) && _ID_equipo > 0)
                {
                    _ID_equipo--;
                }
                if(Input.GetKeyDown(KeyCode.S) && _ID_equipo < _equipo.Count-1)
                {
                    _ID_equipo++;
                }
                _selector.transform.position = _equipo[_ID_equipo].transform.position;

                if(Input.GetKeyDown(KeyCode.F) && _bagActivate)
                {
                    fases_inv = 1;
                }
                break;

                case 1:
                _selector.SetActive(true);
                _options.SetActive(false);

                if (Input.GetKeyDown(KeyCode.F) && _bag[ID].GetComponent<Image>().enabled == true)
                {
                    fases_inv = 2;
                }
                Inv[1].SetActive(true);
                if (Input.GetKeyDown(KeyCode.D) && ID < _bag.Count - 1)
                {
                    ID++;
                }
                if (Input.GetKeyDown(KeyCode.A) && ID > 0)
                {
                    ID--;
                }
                if (Input.GetKeyDown(KeyCode.W) && ID > 3)
                {
                    ID -= 4;
                }
                if (Input.GetKeyDown(KeyCode.S) && ID < 8)
                {
                    ID += 4;
                }
                _selector.transform.position = _bag[ID].transform.position;

                if(Input.GetKeyDown(KeyCode.G) && _bagActivate)
                {
                    fases_inv = 0;
                }
                break;

            case 2:
                if(Input.GetKeyDown(KeyCode.G))
                {
                    fases_inv = 1;
                }
                _options.SetActive(true);
                _options.transform.position = _bag[ID].transform.position;

                _selector.SetActive(false);

                if(Input.GetKeyDown(KeyCode.W) && _ID_Select > 0)
                {
                    _ID_Select--;
                }
                if(Input.GetKeyDown(KeyCode.S) && _ID_Select < _selection.Length - 1)
                {
                    _ID_Select++;
                }
                switch (_ID_Select)
                {
                    case 0:
                        _selection[0].sprite = selection_sprite[1];
                        _selection[1].sprite = selection_sprite[0];
                        if(Input.GetKeyDown(KeyCode.F))
                        {
                            if(_equipo[_ID_equipo].GetComponent<Image>().enabled == false)
                            {
                                _equipo[_ID_equipo].GetComponent<Image>().sprite = _bag[ID].GetComponent<Image>().sprite;
                                _equipo[_ID_equipo].GetComponent<Image>().enabled = true;
                                _bag[ID].GetComponent<Image>().sprite = null;
                                _bag[ID].GetComponent<Image>().enabled = false;
                                //_equipo[_ID_equipo].GetComponent<Items>()._gameObject = _bag[ID].GetComponent<Items>()._gameObject;
                            }
                            else
                            {
                                Sprite obj = _bag[ID].GetComponent<Image>().sprite;
                                //GameObject gameobj = _bag[ID].GetComponent<Items>()._gameObject;
                                _bag[ID].GetComponent<Image>().sprite = obj;
                                _equipo[_ID_equipo].GetComponent<Image>().sprite = obj;

                                //_bag[ID].GetComponent<Items>()._gameObject = _equipo[_ID_equipo].GetComponent<Items>()._gameObject;
                                //_equipo[_ID_equipo].GetComponent<Items>()._gameObject = gameobj;
                            }

                            fases_inv = 0;
                        }
                        break;
                    case 1:
                        _selection[0].sprite = selection_sprite[0];
                        _selection[1].sprite = selection_sprite[1];
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            Instantiate(_gameObject, transform.position + new Vector3(2, 0, 0), Quaternion.identity);
                            _bag[ID].GetComponent<Image>().sprite = null;
                            _bag[ID].GetComponent <Image>().enabled = false;
                            fases_inv = 1;
                        }
                        break;
                }
                break ;
        }

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
                    //_bag[i].GetComponent<Items>()._gameObject = coll.GetComponent<Items>()._gameObject;
                    Debug.Log("i touched");
                    break;
                }
            }
        }
    }
}
