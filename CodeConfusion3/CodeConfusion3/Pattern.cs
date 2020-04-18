using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeConfusion3
{
    class Pattern
    {
        
    }

    class SinglePattern: Pattern
    {
        
    }

    class AnyBasicTokenPattern : SinglePattern
    {

    }

    class StringPattern : SinglePattern
    {
        public string _string;
        public StringPattern(string mString)
        {
            _string = mString;
        }
    }

    class CompoundPattern
        : Pattern
    {
        public Pattern A;
        public Operator.Operator.eType eOperatorType;
        public Pattern B;        
    }
    class CompoundPatternList : Pattern
    {
        public List<Pattern> _patterns;
        public Operator.Operator.eType eOperatorType;        
    }
    class FreeCombination:Pattern
    {
        public List<Pattern> _patterns; 
    }
    class ListPattern : Pattern
    {
        public Pattern  _pattern; 
    }

    class NotPattern:Pattern
    {
        public NotPattern(Pattern pattern)
        {
            _pattern = pattern;
        }
        public Pattern _pattern;
    }
    class NotPatternList : Pattern
    {
        public List<Pattern> _patterns = new List<Pattern>();
    }

    class PatternLinkedWith : Pattern
    {
        public List<Pattern> BaseList;
        public List<Pattern> _LinkList;
    }
}
