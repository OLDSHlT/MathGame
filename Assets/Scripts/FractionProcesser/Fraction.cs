using System;
namespace fractionProcessor
{
    /*
    处理分数的类，使用策略设计模式来更换分数生成器
    */
    public class FractionProcessor{
        private int divisor;//除数
        private int dividend;//被除数
        private int commonDivisior;//公约数
        private IFractionGenerator fractionGenerator;
        public FractionProcessor(IFractionGenerator generator){
            this.fractionGenerator = generator;
            //生成分数
            GenerateFraction();
            GetMaxCommonDivisior();
        }
        public void Reduction(int commonDivisior){ 
            if(IsReducible(commonDivisior)){
                this.divisor = (int)this.divisor/commonDivisior;
                this.dividend = (int)this.dividend/commonDivisior;
            }            
        }
        private Boolean IsReducible(int commonDivisior){
            if(this.divisor%commonDivisior == 0 && this.dividend%commonDivisior == 0){
                return true;
            }
            else{
                return false;
            }
        }
        private void GenerateFraction(){
            var result = this.fractionGenerator.GenerateFraction();
            this.divisor = result.Item1;
            this.dividend = result.Item2;
        }
        public int GetCommonDivisior(){
            return this.commonDivisior;
        }
        public int GetDivisor()
        {
            return this.divisor;
        }
        public int GetDividend()
        {
            return this.dividend;
        }
        public override string ToString(){
            return this.divisor + "/" + this.dividend + " common divisor: " + commonDivisior;
        }
        //更相消减法求最大公约数
        public void GetMaxCommonDivisior(){
            int number1 = this.divisor;
            int number2 = this.dividend;
            int number3 = -1;
            if(divisor > dividend){
                number1 = divisor;
                number2 = dividend;
            }else{
                number2 = divisor;
                number1 = dividend;
            }            
            while(number1 - number2 != 0){
                number3 = number1 - number2;
                //Console.WriteLine(number1 + " - " + number2 + " = " + number3);
                if(number2 > number3){
                    number1 = number2;
                    number2 = number3;
                }else{
                    number1 = number3;
                }
            }
            this.commonDivisior = number1;
        }
        // 判断一个分数是否为最简分数
        public bool IsSimplestFraction()
        {
            GetMaxCommonDivisior();
            if(commonDivisior == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    /*
    分数生成器接口
    */
    public interface IFractionGenerator{
        public (int,int) GenerateFraction();
    }
    /*
    生成能够被2约分的分数,值域为4-100
    */
    public class FractionGeneratorWith2 : IFractionGenerator{
        public (int,int) GenerateFraction(){
            Random random = new();
            int number1 = random.Next(2,50);
            int number2 = random.Next(2,50);
            //防止出现分子分母相同的情况
            if(number1 == number2){
                int changeNum = random.Next(1,3);
                if(changeNum == 1){
                    number1 += 2;
                }else if(changeNum == 2){
                    number1 += 4;
                }else if(changeNum == 3){
                    number1 += 6;
                }else{
                    number1 += 8;
                }
            }
            return (2*number1,2*number2);
        }
    }
    public class FractionGeneratorWith3 : IFractionGenerator{
        public (int,int) GenerateFraction(){
            Random random = new();
            int number1 = random.Next(3,33);
            int number2 = random.Next(3,33);
            //防止出现分子分母相同的情况
            if(number1 == number2){
                int changeNum = random.Next(1,3);
                if(changeNum == 1){
                    number1 += 3;
                }else if(changeNum == 2){
                    number1 += 6;
                }else if(changeNum == 3){
                    number1 += 9;
                }else{
                    number1 += 12;
                }
            }
            return (3*number1,3*number2);
        }
    }
    public class BigFractionGenerator : IFractionGenerator
    {
        static Random random = new Random();
        public (int, int) GenerateFraction()
        {
            // 生成不是最简分数的分数
            int numeratorMultipler = GenerateNumberInRange(1, 27);
            int denominatorMultipler = GenerateNumberInRange(1, 27);
            while(numeratorMultipler == denominatorMultipler)
            {
                numeratorMultipler = GenerateNumberInRange(1, 27);
                denominatorMultipler = GenerateNumberInRange(1, 27);
            }
            int gcd = GenerateNumberInRange(11, 37);
            int numerator = gcd * numeratorMultipler;
            int denominator = gcd * denominatorMultipler;
            return (numerator, denominator);
        }
        static int GenerateNumberInRange(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}