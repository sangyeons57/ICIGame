using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ICI
{
    public class InputPopup : Popup<InputPopup>
    {
        static private Character character;

        public static void OpenPopup()
        {
            PopupManager.OpenPopup<InputPopup>("InputPopup");
        }

        static public void setCharacter(Character _character)
        {
            character = _character;
        }

        public void onClcikBtnMove()
        {
            PopupManager.ClosePopup<InputPopup>();

            CharacterMove.Instance.setPosDirectionKeyObject(character.instance.transform);
            CharacterMove.Instance.setOrderedCharacter(character);
            CharacterMove.Instance.setActive(true);
        }

        public void onClcikBtnAttack() 
        {
            PopupManager.ClosePopup<InputPopup>();

            SkillPopup.OpenPopup(character);
                
        }

    }
}

