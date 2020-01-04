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
    private CutSceneEvent[] events;

    private Queue<CutSceneEvent> _eventQueue;
    private Queue<string> _sentences;
    private PlayableDirector _director;
    private RPGM.Gameplay.CharacterController2D _charController;

    private bool _textLocked;
    private bool _isRunning;
    private bool _sequenceLocked;
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
            Setup();
    }
    
    void Update()
    {
        if (_activated && Input.anyKeyDown)
        {
            //Debug.Log(_textLocked + " " + _sequenceLocked);
            
            if (_textLocked)
            {
                DisplayNextSentence();
            }
            else if (!_sequenceLocked)
            {
                // next sequence
                Next();
            }
        }
    }
    
    #endregion
    
    #region Events
    
    private void Setup()
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
            _eventQueue.Enqueue(events[i]);
        }
        
        // Call first event
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
        
        if (nextEvent.type == CutSceneEvent.EventType.Animation)
        {
            StartCoroutine(PlaySequence(nextEvent));
        } 
        else if (nextEvent.type == CutSceneEvent.EventType.Text)
        {
            StartDialogue(nextEvent);
        }
        
    }
    
    IEnumerator PlaySequence(CutSceneEvent _event)
    {
        //Debug.Log("PlaySequence");
        _sequenceLocked = true;
        _director.Play(_event.animation);

        yield return new WaitForSeconds((float) _event.animation.duration);
        
        _sequenceLocked = false;
        
        Next();
    }
    
    private void End()
    {
        _charController.enabled = true;
        _activated = false;
        Destroy(gameObject);
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
                EndDialogue();
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

    private void EndDialogue()
    {
        _animator.SetBool(IsOpen, false);
        _textLocked = false;
        Next();
    }
    
    #endregion
    
}
