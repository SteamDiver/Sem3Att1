namespace libTask2
{
    public class Programmer
    {
        protected string Sername { get; }
        protected int ProgramCount { get; }
        protected int LangCount { get; }

        public Programmer(string sername, int programCount, int langCount)
        {
            Sername = sername;
            ProgramCount = programCount;
            LangCount = langCount;
        }

        protected virtual int GetQ() => ProgramCount * LangCount;

        public string GetInfo()
        {
            return $"Sername: {Sername}; ProgCount: {ProgramCount}; LangCount: {LangCount}; Q: {GetQ()}";
        }
    }
}