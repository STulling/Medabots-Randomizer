using MedabotsRandomizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Randomizer
{
    public partial class MainWindow
    {
        struct Operation
        {
            public string name;
            public List<OperationArgument> args;

            public bool hasJump()
            {
                return args.Any(x => x.name == "jump");
            }

            public int getJump(List<object> values)
            {
                int index = args.FindIndex(x => x.name == "jump");
                return Convert.ToInt32(values[index]);
            }

            public int getArgLength()
            {
                int result = 0;
                foreach (OperationArgument arg in args)
                {
                    result += arg.getByteCount();
                }
                return result;
            }

            public string getString(byte[] file, uint offset)
            {
                Tuple<string, List<object>> args = getArgString(file, offset + 1);
                string result = $"{this.name}({args.Item1})";
                string comment = "";
                if (name == "Show_Message_A" || name == "Show_Message_B")
                    comment = TextParser.instance.origMessages[((int)args.Item2[0], (int)args.Item2[1])];
                if (name == "Warp_A" || name == "Warp_B")
                    comment = IdTranslator.IdToMap((byte)args.Item2[0]);
                if (name == "Play_Music" || name == "Play_Persistent_Music")
                    if ((byte)args.Item2[0] <= 51)
                        comment = IdTranslator.song_names[(byte)args.Item2[0] - 1];
                if (this.hasJump())
                    comment = (offset + getJump(args.Item2) + 1).ToString("X2");
                if (comment != "")
                    return $"{result} #{comment}";
                return $"{result}";
            }

            public Tuple<string, List<object>> getArgString(byte[] file, uint offset)
            {
                List<string> result = new List<string>();
                List<object> argValues = new List<object>();
                foreach (OperationArgument arg in args)
                {
                    Tuple<string, int, object> res = arg.parseData(file, offset);
                    offset += (uint)res.Item2;
                    result.Add(res.Item1);
                    argValues.Add(res.Item3);
                }
                return new Tuple<string, List<object>>(string.Join(", ", result), argValues);
            }
        }
    }
}
