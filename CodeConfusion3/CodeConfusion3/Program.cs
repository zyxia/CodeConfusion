using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConfusion3
{
    class Program
    {
        static NotPatternList notfuhao_keyWolrdPatten = new NotPatternList();
      
        static void Main(string[] args)
        {
            //1 单位模式识别->单个token的识别
            //2 复合模式识别
            // 2.1 关系运算"V":
            /*  当存在一种模式A使得A既可以是模式B也可以是模式C，我们就称：A = B || C,其中B和C既可以是单位模式，也可以是复合模式
             * 2.2 关系运算"连接"
             *  当存在一种模式A使得A等价于模式B和模式C紧紧的挨在一起，我们就称A=BC,其中B和C既可以是单位模式，也可以是复合模式
             * 2.3 关系运行"非"
             *  当存在一种模式A等价于"不符合模式B"，则我们称A=~B
             * 2.4 关系运行"并且"
             *  当存在一种模式A使得该模式及符合模式B又符合模式C，我们称A = (B 并且 C)或 A = (B and C)
             * 2.5 关系运行"列表"
             *  当存在一种模式A代表着大量的模式B连接在一起，我们称A = List<B>;
             * 2.6 关系运算"不重复组合"
             *  在list B中存在着各种模式，当存在一种模式A，代表着B中的各种模式不重复的组合在一起，就称A =free combination(B)
             * 2.7 关系运算"严格轮换组合"
             *  有两个模式List B 和 C ，当存在一种模式A,代表着B和C中的模式，严格的轮换出现，并且以C中的模式结尾，就称A = List C linked with B
             */

            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern(";"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern(","));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern(":"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("using"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("class"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("static"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("void"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("int"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("float"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("double"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("short"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("new"));
            notfuhao_keyWolrdPatten._patterns.Add(new StringPattern("string"));

            CompoundPattern UsingScetence = new CompoundPattern();
            UsingScetence.A = getUsingPattern();
            UsingScetence.eOperatorType = Operator.Operator.eType.eLink;
            UsingScetence.B = new StringPattern(";");
            
            //1.List<using scetence>
            ListPattern usingScetenceList = new ListPattern();
            usingScetenceList._pattern = UsingScetence;

            //1.List<ClassOrNameSpace>
            ListPattern ClassOrNameSpaceList = new ListPattern();

            Pattern NameSpace = getNameSpacePattern(ClassOrNameSpaceList);
            Pattern Class = getClassPattern();

            CompoundPattern ClassOrNameSpace = new CompoundPattern();
            ClassOrNameSpace.eOperatorType = Operator.Operator.eType.eOr;
            ClassOrNameSpace.A = Class;
            ClassOrNameSpace.B = NameSpace;
             
            usingScetenceList._pattern = ClassOrNameSpace;

             
        }

        public static Pattern getNameSpacePattern(ListPattern ClassOrNameSpaceList)
        {
            CompoundPatternList namespacePattern = new CompoundPatternList();
            namespacePattern.eOperatorType = Operator.Operator.eType.eLink;
            namespacePattern._patterns.Add(new StringPattern("namespace"));
            namespacePattern._patterns.Add(getNameSpaceNamePattern());
            namespacePattern._patterns.Add(new StringPattern("{"));
            namespacePattern._patterns.Add(ClassOrNameSpaceList);
            //暂时卡在嵌套定义上 namespace or class
            namespacePattern._patterns.Add(new StringPattern("}"));
            return namespacePattern;
        }

        public static Pattern getClassPattern()
        {
            CompoundPatternList classPattern = new CompoundPatternList();
            classPattern.eOperatorType = Operator.Operator.eType.eLink;
            classPattern._patterns.Add(getClassModifierPattern());
            classPattern._patterns.Add(new StringPattern("class"));
            classPattern._patterns.Add(getNormalTokenPattern());
            classPattern._patterns.Add(new StringPattern("{"));
            //属性定义，函数定义，表达式，句子

            classPattern._patterns.Add(getPropertyDefineOrFunctionDefineList());
            classPattern._patterns.Add(new StringPattern("}"));
            return classPattern;
        }
        public static Pattern getPropertyDefineOrFunctionDefineList()
        {
            ListPattern PropertyDefineOrFunctionDefineList = new ListPattern();
            PropertyDefineOrFunctionDefineList._pattern = getPropertyDefineOrFunctionDefine();
            return PropertyDefineOrFunctionDefineList;
        }

        public static Pattern getPropertyDefineOrFunctionDefine()
        {
            CompoundPattern PropertyDefineOrFunctionDefine = new CompoundPattern();
            PropertyDefineOrFunctionDefine.eOperatorType = Operator.Operator.eType.eOr;
            PropertyDefineOrFunctionDefine.A = getPropertyDefine();
            PropertyDefineOrFunctionDefine.B = getFunctionDefine();
            return PropertyDefineOrFunctionDefine;
        }

        public static Pattern getPropertyDefine()
        {
            CompoundPattern equle = new CompoundPattern();
            equle.A = new StringPattern("=");
            equle.B = getExpresionPattern();
            equle.eOperatorType = Operator.Operator.eType.eLink;

            CompoundPattern nullOrEaule = new CompoundPattern();
            nullOrEaule.A = null;
            nullOrEaule.B = equle;
            nullOrEaule.eOperatorType = Operator.Operator.eType.eOr;

            CompoundPatternList  Pattern1 = new CompoundPatternList();

            Pattern1.eOperatorType = Operator.Operator.eType.eLink;
            Pattern1._patterns.Add(getPropertyModifierPattern());
            Pattern1._patterns.Add(getTypePattern());
            Pattern1._patterns.Add(PropertyName());
            Pattern1._patterns.Add(nullOrEaule);
            Pattern1._patterns.Add(new StringPattern(";"));
            //暂时不考虑赋值操作

            return Pattern1;
        }

        public static Pattern getExpresionPattern()
        {
            CompoundPatternList UnitPatterns = new CompoundPatternList();
            UnitPatterns.eOperatorType = Operator.Operator.eType.eOr;
            UnitPatterns._patterns.Add(getNormalTokenPattern());
            UnitPatterns._patterns.Add(getFunctionCall());

        }
        public static Pattern getFunctionCall()
        { 

            PatternLinkedWith
            CompoundPattern _ParaList = new CompoundPattern();
            {
                CompoundPattern _ParaDotEnd = new CompoundPattern();
                _ParaDotEnd.A = new StringPattern(",");
                _ParaDotEnd.B = getNormalTokenPattern();
                _ParaDotEnd.eOperatorType = Operator.Operator.eType.eLink;

                _ParaList.A = getNormalTokenPattern();
                _ParaList.eOperatorType = Operator.Operator.eType.eLink;
                var listDotEnd = new ListPattern();
                listDotEnd._pattern = _ParaDotEnd;
                _ParaList.B = listDotEnd;
            }

            CompoundPattern nullOrParaList = new CompoundPattern();
            nullOrParaList.A = null;
            nullOrParaList.B = _ParaList;
            nullOrParaList.eOperatorType = Operator.Operator.eType.eOr;

            CompoundPatternList FunctionCall = new CompoundPatternList();
            FunctionCall.eOperatorType = Operator.Operator.eType.eLink;
            FunctionCall._patterns.Add(getNormalTokenPattern());
            FunctionCall._patterns.Add(new StringPattern("("));
            FunctionCall._patterns.Add(nullOrParaList);
            FunctionCall._patterns.Add(new StringPattern(")"));
            return FunctionCall;
        }
        public static Pattern getFunctionDefine()
        {

        }
        public static Pattern getTypePattern()
        {
            return getNormalTokenPattern();//临时
        }
        public static Pattern PropertyName()
        {
            return getNormalTokenPattern();//临时
        }
        public static Pattern getClassModifierPattern()
        {
            CompoundPatternList ModifierPattern = new CompoundPatternList();
            ModifierPattern.eOperatorType = Operator.Operator.eType.eOr;
            ModifierPattern._patterns.Add(new StringPattern("public"));
            ModifierPattern._patterns.Add(new StringPattern("private"));
            ModifierPattern._patterns.Add(new StringPattern("protected"));
            ModifierPattern._patterns.Add(new StringPattern("internal"));
            FreeCombination modifierPattern = new FreeCombination();
            modifierPattern._patterns.Add(new StringPattern("static"));
            modifierPattern._patterns.Add(ModifierPattern);
            return modifierPattern;
        }
        public static Pattern getPropertyModifierPattern()
        {
            CompoundPatternList ModifierPattern = new CompoundPatternList();
            ModifierPattern.eOperatorType = Operator.Operator.eType.eOr;
            ModifierPattern._patterns.Add(new StringPattern("public"));
            ModifierPattern._patterns.Add(new StringPattern("private"));
            ModifierPattern._patterns.Add(new StringPattern("protected"));
            FreeCombination modifierPattern = new FreeCombination();
            modifierPattern._patterns.Add(new StringPattern("static"));
            modifierPattern._patterns.Add(ModifierPattern);
            return modifierPattern;
        }
        public static Pattern getUsingPattern()
        { 
            CompoundPattern Using = new CompoundPattern();
            Using.A = new StringPattern("using");
            Using.eOperatorType = Operator.Operator.eType.eLink;
            Using.B = getNameSpaceNamePattern();
            return Using;
        }
        public static Pattern getNameSpaceNamePattern()
        { 
            CompoundPattern UsingDotEnd = new CompoundPattern();
            UsingDotEnd.A = new StringPattern(".");
            UsingDotEnd.B = getNormalTokenPattern();
            UsingDotEnd.eOperatorType = Operator.Operator.eType.eLink;
            CompoundPattern namespacenamePattern = new CompoundPattern();
            namespacenamePattern.A = getNormalTokenPattern();
            namespacenamePattern.eOperatorType = Operator.Operator.eType.eOr;
            CompoundPattern _using_bodyCombin = new CompoundPattern();
            {
                _using_bodyCombin.A = getNormalTokenPattern();
                _using_bodyCombin.eOperatorType = Operator.Operator.eType.eLink;
                var listDotEnd = new ListPattern();
                listDotEnd._pattern = UsingDotEnd;
                _using_bodyCombin.B = listDotEnd;
            }
            namespacenamePattern.B = _using_bodyCombin;
            return namespacenamePattern;
        }
        public static Pattern getNormalTokenPattern()
        {
            CompoundPattern normalToken = new CompoundPattern();
            normalToken.A = new AnyBasicTokenPattern();
            normalToken.B = notfuhao_keyWolrdPatten;
            normalToken.eOperatorType = Operator.Operator.eType.eAnd;
            return normalToken;
        }
    }
}
