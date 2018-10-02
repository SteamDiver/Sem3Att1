namespace libTask2
{
    public class CustomProgrammer : Programmer
    {
        //correct programs
        public int P { get; } 
        
        public CustomProgrammer(string sername, int programCount, int langCount, int p) : base(sername, programCount, langCount)
        {
            P = p;
        }

        public override double CalcQ()
        {
            return base.CalcQ() * P / ProgramCount;
        }
    }
}