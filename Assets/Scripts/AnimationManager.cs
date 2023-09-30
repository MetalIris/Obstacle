using DG.Tweening;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public void ScaleChangeAnimation(float scale)
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOShakeScale(0.2f, 0.2f)); 
        sequence.Append(transform.DOScale(new Vector3(scale, scale, scale), 0.5f));
    }

    public void ShootAnimation(Transform pos)
    {
        
        var targetPosition = pos.position + pos.right * 20.0f;
        pos.DOMove(targetPosition, 1.0f)
            .SetEase(Ease.Flash);
    }
    
    public void MoveAndRotate(float duration, float moveDistance, System.Action onComplete = null)
    {
        var endPosition = transform.position + Vector3.right * moveDistance;
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(endPosition, duration).SetEase(Ease.OutQuad));
        sequence.Join(transform.DORotate(new Vector3(0, 360, 0), duration, RotateMode.FastBeyond360).SetEase(Ease.OutQuad));
        sequence.SetAutoKill(true);
        sequence.OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    public void LevelComplete()
    {
        var startPosition = transform.position;
        var upPosition = startPosition + Vector3.up * 2f;
        var forwardPosition = upPosition + Vector3.right * 20f;
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(upPosition, 2f).SetEase(Ease.OutQuad));
        sequence.Append(transform.DOMove(forwardPosition, 2f).SetEase(Ease.Linear));
        sequence.Join(transform.DOScale(new Vector3(0, 0, 0), 0.5f));
    }
}
