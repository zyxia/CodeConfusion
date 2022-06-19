using System;
using System.Collections.Generic;
using TokenParser;

namespace MathParser
{
    partial class Scanner
    {
         public abstract class Node
    {
        public static Node Root = null;

        public virtual bool Contain(string param)
        {
            return false;
        }

        public virtual int ParamCount()
        {
            return 0;
        }

        public virtual void GetParamList(List<string> paramList)
        {
        }

        public virtual void ToFunction(out Function function)
        {
            throw new NotImplementedException();
        }
    }

    public class SinNode : Node
    {
        Node _child;

        public SinNode(Node child)
        {
            this._child = child;
        }

        public override bool Contain(string param)
        {
            return _child.Contain(param);
        }

        public override int ParamCount()
        {
            return _child.ParamCount();
        }

        public override void GetParamList(List<string> paramList)
        {
            _child.GetParamList(paramList);
        }
        public override void ToFunction(out Function function)
        {
            _child.ToFunction(out var @internal);
            function = new Sin(@internal);
        }
    }

    public class CosNode : Node
    {
        Node _child;

        public CosNode(Node child)
        {
            this._child = child;
        }

        public override bool Contain(string param)
        {
            return _child.Contain(param);
        }

        public override int ParamCount()
        {
            return _child.ParamCount();
        }

        public override void GetParamList(List<string> paramList)
        {
            _child.GetParamList(paramList);
        }
        public override void ToFunction(out Function function)
        {
            _child.ToFunction(out var @internal);
            function = new Cos(@internal);
        }
    }

    public class PlusNode : Node
    {
        Node _left;
        Node _right;

        public PlusNode(Node lfs, Node right)
        {
            this._left = lfs;
            this._right = right;
        }

        public override bool Contain(string param)
        {
            return _left.Contain(param) || _right.Contain(param);
        }

        public override int ParamCount()
        {
            var list = new List<string>();
            _left.GetParamList(list);
            _right.GetParamList(list);
            return list.Count;
        }

        public override void GetParamList(List<string> paramList)
        {
            _left.GetParamList(paramList);
            _right.GetParamList(paramList);
        }
        public override void ToFunction(out Function function)
        {
            _left.ToFunction(out var leftF);
            _right.ToFunction(out var rightF);
            function = new AddFunction(leftF,rightF);
        }
    }

    public class MinusNode : Node
    {
        Node _left;
        Node _right;

        public MinusNode(Node lfs, Node right)
        {
            this._left = lfs;
            this._right = right;
        }

        public override bool Contain(string param)
        {
            return _left.Contain(param) || _right.Contain(param);
        }

        public override int ParamCount()
        {
            var list = new List<string>();
            _left.GetParamList(list);
            _right.GetParamList(list);
            return list.Count;
        }

        public override void GetParamList(List<string> paramList)
        {
            _left.GetParamList(paramList);
            _right.GetParamList(paramList);
        }
        public override void ToFunction(out Function function)
        {
            _left.ToFunction(out var leftF);
            _right.ToFunction(out var rightF);
            function = new SubFunction(leftF,rightF);
        }
    }

    public class MulNode : Node
    {
        Node _left;
        Node _right;

        public MulNode(Node lfs, Node right)
        {
            this._left = lfs;
            this._right = right;
        }

        public override bool Contain(string param)
        {
            return _left.Contain(param) || _right.Contain(param);
        }

        public override int ParamCount()
        {
            var list = new List<string>();
            _left.GetParamList(list);
            _right.GetParamList(list);
            return list.Count;
        }

        public override void GetParamList(List<string> paramList)
        {
            _left.GetParamList(paramList);
            _right.GetParamList(paramList);
        }
        public override void ToFunction(out Function function)
        {
            _left.ToFunction(out var leftF);
            _right.ToFunction(out var rightF);
            function = new MulFunction(leftF,rightF);
        }
    }

    public class DivideNode : Node
    {
        Node _left;
        Node _right;

        public DivideNode(Node lfs, Node right)
        {
            this._left = lfs;
            this._right = right;
        }

        public override bool Contain(string param)
        {
            return _left.Contain(param) || _right.Contain(param);
        }

        public override int ParamCount()
        {
            var list = new List<string>();
            _left.GetParamList(list);
            _right.GetParamList(list);
            return list.Count;
        }

        public override void GetParamList(List<string> paramList)
        {
            _left.GetParamList(paramList);
            _right.GetParamList(paramList);
        }
        
        public override void ToFunction(out Function function)
        {
            _left.ToFunction(out var leftF);
            _right.ToFunction(out var rightF);
            function = new DivideFunction(leftF,rightF);
        }
    }

    public class VariableNode : Node
    {
        string _paramName;

        public VariableNode(string param)
        {
            this._paramName = param;
        }

        public override bool Contain(string param)
        {
            return _paramName == param;
        }

        public override int ParamCount()
        {
            return 1;
        }

        public override void GetParamList(List<string> paramList)
        {
            if (!paramList.Contains(_paramName))
                paramList.Add(_paramName);
        }
        public override void ToFunction(out Function function)
        {
            function = new VariableFunction(_paramName);
        }
    }

    public class ConstNode : Node
    {
        float _value;

        public ConstNode(string param)
        {
            this._value = float.Parse(param);
        }
        public override void ToFunction(out Function function)
        {
            function = new ConstValueFunction(_value);
        }
    }
    }
   
}