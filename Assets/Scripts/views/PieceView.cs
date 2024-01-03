using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PieceView : MonoBehaviour
{
    //model
    [HideInInspector]
    public string pieceName;

    [HideInInspector]
    public PieceType pieceType;

    [FormerlySerializedAs("attack")]
    [HideInInspector]
    public int Attack;
    [FormerlySerializedAs("health")]
    [HideInInspector]
    public int Health;

    //view
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer Backimage;
    public TextMeshPro nameText;
    public TextMeshPro attackText;
    public TextMeshPro healthText;

    bool isDead = false;

    Vector3 originalPos;

    [SerializeField]
    private Transform AttackLocation;
    
    [SerializeField]
    private float AttackLocationOffset = 2f;
    public string Id { get; private set; }
    public void initializePiece(PieceData pieceData, PieceType pieceType)
    {
        Id = Guid.NewGuid().ToString();
        this.pieceName = pieceData.pieceName;
        this.pieceType = pieceType;
        this.Attack = pieceData.attack;
        // this.Health = Mathf.RoundToInt(pieceData.health * 1.5f) ;
        this.Health = pieceData.health;

        // spriteRenderer.sprite = pieceData.image;
        if (pieceType == PieceType.Player)
        {
            Backimage.color = Color.blue;
            AttackLocation.localPosition += new Vector3(0, 0, AttackLocationOffset);
        }
        else if (pieceType == PieceType.AIPiece)
        {
            Backimage.color = Color.red;
            AttackLocation.localPosition += new Vector3(0, 0, -AttackLocationOffset);
        }
        
        nameText.text = pieceData.pieceName;
        spriteRenderer.sprite = pieceData.image;
        attackText.text = Attack.ToString();
        healthText.text = Health.ToString();

        originalPos = this.transform.position;
    }

    public void PieceAttackAnimation(PieceView attackedPiece, Action callback)
    {
        StartCoroutine(AttackAnimationRoutine(attackedPiece, callback));
    }

    Sequence s;
    private IEnumerator AttackAnimationRoutine(PieceView attackedPiece, Action callback)
    {
        transform.DOMove(attackedPiece.AttackLocation.position, 0.5f);
        yield return new WaitForSeconds(1f);
        takeDamage(attackedPiece.Attack);
        
        transform.DOShakePosition(0.3f, 0.5f, 10, 90f, false, true);
        attackedPiece.transform.DOShakePosition(0.3f, 0.5f, 10, 90f, false, true);
        yield return new WaitForSeconds(0.4f);
        attackedPiece.takeDamage(this.Attack);
        
        
        yield return new WaitForSeconds(1f);
        transform.DOMove(originalPos, 0.5f);

        yield return new WaitForSeconds(1f);
        callback?.Invoke();
    }

    public bool IsDead()
    {
        return isDead;
    }
    private void takeDamage(int damage)
    {
        Health -= damage;
        UpdateHealth();
        if (Health <= 0)
        {
            isDead = true;
        }

    }
    private void UpdateHealth()
    {
        healthText.color = Color.red;
        healthText.text = Health.ToString();
    }

    public void PieceDeath()
    {
        StartCoroutine(pieceDeathRoutine());
    }

    public IEnumerator pieceDeathRoutine()
    {
        transform.DOShakePosition(0.3f, 0.5f, 10, 90f, false, true);
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
       
      
    }
    public void AddHealth(int i)
    {
        Health += i;
        healthText.color = Color.green;
        healthText.text = Health.ToString();
    }
}
