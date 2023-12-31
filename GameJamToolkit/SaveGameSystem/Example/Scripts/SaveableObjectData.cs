﻿using System;
using IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    [Serializable]
    public class SaveableObjectData
    {
        public float posX, posY, posZ;
        public float rotX, rotY, rotZ;
        public float health;
        public float maxHealth;
        public string name;
        public bool isHostile;
        public SavableType savableType;
    }
}