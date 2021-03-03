/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats {

    public event EventHandler OnStatsChanged;

    public static int STAT_MIN = 0;
    public static int STAT_MAX = 20;

    public enum Type {
        Attack,
        Defence,
        Speed,
        Mana,
        Health,
    }

    private SingleStat attackStat;
    private SingleStat defenceStat;
    private SingleStat speedStat;
    private SingleStat manaStat;
    private SingleStat healthStat;

    public Stats(int attackStatAmount, int defenceStatAmount, int speedStatAmount, int manaStatAmount, int healthStatAmount, int[] maxValues)
    {
        attackStat = new SingleStat(attackStatAmount, maxValues[0]);
        defenceStat = new SingleStat(defenceStatAmount, maxValues[1]);
        speedStat = new SingleStat(speedStatAmount, maxValues[2]);
        manaStat = new SingleStat(manaStatAmount, maxValues[3]);
        healthStat = new SingleStat(healthStatAmount, maxValues[4]);
    }


    private SingleStat GetSingleStat(Type statType) {
        switch (statType) {
        default:
        case Type.Attack:       return attackStat;
        case Type.Defence:      return defenceStat;
        case Type.Speed:        return speedStat;
        case Type.Mana:         return manaStat;
        case Type.Health:       return healthStat;
        }
    }
    
    public void SetStatAmount(Type statType, int statAmount,int max) {
        GetSingleStat(statType).SetStatAmount(statAmount, max);
        if (OnStatsChanged != null) OnStatsChanged(this, EventArgs.Empty);
    }

    public void IncreaseStatAmount(Type statType,int max) {
        SetStatAmount(statType, GetStatAmount(statType) + 1, max);
    }

    public void DecreaseStatAmount(Type statType,int max) {
        SetStatAmount(statType, GetStatAmount(statType) - 1,max);
    }

    public int GetStatAmount(Type statType) {
        return GetSingleStat(statType).GetStatAmount();
    }

    public float GetStatAmountNormalized(Type statType,int max) {
        return GetSingleStat(statType).GetStatAmountNormalized(max);
    }



    /*
     * Represents a Single Stat of any Type
     * */
    private class SingleStat {

        private int stat;

        public SingleStat(int statAmount, int maxValue) {
            SetStatAmount(statAmount, maxValue);
        }

        public void SetStatAmount(int statAmount,int maxValue) {
            stat = Mathf.Clamp(statAmount, STAT_MIN, maxValue);
        }

        public int GetStatAmount() {
            return stat;
        }

        public float GetStatAmountNormalized(int max) {
            //return (float)stat / STAT_MAX;
             return (float)stat / max;
        }
    }
}
