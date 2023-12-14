using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    //model
    [HideInInspector]
    public string pieceName;

    [HideInInspector]
    public PieceType pieceType;

    [HideInInspector]
    public int attack;
    [HideInInspector]
    public int health;

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
        this.attack = pieceData.attack;
        this.health = pieceData.health;

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
        attackText.text = pieceData.attack.ToString();
        healthText.text = pieceData.health.ToString();

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
        takeDamage(attackedPiece.attack);
        
        transform.DOShakePosition(0.3f, 0.5f, 10, 90f, false, true);
        attackedPiece.transform.DOShakePosition(0.3f, 0.5f, 10, 90f, false, true);
        yield return new WaitForSeconds(0.4f);
        attackedPiece.takeDamage(this.attack);
        
        
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
        health -= damage;
        UpdateHealth();
        if (health <= 0)
        {
            isDead = true;
        }

    }
    private void UpdateHealth()
    {
        healthText.color = Color.red;
        healthText.text = health.ToString();
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
}
