using MedabotsRandomizer;
using System;

namespace Clean_Randomizer
{
    public partial class MainWindow
    {
        struct OperationArgument
        {
            public string name;
            public string type;
            public object value;

            public int getByteCount()
            {
                switch (type)
                {
                    case "short":
                        return 2;
                    default:
                        return 1;
                }
            }

            public object readValue(byte[] file, uint offset)
            {
                switch (type)
                {
                    case "short":
                        value = (file[offset] << 8) + file[offset + 1];
                        return value;
                    case "medal":
                        value = file[offset];
                        return IdTranslator.IdToMedal(file[offset]);
                    case "direction":
                        value = file[offset];
                        switch (file[offset])
                        {
                            case 0:
                                return "north";
                            case 1:
                                return "south";
                            case 2:
                                return "west";
                            case 3:
                                return "east";
                            default:
                                return file[offset];
                        }
                    case "part":
                        value = file[offset];
                        switch (file[offset])
                        {
                            case 0:
                                return "head";
                            case 1:
                                return "right";
                            case 2:
                                return "left";
                            case 3:
                                return "legs";
                            default:
                                return "?";
                        }
                    case "bot":
                        value = file[offset];
                        return IdTranslator.IdToBot((byte)value);
                    case "move":
                        if (file[offset] == 0xFF) return "-";
                        string direction = "";
                        switch (file[offset] & 0xF0)
                        {
                            case 0x00:
                                direction = "north";
                                break;
                            case 0x10:
                                direction = "south";
                                break;
                            case 0x20:
                                direction = "west";
                                break;
                            case 0x30:
                                direction = "east";
                                break;
                            default:
                                direction = "?";
                                break;
                        }
                        value = file[offset];
                        string length = (file[offset] & 0x0F).ToString();
                        return $"({direction}, {length})";
                    case "event_bank":
                        value = file[offset] - 2;
                        break;
                    default:
                        value = file[offset];
                        break;
                }
                return value;
            }

            public Tuple<string, int, object> parseData(byte[] file, uint offset)
            {
                object value = readValue(file, offset);
                int bytes_read = getByteCount();
                return new Tuple<string, int, object>($"{name}: {value}", bytes_read, value);
            }
        }
    }
}
