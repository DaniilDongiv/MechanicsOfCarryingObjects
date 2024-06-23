using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Controller
{
    public class CollectController : MonoBehaviour
    {
        [SerializeField]
        private Transform _holderParent;
        
        private Stack<Transform> _collectedTransform = new Stack<Transform>();
        private bool _isInDropArea;
        private Vector3 _dropPosition;
        private UnlockScreen _unlockItemScr;
        private float _distanceBetweenObjects = 0.3f;
        private float _distanceBetweenObjectCreature = 0.1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Drop"))
            {
                _isInDropArea = true;
                _dropPosition = other.transform.position;
                other.TryGetComponent(out _unlockItemScr);

                StartCoroutine(DropSlow(.5f));
            }
            else
            {
                other.TryGetComponent(out Item localItem);

                if (other.CompareTag("Money") && localItem && localItem.IsCollected == false)
                {
                    Transform otherTransform;
                    (otherTransform = other.transform).SetParent(_holderParent);
                    otherTransform.localPosition = new Vector3(0, _collectedTransform.Count * _distanceBetweenObjects, _distanceBetweenObjectCreature);
                    otherTransform.localRotation = Quaternion.identity;
                
                    _collectedTransform.Push(otherTransform);
                    localItem.IsCollected = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Drop"))
                _isInDropArea = false;
        }

        private IEnumerator DropSlow(float delay = 0)
        {
            while (_isInDropArea)
            {
                yield return new WaitForSeconds(delay);
                
                if (_collectedTransform.Count>0)
                {
                    Transform newItem = _collectedTransform.Pop();
                    newItem.parent = null;
                    newItem.DOJump(_dropPosition, 2, 1, .2f).OnComplete(() =>
                        newItem.DOPunchScale(new Vector3(.2f, .2f, .2f), .1f)
                            .OnComplete(() => Destroy(newItem.gameObject)));
                    GeneratingObjectController.Instance.CreateObject();
                    
                    if (_unlockItemScr)
                    {
                        yield return new WaitForSeconds(delay);
                        _unlockItemScr.UnlockItem();
                    }
                }
            }
            yield return null;
        }
    }
}