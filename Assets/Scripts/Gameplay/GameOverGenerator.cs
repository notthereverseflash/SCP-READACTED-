using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverGenerator : MonoBehaviour
{

    void OnEnable()
    {
        int randomQuote = Random.Range(1, 12);

        TMP_Text _quoteText = this.GetComponent<TMP_Text>();
        switch (randomQuote)
        {
            case (1):
                _quoteText.text = "Death and life have their determined appointments; riches and honors depend upon heaven.\n-Confucius";
                break;
            case (2):
                _quoteText.text = "If we don't know life, how can we know death?\n-Confucius";
                break;
            case (3):
                _quoteText.text = "Life is going forth; death is returning home.\n-Confucius";
                break;
            case (4):
                _quoteText.text = "The contented person finds rest in death, and for the greedy person, death puts an end to his long list of desires... Death, then, for everyone is a king of homecoming\n-Lie Yukou";
                break;
            case (5):
                _quoteText.text = "Life and death are one thread, the same line viewed from different sieds\n-Laozi";
                break;
            case (6):
                _quoteText.text = "In the middle of the journey of our life I found myself within a dark woods where the straight way was lost\n-Dante Alighieri";
                break;
            case (7):
                _quoteText.text = "Through me you go into a city of weeping; through me you go into eternal pain; through me you go amongst the lost people\n-Dante Alighieri";
                break;
            case (8):
                _quoteText.text = "We kept our eyes focused on the sky, and it almost seemed as if our souls could talk thoughout the epidermis of our own hands which could hug within our stares which collided in the stars.\n-Giovanni Verga";
                break;
            case (9):
                _quoteText.text = "That is not dead which can eternal lie, and with strange aeons even death may die.\n-H.P. Lovecraft";
                break;
            case (10):
                _quoteText.text = "The fear of death follows from the fear of life. A man who lives fully is prepared to die at any time.\n-Mark Twain";
                break;
            case (11):
                _quoteText.text = "\"I wish it need not have happened in my time\" said Frodo. \"So do I,\" said Gandalf, \"and so do all who live to see such times. But that is not for them to decide. All we have to decide is what to do with the time that is given us.\"\n-J.R.R. Tolkien";
                break;
            case (12):
                _quoteText.text = "One wants to live, of course, indeed one only stays alive by virtue of the fear of death.\n-George Orwell";
                break;
            default:
                break;
        }
    }
}
