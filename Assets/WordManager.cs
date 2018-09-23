using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{

    public List<Word> words;
    public WordSpawner wordSpawner;
    //Word Typed
    public float wordTyped = 0f, totalWord = 0f;

    private bool hasActiveWord;
    private Word activeWord;
 
    public void AddWord()
    {
        totalWord++;
        Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
        Debug.Log(word.word);

        words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        else
        {
            foreach (Word word in words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        if (hasActiveWord && activeWord.WordTyped())
        {
            wordTyped++;
            hasActiveWord = false;
            words.Remove(activeWord);
        }
    }

    //Missed Points
    public float GetMissedWord ()
    {
        return totalWord - wordTyped;
    }
}