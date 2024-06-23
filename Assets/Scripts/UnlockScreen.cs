using DG.Tweening;
using TMPro;
using UnityEngine;

public class UnlockScreen : MonoBehaviour
{
    [SerializeField] private TextMeshPro _txtAmtUnlock;
    [SerializeField] private GameObject _itemToUnlock;
    
    private int _numToUnlock = 10;
    private int _numAdded = 0;

    public void UnlockItem()
    {
        _numAdded++;
        _txtAmtUnlock.text = string.Format("{0}/{1}", _numAdded, _numToUnlock);
        _txtAmtUnlock.DOBlendableColor(Color.green, 5f).OnComplete(() => _txtAmtUnlock.DOColor(Color.white, 2f));

        if (_numAdded >= _numToUnlock && !_itemToUnlock.activeInHierarchy)
        {
            _itemToUnlock.SetActive(true);
            _txtAmtUnlock.gameObject.SetActive(false);
        }
    }
}