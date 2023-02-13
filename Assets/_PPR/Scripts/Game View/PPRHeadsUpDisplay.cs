﻿using PPR.Core;
using UnityEngine;

namespace PPR.GameView
{
    public class PPRHeadsUpDisplay : PPRMonoBehaviour
    {
        [SerializeField] private PPRCurrencyView crewView;
        [SerializeField] private PPRCurrencyView piesView;

        private void OnEnable()
        {
            AddListener(PPREvents.crew_owned_changed, UpdateCrew);
            AddListener(PPREvents.pies_owned_changed, UpdatePies);
        }

        private void UpdateCrew(object obj) => UpdateCurrencyView(ref crewView, (int)obj);
        private void UpdatePies(object obj) => UpdateCurrencyView(ref piesView, (int)obj);


        private void UpdateCurrencyView(ref PPRCurrencyView view, int amount)
        {
            view.SetAmount(amount);
        }

        private void OnDisable()
        {
            RemoveListener(PPREvents.crew_owned_changed, UpdateCrew);
            RemoveListener(PPREvents.pies_owned_changed, UpdatePies);
        }
    }
}