using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class AutoConfig_GridLayoutGroup : MonoBehaviour {
    enum TypeConfig{
        Horizontal, Vertical
    }
    [SerializeField] TypeConfig myTypeConfig;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] RectTransform myRectTransform;
    [SerializeField] bool runOnAwake = true;

    void Awake(){
        if(runOnAwake){
            AutoConfig();
        }
    }
    [Button] public void AutoConfig(){
        // Debug.Log(myRectTransform.rect.size);
        if(myTypeConfig == TypeConfig.Vertical){ 
            int _hContent = (int) (myRectTransform.rect.size.y - (gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom));
            int _num = (int) (_hContent / gridLayoutGroup.cellSize.y);
            int _spacingY = (_hContent - (int) (_num * gridLayoutGroup.cellSize.y)) / _num;
            // Debug.Log(_num + " - " + _spacingY);

            Vector2 _spacing = gridLayoutGroup.spacing;
            _spacing.y = _spacingY;
            gridLayoutGroup.spacing = _spacing;

            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayoutGroup.constraintCount = _num;
        }else{
            int _wContent = (int) (myRectTransform.rect.size.x - (gridLayoutGroup.padding.left + gridLayoutGroup.padding.right));
            int _num = (int) (_wContent / gridLayoutGroup.cellSize.x);
            int _spacingX = (_wContent - (int) (_num * gridLayoutGroup.cellSize.x)) / _num;
            // Debug.Log(_num + " - " + _spacing);

            Vector2 _spacing = gridLayoutGroup.spacing;
            _spacing.x = _spacingX;
            gridLayoutGroup.spacing = _spacing;

            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayoutGroup.constraintCount = _num;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(myRectTransform);
    }
}
