using MedabotsRandomizer.Data.Wrappers;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MedabotsRandomizer.Data
{
    //memory offset: 0x083b841c
    //size: 0x10
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class Part
    {
        public byte medal_compatibility;
        byte technique_legtype;
        byte speciality;
        public byte gender;
        byte armor;
        byte RoS;
        byte power;
        byte chain_reaction;
        byte amount_of_uses;
        byte unknown2;
        byte unknown3;
        byte unknown4;
        byte unknown5;
        byte unknown6;
        byte unknown7;
        byte unknown8;

        public IEnumerable<FieldWrapper> toDataSource(string partType)
        {
            List<FieldWrapper> fields = new List<FieldWrapper>();
            fields.Add(new MedalIdWrapper(medal_compatibility, "Medal Compatibility"));
            if (partType == "Head")
            {
                fields.Add(new TechniqueWrapper(technique_legtype, "Technique"));
                fields.Add(new SpecialityWrapper(speciality, "Speciality"));
                fields.Add(new BoolWrapper(gender, "Female"));
                fields.Add(new ByteWrapper(armor, "Armor"));
                fields.Add(new ByteWrapper(RoS, "Rate of Success"));
                fields.Add(new ByteWrapper(power, "Power"));
                fields.Add(new BoolWrapper(chain_reaction, "Chain Reaction"));
                fields.Add(new ByteWrapper(amount_of_uses, "Amount of Uses"));
                fields.Add(new ByteWrapper(unknown3, "Unknown"));
                fields.Add(new ByteWrapper(unknown4, "Unknown"));
                fields.Add(new ByteWrapper(unknown5, "Unknown"));
            }
            else if (partType == "Left Arm" || partType == "Right Arm")
            {
                fields.Add(new TechniqueWrapper(technique_legtype, "Technique"));
                fields.Add(new SpecialityWrapper(speciality, "Speciality"));
                fields.Add(new BoolWrapper(gender, "Female"));
                fields.Add(new ByteWrapper(armor, "Armor"));
                fields.Add(new ByteWrapper(RoS, "Rate of Success"));
                fields.Add(new ByteWrapper(power, "Power"));
                fields.Add(new BoolWrapper(chain_reaction, "Chain Reaction"));
                fields.Add(new ByteWrapper(amount_of_uses, "CRG"));
                fields.Add(new ByteWrapper(unknown2, "RAD"));
                fields.Add(new ByteWrapper(unknown3, "Unknown"));
                fields.Add(new ByteWrapper(unknown4, "Unknown"));
                fields.Add(new ByteWrapper(unknown5, "Unknown"));
            }
            else
            {
                fields.Add(new ByteWrapper(technique_legtype, "Leg Type"));
                fields.Add(new BoolWrapper(gender, "Female"));
                fields.Add(new ByteWrapper(armor, "Armor"));
                fields.Add(new ByteWrapper(RoS, "Propulsion"));
                fields.Add(new ByteWrapper(power, "Evasion"));
                fields.Add(new ByteWrapper(chain_reaction, "Defense"));
                fields.Add(new ByteWrapper(amount_of_uses, "Proximity"));
                fields.Add(new ByteWrapper(unknown2, "Remoteness"));
                fields.Add(new ByteWrapper(unknown3, "Unknown"));
                fields.Add(new ByteWrapper(unknown4, "Unknown"));
                fields.Add(new ByteWrapper(unknown5, "Unknown"));
            }
            return fields;
        }
    }
}
