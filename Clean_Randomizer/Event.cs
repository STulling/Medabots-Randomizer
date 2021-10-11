using MedabotsRandomizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Randomizer
{
    public partial class MainWindow
    {
        class Event
        {
            public Dictionary<uint, string> event_operations;

            private Event(Dictionary<uint, string> event_operations)
            {
                this.event_operations = event_operations;
            }

            public static Event fromData(byte[] file, uint offset, Dictionary<string, Operation> operations)
            {
                Dictionary<uint, string> operationMap = new Dictionary<uint, string>();
                buildEvent(file, offset, operationMap, operations);
                return new Event(operationMap);
            }

            public static void buildEvent(byte[] file, uint offset, Dictionary<uint, string> operationMap, Dictionary<string, Operation> operations)
            {
                while (true)
                {
                    byte op = file[offset];
                    if (op == 0x2F)
                    {
                        //multiconditional jump
                        List<int> jumps = new List<int>();
                        jumps.Add(file[offset + 1]);
                        jumps.Add(file[offset + 2]);
                        int nArgs = 0;
                        for (int i = 2; i < 8; i++)
                        {
                            int minJump = jumps.Min();
                            if (minJump == jumps.Count)
                            {
                                nArgs = minJump;
                                break;
                            }
                            else
                            {
                                jumps.Add(file[offset + 1 + i]);
                            }
                        }
                        int[] args = new int[nArgs];
                        for (int i = 0; i < nArgs; i++)
                        {
                            args[i] = file[offset + 1 + i];
                        }
                        string argString = String.Join(",", args);
                        operationMap[offset] = $"<Conditional_Multijump: {argString}>";
                        foreach (uint jump in args)
                        {
                            buildEvent(file, offset + jump + 1, operationMap, operations);
                        }
                        break;
                    }
                    else if (op == 0x06)
                    {
                        operationMap[offset] = "<END>";
                        break;
                    }
                    else if (op == 0x19)
                    {
                        ushort goto_event = (ushort)((ushort)(file[offset + 1] << 8) + (ushort)file[offset + 2]);
                        operationMap[offset] = $"<GOTO_EVENT: {goto_event}>";
                        break;
                    }
                    else
                    {
                        if (!operations.TryGetValue(op.ToString("X2"), out Operation operation))
                        {
                            uint opLength = IdTranslator.operationBytes[op];
                            IdTranslator.opNames.TryGetValue(op, out string opName);
                            if (opName == "" || opName == null) opName = $"UNKNOWN({op.ToString("X2")})";
                            if (opLength > 1)
                            {
                                int[] args = new int[opLength - 1];
                                for (int i = 0; i < opLength - 1; i++)
                                {
                                    args[i] = file[offset + i + 1];
                                }
                                string argString = string.Join(",", args);
                                operationMap[offset] = $"<{opName}: {argString}>";
                            }
                            else
                            {
                                operationMap[offset] = $"<{opName}>";
                            }
                            offset += opLength;
                        }
                        else
                        {
                            uint opLength = (uint)operation.getArgLength() + 1;
                            operationMap[offset] = operation.getString(file, offset);
                            /*
                            if (operation.args.Any(x => x.name == "jump"))
                            {
                                buildEvent(file, offset + jump + 1, operationMap, operations);
                            }
                            */
                            offset += opLength;
                        }
                    }
                }
            }
        }
    }
}
