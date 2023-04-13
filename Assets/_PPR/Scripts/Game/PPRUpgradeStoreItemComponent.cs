using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace PPR.Game
{
    public class PPRUpgradeStoreItemComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private TMP_Text title;

        private UpgradeableTypeIDs type;
        public void Init(UpgradeableTypeIDs upgradeableType)
        {
            type = upgradeableType;
            title.text = Regex.Replace(type.ToString(), "(\\B[A-Z])", " $1");
        }
    }
}