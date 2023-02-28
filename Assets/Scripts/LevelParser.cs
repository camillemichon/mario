﻿using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public string filename;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public GameObject waterPrefab;
    public GameObject flagPrefab;
    public Transform environmentRoot;

    // --------------------------------------------------------------------------
    void Start()
    {
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int row = 0;
        
        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                Vector3 position = new Vector2(column, row);
                switch (letter)
                {
                    case 'x':
                        Instantiate(rockPrefab, position, Quaternion.identity, environmentRoot);
                        break;
                    case '?':
                        Instantiate(questionBoxPrefab, position, Quaternion.identity, environmentRoot);
                        break;
                    case 'b':
                        Instantiate(brickPrefab, position, Quaternion.identity, environmentRoot);
                        break;
                    case 's':
                        Instantiate(stonePrefab, position, Quaternion.identity, environmentRoot);
                        break;
                    
                    case 'w':
                        Instantiate(waterPrefab, position, Quaternion.identity, environmentRoot);
                        break;
                    
                    case 'f':
                        Instantiate(flagPrefab, flagPrefab.transform.position + position, flagPrefab.transform.rotation, environmentRoot);
                        break;

                }
            }

            row++;
        }
    }

    // --------------------------------------------------------------------------
    private void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
