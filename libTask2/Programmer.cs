namespace libTask2
{
    public class Programmer
    {
        public string Surname { get; }
        public int ProgramCount { get; }
        public int LangCount { get; }

        public Programmer(string surname, int programCount, int langCount)
        {
            Surname = surname;
            ProgramCount = programCount;
            LangCount = langCount;
        }

        public virtual double CalcQ() => ProgramCount * LangCount;

        public double Q() => CalcQ();
    }
}