﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankTile : MonoBehaviour {

    public GameObject boardManager;

    public Transform greenArrowPrefab;

    public float randomGreyValue;
    public Color _color;

    private Renderer _renderer;
    public Texture blankTileTexture;
    public Texture greenArrowSelectedTexture;

    private void Start()
    {
        boardManager = GameObject.FindGameObjectWithTag("Board Manager");
        _renderer = GetComponent<Renderer>();

        randomGreyValue = Random.Range(0.75f, .95f);

        _color = new Color(randomGreyValue, randomGreyValue, randomGreyValue);

        _renderer.material.color = _color;
    }

    private void OnMouseOver()
    {   
        if (!GameManager.simulationIsRunning && !CurrentLevelManager.isGreenArrowStockEmpty && InGameUIManager.isGreenArrowSelected && GameManager.playerCanModifyBoard)
        {
            _renderer.material.SetTexture("_MainTex", greenArrowSelectedTexture);

            if (Input.GetMouseButtonDown(0))
            {
                int hierarchyIndex = transform.GetSiblingIndex();                                                                               //Store the current hierarchy index of the blank tile.
                Destroy(gameObject);                                                                                                            //Destroy the blank tile.
                Transform newTile = Instantiate(greenArrowPrefab, transform.position, Quaternion.identity, boardManager.transform);        //Instantiate and store the new tile type at the end of the BoardManager.
                newTile.SetSiblingIndex(hierarchyIndex);                                                                                  //Use the stored hierarchy index to put the new tile in place of the deleted one.
                BoardManager.playerHasChangedATile = true;
                CurrentLevelManager.greenArrowStock_static--;
                //Debug.Log("stock is empty = " + CurrentLevelManager.isGreenArrowStockEmpty.ToString());
            }
            else if (GameManager.simulationIsRunning || Input.GetMouseButtonDown(1))
            {
                _renderer.material.SetTexture("_MainTex", blankTileTexture);
            }
        }
    }

    private void OnMouseExit()
    {
        if (InGameUIManager.isGreenArrowSelected)
        {
            _renderer.material.SetTexture("_MainTex", blankTileTexture);
        }
    }
}
