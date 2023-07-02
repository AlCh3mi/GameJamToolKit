using System;
using System.Collections.Generic;
using IceBlink.GameJamToolkit.DamageSystem.Damage;

namespace IceBlink.GameJamToolkit.DamageSystem.Defenses
{
    [Serializable]
    public class Defense
    {
        public Dictionary<DamageType, Resistance> Resistances;

        public event Action<Resistance> ResistanceUpdatedEvent; 

        public Defense(List<Resistance> resistances = null)
        {
            Resistances = new Dictionary<DamageType, Resistance>();
            if (resistances == null || resistances.Count == 0) 
                return;

            foreach (var resistance in resistances)
                AddResistance(resistance);
        }

        public void AddResistance(Resistance resistance)
        {
            if (Resistances.ContainsKey(resistance.damageType))
            {
                var tmpResist = Resistances[resistance.damageType];
                tmpResist.amount += resistance.amount;
                Resistances[resistance.damageType] = tmpResist;
                ResistanceUpdatedEvent?.Invoke(Resistances[resistance.damageType]);
                return;
            }
            
            Resistances.Add(resistance.damageType, resistance);
            ResistanceUpdatedEvent?.Invoke(Resistances[resistance.damageType]);
        }

        public float GetResistancePercentageVs(DamageType type)
        {
            if (type == null)
                return 0f;
            
            return !Resistances.ContainsKey(type) ? 0f : Resistances[type].amount;
        }

        public DamageReport DefendAgainst(DamageInstance damageInstance) => new DamageReport(damageInstance, this);
    }
}