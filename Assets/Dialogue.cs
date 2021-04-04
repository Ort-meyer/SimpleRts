using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public List<PlayerSentence> m_playerSentences = new List<PlayerSentence>();
    // Use this for initialization
    void Start()
    {
        M_CreateSimpleDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO improve this. Multiple triggers if unit has multiple colliders
        BaseUnit unit = other.GetComponentInParent<BaseUnit>();
        if(unit)
        {
            DialogueManager.Instance.M_StartConversation(this);
            unit.GetComponent<TankMovement>().M_StopMoving();
        }
    }

    private void M_CreateSimpleDialogue()
    {
        int protag = 0;
        int quest = 1;

        m_playerSentences = new List<PlayerSentence>()
        {
            //0
            new PlayerSentence("Hello", true, new List<Sentence>()
            {
                new Sentence("Hello", protag, false),
                new Sentence("Hi there!", quest, false),
            },
            new List<int>(){1, 3},
            new List<int>(){0}),
            //1
            new PlayerSentence("Are you OK?", false, new List<Sentence>()
            {
                new Sentence("Are you OK", protag, false),
                new Sentence("No man, shit's bad. Can you help me? Some god damn dudes took some shit" +
                "and this is just yet another long ass sentence to test the system. But that should be it", quest, false),
                new Sentence("Maybe it's better to just have multiple sentences instead of one big? I dunno", quest, false),
            },
            new List<int>(){2},
            new List<int>(){0, 1}),
            //2
            new PlayerSentence("Sure, I'll help out", false, new List<Sentence>()
            {
                new Sentence("Sure, I'll help out", protag, false),
                new Sentence("Thanks buddy! Go kick some ass", quest, true),
            },
            new List<int>(){},
            new List<int>(){2}),
            //3
            new PlayerSentence("Got to go!", false, new List<Sentence>()
            {
                new Sentence("Got to go!", protag, false),
                new Sentence("OK, cya boyo!", quest, true),
            },
            new List<int>(){},
            new List<int>(){0, 1, 2, 3}), // Unnecessary to have this list
        };
    }
}
