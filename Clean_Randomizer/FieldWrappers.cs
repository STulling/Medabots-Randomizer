using System;

namespace MedabotsRandomizer
{
    public class ByteWrapper : FieldWrapper
    {
        public byte field;
        public string name;
        public string Info { get => field.ToString(); }
        public string Name { get => name; }
        public string Field { get => field.ToString("X2"); set => field = Convert.ToByte(value, 16); }

        public ByteWrapper(byte field, string name)
        {
            this.field = field;
            this.name = name;
        }
    }

    public class BoolWrapper : FieldWrapper
    {
        public byte field;
        public string name;
        public string Info { get => (field == 1).ToString(); }
        public string Name { get => name; }
        public string Field { get => field.ToString("X2"); set => field = Convert.ToByte(value, 16); }

        public BoolWrapper(byte field, string name)
        {
            this.field = field;
            this.name = name;
        }
    }

    public class SpecialityWrapper : FieldWrapper
    {
        public byte field;
        public string name;
        public string Info { get => IdTranslator.IdToSpeciality(field); }
        public string Name { get => name; }
        public string Field { get => field.ToString("X2"); set => field = Convert.ToByte(value, 16); }

        public SpecialityWrapper(byte field, string name)
        {
            this.field = field;
            this.name = name;
        }
    }

    public class TechniqueWrapper : FieldWrapper
    {
        public byte field;
        public string name;
        public string Info { get => IdTranslator.IdToTechnique(field); }
        public string Name { get => name; }
        public string Field { get => field.ToString("X2"); set => field = Convert.ToByte(value, 16); }

        public TechniqueWrapper(byte field, string name)
        {
            this.field = field;
            this.name = name;
        }
    }

    public class MedalIdWrapper : FieldWrapper
    {
        public byte field;
        public string name;
        public string Info { get => IdTranslator.IdToMedal(field); }
        public string Name { get => name; }
        public string Field { get => field.ToString("X2"); set => field = Convert.ToByte(value, 16); }

        public MedalIdWrapper(byte field, string name)
        {
            this.field = field;
            this.name = name;
        }
    }

    public class PartIdWrapper : FieldWrapper
    {
        public byte field;
        public string name;
        private int part;
        public string Info { get => IdTranslator.IdToPart(field, part); }
        public string Name { get => name; }
        public string Field { get => field.ToString("X2"); set => field = Convert.ToByte(value, 16); }

        public PartIdWrapper(byte field, string name, int part)
        {
            this.field = field;
            this.name = name;
            this.part = part;
        }
    }

    public interface FieldWrapper
    {
        string Name { get; }
        string Field { get; set; }
        string Info { get; }
    }
}
