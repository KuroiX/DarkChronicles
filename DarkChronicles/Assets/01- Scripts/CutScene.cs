using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    private Text _dialogueText;
    private Text _nameText;
    private Animator _animator;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");
    
    [SerializeField] 
    private GameObject nextCutscene;
    [SerializeField]
    private ScriptableCutScene[] events;
    
    private Queue<CutSceneEvent> _eventQueue;
    private Queue<string> _sentences;
    private PlayableDirector _director;
    private RPGM.Gameplay.CharacterController2D _charController;

    private bool _textLocked;
    private bool _isRunning;
    private bool _activated;

    #region MonoBehaviour

    void Start()
    {
        _dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
        _nameText = GameObject.Find("NameText").GetComponent<Text>();
        _animator = GameObject.Find("DialogueBox").GetComponent<Animator>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("CutScene.OnTriggerEnter2D()");
        if (!_activated)
            Setup(col);
    }
    
    void Update()
    {
        if (_activated && _textLocked && Input.anyKeyDown)
        {
            DisplayNextSentence();
        }
    }
    
    #endregion
    
    #region Events
    
    private void Setup(Collider2D col)
    {
        // Initialize
        _eventQueue = new Queue<CutSceneEvent>();
        _sentences = new Queue<string>();
        _director = GetComponent<PlayableDirector>();
        _activated = true;
        
        // Disable character movement
        _charController = GameObject.Find("Character").GetComponent<RPGM.Gameplay.CharacterController2D>();
        _charController.LockMovement();
        _charController.enabled = false;

        // Enqueue events
        for (int i = 0; i < events.Length; i++)
        {
            for (int j = 0; j < events[i].playables.Length; j++)
            {
                _eventQueue.Enqueue(new CutSceneEvent(events[i].playables[j]));
            }
            
            for (int j = 0; j < events[i].dialogues.Length; j++)
            {
                _eventQueue.Enqueue(new CutSceneEvent(events[i].dialogues[j]));
            }
        }
        
        // Call first event
        //Next();
        StartCoroutine(MovePlayer(col));
    }

    IEnumerator MovePlayer(Collider2D col)
    {
        //TODO: walking animation
        
        Vector3 scenePos = transform.position;
        Vector3 distance = col.transform.position - scenePos;
        
        while (distance.magnitude > 0.05f)
        {
            col.transform.position = Vector3.Lerp(col.transform.position, scenePos, 0.15f);
            yield return null;
            distance = col.transform.position - scenePos;
        }

        Next();
    }

    private void Next()
    {
        //Debug.Log("Next");

        if (_eventQueue.Count == 0)
        {
            End();
            return;
        }
        
        var nextEvent = _eventQueue.Dequeue();
        
        if (nextEvent.type == CutSceneEvent.EventType.Playable)
        {
            StartCoroutine(PlaySequence(nextEvent));
        } 
        else if (nextEvent.type == CutSceneEvent.EventType.Dialogue)
        {
            StartDialogue(nextEvent);
        }
        
    }
    
    IEnumerator PlaySequence(CutSceneEvent _event)
    {
        //Debug.Log("PlaySequence");
        
        _director.Play(_event.playable);

        yield return new WaitForSeconds((float) _event.playable.duration);

        Next();
    }
    
    private void End()
    {
        _charController.enabled = true;
        _activated = false;
        ActivateNextCutscene();
        Destroy(gameObject);
    }

    private void ActivateNextCutscene()
    {
        if (!nextCutscene.Equals(null))
        {
            nextCutscene.SetActive(true);
        }
    }

    #endregion
    
    #region Dialogue
    
    private void StartDialogue (CutSceneEvent _event)
    {
        _nameText.text = _event.dialogue.name;
        
        _textLocked = true;
        
        _animator.SetBool(IsOpen, true);

        _sentences.Clear();

        foreach (string sentence in _event.dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence ()
    {
        if (!_isRunning)
        {
            if (_sentences.Count == 0)
            {
                StartCoroutine(EndDialogue());
                return;
            }

            string sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        _isRunning = true;
        
        _dialogueText.text = "";
        foreach (char letter in sentence)
        {
            _dialogueText.text += letter;
            yield return null;
        }

        _isRunning = false;
    }

    IEnumerator EndDialogue()
    {
        _animator.SetBool(IsOpen, false);
        yield return new WaitForSeconds(0.2f);
        _textLocked = false;
        Next();
    }
    
    #endregion
    
}
