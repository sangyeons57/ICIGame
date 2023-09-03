using ICI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ICI
{
    public class SkillPopup : Popup<SkillPopup>
    {
        public Button[] Skills;
        private Character character;

        public static void OpenPopup()
        {
            PopupManager.OpenPopup<SkillPopup>("SkillPopup");
        }

        public static void OpenPopup(Character character)
        {
            PopupManager.OpenPopup<SkillPopup>("SkillPopup")
                .setChacterAndGetClass(character);
        }
        
        public SkillPopup setChacterAndGetClass(Character character)
        {
            InitializeClass();

            setCharacter(character);

            return this;
        }

        private void InitializeClass()
        {
            makeNotSettingLevel();
        }

        private void setCharacter(Character character)
        {
            this.character = character;

            for (int i = 0; i < character.level; i++)
            {
                Skills[i].gameObject.SetActive(true);
            }
        }

        private void makeNotSettingLevel()
        {
            foreach (var skill in Skills)
            {
                skill.gameObject.SetActive(false);
            }

        }

        public void onCLickSkill1() => character.action_Skill1();
        public void onCLickSkill2() => character.action_Skill2();
        public void onCLickSkill3() => character.action_Skill3();

    }
}
